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
    public partial class ReceiptQueryForm : Form
    {
        private string strSql;
        private int type; //0高级查询，1普通查询
        public string curWorkMonth;
        public ReceiptQueryForm(string strSql, int type)
        {
            InitializeComponent();
            this.strSql = strSql;
            this.type = type;
        }

        private void ReceiptQueryForm_Load(object sender, EventArgs e)
        {
            InitInfo();
            if (this.type == 0)
                InitDataGridViewMain_Adva();
            else if (this.type == 1)
                InitDataGridViewMain();

            InitDataGridViewDetail("","");
        }

        private void InitInfo()
        {
            (new InitFuncs()).InitComboBox(this.comboBoxReceName, "T_ReceiptModal", "ReceName");
            this.dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//默认当月第一天
        }

        private void comboBoxReceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxReceName.SelectedIndex == 0)
                return;
            this.txtReceTypeID.Text = (new DBUtil()).Get_Single_val("T_ReceiptModal", "ReceTypeID", "ReceName",this.comboBoxReceName.Text.Trim());
        }
        /// <summary>
        /// 初始化 单据总表
        /// </summary>
        private void InitDataGridViewMain_Adva()
        { 
            DataTable dt = (new SqlDBConnect()).Get_Dt(this.strSql);

            (new InitFuncs()).InitDataGridView(this.dgvReceipt_Main, dt);
        }
        /// <summary>
        /// 初始化 单据总表
        /// </summary>
        private void InitDataGridViewMain()
        {
            string customerReceiptId = this.txtCustomerReceiptId.Text.Trim();
            string receiptTypeID = this.txtReceTypeID.Text.Trim();
            string occurTimeFrom = this.dtpFrom.Value.ToString().Trim();
            string occurTimeTo = this.dtpTo.Value.ToString().Trim();
            if (receiptTypeID == "") return;

            SortedList<int, string> listReceMainTopItems = new SortedList<int,string>();//单据总表上部项
            SortedList<int, string> listReceMainButtomItems = new SortedList<int,string>();//单据总表下部项            
            listReceMainTopItems = ReceiptModalCfgDAO.GetShowItems(receiptTypeID, 0, 0);
            listReceMainButtomItems = ReceiptModalCfgDAO.GetShowItems(receiptTypeID, 0, 1);           

            //构造Sql
            string strSql = "select T_Receipt_Main.ReceiptTypeID as 单据类别, ";
            string fileds = "";
            if (listReceMainTopItems != null)
            {
                foreach (string str in listReceMainTopItems.Values)
                {
                    string filedTemp = ReceiptModCfg.GetReceiptMainItems()[str.Trim()].Trim();
                    if (filedTemp == "s_CustID")
                        fileds += "CustName" + " as " + str + ",";
                    else if (filedTemp == "s_SourceStoreH")
                    {
                        fileds += "T_StoreHouse.SHName" + " as " + str + ",";
                    }
                    else
                        fileds += filedTemp.Substring(2) + " as " + str + ",";
                }
            }
            if (listReceMainButtomItems != null)
            {
                foreach (string str in listReceMainButtomItems.Values)
                {
                    string filedTemp = ReceiptModCfg.GetReceiptMainItems()[str.Trim()].Trim();
                    if (filedTemp == "s_CustID")
                        fileds += "CustName" + " as " + str + ",";
                    else if (filedTemp == "s_SourceStoreH")
                    {
                        fileds += "T_StoreHouse.SHName" + " as " + str + ",";
                    }
                    else
                        fileds += filedTemp.Substring(2) + " as " + str + ",";
                }
            }
            if (fileds == "")
                return;
            fileds = fileds.Remove(fileds.Length - 1);

            strSql += fileds + " from T_Receipt_Main,T_StoreHouse where ReceiptTypeID='{0}' ";
            strSql = string.Format(strSql, receiptTypeID);

            if (customerReceiptId != "")
            {
                strSql += " and CustomerReceiptNo='" + customerReceiptId + "'";
            }

            strSql += " and (OccurTime between '{0}' And '{1}') ";
            strSql = string.Format(strSql, occurTimeFrom, occurTimeTo);

            strSql += " and T_Receipt_Main.SourceStoreH=T_StoreHouse.SHId order by OccurTime";

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            (new InitFuncs()).InitDataGridView(this.dgvReceipt_Main, dt);
        }
        /// <summary>
        /// 初始化 单据子表
        /// </summary>
        private void InitDataGridViewDetail(string ReceiptId, string receiptTypeID)
        {                      
            if (receiptTypeID == "") return;                      

            SortedList<int, string> listReceDetailItems;//单据子表项            
            listReceDetailItems = ReceiptModalCfgDAO.GetShowItems(receiptTypeID, 1, -1);

            //构造Sql
            string strSql = "select ";
            string fileds = "";
            if (listReceDetailItems != null)
            {
                foreach (string str in listReceDetailItems.Values)
                {
                    string filedTemp = ReceiptModCfg.GetReceiptDetailItems()[str.Trim()].Trim();
                    fileds += filedTemp.Substring(2) + " as " + str + ",";
                }
            }
            if (fileds == "")
                return;
            fileds = fileds.Remove(fileds.Length - 1);

            //显示相应03单单号
            if (receiptTypeID == "01")
                fileds += ", ReceiptId01_03 as 对应03单单号";
            if (receiptTypeID == "20")
                fileds += ", ReceiptId20_03 as 对应03单单号";
            if (receiptTypeID == "90")
                fileds += ", ReceiptId90_03 as 对应03单单号";

            strSql += fileds + " from T_Receipts_Det where ReceiptId='{0}' ";
            strSql = string.Format(strSql, ReceiptId);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            (new InitFuncs()).InitDataGridView(this.dgvReceipt_Det, dt);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.comboBoxReceName.SelectedIndex == 0)
            {
                MessageBox.Show("请选择单据类别再进行查询！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            InitDataGridViewMain();
            InitDataGridViewDetail("", "");
        }

        private void dgvReceipt_Main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvReceipt_Main.SelectedRows.Count <= 0 || this.dgvReceipt_Main.SelectedRows[0].Cells["单据号"].Value == null || 
                this.dgvReceipt_Main.SelectedRows[0].Cells["单据类别"].Value == null)
                return;           

            string ReceiptId = this.dgvReceipt_Main.SelectedRows[0].Cells["单据号"].Value.ToString().Trim();
            string ReceiptTypeId = this.dgvReceipt_Main.SelectedRows[0].Cells["单据类别"].Value.ToString().Trim();

            //查找子表中是否存在相应记录
            string strSqlSel = "select ReceiptId from T_Receipts_Det where ReceiptId='{0}'";
            strSqlSel = string.Format(strSqlSel, ReceiptId);
            bool isExist = (new DBUtil()).yn_exist_data(strSqlSel);
            if (isExist == false)
                InitDataGridViewDetail("", "");
            else
                InitDataGridViewDetail(ReceiptId, ReceiptTypeId);
            
        }
        /// <summary>
        /// 双击修改某一单据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceipt_Main_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvReceipt_Main.SelectedRows.Count <= 0 || this.dgvReceipt_Main.SelectedRows[0].Cells["单据号"].Value == null)
                return;

            string ReceiptId = this.dgvReceipt_Main.SelectedRows[0].Cells["单据号"].Value.ToString().Trim();
            string ReceiptTypeId = (new DBUtil()).Get_Single_val("T_Receipt_Main", "ReceiptTypeID", "ReceiptId", ReceiptId);
            if (ReceiptTypeId == "")
                return;

            ListModalForm listModalForm = new ListModalForm(ReceiptTypeId, "edit", ReceiptId, "");
            listModalForm.curWorkMonth = this.curWorkMonth;
            listModalForm.MdiParent = this.MdiParent;
            listModalForm.Show();

        }

    }
}
