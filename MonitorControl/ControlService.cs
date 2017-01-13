using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;

namespace TradingLib.MoniterControl
{
    public class ControlService
    {
        static ControlService defaultintance = null;
        static ControlService()
        {
            defaultintance = new ControlService();
        }

        private ControlService()
        {
            
        }


        public static event Action<FilterArgs> FilterArgsChanged;

        static internal void FireFilterArgsChanged(FilterArgs arg)
        {
            if (FilterArgsChanged != null)
                FilterArgsChanged(arg);
        }

        /// <summary>
        /// 交易账户Grid显示账户显示数量
        /// </summary>
        public static event Action<int> AccGridNumChanged;

        static internal void FireAccGridNumChanged(int num)
        {
            if (AccGridNumChanged != null)
                AccGridNumChanged(num);
        }
    }
}
