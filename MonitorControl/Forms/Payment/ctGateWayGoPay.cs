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
    public partial class ctGateWayGoPay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.GoPay; } }
        public ctGateWayGoPay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            payurl.Text = data["PayUrl"].ToString();
            merid.Text = data["MerID"].ToString();
            VerifyCode.Text = data["VerficationCode"].ToString();
            accNo.Text = data["AccNo"].ToString();
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
                MerID = merid.Text,
                VerficationCode = VerifyCode.Text,
                AccNo = accNo.Text,
                Domain= domain.Text,
            }
            .SerializeObject();
        }
    }
}
