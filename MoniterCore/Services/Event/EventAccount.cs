using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public class EventAccount
    {

        /// <summary>
        /// 动态财务信息
        /// </summary>
        public event Action<AccountStatistic> AccountStatisticNotify;

        internal void FireInfoLiteEvent(AccountStatistic notify)
        {
            if (AccountStatisticNotify != null)
                AccountStatisticNotify(notify);
        }

        /// <summary>
        /// 交易帐户变化事件
        /// </summary>
        public event Action<AccountItem> OnAccountChangedEvent;

        internal void FireAccountChangedEvent(AccountItem account)
        {
            LogService.Debug("FireAccountChangedEvent");
            if (OnAccountChangedEvent != null)
                OnAccountChangedEvent(account);
        }

        ///// <summary>
        ///// 获得恢复交易帐号事件
        ///// </summary>
        //public event Action<RspMGRResumeAccountResponse> OnAccountResumeEvent;

        //internal void FireAccountResumeEvent(RspMGRResumeAccountResponse response)
        //{
        //    LogService.Debug("FireAccountResumeEvent");
        //    if (OnAccountResumeEvent != null)
        //        OnAccountResumeEvent(response);
        //}
    }
}
