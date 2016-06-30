using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public class EventQuery
    {
        /// <summary>
        /// 查询委托事件
        /// </summary>
        public event Action<Trade, RspInfo, int, bool> OnRspMGRQryFillResponse;

        internal void FireRspMGRQryFillResponse(Trade fill, RspInfo rsp, int reqid, bool islast)
        {
            if (OnRspMGRQryFillResponse != null)
            {
                OnRspMGRQryFillResponse(fill, rsp, reqid, islast);
            }
        }

        /// <summary>
        /// 查询委托事件
        /// </summary>
        public event Action<Order, RspInfo, int, bool> OnRspMGRQryOrderResponse;
        internal void FireRspMGRQryOrderResponse(Order order, RspInfo rsp, int reqid, bool islast)
        {
            if (OnRspMGRQryOrderResponse != null)
            {
                OnRspMGRQryOrderResponse(order, rsp, reqid, islast);
            }
        }
    }
}
