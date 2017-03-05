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
    public partial class ctGateWayIPS : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.IPS; } }
        public ctGateWayIPS()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            merCode.Text = data["MerCode"].ToString();
            md5key.Text = data["Key"].ToString();
            account.Text = data["Account"].ToString();
            domain.Text = data["Domain"].ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                MerCode = merCode.Text,
                Account=account.Text,
                Key = md5key.Text,
                Domain=domain.Text
            }
            .SerializeObject();
        }
    }
}
