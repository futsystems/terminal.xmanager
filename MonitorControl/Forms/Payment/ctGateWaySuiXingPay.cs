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
    public partial class ctGateWaySuiXingPay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.SuiXingPay; } }
        public ctGateWaySuiXingPay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            merNo.Text = data["MerNo"].ToString();
            merPrivateKey.Text = data["PrivateKey"].ToString();
            tfbPublicKey.Text= data["PublickKey"].ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                MerNo = merNo.Text,
                PrivateKey = merPrivateKey.Text,
                PublickKey = tfbPublicKey.Text,
            }
            .SerializeObject();
        }
    }
}
