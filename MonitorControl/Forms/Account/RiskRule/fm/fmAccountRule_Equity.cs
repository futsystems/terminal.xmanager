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
    public partial class fmAccountRule_Equity : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmAccountRule_Equity()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmAccountRule_Equity_Load);
        }

        void fmAccountRule_Equity_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }


        public string Account { get; set; }

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
                lbDesp.Text = _ruleclass.Description;
                //ruletitle.Text = _ruleclass.Title;
                //rulecheckargname.Text = _ruleclass.ValueName;

                //description.Text = _ruleclass.Description;

                //symbolset.Enabled = _ruleclass.CanSetSymbols;
                //comparetype.Enabled = _ruleclass.CanSetCompare;
                //argvalue.Enabled = _ruleclass.CanSetValue;

                //if (!_ruleclass.CanSetCompare)
                //{
                //    comparetype.SelectedValue = _ruleclass.DefaultCompare;
                //}
                //if (!_ruleclass.CanSetValue)
                //{
                //    argvalue.Text = _ruleclass.DefaultValue;
                //}
            }

        }

        RuleItem _rule = null;
        public RuleItem Rule
        {
            get { return _rule; }
            set
            {
                _rule = value;
                RuleClassItem klass = CoreService.BasicInfoTracker.GetRuleItemClass(_rule);
                //ruletitle.Text = klass.Title;
                //rulecheckargname.Text = klass.ValueName;
                //description.Text = klass.Description;

                //argvalue.Text = _rule.Value;
                //comparetype.SelectedValue = _rule.Compare;
                //symbolset.Text = _rule.SymbolSet.Replace('_', ',');
            }
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Rule == null)
            {
                RuleItem item = new RuleItem();
                item.Account = this.Account;
                item.Compare = QSEnumCompareType.Equals;
                item.Enable = true;
                item.RuleName = _ruleclass.ClassName;
                item.RuleType = _ruleclass.Type;
                item.SymbolSet = "";
                var args = new
                {
                    equity_flat = equity_flat.Value,
                    equity_warn = equity_warn.Value,
                };

                item.Value = args.SerializeObject();// TradingLib.Mixins.Json.JsonMapper.ToJson(args);

                if (MoniterHelper.WindowConfirm("确添加帐户风控规则?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateRuleItem(item);
                }
            }

            this.Close();
        }


    }
}
