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
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public partial class ctGateWayDinPay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.UnsPay; } }
        public ctGateWayDinPay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            mercode.Text = data["MerCode"].ToString();
            merPrivateKey.Text = data["MerPrivateKey"].ToString();
            dinPublicKey.Text= data["DinPublickKey"].ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                MerCode = mercode.Text,
                MerPrivateKey = merPrivateKey.Text,
                DinPublickKey=dinPublicKey.Text
            }
            .SerializeObject();
        }
    }
}
