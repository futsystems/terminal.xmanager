using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using System.Windows.Forms;


namespace TradingLib.TinyMGRControl
{
    public partial class ctAccountMontier
    {
        void InitMenu()
        {
            //accountgrid.ContextMenuStrip = MenuService.CreateContextMenu(this, "/AccountList/ContextMenu");
        }


        int _mouseRowIdx = -1;
        void accountgrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                
                accountgrid.ContextMenuStrip = CreateMenu();
                accountgrid.ContextMenuStrip.Show(e.Location);
            }
        }

        void accountgrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //设置选中
                    if (accountgrid.Rows[e.RowIndex].Selected == false)
                    {
                        accountgrid.ClearSelection();
                        accountgrid.Rows[e.RowIndex].Selected = true;
                    }

                    _mouseRowIdx = e.RowIndex;//设定当前选中行号
                    //显示到当前光标位置
                    CreateMenu().Show(Cursor.Position);

                }
            }
        }


        ContextMenuStrip CreateMenu()
        {
            ContextMenuStrip tmp = new ContextMenuStrip();
            AccountLite account = GetAccount(CurrentAccount);
            if (account != null)
            {
                
                if (account.Execute)
                {
                    tmp.Items.Add("冻结账户", null, InActiveAccount_Click);
                }
                else
                {
                    tmp.Items.Add("激活账户", null, ActiveAccount_Click);
                }
                

                tmp.Items.Add(new ToolStripSeparator());
                tmp.Items.Add("出入金", null, CashOperation_Click);
                tmp.Items.Add("修改账户", null, EditAccount_Click);

                tmp.Items.Add(new ToolStripSeparator());
                tmp.Items.Add("删除账户", null, DelAccount_Click);

            }

            return tmp;
        }

        /// <summary>
        /// 出入金操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CashOperation_Click(object sender, EventArgs e)
        {
             AccountLite account = GetAccount(CurrentAccount);
             if (account != null)
             {
                 fmCashOperation fm = new fmCashOperation();
                 fm.SetAccount(account);
                 fm.ShowDialog();
                 fm.Close();
             }
        }

        /// <summary>
        /// 删除账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DelAccount_Click(object sender, EventArgs e)
        {
             AccountLite account = GetAccount(CurrentAccount);
             if (account != null)
             {

                 if (MoniterHelper.WindowConfirm("确认删除交易帐户?") == System.Windows.Forms.DialogResult.Yes)
                 {
                     CoreService.TLClient.ReqDelAccount(account.Account);

                 }
             }
             else
             {
                 MoniterHelper.WindowMessage("请选择需要编辑的交易帐户！");
             }
        }

        /// <summary>
        /// 冻结账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InActiveAccount_Click(object sender, EventArgs e)
        {
            AccountLite account = GetAccount(CurrentAccount);
            if (account != null)
            {

                CoreService.TLClient.ReqUpdateAccountExecute(account.Account, false);
            }
            else
            {
                MoniterHelper.WindowMessage("请选择需要编辑的交易帐户！");
            }
        }

        /// <summary>
        /// 激活账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ActiveAccount_Click(object sender, EventArgs e)
        {
            AccountLite account = GetAccount(CurrentAccount);
            if (account != null)
            {

                CoreService.TLClient.ReqUpdateAccountExecute(account.Account, true);
            }
            else
            {
                MoniterHelper.WindowMessage("请选择需要编辑的交易帐户！");
            }
        }


        /// <summary>
        /// 编辑某个交易帐号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EditAccount_Click(object sender, EventArgs e)
        {
            AccountLite account = GetAccount(CurrentAccount);
            if (account != null)
            {
                fmEditAccount fm = new fmEditAccount();
                fm.SetAccount(account);
                fm.ShowDialog();
                fm.Close();
            }
            else
            {
                MoniterHelper.WindowMessage("请选择需要编辑的交易帐户！");
            }
        }


        void ClearTerminal_Click(object sender, EventArgs e)
        {
            AccountLite account = GetAccount(CurrentAccount);
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
        /// 添加交易帐户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            fmAddAccount fm = new fmAddAccount();
            fm.ShowDialog();
            fm.Close();
        }


        void QrySettlement_Click(object sender, EventArgs e)
        {
            //AccountLite account = GetVisibleAccount(CurrentAccount);
            //if (account != null)
            //{
            //    fmSettlement fm = new fmSettlement();
            //    fm.SetAccount(account.Account);
            //    fm.Show();

            //}
            //else
            //{
            //    MoniterHelper.WindowMessage("请选择需要查询的交易帐户！");
            //}
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
            AccountLite accountlite = null;

            if (accountmap.TryGetValue(account, out accountlite))
            {
                //设定当前选中帐号
                accountselected = accountlite;

                lbAccountSelected.Text = accountlite.Account;
                //触发事件中继的帐户选择事件
                CoreService.EventAccount.FireAccountSelectedEvent(accountlite);
            }
        }

        

    }
}
