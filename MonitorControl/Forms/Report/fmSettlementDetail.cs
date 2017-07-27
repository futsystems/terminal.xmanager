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
    public partial class fmSettlementDetail : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {

        StringBuilder sb = new StringBuilder();

        public fmSettlementDetail()
        {
            InitializeComponent();
            CoreService.EventCore.RegIEventHandler(this);
            
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_SETTLEMENT, OnSettlement);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_SETTLEMENT, OnSettlement);
        }

        public void SetAccount(string acc)
        {
            account.Text = acc;
        }

        public void SetSettleday(int day)
        {
            settleday.Value = Util.ToDateTime(day, 0);
        }

        void OnSettlement(string json, bool islast)
        {
            string settlement = CoreService.ParseJsonResponse<string>(json);
            sb.Append(settlement.Replace('*', '|') + "\n");
            if (islast)
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
        bool _first = true;
        private void btnQryHist_Click(object sender, EventArgs e)
        {
            if (_first)
            {
                _first = false;
            }
            else
            {
                if (!(DateTime.Now.Subtract(lastqrytime).TotalSeconds > 2))
                {
                    MoniterHelper.WindowMessage("请不要频繁查询,每隔2秒查询一次!");
                    return;
                }
            }

            if (string.IsNullOrEmpty(account.Text))
            {
                MoniterHelper.WindowMessage("请输入要查询的交易帐号");
                return;
            }

            this.QrySettlementDetail();
        }

        public void QrySettlementDetail()
        {
            if (_first)
            {
                _first = false;
            }

            sb.Clear();
            settlebox.Clear();
            lastqrytime = DateTime.Now;
            CoreService.TLClient.ReqQryAccountSettlement(account.Text, Settleday);

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
