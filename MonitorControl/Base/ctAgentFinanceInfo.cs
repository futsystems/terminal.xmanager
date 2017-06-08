using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;


namespace TradingLib.MoniterControl
{
    public partial class ctAgentFinanceInfo : UserControl,IEventBinder
    {
        public ctAgentFinanceInfo()
        {
            InitializeComponent();
            this.Load += new EventHandler(ctFinanceInfo_Load);
        }

        void ctFinanceInfo_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_agent == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请设定代理帐户");
                return;
            }
            CoreService.TLClient.ReqQryAgentFinInfo(_agent.Account);
        }

        /// <summary>
        /// 作为模块使用 不用设置account,只需要监听全局的OnAccountSelected 就可以获得Account对象
        /// 在作为帐户编辑的控件时,需要设置初始化的account
        /// </summary>
        /// <param name="acc"></param>
        public void SetAgent(AgentSetting agent)
        {
            _agent = agent;
            account.Text = _agent.Account;
        }

        public void OnInit()
        {

            CoreService.EventCore.RegisterCallback(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT_FINANCE_INFO, this.OnQryAgentInfo);
            CoreService.EventCore.RegisterNotifyCallback(Modules.AgentManager, Method_ACC_MGR.NOTIFY_ACC_FININFO, this.OnNotifyAccountInfo);
            CoreService.TLClient.ReqQryAgentFinInfo(_agent.Account);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT_FINANCE_INFO, this.OnQryAgentInfo);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.AgentManager, Method_ACC_MGR.NOTIFY_ACC_FININFO, this.OnNotifyAccountInfo);

         }

        void OnNotifyAccountInfo(string json)
        {
            OnQryAgentInfo(json, true);
        }
        void OnQryAgentInfo(string json, bool islast)
        {
            AgentFinanceInfo obj = CoreService.ParseJsonResponse<AgentFinanceInfo>(json);
            if (obj != null)
            {
                this.GotAgentInfo(obj);
            }
        }


        AgentSetting _agent = null;

        public void GotAgentInfo(AgentFinanceInfo info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AgentFinanceInfo>(GotAgentInfo), new object[] { info });
            }
            else
            {
                var a = CoreService.BasicInfoTracker.GetAccount(info.Account);

                account.Text = info.Account;
                currency.Text = Util.GetEnumDescription(info.Currency);
                lastequtiy.Text = info.LastEquity.ToFormatStr();
                realizedpl.Text = info.RealizedPL.ToFormatStr();
                unrealizedpl.Text = info.UnRealizedPL.ToFormatStr();
                commissionCost.Text = info.CommissionCost.ToFormatStr();
                commissionIncome.Text = info.CommissioinIncome.ToFormatStr();
                cashin.Text = info.CashIn.ToFormatStr();
                cashout.Text = info.CashOut.ToFormatStr();
                nowequity.Text = info.NowEquity.ToFormatStr();
                margin.Text = info.Margin.ToFormatStr();
                marginfrozen.Text = info.MarginFrozen.ToFormatStr();
                
                staticEquity.Text = info.StaticEquity.ToFormatStr();
                subStaticEquity.Text = info.SubStaticEquity.ToFormatStr();
                canUseEquity.Text = (info.StaticEquity - info.SubStaticEquity).ToFormatStr();

                lastcredit.Text = info.LastCredit.ToFormatStr();
                creditcashin.Text = info.CreditCashIn.ToFormatStr();
                creditcashout.Text = info.CreditCashOut.ToFormatStr();
                credit.Text = info.NowCredit.ToFormatStr();

                totalEquity.Text = (info.NowCredit + info.NowEquity).ToFormatStr();
            }
        }


    }
}
