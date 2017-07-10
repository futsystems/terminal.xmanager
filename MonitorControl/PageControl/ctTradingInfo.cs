using System;
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
        ILog logger = LogManager.GetLogger("ctTradingInfo");
        public Control FilterToolBar { get; set; }
        AccountItem _account = null;
        /// <summary>
        /// 判断是否是当前选中帐户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool IsCurrentAccount(string account)
        {
            if (_account == null) return false;
            if (_account.Account.Equals(account)) return true;
            return false;
        }


        public ctTradingInfo()
        {
            InitializeComponent();
            this.Load += new EventHandler(ctTradingInfo_Load);
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

            //CoreService.EventHub.OnResumeDataEnd += new Action(EventHub_OnResumeDataEnd);
            //CoreService.EventHub.OnResumeDataStart += new Action(EventHub_OnResumeDataStart);
            CoreService.EventCore.RegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_ACC_RESUME, OnNotifyResume);
            if (!CoreService.SiteInfo.Domain.Super)
            {
                //管理员可以执行平仓操作
                ctOrderView1.EnableOperation = CoreService.SiteInfo.Manager.IsRoot() || CoreService.SiteInfo.Permission.r_execution;
                ctPositionView1.EnableOperation = CoreService.SiteInfo.Manager.IsRoot() || CoreService.SiteInfo.Permission.r_execution;
            }

        }


        void EventHub_OnResumeDataStart()
        {
            

        }

        void OnNotifyResume(string json)
        {
            int status = json.DeserializeObject<int>();
            if (status == 1)
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
                CoreService.TradingInfoTracker.EndResume();

                //交易数据恢复完毕后 查询行情快照并驱动持仓列表数据
                foreach (var pos in CoreService.TradingInfoTracker.PositionTracker)
                {
                    if (pos.oSymbol == null) continue;
                    Tick k = CoreService.BasicInfoTracker.GetTickSnapshot(pos.oSymbol.Exchange, pos.oSymbol.Symbol);
                    if (k == null) continue;
                    this.GotTick(k);
                }
            }
        }

        void EventHub_OnResumeDataEnd()
        {
            
        }



        public void OnDisposed()
        {
            ControlService.AccountSelected -= new Action<AccountItem>(OnAccountSelected);

            CoreService.EventIndicator.GotTickEvent -= new Action<Tick>(GotTick);
            CoreService.EventIndicator.GotOrderEvent -= new Action<Order>(GotOrder);
            CoreService.EventIndicator.GotFillEvent -= new Action<Trade>(GotTrade);

            //CoreService.EventHub.OnResumeDataEnd -= new Action(EventHub_OnResumeDataEnd);
            //CoreService.EventHub.OnResumeDataStart -= new Action(EventHub_OnResumeDataStart);
            CoreService.EventCore.UnRegisterNotifyCallback(Modules.MGR_EXCH, Method_MGR_EXCH.NOTIFY_ACC_RESUME, OnNotifyResume);
        }




       

        void OnAccountSelected(AccountItem account)
        {
            _account = account;
            this.Clear();
            CoreService.TradingInfoTracker.StartResume(account);
        }

       
        /// <summary>
        /// 清空界面数据和交易记录缓存
        /// </summary>
        public void Clear()
        {
            ctOrderView1.Clear();
            ctPositionView1.Clear();
            ctTradeView1.Clear();
        }

        void GotTick(Tick k)
        {
            if (CoreService.TradingInfoTracker.IsInResume) return;
            ctPositionView1.GotTick(k);
        }

        /// <summary>
        /// 获得服务端转发的委托
        /// </summary>
        /// <param name="o"></param>
        void GotOrder(Order o)
        {
            if (CoreService.TradingInfoTracker.IsInResume) return;
            if (!IsCurrentAccount(o.Account)) return;
            ctOrderView1.GotOrder(o);
            ctPositionView1.GotOrder(o);
            
        }

        /// <summary>
        /// 获得服务端转发的成交
        /// </summary>
        /// <param name="f"></param>
        void GotTrade(Trade f)
        {
            if (CoreService.TradingInfoTracker.IsInResume) return;
            if (!IsCurrentAccount(f.Account)) return;
            ctTradeView1.GotFill(f);
            ctPositionView1.GotFill(f);
            
        }


        void CancelOrder(long oid)
        {
            
            OrderAction actoin = new OrderActionImpl();
            actoin.Account = _account.Account;
            actoin.ActionFlag = QSEnumOrderActionFlag.Delete;
            actoin.OrderID = oid;
            SendOrderAction(actoin);
        }

        void SendOrderAction(OrderAction action)
        {
            if (_account != null)
            {
                action.Account = _account.Account;
                CoreService.TLClient.ReqOrderAction(action);
            }
        }

        void SendOrder(Order o)
        {
            if (_account != null)
            {
                o.Account = _account.Account;
                CoreService.TLClient.ReqOrderInsert(o);
            }
        }


    }
}
