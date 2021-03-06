﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Bargain
{
    public partial class FormAddCopySettlePrePay : Form
    {
        public string type;  //新增 add ,编辑 edit
        public string sysid;   //该表的主健，编辑用

        public string cmsysid;  //客户机器对应编号
        public string barid;    //合同编号
        public string hctype;   //幅面

        public int hsxh;      //第几次核算
        public int czxh;      //第几次抄张
        public DateTime StartDay;  //抄张起始日
        public DateTime EndDay;  //抄张起始日
 

        public FormAddCopySettlePrePay()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAddCopySettle_Load(object sender, EventArgs e)
        {
            string sql_ = "";
            if (type == "add")
            {
                n_cmsysid.Text = this.cmsysid;
                //初始化机器信息
                sql_ = "select CustID,Mtype,Manufactcode from T_CustomerMac "
                    + " where SysID=" + this.cmsysid;
                DataTable dt;
                dt = (new SqlDBConnect()).Get_Dt(sql_);
                if (dt.Rows.Count >= 0)
                {
                    s_custid.Text = dt.Rows[0]["CustId"].ToString().Trim();
                    s_mtype.Text = dt.Rows[0]["Mtype"].ToString().Trim();
                    s_Manufactcode.Text = dt.Rows[0]["Manufactcode"].ToString().Trim();
                }

                s_bargid.Text = this.barid;
                s_hctype.Text = this.hctype;

                //初始化抄张信息
                n_hsxh.Text = this.hsxh.ToString().Trim();
                n_czxh.Text = this.czxh.ToString().Trim();
                s_czfrom.Text = this.StartDay.ToShortDateString();
                s_czto.Text = this.EndDay.ToShortDateString();
            }
            else
            {
                sql_ = "select * from T_MacSettle where Sysid=" + this.sysid;
                (new InitFuncs()).ShowDatas(this.panel1, sql_);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            string swhere = "";
            if (s_moneyType.SelectedIndex <= 0)
            {
                MessageBox.Show("请设定费用类型");
                return;
            }
            if (this.type == "add")
            {
                sql_ = "select sysid from T_RecordCopy where mcsysid=" + this.cmsysid
                + " and Hsxh=" + this.hsxh + " and czxh=" + this.czxh;
                bool isExist = (new DBUtil()).yn_exist_data(sql_);
                if (!isExist)
                {
                    MessageBox.Show("还未录入该抄张周期的张数");
                    return;
                }
                sql_ = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_MacSettle");
            }
            else
            {
                swhere = "where sysid=" + this.sysid;
                sql_ = (new InitFuncs()).Build_Update_Sql(this.panel1,"T_MacSettle",swhere);
            }

            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
                this.DialogResult = DialogResult.OK;
            }
            catch
            { }

        }

        private void n_Fee_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int n_ = Convert.ToInt32(this.n_Num.Text);
                double f_ = Convert.ToDouble(this.n_Fee.Text);
                this.n_TotMoney.Text = (n_ * f_).ToString().Trim();
            }
            catch
            {
               
            }
        }

        
    }
}
