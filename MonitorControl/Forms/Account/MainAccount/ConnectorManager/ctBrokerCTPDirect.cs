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
    public partial class ctBrokerCTPDirect : UserControl,IConnectorControl
    {
        ConnectorConfig _cfg;
        public ctBrokerCTPDirect()
        {
            InitializeComponent();
            this.Load += new EventHandler(OnLoad);
        }

        void OnLoad(object sender, EventArgs e)
        {
            username.Leave += new EventHandler(username_Leave);
        }

        void username_Leave(object sender, EventArgs e)
        {
            this.IDChanged();
        }


        public void SetConnectorConfig(ConnectorConfig cfg)
        {
            _cfg = cfg;
            address.Text = cfg.srvinfo_ipaddress;
            port.Text = cfg.srvinfo_port.ToString();
            username.Text = cfg.usrinfo_userid;
            pass.Text = cfg.usrinfo_password;
            uf1.Text = cfg.usrinfo_field1;
        }

        public void GetConnectorConfig(ref ConnectorConfig cfg)
        {
            cfg.srvinfo_ipaddress = address.Text;
            cfg.srvinfo_port = int.Parse(port.Text);
            cfg.usrinfo_userid = username.Text;
            cfg.usrinfo_password = pass.Text;
            cfg.usrinfo_field1 = uf1.Text;//BroekrID

        }

        /// <summary>
        /// 验证参数
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            if (!InputReg.ServerPort.IsMatch(port.Text))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入正确的交易端口");
                return false;
            }
            System.Net.IPAddress outip;
            if (!System.Net.IPAddress.TryParse(address.Text, out outip))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入有效的IP地址");
                return false;
            }
            return true;

        }

        public string ID { get { return username.Text; } }

        public event Action IDChanged = delegate { };

        public string Prefix { get { return "CTP"; } }
    }
}
