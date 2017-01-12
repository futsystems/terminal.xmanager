//Copyright 2013 by FutSystems,Inc.
//20170112 去除字符串硬编码

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

        /// <summary>
        /// 更新汇率
        /// </summary>
        /// <param name="rate"></param>
        public void ReqUpdateExchangeRate(ExchangeRate rate)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_EXCHANGERATES, rate);
        }

        /// <summary>
        /// 查询任务运行日志
        /// </summary>
        public void ReqQryExchangeRate()
        {
            logger.Info("查询汇率信息");
            MGRQryExchangeRateRequuest request = RequestTemplate<MGRQryExchangeRateRequuest>.CliSendRequest(++requestid);
            SendPacket(request);
        }

        public void ReqQryExchangeRateContrib()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXCHANGERATES, "");
        }
     
    }
}
