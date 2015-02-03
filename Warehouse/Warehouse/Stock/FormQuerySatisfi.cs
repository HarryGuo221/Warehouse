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
    public partial class FormQuerySatisfi : Form
    {
        public string wssysid = "";

        public FormQuerySatisfi()
        {
            InitializeComponent();
        }
        
       
        private void FormQuerySatisfi_Load(object sender, EventArgs e)
        {
            string sql_ = "select * from t_worksheet where sysid=" + this.wssysid;
            (new InitFuncs()).ShowDatas(this.panelQuery, sql_);
            int fz1=0,fz2=0,fz3=0,fz4=0,fz5=0;

            if (this.n_attemper.Text.Trim() == "")
                comboBox1.SelectedIndex = -1;
            else
            {
               fz1 = Convert.ToInt16(this.n_attemper.Text.Trim());
               comboBox1.SelectedIndex = fz1 / 5;
            }

            if (this.n_Arrive_speed.Text.Trim() == "")
                comboBox2.SelectedIndex = -1;
            else
            {
                fz2 = Convert.ToInt16(this.n_Arrive_speed.Text.Trim());
                comboBox2.SelectedIndex = fz2 / 5;
            }


            if (this.n_Tech_manner.Text.Trim() == "")
                comboBox3.SelectedIndex = -1;
            else
            {
                fz3 = Convert.ToInt16(this.n_Tech_manner.Text.Trim());
                comboBox3.SelectedIndex = fz3 / 5;
            }


            if (this.n_oper_prompt.Text.Trim() == "")
                comboBox4.SelectedIndex = -1;
            else
            {
                fz4 = Convert.ToInt16(this.n_oper_prompt.Text.Trim());
                comboBox4.SelectedIndex = fz4 / 5;
            }

            if (this.n_Content_explain.Text.Trim() == "")
                comboBox5.SelectedIndex = -1;
            else
            {
                fz5 = Convert.ToInt16(this.n_Content_explain.Text.Trim());
                comboBox5.SelectedIndex = fz5 / 5;
            }

            if (this.s_sfsl.Text.Trim() == "0")
                checkbox1.Checked = false;
            else
                checkbox1.Checked = true;

            this.tb_tot.Text = (fz1 + fz2 + fz3 + fz4 + fz5).ToString().Trim();
        }

        private void Querysave_Click(object sender, EventArgs e)
        {
            string swhere = " where sysid=" + this.wssysid;
            string sql_=(new InitFuncs()).Build_Update_Sql(this.panelQuery, "T_worksheet", swhere);
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
                this.DialogResult = DialogResult.OK;
            }
            catch
            {

            }
        }

        private void FormQuerySatisfi_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                n_attemper.Text = (comboBox1.SelectedIndex * 5).ToString().Trim();
            }
            else
                n_attemper.Text = "";
           
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex != -1)
            {
                n_Tech_manner.Text = (comboBox3.SelectedIndex * 5).ToString().Trim();
            }
            else
                n_Tech_manner.Text = "";
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex != -1)
            {
                n_Content_explain.Text = (comboBox5.SelectedIndex * 5).ToString().Trim();
            }
            else
                n_Content_explain.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                n_Arrive_speed.Text = (comboBox2.SelectedIndex * 5).ToString().Trim();
            }
            else
                n_Arrive_speed.Text = "";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex != -1)
            {
                n_oper_prompt.Text = (comboBox4.SelectedIndex * 5).ToString().Trim();
            }
            else
                n_oper_prompt.Text = "";
        }

        private void checkbox1_Click(object sender, EventArgs e)
        {
            if (checkbox1.Checked)
                s_sfsl.Text = "1";
            else
                s_sfsl.Text = "0";
        }

        private void Queryclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
