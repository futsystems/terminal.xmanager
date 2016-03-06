using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;


namespace TradingLib.MoniterControl
{
    [MoniterControlAttr("FunctionPanel", "功能面板", EnumControlLocation.BottomRight)]
    public partial class ctFunctionPanel : UserControl, IMoniterControl,IEventBinder
    {
        public ctFunctionPanel()
        {
            InitializeComponent();

            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            foreach (Symbol s in CoreService.BasicInfoTracker.GetSymbolTradable())
            {
                //quoteList.addSecurity(s);
                //quotemoniter
            }
            CoreService.EventIndicator.GotTickEvent += new Action<Tick>(GotTick);
            CoreService.EventBasicInfo.OnSymbolEvent += new Action<SymbolImpl>(EventBasicInfo_OnSymbolEvent);

            if (!CoreService.SiteInfo.Manager.IsRoot())
            {
                //pageExecution.Visible = CoreService.SiteInfo.UIAccess.r_execution;
            }
        }

        void EventBasicInfo_OnSymbolEvent(SymbolImpl obj)
        {
            //uoteList.addSecurity(obj);
        }

        public void OnDisposed()
        { 
            
        }

        void GotTick(Tick k)
        {
            //quoteList.GotTick(k);
        }
    }

    
}
