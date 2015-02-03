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
//using System.Collections.Generic;

namespace Warehouse.Receipt
{
    public partial class NotVerificateReceiptForm : Form
    {       
        private Form mainForm;
        public NotVerificateReceiptForm(Form mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void NotVerificateReceiptForm_Load(object sender, EventArgs e)
        {
            this.dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//默认当月第一天
           
            //InitDataGridViewDetail(0);
        }
        /// <summary>
        /// 初始化 单据子表
        /// </summary>
        public void InitDataGridViewDetail(int type)
        {
            this.progressBar1.Value = 0;            

            string receiptTypeID = "03";//单据模板编码(假设进货单)  
            string receiptId = this.txtReceiptId.Text.Trim();
            string customerReceiptId = this.txtCustomerReceiptId.Text.Trim();
            string occurTimeFrom = this.dtpFrom.Value.ToString().Trim();
            string occurTimeTo = this.dtpTo.Value.ToString().Trim();
            
            SortedList<int, string> listReceDetailItems;//单据子表项            
            listReceDetailItems = ReceiptModalCfgDAO.GetShowItems(receiptTypeID, 1, -1);
            
            //构造Sql
            string strSql = "select T_Receipts_Det.ReceiptId as 单据号,T_Receipt_Main.CustomerReceiptNo as 自定义单据号, T_Receipts_Det.OrderNo as 顺序号, "+
                            "CustName as 客户名称, OccurTime as 单据日期, T_MatInf.MatName as 物料名称, ";
            string fileds = "";
            if (listReceDetailItems != null)
            {
                foreach (string str in listReceDetailItems.Values)
                {
                    string filedTemp = ReceiptModCfg.GetReceiptDetailItems()[str.Trim()].Trim().Substring(2);
                    if (filedTemp == "MatId")
                        filedTemp = "T_Receipts_Det." + filedTemp;
                    fileds += filedTemp + " as " + str + ",";
                }
            }
            if (fileds == "")
                return;
            fileds = fileds.Remove(fileds.Length - 1);
            
            strSql += fileds + " from T_Receipt_Main,T_Receipts_Det,T_MatInf " +
                               " where T_Receipt_Main.ReceiptTypeID='{0}' and OccurTime > '2003-12-31' and " + //2003-12-31之前不处理未核销
                               " T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and T_Receipts_Det.MatId=T_MatInf.MatID ";
            strSql = string.Format(strSql, receiptTypeID);

            if (type == 1) //未核销查询
            {
                if (receiptId != "")
                {
                    strSql += " and T_Receipts_Det.ReceiptId='" + receiptId + "'";
                }
                if (customerReceiptId != "")
                {
                    strSql += " and T_Receipt_Main.CustomerReceiptNo='" + customerReceiptId + "'";
                }

                strSql += " and OccurTime between '{0}' And '{1}'";
                strSql = string.Format(strSql, occurTimeFrom, occurTimeTo);
            }            
            else if (type == 0) //高级查找
            {               
                //添加查找窗体           
                WFilter wf = new WFilter(0, "单据号", false);
                wf.strSql = strSql;
                wf.s_items.Add("单据号,T_Receipts_Det.ReceiptId,C");
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
                    //返回条件框中的sql语句
                    strSql = wf.Return_Sql;
                    int index = strSql.IndexOf(" where ");
                    if (!strSql.Substring(index + 6).Contains("OccurTime "))
                    {
                        strSql += " and (OccurTime between '{0}' And '{1}')";
                        strSql = string.Format(strSql, occurTimeFrom, occurTimeTo);
                    }
                }
                else
                    return;
            }           

            strSql += " order by OccurTime"; //排序

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
             (new InitFuncs()).InitDataGridView(this.dgvReceipt_Det, dt);

            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = dt.Rows.Count;

            try
            {
                List<DataGridViewRow> deleteRows = new List<DataGridViewRow>();               
                //更新 未核销的商品数量
                foreach (DataGridViewRow dgvr in this.dgvReceipt_Det.Rows)
                {
                    string receiptId03 = "";
                    int orderNo03 = 0;
                    string matId = "";
                    int matType = 0;
                    double price = 0;
                    int num = 0;
                    if (dgvr.Cells["单据号"].Value == null || dgvr.Cells["单据号"].Value.ToString().Trim() == "")
                        continue;
                    if (dgvr.Cells["顺序号"].Value == null || dgvr.Cells["顺序号"].Value.ToString().Trim() == "")
                        continue;
                    if (dgvr.Cells["物料编码"].Value == null || dgvr.Cells["物料编码"].Value.ToString().Trim() == "")
                        continue;
                    if (dgvr.Cells["类型"].Value == null || dgvr.Cells["类型"].Value.ToString().Trim() == "")
                        continue;
                    if (dgvr.Cells["单价"].Value == null || dgvr.Cells["单价"].Value.ToString().Trim() == "")
                        continue;
                    if (dgvr.Cells["数量"].Value == null || dgvr.Cells["数量"].Value.ToString().Trim() == "")
                        continue;

                    receiptId03 = dgvr.Cells["单据号"].Value.ToString().Trim();
                    orderNo03 = Convert.ToInt32(dgvr.Cells["顺序号"].Value.ToString().Trim());
                    num = Convert.ToInt32(dgvr.Cells["数量"].Value.ToString().Trim());
                    matId = dgvr.Cells["物料编码"].Value.ToString().Trim();
                    matType = Convert.ToInt32(dgvr.Cells["类型"].Value.ToString().Trim());
                    price = Convert.ToDouble(dgvr.Cells["单价"].Value.ToString().Trim());

                    UpdateNotVerificateMatNum(receiptId03, orderNo03, num, price, dgvr, ref deleteRows);

                    this.progressBar1.Value++;
                }
                if (this.checkBox1.Checked == false)
                {
                    //删除已冲销完的行
                    foreach (DataGridViewRow row in deleteRows)
                    {
                        this.dgvReceipt_Det.Rows.Remove(row);
                    }
                }
                this.progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            
        }
        private void UpdateNotVerificateMatNum(string receiptId03, int orderNo03, int num, double price, DataGridViewRow dgvr, ref List<DataGridViewRow> deleteRows)
        {
            string strSql = "select * from T_Receipts_HXDet where ReceiptId03='{0}' and Order03={1}";
            strSql = string.Format(strSql, receiptId03, orderNo03);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)            
                return;
               
            int numSum = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["doNum"].ToString().Trim() != "")
                    numSum = numSum + Convert.ToInt32(dr["doNum"].ToString().Trim());
            }
            if (num - numSum <= 0)
            {
                //核销完，删除DataGridview该行                
                deleteRows.Add(dgvr);

                //更新该行当前未核销的数量
                dgvr.Cells["数量"].Value = (num - numSum).ToString().Trim();
                dgvr.Cells["金额"].Value = ((num - numSum) * price).ToString().Trim();
            }
            else
            {
                //更新该行当前未核销的数量
                dgvr.Cells["数量"].Value = (num - numSum).ToString().Trim();
                dgvr.Cells["金额"].Value = ((num - numSum) * price).ToString().Trim();
            }
        }
        /// <summary>
        /// 之前方法
        /// </summary>       
        private void UpdateNotVerificateMatNum(string receiptId, string matId, int matType, double price,int num, DataGridViewRow dgvr)
        {
            string strSql = "select price,num from T_Receipts_Det where ReceiptId20_03='{0}' and MatId='{1}' and MatType={2} and price={3}";
            strSql = string.Format(strSql, receiptId, matId, matType, price);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["num"].ToString().Trim() != "")
                    num = num + Convert.ToInt32(dr["num"].ToString().Trim());
            }
            if (num <= 0)
            {
                //核销完，删除DataGridview该行
                this.dgvReceipt_Det.Rows.Remove(dgvr);
            }
            else
            {
                //更新该行当前未核销的数量
                dgvr.Cells["数量"].Value = num.ToString().Trim();
            }           
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InitDataGridViewDetail(1);
        }
        /// <summary>
        /// 核销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerificate_Click(object sender, EventArgs e)
        {
            if (!this.dgvReceipt_Det.Columns.Contains("单据号"))
                return;
            if (this.dgvReceipt_Det.SelectedRows.Count <= 0 || this.dgvReceipt_Det.SelectedRows[0].Cells["单据号"].Value == null)
                return;
            string receiptId = this.dgvReceipt_Det.SelectedRows[0].Cells["单据号"].Value.ToString().Trim();
            
            VerificateConfigForm form = new VerificateConfigForm(this.mainForm, receiptId);
            form.dataGridview = Util.CopyNewDataGridViewSelectedRows(this.dgvReceipt_Det);            
            form.ShowDialog();
        }

        private void dgvReceipt_Det_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, this.dgvReceipt_Det.RowHeadersWidth - 5, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvReceipt_Det.RowHeadersDefaultCellStyle.Font, rectangle,
                                  dgvReceipt_Det.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
       

    }
}
