using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.MoniterCore;
using TradingLib.API;
using TradingLib.Common;


namespace TradingLib.MoniterControl
{
    public partial class fmTradingRange : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmTradingRange()
        {
            InitializeComponent();

            MoniterHelper.AdapterToIDataSource(startday).BindDataSource(GetWeekDayArray());
            MoniterHelper.AdapterToIDataSource(endday).BindDataSource(GetWeekDayArray());
            MoniterHelper.AdapterToIDataSource(settleflag).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumRangeSettleFlag>());
            starttime.Value = Util.ToDateTime(Util.ToTLDate(), 0);
            endtime.Value = Util.ToDateTime(Util.ToTLDate(), 0);

            btnOk.Click += new EventHandler(btnOk_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        TradingRange _range = null;
        public void SetTradingRange(TradingRange range)
        {
            _range = range;
            startday.SelectedValue = range.StartDay;
            starttime.Value = Util.ToDateTime(Util.ToTLDate(), range.StartTime);
            endday.SelectedValue = range.EndDay;
            endtime.Value = Util.ToDateTime(Util.ToTLDate(), range.EndTime);
            settleflag.SelectedValue = range.SettleFlag;
            marketclose.Checked = range.MarketClose;

        }

        public TradingRange CurrentRange
        {
            get
            {
                if (_range != null)
                {
                    _range.StartDay = (DayOfWeek)startday.SelectedValue;
                    _range.StartTime = Util.ToTLTime(starttime.Value);
                    _range.EndDay = (DayOfWeek)endday.SelectedValue;
                    _range.EndTime = Util.ToTLTime(endtime.Value);
                    _range.SettleFlag = (QSEnumRangeSettleFlag)settleflag.SelectedValue;
                    _range.MarketClose = marketclose.Checked;
                    return _range;
                }
                else
                {
                    TradingRange range = new TradingRange();
                    range.StartDay = (DayOfWeek)startday.SelectedValue;
                    range.StartTime = Util.ToTLTime(starttime.Value);
                    range.EndDay = (DayOfWeek)endday.SelectedValue;
                    range.EndTime = Util.ToTLTime(endtime.Value);
                    range.SettleFlag = (QSEnumRangeSettleFlag)settleflag.SelectedValue;
                    range.MarketClose = marketclose.Checked;
                    return range;
                }
            }
        }
        ArrayList GetWeekDayArray()
        {
            ArrayList list = new ArrayList();
            ValueObject<DayOfWeek> t1 = new ValueObject<DayOfWeek>();
            t1.Name = "星期一";
            t1.Value = DayOfWeek.Monday;
            list.Add(t1);

            ValueObject<DayOfWeek> t2 = new ValueObject<DayOfWeek>();
            t2.Name = "星期二";
            t2.Value = DayOfWeek.Tuesday;
            list.Add(t2);

            ValueObject<DayOfWeek> t3 = new ValueObject<DayOfWeek>();
            t3.Name = "星期三";
            t3.Value = DayOfWeek.Wednesday;
            list.Add(t3);

            ValueObject<DayOfWeek> t4 = new ValueObject<DayOfWeek>();
            t4.Name = "星期四";
            t4.Value = DayOfWeek.Thursday;
            list.Add(t4);

            ValueObject<DayOfWeek> t5 = new ValueObject<DayOfWeek>();
            t5.Name = "星期五";
            t5.Value = DayOfWeek.Friday;
            list.Add(t5);

            ValueObject<DayOfWeek> t6 = new ValueObject<DayOfWeek>();
            t6.Name = "星期六";
            t6.Value = DayOfWeek.Saturday;
            list.Add(t6);

            ValueObject<DayOfWeek> t7 = new ValueObject<DayOfWeek>();
            t7.Name = "星期日";
            t7.Value = DayOfWeek.Sunday;
            list.Add(t7);

            return list;

        }
    }
}
