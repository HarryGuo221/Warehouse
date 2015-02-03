using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DAO;
using Warehouse.Base;
using Warehouse.DB;

namespace Warehouse.Sys
{
    public partial class UsersForm : Form
    {
        private int curRowIndex = 0;
        private MenuStrip menuStripMain;

        public UsersForm(MenuStrip menuStripMain)
        {
            InitializeComponent();

            this.menuStripMain = menuStripMain;
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            initDataGridView();                                    
        }
        /// <summary>
        /// 初始化显示数据表
        /// </summary>
        public void initDataGridView()
        {
            DataTable dt = UsersDAO.GetDatasOfUsers();

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridViewUsers, dt);

            this.dataGridViewUsers.RowHeadersWidth = 45;

            this.dataGridViewUsers.ClearSelection();
            this.dataGridViewUsers.Rows[this.curRowIndex].Selected = true;
        }       
        /// <summary>
        /// 双击DataGridView的某一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.curRowIndex = e.RowIndex;
            toolStripButtonEdit_Click(sender, e); 
        }         
        /// <summary>
        /// 添加用户
        /// </summary> 
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            EditUserForm editUserForm = new EditUserForm(this.menuStripMain, "add", "");
            editUserForm.userInfoFormChange += new EditUserForm.UserInfoFormChange(editUserForm_userInfoFormChange);
            editUserForm.ShowDialog();
        }
        /// <summary>
        /// 响应编辑用户信息改变事件
        /// </summary>
        void editUserForm_userInfoFormChange()
        {
            initDataGridView();
        }
        /// <summary>
        /// 修改用户
        /// </summary> 
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewUsers.SelectedRows.Count <= 0)
                return;
            this.curRowIndex = this.dataGridViewUsers.SelectedRows[0].Index;

            if (this.dataGridViewUsers.SelectedRows[0].Cells["用户编码"].Value == null) return;
            string userId = this.dataGridViewUsers.SelectedRows[0].Cells["用户编码"].Value.ToString().Trim();
            if (userId == "") return;

            EditUserForm editUserForm = new EditUserForm(this.menuStripMain, "edit", userId);
            editUserForm.userInfoFormChange += new EditUserForm.UserInfoFormChange(editUserForm_userInfoFormChange);
            editUserForm.ShowDialog();
        }
        /// <summary>
        /// 删除用户
        /// </summary> 
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            string userId = this.dataGridViewUsers.SelectedRows[0].Cells["用户编码"].Value.ToString().Trim();

            if (MessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
            {
                UsersDAO.DeleteByUserId(userId);

                initDataGridView();
            }
        }
        /// <summary>
        /// 角色管理
        /// </summary>  
        private void toolStripButtonRoleManage_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewUsers.SelectedRows.Count <= 0)
                return;
            string userId = this.dataGridViewUsers.SelectedRows[0].Cells["用户编码"].Value.ToString().Trim();

            UserRoleForm form = new UserRoleForm(userId);
            form.ShowDialog();
        }
        /// <summary>
        /// 下级人员管理
        /// </summary> 
        private void toolStripButtonChildUsers_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewUsers.SelectedRows.Count <= 0)
                return;
            string userId = this.dataGridViewUsers.SelectedRows[0].Cells["用户编码"].Value.ToString().Trim();

            UserRelationForm form = new UserRelationForm(userId);
            form.ShowDialog();
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void 添加人员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonAdd_Click(sender, e);
        }

        private void 查看人员信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonEdit_Click(sender, e);
        }

        private void 删除人员信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonDelete_Click(sender, e);
        }

        private void 角色管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonRoleManage_Click(sender, e);
        }

        private void 下级人员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonChildUsers_Click(sender, e);
        }

        private void dataGridViewUsers_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {            
            List<string> UserIds = (new DBUtil()).GetOneFiledData("T_UserRelation", "ParentPId");           

            foreach (DataGridViewRow dr in this.dataGridViewUsers.Rows)
            {
                string userId = "";
                if (dr.Cells["用户编码"].Value != null)
                    userId = dr.Cells["用户编码"].Value.ToString().Trim();
                if (UserIds.Contains(userId))
                {
                    dr.HeaderCell.Value = "*";
                    dr.HeaderCell.Style.ForeColor = Color.Red;
                }
            }
        }

    }
}
