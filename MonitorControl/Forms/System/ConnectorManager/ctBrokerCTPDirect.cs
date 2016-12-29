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


namespace TradingLib.MoniterControl
{
    public partial class ctBrokerCTPDirect : UserControl
    {
        ConnectorConfig _cfg;
        public ctBrokerCTPDirect()
        {
            InitializeComponent();
        }


        public void SetConnectorConfig(ConnectorConfig cfg)
        {
            _cfg = cfg;
            address.Text = cfg.srvinfo_ipaddress;
            port.Text = cfg.srvinfo_port.ToString();
            username.Text = cfg.usrinfo_userid;
            pass.Text = cfg.usrinfo_password;
            uf1.Text = cfg.usrinfo_field1;

            this.Text = string.Format("编辑主帐户[{0}-{1}]", cfg.ID, cfg.Token);

        }
    }
}
