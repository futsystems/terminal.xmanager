using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using Common.Logging;
using ICSharpCode.Core;

namespace TradingLib.MoniterControl
{
    [MoniterControlAttr("AccountMoniter","分帐户列表",EnumControlLocation.TopPanel)]
    public partial class ctAccountMontier : UserControl, IEventBinder, IMoniterControl
    {
        ILog logger = LogManager.GetLogger("AccountMontier");
        public Control FilterToolBar { get; set; }
        
        ConfigFile config;

        public ctAccountMontier()
        {
            try
            {
                config = ConfigFile.GetConfigFile("moniter.cfg");
                InitializeComponent();

                InitGridView();
                InitToolBox();
                
                this.Load += new EventHandler(ctAccountMontier_Load);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error ex:" + ex.ToString());
            }
            
        }

        void ctAccountMontier_Load(object sender, EventArgs e)
        {
            WireEvents();

            FilterAccount();
        }


        void WireEvents()
        {
            //绑定表格菜单
            accountgrid.ContextMenuStrip = MenuService.CreateContextMenu(this, "/AccountList/ContextMenu");
            //表格事件
            accountgrid.CellDoubleClick += new DataGridViewCellEventHandler(accountgrid_CellDoubleClick);//双击单元格
            accountgrid.SizeChanged += new EventHandler(accountgrid_SizeChanged);
            accountgrid.SizeChanged += new EventHandler(accountgrid_SizeChanged_FixWidth);//大小改变
            accountgrid.Scroll += new ScrollEventHandler(accountgrid_Scroll);//滚轮滚动
            //表格样式
            accountgrid.CellFormatting += new DataGridViewCellFormattingEventHandler(accountgrid_CellFormatting);//格式化单元格
            accountgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(accountgrid_RowPrePaint);

            CoreService.EventCore.RegIEventHandler(this);
        }

       
        /// <summary>
        /// 选中账户编号列表
        /// </summary>
        public List<string> AccountsSelected
        {
            get
            {
                List<string> list = new List<string>();
                int rowcnt = accountgrid.SelectedRows.Count;

                for (int i = 0; i < rowcnt; i++)
                {
                    list.Add(accountgrid[0, accountgrid.SelectedRows[i].Index].Value.ToString());
                }

                return list;
            }
        }

        

       



        

        public void OnInit()
        {
            //根据角色隐藏表格相关列
            if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
            {
                //只有管理员可以查看路由类别
                accountgrid.Columns[ROUTEIMG].Visible = CoreService.SiteInfo.Manager.IsRoot();
                //管理员可以查看帐户类别
                //accountgrid.Columns[CATEGORYSTR].Visible = CoreService.SiteInfo.Manager.IsRoot();
                //如果有实盘交易权限则可以查看路由组
                accountgrid.Columns[ROUTERGROUPSTR].Visible = CoreService.SiteInfo.Manager.IsRoot();

                CounterMoniterWidth();
            }


            //有代理模块 显示左侧树状菜单
            if (CoreService.SiteInfo.Domain.Module_Agent)
            {
                //初始化左侧树状菜单
                agentTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(agentTree_NodeMouseClick);
                InitTreeMenu();

                splitContainer.Panel1Collapsed = false;
                splitContainer.SplitterWidth = 5;
                splitContainer.SplitterDistance = 120;
            }
            else
            {
                splitContainer.Panel1Collapsed = true;
                splitContainer.SplitterWidth = 0;
                ctAgentSummary.Visible = false;
                accountgrid.Dock = DockStyle.Fill;
            }


            //加载帐户
            foreach (AccountItem account in CoreService.BasicInfoTracker.Accounts.ToArray())
            {
                //accountcache.Write(account);
                InvokeGotAccount(account);
            }
            //加载帐户
            foreach (AccountItem account in CoreService.BasicInfoTracker.Accounts.ToArray())
            {
                //accountcache.Write(account);
                InvokeGotAccount(account);
            }
            
            

            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_ACC_CHANGED, OnAccountChanged);//交易账户参数变化
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_ACC_STATISTIC, OnNotifyAccountStatistic);//交易账户统计
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_AGENT_CREATE, OnNotifyAgentCreate);//代理账户添加
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_MANGER_DELETE, OnManagerDelete);//管理账户删除

            UpdateAccountNum();

            //启动更新线程
            //StartUpdate();

            //初始化后自动设定到当前顶级管理员
            if (CoreService.SiteInfo.Agent != null)
            {
                ctAgentSummary.SetAgent(CoreService.SiteInfo.Manager, CoreService.SiteInfo.Agent);
                CoreService.TLClient.ReqWatchAgents(new string[] { CoreService.SiteInfo.Agent.Account });
            }
            
        }

        




     
        

        


        public void OnDisposed()
        {
        

        }

        /// <summary>
        /// 账户统计更新
        /// </summary>
        /// <param name="json"></param>
        void OnNotifyAccountStatistic(string json)
        {
            AccountStatistic item = CoreService.ParseJsonResponse<AccountStatistic>(json);
            if (item != null)
            {
                accountinfocache.Write(item);
            }
            
        }


        /// <summary>
        /// 帐户变动
        /// </summary>
        /// <param name="account"></param>
        public void OnAccountChanged(string json)
        {
            AccountItem item = CoreService.ParseJsonResponse<AccountItem>(json);
            if (item != null)
            {
                accountcache.Write(item);
            }
        }


        /// <summary>
        /// grid滚动 重新设定观察列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accountgrid_Scroll(object sender, ScrollEventArgs e)
        {
            GridChanged();
        }

        /// <summary>
        /// grid尺寸变化 重新设定观察列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accountgrid_SizeChanged(object sender, EventArgs e)
        {
            GridChanged();
        }

        /// <summary>
        /// 格式化表格盈亏颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accountgrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 11)
                {
                    e.CellStyle.Font = UIConstant.BoldFont;
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
                if (e.ColumnIndex == 0)
                {
                    var account = accountgrid[TAG, e.RowIndex].Value as AccountItem;
                    if (account.Deleted)
                    {
                        e.CellStyle.Font = new Font("雅黑", 9, FontStyle.Strikeout);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("cell formatting err:"+ex.ToString());
            }
        }

        /// <summary>
        /// 禁止绘制单元格虚线选中框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void accountgrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }


        DateTime _lastresumetime = DateTime.Now;
        /// <summary>
        /// 双击选择交易账户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            AccountItem account = this.CurrentAccount;
            if (account != null)
            {
                //触发事件中继的帐户选择事件
                ControlService.FireAccountSelected(account);
            }
        }
        

        
    }





    internal class MenuItem
    {
        public MenuItem(int id,int parent,AgentSetting  agent,ManagerSetting manager)
        {
            this.MGR_ID = id;
            this.Manager = manager;
            this.Parent_ID = parent;
            this.AgentAccount = agent;
            this.SubAgents = new List<MenuItem>();
            this.Title = string.Format("{0}({1})-{2}", manager.Login,Util.GetEnumDescription(agent.AgentType).Substring(0,2),manager.mgr_fk);
            if (manager.GetBaseMGR() == CoreService.SiteInfo.Manager.GetBaseMGR())
            {
                this.Title = string.Format("{0}-{1}", manager.Login, manager.mgr_fk);
            }
        }

        

        public int Parent_ID { get; set; }

        /// <summary>
        /// 管理域编号
        /// </summary>
        public int MGR_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ManagerSetting Manager { get; set; }

        public string Title { get; private set; }
        /// <summary>
        /// 管理域财务账户ID
        /// </summary>
        public AgentSetting AgentAccount { get; set; }


        public List<MenuItem> SubAgents { get; set; }
    }
}
