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

        void CliOnMGROrderResponse(RspMGRQryOrderResponse response)
        {
            CoreService.EventIndicator.FireHistOrderEvent(response.OrderToSend, response.IsLast);

            Order o = response.OrderToSend;
            if (o != null)
            {
                o.oSymbol = CoreService.BasicInfoTracker.GetSymbol(o.Symbol);
            }
            CoreService.EventQuery.FireRspMGRQryOrderResponse(o, response.RspInfo, response.RequestID, response.IsLast);
        }

        void CliOnMGRTradeResponse(RspMGRQryTradeResponse response)
        {
            CoreService.EventIndicator.FireHistTradeEvent(response.TradeToSend, response.IsLast);
            Trade f = response.TradeToSend;
            if (f != null)
            {
                f.oSymbol = CoreService.BasicInfoTracker.GetSymbol(f.Symbol);
            }
            CoreService.EventQuery.FireRspMGRQryFillResponse(f, response.RspInfo, response.RequestID, response.IsLast);
            
        }


        void CliOnMGRPositionResponse(RspMGRQryPositionResponse response)
        {
            CoreService.EventIndicator.FireHistPositionEvent(response.PostionToSend, response.IsLast);
            PositionDetail pd = response.PostionToSend;
            if (pd != null)
            {
                pd.oSymbol = CoreService.BasicInfoTracker.GetSymbol(pd.Symbol);
            }
            CoreService.EventQuery.FireRspMGRQryPositionResponse(pd, response.RspInfo, response.RequestID, response.IsLast);
        }

        void CliOnMGRCashTransactionResponse(RspMGRQryCashResponse response)
        {
            CoreService.EventQuery.FireRspMGRQryCashTxnResponse(response.CashTransToSend, response.RspInfo, response.RequestID, response.IsLast);
        }

        void CliOnMGRSettlementResponse(RspMGRQrySettleResponse response)
        {
            //logger.Info("got settlement response:" + response.ToString());
            CoreService.EventIndicator.FireSettlementEvent(response);
        }
    }
}
