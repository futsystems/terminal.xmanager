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
using TradingLib.Mixins.JsonObject;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public partial class fmPermissionTemplateEdit : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public fmPermissionTemplateEdit()
        {
            InitializeComponent();
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (MoniterHelper.WindowConfirm("确认添加模板?") == System.Windows.Forms.DialogResult.Yes)
            {
                Permission access = new Permission();
                access.id = 0;
                access.name = name.Text;
                access.desp = desp.Text;
                CoreService.TLClient.ReqUpdatePermissionTemplate(access.ToJson());
                this.Close();
            }

        }
    }
}
