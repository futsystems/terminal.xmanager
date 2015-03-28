using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.MoniterCore;


namespace TradingLib.MoniterControl
{
    public partial class fmChangePasswordAgent : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmChangePasswordAgent()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string _oldpass = oldpass.Text;
            string _newpass = newpass.Text;
            string _newpass2 = newpass2.Text;

            if (string.IsNullOrEmpty(_oldpass))
            {
                MoniterHelper.WindowMessage("旧密码为空，请输入旧密码!");
                return;
            }
            if (string.IsNullOrEmpty(_newpass))
            {
                MoniterHelper.WindowMessage("新密码为空，请输入新密码！");
                return;
            }
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            if (!reg1.IsMatch(_newpass))
            {
                MoniterHelper.WindowMessage("密码只能包含数字和字母!");
                return;
            }
            if (!_newpass.Equals(_newpass2))
            {
                MoniterHelper.WindowMessage("两次输入的密码不相符!");
                return;
            }

            if (_oldpass.Equals(_newpass2))
            {
                MoniterHelper.WindowMessage("新密码与旧密码相同,请输入不同的密码!");
                return;
            }
            CoreService.TLClient.ReqUpdatePass(_oldpass, _newpass2);
            this.Close();
        }
    }
}
