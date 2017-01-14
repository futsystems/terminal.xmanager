using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;



namespace TradingLib.MoniterControl
{

    public partial class fmHistQuery : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmHistQuery()
        {
            InitializeComponent();

            this.Load += new EventHandler(fmHistQuery_Load);
            settleday.Value = DateTime.Now;
        }

        void fmHistQuery_Load(object sender, EventArgs e)
        {
            this.btnQryHist.Click +=new EventHandler(btnQryHist_Click);
            this.btnExport.Click += new EventHandler(btnExport_Click);
            CoreService.EventCore.RegIEventHandler(this);
        }

        void btnExport_Click(object sender, EventArgs e)
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView gv = GetCurrentGridView();
            if (gv != null)
            {
                MoniterHelper.ExportToCSV(GetCsvFilePrefix(), gv);
            }
        }

        ComponentFactory.Krypton.Toolkit.KryptonDataGridView GetCurrentGridView()
        {
            if (kryptonNavigator1.SelectedPage.Name == "pageOrder")
            {
                return ctHistOrder1.GridView;
            }
            else if (kryptonNavigator1.SelectedPage.Name == "pageTrade")
            {
                return ctHistTrade1.GridView;
            }
            else if (kryptonNavigator1.SelectedPage.Name == "pagePosition")
            {
                return ctHistPosition1.GridView;
            }
            return null;
        }
        string GetCsvFilePrefix()
        {
            string b= string.Format("{0}_{1}", account.Text, Settleday);
            if (kryptonNavigator1.SelectedPage.Name == "pageOrder")
            {
                return b + "_Order";
            }
            else if (kryptonNavigator1.SelectedPage.Name == "pageTrade")
            {
                return b + "_Trade";
            }
            else if (kryptonNavigator1.SelectedPage.Name == "pagePosition")
            {
                return b + "_Position";
            }
            return b;
        }
        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_ORDER, OnOrder);
            CoreService.EventCore.RegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_TRADE, OnTrade);
            CoreService.EventCore.RegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_POSITION, OnPositioin);
        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_ORDER, OnOrder);
            CoreService.EventCore.UnRegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_TRADE, OnTrade);
            CoreService.EventCore.UnRegisterCallback(Modules.ACC_MGR, Method_ACC_MGR.QRY_ACC_POSITION, OnPositioin);
        
        }

        void GotHistPosition(PositionDetail d, RspInfo rsp, int reqid, bool islast)
        {
            if (islast)
            {
                if (d != null)
                {
                    ctHistPosition1.GotHistPosition(d);
                }
            }
            else
            {
                ctHistPosition1.GotHistPosition(d);
            }
        }

        void OnOrder(string json, bool isLast)
        {
            string omessage = CoreService.ParseJsonResponse<string>(json);
            if (!string.IsNullOrEmpty(omessage))
            {
                Order o = OrderImpl.Deserialize(omessage);
                o.oSymbol = CoreService.BasicInfoTracker.GetSymbol(o.Symbol);
                ctHistOrder1.GotHistOrder(o);
            }
            if (isLast)
            {
                CoreService.TLClient.ReqQryAccountTrade(account.Text, Settleday, Settleday);
            }
        }
        void OnTrade(string json, bool isLast)
        {
            string fmessage = CoreService.ParseJsonResponse<string>(json);
            if (!string.IsNullOrEmpty(fmessage))
            {
                Trade f = TradeImpl.Deserialize(fmessage);
                f.oSymbol = CoreService.BasicInfoTracker.GetSymbol(f.Symbol);
                ctHistTrade1.GotHistFill(f);
            }

            if (isLast)
            {
                CoreService.TLClient.ReqQryAccountPosition(account.Text, Settleday);
            }
            
        }

        void OnPositioin(string json, bool isLast)
        {
            string pmessage = CoreService.ParseJsonResponse<string>(json);
            if (!string.IsNullOrEmpty(pmessage))
            {
                PositionDetail p = PositionDetailImpl.Deserialize(pmessage);
                p.oSymbol = CoreService.BasicInfoTracker.GetSymbol(p.Symbol);
                ctHistPosition1.GotHistPosition(p);
            }
        }
        //void GotHistOrder(Order o,RspInfo rsp,int reqid, bool islast)
        //{

        //    if (islast)
        //    {
        //        if (o!=null)
        //        {
        //            ctHistOrder1.GotHistOrder(o);
        //        }
        //        CoreService.TLClient.ReqQryHistTrades(account.Text, Settleday,Settleday);
        //    }
        //    else
        //    {
        //        ctHistOrder1.GotHistOrder(o);
        //    }
        //}
        //public void GotHistTrade(Trade f,RspInfo rsp,int reqid, bool islast)
        //{
        //    if (islast)
        //    {
        //        if (f!=null)
        //        {
        //            ctHistTrade1.GotHistFill(f);
        //        }
        //        CoreService.TLClient.ReqQryHistPosition(account.Text, Settleday);
        //    }
        //    else
        //    {
        //        ctHistTrade1.GotHistFill(f);
        //    }
        //}

        public void SetAccount(string acc)
        {
            account.Text = acc;
        }

        private void fmHistQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        DateTime lastqrytime = DateTime.Now;
        bool _first = true;
        private void btnQryHist_Click(object sender, EventArgs e)
        {
            if (_first)
            {
                _first = false;
            }
            else
            {
                if (!(DateTime.Now.Subtract(lastqrytime).TotalSeconds > 5))
                {
                    MoniterHelper.WindowMessage("请不要频繁查询,每隔5秒查询一次!");
                    return;
                }
            }

            lastqrytime = DateTime.Now;
            //清空当前数据
            ctHistOrder1.Clear();
            ctHistTrade1.Clear();
            ctHistPosition1.Clear();
            CoreService.TLClient.ReqQryAccountOrder(account.Text, Settleday, Settleday);
                
        }

        int Settleday
        {
            get
            {
                return TradingLib.Common.Util.ToTLDate(settleday.Value);
            }
        }
    }
}
