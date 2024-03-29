﻿using System;
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
    public partial class ctAccountFilter : UserControl,IEventBinder
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

        public void OnInit()
        {
            kryptonContextMenuSeparator4.Visible = false;
            kryptonContextMenuItem6.Visible = false;
            if (CoreService.SiteInfo.Manager.IsRoot())
            {
                kryptonContextMenuSeparator4.Visible = true;
                kryptonContextMenuItem6.Visible = true;
            }
        }

        public void OnDisposed()
        {
        
        }


        void ctFilter_Load(object sender, EventArgs e)
        {
            cbLock.CheckStateChanged += new EventHandler(cbExecute_CheckStateChanged);
            cbLogin.CheckedChanged += new EventHandler(cbLogin_CheckedChanged);
            cbPos.CheckedChanged += new EventHandler(cbPos_CheckedChanged);
            tbAccount.TextChanged += new EventHandler(tbAccount_TextChanged);
            tbConfigTemplate.TextChanged += new EventHandler(tbConfigTemplate_TextChanged);
            accountType.SelectedValueChanged += new EventHandler(accountType_SelectedValueChanged);
            tbMemo.TextChanged += new EventHandler(tbMemo_TextChanged);
            btnDebug.Click += new EventHandler(btnDebug_Click);

            kryptonContextMenuItem1.Click += new EventHandler(kryptonContextMenuItem1_Click);
            kryptonContextMenuItem2.Click += new EventHandler(kryptonContextMenuItem2_Click);
            kryptonContextMenuItem3.Click += new EventHandler(kryptonContextMenuItem3_Click);
            kryptonContextMenuItem4.Click +=new EventHandler(kryptonContextMenuItem4_Click);
            kryptonContextMenuItem5.Click += new EventHandler(kryptonContextMenuItem5_Click);

            kryptonContextMenuItem6.Click += new EventHandler(kryptonContextMenuItem6_Click);//临时冻结品种

            CoreService.EventCore.RegIEventHandler(this);
        }

        void kryptonContextMenuItem6_Click(object sender, EventArgs e)
        {
            fmBlockSecurity fm = new fmBlockSecurity();
            fm.ShowDialog();
            fm.Close();
        }

        void kryptonContextMenuItem5_Click(object sender, EventArgs e)
        {
            BatchFlatPosition();
        }

        void kryptonContextMenuItem4_Click(object sender, EventArgs e)
        {
            BatchInActiveAccount();
        }

        void kryptonContextMenuItem3_Click(object sender, EventArgs e)
        {
            BatchActiveAccount();
        }

        void kryptonContextMenuItem2_Click(object sender, EventArgs e)
        {
            BatchDelAccount();
        }

        public event Action BatchConfigTemplate = delegate { };
        public event Action BatchDelAccount = delegate { };
        public event Action BatchActiveAccount = delegate { };
        public event Action BatchInActiveAccount = delegate { };
        public event Action BatchFlatPosition = delegate { };

        void kryptonContextMenuItem1_Click(object sender, EventArgs e)
        {
            BatchConfigTemplate();
        }


        void tbConfigTemplate_TextChanged(object sender, EventArgs e)
        {
            _arg.ConfigNameSearch = tbConfigTemplate.Text;
            FireFilterArgsChanged(_arg);
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
