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
        void CliOnMGRContribResponse(RspMGRContribResponse response)
        {

            string module = response.ModuleID;
            string cmd = response.CMDStr;
            string ret = response.Result;
            logger.Debug("ContribResponse ->Module:" + module + " CMD:" + cmd + " Ret:" + ret);
            CoreService.EventContrib.GotMGRContribResponse(module, cmd, ret, response.IsLast);
        }
        void CliOnMGRContribNotify(NotifyMGRContribNotify notify)
        {
            string module = notify.ModuleID;
            string cmd = notify.CMDStr;
            string ret = notify.Result;

            logger.Debug("ContribNotify ->Module:" + module + " CMD:" + cmd + " Ret:" + ret);
            CoreService.EventContrib.GotMGRContribNotifyResponse(module, cmd, ret);
        }
    }
}
