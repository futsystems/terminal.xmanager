﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using Common.Logging;


namespace TradingLib.MoniterControl
{
    [MoniterControlAttr("TradingInfoMoniter", "交易信息查看组件", EnumControlLocation.BottomLeft)]
    public partial class ctTradingInfo : UserControl, IEventBinder, IMoniterControl
    {
        public ctTradingInfo()
        {
            InitializeComponent();
            this.Load += new EventHandler(ctTradingInfo_Load);
        }
        ILog logger = LogManager.GetLogger("ctTradingInfo");

        void debug(string msg)
        {
            logger.Debug(msg);
        }
        void ctTradingInfo_Load(object sender, EventArgs e)
        {
            //交易信息显示控件事件
            ctOrderView1.SendOrderCancel += new LongDelegate(CancelOrder);

            ctPositionView1.SendCancelEvent += new LongDelegate(CancelOrder);
            ctPositionView1.SendOrderEvent += new OrderDelegate(SendOrder);

            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            ControlService.AccountSelected += new Action<AccountItem>(OnAccountSelected);
            CoreService.EventIndicator.GotTickEvent += new Action<Tick>(GotTick);
            CoreService.EventIndicator.GotOrderEvent += new Action<Order>(GotOrder);
            CoreService.EventIndicator.GotFillEvent += new Action<Trade>(GotTrade);

            CoreService.EventAccount.OnAccountResumeEvent += new Action<RspMGRResumeAccountResponse>(OnResume);

            if (!CoreService.SiteInfo.Domain.Super)
            {
                //管理员可以执行平仓操作
                ctOrderView1.EnableOperation = CoreService.SiteInfo.Manager.IsRoot() || CoreService.SiteInfo.UIAccess.r_execution;
                ctPositionView1.EnableOperation = CoreService.SiteInfo.Manager.IsRoot() || CoreService.SiteInfo.UIAccess.r_execution;
            }

        }



        public void OnDisposed()
        {
            ControlService.AccountSelected -= new Action<AccountItem>(OnAccountSelected);
            //Globals.LogicEvent.GotAccountSyncEvent += new Action<AccountItem>(OnAccountSyncEvent);
            CoreService.EventIndicator.GotTickEvent -= new Action<Tick>(GotTick);
            CoreService.EventIndicator.GotOrderEvent -= new Action<Order>(GotOrder);
            CoreService.EventIndicator.GotFillEvent -= new Action<Trade>(GotTrade);

            CoreService.EventAccount.OnAccountResumeEvent += new Action<RspMGRResumeAccountResponse>(OnResume);
        }



        AccountItem _account = null;
        AccountItem CurrentAccount { get { return _account; } }
        /// <summary>
        /// 判断是否是当前选中帐户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool IsCurrentAccount(string account)
        {
            if (CurrentAccount == null) return false;
            if (CurrentAccount.Account.Equals(account)) return true;
            return false;
        }

        void OnAccountSelected(AccountItem account)
        {
            _account = account;
            //清空控件交易记录
            ClearTradingInfo();

            //开始恢复该帐户交易记录
            CoreService.TradingInfoTracker.RequetResume(account.Account);
        }

        void OnAccountSyncEvent(AccountItem obj)
        {
            //如果同步交易数据的时候 当前显示的帐户与我们同步的帐户一致，则需要清空当前交易记录，否则同步后会造成重复
            if (_account.Account == obj.Account)
            {
                ClearTradingInfo();
            }
        }

        /// <summary>
        /// 清空界面数据和交易记录缓存
        /// </summary>
        public void Clear()
        {
            ClearTradingInfo();
        }


        /// <summary>
        /// 响应交易数据恢复
        /// </summary>
        void OnResume(RspMGRResumeAccountResponse response)
        {
            if (response.IsBegin)
            {
                debug("resume account:" + response.ResumeAccount + " start...");

                string account = response.ResumeAccount;
                AccountItem acclit = null;
                //if (accountmap.TryGetValue(account, out acclit))
                {

                    CoreService.TradingInfoTracker.StartResume(CurrentAccount);
                }
                //else
                {
                    //debug("无法找到对应的帐户:" + account);
                }
            }
            else
            {
                //数据恢复结束
                debug("resume account:" + response.ResumeAccount + " end...");
                CoreService.TradingInfoTracker.EndResume();
                LoadAccountInfo(response.ResumeAccount);
            }
        }

        /// <summary>
        /// 清空交易记录
        /// </summary>
        void ClearTradingInfo()
        {
            //清空交易信息缓存
            CoreService.TradingInfoTracker.Clear();
            ctOrderView1.Clear();
            ctPositionView1.Clear();
            ctTradeView1.Clear();
        }


        /// <summary>
        /// 加载帐户数据
        /// </summary>
        /// <param name="account"></param>
        void LoadAccountInfo(string account)
        {
            //debug("try to load trading info tracker account:" + Globals.TradingInfoTracker.Account.Account + " request account:" + account);
            if (CoreService.TradingInfoTracker.Account.Account.Equals(account))
            {
                ctPositionView1.PositionTracker = CoreService.TradingInfoTracker.PositionTracker;
                ctPositionView1.OrderTracker = CoreService.TradingInfoTracker.OrderTracker;
                ctOrderView1.OrderTracker = CoreService.TradingInfoTracker.OrderTracker;

                foreach (Position pos in CoreService.TradingInfoTracker.HoldPositionTracker)
                {
                    ctPositionView1.GotPosition(pos);
                }

                foreach (Order o in CoreService.TradingInfoTracker.OrderTracker)
                {
                    ctOrderView1.GotOrder(o);
                }

                foreach (Trade f in CoreService.TradingInfoTracker.TradeTracker)
                {
                    ctTradeView1.GotFill(f);
                    ctPositionView1.GotFill(f);
                }
            }
            else
            {
                debug("TradingInfoTracker 维护帐户与请求加载帐户不一致..");
            }

        }


        void GotTick(Tick k)
        {
            ctPositionView1.GotTick(k);
        }

        /// <summary>
        /// 获得服务端转发的委托
        /// </summary>
        /// <param name="o"></param>
        void GotOrder(Order o)
        {
            if (IsCurrentAccount(o.Account) && CoreService.TradingInfoTracker.IsReady(o.Account))
            {
                ctOrderView1.GotOrder(o);
                ctPositionView1.GotOrder(o);
            }
        }

        /// <summary>
        /// 获得服务端转发的成交
        /// </summary>
        /// <param name="f"></param>
        void GotTrade(Trade f)
        {
            if (IsCurrentAccount(f.Account) && CoreService.TradingInfoTracker.IsReady(f.Account))
            {
                ctTradeView1.GotFill(f);
                ctPositionView1.GotFill(f);
            }
        }


        void CancelOrder(long oid)
        {
            OrderAction actoin = new OrderActionImpl();
            actoin.Account = CurrentAccount.Account;
            actoin.ActionFlag = QSEnumOrderActionFlag.Delete;
            actoin.OrderID = oid;
            SendOrderAction(actoin);
        }

        void SendOrderAction(OrderAction action)
        {
            CoreService.TLClient.ReqOrderAction(action);
        }

        void SendOrder(Order o)
        {
            o.Account = CurrentAccount.Account;
            CoreService.TLClient.ReqOrderInsert(o);
        }


    }
}
