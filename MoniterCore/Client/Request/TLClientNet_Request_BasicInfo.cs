//Copyright 2013 by FutSystems,Inc.
//20170112 去除字符串硬编码

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.Protocol;


namespace TradingLib.MoniterCore
{
    public partial class TLClientNet
    {
        public void ReqQryCalendar()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_CALENDAR, "");
        }

        public void ReqSyncSecurity()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.SYNC_SEC_INFO, "");
        }

        public void ReqSyncSymbol()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.SYNC_SYMBOL_INFO, "");
        }

        public void ReqDisableAllSymbols()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.DISABLE_ALL_SYMBOL, "");
        }

        public void ReqQryMarketTime()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_MARKETTIME, "");
        }

        public void ReqUpdateMarketTime(MarketTimeImpl mt)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_MARKETTIME, MarketTimeImpl.Serialize(mt), true);
        }

        public void ReqQryExchange()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_EXCHANGE, "");
        }

        public void ReqUpdateExchange(ExchangeImpl ex)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_EXCHANGE, ExchangeImpl.Serialize(ex), true);

        }
        
        public void ReqQrySecurity()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_SEC, "");
        }
        public void ReqUpdateSecurity(SecurityFamilyImpl sec)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_SEC, SecurityFamilyImpl.Serialize(sec), true);
        }

        public void ReqQrySymbol()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_SYM, "");
        }

        public void ReqUpdateSymbol(SymbolImpl sym)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_SYM, SymbolImpl.Serialize(sym), true);
        }

        public void ReqQryExchangeRate()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_INFO_EXCHANGERATES, "");
        }

        public void ReqUpdateExchangeRate(ExchangeRate rate)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_INFO_EXCHANGERATES, rate);
        }

        public void ReqQryTickSnapshot(string exchange ="", string symbol = "")
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_TICK_SNAPSHOT, new { exchange = exchange, symbol = symbol });
        }
    }

}
