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
    public partial class ctGateWayZhiHPay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.ZhiHPay; } }
        public ctGateWayZhiHPay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            mercode.Text = data["MerCode"].ToString();
            merPrivateKey.Text = data["MerPrivateKey"].ToString();
            dinPublicKey.Text= data["ZhiHPublickKey"].ToString();
            var val = data["Domain"];
            domain.Text = val == null ? string.Empty : val.ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                MerCode = mercode.Text,
                MerPrivateKey = merPrivateKey.Text,
                ZhiHPublickKey = dinPublicKey.Text,
                Domain=domain.Text,
            }
            .SerializeObject();
        }
    }
}
