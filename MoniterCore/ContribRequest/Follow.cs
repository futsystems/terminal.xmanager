using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public class Method_Follow
    {
        /// <summary>
        /// 查询交易端会话信息
        /// </summary>
        public const string QRY_FOLLOW_ITEM_DETAIL = "QryFollowItemDetail";

        /// <summary>
        /// 断开某个交易账户所有连接终端
        /// </summary>
        public const string FLAT_FOLLOWITEM = "FlatFollowItem";

        /// <summary>
        /// 查询跟单执行项
        /// </summary>
        public const string QRY_FOLLOW_EXECUTION = "QryFollowExecution";


        /// <summary>
        /// 查询跟单策略配置列表
        /// </summary>
        public const string QRY_STRATEGY_CFG_LIST = "QryFollowStrategyList";

    }

    public static class Follow
    {

        /// <summary>
        /// 查询跟单项明细
        /// </summary>
        /// <param name="account"></param>
        public static int ReqQryFollowItemDetail(this TLClientNet client,int strategyId, string followKey)
        {
            return client.ReqContribRequest(Modules.Follow, Method_Follow.QRY_FOLLOW_ITEM_DETAIL, new { StrategyID = strategyId, FollowKey = followKey });
        }

        /// <summary>
        /// 强平某个跟单项
        /// </summary>
        public static int ReqFlatFollowItem(this TLClientNet client, int strategyId, string followKey)
        {
            return client.ReqContribRequest(Modules.Follow, Method_Follow.FLAT_FOLLOWITEM, new { StrategyID = strategyId, FollowKey = followKey });
        }

        /// <summary>
        /// 查询跟单项
        /// </summary>
        /// <param name="client"></param>
        /// <param name="strategyid"></param>
        /// <param name="settleday"></param>
        /// <returns></returns>
        public static int ReqQryFollowExecution(this TLClientNet client, int strategyid, int settleday)
        {
            return client.ReqContribRequest(Modules.Follow, Method_Follow.QRY_FOLLOW_EXECUTION, new { StrategyID = strategyid, Settleday = settleday });
        }

        /// <summary>
        /// 查询跟单列表
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static int ReqQryFollowStrategyList(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.Follow, Method_Follow.QRY_STRATEGY_CFG_LIST, "");
        }
    }
}
