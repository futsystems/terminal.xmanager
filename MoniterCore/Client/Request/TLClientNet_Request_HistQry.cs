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
        public int ReqQryHistOrders(string account, int start,int end)
        {
            MGRQryOrderRequest request = RequestTemplate<MGRQryOrderRequest>.CliSendRequest(++requestid);
            request.Account = account;
            request.Start = start;
            request.End = end;

            SendPacket(request);
            return requestid;

        }

        public int ReqQryHistTrades(string account, int start,int end)
        {
            MGRQryTradeRequest request = RequestTemplate<MGRQryTradeRequest>.CliSendRequest(++requestid);
            request.Account = account;
            request.Start = start;
            request.End = end;

            SendPacket(request);
            return requestid;
        }

        public int ReqQryHistPosition(string account, int settleday)
        {
            MGRQryPositionRequest request = RequestTemplate<MGRQryPositionRequest>.CliSendRequest(++requestid);
            request.Account = account;
            request.Settleday = settleday;

            SendPacket(request);
            return requestid;
        }

        public int ReqQryHistCashTransaction(string account, int start,int end)
        {
            MGRQryCashRequest request = RequestTemplate<MGRQryCashRequest>.CliSendRequest(++requestid);
            request.Account = account;
            request.Start = start;
            request.End = end;

            SendPacket(request);
            return requestid;
        }

        public int ReqQryHistSettlement(string account, int settleday)
        {
            MGRQrySettleRequest request = RequestTemplate<MGRQrySettleRequest>.CliSendRequest(++requestid);
            request.Account = account;
            request.Settleday = settleday;

            SendPacket(request);

            return requestid;
        }
      
    }
}
