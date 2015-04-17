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
    public partial class fmCashOperation : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmCashOperation()
        {
            InitializeComponent();
            //cashop_type.Items.Add("入金");
            //cashop_type.Items.Add("出金");
            //cashop_type.SelectedIndex = 0;
            MoniterHelper.AdapterToIDataSource(cbEquityTypeList).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumEquityType>());

            this.Load += new EventHandler(fmCashOperation_Load);
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("BrokerRouterPassThrough", "QryBrokerAccountInfo", this.OnAccountInfo);
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("BrokerRouterPassThrough", "QryBrokerAccountInfo", this.OnAccountInfo);

        }

        decimal _mainstaticequity = 0;
        void OnAccountInfo(string json, bool islast)
        {
            var data = TradingLib.Mixins.Json.JsonMapper.ToObject(json)["Payload"];
            decimal lastequity = decimal.Parse(data["LastEquity"].ToString());
            decimal deposit = decimal.Parse(data["Deposit"].ToString());
            decimal withdraw = decimal.Parse(data["Withdraw"].ToString());
            decimal commission = decimal.Parse(data["Commission"].ToString());
            decimal closeprofit = decimal.Parse(data["CloseProfit"].ToString());
            decimal positionprofit = decimal.Parse(data["PositionProfit"].ToString());

            lbPreBalance.Text = Util.FormatDecimal(lastequity);
            lbDeposit.Text = Util.FormatDecimal(deposit);
            lbWithDraw.Text = Util.FormatDecimal(withdraw);
            lbCommission.Text = Util.FormatDecimal(commission);
            lbCloseProfit.Text = Util.FormatDecimal(closeprofit);
            lbPositionProfit.Text = Util.FormatDecimal(positionprofit);
            lbNowEquity.Text = Util.FormatDecimal(lastequity + deposit - withdraw + closeprofit + positionprofit - commission);
            _mainstaticequity = lastequity + deposit - withdraw;
            lbStaticEquity.Text = Util.FormatDecimal(_mainstaticequity);
            lbProfit.Text = Util.FormatDecimal(closeprofit + positionprofit - commission);
        }

        void btnWithdraw_Click(object sender, EventArgs e)
        {
            string _acc = _account.Account;
            double _amount = (double)amount.Value;
            string _pass = pass.Text;

            if (MoniterHelper.WindowConfirm(string.Format("确认从主帐户:{0}所绑定的底层帐户出金:{1}", _account.ConnectorToken, _amount)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqWithdrawMainAccount(_acc, _amount, _pass);
            }

        }

        void btnDeposit_Click(object sender, EventArgs e)
        {
            string _acc = _account.Account;
            double _amount = (double)amount.Value;
            string _pass = pass.Text;

            if (MoniterHelper.WindowConfirm(string.Format("确认向主帐户:{0}所绑定的底层帐户入金:{1}", _account.ConnectorToken, _amount)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqDepositMainAccount(_acc, _amount, _pass);
            }
        }




        private void btnQry_Click(object sender, EventArgs e)
        {
            CoreService.TLClient.ReqQryConnectorAccountInfo(_account.Account);
        }


        void fmCashOperation_Load(object sender, EventArgs e)
        {
            btnAccountDeposit.Click += new EventHandler(btnAccountDeposit_Click);
            btnAccountWithdraw.Click += new EventHandler(btnAccountWithdraw_Click);
            btnDeposit.Click += new EventHandler(btnDeposit_Click);
            btnWithdraw.Click += new EventHandler(btnWithdraw_Click);
            btnSyncEquity.Click += new EventHandler(btnSyncEquity_Click);

            btnQryBrokerAccountInfo.Click += new EventHandler(btnQryBrokerAccountInfo_Click);
            CoreService.EventCore.RegIEventHandler(this);
            CoreService.TLClient.ReqQryConnectorAccountInfo(_account.Account);
        }

        void btnQryBrokerAccountInfo_Click(object sender, EventArgs e)
        {
            CoreService.TLClient.ReqQryConnectorAccountInfo(_account.Account);
        }

        void btnSyncEquity_Click(object sender, EventArgs e)
        {
            fmSyncEquity fm = new fmSyncEquity();
            if(ctFinanceInfo1.AccountInfo != null)
            {
                fm.SetAccountInfo(ctFinanceInfo1.AccountInfo);
                fm.SetStaticEquity(_mainstaticequity);
                fm.ShowDialog();
                fm.Close();
            }
        }

        void btnAccountWithdraw_Click(object sender, EventArgs e)
        {
            bool sync = cashop_sync.Checked;
            decimal amount = cashop_amount.Value;
            if (amount == 0)
            {
                MoniterHelper.WindowMessage("请输入出入金金额");
                return;
            }
            string comment = cashop_comment.Text;
            string cashoptitle = string.Empty;

            decimal amount2 = amount * -1;

            QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
            if (MoniterHelper.WindowConfirm("确认向帐户[" + _account.Account + "] 出金" + amount.ToString()) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqCashOperation(_account.Account, amount2, type, "", comment, sync);
            }
        }

        void btnAccountDeposit_Click(object sender, EventArgs e)
        {
            
            bool sync = cashop_sync.Checked;
            decimal amount = cashop_amount.Value;
            if (amount == 0)
            {
                MoniterHelper.WindowMessage("请输入出入金金额");
                return;
            }
            string comment = cashop_comment.Text;
            string cashoptitle = string.Empty;

            decimal amount2 = amount * 1;
            
            QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
            if (MoniterHelper.WindowConfirm("确认向帐户[" + _account.Account + "] " + cashoptitle + " " + amount.ToString()) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqCashOperation(_account.Account, amount2, type,"", comment, sync);
            }
        }

        AccountLite _account = null;
        /// <summary>
        /// 设定交易帐户
        /// </summary>
        /// <param name="account"></param>
        public void SetAccount(AccountLite account)
        {
            _account = account;
            ctFinanceInfo1.SetAccount(account);
        }


    }
}
