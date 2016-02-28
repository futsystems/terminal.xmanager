﻿using System;
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
    public partial class fmInsertTrade : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmInsertTrade()
        {
            InitializeComponent();
            cbside.Items.Add("买入");
            cbside.Items.Add("卖出");
            cbside.SelectedIndex = 0;

            cboffsetflag.Items.Add("开仓");;
            cboffsetflag.SelectedIndex = 0;

            timestr.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        public void SetAccount(string acc)
        {
            account.Text = acc;
        }
        public void SetSymbol(Symbol sym)
        {
            if (sym != null)
            {
                symbol.Text = sym.Symbol;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime t = DateTime.ParseExact(timestr.Text, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                string sym = symbol.Text;
                SymbolImpl symimpl = CoreService.BasicInfoTracker.GetSymbol(sym);
                if (symimpl == null || !symimpl.IsTradeable)
                {

                    MoniterHelper.WindowMessage("合约不存在或不可交易");
                    return;
                }

                decimal xprice = price.Value;
                if (xprice == 0)
                {
                    MoniterHelper.WindowMessage("设定成交价格");
                    return;
                }

                int xsize = (int)size.Value;
                if (xsize == 0)
                {
                    MoniterHelper.WindowMessage("手数需大于0");
                    return;
                }

                bool side = cbside.SelectedIndex == 0 ? true : false;
                QSEnumOffsetFlag flag = cboffsetflag.SelectedIndex == 0 ? QSEnumOffsetFlag.OPEN : QSEnumOffsetFlag.CLOSE;
                Trade f = new TradeImpl(sym, xprice, xsize * (side ? 1 : -1));
                f.xDate = Util.ToTLDate();
                f.xTime = Util.ToTLTime(t);
                f.OffsetFlag = flag;

                f.Account = account.Text;
                CoreService.TLClient.ReqInsertTrade(f);
                this.Close();

            }
            catch (Exception ex)
            {
                MoniterHelper.WindowMessage("请输入正确的参数!");
                return;
            }

        }
    }
}
