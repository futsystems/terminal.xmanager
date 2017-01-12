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
        /// 查询接口设置
        /// </summary>
        public void ReqQryInterface()
        {
            logger.Info("查询所有接口设置");
            this.ReqContribRequest(Modules.CONN_MGR,Method_CONN_MGR.QRY_INTERFACE, "");
        }

        /// <summary>
        /// 更新接口设置
        /// </summary>
        /// <param name="json"></param>
        public void ReqUpdateInterface(string json)
        {
            logger.Info("请求更新或添加接口");
            this.ReqContribRequest(Modules.CONN_MGR, "UpdateInterface", json);
        }


        /// <summary>
        /// 查询通道是否可用
        /// </summary>
        /// <param name="token"></param>
        public void ReqQryTokenValid(string token)
        {
            logger.Info("查询某个通道ID是否可用");
            this.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_TOKEN_VALID, token);
        }

        public void ReqQryConnectorNotInGroup()
        {
            this.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_CONN_NOTIN_GROUP, "");
        }

        /// <summary>
        /// 查询所有通道设置
        /// </summary>
        public void ReqQryConnectorConfig()
        {
            logger.Info("查询所有通道设置");
            this.ReqContribRequest(Modules.CONN_MGR,Method_CONN_MGR.QRY_CONN_CONFIG, "");
        }

        /// <summary>
        /// 查询默认通道
        /// </summary>
        public void ReqQryDefaultConnectorConfig()
        {
            logger.Info("查询默认通道");
            this.ReqContribRequest(Modules.CONN_MGR,Method_CONN_MGR.QRY_DEFAULT_CONN_CONFIG, "");
        }

        /// <summary>
        /// 查询所有通道状态
        /// </summary>
        public void ReqQryConnectorStatus()
        {
            logger.Info("查询所有通道状态");
            this.ReqContribRequest(Modules.CONN_MGR,Method_CONN_MGR.QRY_CONN_STATUS, "");
        }

        public void ReqQryDefaultConnectorStatus()
        {
            logger.Info("查询默认通道状态");
            this.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_DEFAULT_CONN_STATUS, "");
        }

        /// <summary>
        /// 更新接口设置 包含新增与更新
        /// </summary>
        public void ReqUpdateConnectorConfig(string json)
        {
            logger.Info("请求添加或更新通道设置");
            this.ReqContribRequest(Modules.CONN_MGR,Method_CONN_MGR.UPDATE_CONN_CONFIG, json);
        }
        

        /// <summary>
        /// 请求启动通道
        /// </summary>
        /// <param name="id"></param>
        public void ReqStartConnector(int id)
        {
            logger.Info("请求启动某个通道");
            this.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.CONN_START, id.ToString());
        }

        /// <summary>
        /// 请求停止通道
        /// </summary>
        /// <param name="id"></param>
        public void ReqStopConnector(int id)
        {
            logger.Info("请求停止某个通道");
            this.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.CONN_STOP, id.ToString());
        }


        /// <summary>
        /// 更新接口设置
        /// </summary>
        /// <param name="json"></param>
        public void ReqQryRouterGroup()
        {
            logger.Info("请求查询路由组");
            this.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_ROUTEGROUP, "");
        }



        /// <summary>
        /// 查询某个路由组的路由项目
        /// </summary>
        public void ReqQryRouterItem(int rgid)
        {
            logger.Info("请求查询某路由组内路由项");
            this.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.QRY_ROUTEITEM, rgid.ToString());
        }

        

        /// <summary>
        /// 更新路由项目
        /// </summary>
        /// <param name="item"></param>
        public void ReqUpdateRouterItem(RouterItemSetting item)
        {
            logger.Info("请求更新或添加路由项目");
            this.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.UPDATE_ROUTEITEM, item.SerializeObject());
        }

        /// <summary>
        /// 更新路由组
        /// </summary>
        /// <param name="group"></param>
        public void ReqUpdateRouterGroup(RouterGroupSetting group)
        {
            logger.Info("请求更新或添加路由组");
            this.ReqContribRequest(Modules.CONN_MGR, Method_CONN_MGR.UPDATE_ROUTEGROUP, group.SerializeObject());
        }

    }
}
