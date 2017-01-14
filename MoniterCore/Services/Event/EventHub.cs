using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public class EventHub
    {

        /// <summary>
        /// 动态财务信息
        /// </summary>
        public event Action<AccountStatistic> AccountStatisticNotify;

        internal void FireInfoLiteEvent(AccountStatistic notify)
        {
            if (AccountStatisticNotify != null)
                AccountStatisticNotify(notify);
        }

        /// <summary>
        /// 交易帐户变化事件
        /// </summary>
        public event Action<AccountItem> OnAccountChangedEvent;

        internal void FireAccountChangedEvent(AccountItem account)
        {
            if (OnAccountChangedEvent != null)
                OnAccountChangedEvent(account);
        }


        /// <summary>
        /// 交易记录开始恢复
        /// </summary>
        public event Action OnResumeDataStart;
        internal void FireResumeDataStart()
        {
            if (OnResumeDataStart != null)
            {
                OnResumeDataStart();
            }
        }

        /// <summary>
        /// 交易记录恢复完成
        /// </summary>
        public event Action OnResumeDataEnd;
        internal void FireResumeDataEnd()
        {
            if (OnResumeDataEnd != null)
            {
                OnResumeDataEnd();
            }
        }


        /// <summary>
        /// 查询成交事件
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

        /// <summary>
        /// 查询持仓记录
        /// </summary>
        public event Action<PositionDetail, RspInfo, int, bool> OnRspMGRQryPositionResponse;
        internal void FireRspMGRQryPositionResponse(PositionDetail detail, RspInfo rsp, int reqid, bool islast)
        {
            if (OnRspMGRQryPositionResponse != null)
            {
                OnRspMGRQryPositionResponse(detail, rsp, reqid, islast);
            }
        }

        ///// <summary>
        ///// 查询出入金记录
        ///// </summary>
        //public event Action<CashTransaction, RspInfo, int, bool> OnRspMGRQryCashTxnResponse;
        //internal void FireRspMGRQryCashTxnResponse(CashTransaction txn, RspInfo rsp, int reqid, bool islast)
        //{
        //    if (OnRspMGRQryCashTxnResponse != null)
        //    {
        //        OnRspMGRQryCashTxnResponse(txn, rsp, reqid, islast);
        //    }
        //}

        //public event Action<string,RspInfo,int,bool> OnSettlementEvent;
        //internal void FireSettlementEvent(string settlement,RspInfo rsp,int requestId,bool isLast)
        //{

        //    if (OnSettlementEvent != null)
        //        OnSettlementEvent(settlement,rsp,requestId,isLast);
        //}

    }
}
