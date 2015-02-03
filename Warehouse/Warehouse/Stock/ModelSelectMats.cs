using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Stock
{
    public partial class ModelSelectMats : Form
    {
        public string Model_ = "";

        public delegate void MatsSelect();
        public event MatsSelect MatsSelect_;

        string BasicSql = "select MatId as 物料编码,"
            + "matName as 物料名称,"
            + "ProductType as 产品种类,"
            + "Speed as 速度等级,"
            + "ColorType as 色彩,"
            + "Format as 幅面,"
            + "Specifications as 规格型号,"
            + "PinYinCode as 拼音助记码 "
            + " from T_matInf ";
        string WhereTj = "";
        public ModelSelectMats()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModelSelectMats_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string swhere = " where T_MatInf.Matid like '%{0}%'"
                    + " or T_MatInf.MatName like '%{1}%' "
                    + " or T_MatInf.PinYinCode like '%{2}%'";
                swhere = string.Format(swhere, textBoxName, textBoxName, textBoxName);
                this.WhereTj = swhere;
                show_mats();
            }
        }

        private void show_mats()
        {
            string sql_ = this.BasicSql + this.WhereTj;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_mats.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lx_, mat_;
            if (this.radioButton1.Checked)
                lx_ = "耗材";
            else
                lx_ = "选购件";
            string sql_ = "";
            bool isexist = false;
            for (int k = 0; k < this.dgv_mats.SelectedRows.Count; k++)
            {
                mat_ = this.dgv_mats.SelectedRows[k].Cells["物料编码"].Value.ToString().Trim();
                if (mat_ != "")
                { 
                  sql_="select MatId from T_ModelMats "
                      +"where ModelName='"+this.Model_+"' "
                      +"and stype='"+lx_+"' "
                      +"and MatId='"+mat_+"' ";
                  isexist = (new DBUtil()).yn_exist_data(sql_);
                  if (!isexist)
                  {
                      sql_ = "insert into T_ModelMats(ModelName,stype,MatId)"
                          + " values ('{0}','{1}','{2}')";
                      sql_ = string.Format(sql_, this.Model_, lx_, mat_);
                      try
                      {
                          (new SqlDBConnect()).ExecuteNonQuery(sql_);
                      }
                      catch
                      { }
                  }
                
                }
            }
            MatsSelect_();
            this.dgv_mats.ClearSelection();
        }
    }
}
