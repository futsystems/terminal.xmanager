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
        /// 查询交易帐户对应的支付信息
        /// </summary>
        /// <param name="account"></param>
        public void ReqQryAccountPaymentInfo(string account)
        {
            this.ReqContribRequest("MgrExchServer", "QryAccountPaymentInfo", account);
        }

        /// <summary>
        /// 查询所有交易帐户出入金操作
        /// </summary>
        public void ReqQryAccountCashopOperationTotal()
        {
            this.ReqContribRequest("MgrExchServer", "QryAccountCashOperationTotal", "");
        }

        
        /// <summary>
        /// 请求出入金操作
        /// </summary>
        /// <param name="playload"></param>
        public void ReqRequestAccountCashOperation(string playload)
        {
            this.ReqContribRequest("MgrExchServer", "RequestAccountCashOperation", playload);
        }

        /// <summary>
        /// 确认出入金操作
        /// </summary>
        /// <param name="playload"></param>
        public void ReqConfirmAccountCashOperation(string playload)
        {
            this.ReqContribRequest("MgrExchServer", "ConfirmAccountCashOperation", playload);
        }

        /// <summary>
        /// 取消出入金操作
        /// </summary>
        /// <param name="playload"></param>
        public void ReqCancelAccountCashOperation(string playload)
        {
            this.ReqContribRequest("MgrExchServer", "CancelAccountCashOperation", playload);
        }

        /// <summary>
        /// 拒绝出入金操作
        /// </summary>
        /// <param name="playload"></param>
        public void ReqRejectAccountCashOperation(string playload)
        {
            this.ReqContribRequest("MgrExchServer", "RejectAccountCashOperation", playload);
        }
    }
}
