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
    public partial class ctFinanceInfo : UserControl,IEventBinder
    {
        public ctFinanceInfo()
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
            if (_account == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请设定帐户");
                return;
            }
            CoreService.TLClient.ReqQryAccountFinInfo(_account.Account);
        }

        /// <summary>
        /// 作为模块使用 不用设置account,只需要监听全局的OnAccountSelected 就可以获得Account对象
        /// 在作为帐户编辑的控件时,需要设置初始化的account
        /// </summary>
        /// <param name="acc"></param>
        public void SetAccount(AccountItem acc)
        {
            OnAccountSelected(acc);
        }

        public void OnInit()
        {

            CoreService.EventContrib.RegisterCallback(Modules.ACC_MGR,Method_ACC_MGR.QRY_ACC_FININFO, this.OnQryAccountInfo);
            CoreService.EventContrib.RegisterNotifyCallback(Modules.ACC_MGR, Method_ACC_MGR.NOTIFY_ACC_FININFO, this.OnNotifyAccountInfo);
            //Globals.LogicEvent.GotAccountSelectedEvent += new Action<AccountItem>(OnAccountSelected);

            CoreService.TLClient.ReqQryAccountFinInfo(_account.Account);
        }

        void OnAccountSelected(AccountItem obj)
        {
            _account = obj;
            account.Text = _account.Account;
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_FININFO, this.OnQryAccountInfo);
            CoreService.EventContrib.UnRegisterNotifyCallback(Modules.ACC_MGR, Method_ACC_MGR.NOTIFY_ACC_FININFO, this.OnNotifyAccountInfo);
            //Globals.LogicEvent.GotAccountSelectedEvent -= new Action<AccountItem>(OnAccountSelected);
            //Globals.Debug("ctFinanceInfo disposed...");
         }

        void OnNotifyAccountInfo(string json)
        {
            OnQryAccountInfo(json, true);
        }
        void OnQryAccountInfo(string json, bool islast)
        {
            AccountInfo obj = CoreService.ParseJsonResponse<AccountInfo>(json);
            if (obj != null)
            {
                this.GotAccountInfo(obj);
            }
            else//如果没有配资服
            {

            }
        }


        AccountItem _account = null;

        AccountInfo _info = null;
        public AccountInfo AccountInfo { get { return _info; } }

        public void GotAccountInfo(AccountInfo info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AccountInfo>(GotAccountInfo), new object[] { info });
            }
            else
            {
                _info = info;
                var a = CoreService.BasicInfoTracker.GetAccount(info.Account);
                account.Text = info.Account;//string.Format("{0}({1})", info.Account, a != null ? a.Currency.ToString() : "");
                currency.Text = a != null ? a.Currency.ToString() : "";
                lastequtiy.Text = info.LastEquity.ToFormatStr();
                realizedpl.Text = info.RealizedPL.ToFormatStr();
                unrealizedpl.Text = info.UnRealizedPL.ToFormatStr();
                commission.Text = info.Commission.ToFormatStr();
                netprofit.Text = info.Profit.ToFormatStr();
                cashin.Text = info.CashIn.ToFormatStr();
                cashout.Text = info.CashOut.ToFormatStr();
                nowequity.Text = info.NowEquity.ToFormatStr();
                margin.Text = info.Margin.ToFormatStr();
                marginfrozen.Text = info.MarginFrozen.ToFormatStr();
                staticEquity.Text = (info.LastEquity + info.CashIn - info.CashOut).ToFormatStr();
                lastcredit.Text = info.LastCredit.ToFormatStr();
                creditcashin.Text = info.CreditCashIn.ToFormatStr();
                creditcashout.Text = info.CreditCashOut.ToFormatStr();
                credit.Text = info.Credit.ToFormatStr();
                totalEquity.Text = (info.Credit + info.NowEquity).ToFormatStr();
            }
        }


    }
}
