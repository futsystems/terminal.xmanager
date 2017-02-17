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
    public partial class ctFunctionPanel : UserControl, IMoniterControl
    {
        public Control FilterToolBar { get; set; }
        public ctFunctionPanel()
        {
            InitializeComponent();
        }
    }

    
}
