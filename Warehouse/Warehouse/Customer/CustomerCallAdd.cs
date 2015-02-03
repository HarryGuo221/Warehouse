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
using Warehouse.Stock;
using Warehouse.Sys;

namespace Warehouse.Customer
{
    public partial class CustomerCallAdd : Form
    {
        public string CurrentUser = "";  //当前用户
        
        public string CallId;       //修改时，传入的召唤单编号
        public string type;        //"新增","修改"
        
        public string cmsysid;        //客户机器表中的PK=machineid //机器号
          

        public CustomerCallAdd()
        {
            InitializeComponent();
        }
        
        public delegate void CustomerCallFormChange();
        public event CustomerCallFormChange customerCallFormChange_;

        private void CustomerCallAdd_Load(object sender, EventArgs e)
        {
            string sql_="";
            string curtime_ = "";
            
            s_Technician1.KeyDown+=new KeyEventHandler(InfoFind.UserName_KeyDown);
            s_Technician2.KeyDown += new KeyEventHandler(InfoFind.UserName_KeyDown);

            //区域:报修内容
            (new InitFuncs()).InitComboBox(this.s_Teltrbtype, "T_TelTrbType", "telmemo");
            //合同:有码数据初始化
            (new InitFuncs()).InitComboBox(this.t_WorkType, "T_WorkType", "wcName");  //工作类别
           

            // 合同:无码数据初始化
            InitFuncs inf = new InitFuncs();
            inf.InitComboBox(this.s_UrgentDegree, "[召唤]紧急程度");
            inf.InitComboBox(this.s_ActionResults, "[召唤]行动结果");
            
            DBUtil dbu = new DBUtil();
            
            if (type == "add")
            {
                this.n_endflag.Text = "0";   //未派
                s_operauser.Text = this.CurrentUser;
                n_cmsysid.Text = this.cmsysid;
                //根据Cmsysid得到<机型>、<机号>、<客户编号> 
                this.s_mtype.Text = dbu.Get_Single_val("T_CustomerMac", "mType", "sysid", this.cmsysid);
                this.s_ManufactCode.Text = dbu.Get_Single_val("T_CustomerMac", "Manufactcode", "sysid", this.cmsysid);
                this.s_CustCode.Text = dbu.Get_Single_val("T_CustomerMac", "custid", "sysid", this.cmsysid);
               
                //获取预定技术员
                this.s_Technician1.Text = dbu.Get_Single_val("T_CustomerMac", "ptech", "sysid", this.cmsysid);
                //
                curtime_ = DBUtil.getServerTime().ToString();
                //查找或获取关联值
                s_CallDate.Text = Convert.ToDateTime(curtime_).ToShortDateString();
                s_CallTime.Text = Convert.ToDateTime(curtime_).ToShortTimeString();
            }
            else
            {
                sql_ = "select * from T_CustomerCall where sysid=" + this.CallId ;
                inf.ShowDatas(this.panelCustCall, sql_);

                this.cmsysid = this.n_cmsysid.Text.Trim();
                //
                t_WorkType.Text = (new DBUtil()).Get_Single_val("T_worktype", "wcname", "wcid", s_WorkType.Text.Trim());

                this.s_CallDate.Text = Convert.ToDateTime(s_CallDate.Text).ToShortDateString();
                this.s_CallTime.Text = Convert.ToDateTime(s_CallTime.Text).ToShortTimeString();
                if (n_endflag.Text == "已派")
                {
                    btn_time.Visible = true;
                }
                else
                {
                    btn_time.Visible = false;
                }
            }

            this.t_CustCode.Text = dbu.Get_Single_val("T_CustomerMac", "Mdepart", "sysid", this.cmsysid);
           
            //根据Cmsysid得到<合同信息>//得到合同类别
            sql_ = "select ContractType from T_Bargains where MachineId=" + this.cmsysid;
            this.lb_bargainType.Text = "合同类别：" + dbu.Get_Single_val(sql_);

            //显示联系人列表
            show_contacts(this.cmsysid);
        }

        private void show_contacts(string macsysid)
        {
            //显示联系人列表
            string sql_ = "";
            sql_ = "select T_CustMaContacts.ctype as 类别,"
                   + "T_CustMaContacts.Cname as 联系人,"
                   + "T_CustMaContacts.sex as 性别,"
                   + "T_CustMaContacts.tel as 手机,"
                   + "T_CustMaContacts.tel1 as 联系电话1,"
                   + "T_CustMaContacts.tel2 as 联系电话2,"
                   + "T_CustMaContacts.Email "
                   + " from T_CustMaContacts,T_CustomerMac "
                   + " where T_CustMaContacts.CmSysId=T_CustomerMac.sysid "
                   + " and T_CustomerMac.sysid=" + macsysid;
              DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
              (new InitFuncs()).InitDataGridView(this.dataGridView_contacts, dt);
         }


        private void buttonok_Click(object sender, EventArgs e)
        {
            //受理单不一定有合同,但一定要有客户
            if (Util.ControlTextIsNUll(this.s_CustCode))
            {
                MessageBox.Show("未选择客户！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            t_Technician2_Leave(null, null);
            t_Technician1_Leave(null, null);

          
         
                    
            //数据处理
            string strSql ="";
            InitFuncs initFuncs = new InitFuncs();
            try
            {
                if (this.type == "add")
                {
                     strSql=initFuncs.Build_Insert_Sql(this.panelCustCall, "T_CustomerCall");
                }
                else if (this.type == "edit")
                {
                     strSql = initFuncs.Build_Update_Sql(this.panelCustCall, "T_CustomerCall", " where Sysid=" + this.CallId);
                }

                try
                {
                    //执行SQL
                    (new SqlDBConnect()).ExecuteNonQuery(strSql);
                    MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    customerCallFormChange_(); //激活代理事件
                    this.DialogResult = DialogResult.OK;
                }
                catch
                { }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttoncancell_Click(object sender, EventArgs e)
        {
            this.Close();
        }

              
        private void dataGridView_contacts_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView_contacts.SelectedRows.Count > 0)
            {
                string ContactPerson = this.dataGridView_contacts.SelectedRows[0].Cells["联系人"].Value.ToString().Trim();
                string ContactTel = this.dataGridView_contacts.SelectedRows[0].Cells["联系电话"].Value.ToString().Trim();
                s_Contact.Text = ContactPerson.Trim();
                s_Tel.Text = ContactTel.Trim();
            }
        }

      
        private void t_WorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_WorkType.Text = (new DBUtil()).Get_Single_val("T_worktype", "wcid", "wcname", t_WorkType.Text.Trim());
        }

       

        private void dataGridView_contacts_DoubleClick_1(object sender, EventArgs e)
        {
            if (dataGridView_contacts.SelectedRows.Count > 0)
            {
                if (dataGridView_contacts.SelectedRows[0].Cells["联系人"].Value.ToString().Trim() != "")
                {
                    s_Contact.Text = dataGridView_contacts.SelectedRows[0].Cells["联系人"].Value.ToString().Trim();
                    s_Tel.Text = dataGridView_contacts.SelectedRows[0].Cells["手机"].Value.ToString().Trim();
                }
            }
        }

        private void 故障信息查询(object sender, EventArgs e)
        {
            //if (this.matid.Trim() == "") return;
            //FormMatErrLst fme = new FormMatErrLst();
            //fme.mat_id = this.matid;
            //fme.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ////不需要回写，因为通常电话是无效的
            //if (s_Contact.Text.Trim()=="") return;
            //if (s_Tel.Text.Trim()=="") return;
            
            //string sql_ = "select * from T_CustContacts where custid='{0}' and cname='{1}'";
            //sql_ = string.Format(sql_,this.custid, s_Contact.Text.Trim());

            //if ((new DBUtil()).yn_exist_data(sql_))
            //{
            //    MessageBox.Show("该联系人信息已存在!");
            //    return;
            //}

            //sql_ = "insert into T_CustContacts(CustID,CName,Tel) values ('{0}','{1}','{2}')";
            //sql_=string.Format(sql_, this.custid, s_Contact.Text.Trim(), s_Tel.Text.Trim());
            //try
            //{
            //  (new SqlDBConnect()).ExecuteNonQuery(sql_);
            //  MessageBox.Show("联系人信息添加成功！");
            //  //显示联系人列表
            //  show_contacts();
            //}
            //catch
            //{}
        }

        
        private void t_Technician2_Leave(object sender, EventArgs e)
        {
            if (this.s_Technician2.Text.Trim() == "") return;
            if (!InitFuncs.isRightValue(s_Technician2, "T_Users", "userName"))
            {
                MessageBox.Show("该技术员不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Technician2.Focus();
                this.s_Technician2.Clear();
                return;
            }
          
        }

        private void t_Technician1_KeyDown(object sender, KeyEventArgs e)
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
                strSql = string.Format(strSql, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);
                InitFuncs.FindInfoToControl(textBox, strSql, "姓名");

            }
        }

        private void t_Technician1_Leave(object sender, EventArgs e)
        {
            if (this.s_Technician1.Text.Trim() == "") return;
            if (!InitFuncs.isRightValue(s_Technician1, "T_Users", "userName"))
            {
                MessageBox.Show("该技术员不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Technician1.Focus();
                this.s_Technician1.Clear();
                return;
            }
        }

        private void btn_time_Click(object sender, EventArgs e)
        {
            FormChgSpecialData fcsd = new FormChgSpecialData();
            fcsd.curUser = this.CurrentUser;
            fcsd.chgItems = "修改召唤派工4个时间";
            if (fcsd.ShowDialog() == DialogResult.OK)
            {
                //FormSetCustCall4Time fs4t = new FormSetCustCall4Time();
                //fs4t.callid_ = this.CallId;
                //fs4t.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void CustomerCallAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void t_Teltrbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
           string trbcode_="";
           trbcode_= (new DBUtil()).Get_Single_val("T_TelTrbType", "telcode", "telmemo", this.s_Teltrbtype.Text.Trim());
           
           string sql_ = "select trbcodename from T_TrbTypeDtl where trbcode='" + trbcode_ + "'";
           DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
           s_probcode.Items.Clear();
            for (int m = 0; m < dt.Rows.Count; m++)
           {
               s_probcode.Items.Add(dt.Rows[m][0].ToString().Trim());
           }
         
        }

        
        
    }

}
