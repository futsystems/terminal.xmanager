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
        /// 查询交易帐户列表
        /// </summary>
        public void ReqQryAccountList()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_ACC_LIST, "");
        }

        /// <summary>
        /// 设定观察帐户列表
        /// </summary>
        /// <param name="list"></param>
        public void ReqWatchAccount(List<string> list)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.WATCH_ACC_LIST, list.ToArray());
        }

        /// <summary>
        /// 恢复某个交易帐号的日内交易数据
        /// </summary>
        /// <param name="account"></param>
        public void ReqResumeAccount(string account)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.RESUME_ACC, new { account = account });
        }



      


        

        /// <summary>
        /// 修改交易帐户路由组
        /// </summary>
        /// <param name="account"></param>
        /// <param name="rgid"></param>
        public void ReqUpdateRouterGroup(string account,int rgid)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.UPDATE_ACC_ROUTEGROUP, account + "," + rgid.ToString());
        }

        /// <summary>
        /// 查询交易账户回话信息
        /// </summary>
        /// <param name="account"></param>
        public void ReqQrySessionInfo(string account)
        {
            this.ReqContribRequest(Modules.MSG_EXCH, Method_MSG_EXCH.QRY_SESSION_INFO, account);
        }
    }
}
