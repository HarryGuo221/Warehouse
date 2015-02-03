using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Modal;
using Warehouse.Base;
using Warehouse.DB;

namespace Warehouse.Receipt
{
    public partial class VerificateConfigForm : Form
    {
        public DataGridView dataGridview;        
        public string strSql;
        private Form mainForm;
        string receiptId;
        private SortedList<int, double> notVerNums = new SortedList<int, double>();//当前未核销的数量（多行）

        public VerificateConfigForm(Form mainForm, string receiptId)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.receiptId = receiptId;
        }

        private void VerificateConfigForm_Load(object sender, EventArgs e)
        {
            this.panel1.Controls.Add(dataGridview);
            dataGridview.Dock = DockStyle.Fill;
            dataGridview.SelectionMode = DataGridViewSelectionMode.CellSelect;
            foreach (DataGridViewColumn column in dataGridview.Columns)
            {
                if (column.HeaderText == "数量")
                    column.ReadOnly = false;
                else
                    column.ReadOnly = true;
            }

            foreach (DataGridViewRow dgvr in dataGridview.Rows)
            {
                if (dgvr.Cells["数量"].Value != null && dgvr.Cells["数量"].Value.ToString().Trim() != "")
                    notVerNums.Add(dgvr.Index, Convert.ToDouble(dgvr.Cells["数量"].Value.ToString().Trim()));
            }
        }
        /// <summary>
        /// 核销入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerificateIn_Click(object sender, EventArgs e)
        {            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

            try
            {
                this.dataGridview.EndEdit();
                List<MatInfo> matInfos = new List<MatInfo>();
                ListModalForm listModalForm = new ListModalForm("01", "add", this.receiptId, "add_03_01");
                foreach (DataGridViewRow dgvr in dataGridview.Rows)
                {
                    if (dgvr.Cells["单据号"].Value == null || dgvr.Cells["物料编码"].Value == null)
                        continue;
                    string SStoreHId = (new DBUtil()).Get_Single_val("T_Receipt_Main", "SourceStoreH", "ReceiptId", dgvr.Cells["单据号"].Value.ToString().Trim());
                    MatInfo matInfo = new MatInfo();
                    if (dgvr.Cells["物料编码"].Value != null)
                        matInfo.matId = dgvr.Cells["物料编码"].Value.ToString().Trim();
                    if (dgvr.Cells["数量"].Value != null && dgvr.Cells["数量"].Value.ToString().Trim() != "")
                        matInfo.num = Convert.ToDouble(dgvr.Cells["数量"].Value.ToString().Trim());
                    if (dgvr.Cells["单价"].Value != null && dgvr.Cells["单价"].Value.ToString().Trim() != "")
                        matInfo.price = Convert.ToDouble(dgvr.Cells["单价"].Value.ToString().Trim());
                    if (dgvr.Cells["单据号"].Value != null && dgvr.Cells["单据号"].Value.ToString().Trim() != "")
                        matInfo.receiptId = dgvr.Cells["单据号"].Value.ToString().Trim();
                    if (dgvr.Cells["顺序号"].Value != null && dgvr.Cells["顺序号"].Value.ToString().Trim() != "")
                        matInfo.orderNo = Convert.ToInt32(dgvr.Cells["顺序号"].Value.ToString().Trim());
                    if (dgvr.Cells["类型"].Value != null && dgvr.Cells["类型"].Value.ToString().Trim() != "")
                        matInfo.matType = Convert.ToInt32(dgvr.Cells["类型"].Value.ToString().Trim());
                    matInfo.notVerNum = notVerNums[dgvr.Index];//未核销数量（修改前）
                    matInfo.SStoreHId = SStoreHId;

                    matInfos.Add(matInfo);
                }

                listModalForm.MdiParent = mainForm;
                listModalForm.matInfos_03 = matInfos;//
                listModalForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

    }
}
