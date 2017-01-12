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
    public partial class fmSyncEquity : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmSyncEquity()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmSyncEquity_Load);
        }

        void fmSyncEquity_Load(object sender, EventArgs e)
        {
            targetCredit.ValueChanged += new EventHandler(targetCredit_ValueChanged);
            targetCredit.TextChanged += new EventHandler(num_Changed);
            btnSync.Click += new EventHandler(btnSync_Click);
        }



        void btnSync_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm(string.Format("确认同步后,优先资金:{0} 客户资金:{1}", targetCredit.Value, _mainAcctStaticEquity - targetCredit.Value)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqContribRequest("BrokerRouterPassThrough", "SyncEquity", new { account = _info.Account, target_static_equity = _mainAcctStaticEquity - targetCredit.Value, target_static_credit = targetCredit.Value });
                this.Close();
            }
        }

        void num_Changed(object sender, EventArgs e)
        {
            CalValue();
        }

        void targetCredit_ValueChanged(object sender, EventArgs e)
        {
            CalValue();
        }

        void CalValue()
        {
            targetEquit.Text = (_mainAcctStaticEquity - targetCredit.Value).ToFormatStr();
        }

        //AccountItem _account;
        AccountInfo _info;
        public void SetAccountInfo(AccountInfo info)
        {
            _info = info;
            lbEquity.Text = (_info.LastEquity + _info.CashIn - _info.CashOut).ToFormatStr();
            lbCredit.Text = (_info.LastCredit + _info.CreditCashIn - _info.CreditCashOut).ToFormatStr();
        }

        decimal _mainAcctStaticEquity = 0;
        public void SetStaticEquity(decimal staticequity)
        {
            _mainAcctStaticEquity = staticequity;
            lbMainAcctStaticEquity.Text = (_mainAcctStaticEquity).ToFormatStr();
            targetCredit.Maximum = _mainAcctStaticEquity;
        }




    }
}
