using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.DAO;
using Warehouse.Base;

namespace Warehouse.Sys
{
    public partial class UserRelationForm : Form
    {
        /// <summary>
        /// 当前人员Id
        /// </summary>
        private string userId;
        
        public UserRelationForm(string userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void UserRelationForm_Load(object sender, EventArgs e)
        {            
            string strUserName = (new DBUtil()).Get_Single_val("T_Users", "UserName", "UserId", this.userId);
            this.txtCurUserName.Text = strUserName;

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitComboBox(this.comboBoxUserType, "T_UserType", "UtypeName");
            initFuncs.InitComboBox(this.comboBoxUserGrade, "T_UserGrade", "Gname");

            InitDataGridView();
        }

        private void InitDataGridView()
        {
            DataTable dt = UserRelationDAO.GetDatasByParentPId(this.userId);
            if (dt == null || dt.Rows.Count <= 0)
                return;

            (new InitFuncs()).InitDataGridView(this.dataGridViewUserRela, dt);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            WFilter wf = new WFilter(1, "UserName", true);
            wf.tableName = "T_users";    //表名             
            wf.strSql = "select UserId as 用户编码, UserName as 用户名, ynAdmin as 是否系统管理员,BranchId as 部门编码," +
                        "JobPosition as 职位, atGroup as 组别, DefaultUserType as 类别,SmsTel as 接收短信电话号码 " +
                        "from [T_Users] ";                        

            wf.s_items.Add("用户编码,UserId,C");
            wf.s_items.Add("用户名,UserName,C");
            wf.s_items.Add("部门编码,BranchId,N");
            wf.s_items.Add("职位,JobPosition,C");
            wf.s_items.Add("组别,atGroup,C");
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {                
                //插入
                List<string> sqls = new List<string>();
                DBUtil dbUtil = new DBUtil();
                string curUserId = this.userId;

                foreach (string userName in wf.Return_Items)
                {
                    string userId = dbUtil.Get_Single_val("T_Users", "UserId", "UserName", userName.Trim());
                    if (userId == "")
                        continue;

                    //插入前判断
                    string strSqlSel = "select * from T_UserRelation where ParentPId='{0}' and Pid='{1}'";
                    strSqlSel = string.Format(strSqlSel, curUserId, userId);
                    bool isExit = dbUtil.yn_exist_data(strSqlSel);

                    if (isExit == true)
                        continue;

                    string strSql = "insert into T_UserRelation(ParentPId,Pid) values('{0}','{1}')";
                    strSql = string.Format(strSql, curUserId, userId);
                    sqls.Add(strSql);
                }
                (new SqlDBConnect()).Exec_Tansaction(sqls);

                InitDataGridView();
            }
        }      

        private void btnSave_Click(object sender, EventArgs e)
        {
            DBUtil dbUtil = new DBUtil();
            string strChildUserName = this.txtChildUserName.Text.Trim();
            if (strChildUserName == "")
                return;
            string strChildUserId = dbUtil.Get_Single_val("T_Users", "UserId", "UserName", strChildUserName);

            string strSql = "update T_UserRelation set ";

            string strUtypeName = "";
            if (this.comboBoxUserType.SelectedIndex != 0)
            {
                strUtypeName = this.comboBoxUserType.Text.Trim();
                string strUtypeId = dbUtil.Get_Single_val("T_UserType", "TypeId", "UtypeName", strUtypeName);
                strSql = strSql + "Utype='" + strUtypeId + "',";
            }

            string strGidName = "";
            if (this.comboBoxUserGrade.SelectedIndex != 0)
            {
                strGidName = this.comboBoxUserGrade.Text.Trim();
                string strGidId = dbUtil.Get_Single_val("T_UserGrade", "GId", "Gname", strGidName);
                strSql = strSql + "Gid='" + strGidId + "',";
            }

            string strCanRepair = this.txtCanRepair.Text.Trim();            
            strSql = strSql + "CanRepair='" + strCanRepair + "'";

            strSql = strSql + " where ParentPId='{0}' and Pid='{1}'";
            strSql = string.Format(strSql, this.userId, strChildUserId);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
            MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            InitDataGridView();
        }

        private void 删除该人员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Cancel)
                return;

            int selectedCellRowIndex = (((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView).SelectedCells[0].RowIndex;
            string strUserName = this.dataGridViewUserRela.Rows[selectedCellRowIndex].Cells["下级人员"].Value.ToString().Trim();

            DBUtil dbUtil = new DBUtil();
            string strUserId = dbUtil.Get_Single_val("T_Users", "UserId", "UserName", strUserName);

            string strSqlDel = "delete from T_UserRelation where ParentPId='{0}' and Pid='{1}'";
            strSqlDel = string.Format(strSqlDel, this.userId, strUserId);

            (new SqlDBConnect()).ExecuteNonQuery(strSqlDel);

            //初始化DataGridView
            InitDataGridView();

            Util.ClearControlText(this.groupBoxChildUser);
        }
        /// <summary>
        /// 单击GridView的一行
        /// </summary>
        private void dataGridViewUserRela_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewUserRela.SelectedRows.Count <= 0)
                return;
            string strChildUserName = this.dataGridViewUserRela.SelectedRows[0].Cells["下级人员"].Value.ToString().Trim();
            
            this.txtChildUserName.Text = strChildUserName;
            this.txtCanRepair.Text = this.dataGridViewUserRela.SelectedRows[0].Cells["可维修机型"].Value.ToString().Trim();

            if (this.dataGridViewUserRela.SelectedRows[0].Cells["人员类型"].Value.ToString().Trim() == "")
                this.comboBoxUserType.SelectedIndex = 0;
            else
                this.comboBoxUserType.Text = this.dataGridViewUserRela.SelectedRows[0].Cells["人员类型"].Value.ToString().Trim();

            if (this.dataGridViewUserRela.SelectedRows[0].Cells["人员等级"].Value.ToString().Trim() == "")
                this.comboBoxUserGrade.SelectedIndex = 0;
            else
                this.comboBoxUserGrade.Text = this.dataGridViewUserRela.SelectedRows[0].Cells["人员等级"].Value.ToString().Trim();
                        
        }


    }
}
