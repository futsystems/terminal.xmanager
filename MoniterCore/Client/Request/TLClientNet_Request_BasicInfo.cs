﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.Protocol;
using TradingLib.Mixins.JsonObject;

namespace TradingLib.MoniterCore
{
    public partial class TLClientNet
    {
       
        #region 基础数据维护

        public void ReqUpdateRecvBank(JsonWrapperReceivableAccount bank)
        {
            this.ReqContribRequest("MgrExchServer", "UpdateReceiveableBank",TradingLib.Mixins.Json.JsonMapper.ToJson(bank)); 
        }
        /// <summary>
        /// 请求同步品种
        /// </summary>
        public void ReqSyncSecurity()
        {
            this.ReqContribRequest("MgrExchServer", "SyncSecInfo", "");
        }

        /// <summary>
        /// 请求同步合约数据
        /// </summary>
        public void ReqSyncSymbol()
        {
            this.ReqContribRequest("MgrExchServer", "SyncSymbol", "");
        }
        /// <summary>
        /// 请求禁止所有合约
        /// </summary>
        public void ReqDisableAllSymbols()
        {
            this.ReqContribRequest("MgrExchServer", "DisableAllSymbols", "");
        }


        public void ReqQryExchange()
        {
            logger.Info("请求查询交易所列表");
            MGRQryExchangeRequuest request = RequestTemplate<MGRQryExchangeRequuest>.CliSendRequest(requestid++);

            SendPacket(request);
        }

        public void ReqQryMarketTime()
        {
            logger.Info("请求查询市场时间列表");
            MGRQryMarketTimeRequest request = RequestTemplate<MGRQryMarketTimeRequest>.CliSendRequest(requestid++);

            SendPacket(request);
        }

        public void ReqUpdateMarketTime(MarketTime mt)
        {
            logger.Info("请求更新交易时间段");
            MGRUpdateMarketTimeRequest request = RequestTemplate<MGRUpdateMarketTimeRequest>.CliSendRequest(requestid++);
            request.MarketTime = mt;

            SendPacket(request);
        }
        public void ReqQrySecurity()
        {
            logger.Info("请求查询品种列表");
            MGRQrySecurityRequest request = RequestTemplate<MGRQrySecurityRequest>.CliSendRequest(requestid++);

            SendPacket(request);
        }
        public void ReqUpdateSecurity(SecurityFamilyImpl sec)
        {
            logger.Info("请求更新品种信息");
            MGRUpdateSecurityRequest request = RequestTemplate<MGRUpdateSecurityRequest>.CliSendRequest(requestid++);
            request.SecurityFaimly = sec;

            SendPacket(request);
        }

        //public void ReqAddSecurity(SecurityFamilyImpl sec)
        //{
        //    logger.Info("请求添加品种信息");
        //    MGRReqAddSecurityRequest request = RequestTemplate<MGRReqAddSecurityRequest>.CliSendRequest(requestid++);
        //    request.SecurityFaimly = sec;

        //    SendPacket(request);
        //}

        public void ReqQrySymbol()
        {
            logger.Info("请求查询合约列表");
            MGRQrySymbolRequest request = RequestTemplate<MGRQrySymbolRequest>.CliSendRequest(requestid++);

            SendPacket(request);
        }

        public void ReqUpdateSymbol(SymbolImpl sym)
        {
            logger.Info("请求更新合约");
            MGRUpdateSymbolRequest request = RequestTemplate<MGRUpdateSymbolRequest>.CliSendRequest(requestid++);
            request.Symbol = sym;

            SendPacket(request);
        }

        //public void ReqUpdateSymbol(SymbolImpl sym)
        //{
        //    logger.Info("请求更新合约");
        //    MGRUpdateSymbolRequest request = RequestTemplate<MGRUpdateSymbolRequest>.CliSendRequest(requestid++);
        //    request.Symbol = sym;

        //    SendPacket(request);
        //}
        #endregion
    }
}
