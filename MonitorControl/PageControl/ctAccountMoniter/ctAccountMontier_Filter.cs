using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterControl
{
    


    public partial class ctAccountMontier
    {
        //FilterArgs filterArgs = ControlService.FilterArgs;

        
        #region 过滤帐户列表

        /// <summary>
        /// 帐户类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void ctAccountType1_AccountTypeSelectedChangedEvent()
        //{
        //    RefreshAccountQuery();
        //}

        /// <summary>
        /// 路由类别选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void ctRouterType1_RouterTypeSelectedChangedEvent()
        //{
        //    RefreshAccountQuery();
        //}
        ///// <summary>
        ///// 帐户可交易选择
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void accexecute_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RefreshAccountQuery();
        //}

        ///// <summary>
        ///// 是否登入选择
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="args"></param>
        //private void accLogin_CheckedChanged(object sender, EventArgs args)
        //{
        //    RefreshAccountQuery();
        //}
        ///// <summary>
        ///// 单帐户查询框
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void acct_TextChanged(object sender, EventArgs e)
        //{
        //    RefreshAccountQuery();
        //}
        ///// <summary>
        ///// 代理列表选择
        ///// </summary>
        ////private void ctAgentList1_AgentSelectedChangedEvent()
        ////{
        ////    RefreshAccountQuery();
        ////}

        //private void acchodpos_CheckedChanged(object sender, EventArgs args)
        //{
        //    RefreshAccountQuery();
        //}
        //void ctRouterGroupList1_RouterGroupSelectedChangedEvent()
        //{
        //    RefreshAccountQuery();
        //}

        /// <summary>
        /// 刷新帐户筛选结果
        /// </summary>
        void FilterAccount(FilterArgs filterArgs)
        {
            string strFilter = string.Empty;

            string acctype = string.Empty;
            //if (!filterArgs.AcctTypeEnable)
            //{
                acctype = "*";
            //}
            //else
            //{
            //    acctype = filterArgs.AcctType.ToString();
            //}
            ////帐户类别
            //if (!filterArgs.AcctTypeEnable)
            //{
                strFilter = string.Format(CATEGORY + " > '{0}'", acctype);
            //}
            //else
            //{
                //strFilter = string.Format(CATEGORY + " = '{0}'", acctype);
            //}

            strFilter = string.Format(strFilter + " and " + DELETE + " ='{0}'", false);

            if (filterArgs != null)
            {
                //路由
                //if (filterArgs.OrderRouterTypeEnable)
                //{
                //    strFilter = string.Format(strFilter + " and " + ROUTE + " = '{0}'", filterArgs.OrderRouterType);
                //}

                //帐户冻结
                if (filterArgs.AccLock)
                {
                    strFilter = string.Format(strFilter + " and " + EXECUTE + " = '{0}'", getExecuteStatus(false));
                }

                ////路由组筛选
                //if (filterArgs.RouterGroupEnable)
                //{
                //    strFilter = string.Format(strFilter + " and " + ROUTERGROUP + " = '{0}'", filterArgs.RouterGroupID);
                //}

                //if (filterArgs.AgentEnable)
                //{
                //    strFilter = string.Format(strFilter + " and " + AGENTMGRFK + " = '{0}'", filterArgs.AgentID);
                //}


                //登入
                if (filterArgs.AccLogin)
                {
                    strFilter = string.Format(strFilter + " and " + LOGINSTATUS + " = '{0}'", getLoginStatus(true));
                }

                //持仓
                if (filterArgs.AccPos)
                {
                    strFilter = string.Format(strFilter + " and " + HOLDSIZE + " > 0");
                }
                //帐户检索
                if (!string.IsNullOrEmpty(filterArgs.AccSearch))
                {
                    strFilter = string.Format(strFilter + " and " + ACCOUNT + " like '{0}*'", filterArgs.AccSearch);
                }

            }

            logger.Info("strfilter:" + strFilter);
            datasource.Filter = strFilter;

            //更新帐户数目
            UpdateAccountNum();

            //订阅观察列表
            //if (Globals.EnvReady)
            {
                GridChanged();
            }
        }

        void UpdateAccountNum()
        {
            //num.Text = accountgrid.Rows.Count.ToString();
        }

        #endregion
    }
}
