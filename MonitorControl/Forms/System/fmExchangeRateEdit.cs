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
    public partial class fmExchangeRateEdit : Form
    {
        public fmExchangeRateEdit()
        {
            InitializeComponent();
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认更新汇率信息?") == System.Windows.Forms.DialogResult.Yes)
            {
                _rate.IntermediateRate = inter_rate.Value;
                _rate.AskRate = ask_rate.Value;
                _rate.BidRate = bid_rate.Value;

                CoreService.TLClient.ReqContribRequest("MgrExchServer", "UpdateExchangeRate", _rate);
                this.Close();
            }
        }

        ExchangeRate _rate = null;
        public void SetExchangeRate(ExchangeRate rate)
        {
            _rate = rate;
            currency.Text = _rate.Currency.ToString();
            inter_rate.Value = _rate.IntermediateRate;
            ask_rate.Value = _rate.AskRate;
            bid_rate.Value = _rate.BidRate;

        }
    }
}
