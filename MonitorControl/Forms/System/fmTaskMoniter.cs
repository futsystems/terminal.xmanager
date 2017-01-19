using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TradingLib.API;
using TradingLib.Common;
using TradingLib.MoniterCore;
using TradingLib.Contirb.Protocol;


namespace TradingLib.MoniterControl
{
    public partial class fmTaskMoniter : ComponentFactory.Krypton.Toolkit.KryptonForm,IEventBinder
    {
        public fmTaskMoniter()
        {
            InitializeComponent();
            SetPreferences();
            InitTable();
            BindToTable();

            this.Load += new EventHandler(fmTaskMoniter_Load);
        }

        void fmTaskMoniter_Load(object sender, EventArgs e)
        {
            taskgrid.DoubleClick += new EventHandler(taskgrid_DoubleClick);
            taskgrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(taskgrid_RowPrePaint);
            CoreService.EventCore.RegIEventHandler(this);
        }

        void taskgrid_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus;
        }

        //得到当前选择的行号
        private LogTaskEvent CurrentLog
        {
            get
            {
                int row = taskgrid.SelectedRows.Count > 0 ? taskgrid.SelectedRows[0].Index : -1;
                if (row >= 0)
                {
                    return taskgrid[TAG, row].Value as LogTaskEvent;
                }
                else
                {
                    return null;
                }
            }
        }


        void taskgrid_DoubleClick(object sender, EventArgs e)
        {
            LogTaskEvent log = CurrentLog;
            if (log != null)
            {
                fmLogTaskEvent fm = new fmLogTaskEvent();
                fm.SetLogTaskEvent(log);
                fm.ShowDialog();
            }
        }


        public void OnInit()
        {
            CoreService.EventCore.RegisterCallback("LogServer", "QryTaskRunLog", this.OnQryLogTaskEvent);
            CoreService.TLClient.ReqQryTaskRunLog();
        }


        public void OnDisposed()
        {
            CoreService.EventCore.UnRegisterCallback("LogServer", "QryTaskRunLog", this.OnQryLogTaskEvent);
        }


        void OnQryLogTaskEvent(string json, bool islast)
        {
            LogTaskEvent[] logs = CoreService.ParseJsonResponse<LogTaskEvent[]>(json);
            if (logs != null)
            {
                foreach (LogTaskEvent log in logs)
                {
                    GotLogTaskEvent(log);
                }
            }

        }
        ConcurrentDictionary<int, int> logtaskeventmaprowid = new ConcurrentDictionary<int, int>();

        int LogTaskEventIdx(int id)
        {
            int rowid = -1;
            if (!logtaskeventmaprowid.TryGetValue(id, out rowid))
            {
                return -1;
            }
            else
            {
                return rowid;
            }
        }

        void GotLogTaskEvent(LogTaskEvent log)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<LogTaskEvent>(GotLogTaskEvent), new object[] { log });
            }
            else
            {
                int r = LogTaskEventIdx(log.ID);
                if (r == -1)
                {
                    gt.Rows.Add(log.ID);
                    int i = gt.Rows.Count - 1;

                    gt.Rows[i][SETTLEDAY] = log.Settleday;
                    gt.Rows[i][TASKNAME] = log.TaskName;
                    gt.Rows[i][TASKTYPE] = Util.GetEnumDescription(log.TaskType);
                    gt.Rows[i][TASKMEMO] = log.TaskMemo;
                    gt.Rows[i][DATE] = log.Date;
                    gt.Rows[i][TIME] = log.Time;
                    gt.Rows[i][RESULT] = GetTaskRunResult(log.Result);
                    gt.Rows[i][TAG] = log;

                    logtaskeventmaprowid.TryAdd(log.ID, i);

                }
            }
        }


        Image GetTaskRunResult(bool execute)
        {
            if (execute)
                return (Image)Properties.Resources.account_go;
            else
                return (Image)Properties.Resources.account_stop;

        }


        #region 表格
        #region 显示字段

        const string ID = "日志序号";
        const string SETTLEDAY = "结算日";
        const string TASKNAME = "任务名称";
        const string TASKTYPE = "任务类型";
        const string TASKMEMO = "任务描述";
        const string DATE = "完成日期";
        const string TIME = "完成时间";
        const string RESULT = "执行结果";
        const string TAG = "TAG";

        #endregion

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        /// <summary>
        /// 设定表格控件的属性
        /// </summary>
        private void SetPreferences()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = taskgrid;

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

            grid.StateCommon.Background.Color1 = Color.WhiteSmoke;
            grid.StateCommon.Background.Color2 = Color.WhiteSmoke;


        }

        //初始化Account显示空格
        private void InitTable()
        {
            gt.Columns.Add(ID);
            
            gt.Columns.Add(DATE);
            gt.Columns.Add(TIME);
            gt.Columns.Add(SETTLEDAY);
            gt.Columns.Add(TASKNAME);
            gt.Columns.Add(TASKTYPE);
            gt.Columns.Add(TASKMEMO);
            gt.Columns.Add(RESULT,typeof(Image));
            gt.Columns.Add(TAG, typeof(LogTaskEvent));
        }


        /// <summary>
        /// 绑定数据表格到grid
        /// </summary>
        private void BindToTable()
        {
            ComponentFactory.Krypton.Toolkit.KryptonDataGridView grid = taskgrid;

            datasource.DataSource = gt;
            grid.DataSource = datasource;
            grid.Columns[ID].Visible = false;
            grid.Columns[TASKTYPE].Visible = false;
            grid.Columns[TASKMEMO].Visible = false;
            grid.Columns[SETTLEDAY].Visible = false;
            grid.Columns[TAG].Visible = false;

            grid.Columns[SETTLEDAY].Width = 80;
            grid.Columns[TASKNAME].Width = 150;
            grid.Columns[TASKTYPE].Width = 80;
            grid.Columns[TASKMEMO].Width = 200;
            grid.Columns[DATE].Width = 80;
            grid.Columns[TIME].Width = 80;
            grid.Columns[RESULT].Width = 80;

        }






        #endregion

    }
}
