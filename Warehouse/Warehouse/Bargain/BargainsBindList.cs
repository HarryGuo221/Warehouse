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
    public partial class BargainsBindList : Form
    {
        string TjWhere = "";
        public BargainsBindList()
        {
            InitializeComponent();
        }

        void BargainsBindAddChg_()
        {
            show_dgv_lst();
        }

        private void show_dgv_lst()
        {
            string sql_ = "select bdid as 编号,bdstatus as 状态,occurtime as 捆绑时间,memo as 备注,"
                    +"BaseFee as 基本金额,ynbasefee as 捆绑金额,"
                    + "ynbaseNum as 捆绑张数,All_manufacts as 相关机号"
                    + " from T_BargBind ";
            sql_ = sql_ + TjWhere;
            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_lst.DataSource = dt.DefaultView;
            this.dgv_lst.Columns["相关机号"].Width = 260;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            BargainsBindAdd bba = new BargainsBindAdd();
            bba.BargainsBindAddChg_ += new BargainsBindAdd.BargainsBindAddChg(BargainsBindAddChg_);
            bba.type = "add";
            bba.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.dgv_lst.SelectedRows.Count <= 0) return;
            if (this.dgv_lst.SelectedRows[0].Cells["编号"].Value == null) return;
            string djh_ = this.dgv_lst.SelectedRows[0].Cells["编号"].Value.ToString().Trim();
            if (djh_ == "") return;
            BargainsBindAdd bba = new BargainsBindAdd();
            bba.BargainsBindAddChg_ += new BargainsBindAdd.BargainsBindAddChg(BargainsBindAddChg_);
            bba.bdid = djh_;
            bba.type = "edit";
            bba.ShowDialog();
        }

        private void BargainsBindList_Load(object sender, EventArgs e)
        {
            //this.dgv_lst.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; 
            this.dgv_lst.AllowUserToAddRows = false;
            this.comboBox1.SelectedIndex = 0;
            show_dgv_lst();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.dgv_lst.SelectedRows.Count <= 0) return;
            if (this.dgv_lst.SelectedRows[0].Cells["编号"].Value == null) return;
            string djh_ = this.dgv_lst.SelectedRows[0].Cells["编号"].Value.ToString().Trim();
            if (djh_ == "") return;
            if (MessageBox.Show("确认移除该项捆绑信息吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                 == DialogResult.OK)
            {
                string sql_ = "";
                List<string> SqlLst = new List<string>();
                sql_ = "delete from T_BargBindSet where bdid='" + djh_+"'";
                SqlLst.Add(sql_);
                sql_ = "delete from T_BargBindMacs where bdid='" + djh_ + "'";
                SqlLst.Add(sql_);
                sql_ = "delete from T_BargBind where bdid='" + djh_ + "'";
                SqlLst.Add(sql_);
                try
                {
                    (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                    MessageBox.Show("删除成功");
                    BargainsBindAddChg_();
                }
                catch
                { 
                }

            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() != "全部")
                TjWhere = " where bdstatus like '" + comboBox1.Text.Trim() + "'";
            else
                TjWhere = "";
            show_dgv_lst();
        }
    }
}
