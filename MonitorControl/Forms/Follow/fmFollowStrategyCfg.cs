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
    public partial class fmFollowStrategyCfg : ComponentFactory.Krypton.Toolkit.KryptonForm
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
        

            this.Load += new EventHandler(fmFollowStrategyCfg_Load);
        }

        void fmFollowStrategyCfg_Load(object sender, EventArgs e)
        {
            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);
            
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_cfg == null)
            { 
            
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
