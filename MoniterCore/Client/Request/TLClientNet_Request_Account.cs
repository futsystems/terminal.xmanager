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
            MGRQryAccountRequest request = RequestTemplate<MGRQryAccountRequest>.CliSendRequest(requestid++);

            SendPacket(request);

        }

        /// <summary>
        /// 设定观察帐户列表
        /// </summary>
        /// <param name="list"></param>
        public void ReqWatchAccount(List<string> list)
        {
            logger.Info("请求设置观察帐户列表:" + string.Join(",", list.ToArray()));
            MGRWatchAccountRequest request = RequestTemplate<MGRWatchAccountRequest>.CliSendRequest(requestid++);
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
            MGRResumeAccountRequest request = RequestTemplate<MGRResumeAccountRequest>.CliSendRequest(requestid++);
            request.ResumeAccount = account;

            SendPacket(request);
        }

        public void ReqCashOperation(string account, decimal amount,QSEnumEquityType eq_type, string transref, string comment)
        {
            logger.Info("请求出入金操作:" + account + " amount:" + amount.ToString() + " transref:" + transref + " comment:" + comment);
            this.ReqContribRequest("AccountManager", "AccountCashOperation", Mixins.Json.JsonMapper.ToJson(new { account = account, amount = amount, txnref = transref, comment = comment,equity_type=eq_type }));
     
        }

        public void ReqUpdateAccountIntraday(string account, bool intraday)
        {
            logger.Info("请求更新帐户日内属性:" + account + " intraday:" + intraday.ToString());
            this.ReqContribRequest("AccountManager", "UpdateAccountIntraday", Mixins.Json.JsonMapper.ToJson(new { account = account, intraday = intraday }));
     
        }

        public void ReqUpdateAccountCategory(string account, QSEnumAccountCategory category)
        {
            logger.Info("请求更新帐户类别:" + account + " category:" + category.ToString());
            this.ReqContribRequest("AccountManager", "UpdateAccountCategory", Mixins.Json.JsonMapper.ToJson(new { account = account, category = category }));
        }

        public void ReqUpdateRouteType(string account, QSEnumOrderTransferType routertrype)
        {
            logger.Info("请求更新路由类别:" + account + " category:" + routertrype.ToString());
            this.ReqContribRequest("AccountManager", "UpdateRouteType", Mixins.Json.JsonMapper.ToJson(new { account = account, routertrype = routertrype }));
       
        }

        public void ReqUpdateAccountExecute(string account, bool active)
        {
            logger.Info("请求更新交易权限:" + account + " active:" + active.ToString());
            this.ReqContribRequest("AccountManager", "UpdateAccountExecute", Mixins.Json.JsonMapper.ToJson(new { account = account, execute = active }));
       
        }

        public void ReqAddAccount(QSEnumAccountCategory category, string account, string pass, int mgrid, int userid, int routergroupid)
        {
            logger.Info("请求添加交易帐号");
            this.ReqContribRequest("AccountManager", "AddAccount", Mixins.Json.JsonMapper.ToJson(new { account = account, category = category, password = pass, user_id = userid, manager_id = mgrid, routergroup_id = routergroupid }));
        }

        public void ReqDelAccount(string account)
        {
            logger.Info("请求删除交易帐号");
            this.ReqContribRequest("AccountManager", "DelAccount", Mixins.Json.JsonMapper.ToJson(new { account = account }));
        
        }

        public void ReqChangeAccountPass(string account, string pass)
        {
            logger.Info("请求修改交易帐号密码");
            this.ReqContribRequest("AccountManager", "UpdateAccountPass", Mixins.Json.JsonMapper.ToJson(new { account = account, newpass = pass }));
        
        }

        public void ReqChangeInverstorInfo(string account, string name, string broker, int bankfk, string bankac)
        {
            logger.Info("请求修改投资者信息");
            this.ReqContribRequest("AccountManager", "UpdateAccountInvestor", Mixins.Json.JsonMapper.ToJson(new { account = account, name = name, broker = broker, bank_id = bankfk, bank_ac = bankac }));
       
        }
        public void ReqUpdateAccountCommissionTemplate(string account,int templateid)
        {
            logger.Info("请求更新帐户手续费模板");
            this.ReqContribRequest("AccountManager", "UpdateAccountCommissionTemplate", account + "," + templateid.ToString());
        }

        public void ReqUpdateAccountMarginTemplate(string account, int templateid)
        {
            logger.Info("请求更新帐户保证金模板");
            this.ReqContribRequest("AccountManager", "UpdateAccountMarginTemplate", account + "," + templateid.ToString());
        }

        public void ReqUpdateAccountExStrategyTemplate(string account, int templateid)
        {
            logger.Info("请求更交易参数模板");
            this.ReqContribRequest("AccountManager", "UpdateAccountExStrategyTemplate", account + "," + templateid.ToString());
        }


        public void ReqUpdateAccountCreditSeparate(string account, bool separate)
        {
            this.ReqContribRequest("AccountManager", "UpdateAccountCreditSeparate", account + "," + separate.ToString());
        }

        /// <summary>
        /// 查询分区管理员登入信息
        /// </summary>
        /// <param name="domainid"></param>
        public void ReqQryAccountLoginInfo(string account)
        {
            this.ReqContribRequest("AccountManager", "QryAccountLoginInfo", account);
        }


        /// <summary>
        /// 查询交易帐户信息
        /// </summary>
        /// <param name="account"></param>
        public void ReqQryAccountFinInfo(string account)
        {
            this.ReqContribRequest("AccountManager", "QryAccountFinInfo", account);
        }

        /// <summary>
        /// 修改交易帐户路由组
        /// </summary>
        /// <param name="account"></param>
        /// <param name="rgid"></param>
        public void ReqUpdateRouterGroup(string account,int rgid)
        {
            this.ReqContribRequest("AccountManager", "UpdateAccountRouterGroup", account + "," + rgid.ToString());
        }
    }
}
