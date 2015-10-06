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
using System.Net;
using System.IO;
using TradingLib.Mixins.Json;
using System.Runtime.Remoting.Messaging;

namespace TradingLib.MoniterControl
{
    public partial class ctAccountInfo : UserControl,IEventBinder
    {
        public ctAccountInfo()
        {
            InitializeComponent();
            btnExecute.Enabled = false;
            btnUpdateTransferType.Enabled = false;
            btnUpdateInterday.Enabled = false;

            MoniterHelper.AdapterToIDataSource(cbTransferType).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumOrderTransferType>());
            MoniterHelper.AdapterToIDataSource(cbCurrnecy).BindDataSource(MoniterHelper.GetEnumValueObjects<CurrencyType>());
            this.Load += new EventHandler(ctAccountInfo_Load);
            
        }




        void ctAccountInfo_Load(object sender, EventArgs e)
        {
            btnUpdateInterday.Click += new EventHandler(btnUpdateInterday_Click);
            btnUpdateTransferType.Click += new EventHandler(btnUpdateTransferType_Click);
            btnExecute.Click += new EventHandler(btnExecute_Click);
            btnUpdateCurrency.Click += new EventHandler(btnUpdateCurrency_Click);

            cbHoldNight.CheckedChanged += new EventHandler(cbHoldNight_CheckedChanged);
            cbTransferType.SelectedIndexChanged += new EventHandler(cbTransferType_SelectedIndexChanged);
            cbCurrnecy.SelectedIndexChanged += new EventHandler(cbCurrnecy_SelectedIndexChanged);
            cbCurrnecy.Enabled = false;

            CoreService.EventAccount.OnAccountSelectedEvent += new Action<AccountLite>(OnAccountSelected);
            CoreService.EventAccount.OnAccountChangedEvent += new Action<AccountLite>(EventAccount_OnAccountChangedEvent);

            CoreService.EventCore.RegIEventHandler(this);
            
        }



        

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("MsgExch", "QrySessionInfo", OnSessionInfo);
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MsgExch", "QrySessionInfo", OnSessionInfo);
        }


        void OnSessionInfo(string json, bool islast)
        {
            SessionInfo info = MoniterHelper.ParseJsonResponse<SessionInfo>(json);
            if (info != null)
            {
                InvokeGotSessionInfo(info);
            }
        }

        void InvokeGotSessionInfo(SessionInfo info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SessionInfo>(InvokeGotSessionInfo), new object[] { info });
            }
            else
            {
                if (!string.IsNullOrEmpty(info.ClientID))
                {
                    lbFront.Text = string.IsNullOrEmpty(info.FrontID) ? "直连" : info.FrontID;
                    lbProductInfo.Text = info.ProductInfo;
                    lbIPAddress.Text = info.IPAddress;
                    if (!string.IsNullOrEmpty(info.IPAddress))
                    {
                        string[] rec = info.IPAddress.Split(':');
                        string ip =rec[0];
                        //异步查询物理位置
                        Func<string, string> del = new Func<string, string>(QryLocation);
                        del.BeginInvoke(ip, QryLocationCallback, null);
                    }
                }
            }
        }

        

        void InvokeGotGLocation(string location)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(InvokeGotGLocation), new object[] { location });
            }
            else
            {
                lbGLocation.Text = location;
            }
        }

        void btnExecute_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认" + (account.Execute ? "冻结" : "激活") + "交易帐号:" + account.Account + "?") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountExecute(account.Account, !account.Execute);
            }
        }

        void btnUpdateCurrency_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认更新交易帐户货币类型?") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountCurrency(account.Account, (CurrencyType)cbCurrnecy.SelectedValue);
            }
        }


        void EventAccount_OnAccountChangedEvent(AccountLite obj)
        {
            if (account != null && account.Account.Equals(obj.Account))
            {
                account = obj;
                lbAccountType.Text = Util.GetEnumDescription(account.Category);

                btnExecute.Text = obj.Execute ? "冻 结" : "激 活";
                btnExecute.StateCommon.Content.ShortText.Color1 = !obj.Execute ? UIConstant.ShortSideColor : UIConstant.LongSideColor;
            
                cbHoldNight.Checked = !account.IntraDay;
                cbTransferType.SelectedValue = account.OrderRouteType;
                btnUpdateInterday.Enabled = false;
                btnUpdateTransferType.Enabled = false;
                btnUpdateCurrency.Enabled = false;
            }
        }

        void cbTransferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account == null)
                return;
            if (account.OrderRouteType != (QSEnumOrderTransferType)cbTransferType.SelectedValue)
            {
                btnUpdateTransferType.Enabled = true;
            }
            else
            {
                btnUpdateTransferType.Enabled = false;
            }
        }

        void cbHoldNight_CheckedChanged(object sender, EventArgs e)
        {
            if (account == null)
                return;
            if (account.IntraDay != cbHoldNight.Checked)
            {
                btnUpdateInterday.Enabled = false;
            }
            else
            {
                btnUpdateInterday.Enabled = true;
            }
        }

        void cbCurrnecy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (account == null) return;
            if (account.Currency != (CurrencyType)cbCurrnecy.SelectedValue)
            {
                btnUpdateCurrency.Enabled = true;
            }
            else
            {
                btnUpdateCurrency.Enabled = false;
            }
        }

        void btnUpdateTransferType_Click(object sender, EventArgs e)
        {
            if (account == null)
            {
                MoniterHelper.WindowMessage("请选择交易帐号");
                return;
            }
            if (MoniterHelper.WindowConfirm("确认修改交易帐户:" + account.Account + "成交方式为:" + Util.GetEnumDescription((QSEnumOrderTransferType)cbTransferType.SelectedValue)) == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateRouteType(account.Account, (QSEnumOrderTransferType)cbTransferType.SelectedValue);
                //btnUpdateTransferType.Enabled = false;
            }
        }

        void btnUpdateInterday_Click(object sender, EventArgs e)
        {
            if (account == null)
            {
                MoniterHelper.WindowMessage("请选择交易帐号");
                return;
            }
            if (MoniterHelper.WindowConfirm("确认更新交易帐户:" + account.Account + "为" + (cbHoldNight.Checked ? "允许" : "禁止") + "隔夜") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountIntraday(account.Account, !cbHoldNight.Checked);
                
            }
        }


        AccountLite account = null;
        void OnAccountSelected(AccountLite obj)
        {
            account = obj;
            lbAccount.Text = account.Account;
            lbAccountType.Text = Util.GetEnumDescription(account.Category);

            btnExecute.Text = obj.Execute ? "冻 结" : "激 活";
            btnExecute.StateCommon.Content.ShortText.Color1 = !obj.Execute ? UIConstant.ShortSideColor : UIConstant.LongSideColor;
            
            cbHoldNight.Checked = !account.IntraDay;
            cbTransferType.SelectedValue = account.OrderRouteType;
            cbCurrnecy.SelectedValue = account.Currency;

            btnExecute.Enabled = true;

            QrySessionInfo(obj.Account);
        }


        void QrySessionInfo(string account)
        {
            lbFront.Text = "--";
            lbProductInfo.Text = "--";
            lbIPAddress.Text = "--";
            lbGLocation.Text = "--";

            CoreService.TLClient.ReqContribRequest("MsgExch", "QrySessionInfo", account);
        }


        void QryLocationCallback(IAsyncResult async)
        {
            Func<string, string> proc = ((AsyncResult)async).AsyncDelegate as Func<string, string>;
            string location = proc.EndInvoke(async);
            InvokeGotGLocation(location);
        }

        string QryLocation(string ip)
        {
            try
            {
                String direction = "";
                WebRequest request = WebRequest.Create("http://ip.taobao.com/service/getIpInfo.php?ip=" + ip);
                using (WebResponse response = request.GetResponse())
                using (StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GB18030")))
                {
                    direction = stream.ReadToEnd();
                    JsonData data = JsonMapper.ToObject(direction);
                    var code = int.Parse(data["code"].ToString());
                    if (code == 0)
                    {
                        var country = data["data"]["country"].ToString();
                        var area = data["data"]["area"].ToString();
                        var region = data["data"]["region"].ToString();
                        var city = data["data"]["city"].ToString();
                        var isp = data["data"]["isp"].ToString();

                        return string.Format("{0}-{1}-{2}({3})",country, region, city, isp);
                    }
                    return "返回错误";
                }
            }
            catch (Exception ex)
            {
                return "查询异常";
            }
        }
        private void kryptonButton1_Click(object sender, EventArgs e)
        {

            
        }

    }
}
