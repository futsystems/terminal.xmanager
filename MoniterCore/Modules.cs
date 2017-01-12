using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradingLib.MoniterCore
{
    /// <summary>
    /// 模块常数列表
    /// </summary>
    public static class Modules
    {

        /// <summary>
        /// 账户管理模块
        /// </summary>
        public const string ACC_MGR = "AccountManager";

        /// <summary>
        /// 管理消息交换模块
        /// </summary>
        public const string MGR_EXCH = "MgrExchServer";


    }


    /// <summary>
    /// 账户管理模块扩展命令
    /// </summary>
    public class Method_ACC_MGR
    {
        /// <summary>
        /// 添加账户
        /// </summary>
        public  const string ADD_ACC = "AddAccountFacde";

        /// <summary>
        /// 删除账户
        /// </summary>
        public const string DEL_ACC = "DelAccount";

        /// <summary>
        /// 查询账户Profile
        /// </summary>
        public const string QRY_ACC_PROFILE = "QryAccountProfile";

        /// <summary>
        /// 更新账户Profile
        /// </summary>
        public const string UPDATE_ACC_PROFILE = "UpdateAccountProfile";

        /// <summary>
        /// 更新账户日内属性
        /// </summary>
        public const string UPDATE_ACC_INTRADAY = "UpdateAccountIntraday";

        /// <summary>
        /// 更新账户路由属性
        /// </summary>
        public const string UPDATE_ROUTE_TYPE = "UpdateRouteType";

        /// <summary>
        /// 更新账户交易状态
        /// </summary>
        public const string UPDATE_ACC_EXECUTE = "UpdateAccountExecute";

        /// <summary>
        /// 更新账户密码
        /// </summary>
        public const string UPDATE_ACC_PASS = "UpdateAccountPass";

        /// <summary>
        /// 更新账户货币
        /// </summary>
        public const string UPDATE_ACC_CURRENCY = "UpdateAccountCurrency";

        /// <summary>
        /// 更新账户手续费模板
        /// </summary>
        public const string UPDATE_TEMPLATE_COMMISSION = "UpdateAccountCommissionTemplate";

        /// <summary>
        /// 更新保证金模板
        /// </summary>
        public const string UPDATE_TEMPLATE_MARGIN = "UpdateAccountMarginTemplate";

        /// <summary>
        /// 更新交易参数模板
        /// </summary>
        public const string UPDATE_TEMPLATE_EXSTRATEGY = "UpdateAccountExStrategyTemplate";


        /// <summary>
        /// 更新账户路由组
        /// </summary>
        public const string UPDATE_ACC_ROUTEGROUP = "UpdateAccountRouterGroup";

        /// <summary>
        /// 执行出入金操作
        /// </summary>
        public const string CASH_ACC = "AccountCashOperation";

        /// <summary>
        /// 查询账户财务信息
        /// </summary>
        public const string QRY_ACC_FININFO = "QryAccountFinInfo";

        /// <summary>
        /// 账户财务信息更新
        /// </summary>
        public const string NOTIFY_ACC_FININFO = "NotifyAccountFinInfo";


        /// <summary>
        /// 查询交易账户登入信息
        /// </summary>
        public const string QRY_LOGIN_INFO = "QryAccountLoginInfo";
    }


    public class Method_MGR_EXCH
    {
        /// <summary>
        /// 查询手续费模板
        /// </summary>
        public const string QRY_COMMISSION_TEMPLATE = "QryCommissionTemplate";

        /// <summary>
        /// 更新手续费模板
        /// </summary>
        public const string UPDATE_COMMISSION_TEMPLATE = "UpdateCommissionTemplate";

        /// <summary>
        /// 手续费模板通知
        /// </summary>
        public const string NOTIFY_COMMISSION_TEMPLATE = "NotifyCommissionTemplate";

        /// <summary>
        /// 手续费模板删除通知
        /// </summary>
        public const string NOTIFY_COMMISSION_TEMPLATE_DELETE = "NotifyDeleteCommissionTemplate";

        /// <summary>
        /// 删除手续费模板
        /// </summary>
        public const string DEL_COMMISSION_TEMPLATE = "DeleteCommissionTemplate";

        /// <summary>
        /// 查询手续费模板项
        /// </summary>
        public const string QRY_COMMISSION_ITEM = "QryCommissionTemplateItem";

        /// <summary>
        /// 更新手续费模板项
        /// </summary>
        public const string UPDATE_COMMISSION_ITEM = "UpdateCommissionTemplateItem";

        /// <summary>
        /// 手续费模板项通知
        /// </summary>
        public const string NOTIFY_COMMISSION_ITEM = "NotifyCommissionTemplateItem";

        /// <summary>
        /// 查询保证金模板
        /// </summary>
        public const string QRY_MARGIN_TEMPLATE = "QryMarginTemplate";

        /// <summary>
        /// 更新保证金模板
        /// </summary>
        public const string UPDATE_MARGIN_TEMPLATE = "UpdateMarginTemplate";

        /// <summary>
        /// 保证金模板通知
        /// </summary>
        public const string NOTIFY_MARGIN_TEMPLATE = "NotifyMarginTemplate";

        /// <summary>
        /// 保证金模板删除通知
        /// </summary>
        public const string NOTIFY_MARGIN_TEMPLATE_DELETE = "NotifyDeleteMarginTemplate";

        /// <summary>
        /// 删除保证金模板
        /// </summary>
        public const string DEL_MARGIN_TEMPLATE = "DeleteMarginTemplate";

        /// <summary>
        /// 查询保证金模板项
        /// </summary>
        public const string QRY_MARGIN_ITEM = "QryMarginTemplateItem";

        /// <summary>
        /// 更新保证金模板项
        /// </summary>
        public const string UPDATE_MARGIN_ITEM = "UpdateMarginTemplateItem";

        /// <summary>
        /// 保证金模板项更新
        /// </summary>
        public const string NOTIFY_MARGIN_ITEM = "NotifyMarginTemplateItem";

        /// <summary>
        /// 查询策略模板
        /// </summary>
        public const string QRY_EXSTRATEGY_TEMPLATE = "QryExStrategyTemplate";

        /// <summary>
        /// 更新策略模板
        /// </summary>
        public const string UPDATE_EXSTRATEGY_TEMPLATE = "UpdateExStrategyTemplate";

        /// <summary>
        /// 策略模板通知
        /// </summary>
        public const string NOTIFY_EXSTRATEGY_TEMPLATE = "NotifyExStrategyTemplate";

        /// <summary>
        /// 策略模板删除通知
        /// </summary>
        public const string NOTIFY_EXSTRATEGY_TEMPLATE_DELETE = "NotifyDeleteExStrategyTemplate";

        /// <summary>
        /// 删除策略模板
        /// </summary>
        public const string DEL_EXSTRATEGY_TEMPLATE = "DeleteExStrategyTemplate";

        /// <summary>
        /// 查询策略模板项
        /// </summary>
        public const string QRY_EXSTRATEGY_ITEM = "QryExStrategyTemplateItem";

        /// <summary>
        /// 更新策略模板项
        /// </summary>
        public const string UPDATE_EXSTRATEGY_ITEM = "UpdateExStrategyTemplateItem";

        /// <summary>
        /// 策略项通知
        /// </summary>
        public const string NOTIFY_EXSTRATEGY_ITEM = "NotifyExStrategyTemplateItem";
    }
}
