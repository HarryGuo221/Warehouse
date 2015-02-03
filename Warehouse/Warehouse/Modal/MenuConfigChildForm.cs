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

namespace Warehouse.Modal
{
    public partial class MenuConfigChildForm : Form
    {
        private bool isAdd = true;
        private int curRowIndex = 0;
        private MenuStrip menuStipMain;
        public MenuConfigChildForm(MenuStrip menuStipMain)
        {
            InitializeComponent();
            this.menuStipMain = menuStipMain;
        }

        private void MenuConfigChildForm_Load(object sender, EventArgs e)
        {
            InitComboBox();
            InitDataGridView();
        }
        /// <summary>
        /// 初始化绑定ComboBox
        /// </summary>
        private void InitComboBox()
        {
            //初始化ComboBox
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitComboBox(this.comboBoxMainMenu, "T_MenuCfgMain", "MainMenu");
            initFuncs.InitComboBox(this.comboBoxReceName, "T_ReceiptModal", "ReceName"); 
        }

        private void comboBoxMainMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.n_MainMenuId.Text = (new DBUtil()).Get_Single_val("T_MenuCfgMain", "Sysid", "MainMenu", this.comboBoxMainMenu.Text.Trim());
        }

        private void comboBoxReceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_SubMenu.Text = (new DBUtil()).Get_Single_val("T_ReceiptModal", "ReceTypeID", "ReceName", this.comboBoxReceName.Text.Trim());
        }
        /// <summary>
        /// 初始化绑定DataGridView
        /// </summary>
        private void InitDataGridView()
        {
            DataTable dt = MenuCfgChildDAO.GetDatas();

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridViewMenuChid, dt);

            this.dataGridViewMenuChid.ClearSelection();
            this.dataGridViewMenuChid.Rows[this.curRowIndex].Selected = true;

            this.dataGridViewMenuChid.Columns[0].Visible = false;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DBUtil dbUtil = new DBUtil();
            int intMainMenuOrderIndex;
            string strMainMenuOrderIndex = dbUtil.Get_Single_val("T_MenuCfgMain", "OrderIndex", "MainMenu", this.comboBoxMainMenu.Text.Trim());
            if (strMainMenuOrderIndex != "")
                intMainMenuOrderIndex = Convert.ToInt32(strMainMenuOrderIndex);
            else
                intMainMenuOrderIndex = this.menuStipMain.Items.Count-1;//空值时，默认为最后一个位置

            int intSubMenuOrderIndex = (this.menuStipMain.Items[intMainMenuOrderIndex] as ToolStripMenuItem).DropDownItems.Count + 1;

            #region 验证
            if (this.comboBoxMainMenu.SelectedIndex == 0)
            {
                MessageBox.Show("主菜单项不能为空，请选择！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.comboBoxReceName.SelectedIndex == 0)
            {
                MessageBox.Show("子菜单项不能为空，请选择！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.n_OrderIndex.Text.Trim() != "" && !Util.IsNumberic(this.n_OrderIndex))
            {
                MessageBox.Show("顺序号输入有误，请输入数值！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.LessOneNumber(this.n_OrderIndex, intSubMenuOrderIndex) == false)
            {
                MessageBox.Show("顺序号只能输入大于0，小于等于" + intSubMenuOrderIndex + "的整数，请输入数值！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.n_OrderIndex.Text.Trim() != "" && Convert.ToInt32(this.n_OrderIndex.Text.Trim()) <= 0)
            {
                MessageBox.Show("顺序号只能输入大于0，小于等于" + intSubMenuOrderIndex + "的整数，请输入数值！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            SqlDBConnect db = new SqlDBConnect();
            InitFuncs initFuncs = new InitFuncs();

            int intMainMenuId = -1;
            string strMainMenuId = dbUtil.Get_Single_val("T_MenuCfgMain", "Sysid", "MainMenu", this.comboBoxMainMenu.Text.Trim());
            if (strMainMenuId != "")
                intMainMenuId = Convert.ToInt32(strMainMenuId);
            string strSubMenu = dbUtil.Get_Single_val("T_ReceiptModal", "ReceTypeID", "ReceName", this.comboBoxReceName.Text.Trim());

            MenuCfgChild menuCfgChild = new MenuCfgChild();
            menuCfgChild.MainMenuId = intMainMenuId;
            menuCfgChild.SubMenu = strSubMenu;
            menuCfgChild.ShortCut = this.s_ShortCut.Text.Trim();
            menuCfgChild.OrderIndex = Convert.ToInt32(this.n_OrderIndex.Text.Trim());

            string strSql = "";
            string strSql_ = "";
                        
            if (this.isAdd == true) //新增
            {
                //插入之前的判断
                strSql_ = "select * from T_MenuCfgChild where MainMenuId={0} and SubMenu='{1}'";
                strSql_ = string.Format(strSql_, intMainMenuId, strSubMenu);
                //插入
                strSql = initFuncs.Build_Insert_Sql(this.panelMenuChild, "T_MenuCfgChild");               
            }
            else //修改
            {
                if (this.txtSysid.Text.Trim() == "") return;
                int intSysIdMenuChild = Convert.ToInt32(this.txtSysid.Text.Trim());                

                strSql_ = "select * from T_MenuCfgChild where SysId!={0} and MainMenuId={1} and SubMenu='{2}'";
                strSql_ = string.Format(strSql_, intSysIdMenuChild, intMainMenuId, strSubMenu);
                //更新                            
                string strWhere = string.Format("where SysId={0}", intSysIdMenuChild);
                strSql = initFuncs.Build_Update_Sql(this.panelMenuChild, "T_MenuCfgChild", strWhere);
            }

            bool isExist = dbUtil.yn_exist_data(strSql_);
            if (isExist == true)
            {
                MessageBox.Show("该记录已存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }  

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
            MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
            InitDataGridView();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.ClearData(this.panelMenuChild);
            this.comboBoxMainMenu.Focus();
            this.isAdd = true;
        }

        private void dataGridViewMenuChid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewMenuChid.SelectedRows.Count <= 0)
                return;
            this.curRowIndex = e.RowIndex;

            string strSysId = this.dataGridViewMenuChid.SelectedRows[0].Cells["编号"].Value.ToString().Trim();
            if (strSysId == "") return;
            int intSyId = Convert.ToInt32(strSysId);
            this.txtSysid.Text = strSysId;

            string strSql = "select * from T_MenuCfgChild where SysId={0}";
            strSql = string.Format(strSql, intSyId);

            InitFuncs initFuncs = new InitFuncs();
            initFuncs.ShowDatas(this.panelMenuChild, strSql);////

            this.comboBoxMainMenu.Text = (new DBUtil()).Get_Single_val("T_MenuCfgMain", "MainMenu", "Sysid", this.n_MainMenuId.Text.Trim());
            this.comboBoxReceName.Text = (new DBUtil()).Get_Single_val("T_ReceiptModal", "ReceName", "ReceTypeID", this.s_SubMenu.Text.Trim());
          
            this.isAdd = false;//修改
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewMenuChid.SelectedRows.Count <= 0)
                return;

            string strSysId = this.dataGridViewMenuChid.SelectedRows[0].Cells["编号"].Value.ToString().Trim();
            if (strSysId == "") return;
            int intSyId = Convert.ToInt32(strSysId);

            MenuCfgChildDAO.DeleteBySysId(intSyId);

            (new InitFuncs()).ClearData(this.panelMenuChild);
            InitDataGridView();
        }


    }
}
