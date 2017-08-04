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


        /// <summary>
        /// 查询代理出入金记录
        /// </summary>
        public const string QRY_AGENT_CASH_TXN = "QueryAgentCashTxn";


        /// <summary>
        /// 查询一个时间区间内的结算单
        /// </summary>
        public const string QRY_AGENT_SETTLEMENTS = "QueryAgentSettlements";

        /// <summary>
        /// 统计查询
        /// </summary>
        public const string QRY_STATISTIC_SETTLEMENT = "QueryStatisticSettlement";

        /// <summary>
        /// 更新强平权益值
        /// </summary>
        public const string REQ_UPDATE_AGENT_FLAT_EQUITY = "UpdateAgentFlatEquity";

        /// <summary>
        /// 设定代理默认配置模板
        /// </summary>
        public const string REQ_UPDATE_AGEENT_FLAT_EQUITY = "UpdateAgentDefaultConfigTemplate";
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

        /// <summary>
        /// 查询代理帐户出入金记录
        /// </summary>
        /// <param name="account"></param>
        public static int ReqQryAgentCashTxn(this TLClientNet client, string account, long start, long end)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT_CASH_TXN, new { account = account, start = start, end = end });
        }

        /// <summary>
        /// 查询代理帐户结算单记录
        /// </summary>
        /// <param name="account"></param>
        public static int ReqQryAgentSettlement(this TLClientNet client, string account, int start, int end)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.QRY_AGENT_SETTLEMENTS, new { account = account, start = start, end = end });
        }

        /// <summary>
        /// 查询某个代理下账户结算统计
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int ReqQrySettlementStatistic(this TLClientNet client, string account, int start, int end ,bool direct)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.QRY_STATISTIC_SETTLEMENT, new { account = account, start = start, end = end, direct = direct });
        }

        /// <summary>
        /// 更新代理强平权益
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ReqUpdateAgentFlatEquity(this TLClientNet client, string account, decimal val)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.REQ_UPDATE_AGENT_FLAT_EQUITY, new { account = account, flat_equity = val });
        }

        /// <summary>
        /// 更新代理默认配置模板
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account"></param>
        /// <param name="template_id"></param>
        /// <param name="setaccount"></param>
        /// <returns></returns>
        public static int ReqUpdateAgentDefaultConfigTemplate(this TLClientNet client, string account, int config_id, bool setaccount)
        {
            return client.ReqContribRequest(Modules.AgentManager, Method_AGENT_MGR.REQ_UPDATE_AGEENT_FLAT_EQUITY, new { account = account, config_id = config_id, setaccount = setaccount });
        }
    }
}
