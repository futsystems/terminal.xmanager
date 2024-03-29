﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

using System.Windows.Forms;
using System.Net;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using ComponentFactory.Krypton.Toolkit;



namespace TradingLib.MoniterControl
{
    public class MoniterHelper
    {
        /// <summary>
        /// 将控件适配到IDataSource用于数据的统一绑定
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDataSource AdapterToIDataSource(object obj)
        {
            if (obj is KryptonComboBox)
                return new KryptonComboBox2IDataSource(obj as KryptonComboBox);
            else if (obj is KryptonListBox)
                return new KryptonListBox2IDataSource(obj as KryptonListBox);
            else if (obj is ListBox)
                return new ListBox2IDataSource(obj as ListBox);
            else if (obj is ComboBox)
                return new ComboBox2IDataSource(obj as ComboBox);
            else if (obj is CheckedListBox)
                return new CheckedListBox2IDataSource(obj as CheckedListBox);
            return new Invalid2IDataSource(); ;
        }

        public static System.Windows.Forms.DialogResult WindowConfirm(string message, string title = "确认操作")
        {
            return ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show(message, title, System.Windows.Forms.MessageBoxButtons.YesNo);
        }

        public static System.Windows.Forms.DialogResult WindowMessage(string message, string title = "提示")
        {
            return ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show(message, title, System.Windows.Forms.MessageBoxButtons.OK);
        }


        /// <summary>
        /// 将数据导出到CSV文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="view"></param>
        public static void ExportToCSV(string name, ComponentFactory.Krypton.Toolkit.KryptonDataGridView view)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.csv)|*.csv";
            saveFileDialog.FileName = name;
            if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                MoniterHelper.WindowMessage("请填写输出文件名");
                return;
            }

            string filename = CreateFile(saveFileDialog.FileName);
            StringBuilder strColumn = new StringBuilder();
            StringBuilder strValue = new StringBuilder();


            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.GetEncoding("gb2312")))
            {
                for (int k = 0; k < view.Columns.Count; k++)
                {
                    if (!view.Columns[k].Visible) continue;
                    //add separator
                    strColumn.Append(view.Columns[k].HeaderText + ',');
                }
                strColumn.Remove(strColumn.Length - 1, 1);
                sw.WriteLine(strColumn);

                for (int i = 0; i < view.Rows.Count; i++)
                {
                    

                    strValue.Remove(0, strValue.Length); //clear the temp row value
                    for (int k = 0; k < view.Columns.Count; k++)
                    {
                        if (!view.Columns[k].Visible) continue;
                        //add separator
                        strValue.Append(view.Rows[i].Cells[k].Value.ToString() + ',');
                    }
                    strValue.Remove(strValue.Length - 1, 1);
                    sw.WriteLine(strValue);
                }
            }
        }

        public static string CreateFile(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = File.Create(filename);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }
            return filename;
        }


       

        /// <summary>
        /// 返回帐户类别
        /// </summary>
        /// <param name="all"></param>
        /// <param name="includeself"></param>
        /// <returns></returns>
        public static ArrayList GetAccountTypeCombList(bool any = false)
        {
            ArrayList list = new ArrayList();
            if (any)
            {
                ValueObject<QSEnumAccountCategory> vo = new ValueObject<QSEnumAccountCategory>();
                vo.Name = UIConstant.COMBOX_ANY_STR;
                vo.Value = (QSEnumAccountCategory)(-1);
                list.Add(vo);
            }

            {
                ValueObject<QSEnumAccountCategory> vo = new ValueObject<QSEnumAccountCategory>();
                vo.Name = Util.GetEnumDescription(QSEnumAccountCategory.SUBACCOUNT);
                vo.Value = QSEnumAccountCategory.SUBACCOUNT;
                list.Add(vo);
            }

            if (CoreService.SiteInfo.Domain.Super || (CoreService.SiteInfo.Domain.Module_Follow))
            {
                ValueObject<QSEnumAccountCategory> vo1 = new ValueObject<QSEnumAccountCategory>();
                vo1.Name = Util.GetEnumDescription(QSEnumAccountCategory.SIGACCOUNT);
                vo1.Value = QSEnumAccountCategory.SIGACCOUNT;
                list.Add(vo1);

                ValueObject<QSEnumAccountCategory> vo2 = new ValueObject<QSEnumAccountCategory>();
                vo2.Name = Util.GetEnumDescription(QSEnumAccountCategory.STRATEGYACCOUNT);
                vo2.Value = QSEnumAccountCategory.STRATEGYACCOUNT;
                list.Add(vo2);
            }

            //根据域实盘和模拟权限来控制显示的帐户类型列表
            //if (CoreService.SiteInfo.Domain.Super || (CoreService.SiteInfo.Domain.Router_Sim))
            //{
            //    ValueObject<QSEnumAccountCategory> vo = new ValueObject<QSEnumAccountCategory>();
            //    vo.Name = Util.GetEnumDescription(QSEnumAccountCategory.SUBACCOUNT);
            //    vo.Value = QSEnumAccountCategory.SUBACCOUNT;
            //    list.Add(vo);
            //}

            //if (CoreService.SiteInfo.Domain.Super || (CoreService.SiteInfo.Domain.Router_Live))
            //{
            //    ValueObject<QSEnumAccountCategory> vo = new ValueObject<QSEnumAccountCategory>();
            //    vo.Name = Util.GetEnumDescription(QSEnumAccountCategory.SIGACCOUNT);
            //    vo.Value = QSEnumAccountCategory.SIGACCOUNT;
            //    list.Add(vo);
            //}
            return list;
        }


        public static ArrayList GetRouterTypeCombList(bool any = false)
        {
            ArrayList list = new ArrayList();
            if (any)
            {
                ValueObject<QSEnumOrderTransferType> vo = new ValueObject<QSEnumOrderTransferType>();
                vo.Name = UIConstant.COMBOX_ANY_STR;
                vo.Value = (QSEnumOrderTransferType)(-1);
                list.Add(vo);
            }
            if (CoreService.SiteInfo.Domain.Super || (CoreService.SiteInfo.Domain.Router_Sim))
            {
                ValueObject<QSEnumOrderTransferType> vo = new ValueObject<QSEnumOrderTransferType>();
                vo.Name = Util.GetEnumDescription(QSEnumOrderTransferType.SIM);
                vo.Value = QSEnumOrderTransferType.SIM;
                list.Add(vo);
            }
            if (CoreService.SiteInfo.Domain.Super || (CoreService.SiteInfo.Domain.Router_Live))
            {
                ValueObject<QSEnumOrderTransferType> vo = new ValueObject<QSEnumOrderTransferType>();
                vo.Name = Util.GetEnumDescription(QSEnumOrderTransferType.LIVE);
                vo.Value = QSEnumOrderTransferType.LIVE;
                list.Add(vo);
            }
            return list;
        }

        public static ArrayList GenExpireMonthWithOutYear()
        {
            ArrayList list = new ArrayList();
            for (int i = 1; i <= 12; i++)
            {
                ValueObject<string> vo = new ValueObject<string>();
                vo.Name = string.Format("{0:00}", i);
                vo.Value = string.Format("{0:00}", i);
                list.Add(vo);
            }
            return list;
        }


        public static ArrayList GetOffsetCBList()
        {
            ArrayList list = new ArrayList();
            ValueObject<QSEnumOffsetFlag> vo0 = new ValueObject<QSEnumOffsetFlag>();
            vo0.Name = Util.GetEnumDescription(QSEnumOffsetFlag.UNKNOWN);
            vo0.Value = QSEnumOffsetFlag.UNKNOWN;
            list.Add(vo0);

            ValueObject<QSEnumOffsetFlag> vo1 = new ValueObject<QSEnumOffsetFlag>();
            vo1.Name = Util.GetEnumDescription(QSEnumOffsetFlag.OPEN);
            vo1.Value = QSEnumOffsetFlag.OPEN;
            list.Add(vo1);

            ValueObject<QSEnumOffsetFlag> vo2 = new ValueObject<QSEnumOffsetFlag>();
            vo2.Name = Util.GetEnumDescription(QSEnumOffsetFlag.CLOSE);
            vo2.Value = QSEnumOffsetFlag.CLOSE;
            list.Add(vo2);

            ValueObject<QSEnumOffsetFlag> vo3 = new ValueObject<QSEnumOffsetFlag>();
            vo3.Name = Util.GetEnumDescription(QSEnumOffsetFlag.CLOSETODAY);
            vo3.Value = QSEnumOffsetFlag.CLOSETODAY;
            list.Add(vo3);
            return list;

        }

        public static ArrayList GetOrderTypeCBList()
        {
            ArrayList list = new ArrayList();
            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "限价";
            vo1.Value = 0;
            list.Add(vo1);

            ValueObject<int> vo2 = new ValueObject<int>();
            vo2.Name = "市价";
            vo2.Value = 1;
            list.Add(vo2);
            return list;
        }

        public static ArrayList GetTradeableCBList(bool any = true)
        {
            ArrayList list = new ArrayList();
            if (any)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = UIConstant.COMBOX_ANY_STR;
                vo.Value = 0;
                list.Add(vo);
            }

            ValueObject<int> vo1 = new ValueObject<int>();
            vo1.Name = "可交易";
            vo1.Value = 1;
            list.Add(vo1);

            ValueObject<int> vo2 = new ValueObject<int>();
            vo2.Name = "不可交易";
            vo2.Value = -1;
            list.Add(vo2);
            return list;
        }

        public static ArrayList GetManagerTypeCBList()
        {
            ArrayList list = new ArrayList();

            ValueObject<QSEnumManagerType> vo2 = new ValueObject<QSEnumManagerType>();
            vo2.Name = Util.GetEnumDescription(QSEnumManagerType.STAFF);
            vo2.Value = QSEnumManagerType.STAFF;
            list.Add(vo2);

            bool agentAvabile=true;

            if (!CoreService.SiteInfo.Domain.Module_Agent) agentAvabile = false;
            if (CoreService.SiteInfo.Manager.IsAgent() && (CoreService.SiteInfo.Manager.AgentLimit == 0)) agentAvabile = false;

            if (agentAvabile)
            {
                ValueObject<QSEnumManagerType> vo1 = new ValueObject<QSEnumManagerType>();
                vo1.Name = Util.GetEnumDescription(QSEnumManagerType.AGENT);
                vo1.Value = QSEnumManagerType.AGENT;
                list.Add(vo1);
            }

            return list;

        }

        public static ArrayList GetEnumValueObjects<T>(bool isany = false)
        {
            ArrayList list = new ArrayList();
            if (isany)
            {
                ValueObject<T> vo = new ValueObject<T>();
                vo.Name = UIConstant.COMBOX_ANY_STR;
                vo.Value = (T)(Enum.GetValues(typeof(T)).GetValue(0));
                list.Add(vo);
            }
            foreach (T c in Enum.GetValues(typeof(T)))
            {

                ValueObject<T> vo = new ValueObject<T>();
                vo.Name = Util.GetEnumDescription(c);
                vo.Value = c;
                list.Add(vo);
            }
            return list;
        }

        /// <summary>
        /// 在只有合约字符串
        /// 无法确保有合约对象时 通过合约解析出品种字符串然后通过品种来格式化价格
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static  string FormatPrice(string symbol, decimal val)
        {
            string sec = SymbolImpl.ParseSecCode(symbol);
            SecurityFamily s = CoreService.BasicInfoTracker.GetSecurity(sec);
            if (s == null) val.ToFormatStr();
            return val.ToFormatStr(s.GetPriceFormat());
        }

        /// <summary>
        /// 获得某个合约的价格格式字符串
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static string GetPriceFormat(string symbol)
        {
            string sec = SymbolImpl.ParseSecCode(symbol);
            SecurityFamily s = CoreService.BasicInfoTracker.GetSecurity(sec);
            if (s == null) return "{0:F2}";
            return s.GetPriceFormat();
        }

        public static int GetDecimalPlace(string symbol)
        {
            string sec = SymbolImpl.ParseSecCode(symbol);
            SecurityFamily s = CoreService.BasicInfoTracker.GetSecurity(sec);
            if (s == null) return 2;
            return s.GetDecimalPlaces();
        }



        static bool IsWindows(PlatformID id)
        {
            if (id == PlatformID.Unix)
                return false;
            return true;
        }

        public static ArrayList GetExchangeList()
        {
            ArrayList list = new ArrayList();
            foreach (var exchange in CoreService.BasicInfoTracker.Exchanges)
            {
                ValueObject<string> vo0 = new ValueObject<string>();
                vo0.Name = exchange.Title;
                vo0.Value =exchange.EXCode;
                list.Add(vo0);
            }
            return list;
        }
        /// <summary>
        /// 获得时区列表
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetTimeZoneList()
        {

            ArrayList list = new ArrayList();
            ValueObject<string> vo0 = new ValueObject<string>();
            vo0.Name = "系统默认时区";
            vo0.Value = "";
            list.Add(vo0);

            ValueObject<string> vo1 = new ValueObject<string>();
            vo1.Name = "中国标准时间";
            vo1.Value = "Asia/Shanghai";
            list.Add(vo1);


            ValueObject<string> vo10 = new ValueObject<string>();
            vo10.Name = "香港标准时间";
            vo10.Value = "Asia/Hong_Kong";
            list.Add(vo10);


            ValueObject<string> vo2 = new ValueObject<string>();
            vo2.Name = "新加坡标准时间";
            vo2.Value = "Asia/Singapore";
            list.Add(vo2);

            ValueObject<string> vo3 = new ValueObject<string>();
            vo3.Name = "美国中部时间(CT)";
            vo3.Value = "US/Central";
            list.Add(vo3);

            ValueObject<string> vo4 = new ValueObject<string>();
            vo4.Name = "美国东部时间(ET)";
            vo4.Value = "US/Eastern";
            list.Add(vo4);
             
            return list;
        }

        public static string GenSymbol(SecurityFamilyImpl sec, int month)
        {
            switch (sec.Type)
            {
                case SecurityType.FUT:
                    return GenFutSymbol(sec, month);
                default:
                    return sec.Code;
            }
        }

        static string GenFutSymbol(SecurityFamilyImpl sec, int month)
        {
            if (sec.Exchange.Country == Country.CN &&　sec.Exchange.EXCode !="HKEX")//中国交易所 非香港交易所 合约按中国格式生成
            {
                
                if (sec.Exchange.EXCode.Equals("CZCE"))
                {
                    return sec.Code + month.ToString().Substring(3);
                }
                else
                {
                    return sec.Code + month.ToString().Substring(2);
                }
            }

            //按合约XXV5 的格式进行合约
            string monthstr = month.ToString().Substring(4);
            string yearstr = month.ToString().Substring(3,1);
            return string.Format("{0}{1}{2}", sec.Code, GetMonthCode(monthstr), yearstr);
        }


        /// <summary>
        /// 生成月连续合约
        /// [SecCode][Month]
        /// </summary>
        /// <param name="seccode"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GenSymbolMonthContinuous(string seccode, string month)
        {
            return string.Format("{0}{1}", seccode, month);
        }

        /// <summary>
        /// 201501 获得01作为Month
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GetMonth(int month)
        {
            return month.ToString().Substring(4);
        }


        static string GetMonthCode(string month)
        {
            if (month == "01")
            {
                return "F";
            }
            else if (month == "02")
            {
                return "G";
            }
            else if (month == "03")
            {
                return "H";
            }
            else if (month == "04")
            {
                return "J";
            }
            else if (month == "05")
            {
                return "K";
            }
            else if (month == "06")
            {
                return "M";
            }
            else if (month == "07")
            {
                return "N";
            }
            else if (month == "08")
            {
                return "Q";
            }
            else if (month == "09")
            {
                return "U";
            }
            else if (month == "10")
            {
                return "V";
            }
            else if (month == "11")
            {
                return "X";
            }
            else
            {
                return "Z";
            }
        }


        #region 获取下拉列表Array
        /// <summary>
        /// 交易所下拉列表Array
        /// </summary>
        /// <param name="isany"></param>
        /// <returns></returns>
        public static ArrayList GetExchangeComboxArray(bool isany = false)
        {
            ArrayList list = new ArrayList();
            if (isany)
            {
                ValueObject<int> vo1 = new ValueObject<int>();
                vo1.Name = UIConstant.COMBOX_ANY_STR;
                vo1.Value = 0;
                list.Add(vo1);
            }
            foreach (ExchangeImpl ex in CoreService.BasicInfoTracker.Exchanges)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = ex.Name;
                vo.Value = ex.ID;
                list.Add(vo);
            }
            return list;
        }

        /// <summary>
        /// 交易时间段下拉列表Array
        /// </summary>
        /// <param name="isany"></param>
        /// <returns></returns>
        public static ArrayList GetMarketTimeComboxArray(bool isany = false)
        {
            ArrayList list = new ArrayList();
            if (isany)
            {
                ValueObject<int> vo1 = new ValueObject<int>();
                vo1.Name = UIConstant.COMBOX_ANY_STR;
                vo1.Value = 0;
                list.Add(vo1);
            }
            foreach (MarketTimeImpl mt in CoreService.BasicInfoTracker.MarketTimes)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = mt.Name;
                vo.Value = mt.ID;
                list.Add(vo);
            }
            return list;
        }

        /// <summary>
        /// 委托规则下拉列表Array
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetOrderRuleClassComboxArray()
        {
            ArrayList list = new ArrayList();
            foreach (RuleClassItem item in CoreService.BasicInfoTracker.OrderRuleClass)
            {
                ValueObject<RuleClassItem> vo = new ValueObject<RuleClassItem>();
                vo.Name = item.Title;
                vo.Value = item;
                list.Add(vo);
            }
            return list;
        }

        /// <summary>
        /// 账户规则下拉列表Array
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetAccountRuleClassComboxArray()
        {
            ArrayList list = new ArrayList();

            foreach (RuleClassItem item in CoreService.BasicInfoTracker.AccountRuleClass)
            {
                ValueObject<RuleClassItem> vo = new ValueObject<RuleClassItem>();
                vo.Name = item.Title;
                vo.Value = item;
                list.Add(vo);
            }
            return list;
        }


        /// <summary>
        /// 获得某个交易所的所有品种
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArrayList GetSecurityComboxArrayViaExchange(int id, SecurityType type = SecurityType.NIL, bool any = false)
        {
            ArrayList list = new ArrayList();
            if (any)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = UIConstant.COMBOX_ANY_STR;
                vo.Value = 0;
                list.Add(vo);
            }
            //未指定品种类型 则返回所有品种
            if (type == SecurityType.NIL)
            {
                if (id == 0)
                {
                    foreach (SecurityFamilyImpl sec in CoreService.BasicInfoTracker.Securities)
                    {
                        ValueObject<int> vo = new ValueObject<int>();
                        vo.Name = sec.Code + "-" + sec.Name;
                        vo.Value = sec.ID;
                        list.Add(vo);
                    }

                }
                else
                {
                    foreach (SecurityFamilyImpl sec in CoreService.BasicInfoTracker.Securities.Where(ex => (ex != null && ((ex.Exchange as ExchangeImpl).ID == id))).ToArray())
                    {
                        ValueObject<int> vo = new ValueObject<int>();
                        vo.Name = sec.Code + "-" + sec.Name;
                        vo.Value = sec.ID;
                        list.Add(vo);
                    }
                }
            }
            else
            {
                if (id == 0)
                {
                    foreach (SecurityFamilyImpl sec in CoreService.BasicInfoTracker.Securities.Where(s => s.Type == type))
                    {
                        ValueObject<int> vo = new ValueObject<int>();
                        vo.Name = sec.Code + "-" + sec.Name;
                        vo.Value = sec.ID;
                        list.Add(vo);
                    }

                }
                else
                {
                    foreach (SecurityFamilyImpl sec in CoreService.BasicInfoTracker.Securities.Where(s => s.Type == type).Where(ex => (ex != null && ((ex.Exchange as ExchangeImpl).ID == id))).ToArray())
                    {
                        ValueObject<int> vo = new ValueObject<int>();
                        vo.Name = sec.Code + "-" + sec.Name;
                        vo.Value = sec.ID;
                        list.Add(vo);
                    }
                }
            }
            return list;
        }

        public static ArrayList GetExpireMonthComboxArray()
        {
            ArrayList list = new ArrayList();
            DateTime lastday = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1);
            for (int i = 0; i < 12; i++)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = lastday.AddMonths(i).ToString("yyyyMM");//201501
                vo.Value = Convert.ToInt32(vo.Name);
                list.Add(vo);
            }
            return list;
        }
        /// <summary>
        /// 返回manger选择项
        /// 用于创建用户
        /// </summary>
        /// <returns></returns>
        //public static ArrayList GetBaseManagerComboxArray(bool all = false, bool includeself = true)
        //{
        //    ArrayList list = new ArrayList();

        //    if (all)
        //    {
        //        list.Add(new ValueObject<int> { Name = MoniterUtil.AnyCBStr, Value = 0 });
        //    }
        //    //从柜员列表中获得管理员或代理域
        //    foreach (ManagerSetting m in CoreService.BasicInfoTracker.Managers.Where(g => (g.Type == QSEnumManagerType.ROOT || g.Type == QSEnumManagerType.AGENT)))
        //    {
        //        if (!includeself && m.mgr_fk == CoreService.SiteInfo.BaseMGRFK)
        //        {
        //            continue;
        //        }
        //        ValueObject<int> vo1 = new ValueObject<int>();
        //        vo1.Name = m.Name + " - " + m.mgr_fk;
        //        vo1.Value = m.mgr_fk;
        //        list.Add(vo1);

        //    }
        //    return list;
        //}
        #endregion

    }
}
