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


        FilterArgs _filterArgs = null;

        /// <summary>
        /// 获得当前帐户过滤参数
        /// 过滤器与帐户列表显示控件是分开的
        /// </summary>
        public static FilterArgs FilterArgs
        {
            get
            {
                if (defaultintance._filterArgs == null)
                    defaultintance._filterArgs = new FilterArgs();
                return defaultintance._filterArgs;
            }
        }


        public static event VoidDelegate OnFilterArgsChangeEvent;

        static internal void FireFilterArgsChangeEvent()
        {
            if (OnFilterArgsChangeEvent != null)
                OnFilterArgsChangeEvent();
        }
    }
}
