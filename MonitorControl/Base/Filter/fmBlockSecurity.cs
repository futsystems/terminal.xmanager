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
using TradingLib.Protocol;
using TradingLib.MoniterCore;



namespace TradingLib.MoniterControl
{
    public partial class fmBlockSecurity : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmBlockSecurity()
        {
            InitializeComponent();
            this.Load +=new EventHandler(fmBlockSecurity_Load);
        }

        void fmBlockSecurity_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            CoreService.EventCore.RegIEventHandler(this);
            CoreService.TLClient.ReqQrySecurityBlocked();
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            CoreService.TLClient.ReqBlockSecurity(seclist.Text.Split(','));
        }


        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.RiskCentre, Method_RiskCentre.QRY_SEC_BLOCKED, this.OnQrySecBlocked);
            CoreService.EventCore.RegisterCallback(Modules.RiskCentre, Method_RiskCentre.REQ_BLOCK_SEC, this.OnQrySecBlocked);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.RiskCentre, Method_RiskCentre.QRY_SEC_BLOCKED, this.OnQrySecBlocked);
            CoreService.EventCore.UnRegisterCallback(Modules.RiskCentre, Method_RiskCentre.REQ_BLOCK_SEC, this.OnQrySecBlocked);
        }

        void OnQrySecBlocked(string json,bool islast)
        {
            string[] req = CoreService.ParseJsonResponse<string[]>(json);
            secblocked.Text = string.Join(",", req);
        }


    }
}
