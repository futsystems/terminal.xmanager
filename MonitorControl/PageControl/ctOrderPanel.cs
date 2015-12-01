using System;
using System.Collections.Generic;
using System.Collections;
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
    
    public partial class ctOrderPanel : UserControl,IEventBinder
    {

        AccountLite _account = null;
        //Symbol _symbol = null;
        public event OrderDelegate SendOrderEvent;
        public ctOrderPanel()
        {
            InitializeComponent();
            this.Load += new EventHandler(ctOrderPanel_Load);
        }

        void ctOrderPanel_Load(object sender, EventArgs e)
        {
            side_buy.Checked = true;
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            btnInsertTrade.Click += new EventHandler(btnInsertTrade_Click);
            this.SendOrderEvent += new OrderDelegate(SendOrderOut);
            CoreService.EventUI.OnSymbolSelectedEvent += new Action<Symbol>(EventUI_OnSymbolSelectedEvent);

            CoreService.EventCore.RegIEventHandler(this);
        }

        void EventUI_OnSymbolSelectedEvent(Symbol obj)
        {
            this.cbSymbolList.SelectedValue = obj;
        }

        public void OnInit()
        {
            //btnInsertTrade.Visible = Globals.UIAccess.fun_tab_placeorder_insert;
            CoreService.EventAccount.OnAccountSelectedEvent += new Action<AccountLite>(OnAccountSelected);

            MoniterHelper.AdapterToIDataSource(cboffsetflag).BindDataSource(MoniterHelper.GetOffsetCBList());
            //MoniterHelper.AdapterToIDataSource(cbordertype).BindDataSource(MoniterHelper.GetOrderTypeCBList());
            //Globals.Debug("~~~~~~~~~~~~~~~~~~~~~~~~~~ order sender insert:" + Globals.Domain.Misc_InsertTrade.ToString() + " is root:" + Globals.Manager.IsRoot());
            //如果不是超级域 则需要按设置来判断是否显示调试插入按钮
            btnInsertTrade.Visible = false;
            //if (!CoreService.SiteInfo.Domain.Super)
            //{
            //    btnInsertTrade.Visible = CoreService.SiteInfo.Domain.Misc_InsertTrade && CoreService.SiteInfo.Manager.IsRoot();
            //}
            if (CoreService.BasicInfoTracker.Symbols.Count() > 0)
            {
                MoniterHelper.AdapterToIDataSource(cbSymbolList).BindDataSource(GetSymbolCombList());
            }
        }

        ArrayList GetSymbolCombList()
        {
            ArrayList list = new ArrayList();

            foreach (var sym in CoreService.BasicInfoTracker.Symbols)
            {
                ValueObject<Symbol> vo = new ValueObject<Symbol>();
                vo.Name = sym.SecurityFamily.Name + "-" + sym.Symbol;
                vo.Value = sym;
                list.Add(vo);
            }
            return list;
        }

        

        public void OnDisposed()
        {
            CoreService.EventAccount.OnAccountSelectedEvent -= new Action<AccountLite>(OnAccountSelected);
        }

        
        void OnAccountSelected(AccountLite obj)
        {
            this.SetAccount(obj);
        }

        void SendOrderOut(Order o)
        {
            o.Account = _account.Account;
            CoreService.TLClient.ReqOrderInsert(o);
        }

        /// <summary>
        /// 绑定帐户
        /// </summary>
        /// <param name="acc"></param>
        void SetAccount(AccountLite acc)
        {
            _account = acc;
            account.Text = _account.Account;
        }

        //public void SetSymbol(Symbol sym)
        //{
        //    _symbol = sym;
        //    symbol.Text = _symbol.Symbol;
        //    int decimalplace = Util.GetDecimalPlace(sym.SecurityFamily.PriceTick);
        //    price.DecimalPlaces = decimalplace;
        //}

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool side = side_buy.Checked;
                genOrder(side);
            }
            catch (Exception ex)
            {
                //Globals.Debug("error:" + ex.ToString());
            }
        }


        private void btnInsertTrade_Click(object sender, EventArgs e)
        {
            fmInsertTrade fm = new fmInsertTrade();
            if (!ValidAccount()) return;
            //if (!validSecurity()) return;
            fm.SetAccount(_account.Account);
            //fm.SetSymbol(_symbol);
            fm.ShowDialog();
        }



        /// <summary>
        /// 生成对应的买 卖委托并发送出去
        /// </summary>
        /// <param name="f"></param>
        private void genOrder(bool f)
        {
            if (!ValidAccount()) return;
            if (!validSecurity()) return;
            if (!validSize()) return;
            if (!validPrice()) return;

            Symbol _symbol = cbSymbolList.SelectedValue as Symbol;
            //生成对应的委托
            Order work = new OrderImpl(_symbol.Symbol, 0);
            work.Account = _account.Account;
            work.LocalSymbol = _symbol.Symbol;
            work.Side = f;
            work.Size = Math.Abs((int)size.Value);
            work.OffsetFlag = (QSEnumOffsetFlag)cboffsetflag.SelectedValue;
            //if (ismarket)
            //{
            //    work.LimitPrice = 0;
            //    work.StopPrice = 0;
            //}
            //else
            {
                //bool islimit = this.islimit;
                decimal limit = (decimal)(price.Value);
                //decimal stop = !islimit ? (decimal)(price.Value) : 0;
                work.LimitPrice = limit;
                work.StopPrice = 0;
            }
            work.id = 0;
            SendOrder(work);
        }


        //bool ismarket { get { return int.Parse(cbordertype.SelectedValue.ToString())==1; } }
        //bool islimit { get { return int.Parse(cbordertype.SelectedValue.ToString()) == 0; } }

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
            if((cbSymbolList.SelectedValue as Symbol) == null)
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
            if ((decimal)(price.Value) >= 0)
            {
                return true;
            }
            else
            {
                MoniterHelper.WindowMessage("请设定价格");
                return false;
            }
        }
        


        void SendOrder(Order order)
        {

            if (SendOrderEvent != null)
                SendOrderEvent(order);
        }

        

    }
}
