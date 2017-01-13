using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public enum QSEnumInfoTrackerStatus
    {
        UNKNOWN,

        /// <summary>
        /// 恢复开始
        /// </summary>
        RESUMEBEGIN,

        /// <summary>
        /// 恢复结束
        /// </summary>
        RESUMEEND,

        /// <summary>
        /// 请求恢复 本地
        /// </summary>
        RESUMEREQUEST,
    }

    public class TradingInfoTracker
    {
        public OrderTracker OrderTracker { get; set; }
        public LSPositionTracker PositionTracker { get; set; }
        public LSPositionTracker HoldPositionTracker { get; set; }
        public ThreadSafeList<Trade> TradeTracker { get; set; }

        public AccountItem Account { get; set; }
        QSEnumInfoTrackerStatus status = QSEnumInfoTrackerStatus.UNKNOWN;
        public QSEnumInfoTrackerStatus Status { get { return status; } }

        /// <summary>
        /// 行情快照维护器
        /// </summary>
        public TickTracker TickTracker { get; set; }


        public TradingInfoTracker()
        {
            TickTracker = new TickTracker();
            OrderTracker = new OrderTracker();
            PositionTracker = new LSPositionTracker("");
            HoldPositionTracker = new LSPositionTracker("");
            TradeTracker = new ThreadSafeList<Trade>();
            Account = new AccountItem();
           
        }
        /// <summary>
        /// 开始恢复交易记录
        /// </summary>
        /// <param name="account"></param>
        public void StartResume(AccountItem account)
        {
            status = QSEnumInfoTrackerStatus.RESUMEREQUEST;
            this.Clear();
            this.Account = account;
            CoreService.TLClient.ReqResumeAccount(account.Account);
        }

        /// <summary>
        /// 交易记录恢复结束
        /// 用于界面加载数据后再设置状态 避免状态提前设置后 界面处理数据导致卡死
        /// </summary>
        public void EndResume()
        {
            status = QSEnumInfoTrackerStatus.RESUMEEND;
        }

        /// <summary>
        /// 获得持仓回报
        /// </summary>
        /// <param name="o"></param>
        public void GotOrder(Order o)
        {
            if (Account.Account != o.Account) return;
            OrderTracker.GotOrder(o);
            CoreService.EventIndicator.FireOrder(o);
        }


        /// <summary>
        /// 获得成交回报
        /// </summary>
        /// <param name="f"></param>
        public void GotFill(Trade f)
        {
            if (Account.Account != f.Account) return;
            LogService.Debug("got trade notify:" + f.GetTradeDetail());
            bool accept = false;
            PositionTracker.GotFill(f, out accept);
            if (accept)
            {
                OrderTracker.GotFill(f);

                TradeTracker.Add(f);
                CoreService.EventIndicator.FireFill(f);
            }
        }

        public void GotTick(Tick k)
        {
            TickTracker.GotTick(k);
            PositionTracker.GotTick(k);
            CoreService.EventIndicator.FireTick(k);
        }

        /// <summary>
        /// 获得隔夜持仓数据
        /// </summary>
        /// <param name="pos"></param>
        public void GotHoldPosition(PositionDetail pos)
        {
            if (Account.Account != pos.Account) return;

            PositionTracker.GotPosition(pos);
            HoldPositionTracker.GotPosition(pos);

            CoreService.EventIndicator.FirePositionDetail(pos);
        }

        /// <summary>
        /// 是否处于恢复数据状态
        /// </summary>
        public bool IsInResume
        {
            get
            {
                if (status == QSEnumInfoTrackerStatus.RESUMEEND || status == QSEnumInfoTrackerStatus.UNKNOWN)
                    return false;
                return true;
            }
        }


        /// <summary>
        /// 清空所有历史数据
        /// </summary>
        public void Clear()
        {
            OrderTracker.Clear();
            HoldPositionTracker.Clear();
            PositionTracker.Clear();
            TradeTracker.Clear();
        }
    }
}

