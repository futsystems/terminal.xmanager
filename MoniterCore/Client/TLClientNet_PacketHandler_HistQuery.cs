﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public partial class TLClientNet
    {

        void CliOnMGROrderResponse(RspMGRQryOrderResponse response)
        {
            Order o = response.OrderToSend;
            if (o != null)
            {
                o.oSymbol = CoreService.BasicInfoTracker.GetSymbol(o.Symbol);
            }
            CoreService.EventHub.FireRspMGRQryOrderResponse(o, response.RspInfo, response.RequestID, response.IsLast);
        }

        void CliOnMGRTradeResponse(RspMGRQryTradeResponse response)
        {
            Trade f = response.TradeToSend;
            if (f != null)
            {
                f.oSymbol = CoreService.BasicInfoTracker.GetSymbol(f.Symbol);
            }
            CoreService.EventHub.FireRspMGRQryFillResponse(f, response.RspInfo, response.RequestID, response.IsLast);
        }


        void CliOnMGRPositionResponse(RspMGRQryPositionResponse response)
        {
            PositionDetail pd = response.PostionToSend;
            if (pd != null)
            {
                pd.oSymbol = CoreService.BasicInfoTracker.GetSymbol(pd.Symbol);
            }
            CoreService.EventHub.FireRspMGRQryPositionResponse(pd, response.RspInfo, response.RequestID, response.IsLast);
        }

        void CliOnMGRCashTransactionResponse(RspMGRQryCashResponse response)
        {
            CoreService.EventHub.FireRspMGRQryCashTxnResponse(response.CashTransToSend, response.RspInfo, response.RequestID, response.IsLast);
        }

        void CliOnMGRSettlementResponse(RspMGRQrySettleResponse response)
        {
            CoreService.EventHub.FireSettlementEvent(response.SettlementContent, response.RspInfo, response.RequestID, response.IsLast);
        }
    }
}
