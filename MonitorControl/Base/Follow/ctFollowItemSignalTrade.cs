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
    public partial class ctFollowItemSignalTrade : UserControl
    {
        public ctFollowItemSignalTrade()
        {
            InitializeComponent();
        }

        public void GotSignalTrade(FollowItemSignalTradeInfo signal)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FollowItemSignalTradeInfo>(GotSignalTrade), new object[] { signal });
            }
            else
            {
                tradeIDs.Text = string.Format("{0}/{1}", signal.LocalTradeID, signal.RemoteTradeID);
                //remoteTradeID.Text = signal.RemoteTradeID;
                side.Text = signal.Side ? "买入" : "卖出";
                side.StateCommon.ShortText.Color1 = signal.Side ? UIConstant.LongSideColor : UIConstant.ShortSideColor;
                symbol.Text = signal.Symbol;
                size.Text = signal.Size.ToString();
                price.Text = Util.FormatDecimal(signal.Price);
            }
        }
    }
}
