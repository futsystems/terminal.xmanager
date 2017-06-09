using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 该分部类
    /// 用于实现基础数据获取接口
    /// 比如获得某个合约，获得某个风控规则等
    /// </summary>
    public partial class BasicInfoTracker
    {

        #region 数据集
        /// <summary>
        /// 市场时间段map
        /// </summary>
        Dictionary<int, MarketTimeImpl> markettimemap = new Dictionary<int, MarketTimeImpl>();

        /// <summary>
        /// 交易所map
        /// </summary>
        Dictionary<int, ExchangeImpl> exchangemap = new Dictionary<int, ExchangeImpl>();

        /// <summary>
        /// 品种map
        /// </summary>
        Dictionary<int, SecurityFamilyImpl> securitymap = new Dictionary<int, SecurityFamilyImpl>();

        /// <summary>
        /// 合约map
        /// </summary>
        Dictionary<int, SymbolImpl> symbolmap = new Dictionary<int, SymbolImpl>();

        /// <summary>
        /// 合约名称map
        /// </summary>
        Dictionary<string, SymbolImpl> symbolnammap = new Dictionary<string, SymbolImpl>();


        Dictionary<int, ExchangeRate> exchangeRateaIDMap = new Dictionary<int, ExchangeRate>();
        /// <summary>
        /// 汇率信息map
        /// </summary>
        Dictionary<CurrencyType, ExchangeRate> exchangeRateCurrencyMap = new Dictionary<CurrencyType, ExchangeRate>();


        /// <summary>
        /// 主管理员map
        /// </summary>
        Dictionary<int, ManagerSetting> managermap = new Dictionary<int, ManagerSetting>();

        /// <summary>
        /// 代理账户列表
        /// </summary>
        Dictionary<int, AgentSetting> agentmap = new Dictionary<int, AgentSetting>();
        Dictionary<string, AgentSetting> agentAccountMap = new Dictionary<string, AgentSetting>();

        /// <summary>
        /// 路由组数据
        /// </summary>
        Dictionary<int, RouterGroupSetting> rgmap = new Dictionary<int, RouterGroupSetting>();

        /// <summary>
        /// 委托风控规则map
        /// </summary>
        Dictionary<string, RuleClassItem> orderruleclassmap = new Dictionary<string, RuleClassItem>();


        /// <summary>
        /// 帐户风控规则map
        /// </summary>
        Dictionary<string, RuleClassItem> accountruleclassmap = new Dictionary<string, RuleClassItem>();


        /// <summary>
        /// 交易帐户列表map
        /// </summary>
        Dictionary<string, AccountItem> accountmap = new Dictionary<string, AccountItem>();



        /// <summary>
        /// 行情快照维护器
        /// </summary>
        TickTracker ticktracker = new TickTracker();
        #endregion

        #region 获得市场事件，交易所，品种，合约等基础数据
        /// <summary>
        /// 市场时间段
        /// </summary>
        public IEnumerable<MarketTime> MarketTimes
        {
            get
            {
                return markettimemap.Values;
            }
        }

        /// <summary>
        /// 交易所
        /// </summary>
        public IEnumerable<ExchangeImpl> Exchanges
        {
            get
            {
                return exchangemap.Values;
            }
        }

        /// <summary>
        /// 品种
        /// </summary>
        public IEnumerable<SecurityFamilyImpl> Securities
        {
            get
            {
                return securitymap.Values;
            }

        }

        /// <summary>
        /// 合约
        /// </summary>
        public IEnumerable<SymbolImpl> Symbols
        {
            get
            {
                return symbolmap.Values.ToArray();
            }
        }






        public MarketTimeImpl GetMarketTime(int id)
        {
            MarketTimeImpl mt = null;
            if (markettimemap.TryGetValue(id, out mt))
            {
                return mt;
            }
            return null;
        }

        public ExchangeImpl GetExchange(int id)
        {
            ExchangeImpl ex = null;
            if (exchangemap.TryGetValue(id, out ex))
            {
                return ex;
            }
            return null;
        }
        public SecurityFamilyImpl GetSecurity(int id)
        {
            SecurityFamilyImpl sec = null;
            if (securitymap.TryGetValue(id, out sec))
            {
                return sec;
            }
            return null;
        }

        public SymbolImpl GetSymbol(int id)
        {
            SymbolImpl sym = null;
            if (symbolmap.TryGetValue(id, out sym))
            {
                return sym;
            }
            return null;
        }

        public SecurityFamilyImpl GetSecurity(string code)
        {
            foreach (SecurityFamilyImpl sec in securitymap.Values)
            {
                if (sec.Code.Equals(code))
                    return sec;
            }
            return null;
        }

        public SymbolImpl GetSymbol(string symbol)
        {
            SymbolImpl sym = null;
            if (symbolnammap.TryGetValue(symbol, out sym))
            {
                return sym;
            }
            return null;
        }

        #endregion

        #region 管理员列表数据 
        /// <summary>
        /// 管理员
        /// </summary>
        public IEnumerable<ManagerSetting> Managers
        {
            get
            {
                return managermap.Values;
            }
        }


        public ManagerSetting GetManager(int mgrid)
        {
            ManagerSetting mgr = null;
            if (managermap.TryGetValue(mgrid, out mgr))
            {
                return mgr;
            }
            return null;
        }
        #endregion

        #region Agent数据

        public IEnumerable<AgentSetting> Agents
        {
            get
            {
                return agentmap.Values;
            }
        }

        public AgentSetting GetAgent(int agentid)
        {
            AgentSetting agent = null;
            if (agentmap.TryGetValue(agentid, out agent))
            {
                return agent;
            }
            return null;
        }

        public AgentSetting GetAgent(string account)
        {
            AgentSetting agent = null;
            if (agentAccountMap.TryGetValue(account, out agent))
            {
                return agent;
            }
            return null;
        }
        #endregion

        #region 风控规则

        /// <summary>
        /// 委托规则集
        /// </summary>
        public IEnumerable<RuleClassItem> OrderRuleClass
        {
            get
            {
                return orderruleclassmap.Values;
            }
        }

        /// <summary>
        /// 帐户规则集
        /// </summary>
        public IEnumerable<RuleClassItem> AccountRuleClass
        {
            get
            {
                return accountruleclassmap.Values;
            }
        }

        public RuleClassItem GetOrderRuleClass(string classname)
        {
            RuleClassItem item = null;
            if (orderruleclassmap.TryGetValue(classname, out item))
            {
                return item;
            }
            else
            {
                return null;
            }
        }


        public RuleClassItem GetAccountRuleClass(string classname)
        {
            RuleClassItem item = null;
            if (accountruleclassmap.TryGetValue(classname, out item))
            {
                return item;
            }
            else
            {
                return null;
            }
        }

        public RuleClassItem GetRuleItemClass(RuleItem item)
        {
            if (item.RuleType == QSEnumRuleType.OrderRule)
            {
                return GetOrderRuleClass(item.RuleName);
            }
            else if (item.RuleType == QSEnumRuleType.AccountRule)
            {
                return GetAccountRuleClass(item.RuleName);
            }
            return null;
        }

        #endregion

        public IEnumerable<AccountItem> Accounts { get { return accountmap.Values; } }

        public AccountItem GetAccount(string account)
        {
            if (string.IsNullOrEmpty(account)) return null;
            AccountItem target = null;
            if (accountmap.TryGetValue(account, out target))
            {
                return target;
            }
            return null;
        }


        public IEnumerable<RouterGroupSetting> RouterGroups { get { return rgmap.Values; } }

        public RouterGroupSetting GetRouterGroup(int rgid)
        {
            if (rgmap.Keys.Contains(rgid))
                return rgmap[rgid];
            return null;
        }


        /// <summary>
        /// 所有汇率数据
        /// </summary>
        public IEnumerable<ExchangeRate> ExchangeRates { get { return exchangeRateCurrencyMap.Values; } }

        /// <summary>
        /// 返回某个币种的汇率
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public ExchangeRate GetExchangeRate(CurrencyType currency)
        {
            ExchangeRate target = null;
            if (exchangeRateCurrencyMap.TryGetValue(currency, out target))
            {
                return target;
            }
            return null;
        }


        /// <summary>
        /// 获得实时行情数据
        /// 更新本地快照数据
        /// </summary>
        /// <param name="k"></param>
        public void GotTick(Tick k)
        {
            ticktracker.GotTick(k);
        }

        public IEnumerable<Tick> TickSnapshots { get { return ticktracker.TickSnapshots; } }


        public Tick GetTickSnapshot(string exchange, string symbol)
        {
            return ticktracker[exchange, symbol];
        }
    }
}
