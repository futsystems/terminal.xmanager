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
    public partial class ctGateWayFjelt : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.Fjelt; } }
        public ctGateWayFjelt()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            appid.Text = data["APPID"].ToString();
            session.Text = data["Session"].ToString();
            key.Text= data["Key"].ToString();
          
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                APPID = appid.Text,
                Session = session.Text,
                Key = key.Text,
            }
            .SerializeObject();
        }
    }
}
