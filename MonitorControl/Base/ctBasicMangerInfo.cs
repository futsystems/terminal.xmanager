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
            this.btnChangePass.Click +=new EventHandler(btnChangePass_Click);
        }

        public void OnInit()
        {
            InitAgentList();
        }

        public void OnDisposed()
        { 
            
        }

        void InitAgentList()
        {
            lbbasemgrfk.Text = CoreService.SiteInfo.BaseMGRFK.ToString();
            lblogin.Text = CoreService.SiteInfo.ContractorInfo.LoginID;
            lbname.Text = CoreService.SiteInfo.ContractorInfo.Name;
            lbmobile.Text = CoreService.SiteInfo.ContractorInfo.Mobile;
            lbqq.Text = CoreService.SiteInfo.ContractorInfo.QQ;
            lbrole.Text = Util.GetEnumDescription(CoreService.SiteInfo.Manager.Type);
        }

        /// <summary>
        /// 响应环境初始化完成事件 用于在环境初始化之前创立的空间获得对应的基础数据
        /// </summary>
        public void OnInitFinished()
        {
            InitAgentList();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            fmChangePasswordAgent fm = new fmChangePasswordAgent();
            fm.ShowDialog();
        }
    }
}
