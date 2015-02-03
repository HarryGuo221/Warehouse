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
    public partial class CustomerInfAdd : Form
    {
        private string type;
        private string custid;
        public CustomerInfAdd(string type, string custid)
        {
            this.type = type;
            this.custid = custid;
            InitializeComponent();
        }

        public delegate void CustomerInfoFormChange();
        public event CustomerInfoFormChange customerInfoFormChange;

        private void CustomerInfAdd_Load(object sender, EventArgs e)
        {
            //初始化ComboBox
            InitFuncs inf = new InitFuncs();
            inf.InitComboBox(this.s_CustType, "[客户]类别");
            inf.InitComboBox(this.s_Degree, "[客户]等级");
            inf.InitComboBox(this.s_whichTrade, "[客户]所在行业");
            inf.InitComboBox(this.s_Country, "[客户]国家");
            inf.InitComboBox(this.s_Province, "[客户]省");
            inf.InitComboBox(this.s_City, "[客户]城市");

            inf.InitComboBox(this.t_AreaCode, "T_AreaInf", "Area");
            inf.InitComboBox(this.t_ImportanceDegreeId, "T_CustomerImp", "importance");

            if (custid.Trim() == "")
            {
                tabControl1.Enabled = false;
            }
            else
            {
                tabControl1.Enabled = true ;
            }
            
            s_InputTime.Text = DBUtil.getServerTime().ToString();
            if (this.type == "edit")
            {
                this.s_CustID.ReadOnly = true;
                string strsql = "select * from T_CustomerInf where CustID='{0}'";
                strsql = string.Format(strsql, this.custid);

                inf.ShowDatas(this.panelcustomer, strsql);

                //以下两行必须在ShowDatas后
                DBUtil dbUtil = new DBUtil();
                t_AreaCode.Text = (new DBUtil()).Get_Single_val("T_AreaInf", "Area", "areaId", S_AreaCode.Text.Trim());
                t_ImportanceDegreeId.Text = (new DBUtil()).Get_Single_val("T_CustomerImp", "importance", "Iid", S_ImportanceDegreeId.Text.Trim());

                //初始化抬头
                SqlDBConnect SDB = new SqlDBConnect();
                s_BillHead.Clear();
                string strSql = "select BillHead as 发票抬头 from T_CustomerTitle where CustID='{0}'";//"select * from T_SelItems where ItemType='{0}'";
                strSql = string.Format(strSql, s_CustID.Text.Trim());
                DataSet ds = SDB.Get_Ds(strSql);
                int Lins = ds.Tables[0].Rows.Count;
                string[] match = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < match.Length; i++)
                {
                    s_BillHead.Text += ds.Tables[0].Rows[i]["发票抬头"].ToString() + "\r\n";//回车换行
                }

            }
            if (this.custid!="")
            {//初始化联系人;
                init_lxr();
             //初始化发票
                init_fp();
             //初始化多地址
                init_muladd();
            }
            //限制值类型文本框的输入
            (new InitFuncs()).Num_limited(this.panelcustomer);
        }

        private void init_fp()
        {

            string sql_ = "select sysid as 系统编号,IType as 发票种类,Ititle as 发票抬头,Idet as 发票内容,Memo as 备注 "
                + " from T_CustomerInvoice "
                + " where custid='" + s_CustID.Text.Trim() + "'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridView_Invoice, dt);
            this.dataGridView_Invoice.Columns[0].Visible = false;
        }
        private void init_lxr()
        {

            string sql_ = "select sysid as 系统编号,Cname as 联系人,ctype as 类别,sex as 性别,Email,Tel1 as 联系电话1,Tel2 as 联系电话2, tel as 手机 "
                + " from T_CustContacts "
                + " where custid='" + s_CustID.Text.Trim() + "'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridView_contacts, dt);
            this.dataGridView_contacts.Columns[0].Visible = false;
        }
        private void init_muladd()
        {

            string sql_ = "select sysid as 系统编号,AddType as 地址种类,Addr as 客户地址,Memo as 备注 "
                + " from T_CustomerAdd "
                + " where custid='" + s_CustID.Text.Trim() + "'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridView_CustomerAdd, dt);
            this.dataGridView_CustomerAdd.Columns[0].Visible = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_CustID ))
            {
                MessageBox.Show("客户编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_CustName ))
            {
                MessageBox.Show("客户名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Util.ControlTextIsNUll(this.s_CustType))
            {
                MessageBox.Show("请选择类别！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_whichTrade ))
            {
                MessageBox.Show("请选择所在行业！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.s_PostCode.Text != "" && Util.IsPhoneNumber(this.s_PostCode, 6) == false)
            {
                MessageBox.Show("邮编输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.s_Fax.Text.Trim() != "" && Util.IsNumberic(this.s_Fax) == false)
            {
                MessageBox.Show("传真输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            //数据处理
            InitFuncs initFuncs = new InitFuncs();
            DBUtil dbUtil = new DBUtil();
            try
            {
                #region 添加
                if (this.type == "add")
                {
                    // 判断客户ID是否存在
                    string sql = "select * from T_CustomerInf where CustID='" + this.s_CustID.Text.Trim() + "'";
                    bool ynExistID = dbUtil.yn_exist_data(sql);
                    if (ynExistID == true)
                    {
                        MessageBox.Show("该客户已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        s_CustID.Focus();
                        return;
                    }
                    string strSqlInsert = initFuncs.Build_Insert_Sql(this.panelcustomer, "T_CustomerInf");

                    //插入之前首先判断是否存在该客户                
                    bool isExist = dbUtil.Is_Exist_Data("T_CustomerInf", "CustName", this.s_CustName.Text.Trim());

                    if (isExist == false) //插入一条
                    {
                        try
                        {
                            (new SqlDBConnect()).ExecuteNonQuery(strSqlInsert);
                            MessageBox.Show("添加成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //this.DialogResult = DialogResult.OK;
                            this.custid = s_CustID.Text.Trim();
                            this.type = "edit";
                            tabControl1.Enabled = true;
                        }
                        catch
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    else //该客名已存在
                    {
                        MessageBox.Show("该客户已存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    customerInfoFormChange();
                }
                #endregion
                #region 编辑
                else if (this.type == "edit")
                {
                    //插入之前首先判断是否存在该客户                
                    string sql_ = "select CustName from T_CustomerInf where CustName='" + this.s_CustName.Text.Trim() + "' and CustID not like '" + custid + "'";
                    bool isExist = dbUtil.yn_exist_data(sql_);
                    if (isExist)
                    {
                        MessageBox.Show("该客户名已存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string strSqlUpdate = initFuncs.Build_Update_Sql(this.panelcustomer, "T_CustomerInf", " where CustID='" + this.s_CustID.Text.Trim() + "'");

                    
                    //执行
                    (new SqlDBConnect()).ExecuteNonQuery(strSqlUpdate);
                    MessageBox.Show("更新成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    customerInfoFormChange(); //激活代理事件，在UserForm中处理
                    this.DialogResult = DialogResult.OK;
                } 
                #endregion
            }
               
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
            
        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void t_AreaCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            S_AreaCode.Text = (new DBUtil()).Get_Single_val("T_AreaInf", "AreaId", "area", t_AreaCode.Text.Trim());
        }

        private void t_ImportanceDegreeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            S_ImportanceDegreeId.Text = (new DBUtil()).Get_Single_val("T_CustomerImp", "Iid", "importance", t_ImportanceDegreeId.Text.Trim());
        }

        private void s_CustName_TextChanged(object sender, EventArgs e)
        {
            S_PinYinCode.Text = Util.GetPYM(s_CustName.Text);
        }
        private void CustomerInfAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
            //if (e.KeyChar == '\r')
            //{
            //    SendKeys.Send("{Tab}");
            //}
        }

        private void butsave_Click(object sender, EventArgs e)
        {
            
        }
        private void T_RepairPerson_Leave(object sender, EventArgs e)
        {
            (new InitFuncs()).UserTextBoxInput(sender as TextBox);
        
        }

        private void T_RepairPerson_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                (new InitFuncs()).UserTextBoxInput(sender as TextBox);
            }
        }
        private void butsave_Click_1(object sender, EventArgs e)
        {
            FormCustInvoiceAdd FCIA = new FormCustInvoiceAdd();
            FCIA.type = "add";
            FCIA.custid = this.custid;
            FCIA.customerfpFormChange += new FormCustInvoiceAdd.CustomerfpFormChange(init_fp);
            FCIA.ShowDialog();
            init_fp();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormCustContactAdd fcca = new FormCustContactAdd();
            fcca.type = "add";
            fcca.custid = this.custid;
            fcca.customerContactFormChange += new FormCustContactAdd.CustomerContactFormChange(init_lxr);
            fcca.ShowDialog();
            init_lxr();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView_contacts.SelectedRows.Count > 0)
            {
                if (this.dataGridView_contacts.SelectedRows[0].Cells["系统编号"].Value==null) return;
                string sysid_ = dataGridView_contacts.SelectedRows[0].Cells["系统编号"].Value.ToString().Trim();
                if (sysid_ == "") return;
                FormCustContactAdd fcca = new FormCustContactAdd();
                
                fcca.type = "edit";
                fcca.customerContactFormChange += new FormCustContactAdd.CustomerContactFormChange(init_lxr);
                fcca.custid = this.custid;
                fcca.sysid_ = sysid_;
                fcca.ShowDialog();
                init_lxr();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_contacts.SelectedRows.Count <= 0) return;
            if (this.dataGridView_contacts.SelectedRows[0].Cells["联系人"].Value == null) return;

            string strName = this.dataGridView_contacts.SelectedRows[0].Cells["联系人"].Value.ToString().Trim();
            if (strName == "") return;
            if (DialogResult.Yes == MessageBox.Show("确定删除吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                string strSqlDel = "delete from T_CustContacts where CustID='{0}' and CName='{1}'";
                strSqlDel = string.Format(strSqlDel, this.custid, strName);
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(strSqlDel);
                    //初始化DataGridView
                    init_lxr();
                }
                catch (Exception w)
                { }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_Invoice.SelectedRows.Count <= 0) return;
            if (this.dataGridView_Invoice.SelectedRows[0].Cells["发票抬头"].Value == null) return;

            string Ititle = this.dataGridView_Invoice.SelectedRows[0].Cells["发票抬头"].Value.ToString().Trim();
            if (Ititle == "") return;
            if (DialogResult.Yes == MessageBox.Show("确定删除吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                string strSqlDel = "delete from T_CustomerInvoice where CustID='{0}'and Ititle='{1}'";
                strSqlDel = string.Format(strSqlDel, this.custid, Ititle);
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(strSqlDel);
                    //初始化DataGridView
                    init_fp();
                }
                catch (Exception w)
                { }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView_Invoice.SelectedRows.Count > 0)
            {
                if (this.dataGridView_Invoice.SelectedRows[0].Cells["系统编号"].Value == null) return;
                string sysid_ = dataGridView_Invoice.SelectedRows[0].Cells["系统编号"].Value.ToString().Trim();
                if (sysid_ == "") return;
                FormCustInvoiceAdd FCIA = new FormCustInvoiceAdd();
                FCIA.customerfpFormChange += new FormCustInvoiceAdd.CustomerfpFormChange(init_lxr);
                FCIA.type = "edit";
                FCIA.custid = this.custid;
                FCIA.sysid_ = sysid_;
                FCIA.ShowDialog();
                init_fp();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FormCustAdressadd Fcad = new FormCustAdressadd();
            Fcad.type = "add";
            Fcad.custid = this.custid;
            Fcad.customeraddFormChange += new FormCustAdressadd.CustomerAddFormChange(init_muladd);
            Fcad.ShowDialog();
            init_muladd();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView_CustomerAdd.SelectedRows.Count > 0)
            {
                if (this.dataGridView_CustomerAdd.SelectedRows[0].Cells["系统编号"].Value == null) return;
                string sysid_ = dataGridView_CustomerAdd.SelectedRows[0].Cells["系统编号"].Value.ToString().Trim();
                if (sysid_ == "") return;
                FormCustAdressadd Fcad = new FormCustAdressadd();
                Fcad.customeraddFormChange += new FormCustAdressadd.CustomerAddFormChange(init_lxr);
                Fcad.type = "edit";
                Fcad.custid = this.custid;
                Fcad.sysid_ = sysid_;
                Fcad.ShowDialog();
                init_muladd();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_CustomerAdd.SelectedRows.Count <= 0) return;
            if (this.dataGridView_CustomerAdd.SelectedRows[0].Cells["系统编号"].Value == null) return;

            string SysId = this.dataGridView_CustomerAdd.SelectedRows[0].Cells["系统编号"].Value.ToString().Trim();
            if (SysId == "") return;
            if (DialogResult.Yes == MessageBox.Show("确定删除吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                string strSqlDel = "delete from T_CustomerAdd where CustID='{0}' and SysId='{1}'";
                strSqlDel = string.Format(strSqlDel, this.custid, SysId);
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(strSqlDel);
                    //初始化DataGridView
                    init_muladd();
                }
                catch (Exception w)
                { }
            }
        } 

    }
}
