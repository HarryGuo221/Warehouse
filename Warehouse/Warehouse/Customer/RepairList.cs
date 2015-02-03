using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DAO;
using Warehouse.DB;
using Warehouse.Stock;


namespace Warehouse.Customer
{
    public partial class RepairList : Form
    {
        public RepairList()
        {
            InitializeComponent();
        }

        void _AddWorkFormChange()
        {
            InitDataGridView("T_WorkSheet");
        }

        
        #region// 初始化DataGridView
        public void InitDataGridView(string Table)
        {
            //记录操作前行号
            int curindex_ = -1;
            if (this.dataGridViewRepair.SelectedRows.Count > 0)
            {
                curindex_ = this.dataGridViewRepair.CurrentRow.Index;
            }

            WorkSheetDAO WD = new WorkSheetDAO();
            try
            {
                DataTable dt = WD.GetDatasOfUsers(Table);
                WD.InitDataGridView(this.dataGridViewRepair, dt);
                if (dataGridViewRepair.SelectedRows.Count > 0)
                {
                    dataGridViewRepair.ClearSelection();
                    this.dataGridViewRepair.CurrentCell = this.dataGridViewRepair.Rows[curindex_].Cells[0];
                    dataGridViewRepair.Rows[curindex_].Selected = true;
                }
            }
            catch
            {
                MessageBox.Show("初始化失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          
        }
        #endregion 

        private void RepairList_Load(object sender, EventArgs e)
        {
            InitDataGridView("T_WorkSheet");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewRepair.SelectedRows.Count > 0)
            {
                string WsCode = this.dataGridViewRepair.SelectedRows[0].Cells["工单编号"].Value.ToString().Trim();
                AddWorkForm awf = new AddWorkForm();
                //awf.wsid_ = WsCode;
                awf.type = "edit";
                awf.addWorkFormChange_ += new AddWorkForm.AddWorkFormChange(_AddWorkFormChange);
                awf.ShowDialog();
            }
          
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddWorkForm awf = new AddWorkForm();
            awf.type = "add";
            awf.addWorkFormChange_ += new AddWorkForm.AddWorkFormChange(_AddWorkFormChange);
            awf.ShowDialog();
        }

    }
}
