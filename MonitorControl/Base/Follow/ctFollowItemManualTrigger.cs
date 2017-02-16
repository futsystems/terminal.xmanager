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
    public partial class ctFollowItemManualTrigger : UserControl
    {
        public ctFollowItemManualTrigger()
        {
            InitializeComponent();
        }

        public void GotManualTrigger(FollowItemManualTriggerInfo signal)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<FollowItemManualTriggerInfo>(GotManualTrigger), new object[] { signal });
            }
            else
            {
                //remoteTradeID.Text = signal.RemoteTradeID;
                side.Text = signal.Side ? "买入" : "卖出";
                side.StateCommon.ShortText.Color1 = signal.Side ? UIConstant.LongSideColor : UIConstant.ShortSideColor;
                symbol.Text = signal.Symbol;
                size.Text = Math.Abs(signal.Size).ToString();
                price.Text = signal.Price.ToFormatStr();
            }
        }
    }
}
