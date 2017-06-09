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
    public partial class ctAccountFilter : UserControl
    {
        public event Action<FilterArgs> FilterArgsChanged = delegate { };
        void FireFilterArgsChanged(FilterArgs arg)
        {
            FilterArgsChanged(arg);
        }

        public ctAccountFilter()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(accountType).BindDataSource(MoniterHelper.GetAccountTypeCombList(true));

            this.Load += new EventHandler(ctFilter_Load);
        }
        FilterArgs _arg = new FilterArgs();

        void ctFilter_Load(object sender, EventArgs e)
        {
            cbLock.CheckStateChanged += new EventHandler(cbExecute_CheckStateChanged);
            cbLogin.CheckedChanged += new EventHandler(cbLogin_CheckedChanged);
            cbPos.CheckedChanged += new EventHandler(cbPos_CheckedChanged);
            tbAccount.TextChanged += new EventHandler(tbAccount_TextChanged);
            accountType.SelectedValueChanged += new EventHandler(accountType_SelectedValueChanged);
            tbMemo.TextChanged += new EventHandler(tbMemo_TextChanged);
            btnDebug.Click += new EventHandler(btnDebug_Click);
        }


        public event VoidDelegate DebugEvent;

        void btnDebug_Click(object sender, EventArgs e)
        {
            if (DebugEvent != null)
            {
                DebugEvent();
            }
        }

        void tbMemo_TextChanged(object sender, EventArgs e)
        {
            _arg.MemoSearch = tbMemo.Text;
            FireFilterArgsChanged(_arg);
        }

        void accountType_SelectedValueChanged(object sender, EventArgs e)
        {
            QSEnumAccountCategory val = (QSEnumAccountCategory)accountType.SelectedValue;
            if ((int)val == -1)
            {
                _arg.AcctTypeEnable = false;
            }
            else
            {
                _arg.AcctTypeEnable = true;
                _arg.AcctType = val;
            }
            FireFilterArgsChanged(_arg);
        }

        public void SetAccountNum(int num)
        {
            lbAccNum.Text = string.Format("帐户数:{0}", num);
        }

        void tbAccount_TextChanged(object sender, EventArgs e)
        {
            _arg.AccSearch = tbAccount.Text;
            FireFilterArgsChanged(_arg);
        }

        void cbPos_CheckedChanged(object sender, EventArgs e)
        {
            _arg.AccPos = cbPos.Checked;
            FireFilterArgsChanged(_arg);
        }

        void cbLogin_CheckedChanged(object sender, EventArgs e)
        {
            _arg.AccLogin = cbLogin.Checked;
            FireFilterArgsChanged(_arg);
        }

        void cbExecute_CheckStateChanged(object sender, EventArgs e)
        {
            _arg.AccLock = cbLock.Checked;
            FireFilterArgsChanged(_arg);
        }
    }
}
