﻿using System;
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
    public partial class ctGateWayYeePay : UserControl, IGateWayControl
    {
        public QSEnumGateWayType GateWayType { get { return QSEnumGateWayType.YeePay; } }
        public ctGateWayYeePay()
        {
            InitializeComponent();
        }

        public void SetGatewayConfig(GateWayConfig config)
        {
            var data = config.Config.DeserializeObject();
            try
            {
                payurl.Text = data["PayUrl"].ToString();
                mercode.Text = data["MerID"].ToString();
                MD5Key.Text = data["Key"].ToString();
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
                MerID = mercode.Text,
                Key = MD5Key.Text,
                Domain=domain.Text,
            }
            .SerializeObject();
        }
    }
}
