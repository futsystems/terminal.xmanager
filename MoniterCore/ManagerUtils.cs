using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public static class ManagerUtils
    {
        public static int GetBaseMGR(this ManagerSetting mgr)
        {
            return mgr.mgr_fk;
        }

        /// <summary>
        /// 判断当前管理员是否是超级管理员
        /// </summary>
        /// <returns></returns>
        public static bool RightRootDomain(this ManagerSetting mgr)
        {
            return mgr.Type == QSEnumManagerType.ROOT;
        }

        /// <summary>
        /// 是否有权限添加管理员
        /// </summary>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public static bool RightAddManager(this ManagerSetting mgr)
        {
            if (mgr == null)
                return false;
            if (mgr.Type == QSEnumManagerType.ROOT)
                return true;
            if (mgr.Type == QSEnumManagerType.AGENT)
                return true;
            return false;
        }
        /// <summary>
        /// 是否是管理员
        /// </summary>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public static bool IsRoot(this ManagerSetting mgr)
        {
            return mgr.Type == QSEnumManagerType.ROOT;
        }

        public static bool IsAgent(this ManagerSetting mgr)
        {
            return mgr.Type == QSEnumManagerType.AGENT;
        }

        public static bool IsAgentDomain(this ManagerSetting mgr)
        {
            return mgr.Type == QSEnumManagerType.AGENT;//'|| mgr.BaseManager.Type == QSEnumManagerType.AGENT;
        }
    }
}
