using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public static class Client_ConnectorManager
    {
        /// <summary>
        /// 查询接口设置
        /// </summary>
        public static int ReqQryInterface(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_INTERFACE, "");
        }

        /// <summary>
        /// 更新接口设置
        /// </summary>
        /// <param name="json"></param>
        public static int ReqUpdateInterface(this TLClientNet client, ConnectorInterface intfac)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, "UpdateInterface", intfac);
        }


        /// <summary>
        /// 查询通道是否可用
        /// </summary>
        /// <param name="token"></param>
        public static int ReqQryTokenValid(this TLClientNet client, string token)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_TOKEN_VALID, token);
        }

        public static int ReqQryConnectorNotInGroup(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_CONN_NOTIN_GROUP, "");
        }

        /// <summary>
        /// 查询所有通道设置
        /// </summary>
        public static int ReqQryConnectorConfig(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_CONN_CONFIG, "");
        }

        /// <summary>
        /// 查询默认通道
        /// </summary>
        public static int ReqQryDefaultConnectorConfig(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_DEFAULT_CONN_CONFIG, "");
        }

        /// <summary>
        /// 查询所有通道状态
        /// </summary>
        public static int ReqQryConnectorStatus(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_CONN_STATUS, "");
        }

        public static int ReqQryDefaultConnectorStatus(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_DEFAULT_CONN_STATUS, "");
        }

        /// <summary>
        /// 更新接口设置 包含新增与更新
        /// </summary>
        public static int ReqUpdateConnectorConfig(this TLClientNet client, ConnectorConfig cfg)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.UPDATE_CONN_CONFIG, cfg);
        }


        /// <summary>
        /// 请求启动通道
        /// </summary>
        /// <param name="id"></param>
        public static int ReqStartConnector(this TLClientNet client, int id)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.CONN_START, id.ToString());
        }

        /// <summary>
        /// 请求停止通道
        /// </summary>
        /// <param name="id"></param>
        public static int ReqStopConnector(this TLClientNet client, int id)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.CONN_STOP, id.ToString());
        }


        /// <summary>
        /// 更新接口设置
        /// </summary>
        /// <param name="json"></param>
        public static int ReqQryRouterGroup(this TLClientNet client)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_ROUTEGROUP, "");
        }



        /// <summary>
        /// 查询某个路由组的路由项目
        /// </summary>
        public static int ReqQryRouterItem(this TLClientNet client, int rgid)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_ROUTEITEM, rgid.ToString());
        }



        /// <summary>
        /// 更新路由项目
        /// </summary>
        /// <param name="item"></param>
        public static int ReqUpdateRouterItem(this TLClientNet client, RouterItemSetting item)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.UPDATE_ROUTEITEM, item);
        }

        /// <summary>
        /// 更新路由组
        /// </summary>
        /// <param name="group"></param>
        public static int ReqUpdateRouterGroup(this TLClientNet client, RouterGroupSetting group)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.UPDATE_ROUTEGROUP, group);
        }
    }
}
