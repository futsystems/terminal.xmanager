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
    public partial class ctQueryOrder : UserControl
    {
        public ctQueryOrder()
        {
            InitializeComponent();

            CoreService.EventQuery.OnRspMGRQryOrderResponse += new Action<Order, RspInfo, int, bool>(EventQuery_OnRspMGRQryOrderResponse);
            btnQry.Click += new EventHandler(btnQry_Click);
        }

        int _qryid = 0;
        DateTime _lastqrytime = DateTime.Now;

        void btnQry_Click(object sender, EventArgs e)
        {
            //查询频率过滤
            if (DateTime.Now.Subtract(_lastqrytime).TotalSeconds < UIConstant.QRYINTERVAL)
            {
                //TraderHelper.WindowMessage("请勿频繁查询");
                return;
            }



            //logger.Info("Qry Hist Order");
            ctOrderViewSTK1.Clear();
            _lastqrytime = DateTime.Now;
            btnQry.Enabled = false;
            _qryid = CoreService.TLClient.ReqQryHistOrders(account.Text, Util.ToTLDate(start.Value), Util.ToTLDate(end.Value));
        }

        void EventQuery_OnRspMGRQryOrderResponse(Order arg1, RspInfo arg2, int arg3, bool arg4)
        {
            if (arg3 != _qryid) return;//查询RequestID不一致表面非当前控件查询 直接返回
            //返回委托对象不为空则调用OrderView进行输出显示
            if (arg1 != null)
            {
                ctOrderViewSTK1.GotOrder(arg1);
            }
            //如果是最后一条查询则重置查询ID和按钮可用
            if (arg4)
            {
                _qryid = 0;
                btnQry.Enabled = true;
            }
        }
    }
}
