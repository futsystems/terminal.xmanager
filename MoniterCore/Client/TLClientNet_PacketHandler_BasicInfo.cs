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
        void CliOnMGRMarketTime(RspMGRQryMarketTimeResponse response)
        {
            logger.Debug("got markettime response:" + response.ToString());
            CoreService.BasicInfoTracker.GotMarketTime(response.MarketTime, response.IsLast);
        }

        void CliOnMGRExchange(RspMGRQryExchangeResponse response)
        {
            logger.Debug("got exchange response:" + response.ToString());
            CoreService.BasicInfoTracker.GotExchange(response.Exchange, response.IsLast);
        }

        void CliOnMGRSecurity(RspMGRQrySecurityResponse response)
        {
            logger.Debug("got security response:" + response.ToString());
            CoreService.BasicInfoTracker.GotSecurity(response.SecurityFaimly, response.IsLast);
        }

        void CliOnMGRSymbol(RspMGRQrySymbolResponse response)
        {
            logger.Debug("got symbol response:" + response.ToString());
            CoreService.BasicInfoTracker.GotSymbol(response.Symbol, response.IsLast);
        }
    }
}
