﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;

namespace TradingLib.MoniterControl
{
    public class QuoteRow
    {
        /// <summary>
        /// QuoteList报价对象
        /// </summary>
        public ViewQuoteList _quotelist;

        /// <summary>
        /// Quote样式
        /// </summary>
        QuoteStyle _defaultQuoteStyle;

        /// <summary>
        /// 报价显示样式
        /// </summary>
        string _pricedispformat = "{0:F1}";

        private int _rowid;
        /// <summary>
        /// 行号
        /// </summary>
        public int RowID { get { return _rowid; } set { _rowid = value; } }


        Symbol _symbol = null;
        /// <summary>
        /// 合约
        /// </summary>
        public Symbol Symbol { get { return _symbol; } }


        EnumQuoteType _quoteType = EnumQuoteType.CNQUOTE;
        /// <summary>
        /// 报价类别
        /// </summary>
        public EnumQuoteType QuoteType { get { return _quoteType; } set { _quoteType = value; } }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotelist">用于传递quotelist控件引用,调用invaildate</param>
        /// <param name="rid">行序号</param>
        /// <param name="symbol">合约</param>
        /// <param name="type">报价类别</param>
        public QuoteRow(ViewQuoteList quotelist, int rid,Symbol symbol, EnumQuoteType type)
        {
            _quotelist = quotelist;
            _symbol = symbol;
            _rowid = rid;

            _defaultQuoteStyle = quotelist.DefaultQuoteStyle;
            _pricedispformat = symbol.SecurityFamily.GetPriceFormat();
            _quoteType = type;

            //初始化行
            InitRow();

        }

        /// <summary>
        /// 初始化QuoteRow
        /// </summary>
        public void InitRow()
        {
            //根据行号得到列底色基本配置
            CellStyle cellstyle = new CellStyle(RowID % 2 == 0 ? _defaultQuoteStyle.QuoteBackColor1 : _defaultQuoteStyle.QuoteBackColor2, Color.DarkRed, _defaultQuoteStyle.QuoteFont, _defaultQuoteStyle.SymbolFont, _defaultQuoteStyle.LineColor);
            //遍历所有的行名 并初始化单元格
            for (int i = 0; i < _quotelist.Columns.Length; i++)
            {
                string columnName = _quotelist.Columns[i];
                QuoteCell cell = new QuoteCell(columnName, cellstyle, 0M, _pricedispformat);
                cell.SendDebutEvent += new DebugDelegate(debug);

                //添加Cell到数据结构
                _columeCellMap.Add(i, cell);
                _columeName2idx.Add(_quotelist.Columns[i], i);

            }

            //设置合约/名称字段
            if (_quotelist.Columns.Contains(QuoteListConst.SYMBOL))
            {
                this[QuoteListConst.SYMBOL].Symbol = _symbol.Symbol;
            }
            if (_quotelist.Columns.Contains(QuoteListConst.SYMBOLNAME))
            {
                this[QuoteListConst.SYMBOLNAME].Symbol = _symbol.GetTitleName(true);
            }
        }

 
        
        
        //{ SYMBOL, LAST, LASTSIZE, ASK, BID, ASKSIZE, BIDSIZE, VOL, CHANGE, OI, OICHANGE, SETTLEMENT, OPEN, HIGH, LOW, LASTSETTLEMENT };

        public void GotTick(Tick k)
        {
            //将数据更新到cell value中去
            if (k.IsTrade())
            {
                //1.更新最新价
                if (k.Trade != this[QuoteListConst.LAST].Value)
                {
                    //选中的行 不执行闪亮操作
                    if (RowID != _quotelist.SelectedQuoteRow)
                    {
                        this[QuoteListConst.LAST].CellStyle.FontColor = k.Trade < k.PreSettlement ? _defaultQuoteStyle.DNColor : _defaultQuoteStyle.UPColor;
                        CellChangeColor(QuoteListConst.LAST, k.Trade > this[QuoteListConst.LAST].Value ? Color.Tomato : Color.SpringGreen);
                    }
                    this[QuoteListConst.LAST].Value = k.Trade;
                }

                //更新涨跌
                decimal baseprice = _quoteType== EnumQuoteType.CNQUOTE?k.PreSettlement:k.PreClose;
                if ((k.Trade - baseprice) != this[QuoteListConst.CHANGE].Value)
                {
                    this[QuoteListConst.CHANGE].Value = k.Trade - baseprice;
                    this[QuoteListConst.CHANGE].CellStyle.FontColor = (k.Trade - k.PreSettlement) < 0 ? _defaultQuoteStyle.DNColor : _defaultQuoteStyle.UPColor;
                }

                this[QuoteListConst.LASTSIZE].Value = k.Size;
            }

            
            //更新当前的Tick数据
            if (k.AskPrice != this[QuoteListConst.ASK].Value)
            {
                this[QuoteListConst.ASK].Value = k.AskPrice;
                this[QuoteListConst.ASK].CellStyle.FontColor = k.AskPrice < k.PreSettlement ? _defaultQuoteStyle.DNColor : _defaultQuoteStyle.UPColor;
            }
            if (k.BidPrice != this[QuoteListConst.BID].Value)
            {
                this[QuoteListConst.BID].Value = k.BidPrice;
                this[QuoteListConst.BID].CellStyle.FontColor = k.BidPrice < k.PreSettlement ? _defaultQuoteStyle.DNColor : _defaultQuoteStyle.UPColor;
            }

            this[QuoteListConst.BIDSIZE].Value = k.BidSize;
            this[QuoteListConst.ASKSIZE].Value = k.AskSize;

            this[QuoteListConst.VOL].Value = k.Vol;

            this[QuoteListConst.OI].Value = _quoteType == EnumQuoteType.CNQUOTE ? k.OpenInterest : k.PreOpenInterest;

            this[QuoteListConst.OICHANGE].Value = k.OpenInterest != 0 ? (k.OpenInterest - k.PreOpenInterest) : 0;

            this[QuoteListConst.SETTLEMENT].Value = _quoteType == EnumQuoteType.CNQUOTE ? k.Settlement :0;
            this[QuoteListConst.LASTSETTLEMENT].Value =  k.PreSettlement;//EnumQuoteType.CNQUOTE ? k.PreSettlement : k.PreClose;


            if (k.Open != this[QuoteListConst.OPEN].Value)
            {
                this[QuoteListConst.OPEN].Value = k.Open;
                this[QuoteListConst.OPEN].CellStyle.FontColor = k.Open < k.PreSettlement ? _defaultQuoteStyle.DNColor : _defaultQuoteStyle.UPColor;
            }
            if (k.Low != this[QuoteListConst.HIGH].Value)
            {
                this[QuoteListConst.HIGH].Value = k.High;
                this[QuoteListConst.HIGH].CellStyle.FontColor = k.High < k.PreSettlement ? _defaultQuoteStyle.DNColor : _defaultQuoteStyle.UPColor;
            }

            if (k.Low != this[QuoteListConst.LOW].Value)
            {
                this[QuoteListConst.LOW].Value = k.Low;
                this[QuoteListConst.LOW].CellStyle.FontColor = k.Low < k.PreSettlement ? _defaultQuoteStyle.DNColor : _defaultQuoteStyle.UPColor;
            }

            //重绘该行
            _quotelist.Invalidate(this.Rect);
        }
        
        //改变某个单元格的背景颜色
        private void CellChangeColor(string col, Color c)
        {
            this[col].CellStyle.BackColor = c;
            _quotelist.BookLocation(_rowid);
        }


        #region 数据结构 用于存放序号与Cell
        //序号对应的单元格
        public Dictionary<int, QuoteCell> _columeCellMap = new Dictionary<int, QuoteCell>();
        //colume名称对应的序号
        public Dictionary<string, int> _columeName2idx = new Dictionary<string, int>();


        int column2Idx(string column)
        {
            int idx = -1;
            if (_columeName2idx.TryGetValue(column, out idx))
                return idx;
            return idx;
        }

        /// <summary>
        /// 通过序号返回对应的Cell
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public QuoteCell this[int index]
        {
            get { return _columeCellMap[index]; }
        }


        /// <summary>
        /// 通过列名返回对应的Cell
        /// </summary>
        /// <param name="columnname"></param>
        /// <returns></returns>
        public QuoteCell this[string columnname]
        {

            get { return _columeCellMap[column2Idx(columnname)]; }
        }

        #endregion


        #region 序号与RectangleMap
        //获得该行中某个单元格的区域
        private Dictionary<int, Rectangle> cellRectsMap = new Dictionary<int, Rectangle>();
        //通过将rect计算后放入映射列表 可以避免每次更新都进行运算,但是当列宽改变的时候我们需要将
        //public void ResetCellRect()
        //{
            
        //}

        /// <summary>
        /// 获得某个Cell的绘图区域
        /// </summary>
        /// <param name="colindex"></param>
        /// <returns></returns>
        Rectangle GetCellRect(int colindex)
        {
            int i = colindex;
            Rectangle cellRect;
            if (cellRectsMap.TryGetValue(i, out cellRect))
                return cellRect;
            //没有缓存单元格序号对应的绘图区域 需要计算该绘图区域
            Point cellLocation = new Point(_quotelist.GetColumnStarX(i), (RowID - _quotelist.GetBeginIndex()) * _defaultQuoteStyle.RowHeight + _defaultQuoteStyle.HeaderHeight);
            cellRect = new Rectangle(cellLocation.X, cellLocation.Y, _quotelist.GetColumnWidth(i), _defaultQuoteStyle.RowHeight);
            cellRectsMap.Add(i, cellRect);
            return cellRect;
        }


        /// <summary>
        /// 重置Row绘图区域
        /// </summary>
        public void ResetRowRect()
        {
            _rowrectsetted = false;
            lock (cellRectsMap)
            {
                cellRectsMap.Clear();
            }

        }
        //返回本row所在区域 每行起点就是从0-整个控件宽度
        private Rectangle _rowrect;
        private bool _rowrectsetted = false;

        /// <summary>
        /// 获得QuoteRow对应的绘图区域
        /// 当重置RowRect后 需要重新计算Quote对应的绘图区域
        /// </summary>
        public Rectangle Rect
        {
            get
            {
                if (_rowrectsetted)
                    return _rowrect;

                Point cellLocation = new Point(0, (RowID - _quotelist.GetBeginIndex()) * _defaultQuoteStyle.RowHeight + _defaultQuoteStyle.HeaderHeight);
                Rectangle cellRect = new Rectangle(cellLocation.X, cellLocation.Y, _quotelist.GetRowWidth(), _defaultQuoteStyle.RowHeight);
                _rowrect = cellRect;
                return cellRect;
            }
        }

        #endregion

        

        //行的绘制函数
        //paint过程调用的函数要尽量减少运算,这样可以降低系统资源的消耗。
        //可以将一些运算通过一次运算下次取值的方式放入映射列表。这样可以有效的降低运算CPU消耗
        public void Paint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //检查需要更新的矩形区域与本单元格的矩形区域是否相交,如果相交则我们进行更新
            if (e.ClipRectangle.IntersectsWith(this.Rect))
            {
                //遍历每一个单元格并绘制
                for (int i = 0; i < _quotelist.Columns.Length; i++)
                {
                    Rectangle cellRect = GetCellRect(i);
                    _columeCellMap[i].Paint(e,cellRect , _defaultQuoteStyle);
                }
            }


        }

        public event DebugDelegate SendDebutEvent;
        void debug(string msg)
        {
            if (SendDebutEvent != null)
                SendDebutEvent(msg);
        }

    }

}
