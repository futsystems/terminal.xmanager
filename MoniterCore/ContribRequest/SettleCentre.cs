using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public class Method_SettleCentre
    {
        /// <summary>
        /// 查询结算状态
        /// </summary>
        public const string QRY_SETTLESTATUS = "QrySettleStatus";

        /// <summary>
        /// 查询结算持仓
        /// </summary>
        public const string QRY_POSITIONHOLD = "QryPositionHold";

        /// <summary>
        /// 强平结算持仓
        /// </summary>
        public const string FLAT_POSITION = "FlatPositionHold";

        /// <summary>
        /// 查询结算价
        /// </summary>
        public const string QRY_SETTLEPRICE = "QrySettlementPrice";

        /// <summary>
        /// 更新结算价
        /// </summary>
        public const string UPDATE_SETTLEPRICE = "UpdateSettlementPrice";

        /// <summary>
        /// 回滚到交易日
        /// </summary>
        public const string  ROLLBACK ="RollBackToDay";

        /// <summary>
        /// 重新结算交易所
        /// </summary>
        public const string RESETTLE_EXCHANGE = "ReSettleExchange";

        /// <summary>
        /// 重新结算
        /// </summary>
        public const string RESETTLE = "ReSettle";

        /// <summary>
        /// 结算系统重置
        /// </summary>
        public const string RESET = "ResetSystem";


        /// <summary>
        /// 保存数据
        /// </summary>
        public const string REQ_STOREDATA = "StoreSettledData";

        /// <summary>
        /// 删除数据
        /// </summary>
        public const string REQ_DELDATA = "DeleteSettledData";

        /// <summary>
        /// 清除未结算数据
        /// </summary>
        public const string REQ_CLEAR_UNSETTLED = "ClearUnsettledInfo";

    }
    public static class SettleCentre
    {
        /// <summary>
        /// 清除未结算数据
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static int ReqClearUnsettledInfo(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.REQ_CLEAR_UNSETTLED, "");
        }

        /// <summary>
        /// 查询结算状态
        /// </summary>
        public static int ReqQrySettleStatus(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.QRY_SETTLESTATUS, "");
        }

        /// <summary>
        /// 查询当前所有持仓数据
        /// </summary>
        public static int ReqQryPositionHold(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.QRY_POSITIONHOLD, "");
        }

        /// <summary>
        /// 手工平掉某个持仓
        /// </summary>
        /// <param name="data"></param>
        public static int ReqFlatPositionHold(this TLClientNet client, object data)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.FLAT_POSITION, data);
        }

        /// <summary>
        /// 查询某个交易日的结算价格信息
        /// </summary>
        /// <param name="currentday"></param>
        public static int ReqQrySettlementPrice(this TLClientNet client, int settleday)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.QRY_SETTLEPRICE, new { settleday = settleday });
        }


        /// <summary>
        /// 更新结算价格
        /// </summary>
        /// <param name="price"></param>
        public static int ReqUpdateSettlementPrice(this TLClientNet client, SettlementPrice price)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.UPDATE_SETTLEPRICE, price);
        }

        /// <summary>
        /// 重新加载某个日期的交易数据
        /// </summary>
        /// <param name="settleday"></param>
        public static int ReqRollBackToDay(this TLClientNet client, int currentday)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.ROLLBACK, new { currentday = currentday });
        }


        /// <summary>
        /// 重新执行交易所结算
        /// </summary>
        /// <param name="exchange"></param>
        public static int ReqReSettleExchange(this TLClientNet client, string exchange)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.RESETTLE_EXCHANGE, new { exchange = exchange });
        }


        /// <summary>
        /// 重新对某个交易日进行结算操作
        /// </summary>
        /// <param name="settleday"></param>
        public static int ReqReSettle(this TLClientNet client, int settleday)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.RESETTLE, new { settleday = settleday });
        }

       
        /// <summary>
        /// 重置系统 进入工作状态
        /// </summary>
        public static int ReqResetSystem(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.RESET, "");

        }

        public static int ReqStoreSettleData(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.REQ_STOREDATA, "");

        }

        public static int ReqDelSettleData(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.SettleCentre, Method_SettleCentre.REQ_DELDATA, "");

        }
    }
}
