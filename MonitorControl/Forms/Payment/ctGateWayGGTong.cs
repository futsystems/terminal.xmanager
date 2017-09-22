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
    public partial class ctGateWayGGTong : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.GGTong; } }
        public ctGateWayGGTong()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            pid.Text = data["Partner"].ToString();
            merId.Text = data["UserSeller"].ToString();
            merKey.Text = data["MD5Key"].ToString();
            domain.Text = data["Domain"].ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                Partner = pid.Text,
                UserSeller = merId.Text,
                MD5Key = merKey.Text,
                Domain=domain.Text,
            }
            .SerializeObject();
        }
    }
}
