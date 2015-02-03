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
using Warehouse.Base;

namespace Warehouse.Modal
{
    public partial class MenuConfigMainForm : Form
    {
        private bool isAdd = true;
        private int curRowIndex = 0;
        private MenuStrip menuStripMain;
        public MenuConfigMainForm(MenuStrip menuStripMain)
        {
            InitializeComponent();
            this.menuStripMain = menuStripMain;
        }

        private void MenuConfigMainForm_Load(object sender, EventArgs e)
        {
            InitDataGridView();
        }

        private void InitDataGridView()
        {
            DataTable dt = MenuCfgMainDAO.GetDatas();

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridViewMenuMain, dt);

            this.dataGridViewMenuMain.ClearSelection();
            this.dataGridViewMenuMain.Rows[this.curRowIndex].Selected = true;

            //隐藏第一列
            this.dataGridViewMenuMain.Columns[0].Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region //验证
            if (this.s_MainMenu.Text.Trim() == "")
            {
                MessageBox.Show("主菜单项不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.n_OrderIndex.Text.Trim() != "" && !Util.IsNumberic(this.n_OrderIndex))
            {
                MessageBox.Show("主顺序号输入有误，请输入数值！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.LessOneNumber(this.n_OrderIndex, this.menuStripMain.Items.Count) == false)
            {
                MessageBox.Show("主顺序号只能输入小于等于" + this.menuStripMain.Items.Count + "的数字！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            SqlDBConnect db = new SqlDBConnect();
            InitFuncs initFuncs = new InitFuncs();
            DBUtil dbUtil = new DBUtil();

            string strSql = "";
            string strSql_ = "";
            if (this.isAdd == true) //新增
            {                
                strSql_ = "select * from T_MenuCfgMain where Ltrim(Rtrim(MainMenu))='{0}'";
                strSql_ = string.Format(strSql_, this.s_MainMenu.Text.Trim());
                //插入
                strSql = initFuncs.Build_Insert_Sql(this.panelMenuMain, "T_MenuCfgMain");  
            }
            else //修改
            {
                //更新
                string strSqlWhere = "where Sysid='" + this.n_Sysid.Text.Trim() + "'";
                strSql = initFuncs.Build_Update_Sql(this.panelMenuMain, "T_MenuCfgMain", strSqlWhere);

                strSql_ = "select * from T_MenuCfgMain where Ltrim(Rtrim(Sysid)) != {0} and Ltrim(Rtrim(MainMenu))='{1}'";
                strSql_ = string.Format(strSql_, this.n_Sysid.Text.Trim(), this.s_MainMenu.Text.Trim());
            }            
            if (dbUtil.yn_exist_data(strSql_))
            {
                MessageBox.Show("该主菜单项已存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.ExecuteNonQuery(strSql);
            MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            InitDataGridView();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.isAdd = true;
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.ClearData(this.panelMenuMain);
            this.s_MainMenu.Focus();
        }
        /// <summary>
        /// 单击了dataGridView的某一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMenuMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewMenuMain.SelectedRows.Count <= 0)
                return;
            this.curRowIndex = e.RowIndex;

            string strSysId = this.dataGridViewMenuMain.SelectedRows[0].Cells["编号"].Value.ToString().Trim();
            if (strSysId == "") return;
            int intSyId = Convert.ToInt32(strSysId);

            string strSql = "select * from T_MenuCfgMain where Sysid={0}";
            strSql = string.Format(strSql, intSyId);

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.ShowDatas(this.panelMenuMain, strSql);
            this.isAdd = false;//修改
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSysId = (((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl as DataGridView).SelectedRows[0].Cells["编号"].Value.ToString().Trim();
            if (strSysId == "") return;
            int intSysId = Convert.ToInt32(strSysId);

            try
            {
                //string strSqlMenuChild = "delete from T_MenuCfgChild where MainMenuId={0}";
                //strSqlMenuChild = string.Format(strSqlMenuChild, intSysId);//删除T_MenuCfgChild

                string strSqlMenuMain = "delete from T_MenuCfgMain where Sysid={0}";
                strSqlMenuMain = string.Format(strSqlMenuMain, intSysId);//删除T_Roles            

                //事务
                List<string> sqls = new List<string>();
                //sqls.Add(strSqlMenuChild);
                sqls.Add(strSqlMenuMain);

                (new SqlDBConnect()).Exec_Tansaction(sqls);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InitDataGridView();//刷新DataGridView

            btnClear_Click(sender, e);
        }
    }
}
