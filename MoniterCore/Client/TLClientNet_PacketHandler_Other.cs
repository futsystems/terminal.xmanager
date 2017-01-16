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
        bool _firstlogin = true;
        /// <summary>
        /// 登入回报
        /// </summary>
        /// <param name="response"></param>
        void CliOnRspMGRLoginResponse(RspMGRLoginResponse response)
        {
            logger.Info("got login response:" + response.ToString());

            CoreService.EventCore.FireLoginEvent(response);
            //第一次登入成功 请求基础数据
            if (response.RspInfo.ErrorID == 0 && _firstlogin)
            {
                _firstlogin = false;
                //查询
                CoreService.SiteInfo.LoginResponse = response;
                this.ReqQryMarketTime();
            }
        }

        void CliOnMGRContribResponse(RspMGRContribResponse response)
        {

            string module = response.ModuleID;
            string cmd = response.CMDStr;
            string ret = response.Result;
            logger.Debug("ContribResponse ->Module:" + module + " CMD:" + cmd + " Ret:" + ret);
            CoreService.EventCore.GotMGRContribResponse(module, cmd, ret, response.IsLast);
        }

        void CliOnMGRContribNotify(NotifyMGRContribNotify notify)
        {
            string module = notify.ModuleID;
            string cmd = notify.CMDStr;
            string ret = notify.Result;

            logger.Debug("ContribNotify ->Module:" + module + " CMD:" + cmd + " Ret:" + ret);
            CoreService.EventCore.GotMGRContribNotifyResponse(module, cmd, ret);
        }

        void CliOnRspMGRResponse(RspMGRResponse response)
        {
            logger.Info("got operation response:" + response.ToString());
            CoreService.EventCore.FireRspInfoEvent(response.RspInfo);
        }
    }
}
