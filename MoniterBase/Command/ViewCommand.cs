using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using TradingLib.MoniterCore;
using TradingLib.MoniterControl;

namespace TradingLib.MoniterBase.Command
{
    public class InitCommand : AbstractCommand
    {
        public override void Run()
        {
            Workbench workbench = Workbench.Instance;
            workbench.ShowControl("ACCOUNTMONITER");

            ctAccountMontier accountMoniter = new ctAccountMontier();
            accountMoniter.Dock = DockStyle.Fill;
            workbench.AppendControl(accountMoniter);

            ctTradingInfo infoMoniter = new ctTradingInfo();
            infoMoniter.Dock = DockStyle.Fill;
            workbench.AppendControl(infoMoniter);

            ctQuoteMoniter quoteMoniter = new ctQuoteMoniter();
            quoteMoniter.Dock = DockStyle.Fill;
            workbench.AppendControl(quoteMoniter);

            ctOrderPanel orderPanel = new ctOrderPanel();
            orderPanel.Dock = DockStyle.Fill;
            workbench.AppendControl(orderPanel);

            workbench.ShowControl("AccountMoniter", EnumControlLocation.TopPanel);
            workbench.ShowControl("TradingInfoMoniter", EnumControlLocation.BottomLeft);
            workbench.ShowControl("OrderPanel", EnumControlLocation.BottomRight);
        }
    }


    public class ShowQuoteCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            Workbench workbench = (Workbench)this.Owner;
            workbench.ShowControl("QuoteMoniter", EnumControlLocation.TopPanel);
        }
    }

    public class ShowAccountListCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            Workbench workbench = (Workbench)this.Owner;
            workbench.ShowControl("AccountMoniter", EnumControlLocation.TopPanel);
        }
    }



    public class ExpandViewCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            Workbench workbench = Workbench.Instance;

            workbench.CollapseBottom();
        }
    }

    public class CollapseViewCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            Workbench workbench = Workbench.Instance;

            workbench.ExpandBottom();
        }
    }

    public class ResetViewCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            Workbench workbench = Workbench.Instance;

            workbench.ResetView();
        }
    }

}
