using System;
using System.Collections.Generic;
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
    public partial class fmPaymentGateway : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmPaymentGateway()
        {
            InitializeComponent();

            MoniterHelper.AdapterToIDataSource(gatewayType).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumGateWayType>());

            this.Load += new EventHandler(fmPaymentGateway_Load);
        }

        void fmPaymentGateway_Load(object sender, EventArgs e)
        {
            gatewayType.SelectedIndexChanged += new EventHandler(gatewayType_SelectedIndexChanged);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            gatewayType_SelectedIndexChanged(null, null);
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.APIService, Method_API.QRY_GATEWAY_CONFUIG, this.OnQryGateWayConfig);
            CoreService.TLClient.ReqQryGateWayConfig();
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.APIService, Method_API.QRY_GATEWAY_CONFUIG, this.OnQryGateWayConfig);
        }

        void OnQryGateWayConfig(string json, bool islast)
        {
            GateWayConfig config = json.DeserializeObject<GateWayConfig>();
            if (config != null)
            {
                _cfg = config;
            }
            if (islast && _cfg != null)
            {

                gatewayType.SelectedValue = _cfg.GateWayType;
                avabile.Checked = _cfg.Avabile;
                LoadInputControl(_cfg.GateWayType);

                LoadConfig();
            }
        }

        
        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_cfg == null)
            {
                GateWayConfig tmp = new GateWayConfig();
                tmp.Avabile = avabile.Checked;
                tmp.Config = gwControl.GetGateWayConfig();
                tmp.GateWayType = (QSEnumGateWayType)gatewayType.SelectedValue;

                if (ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("确认添加支付通道", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateGateWayConfig(tmp);
                }
            }
            else
            {
                GateWayConfig tmp = _cfg;
                tmp.Avabile = avabile.Checked;
                tmp.Config = gwControl.GetGateWayConfig();
                tmp.GateWayType = (QSEnumGateWayType)gatewayType.SelectedValue;

                if (ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("确认更新支付通道", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateGateWayConfig(tmp);
                }
            }
        }

        void gatewayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInputControl((QSEnumGateWayType)gatewayType.SelectedValue);
        }

        IGateWayControl gwControl = null;
        GateWayConfig _cfg = null;

        /// <summary>
        /// 加载接口设置面板
        /// 不同的接口接口参数不同
        /// </summary>
        /// <param name="interfaceId"></param>
        void LoadInputControl(QSEnumGateWayType gwtype)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<QSEnumGateWayType>(LoadInputControl), new object[] { gwtype });
            }
            else
            {
                gwControl = null;
                holder.Controls.Clear();
                switch (gwtype)
                {
                    case  QSEnumGateWayType.BaoFu:
                        {
                            ctGateWayBaoFu input = new ctGateWayBaoFu();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                          
                        }
                        break;
                    case QSEnumGateWayType.AliPay:
                        {
                            ctGateWayAliPay input = new ctGateWayAliPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.IPS:
                        {
                            ctGateWayIPS input = new ctGateWayIPS();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.UnsPay:
                        {
                            ctGateWayUnsPay input = new ctGateWayUnsPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.DinPay:
                        {
                            ctGateWayDinPay input = new ctGateWayDinPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.ChinagPay:
                        {
                            ctGateWayChinagPay input = new ctGateWayChinagPay();
                             holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.Cai1Pay:
                        {
                            ctGateWayCai1Pay input = new ctGateWayCai1Pay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.GoPay:
                        {
                            ctGateWayGoPay input = new ctGateWayGoPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.TFBPay:
                        {
                            ctGateWayTfbPay input = new ctGateWayTfbPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.MoBoPay:
                        {
                            ctGateWayMoBoPay input = new ctGateWayMoBoPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.SuiXingPay:
                        {
                            ctGateWaySuiXingPay input = new ctGateWaySuiXingPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.IELPMPay:
                        {
                            ctGateWayIELPMPay input = new ctGateWayIELPMPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.ETonePay:
                        {
                            ctGateWayETonePay input = new ctGateWayETonePay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.ZhiHPay:
                        {
                            ctGateWayZhiHPay input = new ctGateWayZhiHPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.FZPay:
                        {
                            ctGateWayFZPay input = new ctGateWayFZPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.DDBillPay:
                        {
                            ctGateWayDDBillPay input = new ctGateWayDDBillPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
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
            if (gwControl != null && _cfg != null && _cfg.GateWayType == gwControl.GateWayType)
                gwControl.SetGatewayConfig(_cfg);
        }

    }
}
