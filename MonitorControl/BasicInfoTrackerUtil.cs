﻿using System;
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
        public static string GetExchangeName(this BasicInfoTracker info, string exchange)
        {
            string title = exchange;
            ExchangeImpl ex = info.Exchanges.FirstOrDefault(e => e.EXCode.Equals(exchange));
            if (ex != null)
            {
                title = ex.Title;
            }
            return title;
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
                list.Add(new ValueObject<int> { Name = UIConstant.COMBOX_ANY_STR, Value = 0 });
            }
            //从柜员列表中获得超级管理员域或代理域
            foreach (ManagerSetting m in info.Managers.Where(g => (g.Type == QSEnumManagerType.ROOT || g.Type == QSEnumManagerType.AGENT)))
            {
                if (!includeself && m.mgr_fk == CoreService.SiteInfo.Manager.mgr_fk)
                {
                    continue;
                }
                if (m.Login == "adminx")
                    continue;
                ValueObject<int> vo1 = new ValueObject<int>();
                vo1.Name = string.Format("{0:d2}-{1}", m.ID, m.Login);
                vo1.Value = m.mgr_fk;
                list.Add(vo1);

            }
            return list;
        }


       
    }
}
