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

            
            MoniterHelper.AdapterToIDataSource(orderRuleClassList).BindDataSource(CoreService.BasicInfoTracker.GetOrderRuleClassListItems());
            MoniterHelper.AdapterToIDataSource(accountRuleClassList).BindDataSource(CoreService.BasicInfoTracker.GetAccountRuleClassListItems());

            accountRuleClassList.ContextMenuStrip = new ContextMenuStrip();
            accountRuleClassList.ContextMenuStrip.Items.Add("添加强平规则", null, new EventHandler(AddAccountRule_Click));//0

            accountRuleItemList.ContextMenuStrip = new ContextMenuStrip();
            accountRuleItemList.ContextMenuStrip.Items.Add("删除强平规则", null, new EventHandler(DelAccountRule_Click));//0

            CoreService.EventCore.RegIEventHandler(this);
        }


        public void OnInit()
        {

            CoreService.EventContrib.RegisterCallback("RiskCentre", "QryRuleItem", this.OnRuleItem);
            CoreService.EventContrib.RegisterCallback("RiskCentre", "UpdateRuleItem", this.OnRuleItemUpdate);
            CoreService.EventContrib.RegisterCallback("RiskCentre", "DelRuleItem", this.OnRuleItemDel);
            CoreService.TLClient.ReqQryRuleItem(_account.Account, QSEnumRuleType.OrderRule);
            CoreService.TLClient.ReqQryRuleItem(_account.Account, QSEnumRuleType.AccountRule);
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("RiskCentre", "QryRuleItem", this.OnRuleItem);
            CoreService.EventContrib.UnRegisterCallback("RiskCentre", "UpdateRuleItem", this.OnRuleItemUpdate);
            CoreService.EventContrib.UnRegisterCallback("RiskCentre", "DelRuleItem", this.OnRuleItemDel);
        }

        void OnRuleItem(string json, bool islast)
        {
            RuleItem item = MoniterHelper.ParseJsonResponse<RuleItem>(json);
            if (item == null) return;
            OnRuleItem(item, true);
        }

        void OnRuleItemDel(string json, bool islast)
        {
            RuleItem item = MoniterHelper.ParseJsonResponse<RuleItem>(json);
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
            RuleItem item = MoniterHelper.ParseJsonResponse<RuleItem>(json);
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



        /// <summary>
        /// 编辑某个交易帐号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddAccountRule_Click(object sender, EventArgs e)
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
                }
                if (item.ClassName.Equals("RuleSet2.Account.RSMaxLoss"))
                {
                    fmAccountRule_Loss fm = new fmAccountRule_Loss();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                }
                if (item.ClassName.Equals("RuleSet2.Account.RSEquityByCredit"))
                {
                    fmAccountRule_EquityByCredit fm = new fmAccountRule_EquityByCredit();
                    fm.Account = _account;
                    fm.RuleClass = item;
                    fm.ShowDialog();
                }
                else
                {

                    fmRuleSetConfig fm = new fmRuleSetConfig();
                    fm.Account = _account;
                    fm.RuleClass = (RuleClassItem)accountRuleClassList.SelectedValue;
                    fm.ShowDialog();
                }
            }
        }

        void DelAccountRule_Click(object sender, EventArgs e)
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


        AccountLite _account = null;
        public void SetAccount(AccountLite account)
        {
            _account = account;
        }
    }
}
