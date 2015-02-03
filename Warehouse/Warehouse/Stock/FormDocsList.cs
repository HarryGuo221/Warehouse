using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Stock
{
    public partial class FormDocsList : Form
    {
        public FormDocsList()
        {
            InitializeComponent();
        }

        private void show_docs()
        {
            string sql_ = "select sysid,DocName as 资料名称,"
                + "Doctype as 类型,memo as 备注 "
                + " from T_MatDocs ";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_docs.DataSource = dt.DefaultView;
            this.dgv_docs.Columns[0].Visible = false;
        }

        private void FormDocsList_Load(object sender, EventArgs e)
        {
            show_docs();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.dgv_docs.SelectedRows.Count <= 0) return;
            if (this.dgv_docs.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid1_ = this.dgv_docs.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid1_ == "") return;


            DialogResult dr = MessageBox.Show("确认删除所选技术资料信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                string sql_ = "delete from T_MatDocs where sysid=" + sysid1_;
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
                show_docs();
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MatDocsForm mdf = new MatDocsForm();
            mdf.Type = "add";
            mdf.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.dgv_docs.SelectedRows.Count <= 0) return;
            if (this.dgv_docs.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_docs.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            MatDocsForm mdf = new MatDocsForm();
            mdf.Type = "edit";
            mdf.sysid_ = sysid_;
            mdf.ShowDialog();
        }
    }
}
