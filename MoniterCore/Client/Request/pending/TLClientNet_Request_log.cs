﻿using System;
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
        /// 查询任务运行日志
        /// </summary>
        public void ReqQryTaskRunLog()
        {
            this.ReqContribRequest("LogServer", "QryTaskRunLog", "");
        }

     
    }
}
