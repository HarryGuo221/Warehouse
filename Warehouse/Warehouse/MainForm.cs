using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Sys;
using Warehouse.Modal;
using Warehouse.DAO;
using Warehouse.DB;
using Warehouse.Stock;
using Warehouse.Customer;
using Warehouse.Base;
using Warehouse.Bargain;
using Warehouse.Receipt;
using Warehouse.StockReport;
using grproLib;
using System.Data.SqlClient;
using Warehouse.Financial;
using AxgrproLib;

namespace Warehouse
{
    public partial class MainForm : Form
    {
        public string loginTime;     //登陆时间()

        public string userId;        //登陆用户ID
        public string userName;
        public string curWorkMonth;  //当前工作月

        public MainForm(string userId,string workMonth) 
        {
            InitializeComponent();

            this.userId = userId;
            this.curWorkMonth = workMonth;
            this.userName = (new DBUtil()).Get_Single_val("T_Users", "UserName", "UserId", this.userId);
            
            //记录登陆信息
            loginTime = DBUtil.getServerTime().ToString();
            string sql_ = "insert into T_LogHist(UserName,LoginTime) values('"
                + this.userName + "','" + loginTime + "')";
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
            }
            catch
            { }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //显示相关信息
            this.toolStripStatusLabelUserName.Text = this.userName;
            this.toolStripStatusLabe_CurWorkMonth.Text = "当前工作月：" + this.curWorkMonth;
            //设置菜单项
            InitMenuStripMain();
            InitMenuStripChild();
            //设置当前登录用户的菜单项
            if (this.userId != "admin") //超级管理员不限制
                InitCurUserMenuStrip();
        }
        /// <summary>
        /// 初始化主菜单项
        /// </summary>
        private void InitMenuStripMain()
        {
            string mainMenu = "";
            string shortCut = "";
            int orderIndex = -1;
            DataTable dt = MenuCfgMainDAO.GetDatasToMainMenu();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows)
            {
                mainMenu = dr["MainMenu"].ToString().Trim();
                shortCut = dr["ShortCut"].ToString().Trim();
                if (dr["OrderIndex"].ToString().Trim() != "")
                    orderIndex = Convert.ToInt32(dr["OrderIndex"].ToString().Trim());

                //添加到菜单条                
                ToolStripMenuItem item = new ToolStripMenuItem(mainMenu);
                if (shortCut != "")
                    item.Text = mainMenu + "(&" + shortCut + ")";
                item.Click += new EventHandler(item_Click);

                if (orderIndex == -1)
                    this.menuStripMain.Items.Insert(this.menuStripMain.Items.Count, item);//插入到最后
                else
                    this.menuStripMain.Items.Insert(orderIndex, item);
            }
        }
        /// <summary>
        /// 初始化子菜单
        /// </summary>
        private void InitMenuStripChild()
        {
            int mainMenuId = -1;
            string subMenu = "";
            string shortCut = "";
            int orderIndex = -1;
            DataTable dt = MenuCfgChildDAO.GetDatasToChildMenu();
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["MainMenuId"].ToString().Trim() != "")
                    mainMenuId = Convert.ToInt32(dr["MainMenuId"].ToString().Trim());
                subMenu = dr["SubMenu"].ToString().Trim();
                shortCut = dr["ShortCut"].ToString().Trim();
                if (dr["OrderIndex"].ToString().Trim() != "")
                    orderIndex = Convert.ToInt32(dr["OrderIndex"].ToString().Trim());
                if (mainMenuId == -1)
                    continue;
                //获得子菜单对应的单据模板编号
                string strReceTypeId = subMenu;
                string strReceName = (new DBUtil()).Get_Single_val("T_ReceiptModal", "ReceName", "ReceTypeID", strReceTypeId);

                //添加到主菜单项                
                ToolStripMenuItem item = new ToolStripMenuItem(strReceName, null, null, strReceTypeId);
                if (shortCut != "")
                    item.Text = strReceName + "(&" + shortCut + ")";
                item.Click += new EventHandler(item_Click);


                int intOrderIndexMainMenu = -1;//主菜单项顺序号
                string strOrderIndexMainMenu = (new DBUtil()).Get_Single_val("T_MenuCfgMain", "OrderIndex", "Sysid", mainMenuId);
                if (strOrderIndexMainMenu != "")
                    intOrderIndexMainMenu = Convert.ToInt32(strOrderIndexMainMenu);

                if (intOrderIndexMainMenu == -1)
                    (this.menuStripMain.Items[this.menuStripMain.Items.Count - 1] as ToolStripMenuItem).DropDownItems.Insert(orderIndex - 1, item);
                else
                    (this.menuStripMain.Items[intOrderIndexMainMenu] as ToolStripMenuItem).DropDownItems.Insert(orderIndex - 1, item);
            }
        }
        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.DropDownItems.Count > 0) //有子菜单的菜单项，不处理
                return;

            string strReceTypeId = item.Name.Trim();
            ListModalForm listModalForm = new ListModalForm(strReceTypeId, "add", "", "");
            listModalForm.curWorkMonth = this.curWorkMonth;
            listModalForm.curUserId = this.userId;
            listModalForm.MdiParent = this;
            listModalForm.Show();
        }

        #region 设置当前登录用户的菜单项
        /// <summary>
        /// 设置当前登录用户的菜单项
        /// </summary>
        private void InitCurUserMenuStrip()
        {
            string strSql = "select T_Role_Rights.[Function] from T_User_Role,T_Role_Rights " +
                            "where T_User_Role.UserId='{0}' and T_User_Role.RoleId=T_Role_Rights.RoleId";
            strSql = string.Format(strSql, this.userId);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                string function = dr["Function"].ToString().Trim();

                foreach (ToolStripMenuItem item in this.menuStripMain.Items)
                {
                    SetCurUserMenuStrip(item, function);
                }
            }
            foreach (ToolStripMenuItem item in this.menuStripMain.Items)
            {
                SetCurUserMenuStrip(item);
            }
        }
        private void SetCurUserMenuStrip(ToolStripMenuItem menuItem, string MenuItemName)
        {
            if (menuItem.Text.Equals(MenuItemName)) //找到菜单项
                menuItem.Enabled = false;

            for (int i = 0; i < menuItem.DropDownItems.Count; i++)
            {
                if (menuItem.DropDownItems[i] is ToolStripSeparator)
                    continue;
                else
                {
                    SetCurUserMenuStrip((ToolStripMenuItem)menuItem.DropDownItems[i], MenuItemName);
                }
            }
        }
        private void SetCurUserMenuStrip(ToolStripMenuItem menuItem)
        {
            if (menuItem.DropDownItems.Count <= 0 && menuItem.Enabled == true)
            {
                menuItem.Visible = false;
            }
            if (menuItem.DropDownItems.Count <= 0 && menuItem.Enabled == false)
            {
                menuItem.Enabled = true;
            }
            else
            {
                for (int i = 0; i < menuItem.DropDownItems.Count; i++)
                {
                    if (menuItem.DropDownItems[i] is ToolStripSeparator)
                        continue;
                    else
                        SetCurUserMenuStrip((ToolStripMenuItem)menuItem.DropDownItems[i]);
                }
            }
        }
        #endregion
        /// <summary>
        ///报表通用连接查询语句
        /// </summary>
        public void Connect(string strSql, string receiname)
        {
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = new DataTable();
            dt = db.Get_Dt(strSql);
            string count = dt.Rows.Count.ToString();
            //通过sql条件显示报表窗体
            PublicDetailForm form = new PublicDetailForm();
            form.MdiParent = this;
            form.Show();
            form.ReiceName = receiname;
            form.Username = userName;
            form.strSql = strSql;
            form.count = count;
            form.ShowReport();
        }

        private void 操作用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersForm usersForm = new UsersForm(this.menuStripMain);
            usersForm.MdiParent = this;
            usersForm.Show();
        }
        private void 操作角色定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RolesForm rolesForm = new RolesForm(menuStripMain);
            rolesForm.MdiParent = this;
            rolesForm.Show();
        }
        private void 客户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerInf customer = new CustomerInf();
            customer.MdiParent = this;
            customer.Show();
        }

       
        //private void testFormToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    TestForm testForm = new TestForm();
        //    testForm.MdiParent = this;
        //    testForm.Show();//
        //}

        private void 系统环境设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void 单据模板定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiptModalDefineForm receiptModalDefineForm = new ReceiptModalDefineForm();
            receiptModalDefineForm.MdiParent = this;
            receiptModalDefineForm.Show();
        }

        private void 主菜单单据模板设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuConfigMainForm form = new MenuConfigMainForm(this.menuStripMain);
            form.MdiParent = this;
            form.Show();
        }

        private void 子菜单单据模板设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuConfigChildForm form = new MenuConfigChildForm(this.menuStripMain);
            form.MdiParent = this;
            form.Show();
        }

        private void 单据模板可操作项设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiptModalConfigForm form = new ReceiptModalConfigForm();
            form.MdiParent = this;
            form.Show();
        }

        private void 物料卡片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Material_info Matinfo = new Material_info();
            Matinfo.MdiParent = this;
            Matinfo.Show();
        }

        private void 合同ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BargainsForm form = new BargainsForm();
            form.MdiParent = this;
            form.Show();
        }

        private void 库存卡片管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Material_info Matinfo = new Material_info();
            Matinfo.MdiParent = this;
            Matinfo.Show();
        }

        private void 单据查询ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string strSql = "select T_Receipt_Main.ReceiptId as 单据号,T_Receipt_Main.CustomerReceiptNo as 自定义单据号, T_Receipt_Main.ReceiptTypeID as 单据类别, " +
                            "CurWorkMonth as 工作年月, OccurTime as 单据日期, CustId as 客户编码, CustName as 客户名称, InvoiceNo as 发票号,SourceStoreH as 仓库编号 "+
                            "from T_Receipt_Main,T_Receipts_Det,T_MatInf "+
                            "where T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and T_Receipts_Det.MatId=T_MatInf.MatID ";
            
            //添加查找窗体           
            WFilter wf = new WFilter(0, "单据号", true);
            wf.strSql = strSql;
            wf.s_items.Add("单据类别,T_Receipt_Main.ReceiptTypeID,C");
            wf.s_items.Add("单据号,T_Receipt_Main.ReceiptId,C");
            wf.s_items.Add("自定义单据号,T_Receipt_Main.CustomerReceiptNo,C");
            wf.s_items.Add("客户编码,CustId,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("单据日期,OccurTime,N");
            wf.s_items.Add("物料编码,T_Receipts_Det.MatId,C");
            wf.s_items.Add("物料名称,T_MatInf.MatName,C");           
            wf.btnOK.Enabled = false;
            wf.ShowDialog();


            if (wf.DialogResult == DialogResult.OK)
            {
                wf.Return_Sql += " order by OccurTime";
                ReceiptQueryForm form = new ReceiptQueryForm(wf.Return_Sql, 0);
                form.curWorkMonth = this.curWorkMonth;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChgPsw fcp = new FormChgPsw();
            fcp.UserId = this.userId;
            fcp.ShowDialog();
        }

        //private void 客户召唤受理ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //CustomCallList ccl = new CustomCallList();
        //    //ccl.CurrentUser = this.userId; //传入UserId
        //    //ccl.MdiParent = this;
        //    //ccl.Show();
        //    //ccl.WindowState = FormWindowState.Maximized;
        //    Formorder fo = new Formorder();
        //    fo.CurrentUser = this.userId;  //传入UserId
        //    fo.MdiParent = this;
        //    fo.WindowState = FormWindowState.Maximized;
        //    fo.Show();
        //}

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormCustMatLst fm = new FormCustMatLst();
            fm.MdiParent = this;
            fm.Show();
        }

        private void 未核销单据查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotVerificateReceiptForm form = new NotVerificateReceiptForm(this);
            form.MdiParent = this;
            form.Show();
            form.InitDataGridViewDetail(0);
        }

        private void 用户报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserReportForm form = new UserReportForm();
            form.MdiParent = this;
            form.Show();
        }
        //查询未核销估价明细
        private void 未核销估价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string DataSql = "select * from T_NotOffset where T_NotOffset.单据日期>'2003-12-31' and T_NotOffset.未核销数量<>0 ";

            //查询窗体
            WFilter wf = new WFilter(0, "单据号", false);
            wf.strSql = DataSql;

            wf.s_items.Add("单据号," + "T_NotOffset" + ".单据号,C");
            wf.s_items.Add("客户名称," + "T_NotOffset" + ".客户名称,C");
            wf.s_items.Add("单据日期," + "T_NotOffset" + ".单据日期,N");

            wf.btnOK.Enabled = false;
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                string StrSql = wf.Return_Sql + " order by T_NotOffset.单据日期 ";
                DataTable DT = (new SqlDBConnect()).Get_Dt(StrSql);
                if (DT.Rows.Count <= 0)
                {
                    MessageBox.Show("未找到与所选条件相关的数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                NotOffSetForm form = new NotOffSetForm(StrSql);
                form.MdiParent = this;
                form.Show();
            }
        }

        #region//进销存报表
        private void 进销存明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WFilter wf = new WFilter(0, "MatID", true);
            //wf.tableName = "T_MatInf";    //表名   
            wf.strSql = "SELECT  dbo.T_Receipt_Main_Det.MatId 物料编号," +
                        "T_Receipt_Main_Det.SourceStoreH AS 仓库编号," +
                        "CONVERT(char(10), dbo.T_Receipt_Main_Det.OccurTime, 121) AS 单据日期," +
                        "dbo.T_ReceiptModal.ReceName AS 单据名称," +
                        " dbo.T_Receipt_Main_Det.ReceiptId AS 单据编号, " +
                        " dbo.T_Receipt_Main_Det.InvoiceNO AS 发票号," +
                        " dbo.T_CustomerInf.CustName AS 客户名称, " +
                        " dbo.T_Receipt_Main_Det.MatType AS 物料类型  FROM  dbo.T_Receipt_Main_Det LEFT OUTER JOIN" +
                        " dbo.T_CustomerInf ON dbo.T_CustomerInf.CustID = dbo.T_Receipt_Main_Det.CustID INNER JOIN" +
                        " dbo.T_ReceiptModal ON dbo.T_Receipt_Main_Det.ReceiptTypeID = dbo.T_ReceiptModal.ReceTypeID ";

            wf.s_items.Add("物料编号,T_Receipt_Main_Det.MatID,C");
            wf.s_items.Add("仓库编号,T_Receipt_Main_Det.SourceStoreH,C");
            wf.s_items.Add("物料类型,T_Receipt_Main_Det.MatType,C");
            wf.s_items.Add("单据日期,T_Receipt_Main_Det.OccurTime,N");

            //wf.button3.Enabled = false;//
            //wf.btnOK.Enabled = false;
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                string strSql = wf.Return_Sql;

                string strSqlWhere = "";
                if (strSql.IndexOf(" where ") != -1)
                {
                    strSqlWhere = strSql.Substring(strSql.IndexOf(" where "));
                }
                PurchaseSSForm form = new PurchaseSSForm(strSqlWhere);
                form.MdiParent = this;
                form.Show();

                form.progressBar1.Maximum = 7;
                form.progressBar1.Value = 0;
                form.build_dt();
                form.Produce_dt();
                form.progressBar1.Value = 0;
            }
        }
        //对取出值为空的量赋零值
        public static double YNdbnull(string DTelement)
        {
            object o = DTelement;
            if (o != DBNull.Value && DTelement != "")
            {
                return Convert.ToDouble(DTelement);
            }
            else
                return 0;
        }
        #endregion

        
        private void 单据报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiptReportForm form = new ReceiptReportForm();
            form.MdiParent = this;
            form.Show();
        }

        private void 系统设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SysEnviForm sys = new SysEnviForm(this.menuStripMain);
            sys.MdiParent = this;
            sys.Show();
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            Application.Exit();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FormCopySettleInvoiceLst fl = new FormCopySettleInvoiceLst();
            fl.OpaMethod = "1";   //开票登记
            fl.MdiParent = this;
            fl.Show();
        }

        //private void 库存盘点表ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    string TempName = "R_StoCheck" + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString();
        //    //获取上期结存月

        //    string MaxBaTime = StockStatusDAO.GetBalanceTime();//本月

        //    string sqlPreTime = "";//前期发生时间段

        //    string TableInName = TempName + "IN";
        //    string TableOutName = TempName + "Out";
        //    string TempPreName = TempName + "tempPre";
        //    string StoCheckAll_name = TempName + "_ALL";
        //    //查询窗体
        //    WFilter wf = new WFilter(0, "", false);
        //    string Select_Data = "select distinct * from " + StoCheckAll_name;

        //    wf.strSql = Select_Data;
        //    wf.s_items.Add("物料编号," + StoCheckAll_name + ".物料编号,C");
        //    wf.s_items.Add("物料名称," + StoCheckAll_name + ".物料名称,C");
        //    wf.s_items.Add("仓库编号," + StoCheckAll_name + ".仓库编号,C");
        //    wf.s_items.Add("物料类型," + StoCheckAll_name + ".物料类型,C");
        //    wf.s_items.Add("期初数量," + StoCheckAll_name + ".期初数量,N");

        //    wf.s_items.Add("收入数量," + StoCheckAll_name + ".收入数量,N");
        //    wf.s_items.Add("发出数量," + StoCheckAll_name + ".发出数量,N");
        //    wf.s_items.Add("结存数量," + StoCheckAll_name + ".结存数量,N");
        //    wf.s_items.Add("结存成本," + StoCheckAll_name + ".结存成本,N");
        //    wf.s_items.Add("单据日期," + StoCheckAll_name + ".单据日期,N");
        //    wf.s_items.Add("工作年月," + StoCheckAll_name + ".工作年月,N");

        //    wf.btnOK.Enabled = false;
        //    wf.ShowDialog();

        //    string sqlwhere = " where ";//非时间条件
        //    string sqlwhen = " where ";  //时间条件
        //    #region//键值对返回值
        //    if (wf.DialogResult == DialogResult.OK)
        //    {

        //        Dictionary<string, Dictionary<string, string>> return_Fileds_Values = wf.Return_Fileds_Values;

        //        int circletimes = 0;//记录外层foreach循环次数
        //        int Timecircletimes = 0;

        //        foreach (string keys in return_Fileds_Values.Keys)
        //        {
        //            Dictionary<string, string> return_values = return_Fileds_Values[keys];
        //            string[] ConditionC = { "包含", "等于" };
        //            string[] ConditionN = { ">", "<", ">=", "<=", "!=", "=", "范围" };

        //            if (circletimes >= 1 && keys.ToString().IndexOf("单据日期") == -1 && keys.ToString().IndexOf("工作年月") == -1)
        //            {
        //                sqlwhere += " and ";
        //            }
        //            if (Timecircletimes >= 1 && keys.ToString().IndexOf("物料编号") == -1 && keys.ToString().IndexOf("物料名称") == -1
        //                && keys.ToString().IndexOf("仓库编号") == -1 && keys.ToString().IndexOf("物料类型") == -1
        //                && keys.ToString().IndexOf("期初数量") == -1 && keys.ToString().IndexOf("收入数量") == -1
        //                && keys.ToString().IndexOf("发出数量") == -1 && keys.ToString().IndexOf("结存数量") == -1
        //                && keys.ToString().IndexOf("结存成本") == -1)
        //            {
        //                sqlwhen += " and ";
        //            }

        //            foreach (string itemKey in return_values.Keys)
        //            {
        //                if (return_values != null && return_values.Count > 0)
        //                {
        //                    //物料编号
        //                    if (keys.ToString().IndexOf("物料编号") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf("包含") != -1)
        //                        {
        //                            sqlwhere += " 物料编号 like '%" + return_values["包含"].ToString() + "%'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("等于") != -1)
        //                        {
        //                            sqlwhere += " 物料编号 ='" + return_values["等于"].ToString() + "'";
        //                        }
        //                        circletimes++;
        //                    }
        //                    //物料名称
        //                    else if (keys.ToString().IndexOf("物料名称") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf("包含") != -1)
        //                        {
        //                            sqlwhere += "物料名称 like '%" + return_values["包含"].ToString() + "%'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("等于") != -1)
        //                        {
        //                            sqlwhere += "物料名称 ='" + return_values["等于"].ToString() + "'";
        //                        }
        //                        circletimes++;
        //                    }
        //                    //仓库编号
        //                    else if (keys.ToString().IndexOf("仓库编号") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf("包含") != -1)
        //                        {
        //                            sqlwhere += "仓库编号 like '%" + return_values["包含"].ToString() + "%'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("等于") != -1)
        //                        {
        //                            sqlwhere += "仓库编号 ='" + return_values["等于"].ToString() + "'";
        //                        }
        //                        circletimes++;
        //                    }
        //                    //物料类型
        //                    else if (keys.ToString().IndexOf("物料类型") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf("包含") != -1)
        //                        {
        //                            sqlwhere += "物料类型 like '%" + return_values["包含"].ToString() + "%'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("等于") != -1)
        //                        {
        //                            sqlwhere += "物料类型 ='" + return_values["等于"].ToString() + "'";
        //                        }
        //                        circletimes++;
        //                    }
        //                    //期初数量
        //                    else if (keys.ToString().IndexOf("期初数量") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                            sqlwhere += "期初数量 >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "期初数量 <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                            sqlwhere += "期初数量 >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlwhere += "期初数量 <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlwhere += "期初数量 !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "期初数量 =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] FirstNum = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlwhere += "结存成本 between " + FirstNum[0] + " and " + FirstNum[1];
        //                        }
        //                        circletimes++;
        //                    }
        //                    //收入数量
        //                    else if (keys.ToString().IndexOf("收入数量") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                            sqlwhere += "收入数量 >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "收入数量 <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                            sqlwhere += "收入数量 >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlwhere += "收入数量 <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlwhere += "收入数量 !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "收入数量 =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] InNum = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlwhere += "结存成本 between " + InNum[0] + " and " + InNum[1];
        //                        }
        //                        circletimes++;
        //                    }
        //                    //发出数量
        //                    else if (keys.ToString().IndexOf("发出数量") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                            sqlwhere += "发出数量 >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "发出数量 <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                            sqlwhere += "发出数量 >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlwhere += "发出数量 <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlwhere += "发出数量 !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "发出数量 =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] OutNum = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlwhere += "结存成本 between " + OutNum[0] + " and " + OutNum[1];
        //                        }
        //                        circletimes++;
        //                    }
        //                    //结存数量
        //                    else if (keys.ToString().IndexOf("结存数量") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                            sqlwhere += "结存数量 >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "结存数量 <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                            sqlwhere += "结存数量 >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlwhere += "结存数量 <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlwhere += "结存数量 !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "结存数量 =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] Num = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlwhere += "结存成本 between " + Num[0] + " and " + Num[1];
        //                        }
        //                        circletimes++;
        //                    }
        //                    //结存成本
        //                    else if (keys.ToString().IndexOf("结存成本") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                            sqlwhere += "结存成本 >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "结存成本 <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                            sqlwhere += "结存成本 >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlwhere += "结存成本 <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlwhere += "结存成本 !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhere += "结存成本 =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] Cost = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlwhere += "结存成本 between " + Cost[0] + " and " + Cost[1];
        //                        }
        //                        circletimes++;
        //                    }
        //                    //单据日期
        //                    else if (keys.ToString().IndexOf("单据日期") != -1)
        //                    {
        //                        string[] CurBaTime;//获取所选工作月

        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                            sqlwhen += "单据日期 >'" + return_values[">"].ToString() + "'";

        //                            CurBaTime = return_values[">"].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];


        //                            //获取起始时间的前一天日期
        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values[">"].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += " where 单据日期 between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhen += "单据日期 <'" + return_values["<"].ToString() + "'";
        //                            CurBaTime = return_values["<"].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values["<"].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += " where 单据日期 between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                            sqlwhen += "单据日期 >='" + return_values[">="].ToString() + "'";
        //                            CurBaTime = return_values[">="].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values[">="].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += " where 单据日期 between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlwhen += "单据日期 <='" + return_values["<="].ToString() + "'";
        //                            CurBaTime = return_values["<="].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlwhen += "单据日期 !='" + return_values["!="].ToString() + "'";
        //                            CurBaTime = return_values["!="].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values["!="].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += " where 单据日期 between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhen += "单据日期 ='" + return_values["="].ToString() + "'";
        //                            CurBaTime = return_values["="].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values["="].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += " where 单据日期 between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] WorkMonth = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlwhen += "单据日期 between '" + WorkMonth[0] + "' and '" + WorkMonth[1] + "'";
        //                            CurBaTime = WorkMonth[0].Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + WorkMonth[0] + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();
        //                            sqlPreTime += " where 单据日期 between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";

        //                        }

        //                        //MaxBaTime = sqlwhen.Substring(0,sqlwhen.IndexOf(" where")) + sqlwhen.Substring(sqlwhen.IndexOf("-", 2));
        //                        Timecircletimes++;
        //                    }
        //                    //工作年月
        //                    else if (keys.ToString().IndexOf("工作年月") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                            sqlwhen += "工作年月 >'" + return_values[">"].ToString() + "'";
        //                            MaxBaTime = return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhen += "工作年月 <'" + return_values["<"].ToString() + "'";
        //                            MaxBaTime = return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                            sqlwhen += "工作年月 >='" + return_values[">="].ToString() + "'";
        //                            MaxBaTime = return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlwhen += "工作年月 <='" + return_values["<="].ToString() + "'";
        //                            MaxBaTime = return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlwhen += "工作年月 !='" + return_values["!="].ToString() + "'";
        //                            MaxBaTime = return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhen += "工作年月 ='" + return_values["="].ToString() + "'";
        //                            MaxBaTime = return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] WorkMonth = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlwhen += "工作年月 between '" + WorkMonth[0] + "' and '" + WorkMonth[1] + "'";
        //                            MaxBaTime = WorkMonth[0];
        //                        }
        //                        Timecircletimes++;
        //                    }
        //                }
        //            }
        //        }
        //    #endregion
        //        List<string> update_sqls = new List<string>();

        //        //连接物料表和结存表(结存时间限制为根据选择条件中判断出的时间,若未选条件时默认当前工作月)
        //        string TempPreData = " SELECT DISTINCT " +
        //             " dbo.T_Stock_Status.MatId AS 物料编号, dbo.T_MatInf.MatName AS 物料名称, dbo.T_Stock_Status.StoreHouseId AS 仓库编号, " +
        //             " dbo.T_Stock_Status.MatType AS 物料类型, dbo.T_MatInf.Specifications AS 型号规格, dbo.T_MatInf.Units AS 单位, dbo.T_Stock_Status.FirstCount AS 期初数量," +
        //             " dbo.T_Stock_Status.FirstMoney AS 期初金额, dbo.T_Stock_Status.BalanceTime AS 结存时间 " +
        //             " into " + TempPreName + " FROM         dbo.T_Stock_Status INNER JOIN " +
        //             " dbo.T_MatInf ON dbo.T_Stock_Status.BalanceTime = " + MaxBaTime + " AND dbo.T_Stock_Status.MatId = dbo.T_MatInf.MatId ";

        //        string SQLwhere = "";//控制连接后的表
        //        string SQLwhere_ = "";//控制前半部分视图
        //        string StoCheckAll = "select distinct {0}.*, IsNull({1}.SumNum_1,0) as 收入数量, IsNull({2}.SumNum_2,0) as 发出数量, isnull({3}.期初数量,0)+isnull({4}.SumNum_1,0)-isnull({5}.SumNum_2,0) as 结存数量," +
        //                             " isnull({6}.InMoney,0)-isnull({7}.OutMoney,0)+isnull({8}.期初金额,0) as 结存成本 ," + TableInName + ".InMoney as 收入金额," + TableOutName + ".OutMoney as 发出金额 " +
        //                             " into " + StoCheckAll_name + " from {9} left join {10} on {11}.物料编号={12}.Matid_1  " +
        //                             " and {13}.仓库编号={14}.SHname_1 and {15}.物料类型={16}.MATY_1 " +
        //                             " left join {17} on {18}.物料编号={19}.Matid_2 and {20}.仓库编号={21}.SHname_2 and {22}.物料类型={23}.MATY_2 ";
        //        if (sqlwhere.Length != 7)
        //        {
        //            SQLwhere = sqlwhere + " and ";
        //            if (sqlwhere.IndexOf("结存数量") == -1 && sqlwhere.IndexOf("结存成本") == -1 && sqlwhere.IndexOf("收入数量") == -1 && sqlwhere.IndexOf("发出数量") == -1)
        //            {
        //                SQLwhere_ = sqlwhere;
        //                if (SQLwhere_.IndexOf("物料编号") != -1)
        //                {
        //                    SQLwhere_ = SQLwhere_.Replace("物料编号", " T_Stock_Status.MatId ");
        //                }
        //                if (SQLwhere_.IndexOf("物料名称") != -1)
        //                {
        //                    SQLwhere_ = SQLwhere_.Replace("物料名称", " T_MatInf.MatName ");
        //                }
        //                if (SQLwhere_.IndexOf("仓库编号") != -1)
        //                {
        //                    SQLwhere_ = SQLwhere_.Replace("仓库编号", " T_Stock_Status.StoreHouseId ");
        //                }
        //                if (SQLwhere_.IndexOf("物料类型") != -1)
        //                {
        //                    SQLwhere_.Replace("物料类型", " T_Stock_Status.MatType ");
        //                }
        //                if (SQLwhere_.IndexOf("期初数量") != -1)
        //                {
        //                    SQLwhere_.Replace("期初数量", " T_Stock_Status.FirstCount ");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            sqlwhere = "";
        //            SQLwhere_ = "";
        //            SQLwhere = " where ";
        //        }

        //        TempPreData = TempPreData + SQLwhere_;//筛选前半部分数据时加上所输入的"相关"条件

        //        StoCheckAll = string.Format(StoCheckAll, TempPreName, TableInName, TableOutName, TempPreName, TableInName, TableOutName,
        //                            TableInName, TableOutName, TempPreName, TempPreName, TableInName, TempPreName,
        //                            TableInName, TempPreName, TableInName, TempPreName, TableInName,
        //                            TableOutName, TempPreName, TableOutName, TempPreName, TableOutName, TempPreName, TableOutName, TempPreName);
        //        //sqlwhere = "";
        //        string sqlwhenIn = "", sqlwhenOut = "";//从收入、发出视图选数据时的时间条件
        //        if (sqlwhen.Length == 7)//未选限制条件
        //        {
        //            sqlwhenIn = sqlwhen + "T_StoCheck_In.工作年月 ='" + MaxBaTime + "'";
        //            sqlwhenOut = sqlwhen + "T_StoCheck_Out.工作年月 ='" + MaxBaTime + "'";
        //        }
        //        else//有限制条件
        //        {
        //            sqlwhenIn = sqlwhen;
        //            sqlwhenOut = sqlwhen;
        //        }
        //        string StoCheck_IN = "select T_StoCheck_In.Matid_1 ,T_StoCheck_In.SHname_1, T_StoCheck_In.MATY_1, sum(isnull(T_StoCheck_In.num,0)) as SumNum_1," +
        //                           "sum(isnull(T_StoCheck_In.InMoney,0)) as InMoney into " + TableInName + " from T_StoCheck_In  " + sqlwhenIn + " group by Matid_1,SHname_1,MATY_1";
        //        string StoCheck_Out = "select T_StoCheck_Out.Matid_2, T_StoCheck_Out.SHname_2 , T_StoCheck_Out.MATY_2 ,sum(isnull(T_StoCheck_Out.num,0)) as SumNum_2 ," +
        //                         "sum(isnull(T_StoCheck_Out.OutMoney,0)) as OutMoney into " + TableOutName + " from T_StoCheck_Out " + sqlwhenOut + "group by Matid_2,SHname_2,MATY_2";
        //        //报表查询sql
        //        Select_Data = Select_Data + SQLwhere + StoCheckAll_name + ".结存时间='" + MaxBaTime + "'";
        //        update_sqls.Add(TempPreData);
        //        update_sqls.Add(StoCheck_IN);
        //        update_sqls.Add(StoCheck_Out);
        //        update_sqls.Add(StoCheckAll);
        //        (new SqlDBConnect()).Exec_Tansaction(update_sqls);

        //        string TableOccurInName = TempName + "OccurIn";//前期收入发生数据表
        //        string TableOccurOutName = TempName + "OccurOut";//前期发出发生数据表
        //        //string StoCheck_OccurIN = "select T_StoCheck_In.Matid_1 ,T_StoCheck_In.SHname_1, T_StoCheck_In.MATY_1, sum(isnull(T_StoCheck_In.num,0)) as SumNum_1," +
        //        //                    "sum(isnull(T_StoCheck_In.InMoney,0)) as InMoney into " + TableOccurInName + " from T_StoCheck_In  " + sqlPreTime + " group by Matid_1,SHname_1,MATY_1";
        //        //string StoCheck_OccurOut = "select T_StoCheck_Out.Matid_2, T_StoCheck_Out.SHname_2 , T_StoCheck_Out.MATY_2 ,sum(isnull(T_StoCheck_Out.num,0)) as SumNum_2 ," +
        //        //                    "sum(isnull(T_StoCheck_Out.OutMoney,0)) as OutMoney into " + TableOccurOutName + " from T_StoCheck_Out " + sqlPreTime + " group by Matid_2,SHname_2,MATY_2";

        //        string StoCheck_OccurIN = "select T_StoCheck_In.Matid_1 ,T_StoCheck_In.SHname_1, T_StoCheck_In.MATY_1, sum(isnull(T_StoCheck_In.num,0)) as SumNum_1," +
        //                            "sum(isnull(T_StoCheck_In.InMoney,0)) as InMoney  from T_StoCheck_In  " + sqlPreTime + " group by Matid_1,SHname_1,MATY_1";
        //        string StoCheck_OccurOut = "select T_StoCheck_Out.Matid_2, T_StoCheck_Out.SHname_2 , T_StoCheck_Out.MATY_2 ,sum(isnull(T_StoCheck_Out.num,0)) as SumNum_2 ," +
        //                            "sum(isnull(T_StoCheck_Out.OutMoney,0)) as OutMoney  from T_StoCheck_Out " + sqlPreTime + " group by Matid_2,SHname_2,MATY_2";
            
        //        //报表查询sql
        //        Select_Data = Select_Data + SQLwhere + StoCheckAll_name + ".结存时间='" + MaxBaTime + "'";
         
        //        List<string> strsqls = new List<string>();
        //        DataTable DTin = (new SqlDBConnect()).Get_Dt(StoCheck_OccurIN);//前期收入发生数据
        //        DataTable DTout = (new SqlDBConnect()).Get_Dt(StoCheck_OccurOut);//前期发出发生数据
        //        for (int i = 0; i < DTin.Rows.Count; i++)
        //        {
        //            string updatesql = "update {0} set {0}.期初数量={0}.期初数量+" + DTin.Rows[i]["SumNum_1"] + ",{0}.期初金额={0}.期初金额+" + DTin.Rows[i]["InMoney"] +
        //                " where 物料编号= '" + DTin.Rows[0]["Matid_1"] + "' and 仓库编号='" + DTin.Rows[0]["SHname_1"] + "' and 物料类型= " + DTin.Rows[0]["MATY_1"];
        //            updatesql = string.Format(updatesql, StoCheckAll_name);
        //            strsqls.Add(updatesql);
        //            (new SqlDBConnect()).Exec_Tansaction(strsqls);
        //        }
        //        for (int j = 0; j < DTout.Rows.Count; j++)
        //        {
        //            string updatesql = "update {0} set {0}.期初数量={0}.期初数量-" + DTin.Rows[j]["SumNum_2"] + ",{0}.期初金额={0}.期初金额-" + DTin.Rows[j]["OutMoney"] +
        //                " where 物料编号= '" + DTin.Rows[0]["Matid_2"] + "' and 仓库编号='" + DTin.Rows[0]["SHname_2"] + "' and 物料类型= " + DTin.Rows[0]["MATY_2"];         
        //            updatesql = string.Format(updatesql, StoCheckAll_name);     
        //            strsqls.Add(updatesql);
        //            (new SqlDBConnect()).Exec_Tansaction(strsqls);
        //        }
        //        //更新结存成本
        //        string update_cost = "update {0} set {0}.结存成本={0}.期初金额+{0}.收入金额-{0}.发出金额";
        //        update_cost = string.Format(update_cost, StoCheckAll_name);
        //        (new SqlDBConnect()).ExecuteNonQuery(update_cost);

        //        //StockCheckForm form = new StockCheckForm(Select_Data, StoCheckAll_name, TableInName, TableOutName, TempPreName);
        //        //form.MdiParent = this;
        //        //form.Show();
        //    }
        //}

        private void 进货单明细表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSql;
            WFilter wf = new WFilter(0, "", true);
            wf.tableName = "T_Receipt_Main";    //表名  
            wf.strSql = "SELECT ReceiptId  单据号,InvoiceNo 发票编号, ReceiptTypeID  单据类别," +
                        "CustomerReceiptNo 自定义编号,CurWorkMonth 工作年月,OccurTime 单据日期," +
                        "CustName 客户名称,MatId 商品编号 ,MatName 商品名称,SourceStoreH 仓库," +
                        "MatType 类型,num 数量,price 含税单价,Amount 含税金额,TTaxPurchPrice 成本金额," +
                        "lotCode 批号,VerifyPerson 验收员,BillUser 操作员,Memo 备注 " +
                        "FROM T_JXDetail  where (ReceiptTypeID ='01'or ReceiptTypeID ='03'or ReceiptTypeID='20')";

            wf.s_items.Add("单据号,ReceiptId,C");
            wf.s_items.Add("单据类别,ReceiptTypeID,C");
            wf.s_items.Add("发票编号,InvoiceNo,C");
            wf.s_items.Add("自定义编号,CustomerReceiptNo,C");
            wf.s_items.Add("工作年月,CurWorkMonth,N");
            wf.s_items.Add("单据日期,OccurTime,N");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("商品编号,MatId,C");
            wf.s_items.Add("商品名称,MatName,C");
            wf.s_items.Add("类型,MatType,C");
            wf.s_items.Add("数量,num,N");
            wf.s_items.Add("含税单价,price,N");
            wf.s_items.Add("含税金额,Amount,N");
            wf.s_items.Add("成本金额,TTaxPurchPrice,N");
            wf.s_items.Add("批号,lotCode,C");
            wf.s_items.Add("验收员,VerifyPerson,C");
            wf.s_items.Add("操作员,BillUser,C");
            //隐掉查询键
            wf.btnOK.Enabled = false;

            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                //返回条件框中的sql语句
                strSql = wf.Return_Sql;
                strSql += "order by OccurTime,receipttypeid";
                if (!(new DBUtil()).yn_exist_data(strSql))
                {
                    MessageBox.Show("不存在该条数据！", "提示");
                    return;
                }
                Connect(strSql, "JHDetail");
            }
        }

        private void 销售单明细表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSql;
            WFilter wf = new WFilter(0, "", true);
            wf.tableName = "T_Receipt_Main";    //表名  
            wf.strSql = "SELECT ReceiptId  单据号,InvoiceNo 发票编号, ReceiptTypeID  单据类别," +
                        "CustomerReceiptNo 自定义编号,CurWorkMonth 工作年月,OccurTime 单据日期," +
                        "CustName 客户名称,MatId 商品编号 ,MatName 商品名称,SourceStoreH 仓库," +
                        "MatType 类型,num 数量,price 含税单价,Amount 含税金额,TTaxPurchPrice 成本金额," +
                        "ML 毛利, lotCode 批号,VerifyPerson 验收员,BillUser 操作员,Memo 备注 " +
                        "FROM T_JXDetail  where (ReceiptTypeID >='51' AND ReceiptTypeID <='99')";

            wf.s_items.Add("单据号,ReceiptId,C");
            wf.s_items.Add("单据类别,ReceiptTypeID,C");
            wf.s_items.Add("发票编号,InvoiceNo,C");
            wf.s_items.Add("自定义编号,CustomerReceiptNo,C");
            wf.s_items.Add("工作年月,CurWorkMonth,N");
            wf.s_items.Add("单据日期,OccurTime,N");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("商品编号,MatId,C");
            wf.s_items.Add("商品名称,MatName,C");
            wf.s_items.Add("仓库,SourceStoreH,C");
            wf.s_items.Add("类型,MatType,C");
            wf.s_items.Add("数量,num,N");
            wf.s_items.Add("含税单价,price,N");
            wf.s_items.Add("含税金额,Amount,N");
            wf.s_items.Add("成本金额,TTaxPurchPrice,N");
            wf.s_items.Add("毛利,ML,N");
            wf.s_items.Add("批号,lotCode,C");
            wf.s_items.Add("验收员,VerifyPerson,C");
            wf.s_items.Add("操作员,BillUser,C");
            //隐掉查询键
            wf.btnOK.Enabled = false;


            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                //返回条件框中的sql语句
                strSql = wf.Return_Sql;
                strSql += "order by OccurTime,receipttypeid";
                if (!(new DBUtil()).yn_exist_data(strSql))
                {
                    MessageBox.Show("不存在该条数据！", "提示");
                    return;
                }
                Connect(strSql, "XSDetail");
            }
        }

        private void 未销借用查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSql;
            WFilter wf = new WFilter(0, "", false);
            wf.tableName = "T_Receipt_Main";    //表名  
            wf.strSql = "SELECT Custid 客户代码,Custname 客户名称,SourceStoreH 仓库,MatId 商品编号," +
                       "MatName 卡片名称,MatType 类别,num 数量,amount 金额,VerifyPerson 验收员 " +
                       "FROM T_WXChildren where (ReceiptTypeID ='75')";
            wf.s_items.Add("客户代码,CustID,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("仓库,SourceStoreH,C");
            wf.s_items.Add("商品编号,MatId,C");
            wf.s_items.Add("卡片名称,MatName,C");
            wf.s_items.Add("类别,MatType,C");
            wf.s_items.Add("数量,num,N");
            wf.s_items.Add("金额,amount,N");
            wf.s_items.Add("验收员,VerifyPerson,N");
            //隐掉查询键
            wf.btnOK.Enabled = false;


            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                //返回条件框中的sql语句
                strSql = wf.Return_Sql;
                strSql += "order by Custid,Matid";
                if (!(new DBUtil()).yn_exist_data(strSql))
                {
                    MessageBox.Show("不存在该条数据！", "提示");
                    return;
                }
                Connect(strSql, "WXBorrow");
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //记录退出信息
            string loginOutTime = DBUtil.getServerTime().ToString();
            string sql_ = "update T_LogHist set LogoutTime='" + loginOutTime
                + "' where userName='" + this.userName
                + "' and LoginTime='" + this.loginTime + "'";
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
            }
            catch
            { }
            //
        }

        /// <summary>
        /// Log日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            LoginOpaHist loh = new LoginOpaHist();
            loh.MdiParent = this;
            loh.Show();
        }

        private void MainForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认退出系统吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
        //是否可删
        private void 未销租机查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSql;
            WFilter wf = new WFilter(0, "", false);
            wf.tableName = "T_Receipt_Main";    //表名  
            wf.strSql = "SELECT Custid 客户代码,Custname 客户名称,SourceStoreH 仓库,MatId 商品编号," +
                       "MatName 卡片名称,MatType 类别,num 数量,amount 金额,VerifyPerson 验收员 " +
                       "FROM T_WXChildren where (ReceiptTypeID ='88')";
            wf.s_items.Add("客户代码,CustID,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("仓库,SourceStoreH,C");
            wf.s_items.Add("商品编号,MatId,C");
            wf.s_items.Add("卡片名称,MatName,C");
            wf.s_items.Add("类别,MatType,C");
            wf.s_items.Add("数量,num,N");
            wf.s_items.Add("金额,amount,N");
            wf.s_items.Add("验收员,VerifyPerson,N");
            //隐掉查询键
            wf.btnOK.Enabled = false;


            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                //返回条件框中的sql语句
                strSql = wf.Return_Sql;
                strSql += "order by Custid,Matid";
                if (!(new DBUtil()).yn_exist_data(strSql))
                {
                    MessageBox.Show("不存在该条数据！", "提示");
                    return;
                }
                Connect(strSql, "WXRent");
            }
        }
        //是否可删除
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            RecordCopyNum rcn = new RecordCopyNum();
            rcn.MdiParent = this;
            rcn.Show();
        }
        //可删？
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            RecordCopySettle fcs = new RecordCopySettle();
            fcs.curUser = this.userName;
            fcs.MdiParent = this;
            fcs.Show();
        }

        //private void 库存结存ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    StockStatusOperateForm form = new StockStatusOperateForm();
        //    form.curWorkMonth = this.curWorkMonth;
        //    form.ShowDialog();
        //}

        private void toolStripMenuItem5_Click_1(object sender, EventArgs e)
        {
            BargainsBindList bbl = new BargainsBindList();
            bbl.MdiParent = this;
            bbl.Show();
        }

        private void 应收账务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinancialForm form = new FinancialForm("收入");
            form.MdiParent = this;
            form.Show();
        }

        private void 应付账务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinancialForm form = new FinancialForm("支出");
            form.MdiParent = this;
            form.Show();
        }


        //可删否
        private void 机器信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MacModelForm form = new MacModelForm();
            form.MdiParent = this;
            form.Show();
        }

        private void 日报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayReportForm form = new DayReportForm();
            form.MdiParent = this;
            form.Show();
        }
        //可删？
        private void 机器信息ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MacModelForm form = new MacModelForm();
            form.MdiParent = this;
            form.Show();
        }

        private void 库存盘点表ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StockStatusReportForm form = new StockStatusReportForm();
            form.MdiParent = this;
            form.Show();
        }

        private WFilter wf;
        //是否可删
        private void 应收款查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            wf = new WFilter(0, "", true);
            wf.tableName = "T_YSSearch";    //表名  
            wf.strSql = "select ReceiptId 单据号,ReceiptTypeID 单据类别,InvoiceNo 发票编号," +
                        "OccurTime 单据日期,Custid 客户编码, CustName 客户名称," +
                        "T_Receipt_Main_Det.MatId  商品编号,MatName 商品名称,SourceStoreH 仓库,num 数量," +
                        "Amount 含税金额,MoneyPayed 核销金额,Amount- MoneyPayed 余额," +
                        "TTaxPurchPrice 成本金额,Amount-TTaxPurchPrice 毛利," +
                        "OffPeriod 账期,Area 管理区号,CheckPerson 复核员,Salesman 业务员," +
                        "BillUser 操作员 from T_Receipt_Main_Det left join T_MatInf on T_MatInf .Matid =T_Receipt_Main_Det .MatId " +
                        "where (ReceiptTypeID ='51' or ReceiptTypeID ='52' or ReceiptTypeID ='56' or ReceiptTypeID ='YS')";
            wf.s_items.Add("单据号,ReceiptId,C");
            wf.s_items.Add("单据类别,ReceiptTypeID,C");
            wf.s_items.Add("发票编号,InvoiceNo,C");
            wf.s_items.Add("单据日期,OccurTime,N");
            wf.s_items.Add("客户编码,Custid,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("商品编号,MatId,C");
            wf.s_items.Add("商品名称,MatName,C");
            wf.s_items.Add("仓库,SourceStoreH,C");
            wf.s_items.Add("数量,num,N");
            wf.s_items.Add("含税金额,Amount,N");
            wf.s_items.Add("成本金额,TTaxPurchPrice,N");
            wf.s_items.Add("核销金额,MoneyPayed,N");
            wf.s_items.Add("余额,Amount- MoneyPayed,N");
            wf.s_items.Add("毛利,Amount-TTaxPurchPrice,N");
            wf.clickOkChange += new WFilter.ClickOkChange(wf_clickOkChange应收款查询);
            //隐掉查询键
            wf.btnOK.Enabled = false;
            
            wf.Show();            
        }

        void wf_clickOkChange应收款查询()
        {
            if (wf.IsClickOk == true)
            {
                //返回条件框中的sql语句
                string strSql = wf.Return_Sql;
                strSql += "order by InvoiceNo ,CustName ";
                YSYFForm form = new YSYFForm();

                //form.TypeName = "YFSearch";
                form.Strsql = strSql;
                form.MdiParent = this;
                form.Show();
                form.TypeName = "YSSearch";
                form.BringToFront();
                form.ShowReport();
            }
        }

        private void 应付款查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSql;
            WFilter wf = new WFilter(0, "", false);
            wf.tableName = "T_YFSearch";    //表名  
            wf.strSql = "select InvoiceNo 发票编号,OccurTime 单据日期,CustName 客户名称,T_Receipt_Main_Det.MatId 商品编号," +
                        "MatName 商品名称,SourceStoreH 仓库,num 数量,Amount 含税金额,MoneyPayed 核销金额,Amount-MoneyPayed 余额, "+
                        "ReceiptId 单据号,ReceiptTypeID 单据类别 from T_Receipt_Main_Det " +
                        "left join T_MatInf on T_MatInf .Matid =T_Receipt_Main_Det .MatId " +
                        "where ((ReceiptTypeID ='01' and InvoiceNo<>'11111111' and InvoiceNo<>'22222222' " +
                        "and InvoiceNo<>'33333333' and InvoiceNo<>'55555555' and InvoiceNo<>'66666666' " +
                        "and InvoiceNo<>'77777777')or (ReceiptTypeID='03') or (ReceiptTypeID='20') or (ReceiptTypeID='YF')) ";
            wf.s_items.Add("单据号,ReceiptId,C");
            wf.s_items.Add("单据类别,ReceiptTypeID,C");
            wf.s_items.Add("发票编号,InvoiceNo,C");
            wf.s_items.Add("单据日期,OccurTime,N");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("商品编号,MatId,C");
            wf.s_items.Add("商品名称,MatName,C");
            wf.s_items.Add("仓库,SourceStoreH,C");
            wf.s_items.Add("数量,num,N");
            wf.s_items.Add("含税金额,Amount,N");
            wf.s_items.Add("核销金额,MoneyPayed,N");
            wf.s_items.Add("余额,Amount-MoneyPayed,N");
            //隐掉查询键
            wf.btnOK.Enabled = false;


            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                //返回条件框中的sql语句
                strSql = wf.Return_Sql;
                strSql += "order by InvoiceNo ,CustName ";
                YSYFForm form = new YSYFForm();
               
                //form.TypeName = "YFSearch";
                form.Strsql = strSql;
                form.MdiParent = this;
                form.Show();                                  
                form.TypeName = "YFSearch";
                form.BringToFront();
                form.ShowReport();
            }
        }
        //是否可删

        //private void 应收核销查询ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    WFilter wf = new WFilter(0, "", false);
        //    wf.strSql = "select CurWorkMonth 工作年月,OccurTime 核销日期,CustId 客户代码,CustName 客户名称,InMoney 核销金额,"+
        //                "PayMethod 支付方式,PinZhengHao 凭证号, Memo 备注 "+
        //                " from T_Financial  ";
        //    wf.s_items.Add("工作年月,CurWorkMonth,N");
        //    wf.s_items.Add("核销日期,OccurTime,N");
        //    wf.s_items.Add("客户代码,CustId,C");
        //    wf.s_items.Add("客户名称,CustName,C");
        //    wf.s_items.Add("核销金额,InMoney,N");
        //    wf.s_items.Add("支付方式,PayMethod,C");
        //    wf.s_items.Add("凭证号,PinZhengHao,C");
        //    wf.s_items.Add("备注,Memo,C");
        //    //隐掉查询键
        //    wf.btnOK.Enabled = false;


        //    wf.ShowDialog();

        //    if (wf.DialogResult == DialogResult.OK)
        //    {
        //        //返回条件框中的sql语句
        //        string strSql = wf.Return_Sql;
        //        //if (!(new DBUtil()).yn_exist_data(strSql))
        //        //{
        //        //    MessageBox.Show("不存在该条数据！", "提示");
        //        //    return;
        //        //}
        //        Connect(strSql, "YFSearch");
        //        //PublicDetailForm form = new PublicDetailForm(strSql, "", "", "ReceivableReport");
        //        //form.MdiParent = this;
        //        //form.Show();
        //    }
        //}

        //private void 应付核销查询ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    WFilter wf = new WFilter(0, "", false);
        //    wf.strSql = "select CurWorkMonth 工作年月,OccurTime 核销日期,CustId 客户代码,CustName 客户名称,OutMoney 核销金额," +
        //               " PinZhengHao 凭证号, Memo 备注 " +
        //               "  from T_Financial ";
        //    wf.s_items.Add("工作年月,CurWorkMonth,N");
        //    wf.s_items.Add("核销日期,OccurTime,N");
        //    wf.s_items.Add("客户代码,CustId,C");
        //    wf.s_items.Add("客户名称,CustName,C");
        //    wf.s_items.Add("核销金额,OutMoney,N");
        //    wf.s_items.Add("支付方式,PayMethod,C");
        //    wf.s_items.Add("凭证号,PinZhengHao,C");
        //    wf.s_items.Add("备注,Memo,C");
        //    //隐掉查询键
        //    wf.btnOK.Enabled = false;


        //    wf.ShowDialog();

        //    if (wf.DialogResult == DialogResult.OK)
        //    {
        //        //返回条件框中的sql语句
        //        string strSql = wf.Return_Sql;
        //        //if (!(new DBUtil()).yn_exist_data(strSql))
        //        //{
        //        //    MessageBox.Show("不存在该条数据！", "提示");
        //        //    return;
        //        //}
        //        Connect(strSql, "YFSearch");
        //        //PublicDetailForm form = new PublicDetailForm(strSql, "", "", "Payable");
        //        //form.MdiParent = this;
        //        //form.Show();
        //    }

        //}

        //private void 机型对应耗材及选购件ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ModelMats mm = new ModelMats();
        //    mm.MdiParent = this;
        //    mm.Show();
        //}

        private void 技术资料ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormDocsList dcl = new FormDocsList();
            dcl.MdiParent = this;
            dcl.Show();
        }

        private void 故障及维修参考ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMatErrLst fme = new FormMatErrLst();
            fme.MdiParent = this;
            fme.Show();
        }

        private void 机型相关ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelErrDocDEF ded = new ModelErrDocDEF();
            ded.MdiParent = this;
            ded.Show();
        }

       
        //private void 库存盘点1ToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{
        //    //查询窗体
        //    WFilter wf = new WFilter(0, "", true);
            
        //    wf.strSql = "";
        //    wf.s_items.Add("物料编号," + "T_Receipt_Main_Det" + ".MatId,C");
        //    wf.s_items.Add("物料名称," + "T_MatInf" + ".MatName,C");
        //    wf.s_items.Add("仓库编号," + "T_Receipt_Main_Det" + ".SourceStoreH,C");
        //    wf.s_items.Add("物料类型," + "T_Receipt_Main_Det" + ".MatType,C");

        //    //wf.s_items.Add("期初数量," + "" + "期初数量,N");
        //    //wf.s_items.Add("收入数量," + "" + "收入数量,N");
        //    //wf.s_items.Add("发出数量," + "" + "发出数量,N");
        //    //wf.s_items.Add("结存数量," + "" + "结存数量,N");
        //    //wf.s_items.Add("结存成本," + "" + "结存成本,N");

        //    wf.s_items.Add("单据日期," + "T_Receipt_Main_Det" + ".OccurTime,N");
        //    wf.s_items.Add("工作年月," + "T_Receipt_Main_Det" + ".CurWorkMonth,N");

        //    wf.btnOK.Enabled = false;
        //    wf.ShowDialog();

        //    string MaxBaTime = StockStatusDAO.GetBalanceTime();//当月
        //    string sqlPreTime = " and ";//前期发生时间段
        //    string sqlwhere = " and ";//非时间条件
        //    string sqlwhen = " and ";  //时间条件
        //    string sqlNum = "";//数量条件
            
        //    List<string> TJnum = new List<string>();

        //    if (wf.DialogResult == DialogResult.OK)
        //    {
        //        #region//键值对返回值
        //        Dictionary<string, Dictionary<string, string>> return_Fileds_Values = wf.Return_Fileds_Values;

        //        int circletimes = 0;//记录外层foreach循环次数
        //        int Timecircletimes = 0;//时间条件循环
        //        int Numcircletimes = 0;//数量条件循环

        //        foreach (string keys in return_Fileds_Values.Keys)
        //        {
        //            Dictionary<string, string> return_values = return_Fileds_Values[keys];
                
        //            foreach (string itemKey in return_values.Keys)
        //            {
        //                if (return_values != null && return_values.Count > 0)
        //                {
        //                    if (circletimes >= 1 && keys.ToString().IndexOf("单据日期") == -1 && keys.ToString().IndexOf("工作年月") == -1
        //                        && keys.ToString().IndexOf("期初数量") == -1 && keys.ToString().IndexOf("收入数量") == -1
        //                        && keys.ToString().IndexOf("发出数量") == -1 && keys.ToString().IndexOf("结存数量") == -1
        //                        && keys.ToString().IndexOf("结存成本") == -1)
        //                    {
        //                        sqlwhere += " and ";
        //                    }
        //                    if (Timecircletimes >= 1 && keys.ToString().IndexOf("物料编号") == -1 && keys.ToString().IndexOf("物料名称") == -1
        //                        && keys.ToString().IndexOf("仓库编号") == -1 && keys.ToString().IndexOf("物料类型") == -1
        //                        && keys.ToString().IndexOf("期初数量") == -1 && keys.ToString().IndexOf("收入数量") == -1
        //                        && keys.ToString().IndexOf("发出数量") == -1 && keys.ToString().IndexOf("结存数量") == -1
        //                        && keys.ToString().IndexOf("结存成本") == -1)
        //                    {
        //                        sqlwhen += " and ";
        //                    }

        //                    if (Numcircletimes >= 1 && keys.ToString().IndexOf("物料编号") == -1 && keys.ToString().IndexOf("物料名称") == -1
        //                        && keys.ToString().IndexOf("仓库编号") == -1 && keys.ToString().IndexOf("物料类型") == -1
        //                        && keys.ToString().IndexOf("单据日期") == -1 && keys.ToString().IndexOf("工作年月") == -1)
        //                    {
        //                        sqlNum += " && ";
        //                    }



        //                    //物料编号
        //                    if (keys.ToString().IndexOf("物料编号") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf("包含") != -1)
        //                        {
        //                            sqlwhere += " T_Receipt_Main_Det.MatId like '%" + return_values["包含"].ToString() + "%'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("等于") != -1)
        //                        {
        //                            sqlwhere += " T_Receipt_Main_Det.MatId ='" + return_values["等于"].ToString() + "'";
        //                        }
        //                        circletimes++;
        //                    }
        //                    //物料名称
        //                    else if (keys.ToString().IndexOf("物料名称") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf("包含") != -1)
        //                        {
        //                            sqlwhere += " T_Receipt_Main_Det.MatName like '%" + return_values["包含"].ToString() + "%'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("等于") != -1)
        //                        {
        //                            sqlwhere += " T_Receipt_Main_Det.MatName ='" + return_values["等于"].ToString() + "'";
        //                        }
        //                        circletimes++;
        //                    }
        //                    //仓库编号
        //                    else if (keys.ToString().IndexOf("仓库编号") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf("包含") != -1)
        //                        {
        //                            sqlwhere += " T_Receipt_Main_Det.SourceStoreH like '%" + return_values["包含"].ToString() + "%'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("等于") != -1)
        //                        {
        //                            sqlwhere += " T_Receipt_Main_Det.SourceStoreH  ='" + return_values["等于"].ToString() + "'";
        //                        }
        //                        circletimes++;
        //                    }
        //                    //物料类型
        //                    else if (keys.ToString().IndexOf("物料类型") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf("包含") != -1)
        //                        {
        //                            sqlwhere += " T_Receipt_Main_Det.MatType like '%" + return_values["包含"].ToString() + "%'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("等于") != -1)
        //                        {
        //                            sqlwhere += " T_Receipt_Main_Det.MatType ='" + return_values["等于"].ToString() + "'";
        //                        }
        //                        circletimes++;
        //                    }

        //                    //期初数量
        //                    else if (keys.ToString().IndexOf("期初数量") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {

        //                            sqlNum+="firstNum >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="firstNum <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                             sqlNum+="firstNum >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlNum += "firstNum <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                             sqlNum+="firstNum !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="firstNum =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] FirstNum = return_values["范围"].ToString().Split(new char[] { '~' });
        //                             sqlNum+="firstNum between " + FirstNum[0] + " and " + FirstNum[1];
        //                        }

        //                        //TjNum.Add("期初数量", return_values);
        //                        Numcircletimes++;
        //                    }
        //                    //收入数量
        //                    else if (keys.ToString().IndexOf("收入数量") != -1)
        //                    {

        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                             sqlNum+="inNum >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="inNum <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                             sqlNum+="inNum >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                             sqlNum+="inNum <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                             sqlNum+="inNum !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="inNum =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] InNum = return_values["范围"].ToString().Split(new char[] { '~' });
        //                             sqlNum+="inNum between " + InNum[0] + " and " + InNum[1];
        //                        }

        //                        Numcircletimes++;
        //                    }
        //                    //发出数量
        //                    else if (keys.ToString().IndexOf("发出数量") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                             sqlNum+="outNum >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="outNum <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                             sqlNum+="outNum >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                             sqlNum+="outNum <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlNum+="outNum !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="outNum =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] OutNum = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlNum+="outNum between " + OutNum[0] + " and " + OutNum[1];
        //                        }

        //                        Numcircletimes++;
        //                    }
        //                    //结存数量
        //                    else if (keys.ToString().IndexOf("结存数量") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                             sqlNum+="sumNum >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="sumNum <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                             sqlNum+="sumNum >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlNum+="sumNum <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                             sqlNum+="sumNum !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="sumNum =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] Num = return_values["范围"].ToString().Split(new char[] { '~' });
        //                             sqlNum+="sumNum between " + Num[0] + " and " + Num[1];
        //                        }

        //                        Numcircletimes++;
        //                    }
        //                    //结存成本
        //                    else if (keys.ToString().IndexOf("结存成本") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                             sqlNum+="sumMoney >" + return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="sumMoney <" + return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                             sqlNum+="sumMoney >=" + return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlNum+="sumMoney <=" + return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                             sqlNum+="sumMoney !=" + return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                             sqlNum+="sumMoney =" + return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            //sqlNum += return_values["范围"].ToString();
        //                            string[] Cost = return_values["范围"].ToString().Split(new char[] { '~' });
        //                             sqlNum+="sumMoney between " + Cost[0] + " and " + Cost[1];
        //                        }

        //                        Numcircletimes++;
        //                    }

        //                    //单据日期
        //                    else if (keys.ToString().IndexOf("单据日期") != -1)
        //                    {
        //                        string[] CurBaTime;//获取所选工作月

        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                            sqlwhen += " T_Receipt_Main_Det.OccurTime >'" + return_values[">"].ToString() + "'";

        //                            CurBaTime = return_values[">"].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            //获取起始时间的前一天日期
        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values[">"].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += " T_Receipt_Main_Det.OccurTime between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhen += " T_Receipt_Main_Det.OccurTime <'" + return_values["<"].ToString() + "'";
        //                            CurBaTime = return_values["<"].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values["<"].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += " T_Receipt_Main_Det.OccurTime between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                            sqlwhen += " T_Receipt_Main_Det.OccurTime >='" + return_values[">="].ToString() + "'";
        //                            CurBaTime = return_values[">="].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values[">="].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += "  T_Receipt_Main_Det.OccurTime between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlwhen += " T_Receipt_Main_Det.OccurTime <='" + return_values["<="].ToString() + "'";
        //                            CurBaTime = return_values["<="].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlwhen += " T_Receipt_Main_Det.OccurTime !='" + return_values["!="].ToString() + "'";
        //                            CurBaTime = return_values["!="].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values["!="].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += " T_Receipt_Main_Det.OccurTime between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhen += " T_Receipt_Main_Det.OccurTime ='" + return_values["="].ToString() + "'";
        //                            CurBaTime = return_values["="].ToString().Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + return_values["="].ToString() + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

        //                            sqlPreTime += " T_Receipt_Main_Det.OccurTime between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] WorkMonth = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlwhen += " T_Receipt_Main_Det.OccurTime between '" + WorkMonth[0] + "' and '" + WorkMonth[1] + "'";
        //                            CurBaTime = WorkMonth[0].Split(new char[] { '-' });
        //                            MaxBaTime = CurBaTime[0] + CurBaTime[1];

        //                            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + WorkMonth[0] + "'),120)";
        //                            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();
        //                            sqlPreTime += " T_Receipt_Main_Det.OccurTime between '" + CurBaTime[0] + "-" + CurBaTime[1] + "-01' and '" + TimeTo + "'";

        //                        }
        //                        Timecircletimes++;
        //                    }
        //                    //工作年月
        //                    else if (keys.ToString().IndexOf("工作年月") != -1)
        //                    {
        //                        if (itemKey.ToString().IndexOf(">") != -1 && itemKey.ToString().IndexOf(">=") == -1)
        //                        {
        //                            sqlwhen += "  T_Receipt_Main_Det.CurWorkMonth >'" + return_values[">"].ToString() + "'";
        //                            MaxBaTime = return_values[">"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<") != -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhen += "  T_Receipt_Main_Det.CurWorkMonth <'" + return_values["<"].ToString() + "'";
        //                            MaxBaTime = return_values["<"].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf(">=") != -1)
        //                        {
        //                            sqlwhen += "  T_Receipt_Main_Det.CurWorkMonth >='" + return_values[">="].ToString() + "'";
        //                            MaxBaTime = return_values[">="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("<=") != -1)
        //                        {
        //                            sqlwhen += "  T_Receipt_Main_Det.CurWorkMonth <='" + return_values["<="].ToString() + "'";
        //                            MaxBaTime = return_values["<="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("!=") != -1)
        //                        {
        //                            sqlwhen += "  T_Receipt_Main_Det.CurWorkMonth !='" + return_values["!="].ToString() + "'";
        //                            MaxBaTime = return_values["!="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("=") != -1 && itemKey.ToString().IndexOf("!=") == -1 && itemKey.ToString().IndexOf(">=") == -1 && itemKey.ToString().IndexOf("<=") == -1)
        //                        {
        //                            sqlwhen += "  T_Receipt_Main_Det.CurWorkMonth ='" + return_values["="].ToString() + "'";
        //                            MaxBaTime = return_values["="].ToString();
        //                        }
        //                        else if (itemKey.ToString().IndexOf("范围") != -1)
        //                        {
        //                            string[] WorkMonth = return_values["范围"].ToString().Split(new char[] { '~' });
        //                            sqlwhen += "  T_Receipt_Main_Det.CurWorkMonth between '" + WorkMonth[0] + "' and '" + WorkMonth[1] + "'";
        //                            MaxBaTime = WorkMonth[0];
        //                        }
        //                        Timecircletimes++;
        //                    }
        //                }
        //            }
        //        }
        //        #endregion
              
        //        if (sqlwhere.Length == 5)
        //        {
        //            sqlwhere = "";
        //        }
        //        if (sqlwhen.Length == 5)
        //        {
        //            sqlwhen = "";
        //        }
        //        if (sqlPreTime.Length == 5)
        //        {
        //            sqlPreTime = "";
        //        }

        //        FormStockPandian form = new FormStockPandian(sqlwhere, MaxBaTime, sqlPreTime, sqlwhen,sqlNum);
        //        form.MdiParent = this;
        //        form.Show();

        //        form.build_dt();
        //        form.Produce_dt();
        //        form.progressBar1.Value = 0;
        //    }
        //}

        private void 机型对应耗材及选购件报价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelMatsPrice form = new ModelMatsPrice();
            form.MdiParent = this;
            form.Show();
        }

        private void 水平平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void 垂直平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void 层叠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void 全部关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int m = this.MdiChildren.Count()-1; m >= 0; m--)
            {
                this.MdiChildren[m].Close();
            }
        }
       
        private void 抄张财务开票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCopySettleInvoiceLst fl = new FormCopySettleInvoiceLst();
            fl.OpaMethod = "0";   //开票登记
            fl.MdiParent = this;
            fl.Show();
        }

        private void 其他报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 预付款查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSql;
            WFilter wf = new WFilter(0, "", false);
            wf.strSql = "select OccurTime 单据日期,CustID 客户编号,CustName 客户名称," +
                        "T_Receipt_Main_Det.MatId 商品编号,T_MatInf .MatName 商品名称," +
                        "Amount 含税金额,VerifyPerson 验收员 from T_Receipt_Main_Det " +
                        "LEFT JOIN T_MatInf ON T_MatInf .Matid =T_Receipt_Main_Det .MatId " +
                        "where ReceiptTypeID ='YF'";
            wf.s_items.Add("单据日期,OccurTime,N");
            wf.s_items.Add("客户编号,CustID,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("商品编号,T_Receipt_Main_Det.MatId,C");
            wf.s_items.Add("商品名称,T_MatInf .MatName,C");
            wf.s_items.Add("含税金额,Amount,N");
            wf.s_items.Add("验收员,VerifyPerson,C");
            //隐掉查询键
            wf.btnOK.Enabled = false;


            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                //返回条件框中的sql语句
                strSql = wf.Return_Sql;
                if (!(new DBUtil()).yn_exist_data(strSql))
                {
                    MessageBox.Show("不存在该条数据！", "提示");
                    return;
                }
                Connect(strSql, "YFKSearch");
            }

        }

        private void 预收款查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strSql;
            WFilter wf = new WFilter(0, "", false);
            wf.strSql = "select OccurTime 单据日期,CustID 客户编号,CustName 客户名称," +
                        "T_Receipt_Main_Det.MatId 商品编号,T_MatInf .MatName 商品名称," +
                        "Amount 含税金额,VerifyPerson 验收员 from T_Receipt_Main_Det " +
                        "LEFT JOIN T_MatInf ON T_MatInf .Matid =T_Receipt_Main_Det .MatId " +
                        "where ReceiptTypeID ='YS'";
            wf.s_items.Add("单据日期,OccurTime,N");
            wf.s_items.Add("客户编号,CustID,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("商品编号,T_Receipt_Main_Det.MatId,C");
            wf.s_items.Add("商品名称,T_MatInf .MatName,C");
            wf.s_items.Add("含税金额,Amount,N");
            wf.s_items.Add("验收员,VerifyPerson,C");
            //隐掉查询键
            wf.btnOK.Enabled = false;


            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                //返回条件框中的sql语句
                strSql = wf.Return_Sql;
                if (!(new DBUtil()).yn_exist_data(strSql))
                {
                    MessageBox.Show("不存在该条数据！", "提示");
                    return;
                }
                Connect(strSql, "YSKSearch");
            }
        }

        private void 历史应收应付ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            S70_71Form form = new S70_71Form();
            form.MdiParent=this;
            form.Show();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请拨打110！");
        }

        private void 库存结存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockStatusOperateForm form = new StockStatusOperateForm();
            form.curWorkMonth = this.curWorkMonth;
            form.ShowDialog();
        }

    }
}
