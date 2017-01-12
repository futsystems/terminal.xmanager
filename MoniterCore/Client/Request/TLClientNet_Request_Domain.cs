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
        /// 查询域
        /// </summary>
        public void ReqQryDomain()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_DOMAIN, "");
        }

        /// <summary>
        /// 查询分区管理员登入信息
        /// </summary>
        /// <param name="domainid"></param>
        public void ReqQryDomainRootLoginInfo(int domainid)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_DOMAIN_LOGININFO, domainid.ToString());
        }

        /// <summary>
        /// 更新域
        /// </summary>
        /// <param name="domain"></param>
        public void ReqUpdateDomain(DomainImpl domain)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_DOMAIN, domain);
        }

      
    }
}
