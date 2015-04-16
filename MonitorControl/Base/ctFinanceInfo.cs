﻿using System;
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
        public void SetAccount(AccountLite acc)
        {
            OnAccountSelected(acc);
        }

        public void OnInit()
        {

            CoreService.EventContrib.RegisterCallback("AccountManager", "QryAccountFinInfo", this.OnQryAccountInfo);
            CoreService.EventContrib.RegisterNotifyCallback("AccountManager", "NotifyAccountFinInfo", this.OnNotifyAccountInfo);
            //Globals.LogicEvent.GotAccountSelectedEvent += new Action<AccountLite>(OnAccountSelected);

            CoreService.TLClient.ReqQryAccountFinInfo(_account.Account);
        }

        void OnAccountSelected(AccountLite obj)
        {
            _account = obj;
            account.Text = _account.Account;
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("AccountManager", "QryAccountFinInfo", this.OnQryAccountInfo);
            CoreService.EventContrib.UnRegisterNotifyCallback("AccountManager", "NotifyAccountFinInfo", this.OnNotifyAccountInfo);
            //Globals.LogicEvent.GotAccountSelectedEvent -= new Action<AccountLite>(OnAccountSelected);
            //Globals.Debug("ctFinanceInfo disposed...");
         }

        void OnNotifyAccountInfo(string json)
        {
            OnQryAccountInfo(json, true);
        }
        void OnQryAccountInfo(string json, bool islast)
        {
            AccountInfo obj = MoniterHelper.ParseJsonResponse<AccountInfo>(json);
            if (obj != null)
            {
                this.GotAccountInfo(obj);
            }
            else//如果没有配资服
            {

            }
        }


        AccountLite _account = null;

        public void GotAccountInfo(AccountInfo info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AccountInfo>(GotAccountInfo), new object[] { info });
            }
            else
            {
                account.Text = info.Account;
                lastequtiy.Text = Util.FormatDecimal(info.LastEquity);
                realizedpl.Text = Util.FormatDecimal(info.RealizedPL);
                unrealizedpl.Text = Util.FormatDecimal(info.UnRealizedPL);
                commission.Text = Util.FormatDecimal(info.Commission);
                netprofit.Text = Util.FormatDecimal(info.Profit);
                cashin.Text = Util.FormatDecimal(info.CashIn);
                cashout.Text = Util.FormatDecimal(info.CashOut);
                nowequity.Text = Util.FormatDecimal(info.NowEquity);
                margin.Text = Util.FormatDecimal(info.Margin);
                marginfrozen.Text = Util.FormatDecimal(info.MarginFrozen);
                staticEquity.Text = Util.FormatDecimal(info.LastEquity + info.CashIn - info.CashOut);
                lastcredit.Text = Util.FormatDecimal(info.LastCredit);
                creditcashin.Text = Util.FormatDecimal(info.CreditCashIn);
                creditcashout.Text = Util.FormatDecimal(info.CreditCashOut);
                credit.Text = Util.FormatDecimal(info.Credit);
                totalEquity.Text = Util.FormatDecimal(info.Credit + info.NowEquity);
            }
        }


    }
}
