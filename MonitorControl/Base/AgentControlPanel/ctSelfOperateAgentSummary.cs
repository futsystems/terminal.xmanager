using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
