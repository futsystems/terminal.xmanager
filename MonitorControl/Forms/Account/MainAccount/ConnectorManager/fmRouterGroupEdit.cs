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

        RouterGroupSetting _rg = null;
        public void SetRouterGroup(RouterGroupSetting rg)
        {
            _rg = rg;
            this.Text = string.Format("编辑主帐户组:{0}", _rg.Name);
            rgname.Text = _rg.Name;
            rgdescrption.Text = _rg.Description;
            cbrgstrategytype.SelectedValue = _rg.Strategy;

        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rgname.Text))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("请填写主帐户组名称");
                return;
            }
            if (!InputReg.RouterGroupName.IsMatch(rgname.Text))
            {
                ComponentFactory.Krypton.Toolkit.KryptonMessageBox.Show("主帐户组名称只能包含数字,字母和-");
                return;
            }

            if (_rg == null)
            {
                RouterGroupSetting rg = new RouterGroupSetting();
                rg.Description = rgdescrption.Text;
                rg.Name = rgname.Text;
                rg.Strategy = (QSEnumRouterStrategy)cbrgstrategytype.SelectedValue;
                
                if (MoniterHelper.WindowConfirm("确认添加主帐户组?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateRouterGroup(rg);
                    this.Close();
                }
            }
            else
            {
                _rg.Description = rgdescrption.Text;
                _rg.Name = rgname.Text;
                _rg.Strategy = (QSEnumRouterStrategy)cbrgstrategytype.SelectedValue;
                if (MoniterHelper.WindowConfirm("确认更新主帐户组?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CoreService.TLClient.ReqUpdateRouterGroup(_rg);
                    this.Close();
                }
                
            }

        }



    }
}
