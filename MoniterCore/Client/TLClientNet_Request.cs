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
            //MGRLoginRequest request = RequestTemplate<MGRLoginRequest>.CliSendRequest(++requestid);
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

        #region 交易类操作
        /// <summary>
        /// 发送委托
        /// </summary>
        /// <param name="order"></param>
        public void ReqOrderInsert(Order order)
        {
            OrderInsertRequest request = RequestTemplate<OrderInsertRequest>.CliSendRequest(++requestid);
            request.Order = order;

            SendPacket(request);
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

        #endregion





        #region 扩展请求







        #region 查询报表

        /// <summary>
        /// 查询某日所有代理的利润报表
        /// </summary>
        /// <param name="settleday"></param>
        public void ReqQryTotalReport(int agentfk,int settleday)
        {
            this.ReqContribRequest("FinServiceCentre", "QryTotalReport", agentfk.ToString()+","+settleday.ToString());
        }

        /// <summary>
        /// 查询某个代理的在一个时间段内的汇总
        /// </summary>
        /// <param name="settleday"></param>
        public void ReqQrySummaryReport(int agentfk, int start, int end)
        {
            this.ReqContribRequest("FinServiceCentre", "QrySummaryReport", agentfk.ToString() + "," + start.ToString() + "," + end.ToString());
        }


        /// <summary>
        /// 查询某个代理某个时间段内的所有利润流水
        /// </summary>
        /// <param name="agentfk"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void ReqQryTotalReportByDayRange(int agentfk, int start, int end)
        {
            this.ReqContribRequest("FinServiceCentre", "QryTotalReportDayRange", agentfk.ToString() + "," + start.ToString() + "," + end.ToString());
        }

        /// <summary>
        /// 查询某个代理某个交易日的按帐户汇总的利润报表
        /// </summary>
        /// <param name="agentfk"></param>
        /// <param name="settleday"></param>
        public void ReqQryDetailReportByAccount(int agentfk, int settleday)
        {
            this.ReqContribRequest("FinServiceCentre", "QryDetailReportByAccount", agentfk.ToString() + "," + settleday.ToString());
        }
        #endregion

        
        /// <summary>
        /// 调用某个模块 某个命令 某个参数 
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd"></param>
        /// <param name="args"></param>
        public void ReqContribRequest(string module, string cmd, string args)
        {
            logger.Info("请求扩展命令,module:" + module + " cmd:" + cmd + " args:" + args);
            MGRContribRequest request = RequestTemplate<MGRContribRequest>.CliSendRequest(++requestid);
            request.ModuleID = module;
            request.CMDStr = cmd;
            request.Parameters = args;

            SendPacket(request);
        
        }

        public void ReqContribRequest(string module, string cmd,object jobj)
        {

            string args = TradingLib.Mixins.Json.JsonMapper.ToJson(jobj);
            logger.Info("请求扩展命令,module:" + module + " cmd:" + cmd + " args:" + args);
            this.ReqContribRequest(module, cmd, args);
        }

        

        #endregion

        #region 插入成交
        public void ReqInsertTrade(Trade f)
        {
            logger.Info("请求插入成交");
            MGRReqInsertTradeRequest request = RequestTemplate<MGRReqInsertTradeRequest>.CliSendRequest(++requestid);
            request.TradeToSend = f;
            SendPacket(request);

        }
        #endregion
    }
}
