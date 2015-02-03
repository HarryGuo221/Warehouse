using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Bargain
{
    public partial class FormCopySettleInvoiceLst : Form
    {
        public string OpaMethod; //开票人操作="0",结算登记开票人操作="1"

        public FormCopySettleInvoiceLst()
        {
            InitializeComponent();
        }

        private void FormCopySettleInvoiceLst_Load(object sender, EventArgs e)
        {
            this.dgv_lst.AllowUserToAddRows = false;
            this.dgv_lst.AllowUserToDeleteRows = false;
            show_lst();
        }

        private void show_lst()
        {
            string sql_ = "";
            sql_ = "select rid as 单据编号,total as 开票金额,"
                +" custName as 单位名称,"
                + "lx as 开票类型,occurTime as 单据日期"
                + " from T_SettleAccountMain";
            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_lst.DataSource = dt.DefaultView;
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.dgv_lst.SelectedRows.Count <= 0) return;
            if (this.dgv_lst.SelectedRows[0].Cells["单据编号"].Value == null) return;
            string djh = this.dgv_lst.SelectedRows[0].Cells["单据编号"].Value.ToString().Trim();
            if (djh == "") return;

            FormCopySettleInvoiceAdd fa = new FormCopySettleInvoiceAdd();
            fa.OpaMethod = this.OpaMethod;
            fa.type = "edit";
            fa.Rid = djh;
            fa.ShowDialog();
           
        }
    }
}
