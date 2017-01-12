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
            
        
        }




        void ClearTerminal_Click(object sender, EventArgs e)
        {
            AccountItem account = GetVisibleAccount(CurrentAccount);
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
            AccountItem account = GetVisibleAccount(CurrentAccount);
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
            AccountItem account = GetVisibleAccount(CurrentAccount);
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
        //private void btnAddAccount_Click(object sender, EventArgs e)
        //{
        //    fmAddAccount fm = new fmAddAccount();
        //    fm.TopMost = true;
        //    fm.ShowDialog();
        //}


        void QrySettlement_Click(object sender, EventArgs e)
        {
            AccountItem account = GetVisibleAccount(CurrentAccount);
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
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请稍后再请求帐户日内数据");
                return;
            }
            _lastresumetime = DateTime.Now;
            string account = CurrentAccount;
            AccountItem accountlite = null;

            if (accountmap.TryGetValue(account, out accountlite))
            {
                //设定当前选中帐号
                accountselected = accountlite;

                //触发事件中继的帐户选择事件
                CoreService.EventAccount.FireAccountSelectedEvent(accountlite);
            }
        }

        

    }
}
