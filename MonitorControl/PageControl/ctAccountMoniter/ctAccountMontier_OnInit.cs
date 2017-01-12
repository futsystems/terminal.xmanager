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

        public void OnInit()
        {
            //加载帐户
            foreach (AccountItem account in CoreService.BasicInfoTracker.Accounts)
            {
                InvokeGotAccount(account);
            }
            //更新帐户数目
            UpdateAccountNum();

            //帐户事件
            //CoreService.EventAccount.OnNewAccountEvent += new Action<AccountItem>(GotAccount);
            CoreService.EventAccount.OnInfoLiteEvent += new Action<AccountInfoLite>(GotAccountInfoLite);
            CoreService.EventAccount.OnAccountChangedEvent += new Action<AccountItem>(GotAccountChanged);
            CoreService.EventAccount.OnSessionUpdateEvent += new Action<NotifyMGRSessionUpdateNotify>(GotSessionUpdate);
            

            //主帐户监控
            if (CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter)
            {
                
                accountgrid.Columns[CATEGORYSTR].Visible = false;
                accountgrid.Columns[AGENTCODE].Visible = false;


                accountgrid.Columns[LOGINSTATUSIMG].Visible = false;//登入标识
                //accountgrid.Columns[ADDRESS].Visible = false;//终端地址

                accountgrid.Columns[ROUTEIMG].Visible = false;
                accountgrid.Columns[ROUTERGROUPSTR].Visible = false;

                //调整宽度
                VendorMoniterWidth();
            }

            if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
            {
                //只有管理员可以查看路由类别
                accountgrid.Columns[ROUTEIMG].Visible = CoreService.SiteInfo.Manager.IsRoot();
                //管理员可以查看帐户类别
                accountgrid.Columns[CATEGORYSTR].Visible = CoreService.SiteInfo.Manager.IsRoot();

                //如果有实盘交易权限则可以查看路由组
                accountgrid.Columns[ROUTERGROUPSTR].Visible = CoreService.SiteInfo.Manager.IsRoot();


                accountgrid.Columns[MAINACCOUNT].Visible = false;


                accountgrid.Columns[CATEGORYSTR].Visible = false;
                accountgrid.Columns[MAINACCOUNT].Visible = false;
                accountgrid.Columns[MAINACCTRISKRULE].Visible = false;
                accountgrid.Columns[MACTCONNIMG].Visible = false;

                CounterMoniterWidth();
            }

            //根据产品类别来调整界面

            //启动更新线程
            StartUpdate();
        }

        public void OnDisposed()
        {
            //帐户事件
            //CoreService.EventAccount.OnNewAccountEvent -= new Action<AccountItem>(GotAccount);
            CoreService.EventAccount.OnInfoLiteEvent -= new Action<AccountInfoLite>(GotAccountInfoLite);
            CoreService.EventAccount.OnAccountChangedEvent -= new Action<AccountItem>(GotAccountChanged);
            CoreService.EventAccount.OnSessionUpdateEvent -= new Action<NotifyMGRSessionUpdateNotify>(GotSessionUpdate);
        }
    }
}
