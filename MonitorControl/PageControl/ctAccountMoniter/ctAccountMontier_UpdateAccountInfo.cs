﻿using System;
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
        const string ROUTE = "RouteType";
        const string ROUTEIMG = "成交";

        const string EXECUTE = "ExecuteStatus";
        const string EXECUTEIMG = "状态";
        //const string PROFITLOSSIMG = "盈/亏";

        const string LOGINSTATUS = "LoginStatus";
        const string LOGINSTATUSIMG = "登入";
        //const string ADDRESS = "地址";

        const string LASTEQUITY = "昨日权益";
        const string NOWEQUITY = "当前权益";
        const string CREDIT = "优先资金";
        const string TOTALEQUITY = "总权益";

        const string MARGIN = "保证金";
        const string FROZENMARGIN = "冻结保证金";
        const string CASH = "可用资金";
        const string BUYPOWER = "购买力";
        const string REALIZEDPL = "平仓盈亏";
        const string UNREALIZEDPL = "浮动盈亏";
        const string STKMARKETVALIE = "证券市值";
        const string COMMISSION = "手续费";
        const string PROFIT = "净利";
        const string HOLDSIZE = "持仓";


        const string CATEGORYSTR = "帐户类型";
        const string CATEGORY = "CATEGORY";
        const string RACEENTRYTIME = "参赛日期";
        const string RACEID = "比赛编号";
        const string RACESTATUS = "比赛状态";

        const string HOLDNIGHT = "隔夜";
        
        const string AGENTCODE = "代理编号";
        const string AGENTMGRFK = "AGENTMGRFK";


        const string NAME = "客户姓名";

        const string MACTCONNSTATUS = "监控状态标识";//主帐户是否处于连接状态
        const string MACTCONNIMG = "连接";//主帐户连接状态图像

        const string DELETE = "DELETE";
        const string ROUTERGROUP = "Group";
        const string ROUTERGROUPSTR = "主帐户组";
        const string MAINACCOUNT = "主帐户";
        const string MAINACCOUNTBINDED = "主帐户是否绑定";
        const string MAINACCTRISKRULE = "强平规则";
        const string WARN = "警告";
        const string WARNSTR = "警告消息";
        const string CURRENCY = "基币";


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
            gt.Columns.Add(STKMARKETVALIE);//
            gt.Columns.Add(COMMISSION);//10
            gt.Columns.Add(PROFIT);//11
            gt.Columns.Add(HOLDSIZE);//
            //帐户类别
            gt.Columns.Add(CATEGORY);//18
            gt.Columns.Add(CATEGORYSTR);

            //日内属性
            gt.Columns.Add(HOLDNIGHT);//19
           
            
            

            //----------------主帐户字段
            //主帐户
            gt.Columns.Add(MAINACCOUNT);//
            gt.Columns.Add(MAINACCOUNTBINDED);//是否绑定

            //主帐户强平规则
            gt.Columns.Add(MAINACCTRISKRULE);

            //主帐户连接
            gt.Columns.Add(MACTCONNSTATUS);
            gt.Columns.Add(MACTCONNIMG, typeof(Image));


            //---------------分帐户字段
            gt.Columns.Add(ROUTERGROUP);
            gt.Columns.Add(ROUTERGROUPSTR);

            gt.Columns.Add(LOGINSTATUS);//6
            gt.Columns.Add(LOGINSTATUSIMG, typeof(Image));//7
            //gt.Columns.Add(ADDRESS);//8

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
            accountgrid.Columns[AGENTMGRFK].Visible = false;

            accountgrid.Columns[MAINACCOUNTBINDED].Visible = false;
            accountgrid.Columns[MACTCONNSTATUS].Visible = false;


            accountgrid.Columns[LOGINSTATUS].Visible = false;
            accountgrid.Columns[ROUTE].Visible = false;
            accountgrid.Columns[ROUTERGROUP].Visible = false;


            accountgrid.Columns[WARN].Visible = false;
            //accountgrid.Columns[WARNSTR].Visible = false;

            accountgrid.Columns[EXECUTE].Visible = false;
            accountgrid.Columns[DELETE].Visible = false;


            
            for (int i = 0; i < gt.Columns.Count; i++)
            {
                accountgrid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void VendorMoniterWidth()
        {

            accountgrid.Columns[ACCOUNT].Width = 100;
            accountgrid.Columns[NAME].Width = 80;
            accountgrid.Columns[EXECUTEIMG].Width = 30;

            
            accountgrid.Columns[HOLDNIGHT].Width = 50;
            accountgrid.Columns[MAINACCOUNT].Width = 140;
            accountgrid.Columns[MACTCONNIMG].Width = 50;
            accountgrid.Columns[EXECUTEIMG].Width = 50;
            accountgrid.Columns[MAINACCTRISKRULE].Width = 120;
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
        }

        private void accountgrid_SizeChanged_FixWidth(object sender, EventArgs e)
        {
            if (CoreService.BasicInfoTracker.Initialized)
            {
                if (CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter)
                {
                    VendorMoniterWidth();
                }
                if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
                {
                    CounterMoniterWidth();
                }
            }
            
        }
        #endregion

        #region 帐户内存数据结构
        private ConcurrentDictionary<string, AccountLite> accountmap = new ConcurrentDictionary<string, AccountLite>();
        private ConcurrentDictionary<string, int> accountrowmap = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// 获得某个账户在datatable中的序号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private int accountIdx(string account)
        {
            int rowid = -1;
            //map没有account键 还是会给out赋值,因此这里需要用if进行判断 来的到正确的逻辑 否则一致会返回0 出错
            if (!accountrowmap.TryGetValue(account, out rowid))
                return -1;
            else
                return rowid;
        }

        /// <summary>
        /// 查询是否存在某个交易帐号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private bool HaveAccount(string account)
        {
            return (accountmap.ContainsKey(account));
        }

        AccountLite accountselected = null;
        public AccountLite AccountSetlected { get { return accountselected; } }

        //得到当前选择的交易帐户
        public string CurrentAccount
        {
            get
            {
                int row = (accountgrid.SelectedRows.Count > 0 ? accountgrid.SelectedRows[0].Index : -1);
                return row==-1?string.Empty:(accountgrid[0, row].Value.ToString());
            }
        }



        //通过行号得该行的Security
        AccountLite GetVisibleAccount(string account)
        {
            //MessageBox.Show("account:" + account + " haveaccount:" + HaveAccount(account).ToString());
            if (HaveAccount(account))
                return accountmap[account];
            return null;
        }
        #endregion







        #region 更新交易账户

        /// <summary>
        /// 检查帐户缓存中是否有帐号未填充
        /// 用于初始化登入时 等待所有帐户加载完毕
        /// </summary>
        public bool AnyAccountInCache
        {
            get
            {
                return accountcache.hasItems;
            }
        }

        /// <summary>
        /// 帐户数量
        /// 未删除帐户数量
        /// </summary>
        public int AccountCount
        {
            get
            {
                return accountmap.Values.Where(acc=>!acc.Deleted).Count();
            }
        }

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
        
        void acccountproc()
        {
            while (_accountgo)
            {
               // Globals.Debug("got account in cache*************************");
                try
                {
                    //更新账户主体信息
                    while (accountcache.hasItems)
                    {
                       // Globals.Debug("got account in cache*************************");
                        AccountLite account = accountcache.Read();
                        InvokeGotAccount(account);
                        UpdateAccountNum();
                        //如果在初始化之后获得AccountLite信息 则表明该帐户是新增造成的 需要重新watchaccount
                        if (CoreService.BasicInfoTracker.Initialized)
                        {
                            GridChanged();
                        }
                        Thread.Sleep(1);
                    }
                    //更新账户登入 或者 注销 状态
                    while (!accountcache.hasItems && sessionupdatecache.hasItems)
                    {
                        NotifyMGRSessionUpdateNotify notify = sessionupdatecache.Read();
                        InvokeGotMGRSessionUpdate(notify);
                        Thread.Sleep(20);
                    }
                    //更新账户盘中动态财务信息
                    while (!accountcache.hasItems && accountinfocache.hasItems)
                    {
                        //Globals.Debug("got account in cache*************************");
                        InvokeGotAccountInfoLite(accountinfocache.Read());
                        Thread.Sleep(1);
                    }
                    //设置观察帐户列表
                    SwtWathAccounts();

                    //闪烁警告
                    FlashAccountWarn();
                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    debug("Account信息更新错误:" + ex.ToString());
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
            _accountthread.Abort();
        }





        const int bufferisze = 1000;
        RingBuffer<AccountLite> accountcache = new RingBuffer<AccountLite>(bufferisze);//交易帐户缓存
        RingBuffer<AccountInfoLite> accountinfocache = new RingBuffer<AccountInfoLite>(bufferisze);//交易帐户财务数据更新缓存
        RingBuffer<NotifyMGRSessionUpdateNotify> sessionupdatecache = new RingBuffer<NotifyMGRSessionUpdateNotify>(bufferisze);//交易帐户session更新缓存


        /// <summary>
        /// 当有帐户新增或者初始化时调用
        /// </summary>
        /// <param name="account"></param>
        void InvokeGotAccount(AccountLite account)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<AccountLite>(InvokeGotAccount), new object[] { account });
            }
            else
            {
                try
                {
                    int r = accountIdx(account.Account);//管理端是以account为唯一键值,应该不会出现重复？？
                    if (r == -1)//datatable不存在该行，我们则增加该行
                    {
                        gt.Rows.Add(account.Account);
                        int i = gt.Rows.Count - 1;

                        gt.Rows[i][ACCOUNT] = account.Account;
                        gt.Rows[i][NAME] = account.Name;

                        gt.Rows[i][LASTEQUITY] = decDisp(account.LastEquity);
                        gt.Rows[i][NOWEQUITY] = decDisp(account.NowEquity);
                        gt.Rows[i][CREDIT] = decDisp(account.Credit);
                        gt.Rows[i][TOTALEQUITY] = decDisp(account.NowEquity + account.Credit);

                        gt.Rows[i][MARGIN] = decDisp(0);
                        gt.Rows[i][FROZENMARGIN] = decDisp(0);
                        gt.Rows[i][REALIZEDPL] = decDisp(0);
                        gt.Rows[i][UNREALIZEDPL] = decDisp(0);
                        gt.Rows[i][STKMARKETVALIE] = decDisp(0);
                        gt.Rows[i][COMMISSION] = decDisp(0);
                        gt.Rows[i][PROFIT] = decDisp(0);
                        gt.Rows[i][HOLDSIZE] = 0;
                        gt.Rows[i][CATEGORY] = account.Category.ToString();
                        gt.Rows[i][CATEGORYSTR] = Util.GetEnumDescription(account.Category);

                        gt.Rows[i][HOLDNIGHT] = account.IntraDay ? "禁止" : "允许";

                        gt.Rows[i][AGENTMGRFK] = account.MGRID;
                        ManagerSetting mgr = CoreService.BasicInfoTracker.GetManager(account.MGRID);
                        gt.Rows[i][AGENTCODE] = string.Format("{0:d2}-{1}",mgr.ID, mgr.Login);


                        //主帐户 主帐户风控规则 主帐户连接
                        if (CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter)
                        {
                            gt.Rows[i][MAINACCOUNT] = account.ConnectorToken;
                            gt.Rows[i][MAINACCOUNTBINDED] = !string.IsNullOrEmpty(account.ConnectorToken);

                            gt.Rows[i][MAINACCTRISKRULE] = account.MAcctRiskRule;

                            gt.Rows[i][MACTCONNSTATUS] = account.MAcctConnected;
                            gt.Rows[i][MACTCONNIMG] = getMAcctConnectImg(account.MAcctConnected);
                            
                        }

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

                        
                        gt.Rows[i][EXECUTE] = getExecuteStatus(account.Execute);
                        gt.Rows[i][EXECUTEIMG] = getExecuteStatusImage(account.Execute);

                        gt.Rows[i][DELETE] = account.Deleted;
                        
                        gt.Rows[i][WARN] = account.IsWarn;
                        gt.Rows[i][WARNSTR] = account.WarnMessage;
                        gt.Rows[i][CURRENCY] = Util.GetEnumDescription(account.Currency);


                        accountmap.TryAdd(account.Account, account);
                        accountrowmap.TryAdd(account.Account, i);
                       
                    }
                    else //如果存在表面是进行修改
                    {
                        accountmap[account.Account] = account;//直接更新帐户对象 这里并没有通过字段进行修改原始对象

                        gt.Rows[r][LASTEQUITY] = decDisp(account.LastEquity);
                        //gt.Rows[i][NOWEQUITY] = decDisp(account.NowEquity);
                        //gt.Rows[i][CREDIT] = decDisp(account.Credit);
                        //gt.Rows[i][TOTALEQUITY] = decDisp(account.NowEquity + account.Credit);

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

                        if (CoreService.TLClient.ServerVersion.ProductType == QSEnumProductType.VendorMoniter)
                        {
                            gt.Rows[r][MAINACCOUNT] = account.ConnectorToken;
                            gt.Rows[r][MAINACCOUNTBINDED] = !string.IsNullOrEmpty(account.ConnectorToken);

                            gt.Rows[r][MACTCONNSTATUS] = account.MAcctConnected;
                            gt.Rows[r][MACTCONNIMG] = getMAcctConnectImg(account.MAcctConnected);
                            gt.Rows[r][MAINACCTRISKRULE] = account.MAcctRiskRule;
                        }

                        bool oldwarn = bool.Parse(gt.Rows[r][WARN].ToString());
                        gt.Rows[r][WARN] = account.IsWarn;
                        gt.Rows[r][WARNSTR] = account.WarnMessage;
                        if (oldwarn && !account.IsWarn)
                        {
                            AccountWarnOff(account.Account);
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
                    debug("got account error:" + ex.ToString(), QSEnumDebugLevel.ERROR);
                }
            }
        }



        /// <summary>
        /// 服务端推送的帐户实时财务数据
        /// </summary>
        /// <param name="account"></param>
        delegate void IAccountInfoLiteDel(AccountInfoLite account);
        void InvokeGotAccountInfoLite(AccountInfoLite account)
        {
            if (InvokeRequired)
            {
                Invoke(new IAccountInfoLiteDel(InvokeGotAccountInfoLite), new object[] { account });
            }
            else
            {
                string acc = account.Account;
                int r = accountIdx(acc);
                //Globals.Debug("account idx:" + r);
                if (r == -1)
                    return;
                else
                {
                    //Globals.Debug("account:"+account.Account + "now:" + decDisp(account.NowEquity) + " margin:" + decDisp(account.Margin));
                    gt.Rows[r][NOWEQUITY] = decDisp(account.NowEquity);
                    gt.Rows[r][CREDIT] = decDisp(account.Credit);
                    gt.Rows[r][TOTALEQUITY] = decDisp(account.NowEquity + account.Credit);
                    gt.Rows[r][MARGIN] = decDisp(account.Margin);
                    gt.Rows[r][FROZENMARGIN] = decDisp(account.ForzenMargin);
                    gt.Rows[r][REALIZEDPL] = decDisp(account.RealizedPL);
                    gt.Rows[r][UNREALIZEDPL] = decDisp(account.UnRealizedPL);
                    gt.Rows[r][STKMARKETVALIE] = decDisp(account.StkPositoinValue);
                    gt.Rows[r][COMMISSION] = decDisp(account.Commission);
                    gt.Rows[r][PROFIT] = decDisp(account.Profit);
                    //gt.Rows[r][PROFITLOSSIMG] = getProfitLossImage(account.Profit);
                    gt.Rows[r][HOLDSIZE] = account.TotalPositionSize;
                }
            }
        }

        /// <summary>
        /// 登入登出状态更新
        /// </summary>
        /// <param name="notify"></param>
        delegate void MGRSessionUpdateDel(NotifyMGRSessionUpdateNotify notify);
        void InvokeGotMGRSessionUpdate(NotifyMGRSessionUpdateNotify notify)
        {
            if (InvokeRequired)
            {
                Invoke(new MGRSessionUpdateDel(InvokeGotMGRSessionUpdate), new object[] { notify });
            }
            else
            {

                string acc = notify.TradingAccount;
                int i = accountIdx(acc);
                //debug("got sessionupdate info account:" + notify.TradingAccount + " status:" + notify.IsLogin.ToString() + " rindex:" + i.ToString(), QSEnumDebugLevel.INFO);
                if (i == -1)
                    return;
                else
                {
                    gt.Rows[i][LOGINSTATUS] = getLoginStatus(notify.IsLogin);
                    gt.Rows[i][LOGINSTATUSIMG] = getLoginStatusImage(notify.IsLogin);
                    //if (notify.IsLogin)
                    //{
                    //    gt.Rows[i][ADDRESS] = notify.IPAddress;
                    //}
                    //else
                    //{
                    //    gt.Rows[i][ADDRESS] = "";
                    //}
                }
            }
        }
        #endregion


        private void accountgrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 11)
            {
                e.CellStyle.Font =  UIConstant.BoldFont;
                decimal v = 0;
                decimal.TryParse(e.Value.ToString(), out v);
                if (v > 0)
                {
                    e.CellStyle.ForeColor = UIConstant.LongSideColor;
                }
                else if (v < 0)
                {
                    e.CellStyle.ForeColor = UIConstant.ShortSideColor;
                }
                else if (v == 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.Black;
                    
                }


            }
        }

        void accountgrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

    }
}
