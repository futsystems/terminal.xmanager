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
    public partial class fmCommission : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmCommission()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder_sel);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.items);
            tempateTree.ImageList = this.imageList1;
            tempateTree.Nodes.Add(CreateBaseItem("手续费率模板"));

            
            this.btnAddTemplate.Click += new EventHandler(btnAddTemplate_Click);
            commissionGrid.DoubleClick += new EventHandler(commissionGrid_DoubleClick);
            commissionGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(commissionGrid_RowPrePaint);

            this.Load += new EventHandler(fmCommission_Load);
        }

        bool _viewMode = false;
        int _viewTemplateId = 0;
        public void ShowTemplate(int templateid)
        {
            _viewMode = true;
            _viewTemplateId = templateid;
            treePanel.Visible = false;
            listPanel.Location = new Point(0, 0);
            this.Width = this.Width - treePanel.Width;

            this.ShowDialog();

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

        private ComponentFactory.Krypton.Toolkit.KryptonTreeNode CreateCommissionTemplateItem(CommissionTemplateSetting template)
        {
            ComponentFactory.Krypton.Toolkit.KryptonTreeNode item = new ComponentFactory.Krypton.Toolkit.KryptonTreeNode();
            item.Text = string.Format("{0}{1}",template.Name,template.Manager_ID!=CoreService.SiteInfo.Manager.ID?"*":"");
            item.ImageIndex = 0;
            item.SelectedImageIndex = 1;
            item.Tag = template;
            return item;
        }


        void fmCommission_Load(object sender, EventArgs e)
        {
            this.tempateTree.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.tempateTree.ContextMenuStrip.Items.Add("添加手续费模板", null, new EventHandler(Add_Click));
            this.tempateTree.ContextMenuStrip.Items.Add("删除手续费模板", null, new EventHandler(Del_Click));
            tempateTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(tempateTree_NodeMouseClick);

            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            //超级域 可以单独添加模板项
            if (CoreService.SiteInfo.Domain.Super)
            {
                commissionGrid.ContextMenuStrip = new ContextMenuStrip();
                commissionGrid.ContextMenuStrip.Items.Add("添加模板项目", null, new EventHandler(AddItem_Click));
            }


            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_TEMPLATE, this.OnQryCommissionTemplate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_COMMISSION_TEMPLATE, this.OnNotifyCommissionTemplate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_COMMISSION_TEMPLATE_DELETE, this.OnNotifyDelCommissionTemplate);

            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_ITEM, this.OnQryCommissionTemplateItem);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_COMMISSION_ITEM, this.OnNotifyCommissionTemplateItem);
            CoreService.TLClient.ReqQryCommissionTemplate();

            if (_viewMode)
            {
                QryTemplate(_viewTemplateId);
            }
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_TEMPLATE, this.OnQryCommissionTemplate);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_COMMISSION_TEMPLATE, this.OnNotifyCommissionTemplate);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_COMMISSION_TEMPLATE_DELETE, this.OnNotifyDelCommissionTemplate);

            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_ITEM, this.OnQryCommissionTemplateItem);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_COMMISSION_ITEM, this.OnNotifyCommissionTemplateItem);


        }

        bool _onqry = false;
        void tempateTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                return;
            if(e.Node.Parent != null)
            {
                if (e.Node.Parent.Index == 0)
                {
                    CommissionTemplateSetting t = e.Node.Tag as CommissionTemplateSetting;
                    if (t != null)
                    {
                        if (!_onqry)
                        {
                            QryTemplate(t.ID);
                        }
                        else
                        {
                            MoniterHelper.WindowMessage("正在查询中 请稍后");
                        }
                    }
                }
            }
        }

        void QryTemplate(int templateid)
        {
            _onqry = true;
            ClearItem();
            CoreService.TLClient.ReqQryCommissionTemplateItem(templateid);
        }


        void AddItem_Click(object sender, EventArgs e)
        {
            fmCommissionTemplateItemEdit fm = new fmCommissionTemplateItemEdit();
            try
            {
                if (tempateTree.SelectedNode.Parent != null)
                {
                    CommissionTemplateSetting t = tempateTree.SelectedNode.Tag as CommissionTemplateSetting;
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
            fmTemplateEdit fm = new fmTemplateEdit(TemplateEditType.Commission);
            fm.ShowDialog();
        }

        void btnAddTemplate_Click(object sender, EventArgs e)
        {
            fmTemplateEdit fm = new fmTemplateEdit(TemplateEditType.Commission);
            fm.ShowDialog();
        }

        void Del_Click(object sender, EventArgs e)
        { 
            if(tempateTree.SelectedNode.Parent != null)//父节点不为空 表面为二级节点
            {
                if (tempateTree.SelectedNode.Parent.Index == 0)//父节点 index为0 表面为二级节点
                {
                    CommissionTemplateSetting t = tempateTree.SelectedNode.Tag as CommissionTemplateSetting;
                    MessageBox.Show(t.Name);

                    if (MoniterHelper.WindowConfirm(string.Format("确认删除手续费模板:{0}?", t.Name)) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ClearItem();
                        CoreService.TLClient.ReqDelCommissionTemplate(t);
                    }

                }
            }
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
            commissionGrid.DataSource = null;
            itemrowmap.Clear();
            gt.Rows.Clear();
            BindToTable();
        }


        #region CommissioinTemplateItem
        private CommissionTemplateItemSetting CurrentItem
        {
            get
            {
                int row = commissionGrid.SelectedRows.Count > 0 ? commissionGrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return commissionGrid[TAG, row].Value as CommissionTemplateItemSetting;
                }
                else
                {
                    return null;
                }
            }
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

        void OnQryCommissionTemplateItem(string json, bool islast)
        {
            CommissionTemplateItemSetting obj = CoreService.ParseJsonResponse<CommissionTemplateItemSetting>(json);
            if (obj != null)
            {
                GotCommissionTemplateItem(obj);
            }
            if (islast)
            {
                _onqry = false;
            }
        }

        void OnNotifyCommissionTemplateItem(string json)
        {
            CommissionTemplateItemSetting obj = CoreService.ParseJsonResponse<CommissionTemplateItemSetting>(json);
            if (obj != null)
            {
                GotCommissionTemplateItem(obj);
            }
        }

        void GotCommissionTemplateItem(CommissionTemplateItemSetting item)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<CommissionTemplateItemSetting>(GotCommissionTemplateItem), new object[] { item });
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
                    gt.Rows[i][OPENBYMONEY] = item.OpenByMoney;
                    gt.Rows[i][OPENBYVOLUME] = item.OpenByVolume;
                    gt.Rows[i][CLOSETODAYBYMONEY] = item.CloseTodayByMoney;
                    gt.Rows[i][CLOSETODAYBYVOLUME] = item.CloseTodayByVolume;
                    gt.Rows[i][CLOSEBYMONEY] = item.CloseByMoney;
                    gt.Rows[i][CLOSEBYVOLUME] = item.CloseByVolume;
                    gt.Rows[i][PERCENT] = item.Percent;
                    gt.Rows[i][CHARGETYPE] = GetChargeTypeStr(item.ChargeType);// == QSEnumChargeType.Absolute ? "绝对" : "相对";

                    gt.Rows[i][TAG] = item;
                    itemrowmap.Add(item.ID, i);

                }
                else
                {
                    int i = r;
                    gt.Rows[i][OPENBYMONEY] = item.OpenByMoney;
                    gt.Rows[i][OPENBYVOLUME] = item.OpenByVolume;
                    gt.Rows[i][CLOSETODAYBYMONEY] = item.CloseTodayByMoney;
                    gt.Rows[i][CLOSETODAYBYVOLUME] = item.CloseTodayByVolume;
                    gt.Rows[i][CLOSEBYMONEY] = item.CloseByMoney;
                    gt.Rows[i][CLOSEBYVOLUME] = item.CloseByVolume;
                    gt.Rows[i][PERCENT] = item.Percent;
                    gt.Rows[i][CHARGETYPE] = GetChargeTypeStr(item.ChargeType);// == QSEnumChargeType.Absolute ? "绝对" : "相对";
                    gt.Rows[i][TAG] = item;
                }
            }
        }

        #endregion



        Dictionary<int, CommissionTemplateSetting> templatemap = new Dictionary<int, CommissionTemplateSetting>();
        void OnQryCommissionTemplate(string json, bool islast)
        {
            try
            {
                CommissionTemplateSetting item = CoreService.ParseJsonResponse<CommissionTemplateSetting>(json);
                if (item != null)
                {
                    templatemap.Add(item.ID, item);
                    InvokeGotCommissiontTemplateItem(item);
                }
                if (islast)
                {
                    tempateTree.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void OnNotifyDelCommissionTemplate(string json)
        {
            CommissionTemplateSetting obj = CoreService.ParseJsonResponse<CommissionTemplateSetting>(json);
            if (obj != null)
            {
                CommissionTemplateSetting template = tempateTree.SelectedNode.Tag as CommissionTemplateSetting;
                if (template.ID == obj.ID)
                {
                    tempateTree.SelectedNode.Parent.Nodes.Remove(tempateTree.SelectedNode);
                }
            }
        }
        void OnNotifyCommissionTemplate(string json)
        {
            CommissionTemplateSetting obj = CoreService.ParseJsonResponse<CommissionTemplateSetting>(json);
            if (obj != null)
            {
                CommissionTemplateSetting target = null;
                if (templatemap.TryGetValue(obj.ID, out target))
                {
                    target.Name = obj.Name;
                    target.Description = obj.Description;
                    tempateTree.Refresh();
                }
                else
                {
                    templatemap.Add(obj.ID, obj);
                    InvokeGotCommissiontTemplateItem(obj);
                }
                tempateTree.ExpandAll();
            }
        }
        void InvokeGotCommissiontTemplateItem(CommissionTemplateSetting commission)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<CommissionTemplateSetting>(InvokeGotCommissiontTemplateItem), new object[] { commission });
            }
            else
            {
                tempateTree.Nodes[0].Nodes.Add(CreateCommissionTemplateItem(commission));
            }
        }






        #region 表格
        #region 显示字段

        const string ID = "全局ID";
        const string CODE = "品种";
        const string MONTH = "月份";
        const string OPENBYMONEY = "开仓(金额)";
        const string OPENBYVOLUME = "开仓(手数)";
        const string CLOSETODAYBYMONEY = "平今(金额)";
        const string CLOSETODAYBYVOLUME = "平今(手数)";
        const string CLOSEBYMONEY = "平仓(金额)";
        const string CLOSEBYVOLUME = "平仓(手数)";
        const string PERCENT = "百分比";
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
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = commissionGrid;

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
            gt.Columns.Add(OPENBYMONEY);//
            gt.Columns.Add(OPENBYVOLUME);//
            gt.Columns.Add(CLOSETODAYBYMONEY);//
            gt.Columns.Add(CLOSETODAYBYVOLUME);//
            gt.Columns.Add(CLOSEBYMONEY);//
            gt.Columns.Add(CLOSEBYVOLUME);//
            gt.Columns.Add(PERCENT);
            gt.Columns.Add(CHARGETYPE);
            gt.Columns.Add(TAG, typeof(CommissionTemplateItemSetting));
        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = commissionGrid;
            datasource.DataSource = gt;
            grid.DataSource = datasource;

            grid.Columns[ID].Visible = false;
            grid.Columns[TAG].Visible = false;

            grid.Columns[ID].Width = 60;
            grid.Columns[CODE].Width =60;
            grid.Columns[MONTH].Width = 60;
        }



        void commissionGrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }
        void commissionGrid_DoubleClick(object sender, EventArgs e)
        {
            if (_viewMode) return;

            CommissionTemplateItemSetting item = CurrentItem;
            if (item == null)
            {
                MoniterHelper.WindowMessage("请选择需要编辑的手续费模板项目");
                return;
            }

            fmCommissionTemplateItemEdit fm = new fmCommissionTemplateItemEdit();
            fm.SetCommissionTemplateItem(item);
            //fm.SetCommissionTemplateItems(itemmap.Values);
            fm.ShowDialog();
        }



        #endregion


    }
}
