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
    [MoniterControlAttr("QuoteMoniter", "分帐户列表", EnumControlLocation.TopPanel)]
    public partial class ctQuoteMoniter : UserControl,IEventBinder,IMoniterControl
    {
        public ctQuoteMoniter()
        {
            InitializeComponent();
            viewquotemap.Add("SHFE", quote_shfe);
            viewquotemap.Add("DCE", quote_dce);
            viewquotemap.Add("CZCE", quote_czce);
            viewquotemap.Add("CFFEX", quote_cffex);

            this.Load += new EventHandler(ctQuoteMoniter_Load);
        }

        //public string UUID { get { return } }
        Dictionary<string, ViewQuoteList> viewquotemap = new Dictionary<string, ViewQuoteList>();
        void ctQuoteMoniter_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);

        }


        ViewQuoteList GetViewQuote(Symbol symbol)
        {
            if (symbol == null) return null;
            return GetViewQuote(symbol.SecurityFamily);
                
        }
        ViewQuoteList GetViewQuote(SecurityFamily sec)
        {
            if (sec == null) return null;
            if (sec.Exchange == null) return null;

            ViewQuoteList vq = null;
            
            if (viewquotemap.TryGetValue(sec.Exchange.EXCode, out vq))
            {
                return vq;
            }
            return null;
        }

        public void OnInit()
        {
            //if (!CoreService.SiteInfo.Domain.Super)
            //{
            //    ctOrderSenderM1.Visible = CoreService.SiteInfo.Manager.IsRoot() || CoreService.SiteInfo.UIAccess.r_execution;
            //    if (!ctOrderSenderM1.Visible)
            //    {
            //        quotenav.Height = quotenav.Height + ctOrderSenderM1.Height;

            //    }
            //}

            quote_cffex.SymbolSelectedEvent += new SymbolDelegate(SelectSymbol);
            quote_czce.SymbolSelectedEvent += new SymbolDelegate(SelectSymbol);
            quote_dce.SymbolSelectedEvent += new SymbolDelegate(SelectSymbol);
            quote_shfe.SymbolSelectedEvent += new SymbolDelegate(SelectSymbol);

            //初始化合约列表
            foreach (Symbol s in CoreService.BasicInfoTracker.GetSymbolTradable())
            {
                //quote_czce.addSecurity(s);

                ViewQuoteList vq = GetViewQuote(s);
                if (vq != null)
                {
                    vq.addSecurity(s);
                }
            }
            //响应行情回报行情

            CoreService.EventIndicator.GotTickEvent += new Action<Tick>(GotTick);

            CoreService.EventBasicInfo.OnSymbolEvent += new Action<SymbolImpl>(GotSymbol);
            CoreService.EventBasicInfo.OnSecurityEvent += new Action<SecurityFamilyImpl>(GotSecurity);
        }

        public void OnDisposed()
        {
            CoreService.EventIndicator.GotTickEvent -= new Action<Tick>(GotTick);
            CoreService.EventBasicInfo.OnSymbolEvent -= new Action<SymbolImpl>(GotSymbol);
            CoreService.EventBasicInfo.OnSecurityEvent -= new Action<SecurityFamilyImpl>(GotSecurity);
        }

        void SelectSymbol(Symbol symbol)
        {
            //ctOrderSenderM1.SetSymbol(symbol);
            CoreService.EventUI.FireSymbolselectedEvent(symbol);
        }

        void GotSecurity(SecurityFamily sec)
        {
            //Globals.Debug("got security reply");
            ViewQuoteList vq = GetViewQuote(sec);
            if (vq == null) return;
            //品种可交易 则加载该品种的所有可交易合约
            if (sec.Tradeable)
            {
                //Globals.Debug("品种:" + sec.Code + " 可交易");
                foreach (Symbol s in CoreService.BasicInfoTracker.GetSymbolTradable().Where(s => s.SecurityFamily.Code.Equals(sec.Code)))
                {
                    //Globals.Debug("添加合约:" + s.Symbol);
                    vq.addSecurity(s);
                }
            }
            else//如果不可交易 则删除所有报价列表的合约
            {
                //Globals.Debug("品种:" + sec.Code + " 不可交易");
                Symbol[] syms = vq.Symbols.Where(s => s.SecurityFamily.Code.Equals(sec.Code)).ToArray();
                foreach (Symbol sym in syms)
                {
                    //Globals.Debug("删除合约:" + sym.Symbol);
                    vq.delSecurity(sym);
                }
            }
        }
        void GotSymbol(SymbolImpl symbol)
        {
            if (symbol.IsTradeable)
            {
                ViewQuoteList vq = GetViewQuote(symbol);
                if (vq != null)
                {
                    vq.addSecurity(symbol);
                }
            }
            else
            {
                ViewQuoteList vq = GetViewQuote(symbol);
                if (vq != null)
                {
                    vq.delSecurity(symbol);
                }
            }
        }

        void GotTick(Tick k)
        {
            Symbol sym = CoreService.BasicInfoTracker.GetSymbol(k.Symbol);
            ViewQuoteList vq = GetViewQuote(sym);
            if (vq != null)
            {
                vq.GotTick(k);
            }
        }


    }
}
