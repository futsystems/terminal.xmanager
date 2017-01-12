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
        /// 激活管理员
        /// </summary>
        /// <param name="mgrid"></param>
        public void ReqActiveManger(int mgrid)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MANAGER_ACTIVE, mgrid.ToString());
        }

        /// <summary>
        /// 冻结管理员
        /// </summary>
        /// <param name="mgrid"></param>
        public void ReqInactiveManger(int mgrid)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MANAGER_INACTIVE, mgrid.ToString());
        }


        public void ReqDelManager(int mgrid)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.DEL_MANAGER, mgrid.ToString());
        }

        /// <summary>
        /// 修改管理员密码
        /// </summary>
        /// <param name="oldpass"></param>
        /// <param name="pass"></param>
        public void ReqUpdatePass(string oldpass, string pass)
        {
            this.ReqContribRequest(Modules.MGR_EXCH,Method_MGR_EXCH.UPDATE_MANAGER_PASS, oldpass + "," + pass); ;
        }

        /// <summary>
        /// 查询分区管理员登入信息
        /// </summary>
        /// <param name="domainid"></param>
        public void ReqQryManagerLoginInfo(int mgrid)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MANAGER_LOGININFO, mgrid.ToString());
        }


        /// <summary>
        /// 查询管理员
        /// </summary>
        public void ReqQryManager()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MANAGER, "");
        }

        /// <summary>
        /// 更新管理员
        /// </summary>
        /// <param name="mgr"></param>
        public void ReqUpdateManager(ManagerSetting mgr)
        {
            this.ReqContribRequest(Modules.MGR_EXCH,Method_MGR_EXCH.UPDATE_MANAGER, mgr.SerializeObject());
        }
    }
}
