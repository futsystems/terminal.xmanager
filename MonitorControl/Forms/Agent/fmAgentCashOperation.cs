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
    public partial class fmAgentCashOperation : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmAgentCashOperation()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(cbEquityTypeList).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumEquityType>());
            MoniterHelper.AdapterToIDataSource(cbCurrency).BindDataSource(MoniterHelper.GetEnumValueObjects<CurrencyType>());

            cbEquityTypeList.SelectedValue = QSEnumEquityType.OwnEquity;//默认为劣后

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
            if (_agent.Currency != currency)
            {
                amount = Math.Round(amount * _agent.GetExchangeRate(currency), 2);
                amount2 = amount * -1;
                QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认向代理帐户[" + _agent.Account + "] 出金:" + amount.ToString() + _agent.Currency.ToString() + "(" + cashop_amount.Value.ToChineseStr() + ")") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqAgentCashOperation(_agent.Account, amount2, type, "", comment);
                }
            }
            else
            {
                QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认向代理帐户[" + _agent.Account + "] 出金:" + amount.ToString() + _agent.Currency.ToString() + "(" + cashop_amount.Value.ToChineseStr() + ")") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqAgentCashOperation(_agent.Account, amount2, type, "", comment);
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
            if (_agent.Currency != currency)
            {
                amount = Math.Round(amount * _agent.GetExchangeRate(currency), 2); ;
                amount2 = amount * 1;
                QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认向代理帐户[" + _agent.Account + "] 入金:" + amount.ToString() + _agent.Currency.ToString() + "(" + cashop_amount.Value.ToChineseStr() + ")") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqAgentCashOperation(_agent.Account, amount2, type, "", comment);
                }
            }
            else
            {
                QSEnumEquityType type = (QSEnumEquityType)cbEquityTypeList.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认向代理帐户[" + _agent.Account + "] 入金:" + amount.ToString() + _agent.Currency.ToString() + "(" + cashop_amount.Value.ToChineseStr() + ")") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqAgentCashOperation(_agent.Account, amount2, type, "", comment);
                }
            }
        }

        AgentSetting _agent = null;
        /// <summary>
        /// 设定交易帐户
        /// </summary>
        /// <param name="account"></param>
        public void SetAgent(AgentSetting agent)
        {
            _agent = agent;
            ctAgentFinanceInfo1.SetAgent(agent);
            this.Text = string.Format("出入金/帐户查询[{0}]", Util.GetEnumDescription(_agent.AgentType));

            if (_agent.AgentType == EnumAgentType.Normal)
            {
                cbEquityTypeList.Enabled = false;
            }

        }

    }
}
