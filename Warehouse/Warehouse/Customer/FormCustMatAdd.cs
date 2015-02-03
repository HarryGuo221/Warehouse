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
    public partial class FormCustMatAdd : Form
    {
        public string custid = ""; // 客户编号
        public string type = "";  //新增、编辑
        public string Cmsysid = "";  //传入的系统编号（编辑用）
        
        public FormCustMatAdd()
        {
            InitializeComponent();
        }
        public delegate void CustMatFormChange();
        public event CustMatFormChange custmatFormChange;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCustMatAdd_Load(object sender, EventArgs e)
        {
            InitFuncs inf = new InitFuncs();
            //无码初始化
            inf.InitComboBox(this.t_MainteType, "[物料]品牌");
            inf.InitComboBox(this.s_Degreed, "[客户]等级");
            inf.InitComboBox(this.t_used_type, "[物料]机器种类");
            inf.InitComboBox(this.s_install_result, "[机器]安装结果");

            //区域:有码数据初始化
            inf.InitComboBox(this.T_Areaid, "T_AreaInf", "Area");  //区域
            //保修类型
            inf.InitComboBox(this.t_MainteType,"tb_maintetype", "maintetypename");  //区域
            //使用类型
            inf.InitComboBox(this.t_used_type, "TB_machusedtype", "usedname");  //区域
            
            if (this.type == "edit")
            {
                string strSql = "select * from T_CustomerMac where sysid={0}";
                strSql = string.Format(strSql, this.Cmsysid);
                (new InitFuncs()).ShowDatas(this.panel1, strSql);
                this.T_CustID.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "custname", "custid", this.s_CustID.Text.Trim());
                T_Areaid.Text = (new DBUtil()).Get_Single_val("T_AreaInf", "Area", "Areaid", s_Areaid.Text.Trim());
                this.t_MainteType.Text = (new DBUtil()).Get_Single_val("tb_maintetype", "maintetypename", "maintetypecode", this.s_MainteType.Text.Trim());
                this.t_used_type.Text = (new DBUtil()).Get_Single_val("TB_machusedtype", "usedname", "used_type", this.s_used_type.Text.Trim());
                
                tabControl_CustomerMaInvoice.Enabled = true;
            }
            if (this.s_CustID.Text .Trim () != "")
            {//初始化联系人;
                init_lxr();
                //初始化发票
                init_fp();
            }
        }
        private void init_fp()
        {

            string sql_ = "select T_CustomerMaInvoice.sysid as 系统编号,"
                + "T_StoreHouse.shname as 所属公司,"
                + "T_Invoice.itname as 发票种类,"
                + "T_CustomerMaInvoice.Ititle as 发票抬头,"
                + "T_CustomerMaInvoice.matid as 发票内容,"
                + "T_CustomerMaInvoice.Memo as 备注 "
                + " from T_CustomerMaInvoice "
                +" left join T_StoreHouse on T_CustomerMaInvoice.kpcorp=T_StoreHouse.shid "
                + " left join T_Invoice on T_CustomerMaInvoice.IType=T_Invoice.itcode "
                + " where T_CustomerMaInvoice.CmSysId='" + Cmsysid + "'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridView_CustomerMaInvoice, dt);
            this.dataGridView_CustomerMaInvoice.Columns[0].Visible = false;
        }
        private void init_lxr()
        {

            string sql_ = "select sysid as 系统编号,Cname as 联系人,ctype as 类别,sex as 性别,Email,Tel1 as 联系电话1,Tel2 as 联系电话2, tel as 手机 "
                + " from T_CustMaContacts "
                + " where CmSysId='" + Cmsysid + "'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridView_CustMaContacts, dt);
            this.dataGridView_CustMaContacts.Columns[0].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WFilter wf = new WFilter(0, "CustName", true);
            wf.tableName = "T_customerInf";    //表名            
            wf.strSql = "select custid as 客户编码,CustName as 客户名称 from T_customerInf";

            wf.s_items.Add("客户编码,CustId,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("联系人,Contact,C");
            wf.s_items.Add("拼音助记码,PinYinCode,C");
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                if (wf.Return_Items.Count >0)
                {
                    T_CustID.Text = wf.Return_Items[0].Trim();
                    s_CustID.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "custid", "custname", T_CustID.Text.Trim());

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Util.ControlTextIsNUll(this.T_CustID))
            {
                MessageBox.Show("客户名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.T_CustID.Focus();
                return;
            }
            T_CustID_Leave(sender, e);
            T_Puser_Leave(sender, e);

            if (Util.ControlTextIsNUll(this.s_Mtype))
            {
                MessageBox.Show("机型不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Mtype.Focus();
                return;
            }

            if (!InitFuncs.isRightValue(this.s_Mtype, "T_Model", "ModelName"))
            {
                MessageBox.Show("输入的机型不存在,请重新录入!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Mtype.Focus();
                return;
            }
         
            if (Util.ControlTextIsNUll(this.s_Manufactcode))
            {
                MessageBox.Show("机号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Manufactcode.Focus();
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
                sql_ = "select CustID from T_CustomerMac where CustID='" + this.custid 
                    +"' and Mtype='"+this.s_Mtype.Text.Trim()
                    + "' and Manufactcode='" + this.s_Manufactcode.Text.Trim() + "'";
                isExist = dbUtil.yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该客户已存在该机号！");
                    this.s_Manufactcode.Focus();
                    return;
                }
                sqlexe = inf.Build_Insert_Sql(this.panel1, "T_CustomerMac");
            }
            else
            {
                bool isExist = false;
                sql_ = "select CustID from T_CustomerMac where CustID='" + this.custid
                    + "' and Mtype='" + this.s_Mtype.Text.Trim()
                    + "' and Manufactcode='" + this.s_Manufactcode.Text.Trim()  
                    + "' and Sysid!=" + Cmsysid;
                isExist = dbUtil.yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该客户已存在该机号！");
                    this.s_Manufactcode.Focus();
                    return;
                }
                string swhere = " where Sysid=" + this.Cmsysid;
                sqlexe = inf.Build_Update_Sql(this.panel1, "T_CustomerMac", swhere);
            }
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sqlexe);
                MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                custmatFormChange();
                //新增成功后让联系人和发票类型可操作
                if (this.type == "add")
                {
                    sql_ = "select sysid from T_CustomerMac "
                        + "where custid='" + this.s_CustID.Text.Trim()
                        + "' and mtype='" + this.s_Mtype.Text.Trim()
                        + "' and Manufactcode='" + this.s_Manufactcode.Text.Trim() + "'";
                    try
                    {
                        string sysid_ = (new DBUtil()).Get_Single_val(sql_);
                        this.type = "edit";
                        this.Cmsysid = sysid_;
                        this.tabControl_CustomerMaInvoice.Enabled = true;
                    }
                    catch
                    {
                        this.Close();
                    }

                }
            }
            catch (Exception w)
            {
                MessageBox.Show(w.ToString());
            }
        }

       

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void T_Areaid_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_Areaid.Text = (new DBUtil()).Get_Single_val("T_AreaInf", "Areaid", "Area", T_Areaid.Text.Trim());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void butsave_Click(object sender, EventArgs e)
        {
            FormCustomerMaInvoiceAdd FCIA = new FormCustomerMaInvoiceAdd();
            FCIA.type = "add";
            FCIA.Cmsysid = Cmsysid;
            FCIA.customerfpmatFormChange += new FormCustomerMaInvoiceAdd.CustomerfpmatFormChange(init_fp);
            FCIA.ShowDialog();
            init_fp();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormCustMaContactsAdd fcmca = new FormCustMaContactsAdd();
            fcmca.type = "add";
            fcmca.Cmsysid = Cmsysid;
            fcmca.customermacontact += new FormCustMaContactsAdd.CustomerMacontact(init_lxr);
            fcmca.ShowDialog();
            init_lxr();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView_CustMaContacts.SelectedRows.Count > 0)
            {
                if (this.dataGridView_CustMaContacts.SelectedRows[0].Cells["系统编号"].Value == null) return;
                string sysid = dataGridView_CustMaContacts.SelectedRows[0].Cells["系统编号"].Value.ToString().Trim();
                if (sysid == "") return;
                FormCustMaContactsAdd fcca = new FormCustMaContactsAdd();
                fcca.customermacontact += new FormCustMaContactsAdd.CustomerMacontact(init_lxr);
                fcca.type = "edit";
                fcca.sysid = sysid;
                fcca.Cmsysid = this.Cmsysid;
                fcca.ShowDialog();
                init_lxr();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView_CustomerMaInvoice.SelectedRows.Count > 0)
            {
                if (this.dataGridView_CustomerMaInvoice.SelectedRows[0].Cells["系统编号"].Value == null) return;
                string sysid = dataGridView_CustomerMaInvoice.SelectedRows[0].Cells["系统编号"].Value.ToString().Trim();
                if (sysid == "") return;
                FormCustomerMaInvoiceAdd FCIA = new FormCustomerMaInvoiceAdd();
                FCIA.customerfpmatFormChange += new FormCustomerMaInvoiceAdd.CustomerfpmatFormChange(init_fp);
                FCIA.type = "edit";
                FCIA.Cmsysid = this.Cmsysid;
                FCIA.sysid = sysid;
                FCIA.ShowDialog();
                init_fp();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_CustMaContacts.SelectedRows.Count <= 0) return;
            if (this.dataGridView_CustMaContacts.SelectedRows[0].Cells["联系人"].Value == null) return;

            string strName = this.dataGridView_CustMaContacts.SelectedRows[0].Cells["联系人"].Value.ToString().Trim();
            if (strName == "") return;
            if (DialogResult.Yes == MessageBox.Show("确定删除吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                string strSqlDel = "delete from T_CustMaContacts where CmSysId='{0}' and CName='{1}'";
                strSqlDel = string.Format(strSqlDel, this.Cmsysid, strName);
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_CustomerMaInvoice.SelectedRows.Count <= 0) return;
            if (this.dataGridView_CustomerMaInvoice.SelectedRows[0].Cells["系统编号"].Value == null) return;

            string sysid_ = this.dataGridView_CustomerMaInvoice.SelectedRows[0].Cells["系统编号"].Value.ToString().Trim();
            if (sysid_ == "") return;
            if (DialogResult.Yes == MessageBox.Show("确定删除吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                string strSqlDel = "delete from T_CustomerMaInvoice where CmSysId='{0}' and sysid={1}";
                strSqlDel = string.Format(strSqlDel, this.Cmsysid, sysid_);
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

        private void T_CustID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                //Dictionary<string, string> findFields = new Dictionary<string, string>();
                //findFields.Add("CustID", "s");
                //findFields.Add("CustName", "s");
                //Dictionary<string,string> displayFields = new Dictionary<string,string>();
                //displayFields.Add("CustID", "客户编码");
                //displayFields.Add("CustName", "客户名称");
                //displayFields.Add("communicateAddr", "通信地址");
                //displayFields.Add("AtBank", "开户银行");
                //InitFuncs.FindInfoToControl(textBox, "T_CustomerInf", "CustID", findFields, displayFields);

                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string strSql = "select CustID as 客户编码,CustName as 客户名称,"
                                +"PinYinCode as 助记符,communicateAddr as 通信地址, "
                                +"Degree as 客户等级 " +
                                "from T_CustomerInf where CustID like '%{0}%' or CustName like '%{1}%' or PinYinCode like '%{2}%'";
                strSql = string.Format(strSql, textBoxName, textBoxName, textBoxName);
                InitFuncs.FindInfoToControl(textBox, strSql, "客户名称");
              
            }
        }

        private void T_CustID_Leave(object sender, EventArgs e)
        {
            if (this.T_CustID.Text.Trim() == "") return;
            if (!InitFuncs.isRightValue(T_CustID, "T_customerinf", "custName"))
            {
                MessageBox.Show("该客户名称不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.T_CustID.Focus();
                this.s_CustID.Clear();
                return;
            }
            s_CustID.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "custid", "custname", T_CustID.Text.Trim());

        }

        private void T_Puser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string strSql = "select T_Users.UserID as 人员编号,"
                               + "T_Users.UserName as 姓名,"
                               + "T_Users.UserNameZJM as 助记符,"
                               + " T_Branch.BName as 部门, "
                               + " T_Users.groups as 组别"
                               + " from T_Users, T_Branch"
                               + " where T_Users.BranchId=T_Branch.bid "
                               + " and ( UserID like '%{0}%' or UserName like '%{1}%'"
                               + " or UserNameZJM like '%{2}%' or BName like '%{3}%'"
                               + " or groups like '%{4}%')";
                strSql = string.Format(strSql, textBoxName, textBoxName,textBoxName,textBoxName,textBoxName);
                InitFuncs.FindInfoToControl(textBox, strSql, "姓名");

            }
        }

        private void T_Puser_Leave(object sender, EventArgs e)
        {
            if (this.s_ptech.Text.Trim() == "") return;
            if (!InitFuncs.isRightValue(s_ptech, "T_users", "username"))
            {
                MessageBox.Show("该人员不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_ptech.Focus();
                this.s_ptech.Clear();
                return;
            }
       
        }

        private void FormCustMatAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void p_sale_Leave(object sender, EventArgs e)
        {
            if (this.s_psale.Text.Trim() == "") return;
            if (!InitFuncs.isRightValue(s_psale, "T_users", "username"))
            {
                MessageBox.Show("该人员不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_psale.Focus();
                this.s_psale.Clear();
                return;
            }
        }

        private void t_used_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_used_type.Text = (new DBUtil()).Get_Single_val("TB_machusedtype", "usedname", "used_type", this.s_used_type.Text.Trim());
        
        }

        private void t_MainteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_MainteType.Text = (new DBUtil()).Get_Single_val("tb_maintetype", "maintetypename", "maintetypecode", this.s_MainteType.Text.Trim());
        }

        private void s_Mtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string BasicSql = "select SysID, ModelName as 机型名,"
                + "ProdCtgrID as 分类,"  
                + "ModelType as 机器类别,"
                + "ModelName1 as 机型别名1,ModelName2 as 机型别名2,"
                + "ModelGrade  as 机器等级,"
                + "Modelbrand as 品牌,"
                + "Ndxs as 难度系数 "
                + "from T_Model";

                string swhere = " where ModelName like '%{0}%'" +
                                 "or ModelName1 like '%{1}%'" +
                                 "or ModelName2 like '%{2}%'" +
                                 "or ProdCtgrID like '%{3}%'" +
                                 "or ModelType like '%{4}%'" +
                                 "or Modelbrand like '%{5}%'" +
                                 "or ModelGrade like '%{6}%'";
                swhere = string.Format(swhere, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);
                
                //显示列表
                DataTable dt = (new SqlDBConnect()).Get_Dt(BasicSql + swhere);
                FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                fr.unVisible_Column_index_ = 0;
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    if (fr.dr_.Cells["sysid"].Value == null) return;
                    if (fr.dr_.Cells["sysid"].Value.ToString().Trim() == "") return;
                    this.s_Mtype.Text = fr.dr_.Cells["机型名"].Value.ToString().Trim();
                }
            }
        }
    }
}
