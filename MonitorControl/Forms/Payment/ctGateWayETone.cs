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
    public partial class ctGateWayETone : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.ETonePay; } }
        public ctGateWayETone()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            mercode.Text = data["MerID"].ToString();
            MD5Key.Text = data["MerKey"].ToString();
            businessId.Text = data["BussID"].ToString();
     
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                MerID = mercode.Text,
                MerKey = MD5Key.Text,
                BussID = businessId.Text,
            }
            .SerializeObject();
        }
    }
}
