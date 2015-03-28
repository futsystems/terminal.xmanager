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
    public partial class fmEditAccountTemplate : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmEditAccountTemplate()
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

            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryCommissionTemplate", this.OnQryCommissionTemplate);
            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryMarginTemplate", this.OnQryMarginTemplate);
            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryExStrategyTemplate", this.OnQryExStrategyTemplate);
            CoreService.TLClient.ReqQryCommissionTemplate();
            CoreService.TLClient.ReqQryMarginTemplate();
            CoreService.TLClient.ReqQryExStrategyTemplate();
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryCommissionTemplate", this.OnQryCommissionTemplate);
            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryMarginTemplate", this.OnQryMarginTemplate);
            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryExStrategyTemplate", this.OnQryExStrategyTemplate);

        }

        #region 响应模板查询 生成comboxlist
        void OnQryCommissionTemplate(string json, bool islast)
        {
            CommissionTemplateSetting[] list = MoniterHelper.ParseJsonResponse<CommissionTemplateSetting[]>(json);
            if (list != null)
            {
                MoniterHelper.AdapterToIDataSource(cbCommissionTemplate).BindDataSource(GetCommissionTemplateCBList(list));
                cbCommissionTemplate.SelectedValue = _account.Commissin_ID;
            }
        }

        void OnQryMarginTemplate(string json, bool islast)
        {
            MarginTemplateSetting[] list = MoniterHelper.ParseJsonResponse<MarginTemplateSetting[]>(json);
            if (list != null)
            {
                MoniterHelper.AdapterToIDataSource(cbMarginTemplate).BindDataSource(GetMarginTemplateCBList(list));
                cbMarginTemplate.SelectedValue = _account.Margin_ID;
            }
        }

        void OnQryExStrategyTemplate(string json, bool islast)
        {
            ExStrategyTemplateSetting[] list = MoniterHelper.ParseJsonResponse<ExStrategyTemplateSetting[]>(json);
            if (list != null)
            {
                MoniterHelper.AdapterToIDataSource(cbExStrategyTemplate).BindDataSource(GetExStrategyTemplateCBList(list));
                cbExStrategyTemplate.SelectedValue = _account.ExStrategy_ID;
            }
        }

        static ArrayList GetExStrategyTemplateCBList(ExStrategyTemplateSetting[] items)
        {
            ArrayList list = new ArrayList();
            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "系统默认";
            vo1.Value = 0;
            list.Add(vo1);

            foreach (ExStrategyTemplateSetting item in items)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = item.Name;
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }
        static ArrayList GetMarginTemplateCBList(MarginTemplateSetting[] items)
        {
            ArrayList list = new ArrayList();
            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "系统默认";
            vo1.Value = 0;
            list.Add(vo1);

            foreach (MarginTemplateSetting item in items)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = item.Name;
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }
        static ArrayList GetCommissionTemplateCBList(CommissionTemplateSetting[] items)
        {
            ArrayList list = new ArrayList();
            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "系统默认";
            vo1.Value = 0;
            list.Add(vo1);

            foreach (CommissionTemplateSetting item in items)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = item.Name;
                vo.Value = item.ID;
                list.Add(vo);
            }
            return list;
        }
        #endregion



        AccountLite _account = null;
        public void SetAccount(AccountLite account)
        {
            _account = account;
            this.Text = string.Format("编辑帐户:{0}模板项", account.Account);

        }
        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认更新帐户手续费与保证金模板?") == System.Windows.Forms.DialogResult.Yes)
            {
                int commissionid = (int)cbCommissionTemplate.SelectedValue;
                if (_account.Commissin_ID != commissionid)
                {
                    CoreService.TLClient.ReqUpdateAccountCommissionTemplate(_account.Account, commissionid);
                }
                int marginid = (int)cbMarginTemplate.SelectedValue;
                if (_account.Margin_ID != marginid)
                {
                    CoreService.TLClient.ReqUpdateAccountMarginTemplate(_account.Account, marginid);
                }
                int strategyid = (int)cbExStrategyTemplate.SelectedValue;
                if (_account.ExStrategy_ID != strategyid)
                {
                    CoreService.TLClient.ReqUpdateAccountExStrategyTemplate(_account.Account, strategyid);
                }
                this.Close();
            }
            
        }
    }
}
