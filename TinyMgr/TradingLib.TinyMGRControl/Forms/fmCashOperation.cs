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


namespace TradingLib.TinyMGRControl
{
    public partial class fmCashOperation : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmCashOperation()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(cbEquityTypeList).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumEquityType>());

            btnAccountDeposit.Click += new EventHandler(btnAccountDeposit_Click);
            btnAccountWithdraw.Click += new EventHandler(btnAccountWithdraw_Click);
        }

        AccountLite _account = null;
        /// <summary>
        /// 设定交易帐户
        /// </summary>
        /// <param name="account"></param>
        public void SetAccount(AccountLite account)
        {
            _account = account;
            this.Text = "出入金操作[{0}]".Put(_account.Account);
            //ctFinanceInfo1.SetAccount(account);
        }

        void btnAccountWithdraw_Click(object sender, EventArgs e)
        {
            decimal amount = cashop_amount.Value;
            if (amount == 0)
            {
                MoniterHelper.WindowMessage("请输入出入金金额");
                return;
            }
            string comment = cashop_comment.Text;
            string cashoptitle = string.Empty;

            decimal amount2 = amount * -1;

            QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
            if (MoniterHelper.WindowConfirm("确认向帐户[" + _account.Account + "] 出金" + amount.ToChineseStr()) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqCashOperation(_account.Account, amount2, type, "", comment, false);
            }
        }

        void btnAccountDeposit_Click(object sender, EventArgs e)
        {
            decimal amount = cashop_amount.Value;
            if (amount == 0)
            {
                MoniterHelper.WindowMessage("请输入出入金金额");
                return;
            }
            string comment = cashop_comment.Text;
            string cashoptitle = string.Empty;

            decimal amount2 = amount * 1;

            QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
            if (MoniterHelper.WindowConfirm("确认向帐户[" + _account.Account + "] 入金" + cashoptitle + " " + amount.ToChineseStr()) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqCashOperation(_account.Account, amount2, type, "", comment, false);
            }
        }
    }
}
