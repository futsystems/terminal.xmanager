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
                currenttradingday.Text = s.CurrentTradingday.ToString();
                nexttradingday.Text = s.NextTradingday.ToString();
                settlecentrestatus.Text = s.IsSettleNormal ? "正常" : "异常";
                istradingday.Text = s.IsTradingday ? "开市" : "休市";

                clearcentrestatus.Text = GetClearCentreStatus(s.ClearCentreStatus);//? "开启" : "关闭";
                totalaccountnum.Text = s.TotalAccountNum.ToString();
                marketopencheck.Text = s.MarketOpenCheck ? "检查" : "不检查";
                runmode.Text = s.IsDevMode ? "开发" : "运营";
            }
        }

        string GetClearCentreStatus(QSEnumClearCentreStatus status)
        {
            if (status == QSEnumClearCentreStatus.CCOPEN)
            {
                return "是";
            }
            if (status == QSEnumClearCentreStatus.CCCLOSE)
            {
                return "否";
            }
            return Util.GetEnumDescription(status);
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QrySystemStatus", this.OnQrySystemStatus);
            CoreService.TLClient.ReqQrySystemStatus();
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QrySystemStatus", this.OnQrySystemStatus);
        }
        void OnQrySystemStatus(string json, bool islast)
        {
            SystemStatus status = MoniterHelper.ParseJsonResponse<SystemStatus>(json);
            if (status != null)
            {
                GotSystemStatus(status);
            }
        }

    }
}
