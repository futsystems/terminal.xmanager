using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.Mixins.Json;
using TradingLib.MoniterCore;

namespace TradingLib.TinyMGRControl
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

             CounterMoniterWidth();
            

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
