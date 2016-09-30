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

namespace TradingLib.TinyMGRControl
{

    public partial class fmTemplateEdit : ComponentFactory.Krypton.Toolkit.KryptonForm
    {

        public fmTemplateEdit()
        {

            InitializeComponent();
            this.Load += new EventHandler(fmCommissionTemplateEdit_Load);
        }

        void fmCommissionTemplateEdit_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        CommissionTemplateSetting _commissionTemplate = null;
       

        public void SetTemplate(object t)
        {

            if(t is CommissionTemplateSetting)
            {
                _commissionTemplate = t as CommissionTemplateSetting;
                name.Text = _commissionTemplate.Name;
                desp.Text = _commissionTemplate.Description;
                stkcommisionrate.Value = _commissionTemplate.STKCommissioinRate;
                stktransferfee.Value = _commissionTemplate.STKTransferFee;
                stkstamprate.Value = _commissionTemplate.STKStampTaxRate;

                //id.Text = _commissionTemplate.ID.ToString();
                this.Text = "编辑手续费模板";
            }
            else
            {
                MoniterHelper.WindowMessage("请设定要编辑的手续费模板");
            }
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {

            if (_commissionTemplate == null)
            {
                CommissionTemplateSetting target = new CommissionTemplateSetting();
                target.Name = name.Text;
                target.Description = desp.Text;
                target.STKCommissioinRate = stkcommisionrate.Value;
                target.STKTransferFee = stktransferfee.Value;
                target.STKStampTaxRate = stkstamprate.Value;

                CoreService.TLClient.ReqUpdateCommissionTemplate(target);
            }
            else
            {
                _commissionTemplate.Name = name.Text;
                _commissionTemplate.Description = desp.Text;
                _commissionTemplate.STKCommissioinRate = stkcommisionrate.Value;
                _commissionTemplate.STKTransferFee = stktransferfee.Value;
                _commissionTemplate.STKStampTaxRate = stkstamprate.Value;

                CoreService.TLClient.ReqUpdateCommissionTemplate(_commissionTemplate);
            }
           

            this.Close();
        }
    }
}
