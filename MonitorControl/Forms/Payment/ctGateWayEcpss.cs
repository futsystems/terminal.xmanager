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
    public partial class ctGateWayEcpss : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.Ecpss; } }
        public ctGateWayEcpss()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            merno.Text = data["MerNo"].ToString();
            md5key.Text = data["MD5Key"].ToString();
            var val = data["Domain"];
            domain.Text = val == null ? string.Empty : val.ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                MerNo = merno.Text,
                MD5Key = md5key.Text,
                Domain=domain.Text,
            }
            .SerializeObject();
        }
    }
}
