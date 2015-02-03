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
using Warehouse.Sys;
using AxgrproLib;

namespace Warehouse.StockReport
{
    public partial class ReceiptReportForm : Form
    {
        //定义Grid++Report报表主对象
        protected GridppReport Report = new GridppReport();
        protected GridppReport SubReport = new GridppReport();
        public ReceiptReportForm()
        {
            InitializeComponent();

            Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "JXMainbord.grf");

            //设置与数据源的连接串，因为在设计时指定的数据库路径是绝对路径。
            Report.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);


            //子报表关联
            SubReport.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "CRstoreChild.grf");
            SubReport.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            SubReport.Initialize += new _IGridppReportEvents_InitializeEventHandler(SubReport_Initialize);

            Report.ControlByName("SubReport1").AsSubReport.Report = SubReport;

            //设定查询显示器关联的报表
            this.axGRDisplayViewer1.Report = Report;     

        }

        void Report_Initialize()
        {
            Report.ParameterByName("BeginDate").AsString = this.DateTimePickerFrom.Value.ToString("yyyy-MM-dd").Trim();
            Report.ParameterByName("EndDate").AsString = this.DateTimePickerTo.Value.ToString("yyyy-MM-dd").Trim();
            string sql = "select SiteName from T_SysConfig";
            Report.ParameterByName("Title").AsString = (new DBUtil()).Get_Single_val(sql);
            string strSql = "";
            if (this.comboBoxReceiptTypeID.SelectedIndex == 0)
            {
                Report.ParameterByName("ReceiptTypeID").AsString = "";
                strSql = "select T_ReceiUsCsStore.* from T_ReceiUsCsStore order by 类别,单据日期 ";
            }
            else
            {
                Report.ParameterByName("ReceiptTypeID").AsString = this.comboBoxReceiptTypeID.Text.Trim();
                strSql = "select T_ReceiUsCsStore.* from T_ReceiUsCsStore where '{0}'<=单据日期 And 单据日期<='{1}'and 类别='{2}' order by 类别,单据日期 ";
            }
            strSql = string.Format(strSql, this.DateTimePickerFrom.Value.ToString().Trim(), this.DateTimePickerTo.Value.ToString().Trim(), this.comboBoxReceiptTypeID.Text.Trim());

            Report.DetailGrid.Recordset.QuerySQL = strSql;            
        }

        void SubReport_Initialize()
        {
            //此处相当于重新绑定了SQL查询语句(可为另一个不同的SQL)
            string ReceiptId = Report.FieldByName("单据号").AsString;
            string strSql = "select T_Receipts_Det.ReceiptId as 单据号,T_Receipts_Det.MatId 商品编号, T_MatInf.matname 商品名称," +
                           "T_Receipts_Det.num 数量,T_MatInf.Units 单位, T_Receipts_Det.price 单价,T_Receipts_Det.Amount 金额," +
                           "T_Receipts_Det.BoxNo 箱号, T_Receipts_Det.lotCode 批号 from T_Receipts_Det, " +
                           "T_MatInf where T_Receipts_Det.MatId= T_MatInf.MatId " +
                            "and ReceiptID='{0}'";
            strSql = string.Format(strSql, ReceiptId);
            SubReport.DetailGrid.Recordset.QuerySQL = strSql; 

        }

        private void ReceiptReportForm_Load(object sender, EventArgs e) 
        { 
            (new InitFuncs()).InitComboBox(this.comboBoxReceiptTypeID, "T_ReceiptModal", "ReceTypeID");
            this.DateTimePickerFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//默认当月第一天

            //this.axGRDisplayViewer1.Start();           
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer1.Stop();
            this.axGRDisplayViewer1.Start();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Report.Print(true);
        }

    }
}
