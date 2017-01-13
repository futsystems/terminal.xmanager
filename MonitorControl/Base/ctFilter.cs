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
    public partial class ctFilter : UserControl
    {
        public ctFilter()
        {
            InitializeComponent();

            this.Load += new EventHandler(ctFilter_Load);
        }
        FilterArgs _arg = new FilterArgs();

        void ctFilter_Load(object sender, EventArgs e)
        {
            cbLock.CheckStateChanged += new EventHandler(cbExecute_CheckStateChanged);
            cbLogin.CheckedChanged += new EventHandler(cbLogin_CheckedChanged);
            cbPos.CheckedChanged += new EventHandler(cbPos_CheckedChanged);
            tbAccount.TextChanged += new EventHandler(tbAccount_TextChanged);
        }

        void tbAccount_TextChanged(object sender, EventArgs e)
        {
            _arg.AccSearch = tbAccount.Text;
            ControlService.FireFilterArgsChanged(_arg);
            lbAccNum.Text = _arg.AccNum.ToString();
        }

        void cbPos_CheckedChanged(object sender, EventArgs e)
        {
            _arg.AccPos = cbPos.Checked;
            ControlService.FireFilterArgsChanged(_arg);
            lbAccNum.Text = _arg.AccNum.ToString();
        }

        void cbLogin_CheckedChanged(object sender, EventArgs e)
        {
            _arg.AccLogin = cbLogin.Checked;
            ControlService.FireFilterArgsChanged(_arg);
            lbAccNum.Text = _arg.AccNum.ToString();
        }

        void cbExecute_CheckStateChanged(object sender, EventArgs e)
        {
            _arg.AccLock = cbLock.Checked;
            ControlService.FireFilterArgsChanged(_arg);
            lbAccNum.Text = _arg.AccNum.ToString();
            
        }
    }
}
