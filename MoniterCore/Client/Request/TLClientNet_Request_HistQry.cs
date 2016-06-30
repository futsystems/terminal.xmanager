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
        public int ReqQryHistOrders(string account, int settleday)
        {
            MGRQryOrderRequest request = RequestTemplate<MGRQryOrderRequest>.CliSendRequest(++requestid);
            request.TradingAccount = account;
            request.Settleday = settleday;

            SendPacket(request);
            return requestid;

        }

        public int ReqQryHistTrades(string account, int settleday)
        {
            MGRQryTradeRequest request = RequestTemplate<MGRQryTradeRequest>.CliSendRequest(++requestid);
            request.TradingAccount = account;
            request.Settleday = settleday;

            SendPacket(request);
            return requestid;
        }

        public int ReqQryHistPosition(string account, int settleday)
        {
            MGRQryPositionRequest request = RequestTemplate<MGRQryPositionRequest>.CliSendRequest(++requestid);
            request.TradingAccount = account;
            request.Settleday = settleday;

            SendPacket(request);
            return requestid;
        }

        public int ReqQryHistCashTransaction(string account, int settleday)
        {
            MGRQryCashRequest request = RequestTemplate<MGRQryCashRequest>.CliSendRequest(++requestid);
            request.TradingAccount = account;
            request.Settleday = settleday;

            SendPacket(request);
            return requestid;
        }

        public int ReqQryHistSettlement(string account, int settleday)
        {
            MGRQrySettleRequest request = RequestTemplate<MGRQrySettleRequest>.CliSendRequest(++requestid);
            request.TradingAccount = account;
            request.Settleday = settleday;

            SendPacket(request);

            return requestid;
        }
      
    }
}
