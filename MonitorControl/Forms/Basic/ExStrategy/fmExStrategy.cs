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

            MoniterHelper.AdapterToIDataSource(margin).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumMarginPrice>());
            //MoniterHelper.AdapterToIDataSource(avabilefund).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumAvabileFundStrategy>());

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
            _current.MarginPrice = (QSEnumMarginPrice)margin.SelectedValue;
            _current.IncludeCloseProfit = includecloseprofit.Checked;
            _current.IncludePositionProfit = includepositionprofit.Checked;
            _current.PositionLock = poslock.Checked;
            _current.EntrySlip = (int)entrySlip.Value;
            _current.ExitSlip = (int)exitSlip.Value;
            _current.LimitCheck = limitcheck.Checked;
            _current.Probability = (int)probability.Value;

            CoreService.TLClient.ReqUpdateExStrategyTemplateItem(_current);
        }

        void fmCommission_Load(object sender, EventArgs e)
        {
            this.templateTree.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.templateTree.ContextMenuStrip.Items.Add("添加交易参数模板", null, new EventHandler(Add_Click));

            templateTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(templateTree_NodeMouseClick);

            CoreService.EventCore.RegIEventHandler(this);
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


        void Add_Click(object sender, EventArgs e)
        {
            fmTemplateEdit fm = new fmTemplateEdit(TemplateEditType.Strategy);
            fm.ShowDialog();
        }


        public void OnInit()
        {
            if (!CoreService.SiteInfo.Domain.Module_Slip)
            {
                kryptonGroupBox3.Visible = false;
            }

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
            if (InvokeRequired)
            {
                Invoke(new Action<ExStrategy>(GotExStrategy), new object[] { item });
            }
            else
            {
                margin.SelectedValue = item.MarginPrice;
                includecloseprofit.Checked = item.IncludeCloseProfit;
                includepositionprofit.Checked = item.IncludePositionProfit;
                sidemargin.Checked = item.SideMargin;
                creditseparate.Checked = item.CreditSeparate;
                poslock.Checked = item.PositionLock;
                entrySlip.Value = item.EntrySlip;
                exitSlip.Value = item.ExitSlip;
                limitcheck.Checked = item.LimitCheck;
                probability.Value = item.Probability;
                _current = item;
            }
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
                    InvokeGotExStrategyTemplate(t);
                }
            }

            if (islast)
            {
                templateTree.ExpandAll();
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
