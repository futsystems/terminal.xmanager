using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.Mixins.Json;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public partial class ctAccountMontier
    {

        public void OnInit()
        {
            //加载帐户
            foreach (AccountLite account in CoreService.BasicInfoTracker.Accounts)
            {
                InvokeGotAccount(account);
            }
            //更新帐户数目
            UpdateAccountNum();

            //帐户事件

            CoreService.EventAccount.OnNewAccountEvent += new Action<AccountLite>(GotAccount);
            CoreService.EventAccount.OnInfoLiteEvent += new Action<AccountInfoLite>(GotAccountInfoLite);
            CoreService.EventAccount.OnAccountChangedEvent += new Action<AccountLite>(GotAccountChanged);
            CoreService.EventAccount.OnSessionUpdateEvent += new Action<NotifyMGRSessionUpdateNotify>(GotSessionUpdate);
            

            if(CoreService.TLClient.ServerVersion.ProductType == QSEnumProductType.CounterSystem)
            {
                //只有管理员可以查看路由类别
                accountgrid.Columns[ROUTEIMG].Visible = CoreService.SiteInfo.Manager.IsRoot();
                //管理员可以查看帐户类别
                accountgrid.Columns[CATEGORYSTR].Visible = CoreService.SiteInfo.Manager.IsRoot();

                //如果有实盘交易权限则可以查看路由组
                accountgrid.Columns[ROUTERGROUPSTR].Visible = CoreService.SiteInfo.Domain.Router_Live && CoreService.SiteInfo.Manager.IsRoot();


                accountgrid.Columns[MAINACCOUNT].Visible = false;


                ////只有管理员可以修改路由组和删除交易帐户
                //if (!CoreService.SiteInfo.Manager.IsRoot())
                //{
                //    accountgrid.ContextMenuStrip.Items[4].Visible = false;

                //    accountgrid.ContextMenuStrip.Items[8].Visible = false;
                //    accountgrid.ContextMenuStrip.Items[9].Visible = false;
                //    accountgrid.ContextMenuStrip.Items[11].Visible = false;
                //    accountgrid.ContextMenuStrip.Items[12].Visible = false;
                //}
                //accountgrid.ContextMenuStrip.Items[10].Visible = false;


            }
            //主帐户监控
            if (CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter)
            {
                //隐藏帐户过滤窗口以及其他过滤控件
                //btnAcctFilter.Visible = false;
                //accLogin.Visible = false;
                //acchodpos.Visible = false;
                //btnAddAccount.Text = "添加配资客户";
                //btnAddAccount.Width = 100;
                //btnAddAccount.Location = new System.Drawing.Point(btnAddAccount.Location.X - 40, btnAddAccount.Location.Y);

                //accountgrid.Columns[ACCOUNT].Visible = false;
                accountgrid.Columns[ROUTEIMG].Visible = false;
                accountgrid.Columns[PROFITLOSSIMG].Visible = false;//盈亏标识
                accountgrid.Columns[LOGINSTATUSIMG].Visible = false;//登入标识
                accountgrid.Columns[ADDRESS].Visible = false;//终端地址
                accountgrid.Columns[CATEGORYSTR].Visible = false;
                accountgrid.Columns[ROUTERGROUPSTR].Visible = false;
                accountgrid.Columns[HOLDSIZE].Visible = false;
                accountgrid.Columns[AGENTCODE].Visible = false;

                //调整宽度
                VendorMoniterWidth();
            }

            //根据产品类别来调整界面

            //启动更新线程
            StartUpdate();
        }

        public void OnDisposed()
        {
            //帐户事件

            CoreService.EventAccount.OnNewAccountEvent -= new Action<AccountLite>(GotAccount);
            CoreService.EventAccount.OnInfoLiteEvent -= new Action<AccountInfoLite>(GotAccountInfoLite);
            CoreService.EventAccount.OnAccountChangedEvent -= new Action<AccountLite>(GotAccountChanged);
            CoreService.EventAccount.OnSessionUpdateEvent -= new Action<NotifyMGRSessionUpdateNotify>(GotSessionUpdate);
        }
    }
}
