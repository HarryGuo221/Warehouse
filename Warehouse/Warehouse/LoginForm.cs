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

namespace Warehouse
{
    public partial class LoginForm : Form
    {
        private bool ynDownLoad = false;//打开更新功能

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            string ver_ = "", vernew_ = "";            
            Util of = new Util();
            of.GetInf(ref SqlDBConnect.ip_, ref SqlDBConnect.db_, ref SqlDBConnect.db_jitoa, ref ver_);

            //vernew_ = of.get_version();

            //this.dateTimePicker1.Value = DBUtil.getServerTime();
            //登录默认工作月为 上次结转的下一个月
            //string maxBalanceTime = StockStatusDAO.GetBalanceTime();
            //this.dateTimePicker1.Value = Convert.ToDateTime(maxBalanceTime.Substring(0, 4) + "-" + maxBalanceTime.Substring(4, 2));
            //如果客户端程序不是最新
            if (ynDownLoad)
            {
                DateTime d1, d2;
                d1 = Convert.ToDateTime(ver_);
                d2 = Convert.ToDateTime(vernew_);
                if ((d1.Year != d2.Year) || (d1.Month != d2.Month) ||
                    (d1.Day != d2.Day) || (d1.Hour != d2.Hour) ||
                    (d1.Minute != d2.Minute) || (d1.Second != d2.Second)
                    )
                //if (ver_ != vernew_)
                {
                     System.Diagnostics.Process.Start("UpdateExe.exe");
                    Application.Exit();
                }
            }
        }
        /// <summary>
        /// 获得登录用户名
        /// </summary>
        /// <returns></returns>
        public string getUserId()
        {
            return this.textBoxUserName.Text.Trim();             
        }
        public string getCurWorkMonth()
        {
            string syear = this.dateTimePicker1.Value.Year.ToString();
            string smonth = this.dateTimePicker1.Value.Month.ToString();
            if (smonth.Length == 1) smonth = "0" + smonth;
            return syear + smonth;            
        }
        /// <summary>
        /// 获得登录密码
        /// </summary>
        /// <returns></returns>
        public string getPassword()
        {
            return this.textBoxPassword.Text.Trim(); 
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //验证工作月是否已经结存

            string strUserName = this.textBoxUserName.Text.Trim();
            string strPassword = this.textBoxPassword.Text.Trim();

            if (this.textBoxUserName.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户名后，再登录！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DBUtil dbUtil = new DBUtil();
            string strSql = "select * from T_Users where UserId='" + this.textBoxUserName.Text.Trim() + "'";
            //check entered information whether exist or not
            bool isExist = dbUtil.yn_exist_data(strSql); 
            if (isExist)
            {
                strPassword = Util.GetMD5str(strPassword);
                //此用户存在 entered information exists
                string strSql_ = "select * from T_Users where UserId='{0}' and PassWord='{1}'";
                strSql_ = string.Format(strSql_, strUserName, strPassword/*Util.GetMD5str(strPassword)*/);
                bool isExist_ = dbUtil.yn_exist_data(strSql_);

                if (isExist_)
                {
                    //合法用户,查找到该用户 legitimate user
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("密码不正确，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }            
            else
            {
                MessageBox.Show("此用户不存在，请重新输入用户名！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBoxUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(sender as TextBox, true, true, false, true);
            }
        }

        private void textBoxUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
         
        }

        private void textBoxUserName_Enter(object sender, EventArgs e)
        {  
          
        }

    }
}
