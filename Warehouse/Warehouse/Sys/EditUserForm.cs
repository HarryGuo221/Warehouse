using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Base;
using Warehouse.DB;
using Warehouse.DAO;

namespace Warehouse.Sys
{
    public partial class EditUserForm : Form
    {
        private int curRowIndex = 0;
        private MenuStrip menuStripMain;
        private string type;
        /// <summary>
        /// 编辑时，传递进来的当前用户名
        /// </summary>
        private string userId;

        public delegate void UserInfoFormChange();
        /// <summary>
        /// 用户信息改变 事件
        /// </summary>
        public event UserInfoFormChange userInfoFormChange;

        public EditUserForm(MenuStrip menuStripMain, string type, string userId)
        {
            InitializeComponent();

            this.menuStripMain = menuStripMain;
            this.type = type;
            this.userId = userId;
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            //初始化ComboBox
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitComboBox(this.comboBoxBranch, "T_Branch", "BName");
            initFuncs.InitComboBox(this.comboBoxType, "T_UserType", "UTypeName");

            // 合同:无码数据初始化
            initFuncs.InitComboBox(this.s_groups, "[用户]所属组");

            DBUtil dbUtil = new DBUtil();
            if (this.type == "edit")
            {
                string strSql = "select * from T_Users where UserId='{0}'";
                strSql = string.Format(strSql, this.userId);
                                
                initFuncs.ShowDatas(this.panelUser, strSql);

                //处理ComboBox
                this.comboBoxBranch.Text = dbUtil.Get_Single_val("T_Branch", "BName", "BId", this.s_BranchId.Text.Trim());
                this.comboBoxType.Text = dbUtil.Get_Single_val("T_UserType", "UtypeName", "TypeId", this.s_DefaultUserType.Text.Trim());

                //处理CheckBox
                //string strYNAdmin = UsersDAO.GetYNAdmin(this.userId);
                //if (strYNAdmin == "True")
                //    this.s_ynAdmin.Checked = true;
                //else
                //    this.s_ynAdmin.Checked = false;

                //处理确认密码
                this.txtPasswordCheck.Text = (new DBUtil()).Get_Single_val("T_Users", "PassWord", "UserId", this.userId);

                this.s_UserId.ReadOnly = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {            
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_UserId))
            {
                MessageBox.Show("用户编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_UserId, 10))
            {
                MessageBox.Show("用户编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_UserName))
            {
                MessageBox.Show("用户名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (Util.ControlTextIsNUll(this.s_SmsTel))
            //{
            //    MessageBox.Show("接收短信电话号码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (Util.IsPhoneNumber(this.s_SmsTel,11) == false)
            //{
            //    MessageBox.Show("接收短信电话号码输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (this.s_OfficeTel.Text.Trim() != "" && Util.IsNumberic(this.s_OfficeTel) == false)
            {
                MessageBox.Show("办公电话输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                s_OfficeTel.Focus();
                return;
            }
            if (this.s_MobileTel.Text.Trim() != "" && Util.IsNumberic(this.s_MobileTel) == false)
            {
                MessageBox.Show("移动电话输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                s_MobileTel.Focus();
                return;
            }
            if (this.s_Email.Text.Trim() != "" && Util.IsEmail(this.s_Email.Text.Trim()) == false)
            {
                MessageBox.Show("Email输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                s_Email.Focus();
                return;
            }
            if (this.s_PassWord.Text.Trim() != "" && this.s_PassWord.Text.Trim() != this.txtPasswordCheck.Text.Trim())
            {
                MessageBox.Show("您两次输入的密码不一致，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                s_PassWord.Focus();
                return;
            }
            #endregion

            //数据处理
            SqlDBConnect db = new SqlDBConnect();
            InitFuncs initFuncs = new InitFuncs();
            DBUtil dbUtil = new DBUtil();

            string strSql = "";
            string strSql_ = "";
            if (this.type == "add")
            {
                string strSqlInsert = initFuncs.Build_Insert_Sql(this.panelUser, "T_Users");                

                //插入之前首先判断是否存在该用户                
                bool isExist = dbUtil.Is_Exist_Data("T_Users", "UserId", this.s_UserId.Text.Trim());

                if (isExist == true) //该用户名已存在
                {
                    MessageBox.Show("该用户已存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;                                   
                }
                strSql_ = "select * from T_Users where Ltrim(Rtrim(UserName))='{0}'";
                strSql_ = string.Format(strSql_, this.s_UserName.Text.Trim());

                //插入
                strSql = initFuncs.Build_Insert_Sql(this.panelUser, "T_Users");               
                
            }           
            else if (this.type == "edit")
            {
                //更新
                strSql_ = "select * from T_Users where Ltrim(Rtrim(UserId)) != '{0}' and Ltrim(Rtrim(UserName))='{1}'";
                strSql_ = string.Format(strSql_, this.s_UserId.Text.Trim(), this.s_UserName.Text.Trim());

                string strSqlWhere = "where UserId='" + this.s_UserId.Text.Trim() + "'";
                strSql = initFuncs.Build_Update_Sql(this.panelUser, "T_Users", strSqlWhere);
            }
            if (dbUtil.yn_exist_data(strSql_))
            {
                MessageBox.Show("该用户已存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            db.ExecuteNonQuery(strSql);
            MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning); 

            userInfoFormChange(); //激活代理事件，在UserForm中处理    
            this.Close(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_BranchId.Text = (new DBUtil()).Get_Single_val("T_Branch","BId","BName",this.comboBoxBranch.Text.Trim());
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_DefaultUserType.Text = (new DBUtil()).Get_Single_val("T_UserType", "TypeId", "UtypeName", this.comboBoxType.Text.Trim());
        }

        
        private void s_UserName_TextChanged(object sender, EventArgs e)
        {
            this.s_UserNameZJM.Text = Util.ChineseCharacterToLetter(this.s_UserName.Text.Trim());
        }
        

    }
}
