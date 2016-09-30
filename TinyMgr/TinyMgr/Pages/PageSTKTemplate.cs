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
using TradingLib.TinyMGRControl;

namespace TinyMgr
{
    public partial class PageSTKTemplate : UserControl, IPage
    {
        string _pageName = PageTypes.PAGE_STK_SYMBOLS;
        public string PageName { get { return _pageName; } }

        public PageSTKTemplate()
        {
            InitializeComponent();
        }
    }
}
