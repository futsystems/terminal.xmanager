using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 账户管理模块扩展命令
    /// </summary>
    public class Method_ACC_MGR
    {
        /// <summary>
        /// 添加账户
        /// </summary>
        public const string ADD_ACC = "AddAccountFacde";

        /// <summary>
        /// 删除账户
        /// </summary>
        public const string DEL_ACC = "DelAccount";

        /// <summary>
        /// 查询账户Profile
        /// </summary>
        public const string QRY_ACC_PROFILE = "QryAccountProfile";

        /// <summary>
        /// 更新账户Profile
        /// </summary>
        public const string UPDATE_ACC_PROFILE = "UpdateAccountProfile";

        /// <summary>
        /// 更新账户日内属性
        /// </summary>
        public const string UPDATE_ACC_INTRADAY = "UpdateAccountIntraday";

        /// <summary>
        /// 更新账户路由属性
        /// </summary>
        public const string UPDATE_ROUTE_TYPE = "UpdateRouteType";

        /// <summary>
        /// 更新账户交易状态
        /// </summary>
        public const string UPDATE_ACC_EXECUTE = "UpdateAccountExecute";

        /// <summary>
        /// 更新账户密码
        /// </summary>
        public const string UPDATE_ACC_PASS = "UpdateAccountPass";

        /// <summary>
        /// 更新账户货币
        /// </summary>
        public const string UPDATE_ACC_CURRENCY = "UpdateAccountCurrency";

        /// <summary>
        /// 更新账户手续费模板
        /// </summary>
        public const string UPDATE_TEMPLATE_COMMISSION = "UpdateAccountCommissionTemplate";

        /// <summary>
        /// 更新保证金模板
        /// </summary>
        public const string UPDATE_TEMPLATE_MARGIN = "UpdateAccountMarginTemplate";

        /// <summary>
        /// 更新交易参数模板
        /// </summary>
        public const string UPDATE_TEMPLATE_EXSTRATEGY = "UpdateAccountExStrategyTemplate";


        /// <summary>
        /// 更新配置模板
        /// </summary>
        public const string UPDATE_TEMPLATE_CONFIG = "UpdateAccountConfigTemplate";

        /// <summary>
        /// 更新账户路由组
        /// </summary>
        public const string UPDATE_ACC_ROUTEGROUP = "UpdateAccountRouterGroup";

        /// <summary>
        /// 执行出入金操作
        /// </summary>
        public const string CASH_ACC = "AccountCashOperation";

        /// <summary>
        /// 查询账户财务信息
        /// </summary>
        public const string QRY_ACC_FININFO = "QryAccountFinInfo";

        /// <summary>
        /// 账户财务信息更新
        /// </summary>
        public const string NOTIFY_ACC_FININFO = "NotifyAccountFinInfo";


        /// <summary>
        /// 查询交易账户登入信息
        /// </summary>
        public const string QRY_LOGIN_INFO = "QryAccountLoginInfo";


        #region 历史查询

        /// <summary>
        /// 查询交易账户结算单
        /// </summary>
        public const string QRY_ACC_SETTLEMENT = "QueryAccountSettlementDetail";


        /// <summary>
        /// 查询交易账户结算单
        /// </summary>
        public const string QRY_ACC_SETTLEMENTS = "QueryAccountSettlements";

        /// <summary>
        /// 查询交易账户出入金记录
        /// </summary>
        public const string QRY_ACC_TXN = "QueryAccountCashTxn";
        /// <summary>
        /// 查询交易账户委托
        /// </summary>
        public const string QRY_ACC_ORDER = "QueryAccountOrder";

        /// <summary>
        /// 查询交易账户成交
        /// </summary>
        public const string QRY_ACC_TRADE = "QueryAccountTrade";

        /// <summary>
        /// 查询交易账户持仓
        /// </summary>
        public const string QRY_ACC_POSITION = "QueryAccountPosition";

        #endregion
    }

    public static class Client_AccountManager
    {
        /// <summary>
        /// 添加交易帐户
        /// </summary>
        /// <param name="createion"></param>
        public static int ReqAddAccount(this TLClientNet client,AccountCreation createion)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.ADD_ACC, createion);
        }

        /// <summary>
        /// 删除交易账号
        /// </summary>
        /// <param name="account"></param>
        public static int ReqDelAccount(this TLClientNet client, string account)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.DEL_ACC, new { accounts = new string[] { account } });
        }

        public static int ReqDelAccount(this TLClientNet client, string[] accounts)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.DEL_ACC, new { accounts = accounts });
        }


        /// <summary>
        /// 查询个人信息
        /// </summary>
        /// <param name="account"></param>
        public static int ReqQryAccountProfile(this TLClientNet client, string account)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_PROFILE, account);
        }

        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="account"></param>
        public static int ReqUpdateAccountProfile(this TLClientNet client, AccountProfile profile)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_PROFILE, profile);
        }


        public static int ReqUpdateAccountCategory(this TLClientNet client, string account, QSEnumAccountCategory category)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, "UpdateAccountCategory", new { account = account, category = category });
        }

        public static int ReqUpdateAccountExecute(this TLClientNet client, string account, bool active)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_EXECUTE, new { accounts = new string[]{account}, execute = active });
        }

        public static int ReqUpdateAccountExecute(this TLClientNet client, string[] accounts, bool active)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_EXECUTE, new { accounts = accounts, execute = active });
        }



        public static int ReqUpdateAccountIntraday(this TLClientNet client, string account, bool intraday)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_INTRADAY, new { account = account, intraday = intraday });
        }

        public static int ReqUpdateRouteType(this TLClientNet client, string account, QSEnumOrderTransferType routertrype)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ROUTE_TYPE, new { account = account, routertrype = routertrype });
        }



        public static int ReqUpdateAccountCurrency(this TLClientNet client, string account, CurrencyType currency)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_CURRENCY, new { account = account, currency = currency.ToString() });
        }

        public static int ReqCashOperation(this TLClientNet client, string account, decimal amount, QSEnumEquityType eq_type, string transref, string comment, bool syncmacct)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.CASH_ACC, new { account = account, amount = amount, txnref = transref, comment = comment, equity_type = eq_type, sync_mainacct = syncmacct });
        }

        public static int ReqChangeAccountPass(this TLClientNet client, string account, string pass)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_PASS, new { account = account, newpass = pass });
        }


        public static int ReqUpdateAccountCommissionTemplate(this TLClientNet client, string account, int templateid)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_TEMPLATE_COMMISSION, account + "," + templateid.ToString());
        }

        public static int ReqUpdateAccountMarginTemplate(this TLClientNet client, string account, int templateid)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_TEMPLATE_MARGIN, account + "," + templateid.ToString());
        }

        public static int ReqUpdateAccountExStrategyTemplate(this TLClientNet client, string account, int templateid)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_TEMPLATE_EXSTRATEGY, account + "," + templateid.ToString());
        }

        public static int ReqUpdateAccountConfigTemplate(this TLClientNet client, string account, int templateid,bool force)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_TEMPLATE_CONFIG, new { accounts = new string[]{ account}, template_id = templateid, force = force });
        }
        public static int ReqUpdateAccountConfigTemplate(this TLClientNet client, string[] list, int templateid, bool force)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_TEMPLATE_CONFIG, new { accounts = list, template_id = templateid, force = force });
        }


        /// <summary>
        /// 修改交易帐户路由组
        /// </summary>
        /// <param name="account"></param>
        /// <param name="rgid"></param>
        public static int ReqUpdateRouterGroup(this TLClientNet client, string account, int rgid)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_ROUTEGROUP, account + "," + rgid.ToString());
        }

        /// <summary>
        /// 查询交易帐户信息
        /// </summary>
        /// <param name="account"></param>
        public static int ReqQryAccountFinInfo(this TLClientNet client, string account)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_FININFO, account);
        }

        /// <summary>
        /// 查询账户登入信息
        /// </summary>
        /// <param name="domainid"></param>
        public static int ReqQryAccountLoginInfo(this TLClientNet client, string account)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_LOGIN_INFO, account);
        }

        #region 历史记录查询
        /// <summary>
        /// 查询账户结算单
        /// </summary>
        /// <param name="mgrid"></param>
        public static int ReqQryAccountSettlement(this TLClientNet client, string account, int tradingday)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_SETTLEMENT, new { account = account, tradingday = tradingday });
        }



        /// <summary>
        /// 查询账户结算单列表
        /// </summary>
        /// <param name="mgrid"></param>
        public static int ReqQryAccountSettlements(this TLClientNet client, string account, int start,int end)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_SETTLEMENTS, new { account = account, start = start, end = end });
        }


        /// <summary>
        /// 查询账户委托记录
        /// </summary>
        /// <param name="mgrid"></param>
        public static int ReqQryAccountOrder(this TLClientNet client, string account, int start, int end)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_ORDER, new { account = account, start = start, end = end });
        }

        /// <summary>
        /// 查询账户成交记录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static int ReqQryAccountTrade(this TLClientNet client, string account, int start, int end)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_TRADE, new { account = account, start = start, end = end });
        }

        /// <summary>
        /// 查询交易账户持仓
        /// </summary>
        /// <param name="account"></param>
        /// <param name="tradingday"></param>
        public static int ReqQryAccountPosition(this TLClientNet client, string account, int tradingday)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_POSITION, new { account = account, tradingday = tradingday });
        }

        /// <summary>
        /// 查询交易帐户出入金记录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static int ReqQryAccountCashTxn(this TLClientNet client, string account, long start, long end)
        {
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_TXN, new { account = account, start = start, end = end });
        }
        #endregion
    }
}
