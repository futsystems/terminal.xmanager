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
    public partial class fmAccountRule_Profit : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmAccountRule_Profit()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmAccountRule_Profit_Load);
        }

        void fmAccountRule_Profit_Load(object sender, EventArgs e)
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
                    profit_flat = profit_flat.Value,
                    profit_warn = profit_warn.Value,
                };

                item.Value = args.SerializeObject();

                if (MoniterHelper.WindowConfirm("确添加帐户风控规则?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateRuleItem(item);
                }
            }

            this.Close();
        }


    }
}
