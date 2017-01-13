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
    public partial class fmFollowStrategyCfg : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmFollowStrategyCfg()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(direction).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowDirection>());

            MoniterHelper.AdapterToIDataSource(entrypricetype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowPriceType>());
            MoniterHelper.AdapterToIDataSource(entrypendingtype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumPendingThresholdType>());
            MoniterHelper.AdapterToIDataSource(entrypendingoptype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumPendingOperationType>());

            MoniterHelper.AdapterToIDataSource(exitpricetype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowPriceType>());
            MoniterHelper.AdapterToIDataSource(exitpendingtype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumPendingThresholdType>());
            MoniterHelper.AdapterToIDataSource(exitpendingoptype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumPendingOperationType>());

            this.Text = "添加跟单策略";
            this.Load += new EventHandler(fmFollowStrategyCfg_Load);
        }

        void fmFollowStrategyCfg_Load(object sender, EventArgs e)
        {
            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);
            CoreService.EventCore.RegIEventHandler(this);
            
        }

        public void OnInit()
        {
            if (_cfg == null)
            {
                CoreService.EventCore.RegisterCallback("FollowCentre", "QryAvabileStrategyAccount", OnQryAvabileStrategyAccount);
          
                CoreService.TLClient.ReqContribRequest("FollowCentre", "QryAvabileStrategyAccount","");
     
            }
        }

        public void OnDisposed()
        {
            if (_cfg == null)
            {
                CoreService.EventCore.UnRegisterCallback("FollowCentre", "QryAvabileStrategyAccount", OnQryAvabileStrategyAccount);
          
            }
        
        }


        void OnQryAvabileStrategyAccount(string json,bool last)
        {
            string[] accounts = CoreService.ParseJsonResponse<string[]>(json);
            if (accounts != null)
            {
                InvokeGotAvabileStrategyAccount(accounts);
            }
        }

        void InvokeGotAvabileStrategyAccount(string[] accounts)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string[]>(InvokeGotAvabileStrategyAccount), new object[] { accounts });
            }
            else
            {
                ArrayList acclist = new ArrayList();
                foreach (string account in accounts)
                {
                    ValueObject<string> vo = new ValueObject<string>();
                    vo.Name = account;
                    vo.Value = account;
                    acclist.Add(vo);
                }
                MoniterHelper.AdapterToIDataSource(followaccount).BindDataSource(acclist);

            }
        }


        void btnSubmit_Click(object sender, EventArgs e)
        {
            //添加
            if (_cfg == null)
            {
                if (MoniterHelper.WindowConfirm("确认添加跟单策略?") == System.Windows.Forms.DialogResult.Yes)
                {
                    FollowStrategyConfig cfg = new FollowStrategyConfig();

                    cfg.Token = token.Text;
                    cfg.FollowDirection = (QSEnumFollowDirection)direction.SelectedValue;
                    cfg.FollowPower = (int)power.Value;
                    cfg.Account = (string)followaccount.SelectedValue;

                    cfg.EntryPriceType = (QSEnumFollowPriceType)entrypricetype.SelectedValue;
                    cfg.EntryOffsetTicks = (int)entrypricetickoffset.Value;
                    cfg.EntryPendingThresholdType = (QSEnumPendingThresholdType)entrypendingtype.SelectedValue;
                    cfg.EntryPendingThresholdValue = (int)entrypendingvalue.Value;
                    cfg.EntryPendingOperationType = (QSEnumPendingOperationType)entrypendingoptype.SelectedValue;

                    cfg.ExitPriceType = (QSEnumFollowPriceType)exitpricetype.SelectedValue;
                    cfg.ExitOffsetTicks = (int)exitpricetickoffset.Value;
                    cfg.ExitPendingThreadsholdType = (QSEnumPendingThresholdType)exitpendingtype.SelectedValue;
                    cfg.ExitPendingThresholdValue = (int)exitpendingvalue.Value;
                    cfg.ExitPendingOperationType = (QSEnumPendingOperationType)exitpendingoptype.SelectedValue;

                    CoreService.TLClient.ReqContribRequest("FollowCentre", "UpdateFollowStrategyCfg", cfg);
                    this.Close();
                }
            }
            else
            {
                if (MoniterHelper.WindowConfirm("确认更新跟单策略参数?") == System.Windows.Forms.DialogResult.Yes)
                {
                    _cfg.EntryPriceType = (QSEnumFollowPriceType)entrypricetype.SelectedValue;
                    _cfg.EntryOffsetTicks = (int)entrypricetickoffset.Value;
                    _cfg.EntryPendingThresholdType = (QSEnumPendingThresholdType)entrypendingtype.SelectedValue;
                    _cfg.EntryPendingThresholdValue = (int)entrypendingvalue.Value;
                    _cfg.EntryPendingOperationType = (QSEnumPendingOperationType)entrypendingoptype.SelectedValue;

                    _cfg.ExitPriceType = (QSEnumFollowPriceType)exitpricetype.SelectedValue;
                    _cfg.ExitOffsetTicks = (int)exitpricetickoffset.Value;
                    _cfg.ExitPendingThreadsholdType = (QSEnumPendingThresholdType)exitpendingtype.SelectedValue;
                    _cfg.ExitPendingThresholdValue = (int)exitpendingvalue.Value;
                    _cfg.ExitPendingOperationType = (QSEnumPendingOperationType)exitpendingoptype.SelectedValue;

                    CoreService.TLClient.ReqContribRequest("FollowCentre", "UpdateFollowStrategyCfg", _cfg);

                    this.Close();
                }

            }

        }

        FollowStrategyConfig _cfg = null;
        public void SetFollowStrategyConfig(FollowStrategyConfig cfg)
        {
            this.Text = string.Format("编辑跟单策略:{0}[{1}]", cfg.Token, cfg.ID);
            _cfg = cfg;
            token.Text = cfg.Token;
            direction.SelectedValue = cfg.FollowDirection;
            power.Value = cfg.FollowPower;
            followaccount.Text = cfg.Account;

            entrypricetype.SelectedValue = cfg.EntryPriceType;
            entrypricetickoffset.Value = cfg.EntryOffsetTicks;
            entrypendingtype.SelectedValue = cfg.EntryPendingThresholdType;
            entrypendingvalue.Value = cfg.EntryPendingThresholdValue;
            entrypendingoptype.SelectedValue = cfg.EntryPendingOperationType;

            exitpricetype.SelectedValue = cfg.ExitPriceType;
            exitpricetickoffset.Value = cfg.ExitOffsetTicks;
            exitpendingtype.SelectedValue = cfg.ExitPendingThreadsholdType;
            exitpendingvalue.Value = cfg.ExitPendingThresholdValue;
            exitpendingoptype.SelectedValue = cfg.ExitPendingOperationType;

            token.Enabled = false;
            direction.Enabled = false;
            //power.Enabled = false;
            followaccount.Enabled = false;
        }
    }
}
