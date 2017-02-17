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

namespace TradingLib.MoniterControl
{
    public partial class ctFollowItemFilter : UserControl
    {
        public event Action<FollowFilterArgs> FilterArgsChanged = delegate { };
        void FireFilterArgsChanged(FollowFilterArgs arg)
        {
            FilterArgsChanged(arg);
        }

        public ctFollowItemFilter()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(eventType).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumPositionEventType>(true));
            profitType.Items.Add(UIConstant.COMBOX_ANY_STR);
            profitType.Items.Add("盈利");
            profitType.Items.Add("亏损");
            profitType.SelectedIndex = 0;

            this.Load += new EventHandler(ctFilter_Load);
        }
        FollowFilterArgs _arg = new FollowFilterArgs();

        void ctFilter_Load(object sender, EventArgs e)
        {
            tbSignal.TextChanged += new EventHandler(tbSignal_TextChanged);
            tbSymbol.TextChanged += new EventHandler(tbSymbol_TextChanged);
            eventType.SelectedValueChanged += new EventHandler(eventType_SelectedValueChanged);
            cbPos.CheckedChanged += new EventHandler(cbPos_CheckedChanged);
            profitType.SelectedValueChanged += new EventHandler(profitType_SelectedValueChanged);
           
        }

        void profitType_SelectedValueChanged(object sender, EventArgs e)
        {
            int val = profitType.SelectedIndex;
            if (val == 0)
            {
                _arg.ProfitEnable = false;
            }
            if (val == 1)
            {
                _arg.ProfitEnable = true;
                _arg.FollowProfit = true;
            }
            if (val == 2)
            {
                _arg.ProfitEnable = true;
                _arg.FollowProfit = false;
            }
            FireFilterArgsChanged(_arg);
        }

        void tbSignal_TextChanged(object sender, EventArgs e)
        {
            _arg.SignalSearch = tbSignal.Text;
            FireFilterArgsChanged(_arg);
        }

        void tbSymbol_TextChanged(object sender, EventArgs e)
        {
            _arg.SymbolSearch = tbSymbol.Text;
            FireFilterArgsChanged(_arg);
        }

        void eventType_SelectedValueChanged(object sender, EventArgs e)
        {
            QSEnumPositionEventType val = (QSEnumPositionEventType)eventType.SelectedValue;
            if (eventType.SelectedIndex == 0)
            {
                _arg.EventTypeEnable = false;
            }
            else
            {
                _arg.EventTypeEnable = true;
                _arg.EventType = val;
            }
            FireFilterArgsChanged(_arg);
        }

        void cbPos_CheckedChanged(object sender, EventArgs e)
        {
            _arg.FollowPos = cbPos.Checked;
            FireFilterArgsChanged(_arg);
        }
    }
}
