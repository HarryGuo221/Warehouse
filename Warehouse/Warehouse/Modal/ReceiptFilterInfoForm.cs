using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Base;

namespace Warehouse.Modal
{
    public partial class ReceiptFilterInfoForm : Form
    {
        private DataTable dt;        
        private DataGridViewCell cell;
        private string fieldName;
        private string SStorehouseId = "";
        public int matType;

        public ReceiptFilterInfoForm(DataGridViewCell cell, DataTable dt, string fieldName, string SStorehouseId, int matType)
        {
            InitializeComponent();
            this.dt = dt;
            this.cell = cell;
            this.fieldName = fieldName;
            this.SStorehouseId = SStorehouseId;
            this.matType = matType;
        }

        private void ReceiptFilterInfoForm_Load(object sender, EventArgs e)
        {
            InitDataGridView();
        }

        /// <summary>
        /// 显示 类型、库存
        /// </summary>
        private void InitDataGridView()
        {
            if (dt.Rows.Count <= 0)
                return;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ReadOnly = true;
            
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string matId = dr["商品编号"].ToString().Trim();
                string matName = dr["商品名称"].ToString().Trim();
                string prefix = matId.Substring(0, 1);
                if (prefix == "0" || prefix == "1")
                {
                    this.dataGridView1.Rows.Add(5);
                    this.dataGridView1.Rows[i].Cells["商品编号"].Value = matId;
                    this.dataGridView1.Rows[i].Cells["商品名称"].Value = matName;
                    this.dataGridView1.Rows[i].Cells["类型"].Value = "0新机";
                    //this.dataGridView1.Rows[i].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, 0).stockNum;
                    i++;
                    this.dataGridView1.Rows[i].Cells["商品编号"].Value = matId;
                    this.dataGridView1.Rows[i].Cells["商品名称"].Value = matName;
                    this.dataGridView1.Rows[i].Cells["类型"].Value = "1旧机";
                    //this.dataGridView1.Rows[i].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, 1).stockNum;
                    i++;
                    this.dataGridView1.Rows[i].Cells["商品编号"].Value = matId;
                    this.dataGridView1.Rows[i].Cells["商品名称"].Value = matName;
                    this.dataGridView1.Rows[i].Cells["类型"].Value = "2样机";
                    //this.dataGridView1.Rows[i].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, 2).stockNum;
                    i++;
                    this.dataGridView1.Rows[i].Cells["商品编号"].Value = matId;
                    this.dataGridView1.Rows[i].Cells["商品名称"].Value = matName;
                    this.dataGridView1.Rows[i].Cells["类型"].Value = "3固新机";
                    //this.dataGridView1.Rows[i].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, 3).stockNum;
                    i++;
                    this.dataGridView1.Rows[i].Cells["商品编号"].Value = matId;
                    this.dataGridView1.Rows[i].Cells["商品名称"].Value = matName;
                    this.dataGridView1.Rows[i].Cells["类型"].Value = "4固旧机";
                    //this.dataGridView1.Rows[i].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, 4).stockNum;
                    i++;
                }
                else if (prefix == "6")
                {
                    this.dataGridView1.Rows.Add(2);
                    this.dataGridView1.Rows[i].Cells["商品编号"].Value = matId;
                    this.dataGridView1.Rows[i].Cells["商品名称"].Value = matName;
                    this.dataGridView1.Rows[i].Cells["类型"].Value = "0新机";
                    //this.dataGridView1.Rows[i].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, 0).stockNum;
                    i++;
                    this.dataGridView1.Rows[i].Cells["商品编号"].Value = matId;
                    this.dataGridView1.Rows[i].Cells["商品名称"].Value = matName;
                    this.dataGridView1.Rows[i].Cells["类型"].Value = "1旧机";
                    //this.dataGridView1.Rows[i].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, 1).stockNum;
                    i++;
                }
                else
                {
                    this.dataGridView1.Rows.Add(1);
                    this.dataGridView1.Rows[i].Cells["商品编号"].Value = matId;
                    this.dataGridView1.Rows[i].Cells["商品名称"].Value = matName;
                    this.dataGridView1.Rows[i].Cells["类型"].Value = "0新机";
                    //this.dataGridView1.Rows[i].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, 0).stockNum;
                    i++;
                }
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0 || dataGridView1.SelectedRows[0].Cells[fieldName].Value == null)
                return;
            if (cell != null)
            {
                cell.Value = dataGridView1.SelectedRows[0].Cells[fieldName].Value.ToString().Trim();
                this.matType = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["类型"].Value.ToString().Trim().Substring(0, 1));
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1_CellDoubleClick(null, null);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0 || dataGridView1.SelectedRows[0].Cells[fieldName].Value == null)
                return;

            string matId = dataGridView1.SelectedRows[0].Cells["商品编号"].Value.ToString().Trim();
            int matType = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["类型"].Value.ToString().Trim().Substring(0, 1));

            dataGridView1.SelectedRows[0].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, matType).stockNum;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (dataGridView1.Rows[index].Cells["商品编号"].Value == null || dataGridView1.Rows[index].Cells["商品编号"].Value.ToString().Trim() == "")
                return;
            
            if (dataGridView1.Rows[index].Cells["库存"].Value == null || dataGridView1.Rows[index].Cells["库存"].Value.ToString().Trim() == "")
            {
                string matId = dataGridView1.Rows[index].Cells["商品编号"].Value.ToString().Trim();
                int matType = Convert.ToInt32(dataGridView1.Rows[index].Cells["类型"].Value.ToString().Trim().Substring(0, 1));

                dataGridView1.Rows[index].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, matType).stockNum;
            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //int index = 0;
            //if (dataGridView1.CurrentRow != null)
            //    index = dataGridView1.CurrentRow.Index;
            //if (dataGridView1.Rows[index].Cells["商品编号"].Value == null || dataGridView1.Rows[index].Cells["商品编号"].Value.ToString().Trim() == "")
            //    return;
            //if (dataGridView1.Rows[index].Cells["库存"].Value == null)
            //    return;

            //if (dataGridView1.Rows[index].Cells["库存"].Value.ToString().Trim() != "")
            //{
            //    string matId = dataGridView1.SelectedRows[0].Cells["商品编号"].Value.ToString().Trim();
            //    int matType = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["类型"].Value.ToString().Trim().Substring(0, 1));

            //    dataGridView1.SelectedRows[0].Cells["库存"].Value = ReceiptModCfg.GetStockNum(this.SStorehouseId, matId, matType).stockNum;
            //}
        }


    }
}
