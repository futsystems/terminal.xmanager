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

        void profit2enable_CheckedChanged(object sender, EventArgs e)
        {
            profit2value1.Enabled = profit2enable.Checked;
            profit2trailing1.Enabled = profit2enable.Checked;
            profit2value1type.Enabled = profit2enable.Checked;
            profit2value2.Enabled = profit2enable.Checked;
            profit2trailing2.Enabled = profit2enable.Checked;
            profit2value2type.Enabled = profit2enable.Checked;
        }

        void profit1enable_CheckedChanged(object sender, EventArgs e)
        {
            profit1value.Enabled = profit1enable.Checked;
            profit1valuetype.Enabled = profit1enable.Checked;
        }

        void stopenable_CheckedChanged(object sender, EventArgs e)
        {
            stopvalue.Enabled = stopenable.Checked;
            stopvaluetype.Enabled = stopenable.Checked;
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

            this.stopvaluetype.SelectedIndexChanged += new EventHandler(stopvaluetype_SelectedIndexChanged);
            this.profit1valuetype.SelectedIndexChanged += new EventHandler(profit1valuetype_SelectedIndexChanged);
            this.profit2value1type.SelectedIndexChanged += new EventHandler(profit2value1type_SelectedIndexChanged);
            this.profit2value2type.SelectedIndexChanged += new EventHandler(profit2value2type_SelectedIndexChanged);

            this.stopenable.CheckedChanged += new EventHandler(stopenable_CheckedChanged);
            this.profit1enable.CheckedChanged += new EventHandler(profit1enable_CheckedChanged);
            this.profit2enable.CheckedChanged += new EventHandler(profit2enable_CheckedChanged);

            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);
            this.btnAddTimeSpan.Click += new EventHandler(btnAddTimeSpan_Click);
            this.btnRemoveTimeSpan.Click += new EventHandler(btnRemoveTimeSpan_Click);

            
            CoreService.EventCore.RegIEventHandler(this);
            
        }

        void profit2value2type_SelectedIndexChanged(object sender, EventArgs e)
        {
            profit2value2.Value = 0;
            profit2trailing2.Value = 0;
            QSEnumFollowProtectValueType valueType = (QSEnumFollowProtectValueType)profit2value2type.SelectedValue;
            if (valueType == QSEnumFollowProtectValueType.Point)
            {
                profit2value2.Maximum = 1000;
                profit2value2.DecimalPlaces = 0;
                profit2trailing2.Maximum = 1000;
                profit2trailing2.DecimalPlaces = 0;
            }
            else
            {
                profit2value2.Maximum = 100;
                profit2value2.DecimalPlaces = 2;
                profit2trailing2.Maximum = 100;
                profit2trailing2.DecimalPlaces = 2;
            }
        }

        void profit2value1type_SelectedIndexChanged(object sender, EventArgs e)
        {
            profit2value1.Value = 0;
            profit2trailing1.Value = 0;
            QSEnumFollowProtectValueType valueType = (QSEnumFollowProtectValueType)profit2value1type.SelectedValue;
            if (valueType == QSEnumFollowProtectValueType.Point)
            {
                profit2value1.Maximum = 1000;
                profit2value1.DecimalPlaces = 0;
                profit2trailing1.Maximum = 1000;
                profit2trailing1.DecimalPlaces = 0;
            }
            else
            {
                profit2value1.Maximum = 100;
                profit2value1.DecimalPlaces = 2;
                profit2trailing1.Maximum = 100;
                profit2trailing1.DecimalPlaces = 2;
            }
        }

        void profit1valuetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            profit1value.Value = 0;
            QSEnumFollowProtectValueType valueType = (QSEnumFollowProtectValueType)profit1valuetype.SelectedValue;
            if (valueType == QSEnumFollowProtectValueType.Point)
            {
                profit1value.Maximum = 1000;
                profit1value.DecimalPlaces = 0;
            }
            else
            {
                profit1value.Maximum = 100;
                profit1value.DecimalPlaces = 2;
            }
        }

        void stopvaluetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            stopvalue.Value = 0;
            QSEnumFollowProtectValueType valueType = (QSEnumFollowProtectValueType)stopvaluetype.SelectedValue;
            if (valueType == QSEnumFollowProtectValueType.Point)
            {
                stopvalue.Maximum = 1000;
                stopvalue.DecimalPlaces = 0;
            }
            else
            {
                stopvalue.Maximum = 100;
                stopvalue.DecimalPlaces = 2;
            }
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
                    _cfg.FollowPower = (int)power.Value;
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
            stopvaluetype.SelectedValue = cfg.StopValueType;
            stopvaluetype_SelectedIndexChanged(null, null);
            stopvalue.Value = cfg.StopValue;
            stopenable.Checked = cfg.StopEnable;
            stopenable_CheckedChanged(null, null);

            profit1valuetype.SelectedValue = cfg.Profit1ValueType;
            profit1valuetype_SelectedIndexChanged(null, null);
            profit1value.Value = cfg.Profit1Value;
            profit1enable.Checked = cfg.Profit1Enable;
            profit1enable_CheckedChanged(null, null);

            profit2value1type.SelectedValue = cfg.Profit2Value1Type;
            profit2value1type_SelectedIndexChanged(null, null);
            profit2value1.Value = cfg.Profit2Value1;
            profit2trailing1.Value = cfg.Profit2Trailing1;

            profit2value2type.SelectedValue = cfg.Profit2Value2Type;
            profit2value2type_SelectedIndexChanged(null, null);
            profit2value2.Value = cfg.Profit2Value2;
            profit2trailing2.Value = cfg.Profit2Trailing2;
            
            profit2enable.Checked = cfg.Profit2Enable;
            profit2enable_CheckedChanged(null, null);


            token.Enabled = false;
            direction.Enabled = false;
            //power.Enabled = false;
            followaccount.Enabled = false;
        }
    }
}
