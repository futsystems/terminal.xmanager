using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterControl
{
    public class FollowFilterArgs
    {
        //帐户状态 冻结 激活
        //public bool AcctExecuteEnable = false;
        public bool AccLock = false;

        public bool AccLogin = false;

        /// <summary>
        /// 是否显示持仓中的跟单项
        /// </summary>
        public bool FollowPos = false;

        public bool ProfitEnable = false;
        public bool FollowProfit = false;

        public bool EventTypeEnable = false;
        /// <summary>
        /// 跟单项类别
        /// </summary>
        public QSEnumPositionEventType EventType = (QSEnumPositionEventType)(-1);
        /// <summary>
        /// 信号账户过滤
        /// </summary>
        public string SignalSearch = string.Empty;

        /// <summary>
        /// 合约过滤
        /// </summary>
        public string SymbolSearch = string.Empty;
    }
}
