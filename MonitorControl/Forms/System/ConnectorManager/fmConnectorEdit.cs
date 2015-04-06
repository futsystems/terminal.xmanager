using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;




namespace TradingLib.MoniterControl
{
    public partial class fmConnectorEdit : ComponentFactory.Krypton.Toolkit.KryptonForm, IEventBinder
    {
        public fmConnectorEdit()
        {
            InitializeComponent();
            this.Text = "添加主帐户";
            btnSubmit.Enabled = false;

            this.Load += new EventHandler(fmConnectorEdit_Load);
            tokenvalid.Visible = false;
        }
        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("ConnectorManager", "QryTokenValid", this.OnQryTokenValid);

        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("ConnectorManager", "QryTokenValid", this.OnQryTokenValid);
        }

        void OnQryTokenValid(string json, bool islast)
        {
            bool valid = MoniterHelper.ParseJsonResponse<bool>(json);
            if (valid)
            {
                btnSubmit.Enabled = true;
                tokenvalid.Text = "可用";
            }
            else
            {
                btnSubmit.Enabled = false;
                tokenvalid.Text = "不可用";
            }
        }

        void fmConnectorEdit_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
            token.Leave += new EventHandler(token_Leave);
        }

        void token_Leave(object sender, EventArgs e)
        {
            if (isadd)
            {
                tokenvalid.Text = "...";
                tokenvalid.Visible = true;
                CoreService.TLClient.ReqQryTokenValid(token.Text);
            }
        }

        public void SetInterfaceCBList(ArrayList list)
        {
            MoniterHelper.AdapterToIDataSource(cbinterfacelist).BindDataSource(list);
        }
        bool isadd = true;
        ConnectorConfig _cfg;
        public void SetConnectorConfig(ConnectorConfig cfg)
        {
            _cfg = cfg;
            //id.Text = cfg.ID.ToString();
            token.Text = cfg.Token;
            address.Text = cfg.srvinfo_ipaddress;
            port.Text = cfg.srvinfo_port.ToString();
            //srvf1.Text = cfg.srvinfo_field1;
            //srvf2.Text = cfg.srvinfo_field2;
            //srvf3.Text = cfg.srvinfo_field3;
            username.Text = cfg.usrinfo_userid;
            pass.Text = cfg.usrinfo_password;
            uf1.Text = cfg.usrinfo_field1;
            uf2.Text = cfg.usrinfo_field2;
            cbinterfacelist.SelectedValue = cfg.interface_fk;
            name.Text = cfg.Name;
            this.Text = string.Format("编辑主帐户[{0}-{1}-{2}]",cfg.ID, cfg.Token,cfg.usrinfo_userid);

            token.Enabled = false;
            cbinterfacelist.Enabled = false;

            isadd = false;
            btnSubmit.Enabled = true;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!InputReg.ConnectorToken.IsMatch(token.Text))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("主帐户编号只允许使用字母和数字");
                return;
            }
            if (!InputReg.ServerPort.IsMatch(port.Text))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入正确的交易端口");
                return;
            }
            System.Net.IPAddress outip;
            if (!System.Net.IPAddress.TryParse(address.Text, out outip))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入有效的IP地址");
                return;
            }
            if (string.IsNullOrEmpty(name.Text))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请输入主帐户名称");
                return;
            }
            //新增
            if (_cfg == null)
            {
                ConnectorConfig cfg = new ConnectorConfig();
                cfg.interface_fk = int.Parse(cbinterfacelist.SelectedValue.ToString());
                cfg.Name = name.Text;
                cfg.srvinfo_ipaddress = address.Text;
                cfg.srvinfo_port = int.Parse(port.Text);
                //cfg.srvinfo_field1 = srvf1.Text;
                //cfg.srvinfo_field2 = srvf2.Text;
                //cfg.srvinfo_field3 = srvf3.Text;

                cfg.usrinfo_userid = username.Text;
                cfg.usrinfo_password = pass.Text;
                cfg.usrinfo_field1 = uf1.Text;
                cfg.usrinfo_field2 = uf2.Text;
                cfg.Token = token.Text;
                if (MoniterHelper.WindowConfirm("确认添加主帐户?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateConnectorConfig(TradingLib.Mixins.Json.JsonMapper.ToJson(cfg));
                    this.Close();
                }
            }
            else
            {
                _cfg.srvinfo_ipaddress = address.Text;
                _cfg.srvinfo_port = int.Parse(port.Text);
                //_cfg.srvinfo_field1 = srvf1.Text;
                //_cfg.srvinfo_field2 = srvf2.Text;
                //_cfg.srvinfo_field3 = srvf3.Text;

                _cfg.usrinfo_userid = username.Text;
                _cfg.usrinfo_password = pass.Text;
                _cfg.usrinfo_field1 = uf1.Text;
                _cfg.usrinfo_field2 = uf2.Text;
                _cfg.Name = name.Text;
                _cfg.NeedVendor = true;
                if (MoniterHelper.WindowConfirm("确认修改主帐户?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateConnectorConfig(TradingLib.Mixins.Json.JsonMapper.ToJson(_cfg));
                    this.Close();
                }

            }
        }
    }
}
