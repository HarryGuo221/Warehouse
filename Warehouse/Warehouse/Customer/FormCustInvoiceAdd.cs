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

namespace Warehouse.Customer
{
    public partial class FormCustInvoiceAdd : Form
    {
        public FormCustInvoiceAdd()
        {
            InitializeComponent();
        }
        public delegate void CustomerfpFormChange();
        public event CustomerfpFormChange customerfpFormChange;

        public string custid;   //客户ID
        public string sysid_;   //系统编号
        public string type;   
        private void FormCustInvoiceAdd_Load(object sender, EventArgs e)
        {
            InitFuncs inf = new InitFuncs();
            inf.InitComboBox(this.s_IType, "[发票]发票种类");

            s_CustID.Text = this.custid;
            if (this.type == "edit")
            {
                string strSql = "select * from T_CustomerInvoice where CustID='{0}' and sysid={1}";
                strSql = string.Format(strSql, this.custid, this.sysid_);
                (new InitFuncs()).ShowDatas(this.panel1, strSql);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Util.ControlTextIsNUll(this.s_Ititle))
            {
                MessageBox.Show("发票抬头不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Ititle.Focus();
                return;
            }
            //数据处理
            InitFuncs inf = new InitFuncs();
            DBUtil dbUtil = new DBUtil();
            string sqlexe = "";
            string sql_ = "";

            if (type == "add")
            {
                //bool isExist = false;
                //sql_ = "select Ititle from T_CustomerInvoice where Custid='" + this.custid + "' and Ititle='" + this.s_Ititle.Text.Trim() + "'";
                //isExist = dbUtil.yn_exist_data(sql_);
                //if (isExist)
                //{
                //    MessageBox.Show("该类发票已存在！");
                //    return;
                //}
                sqlexe = inf.Build_Insert_Sql(this.panel1, "T_CustomerInvoice");
            }
            else
            {
                //bool isExist = false;
                //sql_ = "select Ititle from T_CustomerInvoice where Custid='" + this.custid + "' and Ititle='" + this.s_Ititle.Text.Trim() + "' and sysid!=" + this.sysid_;
                //isExist = dbUtil.yn_exist_data(sql_);
                //if (isExist)
                //{
                //    MessageBox.Show("该类发票已存在！");
                //    return;
                //}
                string swhere = " where custid='" + this.custid + "' and sysid=" + this.sysid_;
                sqlexe = inf.Build_Update_Sql(this.panel1, "T_CustomerInvoice", swhere);
            }
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sqlexe);
                MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                customerfpFormChange();
            }
            catch (Exception w)
            {
                MessageBox.Show(w.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormCustInvoiceAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
