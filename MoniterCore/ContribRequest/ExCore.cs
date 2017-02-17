using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public class Method_MSG_EXCH
    {
        /// <summary>
        /// 查询交易端会话信息
        /// </summary>
        public const string QRY_SESSION_INFO = "QrySessionInfo";

        /// <summary>
        /// 断开某个交易账户所有连接终端
        /// </summary>
        public const string REQ_CLOSE_TERMINALS = "ClearAccountTerminals";


        /// <summary>
        /// 关闭某个会话
        /// </summary>
        public const string KILL_SESSION = "KillSession";

    }

    public static class ExCore
    {

        /// <summary>
        /// 查询交易账户回话信息
        /// </summary>
        /// <param name="account"></param>
        public static int ReqQrySessionInfo(this TLClientNet client, string account)
        {
            return client.ReqContribRequest(Modules.MSG_EXCH, Method_MSG_EXCH.QRY_SESSION_INFO, account);
        }

        /// <summary>
        /// 注销交易帐户的登入客户端
        /// </summary>
        public static int ReqClearTerminals(this TLClientNet client, string account)
        {
            return client.ReqContribRequest(Modules.MSG_EXCH, Method_MSG_EXCH.REQ_CLOSE_TERMINALS, account);
        }

        /// <summary>
        /// 关闭一个会话连接
        /// </summary>
        /// <param name="client"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public static int ReqKillSession(this TLClientNet client, string clientID)
        {
            return client.ReqContribRequest(Modules.MSG_EXCH, Method_MSG_EXCH.KILL_SESSION, clientID);
        }
    }
}
