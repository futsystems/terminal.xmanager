using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public static class BasicInfoTrackerUtil
    {

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
            //从柜员列表中获得超级管理员域或代理域
            foreach (ManagerSetting m in info.Managers.Where(g => (g.Type == QSEnumManagerType.ROOT || g.Type == QSEnumManagerType.AGENT)))
            {
                if (!includeself && m.mgr_fk == CoreService.SiteInfo.Manager.mgr_fk)
                {
                    continue;
                }
                ValueObject<int> vo1 = new ValueObject<int>();
                vo1.Name = string.Format("{0:d2}-{1}", m.ID, m.Login);
                vo1.Value = m.mgr_fk;
                list.Add(vo1);

            }
            return list;
        }


        /// <summary>
        /// 获得某个账户将对应品种货币转换成账户货币的汇率系数
        /// </summary>
        /// <param name="account"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static decimal GetExchangeRate(this AccountLite account, CurrencyType srcCurrency)
        {
            //品种货币与帐户货币一直则返回1
            if (srcCurrency == account.Currency) return 1;

            //帐户货币为主货币
            if (account.Currency == CurrencyType.RMB)
            {
                //获得品种货币对应的汇率 返回中间汇率
                ExchangeRate secRate = CoreService.BasicInfoTracker.GetExchangeRate(srcCurrency);// BasicTracker.ExchangeRateTracker[TLCtxHelper.ModuleSettleCentre.Tradingday, sec.Currency];
                if (secRate == null) return 1;//没有找到品种汇率 则默认返回1
                return secRate.IntermediateRate;
            }
            else
            {
                ExchangeRate secRate = CoreService.BasicInfoTracker.GetExchangeRate(srcCurrency);
                ExchangeRate accRate = CoreService.BasicInfoTracker.GetExchangeRate(account.Currency);
                if (secRate == null || accRate == null) return 1;
                //将品种货币换算成系统基础货币然后再换算成帐户货币
                return secRate.IntermediateRate / accRate.IntermediateRate;
            }
        
        }
    }
}
