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

namespace Warehouse.StockReport
{
    public partial class UserReportForm : Form
    {
        private string filePath;
        private string cons;//连接字符串
        //定义Grid++Report报表主对象
        protected GridppReport Report = new GridppReport();

        public UserReportForm()
        {
            InitializeComponent();

            this.filePath = GridReportUtility.GetReportTemplatePath();
            this.cons = GridReportUtility.GetDatabaseConnectionString();

            //载入报表模板数据
            Report.LoadFromFile(filePath + "用户报表.grf");

            //设置与数据源的连接串，因为在设计时指定的数据库路径是绝对路径。
            Report.DetailGrid.Recordset.ConnectionString = this.cons;

            //挂接报表事件            
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);

            //设定查询显示器关联的报表
            this.axGRDisplayViewer1.Report = Report;
        }

        void Report_Initialize()
        {
            Report.ParameterByName("PUserId").AsString = this.txtUserId.Text.Trim();
            Report.ParameterByName("PUserName").AsString = this.txtUserName.Text.Trim();

            string strSql = "select UserId as 用户编码, UserName as 用户名, ynAdmin as 是否系统管理员,JobPosition as 职位,SmsTel as 接收短信电话号码 " +
                            "from T_Users " +
                            "where UserName like '%{0}%'";
            strSql = string.Format(strSql, Report.ParameterByName("PUserName").AsString);
            //strSql = string.Format(strSql, this.txtUserName.Text.Trim());
            Report.DetailGrid.Recordset.QuerySQL = strSql;
        }

        private void UserReportForm_Load(object sender, EventArgs e)
        {
            this.axGRDisplayViewer1.Start();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer1.Stop();
            axGRDisplayViewer1.Start();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);
        }

    }
}
