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
        /// 注销交易帐户的登入客户端
        /// </summary>
        public void ReqClearTerminals(string account)
        {
            logger.Info("注销登入客户端");
            this.ReqContribRequest("MsgExch", "ClearAccountTerminals", account);
        }
    }
}