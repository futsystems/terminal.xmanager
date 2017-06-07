using System;
using System.Linq;
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
    public partial class fmEditAgentTemplate : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmEditAgentTemplate()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmEditAccountTemplate_Load);
        }

        void fmEditAccountTemplate_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            cbCommissionTemplate.Enabled = CoreService.SiteInfo.UIAccess.r_commission;
            cbMarginTemplate.Enabled = CoreService.SiteInfo.UIAccess.r_margin;
            cbExStrategyTemplate.Enabled = CoreService.SiteInfo.UIAccess.r_exstrategy;

            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_TEMPLATE, this.OnQryCommissionTemplate);
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_TEMPLATE, this.OnQryMarginTemplate);
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_TEMPLATE, this.OnQryExStrategyTemplate);
            if (CoreService.SiteInfo.UIAccess.r_commission)
            {
                CoreService.TLClient.ReqQryCommissionTemplate();
            }
            if (CoreService.SiteInfo.UIAccess.r_margin)
            {
                CoreService.TLClient.ReqQryMarginTemplate();
            }
            if (CoreService.SiteInfo.UIAccess.r_exstrategy)
            {
                CoreService.TLClient.ReqQryExStrategyTemplate();
            }

        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_TEMPLATE, this.OnQryCommissionTemplate);
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_TEMPLATE, this.OnQryMarginTemplate);
            CoreService.EventCore.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_TEMPLATE, this.OnQryExStrategyTemplate);

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
                int commissoinid = AnyMathItem(cbCommissionTemplate, _agent.Commission_ID) ? _agent.Commission_ID : 0;
                cbCommissionTemplate.SelectedValue = commissoinid;
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
                int marginid = AnyMathItem(cbMarginTemplate, _agent.Margin_ID) ? _agent.Margin_ID : 0;
                cbMarginTemplate.SelectedValue = marginid;
            }
        }


        List<ExStrategyTemplateSetting> strategyList = new List<ExStrategyTemplateSetting>();
        void OnQryExStrategyTemplate(string json, bool islast)
        {
            ExStrategyTemplateSetting item= CoreService.ParseJsonResponse<ExStrategyTemplateSetting>(json);
            if (item != null)
            {
                strategyList.Add(item);
            }
            if (islast)
            {
                MoniterHelper.AdapterToIDataSource(cbExStrategyTemplate).BindDataSource(GetExStrategyTemplateCBList(strategyList));
                int strategyid = AnyMathItem(cbExStrategyTemplate, _agent.ExStrategy_ID) ? _agent.ExStrategy_ID : 0;
                cbExStrategyTemplate.SelectedValue = strategyid;
                
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



        AgentSetting _agent = null;
        public void SetAgent(AgentSetting account)
        {
            _agent = account;
            this.Text = string.Format("编辑代理帐户:{0}模板项", _agent.Account);

        }
        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认更新帐户手续费与保证金模板?") == System.Windows.Forms.DialogResult.Yes)
            {

                var obj = new
                {
                    account = _agent.Account,
                    commission_id = (int)cbCommissionTemplate.SelectedValue,
                    margin_id = (int)cbMarginTemplate.SelectedValue,
                    exstrategy_id = (int)cbExStrategyTemplate.SelectedValue,
                };

                CoreService.TLClient.ReqUpdateAgentTemplate(obj);

                //if (CoreService.SiteInfo.UIAccess.r_commission)
                //{
                //    int commissionid = (int)cbCommissionTemplate.SelectedValue;
                //    if (_agent.Commission_ID != commissionid)
                //    {
                //        CoreService.TLClient.ReqUpdateAccountCommissionTemplate(_agent.Account, commissionid);
                //    }
                //}
                //if (CoreService.SiteInfo.UIAccess.r_margin)
                //{
                //    int marginid = (int)cbMarginTemplate.SelectedValue;
                //    if (_agent.Margin_ID != marginid)
                //    {
                //        CoreService.TLClient.ReqUpdateAccountMarginTemplate(_agent.Account, marginid);
                //    }
                //}
                //if (CoreService.SiteInfo.UIAccess.r_exstrategy)
                //{
                //    int strategyid = (int)cbExStrategyTemplate.SelectedValue;
                //    if (_agent.ExStrategy_ID != strategyid)
                //    {
                //        CoreService.TLClient.ReqUpdateAccountExStrategyTemplate(_agent.Account, strategyid);
                //    }
                //}
                //this.Close();
            }
            
        }
    }
}
