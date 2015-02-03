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

namespace Warehouse.Sys
{
    public partial class SelItemsForm : Form
    {
        public SelItemsForm()
        {
            InitializeComponent();
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 插入基础选项ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 删除基础选项ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDBConnect cn = new SqlDBConnect();
            string sq = "";
            for (int i = 0; i < textBox2.Lines.Count(); i++)
            {
                sq = "select T_SelItems values(" + textBox1.Text + textBox2.Lines[i] + ")";

            }
            List<string> list = new List<string>();
            list.Add(sq);
            cn.Exec_Tansaction(list);
        }
    }
}
