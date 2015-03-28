﻿using System;
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
    public partial class fmChangeRouter :ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmChangeRouter()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmChangeRouter_Load);
            //this.ctRouterGroupList1.RouterGroupInitEvent += new VoidDelegate(ctRouterGroupList1_RouterGroupInitEvent);
        }

        //void ctRouterGroupList1_RouterGroupInitEvent()
        //{
        //    if (_account != null)
        //    {
        //        cutrgname.Text = ctRouterGroupList1.GetRrouterGroupName(_account.RG_ID);
        //    }
        //}

        void fmChangeRouter_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            string msg = string.Format("确认修改帐户路由组为:{0}?(修改前请确认帐户没有持仓和挂单)", ctRouterGroupList1.RouterGroup.Name);
            if (ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show(msg,"确认操作",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateRouterGroup(_account.Account, ctRouterGroupList1.RouterGroupID);
                this.Close();
            }
        }

        AccountLite _account = null;
        public void SetAccount(AccountLite account)
        {
            _account = account;
            this.Text = string.Format("修改帐户[{0}]路由组设置", _account.Account);
            RouterGroupSetting rg = CoreService.BasicInfoTracker.GetRouterGroup(_account.RG_ID);
            cutrgname.Text = (rg != null ? rg.Name : "未设置");
        }




    }
}
