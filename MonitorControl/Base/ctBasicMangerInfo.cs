using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;


namespace TradingLib.MoniterControl
{
    public partial class ctBasicMangerInfo : UserControl,IEventBinder
    {
        public ctBasicMangerInfo()
        {
            InitializeComponent();
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            InitManagerInfo();
        }

        public void OnDisposed()
        { 
            
        }

        void InitManagerInfo()
        {
            lbbasemgrfk.Text = CoreService.SiteInfo.BaseMGRFK.ToString();
            lblogin.Text = CoreService.SiteInfo.ContractorInfo.LoginID;
            lbname.Text = CoreService.SiteInfo.ContractorInfo.Name;
            lbmobile.Text = CoreService.SiteInfo.ContractorInfo.Mobile;
            lbqq.Text = CoreService.SiteInfo.ContractorInfo.QQ;
            lbrole.Text = Util.GetEnumDescription(CoreService.SiteInfo.Manager.Type);
        }


    }
}
