using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using TradingLib.Protocol.MainAcctFinService;

namespace TradingLib.MoniterControl
{
   


    public partial class fmEditService : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmEditService()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(cbServiceType).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFinServiceType>());
            MoniterHelper.AdapterToIDataSource(cbChargeFreq).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumChargeFreq>());
            MoniterHelper.AdapterToIDataSource(cbInteresetType).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumInterestType>());
            MoniterHelper.AdapterToIDataSource(cbChargeTime).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumChargeTime>());
            MoniterHelper.AdapterToIDataSource(cbChargeMethod).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumChargeMethod>());
            

            this.Load += new EventHandler(fmEditService_Load);
        }

        void fmEditService_Load(object sender, EventArgs e)
        {
            WireEvent();
            OnServiceTypeChange();
        }


        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("MainAcctFinService", "QryFinService", this.OnFinService);
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MainAcctFinService", "QryFinService", this.OnFinService);
        }


        void WireEvent()
        {
            cbServiceType.SelectedIndexChanged += new EventHandler(cbServiceType_SelectedIndexChanged);
            cbInteresetType.SelectedIndexChanged += new EventHandler(cbInteresetType_SelectedIndexChanged);
            cbChargeFreq.SelectedIndexChanged += new EventHandler(cbChargeFreq_SelectedIndexChanged);
            cbChargeValue.ValueChanged += new EventHandler(cbChargeValue_ValueChanged);

            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnDel.Click += new EventHandler(btnDel_Click);

            CoreService.EventCore.RegIEventHandler(this);
            ReqQryFinService(_account.Account);
            
        }

        void btnDel_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm(string.Format("确认删除交易帐户:{0}的配资服务", _account.Account)) == System.Windows.Forms.DialogResult.Yes)
            {
                ReqDelFinService(_account.Account);
            }
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            FinServiceSetting fs = new FinServiceSetting();
            fs.Account = _account.Account;
            fs.ChargeFreq = ChargeFreq;
            fs.ChargeMethod = (QSEnumChargeMethod)cbChargeMethod.SelectedValue;
            fs.ChargeTime = (QSEnumChargeTime)cbChargeTime.SelectedValue;
            fs.ChargeValue = cbChargeValue.Value;
            fs.InterestType = InterestType;
            fs.ServiceType = ServiceType;

            if (MoniterHelper.WindowConfirm(string.Format("确认更新交易帐户:{0}的配资服务", _account.Account)) == System.Windows.Forms.DialogResult.Yes)
            {
                ReqUpdateFinService(fs);
            }
        }


        void OnFinService(string json,bool islast)
        {
            FinServiceSetting fs = MoniterHelper.ParseJsonResponse<FinServiceSetting>(json);
            if (fs == null)
            {
                btnDel.Enabled = false;
            }
            else
            {
                cbServiceType.SelectedValue = fs.ServiceType;
                cbInteresetType.SelectedValue = fs.InterestType;
                cbChargeFreq.SelectedValue = fs.ChargeFreq;
                cbChargeTime.SelectedValue = fs.ChargeTime;
                cbChargeMethod.SelectedValue = fs.ChargeMethod;
                cbChargeValue.Value = fs.ChargeValue;
            }
               
            
        }
        #region 扩展模块接口函数

        /// <summary>
        /// 删除某个交易帐户的配资服务
        /// </summary>
        /// <param name="account"></param>
        void ReqDelFinService(string account)
        {
            CoreService.TLClient.ReqContribRequest("MainAcctFinService", "DelFinService", new { account = account });
        }

        void ReqUpdateFinService(FinServiceSetting fs)
        {
            CoreService.TLClient.ReqContribRequest("MainAcctFinService", "UpdateFinService", TradingLib.Mixins.Json.JsonMapper.ToJson(fs));
        }

        void ReqQryFinService(string account)
        {
            CoreService.TLClient.ReqContribRequest("MainAcctFinService", "QryFinService", new { account = account });
        }

        #endregion





        /// <summary>
        /// 收费方式
        /// </summary>
        private QSEnumFinServiceType ServiceType
        {
            get
            {
                return (QSEnumFinServiceType)cbServiceType.SelectedValue;
            }
        }

        /// <summary>
        /// 收费频率
        /// </summary>
        private QSEnumChargeFreq ChargeFreq
        {
            get
            {
                return (QSEnumChargeFreq)cbChargeFreq.SelectedValue;
            }
        }
        /// <summary>
        /// 计息方式
        /// </summary>
        private QSEnumInterestType InterestType
        {
            get
            {
                return (QSEnumInterestType)cbInteresetType.SelectedValue;
            }
        }

        void cbChargeValue_ValueChanged(object sender, EventArgs e)
        {
            ReGenDescription();
        }

        void cbChargeFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReGenDescription();
        }

        void cbServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnServiceTypeChange();
        }

        void OnServiceTypeChange()
        {
            if (ServiceType == QSEnumFinServiceType.Interest)
            {
                lbfreq.Visible = true;
                cbChargeFreq.Visible = true;
                cbInteresetType.Visible = true;
                OnInteresetTypeChange();
                lbpercent.Visible = false;

            }
            //按盈利分红 则不用设置收费频率
            if (ServiceType == QSEnumFinServiceType.Bonus)
            {
                lbfreq.Visible = false;
                cbChargeFreq.Visible = false;
                cbInteresetType.Visible = false;
                cbChargeValue.DecimalPlaces = 2;
                cbChargeValue.Maximum = 100;
                cbChargeValue.Minimum = 1;
                cbChargeValue.Value = 10;
                lbpercent.Visible = true;
            }
            ReGenDescription();
        }
        void cbInteresetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnInteresetTypeChange();
        }

        void OnInteresetTypeChange()
        {
            if (ServiceType == QSEnumFinServiceType.Interest)
            {
                if (InterestType == QSEnumInterestType.ByPercent)
                {
                    cbChargeValue.DecimalPlaces = 2;
                    cbChargeValue.Maximum = 100;
                    cbChargeValue.Minimum = 0.01M;
                    cbChargeValue.Value = 0.01M;
                }
                if (InterestType == QSEnumInterestType.ByPoint)
                {
                    cbChargeValue.DecimalPlaces = 2;
                    cbChargeValue.Maximum = 10000;
                    cbChargeValue.Minimum = 1;
                    cbChargeValue.Value = 10;
                }
                if (InterestType == QSEnumInterestType.ByMoney)
                {
                    cbChargeValue.DecimalPlaces = 2;
                    cbChargeValue.Maximum = 10000;
                    cbChargeValue.Minimum = 0M;
                    cbChargeValue.Value = 0;
                }
            }
            ReGenDescription();
        }

        void ReGenDescription()
        {
            description.Text = GetDescription();
        }

        decimal GetChargeValue()
        {
            if (ServiceType == QSEnumFinServiceType.Interest)
            {
                if (InterestType == QSEnumInterestType.ByPoint)
                {
                    return _account.Credit / 10000 * cbChargeValue.Value;
                }
                if (InterestType == QSEnumInterestType.ByPercent)
                {
                    return _account.Credit * cbChargeValue.Value / 100;
                }
                if (InterestType == QSEnumInterestType.ByMoney)
                {
                    return cbChargeValue.Value;
                }
            }
            return -1;
        }
        string GetDescription()
        {
            //按利息收取
            if (ServiceType == QSEnumFinServiceType.Interest)
            {
                return string.Format("{0}以{1}{2}收取利息,金额:{3}", Util.GetEnumDescription(ChargeFreq), cbChargeValue.Value.ToFormatStr(), Util.GetEnumDescription(InterestType), GetChargeValue().ToFormatStr());
            }
            return "未设置";
        }

        AccountLite _account;
        public void SetAccount(AccountLite account)
        {
            _account = account;
            this.Text = string.Format("配资服务设置 帐户[{0}] 优先资金:{1}万", _account.Account, (_account.Credit / 10000).ToFormatStr());
        }





    }
}
