using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterCore
{
    public partial class TLClientNet
    {
        /// <summary>
        /// 查询账户结算单
        /// </summary>
        /// <param name="mgrid"></param>
        public void ReqQryAccountSettlement(string account,int tradingday)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_SETTLEMENT, new { account = account, tradingday = tradingday });
        }

        /// <summary>
        /// 查询账户委托记录
        /// </summary>
        /// <param name="mgrid"></param>
        public void ReqQryAccountOrder(string account, int start,int end)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_ORDER, new { account = account, start = start, end = end });
        }

        /// <summary>
        /// 查询账户成交记录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void ReqQryAccountTrade(string account, int start, int end)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_TRADE, new { account = account, start = start, end = end });
        }

        /// <summary>
        /// 查询交易账户持仓
        /// </summary>
        /// <param name="account"></param>
        /// <param name="tradingday"></param>
        public void ReqQryAccountPosition(string account, int tradingday)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_POSITION, new { account = account, tradingday = tradingday });
        }

        /// <summary>
        /// 查询交易帐户出入金记录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void ReqQryAccountCashTxn(string account, long start, long end)
        {
            this.ReqContribRequest(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_TXN, new { account = account, start = start, end = end });
        }
    }
}
