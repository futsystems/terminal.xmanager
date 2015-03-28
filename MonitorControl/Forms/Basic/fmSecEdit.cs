﻿using System;
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
    public partial class fmSecEdit : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmSecEdit()
        {
            InitializeComponent();

            MoniterHelper.AdapterToIDataSource(currency).BindDataSource(MoniterHelper.GetEnumValueObjects<CurrencyType>());
            MoniterHelper.AdapterToIDataSource(securitytype).BindDataSource(MoniterHelper.GetEnumValueObjects<SecurityType>());
            MoniterHelper.AdapterToIDataSource(exchange).BindDataSource(CoreService.BasicInfoTracker.GetExchangeCombList());
            MoniterHelper.AdapterToIDataSource(underlay).BindDataSource(CoreService.BasicInfoTracker.GetSecurityCombList(true));
            MoniterHelper.AdapterToIDataSource(markettime).BindDataSource(CoreService.BasicInfoTracker.GetMarketTimeCombList());


        }

        SecurityFamilyImpl _sec = null;
        //当前编辑的合约
        public SecurityFamilyImpl Security
        {
            get
            {
                return _sec;
            }
            set
            {
                this.Text = "编辑品种";
                _sec = value;
                code.Text = _sec.Code;
                name.Text = _sec.Name;
                currency.SelectedValue = _sec.Currency;
                securitytype.SelectedValue = _sec.Type;

                multiple.Value = _sec.Multiple;
                pricetick.Value = _sec.PriceTick;
                entrycommission.Value = _sec.EntryCommission;
                exitcommission.Value = _sec.ExitCommission;
                margin.Value = _sec.Margin;
                extramargin.Value = _sec.ExtraMargin;
                maintancemargin.Value = _sec.MaintanceMargin;
                exchange.SelectedValue = _sec.exchange_fk;
                underlay.SelectedValue = _sec.underlaying_fk;
                markettime.SelectedValue = _sec.mkttime_fk;
                tradeable.Checked = _sec.Tradeable;

            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_sec != null)
            {
                //
                _sec.Code = code.Text;
                _sec.Name = name.Text;
                _sec.Currency = (CurrencyType)currency.SelectedValue;
                _sec.Type = (SecurityType)securitytype.SelectedValue;

                _sec.Multiple = (int)multiple.Value;
                _sec.PriceTick = pricetick.Value;
                _sec.EntryCommission = entrycommission.Value;
                _sec.ExitCommission = exitcommission.Value;
                _sec.Margin = margin.Value;
                _sec.ExtraMargin = extramargin.Value;
                _sec.MaintanceMargin = maintancemargin.Value;
                _sec.exchange_fk = (int)exchange.SelectedValue;
                _sec.underlaying_fk = (int)underlay.SelectedValue;
                _sec.mkttime_fk = (int)markettime.SelectedValue;
                _sec.Tradeable = tradeable.Checked;

                CoreService.TLClient.ReqUpdateSecurity(_sec);
            }
            else
            {
                SecurityFamilyImpl target = new SecurityFamilyImpl();

                target.ID = 0;//0标识新增 数据库ID非0
                target.Code = code.Text;
                target.Name = name.Text;
                target.Currency = (CurrencyType)currency.SelectedValue;
                target.Type = (SecurityType)securitytype.SelectedValue;

                target.Multiple = (int)multiple.Value;
                target.PriceTick = pricetick.Value;
                target.EntryCommission = entrycommission.Value;
                target.ExitCommission = exitcommission.Value;
                target.Margin = margin.Value;
                target.ExtraMargin = extramargin.Value;
                target.MaintanceMargin = maintancemargin.Value;
                target.exchange_fk = (int)exchange.SelectedValue;

                target.underlaying_fk = (int)underlay.SelectedValue;
                target.mkttime_fk = (int)markettime.SelectedValue;
                target.Tradeable = tradeable.Checked;

                CoreService.TLClient.ReqAddSecurity(target);
            }
            this.Close();
        }
    }
}
