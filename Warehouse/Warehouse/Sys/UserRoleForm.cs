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

namespace Warehouse.Sys
{
    public partial class UserRoleForm : Form
    {
        private string userId;

        public UserRoleForm(string userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void UserRoleForm_Load(object sender, EventArgs e)
        {
            //初始化DataGridView
            InitDataGridView();

            //初始化ComboBox
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitComboBox(this.comboBoxRole, "T_Roles", "RoleName");

            string strUserName = (new DBUtil()).Get_Single_val("T_Users", "UserName", "UserId", this.userId);
            this.txtUserName.Text = strUserName;
        }

        private void InitDataGridView()
        {
            DataTable dt = UserRoleDAO.GetDatasByUserId(this.userId);

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridView, dt);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            #region 验证            
            if (this.comboBoxRole.SelectedIndex == 0)
            {
                MessageBox.Show("请选择角色！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            DBUtil dbUtil = new DBUtil();
            string userId = this.userId;
            string roleId = dbUtil.Get_Single_val("T_Roles", "RoleId", "RoleName", this.comboBoxRole.Text.Trim());

            //插入之前判断是否又该记录
            string strSqlSel = "select * from T_User_Role where UserId='{0}' and RoleId='{1}'";
            strSqlSel = string.Format(strSqlSel, userId, roleId);
            bool isExist = dbUtil.yn_exist_data(strSqlSel);
            if (isExist)
            {
                MessageBox.Show("已存在该对应关系！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string strSql = "insert into T_User_Role(UserId, RoleId) values('{0}','{1}')";
            strSql = string.Format(strSql, userId, roleId);
            (new SqlDBConnect()).ExecuteNonQuery(strSql);

            MessageBox.Show("添加成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //初始化DataGridView
            InitDataGridView();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除操作会导致删除所有关联的数据，确定要删除吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Cancel)
                return;

            string strRoleName = (((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView).SelectedRows[0].Cells["角色名"].Value.ToString().Trim();

            DBUtil dbUtil = new DBUtil();
            string strRoleId = dbUtil.Get_Single_val("T_Roles", "RoleId", "RoleName", strRoleName);

            string strSqlDel = "delete from T_User_Role where UserId='{0}' and RoleId='{1}'";
            strSqlDel = string.Format(strSqlDel, this.userId, strRoleId);

            (new SqlDBConnect()).ExecuteNonQuery(strSqlDel);

            //初始化DataGridView
            InitDataGridView();
        }

       
    }
}
