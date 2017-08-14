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
    public partial class ctGateWaySe7Pay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.Se7Pay; } }
        public ctGateWaySe7Pay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            mercode.Text = data["CompanyID"].ToString();
            merPrivateKey.Text = data["PrivateKey"].ToString();
            dinPublicKey.Text= data["PublickKey"].ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                CompanyID = mercode.Text,
                PrivateKey = merPrivateKey.Text,
                PublickKey = dinPublicKey.Text,
            }
            .SerializeObject();
        }
    }
}
