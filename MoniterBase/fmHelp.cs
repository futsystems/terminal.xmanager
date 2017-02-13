using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterBase
{
    public partial class fmHelp : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        static fmHelp instance;

        public static fmHelp Instance
        {
            get
            {
                return instance;
            }
        }


        static fmHelp()
        {
            instance = new fmHelp();
        }

        private fmHelp()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(fmHelp_FormClosing);
        }

        void fmHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            TradingLib.MoniterCore.CoreService.TLClient.ReqQryTickSnapshot();
        }
    }
}
