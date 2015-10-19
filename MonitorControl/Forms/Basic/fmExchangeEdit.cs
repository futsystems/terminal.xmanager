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
    public partial class fmExchangeEdit : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmExchangeEdit()
        {
            InitializeComponent();
            this.Text="添加交易所";
            MoniterHelper.AdapterToIDataSource(country).BindDataSource(MoniterHelper.GetEnumValueObjects<Country>());
            MoniterHelper.AdapterToIDataSource(timezone).BindDataSource(MoniterHelper.GetTimeZoneList());

            btnSubmit.Click += new EventHandler(btnSubmit_Click);
            CoreService.EventCore.RegIEventHandler(this);

        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_exchange != null)
            {
                if (MoniterHelper.WindowConfirm("确认更新交易所?") == System.Windows.Forms.DialogResult.Yes)
                {
                    _exchange.Name = this.name.Text;
                    _exchange.Title = this.title.Text;
                    _exchange.Country = (Country)this.country.SelectedValue;
                    _exchange.Calendar = this.calendar.SelectedValue.ToString();
                    _exchange.TimeZoneID = this.timezone.SelectedValue.ToString();
                    _exchange.CloseTime = Util.ToTLTime(this.closetime.Value);
                    CoreService.TLClient.ReqUpdateExchange(_exchange);
                    this.Close();
                }
            }
            else
            {
                if (MoniterHelper.WindowConfirm("确认添加交易所?") == System.Windows.Forms.DialogResult.Yes)
                {
                    Exchange ex = new Exchange();
                    ex.Name = this.name.Text;
                    ex.Title = this.title.Text;
                    ex.Country = (Country)this.country.SelectedValue;
                    ex.EXCode = this.excode.Text;
                    ex.Calendar = this.calendar.SelectedValue.ToString();
                    ex.TimeZoneID = this.timezone.SelectedValue.ToString();
                    ex.CloseTime = Util.ToTLTime(this.closetime.Value);

                    CoreService.TLClient.ReqUpdateExchange(ex);
                    this.Close();
                }
            }
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryCalendarList", OnQryCalendarItems);

            CoreService.TLClient.ReqContribRequest("MgrExchServer", "QryCalendarList", "");
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryCalendarList", OnQryCalendarItems);
        }

        List<CalendarItem> calenarlist = new List<CalendarItem>();
        void OnQryCalendarItems(string json, bool islast)
        {
            CalendarItem item = MoniterHelper.ParseJsonResponse<CalendarItem>(json);
            if (item != null)
            {
                calenarlist.Add(item);
            }
            if (islast)
            {
                UpdateCalendarList(calenarlist);
                if (_exchange != null)
                {
                    this.calendar.SelectedValue = _exchange.Calendar;
                }
            }
        }


        ArrayList GetCalendarCBList(List<CalendarItem> items)
        {
            ArrayList list = new ArrayList();
            ValueObject<string> v = new ValueObject<string>();
            v.Name = "默认";
            v.Value = "";
            list.Add(v);
            foreach (var item in items)
            {
                ValueObject<string> vo = new ValueObject<string>();
                vo.Name = item.Name;
                vo.Value = item.Code;
                list.Add(vo);
            }
            return list;
        }
        void UpdateCalendarList(List<CalendarItem> items)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<CalendarItem>>(UpdateCalendarList), new object[] { items });
            }
            else
            {
                MoniterHelper.AdapterToIDataSource(calendar).BindDataSource(GetCalendarCBList(items));
            }
        }


        Exchange _exchange;
        public void SetExchange(Exchange ex)
        {
            _exchange = ex;
            this.Text = "编辑交易所:"+ex.EXCode;
            this.excode.Text =  _exchange.EXCode;
            this.excode.Enabled = false;
            this.name.Text = _exchange.Name;
            this.title.Text = _exchange.Title;
            this.country.SelectedValue = _exchange.Country;
            this.timezone.SelectedValue = _exchange.TimeZoneID;
            this.closetime.Value = Util.ToDateTime(Util.ToTLDate(), _exchange.CloseTime);
        }


    }
}
