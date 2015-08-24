using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class fmRouterGroupEdit : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmRouterGroupEdit()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(cbrgstrategytype).BindDataSource(MoniterHelper.GetEnumValueObjects<QSEnumRouterStrategy>());
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            RouterGroupSetting rg = new RouterGroupSetting();
            rg.Description = rgdescrption.Text;
            rg.Name = rgname.Text;
            rg.Strategy = (QSEnumRouterStrategy)cbrgstrategytype.SelectedValue;
            if (string.IsNullOrEmpty(rg.Name))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请填写主帐户组名称");
                return;
            }
            if (!InputReg.RouterGroupName.IsMatch(rg.Name))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("主帐户组名称只能包含数字,字母和-");
                return;
            }
            if (MoniterHelper.WindowConfirm("确认添加主帐户组?") == System.Windows.Forms.DialogResult.Yes)
            {
                CoreService.TLClient.ReqUpdateRouterGroup(rg);
                this.Close();
            }

        }



    }
}
