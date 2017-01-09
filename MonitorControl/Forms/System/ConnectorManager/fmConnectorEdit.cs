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
        IConnectorControl connCtrl = null;

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

        

        void fmConnectorEdit_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
            token.Leave += new EventHandler(token_Leave);
            token.TextChanged += new EventHandler(token_TextChanged);

            cbinterfacelist.SelectedIndexChanged += new EventHandler(cbinterfacelist_SelectedIndexChanged);
            if (this.IsAddMode)
            {
                LoadInputControl(1);
            }
            else
            {
                LoadInputControl(_cfg.interface_fk);
            }
        }

        void token_TextChanged(object sender, EventArgs e)
        {
            if (this.IsAddMode)
            {
                if (string.IsNullOrEmpty(token.Text)) return;
                tokenvalid.Text = "...";
                tokenvalid.Visible = true;
                CoreService.TLClient.ReqQryTokenValid(token.Text);
            }
        }
        void token_Leave(object sender, EventArgs e)
        {
            if (this.IsAddMode)
            {
                if (string.IsNullOrEmpty(token.Text)) return;
                tokenvalid.Text = "...";
                tokenvalid.Visible = true;
                CoreService.TLClient.ReqQryTokenValid(token.Text);
            }
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


        void cbinterfacelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInputControl((int)cbinterfacelist.SelectedValue);
            if (connCtrl == null) return;
            this.token.Text = GenToken();
        }

        void connCtrl_IDChanged()
        {
            this.token.Text = GenToken();
        }

        string GenToken()
        {
            //分区-接口类型前缀-账户ID
            return string.Format("{0}-{1}-{2}",CoreService.SiteInfo.Domain.ID,connCtrl.Prefix, connCtrl.ID);
        }

        

        public void SetInterfaceCBList(ArrayList list)
        {
            MoniterHelper.AdapterToIDataSource(cbinterfacelist).BindDataSource(list);
        }

        bool IsAddMode { get { return _cfg == null; } }
        ConnectorConfig _cfg;
        public void SetConnectorConfig(ConnectorConfig cfg)
        {
            _cfg = cfg;
            token.Text = cfg.Token;
            name.Text = cfg.Name;
            this.Text = string.Format("编辑主帐户[{0}-{1}]", cfg.ID, cfg.Token);

            cbinterfacelist.SelectedValue = cfg.interface_fk;
            LoadInputControl(cfg.interface_fk);
            
            token.Enabled = false;
            cbinterfacelist.Enabled = false;
            btnSubmit.Enabled = true;

        }

        /// <summary>
        /// 加载接口设置面板
        /// 不同的接口接口参数不同
        /// </summary>
        /// <param name="interfaceId"></param>
        void LoadInputControl(int interfaceId)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(LoadInputControl), new object[] { interfaceId });
            }
            else
            {
                connCtrl = null;
                holder.Controls.Clear();
                switch (interfaceId)
                {
                    case 1:
                        {
                            ctBrokerCTPDirect input = new ctBrokerCTPDirect();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            connCtrl = input;
                            connCtrl.IDChanged += new Action(connCtrl_IDChanged);
                        }
                        break;
                    case 8:
                        {
                            ctBrokerESunnyDirect input = new ctBrokerESunnyDirect();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            connCtrl = input;
                            connCtrl.IDChanged += new Action(connCtrl_IDChanged);
                        }
                        break;
                    case 7:
                        {
                            ctBrokerIBDirect input = new ctBrokerIBDirect();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            connCtrl = input;
                            connCtrl.IDChanged += new Action(connCtrl_IDChanged);
                        }
                        break;
                        
                    default:
                        break;
                }
                LoadConfig();
            }
        }

        void LoadConfig()
        {
            if (connCtrl != null && _cfg != null)
                connCtrl.SetConnectorConfig(_cfg);
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (connCtrl == null)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("交易接口设置异常");
                return;
            }
            if (!connCtrl.Valid())
            {
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
                cfg.Token = token.Text;

                connCtrl.GetConnectorConfig(ref cfg);

                if (MoniterHelper.WindowConfirm("确认添加主帐户?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateConnectorConfig(cfg.SerializeObject());
                    this.Close();
                }
            }
            else
            {
                
                _cfg.Name = name.Text;
                _cfg.NeedVendor = true;

                connCtrl.GetConnectorConfig(ref _cfg);

                if (MoniterHelper.WindowConfirm("确认修改主帐户?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateConnectorConfig(_cfg.SerializeObject());
                    this.Close();
                }

            }
        }
    }
}
