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
    public partial class ctCustSummary : UserControl
    {
        public ctCustSummary()
        {
            InitializeComponent();
            this.Ratio = 1;
        }

        const string EMPTY = "--";
        public void Reset()
        {
            margin.Text = EMPTY;
            marginfrzoen.Text = EMPTY;
            realizedpl.Text = EMPTY;
            unrealizedpl.Text = EMPTY;
            cashin.Text = EMPTY;
            cashout.Text = EMPTY;
            longpos.Text = EMPTY;
            shortpos.Text = EMPTY;
        }

        public decimal Ratio { get; set; }

        public void GotAgentStatic(AgentStatistic info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AgentStatistic>(GotAgentStatic), new object[] { info });
            }
            else
            {
                margin.Text = (info.CustMargin * Ratio).ToFormatStr();
                marginfrzoen.Text = (info.CustForzenMargin* Ratio).ToFormatStr();
                realizedpl.Text = (info.CustRealizedPL* Ratio).ToFormatStr();
                unrealizedpl.Text = (info.CustUnRealizedPL* Ratio).ToFormatStr();
                cashin.Text = (info.CustCashIn* Ratio).ToFormatStr();
                cashout.Text = (info.CustCashOut* Ratio).ToFormatStr();

                longpos.Text = info.CustLongPositionSize.ToString();
                shortpos.Text = info.CustShortPositionSize.ToString();
            }
        }
    }
}
