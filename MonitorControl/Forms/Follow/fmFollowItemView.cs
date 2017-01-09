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
    public partial class fmFollowItemView : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmFollowItemView()
        {
            InitializeComponent();
            itemsPanel.Visible = false;

            this.Load += new EventHandler(fmFollowItemView_Load);
        }

        void fmFollowItemView_Load(object sender, EventArgs e)
        {

            CoreService.EventCore.RegIEventHandler(this);
            this.btnFlat.Click += new EventHandler(btnFlat_Click);
          
        }

        void btnFlat_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认平掉该跟单项目对应持仓?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqContribRequest("FollowCentre", "FlatFollowItem", _followkey);
            }
        }



        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("FollowCentre", "QryFollowItemDetail", OnQryFollowItemDetail);

            CoreService.TLClient.ReqContribRequest("FollowCentre", "QryFollowItemDetail", _followkey);
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("FollowCentre", "QryFollowItemDetail", OnQryFollowItemDetail);

            
        }

        string _followkey = string.Empty;
        public void SetFollowKey(string followkey)
        {
            _followkey = followkey;
        }
        void OnQryFollowItemDetail(string json, bool islast)
        {
            FollowItemDetail detail = CoreService.ParseJsonResponse<FollowItemDetail>(json);
            if (detail != null)
            {
                GotFollowItemDetail(detail);
                //this.Visible = true;
            }
        }

        void GotFollowItemDetail(FollowItemDetail detail)
        {
            int y_location = 0;
            if (InvokeRequired)
            {
                Invoke(new Action<FollowItemDetail>(GotFollowItemDetail), new object[] { detail });
            }
            else
            {
                ctFollowItemSignalTrade signaltrade = new ctFollowItemSignalTrade();
                signaltrade.GotSignalTrade(detail.EntrySignalTrade);
                signaltrade.Location = new Point(0, y_location); y_location += signaltrade.Size.Height;
                itemsPanel.Controls.Add(signaltrade);


                //遍历所有委托显示委托数据
                foreach (var order in detail.EntrySignalTrade.Orders)
                {
                    ctFollowOrderView orderview = new ctFollowOrderView();
                    orderview.GotOrder(order);
                    orderview.Location = new Point(0, y_location); y_location += orderview.Size.Height;
                    itemsPanel.Controls.Add(orderview);
                }

                y_location += 10;
                //输出平仓信号成交与对应的委托
                foreach (var trade in detail.ExitSignalTrades)
                {
                    ctFollowItemSignalTrade tmp = new ctFollowItemSignalTrade();
                    tmp.GotSignalTrade(trade);
                    tmp.Location = new Point(0, y_location); y_location += tmp.Size.Height;
                    itemsPanel.Controls.Add(tmp);

                    //遍历所有委托显示委托数据
                    foreach (var order in trade.Orders)
                    {
                        ctFollowOrderView orderview = new ctFollowOrderView();
                        orderview.GotOrder(order);
                        orderview.Location = new Point(0, y_location); y_location += orderview.Size.Height;
                        itemsPanel.Controls.Add(orderview);
                    }
                }

                itemsPanel.Visible = true;
            }
        }
    }
}
