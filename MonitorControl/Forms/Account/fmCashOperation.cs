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
    public partial class fmCashOperation : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmCashOperation()
        {
            InitializeComponent();
            cashop_type.Items.Add("入金");
            cashop_type.Items.Add("出金");
            cashop_type.SelectedIndex = 0;
            MoniterHelper.AdapterToIDataSource(cbEquityTypeList).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumEquityType>());

            this.Load += new EventHandler(fmCashOperation_Load);
        }

        void fmCashOperation_Load(object sender, EventArgs e)
        {
            btnCashOperation.Click += new EventHandler(btnCashOperation_Click);
        }

        void btnCashOperation_Click(object sender, EventArgs e)
        {
            decimal amount = cashop_amount.Value;
            string cashopref = cashop_ref.Text;
            string comment = cashop_ref.Text;
            int cashopvalue = cashop_type.SelectedIndex;
            string cashoptitle = string.Empty;
            decimal amount2 = 0;
            bool sync = cashop_sync.Checked;
            if (cashopvalue == -1)
            {
                MoniterHelper.WindowMessage("请选择出入金类型");
                return;
            }
            if (cashopvalue == 0)
            {
                cashoptitle = "入金";
                amount2 = Math.Abs(amount);
            }
            else if (cashopvalue == 1)
            {
                cashoptitle = "出金";
                amount2 = -1 * Math.Abs(amount);
            }
            if (amount == 0)
            {
                MoniterHelper.WindowMessage("请输入出入金金额");
                return;
            }
            
            QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
            if (MoniterHelper.WindowConfirm("确认向帐户[" + _account.Account + "] " + cashoptitle + " " + amount.ToString() + " 流水号:" + cashopref) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqCashOperation(_account.Account, amount2, type, cashopref, comment, sync);
            }
        }

        AccountLite _account = null;
        /// <summary>
        /// 设定交易帐户
        /// </summary>
        /// <param name="account"></param>
        public void SetAccount(AccountLite account)
        {
            _account = account;
        }


    }
}
