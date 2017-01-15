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
        //void CliOnMGRQryAccountList(RspMGRQryAccountResponse response)
        //{
        //    CoreService.BasicInfoTracker.OnMGRQryAccountList(response.AccountItem, response.IsLast);
        //}

        /// <summary>
        /// 帐户参数更新通知
        /// </summary>
        /// <param name="notify"></param>
        //void CliOnMGRAccountUpdate(NotifyMGRAccountChangeUpdateResponse notify)
        //{
        //    CoreService.BasicInfoTracker.OnMGRAccountUpdate(notify.AccountItem);
        //}


        /// <summary>
        /// 交易帐号财务信息更新
        /// </summary>
        /// <param name="response"></param>
        //void CliOnNotifyMGRAccountStatistic(NotifyMGRAccountStatistic response)
        //{
        //    CoreService.EventHub.FireInfoLiteEvent(response.Statistic);
        //}

        /// <summary>
        /// 恢复交易记录回报
        /// 服务端单个交易账户记录恢复过程
        /// 1.发送BEGIN
        /// 2.发送交易记录
        /// 3.发送END记录
        /// 发送消息是以通知形式在单个请求处理中进行发送 可以避免请求延迟导致实时通知与数据恢复之间造成异步无法正常衔接
        /// 执行账户数据恢复后 本地交易记录维护期清空并准备接受服务端恢复过来的交易记录
        /// </summary>
        /// <param name="response"></param>
        //void CliOnMGRResumeAccountResponse(RspMGRResumeAccountResponse response)
        //{
        //    if (response.ResumeStatus == QSEnumResumeStatus.BEGIN)
        //    {
        //        CoreService.EventHub.FireResumeDataStart();
        //    }
        //    else
        //    {
        //        CoreService.EventHub.FireResumeDataEnd();
        //    }
        //}
    }
}
