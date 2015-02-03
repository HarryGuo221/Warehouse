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
    public partial class FormCustAdressadd : Form
    {
        public FormCustAdressadd()
        {
            InitializeComponent();
        }
        public delegate void CustomerAddFormChange();
        public event CustomerAddFormChange customeraddFormChange;

        public string custid;   //客户ID
        public string sysid_;   //系统编号
        public string type;   
        private void button1_Click(object sender, EventArgs e)
        {
            if (Util.ControlTextIsNUll(this.s_Addr))
            {
                MessageBox.Show("联系人地址不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Addr.Focus();
                return;
            }
            //数据处理
            InitFuncs inf = new InitFuncs();
            DBUtil dbUtil = new DBUtil();
            string sqlexe = "";
            string sql_ = "";

            if (type == "add")
            {
                bool isExist = false;
                sql_ = "select Addr from T_CustomerAdd where Custid='" + this.custid + "' and Addr='" + this.s_Addr.Text.Trim() + "'";
                isExist = dbUtil.yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该地址已存在！");
                    return;
                }
                sqlexe = inf.Build_Insert_Sql(this.panel1, "T_CustomerAdd");
            }
            else
            {
                bool isExist = false;
                sql_ = "select Addr from T_CustomerAdd where Custid='" + this.custid + "' and Addr='" + this.s_Addr.Text.Trim() + "' and sysid!=" + this.sysid_;
                isExist = dbUtil.yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该地址已存在！");
                    return;
                }
                string swhere = " where custid='" + this.custid + "' and sysid=" + this.sysid_;
                sqlexe = inf.Build_Update_Sql(this.panel1, "T_CustomerAdd", swhere);
            }
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sqlexe);
                MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                customeraddFormChange();
            }
            catch (Exception w)
            {
                MessageBox.Show(w.ToString());
            }
        }

        private void FormCustAdressadd_Load(object sender, EventArgs e)
        {
            InitFuncs inf = new InitFuncs();
            inf.InitComboBox(this.s_AddType, "[地址]地址种类");

            s_CustID.Text = this.custid;
            if (this.type == "edit")
            {
                string strSql = "select * from T_CustomerAdd where CustID='{0}' and sysid={1}";
                strSql = string.Format(strSql, this.custid, this.sysid_);
                (new InitFuncs()).ShowDatas(this.panel1, strSql);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormCustAdressadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
