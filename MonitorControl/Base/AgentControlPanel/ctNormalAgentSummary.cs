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

namespace TradingLib.MoniterControl
{
    public partial class ctNormalAgentSummary : UserControl
    {
        public ctNormalAgentSummary()
        {
            InitializeComponent();
            this.Ratio = 1;
        }
        public decimal Ratio { get; set; }
        const string EMPTY = "--";
        public void Reset()
        {
            lastequity.Text = EMPTY;
            commissioncost.Text = EMPTY;
            commissionincome.Text = EMPTY;
          
        }

        public void GotAgentStatic(AgentStatistic info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AgentStatistic>(GotAgentStatic), new object[] { info });
            }
            else
            {
                lastequity.Text = (info.LastEquity*this.Ratio).ToFormatStr();
                commissioncost.Text = (info.CommissionCost*this.Ratio).ToFormatStr();
                commissionincome.Text = (info.CommissioinIncome * this.Ratio).ToFormatStr();
            }
        }
    }
}
