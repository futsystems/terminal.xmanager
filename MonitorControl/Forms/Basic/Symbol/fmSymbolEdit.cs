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
    public partial class fmSymbolEdit : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        bool _loaded = false;
        public fmSymbolEdit()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(cbexchange).BindDataSource(MoniterHelper.GetExchangeComboxArray());
            MoniterHelper.AdapterToIDataSource(cboptionside).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumOptionSide>());
            MoniterHelper.AdapterToIDataSource(cbexpiremonth).BindDataSource(MoniterHelper.GetExpireMonthComboxArray());
            MoniterHelper.AdapterToIDataSource(cbSymbolType).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumSymbolType>());
            
            entrycommission.Value = -1;
            exitcommission.Value = -1;
            exitcommissiontoday.Value = -1;
            margin.Value = -1;
            //extramargin.Value = -1;
            //maintancemargin.Value = -1;
            _loaded = true;
            WireEvent();

        }

        private void SymbolEditForm_Load(object sender, EventArgs e)
        {

        }


        public void OnInit()
        {
            if (!CoreService.SiteInfo.Domain.Super)
            {
                expiredate.Enabled = false;
            }
        }

        public void OnDisposed()
        { 
        
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
                int exid = _symbol.SecurityFamily != null ? (_symbol.SecurityFamily.Exchange as ExchangeImpl).ID : 0;
                cbexchange.SelectedValue = exid;
                cbexchange.Enabled = false;

                MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(MoniterHelper.GetSecurityComboxArrayViaExchange(exid));
                cbsecurity.SelectedValue = _symbol.SecurityFamily != null ? (_symbol.SecurityFamily as SecurityFamilyImpl).ID : 0;
                cbsecurity.Enabled = false;

                symbol.Text = _symbol.Symbol;
                

                symbol.Enabled = false;

                symbol_input.Visible = false;
                lbSymbolName.Visible = false;
                symName.Visible = false;

                cbexpiremonth.Enabled = false;
                expiredate.Enabled = false;
                cbSymbolType.Enabled = false;
                cboptionside.Enabled = false;
                strike.Enabled = false;


                if (_symbol.SecurityFamily.Type == SecurityType.FUT)
                {
                    //绑定月份
                    MoniterHelper.AdapterToIDataSource(cbexpiremonth).BindDataSource(MoniterHelper.GetExpireMonthComboxArray());
                    cbexpiremonth.SelectedValue = (int)_symbol.ExpireDate / 100;
                    cbexpiremonth.Enabled = false;

                    //设定过期日
                    this.expiredate.Value = (_symbol.ExpireDate == 0 ? DateTime.Now : Util.ToDateTime(_symbol.ExpireDate, 0));
                    cbSymbolType.Enabled = false;
                    if (_symbol.SymbolType == QSEnumSymbolType.MonthContinuous)
                    {
                        this.expiredate.Enabled = false;
                    }
                }

                //cbCurrency.SelectedValue = _symbol.Currency;

                if (_symbol.SecurityFamily.Type != SecurityType.OPT)
                {
                    cboptionside.SelectedValue = QSEnumOptionSide.NULL;
                }

                if (_symbol.SecurityFamily.Type == SecurityType.OPT)
                {
                    cboptionside.SelectedValue = _symbol.OptionSide;
                    strike.Value = _symbol.Strike;

                }
                if (_symbol.SecurityFamily.Type == SecurityType.STK)
                {
                    symbol_input.Visible = true;
                    lbSymbolName.Visible = true;
                    symName.Visible = true;

                    symbol_input.Text = _symbol.Symbol;
                    symName.Text = _symbol.Name;

                }



                this.entrycommission.Value = _symbol._entrycommission;
                this.exitcommission.Value = _symbol._exitcommission;
                this.exitcommissiontoday.Value = _symbol._exitcommissiontoday;
                this.margin.Value = _symbol._margin;
                this.tradeable.Checked = _symbol.Tradeable;
                
                
            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_symbol != null)
            {
                _symbol.EntryCommission = entrycommission.Value;
                _symbol.ExitCommission = exitcommission.Value;
                _symbol.ExitCommissionToday = exitcommissiontoday.Value;
                _symbol.Margin = margin.Value;
                //_symbol.ExtraMargin = extramargin.Value;
                //_symbol.MaintanceMargin = maintancemargin.Value;
                _symbol.Tradeable = tradeable.Checked;
                switch(_symbol.SecurityFamily.Type)
                {
                    case SecurityType.FUT:
                        _symbol.ExpireDate = Util.ToTLDate(this.expiredate.Value);
                        _symbol.SymbolType = (QSEnumSymbolType)cbSymbolType.SelectedValue;
                        break;
                    case SecurityType.STK:
                        _symbol.Name = symName.Text;
                        break;
                    default:
                        break;
                }

                CoreService.TLClient.ReqUpdateSymbol(_symbol);
            }
            else
            {
                SecurityFamilyImpl sec = CurrentSecurity;
                if (sec == null)
                {
                    MoniterHelper.WindowMessage("请选择品种!");
                    return;
                }


                SymbolImpl target = new SymbolImpl();

                target.Symbol = symbol.Text;
                target.SymbolType = (QSEnumSymbolType)cbSymbolType.SelectedValue; 

                target.EntryCommission = entrycommission.Value;
                target.ExitCommission = exitcommission.Value;
                target.ExitCommissionToday = exitcommissiontoday.Value;
                target.Margin = margin.Value;

                target.security_fk = sec.ID;

                //按照品种设定对应的信息
                switch (sec.Type)
                {
                    case SecurityType.STK:
                        target.Strike = 0;
                        target.OptionSide = QSEnumOptionSide.NULL;
                        
                        target.ExpireDate = 0;
                        target.Month = "";

                        target.Symbol = symbol_input.Text;//股票不自动生成合约编码 通过手工输入
                        target.Name = symName.Text;
                        break;
                    case SecurityType.FUT:
                        target.Strike = 0;
                        target.OptionSide = QSEnumOptionSide.NULL;

                        //标准合约设定到期日
                        if (target.SymbolType == QSEnumSymbolType.Standard)
                        {
                            int fullmonth = (int)cbexpiremonth.SelectedValue;
                            DateTime dt = GetExpireDateTime(fullmonth);

                            target.Month = MoniterHelper.GetMonth(fullmonth);
                            if (this.expiredate.Value < dt.FirstDayOfMonth() || this.expiredate.Value > dt.LastDayOfMonth())
                            {
                                MoniterHelper.WindowMessage("到期日选择不正确");
                                return;
                            }
                            target.ExpireDate = Util.ToTLDate(this.expiredate.Value);
                        }
                        else
                        {
                            target.Month = (string)cbexpiremonth.SelectedValue;
                            target.ExpireDate = 0;
                        }

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


                CoreService.TLClient.ReqUpdateSymbol(target);
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
            cbSymbolType.SelectedIndexChanged += new EventHandler(cbSymbolType_SelectedIndexChanged);
            btnSubmit.Click +=new EventHandler(btnSubmit_Click);
            this.Load += new EventHandler(fmSymbolEdit_Load);
        }

        void fmSymbolEdit_Load(object sender, EventArgs e)
        {
            cbexchange_SelectedIndexChanged(null,null);
        }

        

 



        /// <summary>
        /// 交易所变动
        /// 动态更新品种列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbexchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loaded) return;
            int exid = (int)cbexchange.SelectedValue;
            MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(MoniterHelper.GetSecurityComboxArrayViaExchange(exid));

            GenSymbolName();
        }


        /// <summary>
        /// 品种变化后需要修改界面参数可见部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbsecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loaded) return;
            try
            {
                SecurityFamilyImpl sec = CurrentSecurity;

                symbol.Visible = false;
                symbol_input.Visible = false;
                lbSymbolName.Visible = false;
                symName.Visible = false;

                cboptionside.Enabled = false;
                strike.Enabled = false;

                if (sec == null) return;
                switch (sec.Type)
                {
                    case SecurityType.IDX:
                        cbexpiremonth.Enabled = false;
                        expiredate.Enabled = false;
                        symbol.Visible = true;
                        break;

                    case SecurityType.OPT:
                        cboptionside.SelectedValue = QSEnumOptionSide.CALL;
                        cboptionside.Enabled = true;
                        strike.Value = 0;
                        strike.Enabled = true;
                        break;

                    case SecurityType.STK:
                        cbexpiremonth.Enabled = false;
                        expiredate.Enabled = false;
                        cbSymbolType.Enabled = false;

                        //股票手工输入Symbol
                        symbol_input.Visible = true;
                        lbSymbolName.Visible = true;
                        symName.Visible = true;
                        break;
                    case SecurityType.FUT:
                        symbol.Visible = true;
                        cbexpiremonth.Enabled = true;
                        expiredate.Enabled = true;
                        cbSymbolType.Enabled = true;
                        break;
                    default:
                        cboptionside.SelectedValue = QSEnumOptionSide.NULL;
                        cboptionside.Enabled = false;
                        strike.Enabled = false;
                        break;

                }
            }
            catch (Exception ex)
            {
                //Globals.Debug("error securitychanged:" + ex.ToString());
            }

            GenSymbolName();
        }

        /// <summary>
        /// 通过201501来获得到期月初时间
        /// </summary>
        /// <param name="expire"></param>
        /// <returns></returns>
        DateTime GetExpireDateTime(int expire)
        {
            //month 201602
            int year = expire / 100;
            int month = expire - year * 100;
            //获得某个月最后一天
            DateTime tmp = new DateTime(year, month, 1, 0, 0, 0);
            return tmp;
        }
        /// <summary>
        /// 合约月份变化
        /// 更新过期日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbexpiremonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loaded) return;
            try
            {
                int expire = (int)cbexpiremonth.SelectedValue;
                SecurityFamilyImpl sec = CurrentSecurity;
                if (sec == null) return;
                //如果是标准合约则更新过期日期
                QSEnumSymbolType symboltype = (QSEnumSymbolType)cbSymbolType.SelectedValue;
                
                //MessageBox.Show("year:" + year.ToString() + " month:" + m.ToString());
                if (symboltype == QSEnumSymbolType.Standard)
                {
                    this.expiredate.Value = GetExpireDateTime(expire).LastDayOfMonth();
                }

            }
            catch (Exception ex)
            {
                //Globals.Debug("error expiremonth:" + ex.ToString());
            }

            GenSymbolName();
        }


        void cbSymbolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            QSEnumSymbolType symboltype = (QSEnumSymbolType)cbSymbolType.SelectedValue;
            switch (symboltype)
            { 
                case QSEnumSymbolType.Standard:
                    expiredate.Enabled = true;
                    MoniterHelper.AdapterToIDataSource(cbexpiremonth).BindDataSource(MoniterHelper.GetExpireMonthComboxArray());
           
                    break;
                case QSEnumSymbolType.MonthContinuous:
                    expiredate.Enabled = false;
                    MoniterHelper.AdapterToIDataSource(cbexpiremonth).BindDataSource(MoniterHelper.GenExpireMonthWithOutYear());
           
                    break;
                default:
                    break;
            }

            GenSymbolName();
        }

        void GenSymbolName()
        {
            if (!_loaded) return;
            try
            {
                SecurityFamilyImpl sec = CurrentSecurity;
                if (sec == null) return;

                switch(sec.Type)
                {
                    case SecurityType.FUT:
                        QSEnumSymbolType symboltype = (QSEnumSymbolType)cbSymbolType.SelectedValue;
                        switch(symboltype)
                        {
                            case QSEnumSymbolType.Standard:
                                {
                                    int month = (int)cbexpiremonth.SelectedValue;
                                    symbol.Text = MoniterHelper.GenSymbol(sec, month);
                                    break;
                                }
                            case QSEnumSymbolType.MonthContinuous:
                                {
                                    string month = (string)cbexpiremonth.SelectedValue;
                                    symbol.Text = MoniterHelper.GenSymbolMonthContinuous(sec.Code, month);
                                    this.expiredate.Value = DateTime.MaxValue;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
            }
            
            

            }
            catch (Exception ex)
            {
                //Globals.Debug("error expiremonth:" + ex.ToString());
            }
        }




    }
}
