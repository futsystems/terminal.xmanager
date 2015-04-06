using System;
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
    public partial class fmDomainInfo : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmDomainInfo()
        {
            InitializeComponent();
            this.Load += new EventHandler(fmDomainInfo_Load);
        }

        void fmDomainInfo_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            if (CoreService.SiteInfo.ProductType == QSEnumProductType.CounterSystem)
            {
                lbsubacctname.Text = "子帐户数:";
                lbmainacctname.Text = "母帐户数:";
            }
            if (CoreService.SiteInfo.ProductType == QSEnumProductType.VendorMoniter)
            {
                lbsubacctname.Text = "配资客户数:";
                lbmainacctname.Text = "主帐户数:";
                panelBrokerEngine.Visible = false;//主帐户监控不用查看成交引擎
            }

        }

        public void OnDisposed()
        { 
        
        }

        string ModuleStatus(bool avabile)
        {
            return avabile ? "开启" : "关闭";
        }
        public void SetDomain(DomainImpl domain)
        {
            domainid.Text = domain.ID.ToString();
            name.Text = domain.Name;
            linkman.Text = domain.LinkMan;
            mobile.Text = domain.Mobile;
            qq.Text = domain.QQ;
            email.Text = domain.Email;
            acclimt.Text = domain.AccLimit.ToString();
            //routergrouplimit.Text = domain.RouterGroupLimit.ToString();
            //routeritemlimit.Text = domain.RouterItemLimit.ToString();
            vendorlimit.Text = domain.VendorLimit.ToString();
            router_live.Text = ModuleStatus(domain.Router_Live);
            router_sim.Text = ModuleStatus(domain.Router_Sim);

            //module_agent.Text = ModuleStatus(domain.Module_Agent);
            //module_finservice.Text = ModuleStatus(domain.Module_FinService);
            expiredate.Text = domain.DateExpired.ToString();

            
        }

    }
}
