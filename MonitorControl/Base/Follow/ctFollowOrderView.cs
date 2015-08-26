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
    public partial class ctFollowOrderView : UserControl
    {
        const string empty = "--";
        public ctFollowOrderView()
        {
            InitializeComponent();
            //orderIDs.Text = empty;
            orderIDs.Text = empty;
            status.Text = empty;
            symbol.Text = empty;
            side.Text = empty;
            sizeinfo.Text = empty;
            //sizeinfo.Text = empty;
            price.Text = empty;

            this.Size = new Size(this.Width,45);
        }

        public void GotOrder(FollowItemOrderInfo order)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FollowItemOrderInfo>(GotOrder), new object[] { order });
            }
            else
            {
                //orderIDs.Text = order.LocalOrderID;
                orderIDs.Text = string.Format("{0}/{1}", order.LocalOrderID, order.RemoteOrderID);
                //remoteOrderID.Text = order.RemoteOrderID;
                status.Text = order.Status;
                symbol.Text = order.Symbol;
                side.Text = order.Side ? "买入" : "卖出";
                sizeinfo.Text = string.Format("{0}/{1}", order.SentSize, order.FillSize);
                //fillsize.Text = order.FillSize.ToString();
                price.Text = Util.FormatDecimal(order.Price);

                foreach (var f in order.Trades)
                {
                    GotTrade(f);
                }
            }
        }

        /// <summary>
        /// 获得一条成交数据显示成交
        /// </summary>
        void GotTrade(FollowItemTradeInfo info)
        {
            ctFollowItemTrade trade = new ctFollowItemTrade();
            trade.GotFollowItemTradeInfo(info);

            trade.Location = new Point(0, tradepanel.Controls.Count*20);
            tradepanel.Controls.Add(trade);
            this.Size = new Size(this.Width,this.Height + 20);
        }


    }
}
