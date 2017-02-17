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
        public Control FilterToolBar { get; set; }
        ctFollowItemFilter filterBox = null;
        public ctFollowCentre()
        {
            InitializeComponent();
            filterBox = new ctFollowItemFilter();
            filterBox.FilterArgsChanged += new Action<FollowFilterArgs>(filterBox_FilterArgsChanged);
            this.FilterToolBar = filterBox;

            this.Load += new EventHandler(ctFollowCentre_Load);
        }

        void filterBox_FilterArgsChanged(FollowFilterArgs obj)
        {
            ctFollowItemList1.FilterFollowItem(obj);
        }

        void ctFollowCentre_Load(object sender, EventArgs e)
        {
            ctFollowStrategyMoniter1.StrategySelected += new Action<FollowStrategyConfig>(ctFollowItemList1.OnStrategySelected);
        }

        public void OnInit()
        { 
        
        }

        public void OnDisposed()
        { 
            
        }
    }
}
