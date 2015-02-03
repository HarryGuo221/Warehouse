using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using grproLib;
using Warehouse.Base;
using Warehouse.DB;

namespace Warehouse.StockReport
{
    public partial class NotOffSetForm : Form
    {
        //定义Grid++Report报表主对象
        protected GridppReport Report = new GridppReport();
       
        private string Datasql = "";
        private string TempTablename = "";
        public NotOffSetForm(string Datasql)
        {
            InitializeComponent();
            this.Datasql = Datasql;
         
            Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "NotOffSetReport.grf");

            //设置与数据源的连接串，因为在设计时指定的数据库路径是绝对路径。
            Report.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            Report.Initialize +=new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);

            //设定查询显示器关联的报表
            this.axGRDisplayViewer1.Report = Report;            
        }

        void Report_Initialize()
        {
            string sele_CoName = "select sitename from T_Sysconfig";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sele_CoName);
            if (dt != null && dt.Rows.Count > 0)
                Report.ParameterByName("Title").AsString = dt.Rows[0]["sitename"].ToString().Trim();
            
            //string TimeFrom = this.dateTimePicker1.Value.ToString("yyyy-MM-dd").Trim();
            //string TimeTo = this.dateTimePicker2.Value.ToString("yyyy-MM-dd").Trim();

            Report.DetailGrid.Recordset.QuerySQL = Datasql;
        }
        private void NotOffSetForm_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//默认当月第一天
            this.axGRDisplayViewer1.Start();          
        }

       
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Report.Print(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer1.Stop();
            axGRDisplayViewer1.Start();
        }
    }
}
