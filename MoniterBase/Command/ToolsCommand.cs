using System;
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
    /// 结算管理
    /// </summary>
    public class SettleManagerCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmSettleManager fm = new fmSettleManager();
            fm.ShowDialog();
            fm.Close();
        }
    }
}
