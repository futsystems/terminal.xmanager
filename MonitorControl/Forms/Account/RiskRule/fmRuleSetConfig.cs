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
    public partial class fmRuleSetConfig : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public AccountItem Account { get; set; }

        RuleClassItem _ruleclass = null;

        /// <summary>
        /// 规则
        /// </summary>
        public RuleClassItem RuleClass 
        { 
            get { return _ruleclass; }

            set
            {
                _ruleclass = value;
                ruletitle.Text = _ruleclass.Title;
                rulecheckargname.Text = _ruleclass.ValueName;
                
                description.Text = _ruleclass.Description;

                symbolset.Enabled = _ruleclass.CanSetSymbols;
                comparetype.Enabled = _ruleclass.CanSetCompare;
                argvalue.Enabled = _ruleclass.CanSetValue;

                if (!_ruleclass.CanSetCompare)
                {
                    comparetype.SelectedValue = _ruleclass.DefaultCompare;
                }
                if (!_ruleclass.CanSetValue)
                {
                    argvalue.Text = _ruleclass.DefaultValue;
                }
            }
        
        }

        RuleItem _rule = null;
        public RuleItem Rule { get { return _rule; }
            set
            {
                _rule = value;
                RuleClassItem klass = CoreService.BasicInfoTracker.GetRuleItemClass(_rule);
                ruletitle.Text = klass.Title;
                rulecheckargname.Text = klass.ValueName;
                description.Text = klass.Description;

                argvalue.Text = _rule.Value;
                comparetype.SelectedValue = _rule.Compare;
                symbolset.Text = _rule.SymbolSet.Replace('_',',');
            }
        }


        public fmRuleSetConfig()
        {
            InitializeComponent();
            //_ruleclass = new RuleClassItem();
            MoniterHelper.AdapterToIDataSource(comparetype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumCompareType>());

            btnSubmit.Click +=new EventHandler(btnSubmit_Click);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("rule==null:" + (this.Rule == null).ToString() + " ruleclass==null:" + (this.RuleClass == null).ToString());
            if (this.Rule == null)
            {
                RuleItem item = new RuleItem();
                item.Account = this.Account.Account;
                item.Compare = (QSEnumCompareType)comparetype.SelectedValue;
                item.Enable = true;
                item.RuleName = _ruleclass.ClassName;
                item.RuleType = _ruleclass.Type;
                item.SymbolSet = symbolset.Text.Replace(',','_');
                item.Value = argvalue.Text;

                if (MoniterHelper.WindowConfirm("确添加帐户风控规则?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateRuleItem(item);
                }
            }
            else if (RuleClass == null)
            {
                this.Rule.SymbolSet = symbolset.Text;
                this.Rule.Compare = (QSEnumCompareType)comparetype.SelectedValue;
                this.Rule.Value = argvalue.Text;

                if (MoniterHelper.WindowConfirm("确认更新帐户风控规则?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateRuleItem(this.Rule);
                }
            }
            //MessageBox.Show("it is here");
            this.Close();
        }
    }
}
