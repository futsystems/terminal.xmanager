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
        /// 删除某个交易日的结算信息
        /// </summary>
        /// <param name="bank"></param>
        public void ReqDeleteSettleInfo(int settleday)
        {
            this.ReqContribRequest("SettleCentre", "DeleteSettleInfo", new { settleday = settleday }.SerializeObject());
        }

        /// <summary>
        /// 重新对某个交易日进行结算操作
        /// </summary>
        /// <param name="settleday"></param>
        public void ReqReSettle(int settleday)
        {
            this.ReqContribRequest("SettleCentre", "ReSettle", new { settleday = settleday }.SerializeObject());
        }

        /// <summary>
        /// 重新执行交易所结算
        /// </summary>
        /// <param name="exchange"></param>
        public void ReqReSettleExchange(string exchange)
        {
            this.ReqContribRequest("SettleCentre", "ReSettleExchange", new { exchange = exchange }.SerializeObject());
        }
        /// <summary>
        /// 重新加载某个日期的交易数据
        /// </summary>
        /// <param name="settleday"></param>
        public void ReqRollBackToDay(int currentday)
        {
            this.ReqContribRequest("SettleCentre", "RollBackToDay", new { currentday = currentday }.SerializeObject());
        }

        /// <summary>
        /// 查询某个交易日的结算价格信息
        /// </summary>
        /// <param name="currentday"></param>
        public void ReqQrySettlementPrice(int settleday)
        {
            this.ReqContribRequest("SettleCentre", "QrySettlementPrice", new { settleday = settleday }.SerializeObject());
        }


        /// <summary>
        /// 查询结算状态
        /// </summary>
        public void ReqQrySettleStatus()
        {
            this.ReqContribRequest("SettleCentre", "QrySettleStatus", "");
        }

        /// <summary>
        /// 查询当前所有持仓数据
        /// </summary>
        public void ReqQryPositionHold()
        {
            this.ReqContribRequest("SettleCentre", "QryPositionHold", "");
        }

        /// <summary>
        /// 更新结算价格
        /// </summary>
        /// <param name="price"></param>
        public void ReqUpdateSettlementPrice(SettlementPrice price)
        {
            this.ReqContribRequest("SettleCentre", "UpdateSettlementPrice", price.SerializeObject());
        }


        /// <summary>
        /// 手工平掉某个持仓
        /// </summary>
        /// <param name="data"></param>
        public void ReqFlatPositionHold(object data)
        {
            this.ReqContribRequest("SettleCentre", "FlatPositionHold", data.SerializeObject());
        }

        /// <summary>
        /// 重置系统 进入工作状态
        /// </summary>
        public void ReqResetSystem()
        {
            this.ReqContribRequest("SettleCentre", "ResetSystem","");
      
        }

    }
}
