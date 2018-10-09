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
    public partial class fmHelp : DevExpress.XtraEditors.XtraForm
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

        public void GotDebug(string msg)
        {
            debugControl1.GotDebug(msg);
        }
        private void stop_Click(object sender, EventArgs e)
        {
            CoreService.TLClient.DebugStopTick();
        }

        private void stopMsg_Click(object sender, EventArgs e)
        {
            CoreService.TLClient.DebugStopMessage();
        }
    }
}
