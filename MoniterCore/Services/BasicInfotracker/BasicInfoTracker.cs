using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using Common.Logging;


namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 管理端登入后从服务端拉取数据形成本地基础数据集和交易帐户列表
    /// 
    /// </summary>
    public partial class BasicInfoTracker
    {
        public bool Initialized { get { return _initialized; } }
        bool _initialized = false;

        ILog logger = LogManager.GetLogger("BasicInfoTracker");

        public BasicInfoTracker()
        {

            CoreService.EventContrib.RegisterCallback("RiskCentre", "QryRuleSet", OnQryRuleSet);
            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryManager", OnQryManager);
            CoreService.EventContrib.RegisterNotifyCallback("MgrExchServer", "NotifyManagerUpdate",OnNotifyManagerUpdate);
            CoreService.EventContrib.RegisterNotifyCallback("MgrExchServer", "NotifyManagerDelete", OnNotifyManagerDelete);

            CoreService.EventContrib.RegisterCallback("ConnectorManager", "QryRouterGroup", OnQryRouterGroup);
            CoreService.EventContrib.RegisterNotifyCallback("ConnectorManager", "NotifyRouterGroup", OnNotifyRouterGroup);
            
            //RegisterCallback("MgrExchServer", "NotifyDomain", OnNotifyDomain);
            

            
        }
        /// <summary>
        /// 市场时间段map
        /// </summary>
        Dictionary<int, MarketTime> markettimemap = new Dictionary<int, MarketTime>();

        /// <summary>
        /// 交易所map
        /// </summary>
        Dictionary<int, Exchange> exchangemap = new Dictionary<int, Exchange>();

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
        Dictionary<string, AccountLite> accountmap = new Dictionary<string, AccountLite>();



    }
}
