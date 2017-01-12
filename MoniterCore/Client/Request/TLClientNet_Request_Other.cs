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
        /// 查询系统状态
        /// </summary>
        public void ReqQrySystemStatus()
        {
            this.ReqContribRequest(Modules.MGR_EXCH,Method_MGR_EXCH.QRY_SYSTEM_STATUS,"");
        }

        /// <summary>
        /// 查询银行列表
        /// </summary>
        public void ReqQryBank()
        {
            this.ReqContribRequest(Modules.MGR_EXCH,Method_MGR_EXCH.QRY_CONTRACT_BANK, "");
        }

        /// <summary>
        /// 查询收款银行
        /// </summary>
        public void ReqQryReceiveableBank()
        {
            this.ReqContribRequest(Modules.MGR_EXCH,Method_MGR_EXCH.QRY_RECV_BANK, "");
        }

        /// <summary>
        /// 更新收款银行
        /// </summary>
        /// <param name="bank"></param>
        public void ReqUpdateRecvBank(JsonWrapperReceivableAccount bank)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_RECV_BANK, bank);
        }

    }
}
