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

namespace Warehouse.Sys
{
    public partial class FormChgSpecialData : Form
    {
        public string chgItems;  //修改内容描述
        public string curUser;   //当前操作者

        private int js = 0;  //记数器，超过3次退出

        public FormChgSpecialData()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //判断授权人密码是否正确，并有特殊权限
            string usrid_ = this.t_Superopa.Text.Trim();
            string pwd_ = this.txtpwd.Text;
            string strSql_ = "select * from T_Users where Hasspecial=1 and UserId='{0}' and PassWord='{1}'";
            strSql_ = string.Format(strSql_, usrid_, Util.GetMD5str(pwd_));
            bool isExist_ = (new DBUtil()).yn_exist_data(strSql_);
            if (!isExist_)
            {
                MessageBox.Show("授权人不存在，或密码错误！");
                js = js + 1;
                if (js == 3)   //3次输入错误，则关闭窗口
                  this.Close();
                else
                   return;
            }
            else
            {
                s_Superopa.Text = (new DBUtil()).Get_Single_val("T_users", "username", "userid", this.t_Superopa.Text.Trim());
                strSql_ = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_SpecOpaHist");
                try
                 { 
                    (new SqlDBConnect()).ExecuteNonQuery(strSql_);
                    this.DialogResult = DialogResult.OK;
                 }
                catch
                {}
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormChgSpecialData_Load(object sender, EventArgs e)
        {
            this.s_OccurTime.Text = DBUtil.getServerTime().ToString();
            this.s_ChgItems.Text = this.chgItems;
            this.s_Curopa.Text = this.curUser;
            this.s_Superopa.Focus();
        }
    }
}
