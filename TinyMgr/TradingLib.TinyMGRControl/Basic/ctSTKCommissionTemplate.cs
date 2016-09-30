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

namespace TradingLib.TinyMGRControl
{
    public partial class ctSTKCommissionTemplate : UserControl,IEventBinder
    {
        public ctSTKCommissionTemplate()
        {
            InitializeComponent();

            SetPreferences();
            InitTable();
            BindToTable();

            btnAdd.Click += new EventHandler(btnAdd_Click);
            commissionGrid.DoubleClick += new EventHandler(commissionGrid_DoubleClick);
            this.Load += new EventHandler(ctSTKCommissionTemplate_Load);
        }


        //得到当前选择的行号
        private int CurrentTemplateID
        {
            get
            {
                int row = commissionGrid.SelectedRows.Count > 0 ? commissionGrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return int.Parse(commissionGrid[0, row].Value.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        //通过行号得该行的Security
        CommissionTemplateSetting GetVisibleCommissionItem(int id)
        {
            CommissionTemplateSetting item = null;
            if (templatemap.TryGetValue(id, out item))
            {
                return item;
            }
            else
            {
                return null;
            }
        }


        void commissionGrid_DoubleClick(object sender, EventArgs e)
        {
            CommissionTemplateSetting template = GetVisibleCommissionItem(CurrentTemplateID);
            if (template == null)
            {
                MoniterHelper.WindowMessage("请选择需要编辑的手续费模板项目");
                return;
            }

            fmTemplateEdit fm = new fmTemplateEdit();
            fm.SetTemplate(template);
            fm.ShowDialog();
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            fmTemplateEdit fm = new fmTemplateEdit();
            fm.ShowDialog();
            fm.Close();
        }

        void ctSTKCommissionTemplate_Load(object sender, EventArgs e)
        {
            CoreService.EventCore.RegIEventHandler(this);
        }

        public void OnInit()
        {
            CoreService.EventContrib.RegisterCallback("MgrExchServer", "QryCommissionTemplate", this.OnQryCommissionTemplate);
            CoreService.EventContrib.RegisterNotifyCallback("MgrExchServer", "NotifyCommissionTemplate", this.OnNotifyCommissionTemplate);

            CoreService.TLClient.ReqQryCommissionTemplate();
        }

        public void OnDisposed()
        {
            CoreService.EventContrib.UnRegisterCallback("MgrExchServer", "QryCommissionTemplate", this.OnQryCommissionTemplate);
        }

        
        void OnQryCommissionTemplate(string json, bool islast)
        {
            CommissionTemplateSetting[] list = MoniterHelper.ParseJsonResponse<CommissionTemplateSetting[]>(json);
            if (list != null)
            {
                foreach (CommissionTemplateSetting t in list)
                {
                    InvokeGotCommissiontTemplateItem(t);
                }
            }
        }
        void OnNotifyCommissionTemplate(string json)
        {
            CommissionTemplateSetting obj = MoniterHelper.ParseJsonResponse<CommissionTemplateSetting>(json);
            if (obj != null)
            {
                InvokeGotCommissiontTemplateItem(obj);
            }
        }


        Dictionary<int, CommissionTemplateSetting> templatemap = new Dictionary<int, CommissionTemplateSetting>();
        Dictionary<int, int> templateIdxmap = new Dictionary<int, int>();

        int CommissionID2Idx(int tempalteid)
        {
            int idx = -1;
            if (templateIdxmap.TryGetValue(tempalteid, out idx))
            {
                return idx;
            }
            return -1;
        }

        void InvokeGotCommissiontTemplateItem(CommissionTemplateSetting commission)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<CommissionTemplateSetting>(InvokeGotCommissiontTemplateItem), new object[] { commission });
            }
            else
            {
                int r = CommissionID2Idx(commission.ID);
                if (r < 0)
                {
                    tb.Rows.Add(commission.ID);
                    r = tb.Rows.Count - 1;
                    tb.Rows[r][NAME] = commission.Name;
                    tb.Rows[r][STKCOMMISSONRATE] = commission.STKCommissioinRate;
                    tb.Rows[r][STKTRANSFERFEE] = commission.STKTransferFee;
                    tb.Rows[r][STKSTAMPRATE] = commission.STKStampTaxRate;

                    templateIdxmap.Add(commission.ID, r);
                    templatemap.Add(commission.ID, commission);
                }
                else
                {
                    tb.Rows[r][NAME] = commission.Name;
                    tb.Rows[r][STKCOMMISSONRATE] = commission.STKCommissioinRate;
                    tb.Rows[r][STKTRANSFERFEE] = commission.STKTransferFee;
                    tb.Rows[r][STKSTAMPRATE] = commission.STKStampTaxRate;
                }
            }
        }



        const string ID = "全局编号";
        const string NAME = "名称";

        const string STKCOMMISSONRATE = "交易手续费率";
        const string STKTRANSFERFEE = "过户费";
        const string STKSTAMPRATE = "印花税";
       


        DataTable tb = new DataTable();
        BindingSource datasource = new BindingSource();
        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = commissionGrid;

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeight = 25;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            grid.Margin = new Padding(0);
        }

        /// <summary>
        /// 初始化数据表格
        /// </summary>
        private void InitTable()
        {
            tb.Columns.Add(ID);//0
            tb.Columns.Add(NAME);
            tb.Columns.Add(STKCOMMISSONRATE);
            tb.Columns.Add(STKTRANSFERFEE);
            tb.Columns.Add(STKSTAMPRATE);
           

        }

        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = commissionGrid;
            datasource.DataSource = tb;
            datasource.Sort = ID + " DESC";
            grid.DataSource = datasource;

            grid.Columns[ID].Visible = false;

            grid.Columns[NAME].Width = 60;
            grid.Columns[STKCOMMISSONRATE].Width = 80;
            grid.Columns[STKTRANSFERFEE].Width = 80;
            grid.Columns[STKSTAMPRATE].Width = 80;

            for (int i = 0; i < tb.Columns.Count; i++)
            {
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

      

    }
}
