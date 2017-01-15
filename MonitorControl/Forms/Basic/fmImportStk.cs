using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;

using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;

namespace TradingLib.MoniterControl
{
    public partial class fmImportStk : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        bool _loaded = false;
        public fmImportStk()
        {
            InitializeComponent();
            MoniterHelper.AdapterToIDataSource(cbexchange).BindDataSource(CoreService.BasicInfoTracker.GetExchangeCombList());

            _loaded = true;
            WireEvent();

        }

        void WireEvent()
        {

            cbexchange.SelectedIndexChanged += new EventHandler(cbexchange_SelectedIndexChanged);
            btnSubmit.Click += new EventHandler(btnSubmit_Click);

            this.Load += new EventHandler(fmImportStk_Load);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            int id = (int)cbsecurity.SelectedValue;
            SecurityFamilyImpl sec = CoreService.BasicInfoTracker.GetSecurity(id);//获得当前选中的security
            if (sec == null)
            {
                MoniterHelper.WindowMessage("请选择品种");
                return;
            }

            OpenFileDialog fm = new OpenFileDialog();
            fm.FilterIndex = 1;
            fm.Filter = "文本文件|*.txt|CSV文件|*.csv";
            if (fm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fn = fm.FileName;
                
                //MessageBox.Show(fn);
                Task t = new Task(delegate { ImpoprtSymbol(sec, fn); });
                t.Start();

                //实例化一个文件流--->与写入文件相关联  
                //using (FileStream fs = new FileStream(fn, FileMode.Open))
                //{
                //    实例化一个StreamWriter-->与fs相关联  
                //    using (StreamReader sw = new StreamReader(fs))
                //    {
                //        while (sw.Peek() > 0)
                //        {
                //            string line = sw.ReadLine();
                //            if (string.IsNullOrEmpty(line)) continue;
                //            string[] rec = line.Split(',');
                //            if (rec.Length < 2) continue;

                //            UpdateSymbol(sec, rec[0], rec[1]);
                //            Util.sleep(100);
                //            MessageBox.Show("代码:{0} 名称:{1}".Put(rec[0], rec[1]));
                //        }
                //        sw.Close();
                //    }
                //    fs.Close();
                //}
            }


        }

        void fmImportStk_Load(object sender, EventArgs e)
        {
            cbexchange_SelectedIndexChanged(null, null);
        }

        void cbexchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loaded) return;
            int exid = (int)cbexchange.SelectedValue;
            MoniterHelper.AdapterToIDataSource(cbsecurity).BindDataSource(CoreService.BasicInfoTracker.GetSecurityCombListViaExchange(exid));

            
        }
        void ImportSymbolBackGroudn()
        { 
                
        }


        void ImpoprtSymbol(SecurityFamilyImpl sec ,string fn)
        {
            //实例化一个文件流--->与写入文件相关联  
            using (FileStream fs = new FileStream(fn, FileMode.Open))
            {
                //实例化一个StreamWriter-->与fs相关联  
                using (StreamReader sw = new StreamReader(fs))
                {
                    while (sw.Peek() > 0)
                    {
                        string line = sw.ReadLine();
                        if (string.IsNullOrEmpty(line)) continue;
                        string[] rec = line.Split(',');
                        if (rec.Length < 2) continue;
                        if (CoreService.BasicInfoTracker.GetSymbol(rec[0]) != null) continue;


                        UpdateSymbol(sec, rec[0], rec[1]);
                        //Util.sleep(100);
                        //MessageBox.Show("代码:{0} 名称:{1}".Put(rec[0], rec[1]));
                    }
                    sw.Close();
                }
                fs.Close();
            }
           
        }
        void  UpdateSymbol(SecurityFamilyImpl sec,string symbol, string name)
        {
            SymbolImpl target = new SymbolImpl();

            target.Symbol = symbol;
            target.SymbolType = QSEnumSymbolType.Standard;

            target.EntryCommission = -1;
            target.ExitCommission = -1;
            target.ExitCommissionToday = -1;
            target.Margin = -1;

            target.security_fk = sec.ID;

            target.Strike = 0;
            target.OptionSide = QSEnumOptionSide.NULL;
                        
            target.ExpireDate = 0;
            target.Month = "";

            target.Symbol = symbol;
            target.Name = name;
            target.Tradeable = true;

            CoreService.TLClient.ReqUpdateSymbol(target);
        }
    }
}
