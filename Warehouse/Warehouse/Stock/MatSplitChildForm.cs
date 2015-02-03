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
using Warehouse.Base;

namespace Warehouse.Stock
{
    public partial class MatSplitChildForm : Form
    {
        public delegate void MatSplitChildFormChange();
        /// <summary>
        /// 零件信息改变 事件
        /// </summary>
        public event MatSplitChildFormChange matSplitChildFormChange;
                
        private string strReceiptId;//拆件单据编号
        private string strMainMatID;//被拆物料编号

        public MatSplitChildForm(string strReceiptId, string strMainMatID)
        {
            InitializeComponent();            
            this.strReceiptId = strReceiptId;
            this.strMainMatID = strMainMatID;
        }

        private void MatSplitChildForm_Load(object sender, EventArgs e)
        {
            (new InitFuncs()).InitComboBox(this.comboBoxMatType, "[物料]被拆物料类型");
 
            this.txtReceiptId.Text = this.strReceiptId;
            this.txtMainMatName.Text = this.strMainMatID;

            InitDataGridView();
        }       
        private void InitDataGridView()
        {
            DataTable dt = MatSplitChildDAO.GetDatasByReceiptId(this.strReceiptId);
            (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);

        }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            #region 验证
            if (this.txtReceiptId.Text.Trim() == "")
            {
                MessageBox.Show("拆件单据编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.comboBoxMatType.SelectedIndex == 0 || this.comboBoxMatType.Text.Trim() == "")
            {
                MessageBox.Show("子物料类型不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion

            DBUtil dbUtil = new DBUtil();
            if (this.dataGridView1.SelectedRows.Count <= 0 || this.dataGridView1.SelectedRows[0].Cells["子物料"].Value == null)
            {
                MessageBox.Show("请正确选择一条记录！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string strMatChildName = this.dataGridView1.SelectedRows[0].Cells["子物料"].Value.ToString().Trim();
            if (strMatChildName == "")
            {
                MessageBox.Show("请正确选择一条记录！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string ChildMatID = dbUtil.Get_Single_val("T_MatInf", "MatID", "MatName", strMatChildName);

            string strWhere = "where ReceiptId='{0}' and ChildMatID='{1}'";
            strWhere = string.Format(strWhere, this.txtReceiptId.Text.Trim(), ChildMatID);
            string strSqlUpdate = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_MatSplit_Child", strWhere);

            (new SqlDBConnect()).ExecuteNonQuery(strSqlUpdate);
           
            MessageBox.Show("修改成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            InitDataGridView();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void MatSplitChildForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            matSplitChildFormChange(); //激活代理事件，在MatSplitForm中处理
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0 || this.dataGridView1.SelectedRows[0].Cells["拆件单据编号"].Value == null)
                return;

            string strReceiptId = this.dataGridView1.SelectedRows[0].Cells["拆件单据编号"].Value.ToString().Trim();
            if (strReceiptId == "")
            {
                MessageBox.Show("请选择一条记录！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.n_ChildMatType.Text = this.dataGridView1.SelectedRows[0].Cells["子物料类型"].Value.ToString().Trim();
            if (this.n_ChildMatType.Text.Trim() == "")
                return;
            this.comboBoxMatType.Text = Util.GetMatTypeName(Convert.ToInt32(this.n_ChildMatType.Text.ToString().Trim()));
            this.s_ChildManuCode.Text = this.dataGridView1.SelectedRows[0].Cells["子物料制造编号"].Value.ToString().Trim();
        }

        private void comboBoxMatType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.n_ChildMatType.Text = Util.GetMatType(this.comboBoxMatType.Text.Trim()).ToString();
        }

    }
}
