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
    public partial class ctGateWayBaoFu : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.BaoFu; } }
        public ctGateWayBaoFu()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();

            lbpayurl.Text =data["PayUrl"].ToString();
            lbmemberId.Text = data["MemberID"].ToString();
            lbterminalId.Text = data["TerminalID"].ToString();
            lbversion.Text = data["InterfaceVersion"].ToString();
            lbkeytype.Text = data["KeyType"].ToString();
            lbmd5key.Text = data["Md5Key"].ToString();
        }

        public string GetGateWayConfig()
        {
            return new
            {
                PayUrl = lbpayurl.Text,
                MemberID = lbmemberId.Text,
                TerminalID = lbterminalId.Text,
                InterfaceVersion = lbversion.Text,
                KeyType = lbkeytype.Text,
                Md5Key = lbmd5key.Text,

            }.SerializeObject();
        }
    }
}
