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
    public partial class ModelErrDocDEF : Form
    {
        private string TJwhere = "";
        private string select_model_sysid = "";
        public ModelErrDocDEF()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModelErrDocDEF_Load(object sender, EventArgs e)
        {

        }

        private void show_rela_err_doc(string modelsysid)
        {
            string sql_ = "select T_Errors.sysid,T_Errors.ErrorName as 故障定义,"
                +" T_Errors.ErrorCode as 故障代码,"
                +"T_Errors.ErrorPlace as 故障部位,"
                + "T_Errors.ErrorApperance as 故障现象 "
                + " from t_model, T_ModelErrDocs,T_Errors "
                + " where t_model.sysid=T_ModelErrDocs.modelsysid "
                + " and T_Errors.sysid=T_ModelErrDocs.ErrOrDocid "
                + " and typeid=0"
                + " and T_ModelErrDocs.modelsysid=" + modelsysid;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_err.DataSource = dt.DefaultView;
            this.dgv_err.Columns[0].Visible = false;

            string sql1_ = "select T_MatDocs.sysid,T_MatDocs.DocName as 资料名称,"
               + " T_MatDocs.Doctype as 类型,"
               + "T_MatDocs.Memo as  备注"
               + " from t_model, T_ModelErrDocs,T_MatDocs "
               + " where t_model.sysid=T_ModelErrDocs.modelsysid "
               + " and T_MatDocs.sysid=T_ModelErrDocs.ErrOrDocid "
               + " and typeid=1"
               + " and T_ModelErrDocs.modelsysid=" + modelsysid;
            DataTable dt1 = (new SqlDBConnect()).Get_Dt(sql1_);
            this.dgv_doc.DataSource = dt1.DefaultView;
            this.dgv_doc.Columns[0].Visible = false;
        }

        private void show_model()
        {
            string sql = "select SysID, ModelName as 机型名,"
                + "ProdCtgrID as 分类,"  
                + "ModelType as 机器类别,"
                + "ModelName1 as 机型别名1,ModelName2 as 机型别名2,"
                +" ModelGrade  as 机器等级,"
                + "Modelbrand as 品牌 "
                + "from T_Model" + " " + this.TJwhere;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql);
            //初始化显示数据表
            (new InitFuncs()).InitDataGridView(this.dgv_model, dt);
            //隐藏第一列
            this.dgv_model.Columns[0].Visible = false;
            
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.TJwhere = "";
            show_model();
        }

        private void dgv_model_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            select_model_sysid = "";
            if (this.dgv_model.SelectedRows.Count <= 0) return;
            if (this.dgv_model.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid = this.dgv_model.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid == "") return;
            select_model_sysid = sysid;

            show_rela_err_doc(sysid);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool isexist = false;
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string where = "where ErrorName like '%{0}%'" +
                                 "or ErrorCode like '%{1}%'" +
                                 "or ErrorPlace like '%{2}%'" +
                                 "or ErrorApperance like '%{3}%'" +
                                 "or Memo like '%{4}%'";
                where = string.Format(where, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);

                string BaseSql = "select sysid,ErrorName as 故障定义,"
                + " ErrorCode as 故障代码,"
                + " ErrorPlace as 故障部位,"
                + " ErrorApperance as 故障现象  "
                + " from T_Errors ";

                //显示列表
                DataTable dt = (new SqlDBConnect()).Get_Dt(BaseSql + where);
                FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                fr.unVisible_Column_index_ = 0;
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    if (fr.dr_.Cells["sysid"].Value == null) return;
                    if (fr.dr_.Cells["sysid"].Value.ToString().Trim() == "") return;
                    string sysid_=fr.dr_.Cells["sysid"].Value.ToString().Trim();
                    //
                    string sql_ = "select ErrOrDocid from T_ModelErrDocs "
                        + " where modelsysid=" + select_model_sysid
                        + " and typeid=0 and ErrOrDocid=" + sysid_;
                    isexist = (new DBUtil()).yn_exist_data(sql_);
                    if (isexist == false)
                    {
                        sql_ = "insert into T_ModelErrDocs(modelsysid,typeid,ErrOrDocid)"
                            + " values({0},{1},{2})";
                        sql_ = string.Format(sql_, select_model_sysid, 0, sysid_);
                        (new SqlDBConnect()).ExecuteNonQuery(sql_);
                        show_rela_err_doc(select_model_sysid);
                    }

                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool isexist = false;
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string where = "where DocName like '%{0}%'" +
                                 "or Doctype like '%{1}%'" +
                                 "or Memo like '%{2}%'";
                where = string.Format(where, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);

                string BaseSql = "select sysid,DocName as 资料名称,"
                + " Doctype as 类型,"
                + " Memo as 备注"
                + " from T_MatDocs ";

                //显示列表
                DataTable dt = (new SqlDBConnect()).Get_Dt(BaseSql + where);
                FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                fr.unVisible_Column_index_ = 0;
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    if (fr.dr_.Cells["sysid"].Value == null) return;
                    if (fr.dr_.Cells["sysid"].Value.ToString().Trim() == "") return;
                    string sysid_ = fr.dr_.Cells["sysid"].Value.ToString().Trim();
                    //
                    string sql_ = "select ErrOrDocid from T_ModelErrDocs "
                        + " where modelsysid=" + select_model_sysid
                        + " and typeid=1 and ErrOrDocid=" + sysid_;
                    isexist = (new DBUtil()).yn_exist_data(sql_);
                    if (isexist == false)
                    {
                        sql_ = "insert into T_ModelErrDocs(modelsysid,typeid,ErrOrDocid)"
                            + " values({0},{1},{2})";
                        sql_ = string.Format(sql_, select_model_sysid, 1, sysid_);
                        (new SqlDBConnect()).ExecuteNonQuery(sql_);
                        show_rela_err_doc(select_model_sysid);
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.select_model_sysid == "") return;
            if (this.dgv_doc.SelectedRows.Count <= 0) return;
            if (this.dgv_doc.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_doc.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            if (MessageBox.Show("确定移除该机型与选中文档的关联吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql_ = "delete from T_ModelErrDocs "
                    + " where modelsysid=" + this.select_model_sysid
                    + " and ErrOrDocid=" + sysid_
                    + " and typeid=1";
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
                show_rela_err_doc(this.select_model_sysid);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.select_model_sysid == "") return;
            if (this.dgv_err.SelectedRows.Count <= 0) return;
            if (this.dgv_err.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_err.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            if (MessageBox.Show("确定移除该机型与选中故障的关联吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql_ = "delete from T_ModelErrDocs "
                    + " where modelsysid=" + this.select_model_sysid
                    + " and ErrOrDocid=" + sysid_
                    + " and typeid=0";
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
                show_rela_err_doc(this.select_model_sysid);
            }
        }
    }
}
