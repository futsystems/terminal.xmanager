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

        AccountLite _account = null;
        public ctOrderSentderSTK()
        {
            InitializeComponent();

            size.Value = 1;
            WireEvent();
        }

        void WireEvent()
        {
            CoreService.EventOther.OnSymbolSelectedEvent += new Action<object, Symbol>(EventOther_OnSymbolSelectedEvent);
            CoreService.EventAccount.OnAccountSelectedEvent += new Action<AccountLite>(EventAccount_OnAccountSelectedEvent);
            btnBuy.Click += new EventHandler(btnBuy_Click);
            btnSell.Click += new EventHandler(btnSell_Click);
            this.symbol.TextChanged += new EventHandler(symbol_TextChanged);
        }

        void EventAccount_OnAccountSelectedEvent(AccountLite obj)
        {
            _account = obj;
        }

        void btnSell_Click(object sender, EventArgs e)
        {
            SendOrder(false);
        }

        void btnBuy_Click(object sender, EventArgs e)
        {
            SendOrder(true);
        }

        int _orderInesertId = 0;
        void SendOrder(bool side)
        {
            if (_symbol == null)
            {

                fmMessage.Show("委托参数错误", "请输入交易股票代码");
                return;
            }
            if (size.Value == 0)
            {
                fmMessage.Show("委托参数错误", "请输入交易股票数量");
                return;
            }

            if (_account == null)
            {
                fmMessage.Show("委托参数错误", "请选择交易账户");
                return;
            }

            Order order = new OrderImpl();
            order.Account = _account.Account;
            order.Symbol = _symbol.Symbol;
            order.Size = Math.Abs((int)size.Value);
            order.Side = side;
            order.LimitPrice = price.Value;

            if (side)
            {
                order.OffsetFlag = QSEnumOffsetFlag.OPEN;
            }
            else
            {
                order.OffsetFlag = QSEnumOffsetFlag.CLOSE;
            }

            string msg = "以价格:{0} {1}{2}股票:{3}".Put(order.LimitPrice.ToFormatStr(), order.Side ? "买入" : "卖出", order.UnsignedSize, _symbol.GetName());
            if (fmConfirm.Show("确认提交委托?", msg) == DialogResult.Yes)
            {
                _orderInesertId = CoreService.TLClient.ReqOrderInsert(order);
            }


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
