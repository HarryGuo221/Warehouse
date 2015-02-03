using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Sys
{
    public partial class FormChgPsw : Form
    {
        public string UserId;  //传入的用户ID
        public FormChgPsw()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormChgPsw_Load(object sender, EventArgs e)
        {
            this.s_userid.Text = this.UserId;
            username.Text =(new DBUtil()).Get_Single_val("T_users", "Username", "userid", this.UserId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            if (s_password.Text != tpassword.Text)
            {
                MessageBox.Show("两次输入的密码不一致，请重新输入！");
                s_password.Text = "";
                tpassword.Text = "";
                s_password.Focus();
                return;
            }
            string swhere = " where userid='" + this.UserId + "'";
            sql_ = (new InitFuncs()).Build_Update_Sql(panel1, "T_Users",swhere);
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("修改密码失败！");
            }
        }
    }
}
