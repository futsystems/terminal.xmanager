using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using System.Windows.Forms;

namespace TradingLib.MoniterControl
{
    public partial class ctAccountMontier
    {
        ctAccountFilter toolBox;
        FilterArgs filterArgs = null;
        void InitToolBox()
        {
            toolBox = new ctAccountFilter();
            this.FilterToolBar = toolBox;
            toolBox.FilterArgsChanged += new Action<FilterArgs>(OnFilterArgsChanged);
            toolBox.DebugEvent += new VoidDelegate(toolBox_DebugEvent);
            toolBox.BatchConfigTemplate += new Action(toolBox_BatchConfigTemplate);
            toolBox.BatchDelAccount += new Action(toolBox_BatchDelAccount);
            toolBox.BatchActiveAccount += new Action(toolBox_BatchActiveAccount);
            toolBox.BatchInActiveAccount += new Action(toolBox_BatchInActiveAccount);
            toolBox.BatchFlatPosition += new Action(toolBox_BatchFlatPosition);
        
        }

        
        void OnFilterArgsChanged(FilterArgs obj)
        {
            filterArgs = obj;
            FilterAccount();
        }

        void toolBox_DebugEvent()
        {
            int accCnt = accountmap.Count;
            int rowCnt = accountrowmap.Count;
            int tableRowCnt = gt.Rows.Count;
            int gridCnt = accountgrid.RowCount;


            MessageBox.Show(string.Format("账户:{0} 行:{1} 表:{2} 表格显示:{3} 过滤:{4}", accCnt, rowCnt, tableRowCnt, gridCnt, datasource.Filter));
            datasource.Filter = "";
        }

        void toolBox_BatchFlatPosition()
        {
            if (MoniterHelper.WindowConfirm("批量强平选中账户?") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqFlatAllPosition(this.AccountsSelected.ToArray());
            }
        }

        void toolBox_BatchInActiveAccount()
        {
            if (MoniterHelper.WindowConfirm("批量冻结选中账户?") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountExecute(this.AccountsSelected.ToArray(), false);
            }
        }

        void toolBox_BatchActiveAccount()
        {
            if (MoniterHelper.WindowConfirm("批量激活选中账户?") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateAccountExecute(this.AccountsSelected.ToArray(), true);
            }
        }

        void toolBox_BatchDelAccount()
        {
            if (MoniterHelper.WindowConfirm("批量删除选中账户?") == DialogResult.Yes)
            {
                CoreService.TLClient.ReqDelAccount(this.AccountsSelected.ToArray());
            }
        }

        void toolBox_BatchConfigTemplate()
        {
            fmEditAccountConfigTemplate fm = new fmEditAccountConfigTemplate();
            fm.SetAccount(this.AccountsSelected.ToArray());
            fm.ShowDialog();
            fm.Close();
        }

    }
}
