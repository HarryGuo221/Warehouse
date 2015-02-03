using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Warehouse.Bargain
{
    public partial class FormSelectMonths : Form
    {
        public string month_ = "";
        public FormSelectMonths()
        {
            InitializeComponent();
        }

        private void FormSelectMonths_Load(object sender, EventArgs e)
        {
            
            string s = month_+",";
            string s1;
            int pos_=-1;
            while (s.IndexOf(",")!=-1)
             {
                 pos_=s.IndexOf(",");
                 s1 = s.Substring(0, pos_).Trim();
                 if (s1 == "1") checkBox1.Checked = true;
                 if (s1 == "2") checkBox2.Checked = true;
                 if (s1 == "3") checkBox3.Checked = true;
                 if (s1 == "4") checkBox4.Checked = true;
                 if (s1 == "5") checkBox5.Checked = true;
                 if (s1 == "6") checkBox6.Checked = true;
                 if (s1 == "7") checkBox7.Checked = true;
                 if (s1 == "8") checkBox8.Checked = true;
                 if (s1 == "9") checkBox9.Checked = true;
                 if (s1 == "10") checkBox10.Checked = true;
                 if (s1 == "11") checkBox11.Checked = true;
                 if (s1 == "12") checkBox12.Checked = true;
                 s = s.Substring(pos_+ 1, s.Length-pos_-1);
             }

             

        }

        private void button1_Click(object sender, EventArgs e)
        {
            month_ = "";
            string s="";
            for (int i = this.Controls.Count-1; i>=0; i--)
            {
                object o = this.Controls[i];
                if (o is CheckBox)
                { 
                    if (((CheckBox)o).Checked)
                      {
                          s=((CheckBox)o).Name.ToString().Trim();
                          s=s.Substring(8,s.Length-8);
                          if (month_=="")
                              month_=s;
                          else
                              month_=month_+","+s;
                      }
                }
            }
               this.DialogResult=DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
