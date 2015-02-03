using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Base;
using Warehouse.DB;

namespace Warehouse.Stock
{
    public partial class AddModelMat : Form
    {
        public string type = "";  //新增 或 修改
        public string sysid = "";  //修改时传入的sysid
        public string Mmodel = "";
        public string Mtype = "";

        public delegate void MatsSelect();
        public event MatsSelect MatsSelect_;


        public AddModelMat()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddModelMat_Load(object sender, EventArgs e)
        {
            //限制值类型文本框的输入 
            (new InitFuncs()).Num_limited(this.panel1);
            this.s_MatId.KeyDown += new KeyEventHandler(InfoFind.MatId_KeyDown);

            if (this.type == "add")
            {
                this.s_ModelName.Text = this.Mmodel;
                this.s_stype.Text = this.Mtype;
            }
            else
            {
                string sql_ = "select * from t_ModelMats where sysid=" + this.sysid;
                (new InitFuncs()).ShowDatas(this.panel1, sql_);
                this.s_MatId_Leave(null, null);
            }

        }

        private void s_MatId_Leave(object sender, EventArgs e)
        {
            this.t_NatName.Text=(new DBUtil()).Get_Single_val("T_MatInf","MatName","matid",this.s_MatId.Text.Trim());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Util.ControlTextIsNUll(this.s_MatId))
            {
                MessageBox.Show("请选择物料！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_MatId.Focus();
                return;
            }

            if (Util.ControlTextIsNUll(this.s_stype))
            {
                MessageBox.Show("请确定物料类型！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_stype.Focus();
                return;
            }

            string sql_ = "", swhere = "";
            if (this.type == "add")
            {
                sql_ = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_ModelMats");
            }
            else
            {
                swhere = " where sysid=" + this.sysid;
                sql_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_ModelMats", swhere);
            }
            try
            { 
              (new SqlDBConnect()).ExecuteNonQuery(sql_);
              MatsSelect_();
              this.DialogResult=DialogResult.OK;
            }
            catch
            {}
        }
    }
}
