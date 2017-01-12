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
    public partial class fmCoreStatus : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmCoreStatus()
        {
            InitializeComponent();
            //this.CancelButton = 
            this.Load += new EventHandler(fmCoreStatus_Load);
        }

        void fmCoreStatus_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void GotSystemStatus(SystemStatus s)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SystemStatus>(GotSystemStatus), new object[] { s });
            }
            else
            {
                lastsettleday.Text = s.LastSettleday.ToString();
                currenttradingday.Text = s.Tradingday.ToString();
                nextsettletime.Text = Util.ToDateTime(s.NextSettleTime).ToString("yyyyMMdd HH:mm:ss");
                settlecentrestatus.Text = s.IsSettleNormal ? "正常" : "异常";
                ccstatus.Text = s.IsClearCentreLive ? "开启" : "关闭";

                unsettledacctordernum.Text = s.UnsettledAcctOrderNumOfPreSettleday.ToString();
                unsettledbrokerordernum.Text = s.UnsettledBrokerOrderNumOfPreSettleday.ToString();
                //totalordernum.Text = GetClearCentreStatus(s.ClearCentreStatus);//? "开启" : "关闭";
                totalaccountnum.Text = s.TotalAccountNum.ToString();
                totalordernum.Text = s.TotalOrderNum.ToString();
                totaltradenum.Text = s.TotalTradeNum.ToString();
                
            }
        }


        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_SYSTEM_STATUS, this.OnQrySystemStatus);
            CoreService.TLClient.ReqQrySystemStatus();
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_SYSTEM_STATUS, this.OnQrySystemStatus);
        }
        void OnQrySystemStatus(string json, bool islast)
        {
            SystemStatus status = CoreService.ParseJsonResponse<SystemStatus>(json);
            if (status != null)
            {
                GotSystemStatus(status);
            }
        }

    }
}
