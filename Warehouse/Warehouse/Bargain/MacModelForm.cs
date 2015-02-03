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
    public partial class MacModelForm : Form
    {
        private string TJwhere;
        public MacModelForm()
        {
            InitializeComponent();
        }

        private void show_model()
        {
            string sql = "select SysID, ModelName as 机型名,"
                //+ "ProdCtgrID as 分类,"  //,StatusType as 状态
                //+"ModelType as 机器类别,"
                + "ModelName1 as 机型别名1,ModelName2 as 机型别名2," 
                +"Yearcpycnt as 保修到期张数,Modellifeyear as '使用寿命(年限)',"
                +"Modellifecnt  as '使用寿命(复印量)'," 
                +"Modelmonthcnt as 月复印量,"
                //+"ModelGrade  as 机器等级,"
                +"Yearcpycnt_pred as 预警张数,"
                //+"Modelbrand as 品牌,"
                +"Wgzzs as 无故障张数," 
                +"Ndxs as 难度系数 "
                +"from T_Model" + " " + this.TJwhere;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql);
            //初始化显示数据表
            (new InitFuncs()).InitDataGridView(this.dgv_Model, dt);
            //隐藏第一列
            dgv_Model.Columns[0].Visible = false;
            this.Recordnum.Text = dgv_Model.Rows.Count.ToString();
        }
        private void MacModelForm_Load(object sender, EventArgs e)
        {
            //去除最后空行
            //this.dgv_Model.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Model.AllowUserToAddRows = false; 
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            MacModelFormAdd macform=new MacModelFormAdd ();
            macform.type = "add";
            macform.macmodelchange += new MacModelFormAdd.MacModelChange(show_model);
            macform.ShowDialog();
        }

        private void btnattribute_Click(object sender, EventArgs e)
        {
            if (dgv_Model.SelectedRows.Count <= 0) return;
            if (dgv_Model.SelectedRows[0].Cells["SysID"].Value.ToString() == "") return;
            string ID = this.dgv_Model.SelectedRows[0].Cells["SysID"].Value.ToString();
            if (ID == "") return;
            MacModelFormAdd macform = new MacModelFormAdd();
            macform.type = "edit";
            macform.ID = ID;
            macform.macmodelchange += new MacModelFormAdd.MacModelChange(show_model);
            macform.ShowDialog();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (dgv_Model.SelectedRows.Count <= 0) return;
            if (dgv_Model.SelectedRows[0].Cells["SysID"].Value.ToString() == "") return;
            string ID = this.dgv_Model.SelectedRows[0].Cells["SysID"].Value.ToString();
            if (ID == "") return;
            if (MessageBox.Show("确定删除该条信息吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql_ = "";
                try
                {
                    sql_ = "delete from T_Model where SysID='" + ID + "'";
                    (new SqlDBConnect()).ExecuteNonQuery(sql_);
                    show_model();
                }
                catch(Exception w)
                {
                    MessageBox.Show(w.ToString());
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string where = "where ModelName like '%{0}%'" +
                                 "or ModelName1 like '%{1}%'" +
                                 "or ModelName2 like '%{2}%'" +
                                 "or ProdCtgrID like '%{3}%'" +
                                 "or ModelType like '%{4}%'" +
                                 "or Modelbrand like '%{5}%'" +
                                 "or ModelGrade like '%{6}%' ";
                where = string.Format(where, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);
                this.TJwhere = where;
                show_model();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            show_model();
        }
    }
}
