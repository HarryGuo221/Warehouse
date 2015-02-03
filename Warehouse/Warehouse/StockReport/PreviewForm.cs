using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using grproLib;

namespace Warehouse.StockReport
{
    public partial class PreviewForm : Form
    {
        public string RecordTable;//虚拟表
        public PreviewForm()
        {
            //this.RecordTable = table;
            InitializeComponent();
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            axGRPrintViewer1.Start();
        }
        public void AttachReport(GridppReport Report)
        {
            //设定查询显示器关联的报表
            axGRPrintViewer1.Report = Report;
        }
        private void PreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            axGRPrintViewer1.Stop();
            //删除虚拟表
            //(new SqlDBConnect()).ExecuteNonQuery("drop table  " +this.RecordTable + "");
        }
    }
}
