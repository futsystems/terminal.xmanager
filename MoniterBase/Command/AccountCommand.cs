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
            fm.Close();
        }
    }

    public class RouterGroupCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmRouterGroup fm = new fmRouterGroup();
            fm.ShowDialog();
            fm.Close();
        }
    }


    /// <summary>
    /// 添加配资客户
    /// </summary>
    public class AddFinServiceAccountCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmEditAccount fm = new fmEditAccount();
            fm.ShowDialog();
            fm.Close();
        }
    }

    /// <summary>
    /// 添加交易帐户
    /// </summary>
    public class AddFAccountCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmEditAccount fm = new fmEditAccount();
            fm.ShowDialog();
            fm.Close();
        }
    }

}
