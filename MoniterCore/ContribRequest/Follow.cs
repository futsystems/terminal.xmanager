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
    }
}
