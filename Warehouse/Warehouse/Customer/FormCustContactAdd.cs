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
    public partial class FormCustContactAdd : Form
    {
        public string custid;   //客户ID
        public string sysid_;   //系统编号
        public string type;   

        public FormCustContactAdd()
        {
            InitializeComponent();
            
        }
        public delegate void CustomerContactFormChange();
        public event CustomerContactFormChange customerContactFormChange;

        private void FormCustContactAdd_Load(object sender, EventArgs e)
        {
            InitFuncs inf = new InitFuncs();
            inf.InitComboBox(this.s_CType, "[客户]联系人类别");

            s_custId.Text = this.custid;
            if (this.type == "edit")
            {
                string strSql = "select * from T_CustContacts where CustID='{0}' and sysid={1}";
                strSql = string.Format(strSql, this.custid, this.sysid_);
                (new InitFuncs()).ShowDatas(this.panel1, strSql);

                if (s_Sex.Text.Trim() == "男")
                    t_nan.Checked = true;
                else
                    t_nu.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region 输入验证
            
            if (Util.ControlTextIsNUll(this.s_CName))
            {
                MessageBox.Show("联系人姓名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_CName.Focus();
                return;
            }
            if (this.s_Tel.Text.Trim() != "" && Util.IsNumberic(this.s_Tel) == false)
            {
                MessageBox.Show("输入电话错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Tel.Focus();
                return;
            }
            #endregion

            //数据处理
            InitFuncs inf = new InitFuncs();
            DBUtil dbUtil = new DBUtil();
            string sqlexe = "";
            string sql_ = "";
            

                if (type == "add")
                {
                    bool isExist = false;
                    sql_ = "select CName from T_CustContacts where Custid='" + this.custid + "' and Cname='" + this.s_CName.Text.Trim() + "'";
                    isExist = dbUtil.yn_exist_data(sql_);
                    if (isExist)
                    {
                        MessageBox.Show("该联系人已存在！");
                        return;
                    }
                    sqlexe = inf.Build_Insert_Sql(this.panel1, "T_CustContacts");
                }
                else
                {
                    bool isExist = false;
                    sql_ = "select CName from T_CustContacts where Custid='" + this.custid + "' and Cname='" + this.s_CName.Text.Trim() + "' and sysid!="+this.sysid_;
                    isExist = dbUtil.yn_exist_data(sql_);
                    if (isExist)
                    {
                        MessageBox.Show("该联系人已存在！");
                        return;
                    }
                    string swhere = " where custid='" + this.custid + "' and sysid=" + this.sysid_ ;
                    sqlexe = inf.Build_Update_Sql(this.panel1, "T_CustContacts", swhere);
                }

            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sqlexe);
                MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                customerContactFormChange();
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

        private void t_nan_CheckedChanged(object sender, EventArgs e)
        {
            if (t_nan.Checked)
                s_Sex.Text = "男";
            else
                s_Sex.Text = "女";
        }

        private void FormCustContactAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
