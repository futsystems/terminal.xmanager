﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public class EventBasicInfo
    {
        //public event Action<MarketTimeImpl> OnMarketTimeEvent;

        //public event Action<ExchangeImpl> OnExchangeEvent;

        //public event Action<SecurityFamilyImpl> OnSecurityEvent;

        //public event Action<SymbolImpl> OnSymbolEvent;

        public event Action<RouterGroupSetting> OnRouterGroupEvent;

        public event Action<ManagerSetting> OnManagerEvent;


        //internal void FireMarketTime(MarketTimeImpl mt)
        //{
        //    if (OnMarketTimeEvent != null)
        //    {
        //        OnMarketTimeEvent(mt);
        //    }
        //}

        //internal void FireExchangeEvent(ExchangeImpl exch)
        //{
        //    if (OnExchangeEvent != null)
        //    {
        //        OnExchangeEvent(exch);
        //    }
        //}

        //internal void FireSecurityEvent(SecurityFamilyImpl sec)
        //{
        //    if (OnSecurityEvent != null)
        //        OnSecurityEvent(sec);
        //}

        //internal void FireSymbolEvent(SymbolImpl sym)
        //{
        //    if (OnSymbolEvent != null)
        //        OnSymbolEvent(sym);
        //}

        internal void FireRouterGroupEvent(RouterGroupSetting rg)
        {
            if (OnRouterGroupEvent != null)
                OnRouterGroupEvent(rg);
        }

        internal void FireManagerEvent(ManagerSetting mgr)
        {
            if (OnManagerEvent != null)
                OnManagerEvent(mgr);
        }
    }
}
