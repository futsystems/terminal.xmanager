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
    public partial class fmOrderRule_TimeFilter2 : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmOrderRule_TimeFilter2()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmAccountRule_Profit_Load);
        }

        void fmAccountRule_Profit_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnAddTimeSpan.Click += new EventHandler(btnAddTimeSpan_Click);
            btnRemoveTimeSpan.Click += new EventHandler(btnRemoveTimeSpan_Click);
        }

        void btnRemoveTimeSpan_Click(object sender, EventArgs e)
        {
            if (timefilter.Items.Count == 0) return;
            if (timefilter.SelectedItems.Count == 0)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选中左侧某个时间段");
                return;
            }
            timefilter.Items.RemoveAt(timefilter.SelectedIndex);
        }

        void btnAddTimeSpan_Click(object sender, EventArgs e)
        {
            int v1 = start.Value.ToTLTime();
            int v2 = end.Value.ToTLTime();
            if (v2 <= v1)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("结束时间需要大于开始时间");
                return;
            }
            string ts = string.Format("{0}-{1}", v1, v2);
            foreach (var v in timefilter.Items)
            {
                if (v.ToString() == ts)
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("已经添加过对应时间段");
                    return;
                }
            }
            timefilter.Items.Add(ts);
        }
        string GetTimeFilter()
        {
            if (timefilter.Items.Count == 0) return string.Empty;
            List<string> tmp = new List<string>();
            foreach (var item in timefilter.Items)
            {
                tmp.Add(item.ToString());
            }
            return string.Join(",", tmp.ToArray());
        }

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

                if (!string.IsNullOrEmpty(seclist.Text))
                {
                    string[] list = seclist.Text.Split(',');
                    foreach (var sec in list)
                    {
                        SecurityFamilyImpl s = CoreService.BasicInfoTracker.GetSecurity(sec);
                        if (s == null)
                        {
                            MoniterHelper.WindowMessage(string.Format("品种:{0}不存在，请检查设置", sec));
                            return;
                        }
                    }
                }
               
                

                RuleItem item = new RuleItem();
                item.Account = this.Account.Account;
                item.Compare = QSEnumCompareType.Equals;
                item.Enable = true;
                item.RuleName = _ruleclass.ClassName;
                item.RuleType = _ruleclass.Type;
                item.SymbolSet = "";
                var args = new
                {
                    timespan = GetTimeFilter(),
                    sec_list = seclist.Text,
                };

                item.Value = args.SerializeObject();// TradingLib.Mixins.Json.JsonMapper.ToJson(args);

                if (MoniterHelper.WindowConfirm("确添加委托风控规则?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateRuleItem(item);
                }
            }

            this.Close();
        }


    }
}
