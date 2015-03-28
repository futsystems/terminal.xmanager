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
    /// 管理员管理
    /// </summary>
    public class ManagerCommand:AbstractMenuCommand
    {
        public override void Run()
        {
            fmManagerCentre fm = new fmManagerCentre();
            fm.ShowDialog();
        }
    }
}
