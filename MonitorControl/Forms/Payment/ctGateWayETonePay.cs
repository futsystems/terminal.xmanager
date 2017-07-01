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
    public partial class ctGateWayETonePay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.ETonePay; } }
        public ctGateWayETonePay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            merId.Text = data["MerID"].ToString();
            merKey.Text = data["MerKey"].ToString();
            bussid.Text = data["BussID"].ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                MerID = merId.Text,
                MerKey = merKey.Text,
                BussID = bussid.Text,
            }
            .SerializeObject();
        }
    }
}
