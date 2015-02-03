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

namespace Warehouse.Modal
{
    public partial class ReceiptModalDefineForm : Form
    {
        private int curRowIndex = 0;
        public ReceiptModalDefineForm()
        {
            InitializeComponent();
        }

        private void ReceiptModalDefineForm_Load(object sender, EventArgs e)
        {
            InitDataGridView();
        }

        private void InitDataGridView()
        {
            DataTable dt = ReceiptModalDAO.GetDatas();

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridViewRecMod, dt);

            this.dataGridViewRecMod.ClearSelection();
            this.dataGridViewRecMod.Rows[this.curRowIndex].Selected = true;
        }

        private void dataGridViewRecMod_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.curRowIndex = e.RowIndex;
            toolStripButtonEdit_Click(sender, e);            
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            EditReceiptModalDefineForm form = new EditReceiptModalDefineForm("add", "");
            form.receiptModalDefineChange += new EditReceiptModalDefineForm.ReceiptModalDefineChange(form_receiptModalDefineChange);
            form.ShowDialog();
        }
        /// <summary>
        /// 事件响应处理方法
        /// </summary>
        void form_receiptModalDefineChange()
        {
            InitDataGridView();
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewRecMod.SelectedRows.Count <= 0)
                return;
            if (this.dataGridViewRecMod.SelectedRows[0].Cells["单据模板编号"].Value == null)
                return;

            string receTypeID = this.dataGridViewRecMod.SelectedRows[0].Cells["单据模板编号"].Value.ToString().Trim();

            EditReceiptModalDefineForm form = new EditReceiptModalDefineForm("edit", receTypeID);
            form.receiptModalDefineChange += new EditReceiptModalDefineForm.ReceiptModalDefineChange(form_receiptModalDefineChange);
            form.ShowDialog();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewRecMod.SelectedRows.Count <= 0)
                return;
            if (this.dataGridViewRecMod.SelectedRows[0].Cells["单据模板编号"].Value == null)
                return;

            string receTypeID = this.dataGridViewRecMod.SelectedRows[0].Cells["单据模板编号"].Value.ToString().Trim();

            if (MessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                ReceiptModalDAO.DeleteByReceTypeID(receTypeID);

                InitDataGridView();
            }
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 删除模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonDelete_Click(sender, e);
        }

    }
}
