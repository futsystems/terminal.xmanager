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
        /// 查询所有权限模板
        /// </summary>
        public void ReqQryPermmissionTemplateList()
        {
            logger.Info("请求查询权限模板列表");

            this.ReqContribRequest("MgrExchServer", "QueryPermmissionTemplate", "");
        }


        /// <summary>
        /// 更新某个权限模板
        /// </summary>
        /// <param name="jsonstr"></param>
        public void ReqUpdatePermissionTemplate(string jsonstr)
        {
            logger.Info("请求更新权限模板");

            this.ReqContribRequest("MgrExchServer", "UpdatePermission", jsonstr);
        }

        /// <summary>
        /// 查询某个管理员的代理权限
        /// </summary>
        /// <param name="managerid"></param>
        public void ReqQryAgentPermission(int managerid)
        {
            this.ReqContribRequest("MgrExchServer", "QueryAgentPermission", managerid.ToString());
        }

        /// <summary>
        /// 更新某个管理员的权限设置
        /// </summary>
        /// <param name="managerid"></param>
        /// <param name="accessid"></param>
        public void ReqUpdateAgentPermission(int managerid, int accessid)
        {
            this.ReqContribRequest("MgrExchServer", "UpdateAgentPermission", managerid.ToString() + "," + accessid.ToString());
        }
    }
}
