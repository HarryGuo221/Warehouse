using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.Base;
using Warehouse.DAO;

namespace Warehouse.Stock
{
    public partial class MatAssemblyMainForm : Form
    {
        public int curRowIndex = 0;
        private string Type;


        public MatAssemblyMainForm(string type)
        {
            InitializeComponent();
            this.Type = type;
        }
        /// <summary>
        /// 初始化配套信息
        /// </summary>
        public void initDataGridview()
        {
            DataTable dt = (new MaterialDAO()).getMatAssemblyMain();
            (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);

            this.dataGridView1.ClearSelection();
            this.dataGridView1.Rows[this.curRowIndex].Selected = true;
            this.dataGridView1.Columns["sysid"].Visible = false;
            this.dataGridView1.Columns["品牌"].Width = 80;
            this.dataGridView1.Columns["套机名称"].Width = 180;
            this.dataGridView1.Columns["套机描述"].Width = 500;

        }
        /// <summary>
        /// 初始化配套信息详细
        /// </summary>
        public void initDataGridviewChild()
        {
            //DataTable dt = (new MaterialDAO()).getMatAssemblyChild();
            //(new InitFuncs()).InitDataGridView(this.dataGridView2, dt);
            ShowThisDedail();
            this.dataGridView2.Columns["配套编号"].Visible = false;
        }
        /// <summary>
        /// 添加配套信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            EditMatAssemblyMainForm form = new EditMatAssemblyMainForm("add", 0, "");
            form.matAssemblyMainFormChange += new EditMatAssemblyMainForm.MatAssemblyMainFormChange(form_matAssemblyMainFormChange);
            form.ShowDialog();
        }

        void form_matAssemblyMainFormChange()
        {
            initDataGridview();
        }
        /// <summary>
        /// 修改配套信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string strSysId = "", assname = "";
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;
            strSysId = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (strSysId == "") return;
            assname = this.dataGridView1.SelectedRows[0].Cells["套机名称"].Value.ToString().Trim();

            int sysid = Convert.ToInt32(strSysId);
            EditMatAssemblyMainForm form = new EditMatAssemblyMainForm("edit", sysid, assname);
            form.matAssemblyMainFormChange += new EditMatAssemblyMainForm.MatAssemblyMainFormChange(form_matAssemblyMainFormChange);
            form.ShowDialog();

        }
        /// <summary>
        /// 删除配套信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Cells["套机名称"].Value.ToString() != "")
            {
                DialogResult dr = MessageBox.Show("确认删除选中配套信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    string assName = this.dataGridView1.SelectedRows[0].Cells["套机名称"].Value.ToString().Trim();
                    string strSysId = (new DBUtil()).Get_Single_val("T_MatAssemblyMain", "sysid", "assname", assName);
                    if (strSysId != "")
                    {
                        int sysid = Convert.ToInt32(strSysId);
                        (new MaterialDAO()).dele_MatAssemblyMain(sysid, "");
                        initDataGridview();
                    }
                }
            }
        }
        private void MatAssemblyMainForm_Load(object sender, EventArgs e)
        {
            initDataGridview();
            initDataGridviewChild();
        }
        //右键修改
        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }
        //右键删除配套信息
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }
        /// <summary>
        /// 显示当前行对应明细
        /// </summary>
        public void ShowThisDedail()
        {
            string assname = "";
            if (this.dataGridView1.SelectedRows[0].Cells["套机名称"].Value != null)
                assname = this.dataGridView1.SelectedRows[0].Cells["套机名称"].Value.ToString().Trim();
            string strSysId = (new DBUtil()).Get_Single_val("T_MatAssemblyMain", "sysid", "assname", assname);
            if (strSysId != "")
            {
                int sysid = Convert.ToInt32(strSysId);
                DataTable dt = (new MaterialDAO()).getMatAssemblyChild(sysid);
                (new InitFuncs()).InitDataGridView(this.dataGridView2, dt);
                dataGridView2.Columns["配套编号"].Visible = false;
                //对应字段可操作
                this.dataGridView2.ReadOnly = false;
                //该列位只读
                dataGridView2.Columns["物料名称"].ReadOnly = true;
            }
        }
        /// <summary>
        /// 单击主表一行,子表显示改行配套信息详细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowThisDedail();
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 添加或修改dgv中的数据到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            List<string> SqlLst = new List<string>();
            int i = 0;
            string matid = "";
            string num = "";
            string Sql = "";
            string strsql = "";
            int Count = dataGridView2.Rows.Count;
            string sysid = dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString();
            strsql = "delete T_MatAssemblyChild where sysid='" + sysid + "'";
            SqlLst.Add(strsql);
            for (i = 0; i < Count-1; i++)
            {
                //结束编辑状态，不然不能获取当前值
                dataGridView2.EndEdit();
                matid = dataGridView2.Rows[i].Cells["物料编号"].Value.ToString();
                if (matid == "") continue;
                num = dataGridView2.Rows[i].Cells["数量"].Value.ToString();
                Sql = "insert into T_MatAssemblyChild(sysid,matid,num) values ('" + sysid + "','" + matid + "','" + num + "') ";
                SqlLst.Add(Sql);
            }
            try
            {
                (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowThisDedail();
            }
            catch
            {
                MessageBox.Show("当前存在与该物料编码不对应的物料名,请检查后再保存！", "提示");
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == Keys.Enter) && (dataGridView2.Columns[dataGridView2.CurrentCell.ColumnIndex].HeaderText == "物料编号"))
            {
                //先结束编辑状态,否则获取不了当前输入的值
                this.dataGridView2.EndEdit();
                string LikeNameorID = "";
                int currentRowIndex = dataGridView2.SelectedCells[0].RowIndex;
                //获取当前单元格的值
                LikeNameorID = dataGridView2.CurrentCell.Value.ToString();
                string sql = "select Matid 物料编号,MatName 物料名称 from T_MatInf " +
                            "where Matid like '%" + LikeNameorID + "%' or MatName like '%" + LikeNameorID + "%'";
                DataTable dt = (new SqlDBConnect()).Get_Dt(sql);
                FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                fr.unVisible_Column_index_ = -1;
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    dataGridView2.Rows[currentRowIndex].Cells["配套编号"].Value = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString();
                    dataGridView2.Rows[currentRowIndex].Cells["物料编号"].Value = fr.dr_.Cells["物料编号"].Value.ToString();
                    dataGridView2.Rows[currentRowIndex].Cells["物料名称"].Value = fr.dr_.Cells["物料名称"].Value.ToString();
                }
            }
            //继续原来base.ProcessCmdKey中的处理
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //是否允许删除选中行
            this.dataGridView2 .AllowUserToDeleteRows = true;
            //执行删除代码
            foreach (DataGridViewRow r in dataGridView2.SelectedRows)
            {
                dataGridView2.Rows.Remove(r); 
            }
        }

        private void 删除子表行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton5_Click(sender, e);
        }

        #region 限制只输入数字
        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox tb = e.Control as TextBox;
            if (this.dataGridView2.CurrentCell.OwningColumn.HeaderText == "数量")
                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            else
                tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            //8为删除键
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        #endregion

    }
}
