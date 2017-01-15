using System;
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
        /// <summary>
        /// 请求登入
        /// </summary>
        /// <param name="loginid"></param>
        /// <param name="pass"></param>
        public void ReqLogin(string loginid, string pass)
        {
            logger.Info(string.Format("请求登入,{0} {1}", loginid, pass));
            LoginRequest request = RequestTemplate<LoginRequest>.CliSendRequest(++requestid);
            request.LoginID = loginid;
            request.Passwd = pass;
            
            SendPacket(request);
            Func<LocationInfo> del = new Func<LocationInfo>(Util.GetLocationInfo);
            del.BeginInvoke(QryLocaltionInfoCallback, null);
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
            OrderInsertRequest request = RequestTemplate<OrderInsertRequest>.CliSendRequest(++requestid);
            request.Order = order;
            SendPacket(request);
            return requestid;
        }

        /// <summary>
        /// 提交委托操作
        /// </summary>
        /// <param name="action"></param>
        public void ReqOrderAction(OrderAction action)
        {
            OrderActionRequest requets = RequestTemplate<OrderActionRequest>.CliSendRequest(++requestid);
            requets.OrderAction = action;

            SendPacket(requets);
        }

        public void ReqInsertTrade(Trade f)
        {
            logger.Info("请求插入成交");
            MGRReqInsertTradeRequest request = RequestTemplate<MGRReqInsertTradeRequest>.CliSendRequest(++requestid);
            request.TradeToSend = f;
            SendPacket(request);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="jobj"></param>
        public void ReqContribRequest(string module, string cmd, object jobj)
        {
            this.ReqContribRequest(module, cmd, jobj.SerializeObject());
        }


        /// <summary>
        /// 调用某个模块 某个命令 某个参数 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="args"></param>
        //public void ReqContribCommaRequest(string module, string cmd, string arg)
        //{
        //    logger.Info(string.Format("ContribRequest Module:{0} Cmd:{1} Args:{2}", module, cmd, arg));
        //    MGRContribRequest request = RequestTemplate<MGRContribRequest>.CliSendRequest(++requestid);
        //    request.ModuleID = module;
        //    request.CMDStr = cmd;
        //    request.Parameters = arg;
        //    SendPacket(request);
        //}

        public void ReqContribRequest(string module, string cmd,string arg,bool str2json = false)
        {
            logger.Info(string.Format("ContribRequest Module:{0} Cmd:{1} Args:{2}", module, cmd, arg));
            MGRContribRequest request = RequestTemplate<MGRContribRequest>.CliSendRequest(++requestid);
            request.ModuleID = module;
            request.CMDStr = cmd;
            request.Parameters = str2json ? arg.SerializeObject() : arg;
            SendPacket(request);
        }


        

        
    }
}
