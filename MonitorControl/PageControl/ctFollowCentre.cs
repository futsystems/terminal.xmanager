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
    [MoniterControlAttr("FollowMoniter", "跟单监控", EnumControlLocation.TopPanel)]
    public partial class ctFollowCentre : UserControl, IEventBinder, IMoniterControl
    {
        public ctFollowCentre()
        {
            InitializeComponent();
        }

        public void OnInit()
        { 
        
        }

        public void OnDisposed()
        { 
            
        }
    }
}
