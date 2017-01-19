using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public class Method_CONN_MGR
    {
        /// <summary>
        /// 查询通道接口
        /// </summary>
        public const string QRY_INTERFACE = "QryInterface";

        /// <summary>
        /// 查询通道编号是否可用
        /// </summary>
        public const string QRY_TOKEN_VALID = "QryTokenValid";

        /// <summary>
        /// 启动通道
        /// </summary>
        public const string CONN_START = "StartConnector";

        /// <summary>
        /// 停止通道
        /// </summary>
        public const string CONN_STOP = "StopConnector";

        /// <summary>
        /// 查询通道状态
        /// </summary>
        public const string QRY_CONN_STATUS = "QryConnectorStatus";

        /// <summary>
        /// 查询默认通道状态
        /// </summary>
        public const string QRY_DEFAULT_CONN_STATUS = "QryDefaultConnectorStatus";

        /// <summary>
        /// 查询通道设置
        /// </summary>
        public const string QRY_CONN_CONFIG = "QryConnectorConfig";

        /// <summary>
        /// 查询默认通道设置
        /// </summary>
        public const string QRY_DEFAULT_CONN_CONFIG = "QryDefaultConnectorConfig";

        /// <summary>
        /// 更新通道设置
        /// </summary>
        public const string UPDATE_CONN_CONFIG = "UpdateConnectorConfig";

        /// <summary>
        /// 通道状态通知
        /// </summary>
        public const string NOTIFY_CONN_STATUS = "NotifyConnectorStatus";

        /// <summary>
        /// 通道设置通知
        /// </summary>
        public const string NOTIFY_CONN_CONFIG = "NotifyConnectorCfg";

        /// <summary>
        /// 查询路由组
        /// </summary>
        public const string QRY_ROUTEGROUP = "QryRouterGroup";

        /// <summary>
        /// 更新路由组
        /// </summary>
        public const string UPDATE_ROUTEGROUP = "UpdateRouterGroup";

        /// <summary>
        /// 路由组更新通知
        /// </summary>
        public const string NOTIFY_ROUTEGROUP = "NotifyRouterGroup";

        /// <summary>
        /// 查询路由项
        /// </summary>
        public const string QRY_ROUTEITEM = "QryRouterItem";

        /// <summary>
        /// 更新路由项
        /// </summary>
        public const string UPDATE_ROUTEITEM = "UpdateRouterItem";

        /// <summary>
        /// 路由项更新通知
        /// </summary>
        public const string NOTIFY_ROUTEITEM = "NotifyRouterItem";

        /// <summary>
        /// 查询未绑定通道
        /// </summary>
        public const string QRY_CONN_NOTIN_GROUP = "QryConnectorNotInGroup";


    }

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
        /// 更新接口设置 包含新增与更新
        /// </summary>
        public static int ReqUpdateConnectorConfig(this TLClientNet client, ConnectorConfig cfg)
        {
            return client.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.UPDATE_CONN_CONFIG, cfg);
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
