using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using System.Windows.Forms;
using ICSharpCode.Core;


namespace TradingLib.MoniterControl
{
    public partial class ctAccountMontier
    {
        void InitMenu()
        {
            accountgrid.ContextMenuStrip = MenuService.CreateContextMenu(this, "/AccountList/ContextMenu");
            /*
            accountgrid.ContextMenuStrip = new ContextMenuStrip();
            accountgrid.ContextMenuStrip.Items.Add("编辑账户", null, new EventHandler(EditAccount_Click));//0
            accountgrid.ContextMenuStrip.Items.Add("修改密码",null, new EventHandler(ChangePass_Click));//1
            accountgrid.ContextMenuStrip.Items.Add("修改信息", null, new EventHandler(ChangeInvestor_Click));//2
            accountgrid.ContextMenuStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());//3
            accountgrid.ContextMenuStrip.Items.Add("交易记录查询", null, new EventHandler(QryHist_Click));//4
            accountgrid.ContextMenuStrip.Items.Add("结算单查询", null, new EventHandler(QrySettlement_Click));//5
            accountgrid.ContextMenuStrip.Items.Add("查询密码", null, new EventHandler(QryLoginInfo_Click));//6
            accountgrid.ContextMenuStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());//7
            accountgrid.ContextMenuStrip.Items.Add("修改路由组", null, new EventHandler(UpdateRouterGroup_Click));//8
            accountgrid.ContextMenuStrip.Items.Add("解绑主帐户", null, new EventHandler(DelAccountConnecotr_Click));//9
            accountgrid.ContextMenuStrip.Items.Add("绑定主帐户", null, new EventHandler(UpdateAccountConnecotr_Click));//10
            accountgrid.ContextMenuStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());//11
            accountgrid.ContextMenuStrip.Items.Add("同步交易数据", null, new EventHandler(SyncData_Click));//13
            accountgrid.ContextMenuStrip.Items.Add("主帐户出入金与查询", null, new EventHandler(QryAccountInfo_Click));//13
            accountgrid.ContextMenuStrip.Items.Add(new System.Windows.Forms.ToolStripSeparator());//11
            accountgrid.ContextMenuStrip.Items.Add("删除帐户", null, new EventHandler(DelAccount_Click));//12
            accountgrid.ContextMenuStrip.Items.Add("注销帐户", null, new EventHandler(ClearTerminal_Click));
      **/
        
        }




        void ClearTerminal_Click(object sender, EventArgs e)
        {
            AccountLite account = GetVisibleAccount(CurrentAccount);
            if (account != null)
            {
                if (MoniterHelper.WindowConfirm(string.Format("确认注销交易帐户[{0}]的所有登入交易终端?", account.Account)) == DialogResult.Yes)
                {
                    CoreService.TLClient.ReqClearTerminals(account.Account);
                }
            }
            else
            {
                MoniterHelper.WindowMessage("请选择需要查询的交易帐户");
            }
        }





        /// <summary>
        /// 编辑某个交易帐号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EditAccount_Click(object sender, EventArgs e)
        {
            AccountLite account = GetVisibleAccount(CurrentAccount);
            if (account != null)
            {
                //fmAccountConfig fm = new fmAccountConfig();
                //fm.SetAccount(account);
                //fm.ShowDialog();
            }
            else
            {
                MoniterHelper.WindowMessage("请选择需要编辑的交易帐户！");
            }
        }

    
        /// <summary>
        /// 查询历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void QryHist_Click(object sender, EventArgs e)
        {
            AccountLite account = GetVisibleAccount(CurrentAccount);
            if (account != null)
            {
                //if (QryAccountHistEvent != null)
                //    QryAccountHistEvent(account);
                //fmHistQuery fm = new fmHistQuery();
                //fm.SetAccount(account.Account);
                //fm.Show();

            }
            else
            {
                MoniterHelper.WindowMessage("请选择需要查询的交易帐户！");
            }
        }

        /// <summary>
        /// 添加交易帐户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            fmAddAccount fm = new fmAddAccount();
            fm.TopMost = true;
            fm.ShowDialog();
        }


        void QrySettlement_Click(object sender, EventArgs e)
        {
            AccountLite account = GetVisibleAccount(CurrentAccount);
            if (account != null)
            {
                fmSettlement fm = new fmSettlement();
                fm.SetAccount(account.Account);
                fm.Show();

            }
            else
            {
                MoniterHelper.WindowMessage("请选择需要查询的交易帐户！");
            }
        }


        DateTime _lastresumetime = DateTime.Now;
        private void accountgrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CoreService.TradingInfoTracker.IsInResume)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("交易记录恢复中,请稍候!");
                return;
            }

            if (DateTime.Now.Subtract(_lastresumetime).TotalSeconds <= 1)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请不要频繁请求帐户日内数据");
                return;
            }
            _lastresumetime = DateTime.Now;
            string account = CurrentAccount;
            AccountLite accountlite = null;

            if (accountmap.TryGetValue(account, out accountlite))
            {
                //设定当前选中帐号
                accountselected = accountlite;
                //更新选中lable
                lbCurrentAccount.Text = accountlite.Account;

                //触发事件中继的帐户选择事件
                CoreService.EventAccount.FireAccountSelectedEvent(accountlite);
            }
        }

        

    }
}
