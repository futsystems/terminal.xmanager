using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
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
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.DEL_ACC, account);
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
            return client.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_EXECUTE, new { account = account, execute = active });
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
