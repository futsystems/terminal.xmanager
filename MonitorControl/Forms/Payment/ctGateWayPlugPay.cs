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
    public partial class ctGateWayPlugPay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.PlugPay; } }
        public ctGateWayPlugPay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            AppID.Text = data["AppID"].ToString();
            AppSecret.Text = data["AppSecret"].ToString();
     
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                AppID = AppID.Text,
                AppSecret = AppSecret.Text,
              
            }
            .SerializeObject();
        }
    }
}
