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
    public partial class ctFollowItemTrade : UserControl
    {
        const string empty = "--";
        public ctFollowItemTrade()
        {
            InitializeComponent();
            tradeIDs.Text = empty;
            
            xSize.Text = empty;
            xPrice.Text = empty;
        }

        public void GotFollowItemTradeInfo(FollowItemTradeInfo info)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FollowItemTradeInfo>(GotFollowItemTradeInfo), new object[] { info });
            }
            {
                tradeIDs.Text = string.Format("{0}/{1}", info.LocalTradeID, info.RemoteTradeID);
                //remoteTradeID.Text = info.RemoteTradeID;
                xSize.Text = info.Size.ToString();
                xPrice.Text = Util.FormatDecimal(info.Price);
            }
        }
    }
}
