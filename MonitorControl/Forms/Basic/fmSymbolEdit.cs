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
    public partial class fmSymbolEdit : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        bool _loaded = false;
        public fmSymbolEdit()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(cbexchange).BindDataSource(CoreService.BasicInfoTracker.GetExchangeCombList());
            MoniterHelper.AdapterToIDataSource(cboptionside).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumOptionSide>());
            MoniterHelper.AdapterToIDataSource(cbexpiremonth).BindDataSource(CoreService.BasicInfoTracker.GetExpireMonth());
            entrycommission.Value = -1;
            exitcommission.Value = -1;
            margin.Value = -1;
            extramargin.Value = -1;
            maintancemargin.Value = -1;
            gp_option.Visible = false;// Globals.UIAccess.sectype_option;
            gp_lotto.Visible = false;// Globals.UIAccess.sectype_lotto;
            _loaded = true;
            WireEvent();

        }

        private void SymbolEditForm_Load(object sender, EventArgs e)
        {
            OnCBExchangeChanged();
            OnCBSecurityChanged();
        }


        SymbolImpl _symbol = null;
        public SymbolImpl Symbol
        {
            get
            {
                return _symbol;
            }

            set
            {
                _symbol = value;

                _loaded = false;

                this.Text = "编辑合约[" + _symbol.Symbol + "]";
                //.Debug("set symbol:" + _symbol.Symbol);
                //将symbol显示到界面 
                int exid = _symbol.SecurityFamily != null ? (_symbol.SecurityFamily.Exchange as Exchange).ID : 0;
                cbexchange.SelectedValue = exid;
                cbexchange.Enabled = false;

                MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(CoreService.BasicInfoTracker.GetSecurityCombListViaExchange(exid));
                cbsecurity.SelectedValue = _symbol.SecurityFamily != null ? (_symbol.SecurityFamily as SecurityFamilyImpl).ID : 0;
                cbsecurity.Enabled = false;
                symbol.Text = _symbol.Symbol;
                //Globals.Debug("set symbol:" + _symbol.Symbol);
                symbol.Enabled = false;

                MoniterHelper.AdapterToIDataSource(cbexpiremonth).BindDataSource(CoreService.BasicInfoTracker.GetExpireMonth());
                cbexpiremonth.SelectedValue = (int)_symbol.ExpireDate / 100;//_symbol.ExpireMonth;
                cbexpiremonth.Enabled = false;

                if (_symbol.SecurityFamily.Type != SecurityType.OPT)
                {
                    cboptionside.SelectedValue = QSEnumOptionSide.NULL;
                }

                if (_symbol.SecurityFamily.Type == SecurityType.OPT)
                {
                    cboptionside.SelectedValue = _symbol.OptionSide;
                    strike.Value = _symbol.Strike;

                }
                cboptionside.Enabled = false;
                strike.Enabled = false;
                cblottolevel.Enabled = false;
                cbulsymbol.Enabled = false;

                this.entrycommission.Value = _symbol._entrycommission;
                this.exitcommission.Value = _symbol._exitcommission;
                this.margin.Value = _symbol._margin;
                this.extramargin.Value = _symbol._extramargin;
                this.maintancemargin.Value = _symbol._maintancemargin;

                this.tradeable.Checked = _symbol.Tradeable;
                //this.tradeable.Text = this.tradeable.Checked ? "可交易" : "不可交易";
                this.expiredate.Value = (_symbol.ExpireDate == 0 ? DateTime.Now:Util.ToDateTime(_symbol.ExpireDate,0));

            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_symbol != null)
            {
                _symbol.EntryCommission = entrycommission.Value;
                _symbol.ExitCommission = exitcommission.Value;
                _symbol.Margin = margin.Value;
                _symbol.ExtraMargin = extramargin.Value;
                _symbol.MaintanceMargin = maintancemargin.Value;
                _symbol.Tradeable = tradeable.Checked;
                _symbol.ExpireDate = Util.ToTLDate(this.expiredate.Value);
                //MessageBox.Show("it is here");
                CoreService.TLClient.ReqUpdateSymbol(_symbol);
            }
            else
            {
                SymbolImpl target = new SymbolImpl();

                target.Symbol = symbol.Text;


                target.EntryCommission = entrycommission.Value;
                target.ExitCommission = exitcommission.Value;
                target.Margin = margin.Value;
                target.ExtraMargin = extramargin.Value;
                target.MaintanceMargin = maintancemargin.Value;
                target.ExpireDate = Util.ToTLDate(this.expiredate.Value);
                //MessageBox.Show("expire:" + target.ExpireDate.ToString());
                SecurityFamilyImpl sec = CurrentSecurity;
                if (sec == null)
                {
                    MoniterHelper.WindowMessage("请选择品种!");
                    return;
                }

                target.security_fk = sec.ID;

                //按照品种设定对应的信息
                switch (sec.Type)
                {
                    case SecurityType.STK:
                        target.Strike = 0;
                        target.OptionSide = QSEnumOptionSide.NULL;
                        //target.ExpireMonth = 0;
                        target.ExpireDate = 0;
                        break;
                    case SecurityType.FUT:
                        target.Strike = 0;
                        target.OptionSide = QSEnumOptionSide.NULL;
                        //target.ExpireMonth = CurrentExpireMonth;
                        //target.ExpireDate = 0;
                        break;
                    case SecurityType.OPT:
                        target.Strike = strike.Value;
                        if (target.Strike == 0)
                        {
                            MoniterHelper.WindowMessage("期权合约需要填写对应的执行价!");
                            return;
                        }
                        target.OptionSide = (QSEnumOptionSide)cboptionside.SelectedValue;
                        if (target.OptionSide == QSEnumOptionSide.NULL)
                        {
                            MoniterHelper.WindowMessage("期权合约需要选择期权方向!");
                            return;
                        }

                        break;
                    default:
                        target.Strike = 0;
                        target.OptionSide = QSEnumOptionSide.NULL;
                        //target.ExpireMonth = 0;
                        target.ExpireDate = 0;
                        break;
                }

                target.Tradeable = tradeable.Checked;


                CoreService.TLClient.ReqAddSymbol(target);
            }

            this.Close();
        }

        SecurityFamilyImpl CurrentSecurity
        {
            get
            {
                int id = (int)cbsecurity.SelectedValue;
                return CoreService.BasicInfoTracker.GetSecurity(id);//获得当前选中的security
            }
        }


        int CurrentExpireMonth
        {
            get
            {
                int id = (int)cbexpiremonth.SelectedValue;
                return id;
            }
        }

        void WireEvent()
        {
            cbexchange.SelectedIndexChanged += new EventHandler(cbexchange_SelectedIndexChanged);
            cbsecurity.SelectedIndexChanged += new EventHandler(cbsecurity_SelectedIndexChanged);
            cbexpiremonth.SelectedIndexChanged += new EventHandler(cbexpiremonth_SelectedIndexChanged);
            
            btnSubmit.Click +=new EventHandler(btnSubmit_Click);
        }

 



        /// <summary>
        /// 选择交易所 动态更新品种列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbexchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCBExchangeChanged();
        }

        void OnCBExchangeChanged()
        {
            if (!_loaded) return;
            int exid = (int)cbexchange.SelectedValue;

            MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(CoreService.BasicInfoTracker.GetSecurityCombListViaExchange(exid));

            OnCBExpiremonth();
        }

        /// <summary>
        /// 选择品种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbsecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCBSecurityChanged();
        }

        void OnCBSecurityChanged()
        {
            if (!_loaded) return;
            try
            {
                SecurityFamilyImpl sec = CurrentSecurity;

                cboptionside.Enabled = false;
                strike.Enabled = false;
                cbulsymbol.Enabled = false;
                cblottolevel.Enabled = false;

                if (sec == null) return;
                switch (sec.Type)
                {
                    case SecurityType.OPT:
                        cboptionside.SelectedValue = QSEnumOptionSide.CALL;
                        cboptionside.Enabled = true;
                        strike.Value = 0;
                        strike.Enabled = true;
                        break;
                    case SecurityType.INNOV://异化合约
                        cbulsymbol.Enabled = true;
                        cblottolevel.Enabled = true;
                        break;
                    default:
                        cboptionside.SelectedValue = QSEnumOptionSide.NULL;
                        cboptionside.Enabled = false;
                        strike.Enabled = false;
                        break;

                }
                OnCBExpiremonth();
            }
            catch (Exception ex)
            {
                //Globals.Debug("error securitychanged:" + ex.ToString());
            }
        }

        private void cbexpiremonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCBExpiremonth();
        }

        void OnCBExpiremonth()
        {
            if (!_loaded) return;
            try
            {
                int month = (int)cbexpiremonth.SelectedValue;
                SecurityFamilyImpl sec = CurrentSecurity;
                if (sec == null) return;

                symbol.Text = MoniterHelper.GenSymbol(sec, month);
                //MessageBox.Show(month.ToString());
                this.expiredate.Value = Util.ToDateTime(month * 100 + 30, 0);

            }
            catch (Exception ex)
            {
                //Globals.Debug("error expiremonth:" + ex.ToString());
            }
        }



    }
}
