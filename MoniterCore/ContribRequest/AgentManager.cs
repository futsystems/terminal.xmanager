using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 代理账户管理模块扩展命令
    /// </summary>
    public class Method_AGENT_MGR
    {
        /// <summary>
        /// 查询单个代理财务账户
        /// </summary>
        public const string QRY_AGENT = "QryAgent";

        /// <summary>
        /// 获得所有代理账户
        /// </summary>
        public const string QRY_ALL_AGENT = "QryAllAgent";

        /// <summary>
        /// 更新代理模板
        /// </summary>
        public const string REQ_UPDATE_AGENT_TEMPLATE = "UpdateAgentTemplate";

        /// <summary>
        /// 出入金操作
        /// </summary>
        public const string REQ_CASH_TXN = "AgentCashOperation";

        /// <summary>
        /// 查询代理财务信息
        /// </summary>
        public const string QRY_AGENT_FINANCE_INFO = "QryAgentFinanceInfo";

        /// <summary>
        /// Agent更新通知
        /// </summary>
        public const string NOTIFY_AGENT = "NotifyAgent";

        /// <summary>
        /// 代理账户财务信息更新通知
        /// </summary>
        public const string NOTIFY_AGENT_FINANCE_INFO = "NotifyAgentFinInfo";
    }

    public static class AgentManager
    {
        /// <summary>
        /// 查询单个代理财务账户
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static int ReqQryAgent(this TLClientNet client, string account)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT, account);
        }

        /// <summary>
        /// 查询所有代理账户
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static int ReqQryAgent(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.QRY_ALL_AGENT,"");
        }


        /// <summary>
        /// 更新代理模板设置
        /// </summary>
        /// <param name="client"></param>
        /// <param name="reqObj"></param>
        /// <returns></returns>
        public static int ReqUpdateAgentTemplate(this TLClientNet client, object reqObj)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.REQ_UPDATE_AGENT_TEMPLATE, reqObj);
        }

        /// <summary>
        /// 出入金操作
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account">代理账户</param>
        /// <param name="amount">金额</param>
        /// <param name="eq_type">权益类别</param>
        /// <param name="transref">交易引用</param>
        /// <param name="comment">备注</param>
        /// <param name="syncmacct"></param>
        /// <returns></returns>
        public static int ReqAgentCashOperation(this TLClientNet client, string account, decimal amount, QSEnumEquityType eq_type, string transref, string comment)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.REQ_CASH_TXN, new { account = account, amount = amount, txnref = transref, comment = comment, equity_type = eq_type });
        }


        /// <summary>
        /// 查询代理帐户财务信息
        /// </summary>
        /// <param name="account"></param>
        public static int ReqQryAgentFinInfo(this TLClientNet client, string account)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT_FINANCE_INFO, account);
        }

    }
}
