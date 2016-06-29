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
    public partial class PageDashboard : UserControl,IPage
    {
        string _pageName = PageTypes.PAGE_DASHBOARD;
        public string PageName { get { return _pageName; } }

        public PageDashboard()
        {
            InitializeComponent();
        }
    }
}
