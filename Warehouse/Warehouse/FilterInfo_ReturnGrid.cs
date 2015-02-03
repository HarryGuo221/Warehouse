using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse
{
    
    public partial class FilterInfo_ReturnGrid : Form
    {
        DataTable dt_;
        public DataGridViewRow dr_;
        public int unVisible_Column_index_=0;  //隐藏哪列,传入
        public FilterInfo_ReturnGrid(DataTable dt)
        {
            InitializeComponent();
            this.dt_ = dt;
        }

        private void FilterInfo_ReturnGrid_Load(object sender, EventArgs e)
        {
            (new InitFuncs()).InitDataGridView(this.dataGridView1, this.dt_);
            if (unVisible_Column_index_ != -1)
            {
                this.dataGridView1.Columns[unVisible_Column_index_].Visible = false;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;
            if (dataGridView1.SelectedRows[0].Cells[0].Value == null) return;
               
            this.dr_ = this.dataGridView1.SelectedRows[0];
            this.DialogResult = DialogResult.OK;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1_DoubleClick(null, null);
            }
        }
    }
}
