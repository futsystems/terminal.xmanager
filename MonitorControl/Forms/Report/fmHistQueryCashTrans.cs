using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TradingLib.MoniterControl
{
    public partial class fmHistQueryCashTrans : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmHistQueryCashTrans()
        {
            InitializeComponent();
        }



        public EnumHistReportType HistReportType
        {
            get { return ctCashTrans1.HistReportType; }
            set { ctCashTrans1.HistReportType = value; }
        }
        public void SetAccount(string account)
        {
            ctCashTrans1.SetAccount(account);
        }
    }
}
