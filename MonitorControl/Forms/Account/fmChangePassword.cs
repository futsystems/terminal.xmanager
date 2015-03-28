using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;


namespace TradingLib.MoniterControl
{
    public partial class fmChangePassword :ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmChangePassword()
        {
            InitializeComponent();
        }


        public void SetAccount(string account)
        {
            this.account.Text = account;
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (newpass.Text.Contains(",") || newpass.Text.Contains("|") || newpass.Text.Contains("^"))
            {

                MoniterHelper.WindowMessage("密码不能含有系统保留字符 | , ^");
                return;
            }
            if (newpass.Text.Length < 4)
            {
                MoniterHelper.WindowMessage("密码长度不能小于4位");
                return;
            }
            if (newpass.Text.Length > 10)
            {
                MoniterHelper.WindowMessage("密码不能大于10位");
                return;
            }

            CoreService.TLClient.ReqChangeAccountPass(account.Text, newpass.Text);
            this.Close();
        }
    }
}
