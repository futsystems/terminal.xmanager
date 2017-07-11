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
        public Control FilterToolBar { get; set; }
        public ctFunctionPanel()
        {
            InitializeComponent();
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            if (!CoreService.SiteInfo.Permission.r_execution)
            {
                ctOrderSenderM1.Visible = false;
                ctQuoteMoniterS1.Dock = DockStyle.Fill;
            }
        }

        public void OnDisposed()
        { 
        
        }
    }

    
}
