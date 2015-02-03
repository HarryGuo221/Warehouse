using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Base;
using Warehouse.DAO;
using Warehouse.DB;
using System.Collections;
using System.Data.SqlClient;
using Warehouse.StockReport;

namespace Warehouse.Sys
{
    public partial class SysEnviForm : Form
    {
        BranchDAO BD = new BranchDAO();
        private MenuStrip menuStripMain;

        public SysEnviForm(MenuStrip menuStripMain)
        {
            InitializeComponent();
            this.menuStripMain = menuStripMain;
        }
        #region 初始化DataGridView
        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        /// <param name="Table">表名</param>
        public void InitDataGridView(string Table)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            try
            {
                DataTable dt = BD.GetDatasOfUsers(Table);
                if (Table == "T_SelItems")
                {
                    BD.InitDataGridView(this.dataGridView2, dt);
                }
                else
                {
                    BD.InitDataGridView(this.dataGridView1, dt);
                }
            }
            catch
            {
                MessageBox.Show("初始化失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region//公共存数据方法
        public void Pubmetheod(TextBox ID, string IDName, TextBox EN, string Name, Panel Panelname, string Table)
        {
            try
            {
                bool isExist;
                String sql_ = "", sqle_ = "";
                DBUtil dbUtil = new DBUtil();
                if (ID.ReadOnly)
                {
                    string strSqlWhere = "where " + IDName + "='" + ID.Text.Trim() + "'";
                    sql_ = String.Format("select * from " + Table + " where Ltrim(Rtrim(" + Name + ")) like '{0}' and Ltrim(Rtrim(" + IDName + ")) not like '{1}'", EN.Text.Trim(), ID.Text.Trim());
                    sqle_ = (new InitFuncs()).Build_Update_Sql(Panelname, Table, strSqlWhere);
                }
                else
                {
                    sql_ = String.Format("select * from " + Table + " where Ltrim(Rtrim(" + Name + ")) like '{0}' ", EN.Text.Trim());
                    sqle_ = (new InitFuncs()).Build_Insert_Sql(Panelname, Table);
                }
                isExist = dbUtil.yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该条信息已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    EN.Focus();
                    return;
                }
                (new SqlDBConnect()).ExecuteNonQuery(sqle_);
                InitDataGridView(Table);
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("不能插入重复键！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region //窗体初始化
        private void SysEnviForm_Load(object sender, EventArgs e)
        {
            this.TabAdd.TabPages.Clear();//将Tab菜单隐藏起来
            //this.WindowState = FormWindowState.Maximized;
            try
            {
                InitFuncs initFuncs = new InitFuncs();
                //初始化基础信息定义中的默认仓库选项
                initFuncs.InitComboBox(this.comboBoxstore, "T_StoreHouse", "SHName");
                tabParent_SelectedIndexChanged(sender, e);
            }
            catch (Exception et)
            {
                MessageBox.Show("初始化失败，原因:" + '\n' + et.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 单击treeview相应显示的TabControl并初始化ComboBox
        /// <summary>
        /// 单击treeview相应显示的TabControl并初始化ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strNodeName = SystreeView.SelectedNode.Text.Trim();
            InitFuncs initFuncs = new InitFuncs();
            //用于记录每次输入的记录
            ArrayList list = new ArrayList();
            switch (strNodeName)
            {
                case "部门定义":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.BranchAdd.Parent = this.TabAdd;
                        //this.TabAdd.SelectedIndex = 0;
                        InitDataGridView("T_Branch");
                        //初始化ComboBox
                        initFuncs.InitComboBox(this.comboxLeader, "T_Users", "UserName");
                        break;
                    }
                case "操作角色定义":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.Roles.Parent = this.TabAdd;
                        InitDataGridView("T_Roles");
                        break;
                    }
                case "地区及区域积分":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.AreaInf.Parent = this.TabAdd;
                        //this.TabAdd.SelectedIndex = 1;
                        InitDataGridView("T_AreaInf");
                        break;
                    }
                case "操作员类别":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.UserType.Parent = this.TabAdd;
                        //this.TabAdd.SelectedIndex = 2;
                        InitDataGridView("T_UserType");
                        break;
                    }
                case "人员分管地区":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.User_Area.Parent = this.TabAdd;
                        //this.TabAdd.SelectedIndex = 3;
                        InitDataGridView("T_User_Area");
                        //不让序列号显示出来
                        //dataGridView1.Columns["序列号"].Visible = false;
                        //初始化ComboBox
                        initFuncs.InitComboBox(this.comboBoxUserName, "T_Users", "UserName");
                        initFuncs.InitComboBox(this.comboBoxArea, "T_AreaInf", "Area");
                        break;
                    }
                case "人员等级定义":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.UserGrade.Parent = this.TabAdd;
                        //this.TabAdd.SelectedIndex = 4;
                        InitDataGridView("T_UserGrade");
                        //初始化ComboBox
                        initFuncs.InitComboBox(this.comboxUtype, "T_UserType", "UTypeName");
                        break;
                    }
                case "仓库定义":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.StoreHouse.Parent = this.TabAdd;
                        InitDataGridView("T_StoreHouse");
                        //初始化ComboBox
                        initFuncs.InitComboBox(this.comboBoxSHKeeper, "T_Users", "UserName");
                        initFuncs.InitComboBox(this.comboBoxSort, "T_Material_Sort", "SortName");
                        break;
                    }
                case "发票定义":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.Invoice.Parent = this.TabAdd;
                        //this.TabAdd.SelectedIndex = 5;
                        InitDataGridView("T_Invoice");
                        break;
                    }
                case "物料卡片类别":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.Material_Sort.Parent = this.TabAdd;
                        //this.TabAdd.SelectedIndex = 6;
                        InitDataGridView("T_Material_Sort");
                        break;
                    }
                case "账款支付方式":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.PayMethod.Parent = this.TabAdd;
                        InitDataGridView("T_PayMethod");
                        break;
                    }
                case "客户重要度定义":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.CustomerImp.Parent = this.TabAdd;
                        //this.TabAdd.SelectedIndex = 7;
                        InitDataGridView("T_CustomerImp");
                        break;
                    }
                case "维修类别":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.ServiceType.Parent = this.TabAdd;
                        InitDataGridView("T_ServiceType");
                        break;
                    }
                case "工作类别定义":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.WorkType.Parent = this.TabAdd;
                        InitDataGridView("T_WorkType");
                        break;
                    }
                case "节假日定义":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.Holidays.Parent = this.TabAdd;
                        InitDataGridView("T_Holidays");
                        break;
                    }
                case "故障部位":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.MachLocate.Parent = this.TabAdd;
                        InitDataGridView("T_MachLocate");
                        break;
                    }
                case "故障部位部件":
                    {
                        this.TabAdd.TabPages.Clear();
                        initFuncs.InitComboBox(this.combPmachlctcode, "T_MachLocate", "machlctname");
                        this.machlocatepart.Parent = this.TabAdd;
                        InitDataGridView("T_machlocatepart");
                        break;
                    }
                case "故障类别":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.TrbType.Parent = this.TabAdd;
                        InitDataGridView("T_TrbType");
                        break;
                    }
                case "故障类别明细":
                    {
                        this.TabAdd.TabPages.Clear();
                        initFuncs.InitComboBox(this.comboDtrbcode, "T_TrbType", "trbmemo");
                        this.TrbTypeDtl.Parent = this.TabAdd;
                        InitDataGridView("T_TrbTypeDtl");
                        break;
                    }
                case "故障现象":
                    {
                        this.TabAdd.TabPages.Clear();
                        this.TelTrbType.Parent = this.TabAdd;
                        InitDataGridView("T_TelTrbType");
                        break;
                    }
                case "故障现象明细":
                    {
                        this.TabAdd.TabPages.Clear();
                        initFuncs.InitComboBox(this.comboPtelcode, "T_TelTrbType", "telmemo");
                        this.machproblem.Parent = this.TabAdd;
                        InitDataGridView("T_machproblem");
                        break;
                    }
                default:
                    break;
            }
        }
        #endregion

        #region //部门初始化部分
        private void button1_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_BId))
            {
                MessageBox.Show("部门编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_BId, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_BName))
            {
                MessageBox.Show("部门名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_BId, "Bid", s_BName, "bname", this.panelBranch, "T_Branch");
        }
        #endregion
        #region //重置
        private void button3_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_BId, panelBranch);
        }
        #endregion


        #region 单击dataGridView1显示于不同的Tab
        /// <summary>
        /// 单击dataGridView1显示于不同的Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            DBUtil dbUtil = new DBUtil();
            try
            {
                switch (this.TabAdd.SelectedTab.Name)
                {
                    case "BranchAdd":
                        {
                            s_BId.ReadOnly = true;
                            BD.Data_ToTab("部门编号", "T_Branch", "BId", panelBranch, dataGridView1);
                            comboxLeader.Text = this.dataGridView1.SelectedRows[0].Cells["部门主管"].Value.ToString().Trim();
                            break;
                        }
                    case "Roles":
                        {
                            s_RoleId.ReadOnly = true;
                            BD.Data_ToTab("角色编号", "T_Roles", "RoleId", panelRoles, dataGridView1);
                            break;
                        }
                    case "AreaInf":
                        {
                            s_Areaid.ReadOnly = true;
                            BD.Data_ToTab("系统编号", "T_AreaInf", "Areaid", panelAear, dataGridView1);
                            break;
                        }
                    case "UserType":
                        {
                            n_TypeId.ReadOnly = true;
                            BD.Data_ToTab("操作员类别编号", "T_UserType", "TypeId", panelUser, dataGridView1);
                            break;
                        }
                    case "User_Area":
                        {
                            T_UAid.ReadOnly = true;
                            BD.Data_ToTab("序列号", "T_User_Area", "UAid", panelUserArea, dataGridView1);
                            T_UAid.Text = this.dataGridView1.SelectedRows[0].Cells["序列号"].Value.ToString().Trim();
                            comboBoxUserName.Text = this.dataGridView1.SelectedRows[0].Cells["用户名"].Value.ToString().Trim();
                            comboBoxArea.Text = this.dataGridView1.SelectedRows[0].Cells["地区"].Value.ToString().Trim();
                            break;
                        }
                    case "UserGrade":
                        {
                            s_GId.ReadOnly = true;
                            BD.Data_ToTab("系统等级编码", "T_UserGrade", "GId", panelUserGrade, dataGridView1);
                            comboxUtype.Text = this.dataGridView1.SelectedRows[0].Cells["类型"].Value.ToString().Trim();
                            break;
                        }
                    case "StoreHouse":
                        {
                            s_SHId.ReadOnly = true;
                            BD.Data_ToTab("仓库编号", "T_StoreHouse", "SHId", panelStoreHouse, dataGridView1);
                            comboBoxSHKeeper.Text = this.dataGridView1.SelectedRows[0].Cells["库管员"].Value.ToString().Trim();
                            comboBoxSort.Text = this.dataGridView1.SelectedRows[0].Cells["组织类别"].Value.ToString().Trim();
                            break;
                        }
                    case "Invoice":
                        {
                            s_ITCode.ReadOnly = true;
                            BD.Data_ToTab("发票类型编号", "T_Invoice", "ITCode", panelInvoice, dataGridView1);
                            break;
                        }
                    case "Material_Sort":
                        {
                            s_SortId.ReadOnly = true;
                            BD.Data_ToTab("物料分类编号", "T_Material_Sort", "SortId", panelMaterial, dataGridView1);
                            break;
                        }
                    case "CustomerImp":
                        {
                            s_Iid.ReadOnly = true;
                            BD.Data_ToTab("重要度编码", "T_CustomerImp", "Iid", panelCustomerImp, dataGridView1);
                            break;
                        }
                    case "PayMethod":
                        {
                            s_PMid.ReadOnly = true;
                            BD.Data_ToTab("支付编号", "T_PayMethod", "PMid", panelPayMethod, dataGridView1);
                            break;
                        }
                    case "ServiceType":
                        {
                            s_scid.ReadOnly = true;
                            BD.Data_ToTab("维修类别编码", "T_ServiceType", "scid", panelServiceType, dataGridView1);
                            break;
                        }
                    case "WorkType":
                        {
                            s_wcid.ReadOnly = true;
                            BD.Data_ToTab("工作类别编码", "T_WorkType", "wcid", panelWorkType, dataGridView1);
                            break;
                        }
                    case "MachLocate":
                        {
                            s_Pmachlctcode.ReadOnly = true;
                            //BD.Data_ToTab("部位编码", "T_MachLocate", "machlctcode", panelMachLocate, dataGridView1);
                            s_Pmachlctcode.Text = dataGridView1.SelectedRows[0].Cells["部位编码"].Value.ToString();
                            s_machlctname.Text = dataGridView1.SelectedRows[0].Cells["部位名称"].Value.ToString();
                            s_Macmemo.Text = dataGridView1.SelectedRows[0].Cells["备注"].Value.ToString();
                            break;
                        }
                    case "machlocatepart":
                        {
                            s_machlctpartcode.ReadOnly = true;
                            BD.Data_ToTab("部件编码", "T_Machlocatepart", "machlctpartcode", panelmachlocatepart, dataGridView1);
                            combPmachlctcode.Text = this.dataGridView1.SelectedRows[0].Cells["所属部位名称"].Value.ToString().Trim();
                            break;
                        }
                    case "TrbType":
                        {
                            s_trbcode.ReadOnly = true;
                            BD.Data_ToTab("故障类别代码", "T_TrbType", "trbcode", panelTrbType, dataGridView1);
                            break;
                        }
                    case "TrbTypeDtl":
                        {
                            s_trbcodedtl.ReadOnly = true;
                            // BD.Data_ToTab("故障类别明细代码", "T_TrbTypeDtl", "trbcodedtl", panelTrbTypeDtl, dataGridView1);
                            s_trbcodedtl.Text = dataGridView1.SelectedRows[0].Cells["故障类别明细代码"].Value.ToString();
                            s_trbcodename.Text = dataGridView1.SelectedRows[0].Cells["故障类别名称"].Value.ToString();
                            comboDtrbcode.Text = this.dataGridView1.SelectedRows[0].Cells["故障类别名称"].Value.ToString().Trim();
                            break;
                        }
                    case "TelTrbType":
                        {
                            s_telcode.ReadOnly = true;
                            BD.Data_ToTab("故障代码", "T_TelTrbType", "telcode", panelTelTrbType, dataGridView1);
                            break;
                        }
                    case "machproblem":
                        {
                            T_probcodeid.ReadOnly = true;
                            BD.Data_ToTab("系统编号", "T_Machproblem", "probcodeid", panelmachproblem, dataGridView1);
                            T_probcodeid.Text = this.dataGridView1.SelectedRows[0].Cells["系统编号"].Value.ToString().Trim();
                            comboPtelcode.Text = this.dataGridView1.SelectedRows[0].Cells["故障现象"].Value.ToString().Trim();
                            break;
                        }
                    default:
                        break;
                }
            }
            catch
            {
                return;
            }
        }
        #endregion


        #region //区域积分初始化部分
        private void button4_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_Areaid))
            {
                MessageBox.Show("编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_Areaid, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_Area))
            {
                MessageBox.Show("地区名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_Areaid, "Areaid", s_Area, "Area", this.panelAear, "T_AreaInf");
        }
        #endregion
        #region //重置
        private void button5_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_Areaid, panelAear);
        }
        #endregion

        #region //操作员类别初始化部分
        private void button7_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.n_TypeId))
            {
                MessageBox.Show("操作员类别编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_UTypeName))
            {
                MessageBox.Show("类别名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(n_TypeId, "TypeId", s_UTypeName, "UtypeName", this.panelUser, "T_UserType");
        }
        #endregion
        #region //重置
        private void button19_Click(object sender, EventArgs e)
        {
            BD.Resetdata(n_TypeId, panelUser);
        }
        #endregion

        #region //技术员分管地区初始化
        private void button9_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (((comboBoxUserName.SelectedIndex == 0) && (comboBoxArea.SelectedIndex == 0)) || ((comboBoxUserName.SelectedIndex != 0) && (comboBoxArea.SelectedIndex == 0)) ||
                ((comboBoxUserName.SelectedIndex == 0) && (comboBoxArea.SelectedIndex != 0)))
            {
                MessageBox.Show("请输入完整的信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion
            try
            {
                bool isExist;
                String sql_ = "", sqle_ = "";
                DBUtil dbUtil = new DBUtil();
                isExist = dbUtil.Is_Exist_Data("T_User_Area", "UAid", this.T_UAid.Text.Trim());
                if (T_UAid.ReadOnly)
                {
                    string strSqlWhere = "where UAid='" + T_UAid.Text.Trim() + "'";
                    sql_ = String.Format("select * from T_User_Area where (Ltrim(Rtrim(userid)) like '{0}' and Ltrim(Rtrim(uAreaid)) like '{1}') and Ltrim(Rtrim(UAid)) not like '{2}'", s_userid.Text.Trim(), s_UAreaid.Text.Trim(), T_UAid.Text.Trim());
                    sqle_ = (new InitFuncs()).Build_Update_Sql(this.panelUserArea, "T_User_Area", strSqlWhere);
                }
                else
                {
                    sql_ = String.Format("select * from T_User_Area where (Ltrim(Rtrim(userid)) like '{0}' and Ltrim(Rtrim(uAreaid)) like '{1}')", s_userid.Text.Trim(), s_UAreaid.Text.Trim());
                    sqle_ = (new InitFuncs()).Build_Insert_Sql(this.panelUserArea, "T_User_Area");
                }
                isExist = dbUtil.yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该记录已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                (new SqlDBConnect()).ExecuteNonQuery(sqle_);
                InitDataGridView("T_User_Area");
            }
            catch //(Exception ex)
            {
                MessageBox.Show("不能插入重复键！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region //技术员等级初始化
        private void button11_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_GId))
            {
                MessageBox.Show("等级编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_GId, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboxUtype.SelectedIndex == 0)
            {
                MessageBox.Show("类型名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_Gname))
            {
                MessageBox.Show("等级名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_GId, "GId", s_Gname, "Gname", this.panelUserGrade, "T_UserGrade");
        }
        #endregion
        #region //发票初始化定义
        private void button13_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_ITCode))
            {
                MessageBox.Show("发票编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_ITCode, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_ITName))
            {
                MessageBox.Show("发票类型名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.n_TaxRate))
            {
                MessageBox.Show("税率不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_ITCode, "ITCode", s_ITName, "ITName", this.panelInvoice, "T_Invoice");
        }
        #endregion
        #region //重置
        private void button22_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_ITCode, panelInvoice);
        }
        #endregion

        #region //物料卡片初始化
        private void button15_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_SortId))
            {
                MessageBox.Show("物料编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_SortId, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_SortName))
            {
                MessageBox.Show("物料名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_SortId, "SortId", s_SortName, "SortName", this.panelMaterial, "T_Material_Sort");
        }
        #endregion
        #region //重置
        private void button16_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_SortId, panelMaterial);
        }
        #endregion

        #region //客户重要度定义初始化
        private void button17_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_Iid))
            {
                MessageBox.Show("编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_Iid, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_importance))
            {
                MessageBox.Show("请填写该重要度", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_Iid, "Iid", s_importance, "importance", this.panelCustomerImp, "T_CustomerImp");
        }
        #endregion
        #region //重置
        private void button18_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_Iid, panelCustomerImp);
        }
        #endregion
        #region //重置
        private void button20_Click(object sender, EventArgs e)
        {
            BD.Resetdata(T_UAid, panelUserArea);
        }
        #endregion
        #region //重置
        private void button21_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_GId, panelUserGrade);
        }
        #endregion

        #region //关闭窗体
        private void SysEnviForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult dr = MessageBox.Show("确定要退出？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (dr == DialogResult.OK)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }
        #endregion
        #region //只允许输入小数点和数字(8为删除键)
        private void n_TaxRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region//右键的删除方法
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabParent.SelectedTab.Name == "SelItems")
            {
                try
                {
                    if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        BD.Delete(dataGridView2, "基础数据类型", "T_SelItems", "ItemType", panelSelItems);
                        InitDataGridView("T_SelItems");
                        return;
                    }
                }
                catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }

            switch (this.TabAdd.SelectedTab.Name)
            {
                case "BranchAdd":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "部门编号", "T_Branch", "BId", panelBranch);
                                InitDataGridView("T_Branch");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "Roles":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "角色编号", "T_Roles", "RoleId", panelRoles);
                                InitDataGridView("T_Roles");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "AreaInf":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "系统编号", "T_AreaInf", "Areaid", panelAear);
                                InitDataGridView("T_AreaInf");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "UserType":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "操作员类别编号", "T_UserType", "TypeId", panelUser);
                                InitDataGridView("T_UserType");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "User_Area":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "序列号", "T_User_Area", "UAid", panelUserArea);
                                InitDataGridView("T_User_Area");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "UserGrade":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "系统等级编码", "T_UserGrade", "GId", panelUserGrade);
                                InitDataGridView("T_UserGrade");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "StoreHouse":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "仓库编号", "T_StoreHouse", "SHId", panelStoreHouse);
                                InitDataGridView("T_StoreHouse");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "Invoice":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "发票类型编号", "T_Invoice", "ITCode", panelInvoice);
                                InitDataGridView("T_Invoice");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "Material_Sort":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "物料分类编号", "T_Material_Sort", "SortId", panelMaterial);
                                InitDataGridView("T_Material_Sort");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "PayMethod":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "支付编号", "T_PayMethod", "PMid", panelPayMethod);
                                InitDataGridView("T_PayMethod");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "CustomerImp":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "重要度编码", "T_CustomerImp", "Iid", panelCustomerImp);
                                InitDataGridView("T_CustomerImp");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "ServiceType":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "维修类别编码", "T_ServiceType", "scid", panelServiceType);
                                InitDataGridView("T_ServiceType");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "WorkType":
                    {
                        try
                        {
                            if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                BD.Delete(dataGridView1, "工作类别编码", "T_WorkType", "wcid", panelWorkType);
                                InitDataGridView("T_WorkType");
                            }
                        }
                        catch { MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        break;
                    }
                case "Holidays":
                    {
                        if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            BD.Delete(dataGridView1, "部位编码", "T_MachLocate", "machlctcode", panelMachLocate);
                            InitDataGridView("T_MachLocate");
                        }
                        break;
                    }
                case "MachLocate":
                    {
                        if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            BD.Delete(dataGridView1, "部位编码", "T_MachLocate", "machlctcode", panelMachLocate);
                            InitDataGridView("T_MachLocate");
                        }
                        break;
                    }
                case "machlocatepart":
                    {
                        if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            BD.Delete(dataGridView1, "部件编码", "T_machlocatepart", "machlctpartcode", panelmachlocatepart);
                            InitDataGridView("T_machlocatepart");
                        }
                        break;
                    }
                case "TrbType":
                    {
                        if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            BD.Delete(dataGridView1, "故障类别代码", "T_TrbType", "trbcode", panelTrbType);
                            InitDataGridView("T_TrbType");
                        }
                        break;
                    }
                case "TrbTypeDtl":
                    {
                        if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            BD.Delete(dataGridView1, "故障类别明细代码", "T_TrbTypeDtl", "trbcodedtl", panelTrbTypeDtl);
                            InitDataGridView("T_TrbTypeDtl");
                        }
                        break;
                    }
                case "TelTrbType":
                    {
                        if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            BD.Delete(dataGridView1, "故障代码", "T_TelTrbType", "telcode", panelTelTrbType);
                            InitDataGridView("T_TelTrbType");
                        }
                        break;
                    }
                case "machproblem":
                    {
                        if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            BD.Delete(dataGridView1, "系统编号", "T_machproblem", "probcodeid", panelmachproblem);
                            InitDataGridView("T_machproblem");
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        #endregion

        #region //基础数据类型定义的现实方法
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SqlDBConnect SDB = new SqlDBConnect();
            DBUtil dbUtil = new DBUtil();
            try
            {
                s_ItemVal.Clear();
                string strSql = "select * from T_SelItems where ItemType='{0}'";
                strSql = string.Format(strSql, s_ItemType.Text.Trim());
                DataSet ds = SDB.Get_Ds(strSql);
                int Lins = ds.Tables[0].Rows.Count;
                string[] match = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < match.Length; i++)
                {
                    s_ItemVal.Text += ds.Tables[0].Rows[i]["ItemVal"].ToString() + "\r\n";//回车换行
                }
            }
            catch
            {
                MessageBox.Show("当前行不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region //添加"基础数据类型"信息
        private void AddSell_Click(object sender, EventArgs e)
        {
            SqlDBConnect db = new SqlDBConnect();
            //使用事务处理
            List<string> sqls = new List<string>();
            string sql = "delete from T_SelItems where ItemType='{0}'";
            sql = string.Format(sql, s_ItemType.Text.Trim());
            sqls.Add(sql);
            int Lines = s_ItemVal.Lines.Length;
            if (s_ItemVal.Text.Trim() == "")//为空时
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql);
                return;
            }
            string[] value = new string[Lines - 1];
            for (int i = 0; i <= Lines - 1; i++)
            {
                if (s_ItemVal.Lines[i].ToString().Trim() == "")
                    continue;
                string strsql = "insert into T_SelItems(ItemType,ItemVal) values ('" + s_ItemType.Text.Trim() + "','" + s_ItemVal.Lines[i].ToString().Trim() + "')";
                sqls.Add(strsql);
            }

            try
            {
                db.Exec_Tansaction(sqls);
                MessageBox.Show("添加成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InitDataGridView("T_SelItems");
            }
            catch
            { }
        }
        #endregion

        #region //对应滚动条发生的事件
        private void Reaserch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDBConnect SDB = new SqlDBConnect();
                s_ItemVal.Clear();
                string strSql = "select ItemType as 基础数据类型,ItemVal as 选项数据 from T_SelItems where ItemType='{0}'";//"select * from T_SelItems where ItemType='{0}'";
                strSql = string.Format(strSql, s_ItemType.Text.Trim());
                DataSet ds = SDB.Get_Ds(strSql);
                dataGridView2.DataSource = ds.Tables[0];
                int Lins = ds.Tables[0].Rows.Count;
                string[] match = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < match.Length; i++)
                {
                    s_ItemVal.Text += ds.Tables[0].Rows[i]["选项数据"].ToString() + "\r\n";//回车换行
                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.ToString());
            }
        }
        #endregion

        #region //维修类别初始化
        private void butsave_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_scid))
            {
                MessageBox.Show("维修类别编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_scid, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_scName))
            {
                MessageBox.Show("维修类别名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_scid, "scid", s_scName, "scName", this.panelServiceType, "T_ServiceType");
        }
        #endregion

        #region //重置
        private void butreset_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_scid, panelServiceType);
        }
        #endregion

        #region //重置
        private void Reset_Click(object sender, EventArgs e)
        {
            s_ItemVal.Clear();
        }
        #endregion

        #region //工作类别定义初始化
        private void buttsave_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_wcid))
            {
                MessageBox.Show("编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_wcid, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (Util.ControlTextIsNUll(this.s_importance))
            //{
            //    MessageBox.Show("请填写该重要度", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            #endregion
            Pubmetheod(s_wcid, "wcid", s_wcName, "wcName", this.panelWorkType, "T_WorkType");
        }
        #endregion

        #region //重置
        private void buttreset_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_wcid, panelWorkType);
        }
        #endregion

        #region //通过回车转Tab的方法(先把Form的Keypreview事件设为True)
        private void SysEnviForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Util.Control_keypress(e);
            if (e.KeyChar == '\r')//(char)13
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                //SendKeys.Send("{TAB}");//也可以使用这个代替SelectNextControl
            }
        }
        #endregion

        #region //回车时焦点不变(特殊)
        private void s_ItemVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            //由于这里的特殊性(不需要回车就根据TabIndex的顺序来切换)
            if (e.KeyChar == '\r')
            {
                s_ItemVal.Focus();
            }
        }
        #endregion





        #region //系统基础信息初始化
        private void Syssave_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_SiteCode))
            {
                MessageBox.Show("站点代号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_SiteCode, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (Util.ControlTextIsNUll(this.s_CurWorkMonth))
            //{
            //    MessageBox.Show("当前工作月不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (Util.IsEmail(this.s_Semail.Text.Trim()) == false)
            {
                MessageBox.Show("邮箱输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_calcmethod))
            {
                MessageBox.Show("成本计算方法必须选择！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //try
            //{
            //    string sy = s_CurWorkMonth.Text.Trim().Substring(0, 4);
            //    string sm = s_CurWorkMonth.Text.Trim().Substring(4, 2);
            //    Convert.ToDateTime(sy+"-"+sm+"-01 12:01:01");
            //}
            //catch
            //{
            //    MessageBox.Show("工作月输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    s_CurWorkMonth.Focus();
            //    return;
            //}

            #endregion
            try
            {
                bool isExist;
                String sql_ = "", sqle_ = "";
                DBUtil dbUtil = new DBUtil();
                sql_ = "select SiteCode from T_SysConfig";
                isExist = (new DBUtil()).yn_exist_data(sql_);
                if (!isExist)
                    sqle_ = (new InitFuncs()).Build_Insert_Sql(this.panelSysConfig, "T_SysConfig");
                else
                    sqle_ = (new InitFuncs()).Build_Update_Sql(this.panelSysConfig, "T_SysConfig", "");
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(sqle_);
                    MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    InitDataGridView("T_SysConfig");
                }
                catch
                {
                    MessageBox.Show("保存失败！");
                    return;
                }

            }
            catch (Exception ef)
            {
                MessageBox.Show("操作失败！" + '\n' + ef.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion


        #region //通过条件查找用户名
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                WFilter wf = new WFilter(0, "UserName",true);
                wf.StartPosition = FormStartPosition.CenterScreen;
                wf.tableName = "T_users";    //表名 
                wf.strSql = "select UserId as 用户编码, UserName as 用户名, ynAdmin as 是否系统管理员,BranchId as 部门编码," +
                        "JobPosition as 职位, atGroup as 组别, DefaultUserType as 类别,SmsTel as 接收短信电话号码 " +
                        "from [T_Users] ";

                wf.s_items.Add("编码,UserId,C");
                wf.s_items.Add("用户名,UserName,C");
                wf.s_items.Add("职位,JobPosition,C");
                wf.s_items.Add("组别,atGroup,C");
                wf.ShowDialog();

                if (wf.DialogResult == DialogResult.OK)
                {
                    this.comboxLeader.Text = wf.Return_Items[0].Trim();
                }
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region //通过条件查找用户名
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                WFilter wf = new WFilter(0, "UserName", true);
                wf.StartPosition = FormStartPosition.CenterScreen;
                wf.tableName = "T_users";    //表名 
                wf.strSql = "select UserId as 用户编码, UserName as 用户名, ynAdmin as 是否系统管理员,BranchId as 部门编码," +
                        "JobPosition as 职位, atGroup as 组别, DefaultUserType as 类别,SmsTel as 接收短信电话号码 " +
                        "from [T_Users] ";

                wf.s_items.Add("编码,UserId,C");
                wf.s_items.Add("用户名,UserName,C");
                wf.s_items.Add("职位,JobPosition,C");
                wf.s_items.Add("组别,atGroup,C");
                wf.ShowDialog();

                if (wf.DialogResult == DialogResult.OK)
                {
                    this.comboBoxUserName.Text = wf.Return_Items[0].Trim();
                }
            }
            catch
            {
                MessageBox.Show("请正确操作!", "提示");
            }
        }
        #endregion

        #region //通过判断tabParent.SelectedIndex的值来初始化"基础类型定义表"
        private void tabParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabParent.SelectedIndex == 0)
            {
                DBUtil dbUtil = new DBUtil();
                ////在窗口中初始化T_SysConfig
                InitFuncs inf = new InitFuncs();
                string sel_sql = "select * from T_SysConfig ";
                inf.ShowDatas(this.panelSysConfig, sel_sql);
                // 特殊字段处理
                string siteId = dbUtil.Get_Single_val("T_SysConfig", "DefaStoreHouseId", "SiteCode", this.s_SiteCode.Text.Trim());
                comboBoxstore.Text = dbUtil.Get_Single_val("T_StoreHouse", "SHName", "SHId", siteId);
                //if (s_SiteCode.Text.Trim() != "")
                //{
                //    s_SiteCode.ReadOnly = true;
                //}
                this.dataGridView1.DataSource = null;

            }
            else
                if (this.tabParent.SelectedIndex == 2)
                {
                    //在窗口中初始化T_SelItems
                    InitDataGridView("T_SelItems");
                    //升序排序
                    string sql = "select distinct ItemType from T_SelItems order by 1";
                    DataTable dt = (new SqlDBConnect()).Get_Dt(sql);
                    return;
                }
        }
        #endregion

        private void comboxLeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_HeadId.Text = (new DBUtil()).Get_Single_val("T_Users", "userid", "UserName", comboxLeader.Text.Trim());
        }

        private void comboBoxUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_userid.Text = (new DBUtil()).Get_Single_val("T_Users", "UserId", "UserName", this.comboBoxUserName.Text.Trim());
        }

        private void comboBoxArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_UAreaid.Text = (new DBUtil()).Get_Single_val("T_AreaInf", "Areaid", "Area", this.comboBoxArea.Text.Trim());
        }

        private void comboxUtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_Utype.Text = (new DBUtil()).Get_Single_val("T_UserType", "TypeId", "UTypeName", this.comboxUtype.Text.Trim()).ToString();
        }

        #region //节假日初始化
        private void buttonsave_Click(object sender, EventArgs e)
        {
            bool isExist;
            String sql_ = "", sqle_ = "";
            DBUtil dbUtil = new DBUtil();
            sql_ = (new InitFuncs()).Build_Insert_Sql(this.panelHolidays, "T_Holidays");
            sqle_ = String.Format("select * from T_Holidays where Ltrim(Rtrim(Holiday)) like '{0}'", s_Holiday.Text.Trim());
            isExist = dbUtil.yn_exist_data(sqle_);
            if (isExist)
            {
                MessageBox.Show("该记录已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            (new SqlDBConnect()).ExecuteNonQuery(sql_);
            InitDataGridView("T_Holidays");
        }
        #endregion

        #region //操作定义初始化
        private void rolesave_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_RoleId))
            {
                MessageBox.Show("角色编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_RoleId, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_RoleName))
            {
                MessageBox.Show("角色名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_RoleId, "RoleId", s_RoleName, "RoleName", this.panelRoles, "T_Roles");
        }
        #endregion

        private void rolereset_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_RoleId, panelRoles);
        }

        private void storereset_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_SHId, panelStoreHouse);
        }

        private void Payreset_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_PMid, panelPayMethod);
        }

        private void comboBoxSHKeeper_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_SHKeeper.Text = (new DBUtil()).Get_Single_val("T_Users", "userid", "UserName", this.comboBoxSHKeeper.Text.Trim());
        }

        #region //账款支付方式初始化
        private void Paysave_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_PMid))
            {
                MessageBox.Show("支付编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_PMid, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_PMName))
            {
                MessageBox.Show("请填写支付方式！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_PMid, "PMid", s_PMName, "PMName", this.panelPayMethod, "T_PayMethod");
        }
        #endregion

        #region //仓库定义初始化
        private void storesave_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_SHId))
            {
                MessageBox.Show("仓库编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_SHId, 4))
            {
                MessageBox.Show("编码位数不能超出四位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_SHName))
            {
                MessageBox.Show("仓库名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.IsEmail(this.s_Email.Text.Trim()) == false)
            {
                MessageBox.Show("邮箱输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (Util.IsPhoneNumber(this.s_Tel, 11) == false)
            //{
            //    MessageBox.Show("电话号码输入错误！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            #endregion
            Pubmetheod(s_SHId, "SHId", s_SHName, "SHName", this.panelStoreHouse, "T_StoreHouse");
        }
        #endregion

        #region //通过条件查找用户名
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                WFilter wf = new WFilter(0, "UserName", true);
                wf.StartPosition = FormStartPosition.CenterScreen;
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
                    this.comboBoxSHKeeper.Text = wf.Return_Items[0].Trim();
                }
            }
            catch
            {
                MessageBox.Show("请正确操作!", "提示");
            }
        }
        #endregion

        private void comboBoxstore_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_DefaStoreHouseId.Text = (new DBUtil()).Get_Single_val("T_StoreHouse", "SHId", "SHName", this.comboBoxstore.Text.Trim());
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_Sortcode.Text = (new DBUtil()).Get_Single_val("T_Material_Sort", "SortId", "SortName", this.comboBoxSort.Text.Trim());
        }

        #region //故障信息部分初始化
        private void button36_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_Pmachlctcode))
            {
                MessageBox.Show("编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Util.IsContainCharNumber(this.s_Pmachlctcode, 5))
            {
                MessageBox.Show("编码位数不能超出五位！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_machlctname))
            {
                MessageBox.Show("部位名称为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            //Pubmetheod(s_machlctcode, "machlctcode", s_machlctname, "machlctname", this.panelMachLocate, "T_MachLocate");
            try
            {
                bool isExist;
                String sql_ = "", sqle_ = "";
                DBUtil dbUtil = new DBUtil();
                if (s_Pmachlctcode.ReadOnly)
                {
                    string strSqlWhere = "where machlctcode ='" + s_Pmachlctcode.Text.Trim() + "'";
                    sql_ = String.Format("select * from T_MachLocate where Ltrim(Rtrim(machlctname)) like '{0}' and Ltrim(Rtrim(machlctcode)) not like '{1}'", s_machlctname.Text.Trim(), s_Pmachlctcode.Text.Trim());
                    sqle_ = "update T_MachLocate set machlctname='" + s_machlctname.Text.Trim() + "',memo='" + s_Macmemo.Text.Trim() + "'" +
                            " where machlctcode='" + s_Pmachlctcode.Text.Trim() + "'";
                }
                else
                {
                    sql_ = String.Format("select * from T_MachLocate where Ltrim(Rtrim(machlctname)) like '{0}' ", s_machlctname.Text.Trim());
                    sqle_ = "insert into T_MachLocate(machlctcode,machlctname,memo)values('" + s_Pmachlctcode.Text.Trim() + "','" + s_machlctname.Text.Trim() + "','" + s_Macmemo.Text.Trim() + "')";
                }
                isExist = dbUtil.yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该条信息已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    s_machlctname.Focus();
                    return;
                }
                (new SqlDBConnect()).ExecuteNonQuery(sqle_);
                InitDataGridView("T_MachLocate");
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("不能插入重复键！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void combPmachlctcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_machlctcode.Text = (new DBUtil()).Get_Single_val("T_MachLocate", "machlctcode", "machlctname", this.combPmachlctcode.Text.Trim());
        }

        private void button39_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_machlctpartcode))
            {
                MessageBox.Show("编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_machlctpartname))
            {
                MessageBox.Show("部件名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_machlctpartcode, "machlctpartcode", s_machlctpartname, "machlctpartname", this.panelmachlocatepart, "T_machlocatepart");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_trbcode))
            {
                MessageBox.Show("编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_trbmemo))
            {
                MessageBox.Show("故障类别名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_trbcode, "trbcode", s_trbmemo, "trbmemo", this.panelTrbType, "T_TrbType");
        }

        private void comboDtrbcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_Dtrbcode.Text = (new DBUtil()).Get_Single_val("T_TrbType", "trbcode", "trbmemo", this.comboDtrbcode.Text.Trim());
        }

        private void button45_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_trbcodedtl))
            {
                MessageBox.Show("编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_trbcodename))
            {
                MessageBox.Show("故障类别名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            //Pubmetheod(s_trbcodedtl, "trbcodedtl", s_trbcodename, "trbcodename", this.panelTrbTypeDtl, "T_TrbTypeDtl");
            try
            {
                bool isExist;
                String sql_ = "", sqle_ = "";
                DBUtil dbUtil = new DBUtil();
                if (s_trbcodedtl.ReadOnly)
                {
                    string strSqlWhere = "where trbcodedtl ='" + s_trbcodedtl.Text.Trim() + "'";
                    sql_ = String.Format("select * from T_TrbTypeDtl where Ltrim(Rtrim(trbcodename)) like '{0}' and Ltrim(Rtrim(trbcodedtl)) not like '{1}'",
                        s_trbcodename.Text.Trim(), s_trbcodedtl.Text.Trim());
                    sqle_ = "update T_TrbTypeDtl set trbcodename='" + s_trbcodename.Text.Trim() + "',trbcode='" + s_Dtrbcode.Text.Trim() + "'" +
                            " where trbcodedtl='" + s_trbcodedtl.Text.Trim() + "'";
                }
                else
                {
                    sql_ = String.Format("select * from T_TrbTypeDtl where Ltrim(Rtrim(trbcodename)) like '{0}' ", s_trbcodename.Text.Trim());
                    sqle_ = "insert into T_TrbTypeDtl(trbcodedtl,trbcodename,trbcode)values('" + s_trbcodedtl.Text.Trim() + "','" +
                        s_trbcodename.Text.Trim() + "','" +
                        s_Dtrbcode.Text.Trim() + "')";
                }
                isExist = dbUtil.yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该条信息已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    s_trbcodename.Focus();
                    return;
                }
                (new SqlDBConnect()).ExecuteNonQuery(sqle_);
                InitDataGridView("T_TrbTypeDtl");
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("不能插入重复键！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            #region 输入验证
            if (Util.ControlTextIsNUll(this.s_telcode))
            {
                MessageBox.Show("编码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Util.ControlTextIsNUll(this.s_telmemo))
            {
                MessageBox.Show("故障现象不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            Pubmetheod(s_telcode, "telcode", s_telmemo, "telmemo", this.panelTelTrbType, "T_TelTrbType");
        }

        private void button51_Click(object sender, EventArgs e)
        {
            try
            {
                bool isExist;
                String sql_ = "", sqle_ = "";
                DBUtil dbUtil = new DBUtil();
                isExist = dbUtil.Is_Exist_Data("T_machproblem", "probcodeid", this.T_probcodeid.Text.Trim());
                if (T_probcodeid.ReadOnly)
                {
                    string strSqlWhere = "where probcodeid='" + T_probcodeid.Text.Trim() + "'";
                    sql_ = String.Format("select * from T_machproblem where Ltrim(Rtrim(probname)) like '{0}'and Ltrim(Rtrim(probcodeid)) not like '{1}'",
                    s_probname.Text.Trim(), T_probcodeid.Text.Trim());
                    sqle_ = "update T_machproblem set probname='" + s_probname.Text.Trim() + "'," +
                           "telcode ='" + s_Ptelcode.Text.Trim() + "' where probcodeid='" + T_probcodeid.Text.Trim() + "'";
                }
                else
                {
                    sql_ = String.Format("select * from T_machproblem where (Ltrim(Rtrim(probname)) like '{0}')", s_probname.Text.Trim());
                    sqle_ = "insert into T_machproblem(telcode,probname)values('" + s_Ptelcode.Text.Trim() + "','" + s_probname.Text.Trim() + "')";
                }
                isExist = dbUtil.yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该记录已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                (new SqlDBConnect()).ExecuteNonQuery(sqle_);
                InitDataGridView("T_machproblem");
            }
            catch //(Exception ex)
            {
                MessageBox.Show("不能插入重复键！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonreset_Click(object sender, EventArgs e)
        {
        }

        private void button35_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_Pmachlctcode, panelMachLocate);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_machlctpartcode, panelmachlocatepart);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_trbcode, panelTrbType);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_trbcodedtl, panelTrbTypeDtl);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            BD.Resetdata(s_telcode, panelTelTrbType);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            BD.Resetdata(T_probcodeid, panelmachproblem);
        }

        private void comboPtelcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            s_Ptelcode.Text = (new DBUtil()).Get_Single_val("T_TelTrbType", "telcode", "telmemo", this.comboPtelcode.Text.Trim());
        }
        #endregion
    }
}
