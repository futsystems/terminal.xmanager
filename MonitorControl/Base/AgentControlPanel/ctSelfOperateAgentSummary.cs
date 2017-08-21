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
            this.Ratio = 1;
        }
        public decimal Ratio { get; set; } 
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

        bool _isRootView = false;
        public bool IsRootView
        {
            get
            {
                return _isRootView;
            }
            set
            {
                _isRootView = value;

                if (_isRootView)
                {
                    this.Width = 240;
                    this.panelLastEquity.Visible = false;
                    this.panelIncome.Location = new Point(0, 0);
                    this.panelCost.Location = new Point(120, 0);
                }
                else
                {
                    this.Width = 720;
                    this.lastequity.Visible = true;
                    this.lastequitylabel.Visible = true;
                    this.panelLastEquity.Visible = true;
                    this.panelLastEquity.Location = new Point(0, 0);
                    this.panelIncome.Location = new Point(120, 0);
                    this.panelCost.Location = new Point(240, 0);
                }


            }
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
                commissionincome.Text = (info.CommissioinIncome*this.Ratio).ToFormatStr();

                staticequity.Text = (info.StaticEquity*this.Ratio).ToFormatStr();
                canuseequity.Text = ((info.StaticEquity - info.SubStaticEquity)*this.Ratio).ToFormatStr();
                nowequity.Text = (info.NowEquity*this.Ratio).ToFormatStr();
                flatequity.Text = (info.FlatEquity * this.Ratio).ToFormatStr();
            }
        }

    }
}
