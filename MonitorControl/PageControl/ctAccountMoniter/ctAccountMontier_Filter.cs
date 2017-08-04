using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public partial class ctAccountMontier
    {
        /// <summary>
        /// 过滤账户
        /// </summary>
        /// <param name="filterArgs"></param>
        void FilterAccount()
        {
            string strFilter = ACCOUNT + " like '*'";

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

                if (filterArgs.AcctTypeEnable)
                {
                    strFilter = string.Format(strFilter + " and " + CATEGORY + " = '{0}'", filterArgs.AcctType);
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

                //配置检索
                if (!string.IsNullOrEmpty(filterArgs.ConfigNameSearch))
                {
                    strFilter = string.Format(strFilter + " and " + CFGNAME + " like '{0}*'", filterArgs.ConfigNameSearch);
                }
                
                //备注检索
                if (!string.IsNullOrEmpty(filterArgs.MemoSearch))
                {
                    strFilter = string.Format(strFilter + " and " + MEMO + " like '{0}*'", filterArgs.MemoSearch);
                }

            }

            if (!(_managerSelected == null || _managerSelected.mgr_fk == CoreService.SiteInfo.Manager.mgr_fk))
            {
                strFilter = string.Format(strFilter + " and " + AGENTMGRFK + " = '{0}'", _managerSelected.mgr_fk);
            }

            datasource.Filter = strFilter;
            UpdateAccountNum();
            GridChanged();
        }

        void UpdateAccountNum()
        {
            filterBox.SetAccountNum(accountgrid.Rows.Count);
        }

    }
}
