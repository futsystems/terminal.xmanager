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


namespace TradingLib.TinyMGRControl.Control
{
    public partial class ctOrderSentderSTK : UserControl
    {
        ILog logger = LogManager.GetLogger("ctOrderSentderSTK");

        public ctOrderSentderSTK()
        {
            InitializeComponent();

            size.Value = 1;
            WireEvent();
        }

        void WireEvent()
        {
            CoreService.EventOther.OnSymbolSelectedEvent += new Action<object, Symbol>(EventOther_OnSymbolSelectedEvent);
           
            this.symbol.TextChanged += new EventHandler(symbol_TextChanged);
        }

        Symbol _symbol = null;
        bool _inputControlAdjuestd = false;
        void EventOther_OnSymbolSelectedEvent(object arg1, Symbol arg2)
        {
            if (arg2 == null)
            {
                _symbol = null;
                symbol_name.Text = "--";
                //lbMaxOrderVol.Text = "0";
                //price.Maximum = 0;
                //price.Minimum = 0;
                //price.DecimalPlaces = 0;
                //price.Increment = 0;
                //price.Value = 0;
                //btnSubmit.Enabled = false;
                return;
            }
            if (_symbol == null || _symbol.Symbol != arg2.Symbol)
            {
                _symbol = arg2;
                if (symbol.Text != arg2.Symbol)
                {
                    symbol.Text = arg2.Symbol;
                }
                _inputControlAdjuestd = false;
                AdjustInputControl();
            }
        }

        void AdjustInputControl()
        {
            if (_symbol == null) return;
            if (_inputControlAdjuestd) return;
            symbol_name.Text = _symbol.GetName();

            Tick k = CoreService.TradingInfoTracker.TickTracker[_symbol.Symbol];
            if (k != null)
            {
                price.Maximum = k.UpperLimit;
                price.Minimum = k.LowerLimit;
                price.DecimalPlaces = _symbol.SecurityFamily.GetDecimalPlaces();
                price.Increment = _symbol.SecurityFamily.PriceTick;
                price.Value = k.Trade;

                size.Value = 1;
                //查询最大可开委托数量
                //QryMaxOrderVol();

                _inputControlAdjuestd = true;
                //btnSubmit.Enabled = true;
            }

        }


        /// <summary>
        /// 判定是否需要查找合约
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        bool NeedSearchSymbol(string symbol)
        {
            if (symbol.Length == 6) return true;
            return false;

        }

        //int _qryid = 0;
        void symbol_TextChanged(object sender, EventArgs e)
        {
            logger.Info(string.Format("Symbol changed:{0}", symbol.Text));

            bool needsearch = NeedSearchSymbol(symbol.Text);
            if (needsearch)
            {
                Symbol sym = CoreService.BasicInfoTracker.GetSymbol(symbol.Text);
                if (sym == null)
                {
                   
                }
                else
                {
                    //触发合约选择事件
                    CoreService.EventOther.FireSymbolSelectedEvent(this, sym);
                }
            }
            else
            {
                if (_symbol != null)//如果当前选择的合约不为空 则出发选空 进行重置，边界出发
                {
                    CoreService.EventOther.FireSymbolSelectedEvent(this, null);
                }
            }
        }
    }
}
