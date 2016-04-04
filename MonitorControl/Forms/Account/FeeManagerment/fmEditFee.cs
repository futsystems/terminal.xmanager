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
using TradingLib.Protocol.MainAcctFinService;
using TradingLib.MoniterCore;


namespace TradingLib.MoniterControl
{
    public partial class fmEditFee : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmEditFee()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmEditFee_Load);
        }

        void fmEditFee_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认更新收费项目") == System.Windows.Forms.DialogResult.Yes)
            {
                _fee.Comment = comment.Text;
                _fee.Amount = amount.Value;
                CoreService.TLClient.ReqContribRequest("MainAcctFinService", "UpdateFee", _fee);
                this.Close();
            }

        }

        FeeSetting _fee;
        public void SetFee(FeeSetting f)
        {
            _fee = f;
            account.Text = f.Account;
            settleday.Text = f.Settleday.ToString();
            type.Text = Util.GetEnumDescription(f.FeeType);
            amount.Text = f.Amount.ToFormatStr();
            collectstatus.Text = f.Collected ? "已扣费" : "未扣费";
            comment.Text = f.Comment;
            error.Text = f.Error;

            if (f.Collected)
            {
                btnSubmit.Enabled = false;
            }
        }
    }
}
