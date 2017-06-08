using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.Protocol;


namespace TradingLib.MoniterCore
{
    public class Method_MGR_EXCH
    {

        #region Account
        /// <summary>
        /// 查询交易账户列表
        /// </summary>
        public const string QRY_ACC_LIST = "QryAccountList";

        /// <summary>
        /// 设定观察账户列表
        /// </summary>
        public const string WATCH_ACC_LIST = "WatchAccountList";

        /// <summary>
        /// 恢复某个交易账户交易记录
        /// </summary>
        public const string RESUME_ACC = "ResumeAccount";

        /// <summary>
        /// 交易端会话信息通知
        /// </summary>
        public const string NOTIFY_SESSION_INFO = "NotifySession";

        /// <summary>
        /// 交易账户变动通知
        /// </summary>
        public const string NOTIFY_ACC_CHANGED = "NotifyAccountChanged";

        /// <summary>
        /// 交易账户财务统计通知
        /// </summary>
        public const string NOTIFY_ACC_STATISTIC = "NotifyAccountStatistic";

        /// <summary>
        /// 交易账户数据恢复通知
        /// </summary>
        public const string NOTIFY_ACC_RESUME = "NotifyResume";
        #endregion

        #region BasicInfo
        /// <summary>
        /// 同步品种数据
        /// </summary>
        public const string SYNC_SEC_INFO = "SyncSecInfo";

        /// <summary>
        /// 同步合约数据
        /// </summary>
        public const string SYNC_SYMBOL_INFO = "SyncSymbol";

        /// <summary>
        /// 禁止交易所有合约
        /// </summary>
        public const string DISABLE_ALL_SYMBOL = "DisableAllSymbols";


        /// <summary>
        /// 查询日历文件列表
        /// </summary>
        public const string QRY_CALENDAR = "QryCalendarList";

        /// <summary>
        /// 查询交易时间段
        /// </summary>
        public const string QRY_INFO_MARKETTIME = "QryInfoMarketTime";
        /// <summary>
        /// 更新交易时间段
        /// </summary>
        public const string UPDATE_INFO_MARKETTIME = "UpdateInfoMarketTime";
        /// <summary>
        /// 交易时间段更新通知
        /// </summary>
        public const string NOTIFY_INFO_MARKETTIME = "NotifyMarketTime";

        /// <summary>
        /// 查询交易所
        /// </summary>
        public const string QRY_INFO_EXCHANGE = "QryInfoExchange";

        /// <summary>
        /// 更新交易所
        /// </summary>
        public const string UPDATE_INFO_EXCHANGE = "UpdateInfoExchange";

        /// <summary>
        /// 交易所更新通知
        /// </summary>
        public const string NOTIFY_INFO_EXCHANGE = "NotifyExchange";

        /// <summary>
        /// 查询品种列表
        /// </summary>
        public const string QRY_INFO_SEC = "QryInfoSecurity";

        /// <summary>
        /// 更新品种
        /// </summary>
        public const string UPDATE_INFO_SEC = "UpdateInfoSecurity";

        /// <summary>
        /// 更新品种通知
        /// </summary>
        public const string NOTIFY_INFO_SEC = "NotifySecurity";

        /// <summary>
        /// 查询合约
        /// </summary>
        public const string QRY_INFO_SYM = "QryInfoSymbol";

        /// <summary>
        /// 更新合约
        /// </summary>
        public const string UPDATE_INFO_SYM = "UpdateInfoSymbol";

        /// <summary>
        /// 更新合约通知
        /// </summary>
        public const string NOTIFY_INFO_SYM = "NotifySymbol";

        /// <summary>
        /// 查询汇率
        /// </summary>
        public const string QRY_INFO_EXCHANGERATES = "QryExchangeRates";
        /// <summary>
        /// 更新汇率
        /// </summary>
        public const string UPDATE_INFO_EXCHANGERATES = "UpdateExchangeRate";
        /// <summary>
        /// 汇率更新通知
        /// </summary>
        public const string NOTIFY_INFO_EXCHANGERATES = "NotifyExchangeRateUpdate";

        /// <summary>
        /// 查询行情快照
        /// </summary>
        public const string QRY_TICK_SNAPSHOT = "QryTickSnapshot";
        #endregion

        #region ConfigTemplate
        /// <summary>
        /// 查询手续费模板
        /// </summary>
        public const string QRY_COMMISSION_TEMPLATE = "QryCommissionTemplate";

        /// <summary>
        /// 更新手续费模板
        /// </summary>
        public const string UPDATE_COMMISSION_TEMPLATE = "UpdateCommissionTemplate";

        /// <summary>
        /// 手续费模板通知
        /// </summary>
        public const string NOTIFY_COMMISSION_TEMPLATE = "NotifyCommissionTemplate";

        /// <summary>
        /// 手续费模板删除通知
        /// </summary>
        public const string NOTIFY_COMMISSION_TEMPLATE_DELETE = "NotifyDeleteCommissionTemplate";

        /// <summary>
        /// 删除手续费模板
        /// </summary>
        public const string DEL_COMMISSION_TEMPLATE = "DeleteCommissionTemplate";

        /// <summary>
        /// 查询手续费模板项
        /// </summary>
        public const string QRY_COMMISSION_ITEM = "QryCommissionTemplateItem";

        /// <summary>
        /// 更新手续费模板项
        /// </summary>
        public const string UPDATE_COMMISSION_ITEM = "UpdateCommissionTemplateItem";

        /// <summary>
        /// 手续费模板项通知
        /// </summary>
        public const string NOTIFY_COMMISSION_ITEM = "NotifyCommissionTemplateItem";

        /// <summary>
        /// 查询保证金模板
        /// </summary>
        public const string QRY_MARGIN_TEMPLATE = "QryMarginTemplate";

        /// <summary>
        /// 更新保证金模板
        /// </summary>
        public const string UPDATE_MARGIN_TEMPLATE = "UpdateMarginTemplate";

        /// <summary>
        /// 保证金模板通知
        /// </summary>
        public const string NOTIFY_MARGIN_TEMPLATE = "NotifyMarginTemplate";

        /// <summary>
        /// 保证金模板删除通知
        /// </summary>
        public const string NOTIFY_MARGIN_TEMPLATE_DELETE = "NotifyDeleteMarginTemplate";

        /// <summary>
        /// 删除保证金模板
        /// </summary>
        public const string DEL_MARGIN_TEMPLATE = "DeleteMarginTemplate";

        /// <summary>
        /// 查询保证金模板项
        /// </summary>
        public const string QRY_MARGIN_ITEM = "QryMarginTemplateItem";

        /// <summary>
        /// 更新保证金模板项
        /// </summary>
        public const string UPDATE_MARGIN_ITEM = "UpdateMarginTemplateItem";

        /// <summary>
        /// 保证金模板项更新
        /// </summary>
        public const string NOTIFY_MARGIN_ITEM = "NotifyMarginTemplateItem";

        /// <summary>
        /// 查询策略模板
        /// </summary>
        public const string QRY_EXSTRATEGY_TEMPLATE = "QryExStrategyTemplate";

        /// <summary>
        /// 更新策略模板
        /// </summary>
        public const string UPDATE_EXSTRATEGY_TEMPLATE = "UpdateExStrategyTemplate";

        /// <summary>
        /// 策略模板通知
        /// </summary>
        public const string NOTIFY_EXSTRATEGY_TEMPLATE = "NotifyExStrategyTemplate";

        /// <summary>
        /// 策略模板删除通知
        /// </summary>
        public const string NOTIFY_EXSTRATEGY_TEMPLATE_DELETE = "NotifyDeleteExStrategyTemplate";

        /// <summary>
        /// 删除策略模板
        /// </summary>
        public const string DEL_EXSTRATEGY_TEMPLATE = "DeleteExStrategyTemplate";

        /// <summary>
        /// 查询策略模板项
        /// </summary>
        public const string QRY_EXSTRATEGY_ITEM = "QryExStrategyTemplateItem";

        /// <summary>
        /// 更新策略模板项
        /// </summary>
        public const string UPDATE_EXSTRATEGY_ITEM = "UpdateExStrategyTemplateItem";

        /// <summary>
        /// 策略项通知
        /// </summary>
        public const string NOTIFY_EXSTRATEGY_ITEM = "NotifyExStrategyTemplateItem";

        #endregion

        #region Domain
        /// <summary>
        /// 查询分区
        /// </summary>
        public const string QRY_DOMAIN = "QryDomain";

        /// <summary>
        /// 更新分区
        /// </summary>
        public const string UPDATE_DOMAIN = "UpdateDomain";

        /// <summary>
        /// 分区通知
        /// </summary>
        public const string NOTIFY_DOMAIN = "NotifyDomain";
        /// <summary>
        /// 查询分区登入信息
        /// </summary>
        public const string QRY_DOMAIN_LOGININFO = "QryDomainRootLoginInfo";
        #endregion

        #region Manager
        /// <summary>
        /// 查询管理员
        /// </summary>
        public const string QRY_MANAGER = "QryManager";
        /// <summary>
        /// 查询管理员登入信息
        /// </summary>
        public const string QRY_MANAGER_LOGININFO = "QryManagerLoginInfo";
        /// <summary>
        /// 更新管理员
        /// </summary>
        public const string UPDATE_MANAGER = "UpdateManager";
        /// <summary>
        /// 删除管理员
        /// </summary>
        public const string DEL_MANAGER = "DeleteManager";

        /// <summary>
        /// 管理员删除通知
        /// </summary>
        public const string NOTIFY_MANGER_DELETE = "NotifyManagerDelete";

        /// <summary>
        /// 管理员更新通知
        /// </summary>
        public const string NOTIFY_MANAGER = "NotifyManagerUpdate";

        /// <summary>
        /// 更新管理员密码
        /// </summary>
        public const string UPDATE_MANAGER_PASS = "UpdateManagerPass";
        /// <summary>
        /// 激活管理员
        /// </summary>
        public const string UPDATE_MANAGER_ACTIVE = "ActiveManager";
        /// <summary>
        /// 冻结管理员
        /// </summary>
        public const string UPDATE_MANAGER_INACTIVE = "InactiveManager";


        #endregion

        #region Other

        /// <summary>
        /// 查询签约银行
        /// </summary>
        public const string QRY_CONTRACT_BANK = "QryContractBank";

        /// <summary>
        /// 查询收款银行
        /// </summary>
        public const string QRY_RECV_BANK = "QryReceiveableBank";

        /// <summary>
        /// 更新收款银行信息
        /// </summary>
        public const string UPDATE_RECV_BANK = "UpdateReceiveableBank";

        /// <summary>
        /// 收款银行通知
        /// </summary>
        public const string NOTIFY_RECV_BANK = "NotifyRecvBank";


        /// <summary>
        /// 查询系统状态
        /// </summary>
        public const string QRY_SYSTEM_STATUS = "QrySystemStatus";

        /// <summary>
        /// 请求补充交易记录
        /// </summary>
        public const string REQ_INSERT_TRADE = "InsertTrade";
        #endregion

        #region Agent
       

        /// <summary>
        /// 代理统计通知
        /// </summary>
        public const string NOTIFY_AGENT_STATISTIC = "NotifyAgentStatistic";

        /// <summary>
        /// 观察代理列表
        /// </summary>
        public const string REQ_WATCH_AGENTS = "WatchAgents";
        #endregion

    }

    public static class MgrExch
    {
        #region Account
        /// <summary>
        /// 查询交易帐户列表
        /// </summary>
        public static int ReqQryAccountList(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_ACC_LIST, "");
        }

        /// <summary>
        /// 设定观察帐户列表
        /// </summary>
        /// <param name="list"></param>
        public static int ReqWatchAccount(this TLClientNet client,List<string> list)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.WATCH_ACC_LIST, list.ToArray());
        }

        /// <summary>
        /// 恢复某个交易帐号的日内交易数据
        /// </summary>
        /// <param name="account"></param>
        public static int ReqResumeAccount(this TLClientNet client,string account)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.RESUME_ACC, new { account = account });
        }
        #endregion

        #region BasicInfo
        public static int ReqQryCalendar(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_CALENDAR, "");
        }

        public static int ReqSyncSecurity(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.SYNC_SEC_INFO, "");
        }

        public static int ReqSyncSymbol(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.SYNC_SYMBOL_INFO, "");
        }

        public static int ReqDisableAllSymbols(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.DISABLE_ALL_SYMBOL, "");
        }

        public static int ReqQryMarketTime(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_MARKETTIME, "");
        }

        public static int ReqUpdateMarketTime(this TLClientNet client, MarketTimeImpl mt)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_MARKETTIME, MarketTimeImpl.Serialize(mt), true);
        }

        public static int ReqQryExchange(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_EXCHANGE, "");
        }

        public static int ReqUpdateExchange(this TLClientNet client, ExchangeImpl ex)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_EXCHANGE, ExchangeImpl.Serialize(ex), true);

        }

        public static int ReqQrySecurity(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_SEC, "");
        }
        public static int ReqUpdateSecurity(this TLClientNet client, SecurityFamilyImpl sec)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_SEC, SecurityFamilyImpl.Serialize(sec), true);
        }

        public static int ReqQrySymbol(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_SYM, "");
        }

        public static int ReqUpdateSymbol(this TLClientNet client, SymbolImpl sym)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_SYM, SymbolImpl.Serialize(sym), true);
        }

        public static int ReqQryExchangeRate(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_EXCHANGERATES, "");
        }

        public static int ReqUpdateExchangeRate(this TLClientNet client, ExchangeRate rate)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_EXCHANGERATES, rate);
        }

        public static int ReqQryTickSnapshot(this TLClientNet client, string exchange = "", string symbol = "")
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_TICK_SNAPSHOT, new { exchange = exchange, symbol = symbol });
        }
        #endregion

        #region ConfigTemplate
        /// <summary>
        /// 查询手续费模板
        /// </summary>
        /// <param name="agentfk"></param>
        public static int ReqQryCommissionTemplate(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_TEMPLATE, "");
        }

        /// <summary>
        /// 更新手续费模板
        /// </summary>
        /// <param name="t"></param>
        public static int ReqUpdateCommissionTemplate(this TLClientNet client, CommissionTemplateSetting t)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_COMMISSION_TEMPLATE, t);
        }

        public static int ReqDelCommissionTemplate(this TLClientNet client, CommissionTemplateSetting t)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.DEL_COMMISSION_TEMPLATE, t.ID.ToString());
        }

        /// <summary>
        /// 查询手续费模板项目
        /// </summary>
        public static int ReqQryCommissionTemplateItem(this TLClientNet client, int templateid)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_ITEM, templateid.ToString());
        }

        /// <summary>
        /// 更新手续费项目
        /// </summary>
        /// <param name="t"></param>
        public static int ReqUpdateCommissionTemplateItem(this TLClientNet client, MGRCommissionTemplateItemSetting item)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_COMMISSION_ITEM, item);
        }

        /// <summary>
        /// 查询保证金模板列表
        /// </summary>
        public static int ReqQryMarginTemplate(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_TEMPLATE, "");
        }

        /// <summary>
        /// 更新保证金模板
        /// </summary>
        /// <param name="t"></param>
        public static int ReqUpdateMarginTemplate(this TLClientNet client, MarginTemplateSetting t)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MARGIN_TEMPLATE, t);
        }

        public static int ReqDelMarginTemplate(this TLClientNet client, MarginTemplateSetting t)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.DEL_MARGIN_TEMPLATE, t.ID.ToString());
        }

        /// <summary>
        /// 查询保证金模板项目
        /// </summary>
        public static int ReqQryMarginTemplateItem(this TLClientNet client, int templateid)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_ITEM, templateid.ToString());
        }

        /// <summary>
        /// 更新保证金模板项目
        /// </summary>
        /// <param name="item"></param>
        public static int ReqUpdateCommissionTemplateItem(this TLClientNet client, MGRMarginTemplateItemSetting item)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MARGIN_ITEM, item);
        }


        /// <summary>
        /// 查询手续费模板
        /// </summary>
        /// <param name="agentfk"></param>
        public static int ReqQryExStrategyTemplate(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_TEMPLATE, "");
        }

        /// <summary>
        /// 更新手续费模板
        /// </summary>
        /// <param name="t"></param>
        public static int ReqUpdateExStrategyTemplate(this TLClientNet client, ExStrategyTemplateSetting t)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_EXSTRATEGY_TEMPLATE, t);
        }

        /// <summary>
        /// 查询交易参数模板项目
        /// </summary>
        public static int ReqQryExStrategyTemplateItem(this TLClientNet client, int templateid)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_ITEM, templateid.ToString());
        }

        /// <summary>
        /// 更新手续费项目
        /// </summary>
        /// <param name="t"></param>
        public static int ReqUpdateExStrategyTemplateItem(this TLClientNet client, ExStrategy item)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_EXSTRATEGY_ITEM, item);
        }

        public static int ReqDelExStrategyTemplate(this TLClientNet client, ExStrategyTemplateSetting t)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.DEL_EXSTRATEGY_TEMPLATE, t.ID.ToString());
        }
        #endregion

        #region Domain
        /// <summary>
        /// 查询域
        /// </summary>
        public static int ReqQryDomain(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_DOMAIN, "");
        }

        /// <summary>
        /// 查询分区管理员登入信息
        /// </summary>
        /// <param name="domainid"></param>
        public static int ReqQryDomainRootLoginInfo(this TLClientNet client, int domainid)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_DOMAIN_LOGININFO, domainid.ToString());
        }

        /// <summary>
        /// 更新域
        /// </summary>
        /// <param name="domain"></param>
        public static int ReqUpdateDomain(this TLClientNet client, DomainImpl domain)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_DOMAIN, domain);
        }
        #endregion

        #region Manager
        /// <summary>
        /// 激活管理员
        /// </summary>
        /// <param name="mgrid"></param>
        public static int ReqActiveManger(this TLClientNet client, int mgrid)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MANAGER_ACTIVE, mgrid.ToString());
        }

        /// <summary>
        /// 冻结管理员
        /// </summary>
        /// <param name="mgrid"></param>
        public static int ReqInactiveManger(this TLClientNet client, int mgrid)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MANAGER_INACTIVE, mgrid.ToString());
        }


        public static int ReqDelManager(this TLClientNet client, int mgrid)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.DEL_MANAGER, mgrid.ToString());
        }

        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="oldpass"></param>
        /// <param name="pass"></param>
        public static int ReqUpdatePass(this TLClientNet client, string oldpass, string pass)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MANAGER_PASS, oldpass + "," + pass); ;
        }

        /// <summary>
        /// 查询分区管理员登入信息
        /// </summary>
        /// <param name="domainid"></param>
        public static int ReqQryManagerLoginInfo(this TLClientNet client, int mgrid)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MANAGER_LOGININFO, mgrid.ToString());
        }


        /// <summary>
        /// 查询管理员
        /// </summary>
        public static int ReqQryManager(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MANAGER, "");
        }

        /// <summary>
        /// 更新管理员
        /// </summary>
        /// <param name="mgr"></param>
        public static int ReqUpdateManager(this TLClientNet client, object reqObj)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MANAGER, reqObj.SerializeObject());
        }
        #endregion

        #region Agent
        

        /// <summary>
        /// 观察代理列表
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public static int ReqWatchAgents(this TLClientNet client, string[] accounts)
        {
            return client.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.REQ_WATCH_AGENTS, accounts);
        }
        #endregion

    }
}
