using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Warehouse.Base;
using grproLib;
using Warehouse.DB;

namespace Warehouse.Modal
{
    public partial class ChooseForm : Form
    {

        string strReceiptId = "";
        string strReceTypeId = "";
        //定义Grid++Report报表主对象
        private GridppReport Report = new GridppReport();
        private GridppReport SubReport = new GridppReport();
        public ChooseForm(string a,string b)
        {
            strReceiptId = a;
            strReceTypeId = b;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "JXMainbord.grf");
            //设置与数据源的连接串，因为在设计时指定的数据库路径是绝对路径。
            Report.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);

            //子报表关联
            SubReport.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "CRstoreChild.grf");
            SubReport.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();

            string strSql1 = "select T_ReceiUsCsStore.* from T_ReceiUsCsStore where T_ReceiUsCsStore.单据号='{0}' order by 类别,单据日期 ";
            strSql1 = string.Format(strSql1, this.strReceiptId);

            string strSql = "select T_Receipts_Det.ReceiptId as 单据号,T_Receipts_Det.MatId 商品编号, T_MatInf.matname 商品名称," +
                            "T_Receipts_Det.num 数量,T_MatInf.Units 单位, T_Receipts_Det.price 单价,T_Receipts_Det.Amount 金额," +
                            "T_Receipts_Det.BoxNo 箱号, T_Receipts_Det.lotCode 批号 from T_Receipts_Det, " +
                            "T_MatInf where T_Receipts_Det.MatId= T_MatInf.MatId " +
                             "and ReceiptID='{0}'";
            strSql = string.Format(strSql, this.strReceiptId);
            Report.DetailGrid.Recordset.QuerySQL = strSql1;

            SubReport.DetailGrid.Recordset.QuerySQL = strSql;

            Report.ControlByName("SubReport1").AsSubReport.Report = SubReport;

            Report.PrintPreview(true);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        void Report_Initialize()
        {
            string sql = "select SiteName from T_SysConfig";
            Report.ParameterByName("Title").AsString = (new DBUtil()).Get_Single_val(sql);

            Report.ParameterByName("ReceiptTypeID").AsString = this.strReceTypeId;

            sql = "select ReceName from T_ReceiptModal where ReceTypeID= '" + this.strReceTypeId + "'";
            Report.ParameterByName("ReceiptName").AsString = (new DBUtil()).Get_Single_val(sql);
            if (b1.Checked)
            {
                SubReport.ColumnByName("单价").Visible = false;
                SubReport.ControlByName("SummaryBox6").Visible = false;
            }
            if (b2.Checked)
            {
                SubReport.ColumnByName("金额").Visible = false;
                SubReport.ControlByName("SummaryBox5").Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


    }
}
