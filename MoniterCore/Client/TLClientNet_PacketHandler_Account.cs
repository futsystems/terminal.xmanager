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
        /// 查询帐户列表回报
        /// </summary>
        /// <param name="response"></param>
        void CliOnMGRQryAccountList(RspMGRQryAccountResponse response)
        {
            CoreService.BasicInfoTracker.OnMGRQryAccountList(response.AccountItem, response.IsLast);
           
        }

        /// <summary>
        /// 帐户参数更新通知
        /// </summary>
        /// <param name="notify"></param>
        void CliOnMGRAccountUpdate(NotifyMGRAccountChangeUpdateResponse notify)
        {
            CoreService.BasicInfoTracker.OnMGRAccountUpdate(notify.AccountItem);
        }


        /// <summary>
        /// 交易帐号财务信息更新
        /// </summary>
        /// <param name="response"></param>
        void CliOnNotifyMGRAccountStatistic(NotifyMGRAccountStatistic response)
        {
            CoreService.EventAccount.FireInfoLiteEvent(response.Statistic);
        }

        /// <summary>
        /// 恢复交易记录回报
        /// </summary>
        /// <param name="response"></param>
        void CliOnMGRResumeAccountResponse(RspMGRResumeAccountResponse response)
        {
            //logger.Info("got resume account response:" + response.ToString());
            CoreService.EventAccount.FireAccountResumeEvent(response);
        }

        ///// <summary>
        ///// 交易帐户登入 退出更新
        ///// </summary>
        ///// <param name="notify"></param>
        //void CliOnMGRSesssionUpdate(NotifyMGRSessionUpdateNotify notify)
        //{
        //    //logger.Info("got session update notify:" + notify.ToString());
        //    CoreService.EventAccount.FireSessionUpdateEvent(notify);
        //}

        /// <summary>
        /// 查询交易帐户信息
        /// </summary>
        /// <param name="response"></param>
        //void CliOnMGRQryAccountInfo(RspMGRQryAccountInfoResponse response)
        //{
        //    //logger.Info("got mgr account info response:" + response.ToString());
        //    //this.handler.OnAccountInfo(response.AccountInfoToSend);
        //}


    }
}
