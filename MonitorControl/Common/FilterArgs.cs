using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterControl
{
    public class FilterArgs
    {
        //帐户状态 冻结 激活
        //public bool AcctExecuteEnable = false;
        public bool AccLock = false;

        public bool AccLogin = false;

        public bool AccPos = false;
        //帐户类别
        //public bool AcctTypeEnable = false;
        //public QSEnumAccountCategory AcctType = QSEnumAccountCategory.SUBACCOUNT;

        //所属代理商
        public bool AgentEnable = false;
        public int AgentID = 0;

        //路由通道类别
        //public bool OrderRouterTypeEnable = false;
        //public QSEnumOrderTransferType OrderRouterType = QSEnumOrderTransferType.SIM;

        ////主帐户绑定
        //public bool MAcctBindedEnable = false;
        //public bool MAcctBinded = false;

        ////主帐户连接
        //public bool MAcctConnectedEnable = false;
        //public bool MAcctConnected = false;

        ////路由组
        //public bool RouterGroupEnable = false;
        //public int RouterGroupID = 0;

        //帐号过滤
        public string AccSearch = string.Empty;

        public int AccNum = 0;
    }
}
