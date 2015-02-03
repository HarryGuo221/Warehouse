using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;


namespace Warehouse.Customer
{
    public partial class FormSetCustRela : Form
    {
        public string custid;  //传入的当前客户ID
        private string custName;

        public FormSetCustRela()
        {
            InitializeComponent();
        }

        private void FormSetCustRela_Load(object sender, EventArgs e)
        {
             label1.Text = "";
             custName = (new DBUtil()).Get_Single_val("T_CustomerInf", "custName", "custid",this.custid);
             label1.Text = "客户编码:" + custid + "   客户名称:" + custName;
             show_dgv();
        }

        private void show_dgv()
        {
            string sql_ = "select CustId as 客户编码,CustName as 客户名称 from T_customerInf "
                   + " where ((Custid in (select distinct custid1 from T_CustBrotherRela where custid2='" + this.custid + "') )"
                   + " or (Custid in (select distinct custid2 from T_CustBrotherRela where custid1='" + this.custid + "')))";
                   //+ " and CustId!='" + this.custid + "'";
             DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_brother.DataSource = dt.DefaultView;
            this.dgv_brother.Columns[0].Width = 120;
            this.dgv_brother.Columns[1].Width = 340;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string strSql = "select CustID as 客户编码,CustName as 客户名称,"
                                + "PinYinCode as 助记符,communicateAddr as 通信地址, "
                                + "Degree as 客户等级 " +
                                "from T_CustomerInf where CustID like '%{0}%' or CustName like '%{1}%' or PinYinCode like '%{2}%'";
                strSql = string.Format(strSql, textBoxName, textBoxName, textBoxName);
                InitFuncs.FindInfoToControl(textBox, strSql, "客户名称");

            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.t_CustCode.Text.Trim() == "") return;
            if (!InitFuncs.isRightValue(t_CustCode, "T_customerinf", "custName"))
            {
                MessageBox.Show("该客户名称不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.t_CustCode.Focus();
                return;
            }
            string bcustid_= (new DBUtil()).Get_Single_val("T_CustomerInf", "custid", "custname", t_CustCode.Text.Trim());
            string sql_ = "select Custid1 from T_CustBrotherRela where "
                + "(Custid1='" + bcustid_ + "' and Custid2='" + this.custid + "')"
                +" or (Custid2='" + bcustid_ + "' and Custid1='" + this.custid + "')";
            bool isexist = (new DBUtil()).yn_exist_data(sql_);
            if (isexist)
            {
                MessageBox.Show("该关系已经存在!");
                return;
            }
            else
            {
                sql_ = "insert into T_CustBrotherRela values('"
                     + bcustid_ + "','" + this.custid + "')";
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(sql_);
                    show_dgv();
                }
                catch
                { }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv_brother.SelectedRows.Count <= 0) return;
            if (dgv_brother.SelectedRows[0].Cells["客户编码"].Value == null) return;
            if (dgv_brother.SelectedRows[0].Cells["客户编码"].Value.ToString().Trim() == null) return;

            if (DialogResult.Yes == MessageBox.Show("确定移除客户间的绑定关系吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                string bcustid_ = dgv_brother.SelectedRows[0].Cells["客户编码"].Value.ToString().Trim();
                string sql_ = "delete from T_CustBrotherRela where "
                    + "(Custid1='" + bcustid_ + "' and Custid2='" + this.custid + "')"
                    + " or (Custid2='" + bcustid_ + "' and Custid1='" + this.custid + "')";
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(sql_);
                    show_dgv();
                }
                catch
                { }
            }
        }


       
    }
}
