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

        public void GotAgentStatic(AgentStatistic info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AgentStatistic>(GotAgentStatic), new object[] { info });
            }
            else
            {
                margin.Text = info.CustMargin.ToFormatStr();
                marginfrzoen.Text = info.CustForzenMargin.ToFormatStr();
                realizedpl.Text = info.CustRealizedPL.ToFormatStr();
                unrealizedpl.Text = info.CustUnRealizedPL.ToFormatStr();
                cashin.Text = info.CustCashIn.ToFormatStr();
                cashout.Text = info.CustCashOut.ToFormatStr();

                longpos.Text = info.CustLongPositionSize.ToString();
                shortpos.Text = info.CustShortPositionSize.ToString();
            }
        }
    }
}
