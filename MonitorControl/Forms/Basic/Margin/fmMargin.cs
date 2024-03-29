﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;



namespace TradingLib.MoniterControl
{
    public partial class fmMargin : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmMargin()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder_sel);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.items);
            templateTree.ImageList = this.imageList1;


            templateTree.Nodes.Add(CreateBaseItem("保证金率模板"));

            this.Load += new EventHandler(fmCommission_Load);
        }

        private ComponentFactory.Krypton.Toolkit.KryptonTreeNode CreateBaseItem(string lb)
        {
            ComponentFactory.Krypton.Toolkit.KryptonTreeNode item = new ComponentFactory.Krypton.Toolkit.KryptonTreeNode();
            item.Text = lb;
            item.ImageIndex = 2;
            item.SelectedImageIndex = item.ImageIndex;
            item.Tag = lb;
            return item;
        }

        private ComponentFactory.Krypton.Toolkit.KryptonTreeNode CreateMarginTemplateItem(MarginTemplateSetting template)
        {
            ComponentFactory.Krypton.Toolkit.KryptonTreeNode item = new ComponentFactory.Krypton.Toolkit.KryptonTreeNode();
            item.Text = string.Format("{0}{1}", template.Name, template.Manager_ID != CoreService.SiteInfo.Manager.ID ? "*" : "");
            item.ImageIndex = 0;
            item.SelectedImageIndex = 1;
            item.Tag = template;
            return item;
        }

        void fmCommission_Load(object sender, EventArgs e)
        {
            this.templateTree.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.templateTree.ContextMenuStrip.Items.Add("添加保证金模板", null, new EventHandler(Add_Click));
            this.templateTree.ContextMenuStrip.Items.Add("删除保证金模板", null, new EventHandler(Del_Click));

            marginGrid.DoubleClick += new EventHandler(commissionGrid_DoubleClick);
            marginGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(commissionGrid_RowPrePaint);

            templateTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(templateTree_NodeMouseClick);
            btnAddTemplate.Click += new EventHandler(btnAddTemplate_Click);
            CoreService.EventCore.RegIEventHandler(this);
        }

        void btnAddTemplate_Click(object sender, EventArgs e)
        {
            fmTemplateEdit fm = new fmTemplateEdit(TemplateEditType.Margin);
            fm.ShowDialog();
        }
        bool _onqry = false;
        void templateTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                return;
            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Index == 0)
                {
                    MarginTemplateSetting t = e.Node.Tag as MarginTemplateSetting;
                    if (t != null)
                    {
                        if (!_onqry)
                        {
                            _onqry = true;
                            ClearItem();
                            CoreService.TLClient.ReqQryMarginTemplateItem(t.ID);
                        }
                        else
                        {
                            MoniterHelper.WindowMessage("正在查询中 请稍后");
                        }
                    }
                }
            }
        }

        void commissionGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }


        void commissionGrid_DoubleClick(object sender, EventArgs e)
        {
            MarginTemplateItemSetting item = CurrentItem;
            if (item == null)
            {
                MoniterHelper.WindowMessage("请选择需要编辑的保证金模板项目");
                return;
            }
            fmMarginTemplateItemEdit fm = new fmMarginTemplateItemEdit();
            fm.SetMarginTemplateItem(item);
            fm.ShowDialog();
        }

        //得到当前选择的行号
        private MarginTemplateItemSetting CurrentItem
        {
            get
            {
                int row = marginGrid.SelectedRows.Count > 0 ? marginGrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return marginGrid[TAG, row].Value as MarginTemplateItemSetting;
                }
                else
                {
                    return null;
                }
            }
        }


        void AddItem_Click(object sender, EventArgs e)
        {

            fmMarginTemplateItemEdit fm = new fmMarginTemplateItemEdit();
            try
            {
                if (templateTree.SelectedNode.Parent != null)
                {
                    MarginTemplateSetting t = templateTree.SelectedNode.Tag as MarginTemplateSetting;
                    if (t != null)
                    {
                        fm.SetCommissionTemplateID(t.ID);
                        fm.ShowDialog();
                    }

                }
            }
            catch (Exception ex)
            {
                MoniterHelper.WindowMessage("请选择模板");
            }
        }

        void Add_Click(object sender, EventArgs e)
        {
            fmTemplateEdit fm = new fmTemplateEdit(TemplateEditType.Margin);
            fm.ShowDialog();
        }

        void Del_Click(object sender, EventArgs e)
        {
            if (templateTree.SelectedNode.Parent != null)//父节点不为空 表面为二级节点
            {
                if (templateTree.SelectedNode.Parent.Index == 0)//父节点 index为0 表面为二级节点
                {
                    MarginTemplateSetting t = templateTree.SelectedNode.Tag as MarginTemplateSetting;

                    if (MoniterHelper.WindowConfirm(string.Format("确认删除保证金模板:{0}?", t.Name)) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ClearItem();
                        CoreService.TLClient.ReqDelMarginTemplate(t);
                    }
                }
            }
        }

        public void OnInit()
        {
            //超级管理员可以单独添加模板项
            if (CoreService.SiteInfo.Domain.Super)
            {
                marginGrid.ContextMenuStrip = new ContextMenuStrip();
                marginGrid.ContextMenuStrip.Items.Add("添加模板项目", null, new EventHandler(AddItem_Click));
            }


            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH,Method_MGR_EXCH.QRY_MARGIN_TEMPLATE, this.OnQryMarginTemplate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MARGIN_TEMPLATE, this.OnNotifyMarginTemplate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MARGIN_TEMPLATE_DELETE, this.OnNotifyDelMarginTemplate);

            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_ITEM, this.OnQryMarginTemplateItem);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MARGIN_ITEM, this.OnNotifyMarginTemplateItem);
            CoreService.TLClient.ReqQryMarginTemplate();
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_TEMPLATE, this.OnQryMarginTemplate);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MARGIN_TEMPLATE, this.OnNotifyMarginTemplate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MARGIN_TEMPLATE_DELETE, this.OnNotifyDelMarginTemplate);

            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_ITEM, this.OnQryMarginTemplateItem);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MARGIN_ITEM, this.OnNotifyMarginTemplateItem);
        }

        string GetChargeTypeStr(QSEnumChargeType type)
        {
            switch (type)
            { 
                case QSEnumChargeType.Absolute:
                    return "绝对";
                case QSEnumChargeType.Percent:
                    return "百分比";
                case QSEnumChargeType.Relative:
                    return "相对";
                default:
                    return "";
            }
        }
        void ClearItem()
        {
            marginGrid.DataSource = null;
            itemrowmap.Clear();
            gt.Rows.Clear();
            BindToTable();
        }


        Dictionary<int, int> itemrowmap  = new Dictionary<int, int>();

        int ItemIdx(int id)
        {
            int rowid = -1;
            if (itemrowmap.TryGetValue(id, out rowid))
            {
                return rowid;
            }
            else
            {
                return -1;
            }
        }

        void OnQryMarginTemplateItem(string json, bool islast)
        {
            MarginTemplateItemSetting obj = CoreService.ParseJsonResponse<MarginTemplateItemSetting>(json);
            if (obj != null)
            {
                    GotCommissionTemplateItem(obj);
            }
            if(islast)
            {
               _onqry = false;
            }
        }

        void OnNotifyMarginTemplateItem(string json)
        {
            MarginTemplateItemSetting obj = CoreService.ParseJsonResponse<MarginTemplateItemSetting>(json);
            if (obj != null)
            {
                GotCommissionTemplateItem(obj);
            }
        }

        void GotCommissionTemplateItem(MarginTemplateItemSetting item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<MarginTemplateItemSetting>(GotCommissionTemplateItem), new object[] { item });
            }
            else
            {
                int r = ItemIdx(item.ID);
                if (r == -1)
                {
                    gt.Rows.Add(item.ID);
                    int i = gt.Rows.Count - 1;
                    gt.Rows[i][CODE] = item.Code;
                    gt.Rows[i][MONTH] = item.Month;
                    gt.Rows[i][MARGINBYMONEY] = item.MarginByMoney;
                    gt.Rows[i][MARGINBYVOLUME] = item.MarginByVolume;
                    gt.Rows[i][PERCENT] = item.Percent;
                    gt.Rows[i][CHARGETYPE] = GetChargeTypeStr(item.ChargeType);// == QSEnumChargeType.Absolute ? "绝对" : "相对";
                    gt.Rows[i][TAG] = item;
                    
                    itemrowmap.Add(item.ID, i);

                }
                else
                {
                    int i = r;
                    gt.Rows[i][MARGINBYMONEY] = item.MarginByMoney;
                    gt.Rows[i][MARGINBYVOLUME] = item.MarginByVolume;
                    gt.Rows[i][PERCENT] = item.Percent;
                    gt.Rows[i][CHARGETYPE] = GetChargeTypeStr(item.ChargeType);// == QSEnumChargeType.Absolute ? "绝对" : "相对";
                    gt.Rows[i][TAG] = item;
                }
            }
        }

        Dictionary<int, MarginTemplateSetting> templatemap = new Dictionary<int, MarginTemplateSetting>();
        void OnQryMarginTemplate(string json, bool islast)
        {
            MarginTemplateSetting item = CoreService.ParseJsonResponse<MarginTemplateSetting>(json);
            if (item != null)
            {
                templatemap.Add(item.ID, item);
                InvokeGotMarginTemplate(item);
            }
            if (islast)
            {
                templateTree.ExpandAll();
            }
        }

        void InvokeGotMarginTemplate(MarginTemplateSetting margin)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<MarginTemplateSetting>(InvokeGotMarginTemplate), new object[] { margin });
            }
            else
            {
                templateTree.Nodes[0].Nodes.Add(CreateMarginTemplateItem(margin));
            }
        }

        void OnNotifyDelMarginTemplate(string json)
        {
            MarginTemplateSetting obj = CoreService.ParseJsonResponse<MarginTemplateSetting>(json);
            if (obj != null)
            {
                MarginTemplateSetting template = templateTree.SelectedNode.Tag as MarginTemplateSetting;
                if (template.ID == obj.ID)
                {
                    templateTree.SelectedNode.Parent.Nodes.Remove(templateTree.SelectedNode);
                }
            }
        }

        void OnNotifyMarginTemplate(string json)
        {
            MarginTemplateSetting obj = CoreService.ParseJsonResponse<MarginTemplateSetting>(json);
            if (obj != null)
            {
                MarginTemplateSetting target = null;
                if (templatemap.TryGetValue(obj.ID,out target))
                {
                    target.Name = obj.Name;
                    target.Description = obj.Description;
                    templateTree.Refresh();
                }
                else
                {
                    templatemap.Add(obj.ID, obj);
                    InvokeGotMarginTemplate(obj);
                }
                templateTree.ExpandAll();
            }
        }




        #region 表格
        #region 显示字段

        const string ID = "全局ID";
        const string CODE = "品种";
        const string MONTH = "月份";
        const string MARGINBYMONEY = "保证金(金额)";
        const string MARGINBYVOLUME = "保证金(数量)";
        const string PERCENT = "上浮比例";
        const string CHARGETYPE = "计算方式";
        const string TAG = "TAG";
        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = marginGrid;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeight = 25;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            grid.StateCommon.Background.Color1 = Color.WhiteSmoke;
            grid.StateCommon.Background.Color2 = Color.WhiteSmoke;

        }

        //初始化Account显示空格
        private void InitTable()
        {
            gt.Columns.Add(ID);//
            gt.Columns.Add(CODE);//
            gt.Columns.Add(MONTH);//
            gt.Columns.Add(MARGINBYMONEY);//
            gt.Columns.Add(MARGINBYVOLUME);//
            gt.Columns.Add(PERCENT);
            gt.Columns.Add(CHARGETYPE);
            gt.Columns.Add(TAG, typeof(MarginTemplateItemSetting));
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = marginGrid;
            datasource.DataSource = gt;
            grid.DataSource = datasource;

            grid.Columns[ID].Visible = false;
            grid.Columns[TAG].Visible = false;
            grid.Columns[ID].Width = 60;
            grid.Columns[CODE].Width =60;
            grid.Columns[MONTH].Width = 60;

        }





        #endregion


    }
}
