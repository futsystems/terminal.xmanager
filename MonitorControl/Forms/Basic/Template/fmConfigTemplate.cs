using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class fmConfigTemplate : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmConfigTemplate()
        {
            InitializeComponent();

            

            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.folder_sel);
            this.imageList1.Images.Add((System.Drawing.Image)Properties.Resources.items);
            templateTree.ImageList = this.imageList1;


            templateTree.Nodes.Add(CreateBaseItem("配置模板"));

            
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

        private ComponentFactory.Krypton.Toolkit.KryptonTreeNode CreateConfigTemplateItem(ConfigTemplate template)
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
            this.templateTree.ContextMenuStrip.Items.Add("添加配置模板", null, new EventHandler(Add_Click));
            this.templateTree.ContextMenuStrip.Items.Add("删除配置模板", null, new EventHandler(Del_Click));


            btnAddTemplate.Click += new EventHandler(btnAddTemplate_Click);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnRiskRule.Click += new EventHandler(btnRiskRule_Click);

            templateTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(templateTree_NodeMouseClick);

            CoreService.EventCore.RegIEventHandler(this);

            btnSubmit.Enabled = false;
            btnRiskRule.Enabled = false;
        }

        void btnRiskRule_Click(object sender, EventArgs e)
        {

            if (_current == null)
            {
                MoniterHelper.WindowMessage("请选择要编辑的交易参数模板");
                return;
            }

            fmEditRiskRule fm = new fmEditRiskRule();
            fm.SetAccount(Const.CONFIG_TEMPLATE_PREFIX + _current.ID.ToString());
            fm.ShowDialog();
            fm.Close();
        }



        void btnSubmit_Click(object sender, EventArgs e)
        {

            if (_current == null)
            { 
                MoniterHelper.WindowMessage("请选择要编辑的交易参数模板");
                return;
            }

            int commissionid = (int)cbCommissionTemplate.SelectedValue;
            int marginid = (int)cbMarginTemplate.SelectedValue;
            int strategyid = (int)cbExStrategyTemplate.SelectedValue;

            _current.Margin_ID = marginid;
            _current.Commission_ID = commissionid;
            _current.ExStrategy_ID = strategyid;

            CoreService.TLClient.ReqUpdateConfigTemplate(_current);

        }



        void btnAddTemplate_Click(object sender, EventArgs e)
        {
            fmTemplateEdit fm = new fmTemplateEdit(TemplateEditType.Config);
            fm.ShowDialog();
        }


        void templateTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                return;
            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Index == 0)
                {
                    ConfigTemplate t = e.Node.Tag as ConfigTemplate;
                    if (t != null)
                    {
                        SetCurrent(t);
                        return;
                    }
                }
            }
            ClearCurrent();
        }


        void Add_Click(object sender, EventArgs e)
        {
            fmTemplateEdit fm = new fmTemplateEdit(TemplateEditType.Config);
            fm.ShowDialog();
        }

        void Del_Click(object sender, EventArgs e)
        {
            if (templateTree.SelectedNode.Parent != null)
            {
                if (templateTree.SelectedNode.Parent.Index == 0)
                {
                    ConfigTemplate t = templateTree.SelectedNode.Tag as ConfigTemplate;

                    if (MoniterHelper.WindowConfirm(string.Format("确认删除配置模板:{0}?", t.Name)) == System.Windows.Forms.DialogResult.Yes)
                    {
                        ClearCurrent();
                        CoreService.TLClient.ReqDelConfigTemplate(t);
                    }
                }
            }
        }

        bool ShowInSuper()
        {
            if (!string.IsNullOrEmpty(ControlService.SuperRoot))
            {
                if (CoreService.SiteInfo.Manager.Login == "adminx")
                {
                    return true;
                }
            }
            return false;
        }
        public void OnInit()
        {
           

            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_CONFIG_TEMPLATE, this.OnQryConfigTemplate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_CONFIG_TEMPLATE, this.OnNotifyConfigTemplate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_CONFIG_TEMPLATE_DELETE, this.OnNotifyDelConfigTemplate);

            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_ITEM, this.OnQryExStrategyTemplateItem);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_EXSTRATEGY_ITEM, this.OnNotifyExStrategyTemplateItem);
            CoreService.TLClient.ReqQryConfigTemplate();

            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_TEMPLATE, this.OnQryCommissionTemplate);
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_TEMPLATE, this.OnQryMarginTemplate);
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_TEMPLATE, this.OnQryExStrategyTemplate);

            CoreService.TLClient.ReqQryCommissionTemplate();
            CoreService.TLClient.ReqQryMarginTemplate();
            CoreService.TLClient.ReqQryExStrategyTemplate();


        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_CONFIG_TEMPLATE, this.OnQryConfigTemplate);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_CONFIG_TEMPLATE, this.OnNotifyConfigTemplate);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_CONFIG_TEMPLATE_DELETE, this.OnNotifyDelConfigTemplate);

            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_ITEM, this.OnQryExStrategyTemplateItem);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_EXSTRATEGY_ITEM, this.OnNotifyExStrategyTemplateItem);

            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_TEMPLATE, this.OnQryCommissionTemplate);
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_TEMPLATE, this.OnQryMarginTemplate);
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_TEMPLATE, this.OnQryExStrategyTemplate);

        }

        void ClearCurrent()
        {
            _current = null;
            currentTitle.Text = "--";
            btnSubmit.Enabled = false;
            btnRiskRule.Enabled = false;

            cbCommissionTemplate.SelectedIndex = 0;
            cbExStrategyTemplate.SelectedIndex = 0;
            cbMarginTemplate.SelectedIndex = 0;

        }

        void SetCurrent(ConfigTemplate t)
        {
            _current = t;
            currentTitle.Text = _current.Name;
            btnSubmit.Enabled = true;
            btnRiskRule.Enabled = true;
            cbCommissionTemplate.SelectedValue = AnyMathItem(cbCommissionTemplate, _current.Commission_ID) ? _current.Commission_ID : 0;
            cbMarginTemplate.SelectedValue = AnyMathItem(cbMarginTemplate, _current.Margin_ID) ? _current.Margin_ID : 0;
            cbExStrategyTemplate.SelectedValue = AnyMathItem(cbExStrategyTemplate, _current.ExStrategy_ID) ? _current.ExStrategy_ID : 0;

        }
        ConfigTemplate _current = null;

        void OnQryExStrategyTemplateItem(string json, bool islast)
        {
            ExStrategy item = CoreService.ParseJsonResponse<ExStrategy>(json);
            if (item != null)
            {
                GotExStrategy(item);
            }
        }

        void OnNotifyExStrategyTemplateItem(string json)
        {
            ExStrategy obj = CoreService.ParseJsonResponse<ExStrategy>(json);
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
               
            }
        }


        void InvokeGotConfigTemplate(ConfigTemplate config)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ConfigTemplate>(InvokeGotConfigTemplate), new object[] { config });
            }
            else
            {
                templateTree.Nodes[0].Nodes.Add(CreateConfigTemplateItem(config));
            }
        }

        Dictionary<int, ConfigTemplate> templatemap = new Dictionary<int, ConfigTemplate>();
        void OnQryConfigTemplate(string json, bool islast)
        {
            ConfigTemplate item = CoreService.ParseJsonResponse<ConfigTemplate>(json);
            if (item != null)
            {
                templatemap.Add(item.ID, item);
                InvokeGotConfigTemplate(item);
            }

            if (islast)
            {
                templateTree.ExpandAll();
            }
        }


        void OnNotifyDelConfigTemplate(string json)
        {
            ConfigTemplate obj = CoreService.ParseJsonResponse<ConfigTemplate>(json);
            if (obj != null)
            {
                ConfigTemplate template = templateTree.SelectedNode.Tag as ConfigTemplate;
                if (template.ID == obj.ID)
                {
                    templateTree.SelectedNode.Parent.Nodes.Remove(templateTree.SelectedNode);
                }
            }
        }


        void OnNotifyConfigTemplate(string json)
        {
            ConfigTemplate obj = CoreService.ParseJsonResponse<ConfigTemplate>(json);
            if (obj != null)
            {
                ConfigTemplate target = null;
                if (templatemap.TryGetValue(obj.ID,out target))
                {
                    target.Name = obj.Name;
                    target.Description = obj.Description;
                    templateTree.Refresh();

                    //更新配置模板数据
                }
                else
                {
                    templatemap.Add(obj.ID, obj);
                    InvokeGotConfigTemplate(obj);
                    templateTree.ExpandAll();
                }
            }
        }


        #region 响应模板查询 生成comboxlist
        bool AnyMathItem(ComponentFactory.Krypton.Toolkit.KryptonComboBox cb, int id)
        {
            foreach (var item in cb.Items)
            {
                if (((ValueObject<int>)item).Value == id)
                    return true;
            }
            return false;
        }

        List<CommissionTemplateSetting> commissionList = new List<CommissionTemplateSetting>();
        void OnQryCommissionTemplate(string json, bool islast)
        {
            CommissionTemplateSetting item = CoreService.ParseJsonResponse<CommissionTemplateSetting>(json);
            if (item != null)
            {
                commissionList.Add(item);
            }
            if (islast)
            {
                MoniterHelper.AdapterToIDataSource(cbCommissionTemplate).BindDataSource(GetCommissionTemplateCBList(commissionList));
                cbCommissionTemplate.SelectedValue = 0;
            }
        }


        List<MarginTemplateSetting> marginList = new List<MarginTemplateSetting>();
        void OnQryMarginTemplate(string json, bool islast)
        {
            MarginTemplateSetting item = CoreService.ParseJsonResponse<MarginTemplateSetting>(json);
            if (item != null)
            {
                marginList.Add(item);

            }
            if (islast)
            {
                MoniterHelper.AdapterToIDataSource(cbMarginTemplate).BindDataSource(GetMarginTemplateCBList(marginList));
                cbMarginTemplate.SelectedValue = 0;
            }
        }


        List<ExStrategyTemplateSetting> strategyList = new List<ExStrategyTemplateSetting>();
        void OnQryExStrategyTemplate(string json, bool islast)
        {
            ExStrategyTemplateSetting item = CoreService.ParseJsonResponse<ExStrategyTemplateSetting>(json);
            if (item != null)
            {
                strategyList.Add(item);
            }
            if (islast)
            {
                MoniterHelper.AdapterToIDataSource(cbExStrategyTemplate).BindDataSource(GetExStrategyTemplateCBList(strategyList));
                cbExStrategyTemplate.SelectedValue = 0;

            }
        }

        static ArrayList GetExStrategyTemplateCBList(List<ExStrategyTemplateSetting> items)
        {
            ArrayList list = new ArrayList();
            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "系统默认";
            vo1.Value = 0;
            list.Add(vo1);

            foreach (ExStrategyTemplateSetting item in items)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = string.Format("{0}{1}", item.Name, item.Manager_ID != CoreService.SiteInfo.Manager.ID ? "*" : "");
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }
        static ArrayList GetMarginTemplateCBList(List<MarginTemplateSetting> items)
        {
            ArrayList list = new ArrayList();
            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "系统默认";
            vo1.Value = 0;
            list.Add(vo1);

            foreach (MarginTemplateSetting item in items)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = string.Format("{0}{1}", item.Name, item.Manager_ID != CoreService.SiteInfo.Manager.ID ? "*" : "");
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }
        static ArrayList GetCommissionTemplateCBList(List<CommissionTemplateSetting> items)
        {
            ArrayList list = new ArrayList();
            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "系统默认";
            vo1.Value = 0;
            list.Add(vo1);

            foreach (CommissionTemplateSetting item in items)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = string.Format("{0}{1}", item.Name, item.Manager_ID != CoreService.SiteInfo.Manager.ID ? "*" : "");
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }
        #endregion




    }
}
