using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public enum  QSEnumGateWayType
    {
        /// <summary>
        /// 宝付
        /// </summary>
        BaoFu,

        /// <summary>
        /// 支付宝
        /// </summary>
        AliPay,
    }


    public class GateWayConfig
    {
        public int ID { get; set; }

        public int Domain_ID { get; set; }

        public QSEnumGateWayType GateWayType { get; set; }

        public string Config { get; set; }

        public bool Avabile { get; set; }
    }


    public class Method_API
    {
        public const string UPDATE_GATEWAY_CONFIG = "UpdateGateWayConfig";

        public const string QRY_GATEWAY_CONFUIG = "QryGateWayConfig";

    
    }

    public static class Client_API
    {
        /// <summary>
        /// 添加交易帐户
        /// </summary>
        /// <param name="createion"></param>
        public static int ReqUpdateGateWayConfig(this TLClientNet client,GateWayConfig config)
        {
            return client.ReqContribRequest(Modules.APIService, Method_API.UPDATE_GATEWAY_CONFIG, config);
        }

        /// <summary>
        /// 查询支付网关配置
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static int ReqQryGateWayConfig(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.APIService, Method_API.QRY_GATEWAY_CONFUIG, "");
        }

    }
}
