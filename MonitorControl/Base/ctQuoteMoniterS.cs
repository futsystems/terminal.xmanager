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
    /// <summary>
    /// 按交易所分类显示所有合约行情列表
    /// </summary>
    public partial class ctQuoteMoniterS : UserControl,IEventBinder
    {
        ILog logger = LogManager.GetLogger("QuoteMoniterS");

        public ctQuoteMoniterS()
        {
            InitializeComponent();
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            //初始化交易所报价列表
            foreach (var exchange in CoreService.BasicInfoTracker.Exchanges)
            {
                this.AddExchange(exchange);
                IEnumerable<Symbol> symbols = CoreService.BasicInfoTracker.Symbols.Where(sym => sym.SecurityFamily.Exchange.EXCode == exchange.EXCode && sym.IsTradeable).OrderBy(sym => sym.Symbol);
                this.AddSymbols(exchange, symbols);
            }

            //响应行情回报行情
            CoreService.EventIndicator.GotTickEvent += new Action<Tick>(OnTickEvent);
        }

        void OnTickEvent(Tick k)
        {
            Symbol sym = CoreService.BasicInfoTracker.GetSymbol(k.Symbol);
            if (sym == null || k == null) return;
            //快照 成交 盘口 统计数据更新
            if (k.UpdateType == "S" || k.UpdateType == "X" || k.UpdateType == "Q" || k.UpdateType == "F")
            {
                ViewQuoteList target = GetQuoteList(sym.Exchange);
                Tick snapshot = CoreService.BasicInfoTracker.GetTickSnapshot(k.Exchange, k.Symbol);
                if (target != null && snapshot != null)
                {
                    target.GotTick(snapshot);
                }
            }
        }

        public void OnDisposed()
        {
            CoreService.EventIndicator.GotTickEvent -= new Action<Tick>(OnTickEvent);
        }

        ConcurrentDictionary<string, ViewQuoteList> exchangeQuoteMap = new ConcurrentDictionary<string, ViewQuoteList>();
        ConcurrentDictionary<string, Exchange> exchangeMap = new ConcurrentDictionary<string, Exchange>();
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

        EnumQuoteType GetQuoteType(Exchange ex)
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
        public void AddExchange(Exchange ex)
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
            ComponentFactory.Krypton.Navigator.KryptonPage page = new ComponentFactory.Krypton.Navigator.KryptonPage(ex.Title);
            page.Tag = ex;
            
            page.Controls.Add(quote);
            quote.Dock = DockStyle.Fill;
            navigator.Pages.Add(page);

            exchangeQuoteMap.TryAdd(ex.EXCode, quote);
            exchangeMap.TryAdd(ex.EXCode, ex);
            pageMap.TryAdd(ex.EXCode, page);

        }

        /// <summary>
        /// 添加一组合约
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="symbols"></param>
        void AddSymbols(Exchange exchange, IEnumerable<Symbol> symbols)
        {
            ViewQuoteList target = GetQuoteList(exchange.EXCode);
            if (target != null)
                target.AddSymbols(symbols);
        }
    }
}
