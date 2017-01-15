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

            CoreService.EventCore.RegisterCallback("RiskCentre", "QryRuleSet", OnQryRuleSet);
            //交易时间段
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_MARKETTIME, OnRspMarketTime);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_INFO_MARKETTIME, OnNotifyMarketTime);
            //交易所
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_EXCHANGE, OnRspExchange);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_INFO_EXCHANGE, OnNotifyExchange);
            //品种
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_SEC, OnRspSecurity);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_INFO_SEC, OnNotifySecurity);
            //合约
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_SYM, OnRspSymbol);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_INFO_SYM, OnNotifySymbol);
            //汇率
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_EXCHANGERATES, OnRspExchangeRate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_INFO_EXCHANGERATES, OnNotifyExchangeRate);


            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_ACC_LIST, OnQryAccountList);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_ACC_CHANGED, OnAccountChanged);

            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MANAGER, OnQryManager);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MANAGER, OnNotifyManagerUpdate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH,Method_MGR_EXCH.NOTIFY_MANGER_DELETE, OnNotifyManagerDelete);

            CoreService.EventCore.RegisterCallback(Modules.CONN_MGR, Method_CONN_MGR.QRY_ROUTEGROUP, OnQryRouterGroup);
            CoreService.EventCore.RegisterNotifyCallback(Modules.CONN_MGR, Method_CONN_MGR.NOTIFY_ROUTEGROUP, OnNotifyRouterGroup);

            
        
            //RegisterCallback("MgrExchServer", "NotifyDomain", OnNotifyDomain);
            

            
        }

        void OnQryAccountList(string json, bool isLast)
        {
            AccountItem item = CoreService.ParseJsonResponse<AccountItem>(json);
            if (item != null)
            {
                accountmap[item.Account] = item;
            }
            if (isLast && !_initialized)
            {
                Status("交易帐户列表同步成功");
                //结构化基础数据
                Bind();
                //输出基础数据数量信息
                logger.Info("============基础数据初始化完成============");
                logger.Info("     Markettime num:" + this.MarketTimes.Count().ToString());
                logger.Info("       Exchange num:" + this.Exchanges.Count().ToString());
                logger.Info("       Security num:" + this.Securities.Count().ToString());
                logger.Info("         Symbol num:" + this.Symbols.Count().ToString());
                logger.Info("        RuleSet num:" + (this.AccountRuleClass.Count() + this.OrderRuleClass.Count()).ToString());
                logger.Info("        Manager num:" + this.Managers.Count().ToString());
                logger.Info("    RouterGroup num:" + this.RouterGroups.Count().ToString());
                logger.Info("        Account num:" + this.Accounts.Count().ToString());

                //查询行情快照
                CoreService.TLClient.ReqQryTickSnapshot();

                CoreService.TLClient.StartTick();
                //触发数据初始化完成事件
                CoreService.EventCore.FireInitializedEvent();
            }
        }

        void OnAccountChanged(string json)
        {
            AccountItem item = CoreService.ParseJsonResponse<AccountItem>(json);
            if (item != null)
            {
                accountmap[item.Account] = item;
            }
        }
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

    }
}
