using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
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

        /// <summary>
        /// 环迅
        /// </summary>
        IPS,

        /// <summary>
        /// 银生宝
        /// </summary>
        UnsPay,

        /// <summary>
        /// 智付
        /// </summary>
        DinPay,

        /// <summary>
        /// 爱农
        /// </summary>
        ChinagPay,

        /// <summary>
        /// 财易支付
        /// </summary>
        Cai1Pay,


        /// <summary>
        /// 国付宝
        /// </summary>
        GoPay,

        /// <summary>
        /// 天付宝
        /// </summary>
        TFBPay,

        /// <summary>
        /// 墨宝
        /// </summary>
        MoBoPay,

        /// <summary>
        /// 随行付
        /// </summary>
        SuiXingPay,

        /// <summary>
        /// 易势付
        /// </summary>
        IELPMPay,

        /// <summary>
        /// 易通支付
        /// </summary>
        ETonePay,

        /// <summary>
        /// 智汇付
        /// </summary>
        ZhiHPay,

        /// <summary>
        /// 分众支付
        /// </summary>
        FZPay,

        /// <summary>
        /// 多得宝
        /// </summary>
        DDBillPay,

        /// <summary>
        /// 高汇通
        /// </summary>
        GaoHuiTong,


        /// <summary>
        /// 汇潮支付
        /// </summary>
        Ecpss,

        /// <summary>
        /// 易路通
        /// </summary>
        Fjelt
    }

    public enum EnumBusinessType
    {
        /// <summary>
        /// 普通类别
        /// </summary>
        [Description("普通出入金")]
        Normal,

        /// <summary>
        /// 配资入金 资金会根据账户资金按杠杆比例自动调整优先资金
        /// </summary>
        [Description("配资入金")]
        LeverageDeposit,

        /// <summary>
        /// 减少配资 减少账户优先资金 用于降低风险
        /// </summary>
        [Description("减少配资")]
        CreditWithdraw
    }


    public class CashOperation
    {
        /// <summary>
        /// 交易账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long DateTime { get; set; }

        /// <summary>
        /// 出入金类别
        /// </summary>
        public QSEnumCashOperation OperationType { get; set; }

        /// <summary>
        /// 网关类别
        /// </summary>
        public QSEnumGateWayType GateWayType { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public QSEnumCashInOutStatus Status { get; set; }

        /// <summary>
        /// 单号引用
        /// </summary>
        public string Ref { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 业务类别
        /// 普通 不执行优先资金操作
        /// 配资 执行优先资金操作
        /// </summary>
        public EnumBusinessType BusinessType { get; set; }


        /// <summary>
        /// 分区编号
        /// </summary>
        public int Domain_ID { get; set; }

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

        public const string QRY_CASH_OPERATION = "QryCashOperation";

        public const string REJECT_CASH_OPERATION = "RejectCashOperation";

        public const string CANCEL_CASH_OPERATION = "CancelCashOperation";

        public const string CONFIRM_CASH_OPERATION = "ConfirmCashOperation";

        public const string NOTIFY_CASH_OPERATION = "NotifyCashOperation";
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

        /// <summary>
        /// 查询一个时间段内的出入金请求记录
        /// </summary>
        /// <param name="client"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int ReqQryCashOperation(this TLClientNet client,int start,int end)
        {
            return client.ReqContribRequest(Modules.APIService, Method_API.QRY_CASH_OPERATION, new { start = start, end = end });
        }

        /// <summary>
        /// 拒绝出入金请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="transid"></param>
        /// <returns></returns>
        public static int ReqRejectCashOperation(this TLClientNet client, string transid)
        {
            return client.ReqContribRequest(Modules.APIService, Method_API.REJECT_CASH_OPERATION, transid);
        }

        /// <summary>
        /// 取消出入金操作
        /// </summary>
        /// <param name="client"></param>
        /// <param name="transid"></param>
        /// <returns></returns>
        public static int ReqCancelCashOperation(this TLClientNet client, string transid)
        {
            return client.ReqContribRequest(Modules.APIService, Method_API.CANCEL_CASH_OPERATION, transid);
        }

        /// <summary>
        /// 确认出入金操作
        /// </summary>
        /// <param name="client"></param>
        /// <param name="transid"></param>
        /// <returns></returns>
        public static int ReqConfirmCashOperation(this TLClientNet client, string transid)
        {
            return client.ReqContribRequest(Modules.APIService, Method_API.CONFIRM_CASH_OPERATION, transid);
        }

    }
}
