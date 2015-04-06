using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;




namespace TradingLib.MoniterCore
{
    public static class IBasicInfoUtils
    {
        public static string GetExchangeName(this BasicInfoTracker info,string exchange)
        {
            string title = exchange;
            Exchange ex = info.Exchanges.FirstOrDefault(e => e.EXCode.Equals(exchange));
            if (ex != null)
            {
                title = ex.Title;
            }
            LogService.Debug("exchange:" + exchange + " title:" + title);
            return title;
        }

        public static IEnumerable<SymbolImpl> GetSymbolTradable(this BasicInfoTracker info)
        {
            return info.Symbols.Where(s => s.IsTradeable);
        }


        public static ArrayList GetOrderRuleClassListItems(this BasicInfoTracker info)
        {
            ArrayList list = new ArrayList();
            foreach (RuleClassItem item in info.OrderRuleClass)
            {
                ValueObject<RuleClassItem> vo = new ValueObject<RuleClassItem>();
                vo.Name = item.Title;
                vo.Value = item;
                list.Add(vo);
            }
            return list;
        }

        public static ArrayList GetAccountRuleClassListItems(this BasicInfoTracker info)
        {
            ArrayList list = new ArrayList();

            foreach (RuleClassItem item in info.AccountRuleClass)
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
        public static ArrayList GetSecurityCombListViaExchange(this BasicInfoTracker info, int id,SecurityType type = SecurityType.NIL,bool any=false)
        {
            ArrayList list = new ArrayList();
            if (any)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = MoniterUtil.AnyCBStr;
                vo.Value = 0;
                list.Add(vo);
            }
            //未指定品种类型 则返回所有品种
            if (type == SecurityType.NIL)
            {
                if (id == 0)
                {
                    foreach (SecurityFamilyImpl sec in info.Securities)
                    {
                        ValueObject<int> vo = new ValueObject<int>();
                        vo.Name = sec.Code + "-" + sec.Name;
                        vo.Value = sec.ID;
                        list.Add(vo);
                    }

                }
                else
                {
                    foreach (SecurityFamilyImpl sec in info.Securities.Where(ex => (ex != null && ((ex.Exchange as Exchange).ID == id))).ToArray())
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
                    foreach (SecurityFamilyImpl sec in info.Securities.Where(s=>s.Type == type))
                    {
                        ValueObject<int> vo = new ValueObject<int>();
                        vo.Name = sec.Code + "-" + sec.Name;
                        vo.Value = sec.ID;
                        list.Add(vo);
                    }

                }
                else
                {
                    foreach (SecurityFamilyImpl sec in info.Securities.Where(s => s.Type == type).Where(ex => (ex != null && ((ex.Exchange as Exchange).ID == id))).ToArray())
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

        public static ArrayList GetExchangeCombList(this BasicInfoTracker info, bool isany = false)
        {
            ArrayList list = new ArrayList();
            if (isany)
            {
                ValueObject<int> vo1 = new ValueObject<int>();
                vo1.Name = MoniterUtil.AnyCBStr;
                vo1.Value = 0;
                list.Add(vo1);
            }
            foreach (Exchange ex in info.Exchanges)
            {
                if (ex.EXCode.Equals("INNOVEX"))
                {
                    //if (!Globals.UIAccess.sectype_lotto)
                        continue;
                }
                if (ex.EXCode.Equals("SSE"))
                {
                    //if (!Globals.UIAccess.sectype_stock)
                        continue;
                }
                if (ex.EXCode.Equals("SZSE"))
                {
                    //if (!Globals.UIAccess.sectype_stock)
                        continue;
                }
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = ex.Name;
                vo.Value = ex.ID;
                list.Add(vo);
            }
            return list;
        }

        public static ArrayList GetMarketTimeCombList(this BasicInfoTracker info, bool isany = false)
        {
            ArrayList list = new ArrayList();
            if (isany)
            {
                ValueObject<int> vo1 = new ValueObject<int>();
                vo1.Name =MoniterUtil.AnyCBStr;
                vo1.Value = 0;
                list.Add(vo1);
            }
            foreach (MarketTime mt in info.MarketTimes)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = mt.Name;
                vo.Value = mt.ID;
                list.Add(vo);
            }
            return list;
        }

        public static ArrayList GetSecurityCombList(this BasicInfoTracker info, bool isany = false)
        {
            ArrayList list = new ArrayList();
            if (isany)
            {
                ValueObject<int> vo1 = new ValueObject<int>();
                vo1.Name = MoniterUtil.AnyCBStr;
                vo1.Value = 0;
                list.Add(vo1);
            }
            foreach (Exchange ex in info.Exchanges)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = ex.Name;
                vo.Value = ex.ID;
                list.Add(vo);
            }
            return list;
        }

        public static ArrayList GetSecTyeCombList(this BasicInfoTracker info, bool isany = false)
        {
            ArrayList list = new ArrayList();
            if (isany)
            {
                ValueObject<SecurityType> vo = new ValueObject<SecurityType>();
                vo.Name = MoniterUtil.AnyCBStr;
                vo.Value = (SecurityType)(Enum.GetValues(typeof(SecurityType)).GetValue(0));
                list.Add(vo);
            }
            //if (Globals.UIAccess.sectype_fut)
            {
                ValueObject<SecurityType> vo = new ValueObject<SecurityType>();
                vo.Name = Util.GetEnumDescription(SecurityType.FUT);
                vo.Value = SecurityType.FUT;
                list.Add(vo);
            }
            //if (Globals.UIAccess.sectype_stock)
            //{
            //    ValueObject<SecurityType> vo = new ValueObject<SecurityType>();
            //    vo.Name = Util.GetEnumDescription(SecurityType.STK);
            //    vo.Value = SecurityType.STK;
            //    list.Add(vo);
            //}
            return list;
        }


        public static ArrayList GetExpireMonth(this BasicInfoTracker info)
        {
            ArrayList list = new ArrayList();
            DateTime lastday = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1);
            for (int i = 0; i < 12; i++)
            {
                ValueObject<int> vo = new ValueObject<int>();
                vo.Name = lastday.AddMonths(i).ToString("yyyyMM");
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
        public static ArrayList GetBaseManagerCombList(this BasicInfoTracker info, bool all = false, bool includeself = true)
        {
            ArrayList list = new ArrayList();

            if (all)
            {
                list.Add(new ValueObject<int> { Name = MoniterUtil.AnyCBStr, Value = 0 });
            }
            //从柜员列表中获得管理员或代理域
            foreach (ManagerSetting m in info.Managers.Where(g => (g.Type == QSEnumManagerType.ROOT || g.Type == QSEnumManagerType.AGENT)))
            {
                if (!includeself && m.mgr_fk == CoreService.SiteInfo.BaseMGRFK)
                {
                    continue;
                }
                //Globals.Debug("get agentlist:" + includeself.ToString() + " mgrfk:" + m.mgr_fk.ToString() + " basemgrfk:" + Globals.BaseMGRFK.ToString());
                ValueObject<int> vo1 = new ValueObject<int>();
                vo1.Name = m.Name + " - " + m.mgr_fk;
                vo1.Value = m.mgr_fk;
                list.Add(vo1);

            }
            return list;
        }
    }
}
