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
    public partial class ctSelfOperateAgentSummary : UserControl
    {
        public ctSelfOperateAgentSummary()
        {
            InitializeComponent();
            panelIncome.StateCommon.Color1 = Color.DimGray;
            panelLastEquity.StateCommon.Color1 = Color.DimGray;
            panelCost.StateCommon.Color1 = Color.DimGray;
        }

        const string EMPTY = "--";
        public void Reset()
        {
            lastequity.Text = EMPTY;
            commissioncost.Text = EMPTY;
            commissionincome.Text = EMPTY;
            staticequity.Text = EMPTY;
            canuseequity.Text = EMPTY;
            nowequity.Text = EMPTY;
            flatequity.Text = EMPTY;


        }

        public void GotAgentStatic(AgentStatistic info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AgentStatistic>(GotAgentStatic), new object[] { info });
            }
            else
            {
                lastequity.Text = info.LastEquity.ToFormatStr();
                commissioncost.Text = info.CommissionCost.ToFormatStr();
                commissionincome.Text = info.CommissioinIncome.ToFormatStr();

                staticequity.Text = info.StaticEquity.ToFormatStr();
                canuseequity.Text = (info.StaticEquity - info.SubStaticEquity).ToFormatStr();
                nowequity.Text = info.NowEquity.ToFormatStr();
                flatequity.Text = info.FlatEquity.ToFormatStr();
            }
        }

    }
}
