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
    public partial class FormMatErrLst : Form
    {
        public FormMatErrLst()
        {
            InitializeComponent();
        }

        //显示该物料的相关故障
        private void show_Grid1(int curindex_)
        {
            string strSql = "select sysid,errorName as 故障定义,errorCode as 故障代码,"
                +"ErrorPlace as 故障部位,errorApperance as 故障现象,memo as 故障描述 "
                + "from T_errors";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            InitFuncs IniF = new InitFuncs();
            IniF.InitDataGridView(dataGridView1, dt);
            this.dataGridView1.Columns[0].Visible = false;

            //设置宽度
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (dataGridView1.Columns[i].HeaderText == "故障编码")
                {
                    dataGridView1.Columns[i].Width = 80;
                }
                else if (dataGridView1.Columns[i].HeaderText == "故障部位")
                {
                    dataGridView1.Columns[i].Width = 150;
                }
                else if (dataGridView1.Columns[i].HeaderText == "故障现象")
                {
                    dataGridView1.Columns[i].Width = 200;
                }
                else if (dataGridView1.Columns[i].HeaderText == "故障描述")
                {
                    dataGridView1.Columns[i].Width = 200;
                }
            }
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.ClearSelection();
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[curindex_].Cells[0];
                    dataGridView1.Rows[curindex_].Selected = true;
                }
            }
            catch (Exception ex)
            {
            }

        }

        //显示故障解决方法列表
        private void show_Grid2(string errsysid_, int curindex_)
        {
            string strSql = "select T_ErrSol.Esysid,T_ErrSol.sysid,T_ErrSol.reason as 可能原因,T_ErrSol.solution as 解决方法,"
                    +" T_users.Username as 提供者 "
                    + " from T_ErrSol "
                    + "  left join T_users on T_ErrSol.gperson=T_users.userid "
                    + " where T_ErrSol.Esysid=" + errsysid_;
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            InitFuncs IniF = new InitFuncs();
            IniF.InitDataGridView(dataGridView2, dt);
            //设置宽度
            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                if (dataGridView2.Columns[i].HeaderText == "sysid")
                {
                    dataGridView2.Columns[i].Visible = false;
                }
                if (dataGridView2.Columns[i].HeaderText == "Esysid")
                {
                    dataGridView2.Columns[i].Visible = false;
                }
                else if (dataGridView2.Columns[i].HeaderText == "可能原因")
                {
                    dataGridView2.Columns[i].Width = 300;
                }
                else if (dataGridView2.Columns[i].HeaderText == "解决方法")
                {
                    dataGridView2.Columns[i].Width = 300;
                }
            }
            ///
            if (dataGridView2.Rows.Count > 0)
            {
                try
                {
                    dataGridView2.ClearSelection();
                    this.dataGridView2.CurrentCell = this.dataGridView2.Rows[curindex_].Cells[1];
                    dataGridView2.Rows[curindex_].Selected = true;
                }
                catch
                { }
            }
        }

        private void FormMatErrLst_Load(object sender, EventArgs e)
        {
            show_Grid1(0);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormMatErrAdd fma = new FormMatErrAdd();
            fma.type = "add";
            fma.ErrInfoFormChange_ += new FormMatErrAdd.ErrInfoFormChange(MatErrAddForm_ErrInfoFormChange);
            fma.ShowDialog();

        }

        void MatErrAddForm_ErrInfoFormChange()
        {
            show_Grid1(dataGridView1.Rows.Count);
        }

        void MatErrSolAddForm_ErrSolFormChange()
        {
            
            try
            {
                int curindex_ = this.dataGridView2.CurrentRow.Index;  //选中行index
                string sysid_ = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();

                show_Grid2(sysid_, curindex_);
            }
            catch (Exception ex)
            { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int curindex_ = this.dataGridView1.CurrentRow.Index;
                if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;

                string sysid_ = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
                if (sysid_ == "") return;

                FormMatErrAdd fma = new FormMatErrAdd();
                fma.sysid_ = sysid_;
                fma.type = "edit";
                fma.ErrInfoFormChange_ += new FormMatErrAdd.ErrInfoFormChange(MatErrAddForm_ErrInfoFormChange);
                fma.ShowDialog();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            
            
            this.show_Grid2(sysid_, 0);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            FormErrAddSol feas = new FormErrAddSol();
            feas.type = "add";
            feas.ErrSysid_ = sysid_;
            feas.ErrSolFormChange_ += new FormErrAddSol.ErrSolFormChange(MatErrSolAddForm_ErrSolFormChange);
            feas.ShowDialog();

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;
            string Errsysid_ = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (Errsysid_ == "") return;

            if (dataGridView2.SelectedRows.Count <= 0) return;
            if (this.dataGridView2.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dataGridView2.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();

            if (sysid_ == "") return;
            FormErrAddSol feas = new FormErrAddSol();
            feas.type = "edit";
            feas.ErrSysid_ = Errsysid_;
            feas.sysid_ = sysid_;
            feas.ErrSolFormChange_ += new FormErrAddSol.ErrSolFormChange(MatErrSolAddForm_ErrSolFormChange);
            feas.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            if (dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;

            string sysid_ = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;

            if (MessageBox.Show("确定删除该条[故障现象]及其解决方案吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                List<string> SqlLst = new List<string>();
                //删除解决方案
                sql_ = "delete from T_ErrSol where Esysid=" + sysid_;
                SqlLst.Add(sql_);
                //删除故障编码
                sql_ = "delete from t_errors where sysid=" + sysid_ ;
                SqlLst.Add(sql_);
                try
                {
                    (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                }
                catch
                { }
                show_Grid1(0);
                show_Grid2(sysid_,0);
            }

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            if (dataGridView2.SelectedRows.Count <= 0) return;
            if (this.dataGridView2.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dataGridView2.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            string Errsysid_ = this.dataGridView2.SelectedRows[0].Cells["esysid"].Value.ToString().Trim();

            if (sysid_ == "") return;

            if (MessageBox.Show("确定删除该条[故障解决方法信息]吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql_ = "delete from t_ErrSol where sysid=" + sysid_ + " and Esysid=" + Errsysid_;
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(sql_);
                }
                catch
                { }
                show_Grid2(Errsysid_, 0);
            }
        }
    }


}        


