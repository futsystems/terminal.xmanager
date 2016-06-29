﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.TinyMGRControl
{
    /// <summary>
    /// 服务端回报事件响应 
    /// </summary>
    public partial class ctAccountMontier
    {
        bool IsCurrentAccount(string account)
        {
            if (AccountSetlected == null) return false;
            if (account == AccountSetlected.Account) return true;
            return false;
        }

        /// <summary>
        /// 获得服务端的帐户信息
        /// </summary>
        /// <param name="account"></param>
        void GotAccount(AccountLite account)
        {
            if (string.IsNullOrEmpty(account.Account))
                return;
            accountcache.Write(account);
            //Globals.Debug(">>>>>>>>>>>>>>>>> ctaccountmontier got account");
        }

        /// <summary>
        /// 获得交易帐户的财务状态信息
        /// </summary>
        /// <param name="info"></param>
        void GotAccountInfoLite(AccountInfoLite info)
        {
            accountinfocache.Write(info);
        }

        /// <summary>
        /// 响应帐户变动事件
        /// </summary>
        /// <param name="account"></param>
        public void GotAccountChanged(AccountLite account)
        {
            accountcache.Write(account);
        }


        /// <summary>
        /// 获得客户端登入 退出状态更新
        /// </summary>
        /// <param name="notify"></param>
        public void GotSessionUpdate(NotifyMGRSessionUpdateNotify notify)
        {
            sessionupdatecache.Write(notify);
        }

    }
}
