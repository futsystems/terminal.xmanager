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
    public class HelpInfoCommand:AbstractMenuCommand
    {
        public override void Run()
        {
            fmHelp.Instance.Show();
        }
    }

    public class DomainInfoCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            fmDomainInfo fm = new fmDomainInfo();
            fm.SetDomain(CoreService.SiteInfo.Domain);
            fm.ShowDialog();
            fm.Close();
        }
    }
}
