using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterControl
{
    public class ControlService
    {
        static ControlService defaultintance = null;
        static ControlService()
        {
            defaultintance = new ControlService();
            ConfigFile _config = ConfigFile.GetConfigFile("moniter.cfg");
            SuperRoot = _config["SuperRoot"].AsString();
        }

        private ControlService()
        {
            
        }

        public static string SuperRoot { get; set; }

        /// <summary>
        /// 合约选择事件
        /// </summary>
        public static event Action<Symbol> SymbolSelected = delegate { };
        static internal void FireSymbolSelected(Symbol symbol)
        {
            SymbolSelected(symbol);
        }

        /// <summary>
        /// 账户选择事件
        /// </summary>
        public static event Action<AccountItem> AccountSelected = delegate { };
        static internal void FireAccountSelected(AccountItem item)
        {
            AccountSelected(item);
        }
        
        /// <summary>
        /// 账户过滤参数
        /// </summary>
        public static event Action<FilterArgs> FilterArgsChanged = delegate { };
        static internal void FireFilterArgsChanged(FilterArgs arg)
        {
            FilterArgsChanged(arg);
        }

        /// <summary>
        /// 交易账户Grid显示账户显示数量
        /// </summary>
        public static event Action<int> AccGridNumChanged = delegate { };
        static internal void FireAccGridNumChanged(int num)
        {
            AccGridNumChanged(num);
        }



    }
}
