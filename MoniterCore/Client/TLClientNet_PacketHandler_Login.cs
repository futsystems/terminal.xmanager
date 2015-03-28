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
    }
}
