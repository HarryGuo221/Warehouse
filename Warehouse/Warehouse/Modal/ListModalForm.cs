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
using System.IO;
using Warehouse.Customer;
using Warehouse.Stock;
//using grproLib;

namespace Warehouse.Modal
{
    public partial class ListModalForm : Form
    {
        /// <summary>
        /// 当前单据编号01、03、51
        /// </summary>
        private string strReceTypeId;
        /// <summary>
        /// 当前单据名称
        /// </summary>
        private string strReceName;
        /// <summary>
        /// 当前工作月（格式如：201107）
        /// </summary>
        public string curWorkMonth;
        /// <summary>
        /// 单据上部显示项的列数
        /// </summary>
        private int columnCountTop = 3;
        /// <summary>
        /// 单据下部显示项的列数
        /// </summary>
        private int columnCountButtom = 3;

        private string type; //add\edit
        private string operateType; //记录操作类型
        private string strReceiptId = ""; //当前单据号
        public string curUserId = "";
        private string SStorehouseId = "";//当前单据的源仓库号

        //产生单据号时返回更新＂T_ReceiptRule＂的SQL语句
        private string SqlUpdateBillRull = "";
        private string calType = "移动加权平均";//成本计算方式（移动加权平均0，先进先出1）
        private bool isSaved = false; //默认没有保存

        SortedList<int, string> listReceMainTopItems;//单据总表上部项
        SortedList<int, string> listReceMainButtomItems;//单据总表下部项
        SortedList<int, string> listReceDetailItems;//单据子表项
        public List<MatInfo> matInfos_03;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strReceTypeId">单据类别编码：01，03……</param>
        /// <param name="type">添加、编辑</param>
        /// <param name="strReceiptId">单据号(提取单据信息、修改时)</param>
        /// <param name="operateType">操作类型（如：是核销03单的01单）</param>
        public ListModalForm(string strReceTypeId, string type, string strReceiptId, string operateType)
        {
            InitializeComponent();

            this.calType = SysConfigDAO.GetCalcMethod();//从系统设置表里取
            this.strReceTypeId = strReceTypeId;
            this.type = type;
            this.strReceiptId = strReceiptId;
            this.operateType = operateType;
            listReceMainTopItems = ReceiptModalCfgDAO.GetShowItems(this.strReceTypeId, 0, 0);//提取主表上部
            listReceMainButtomItems = ReceiptModalCfgDAO.GetShowItems(this.strReceTypeId, 0, 1);//提取主表下部
            listReceDetailItems = ReceiptModalCfgDAO.GetShowItems(this.strReceTypeId, 1, -1);     //提取子表部分      
        }

        private void ListModalForm_Load(object sender, EventArgs e)
        {
            this.curWorkMonth = (this.MdiParent as MainForm).curWorkMonth;//获得主窗体的当前工作月
            //初始化显示相关项
            InitInfo();            
            LayoutReceipt();//布局当前单据   

            //设置Tab键顺序            
            if (this.type == "add")
            {
                this.toolStripButtonFD.Enabled = false; //负单
                //初始化“税率”
                InitTaxRat();
            }
            else if (this.type == "edit")
            {
                this.toolStripButtonSave.Enabled = false;
                if (this.strReceTypeId != "55")
                ShowDatas();//提取已有的信息
            }
            //特殊单据（核销）
            if (this.operateType == "add_03_01") //选定的03单中特定的商品核销
            {
                //ShowDatas();//提取已有的信息
                ShowDatas03_01();//未核销的03单商品
            }
        }
        public void ShowDatas03_01()
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = "核销的03单单据号";
            column.Name = "s_ReceiptId01_03";
            column.ReadOnly = true;
            
            this.dataGridViewReceDetail.Columns.Add(column);//添加一列记录核销的是哪一个

            for (int i = 0; i < this.matInfos_03.Count; i++)
            {
                dataGridViewReceDetail.Rows.Add();
                dataGridViewReceDetail.Rows[i].Cells["商品编号"].Value = this.matInfos_03[i].matId.Trim();
                              
                //绑定商品信息
                dataGridViewReceDetail.ClearSelection();
                dataGridViewReceDetail.Rows[i].Selected = true;
                dataGridViewReceDetail.CurrentCell = dataGridViewReceDetail.Rows[i].Cells["商品编号"];
                DataBindMatInf(this.matInfos_03[i].matId.Trim(), this.matInfos_03[i].matType);

                if (dataGridViewReceDetail.Columns.Contains("n_num"))
                    dataGridViewReceDetail.Rows[i].Cells["n_num"].Value = this.matInfos_03[i].num.ToString().Trim();
                if (dataGridViewReceDetail.Columns.Contains("s_ReceiptId01_03"))
                    dataGridViewReceDetail.Rows[i].Cells["s_ReceiptId01_03"].Value = this.matInfos_03[i].receiptId.Trim();
                if (dataGridViewReceDetail.Columns.Contains("n_MatType"))
                    dataGridViewReceDetail.Rows[i].Cells["n_MatType"].Value = this.matInfos_03[i].matType.ToString().Trim();

                string SStoreHName = (new DBUtil()).Get_Single_val("T_StoreHouse","SHName","SHId",matInfos_03[i].SStoreHId.Trim());
                ComboBox comboBoxSStorehouse = ReceiptModCfg.GetControlByName(this.panelMain, "ComboBox", "comboBoxs_SourceStoreH") as ComboBox;
                comboBoxSStorehouse.Text = SStoreHName;
            }
            //冲销时“发票类型”默认为“增值税票”
            ComboBox comboBoxInvoiceType = ReceiptModCfg.GetControlByName(this.panelMain, "ComboBox", "comboBoxs_InvoiceType") as ComboBox;
            if (comboBoxInvoiceType != null)
                comboBoxInvoiceType.SelectedItem = "增值税票";
        }
        private void ShowDatas()
        {
            this.dataGridViewReceDetail.Rows.Clear();
            ReceiptModCfg.ClearControOfReceipt(this.panelMain);

            string strSql = "select * from T_Receipt_Main where ReceiptId='" + this.strReceiptId + "'";
            ReceiptModCfg.ShowDatas(this.panelMain, strSql);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            //客户、供应商区域
            if (listReceMainTopItems.Values.Contains("客户单位") || listReceMainButtomItems.Values.Contains("客户单位"))
                this.s_CustID.Text = dt.Rows[0]["CustID"].ToString().Trim();           
            DataBindCustomerInf(this.s_CustID.Text.Trim());

            //特殊字段
            ShowDatasSpeField(this.listReceMainTopItems); 
            ShowDatasSpeField(this.listReceMainButtomItems);

            #region //提取子表数据
            string strSql1 = "select * from T_Receipts_Det where ReceiptId='" + this.strReceiptId + "'";
            
            DataTable dt1 = (new SqlDBConnect()).Get_Dt(strSql1);
            if (dt1 == null || dt1.Rows.Count <= 0)
                return;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < listReceDetailItems.Count; j++)
                {
                    this.dataGridViewReceDetail.Rows.Add();
                    string columnNameAddType = ReceiptModCfg.GetReceiptDetailItems()[listReceDetailItems[j].Trim()].Trim();//列名(带前缀s_,n_)
                    string columnName = columnNameAddType.Substring(2);
                    if (columnNameAddType == "s_MatId")
                    {
                        this.dataGridViewReceDetail.Rows[i].Cells["商品编号"].Value = dt1.Rows[i][columnName].ToString().Trim();
                        string strMatID = dt1.Rows[i][columnName].ToString().Trim();
                        string strSql_ = "select MatName as 商品名称,Specifications as 规格型号,Units as 计量单位 from T_MatInf where MatID='{0}'";
                        strSql_ = string.Format(strSql_, strMatID);

                        SqlDBConnect db = new SqlDBConnect();
                        DataTable dt2 = db.Get_Dt(strSql_);
                        if (dt2 == null || dt2.Rows.Count <= 0)
                            return;
                        
                        this.dataGridViewReceDetail.Rows[i].Cells["商品名称"].Value = dt2.Rows[0]["商品名称"].ToString().Trim();
                        this.dataGridViewReceDetail.Rows[i].Cells["规格型号"].Value = dt2.Rows[0]["规格型号"].ToString().Trim();
                        this.dataGridViewReceDetail.Rows[i].Cells["计量单位"].Value = dt2.Rows[0]["计量单位"].ToString().Trim();
                        this.dataGridViewReceDetail.EndEdit();
                    }
                    else
                        this.dataGridViewReceDetail.Rows[i].Cells[columnNameAddType].Value = dt1.Rows[i][columnName].ToString().Trim();
                }
            }
            #endregion

            // 计算 合计 信息
            CulSum();
        }
        private void ShowDatasSpeField(SortedList<int, string> listReceMainItems)
        {
            for (int i = 0; i < listReceMainItems.Count; i++)
            {
                string strReceMainItem = listReceMainItems[i].Trim();
                if (strReceMainItem == "仓库" || strReceMainItem == "目标仓库")
                {
                    foreach (Control con in this.panelMain.Controls)
                    {
                        bool isFoud = false;
                        if (con is Panel)
                            foreach (Control o in (con as Panel).Controls)
                            {
                                string strUserName = "";
                                if (o is TextBox && (o as TextBox).Name == ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim())
                                {
                                    string strUserId = (o as TextBox).Text.Trim();
                                    if (strUserId == "")
                                        break;
                                    strUserName = (new DBUtil()).Get_Single_val("T_StoreHouse", "SHName", "SHId", strUserId);
                                    (con as Panel).Controls["comboBox" + ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim()].Text = strUserName;

                                    isFoud = true;
                                }
                            }
                        if (isFoud)
                            break;
                    }
                }
                else if (strReceMainItem == "发票类型")
                {
                    foreach (Control con in this.panelMain.Controls)
                    {
                        bool isFoud = false;
                        if (con is Panel)
                            foreach (Control o in (con as Panel).Controls)
                            {
                                string strUserName = "";
                                if (o is TextBox && (o as TextBox).Name == ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim())
                                {
                                    string strUserId = (o as TextBox).Text.Trim();
                                    if (strUserId == "")
                                        break;
                                    strUserName = (new DBUtil()).Get_Single_val("T_Invoice", "ITName", "ITCode", strUserId);
                                    (con as Panel).Controls["comboBox" + ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim()].Text = strUserName;

                                    isFoud = true;
                                }
                            }
                        if (isFoud)
                            break;
                    }
                }
                else if (strReceMainItem == "送货人" || strReceMainItem == "收货人" ||
                         strReceMainItem == "业务员" || strReceMainItem == "技术员" ||
                         strReceMainItem == "验收员" || strReceMainItem == "保管员" ||
                         strReceMainItem == "制单人" || strReceMainItem == "收款人" || strReceMainItem == "复核员")
                {
                    foreach (Control con in this.panelMain.Controls)
                    {
                        bool isFoud = false;
                        if (con is Panel)
                            foreach (Control o in (con as Panel).Controls)
                            {                                
                                if (o is TextBox && (o as TextBox).Name == ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim())
                                {
                                    string strUserName = (o as TextBox).Text.Trim();                                   
                                    (con as Panel).Controls[ ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim()].Text = strUserName;

                                    isFoud = true;
                                }
                            }
                        if (isFoud)
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 初始化显示相关项
        /// </summary>
        private void InitInfo() 
        {
            DBUtil dbUtil = new DBUtil();
            this.strReceName = dbUtil.Get_Single_val("T_ReceiptModal", "ReceName", "ReceTypeID", this.strReceTypeId);

            //设置窗体的标题
            this.Text = this.strReceTypeId + this.strReceName;
            this.s_ReceiptTypeID.Text = this.strReceTypeId.Trim();

            //设置当前类型单据的总条数、当前条数
            string strSql = "select ReceiptId from T_Receipt_Main where ReceiptTypeID='{0}' order by AutoRecordTime,ReceiptId";
            strSql = string.Format(strSql, this.strReceTypeId);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            int index = dt.Rows.Count + 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Trim() == this.strReceiptId)
                    index = i + 1;
            }

            this.toolStripTextBoxTotalNo.Text = dt.Rows.Count.ToString();
            this.toolStripTextBoxNo.Text = index.ToString();

        }        
        /// <summary>
        /// 布局当前单据
        /// </summary>
        private void LayoutReceipt()
        {            
            #region //单据总表上部
            if (listReceMainTopItems == null)
                return;            
            int left = 20;
            int top = 50;
            bool isColumnCountTopTimes = false;//是否是列数的倍数（为美观占满窗体）
            int panelCount = 0;
            int tabIndex = 2;
            for (int i = 0, j = 0; i < listReceMainTopItems.Count; i++,j++)
            {
                if (listReceMainTopItems[i].Trim() == "客户单位")
                {
                    string InOrOutBound = (new DBUtil()).Get_Single_val("T_ReceiptModal", "InOrOutBound", "ReceTypeID", this.strReceTypeId);

                    if (InOrOutBound == "入库")
                    {
                        this.labelCustName.Text = "供货\r\n单位";
                    }
                    else
                        this.labelCustName.Text = "客户\r\n单位";
                    
                    this.s_CustID.Controls.Add(CreatFormatLabel("客户单位编号"));
                    this.s_CustID.TabIndex = tabIndex;//Tab键顺序
                    continue;
                }                
                Panel panel = new Panel();
                LayoutPanelItem(listReceMainTopItems[i].Trim(), ref panel, ref tabIndex);//布局自定义子控件Panel

                this.panelMain.Controls.Add(panel);
                panelCount++;
                panel.Location = new Point(left, top);

                if (panelCount % this.columnCountTop == 0) //每行3个
                {
                    top = top + panel.Height + 5;//行高
                    left = 20;//和初始值保持一致
                    isColumnCountTopTimes = true;
                }
                else
                {
                    left = left + panel.Width + 1;//列间距
                    isColumnCountTopTimes = false;
                }
            }
            #endregion

            //客户或供应商信息 位置（必须要存在的）
            left = 20;
            if (isColumnCountTopTimes == false)
                top = top + 30;//不改变
           
            this.tableLayoutPanelCust.Location = new Point(left, top);
            top = top + this.tableLayoutPanelCust.Height + 5;                

            #region //单据子表中部
            this.dataGridViewReceDetail.Location = new Point(left, top);

            //添加列
            if (listReceDetailItems == null)
                return;
            for (int i = 0; i < listReceDetailItems.Count; i++)
            {
                DataGridViewTextBoxColumn dgvTextBoxColumn = new DataGridViewTextBoxColumn();
                dgvTextBoxColumn.HeaderText = listReceDetailItems[i].Trim();//列头文本
                dgvTextBoxColumn.Name = ReceiptModCfg.GetReceiptDetailItems()[listReceDetailItems[i].Trim()].Trim();//列名
                if (dgvTextBoxColumn.HeaderText == "物料编码")
                    continue;
                if (dgvTextBoxColumn.HeaderText == "税率" || dgvTextBoxColumn.HeaderText == "不含税金额" ||
                    dgvTextBoxColumn.HeaderText == "税额" || dgvTextBoxColumn.HeaderText == "成本总价" ||
                    dgvTextBoxColumn.HeaderText == "类型")
                {
                    dgvTextBoxColumn.ReadOnly = true;
                }
                dgvTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridViewReceDetail.Columns.Add(dgvTextBoxColumn);

            }
            #endregion

            //合计栏位置
            left = 20;
            top = top + this.dataGridViewReceDetail.Height + 5;
            this.tableLayoutPanelSum.Location = new Point(left, top);             

            #region //单据总表下部
            if (listReceMainButtomItems == null)
                return;

            left = 20;
            top = top + this.tableLayoutPanelSum.Height + 5;

            for (int i = 0; i < listReceMainButtomItems.Count; i++)
            {
                //创建一个Panel
                Panel panel = new Panel();
                LayoutPanelItem(listReceMainButtomItems[i].Trim(), ref panel, ref tabIndex);//布局自定义子控件Panel

                this.panelMain.Controls.Add(panel);
                panel.Location = new Point(left, top);

                if ((i + 1) % this.columnCountButtom == 0) //每行3个
                {
                    top = top + panel.Height + 5;//行高
                    left = 20;//和初始值保持一致
                }
                else
                {
                    left = left + panel.Width + 1;//列间距
                }
            }
            #endregion

            this.Height = top + 85;//窗体的高度
        }
        /// <summary>
        /// 根据默认选择的“发票类型”初始化“税率”
        /// </summary>
        private void InitTaxRat()
        {            
            ComboBox comboBoxInvoiceType = ReceiptModCfg.GetControlByName(this.panelMain, "ComboBox", "comboBoxs_InvoiceType") as ComboBox;
            if (comboBoxInvoiceType.SelectedIndex == 0)
                return;    

            //绑定单据子表中的“税率”
            foreach (DataGridViewRow dgvr in this.dataGridViewReceDetail.Rows)
            {
                string strTaxRate = (new DBUtil()).Get_Single_val("T_Invoice", "TaxRate", "ITName", comboBoxInvoiceType.Text.Trim());//税率
                  
                if (this.dataGridViewReceDetail.Columns.Contains("n_TaxRat"))
                {
                    dgvr.Cells["n_TaxRat"].Value = strTaxRate;
                    if (this.dataGridViewReceDetail.Columns.Contains("n_price") && dgvr.Cells["n_price"].Value != null)
                    {
                        double num = Convert.ToDouble(dgvr.Cells["n_num"].Value.ToString().Trim());
                        double price = Convert.ToDouble(dgvr.Cells["n_price"].Value.ToString().Trim());
                        double taxTat = Convert.ToDouble(dgvr.Cells["n_TaxRat"].Value.ToString().Trim());
                        //计算 不含税金额
                        if (this.dataGridViewReceDetail.Columns.Contains("n_NotTax"))
                            dgvr.Cells["n_NotTax"].Value = string.Format("{0:f2} ", num * price / (1 + taxTat));
                        //计算 税额
                        if (this.dataGridViewReceDetail.Columns.Contains("n_Tax"))
                            dgvr.Cells["n_Tax"].Value = string.Format("{0:f2} ", num * price - num * price / (1 + taxTat));

                        // 计算 合计 信息
                        CulSum();
                    }
                }
            }
        }
        /// <summary>
        /// 创建并格式化一个Label
        /// </summary>
        /// <param name="labelName"></param>
        private Label CreatFormatLabel(string labelName)
        {
            Label label = new Label();
            label.Width = 20;
            label.Height = 25;
            label.Dock = DockStyle.Right;
            label.Image = Warehouse.Properties.Resources.serch1;
            label.ImageAlign = ContentAlignment.MiddleCenter;// TopLeft;
            label.Name = labelName;//Label名称
            label.Click += new EventHandler(label_Click);
            return label;
        }
        /// <summary>
        /// 布局自定义子控件Panel
        /// </summary>
        /// <param name="listReceMainItems"></param>
        /// <param name="panel"></param>
        private void LayoutPanelItem(string strReceMainItem, ref Panel panel, ref int tabIndex)
        {
            //创建一个Panel            
            panel.Size = new System.Drawing.Size(10, 20);
            panel.AutoSize = true;

            //创建一个Label 
            Label label = new Label();
            panel.Controls.Add(label);
            label.AutoSize = true;
            label.Location = new Point(0, 3);

            string labText = Util.FormatStringRight(strReceMainItem.Trim(), 16);
            label.Text = labText;
            
            if (strReceMainItem == "订货类型" || strReceMainItem == "提货方式" || 
                strReceMainItem == "销售信息")
            {
                //创建一个ComboBox
                ComboBox comboBox = new ComboBox();
                panel.Controls.Add(comboBox);
                comboBox.Width = 150;
                comboBox.Location = new Point(label.Width + 5, 0);
                comboBox.Name = ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim();
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.TabIndex = tabIndex;

                List<string> itemVals = SelItemsDAO.GetItemVals("[单据]" + strReceMainItem);
                if (itemVals != null)
                    comboBox.Items.AddRange(itemVals.ToArray());
                comboBox.SelectedIndex = 0;

                //计算机Panel的宽度
                panel.Width = label.Width + 5 + comboBox.Width;
            }
            else if (strReceMainItem == "仓库" || strReceMainItem == "目标仓库" || strReceMainItem == "发票类型")
            {
                //创建一个ComboBox
                ComboBox comboBox = new ComboBox();
                panel.Controls.Add(comboBox);
                comboBox.Width = 150;
                comboBox.Location = new Point(label.Width + 5, 0);
                comboBox.Name = ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim();
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.TabIndex = tabIndex;
                comboBox.Name = "comboBox" + comboBox.Name;
                comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);

                //创建一个额外的TextBox，按规则命名，便于存储
                TextBox textBoxTemp = new TextBox();
                panel.Controls.Add(textBoxTemp);
                textBoxTemp.Visible = false;////
                textBoxTemp.BringToFront();
                textBoxTemp.Location = new Point(0, 0);
                textBoxTemp.Name = ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim();

                if (strReceMainItem == "仓库" || strReceMainItem == "目标仓库")
                {
                    (new InitFuncs()).InitComboBox(comboBox, "T_StoreHouse", "SHName");
                    comboBox.SelectedIndex = 1;
                }
                else if (strReceMainItem == "发票类型")
                {
                    (new InitFuncs()).InitComboBox(comboBox, "T_Invoice", "ITName");
                    if (this.strReceTypeId == "03" && comboBox.Items.Contains("增值税票"))
                        comboBox.SelectedItem = "增值税票";
                    else if (comboBox.Items.Contains("普通发票"))
                        comboBox.SelectedItem = "普通发票";
                   
                }                

                //计算机Panel的宽度
                panel.Width = label.Width + 5 + comboBox.Width;
            }
            else if (strReceMainItem == "预计入库日" || strReceMainItem == "生效起日" ||
                     strReceMainItem == "有效止日" || strReceMainItem == "送货日期" ||
                     strReceMainItem == "收货日期" || strReceMainItem == "送货要求到达时间" ||
                     strReceMainItem == "要求安装时间" || strReceMainItem == "汇款日期" ||
                     strReceMainItem == "单据日期")
            {
                //创建一个DateTimePicker
                DateTimePicker dateTimePicker = new DateTimePicker();
                panel.Controls.Add(dateTimePicker);
                dateTimePicker.Width = 150;
                dateTimePicker.Format = DateTimePickerFormat.Custom;
                dateTimePicker.CustomFormat = "yyyy-MM-dd";
                dateTimePicker.Location = new Point(label.Width + 5, 0);
                dateTimePicker.Name = ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim();
                dateTimePicker.TabIndex = tabIndex;

                //计算机Panel的宽度
                panel.Width = label.Width + 5 + dateTimePicker.Width;
  
            }
            else
            {
                //创建一个TextBox
                TextBox textBox = new TextBox();
                panel.Controls.Add(textBox);
                textBox.Width = 150;
                textBox.BorderStyle = BorderStyle.Fixed3D;
                textBox.Location = new Point(label.Width + 5, 0);
                textBox.Name = ReceiptModCfg.GetReceiptMainItems()[strReceMainItem].Trim();
                
                if (strReceMainItem == "送货人" || strReceMainItem == "收货人" ||
                    strReceMainItem == "业务员" || strReceMainItem == "技术员" ||
                    strReceMainItem == "验收员" || strReceMainItem == "保管员" ||
                    strReceMainItem == "制单人" || strReceMainItem == "收款人" || strReceMainItem == "复核员")
                {
                    Label label1 = new Label();
                    label1.Width = 20;
                    label1.Height = 25;
                    label1.Dock = DockStyle.Right;
                    label1.Image = Warehouse.Properties.Resources.serch1;
                    label1.ImageAlign = ContentAlignment.MiddleCenter;// TopLeft;
                    label1.Name = strReceMainItem;
                    label1.Click += new EventHandler(label_Click);
                    textBox.Controls.Add(label1);                                     
                    textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    textBox.Leave += new EventHandler(textBox_Leave);                                       

                    if (strReceMainItem == "制单人")
                        textBox.Text = (new DBUtil()).Get_Single_val("T_Users","UserName","UserId",this.curUserId);
                }
                else if (strReceMainItem == "单据号")
                {
                    textBox.ReadOnly = true;
                }
                else if (strReceMainItem == "自定义单号")
                {
                    textBox.TabIndex = 1;
                    textBox.Focus();
                }
                else if (strReceMainItem == "当前工作年月")
                {
                    textBox.Text = this.curWorkMonth;
                    textBox.ReadOnly = true;
                }
                else if (strReceMainItem == "用户账期")
                {                    
                    //textBox.ReadOnly = true;
                }
                else if (strReceMainItem == "发票号")
                {
                    textBox.MaxLength = 50;
                    textBox.Leave += new EventHandler(textBox_Leave);
                }
                if (textBox.ReadOnly != true)
                    textBox.TabIndex = tabIndex;
                else
                    textBox.TabStop = true;
               
                //计算Panel的宽度
                panel.Width = label.Width + 5 + textBox.Width;
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Name == "s_InvoiceNo") //发票号
            {
                //不足8位前面补零
                string text = textBox.Text.Trim();
                if (text.Length < 8)
                {
                    text = text.PadLeft(8, '0');
                }
                textBox.Text = text;
                return;
            }           
        }
        /// <summary>
        /// 响应 仓库、目标仓库、发票类型 的comboBox选择项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox);
            string comboBoxName = comboBox.Name.Trim();
            Panel curPanel = (sender as ComboBox).Parent as Panel;
            DBUtil dbUtil = new DBUtil();
            switch (comboBoxName)
            {
                #region Case选择
                case "comboBoxs_InvoiceType":
                    {
                        //发票类型
                        string strITCode = dbUtil.Get_Single_val("T_Invoice", "ITCode", "ITName", comboBox.Text.Trim());//发票类型编号
                        if (strITCode == "")
                            break;

                        //为辅助TextBox赋值
                        foreach (Control o in curPanel.Controls)
                        {
                            if (o is TextBox && (o as TextBox).Name == comboBoxName.Substring(8))
                                (o as TextBox).Text = strITCode;
                        }
                        try
                        {
                            //绑定单据子表中的“税率”
                            foreach (DataGridViewRow dgvr in this.dataGridViewReceDetail.Rows)
                            {
                                string strTaxRate = dbUtil.Get_Single_val("T_Invoice", "TaxRate", "ITName", comboBox.Text.Trim());//税率
                                //if (dgvr.Cells["商品编号"].Value != null)  
                                if (this.dataGridViewReceDetail.Columns.Contains("n_TaxRat"))
                                {
                                    dgvr.Cells["n_TaxRat"].Value = strTaxRate;
                                    if (this.dataGridViewReceDetail.Columns.Contains("n_price") && dgvr.Cells["n_price"].Value != null)
                                    {
                                        double num = Convert.ToDouble(dgvr.Cells["n_num"].Value.ToString().Trim());
                                        double price = Convert.ToDouble(dgvr.Cells["n_price"].Value.ToString().Trim());
                                        double taxTat = Convert.ToDouble(dgvr.Cells["n_TaxRat"].Value.ToString().Trim());
                                        //计算 不含税金额
                                        if (this.dataGridViewReceDetail.Columns.Contains("n_NotTax"))
                                            dgvr.Cells["n_NotTax"].Value = string.Format("{0:f2} ", num * price / (1 + taxTat));
                                        //计算 税额
                                        if (this.dataGridViewReceDetail.Columns.Contains("n_Tax"))
                                            dgvr.Cells["n_Tax"].Value = string.Format("{0:f2} ", num * price - num * price / (1 + taxTat));

                                        // 计算 合计 信息
                                        CulSum();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
                case "comboBoxs_SourceStoreH":
                case "comboBoxs_DestStoreH":
                    {
                        //仓库,目标仓库
                        string strCode = dbUtil.Get_Single_val("T_StoreHouse", "SHId", "SHName", comboBox.Text.Trim());
                        
                        foreach (Control o in curPanel.Controls)
                        {
                            if (o is TextBox && (o as TextBox).Name == comboBoxName.Substring(8))
                                (o as TextBox).Text = strCode;
                        }
                        //仓库ID                        
                        this.SStorehouseId = strCode;//仓库Id
                        if (string.Compare(this.strReceTypeId, "51") >= 0)
                        {
                            if (this.SStorehouseId == "X" || this.SStorehouseId == "H")
                            {
                                ComboBox comboBoxInvoiceType = ReceiptModCfg.GetControlByName(this.panelMain, "ComboBox", "comboBoxs_InvoiceType") as ComboBox;
                                comboBoxInvoiceType.SelectedItem = "服务票";
                            }
                        }
                        break;
                    }
                default:
                    break;
                #endregion
            }
        }
        /// <summary>
        /// 需特殊处理的textBox,为其添加响应Enter键
        /// </summary>
        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            TextBox textBox = (sender as TextBox);
            string textBoxText = textBox.Text.Trim();
            if (textBox.Text.Trim() == "")
                return;
            string textBoxName = textBox.Name.Trim();            
            switch (textBoxName)
            {
                #region Case选择
                case "s_DeliveryPerson":
                case "s_ReceiptPerson":
                case "s_Salesman":
                case "s_technician":
                case "s_VerifyPerson":
                case "s_Keeper":
                case "s_BillUserId":
                case "s_accountPerson":
                case "s_CheckPerson":
                    {
                        //送货人,收货人 业务员 技术员 验收员 保管员 制单人 收款人  复核员                      
                        string strSql = "select distinct UserId as 用户编码,UserName as 用户名,UserNameZJM as 用户名助记码,PersType as 类型,OfficeTel as 办公电话, MobileTel as 移动电话 " +
                                        "from T_Users where UserId like '%{0}%' or UserName like '%{1}%' or UserNameZJM like '%{2}%'";
                        strSql = string.Format(strSql, textBoxText, textBoxText, textBoxText);
                        //InitFuncs.FindInfoToControl(textBox, strSql, "用户名");
                        DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                        if (dt == null || dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("查找的信息不存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox.Text = "";
                            return;
                        }
                        if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
                        {
                            textBox.Text = dt.Rows[0]["类型"].ToString().Trim() + "/" + dt.Rows[0]["用户名"].ToString().Trim();
                        }
                        else
                        {
                            FilterInfoForm form = new FilterInfoForm(textBox, dt, "用户名", "类型");
                            form.StartPosition = FormStartPosition.CenterScreen;
                            form.ShowDialog();
                        }
                        
                        break;
                    }
                default:
                    break;
                #endregion
            } 
        }      
        /// <summary>
        /// 设置DataGridView列单元格的样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewReceDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                (e.Control as DataGridViewTextBoxEditingControl).Controls.Clear();
            }

            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                //检测列
                DataGridView dgv = (DataGridView)sender;
                if (dgv.CurrentCell.OwningColumn.HeaderText == "商品编号")
                {
                    //取得可以编辑被表示的控件
                    DataGridViewTextBoxEditingControl txtControl = (DataGridViewTextBoxEditingControl)e.Control;

                    Label label = new Label();
                    label.Width = 20;
                    label.Height = 25;
                    label.Image = Warehouse.Properties.Resources.serch1;
                    label.ImageAlign = ContentAlignment.MiddleCenter;// TopLeft;
                    label.Dock = DockStyle.Right;
                    label.Name = dgv.CurrentCell.OwningColumn.HeaderText.Trim();
                    label.Click += new EventHandler(label_Click);

                    txtControl.Controls.Clear();
                    txtControl.Controls.Add(label);
                    txtControl.Name = dgv.CurrentCell.OwningColumn.Name.Trim();                    
                }
                //else
                //{
                    ////控制某一列只能输入数字
                    //if (e.Control is TextBox)
                    //{
                    //    TextBox tb = e.Control as TextBox;
                    //    if (this.dataGridViewReceDetail.CurrentCell.OwningColumn.HeaderText == "数量" || this.dataGridViewReceDetail.CurrentCell.OwningColumn.HeaderText == "单价")
                    //        tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                    //    else
                    //        tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
                    //}
                //}
            }
            else if (e.Control is DataGridViewComboBoxEditingControl)
            {
                #region ComboBox列
                //检测列
                //DataGridView dgv = (DataGridView)sender;
                //if (dgv.CurrentCell.OwningColumn.HeaderText == "商品编号")
                //{
                //    //取得可以编辑被表示的控件 
                //    DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)e.Control;
                //    cb.DropDownStyle = ComboBoxStyle.DropDown;
                //    cb.SelectedIndexChanged += new EventHandler(comboBoxCell_SelectedIndexChanged);

                //    //获取数据
                //    List<string> datas = (new DBUtil()).GetOneFiledData("T_MatInf", "MatID");
                //    if (datas.Count <= 0)
                //        return;

                //    //向控件中添加数据                    
                //    cb.Items.Clear();
                //    cb.Items.AddRange(datas.ToArray());
                //}
                #endregion
            } 
        }
        
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }
        /// <summary>
        /// Label的响应事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void label_Click(object sender, EventArgs e)
        {
            string columnName = (sender as Label).Name;            
            switch (columnName)
            {
                #region case段
                case "商品编号":
                    {
                        string strMatId = InfoFind.Find_MatId();
                        DataBindMatInf(strMatId, 0);//绑定                       
                        break;
                    } 
                case "数量":
                    {                        
                        Label label = sender as Label;

                        TempInStoreForm form = new TempInStoreForm();
                        form.Location = new Point(MousePosition.X, MousePosition.Y);
                        form.ShowDialog();
                        break;
                    }
                case "客户单位名称":                    
                case "供应商名称":                    
                case "客户单位编号":
                case "供应商编号":
                    {
                        #region //筛选
                        WFilter wf = new WFilter(0, "T_CustomerInf.CustID", true);
                        wf.tableName = "T_CustomerInf";    //表名    
                        wf.strSql = "select T_CustomerInf.CustID as 客户编号, T_CustomerInf.CustName as 客户名称, T_CustomerInf.CustType as 类别," +
                                    "T_CustomerInf.PinYinCode as 拼音助记码,T_CustomerInf.whichTrade as 所在行业, T_CustomerInf.City as 城市地区,T_CustomerInf.Province as 省," +
                                    "T_CustContacts.CName as 联系人,T_CustContacts.Tel as 联系电话,T_CustomerInf.communicateAddr as 通信地址," +
                                    "T_CustomerInf.BankAccount as 银行帐号, T_CustomerInf.CredDegree as 信用等级, T_CustomerInf.InvoiceTitle as 发票抬头 " +
                                    "from T_CustomerInf,T_CustContacts " +
                                    "where T_CustomerInf.CustID=T_CustContacts.CustID and T_CustContacts.CType='默认联系人'";

                        wf.s_items.Add("客户编号,T_CustomerInf.CustID,C");
                        wf.s_items.Add("客户名称,T_CustomerInf.CustName,C");
                        wf.s_items.Add("通信地址,communicateAddr,C");
                        wf.s_items.Add("联系人,T_CustContacts.CName,C");
                        wf.s_items.Add("联系电话,Tel,C");            
                        wf.ShowDialog();
                        if (wf.DialogResult == DialogResult.OK)
                        {
                            string strCustID = "";
                            if (wf.Return_Items.Count > 0)                                
                                strCustID = wf.Return_Items[0].Trim();//获得客户单位编号
                           
                            DataBindCustomerInf(strCustID);//绑定
                        }
                        #endregion
                        break;
                    }
                case "发票类型":
                    {
                        #region //筛选
                        WFilter wf = new WFilter(0, "ITName", true);
                        wf.tableName = "T_Invoice";    //表名   
                        wf.strSql = "select ITCode as 发票类型编号,ITName as 发票类型名,TaxRate as 税率 " +
                                    "from T_Invoice";

                        wf.s_items.Add("发票类型编号,ITCode,C");
                        wf.s_items.Add("发票类型名,ITName,C");
                        wf.s_items.Add("税率,TaxRate,N");                        
                        wf.ShowDialog();
                        if (wf.DialogResult == DialogResult.OK)
                        {
                            if (wf.Return_Items.Count <= 0) //没有数据
                                return;
                            ((sender as Label).Parent as TextBox).Text = wf.Return_Items[0].Trim();//获得发票类型名
                        }
                        #endregion
                        break;
                    }
                case "仓库":                   
                case "目标仓库":
                    {
                        #region //筛选
                        WFilter wf = new WFilter(0, "SHName", true);
                        wf.tableName = "T_StoreHouse";    //表名   
                        wf.strSql = "select T_StoreHouse.SHId as 仓库编号,T_StoreHouse.SHName as 仓库名," +
                            "T_StoreHouse.SHKeeper as 库管员编号,T_StoreHouse.SHAddr as 仓库地址," +
                            "T_StoreHouse.Tel as 电话,T_StoreHouse.Fax as 传真,T_StoreHouse.NetAddr 网络地址," +
                            "T_StoreHouse.Storememo as 备注 from T_StoreHouse";

                        wf.s_items.Add("仓库编号,SHId,C");
                        wf.s_items.Add("仓库名,SHName,C");
                        wf.s_items.Add("库管员编号,SHKeeper,C");
                        wf.s_items.Add("仓库地址,SHAddr,C");
                        wf.ShowDialog();
                        if (wf.DialogResult == DialogResult.OK)
                        {
                            if (wf.Return_Items.Count <= 0) //没有数据
                                return;
                            ((sender as Label).Parent as TextBox).Text = wf.Return_Items[0].Trim();//获得仓库名称
                        }
                        #endregion
                        break;
                    }
                case "送货人":
                case "收货人":
                case "业务员":
                case "技术员":
                case "验收员":
                case "保管员":
                case "制单人":
                case "收款人":
                case "复核员":
                    {
                        #region //筛选
                        WFilter wf = new WFilter(0, "UserName", true);
                        wf.tableName = "T_users";    //表名   
                        wf.strSql = "select distinct T_Users.UserId as 用户编码, T_Users.UserName as 用户名, T_Users.ynAdmin as 是否系统管理员,T_Branch.BName as 所属部门," +
                                    "T_Users.JobPosition as 职位,T_UserType.UTypeName as 类别,T_Users.SmsTel as 接收短信电话号码 " +
                                    "from [T_Users] left join T_Branch " +
                                    "on T_Users.BranchId=T_Branch.BId left join T_UserType on T_Users.DefaultUserType=T_UserType.TypeId";

                        wf.s_items.Add("用户编码,UserId,C");
                        wf.s_items.Add("用户名,UserName,C");
                        wf.s_items.Add("所属部门,BName,C");
                        wf.s_items.Add("职位,JobPosition,C");
                        wf.s_items.Add("类别,UTypeName,C");
                        wf.ShowDialog();
                        if (wf.DialogResult == DialogResult.OK)
                        {
                            if (wf.Return_Items.Count <= 0) //没有数据
                                return;
                            ((sender as Label).Parent as TextBox).Text = wf.Return_Items[0].Trim();//获得用户名称
                        }
                        #endregion
                        break;
                    }
                default:
                    break;
                #endregion
            }            
        }
        /// <summary>
        /// 根据商品编号绑定 商品属性 信息
        /// </summary>
        /// <param name="strMatCode"></param>
        private void DataBindMatInf(string strMatID, int intMatType)
        {            
            string strSql = "select MatName as 商品名称,Specifications as 规格型号,Units as 计量单位 from T_MatInf where MatID='{0}'";
            strSql = string.Format(strSql, strMatID);

            int currentRowIndex = this.dataGridViewReceDetail.CurrentCell.RowIndex;
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);
            if (strMatID == "" || dt == null || dt.Rows.Count <= 0)
            {                
                this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["商品编号"].Value = "";
                this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["商品名称"].Value = "";
                this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["规格型号"].Value = "";
                this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["计量单位"].Value = "";
                this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["n_MatType"].Value = "0";
                if (this.dataGridViewReceDetail.Columns.Contains("n_STaxPurchPrice"))
                {
                    this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["n_STaxPurchPrice"].Value = "";
                    this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["n_TTaxPurchPrice"].Value = "";
                }               
                return;
            } 
            
            //this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["商品编号"].Value = strMatID;
            if (this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["商品编号"].Value != null && this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["商品编号"].Value.ToString().Trim() != "")
            {
                this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["商品名称"].Value = dt.Rows[0]["商品名称"].ToString().Trim();
                this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["规格型号"].Value = dt.Rows[0]["规格型号"].ToString().Trim();
                this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["计量单位"].Value = dt.Rows[0]["计量单位"].ToString().Trim();
                this.dataGridViewReceDetail.Rows[currentRowIndex].Cells["n_MatType"].Value = intMatType;
            }
            
            //出库时显示成本单价
            if (this.dataGridViewReceDetail.Columns.Contains("n_STaxPurchPrice"))
            {
                ComboBox comboBoxSStorehouse = ReceiptModCfg.GetControlByName(this.panelMain, "ComboBox", "comboBoxs_SourceStoreH") as ComboBox;
                string SStorehouseId = (new DBUtil()).Get_Single_val("T_StoreHouse", "SHId", "SHName", comboBoxSStorehouse.Text.Trim());//仓库Id
                string matId = this.dataGridViewReceDetail.CurrentRow.Cells["商品编号"].Value.ToString().Trim();
                int matType = Convert.ToInt32(this.dataGridViewReceDetail.CurrentRow.Cells["n_MatType"].Value.ToString().Trim());

                double costPrice = CalculateCostPriceOut(matId, matType, SStorehouseId);
                this.dataGridViewReceDetail.CurrentRow.Cells["n_STaxPurchPrice"].Value = string.Format("{0:f2} ", costPrice);
            }

            this.dataGridViewReceDetail.EndEdit();
        }
        /// <summary>
        /// 根据客户编号绑定 客户属性 信息
        /// </summary>
        /// <param name="strMatCode"></param>
        private void DataBindCustomerInf(string strCustID)
        {
            string strSql = "select T_CustomerInf.CustID,T_CustomerInf.CustName,T_CustomerInf.communicateAddr,"+
                                   "T_CustomerInf.TaxRegistNumber,T_CustomerInf.AtBank,T_CustomerInf.BankAccount,T_CustomerInf.AccountPeriod " +
                            "from T_CustomerInf where CustID='{0}'";
            strSql = string.Format(strSql, strCustID);           

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
            {
                this.s_CustID.Text = "";
                this.s_CustName.Text = "";
                this.txtAddressAndTel.Text = "";
                this.txtTaxRegistNumber.Text = "";
                this.txtBank.Text = "";
                this.txtBankAccount.Text = "";

                return;
            }
            //默认联系人电话
            string tel = "";
            string communicateAddr = dt.Rows[0]["communicateAddr"].ToString().Trim();
            string strSql1 = "select Tel from T_CustContacts where CustID='{0}' and CType='{1}'";
            strSql1 = string.Format(strSql1, strCustID, "默认联系人");
            DataTable dt1 = db.Get_Dt(strSql1);
            if (dt1 != null && dt1.Rows.Count > 0)
                tel = dt1.Rows[0]["Tel"].ToString().Trim();
            
            this.s_CustID.Text = dt.Rows[0]["CustID"].ToString().Trim();
            this.s_CustName.Text = dt.Rows[0]["CustName"].ToString().Trim();
            if (tel == "")
                this.txtAddressAndTel.Text = communicateAddr;
            else
                this.txtAddressAndTel.Text = communicateAddr == "" ? tel : communicateAddr + "," + tel;
            this.txtTaxRegistNumber.Text = dt.Rows[0]["TaxRegistNumber"].ToString().Trim();
            this.txtBank.Text = dt.Rows[0]["AtBank"].ToString().Trim();
            this.txtBankAccount.Text = dt.Rows[0]["BankAccount"].ToString().Trim();
                       
            //用户默认账期
            foreach (Control control in this.panelMain.Controls)
            {
                bool isFind = false;
                if (control is Panel)
                {                   
                    foreach (Control o in (control as Panel).Controls)
                    {
                        if (o is TextBox && (o as TextBox).Name == "s_OffPeriod")
                        {
                            (o as TextBox).Text = dt.Rows[0]["AccountPeriod"].ToString().Trim();
                            isFind = true;
                            break;
                        }
                    }
                }
                if (isFind)
                    break;
            }                            
        }

        private void dataGridViewReceDetail_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //DataGridView dgv = (DataGridView)sender;
            //检测列 
            //if (dgv.Columns[e.ColumnIndex].HeaderText == "商品编号" && dgv.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn) 
            //{
            //    DataGridViewComboBoxColumn cbc = (DataGridViewComboBoxColumn)dgv.Columns[e.ColumnIndex];
            //    //追加ComboBox的项目
            //    if (!cbc.Items.Contains(e.FormattedValue))
            //    {
            //        cbc.Items.Add(e.FormattedValue);
            //    }
            //}     
            //this.dataGridViewReceDetail.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridViewReceDetail_EditingControlShowing);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (this.isSaved)
            {
                MessageBox.Show("该单据已经保存！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try 
            {
                this.dataGridViewReceDetail.EndEdit();
                DBUtil dbUtil = new DBUtil();
                ComboBox comboBoxSStorehouse = new ComboBox(); //仓库
                ComboBox comboBoxDStorehouse = new ComboBox(); //目标仓库
               
                #region 验证
                if (!this.listReceMainTopItems.Values.Contains("单据号") && !this.listReceMainButtomItems.Values.Contains("单据号"))
                {
                    MessageBox.Show("该单据必须要有单据号！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (this.listReceMainTopItems.Values.Contains("仓库") || this.listReceMainButtomItems.Values.Contains("仓库"))
                {
                    comboBoxSStorehouse = ReceiptModCfg.GetControlByName(this.panelMain, "ComboBox", "comboBoxs_SourceStoreH") as ComboBox;
                    if (comboBoxSStorehouse.SelectedIndex == 0)
                    {
                        MessageBox.Show("仓库必选！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (this.listReceMainTopItems.Values.Contains("目标仓库") || this.listReceMainButtomItems.Values.Contains("目标仓库"))
                {
                    comboBoxDStorehouse = ReceiptModCfg.GetControlByName(this.panelMain, "ComboBox", "comboBoxs_DestStoreH") as ComboBox;
                    if (comboBoxDStorehouse.SelectedIndex == 0)
                    {
                        MessageBox.Show("目标仓库必选！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                #endregion
                
                #region //add
                if (this.type == "add")
                {
                    //单据号文本框
                    TextBox textBoxReceiptId = ReceiptModCfg.GetControlByName(this.panelMain, "TextBox", "s_ReceiptId") as TextBox;
                    //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句                
                    textBoxReceiptId.Text = DBUtil.Produce_Bill_Id(this.strReceTypeId.Trim(), DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull);
                    this.strReceiptId = textBoxReceiptId.Text.Trim();

                    //插入到单据总表
                    string strSql = ReceiptModCfg.Build_Insert_Sql_Panel(this.panelMain, "T_Receipt_Main");//插入到单据总表            
                    List<string> sqls = new List<string>();
                    InitFuncs initFuncs = new InitFuncs();
                    sqls.Add(strSql);
                    sqls.Add(this.SqlUpdateBillRull);//更新单据号

                    //更新单据总表的“当前工作年月”
                    string strSqlUpdate = "update T_Receipt_Main set CurWorkMonth='{0}',AutoRecordTime='{1}' where ReceiptId='{2}'";
                    strSqlUpdate = string.Format(strSqlUpdate, this.curWorkMonth, DBUtil.getServerTime(), strReceiptId);
                    sqls.Add(strSqlUpdate);
                                        
                    if (this.calType == "移动加权平均") //移动加权平均0
                    {                         
                        int num1 = 0;
                        string receiptId20 = "";
                        string receiptId90 = "";

                        //插入到单据子表
                        for (int j = 0; j < this.dataGridViewReceDetail.Rows.Count - 1; j++)                        
                        {
                            DataGridViewRow dr = this.dataGridViewReceDetail.Rows[j];
                            if (dr.Cells["商品编号"].Value == null || dr.Cells["商品编号"].Value.ToString().Trim() == "")
                                continue;
                            string custId = this.s_CustID.Text.Trim();
                            string custName = this.s_CustName.Text.Trim();
                            string matId = dr.Cells["商品编号"].Value.ToString().Trim();
                            int matType = Convert.ToInt32(dr.Cells["n_MatType"].Value.ToString().Trim());
                            int orderNo = dr.Index + 1; //顺序号
                            double num = 0;
                            if (dr.Cells["n_num"].Value != null && dr.Cells["n_num"].Value.ToString().Trim() != "")
                                num = Convert.ToDouble(dr.Cells["n_num"].Value.ToString().Trim());
                            double price = 0.0;
                            if (dr.Cells["n_price"].Value != null && dr.Cells["n_price"].Value.ToString().Trim() != "")
                                price = Convert.ToDouble(dr.Cells["n_price"].Value.ToString().Trim());
                                                        
                            if (this.operateType != "add_03_01") //不是核销03单的操作
                            {
                                #region 不是核销03单的操作
                                string strSqlDetail = ReceiptModCfg.Build_Insert_Sql_Receipts_Det(strReceiptId, this.dataGridViewReceDetail, dr); //插入单据子表
                                sqls.Add(strSqlDetail);

                                string InOrOutBound = dbUtil.Get_Single_val("T_ReceiptModal", "InOrOutBound", "ReceTypeID", this.strReceTypeId);
                                
                                if (InOrOutBound == "入库")
                                {                             
                                    //更新成本单价、成本金额
                                    string strSqlUpdateCost = "update T_Receipts_Det set STaxPurchPrice={0},TTaxPurchPrice={1} where ReceiptId='{2}' and OrderNo={3}";
                                    strSqlUpdateCost = string.Format(strSqlUpdateCost, price, price * num, strReceiptId, orderNo);
                                    sqls.Add(strSqlUpdateCost);

                                    //如果是03单，要同时更新 YnCompleteVerificate_03 字段
                                    if (this.strReceTypeId == "03")
                                    {
                                        string strSqlUpdate_03 = "update T_Receipts_Det set YnCompleteVerificate_03='false' where ReceiptId='{0}' and OrderNo={1}";
                                        strSqlUpdate_03 = string.Format(strSqlUpdate_03, strReceiptId, orderNo);

                                        sqls.Add(strSqlUpdate_03);
                                    }
                                }                                
                                else if (InOrOutBound == "出库")
                                {
                                    //直接保存（已包含了成本单价、成本金额）  
                                    /*暂时取消验证*/
                                    //double curStockNum = 0;
                                    //StockStatus stockStatus = ReceiptModCfg.GetStockNum(SStorehouseId, matId, matType);
                                    //curStockNum = stockStatus.stockNum;

                                    //if (num > curStockNum)
                                    //{
                                    //    MessageBox.Show(matId +"，类型"+matType+"，没有足够的库存", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    return;
                                    //}
                                }
                                #endregion
                            }
                            else if (this.operateType == "add_03_01") //核销03单的01单
                            {
                                #region //核销03单的01单
                                string SStorehouseId = (new DBUtil()).Get_Single_val("T_StoreHouse", "SHId", "SHName", comboBoxSStorehouse.Text.Trim());//仓库Id
                                DateTimePicker dtpOccurTime = ReceiptModCfg.GetControlByName(this.panelMain, "DateTimePicker", "s_OccurTime") as DateTimePicker; //单据日期
                         
                                if (dr.Cells["s_ReceiptId01_03"].Value != null && dr.Cells["s_ReceiptId01_03"].Value.ToString().Trim() != "")
                                {
                                    //自动产生一个20单，一个90单
                                    if (num > this.matInfos_03[j].notVerNum)
                                    {
                                        MessageBox.Show("数量不能大于未冲销数！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    //插入该01单
                                    string strSqlDetail = ReceiptModCfg.Build_Insert_Sql_Receipts_Det(strReceiptId, this.dataGridViewReceDetail, dr);
                                    sqls.Add(strSqlDetail);

                                    //更新01单成本单价、成本金额
                                    string strSqlUpdateCostPrice_01 = "update T_Receipts_Det set STaxPurchPrice={0},TTaxPurchPrice={1} where ReceiptId='{2}' and OrderNo={3}";
                                    strSqlUpdateCostPrice_01 = string.Format(strSqlUpdateCostPrice_01, price, price * num, strReceiptId, orderNo);
                                    sqls.Add(strSqlUpdateCostPrice_01);                                   

                                    num1++;
                                    //生成20单("核销的03单单据号" 列不为空)                                
                                    if (num1 == 1)//子表有多行，主表只插入一条记录
                                    {
                                        //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
                                        string SqlUpdateBillRull20 = "";
                                        receiptId20 = DBUtil.Produce_Bill_Id("20", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull20);

                                        string strSqlMain_20 = "insert into T_Receipt_Main(ReceiptId,CurWorkMonth,OccurTime,AutoRecordTime,SourceStoreH,ReceiptTypeID,CustId,CustName) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                                        strSqlMain_20 = string.Format(strSqlMain_20, receiptId20, this.curWorkMonth, dtpOccurTime.Value, DBUtil.getServerTime().ToString().Trim(), SStorehouseId, "20",
                                                                      custId, custName);
                                        sqls.Add(strSqlMain_20);
                                        sqls.Add(SqlUpdateBillRull20);//更新单据号
                                    }
                                    string strSqlDet_20 = "insert into T_Receipts_Det(ReceiptId,OrderNo,MatId,MatType,price,num,Amount,STaxPurchPrice,TTaxPurchPrice,ReceiptId20_03) values('{0}',{1},'{2}',{3},{4},{5},{6},{7},{8},'{9}')";
                                    strSqlDet_20 = string.Format(strSqlDet_20, receiptId20, orderNo, matId, matType, this.matInfos_03[j].price, -num, this.matInfos_03[j].price * (-num),
                                                                               this.matInfos_03[j].price, this.matInfos_03[j].price * (-num), this.matInfos_03[j].receiptId);
                                    sqls.Add(strSqlDet_20);
                                    
                                    //生成90单("核销的03单单据号" 列不为空)
                                    if (num1 == 1) 
                                    {
                                        //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
                                        string SqlUpdateBillRull90 = "";
                                        receiptId90 = DBUtil.Produce_Bill_Id("90", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull90);

                                        string strSqlMain_90 = "insert into T_Receipt_Main(ReceiptId,CurWorkMonth,OccurTime,AutoRecordTime,SourceStoreH,ReceiptTypeID,CustId, CustName) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                                        strSqlMain_90 = string.Format(strSqlMain_90, receiptId90, this.curWorkMonth, dtpOccurTime.Value, DBUtil.getServerTime().ToString().Trim(), SStorehouseId, "90",
                                                                      custId, custName);
                                        sqls.Add(strSqlMain_90);
                                        sqls.Add(SqlUpdateBillRull90);//更新单据号

                                    }
                                    //90单只记录一个差额
                                    string strSqlDet_90 = "insert into T_Receipts_Det(ReceiptId,OrderNo,MatId,MatType,TTaxPurchPrice,ReceiptId90_03) values('{0}',{1},'{2}',{3},{4},'{5}')";
                                    strSqlDet_90 = string.Format(strSqlDet_90, receiptId90, orderNo, matId, matType, (price - this.matInfos_03[j].price) * num, this.matInfos_03[j].receiptId); //不计数量、单价
                                    sqls.Add(strSqlDet_90);
                                                                        
                                    //更新03单的已核销数量 AlreadyVerificateNum_03
                                    string strSqlUpdate_AlreadyVerificateNum_03 = "update T_Receipts_Det set AlreadyVerificateNum_03=AlreadyVerificateNum_03 + {0} where ReceiptId='{1}' and OrderNo={2}";
                                    strSqlUpdate_AlreadyVerificateNum_03 = string.Format(strSqlUpdate_AlreadyVerificateNum_03, num, this.matInfos_03[j].receiptId, this.matInfos_03[j].orderNo);
                                    sqls.Add(strSqlUpdate_AlreadyVerificateNum_03);

                                    //如果该条物料核销完，更新相应03单的YnCompleteVerificate_03='true'
                                    if (this.matInfos_03[j].notVerNum + (-num) <= 0)
                                    {
                                        string strSqlUpdate_03 = "update T_Receipts_Det set YnCompleteVerificate_03='true' where ReceiptId='{0}' and OrderNo={1}";
                                        strSqlUpdate_03 = string.Format(strSqlUpdate_03, this.matInfos_03[j].receiptId, this.matInfos_03[j].orderNo);
                                        sqls.Add(strSqlUpdate_03);
                                    }

                                    //更新01单子表该条记录的 ReceiptId01_03
                                    string strSqlUpate_01 = "update T_Receipts_Det set ReceiptId01_03='{0}' where ReceiptId='{1}' and OrderNo={2}";
                                    strSqlUpate_01 = string.Format(strSqlUpate_01, this.matInfos_03[j].receiptId.Trim(), strReceiptId, orderNo);
                                    sqls.Add(strSqlUpate_01);

                                    //把核销过程记录进 核销明细表
                                    string strHxDet = "insert into T_Receipts_HxDet(ReceiptId01,Order01,ReceiptId03,Order03,StockInPrice,doNum,MatId) values('{0}',{1},'{2}',{3},{4},{5},'{6}')";
                                    strHxDet = string.Format(strHxDet, strReceiptId, orderNo, this.matInfos_03[j].receiptId, this.matInfos_03[j].orderNo, this.matInfos_03[j].price, num, matId);
                                    sqls.Add(strHxDet);

                                }
                                else //01单中 不是冲销的物料
                                {
                                    //插入该01单
                                    string strSqlDetail = ReceiptModCfg.Build_Insert_Sql_Receipts_Det(strReceiptId, this.dataGridViewReceDetail, dr);
                                    sqls.Add(strSqlDetail);
                                                                       
                                    //更新01单成本单价、成本金额
                                    string strSqlUpdateCostPrice_01 = "update T_Receipts_Det set STaxPurchPrice={0},TTaxPurchPrice={1} where ReceiptId='{2}' and OrderNo={3}";
                                    strSqlUpdateCostPrice_01 = string.Format(strSqlUpdateCostPrice_01, price, price * num, strReceiptId, orderNo);
                                    sqls.Add(strSqlUpdateCostPrice_01);                                    
                                }
                                #endregion
                            }                                
                        }
                    }                   
                    else if (this.calType == "先进先出") //先进先出
                    {
                        if (this.operateType != "add_03_01") //不是核销03单的操作
                        {
                            int orderNo = 0; //记录出库商品的行号（如果一行商品销售的是多批次入库的商品，要产生多条出库记录）
                            //插入到单据子表            
                            for (int j = 0; j < this.dataGridViewReceDetail.Rows.Count - 1; j++)
                            {                                
                                DataGridViewRow dr = this.dataGridViewReceDetail.Rows[j];
                                if (dr.Cells["商品编号"].Value == null || dr.Cells["商品编号"].Value.ToString().Trim() == "")
                                    continue;
                                string matId = dr.Cells["商品编号"].Value.ToString().Trim();
                                int matType = Convert.ToInt32(dr.Cells["n_MatType"].Value.ToString().Trim());

                                string InOrOutBound = dbUtil.Get_Single_val("T_ReceiptModal", "InOrOutBound", "ReceTypeID", this.strReceTypeId);
                                
                                if (InOrOutBound == "入库")
                                {
                                    #region //入库
                                    //插入到单据子表
                                    string strSqlDetail = ReceiptModCfg.Build_Insert_Sql_Receipts_Det(strReceiptId, this.dataGridViewReceDetail, dr);
                                    sqls.Add(strSqlDetail);

                                    //如果是03单，要同时更新 YnCompleteVerificate_03 字段
                                    if (this.strReceTypeId == "03")
                                    {
                                        string strSqlUpdate_03 = "update T_Receipts_Det set YnCompleteVerificate_03='false' where ReceiptId='{0}' and OrderNo={1}";
                                        strSqlUpdate_03 = string.Format(strSqlUpdate_03, strReceiptId, orderNo);

                                        sqls.Add(strSqlUpdate_03);
                                    }
                                    #endregion
                                }
                                else if (InOrOutBound == "出库")
                                {
                                    #region //出库
                                    double num = Convert.ToDouble(dr.Cells["n_num"].Value.ToString().Trim());
                                    string storehouseId = (new DBUtil()).Get_Single_val("T_StoreHouse", "SHId", "SHName", comboBoxSStorehouse.Text.Trim());//仓库Id
                                    string maxBalanceTime = StockStatusDAO.GetBalanceTime();

                                    //更新成本单价(为入库时的进货价，如果销售的是多批次的商品，要产生多条出库记录)
                                    string strSqlSel = "select OccurTime, T_Receipts_Det.num,T_Receipts_Det.price from T_Receipts_Det,T_Receipt_Main " +
                                                       "where (ReceiptTypeID='01' or ReceiptTypeID='03') and SourceStoreH='{0}' and " +
                                                                "T_Receipt_Main.CurWorkMonth >= '{1}' and T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and " +
                                                                "T_Receipts_Det.ReceiptId01_03 is NULL and T_Receipts_Det.YnCompleteOutStock='false'" +
                                                                "T_Receipts_Det.MatId='{2}' and T_Receipts_Det.MatType={3}" +
                                                       "order by OccurTime"; //01、03单中没有完全出库，且不是冲销的01单
                                    strSqlSel = string.Format(strSqlSel, storehouseId, maxBalanceTime, dr.Cells["商品编号"].Value.ToString().Trim(), dr.Cells["n_MatType"].Value.ToString().Trim());
                                    DataTable dtDet = (new SqlDBConnect()).Get_Dt(strSqlSel);

                                    if (dtDet != null && dtDet.Rows.Count > 0)
                                    {
                                        double numSum = 0;
                                        Dictionary<double, double> NumPrice = new Dictionary<double, double>();                                       
                                        for (int i = 0; i < dtDet.Rows.Count; i++)
                                        {                                           
                                            numSum = numSum + Convert.ToDouble(dtDet.Rows[i]["num"].ToString().Trim());
                                            if (num <= numSum)
                                            {
                                                NumPrice.Add(num - numSum, Convert.ToDouble(dtDet.Rows[i]["price"].ToString().Trim()));
                                                break;
                                            }
                                            else
                                            {
                                                NumPrice.Add(Convert.ToDouble(dtDet.Rows[i]["num"].ToString().Trim()), Convert.ToDouble(dtDet.Rows[i]["price"].ToString().Trim()));
                                            }                                            
                                        } //找到销售的是一共有几批次的商品
                                        if (numSum < num)
                                        {
                                            MessageBox.Show(matId +",类型"+matType+"库存不够，请重新输入数量！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        else
                                        {
                                            foreach (int keyNum in NumPrice.Keys)
                                            {
                                                orderNo++;
                                                //插入到单据子表
                                                string strSqlDetail = ReceiptModCfg.Build_Insert_Sql_Receipts_Det(strReceiptId, this.dataGridViewReceDetail, dr, orderNo,
                                                                                                                    keyNum, NumPrice[keyNum]);
                                                sqls.Add(strSqlDetail);
                                            }                                            
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("该商品没有库存！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    #endregion
                                }
                            }
                        }
                        else if (this.operateType == "add_03_01") //核销03单的01单
                        {
                            #region //核销03单的01单
                            int num1 = 0;
                            string receiptId20 = "";
                            string receiptId90 = "";
                            //自动产生一个20单，一个90单
                            for (int j = 0; j < this.dataGridViewReceDetail.Rows.Count - 1; j++)
                            {
                                DataGridViewRow dr = this.dataGridViewReceDetail.Rows[j];
                                if (dr.Cells["商品编号"].Value == null || dr.Cells["商品编号"].Value.ToString().Trim() == "")
                                    continue;
                                string matId = dr.Cells["商品编号"].Value.ToString().Trim();
                                int matType = Convert.ToInt32(dr.Cells["n_MatType"].Value.ToString().Trim());
                                int orderNo = dr.Index + 1; //顺序号
                                double num = 0;
                                if (dr.Cells["n_num"].Value != null && dr.Cells["n_num"].Value.ToString().Trim() != "")
                                    num = Convert.ToDouble(dr.Cells["n_num"].Value.ToString().Trim());
                                double price = 0.0;
                                if (dr.Cells["n_price"].Value != null && dr.Cells["n_price"].Value.ToString().Trim() != "")
                                    price = Convert.ToDouble(dr.Cells["n_price"].Value.ToString().Trim());

                                if (dr.Cells["s_ReceiptId01_03"].Value != null && dr.Cells["s_ReceiptId01_03"].Value.ToString().Trim() != "")
                                {
                                    //插入该01单
                                    string strSqlDetail = ReceiptModCfg.Build_Insert_Sql_Receipts_Det(strReceiptId, this.dataGridViewReceDetail, dr);
                                    sqls.Add(strSqlDetail);

                                    string SStorehouseId = (new DBUtil()).Get_Single_val("T_StoreHouse", "SHId", "SHName", comboBoxSStorehouse.Text.Trim());//仓库Id
                                    DateTimePicker dtpOccurTime = ReceiptModCfg.GetControlByName(this.panelMain, "DateTimePicker", "s_OccurTime") as DateTimePicker; //单据日期
                         
                                    num1++;
                                    //生成20单("核销的03单单据号" 列不为空)                                
                                    if (num1 == 1)
                                    {
                                        //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
                                        string SqlUpdateBillRull20 = "";
                                        receiptId20 = DBUtil.Produce_Bill_Id("20", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull20);

                                        string strSqlMain_20 = "insert into T_Receipt_Main(ReceiptId,CurWorkMonth,OccurTime,AutoRecordTime,SourceStoreH,ReceiptTypeID,CustId,CustName) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                                        strSqlMain_20 = string.Format(strSqlMain_20, receiptId20, this.curWorkMonth, dtpOccurTime.Value, DBUtil.getServerTime().ToString().Trim(), SStorehouseId, "20",
                                                                      this.s_CustID.Text.Trim(),this.s_CustName.Text.Trim());
                                        sqls.Add(strSqlMain_20);
                                        sqls.Add(SqlUpdateBillRull20);//更新单据号
                                    }
                                    string strSqlDet_20 = "insert into T_Receipts_Det(ReceiptId,OrderNo,MatId,MatType,price,num,Amount,ReceiptId20_03) values('{0}',{1},'{2}',{3},{4},{5},{6},'{7}')";
                                    strSqlDet_20 = string.Format(strSqlDet_20, receiptId20, orderNo, matId,matType, this.matInfos_03[j].price, -num, this.matInfos_03[j].price* (-num), this.matInfos_03[j].receiptId);
                                    sqls.Add(strSqlDet_20);                                                                       

                                    //生成90单("核销的03单单据号" 列不为空)                                
                                    if (num1 == 1) //子表有多行，主表直插入一条记录
                                    {
                                        //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
                                        string SqlUpdateBillRull90 = "";
                                        receiptId90 = DBUtil.Produce_Bill_Id("90", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull90);

                                        string strSqlMain_90 = "insert into T_Receipt_Main(ReceiptId,CurWorkMonth,OccurTime,AutoRecordTime,SourceStoreH,ReceiptTypeID,CustId,CustName) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                                        strSqlMain_90 = string.Format(strSqlMain_90, receiptId90, this.curWorkMonth, dtpOccurTime.Value, DBUtil.getServerTime().ToString().Trim(), SStorehouseId, "90",
                                                                      this.s_CustID.Text.Trim(), this.s_CustName.Text.Trim());
                                        sqls.Add(strSqlMain_90);
                                        sqls.Add(SqlUpdateBillRull90);//更新单据号

                                    }
                                    string strSqlDet_90 = "insert into T_Receipts_Det(ReceiptId,OrderNo,MatId,MatType,TTaxPurchPrice,ReceiptId90_03) values('{0}',{1},'{2}',{3},{4},'{5}')";
                                    strSqlDet_90 = string.Format(strSqlDet_90, receiptId90,orderNo,matId, matType, (price - this.matInfos_03[j].price)* num, this.matInfos_03[j].receiptId.Trim());
                                    sqls.Add(strSqlDet_90);
                                  
                                    //更新03单的已核销数量 AlreadyVerificateNum_03
                                    string strSqlUpdate_AlreadyVerificateNum_03 = "update T_Receipts_Det set AlreadyVerificateNum_03=AlreadyVerificateNum_03 + {0} where ReceiptId='{1}' and OrderNo={2}";
                                    strSqlUpdate_AlreadyVerificateNum_03 = string.Format(strSqlUpdate_AlreadyVerificateNum_03, num, this.matInfos_03[j].receiptId, this.matInfos_03[j].orderNo);
                                    sqls.Add(strSqlUpdate_AlreadyVerificateNum_03);

                                    //如果该条物料核销完，更新相应03单的YnCompleteVerificate_03='true'
                                    if (this.matInfos_03[j].notVerNum + (-Convert.ToDouble(dr.Cells["n_num"].Value.ToString().Trim())) <= 0)
                                    {
                                        string strSqlUpdate_03 = "update T_Receipts_Det set YnCompleteVerificate_03='true' where ReceiptId='{0}' and OrderNo={1}";
                                        strSqlUpdate_03 = string.Format(strSqlUpdate_03, this.matInfos_03[j].receiptId, this.matInfos_03[j].orderNo);
                                        sqls.Add(strSqlUpdate_03);
                                    }

                                    //更新01单子表该条记录的 ReceiptId01_03
                                    string strSqlUpate_01 = "update T_Receipts_Det set ReceiptId01_03='{0}' where ReceiptId='{1}' and OrderNo={2}";
                                    strSqlUpate_01 = string.Format(strSqlUpate_01, this.matInfos_03[j].receiptId.Trim(), strReceiptId, orderNo);

                                    sqls.Add(strSqlUpate_01);
                                }
                                else //01单中 不是核销的物料
                                {
                                    //插入该01单
                                    string strSqlDetail = ReceiptModCfg.Build_Insert_Sql_Receipts_Det(strReceiptId, this.dataGridViewReceDetail, dr);
                                    sqls.Add(strSqlDetail);
                                }
                            }
                            #endregion
                        }
                    }

                    //执行事务处理所有的Sql
                    SqlDBConnect db = new SqlDBConnect();
                    db.Exec_Tansaction(sqls);
                }
                #endregion

                #region //edit
                else if (this.type == "edit")
                {
                    List<string> sqls = new List<string>();
                    InitFuncs initFuncs = new InitFuncs();
                                       
                    string strWhere = " where ReceiptId='" + this.strReceiptId + "'";
                    string strSql = ReceiptModCfg.Build_Update_Sql(this.panelMain, "T_Receipt_Main", strWhere);

                    sqls.Add(strSql);

                    //更新单据总表的“当前工作年月”
                    string strSqlUpdate = "update T_Receipt_Main set CurWorkMonth='{0}' where ReceiptId='{1}'";
                    strSqlUpdate = string.Format(strSqlUpdate, this.curWorkMonth, strReceiptId);
                    sqls.Add(strSqlUpdate);

                    //更新子表
                    for (int j = 0; j < this.dataGridViewReceDetail.Rows.Count - 1; j++)
                    {
                        DataGridViewRow dr = this.dataGridViewReceDetail.Rows[j];
                        string strSqlDetail = "";
                        //判断是否存在该条记录
                        string strSqlSel = "select * from T_Receipts_Det where ReceiptId='{0}' and OrderNo={1}";
                        strSqlSel = string.Format(strSqlSel, strReceiptId, dr.Index + 1);
                        bool isExist = (new DBUtil()).yn_exist_data(strSqlSel);
                        if (isExist)
                        {
                            //更新
                            strSqlDetail = ReceiptModCfg.Build_Update_Sql_Receipts_Det(strReceiptId, this.dataGridViewReceDetail, dr);
                        }
                        else
                        {
                            //插入
                            if (dr.Cells["商品编号"].Value == null || dr.Cells["商品编号"].Value.ToString().Trim() == "")
                                continue;
                            strSqlDetail = ReceiptModCfg.Build_Insert_Sql_Receipts_Det(strReceiptId, this.dataGridViewReceDetail, dr);
                        }
                        sqls.Add(strSqlDetail);
                    }
                    SqlDBConnect db = new SqlDBConnect();
                    db.Exec_Tansaction(sqls);
                }
                #endregion

                MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.isSaved = true;

                toolStripButtonAdd_Click(null, null);//转到新增
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        /// <summary>
        /// 计算物料 成本单价 (移动平均法，入库)
        /// <param name="num"></param>
        /// <param name="price"></param>
        /// <param name="numTem">记录事务处理中，未实际插入数据表的num</param> 
        /// </summary>   
        private Cost CalculateCostPrice(string receiptType, string matId, int matType, string storeHouseId, int num, double price, ref List<string> matCountCostPrice)
        {
            SqlDBConnect db = new SqlDBConnect();
            double costPrice = 0.0;//最终返回的 物料的 成本单价
            double costMoney = 0.0;//最终返回的 物料的 成本金额
                        
            string maxBalanceTime = StockStatusDAO.GetBalanceTime();

            string strSql = "select FirstCount,FirstCostPrice,FirstMoney,BalanceTime from T_Stock_Status " +
                            "where StoreHouseId='{0}' and MatId='{1}' and MatType={2} and BalanceTime='{3}'";
            strSql = string.Format(strSql, storeHouseId, matId, matType, maxBalanceTime);
            DataTable dt = db.Get_Dt(strSql);
                        
            double firstCount = 0; //期初数量
            double firstCostPrice = 0.0; //期初成本单价
            double firstMoney = 0.0; //期初成本金额
            double stockNum = 0; //当前库存                
            double stockInCount = 0; //收入数量
            double stockOutCount = 0; //发出数量
            string BalanceTime = "190001"; //取最近的一次结存的 结存时间

            double curCostPrice = 0.0; //记录当前成本单价
            if (dt != null && dt.Rows.Count > 0)
            {
                //取最近的一次结存的数量、成本单价、成本金额
                firstCount = Convert.ToDouble(dt.Rows[0]["FirstCount"].ToString().Trim());
                firstCostPrice = Convert.ToDouble(dt.Rows[0]["FirstCostPrice"].ToString().Trim());
                firstMoney = Convert.ToDouble(dt.Rows[0]["FirstMoney"].ToString().Trim());
                BalanceTime = dt.Rows[0]["BalanceTime"].ToString().Trim();                     
            }
            curCostPrice = firstCostPrice;            
                       
            //入库
            string sql_1 = "select ReceiptTypeID,num,CurAveragePrice from T_Receipt_Main_Det where SourceStoreH='{0}' and MatId='{1}' and MatType={2} and CurWorkMonth >= '{3}' " +
                            "and ReceiptTypeID < '51' and ReceiptTypeID != 'YS' and ReceiptTypeID != 'YF'";
            sql_1 = string.Format(sql_1, storeHouseId, matId, matType, maxBalanceTime);
            DataTable dt_1 = db.Get_Dt(sql_1);
            if (dt_1 != null && dt_1.Rows.Count > 0)
            {
                for (int i = 0; i < dt_1.Rows.Count; i++)
                {
                    if (dt_1.Rows[i]["num"].ToString().Trim() != "")
                        stockInCount += Convert.ToDouble(dt_1.Rows[i]["num"].ToString().Trim()); ;
                }
                if (dt_1.Rows[dt_1.Rows.Count - 1]["CurAveragePrice"].ToString().Trim() != "")
                    curCostPrice = Convert.ToDouble(dt_1.Rows[dt_1.Rows.Count - 1]["CurAveragePrice"].ToString().Trim());//该仓库该物料该类型最后一次移动平均价
            }

            //出库
            string sql_2 = "select ReceiptTypeID,num from T_Receipt_Main_Det where SourceStoreH='{0}' and MatId='{1}' and MatType={2} and CurWorkMonth >= '{3}' " +
                            "and ReceiptTypeID >= '51'and ReceiptTypeID != 'YS' and ReceiptTypeID != 'YF' and ReceiptTypeID != '90'";
            sql_2 = string.Format(sql_2, storeHouseId, matId, matType, maxBalanceTime);
            DataTable dt_2 = db.Get_Dt(sql_2);
            if (dt_2 != null && dt_2.Rows.Count > 0)
            {
                for (int i = 0; i < dt_2.Rows.Count; i++)
                {
                    if (dt_2.Rows[i]["num"].ToString().Trim() != "")
                        stockOutCount += Convert.ToDouble(dt_2.Rows[i]["num"].ToString().Trim()); ;
                }
            }
            stockNum = firstCount + stockInCount - stockOutCount;
                        
            int index = matCountCostPrice.IndexOf(matId);
            double lastCostPrice = Convert.ToDouble(matCountCostPrice[index + 2].Trim());
            if (lastCostPrice != 0.0) //第一次未实际存入的成本单价                
                curCostPrice = lastCostPrice;//           

            stockNum += Convert.ToDouble(matCountCostPrice[index + 1].Trim());
            if (stockNum != 0) //当前库存不为0
            {
                if (receiptType == "90")
                {
                    costMoney = curCostPrice * stockNum - price * num;
                    costPrice = (curCostPrice * stockNum - price * num) / stockNum;
                }
                else
                {
                    if (stockNum + num != 0)
                    {
                        costMoney = curCostPrice * stockNum + price * num;
                        costPrice = (curCostPrice * stockNum + price * num) / (stockNum + num);
                    }
                    else
                    {
                        costMoney = curCostPrice * stockNum + price * num;
                        costPrice = 0;
                    }
                }
            }
            else //当前库存为0
            {
                if (receiptType == "90")
                {
                    costMoney = 0;//curCostPrice * stockNum - price * num;////
                    costPrice = 0;
                }
                else
                {
                    if (stockNum + num != 0)
                    {
                        costMoney = curCostPrice * stockNum + price * num;
                        costPrice = (curCostPrice * stockNum + price * num) / (stockNum + num);
                    }
                    else
                    {
                        costMoney = curCostPrice * stockNum + price * num;
                        costPrice = 0;
                    }
                }
            }
            if (receiptType != "90")
                matCountCostPrice[index + 1] = (Convert.ToDouble(matCountCostPrice[index + 1].Trim()) + num).ToString().Trim();

            matCountCostPrice[index + 2] = costPrice.ToString();//把未实际存入的成本单价赋给 临时变量

            Cost cost = new Cost();
            cost.costPrice = costPrice;
            cost.costMoney = costMoney;
            return cost;
        }
        /// <summary>
        /// 获得物料 成本单价 (移动平均法，出库)       
        /// </summary>   
        private double CalculateCostPriceOut(string matId, int matType, string storeHouseId)
        {
            SqlDBConnect db = new SqlDBConnect();
            double costPrice = 0.0;//最终返回的 物料的 成本单价
                       
            string maxBalanceTime = StockStatusDAO.GetBalanceTime();

            string strSql = "select FirstCount,FirstCostPrice,FirstMoney,BalanceTime from T_Stock_Status " +
                            "where StoreHouseId='{0}' and MatId='{1}' and MatType={2} and BalanceTime='{3}'";
            strSql = string.Format(strSql, storeHouseId, matId, matType, maxBalanceTime);
            DataTable dt = db.Get_Dt(strSql);

            double firstCount = 0; //期初数量            
            double firstMoney = 0.0; //期初成本金额
            double stockNum = 0; //当前库存
            double sumMoney = 0.0; //当前总的成本金额
            double stockInCount = 0; //收入数量
            double stockOutCount = 0; //发出数量
            string BalanceTime = "190001"; //取最近的一次结存的 结存时间           
            if (dt != null && dt.Rows.Count > 0)
            {
                //取最近的一次结存的期初数量、成本单价、成本金额
                firstCount = Convert.ToDouble(dt.Rows[0]["FirstCount"].ToString().Trim());               
                firstMoney = Convert.ToDouble(dt.Rows[0]["FirstMoney"].ToString().Trim());
                BalanceTime = dt.Rows[0]["BalanceTime"].ToString().Trim();
            }            
            sumMoney += firstMoney;
            
            //入库
            string sql_1 = "select sum(num) as num, sum(TTaxPurchPrice) as TTaxPurchPrice from T_Receipt_Main_Det where SourceStoreH='{0}' and MatId='{1}' and MatType={2} and CurWorkMonth >= '{3}' " +
                           "and ReceiptTypeID < '51' and ReceiptTypeID != 'YS' and ReceiptTypeID != 'YF'";
            sql_1 = string.Format(sql_1, storeHouseId, matId, matType, maxBalanceTime);
            DataTable dt_1 = db.Get_Dt(sql_1);
            if (dt_1 != null && dt_1.Rows.Count > 0)
            {
                if (dt_1.Rows[0]["num"].ToString().Trim() != "")
                    stockInCount += Convert.ToDouble(dt_1.Rows[0]["num"].ToString().Trim());
                if (dt_1.Rows[0]["TTaxPurchPrice"].ToString().Trim() != "")
                    sumMoney += Convert.ToDouble(dt_1.Rows[0]["TTaxPurchPrice"].ToString().Trim());//加入库成本金额                         
            }

            //出库
            string sql_2 = "select sum(num) as num, sum(TTaxPurchPrice) as TTaxPurchPrice from T_Receipt_Main_Det where SourceStoreH='{0}' and MatId='{1}' and MatType={2} and CurWorkMonth >= '{3}' " +
                            "and ReceiptTypeID >= '51' and ReceiptTypeID != 'YS' and ReceiptTypeID != 'YF'";
            sql_2 = string.Format(sql_2, storeHouseId, matId, matType, maxBalanceTime);
            DataTable dt_2 = db.Get_Dt(sql_2);
            if (dt_2 != null && dt_2.Rows.Count > 0)
            {             
                if (dt_2.Rows[0]["num"].ToString().Trim() != "")
                    stockOutCount += Convert.ToDouble(dt_2.Rows[0]["num"].ToString().Trim());
                if (dt_2.Rows[0]["TTaxPurchPrice"].ToString().Trim() != "")
                    sumMoney -= Convert.ToDouble(dt_2.Rows[0]["TTaxPurchPrice"].ToString().Trim()); //减出库成本金额    
            }
            stockNum = firstCount + stockInCount - stockOutCount;
                        
            if (stockNum != 0) //当前库存不为0
            {
                costPrice = sumMoney / stockNum; //成本单价=当前总金额/当前库存数
            }
            else //当前库存为0
            {
                
            }            

            return costPrice;
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 客户编号 文本框 的Enter键
        /// </summary>       
        private void txtCustID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "")
                    return;
                string strSql = "select CustID as 客户编码,CustName as 客户名称,PinYinCode as 拼音助记码,communicateAddr as 通信地址, AtBank as 开户银行 " +
                                "from T_CustomerInf where CustID like '%{0}%' or CustName like '%{1}%' or PinYinCode like '%{2}%'";
                strSql = string.Format(strSql, textBoxName, textBoxName, textBoxName);                

                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    DialogResult dialogResult = MessageBox.Show("查找的客户品信息不存在，是否新增客户信息？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        CustomerInfAdd customeradd = new CustomerInfAdd("add", "");                        
                        customeradd.ShowDialog();                        
                    }
                    else
                        textBox.Text = "";
                }
                else if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
                {
                    textBox.Text = dt.Rows[0]["客户编码"].ToString().Trim();
                }
                else
                {
                    FilterInfoForm form = new FilterInfoForm(textBox, dt, "客户编码");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }

                DataBindCustomerInf(textBox.Text.Trim());
            }
        }
        /// <summary>
        /// 客户名称 文本框 的Enter键
        /// </summary>
        private void txtCustName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "")
                    return;
                string strSql = "select CustID as 客户编码,CustName as 客户名称,PinYinCode as 拼音助记码,communicateAddr as 通信地址, AtBank as 开户银行 " +
                                "from T_CustomerInf where CustID like '%{0}%' or CustName like '%{1}%'";
                strSql = string.Format(strSql, textBoxName, textBoxName);
                InitFuncs.FindInfoToControl(textBox, strSql, "客户名称");

                string custId = (new DBUtil()).Get_Single_val("T_CustomerInf", "CustID", "CustName", textBox.Text.Trim());
                DataBindCustomerInf(custId);
            }

        }
                       
        private void dataGridViewReceDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
            DBUtil dbUtil = new DBUtil();
            SqlDBConnect db = new SqlDBConnect();
            DataGridViewCell cell = this.dataGridViewReceDetail.CurrentCell;
            if (cell == null)
                return;
                          
            if (cell.OwningColumn.HeaderText == "数量")
            {
                /* 暂时取消验证
                string InOrOutBound = dbUtil.Get_Single_val("T_ReceiptModal", "InOrOutBound", "ReceTypeID", this.strReceTypeId);
                if (InOrOutBound == "出库")
                {
                    double num = 0;
                    double curStockNum = 0;
                    if (this.dataGridViewReceDetail.CurrentRow.Cells["n_num"].Value != null && this.dataGridViewReceDetail.CurrentRow.Cells["n_num"].Value.ToString().Trim() != "")
                        num = Convert.ToDouble(this.dataGridViewReceDetail.CurrentRow.Cells["n_num"].Value.ToString().Trim());
                    if (this.tsslCurStockNum.Text.Trim() != "")
                        curStockNum = Convert.ToDouble(tsslCurStockNum.Text.Trim());
                    if (num > curStockNum)
                    {
                        MessageBox.Show("没有足够的库存", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cell.Value = "";
                    }
                }*/
            }
            else if (cell.OwningColumn.HeaderText == "单价")
            {
                #region //单价
                if (this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["商品编号"].Value == null)
                    return;
                  
                double num = 0;
                double price = 0;
                double costPrice = 0.0;
                try
                {
                    if (this.dataGridViewReceDetail.Columns.Contains("n_num") && this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_num"].Value != null &&
                        this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_num"].Value.ToString().Trim() != "")
                        num = Convert.ToDouble(this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_num"].Value.ToString().Trim());
                    if (this.dataGridViewReceDetail.Columns.Contains("n_price") && this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_price"].Value != null &&
                        this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_price"].Value.ToString().Trim() != "")
                        price = Convert.ToDouble(this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_price"].Value.ToString().Trim());
                    if (this.dataGridViewReceDetail.Columns.Contains("n_STaxPurchPrice") && this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_STaxPurchPrice"].Value != null &&
                        this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_STaxPurchPrice"].Value.ToString().Trim() != "")
                        costPrice = Convert.ToDouble(this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_STaxPurchPrice"].Value.ToString().Trim());
                    //计算 金额
                    if (this.dataGridViewReceDetail.Columns.Contains("n_Amount"))//含“金额”列
                        this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_Amount"].Value = string.Format("{0:f2} ", num * price);//Math.Round(num * price);

                    if (this.dataGridViewReceDetail.Columns.Contains("n_TaxRat") && this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_TaxRat"].Value != null &&
                        this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_TaxRat"].Value.ToString().Trim() != "") //含“税率”列,并且该列不空
                    {
                        double taxTat = Convert.ToDouble(this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_TaxRat"].Value.ToString().Trim());
                        //计算 不含税金额
                        if (this.dataGridViewReceDetail.Columns.Contains("n_NotTax"))
                            this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_NotTax"].Value = string.Format("{0:f2} ", num * price / (1 + taxTat));
                        //计算 税额
                        if (this.dataGridViewReceDetail.Columns.Contains("n_Tax"))
                            this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_Tax"].Value = string.Format("{0:f2} ", num * price - num * price / (1 + taxTat));
                    }
                    if (this.dataGridViewReceDetail.Columns.Contains("n_TTaxPurchPrice"))
                        this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_TTaxPurchPrice"].Value = string.Format("{0:f2} ", num * costPrice);
                    // 计算 合计 信息
                    CulSum();
                }                    
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
            }
            else if (cell.OwningColumn.HeaderText == "成本单价")
            {
                double num = 0;
                double costPrice = 0.0;

                if (this.dataGridViewReceDetail.Columns.Contains("n_num") && this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_num"].Value != null &&
                        this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_num"].Value.ToString().Trim() != "")
                    num = Convert.ToDouble(this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_num"].Value.ToString().Trim());               
                if (this.dataGridViewReceDetail.Columns.Contains("n_STaxPurchPrice") && this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_STaxPurchPrice"].Value != null &&
                    this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_STaxPurchPrice"].Value.ToString().Trim() != "")
                    costPrice = Convert.ToDouble(this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_STaxPurchPrice"].Value.ToString().Trim());

                if (this.dataGridViewReceDetail.Columns.Contains("n_TTaxPurchPrice"))
                    this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_TTaxPurchPrice"].Value = string.Format("{0:f2} ", num * costPrice);
                // 计算 合计 信息
                CulSum();
            }
        }
        /// <summary>
        /// 计算 合计 信息
        /// </summary>
        private void CulSum()
        {
            double numSum = 0, amountSum = 0, notTaxSum = 0, taxSum = 0;
            foreach (DataGridViewRow dgvr in this.dataGridViewReceDetail.Rows)
            {
                //计算 数量合计
                if (this.dataGridViewReceDetail.Columns.Contains("n_num") && dgvr.Cells["n_num"].Value != null && dgvr.Cells["n_num"].Value.ToString().Trim() != "")
                    numSum += Convert.ToDouble(dgvr.Cells["n_num"].Value.ToString().Trim());
                //计算 金额合计
                if (this.dataGridViewReceDetail.Columns.Contains("n_Amount") && dgvr.Cells["n_Amount"].Value != null && dgvr.Cells["n_Amount"].Value.ToString().Trim() != "")
                    amountSum += Convert.ToDouble(dgvr.Cells["n_Amount"].Value.ToString().Trim());
                //计算 不含税金额合计
                if (this.dataGridViewReceDetail.Columns.Contains("n_NotTax") && dgvr.Cells["n_NotTax"].Value != null && dgvr.Cells["n_NotTax"].Value.ToString().Trim() != "")
                    notTaxSum += Convert.ToDouble(dgvr.Cells["n_NotTax"].Value.ToString().Trim());
                //计算 税额合计
                if (this.dataGridViewReceDetail.Columns.Contains("n_Tax") && dgvr.Cells["n_Tax"].Value != null && dgvr.Cells["n_Tax"].Value.ToString().Trim() != "")
                    taxSum += Convert.ToDouble(dgvr.Cells["n_Tax"].Value.ToString().Trim());
            }
            this.labelNumSum.Text = numSum.ToString();
            this.labelAmountSum.Text = amountSum.ToString();
            this.labelNotTaxSum.Text = notTaxSum.ToString();
            this.labelTaxSum.Text = taxSum.ToString();
            this.labelPriceTaxSum.Text = "人民币大写："+ Util.ConvertMoney(Convert.ToDecimal(amountSum));
        }
                
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            ListModalForm listModalForm = new ListModalForm(strReceTypeId, "add", "", "");
            listModalForm.MdiParent = this.MdiParent;
            listModalForm.Show();
        }

        private void dataGridViewReceDetail_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.dataGridViewReceDetail.CurrentCell;
            if (cell.OwningColumn.HeaderText == "类型")
            {
                ContextMenuStrip contextMenuStripMatType = new ContextMenuStrip();
                contextMenuStripMatType.Items.Add("0新机");
                contextMenuStripMatType.Items.Add("1旧机");
                contextMenuStripMatType.Items.Add("2样机");
                contextMenuStripMatType.Items.Add("3固新机");
                contextMenuStripMatType.Items.Add("4固旧机");
                contextMenuStripMatType.ItemClicked += new ToolStripItemClickedEventHandler(contextMenuStripMatType_ItemClicked);

                //获得DataGridView当前单元格的坐标
                int dgvX = dataGridViewReceDetail.Location.X;
                int dgvY = dataGridViewReceDetail.Location.Y;
                int cellX = dataGridViewReceDetail.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).X;
                int cellY = dataGridViewReceDetail.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Y;
                int x = dgvX + cellX;
                int y = dgvY + cellY + this.dataGridViewReceDetail.Rows[0].Height;

                Point point = new Point(x, y);
                contextMenuStripMatType.Show(PointToScreen(point));
            }
        }
       
        void contextMenuStripMatType_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Trim() == "0新机")                     
                this.dataGridViewReceDetail.CurrentCell.Value = "0";
            if (e.ClickedItem.Text.Trim() == "1旧机")
                this.dataGridViewReceDetail.CurrentCell.Value = "1";
            if (e.ClickedItem.Text.Trim() == "2样机")
                this.dataGridViewReceDetail.CurrentCell.Value = "2";
            if (e.ClickedItem.Text.Trim() == "3固新机")
                this.dataGridViewReceDetail.CurrentCell.Value = "3";
            if (e.ClickedItem.Text.Trim() == "4固旧机")
                this.dataGridViewReceDetail.CurrentCell.Value = "4";

            if (this.dataGridViewReceDetail.CurrentRow.Cells["商品编号"].Value == null || this.dataGridViewReceDetail.CurrentRow.Cells["商品编号"].Value.ToString().Trim() == "")
                return;

            //显示当前仓库、当前物料、当前类型的库存,成本            
            string matId = this.dataGridViewReceDetail.CurrentRow.Cells["商品编号"].Value.ToString().Trim();
            int matType = Convert.ToInt32(this.dataGridViewReceDetail.CurrentRow.Cells["n_MatType"].Value.ToString().Trim());

            if (this.dataGridViewReceDetail.Columns.Contains("n_STaxPurchPrice"))
            {
                double costPrice = CalculateCostPriceOut(matId, matType, SStorehouseId);//计算成本单价
                this.dataGridViewReceDetail.CurrentRow.Cells["n_STaxPurchPrice"].Value = string.Format("{0:f4} ", costPrice);
            }
            StockStatus stockStatus = ReceiptModCfg.GetStockNum(SStorehouseId, matId, matType);

            this.tsslCurStockNum.Text = stockStatus.stockNum.ToString();
            this.tsslFirstCount.Text = stockStatus.firstCount.ToString();
            this.tsslStockInCount.Text = stockStatus.stockInCount.ToString();
            this.tsslStockOutCount.Text = stockStatus.stockOutCount.ToString();
            if (this.dataGridViewReceDetail.CurrentRow.Cells["商品名称"].Value != null)
                this.tsslMatName.Text = this.dataGridViewReceDetail.CurrentRow.Cells["商品名称"].Value.ToString().Trim();
        }

        private void dataGridViewReceDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {           
           if (this.dataGridViewReceDetail.Columns.Contains("n_TaxRat"))//含“税率”列
           {
               if (dataGridViewReceDetail.NewRowIndex == 0)
                   return;
               if (e.RowIndex <= 0)
                   return;
               if (this.dataGridViewReceDetail.Rows[e.RowIndex - 1].Cells["n_TaxRat"].Value != null)
                   this.dataGridViewReceDetail.Rows[e.RowIndex].Cells["n_TaxRat"].Value = this.dataGridViewReceDetail.Rows[e.RowIndex - 1].Cells["n_TaxRat"].Value.ToString().Trim();
           }
               
        }
        private bool dgvFocus = false; //记录当前DataGridView的获得焦点状态
        private void dataGridViewReceDetail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dgvFocus = this.dataGridViewReceDetail.Focus();
        }

        /// <summary>
        /// 使DataGridView按Enter键向后移动一个单元格
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.dgvFocus == false)
                return false;

            DataGridViewCell cell = this.dataGridViewReceDetail.CurrentCell;
            if (keyData == Keys.Enter && cell.OwningColumn.HeaderText == "商品编号")
            {
                #region 商品编号               
                this.dataGridViewReceDetail.EndEdit();
                if (cell.Value != null && cell.Value.ToString().Trim() != "")
                {
                    if (SStorehouseId == "")
                    {
                        MessageBox.Show("请先选择一个仓库！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false; //////
                    }
                    string strMatID = cell.Value.ToString().Trim();

                    string strSql = "select MatID 商品编号,MatName 商品名称,Specifications 型号规格,Units 计量单位 " +
                                    "from T_MatInf where MatID like '%{0}%' or MatName like '%{1}%'";
                    strSql = string.Format(strSql, strMatID, strMatID);

                    int matType = 0;
                    DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("查找的商品信息不存在，是否新增商品信息？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Add_materialForm add_mat = new Add_materialForm("add", "");
                            add_mat.matId = cell.Value.ToString().Trim();
                            add_mat.ShowDialog();
                            if (add_mat.DialogResult == DialogResult.OK)
                                cell.Value = add_mat.matId;
                        }
                        else
                            cell.Value = "";
                    }
                    else if (dt.Rows.Count == 1)
                    {
                        cell.Value = dt.Rows[0]["商品编号"].ToString().Trim();
                    }
                    else
                    {
                        ReceiptFilterInfoForm form = new ReceiptFilterInfoForm(cell, dt, "商品编号", SStorehouseId, matType);
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.ShowDialog();
                        if (form.DialogResult == DialogResult.OK)
                            matType = form.matType;
                    }

                    DataBindMatInf(cell.Value.ToString().Trim(), matType);
                    //if (this.dataGridViewReceDetail.CurrentCell.RowIndex == 0)
                    //{
                    //    SendKeys.Send("{Up}");
                    //    SendKeys.Send("{TAB}"); SendKeys.Send("{TAB}"); SendKeys.Send("{TAB}"); SendKeys.Send("{TAB}"); SendKeys.Send("{ESC}");
                    //}
                    //else
                    //{
                    //    SendKeys.Send("{TAB}"); SendKeys.Send("{TAB}"); SendKeys.Send("{TAB}"); SendKeys.Send("{TAB}"); SendKeys.Send("{ESC}");
                    //}
                    //显示当前仓库、当前物料、当前类型的库存                
                    string matId = this.dataGridViewReceDetail.CurrentRow.Cells["商品编号"].Value.ToString().Trim();

                    if (this.dataGridViewReceDetail.CurrentRow.Cells["n_MatType"].Value != null && this.dataGridViewReceDetail.CurrentRow.Cells["n_MatType"].Value.ToString().Trim() != "")
                        matType = Convert.ToInt32(this.dataGridViewReceDetail.CurrentRow.Cells["n_MatType"].Value.ToString().Trim());

                    StockStatus stockStatus = ReceiptModCfg.GetStockNum(SStorehouseId, matId, matType);

                    this.tsslCurStockNum.Text = stockStatus.stockNum.ToString();
                    this.tsslFirstCount.Text = stockStatus.firstCount.ToString();
                    this.tsslStockInCount.Text = stockStatus.stockInCount.ToString();
                    this.tsslStockOutCount.Text = stockStatus.stockOutCount.ToString();
                    if (this.dataGridViewReceDetail.CurrentRow.Cells["商品名称"].Value != null)
                        this.tsslMatName.Text = this.dataGridViewReceDetail.CurrentRow.Cells["商品名称"].Value.ToString().Trim();
                }
                
                #endregion
            }
            //if (keyData == Keys.Enter && this.dataGridViewReceDetail.Focused)            
            //{
            //    System.Windows.Forms.SendKeys.Send("{tab}");
            //    return true;
            //}

            bool enterkey = false;
            if (keyData == Keys.Enter)    //监听回车事件     
            {
                if (this.dataGridViewReceDetail.IsCurrentCellInEditMode)   //如果当前单元格处于编辑模式     
                {
                    enterkey = true;    //把是否点击按钮设置为真          
                    //if (btnSetEnter.Text != "竖")
                    if (this.dataGridViewReceDetail.CurrentCell.RowIndex == this.dataGridViewReceDetail.Rows.Count - 1) //最后一行
                    {
                        if (this.dataGridViewReceDetail.CurrentCell.OwningColumn.HeaderText == "商品编号")
                        {
                            SendKeys.Send("{Tab}"); SendKeys.Send("{Tab}");
                            SendKeys.Send("{Tab}"); SendKeys.Send("{Tab}");
                            SendKeys.Send("{ESC}"); SendKeys.Send("{Tab}");
                        }
                        else
                            SendKeys.Send("{Tab}");
                    }
                    else
                    {
                        SendKeys.Send("{Up}");

                        if (this.dataGridViewReceDetail.CurrentCell.OwningColumn.HeaderText == "商品编号")
                        {
                            SendKeys.Send("{Tab}"); SendKeys.Send("{Tab}");
                            SendKeys.Send("{Tab}"); SendKeys.Send("{Tab}");
                            SendKeys.Send("{ESC}"); SendKeys.Send("{Tab}");
                        }
                        else
                            SendKeys.Send("{Tab}");
                    }
                }
            }
            //继续原来base.ProcessCmdKey中的处理
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void ListModalForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewReceDetail.CurrentRow.IsNewRow)
                return;
            int rowIndex = this.dataGridViewReceDetail.CurrentRow.Index;
            this.dataGridViewReceDetail.Rows.RemoveAt(rowIndex);
        }

        //定义Grid++Report报表主对象
        //private GridppReport Report = new GridppReport();
        //private GridppReport SubReport = new GridppReport();
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            if (this.strReceiptId == "")
                return;
            ChooseForm form = new ChooseForm(this.strReceiptId, this.strReceTypeId);
            form.ShowDialog();
        }       

        private void ListModalForm_Activated(object sender, EventArgs e)
        {
            TextBox textBox = ReceiptModCfg.GetControlByName(this.panelMain, "TextBox", "s_CustomerReceiptNo") as TextBox;
            textBox.Focus();
        }

        private void ListModalForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);            
        }

        private void ListModalForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
                tsbPrint_Click(null, null); //打印
            if (e.KeyCode == Keys.F4)
                toolStripButtonSave_Click(null, null); //保存            
        }
        /// <summary>
        /// 当前类型单据的第一个
        /// </summary>
        private void toolStripButtonFirst_Click(object sender, EventArgs e)
        {
            string strSql = "select ReceiptId from T_Receipt_Main where ReceiptTypeID='{0}' order by AutoRecordTime,ReceiptId";
            strSql = string.Format(strSql, this.strReceTypeId);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有单据！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.strReceiptId = dt.Rows[0][0].ToString().Trim();

            this.toolStripButtonSave.Enabled = false;
            ShowDatas();//提取已有的信息

            this.toolStripTextBoxNo.Text = "1";
        }
        /// <summary>
        /// 上一张单据
        /// </summary>        
        private void toolStripButtonPre_Click(object sender, EventArgs e)
        {
            string strSql = "select ReceiptId from T_Receipt_Main where ReceiptTypeID='{0}' order by AutoRecordTime,ReceiptId";
            strSql = string.Format(strSql, this.strReceTypeId);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                       
            if (dt == null || dt.Rows.Count <= 0)
                return;

            int index = dt.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Trim() == this.strReceiptId)
                    index = i;
            }

            if (index > 0)
            {
                this.strReceiptId = dt.Rows[index - 1][0].ToString().Trim();

                this.toolStripButtonSave.Enabled = false;
                ShowDatas();//提取已有的信息
            }
            else
            {
                MessageBox.Show("当前已经是第一个单据了！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.toolStripTextBoxNo.Text = (index).ToString();
        }
        /// <summary>
        /// 下一张单据
        /// </summary>   
        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            string strSql = "select ReceiptId from T_Receipt_Main where ReceiptTypeID='{0}' order by AutoRecordTime,ReceiptId";
            strSql = string.Format(strSql, this.strReceTypeId);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                        
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            int index = dt.Rows.Count - 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString().Trim() == this.strReceiptId)
                    index = i;
            }

            if (index < dt.Rows.Count - 1)
            {
                this.strReceiptId = dt.Rows[index + 1][0].ToString().Trim();

                this.toolStripButtonSave.Enabled = false;
                ShowDatas();//提取已有的信息
            }
            else
            {
                MessageBox.Show("当前已经是最后一个单据了！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.toolStripTextBoxNo.Text = (index + 1+ 1).ToString();
        }
        /// <summary>
        /// 当前类型单据最后一个
        /// </summary>   
        private void toolStripButtonLast_Click(object sender, EventArgs e)
        {
            string strSql = "select ReceiptId from T_Receipt_Main where ReceiptTypeID='{0}' order by AutoRecordTime,ReceiptId";
            strSql = string.Format(strSql, this.strReceTypeId);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
            {               
                return;
            }

            this.strReceiptId = dt.Rows[dt.Rows.Count-1][0].ToString().Trim();

            this.toolStripButtonSave.Enabled = false;
            ShowDatas();//提取已有的信息

            this.toolStripTextBoxNo.Text = dt.Rows.Count.ToString();
        }
        /// <summary>
        /// 显示当前类型第几个单据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripTextBoxNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13) //回车键
                return;

            int index = 0, indexTotal = 0;
            if (this.toolStripTextBoxNo.Text.Trim() != "")
                index = Convert.ToInt32(this.toolStripTextBoxNo.Text.Trim());
            if (this.toolStripTextBoxTotalNo.Text.Trim() != "")
                indexTotal = Convert.ToInt32(this.toolStripTextBoxTotalNo.Text.Trim());
            if (index <= 0 || index > indexTotal)
            {
                MessageBox.Show("输入数据不合法！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string strSql = "select ReceiptId from T_Receipt_Main where ReceiptTypeID='{0}' order by AutoRecordTime,ReceiptId";
            strSql = string.Format(strSql, this.strReceTypeId);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            this.strReceiptId = dt.Rows[index - 1][0].ToString().Trim();

            this.toolStripButtonSave.Enabled = false;
            ShowDatas();//提取已有的信息

            this.toolStripTextBoxNo.Focus();
        }

        private void dataGridViewReceDetail_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //单击行头，显示该行物料的库存
            int rowIndex = e.RowIndex;
            if (rowIndex == -1)
                return;
            if (this.dataGridViewReceDetail.Rows[rowIndex].Cells["商品编号"].Value == null || this.dataGridViewReceDetail.Rows[rowIndex].Cells["商品编号"].Value.ToString().Trim() == "")
            {
                this.tsslCurStockNum.Text = "";
                this.tsslFirstCount.Text = "";
                this.tsslStockInCount.Text = "";
                this.tsslStockOutCount.Text = "";
                this.tsslMatName.Text = "";
                return;
            }
           
            //仓库ID
            ComboBox comboBoxSStorehouse = ReceiptModCfg.GetControlByName(this.panelMain, "ComboBox", "comboBoxs_SourceStoreH") as ComboBox;
            string SStorehouseId = (new DBUtil()).Get_Single_val("T_StoreHouse", "SHId", "SHName", comboBoxSStorehouse.Text.Trim());//仓库Id
            string matId = this.dataGridViewReceDetail.Rows[rowIndex].Cells["商品编号"].Value.ToString().Trim();
            int matType = 0;//默认0新机
            if (this.dataGridViewReceDetail.Rows[rowIndex].Cells["n_MatType"].Value != null && this.dataGridViewReceDetail.Rows[rowIndex].Cells["n_MatType"].Value.ToString().Trim() != "")
                matType = Convert.ToInt32(this.dataGridViewReceDetail.Rows[rowIndex].Cells["n_MatType"].Value.ToString().Trim());
            double num = 0;
            if (this.dataGridViewReceDetail.Rows[rowIndex].Cells["n_num"].Value != null && this.dataGridViewReceDetail.Rows[rowIndex].Cells["n_num"].Value.ToString().Trim() != "")
                num = Convert.ToDouble(this.dataGridViewReceDetail.Rows[rowIndex].Cells["n_num"].Value.ToString().Trim());

            StockStatus stockStatus = ReceiptModCfg.GetStockNum(SStorehouseId, matId, matType);

            string InOrOutBound = (new DBUtil()).Get_Single_val("T_ReceiptModal", "InOrOutBound", "ReceTypeID", this.strReceTypeId);
            if (InOrOutBound == "入库")
            {
                this.tsslCurStockNum.Text = (stockStatus.stockNum + num).ToString();
                this.tsslStockInCount.Text = (stockStatus.stockInCount + num).ToString();
            }
            else
            {
                this.tsslCurStockNum.Text = (stockStatus.stockNum - num).ToString();
                this.tsslStockOutCount.Text = (stockStatus.stockOutCount + num).ToString();
            }
            if (this.dataGridViewReceDetail.Rows[rowIndex].Cells["商品名称"].Value != null)
                this.tsslMatName.Text = this.dataGridViewReceDetail.Rows[rowIndex].Cells["商品名称"].Value.ToString().Trim();
        }
        /// <summary>
        /// 负单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonFD_Click(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// 转旧零件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTurnOld_Click(object sender, EventArgs e)
        {

        }
    }
      

    public struct CountCostPrice
    {        
        public int count;
        public double lastCostPrice;
    }
    /// <summary>
    /// 成本
    /// </summary>
    public struct Cost
    {
        /// <summary>
        /// 成本单价
        /// </summary>
        public double costPrice;
        /// <summary>
        /// 成本金额
        /// </summary>
        public double costMoney;      
    }

}
