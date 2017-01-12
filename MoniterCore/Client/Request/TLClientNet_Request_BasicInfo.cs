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
       
        #region 基础数据维护


        public void ReqQryCalendar()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_CALENDAR, "");
        }

        /// <summary>
        /// 请求同步品种
        /// </summary>
        public void ReqSyncSecurity()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.SYNC_SEC_INFO, "");
        }

        /// <summary>
        /// 请求同步合约数据
        /// </summary>
        public void ReqSyncSymbol()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.SYNC_SYMBOL_INFO, "");
        }
        /// <summary>
        /// 请求禁止所有合约
        /// </summary>
        public void ReqDisableAllSymbols()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.DISABLE_ALL_SYMBOL, "");
        }


        public void ReqQryExchange()
        {
            logger.Info("请求查询交易所列表");
            MGRQryExchangeRequuest request = RequestTemplate<MGRQryExchangeRequuest>.CliSendRequest(++requestid);
            SendPacket(request);
        }

        public void ReqUpdateExchange(ExchangeImpl ex)
        {
            logger.Info("请求更新交易所");
            MGRUpdateExchangeRequest request = RequestTemplate<MGRUpdateExchangeRequest>.CliSendRequest(++requestid);
            request.Exchange = ex;
            SendPacket(request);


        }
        public void ReqQryMarketTime()
        {
            logger.Info("请求查询市场时间列表");
            MGRQryMarketTimeRequest request = RequestTemplate<MGRQryMarketTimeRequest>.CliSendRequest(++requestid);
            SendPacket(request);
        }

        public void ReqUpdateMarketTime(MarketTimeImpl mt)
        {
            logger.Info("请求更新交易时间段");
            MGRUpdateMarketTimeRequest request = RequestTemplate<MGRUpdateMarketTimeRequest>.CliSendRequest(++requestid);
            request.MarketTime = mt;
            SendPacket(request);
        }
        public void ReqQrySecurity()
        {
            logger.Info("请求查询品种列表");
            MGRQrySecurityRequest request = RequestTemplate<MGRQrySecurityRequest>.CliSendRequest(++requestid);
            SendPacket(request);
        }
        public void ReqUpdateSecurity(SecurityFamilyImpl sec)
        {
            logger.Info("请求更新品种信息");
            MGRUpdateSecurityRequest request = RequestTemplate<MGRUpdateSecurityRequest>.CliSendRequest(++requestid);
            request.SecurityFaimly = sec;
            SendPacket(request);
        }

        public void ReqQrySymbol()
        {
            logger.Info("请求查询合约列表");
            MGRQrySymbolRequest request = RequestTemplate<MGRQrySymbolRequest>.CliSendRequest(++requestid);
            SendPacket(request);
        }

        public int ReqUpdateSymbol(SymbolImpl sym)
        {
            logger.Info("请求更新合约");
            MGRUpdateSymbolRequest request = RequestTemplate<MGRUpdateSymbolRequest>.CliSendRequest(++requestid);
            request.Symbol = sym;
            SendPacket(request);
            return requestid;
        }

        /// <summary>
        /// 请求查询合约快照
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public int ReqQryTickSnapshot(string exchange ="", string symbol = "")
        {
            logger.Info(string.Format("QryTickSnapshot Exchange:{0} Symbol:{1}", exchange, symbol));
            MGRQryTickSnapShotRequest request = RequestTemplate<MGRQryTickSnapShotRequest>.CliSendRequest(++requestid);
            request.Exchange = exchange;
            request.Symbol = symbol;
            SendPacket(request);
            return requestid;
        }
        #endregion
    }
}
