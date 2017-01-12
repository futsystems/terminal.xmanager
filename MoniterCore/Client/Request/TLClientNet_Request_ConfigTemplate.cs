//Copyright 2013 by FutSystems,Inc.
//20170112 去除字符串硬编码

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.Protocol;


namespace TradingLib.MoniterCore
{
    public partial class TLClientNet
    {
        /// <summary>
        /// 查询手续费模板
        /// </summary>
        /// <param name="agentfk"></param>
        public void ReqQryCommissionTemplate()
        {
            this.ReqContribRequest(Modules.MGR_EXCH,Method_MGR_EXCH.QRY_COMMISSION_TEMPLATE, "");
        }

        /// <summary>
        /// 更新手续费模板
        /// </summary>
        /// <param name="t"></param>
        public void ReqUpdateCommissionTemplate(CommissionTemplateSetting t)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_COMMISSION_TEMPLATE, t.SerializeObject());
        }

        public void ReqDelCommissionTemplate(CommissionTemplateSetting t)
        {
            this.ReqContribRequest(Modules.MGR_EXCH,Method_MGR_EXCH.DEL_COMMISSION_TEMPLATE,t.ID.ToString());
        }

        /// <summary>
        /// 查询手续费模板项目
        /// </summary>
        public void ReqQryCommissionTemplateItem(int templateid)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_COMMISSION_ITEM, templateid.ToString());
        }

        /// <summary>
        /// 更新手续费项目
        /// </summary>
        /// <param name="t"></param>
        public void ReqUpdateCommissionTemplateItem(MGRCommissionTemplateItemSetting item)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_COMMISSION_ITEM, item.SerializeObject());
        }

        /// <summary>
        /// 查询保证金模板列表
        /// </summary>
        public void ReqQryMarginTemplate()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_TEMPLATE, "");
        }

        /// <summary>
        /// 更新保证金模板
        /// </summary>
        /// <param name="t"></param>
        public void ReqUpdateMarginTemplate(MarginTemplateSetting t)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MARGIN_TEMPLATE, t.SerializeObject());
        }

        public void ReqDelMarginTemplate(MarginTemplateSetting t)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.DEL_MARGIN_TEMPLATE, t.ID.ToString());
        }

        /// <summary>
        /// 查询保证金模板项目
        /// </summary>
        public void ReqQryMarginTemplateItem(int templateid)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_MARGIN_ITEM, templateid.ToString());
        }

        /// <summary>
        /// 更新保证金模板项目
        /// </summary>
        /// <param name="item"></param>
        public void ReqUpdateCommissionTemplateItem(MGRMarginTemplateItemSetting item)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_MARGIN_ITEM, item.SerializeObject());
        }


        /// <summary>
        /// 查询手续费模板
        /// </summary>
        /// <param name="agentfk"></param>
        public void ReqQryExStrategyTemplate()
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_TEMPLATE, "");
        }

        /// <summary>
        /// 更新手续费模板
        /// </summary>
        /// <param name="t"></param>
        public void ReqUpdateExStrategyTemplate(ExStrategyTemplateSetting t)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_EXSTRATEGY_TEMPLATE, t.SerializeObject());
        }

        /// <summary>
        /// 查询交易参数模板项目
        /// </summary>
        public void ReqQryExStrategyTemplateItem(int templateid)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.QRY_EXSTRATEGY_ITEM, templateid.ToString());
        }

        /// <summary>
        /// 更新手续费项目
        /// </summary>
        /// <param name="t"></param>
        public void ReqUpdateExStrategyTemplateItem(ExStrategy item)
        {
            this.ReqContribRequest(Modules.MGR_EXCH, Method_MGR_EXCH.UPDATE_EXSTRATEGY_ITEM, item.SerializeObject());
        }

        public void ReqDelExStrategyTemplate(ExStrategyTemplateSetting t)
        {
            this.ReqContribRequest(Modules.MGR_EXCH,Method_MGR_EXCH.DEL_EXSTRATEGY_TEMPLATE , t.ID.ToString());
        }

    }
}
