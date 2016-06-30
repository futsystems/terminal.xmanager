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
using TradingLib.TinyMGRControl;

namespace TinyMgr
{
    public partial class ctQueryTrades : UserControl
    {
        public ctQueryTrades()
        {
            InitializeComponent();

            CoreService.EventQuery.OnRspMGRQryFillResponse += new Action<Trade, RspInfo, int, bool>(EventQuery_OnRspMGRQryFillResponse);
            btnQry.Click += new EventHandler(btnQry_Click);
        }


        int _qryid = 0;
        DateTime _lastqrytime = DateTime.Now;


        void btnQry_Click(object sender, EventArgs e)
        {
            //查询频率过滤
            if (DateTime.Now.Subtract(_lastqrytime).TotalSeconds < UIConstant.QRYINTERVAL)
            {
                MoniterHelper.WindowMessage("请勿频繁查询");
                return;
            }

            ctTradeViewSTK1.Clear();
            _lastqrytime = DateTime.Now;
            _qryid = CoreService.TLClient.ReqQryHistTrades(account.Text,Util.ToTLDate(start.Value), Util.ToTLDate(end.Value));
        }

        void EventQuery_OnRspMGRQryFillResponse(Trade arg1, RspInfo arg2, int arg3, bool arg4)
        {
            if (arg3 != _qryid) return;//查询RequestID不一致表面非当前控件查询 直接返回
            //返回委托对象不为空则调用OrderView进行输出显示
            if (arg1 != null)
            {
                ctTradeViewSTK1.GotFill(arg1);
            }
            //如果是最后一条查询则重置查询ID和按钮可用
            if (arg4)
            {
                _qryid = 0;
            }
        }
    }
}
