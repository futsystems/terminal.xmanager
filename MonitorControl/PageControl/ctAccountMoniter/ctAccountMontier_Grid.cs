using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    /// <summary>
    /// 更新交易帐户的信息
    /// 替换Grid性能大幅提高，后期会对控件做一个整理
    /// </summary>
    public partial class ctAccountMontier
    {
        #region 表格

        const string ACCOUNT = "帐户编号";
        const string NAME = "客户姓名";
        const string LASTEQUITY = "昨日权益";
        const string NOWEQUITY = "当前权益";
        const string CREDIT = "优先资金";
        const string TOTALEQUITY = "总权益";
        const string MARGIN = "保证金";
        const string FROZENMARGIN = "冻结保证金";
        const string REALIZEDPL = "平仓盈亏";
        const string UNREALIZEDPL = "浮动盈亏";
        const string COMMISSION = "手续费";
        const string PROFIT = "净利";
        const string HOLDSIZE = "持仓";
        const string CATEGORYSTR = "帐户类型";
        const string CATEGORY = "CATEGORY";
        const string HOLDNIGHT = "隔夜";
        const string ROUTERGROUP = "Group";
        const string ROUTERGROUPSTR = "主帐户组";
        const string LOGINSTATUS = "LoginStatus";
        const string LOGINSTATUSIMG = "登入";
        const string ROUTE = "RouteType";
        const string ROUTEIMG = "成交";
        const string WARN = "警告";
        const string WARNSTR = "警告消息";
        const string EXECUTE = "ExecuteStatus";
        const string EXECUTEIMG = "状态";
        const string CURRENCY = "基币";
        const string AGENTMGRFK = "AGENTMGRFK";
        const string AGENTCODE = "代理编号";
        const string MEMO = "备注";
        const string DELETE = "DELETE";
        const string TAG = "ACCOUNTTAG";
        const string TAG2 = "ACCOUNTTAG2";

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();




        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = accountgrid;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.ColumnHeadersHeight = 25;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.StateCommon.Background.Color1 = Color.WhiteSmoke;
            grid.StateCommon.Background.Color2 = Color.WhiteSmoke;

        }

        //初始化Account显示空格
        private void InitTable()
        {
            
            gt.Columns.Add(ACCOUNT);//0
            gt.Columns.Add(NAME);//1

            gt.Columns.Add(LASTEQUITY);//2
            gt.Columns.Add(NOWEQUITY);//3
            gt.Columns.Add(CREDIT);//4
            gt.Columns.Add(TOTALEQUITY);//5
            gt.Columns.Add(MARGIN);//6
            gt.Columns.Add(FROZENMARGIN);//7
            gt.Columns.Add(REALIZEDPL);//8
            gt.Columns.Add(UNREALIZEDPL);//9
            //gt.Columns.Add(STKMARKETVALIE);//
            gt.Columns.Add(COMMISSION);//10
            gt.Columns.Add(PROFIT);//11
            gt.Columns.Add(HOLDSIZE);//
            //帐户类别
            gt.Columns.Add(CATEGORY);//18
            gt.Columns.Add(CATEGORYSTR);

            //日内属性
            gt.Columns.Add(HOLDNIGHT);//19

            gt.Columns.Add(ROUTERGROUP);
            gt.Columns.Add(ROUTERGROUPSTR);

            gt.Columns.Add(LOGINSTATUS);//6
            gt.Columns.Add(LOGINSTATUSIMG, typeof(Image));//7

            gt.Columns.Add(ROUTE);//1
            gt.Columns.Add(ROUTEIMG, typeof(Image));//2

            //警告信息
            gt.Columns.Add(WARN);
            gt.Columns.Add(WARNSTR);

            //交易权限
            gt.Columns.Add(EXECUTE);//3
            gt.Columns.Add(EXECUTEIMG, typeof(Image));//4
            gt.Columns.Add(CURRENCY);

            //代理编号
            gt.Columns.Add(AGENTCODE);//20
            gt.Columns.Add(AGENTMGRFK);//21
            gt.Columns.Add(MEMO);
            gt.Columns.Add(TAG,typeof(AccountItem));
            gt.Columns.Add(TAG2, typeof(AccountStatistic));
            gt.Columns.Add(DELETE);
            
            
        }
        
        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            datasource.DataSource = gt;
            datasource.Sort = ACCOUNT + " ASC";
            accountgrid.DataSource = datasource;

            accountgrid.Columns[CATEGORY].Visible = false;
            accountgrid.Columns[CATEGORYSTR].Visible = false;
            accountgrid.Columns[AGENTMGRFK].Visible = false;
            accountgrid.Columns[LOGINSTATUS].Visible = false;
            accountgrid.Columns[ROUTE].Visible = false;
            accountgrid.Columns[ROUTERGROUP].Visible = false;
            accountgrid.Columns[WARN].Visible = false;
            accountgrid.Columns[EXECUTE].Visible = false;
            accountgrid.Columns[TAG].Visible = false;
            accountgrid.Columns[TAG2].Visible = false;
            accountgrid.Columns[DELETE].Visible = false;

            for (int i = 0; i < gt.Columns.Count; i++)
            {
                accountgrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void CounterMoniterWidth()
        {
            accountgrid.Columns[ACCOUNT].Width = 100;
            accountgrid.Columns[NAME].Width = 80;
            accountgrid.Columns[EXECUTEIMG].Width = 30;
            accountgrid.Columns[HOLDSIZE].Width = 40;

            accountgrid.Columns[HOLDNIGHT].Width = 50;

            accountgrid.Columns[ROUTEIMG].Width = 50;
            accountgrid.Columns[LOGINSTATUSIMG].Width = 50;
            accountgrid.Columns[EXECUTEIMG].Width = 50;
            accountgrid.Columns[CURRENCY].Width = 50;
            accountgrid.Columns[AGENTCODE].Width = 80;
        }

        private void accountgrid_SizeChanged_FixWidth(object sender, EventArgs e)
        {
            if (CoreService.BasicInfoTracker.Initialized)
            {
                if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
                {
                    CounterMoniterWidth();
                }
            }
            
        }
        #endregion

        public int VisualAccountCount { get { return accountgrid.RowCount; } }
        #region 帐户内存数据结构
        private ConcurrentDictionary<string, AccountItem> accountmap = new ConcurrentDictionary<string, AccountItem>();
        private ConcurrentDictionary<string, int> accountrowmap = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// 获得某个账户在datatable中的序号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private int accountIdx(string account)
        {
            int rowid = -1;
            if (accountrowmap.TryGetValue(account, out rowid))
                return rowid;
            else
                return -1;
        }

        /// <summary>
        /// 当前交易账户
        /// </summary>
        public AccountItem CurrentAccount
        {
            get
            {
                int row = (accountgrid.SelectedRows.Count > 0 ? accountgrid.SelectedRows[0].Index : -1);
                return row==-1?null:(accountgrid[TAG, row].Value as AccountItem);
            }
        }

        #endregion

        #region 更新交易账户信息



        #region 根据账户属性获得对应的string 或者 image
        Image getRouteStatusImage(QSEnumOrderTransferType type)
        {
            return type == QSEnumOrderTransferType.SIM ? (Image)Properties.Resources.router_sim : (Image)Properties.Resources.router_real;

        }
        Image getExecuteStatusImage(bool execute)
        {
            if (execute)
                return (Image)Properties.Resources.account_go;
            else
                return (Image)Properties.Resources.account_stop;

        }
        string getExecuteStatus(bool execute)
        {
            return execute ? "execute" : "noexecute";
        }
        Image getLoginStatusImage(bool online)
        {
            if (online)
                return (Image)Properties.Resources.online;
            else
                return (Image)Properties.Resources.offline;
        }
        string getLoginStatus(bool online)
        {
            return online ? "online" : "offline";
        }

        Image getProfitLossImage(decimal profit)
        {
            if (profit > 0)
                return (Image)Properties.Resources.profit;
            if (profit < 0)
                return (Image)Properties.Resources.loss;
            return (Image)Properties.Resources.no_profit_loss;
        }

        Image getMAcctConnectImg(bool connected)
        {
            return (Image)(connected ? Properties.Resources.connect : Properties.Resources.disconnect);
        }
        #endregion



        bool _accountgo = false;
        Thread _accountthread = null;
        const int bufferisze = 1000;
        RingBuffer<AccountItem> accountcache = new RingBuffer<AccountItem>(bufferisze);//交易帐户缓存
        RingBuffer<AccountStatistic> accountinfocache = new RingBuffer<AccountStatistic>(bufferisze);//交易帐户财务数据更新缓存

        void acccountproc()
        {
            while (_accountgo)
            {
                try
                {
                    //更新账户主体信息
                    while (accountcache.hasItems)
                    {
                        AccountItem account = accountcache.Read();
                        InvokeGotAccount(account);
                        //如果在初始化之后获得AccountItem信息 则表明该帐户是新增造成的 需要重新watchaccount
                        if (CoreService.BasicInfoTracker.Initialized)
                        {
                            GridChanged();
                        }
                        Thread.Sleep(1);
                    }
                    //更新账户盘中动态财务信息
                    while (!accountcache.hasItems && accountinfocache.hasItems)
                    {
                        InvokeGotAccountInfoLite(accountinfocache.Read());
                        Thread.Sleep(1);
                    }
                    //设置观察帐户列表
                    SetWathAccounts();

                    //闪烁警告
                    InvokeFlashAccountWarn();
                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    logger.Error("Account Info Process Error:" + ex.ToString());
                }
            }
        }

        void StartUpdate()
        {
            if (_accountgo) return;
            _accountgo = true;
            _accountthread = new Thread(acccountproc);
            _accountthread.IsBackground = true;
            _accountthread.Start();
        }

        void StopUpdate()
        {
            if (!_accountgo) return;
            _accountgo = false;
            _accountthread.Join();
        }

        const string EMPTY_NUM = "0.00";
        void InvokeGotAccount(AccountItem account)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AccountItem>(InvokeGotAccount), new object[] { account });
            }
            else
            {
                try
                {
                    int r = accountIdx(account.Account);
                    if (r == -1)
                    {
                        gt.Rows.Add(account.Account);
                        int i = gt.Rows.Count - 1;

                        gt.Rows[i][ACCOUNT] = account.Account;
                        gt.Rows[i][NAME] = account.Name;

                        gt.Rows[i][LASTEQUITY] = account.LastEquity.ToFormatStr();
                        gt.Rows[i][NOWEQUITY] = account.NowEquity.ToFormatStr();
                        gt.Rows[i][CREDIT] = account.Credit.ToFormatStr();
                        gt.Rows[i][TOTALEQUITY] = (account.NowEquity + account.Credit).ToFormatStr();

                        gt.Rows[i][MARGIN] = EMPTY_NUM;
                        gt.Rows[i][FROZENMARGIN] = EMPTY_NUM;
                        gt.Rows[i][REALIZEDPL] = EMPTY_NUM;
                        gt.Rows[i][UNREALIZEDPL] = EMPTY_NUM;
                        //gt.Rows[i][STKMARKETVALIE] = decDisp(0);
                        gt.Rows[i][COMMISSION] = EMPTY_NUM;
                        gt.Rows[i][PROFIT] = EMPTY_NUM;
                        gt.Rows[i][HOLDSIZE] = 0;
                        gt.Rows[i][CATEGORY] = account.Category.ToString();
                        gt.Rows[i][CATEGORYSTR] = Util.GetEnumDescription(account.Category);

                        gt.Rows[i][HOLDNIGHT] = account.IntraDay ? "禁止" : "允许";

                        gt.Rows[i][AGENTMGRFK] = account.MGRID;
                        ManagerSetting mgr = CoreService.BasicInfoTracker.GetManager(account.MGRID);
                        gt.Rows[i][AGENTCODE] = string.Format("{0:d2}-{1}",mgr.ID, mgr.Login);

                        //分帐户登入 路由组 路由标识
                        if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
                        {
                            gt.Rows[i][LOGINSTATUS] = getLoginStatus(account.IsLogin);
                            gt.Rows[i][LOGINSTATUSIMG] = getLoginStatusImage(account.IsLogin);

                            gt.Rows[i][ROUTE] = account.OrderRouteType.ToString();
                            gt.Rows[i][ROUTEIMG] = getRouteStatusImage(account.OrderRouteType);

                            gt.Rows[i][ROUTERGROUP] = account.RG_ID;
                            RouterGroupSetting rg = CoreService.BasicInfoTracker.GetRouterGroup(account.RG_ID);
                            gt.Rows[i][ROUTERGROUPSTR] = rg != null ? rg.Name : "";

                        }
                        
                        gt.Rows[i][WARN] = account.IsWarn;
                        gt.Rows[i][WARNSTR] = account.WarnMessage;

                        gt.Rows[i][EXECUTE] = getExecuteStatus(account.Execute);
                        gt.Rows[i][EXECUTEIMG] = getExecuteStatusImage(account.Execute);

                        gt.Rows[i][DELETE] = account.Deleted;
                        gt.Rows[i][CURRENCY] = Util.GetEnumDescription(account.Currency);
                        gt.Rows[i][TAG] = account;
                        gt.Rows[i][MEMO] = account.Memo;
                        accountmap.TryAdd(account.Account, account);
                        accountrowmap.TryAdd(account.Account, i);
                       
                    }
                    else //如果存在表面是进行修改
                    {
                        accountmap[account.Account] = account;//直接更新帐户对象 这里并没有通过字段进行修改原始对象
                        gt.Rows[r][TAG] = account;

                        gt.Rows[r][LASTEQUITY] = account.LastEquity.ToFormatStr();
                       
                        gt.Rows[r][ROUTE] = account.OrderRouteType.ToString();
                        gt.Rows[r][ROUTEIMG] = getRouteStatusImage(account.OrderRouteType);
                        gt.Rows[r][EXECUTE] = getExecuteStatus(account.Execute);
                        gt.Rows[r][EXECUTEIMG] = getExecuteStatusImage(account.Execute);
                        gt.Rows[r][CATEGORYSTR] = Util.GetEnumDescription(account.Category);
                        gt.Rows[r][CATEGORY] = account.Category.ToString();
                        gt.Rows[r][HOLDNIGHT] = account.IntraDay ? "禁止" : "允许";

                        ManagerSetting mgr = CoreService.BasicInfoTracker.GetManager(account.MGRID);
                        gt.Rows[r][AGENTCODE] = string.Format("{0:d2}-{1}", mgr.ID, mgr.Login);

                        gt.Rows[r][NAME] = account.Name;
                        gt.Rows[r][DELETE] = account.Deleted;
                        
                        gt.Rows[r][CURRENCY] = Util.GetEnumDescription(account.Currency);


                        gt.Rows[r][ROUTERGROUP] = account.RG_ID;
                        RouterGroupSetting rg = CoreService.BasicInfoTracker.GetRouterGroup(account.RG_ID);
                        gt.Rows[r][ROUTERGROUPSTR] = rg != null ? rg.Name : "";

                        gt.Rows[r][LOGINSTATUS] = getLoginStatus(account.IsLogin);
                        gt.Rows[r][LOGINSTATUSIMG] = getLoginStatusImage(account.IsLogin);



                        bool oldwarn = bool.Parse(gt.Rows[r][WARN].ToString());
                        gt.Rows[r][WARN] = account.IsWarn;
                        gt.Rows[r][WARNSTR] = account.WarnMessage;
                        gt.Rows[r][MEMO] = account.Memo;
                        if (oldwarn && !account.IsWarn)
                        {
                            InvokeAccountWarnOff(account.Account);
                        }

                        //如果删除帐户 则需要刷新帐户列表 防止没有任何选中帐户
                        if (account.Deleted)
                        {
                            if (accountgrid.RowCount > 0)
                            {
                                accountgrid.Rows[0].Selected = true;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    logger.Error("got account error:" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// 服务端推送的帐户实时财务数据
        /// </summary>
        /// <param name="account"></param>
        void InvokeGotAccountInfoLite(AccountStatistic notify)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AccountStatistic>(InvokeGotAccountInfoLite), new object[] { notify });
            }
            else
            {
                int r = accountIdx(notify.Account);
                if (r == -1)
                    return;
                else
                {
                    gt.Rows[r][NOWEQUITY] = notify.NowEquity.ToFormatStr();
                    gt.Rows[r][CREDIT] = notify.Credit.ToFormatStr();
                    gt.Rows[r][TOTALEQUITY] = (notify.NowEquity + notify.Credit).ToFormatStr();
                    gt.Rows[r][MARGIN] = notify.Margin.ToFormatStr();
                    gt.Rows[r][FROZENMARGIN] = notify.ForzenMargin.ToFormatStr();
                    gt.Rows[r][REALIZEDPL] = notify.RealizedPL.ToFormatStr();
                    gt.Rows[r][UNREALIZEDPL] = notify.UnRealizedPL.ToFormatStr();
                    //gt.Rows[r][STKMARKETVALIE] = decDisp(account.StkPositoinValue);
                    gt.Rows[r][COMMISSION] = notify.Commission.ToFormatStr();
                    gt.Rows[r][PROFIT] = notify.Profit.ToFormatStr();
                    //gt.Rows[r][PROFITLOSSIMG] = getProfitLossImage(account.Profit);
                    gt.Rows[r][HOLDSIZE] = notify.TotalPositionSize;
                    gt.Rows[r][TAG2] = notify;
                }
            }
        }

        #endregion

    }
}
