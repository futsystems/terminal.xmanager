using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class fmFollowStrategyCfg : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmFollowStrategyCfg()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(direction).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowDirection>());

            MoniterHelper.AdapterToIDataSource(entrypricetype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowPriceType>());
            MoniterHelper.AdapterToIDataSource(entrypendingtype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumPendingThresholdType>());
            MoniterHelper.AdapterToIDataSource(entrypendingoptype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumPendingOperationType>());

            MoniterHelper.AdapterToIDataSource(exitpricetype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowPriceType>());
            MoniterHelper.AdapterToIDataSource(exitpendingtype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumPendingThresholdType>());
            MoniterHelper.AdapterToIDataSource(exitpendingoptype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumPendingOperationType>());

            MoniterHelper.AdapterToIDataSource(stopvaluetype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowProtectValueType>());
            MoniterHelper.AdapterToIDataSource(profit1valuetype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowProtectValueType>());
            MoniterHelper.AdapterToIDataSource(profit2value1type).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowProtectValueType>());
            MoniterHelper.AdapterToIDataSource(profit2value2type).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumFollowProtectValueType>());


            entrypricetype.SelectedValueChanged += new EventHandler(entrypricetype_SelectedValueChanged);
            exitpricetype.SelectedValueChanged += new EventHandler(exitpricetype_SelectedValueChanged);
            

            this.Text = "添加跟单策略";
            this.Load += new EventHandler(fmFollowStrategyCfg_Load);
        }

        void exitpricetype_SelectedValueChanged(object sender, EventArgs e)
        {
            QSEnumFollowPriceType val = (QSEnumFollowPriceType)exitpricetype.SelectedValue;
            if (val == QSEnumFollowPriceType.SignalPrice)
            {
                exitpricetickoffset.Enabled = true;
            }
            else
            {
                exitpricetickoffset.Enabled = false;
            }
        }

        void entrypricetype_SelectedValueChanged(object sender, EventArgs e)
        {

            QSEnumFollowPriceType val = (QSEnumFollowPriceType)entrypricetype.SelectedValue;
            if (val == QSEnumFollowPriceType.SignalPrice)
            {
                entrypricetickoffset.Enabled = true;
            }
            else
            {
                entrypricetickoffset.Enabled = false;
            }
        }

        void fmFollowStrategyCfg_Load(object sender, EventArgs e)
        {
            exitpricetype_SelectedValueChanged(null,null);
            entrypricetype_SelectedValueChanged(null, null);

            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);
            this.btnAddTimeSpan.Click += new EventHandler(btnAddTimeSpan_Click);
            this.btnRemoveTimeSpan.Click += new EventHandler(btnRemoveTimeSpan_Click);
            CoreService.EventCore.RegIEventHandler(this);
            
        }

        void btnRemoveTimeSpan_Click(object sender, EventArgs e)
        {
            if (timefilter.Items.Count == 0) return;
            if (timefilter.SelectedItems.Count == 0)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请选中左侧某个时间段");
                return;
            }
            timefilter.Items.RemoveAt(timefilter.SelectedIndex);
        }

        void btnAddTimeSpan_Click(object sender, EventArgs e)
        {
            int v1 = start.Value.ToTLTime();
            int v2 = end.Value.ToTLTime();
            if (v2 <= v1)
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("结束时间需要大于开始时间");
                return;
            }
            string ts = string.Format("{0}-{1}",v1,v2);
            foreach (var v in timefilter.Items)
            {
                if (v.ToString() == ts)
                {
                    ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("已经添加过对应时间段");
                    return;
                }
            }
            timefilter.Items.Add(ts);
        }

        string GetTimeFilter()
        {
            if (timefilter.Items.Count == 0) return string.Empty;
            List<string> tmp = new List<string>();
            foreach (var item in timefilter.Items)
            {
                tmp.Add(item.ToString());
            }
            return string.Join(",", tmp.ToArray());
        }

        void ParseTimeFilter(string filter)
        {
            timefilter.Items.Clear();
            if (string.IsNullOrEmpty(filter)) return;
            string[] tmp = filter.Split(',');
            foreach (var item in tmp)
            {
                timefilter.Items.Add(item);
            }
        }
        public void OnInit()
        {
            if (_cfg == null)
            {
                CoreService.EventCore.RegisterCallback("FollowCentre", "QryAvabileStrategyAccount", OnQryAvabileStrategyAccount);
          
                CoreService.TLClient.ReqContribRequest("FollowCentre", "QryAvabileStrategyAccount","");
     
            }
        }

        public void OnDisposed()
        {
            if (_cfg == null)
            {
                CoreService.EventCore.UnRegisterCallback("FollowCentre", "QryAvabileStrategyAccount", OnQryAvabileStrategyAccount);
          
            }
        
        }


        void OnQryAvabileStrategyAccount(string json,bool last)
        {
            string[] accounts = CoreService.ParseJsonResponse<string[]>(json);
            if (accounts != null)
            {
                InvokeGotAvabileStrategyAccount(accounts);
            }
        }

        void InvokeGotAvabileStrategyAccount(string[] accounts)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string[]>(InvokeGotAvabileStrategyAccount), new object[] { accounts });
            }
            else
            {
                ArrayList acclist = new ArrayList();
                foreach (string account in accounts)
                {
                    ValueObject<string> vo = new ValueObject<string>();
                    vo.Name = account;
                    vo.Value = account;
                    acclist.Add(vo);
                }
                MoniterHelper.AdapterToIDataSource(followaccount).BindDataSource(acclist);

            }
        }

        bool ValidSecFilter()
        { 
            if(string.IsNullOrEmpty(secfilter.Text)) return true;
            string[] seclist = secfilter.Text.Split(',');
            foreach (var sec in seclist)
            {
                if (!CoreService.BasicInfoTracker.Securities.Any(s => s.Code == sec))
                    return false;
            }
            return true;
        }
        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidSecFilter())
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("品种过滤设置错误");
                return;
            }

            //添加
            if (_cfg == null)
            {
                if (MoniterHelper.WindowConfirm("确认添加跟单策略?") == System.Windows.Forms.DialogResult.Yes)
                {
                    FollowStrategyConfig cfg = new FollowStrategyConfig();

                    cfg.Token = token.Text;
                    cfg.FollowDirection = (QSEnumFollowDirection)direction.SelectedValue;
                    cfg.FollowPower = (int)power.Value;
                    cfg.Account = (string)followaccount.SelectedValue;

                    cfg.EntryPriceType = (QSEnumFollowPriceType)entrypricetype.SelectedValue;
                    cfg.EntryOffsetTicks = (int)entrypricetickoffset.Value;
                    cfg.EntryPendingThresholdType = (QSEnumPendingThresholdType)entrypendingtype.SelectedValue;
                    cfg.EntryPendingThresholdValue = (int)entrypendingvalue.Value;
                    cfg.EntryPendingOperationType = (QSEnumPendingOperationType)entrypendingoptype.SelectedValue;

                    cfg.ExitPriceType = (QSEnumFollowPriceType)exitpricetype.SelectedValue;
                    cfg.ExitOffsetTicks = (int)exitpricetickoffset.Value;
                    cfg.ExitPendingThreadsholdType = (QSEnumPendingThresholdType)exitpendingtype.SelectedValue;
                    cfg.ExitPendingThresholdValue = (int)exitpendingvalue.Value;
                    cfg.ExitPendingOperationType = (QSEnumPendingOperationType)exitpendingoptype.SelectedValue;

                    cfg.SecFilter = secfilter.Text;
                    cfg.SizeFilter = (int)sizefilter.Value;
                    cfg.TimeFilter = GetTimeFilter();
                    cfg.StopEnable = stopenable.Checked;
                    cfg.StopValue = stopvalue.Value;
                    cfg.StopValueType = (QSEnumFollowProtectValueType)stopvaluetype.SelectedValue;
                    cfg.Profit1Enable = profit1enable.Checked;
                    cfg.Profit1Value = profit1value.Value;
                    cfg.Profit1ValueType = (QSEnumFollowProtectValueType)profit1valuetype.SelectedValue;
                    cfg.Profit2Enable = profit2enable.Checked;
                    cfg.Profit2Value1 = profit2value1.Value;
                    cfg.Profit2Trailing1 = profit2trailing1.Value;
                    cfg.Profit2Value2 = profit2value2.Value;
                    cfg.Profit2Trailing2 = profit2trailing2.Value;
                    cfg.Profit2Value1Type = (QSEnumFollowProtectValueType)profit2value1type.SelectedValue;
                    cfg.Profit2Value2Type = (QSEnumFollowProtectValueType)profit2value2type.SelectedValue;

                    if (string.IsNullOrEmpty(cfg.Account))
                    {
                        ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("跟单策略需要绑定下单账户");
                        return;
                    }
                    CoreService.TLClient.ReqContribRequest("FollowCentre", "UpdateFollowStrategyCfg", cfg);
                    this.Close();
                }
            }
            else
            {
                if (MoniterHelper.WindowConfirm("确认更新跟单策略参数?") == System.Windows.Forms.DialogResult.Yes)
                {
                    _cfg.EntryPriceType = (QSEnumFollowPriceType)entrypricetype.SelectedValue;
                    _cfg.EntryOffsetTicks = (int)entrypricetickoffset.Value;
                    _cfg.EntryPendingThresholdType = (QSEnumPendingThresholdType)entrypendingtype.SelectedValue;
                    _cfg.EntryPendingThresholdValue = (int)entrypendingvalue.Value;
                    _cfg.EntryPendingOperationType = (QSEnumPendingOperationType)entrypendingoptype.SelectedValue;

                    _cfg.ExitPriceType = (QSEnumFollowPriceType)exitpricetype.SelectedValue;
                    _cfg.ExitOffsetTicks = (int)exitpricetickoffset.Value;
                    _cfg.ExitPendingThreadsholdType = (QSEnumPendingThresholdType)exitpendingtype.SelectedValue;
                    _cfg.ExitPendingThresholdValue = (int)exitpendingvalue.Value;
                    _cfg.ExitPendingOperationType = (QSEnumPendingOperationType)exitpendingoptype.SelectedValue;


                    _cfg.SecFilter = secfilter.Text;
                    _cfg.SizeFilter = (int)sizefilter.Value;
                    _cfg.TimeFilter = GetTimeFilter();
                    _cfg.StopEnable = stopenable.Checked;
                    _cfg.StopValue = stopvalue.Value;
                    _cfg.StopValueType = (QSEnumFollowProtectValueType)stopvaluetype.SelectedValue;
                    _cfg.Profit1Enable = profit1enable.Checked;
                    _cfg.Profit1Value = profit1value.Value;
                    _cfg.Profit1ValueType = (QSEnumFollowProtectValueType)profit1valuetype.SelectedValue;
                    _cfg.Profit2Enable = profit2enable.Checked;
                    _cfg.Profit2Value1 = profit2value1.Value;
                    _cfg.Profit2Trailing1 = profit2trailing1.Value;
                    _cfg.Profit2Value2 = profit2value2.Value;
                    _cfg.Profit2Trailing2 = profit2trailing2.Value;
                    _cfg.Profit2Value1Type = (QSEnumFollowProtectValueType)profit2value1type.SelectedValue;
                    _cfg.Profit2Value2Type = (QSEnumFollowProtectValueType)profit2value2type.SelectedValue;


                    CoreService.TLClient.ReqContribRequest("FollowCentre", "UpdateFollowStrategyCfg", _cfg);

                    //this.Close();
                }

            }

        }

        FollowStrategyConfig _cfg = null;
        public void SetFollowStrategyConfig(FollowStrategyConfig cfg)
        {
            this.Text = string.Format("编辑跟单策略:{0}[{1}]", cfg.Token, cfg.ID);
            _cfg = cfg;
            token.Text = cfg.Token;
            direction.SelectedValue = cfg.FollowDirection;
            power.Value = cfg.FollowPower;
            followaccount.Text = cfg.Account;

            entrypricetype.SelectedValue = cfg.EntryPriceType;
            entrypricetickoffset.Value = cfg.EntryOffsetTicks;
            entrypendingtype.SelectedValue = cfg.EntryPendingThresholdType;
            entrypendingvalue.Value = cfg.EntryPendingThresholdValue;
            entrypendingoptype.SelectedValue = cfg.EntryPendingOperationType;

            exitpricetype.SelectedValue = cfg.ExitPriceType;
            exitpricetickoffset.Value = cfg.ExitOffsetTicks;
            exitpendingtype.SelectedValue = cfg.ExitPendingThreadsholdType;
            exitpendingvalue.Value = cfg.ExitPendingThresholdValue;
            exitpendingoptype.SelectedValue = cfg.ExitPendingOperationType;

            secfilter.Text = cfg.SecFilter;
            sizefilter.Value = cfg.SizeFilter;
            ParseTimeFilter(cfg.TimeFilter);
            stopvalue.Value = cfg.StopValue;
            stopvaluetype.SelectedValue = cfg.StopValueType;
            stopenable.Checked = cfg.StopEnable;

            profit1value.Value = cfg.Profit1Value;
            profit1valuetype.SelectedValue = cfg.Profit1ValueType;
            profit1enable.Checked = cfg.Profit1Enable;
            profit2value1.Value = cfg.Profit2Value1;
            profit2trailing1.Value = cfg.Profit2Trailing1;
            profit2value1type.SelectedValue = cfg.Profit2Value1Type;
            profit2value2.Value = cfg.Profit2Value2;
            profit2trailing2.Value = cfg.Profit2Trailing2;
            profit2value2type.SelectedValue = cfg.Profit2Value2Type;
            profit2enable.Checked = cfg.Profit2Enable;



            token.Enabled = false;
            direction.Enabled = false;
            //power.Enabled = false;
            followaccount.Enabled = false;
        }
    }
}
