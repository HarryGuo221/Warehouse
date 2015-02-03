using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.DAO;

namespace Warehouse.Stock
{
    public partial class MatSplitForm : Form
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        private string userName;

        private int CurRowIndex = 0;

        public MatSplitForm(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }

        private void MatSplitForm_Load(object sender, EventArgs e)
        {
            InitDataGridViewMain();
            InitDataGridViewChild();
        }

        private void InitDataGridViewMain()
        {
            DataTable dt = MatSplitMainDAO.GetDatas();

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dgvMatMain, dt);

            this.dgvMatMain.ClearSelection();
            this.dgvMatMain.Rows[this.CurRowIndex].Selected = true;
        }
        private void InitDataGridViewChild()
        {
            DataTable dt = MatSplitChildDAO.GetDatas();

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dgvMatChild, dt);
        }

        private void dgvMatMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.CurRowIndex = e.RowIndex;
            toolStripButtonEdit_Click(sender, e);
        }
        /// <summary>
        /// 拆分机器
        /// </summary> 
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            MatSplitMainForm form = new MatSplitMainForm("add", "", this.userName);
            form.matSplitMainFormChange += new MatSplitMainForm.MatSplitMainFormChange(form_matSplitMainFormChange);
            form.ShowDialog();
        }

        void form_matSplitMainFormChange()
        {
            InitDataGridViewMain();
        }
        /// <summary>
        /// 机器属性
        /// </summary> 
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (this.dgvMatMain.SelectedRows.Count <= 0)
                return;

            string strReceiptId = this.dgvMatMain.SelectedRows[0].Cells["拆件单据编号"].Value.ToString().Trim();
            if (strReceiptId == "")
            {
                MessageBox.Show("请选择一条记录！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            MatSplitMainForm form = new MatSplitMainForm("edit", strReceiptId,this.userName);
            form.matSplitMainFormChange += new MatSplitMainForm.MatSplitMainFormChange(form_matSplitMainFormChange);
            form.ShowDialog();
        }
        /// <summary>
        /// 删除机器
        /// </summary> 
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvMatMain.SelectedRows.Count <= 0)
                return;

            string strReceiptId = this.dgvMatMain.SelectedRows[0].Cells["拆件单据编号"].Value.ToString().Trim();
            if (strReceiptId == "")
            {
                MessageBox.Show("请选择一条记录！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    MatSplitMainDAO.DeleteByReceiptId(strReceiptId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                InitDataGridViewMain();
            }
        }
        /// <summary>
        /// 拆分零件管理
        /// </summary>       
        private void toolStripButtonChildManage_Click(object sender, EventArgs e)
        {
            if (this.dgvMatMain.SelectedRows.Count <= 0 && this.dgvMatMain.SelectedRows[0].Cells["拆件单据编号"].Value==null)
                return;

            string strReceiptId = this.dgvMatMain.SelectedRows[0].Cells["拆件单据编号"].Value.ToString().Trim();
            if (strReceiptId == "")
            {
                MessageBox.Show("请选择一条记录！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string strMainMatName = this.dgvMatMain.SelectedRows[0].Cells["被拆物料"].Value.ToString().Trim();
            string strMainMatID = (new DBUtil()).Get_Single_val("T_MatInf", "MatID", "MatName", strMainMatName);
            string strManufacCode = this.dgvMatMain.SelectedRows[0].Cells["制造编号"].Value.ToString().Trim();
            //MatSplitChildForm form = new MatSplitChildForm(strReceiptId, strMainMatID);
            //form.matSplitChildFormChange += new MatSplitChildForm.MatSplitChildFormChange(form_matSplitChildFormChange);
            //form.ShowDialog();
            MatSplitCForm form = new MatSplitCForm("add",strReceiptId, strMainMatID,strManufacCode);
            form.matSplitChildFormChange+=new MatSplitCForm.MatSplitChildFormChange(form_matSplitChildFormChange);
            form.ShowDialog();
        }

        void form_matSplitChildFormChange()
        {
            InitDataGridViewChild();
        }

        private void dgvMatMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvMatMain.SelectedRows.Count <= 0)
                return;

            string strReceiptId = this.dgvMatMain.SelectedRows[0].Cells["拆件单据编号"].Value.ToString().Trim();
            if (strReceiptId == "")
            {
                //MessageBox.Show("请选择一条记录！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InitDataGridViewChildTemp(strReceiptId);
        }

        private void InitDataGridViewChildTemp(string strReceiptId)
        {
            DataTable dt = MatSplitChildDAO.GetDatasByReceiptId(strReceiptId);

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dgvMatChild, dt);
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonDelete_Click(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string strReceiptId = this.dgvMatMain.SelectedRows[0].Cells["拆件单据编号"].Value.ToString().Trim();
            string strMainMatName = this.dgvMatMain.SelectedRows[0].Cells["被拆物料"].Value.ToString().Trim();
            string strMatID = (new DBUtil()).Get_Single_val("T_MatInf", "MatID", "MatName", strMainMatName);
                  
            MatRestoreForm form = new MatRestoreForm("add", strReceiptId, strMainMatName);
            form.ShowDialog();
        }

    }
}
