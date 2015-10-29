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
    public partial class fmSettleManager : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmSettleManager()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(exchangelist).BindDataSource(MoniterHelper.GetExchangeList());
            this.Load += new EventHandler(fmSettleManager_Load);
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("SettleCentre", "RollBackToDay", OnRollBack);
            CoreService.EventContrib.RegisterCallback("SettleCentre", "QrySettleStatus", OnQrySettleStatus);
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("SettleCentre", "RollBackToDay", OnRollBack);
            CoreService.EventContrib.UnRegisterCallback("SettleCentre", "QrySettleStatus", OnQrySettleStatus);
        }

        void OnQrySettleStatus(string json,bool islast)
        {
            var data = TradingLib.Mixins.Json.JsonMapper.ToObject(json)["Payload"];

            InvokeGotSettleStatus(data);
        }

        /// <summary>
        /// 相应回滚到某个交易日的回报
        /// </summary>
        /// <param name="json"></param>
        void OnRollBack(string json,bool islast)
        {
            //Globals.Debug("?????????????? rollbacktoday called finished....");
            int settleday = Util.ToTLDate(dpSettleday.Value);
            //查询结算价信息
            CoreService.TLClient.ReqQrySettlementPrice(settleday);

            //查询结算状态信息
            CoreService.TLClient.ReqQrySettleStatus();

            //查询持仓数据
            CoreService.TLClient.ReqQryPositionHold();
            
        }
        void fmSettleManager_Load(object sender, EventArgs e)
        {
            //btnDelSettleInfo.Click += new EventHandler(btnDelSettleInfo_Click);
            btnLoadInfo.Click += new EventHandler(btnLoadInfo_Click);
            btnReSettle.Click += new EventHandler(btnReSettle_Click);
            btnResetSystem.Click += new EventHandler(btnResetSystem_Click);
            btnSettleExchange.Click += new EventHandler(btnSettleExchange_Click);
            CoreService.EventCore.RegIEventHandler(this);

            //查询结算状态
            CoreService.TLClient.ReqQrySettleStatus();
        }

        void btnSettleExchange_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认执行交易所结算?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqReSettleExchange(exchangelist.SelectedValue.ToString());
            }
        }

        void btnResetSystem_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认重置系统 进入工作状态") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqResetSystem();
            }
        }


        void btnReSettle_Click(object sender, EventArgs e)
        {
            int settleday = Util.ToTLDate(dpSettleday.Value);
            if (MoniterHelper.WindowConfirm(string.Format("确认对交易日:{0}执行重新结算操作", settleday)) == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqReSettle(settleday);
            }
        }

        void btnLoadInfo_Click(object sender, EventArgs e)
        {
            int settleday = Util.ToTLDate(dpSettleday.Value);
            if (MoniterHelper.WindowConfirm(string.Format("确认回滚到交易日:{0}", settleday)) == System.Windows.Forms.DialogResult.Yes)
            {
                //清空结算价信息表 用于准备获得回滚日期对应的结算价信息
                ctSettlementPrice1.Clear();
                ctPositionHold1.Clear();
                CoreService.TLClient.ReqRollBackToDay(settleday);
            }
        }

        //void btnDelSettleInfo_Click(object sender, EventArgs e)
        //{
        //    int settleday = Util.ToTLDate(dpSettleday.Value);

        //    if (MoniterHelper.WindowConfirm(string.Format("确认删除交易日:{0}", settleday)) == System.Windows.Forms.DialogResult.Yes)
        //    {
        //        CoreService.TLClient.ReqDeleteSettleInfo(settleday);
        //    }
        //}

        void InvokeGotSettleStatus(TradingLib.Mixins.Json.JsonData data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<TradingLib.Mixins.Json.JsonData>(InvokeGotSettleStatus), new object[] { data });
            }
            else
            {
                lbLastSettleday.Text = data["last_settleday"].ToString();
                lbCurrentday.Text = data["current_settleday"].ToString();
                lbSettleMode.Text = ((QSEnumSettleMode)Enum.Parse(typeof(QSEnumSettleMode), data["settle_mode"].ToString())).ToString();
            }
        }
    }
}
