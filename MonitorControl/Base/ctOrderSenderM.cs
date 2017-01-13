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


namespace TradingLib.MoniterControl
{
    public partial class ctOrderSenderM : UserControl,IEventBinder
    {

        AccountItem _account = null;
        Symbol _symbol = null;

        public ctOrderSenderM()
        {
            InitializeComponent();
            this.Load += new EventHandler(ctOrderSenderM_Load);
        }

        void ctOrderSenderM_Load(object sender, EventArgs e)
        {
            btnBuy.Click += new EventHandler(btnBuy_Click);
            btnSell.Click += new EventHandler(btnSell_Click);
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            ControlService.AccountSelected += new Action<AccountItem>(SetAccount);
            ControlService.SymbolSelected += new Action<Symbol>(SetSymbol);
            
            MoniterHelper.AdapterToIDataSource(cboffsetflag).BindDataSource(MoniterHelper.GetOffsetCBList());
            MoniterHelper.AdapterToIDataSource(cbordertype).BindDataSource(MoniterHelper.GetOrderTypeCBList());

        }

        

        public void OnDisposed()
        {
            ControlService.AccountSelected += new Action<AccountItem>(SetAccount);
            ControlService.SymbolSelected += new Action<Symbol>(SetSymbol);
        }



        /// <summary>
        /// 绑定帐户
        /// </summary>
        /// <param name="acc"></param>
        void SetAccount(AccountItem acc)
        {
            _account = acc;
            account.Text = _account.Account;
        }

        void SetSymbol(Symbol sym)
        {
            _symbol = sym;
            symbol.Text = _symbol.Symbol;
            price.DecimalPlaces = sym.SecurityFamily.PriceTick.GetDecimalPlaces();
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            SendOrder(true);
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            SendOrder(false);
            
        }

        /// <summary>
        /// 生成对应的买 卖委托并发送出去
        /// </summary>
        /// <param name="f"></param>
        void SendOrder(bool f)
        {
            if (!ValidAccount()) return;
            if (!validSecurity()) return;
            if (!validSize()) return;
            if (!validPrice()) return;

            //生成对应的委托
            Order work = new OrderImpl();
            work.Account = _account.Account;
            work.Exchange = _symbol.Exchange;
            work.Symbol = _symbol.Symbol;
            work.LocalSymbol = _symbol.Symbol;
            work.Side = f;
            work.Size = Math.Abs((int)size.Value);
            work.OffsetFlag = (QSEnumOffsetFlag)cboffsetflag.SelectedValue;
            if (ismarket)
            {
                work.LimitPrice = 0;
                work.StopPrice = 0;
            }
            else
            {
                bool islimit = this.islimit;
                decimal limit = islimit ? (decimal)(price.Value) : 0;
                decimal stop = !islimit ? (decimal)(price.Value) : 0;
                work.LimitPrice = limit;
                work.StopPrice = stop;
            }
            work.id = 0;
            work.Account = _account.Account;
            CoreService.TLClient.ReqOrderInsert(work);
        }


        bool ismarket { get { return int.Parse(cbordertype.SelectedValue.ToString())==1; } }
        bool islimit { get { return int.Parse(cbordertype.SelectedValue.ToString()) == 0; } }

        bool ValidAccount()
        {
            if (_account == null)
            {
                MoniterHelper.WindowMessage("请选择帐户!");
                return false;
            }
            else
                return true;
        }
        /// <summary>
        /// 检查当前是否选中合约
        /// </summary>
        /// <returns></returns>
        bool validSecurity()
        {
            if (_symbol == null)
            {
                MoniterHelper.WindowMessage("请选择合约！");
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// 检查当前设置手数
        /// </summary>
        /// <returns></returns>
        bool validSize()
        {
            if ((int)(size.Value) == 0)
            {
                MoniterHelper.WindowMessage("请设置手数");
                return false;
            }
            else
            {
                return true;
            }
        }

        bool validPrice()
        {
            if (ismarket || (decimal)(price.Value) > 0)
            {
                return true;
            }
            else
            {
                MoniterHelper.WindowMessage("请设定价格");
                return false;
            }
        }
        

        

    }
}
