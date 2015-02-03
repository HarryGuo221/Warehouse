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
using Warehouse.DAO;

namespace Warehouse.Sys
{
    public partial class RolesForm : Form
    {
        private MenuStrip menuStripMain;
        private int curSelectRowIndex = 0;

        public RolesForm(MenuStrip menuStripMain)
        {
            InitializeComponent();

            this.menuStripMain = menuStripMain;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region //验证
            if (Util.ControlTextIsNUll(this.s_RoleId))
            {
                MessageBox.Show("角色编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_RoleId, 4))
            {
                MessageBox.Show("角色编号不能大于4位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_RoleName))
            {
                MessageBox.Show("角色名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            InitFuncs initFuncs = new InitFuncs();
            string strSqlInsert = initFuncs.Build_Insert_Sql(this.panelRoles, "T_Roles");

            //插入之前首先判断是否存在该角色
            DBUtil dbUtil = new DBUtil();

            bool isExist = false;
            if (this.s_RoleId.Text.Trim() != "")
                isExist = dbUtil.Is_Exist_Data("T_Roles", "RoleId", this.s_RoleId.Text.Trim());
            
            SqlDBConnect db = new SqlDBConnect();
            if (isExist == true)
            {
                if (this.s_RoleId.ReadOnly == false)
                {
                    MessageBox.Show("已存在该角色！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //更新
                List<string> sqls = new List<string>();
                string strSqlWhere = "where RoleId='" + this.s_RoleId.Text.ToString().Trim() + "'";
                string strSqlUpdate = initFuncs.Build_Update_Sql(this.panelRoles, "T_Roles", strSqlWhere);
                sqls.Add(strSqlUpdate);

                //更新权限
                string strRoleId = this.s_RoleId.Text.Trim();

                //先删除
                string strSqlDelete = "delete from T_Role_Rights where RoleId='{0}'";
                strSqlDelete = string.Format(strSqlDelete, strRoleId);
                sqls.Add(strSqlDelete);

                List<TreeNode> checkedNodes = new List<TreeNode>();//存储所有的选中的叶子菜单项
                UserPermission(this.treeViewMenus, checkedNodes);

                foreach (TreeNode treeNode in checkedNodes)
                {
                    string strSql = "insert into T_Role_Rights([RoleId],[Function]) values('{0}','{1}')";
                    strSql = string.Format(strSql, strRoleId, treeNode.Text.Trim());

                    sqls.Add(strSql);
                }

                db.Exec_Tansaction(sqls);                

                MessageBox.Show("更新成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InitDataGridView(this.curSelectRowIndex);
                //btnCancel_Click(null, null);
            }
            else
            {
                //插入
                string strSql_ = "select * from T_Roles where Ltrim(Rtrim(RoleName))='{0}'";
                strSql_ = string.Format(strSql_, this.s_RoleName.Text.Trim());
                if (dbUtil.yn_exist_data(strSql_))
                {
                    MessageBox.Show("该角色名已存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                db.ExecuteNonQuery(strSqlInsert);

                //分配权限
                string strRoleId = this.s_RoleId.Text.Trim();
                if (strRoleId != "")
                {
                    List<string> sqls = new List<string>();

                    List<TreeNode> checkedNodes = new List<TreeNode>();//存储所有的选中的叶子菜单项
                    UserPermission(this.treeViewMenus, checkedNodes);
                    
                    foreach (TreeNode treeNode in checkedNodes)
                    {
                        string strSql = "insert into T_Role_Rights([RoleId],[Function]) values('{0}','{1}')";
                        strSql = string.Format(strSql, strRoleId, treeNode.Text.Trim());

                        sqls.Add(strSql);
                    }

                    db.Exec_Tansaction(sqls);
                }

                MessageBox.Show("添加成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InitDataGridView(0);
                btnCancel_Click(null, null);
            }

        }
        private void RolesForm_Load(object sender, EventArgs e)
        {
            InitDataGridView(0);

            //把一个菜单项转化为一个树
            Util.MenuStripToTreeView(this.treeViewMenus, menuStripMain);
        }
        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        private void InitDataGridView(int seleRowIndex)
        {
            DataTable dt = RolesDAO.GetDatas();

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridViewRoles, dt);
            foreach (DataGridViewRow row in this.dataGridViewRoles.Rows)
            {
                if (row.Index == seleRowIndex)                
                    row.Selected = true;                    
                else
                    row.Selected = false; 
            }
        }
        /// <summary>
        /// 实现树节点的级联选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewMenus_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.s_RoleId.ReadOnly = false;
            this.s_RoleId.Focus();
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.ClearData(this.panelRoles);

            SetTreeViewNull(this.treeViewMenus.Nodes);
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 单击GridView的一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.curSelectRowIndex = e.RowIndex;
            this.s_RoleId.ReadOnly = true;
            string roleId = this.dataGridViewRoles.SelectedRows[0].Cells["角色编号"].Value.ToString().Trim();
            if (roleId == "") return;

            string strSql = "select * from T_Roles where RoleId='{0}'";
            strSql = string.Format(strSql, roleId);

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.ShowDatas(this.panelRoles, strSql);

            //处理DataGridView
            //先置空
            SetTreeViewNull(this.treeViewMenus.Nodes);

            List<string> listFunction = RoleRightsDAO.GetFunctionById(roleId);
            SetUserPermission(this.treeViewMenus, listFunction);
        }
        
        /// <summary>
        /// 获得角色权限
        /// </summary>
        private void UserPermission(TreeView treeViewMenus, List<TreeNode> checkedNodes)
        {
            checkedNodes.Clear();
            GetUserPermission(treeViewMenus.Nodes, checkedNodes);
        }
        /// <summary>
        /// 获得角色权限
        /// </summary>
        private void GetUserPermission(TreeNodeCollection nodes, List<TreeNode> checkedNodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked == true && node.Nodes.Count == 0) //叶子节点
                    checkedNodes.Add(node);
                if (node.Nodes.Count > 0)
                    GetUserPermission(node.Nodes, checkedNodes);
            }
        }
        /// <summary>
        /// 设置用户已有的权限
        /// </summary>
        /// <param name="treeViewMenus"></param>
        /// <param name="listFunction"></param>
        private void SetUserPermission(TreeView treeViewMenus, List<string> listFunction)
        {
            SetTreeViewNodes(treeViewMenus.Nodes, listFunction);
            
        }
        private void SetTreeViewNodes(TreeNodeCollection nodes, List<string> listFunction)
        {
            foreach (string function in listFunction)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Nodes.Count == 0 && node.Text == function) //叶子节点
                    {
                        node.Checked = true;
                        break;
                    }
                    else if (node.Nodes.Count > 0)
                        SetTreeViewNodes(node.Nodes, listFunction);
                }
            }
        }
        /// <summary>
        /// 置空TreeView
        /// </summary>
        /// <param name="nodes"></param>
        private void SetTreeViewNull(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                if (node.Nodes.Count > 0)
                    SetTreeViewNull(node.Nodes);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Cancel)
                return;

            string roleId = (((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView).SelectedRows[0].Cells["角色编号"].Value.ToString().Trim();
            if (roleId == "") return;

            List<string> sqls = new List<string>();
            string strSqlDel1 = "delete from T_Role_Rights where RoleId='{0}'";
            strSqlDel1 = string.Format(strSqlDel1, roleId);

            string strSqlDel2 = "delete from T_Roles where RoleId='{0}'";
            strSqlDel2 = string.Format(strSqlDel2, roleId);

            sqls.Add(strSqlDel1);
            sqls.Add(strSqlDel2);
            (new SqlDBConnect()).Exec_Tansaction(sqls);

            //初始化DataGridView
            InitDataGridView(0);

            btnCancel_Click(null, null);
        }
       
    }
}
