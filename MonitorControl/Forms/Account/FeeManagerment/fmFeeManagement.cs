﻿using System;
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
using TradingLib.Protocol.MainAcctFinService;


namespace TradingLib.MoniterControl
{
    public partial class fmFeeManagement : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmFeeManagement()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmFeeManagement_Load);
        }

        void fmFeeManagement_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("MainAcctFinService", "QryStatus", this.OnStatus);
            CoreService.TLClient.ReqContribRequest("MainAcctFinService", "QryStatus", "");

        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MainAcctFinService", "QryStatus", this.OnStatus);
        }


        string GetStatus(bool s)
        {
            return s ? "已执行" : "未执行";
        }
        void OnStatus(string json,bool islast)
        {
            FinServiceCentreStatus s = MoniterHelper.ParseJsonResponse<FinServiceCentreStatus>(json);
            if (s != null)
            {
                InvokeGotStatus(s);
            }
        }
        void InvokeGotStatus(FinServiceCentreStatus status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FinServiceCentreStatus>(InvokeGotStatus), new object[] { status });
            }
            else
            {
                chargeservicebefore.Text = GetStatus(status.ChargeServiceBeforeTimeSpan);
                qryafter.Text = GetStatus(status.QryAfterTimeSpane);
                chargecommissionafter.Text = GetStatus(status.ChargeCommissionAfterTimeSpan);
                chargeserviceafter.Text = GetStatus(status.ChargeServiceAfterTimeSpan);
            }

        }
    }
}
