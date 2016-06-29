using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.TinyMGRControl;


namespace TinyMgr
{
    public partial class PageSTKSymbol : UserControl,IPage
    {

        string _pageName = PageTypes.PAGE_STK_SYMBOLS;
        public string PageName { get { return _pageName; } }

        public PageSTKSymbol()
        {
            InitializeComponent();
        }
    }
}
