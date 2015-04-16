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
    public partial class fmCollectFeeManual : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmCollectFeeManual()
        {
            InitializeComponent();
            collecttype.Items.Add("计入优先资金");
            collecttype.Items.Add("底层帐户出金");
            collecttype.SelectedIndex = 0;
            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认执行扣费?") == System.Windows.Forms.DialogResult.Yes)
            {
                QSEnumChargeMethod method = QSEnumChargeMethod.Manual;
                if (collecttype.SelectedIndex == 0)
                {
                    method = QSEnumChargeMethod.AutoDepositCredit;
                }
                if (collecttype.SelectedIndex == 1)
                {
                    method = QSEnumChargeMethod.AutoWithdraw;
                }
                CoreService.TLClient.ReqContribRequest("MainAcctFinService", "ManualCollectFee", new { id = _fee.ID, charge_method = method });
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
            amount.Text = Util.FormatDecimal(f.Amount);
            if (f.Collected)
            {
                btnSubmit.Enabled = false;
            }
        }



    }
}
