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
    public partial class ctGateWayAliPay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.AliPay; } }
        public ctGateWayAliPay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            memberId.Text = data["Partner"].ToString();
            md5key.Text = data["Key"].ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                Partner = memberId.Text,
                Key = md5key.Text
            }
            .SerializeObject();
        }
    }
}
