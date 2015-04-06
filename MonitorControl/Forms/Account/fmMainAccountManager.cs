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
    public partial class fmMainAccountManager : ComponentFactory.Krypton.Toolkit.KryptonForm, IEventBinder
    {
        public fmMainAccountManager()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmConnectorAccountInfo_Load);
        }

        AccountLite account = null;
        public void SetAccount(AccountLite acc)
        {
            account = acc;
            this.Text = string.Format("主帐户管理【{0}】-{1}",account.ConnectorToken,acc.Account);
            

        }
        void fmConnectorAccountInfo_Load(object sender, EventArgs e)
        {
            btnDeposit.Click += new EventHandler(btnDeposit_Click);
            btnWithdraw.Click += new EventHandler(btnWithdraw_Click);
            CoreService.EventCore.RegIEventHandler(this);
            CoreService.TLClient.ReqQryConnectorAccountInfo(account.Account);
        }

        void btnWithdraw_Click(object sender, EventArgs e)
        {
            string _acc = account.Account;
            double _amount = (double)amount.Value;
            string _pass = pass.Text;

            if (MoniterHelper.WindowConfirm(string.Format("确认从主帐户:{0}所绑定的底层帐户出金:{1}", account.ConnectorToken, _amount)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqWithdrawMainAccount(_acc, _amount, _pass);
            }

        }

        void btnDeposit_Click(object sender, EventArgs e)
        {
            string _acc = account.Account;
            double _amount = (double)amount.Value;
            string _pass = pass.Text;

            if (MoniterHelper.WindowConfirm(string.Format("确认向主帐户:{0}所绑定的底层帐户入金:{1}", account.ConnectorToken, _amount)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqDepositMainAccount(_acc, _amount, _pass);
            }
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("BrokerRouterPassThrough", "QryBrokerAccountInfo", this.OnAccountInfo);
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("BrokerRouterPassThrough", "QryBrokerAccountInfo", this.OnAccountInfo);
    
        }

        void OnAccountInfo(string json,bool islast)
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

        }


        private void btnQry_Click(object sender, EventArgs e)
        {
            CoreService.TLClient.ReqQryConnectorAccountInfo(account.Account);
        }



    }
}
