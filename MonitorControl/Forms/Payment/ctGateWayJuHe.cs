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
    public partial class ctGateWayJuHe : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.JuHe; } }
        public ctGateWayJuHe()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            mercode.Text = data["APPID"].ToString();
            MD5Key.Text = data["Key"].ToString();
            try
            {
                pubkey.Text = data["PubKey"].ToString();
            }
            catch (Exception ex)
            { 
            
            }
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                APPID = mercode.Text,
                Key = MD5Key.Text,
                PubKey = pubkey.Text,
            }
            .SerializeObject();
        }
    }
}
