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


namespace TradingLib.MoniterControl
{
    public partial class fmStatistic : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmStatistic()
        {
            InitializeComponent();
        }


        ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid;

        public void SetGrid(ComponentFactory.Krypton.Toolkit.KryptonDataGridView g)
        {
            grid = g;
        }

        public void Update()
        {
            Statistic s = new Statistic();

            for(int i=0;i<grid.Rows.Count;i++)
            {
                var account = grid.Rows[i].Cells["ACCOUNTTAG"].Value as AccountItem;
                if (account != null)
                {
                    s.LastEquity += account.LastEquity;
                }
            }

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                var tmp = grid.Rows[i].Cells["ACCOUNTTAG2"].Value as AccountStatistic;
                if (tmp != null)
                {
                    s.Credit += tmp.Credit;
                    s.Margin += tmp.Margin;
                    s.FrozenMargin += tmp.ForzenMargin;
                    s.RealizedPL += tmp.RealizedPL;
                    s.UnRealizedPL += tmp.UnRealizedPL;
                    s.Comission += tmp.Commission;
                    s.Profit += tmp.Profit;
                    s.Position += tmp.TotalPositionSize;
                }
            }

            lastequity.Text = s.LastEquity.ToFormatStr();
            credit.Text = s.Credit.ToFormatStr();
            margin.Text = s.Margin.ToFormatStr();
            frozenmargin.Text = s.FrozenMargin.ToFormatStr();
            realizedpl.Text = s.RealizedPL.ToFormatStr();
            unrealizedpl.Text = s.UnRealizedPL.ToFormatStr();
            commission.Text = s.Comission.ToFormatStr();
            profit.Text = s.Profit.ToFormatStr();
            position.Text = s.Position.ToString();
            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.Update();
        }
    }

    class Statistic
    {
        public decimal LastEquity { get; set; }

        public decimal Credit { get; set; }

        public decimal Margin { get; set; }

        public decimal FrozenMargin { get; set; }

        public decimal RealizedPL { get; set; }

        public decimal UnRealizedPL { get; set; }

        public decimal Comission { get; set; }

        public decimal Profit { get; set; }

        public int Position { get; set; }
    }
}
