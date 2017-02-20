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
    public partial class fmEditRiskRule : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmEditRiskRule()
        {
            InitializeComponent();

            this.Load += new EventHandler(fmRiskRuleConfig_Load);
        }

        void fmRiskRuleConfig_Load(object sender, EventArgs e)
        {

            
            MoniterHelper.AdapterToIDataSource(orderRuleClassList).BindDataSource(MoniterHelper.GetOrderRuleClassComboxArray());
            MoniterHelper.AdapterToIDataSource(accountRuleClassList).BindDataSource(MoniterHelper.GetAccountRuleClassComboxArray());

            accountRuleClassList.ContextMenuStrip = new ContextMenuStrip();
            accountRuleClassList.ContextMenuStrip.Items.Add("添加强平规则", null, new EventHandler(AddAccountRule_Click));//0

            accountRuleItemList.ContextMenuStrip = new ContextMenuStrip();
            accountRuleItemList.ContextMenuStrip.Items.Add("删除强平规则", null, new EventHandler(DelAccountRule_Click));//0

            orderRuleClassList.ContextMenuStrip = new ContextMenuStrip();
            orderRuleClassList.ContextMenuStrip.Items.Add("添加委托规则", null, new EventHandler(AddOrderRule_Click));

            orderRuleItemList.ContextMenuStrip = new ContextMenuStrip();
            orderRuleItemList.ContextMenuStrip.Items.Add("删除委托规则", null, new EventHandler(DelOrderRule_Click));

            btnAddAccountRule.Click += new EventHandler(btnAddAccountRule_Click);
            btnDelAccountRule.Click += new EventHandler(btnDelAccountRule_Click);

            btnAddOrderRule.Click += new EventHandler(btnAddOrderRule_Click);
            btnDelOrderRule.Click += new EventHandler(btnDelOrderRule_Click);
            CoreService.EventCore.RegIEventHandler(this);
        }



        

      




        public void OnInit()
        {

            CoreService.EventCore.RegisterCallback(Modules.RiskCentre, Method_RiskCentre.QRY_RULEITEM, this.OnRuleItem);
            CoreService.EventCore.RegisterCallback(Modules.RiskCentre, Method_RiskCentre.UPDATE_RULEITEM, this.OnRuleItemUpdate);
            CoreService.EventCore.RegisterCallback(Modules.RiskCentre, Method_RiskCentre.DEL_RULEITEM, this.OnRuleItemDel);
            CoreService.TLClient.ReqQryRuleItem(_account.Account, QSEnumRuleType.OrderRule);
            CoreService.TLClient.ReqQryRuleItem(_account.Account, QSEnumRuleType.AccountRule);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.RiskCentre, Method_RiskCentre.QRY_RULEITEM, this.OnRuleItem);
            CoreService.EventCore.UnRegisterCallback(Modules.RiskCentre, Method_RiskCentre.UPDATE_RULEITEM, this.OnRuleItemUpdate);
            CoreService.EventCore.UnRegisterCallback(Modules.RiskCentre, Method_RiskCentre.DEL_RULEITEM, this.OnRuleItemDel);
        }

        void OnRuleItem(string json, bool islast)
        {
            RuleItem item = CoreService.ParseJsonResponse<RuleItem>(json);
            if (item == null) return;
            OnRuleItem(item, true);
        }

        void OnRuleItemDel(string json, bool islast)
        {
            RuleItem item = CoreService.ParseJsonResponse<RuleItem>(json);
            if (item == null) return;

            if (item.RuleType == QSEnumRuleType.OrderRule)
            {
                InvokeGotOrderRuleItemDel(item, true);
            }
            else if (item.RuleType == QSEnumRuleType.AccountRule)
            {
                InvokeGotAccountRuleItemDel(item, true);
            }
        }

        void OnRuleItemUpdate(string json, bool islast)
        {
            RuleItem item = CoreService.ParseJsonResponse<RuleItem>(json);
            if (item == null) return;
            OnRuleItem(item, islast);

        }

        /// <summary>
        /// 获得委托风控项
        /// </summary>
        /// <param name="item"></param>
        /// <param name="islast"></param>
        void OnRuleItem(RuleItem item, bool islast)
        {
            if (item.RuleType == QSEnumRuleType.OrderRule)
            {
                InvokeGotOrderRuleItem(item, islast);
            }
            else if (item.RuleType == QSEnumRuleType.AccountRule)
            {
                InvokeGotAccountRuleItem(item, islast);
            }
        }



        #region 帐户规则
        void btnDelAccountRule_Click(object sender, EventArgs e)
        {
            DelAccountRule();
        }

        void btnAddAccountRule_Click(object sender, EventArgs e)
        {
            AddAccountRule();
        }

        /// <summary>
        /// 编辑某个交易帐号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddAccountRule_Click(object sender, EventArgs e)
        {
            AddAccountRule();
        }

        void DelAccountRule_Click(object sender, EventArgs e)
        {
            DelAccountRule();
        }


        void AddAccountRule()
        {

            if (accountRuleClassList.SelectedItems.Count > 0)
            {
                RuleClassItem item = ((RuleClassItem)accountRuleClassList.SelectedValue);
                if (item.ClassName.Equals("RuleSet2.Account.RSEquity"))
                {
                    fmAccountRule_Equity fm = new fmAccountRule_Equity();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
                else if (item.ClassName.Equals("RuleSet2.Account.RSMaxLoss"))
                {
                    fmAccountRule_Loss fm = new fmAccountRule_Loss();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
                else if (item.ClassName.Equals("RuleSet2.Account.RSEquityByCredit"))
                {
                    fmAccountRule_EquityByCredit fm = new fmAccountRule_EquityByCredit();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
                else if (item.ClassName.Equals("RuleSet2.Account.RSMaxProfit"))
                {
                    fmAccountRule_Profit fm = new fmAccountRule_Profit();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
                else if (item.ClassName.Equals("RuleSet2.Account.RSTime"))
                {
                    fmAccountRule_Time fm = new fmAccountRule_Time();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
                else if (item.ClassName.Equals("RuleSet2.Account.RSMaxChange"))
                {
                    fmAccountRule_MaxChange fm = new fmAccountRule_MaxChange();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
                else if (item.ClassName.Equals("RuleSet2.Account.RSHoldRatio"))
                {
                    fmAccountRule_HoldRatio fm = new fmAccountRule_HoldRatio();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                    
                }
            }
        }





        void DelAccountRule()
        {
            if (accountRuleItemList.SelectedItems.Count > 0)
            {
                RuleItem item = (RuleItem)accountRuleItemList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认删除风控项:" + item.RuleDescription) == System.Windows.Forms.DialogResult.Yes)
                {
                    accountRuleItemList.SelectedItem = null;
                    CoreService.TLClient.ReqDelRuleItem(item);
                }
            }  
        }

        #endregion


        void btnAddOrderRule_Click(object sender, EventArgs e)
        {
            AddOrderRule();
        }

        void btnDelOrderRule_Click(object sender, EventArgs e)
        {
            DelOrderRule();
        }

        void AddOrderRule_Click(object sender, EventArgs e)
        {
            AddOrderRule();
        }
        
        void DelOrderRule_Click(object sender, EventArgs e)
        {
            DelOrderRule();
        }

        void AddOrderRule()
        {
            if (orderRuleClassList.SelectedItems.Count > 0)
            { 
                RuleClassItem item = ((RuleClassItem)orderRuleClassList.SelectedValue);
                
                if (item.ClassName.Equals("RuleSet2.Order.RSSecurityFilter"))
                {
                    fmOrderRule_SecFilter fm = new fmOrderRule_SecFilter();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
                if (item.ClassName.Equals("RuleSet2.Order.RSMaxTradeSize"))
                {
                    fmOrderRule_MaxTradeSize fm = new fmOrderRule_MaxTradeSize();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
                if (item.ClassName.Equals("RuleSet2.Order.RSTimeFilter"))
                {
                    fmOrderRule_TimeFilter fm = new fmOrderRule_TimeFilter();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
                if (item.ClassName.Equals("RuleSet2.Order.RSTimeFilter2"))
                {
                    fmOrderRule_TimeFilter2 fm = new fmOrderRule_TimeFilter2();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                    fm.Close();
                }
            }
        }

        void DelOrderRule()
        {
            if (orderRuleItemList.SelectedItems.Count>0)
            {
                RuleItem item = (RuleItem)orderRuleItemList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认删除风控项:" + item.RuleDescription) == System.Windows.Forms.DialogResult.Yes)
                {
                    orderRuleItemList.SelectedItem = null;
                    CoreService.TLClient.ReqDelRuleItem(item);
                }
            }  
        }


        #region 帐户规则
        Dictionary<int, RuleItem> accountrulemap = new Dictionary<int, RuleItem>();

        /// <summary>
        /// 删除委托规则回报处理
        /// </summary>
        /// <param name="item"></param>
        /// <param name="islast"></param>
        void InvokeGotAccountRuleItemDel(RuleItem item, bool islast)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<RuleItem,bool>(InvokeGotAccountRuleItemDel), new object[] { item, islast });
            }
            else
            {
                if (accountrulemap.Keys.Contains(item.ID))
                {
                    accountrulemap.Remove(item.ID);
                }
                if (islast)
                {
                    MoniterHelper.AdapterToIDataSource(accountRuleItemList).BindDataSource(this.GetAccountRuleItemList());
                }
            }
        }

        /// <summary>
        /// 更新委托规则回报
        /// </summary>
        /// <param name="item"></param>
        /// <param name="islast"></param>
        void InvokeGotAccountRuleItem(RuleItem item, bool islast)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<RuleItem, bool>(InvokeGotAccountRuleItem), new object[] { item, islast });
            }
            else
            {
                if (item.ID == 0 || string.IsNullOrEmpty(item.Account))
                    return;
                //更新
                if (accountrulemap.Keys.Contains(item.ID))
                {
                    RuleItem target = accountrulemap[item.ID];
                    target.Account = item.Account;
                    target.Compare = item.Compare;
                    target.Enable = item.Enable;
                    target.RuleDescription = item.RuleDescription;
                    target.RuleName = item.RuleName;
                    target.RuleType = item.RuleType;
                    target.SymbolSet = item.SymbolSet;
                    target.Value = item.SymbolSet;

                }
                else //添加
                {
                    accountrulemap.Add(item.ID, item);
                }
                if (islast)//当最后一个回报时刷新数据
                {
                    MoniterHelper.AdapterToIDataSource(accountRuleItemList).BindDataSource(this.GetAccountRuleItemList());
                }
            }
        }

        /// <summary>
        /// 交易帐户风控项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArrayList GetAccountRuleItemList()
        {
            ArrayList list = new ArrayList();
            foreach (RuleItem item in accountrulemap.Values)
            {
                ValueObject<RuleItem> vo = new ValueObject<RuleItem>();
                vo.Name = item.RuleDescription;
                vo.Value = item;
                list.Add(vo);
            }
            return list;
        }

        #endregion

        #region 委托规则
        /// <summary>
        /// 委托风控项
        /// </summary>
        Dictionary<int, RuleItem> orderruleitemmap = new Dictionary<int, RuleItem>();

        delegate void RuleItemDel(RuleItem item, bool islast);

        void InvokeGotOrderRuleItemDel(RuleItem item, bool islast)
        {
            if (InvokeRequired)
            {
                Invoke(new RuleItemDel(InvokeGotOrderRuleItemDel), new object[] { item, islast });
            }
            else
            {
              
                if (orderruleitemmap.Keys.Contains(item.ID))
                {
                    orderruleitemmap.Remove(item.ID);
                }
                if (islast)
                {
                    MoniterHelper.AdapterToIDataSource(orderRuleItemList).BindDataSource(this.GetOrderRuleItemList());
                }
            }
        }

        void InvokeGotOrderRuleItem(RuleItem item, bool islast)
        {
            if (InvokeRequired)
            {
                Invoke(new RuleItemDel(InvokeGotOrderRuleItem), new object[] { item, islast });
            }
            else
            {
                if (item.ID == 0 || string.IsNullOrEmpty(item.Account))
                    return;
                if (orderruleitemmap.Keys.Contains(item.ID))
                {
                    RuleItem target = orderruleitemmap[item.ID];
                    target.Account = item.Account;
                    target.Compare = item.Compare;
                    target.Enable = item.Enable;
                    target.RuleDescription = item.RuleDescription;
                    target.RuleName = item.RuleName;
                    target.RuleType = item.RuleType;
                    target.SymbolSet = item.SymbolSet;
                    target.Value = item.SymbolSet;

                }
                else
                {
                    orderruleitemmap.Add(item.ID, item);
                }
                if (islast)//当最后一个回报时刷新数据
                {
                    MoniterHelper.AdapterToIDataSource(orderRuleItemList).BindDataSource(this.GetOrderRuleItemList());
                }
            }
        }

        /// <summary>
        /// 交易帐户风控项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArrayList GetOrderRuleItemList()
        {
            ArrayList list = new ArrayList();
            foreach (RuleItem item in orderruleitemmap.Values)
            {
                ValueObject<RuleItem> vo = new ValueObject<RuleItem>();
                vo.Name = item.RuleDescription;
                vo.Value = item;
                list.Add(vo);
            }
            return list;
        }

        #endregion


        AccountItem _account = null;
        public void SetAccount(AccountItem account)
        {
            _account = account;
        }
    }
}
