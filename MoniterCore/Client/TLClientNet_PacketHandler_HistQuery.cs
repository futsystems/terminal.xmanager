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
        }


        void CliOnMGRPositionResponse(RspMGRQryPositionResponse response)
        {
            CoreService.EventIndicator.FireHistPositionEvent(response.PostionToSend, response.IsLast);
        }

        void CliOnMGRCashTransactionResponse(RspMGRQryCashResponse response)
        {
            logger.Info("got cashtransaction response:" + response.ToString());
            //this.handler.OnMGRCashTransactionResponse(response.CashTransToSend, response.IsLast);
        }

        void CliOnMGRSettlementResponse(RspMGRQrySettleResponse response)
        {
            //logger.Info("got settlement response:" + response.ToString());
            CoreService.EventIndicator.FireSettlementEvent(response);
        }
    }
}
