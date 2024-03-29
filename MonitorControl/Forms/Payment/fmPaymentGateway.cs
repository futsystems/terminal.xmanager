﻿using System;
using System.Collections;
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

            MoniterHelper.AdapterToIDataSource(gatewayType).BindDataSource(GetGateWayOptions());

            this.Load += new EventHandler(fmPaymentGateway_Load);
        }

        void fmPaymentGateway_Load(object sender, EventArgs e)
        {
            gatewayType.SelectedIndexChanged += new EventHandler(gatewayType_SelectedIndexChanged);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            gatewayType_SelectedIndexChanged(null, null);
            CoreService.EventCore.RegIEventHandler(this);
        }

        public static ArrayList GetGateWayOptions()
        {
            ArrayList list = new ArrayList();

            ValueObject<QSEnumGateWayType> vo1 = new ValueObject<QSEnumGateWayType>();
            vo1.Name = Util.GetEnumDescription(QSEnumGateWayType.PlugPay);
            vo1.Value = QSEnumGateWayType.PlugPay;
            list.Add(vo1);

            ValueObject<QSEnumGateWayType> vo2 = new ValueObject<QSEnumGateWayType>();
            vo2.Name = Util.GetEnumDescription(QSEnumGateWayType.BaoFu);
            vo2.Value = QSEnumGateWayType.BaoFu;
            list.Add(vo2);

            return list;
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
                    case QSEnumGateWayType.ZhiHPay:
                        {
                            ctGateWayZhiHPay input = new ctGateWayZhiHPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.Ecpss:
                        {
                            ctGateWayEcpss input = new ctGateWayEcpss();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.Se7Pay:
                        {
                            ctGateWaySe7Pay input = new ctGateWaySe7Pay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.QianTong:
                        {
                            ctGateWayQianTong input = new ctGateWayQianTong();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.Fjelt:
                        {
                            ctGateWayFjelt input = new ctGateWayFjelt();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.XiaoXiaoPay:
                        {
                            ctGateWayXiaoXiaoPay input = new ctGateWayXiaoXiaoPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.ZhongWeiPay:
                        {
                            ctGateWayZhongWeiPay input = new ctGateWayZhongWeiPay();
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
                    case QSEnumGateWayType.P101KA:
                        {
                            ctGateWay101KA input = new ctGateWay101KA();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.NewPay:
                        {
                            ctGateWayNewPay input = new ctGateWayNewPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;

                    case QSEnumGateWayType.HuiCX:
                        {
                            ctGateWayHuiCXPay input = new ctGateWayHuiCXPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.GGTong:
                        {
                            ctGateWayGGTong input = new ctGateWayGGTong();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.OpenEPay:
                        {
                            ctGateWayOpenEPay input = new ctGateWayOpenEPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.UnionPay:
                        {
                            ctGateWayUnionPay input = new ctGateWayUnionPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.Pay848:
                        {
                            ctGateWayPay848 input = new ctGateWayPay848();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.Shopping98:
                        {
                            ctGateWayShopping98 input = new ctGateWayShopping98();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.JoinPay:
                        {
                            ctGateWayJoinPay input = new ctGateWayJoinPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.JuHe:
                        {
                            ctGateWayJuHe input = new ctGateWayJuHe();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.HMPay:
                        {
                            ctGateWayHMPay input = new ctGateWayHMPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.C9Pay:
                        {
                            ctGateWayC9Pay input = new ctGateWayC9Pay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.YeePay:
                        {
                            ctGateWayYeePay input = new ctGateWayYeePay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.GYPay:
                        {
                            ctGateWayGY input = new ctGateWayGY();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.UUOPay:
                        {
                            ctGateWayUUOPay input = new ctGateWayUUOPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.RMTech:
                        {
                            ctGateWayRMTech input = new ctGateWayRMTech();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.HaiFu:
                        {
                            ctGateWayHaiFu input = new ctGateWayHaiFu();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.PlugPay:
                        {
                            ctGateWayPlugPay input = new ctGateWayPlugPay();
                            holder.Controls.Add(input);
                            input.Dock = DockStyle.Fill;
                            gwControl = input;
                        }
                        break;
                    case QSEnumGateWayType.ETonePay:
                        {
                            ctGateWayETone input = new ctGateWayETone();
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
