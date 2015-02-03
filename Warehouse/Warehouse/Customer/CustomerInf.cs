using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DAO;
using Warehouse.DB;
using System.Data.SqlClient;

namespace Warehouse.Customer
{
    public partial class CustomerInf : Form
    {
        //基础SQL语句
        private string BaseSql= "select CustID as 客户编号, CustName as 客户名称, PinYinCode as 拼音助记码,"
                    + " communicateAddr as 通信地址,CustType as 类别,whichTrade as 所在行业,"
                    + "City as 城市地区,BillAddr as 发票寄送地址,"
                    + " DeliveryTitle as 送货抬头,TaxRegistNumber as 税务登记号,"
                    + "AtBank as 开户银行,BankAccount as 银行账号, CredDegree as 信用等级 ,"
                    + "CredDegreeMoney as 信用金额,Area as 管理区号,AccountPeriod as '默认账期(天)',"
                    + "T_CustomerImp.importance as 重要度,Fax as 传真,"
                    + "Email,InputTime as 输入日期 ,Memo as 备注 "
                    + " from T_CustomerInf left join T_CustomerImp on T_CustomerInf.ImportanceDegreeId=T_CustomerImp.Iid "
                    + " left join T_AreaInf on T_CustomerInf.AreaCode =T_AreaInf.Areaid ";
        //筛选条件
        private string WhereTj = "";

        public CustomerInf()
        {
            InitializeComponent();
        }
        public void initDataGridView()
        {
            //记录操作前行号
            int curindex_ = -1;
            if (this.dataGridViewCustmer.SelectedRows.Count > 0)
            {
                curindex_ = this.dataGridViewCustmer.CurrentRow.Index;
            }

            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(this.BaseSql +" "+ this.WhereTj);
            (new InitFuncs()).InitDataGridView(this.dataGridViewCustmer, dt);
            
            this.toolStripStatusLabel3.Text = (this.dataGridViewCustmer.Rows.Count).ToString().Trim();

            this.dataGridViewCustmer.Columns["客户名称"].Width = 270;
            this.dataGridViewCustmer.Columns["通信地址"].Width = 270;
            this.dataGridViewCustmer.Columns["类别"].Width = 70;
            this.dataGridViewCustmer.Columns["开户银行"].Width = 150;
            
            try
            {
                if (dataGridViewCustmer.SelectedRows.Count > 0)
                {
                    dataGridViewCustmer.ClearSelection();
                    this.dataGridViewCustmer.CurrentCell = this.dataGridViewCustmer.Rows[curindex_].Cells[0];
                    dataGridViewCustmer.Rows[curindex_].Selected = true;
                }
             
            }
            catch
            {
            }
        }

        private void CustomerInf_Load(object sender, EventArgs e)
        {
            this.dataGridViewCustmer.AllowUserToAddRows = false;
            //initDataGridView();
        }

        void customerAddForm_customerInfoFormChange()
        {
            //initDataGridView();
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            属性toolStripButton2_Click(sender, e);
        }

        private void 添加客户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            添加客户toolStripButton1_Click(sender, e);
        }

        private void 修改客户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            属性toolStripButton2_Click(sender, e);
        }

        private void 删除客户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            删除toolStripButton3_Click(sender, e);
        }

        private void 客户联系人ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            客户联系人toolStripButton4_Click(sender, e);
        }

        private void 客户关系ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            子客户管理toolStripButton5_Click(sender, e);
        }

        private void 添加客户toolStripButton1_Click(object sender, EventArgs e)
        {
            CustomerInfAdd customeradd = new CustomerInfAdd("add", "");
            customeradd.customerInfoFormChange += new CustomerInfAdd.CustomerInfoFormChange(customerAddForm_customerInfoFormChange);
            customeradd.ShowDialog();
        }

        private void 属性toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewCustmer.SelectedRows.Count > 0)
            {
                if (this.dataGridViewCustmer.SelectedRows[0].Cells["客户编号"].Value == null) return;

                string custid = this.dataGridViewCustmer.SelectedRows[0].Cells["客户编号"].Value.ToString().Trim();
                if (custid == "") return;
                CustomerInfAdd cusadd = new CustomerInfAdd("edit", custid);
                cusadd.customerInfoFormChange += new CustomerInfAdd.CustomerInfoFormChange(customerAddForm_customerInfoFormChange);
                cusadd.ShowDialog();
            }
        }

        private void 删除toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewCustmer.SelectedRows.Count > 0)
            {
                if (this.dataGridViewCustmer.SelectedRows[0].Cells["客户编号"].Value == null) return;

                string custid = this.dataGridViewCustmer.SelectedRows[0].Cells["客户编号"].Value.ToString().Trim();
                if (custid == "") return;
                if (DialogResult.Yes == MessageBox.Show("确定删除选中的客户信息吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    List<string> SqlLst = new List<string>();
                    //删除发票Title
                    string sql_ = "delete from T_CustomerTitle where CustID='" + custid + "'";
                    SqlLst.Add(sql_);
                    //删除联系人
                    sql_ = "delete from T_CustContacts where CustID='" + custid + "'";
                    SqlLst.Add(sql_);
                    //删除客户信息
                    sql_ = "delete from T_CustomerInf where CustID='" + custid + "'";
                    SqlLst.Add(sql_);
                    //删除客户兄弟关系
                    sql_ = "delete from T_CustBrotherRela where custid1='"
                         + custid + "' or custid2='" + custid + "'";
                    SqlLst.Add(sql_);
                    try
                    {
                        (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                        initDataGridView();
                    }
                    catch (Exception w)
                    {
                        MessageBox.Show("该条数据与其他数据存在联系" + '\n' + "不能删除!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }

                }
            }
        }

        private void 客户联系人toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void 子客户管理toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewCustmer.SelectedRows.Count <= 0) return;
            if (this.dataGridViewCustmer.SelectedRows[0].Cells["客户编号"].Value == null) return;
            string custid = this.dataGridViewCustmer.SelectedRows[0].Cells["客户编号"].Value.ToString().Trim();

            Customer_RelaAdd custrela = new Customer_RelaAdd("edit", custid);
            custrela.ShowDialog();
        }



        private void 退出toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewCustmer_DoubleClick(object sender, EventArgs e)
        {
            属性toolStripButton2_Click(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewCustmer.SelectedRows.Count <= 0) return;
            if (this.dataGridViewCustmer.SelectedRows[0].Cells["客户编号"].Value == null) return;
            string custid = this.dataGridViewCustmer.SelectedRows[0].Cells["客户编号"].Value.ToString().Trim();

            FormSetCustRela fr = new FormSetCustRela();
            fr.custid = custid;
            fr.ShowDialog();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //根据Custid(客户ID),CustName(客户名),PinYinCode(拼音码),
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string swhere = " where T_CustomerInf.CustID like '%{0}%'"
                    + " or CustName like '%{1}%' or PinYinCode like '%{2}%'";
                swhere = string.Format(swhere, textBoxName, textBoxName, textBoxName);
                this.WhereTj = swhere;

                initDataGridView();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WhereTj = "";
            initDataGridView();
          }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
