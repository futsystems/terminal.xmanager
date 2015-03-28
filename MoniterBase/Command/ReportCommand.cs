﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using TradingLib.MoniterControl;
using ICSharpCode.Core;


namespace TradingLib.MoniterBase.Command
{
    /// <summary>
    /// 交易记录查询
    /// </summary>
    public class QryHistTradingInfoCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmHistQuery fm = new fmHistQuery();
            fm.ShowDialog();
        }
    }

    /// <summary>
    /// 查询出入金记录
    /// </summary>
    public class QryHistCashTransCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmHistQueryCashTrans fm = new fmHistQueryCashTrans();
            fm.ShowDialog();

        }
    }

    /// <summary>
    /// 查询结算单
    /// </summary>
    public class QrySettlementCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmSettlement fm = new fmSettlement();
            fm.ShowDialog();
        }
    }
}
