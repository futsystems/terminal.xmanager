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
    public partial class fmSettlement : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmSettlement()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmSettlement_Load);
            
        }

        void fmSettlement_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            CoreService.EventHub.OnSettlementEvent += new Action<string, RspInfo, int, bool>(GotSettlement);
        }

        public void OnDisposed()
        {
            CoreService.EventHub.OnSettlementEvent += new Action<string, RspInfo, int, bool>(GotSettlement);
        }

        public void SetAccount(string acc)
        {
            account.Text = acc;
        }

        StringBuilder sb = new StringBuilder();
        public void GotSettlement(string settlement,RspInfo info,int requestId,bool isLast)
        {
            sb.Append(settlement);
            if (isLast)
            {
                UpdateSettlebox(sb.ToString());
            }
            
        }

        void UpdateSettlebox(string content)
        {
            if (this.settlebox.InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateSettlebox), new object[] { content });
            }
            else
            {
                settlebox.Text = content;
            }
        }


        DateTime lastqrytime = DateTime.Now;

        private void btnQryHist_Click(object sender, EventArgs e)
        {
            sb.Clear();
            settlebox.Clear();
            if (string.IsNullOrEmpty(account.Text))
            {
                MoniterHelper.WindowMessage("请输入要查询的交易帐号");
                return;
            }
            string ac = account.Text;
           
            lastqrytime = DateTime.Now;

            CoreService.TLClient.ReqQryHistSettlement(ac, Settleday);
        }
        int Settleday
        {
            get
            {
                return TradingLib.Common.Util.ToTLDate(settleday.Value);
            }
        }
    }
}
