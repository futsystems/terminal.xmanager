﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using System.Runtime.Remoting.Messaging;


namespace TradingLib.MoniterCore
{
    public partial class TLClientNet
    {

        string _user = string.Empty;
        string _pass = string.Empty;
        /// <summary>
        /// 请求登入
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="pass"></param>
        public int ReqLogin(string loginid, string pass)
        {
            logger.Info(string.Format("request login user:{0} pass:{1}", loginid, pass));
            int reqid = NextRequestID;
            LoginRequest request = RequestTemplate<LoginRequest>.CliSendRequest(reqid);
            request.LoginID = loginid;
            request.Passwd = pass;
            _user = loginid;
            _pass = pass;

            SendPacket(request);
            Func<LocationInfo> del = new Func<LocationInfo>(Util.GetLocationInfo);
            del.BeginInvoke(QryLocaltionInfoCallback, null);
            return reqid;
        }

        void QryLocaltionInfoCallback(IAsyncResult async)
        {
            Func<LocationInfo> proc = ((AsyncResult)async).AsyncDelegate as Func<LocationInfo>;
            LocationInfo info = proc.EndInvoke(async);
            //InvokeGotGLocation(location);
            UpdateLocationInfoRequest request = RequestTemplate<UpdateLocationInfoRequest>.CliSendRequest(++requestid);
            request.LocationInfo = info;
            SendPacket(request);
        }

        public void ReqOpenClearCentre()
        {
            logger.Info("请求开启交易中心");
            this.ReqContribRequest("ClearCentre", "OpenClearCentre","");
        }

        public void ReqCloseCentre()
        {
            logger.Info("请求关闭清算中心");
            this.ReqContribRequest("ClearCentre", "CloseClearCentre", "");
        }


        /// <summary>
        /// 发送委托
        /// </summary>
        /// <param name="order"></param>
        public int ReqOrderInsert(Order order)
        {
            int reqid = NextRequestID;
            OrderInsertRequest request = RequestTemplate<OrderInsertRequest>.CliSendRequest(reqid);
            request.Order = order;
            SendPacket(request);
            return reqid;
            
        }

        /// <summary>
        /// 提交委托操作
        /// </summary>
        /// <param name="action"></param>
        public int ReqOrderAction(OrderAction action)
        {
            int reqid = NextRequestID;
            OrderActionRequest requets = RequestTemplate<OrderActionRequest>.CliSendRequest(reqid);
            requets.OrderAction = action;
            SendPacket(requets);
            return reqid;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="jobj"></param>
        public int ReqContribRequest(string module, string cmd, object jobj)
        {
            return this.ReqContribRequest(module, cmd, jobj.SerializeObject());
        }

        public int ReqContribRequest(string module, string cmd,string arg,bool str2json = false)
        {
            logger.Info(string.Format("ContribRequest Module:{0} Cmd:{1} Args:{2}", module, cmd, arg));
            int reqid = NextRequestID;
            MGRContribRequest request = RequestTemplate<MGRContribRequest>.CliSendRequest(reqid);
            request.ModuleID = module;
            request.CMDStr = cmd;
            request.Parameters = str2json ? arg.SerializeObject() : arg;
            SendPacket(request);
            return reqid;
        }


        

        
    }
}
