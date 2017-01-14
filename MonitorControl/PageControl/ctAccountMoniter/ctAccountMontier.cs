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
        public ctAccountMontier()
        {
            try
            {
                InitializeComponent();
                SetPreferences();
                InitTable();
                BindToTable();
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
            FilterAccount(null);
        }


        void WireEvents()
        {
            //绑定表格菜单
            accountgrid.ContextMenuStrip = MenuService.CreateContextMenu(this, "/AccountList/ContextMenu");
            //表格事件
            accountgrid.CellDoubleClick +=new DataGridViewCellEventHandler(accountgrid_CellDoubleClick);//双击单元格
            accountgrid.SizeChanged +=new EventHandler(accountgrid_SizeChanged);
            accountgrid.SizeChanged += new EventHandler(accountgrid_SizeChanged_FixWidth);//大小改变
            accountgrid.Scroll +=new ScrollEventHandler(accountgrid_Scroll);//滚轮滚动
            //表格样式
            accountgrid.CellFormatting += new DataGridViewCellFormattingEventHandler(accountgrid_CellFormatting);//格式化单元格
            accountgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(accountgrid_RowPrePaint);

            //响应过滤参数变更事件
            ControlService.FilterArgsChanged += new Action<FilterArgs>(OnFilterArgsChanged);

            CoreService.EventCore.RegIEventHandler(this);
        }

        

        public void OnInit()
        {
            //加载帐户
            foreach (AccountItem account in CoreService.BasicInfoTracker.Accounts)
            {
                InvokeGotAccount(account);
            }

            UpdateAccountNum();

            //CoreService.EventHub.AccountStatisticNotify += new Action<AccountStatistic>(OnAccountStatisticNotify);
            //CoreService.EventHub.OnAccountChangedEvent += new Action<AccountItem>(OnAccountChanged);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_ACC_CHANGED, OnAccountChanged);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_ACC_STATISTIC, OnNotifyAccountStatistic);
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
            //启动更新线程
            StartUpdate();
        }



        public void OnDisposed()
        {
            //CoreService.EventHub.AccountStatisticNotify -= new Action<AccountStatistic>(OnAccountStatisticNotify);
            //CoreService.EventHub.OnAccountChangedEvent -= new Action<AccountItem>(OnAccountChanged);
        }

        void OnNotifyAccountStatistic(string json)
        {
            AccountStatistic item = CoreService.ParseJsonResponse<AccountStatistic>(json);
            if (item != null)
            {
                accountinfocache.Write(item);
            }
            
        }
        void OnAccountStatisticNotify(AccountStatistic obj)
        {
            accountinfocache.Write(obj);
        }

        /// <summary>
        /// 响应帐户变动事件
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

        void OnFilterArgsChanged(FilterArgs obj)
        {
            FilterAccount(obj);
        }
    }
}
