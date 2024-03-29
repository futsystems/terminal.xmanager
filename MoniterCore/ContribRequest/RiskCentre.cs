﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public class Method_RiskCentre
    {
        /// <summary>
        /// 查询风控规则
        /// </summary>
        public const string QRY_RULESET = "QryRuleSet";

        /// <summary>
        /// 查询账户风控项目时
        /// </summary>
        public const string QRY_RULEITEM = "QryRuleItem";

        /// <summary>
        /// 更新账户风控项目
        /// </summary>
        public const string UPDATE_RULEITEM = "UpdateRuleItem";

        /// <summary>
        /// 删除账户风控项目
        /// </summary>
        public const string DEL_RULEITEM = "DelRuleItem";


        /// <summary>
        /// 风控规则更新通知
        /// </summary>
        public const string NOTIFY_UPDATE_RISKRULE = "NotifyRiskRuleUpdate";

        /// <summary>
        /// 强平分账户所有持仓
        /// </summary>
        public const string Flat_ALL = "FlatAllPosition";


        /// <summary>
        /// 请求冻结品种
        /// </summary>
        public const string REQ_BLOCK_SEC = "BlockSecurity";

        /// <summary>
        /// 查询冻结品种
        /// </summary>
        public const string QRY_SEC_BLOCKED = "QrySecurityBlock";
    }

    public static class Client_RiskCentre
    {
        /// <summary>
        /// 查询风控规则
        /// </summary>
        /// <param name="client"></param>
        public static int ReqQryRuleSet(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.RiskCentre, Method_RiskCentre.QRY_RULESET, "");
        }

        /// <summary>
        /// 查询账户风控规则项目
        /// </summary>
        /// <param name="client"></param>
        /// <param name="account"></param>
        /// <param name="type"></param>
        public static int ReqQryRuleItem(this TLClientNet client, string account, QSEnumRuleType type)
        {
            return client.ReqContribRequest(Modules.RiskCentre, Method_RiskCentre.QRY_RULEITEM, new { account = account, ruletype = type});
        }

        /// <summary>
        /// 更新账户风控规则项
        /// </summary>
        /// <param name="client"></param>
        /// <param name="item"></param>
        public static int ReqUpdateRuleItem(this TLClientNet client, RuleItem item)
        {
            return client.ReqContribRequest(Modules.RiskCentre, Method_RiskCentre.UPDATE_RULEITEM, item);
        }

        /// <summary>
        /// 删除账户风控规则项
        /// </summary>
        /// <param name="client"></param>
        /// <param name="item"></param>
        public static int ReqDelRuleItem(this TLClientNet client, RuleItem item)
        {
            return client.ReqContribRequest(Modules.RiskCentre, Method_RiskCentre.DEL_RULEITEM, item);
        }

        /// <summary>
        /// 强平所有分账户持仓
        /// </summary>
        /// <param name="client"></param>
        public static int ReqFlatAllPosition(this TLClientNet client,string[] accounts)
        {
            return client.ReqContribRequest(Modules.RiskCentre, Method_RiskCentre.Flat_ALL, new { accounts=accounts});
        }

        public static int ReqQrySecurityBlocked(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.RiskCentre, Method_RiskCentre.QRY_SEC_BLOCKED, "");
        }
         public static int ReqBlockSecurity(this TLClientNet client,string[] secs)
        {
            return client.ReqContribRequest(Modules.RiskCentre, Method_RiskCentre.REQ_BLOCK_SEC, secs);
        }
    }
}
