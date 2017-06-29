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
    public partial class ctGateWayMoBoPay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.MoBoPay; } }
        public ctGateWayMoBoPay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            MerNo.Text = data["MerNo"].ToString();
            MD5Key.Text = data["MD5Key"].ToString();
            try
            {
                domain.Text = data["Domain"].ToString();
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
                MerNo = MerNo.Text,
                MD5Key = MD5Key.Text,
                Domain= domain.Text,
            }
            .SerializeObject();
        }
    }
}
