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
    public partial class ModelMatsPrice : Form
    {
        string TJwhere = "";

        string CurModelSysid = "";//机型编号
        string CurModel = "";  //机型名称
        public ModelMatsPrice()
        {
            InitializeComponent();
        }

        private void ModelMatsPrice_Load(object sender, EventArgs e)
        {
            this.TJwhere = "";
            this.show_model();
        }
        private void show_model()
        {
            string sql = "select sysid, ModelName as 机型名,"
                + "ProdCtgrID as 分类,"
                + "ModelType as 机器类别,"
                + "ModelName1 as 机型别名1,ModelName2 as 机型别名2,"
                + "ModelGrade  as 机器等级,"
                + "Modelbrand as 品牌 "
                + "from T_Model" + " " + this.TJwhere;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql);
            //初始化显示数据表
            (new InitFuncs()).InitDataGridView(this.dgv_model, dt);
            //隐藏第一列
            this.dgv_model.Columns["sysid"].Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.TJwhere = "";
            this.show_model();
        }

        private void dgv_model_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RestartShow();
        }
        //初始化
        public void RestartShow()
        {
            if (this.dgv_model.SelectedRows.Count <= 0) return;
            if (this.dgv_model.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_model.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            string m_ = this.dgv_model.SelectedRows[0].Cells["机型名"].Value.ToString().Trim();
            this.CurModelSysid = sysid_;
            this.CurModel = m_;
            //耗材
            show_dgv_consume(this.CurModel);
            //选购件
            show_dgv_part(this.CurModel);
        }
        private void show_dgv_consume(string model)
        {
            string sql_ = "select T_ModelMatsPrice.sysid ,T_ModelMatsPrice.modelsysid 机型编号," +
                          "T_Model .ModelName 机型名,T_ModelMatsPrice.MatId 物料编码," +
                          "T_MatInf .MatName 物料名称,stype 类型,refPrice 参考报价," +
                          "T_ModelMatsPrice.memo 备注 from T_ModelMatsPrice left join T_Model on " +
                          "T_Model .SysID =T_ModelMatsPrice .modelsysid left join T_MatInf on " +
                          "T_MatInf .MatId =T_ModelMatsPrice .MatId" +
                          " where T_Model .ModelName='" + model + "'" +
                          " and stype='耗材'";
            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_consume.DataSource = dt.DefaultView;
            this.dgv_consume.Columns["sysid"].Visible = false;
            this.dgv_consume.ReadOnly = false;
            this.dgv_consume.Columns["机型编号"].Visible = false;
            this.dgv_consume.Columns["机型名"].ReadOnly = true;
            this.dgv_consume.Columns["物料名称"].ReadOnly = true;
            this.dgv_consume.Columns["类型"].ReadOnly = true;

        }

        private void show_dgv_part(string model)
        {
            string sql_ = "select T_ModelMatsPrice.sysid ,T_ModelMatsPrice.modelsysid 机型编号," +
                           "T_Model .ModelName 机型名,T_ModelMatsPrice.MatId 物料编码," +
                           "T_MatInf .MatName 物料名称,stype 类型,refPrice 参考报价," +
                           "T_ModelMatsPrice.memo 备注 from T_ModelMatsPrice left join T_Model on " +
                           "T_Model .SysID =T_ModelMatsPrice .modelsysid left join T_MatInf on " +
                           "T_MatInf .MatId =T_ModelMatsPrice .MatId" +
                           " where T_Model .ModelName='" + model + "'" +
                           " and stype='选购件'";
            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_part.DataSource = dt.DefaultView;
            this.dgv_part.Columns["sysid"].Visible = false;
            this.dgv_part.ReadOnly = false;
            this.dgv_part.Columns["机型编号"].Visible = false;
            this.dgv_part.Columns["机型名"].ReadOnly = true;
            this.dgv_part.Columns["物料名称"].ReadOnly = true;
            this.dgv_part.Columns["类型"].ReadOnly = true;
        }
        #region 相应回车事件
        /// <summary>
        /// 相应回车事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if ((keyData == Keys.Enter) && (dgv_consume.Columns [dgv_consume .CurrentCell .ColumnIndex ].HeaderText =="物料编码"))
                {
                    //先结束边界,否则获取不了当前输入的值
                    dgv_consume.EndEdit();
                    string LikeNameorID = "";
                    int currentRowIndex = dgv_consume.SelectedCells[0].RowIndex;
                    //获取当前单元格的值
                    LikeNameorID = dgv_consume.CurrentCell.Value.ToString();
                    string sql = "select Matid 物料编号,MatName 物料名称 from T_MatInf " +
                                "where Matid like '" + LikeNameorID + "%' or MatName like '" + LikeNameorID + "%'";
                    DataTable dt = (new SqlDBConnect()).Get_Dt(sql);
                    FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                    fr.unVisible_Column_index_ = -1;
                    if (fr.ShowDialog() == DialogResult.OK)
                    {
                        dgv_consume.Rows[currentRowIndex].Cells["机型编号"].Value = this.CurModelSysid;
                        dgv_consume.Rows[currentRowIndex].Cells["机型名"].Value = this.CurModel;
                        dgv_consume.Rows[currentRowIndex].Cells["物料编码"].Value = fr.dr_.Cells["物料编号"].Value.ToString();
                        dgv_consume.Rows[currentRowIndex].Cells["物料名称"].Value = fr.dr_.Cells["物料名称"].Value.ToString();
                        dgv_consume.Rows[currentRowIndex].Cells["类型"].Value = "耗材";
                    }
                }
            }
            else 
            {
                if ((keyData == Keys.Enter) && (dgv_part.Columns[dgv_part.CurrentCell.ColumnIndex].HeaderText == "物料编码"))
                {
                    //先结束编辑状态,否则获取不了当前输入的值
                    dgv_part.EndEdit();
                    string LikeNameorID = "";
                    int currentRowIndex = dgv_part.SelectedCells[0].RowIndex;
                    //获取当前单元格的值
                    LikeNameorID = dgv_part.CurrentCell.Value.ToString();
                    string sql = "select Matid 物料编号,MatName 物料名称 from T_MatInf " +
                                "where Matid like '%" + LikeNameorID + "%' or MatName like '%" + LikeNameorID + "%'";
                    DataTable dt = (new SqlDBConnect()).Get_Dt(sql);
                    FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                    fr.unVisible_Column_index_ = -1;
                    if (fr.ShowDialog() == DialogResult.OK)
                    {
                        dgv_part.Rows[currentRowIndex].Cells["机型编号"].Value = this.CurModelSysid;
                        dgv_part.Rows[currentRowIndex].Cells["机型名"].Value = this.CurModel;
                        dgv_part.Rows[currentRowIndex].Cells["物料编码"].Value = fr.dr_.Cells["物料编号"].Value.ToString();
                        dgv_part.Rows[currentRowIndex].Cells["物料名称"].Value = fr.dr_.Cells["物料名称"].Value.ToString();
                        dgv_part.Rows[currentRowIndex].Cells["类型"].Value = "选购件";
                    }
                }
            }
            //继续原来base.ProcessCmdKey中的处理
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void ChildSave_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                ChildSavedata(dgv_consume, "耗材");
            }
            else
            {
                ChildSavedata(dgv_part, "选购件");
            }
        }
        #region 通用保存数据
        /// <summary>
        /// 通用保存数据
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="Typename"></param>
        public void ChildSavedata(DataGridView dgv, string Typename)
        {
            List<string> SqlLst = new List<string>();
            int i = 0;
            string sysid = "";
            string matid = "";
            string Sql = "";
            string refPrice = "";
            string Memo = "";
            string strsql = "";
            int Count = dgv.Rows.Count;
            string matsysid = dgv_model.SelectedRows[0].Cells["sysid"].Value.ToString();
            strsql = "delete T_ModelMatsPrice where modelsysid='" + matsysid + "' and Stype='" + Typename + "'";
            SqlLst.Add(strsql);
            for (i = 0; i < Count - 1; i++)
            {
                //先结束边界，不然不能获取当前值
                dgv.EndEdit();
                sysid = dgv.Rows[i].Cells["sysid"].Value.ToString();
                matid = dgv.Rows[i].Cells["物料编码"].Value.ToString();
                if (matid == "") continue;
                refPrice = dgv.Rows[i].Cells["参考报价"].Value.ToString();

                Memo = dgv.Rows[i].Cells["备注"].Value.ToString();
                Sql = "insert into T_ModelMatsPrice(modelsysid,Matid,Stype,refPrice,memo) values " +
                          "('" + matsysid + "','" + matid + "','" + Typename + "','" + refPrice + "','" + Memo + "') ";
                SqlLst.Add(Sql);
            }
            try
            {
                (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                msm_MatsSelect_();
            }
            catch
            {
                MessageBox.Show("当前存在与该物料编码不对应的物料名,请检查后再保存！", "提示");
            }
        }
        #endregion
        private void msm_MatsSelect_()
        {
            show_dgv_consume(this.CurModel);
            show_dgv_part(this.CurModel);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                DeleteMethoed(dgv_consume, "耗材");
            }
            else
            {
                DeleteMethoed(dgv_part, "选购件");
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Dagview"></param>
        /// <param name="Typename"></param>
        public void DeleteMethoed(DataGridView Dagview, string Typename)
        {
            //是否允许删除选中行
            Dagview .AllowUserToDeleteRows = true;
            //执行删除代码
            foreach (DataGridViewRow r in Dagview.SelectedRows)
            {
                Dagview.Rows.Remove(r); 
            }
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1 .Text.Trim() != "")
            {
                this.TJwhere = " where ModelName like '%" + this.textBox1.Text.Trim() + "%'" +
                               " or modelbrand like '%" + this.textBox1.Text.Trim() + "%'";
                show_model();
            }
            else
            {
                this.TJwhere  = "";
                show_model();
            }
        }

        private void dgv_consume_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("当前列只能输入数字！按‘Esc’键返回", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgv_part_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("当前列只能输入数字！按‘Esc’键返回", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #region //限制只能输入数字及'.'
        private void dgv_consume_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = e.Control as TextBox;
            if (this.dgv_consume.CurrentCell.OwningColumn.HeaderText == "参考报价")
                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            else
                tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            //8为删除键
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void dgv_part_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = e.Control as TextBox;
            if (this.dgv_part.CurrentCell.OwningColumn.HeaderText == "参考报价")
                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            else
                tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
        }
        #endregion
    }
}
