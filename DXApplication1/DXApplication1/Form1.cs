using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        DevExpress.XtraGrid.GridControl gridControl;
        DevExpress.XtraGrid.Views.Grid.GridView gridView;

        DataTable gt = new DataTable();
        BindingSource datasource = new BindingSource();

        public Form1()
        {
            InitializeComponent();

            gridControl = new DevExpress.XtraGrid.GridControl();
            gridControl.Dock = DockStyle.Fill;

            gridView = new DevExpress.XtraGrid.Views.Grid.GridView();

            gridControl.Location = new System.Drawing.Point(74, 41);
            gridControl.MainView = gridView;
            gridControl.Name = "gridControl1";
            gridControl.Size = new System.Drawing.Size(549, 339);
            gridControl.TabIndex = 1;
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });

            var column1 = new DevExpress.XtraGrid.Columns.GridColumn();
            column1.Name="Demo1";
            column1.Caption = "[Demo1]";
            column1.Visible = true;
            column1.Width = 100;
            gridView.Columns.Add(column1);

            var column2 = new DevExpress.XtraGrid.Columns.GridColumn();
            column2.Name = "Demo2";
            column2.Caption = "[Demo2]";
            column2.Visible = true;
            column2.Width = 200;
            gridView.Columns.Add(column2);

            gridView.Columns.AddField("DEMO3");

            gridView.GridControl = gridControl;

            this.Controls.Add(gridControl);

            gt.Columns.Add("Demo1");//0
            gt.Columns.Add("Demo2");//1

            gt.Rows.Add("");
            gt.Rows[0]["Demo1"] = "demo1";
            gt.Rows[0]["Demo2"] = "demo2";

            datasource.DataSource = gt;
            //datasource.Sort = ACCOUNT + " ASC";

            
            gridControl.DataSource = gt;
        }
    }
}
