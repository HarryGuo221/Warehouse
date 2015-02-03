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

namespace Warehouse
{
    public partial class FilterInfoForm : Form
    {
        private DataTable dt;
        private Control control;
        private DataGridViewCell cell;
        private string fieldName = "";
        private string fieldName1 = "";
       
        public FilterInfoForm(Control control, DataTable dt, string fieldName)
        {
            InitializeComponent();
            this.dt = dt;
            this.control = control;
            this.fieldName = fieldName;
        }
        public FilterInfoForm(Control control, DataTable dt, string fieldName, string fieldName1)
        {
            InitializeComponent();
            this.dt = dt;
            this.control = control;
            this.fieldName = fieldName;
            this.fieldName1 = fieldName1;
        }        

        private void FilterInfoForm_Load(object sender, EventArgs e)
        {
            (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);

            if (this.dataGridView1.Rows.Count > 0)
            {
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Rows[0].Selected = true;
            }
        }
        
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0 || dataGridView1.SelectedRows[0].Cells[fieldName].Value == null)
                return;
            if (control != null)
            {
                if (this.fieldName1 != "")
                    control.Text = dataGridView1.SelectedRows[0].Cells[fieldName1].Value.ToString().Trim() +"/"+ dataGridView1.SelectedRows[0].Cells[fieldName].Value.ToString().Trim();
                else
                    control.Text = dataGridView1.SelectedRows[0].Cells[fieldName].Value.ToString().Trim();
            }
            
            this.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1_CellDoubleClick(null, null);
            }
        }
    }
}
