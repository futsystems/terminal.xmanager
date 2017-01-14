using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public class EventHub
    {

        ///// <summary>
        ///// 动态财务信息
        ///// </summary>
        //public event Action<AccountStatistic> AccountStatisticNotify;

        //internal void FireInfoLiteEvent(AccountStatistic notify)
        //{
        //    if (AccountStatisticNotify != null)
        //        AccountStatisticNotify(notify);
        //}

        ///// <summary>
        ///// 交易帐户变化事件
        ///// </summary>
        //public event Action<AccountItem> OnAccountChangedEvent;

        //internal void FireAccountChangedEvent(AccountItem account)
        //{
        //    if (OnAccountChangedEvent != null)
        //        OnAccountChangedEvent(account);
        //}


        /// <summary>
        /// 交易记录开始恢复
        /// </summary>
        public event Action OnResumeDataStart;
        internal void FireResumeDataStart()
        {
            if (OnResumeDataStart != null)
            {
                OnResumeDataStart();
            }
        }

        /// <summary>
        /// 交易记录恢复完成
        /// </summary>
        public event Action OnResumeDataEnd;
        internal void FireResumeDataEnd()
        {
            if (OnResumeDataEnd != null)
            {
                OnResumeDataEnd();
            }
        }


    }
}
