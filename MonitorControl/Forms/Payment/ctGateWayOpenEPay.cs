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
    public partial class ctGateWayOpenEPay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.OpenEPay; } }
        public ctGateWayOpenEPay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            merid.Text = data["MerID"].ToString();
            Md5Key.Text = data["MD5Key"].ToString();
            domain.Text = data["Domain"].ToString();

        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                MerID = merid.Text,
                MD5Key = Md5Key.Text,
                Domain= domain.Text,
            }
            .SerializeObject();
        }
    }
}
