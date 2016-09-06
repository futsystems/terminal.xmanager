using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
    public partial class ctQuoteMoniterS : UserControl,IEventBinder, IMoniterControl
    {
        public event Action<IExchange> ExchangeChangedEvent;
        public event Action<Symbol> OpenChartEvent;

        ILog logger = LogManager.GetLogger("QuoteMoniterS");

        public ctQuoteMoniterS()
        {

            InitializeComponent();
            
            navigator.SelectedPageChanged += new EventHandler(navigator_SelectedPageChanged);
            navigator.PreviewKeyDown += new PreviewKeyDownEventHandler(navigator_PreviewKeyDown);
            navigator.MouseWheel += new MouseEventHandler(navigator_MouseWheel);

            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {

            //初始化QuoteMoniter
            foreach (var exchange in CoreService.BasicInfoTracker.Exchanges)
            {
                this.AddExchange(exchange);
                IEnumerable<Symbol> symbols = CoreService.BasicInfoTracker.Symbols.Where(sym => sym.SecurityFamily.Exchange.EXCode == exchange.EXCode).OrderBy(sym => sym.Symbol);
                this.AddSymbols(exchange, symbols);
            }

            //响应行情回报行情

            CoreService.EventIndicator.GotTickEvent += new Action<Tick>(EventIndicator_GotTickEvent);

            //CoreService.EventBasicInfo.OnSymbolEvent += new Action<SymbolImpl>(GotSymbol);
            //CoreService.EventBasicInfo.OnSecurityEvent += new Action<SecurityFamilyImpl>(GotSecurity);

        }

        void EventIndicator_GotTickEvent(Tick obj)
        {
            Symbol sym = CoreService.BasicInfoTracker.GetSymbol(obj.Symbol);
            this.GotTick(sym, obj);
        }

        //void GotSecurity(SecurityFamily sec)
        //{
        //    //Globals.Debug("got security reply");
        //    ViewQuoteList vq = GetViewQuote(sec);
        //    if (vq == null) return;
        //    //品种可交易 则加载该品种的所有可交易合约
        //    if (sec.Tradeable)
        //    {
        //        //Globals.Debug("品种:" + sec.Code + " 可交易");
        //        foreach (Symbol s in CoreService.BasicInfoTracker.GetSymbolTradable().Where(s => s.SecurityFamily.Code.Equals(sec.Code)))
        //        {
        //            //Globals.Debug("添加合约:" + s.Symbol);
        //            vq.AddSymbol(s);
        //        }
        //    }
        //    else//如果不可交易 则删除所有报价列表的合约
        //    {
        //        //Globals.Debug("品种:" + sec.Code + " 不可交易");
        //        Symbol[] syms = vq.Symbols.Where(s => s.SecurityFamily.Code.Equals(sec.Code)).ToArray();
        //        foreach (Symbol sym in syms)
        //        {
        //            //Globals.Debug("删除合约:" + sym.Symbol);
        //            vq.RemoveSymbol(sym);
        //        }
        //    }
        //}


        public void OnDisposed()
        {
            CoreService.EventIndicator.GotTickEvent -= new Action<Tick>(EventIndicator_GotTickEvent);
            //CoreService.EventBasicInfo.OnSymbolEvent -= new Action<SymbolImpl>(GotSymbol);
            //CoreService.EventBasicInfo.OnSecurityEvent -= new Action<SecurityFamilyImpl>(GotSecurity);
        }

        void navigator_MouseWheel(object sender, MouseEventArgs e)
        {
            if (navigator.Pages.Count > 0)
            {
                ViewQuoteList quote = GetSelectedQuote();
                if (quote != null)
                {
                    if (e.Delta > 0)
                    {
                        quote.RowUp();
                    }
                    else
                    {
                        quote.RowDown();
                    }
                }
            }
        }



        ViewQuoteList GetSelectedQuote()
        { 
            IExchange exchange = navigator.SelectedPage.Tag as IExchange;
            if (exchange != null)
            {
                return GetQuoteList(exchange.EXCode);
            }
            return null;
        }

        /// <summary>
        /// 当前选中的合约
        /// </summary>
        public Symbol SelectedSymbol
        {
            get
            {
                if (navigator.Pages.Count == 0) return null;
                ViewQuoteList quote = GetSelectedQuote();
                if (quote != null) return quote.SelectedSymbol;
                return null;
            }
        }
        void navigator_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            logger.Info("PreviewKeyDown:" + e.KeyCode);
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    {
                        ViewQuoteList quote = GetSelectedQuote();
                        if (quote != null)
                        {
                            if (e.KeyCode == Keys.Up)
                            {
                                quote.RowUp();
                            }
                            else
                            {
                                quote.RowDown();
                            }
                        }
                    }
                    break;
                case Keys.Return:
                    {
                        ViewQuoteList quote = GetSelectedQuote();
                        if (quote != null)
                        {
                            Symbol symbol = quote.SelectedSymbol;
                            if(symbol != null)
                            {
                                OnOpenKChartEvent(symbol);
                            }
                        }
                    }
                    break;                    
                default:
                    break;
            }
        }

        
      

        void navigator_SelectedPageChanged(object sender, EventArgs e)
        {
            logger.Info("Current Page:" + navigator.SelectedPage.Name);
            if (ExchangeChangedEvent != null)
            {
                IExchange exchange = navigator.SelectedPage.Tag as IExchange;
                if (exchange != null)
                {
                    ExchangeChangedEvent(exchange);
                }
            }
        }


        ConcurrentDictionary<string, ViewQuoteList> exchangeQuoteMap = new ConcurrentDictionary<string, ViewQuoteList>();
        ConcurrentDictionary<string, IExchange> exchangeMap = new ConcurrentDictionary<string, IExchange>();
        ConcurrentDictionary<string, ComponentFactory.Krypton.Navigator.KryptonPage> pageMap = new ConcurrentDictionary<string, ComponentFactory.Krypton.Navigator.KryptonPage>();

        ViewQuoteList GetQuoteList(string exchange)
        {
            ViewQuoteList target = null;
            if (exchangeQuoteMap.TryGetValue(exchange, out target))
            {
                return target;
            }
            return null;
        }


        /// <summary>
        /// 获得所有交易所
        /// </summary>
        public IEnumerable<IExchange> Exchanges { get { return exchangeMap.Values; } }

        EnumQuoteType GetQuoteType(IExchange ex)
        {
            switch (ex.EXCode)
            { 
                case "CFFEX":
                case "SHFE":
                case "CZCE":
                case "DCE":
                    return EnumQuoteType.CNQUOTE;
                default:
                    return EnumQuoteType.FOREIGNQUOTE;
            }
        }
        /// <summary>
        /// 添加一个交易所
        /// </summary>
        /// <param name="ex"></param>
        public void AddExchange(IExchange ex)
        {
            ViewQuoteList quote = new ViewQuoteList();
            quote.HeaderBackColor = Color.FromArgb(0, 0, 0);
            quote.BackColor = Color.FromArgb(0, 0, 0);
            quote.TableLineColor = Color.FromArgb(0, 0, 0);
            quote.HeaderFontColor = Color.FromArgb(0, 255, 255);
            quote.QuoteBackColor1 = Color.FromArgb(0, 0, 0);
            quote.QuoteBackColor2 = Color.FromArgb(0, 0, 0);
            quote.QuoteType = GetQuoteType(ex);
            quote.SelectedColor = Color.FromArgb(75, 75, 75);
            quote.MenuEnable = true;
            quote.OpenKChartEvent += new Action<Symbol>(OnOpenKChartEvent);
            quote.RightLeftMoveEvent += new Action<PreviewKeyDownEventArgs>(OnRightLeftMoveEvent);

            ComponentFactory.Krypton.Navigator.KryptonPage page = new ComponentFactory.Krypton.Navigator.KryptonPage(ex.Title);
            page.Tag = ex;
            
            page.Controls.Add(quote);
            quote.Dock = DockStyle.Fill;
            navigator.Pages.Add(page);

            exchangeQuoteMap.TryAdd(ex.EXCode, quote);
            exchangeMap.TryAdd(ex.EXCode, ex);
            pageMap.TryAdd(ex.EXCode, page);

        }

        void OnRightLeftMoveEvent(PreviewKeyDownEventArgs obj)
        {
            if (obj.KeyCode == Keys.Right)
            {
                navigator.SelectNextPage(true);
            }
            else if (obj.KeyCode == Keys.Left)
            {
                navigator.SelectPreviousPage(true);
            }

        }

        void OnOpenKChartEvent(Symbol symbol)
        {
            if (OpenChartEvent != null)
            {
                OpenChartEvent(symbol);
            }
        }

        /// <summary>
        /// 添加单个合约
        /// </summary>
        /// <param name="symbol"></param>
        public void AddSymbol(Symbol symbol)
        {
            ViewQuoteList target = GetQuoteList(symbol.Exchange);
            if (target != null)
                target.AddSymbol(symbol);
        }

        /// <summary>
        /// 添加一组合约
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="symbols"></param>
        public void AddSymbols(IExchange exchange, IEnumerable<Symbol> symbols)
        {
            ViewQuoteList target = GetQuoteList(exchange.EXCode);
            if (target != null)
                target.AddSymbols(symbols);
        }


        public void ShowQuote(Symbol symbol)
        {
            ComponentFactory.Krypton.Navigator.KryptonPage target = null;
            string exchange = symbol.Exchange;
            if (pageMap.TryGetValue(exchange, out target))
            {
                navigator.SelectedPage = target;
                ViewQuoteList quote = null;
                if (exchangeQuoteMap.TryGetValue(exchange, out quote))
                {
                    quote.Focus();
                }
            }



        }
        public void GotTick(Symbol symbol, Tick k)
        {
            ViewQuoteList target = GetQuoteList(symbol.Exchange);
            if (target != null)
                target.GotTick(k);
        }
    }
}
