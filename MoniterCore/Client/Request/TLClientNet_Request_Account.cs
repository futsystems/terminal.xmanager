//Copyright 2013 by FutSystems,Inc.
//20170112 去除字符串硬编码

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public partial class TLClientNet
    {
        /// <summary>
        /// 查询交易帐户列表
        /// </summary>
        public void ReqQryAccountList()
        {
            logger.Info("查询交易帐户列表");
            MGRQryAccountRequest request = RequestTemplate<MGRQryAccountRequest>.CliSendRequest(++requestid);
            SendPacket(request);

        }

        /// <summary>
        /// 设定观察帐户列表
        /// </summary>
        /// <param name="list"></param>
        public void ReqWatchAccount(List<string> list)
        {
            logger.Info("请求设置观察帐户列表:" + string.Join(",", list.ToArray()));
            MGRWatchAccountRequest request = RequestTemplate<MGRWatchAccountRequest>.CliSendRequest(++requestid);
            request.Add(list);
            SendPacket(request);
        }

        /// <summary>
        /// 恢复某个交易帐号的日内交易数据
        /// </summary>
        /// <param name="account"></param>
        public void ReqResumeAccount(string account)
        {
            logger.Info("请求恢复日内交易数据 Account:" + account);
            MGRResumeAccountRequest request = RequestTemplate<MGRResumeAccountRequest>.CliSendRequest(++requestid);
            request.ResumeAccount = account;
            SendPacket(request);
        }

        public void ReqCashOperation(string account, decimal amount,QSEnumEquityType eq_type, string transref, string comment,bool syncmacct)
        {
            logger.Info("请求出入金操作:" + account + " amount:" + amount.ToString() + " transref:" + transref + " comment:" + comment + " syncmainacct:" + syncmacct.ToString());
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.CASH_ACC, new { account = account, amount = amount, txnref = transref, comment = comment, equity_type = eq_type, sync_mainacct = syncmacct });
        }

        public void ReqUpdateAccountIntraday(string account, bool intraday)
        {
            logger.Info("请求更新帐户日内属性:" + account + " intraday:" + intraday.ToString());
            this.ReqContribRequest(Modules.ACC_MGR,Method_ACC_MGR.UPDATE_ACC_INTRADAY, new { account = account, intraday = intraday });
     
        }

        public void ReqUpdateAccountCategory(string account, QSEnumAccountCategory category)
        {
            logger.Info("请求更新帐户类别:" + account + " category:" + category.ToString());
            this.ReqContribRequest(Modules.ACC_MGR, "UpdateAccountCategory", new { account = account, category = category });
        }

        public void ReqUpdateRouteType(string account, QSEnumOrderTransferType routertrype)
        {
            logger.Info("请求更新路由类别:" + account + " category:" + routertrype.ToString());
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ROUTE_TYPE, new { account = account, routertrype = routertrype });
       
        }

        public void ReqUpdateAccountExecute(string account, bool active)
        {
            logger.Info("请求更新交易权限:" + account + " active:" + active.ToString());
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_EXECUTE, new { account = account, execute = active });
       
        }

        public void ReqUpdateAccountCurrency(string account, CurrencyType currency)
        {
            logger.Info("请求更新帐户货贝类型:" + account + " currency:" + currency.ToString());
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_CURRENCY, new { account = account, currency = currency.ToString() });
       
        }

        /// <summary>
        /// 添加交易帐户
        /// </summary>
        /// <param name="createion"></param>
        public void ReqAddAccount(AccountCreation createion)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.ADD_ACC, createion);
        }

        /// <summary>
        /// 查询个人信息
        /// </summary>
        /// <param name="account"></param>
        public void ReqQryAccountProfile(string account)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_PROFILE, account);
        }

        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="account"></param>
        public void ReqUpdateAccountProfile(AccountProfile profile)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_PROFILE, profile);
        }

        /// <summary>
        /// 删除交易账号
        /// </summary>
        /// <param name="account"></param>
        public void ReqDelAccount(string account)
        {
            logger.Info("请求删除交易帐号");
            this.ReqContribRequest(Modules.ACC_MGR,Method_ACC_MGR.DEL_ACC,account);
        
        }

        public void ReqChangeAccountPass(string account, string pass)
        {
            logger.Info("请求修改交易帐号密码");
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_PASS, new { account = account, newpass = pass });
        
        }

        public void ReqUpdateAccountCommissionTemplate(string account,int templateid)
        {
            logger.Info("请求更新帐户手续费模板");
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_TEMPLATE_COMMISSION, account + "," + templateid.ToString());
        }

        public void ReqUpdateAccountMarginTemplate(string account, int templateid)
        {
            logger.Info("请求更新帐户保证金模板");
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_TEMPLATE_MARGIN, account + "," + templateid.ToString());
        }

        public void ReqUpdateAccountExStrategyTemplate(string account, int templateid)
        {
            logger.Info("请求更交易参数模板");
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_TEMPLATE_EXSTRATEGY, account + "," + templateid.ToString());
        }

        /// <summary>
        /// 查询账户登入信息
        /// </summary>
        /// <param name="domainid"></param>
        public void ReqQryAccountLoginInfo(string account)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_LOGIN_INFO, account);
        }


        /// <summary>
        /// 查询交易帐户信息
        /// </summary>
        /// <param name="account"></param>
        public void ReqQryAccountFinInfo(string account)
        {
            this.ReqContribRequest(Modules.ACC_MGR,Method_ACC_MGR.QRY_ACC_FININFO, account);
        }

        /// <summary>
        /// 修改交易帐户路由组
        /// </summary>
        /// <param name="account"></param>
        /// <param name="rgid"></param>
        public void ReqUpdateRouterGroup(string account,int rgid)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_ROUTEGROUP, account + "," + rgid.ToString());
        }
    }
}
