﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public class EventAccount
    {
        /// <summary>
        /// 交易帐户选择事件
        /// 双击某个交易帐户 出发该事件 用于通知所有监听该事件的对象
        /// </summary>
        public event Action<AccountItem> OnAccountSelectedEvent;

        public void FireAccountSelectedEvent(AccountItem account)
        {
            LogService.Debug("FireAccountSelectedEvent");
            if (OnAccountSelectedEvent != null)
                OnAccountSelectedEvent(account);
        }


        /// <summary>
        /// 交易帐户同步事件
        /// </summary>
        public event Action<AccountItem> OnSyncAccountEvent;

        public void FireSyncAccountEvent(AccountItem account)
        {
            LogService.Debug("FireSyncAccountEvent");
            if (OnSyncAccountEvent != null)
                OnSyncAccountEvent(account);
        }


        /// <summary>
        /// 增加交易帐户事件 服务端新增交易帐户
        /// </summary>
        public event Action<AccountItem> OnNewAccountEvent;

        internal void FireNewAccountEvent(AccountItem account)
        {
            LogService.Debug("FireNewAccountEvent");
            if (OnNewAccountEvent != null)
                OnNewAccountEvent(account);
        }


        /// <summary>
        /// 财务信息
        /// </summary>
        //public event Action<IAccountInfo> GotFinanceInfoEvent;

        /// <summary>
        /// 动态财务信息
        /// </summary>
        public event Action<AccountInfoLite> OnInfoLiteEvent;

        internal void FireInfoLiteEvent(AccountInfoLite lite)
        {
            if (OnInfoLiteEvent != null)
                OnInfoLiteEvent(lite);
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


        /// <summary>
        /// 交易帐户登入信息事件
        /// </summary>
        public event Action<NotifyMGRSessionUpdateNotify> OnSessionUpdateEvent;
        internal void FireSessionUpdateEvent(NotifyMGRSessionUpdateNotify notify)
        {
            LogService.Debug("FireSessionUpdateEvent");
            if (OnSessionUpdateEvent != null)
                OnSessionUpdateEvent(notify);
        }

        /// <summary>
        /// 获得恢复交易帐号事件
        /// </summary>
        public event Action<RspMGRResumeAccountResponse> OnAccountResumeEvent;

        internal void FireAccountResumeEvent(RspMGRResumeAccountResponse response)
        {
            LogService.Debug("FireAccountResumeEvent");
            if (OnAccountResumeEvent != null)
                OnAccountResumeEvent(response);
        }
    }
}
