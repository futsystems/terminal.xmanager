using System;
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
    public partial class fmExStrategy : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmExStrategy()
        {
            InitializeComponent();

            MoniterHelper.AdapterToIDataSource(margin).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumMarginStrategy>());
            MoniterHelper.AdapterToIDataSource(avabilefund).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumAvabileFundStrategy>());

            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder_sel);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.items);
            templateTree.ImageList = this.imageList1;


            templateTree.Nodes.Add(CreateBaseItem("交易参数模板"));

            btnSubmit.Click += new EventHandler(btnSubmit_Click);
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

        private ComponentFactory.Krypton.Toolkit.KryptonTreeNode CreateExStrategyTemplateItem(ExStrategyTemplateSetting template)
        {
            ComponentFactory.Krypton.Toolkit.KryptonTreeNode item = new ComponentFactory.Krypton.Toolkit.KryptonTreeNode();
            item.Text = template.ToString();
            item.ImageIndex = 0;
            item.SelectedImageIndex = 1;
            item.Tag = template;
            return item;
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {

            if (_current == null)
            { 
                MoniterHelper.WindowMessage("请选择要编辑的交易参数模板");
                return;
            }

            _current.SideMargin = sidemargin.Checked;
            _current.CreditSeparate = creditseparate.Checked;
            _current.Margin = (QSEnumMarginStrategy)margin.SelectedValue;
            _current.AvabileFund = (QSEnumAvabileFundStrategy)avabilefund.SelectedValue;
            _current.PositionLock = poslock.Checked;

            CoreService.TLClient.ReqUpdateExStrategyTemplateItem(_current);
        }

        void fmCommission_Load(object sender, EventArgs e)
        {
            //this.templatelist.ContextMenuStrip = new ContextMenuStrip();
            //this.templatelist.ContextMenuStrip.Items.Add("添加模板", null, new EventHandler(Add_Click));
            //this.templatelist.ContextMenuStrip.Items.Add("修改模板", null, new EventHandler(Edit_Click));
            //this.templatelist.ContextMenuStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());
            //this.templatelist.ContextMenuStrip.Items.Add("加载数据", null, new EventHandler(Qry_Click));

            //commissionGrid.ContextMenuStrip = new ContextMenuStrip();
            //commissionGrid.ContextMenuStrip.Items.Add("添加模板项目", null, new EventHandler(AddItem_Click));
            this.templateTree.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.templateTree.ContextMenuStrip.Items.Add("添加交易参数模板", null, new EventHandler(Add_Click));

            templateTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(templateTree_NodeMouseClick);

            CoreService.EventCore.RegIEventHandler(this);
            //this.templatelist.ContextMenuStrip.Items.Add("添加模板", null, new EventHandler(Add_Click));
            //commissionGrid.DoubleClick += new EventHandler(commissionGrid_DoubleClick);
            //commissionGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(commissionGrid_RowPrePaint);
        }


        void templateTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                return;
            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Index == 0)
                {
                    ExStrategyTemplateSetting t = e.Node.Tag as ExStrategyTemplateSetting;
                    if (t != null)
                    {
                        ClearItem();
                        CoreService.TLClient.ReqQryExStrategyTemplateItem(t.ID);
                    }
                }
            }
        }


        //void AddItem_Click(object sender, EventArgs e)
        //{

        //    fmCommissionTemplateItemEdit fm = new fmCommissionTemplateItemEdit();
        //    try
        //    {
        //        int id = int.Parse(templateid.Text);
        //        fm.SetCommissionTemplateID(id);
        //        fm.ShowDialog();
        //    }
        //    catch (Exception ex)
        //    {
        //        MoniterHelper.WindowMessage("请选择模板");
        //    }
        //}

        //void Qry_Click(object sender, EventArgs e)
        //{
        //    ExStrategyTemplateSetting t = templatelist.SelectedItem as ExStrategyTemplateSetting;
        //    if (t == null)
        //    {
        //        MoniterHelper.WindowMessage("请选择交易参数模板");
        //        return;
        //    }
        //    ClearItem();
        //    templatename.Text = t.Name;
        //    templateid.Text = t.ID.ToString();
        //    CoreService.TLClient.ReqQryExStrategyTemplateItem(t.ID);
        //}

        void Add_Click(object sender, EventArgs e)
        {
            fmTemplateEdit fm = new fmTemplateEdit(TemplateEditType.Strategy);
            fm.ShowDialog();
        }
        //void Edit_Click(object sender, EventArgs e)
        //{
        //    ExStrategyTemplateSetting t = templatelist.SelectedItem as ExStrategyTemplateSetting;
        //    fmTemplateEdit fm = new fmTemplateEdit(TemplateEditType.Strategy);

        //    fm.SetTemplate(t);
        //    fm.ShowDialog();
        //}

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryExStrategyTemplate", this.OnQryExStrategyTemplate);
            CoreService.EventContrib.RegisterNotifyCallback("MgrExchServer", "NotifyExStrategyTemplate", this.OnNotifyExStrategyTemplate);

            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryExStrategyTemplateItem", this.OnQryExStrategyTemplateItem);
            CoreService.EventContrib.RegisterNotifyCallback("MgrExchServer", "NotifyExStrategyTemplateItem", this.OnNotifyExStrategyTemplateItem);
            CoreService.TLClient.ReqQryExStrategyTemplate();
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryExStrategyTemplate", this.OnQryExStrategyTemplate);
            CoreService.EventContrib.UnRegisterNotifyCallback("MgrExchServer", "NotifyExStrategyTemplate", this.OnNotifyExStrategyTemplate);

            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryExStrategyTemplateItem", this.OnQryExStrategyTemplateItem);
            CoreService.EventContrib.UnRegisterNotifyCallback("MgrExchServer", "NotifyExStrategyTemplateItem", this.OnNotifyExStrategyTemplateItem);
        }


        void ClearItem()
        {
            //commissionGrid.DataSource = null;
            //itemrowmap.Clear();
            //itemmap.Clear();
            //gt.Rows.Clear();
            //BindToTable();
        }
        ExStrategy _current = null;

        void OnQryExStrategyTemplateItem(string json, bool islast)
        {
            ExStrategy item = MoniterHelper.ParseJsonResponse<ExStrategy>(json);
            if (item != null)
            {
                GotExStrategy(item);
            }
        }

        void OnNotifyExStrategyTemplateItem(string json)
        {
            ExStrategy obj = MoniterHelper.ParseJsonResponse<ExStrategy>(json);
            if (obj != null)
            {
                GotExStrategy(obj);
            }
        }

        void GotExStrategy(ExStrategy item)
        {
            margin.SelectedValue = item.Margin;
            avabilefund.SelectedValue = item.AvabileFund;
            sidemargin.Checked = item.SideMargin;
            creditseparate.Checked = item.CreditSeparate;
            poslock.Checked = item.PositionLock;
            _current = item;
        }


        void InvokeGotExStrategyTemplate(ExStrategyTemplateSetting strategy)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ExStrategyTemplateSetting>(InvokeGotExStrategyTemplate), new object[] { strategy });
            }
            else
            {
                templateTree.Nodes[0].Nodes.Add(CreateExStrategyTemplateItem(strategy));
            }
        }

        Dictionary<int, ExStrategyTemplateSetting> templatemap = new Dictionary<int, ExStrategyTemplateSetting>();
        void OnQryExStrategyTemplate(string json, bool islast)
        {
            ExStrategyTemplateSetting[] list = MoniterHelper.ParseJsonResponse<ExStrategyTemplateSetting[]>(json);
            if (list != null)
            {
                foreach (ExStrategyTemplateSetting t in list)
                {
                    templatemap.Add(t.ID, t);
                    //templatelist.Items.Add(t);
                    InvokeGotExStrategyTemplate(t);
                }
            }
        }

        void OnNotifyExStrategyTemplate(string json)
        {
            ExStrategyTemplateSetting obj = MoniterHelper.ParseJsonResponse<ExStrategyTemplateSetting>(json);
            if (obj != null)
            {
                ExStrategyTemplateSetting target = null;
                if (templatemap.TryGetValue(obj.ID,out target))
                {
                    target.Name = obj.Name;
                    target.Description = obj.Description;
                    //templatelist.Refresh();
                    templateTree.Refresh();
                }
                else
                {
                    templatemap.Add(obj.ID, obj);
                    //templatelist.Items.Add(obj);
                    InvokeGotExStrategyTemplate(obj);
                }
            }
        }



    }
}
