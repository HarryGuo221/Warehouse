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

namespace Warehouse.Stock
{
    public partial class EditMatBasicInfo : Form
    {
        public EditMatBasicInfo()
        {
            InitializeComponent();
        }
        private string Type;
        private string matid;
        private string matname;

        public delegate void MatBasicInfoFormChange();
        public event MatBasicInfoFormChange matBasicInfoFormChange;

        public EditMatBasicInfo(string type, string matsysid, string matname)
        {
            InitializeComponent();
            this.Type = type;
            this.matid = matsysid;
            this.matname = matname;
        }

        //#region//初始化datagridview
        //public void initdataGridview()
        //{
        //    DataTable dt;
        //    dt = (new MaterialDAO()).getMatLifeInf(this.matsysid);
        //    (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);
        //}
        //#endregion

       
        private void EditMatBasicInfo_Load(object sender, EventArgs e)
        {
            #region//限制值类型文本框的输入
            foreach (object o in this.panel1.Controls)
            {
                if (o is TextBox)
                {
                    (new InitFuncs()).Num_limited(this.panel1);
                }
                else
                    continue;
            }
            #endregion

            MatNametextBox.Text = this.matname;
            s_MatID.Text = this.matid;
            string sql_ = "select matid from T_MatLifeInf where Matid='" + this.matid + "'";
            bool isexist=(new DBUtil()).yn_exist_data(sql_);
            if (isexist)
            {
                this.Type = "edit";
                string strsql = "select * from T_MatLifeInf where MatID='{0}'";
                strsql = string.Format(strsql, this.matid);
                (new InitFuncs()).ShowDatas(this.panel1, strsql);
            }
            else
                this.Type = "add";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.Type == "add")
            {
                string insert_sql = (new InitFuncs()).Build_Insert_Sql(panel1, "T_MatLifeInf");
                    try
                    {
                        (new SqlDBConnect()).ExecuteNonQuery(insert_sql);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
               
            }
            else if (this.Type == "edit")
            {
                string swhere = " where matid='" + this.matid + "'";
                string updatesql = (new InitFuncs()).Build_Update_Sql(panel1, "T_MatLifeInf", swhere);
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(updatesql);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditMatBasicInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }


    }
        
}
       