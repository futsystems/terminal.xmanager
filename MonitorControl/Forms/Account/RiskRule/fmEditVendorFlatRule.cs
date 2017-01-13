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
    public partial class fmEditVendorFlatRule : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmEditVendorFlatRule()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmEditRiskItem_Load);
        }

        void fmEditRiskItem_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            btnDel.Click += new EventHandler(btnDel_Click);
            CoreService.EventCore.RegIEventHandler(this);
        }



        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback("RiskCentre", "QryVendorFlatRule", this.OnQryVenderFlatRule);
            this.ReqQryVendorFlatRule(_account.Account);

        }

        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback("RiskCentre", "QryVendorFlatRule", this.OnQryVenderFlatRule);
            
        }


        void OnQryVenderFlatRule(string json,bool islast)
        {
            var payload = CoreService.ParseJsonResponse(json);
            if (payload != null)
            {
                var _equity = decimal.Parse(payload["equity"].ToString());//初始权益
                var _warn_level = int.Parse(payload["warn_level"].ToString());//报警线
                var _flat_level = int.Parse(payload["flat_level"].ToString());//强平线
                var _night_hold = decimal.Parse(payload["night_hold"].ToString());//过夜倍数

                equity.Value = _equity;
                warnlevel.Value = _warn_level;
                flatlevel.Value = _flat_level;
                nighthold.Value = _night_hold;
            }
        }


        AccountItem _account = null;
        public void SetAccount(AccountItem account)
        {
            _account = account;
            this.Text = string.Format("强平设置[{0}]", _account.Account);
            equity.Value = _account.NowEquity;


        }
        void btnSubmit_Click(object sender, EventArgs e)
        {
            var args = new
            {
                account = _account.Account,
                equity = equity.Value,
                warn_level = (int)warnlevel.Value,
                flat_level = (int)flatlevel.Value,
                night_hold = nighthold.Value
            };

            if (MoniterHelper.WindowConfirm("确认更新强平规则?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.ReqUpdateVendorFlatRule(args);
                this.Close();
            }
        }

        void btnDel_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认删除强平规则?") == System.Windows.Forms.DialogResult.Yes)
            {
                this.ReqDelVendorFlatRule(_account.Account);

            }
        }

        /// <summary>
        /// 更新主帐户风控强平规则
        /// </summary>
        /// <param name="obj"></param>
        void ReqUpdateVendorFlatRule(object obj)
        {
            CoreService.TLClient.ReqContribRequest("RiskCentre", "UpdateVendorFlatRule", obj);
        }

        /// <summary>
        /// 查询主帐户风控强平规则
        /// </summary>
        /// <param name="account"></param>
        void ReqQryVendorFlatRule(string account)
        {
            CoreService.TLClient.ReqContribRequest("RiskCentre", "QryVendorFlatRule", account);
        }

        /// <summary>
        /// 删除某个帐户的主帐户风控规则
        /// </summary>
        /// <param name="account"></param>
        void ReqDelVendorFlatRule(string account)
        {
            CoreService.TLClient.ReqContribRequest("RiskCentre", "DelVendorFlatRule", account);
        }
        private void equity_ValueChanged(object sender, EventArgs e)
        {
            lbFlat.Text = ((equity.Value * flatlevel.Value / 100)).ToFormatStr();
            lbWarn.Text = ((equity.Value * warnlevel.Value / 100)).ToFormatStr();
            lbNight.Text = ((equity.Value * nighthold.Value)).ToFormatStr();
        }

        private void flatlevel_ValueChanged(object sender, EventArgs e)
        {
            lbFlat.Text = ((equity.Value * flatlevel.Value / 100)).ToFormatStr();
        }

        private void warnlevel_ValueChanged(object sender, EventArgs e)
        {
            lbWarn.Text = ((equity.Value * warnlevel.Value / 100)).ToFormatStr();
        }

        private void nighthold_ValueChanged(object sender, EventArgs e)
        {
            lbNight.Text = ((equity.Value * nighthold.Value)).ToFormatStr();
        }
    }
}
