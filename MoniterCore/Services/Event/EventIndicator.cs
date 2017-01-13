using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public class EventIndicator
    {
        /// <summary>
        /// 行情事件
        /// </summary>
        public event Action<Tick> GotTickEvent;

        /// <summary>
        /// 委托事件
        /// </summary>
        public event Action<Order> GotOrderEvent;

        /// <summary>
        /// 成交事件
        /// </summary>
        public event Action<Trade> GotFillEvent;

        /// <summary>
        /// 持仓明细事件
        /// </summary>
        public event Action<PositionDetail> GotPositionDetailEvent;

        internal void FireTick(Tick k)
        {
            if (GotTickEvent != null)
                GotTickEvent(k);
        }

        internal void FireOrder(Order o)
        {
            if (GotOrderEvent != null)
                GotOrderEvent(o);
        }

        internal void FireFill(Trade f)
        {
            if (GotFillEvent != null)
                GotFillEvent(f);
        }

        internal void FirePositionDetail(PositionDetail p)
        {
            if (GotPositionDetailEvent != null)
                GotPositionDetailEvent(p);
        }
    }
}
