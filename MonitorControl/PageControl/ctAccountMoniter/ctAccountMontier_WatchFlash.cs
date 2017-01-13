using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using System.Windows.Forms;

namespace TradingLib.MoniterControl
{
    /// <summary>
    /// 动态更新观察者列表
    /// </summary>
    public partial class ctAccountMontier
    {
        DateTime _gridChangeTime;//滚动时间
        int _freshdeay = 1;//滚动停止多少秒后开始刷新数据
        bool _watchchanged = false;

        void GridChanged()
        {
            _watchchanged = true;
            _gridChangeTime = DateTime.Now;
        }


        /// <summary>
        /// 获得所有可视范围内的帐户
        /// </summary>
        /// <returns></returns>
        List<string> GetVisualAccounts()
        {
            List<string> accountlist = new List<string>();
            if (accountgrid.RowCount == 0) return accountlist;

            int _startrow = accountgrid.FirstDisplayedCell.RowIndex;
            int _rownum = accountgrid.DisplayedRowCount(true);
            
            for(int i=0;i<_rownum;i++)
            {
                accountlist.Add(accountgrid[0, _startrow + i].Value.ToString());
            }
            return accountlist;
        }

        /// <summary>
        /// 设定观察账户列表
        /// </summary>
        void SetWathAccounts()
        {
            if ((!_watchchanged) || (DateTime.Now - _gridChangeTime).TotalSeconds < _freshdeay) return;//如果没有发生变化 并且时间没有超过2秒，则不用设置观察更新
            
            List<string> acclist = GetVisualAccounts();//获得当前列表
            if (acclist.Count > 0)
            {
                CoreService.TLClient.ReqWatchAccount(GetVisualAccounts());
            }
            _watchchanged = false;
        }



        bool flash = false;
        DateTime flashtime = DateTime.Now;
        /// <summary>
        /// 某个交易帐户警告解除
        /// 当过滤帐户时 单元格变化会改变显示颜色，如果单元格没有变化则颜色不会发生变化，需要手工强制修改颜色
        /// </summary>
        /// <param name="account"></param>
        void AccountWarnOff(string account)
        { 
            int _startrow = accountgrid.FirstDisplayedCell.RowIndex;
            int _rownum = accountgrid.DisplayedRowCount(true);
            for (int i = 0; i < _rownum; i++)
            {
                if (accountgrid[ACCOUNT, _startrow + i].Value.ToString() == account)
                { 
                    if (_startrow + i % 2 == 0)
                    {
                        accountgrid[0, _startrow + i].Style.BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        accountgrid[0, _startrow + i].Style.BackColor = System.Drawing.Color.WhiteSmoke;
                    }
                    accountgrid[0, _startrow + i].Style.ForeColor = System.Drawing.Color.Black;
                }
            }
        }



        /// <summary>
        /// 闪烁处于警告状态的帐户
        /// </summary>
        void FlashAccountWarn()
        {
            //每隔2秒闪烁一次
            if (DateTime.Now.Subtract(flashtime).TotalSeconds > 1 && accountgrid.Rows.Count > 0)
            {
                bool anywarn = false;
                flashtime = DateTime.Now;
                //遍历界面中的可视grid
                int _startrow = accountgrid.FirstDisplayedCell.RowIndex;
                int _rownum = accountgrid.DisplayedRowCount(true);
                for (int i = 0; i < _rownum; i++)
                {
                    int rowidx = _startrow + i;
                    //如果该帐户行对应的警告标识处于警告状态
                    bool iswarn = bool.Parse(accountgrid[WARN, rowidx].Value.ToString());
                    //如果处于警告状态则进行闪烁
                    if (iswarn)
                    {
                        anywarn = true;
                        if (!flash)
                        {
                            accountgrid[0, rowidx].Style.BackColor = System.Drawing.Color.Crimson;
                            accountgrid[0, rowidx].Style.ForeColor = System.Drawing.Color.White;
                            flash = true;
                        }
                        else
                        {
                            accountgrid[0, rowidx].Style.BackColor = rowidx % 2 == 0 ? System.Drawing.Color.White : System.Drawing.Color.WhiteSmoke;
                            accountgrid[0, rowidx].Style.ForeColor = System.Drawing.Color.Crimson;
                            flash = false;
                        }
                    }
                }
                if (anywarn & flash)
                {
                    Console.Beep();
                }
            }
        }

        


    }
}
