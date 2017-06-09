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

            //查询风控规则
            CoreService.EventCore.RegisterCallback(Modules.RiskCentre, Method_RiskCentre.QRY_RULESET, OnRspRuleSet);

            //路由组
            CoreService.EventCore.RegisterCallback(Modules.CONN_MGR, Method_CONN_MGR.QRY_ROUTEGROUP, OnQryRouterGroup);
            CoreService.EventCore.RegisterNotifyCallback(Modules.CONN_MGR, Method_CONN_MGR.NOTIFY_ROUTEGROUP, OnNotifyRouterGroup);

            //管理员
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MANAGER, OnRspManager);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MANAGER, OnNotifyManagerUpdate);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MANGER_DELETE, OnNotifyManagerDelete);

            //管理员账户
            CoreService.EventCore.RegisterCallback(Modules.AgentManager, Method_AGENT_MGR.QRY_ALL_AGENT, OnRspAgent);


            //交易账户
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_ACC_LIST, OnQryAccountList);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_ACC_CHANGED, OnAccountChanged);

            //查询行情快照
            CoreService.EventCore.RegisterCallback(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_TICK_SNAPSHOT, OnRspTickSnapshot);
        }

        

        void OnRspTickSnapshot(string json, bool isLast)
        {
            string content = json.DeserializeObject<string>();
            Tick tick = TickImpl.Deserialize2(content);
            if (tick != null)
            {
                //查询行情快照 此处与connecton_OnTick 相同 用于更新本地行情数据
                CoreService.BasicInfoTracker.GotTick(tick);//更新快照
                CoreService.TradingInfoTracker.GotTick(tick);//驱动交易数据更新
                CoreService.EventIndicator.FireTick(tick);
            }
            
        }
    }
}
