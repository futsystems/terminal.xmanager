using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.TinyMGRControl
{
    


    public partial class ctAccountMontier
    {
        //FilterArgs filterArgs = ControlService.FilterArgs;

        
        #region 过滤帐户列表

        
        /// <summary>
        /// 帐户可交易选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accexecute_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshAccountQuery();
        }

        /// <summary>
        /// 是否登入选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void accLogin_CheckedChanged(object sender, EventArgs args)
        {
            RefreshAccountQuery();
        }

        void acctfilter_TextChanged(object sender, EventArgs e)
        {
            RefreshAccountQuery();
        }

        /// <summary>
        /// 刷新帐户筛选结果
        /// </summary>
        void RefreshAccountQuery()
        {
            
            
            string strFilter = string.Empty;

            strFilter = string.Format(DELETE + " ='{0}'", false);


            //帐户状态
            strFilter = string.Format(strFilter + " and " + EXECUTE + " = '{0}'", getExecuteStatus(true));
            
            //登入
            if (accLogin.Checked)
            {
                strFilter = string.Format(strFilter + " and " + LOGINSTATUS + " = '{0}'", getLoginStatus(true));
            }
            //帐户检索
            if (!string.IsNullOrEmpty(acctfilter.Text))
            {
                strFilter = string.Format(strFilter + " and " + ACCOUNT + " like '{0}*'", acctfilter.Text);
            }


            debug("strfilter:" + strFilter, QSEnumDebugLevel.INFO);
            datasource.Filter = strFilter;

            GridChanged();

        }

       
        #endregion
    }
}
