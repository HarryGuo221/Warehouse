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
using Warehouse.Modal;

namespace Warehouse.StockReport
{
    public partial class PublicDetailForm : Form
    {
        protected GridppReport Report = new GridppReport();
        public  string Username; //操作员
        public  string strSql;   //传回sql
        public  string count;    //记录数据条数
        public  string ReiceName; //报表名称
        public PublicDetailForm()
        {
            InitializeComponent();
        }

        public void ShowReport()
        {
            switch (ReiceName)
            {
                case "JHDetail":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "JHDetail.grf");
                    this.Text = "进货单明细";
                    break;
                case "XSDetail":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "XSDetail.grf");
                    this.Text = "销售单明细";
                    break;
                case "WXBorrow":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "WXBorrowSearch.grf");
                    this.Text = "未销借用查询";
                    break;
                case "WXRent":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "WXRentSearch.grf");
                    this.Text = "未销租机查询";
                    break;
                case "YSSearch":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "YSSearch.grf");
                    this.Text = "应收核销查询";
                    break;
                case "YFSearch":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "YFSearch.grf");
                    this.Text = "应收核销查询";
                    break;
                case "ReceivableReport":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "ReceivableReport.grf");
                    this.Text = "应收核销查询";
                    break;
                case "YFKSearch":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "YFKSearch.grf");
                    this.Text = "预付款查询";
                    break;
                case "YSKSearch":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "YFKSearch.grf");
                    this.Text = "预付款查询";
                    break;
                case "PayableReport":
                    Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "PayableReport.grf");
                    this.Text = "应付核销查询";
                    break;
                default:
                    return;
            }
            Report.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);
            
            //Report.ProcessRecord += new _IGridppReportEvents_ProcessRecordEventHandler(Report_ProcessRecord);
            //设定查询显示器关联的报表
            this.axGRDisplayViewer1.Report = Report;

            this.axGRDisplayViewer1.Start();
            //滚动条归零
            this.progressBar1.Value = 0;
        }
        private void Report_BeforePostRecord()
        {
            this.progressBar1.Value++;
        }
        public void Report_Initialize()
        {
            string sql = "select SiteName from T_SysConfig";
            Report.ParameterByName("Title").AsString = (new DBUtil()).Get_Single_val(sql);
            string strsql = "";
            strsql = this.strSql; 
            
            Report.DetailGrid.Recordset.QuerySQL = strsql;
            //获取提交每条记录的记录数所触发BeforePostRecord事件
            Report.BeforePostRecord +=new _IGridppReportEvents_BeforePostRecordEventHandler(Report_BeforePostRecord);
            //this.progressBar1.Value = 0;
            ShowDetail();
            
        }
        public void ShowDetail()
        {
            LoginForm loginForm = new LoginForm();
            Worktime.Text = "工作年月：" + loginForm.getCurWorkMonth();
            Date.Text = "日期：" + DBUtil.getServerTime().ToString("yyyy年MM月dd日");
            Opername.Text = "操作员：" + this.Username;
            RecordNum.Text = "查询结果：共有" + count + "条记录";
        }
        private void JHDetailForm_Load(object sender, EventArgs e)
        {
            //this.axGRDisplayViewer1.Start();
            //初始化控件 
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            Report.Print(true);
        }

        private void btnprintview_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);
        }

        private void axGRDisplayViewer1_ContentCellDblClick(object sender, global::AxgrproLib._IGRDisplayViewerEvents_ContentCellDblClickEvent e)
        {
            //获取对应列的行头值
            string ReceiptId = Report.FieldByName("单据号").AsString;
            string ReceiptTypeId = Report.FieldByName("单据类别").AsString;
            if (ReceiptId == "") return;
            ListModalForm listModalForm = new ListModalForm(ReceiptTypeId, "edit", ReceiptId, "");
            listModalForm.MdiParent = this.MdiParent;
            listModalForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.axGRDisplayViewer1.Stop();
            this.axGRDisplayViewer1.Start();
            //滚动条归零
            this.progressBar1.Value = 0;
        }
    }
}
