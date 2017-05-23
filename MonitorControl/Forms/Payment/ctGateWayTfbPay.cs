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
    public partial class ctGateWayTfbPay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.TFBPay; } }
        public ctGateWayTfbPay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            spid.Text = data["SPID"].ToString();
            spuserid.Text = data["SPUserID"].ToString();
            merPrivateKey.Text = data["PrivateKey"].ToString();
            tfbPublicKey.Text= data["PublickKey"].ToString();
            md5key.Text = data["MD5Key"].ToString();
            test.Checked = bool.Parse(data["Test"].ToString());
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = payurl.Text,
                SPID = spid.Text,
                SPUserID = spuserid.Text,
                PrivateKey = merPrivateKey.Text,
                PublickKey = tfbPublicKey.Text,
                MD5Key = md5key.Text,
                Test = test.Checked,
            }
            .SerializeObject();
        }
    }
}
