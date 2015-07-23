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
        /// <summary>
        /// Workbench初始化默认控件
        /// </summary>
        public override void Run()
        {
            Workbench workbench = Workbench.Instance;

            //帐户列表控件
            ctAccountMontier accountMoniter = new ctAccountMontier();
            accountMoniter.Dock = DockStyle.Fill;
            workbench.AppendControl(accountMoniter);

            //交易记录控件
            ctTradingInfo infoMoniter = new ctTradingInfo();
            infoMoniter.Dock = DockStyle.Fill;
            workbench.AppendControl(infoMoniter);

            //行情列表控件
            ctQuoteMoniter quoteMoniter = new ctQuoteMoniter();
            quoteMoniter.Dock = DockStyle.Fill;
            workbench.AppendControl(quoteMoniter);

            //下单面板控件
            ctFunctionPanel functoinPanel = new ctFunctionPanel();
            functoinPanel.Dock = DockStyle.Fill;
            workbench.AppendControl(functoinPanel);

            ctFollowCentre followMoniter = new ctFollowCentre();
            followMoniter.Dock = DockStyle.Fill;
            workbench.AppendControl(followMoniter);

            //ctOrderPanel orderPanel = new ctOrderPanel();
            //orderPanel.Dock = DockStyle.Fill;
            //workbench.AppendControl(orderPanel);

            //显示默认视区
            workbench.ShowControl("AccountMoniter", EnumControlLocation.TopPanel);
            workbench.ShowControl("TradingInfoMoniter", EnumControlLocation.BottomLeft);
            workbench.ShowControl("FunctionPanel", EnumControlLocation.BottomRight);

            workbench.ResetView();

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

    public class ShowFollowCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            Workbench workbench = (Workbench)this.Owner;
            workbench.ShowControl("FollowMoniter", EnumControlLocation.TopPanel);
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
