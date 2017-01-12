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
    public partial class fmCashOperationCounter : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmCashOperationCounter()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(cbEquityTypeList).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumEquityType>());
            MoniterHelper.AdapterToIDataSource(cbCurrency).BindDataSource(MoniterHelper.GetEnumValueObjects<CurrencyType>());
            this.Load += new EventHandler(fmCashOperationCounter_Load);
        }

        void fmCashOperationCounter_Load(object sender, EventArgs e)
        {
            btnAccountDeposit.Click += new EventHandler(btnAccountDeposit_Click);
            btnAccountWithdraw.Click += new EventHandler(btnAccountWithdraw_Click);
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
            CurrencyType currency = (CurrencyType)cbCurrency.SelectedValue;
            if (_account.Currency != currency)
            {
                amount = Math.Round(amount * _account.GetExchangeRate(currency),2);
                amount2 = amount * -1;
                QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认向帐户[" + _account.Account + "] 出金:" + amount.ToString() + _account.Currency.ToString() + "(" + cashop_amount.Value.ToChineseStr() + ")") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqCashOperation(_account.Account, amount2, type, "", comment, false);
                }
            }
            else
            {
                QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认向帐户[" + _account.Account + "] 出金:" + amount.ToString() + _account.Currency.ToString() + "(" + cashop_amount.Value.ToChineseStr() + ")") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqCashOperation(_account.Account, amount2, type, "", comment, false);
                }
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

            CurrencyType currency = (CurrencyType)cbCurrency.SelectedValue;
            if (_account.Currency != currency)
            {
                amount = Math.Round(amount * _account.GetExchangeRate(currency), 2); ;
                amount2 = amount * 1;
                QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认向帐户[" + _account.Account + "] 入金:" + amount.ToString() + _account.Currency.ToString() + "(" + cashop_amount.Value.ToChineseStr() + ")") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqCashOperation(_account.Account, amount2, type, "", comment, false);
                }
            }
            else
            {
                QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认向帐户[" + _account.Account + "] 入金:" + amount.ToString() + _account.Currency.ToString() + "(" + cashop_amount.Value.ToChineseStr() + ")") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqCashOperation(_account.Account, amount2, type, "", comment, false);
                }
            }
        }

        AccountItem _account = null;
        /// <summary>
        /// 设定交易帐户
        /// </summary>
        /// <param name="account"></param>
        public void SetAccount(AccountItem account)
        {
            _account = account;
            ctFinanceInfo1.SetAccount(account);
        }

    }
}
