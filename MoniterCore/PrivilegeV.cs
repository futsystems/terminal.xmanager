using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;



namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 权限检查
    /// </summary>
    public static class PrivilegeChecker
    {

        #region 系统

        /// <summary>
        /// 默认行情与成交服务权限
        /// </summary>
        /// <returns></returns>
        public static bool RightAccessDefaultConnector()
        {
            if ((CoreService.SiteInfo.Domain.Super || CoreService.SiteInfo.Domain.Dedicated) && CoreService.SiteInfo.Manager.IsRoot())
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}