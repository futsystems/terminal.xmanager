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
    /// 交易通道管理
    /// </summary>
    public class ManagerConnectorCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmConnectorManager fm = new fmConnectorManager();
            fm.ShowDialog();
        }
    }

    /// <summary>
    /// 添加配资客户
    /// </summary>
    public class AddFinServiceAccountCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmEditFinAccount fm = new fmEditFinAccount();
            fm.ShowDialog();
        }
    }

}
