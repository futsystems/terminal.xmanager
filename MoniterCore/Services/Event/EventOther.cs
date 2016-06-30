using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterCore
{
    public class EventOther
    {

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






        public event Action<object,Symbol> OnSymbolSelectedEvent;

        /// <summary>
        /// 触发合约选择事件
        /// </summary>
        /// <param name="symbol"></param>
        public void FireSymbolSelectedEvent(object obj,Symbol symbol)
        {
            if (OnSymbolSelectedEvent != null)
                OnSymbolSelectedEvent(obj,symbol);
        }



    }
}
