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

namespace Warehouse.Financial
{
    public partial class FinancialForm : Form
    {
        private string curUserName;
        private string curWorkMonth;
        private string financialType;//收入/支出
        private string Stsql = "";//查找发票号(不重复)
        DataTable dt;

        public FinancialForm(string financialType)
        {
            InitializeComponent();
            this.financialType = financialType;
        }

        private void FinancialForm_Load(object sender, EventArgs e)
        {
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitComboBox(this.comboBoxPayMethod, "T_PayMethod", "PMName");

            this.txtCustId.KeyDown += new KeyEventHandler(InfoFind.CustId_KeyDown);
            this.txtCustName.KeyDown += new KeyEventHandler(InfoFind.CustName_KeyDown);

            if (this.financialType == "收入")
            {
                this.Text = "应收账务核销";
                this.labelInfo.Text = "应收账务";
                this.btnAutoVeri.Visible = false;
            }
            if (this.financialType == "支出")
            {
                this.Text = "应付账务核销";
                this.labelInfo.Text = "应付账务";
                this.btnAutoVeri.Visible = true;
            }
            this.curUserName = (this.MdiParent as MainForm).userName;
            this.curWorkMonth = (this.MdiParent as MainForm).curWorkMonth;//获得主窗体的当前工作月

            this.txtCustId.Focus();
        }

        private void InitDataGridView()
        {
            //删除最后一条空行
            this.dataGridView1.AllowUserToAddRows = false;
            string custId = this.txtCustId.Text.Trim();
            string invoiceNo = this.txtInvoiceNo.Text.Trim();
            if (custId == "")
                return;

            string strSql = "";
            if (this.financialType == "收入")
            {
                //应收账务(51、52、56单)
                strSql = "select T_Receipt_Main.CurWorkMonth 单据工作月,ReceiptTypeId 单据类别,T_Receipt_Main.ReceiptId 单据号,T_Receipts_Det.OrderNo 分录号,T_Receipt_Main.OccurTime 开单日期," +
                                "T_Receipt_Main.InvoiceNo 发票号,T_Receipt_Main.SourceStoreH 仓库, T_Receipts_Det.MatType 商品类型,T_Receipts_Det.MatId 商品代码, " +
                                "T_MatInf.MatName 商品名称,T_MatInf.Specifications 规格型号,num 数量, price 单价, T_Receipts_Det.Memo 备注, " +
                                "convert(numeric(8,2),round(Amount - MoneyPayed,2)) 应核销金额, " +
                                "convert(numeric(8,2),round(Amount,2)) 单据发票合计数 " +
                                "from T_Receipt_Main,T_Receipts_Det,T_MatInf " +
                                "where T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and T_Receipts_Det.MatId=T_MatInf.MatId " +
                                "and (T_Receipt_Main.ReceiptTypeId = '51' or T_Receipt_Main.ReceiptTypeId = '52' or T_Receipt_Main.ReceiptTypeId = '56' " +
                                " or T_Receipt_Main.ReceiptTypeId = 'YS') " +
                                "and Amount - MoneyPayed != 0 " +
                                "and T_Receipt_Main.CustId='" + custId + "' ";
                if (invoiceNo != "")
                    strSql = strSql + " and T_Receipt_Main.InvoiceNo='" + invoiceNo + "' ";
                strSql += " order by InvoiceNo";
                //查找编号
                Stsql = "select distinct InvoiceNo  from T_Receipt_Main_Det " +
                      " where (ReceiptTypeId = '51' or ReceiptTypeId = '52' " +
                      " or ReceiptTypeId = '56' or ReceiptTypeId = 'YS') " +
                      " and CustId='" + custId + "' order by InvoiceNo ";
            }
            else if (this.financialType == "支出")
            {
                //应付账务(01（且发票号不为“77777777”的单据,还有 11111111，22222222，33333333，55555555，66666666）不取）、03、20单)
                strSql = "select T_Receipt_Main.CurWorkMonth 单据工作月,ReceiptTypeId 单据类别,T_Receipt_Main.ReceiptId 单据号,T_Receipts_Det.OrderNo 分录号,T_Receipt_Main.OccurTime 开单日期," +
                                "T_Receipt_Main.InvoiceNo 发票号,T_Receipt_Main.SourceStoreH 仓库, T_Receipts_Det.MatType 商品类型,T_Receipts_Det.MatId 商品代码, " +
                                "T_MatInf.MatName 商品名称,T_MatInf.Specifications 规格型号,num 数量, price 单价, T_Receipts_Det.Memo 备注,  " +
                                "convert(numeric(8,2),round(Amount - MoneyPayed,2)) 应核销金额, " +
                                "convert(numeric(8,2),round(Amount,2)) 单据发票合计数 " +
                                "from T_Receipt_Main,T_Receipts_Det,T_MatInf " +
                                "where T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and T_Receipts_Det.MatId=T_MatInf.MatId " +
                                " and ((T_Receipt_Main.ReceiptTypeId = '01' and T_Receipt_Main.InvoiceNo!='77777777' " +
                                " and T_Receipt_Main.InvoiceNo!='11111111' and T_Receipt_Main.InvoiceNo!='22222222' " +
                                " and T_Receipt_Main.InvoiceNo!='33333333' and T_Receipt_Main.InvoiceNo!='55555555' " +
                                " and T_Receipt_Main.InvoiceNo!='66666666') " +
                                " or T_Receipt_Main.ReceiptTypeId = '03' or T_Receipt_Main.ReceiptTypeId = '20' " +
                                " or T_Receipt_Main.ReceiptTypeId = 'YF') " +
                                " and Amount - MoneyPayed != 0 " +
                                " and T_Receipt_Main.CustId='" + custId + "' ";
                if (invoiceNo != "")
                    strSql = strSql + " and T_Receipt_Main.InvoiceNo='" + invoiceNo + "' ";
                strSql += " order by InvoiceNo";
                //查找编号
                Stsql = "select distinct InvoiceNo  from T_Receipt_Main_Det " +
                      " where ((ReceiptTypeId = '01' and InvoiceNo!='77777777' " +
                      " and InvoiceNo!='11111111' and InvoiceNo!='22222222' " +
                      " and InvoiceNo!='33333333' and InvoiceNo!='55555555' " +
                      " and InvoiceNo!='66666666') " +
                      " or ReceiptTypeId = '03' " +
                      " or ReceiptTypeId = '20' or ReceiptTypeId = 'YF') " +
                      " and CustId='" + custId + "'order by InvoiceNo ";
            }

            dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
            {
                this.dataGridView1.DataSource = null;
                return;
            }
            (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.ReadOnly = false;
            this.dataGridView1.Columns["单据工作月"].Visible = false;
            this.dataGridView1.Columns["分录号"].Visible = false;
            this.dataGridView1.Columns["单价"].Visible = false;
            dataGridView1.Columns["单据发票合计数"].Visible = false;
            this.dataGridView1.Columns["分录号"].Width = 65;
            this.dataGridView1.Columns["仓库"].Width = 55;
            this.dataGridView1.Columns["商品类型"].Width = 80;
            this.dataGridView1.Columns["数量"].Width = 55;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.HeaderText = "实核销金额";
            column1.Name = "实核销金额";
            DataGridViewCheckBoxColumn column2 = new DataGridViewCheckBoxColumn();
            column2.HeaderText = "核完";
            column2.Name = "核完";
            //增加一列记录相同发票号累加的发票合计数
            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.HeaderText = "发票合计数";
            column3.Name = "发票合计数";
            this.dataGridView1.Columns.Add(column3);
            this.dataGridView1.Columns.Add(column1);
            this.dataGridView1.Columns.Add(column2);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            {
                if (column.HeaderText == "实核销金额" || column.HeaderText == "核完")
                    column.ReadOnly = false;
                else
                    column.ReadOnly = true;
            }

            //合计数
            double sumInvoice = 0.0;
            double sumY = 0.0;
            double InvoiceAmount = 0.0; //发票合计数
            int i = 0;
            foreach (DataGridViewRow dr in this.dataGridView1.Rows)
            {
                if (dr.Cells["单据号"].Value != null && dr.Cells["单据号"].Value.ToString().Trim() != "")
                {
                    sumInvoice += Convert.ToDouble(dr.Cells["单据发票合计数"].Value.ToString().Trim());
                    sumY += Convert.ToDouble(dr.Cells["应核销金额"].Value.ToString().Trim());
                }
            }
            this.labSumInvoice.Text = string.Format("{0:f2} ", sumInvoice);
            this.labSumY.Text = string.Format("{0:f2} ", sumY);
            //根据相同的发票号将发票合计数累加
            DataTable table = (new SqlDBConnect()).Get_Dt(Stsql);
            for (i = 0; i < table.Rows.Count; i++)
            {
                bool isNotFound = false;
                foreach (DataGridViewRow dr in this.dataGridView1.Rows)
                {
                    //a = table.Rows[i][0].ToString();
                    if ((dr.Cells["发票号"].Value != null) &&
                        (dataGridView1.Rows[dr.Index].Cells["发票号"].Value.ToString() ==
                        table.Rows[i][0].ToString()))
                    {

                        InvoiceAmount += Convert.ToDouble(dr.Cells["单据发票合计数"].Value.ToString().Trim());
                        //dataGridView1.Rows[dr.Index].Cells["发票合计数"].Value = InvoiceAmount.ToString();
                        isNotFound = true;
                    }
                    else
                    {
                        if (isNotFound)
                        {
                            if (InvoiceAmount == 0.0)
                                dataGridView1.Rows[dr.Index - 1].Cells["发票合计数"].Value = "0.0";
                            else
                                dataGridView1.Rows[dr.Index - 1].Cells["发票合计数"].Value = string.Format("{0:f2} ", InvoiceAmount);

                            InvoiceAmount = 0.0;
                            break;
                        }
                    }
                }
            }
        }

        private void comboBoxPayMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.s_PayMethodId.Text = (new DBUtil()).Get_Single_val("T_PayMethod", "PMid", "PMName", this.comboBoxPayMethod.Text.Trim());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtCustId.Text.Trim() == "")
            {
                MessageBox.Show("客户代码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtInMoney.Text.Trim() != "" && !Util.IsNumberic(this.txtInMoney))
            {
                MessageBox.Show("核销金额只能为数值型数据！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<string> sqls = new List<string>();
            string custId = this.txtCustId.Text.Trim();
            string custName = this.txtCustName.Text.Trim();
            string payMethod = "";
            if (this.comboBoxPayMethod.SelectedIndex != 0)
                payMethod = this.comboBoxPayMethod.Text.Trim();
            string bankAccount = this.txtBankAccount.Text.Trim();
            double inMoney = 0.0;
            if (this.txtInMoney.Text.Trim() != "")
                inMoney = Convert.ToDouble(this.txtInMoney.Text.Trim());
            string pinZhengHao = this.txtPinZhengHao.Text.Trim();
            string abstact = this.txtAbstract.Text.Trim();
            string record = this.txtRecord.Text.Trim();
            string memo = this.txtMemo.Text.Trim();

            string financialId = "";
            if (this.financialType == "收入")
            {
                //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
                string SqlUpdateBillRullYS = "";
                financialId = DBUtil.Produce_Bill_Id("YS", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRullYS);

                //插入账务主表
                string strSqlMain = "insert into T_Financial(FinancialId,CurWorkMonth,FinancialType,CustId,CustName,PayMethod,BankAccount,InMoney," +
                                                            "OccurTime,PinZhengHao,Abstract,Record,OperatePerson,Memo) " +
                                    "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')";
                strSqlMain = string.Format(strSqlMain, financialId, this.curWorkMonth, "收入", custId, custName, payMethod, bankAccount, inMoney,
                                           DBUtil.getServerTime().ToString().Trim(), pinZhengHao, abstact, record, this.curUserName, memo);

                sqls.Add(strSqlMain);
                sqls.Add(SqlUpdateBillRullYS);//更新单据号
            }
            else if (this.financialType == "支出")
            {
                //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
                string SqlUpdateBillRullYF = "";
                financialId = DBUtil.Produce_Bill_Id("YF", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRullYF);

                //插入账务主表
                string strSqlMain = "insert into T_Financial(FinancialId,CurWorkMonth,FinancialType,CustId,CustName,PayMethod,BankAccount,OutMoney," +
                                                            "OccurTime,PinZhengHao,Abstract,Record,OperatePerson,Memo) " +
                                    "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')";
                strSqlMain = string.Format(strSqlMain, financialId, this.curWorkMonth, "支出", custId, custName, payMethod, bankAccount, inMoney,
                                           DBUtil.getServerTime().ToString().Trim(), pinZhengHao, abstact, record, this.curUserName, memo);

                sqls.Add(strSqlMain);
                sqls.Add(SqlUpdateBillRullYF);//更新单据号
            }

            //插入账务子表
            foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
            {

                if (dgvr.Cells["单据号"].Value == null || dgvr.Cells["单据号"].Value.ToString().Trim() == "")
                    continue;
                string workMonth = dgvr.Cells["单据工作月"].Value.ToString().Trim();
                string receiptId = dgvr.Cells["单据号"].Value.ToString().Trim();
                string orderNo = dgvr.Cells["分录号"].Value.ToString().Trim();
                double moneyPayed = 0.0;
                if (dgvr.Cells["实核销金额"].Value != null && dgvr.Cells["实核销金额"].Value.ToString().Trim() != "")
                    moneyPayed = Convert.ToDouble(dgvr.Cells["实核销金额"].Value.ToString().Trim());
                string strSqlDet = "insert into T_Financial_Det(FinancialId,workMonth,ReceiptId,OrderNo,MoneyPayed) " +
                                   "values('{0}','{1}','{2}','{3}','{4}')";
                strSqlDet = string.Format(strSqlDet, financialId, workMonth, receiptId, orderNo, moneyPayed);
                sqls.Add(strSqlDet);

                //更新单据子表的MoneyPayed
                string strSqlUpdate = "update T_Receipts_Det set MoneyPayed = MoneyPayed + {0} where ReceiptId='{1}' and OrderNo='{2}'";
                strSqlUpdate = string.Format(strSqlUpdate, moneyPayed, receiptId, orderNo);
                sqls.Add(strSqlUpdate);
            }

            //执行事务处理所有的Sql
            SqlDBConnect db = new SqlDBConnect();
            db.Exec_Tansaction(sqls);

            MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //保存后，新建一个
            if (this.financialType == "收入")
            {
                FinancialForm form = new FinancialForm("收入");
                form.MdiParent = this.MdiParent as MainForm;
                form.Show();
            }
            if (this.financialType == "支出")
            {
                FinancialForm form = new FinancialForm("支出");
                form.MdiParent = this.MdiParent as MainForm;
                form.Show();
            }
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Util.ClearControlText(this.panel1);


        }
        /// <summary>
        /// 高级查找“客户ID”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindCustId_Click(object sender, EventArgs e)
        {
            this.txtCustId.Text = InfoFind.Find_CustId();
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void s_CustId_TextChanged(object sender, EventArgs e)
        {
            this.txtCustName.TextChanged -= s_CustName_TextChanged;
            this.txtCustName.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "CustName", "CustID", this.txtCustId.Text.Trim());

            this.dataGridView1.DataSource = (new DataTable()).DefaultView;
            InitDataGridView();
            //限制列的排序及显示顺序
            SortMode();

            this.txtCustName.TextChanged += s_CustName_TextChanged;
        }

        private void s_CustName_TextChanged(object sender, EventArgs e)
        {
            this.txtCustId.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "CustID", "CustName", this.txtCustName.Text.Trim());
        }
        /// <summary>
        /// 发票号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            InitDataGridView();
            SortMode();
        }

        /// <summary>
        /// 限制列的排序及显示顺序
        /// </summary>
        private void SortMode()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.dataGridView1.CurrentCell;
            if (cell.Value == null || cell.Value.ToString().Trim() == "")
                return;

            if (cell.OwningColumn.HeaderText == "实核销金额" && cell.Value.ToString() != "")
            {
                CalSumMoney(); //计算总核销金额

                string Realamount = cell.Value.ToString();
                string YHmoney = dataGridView1.Rows[cell.RowIndex].Cells["应核销金额"].Value.ToString();
                double SHamount = 0.0;
                if (Realamount != "")
                    SHamount = Convert.ToDouble(Realamount);//实核销金额
                double YHamount = 0.0;
                if (YHmoney != "")
                    YHamount = Convert.ToDouble(YHmoney);//应核销金额
                //判断实核销金额的取值情况
                if (YHamount < 0)
                {
                    if (SHamount > 0)
                    {
                        MessageBox.Show("实核销金额不能大于0 ！", "提示");
                        cell.Value = "";
                        return;
                    }
                    else if (SHamount < YHamount)//特殊
                    {
                        MessageBox.Show("实核销金额不能大于应核销金额！", "提示");
                        cell.Value = "";
                        return;
                    }
                }
                else  //  >=0
                {
                    if (SHamount < 0)
                    {
                        MessageBox.Show("实核销金额不能小于0 ！", "提示");
                        cell.Value = "";
                        //this.dataGridView1.CurrentCell = this.dataGridView1.Rows[cell.RowIndex].Cells["应核销金额"];
                        //this.dataGridView1.BeginEdit(true); 
                        return;
                    }
                    if (SHamount > YHamount)
                    {
                        MessageBox.Show("实核销金额不能大于应核销金额！", "提示");
                        cell.Value = "";
                        return;
                    }
                }
                if (SHamount == YHamount)
                {
                    dataGridView1.Rows[cell.RowIndex].Cells["核完"].Value = true;
                }
            }
        }
        private void CalSumMoney()
        {
            double inMoney = 0.0;
            foreach (DataGridViewRow dr in this.dataGridView1.Rows)
            {
                if (dr.Cells["实核销金额"].Value != null && dr.Cells["实核销金额"].Value.ToString().Trim() != "" &&
                    dr.Cells["单据号"].Value != null && dr.Cells["单据号"].Value.ToString().Trim() != "")
                    inMoney += Convert.ToDouble(dr.Cells["实核销金额"].Value.ToString().Trim());
            }
            this.txtInMoney.Text = string.Format("{0:f2} ", inMoney);
            this.labSumS.Text = string.Format("{0:f2} ", inMoney);
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, this.dataGridView1.RowHeadersWidth - 5, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridView1.RowHeadersDefaultCellStyle.Font, rectangle,
                                  dataGridView1.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.OwningColumn.HeaderText == "核完")
            {
                int rowIndex = e.RowIndex;
                if (this.dataGridView1.Rows[rowIndex].Cells["单据号"].Value == null || this.dataGridView1.Rows[rowIndex].Cells["单据号"].Value.ToString().Trim() == "")
                    return;
                DataGridViewCheckBoxCell checkBoxCell = this.dataGridView1[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;

                if (checkBoxCell != null)
                {
                    if (object.Equals(checkBoxCell.EditingCellFormattedValue, true))
                    {
                        //false
                        this.dataGridView1.Rows[rowIndex].Cells["实核销金额"].Value = "";
                        CalSumMoney();
                    }
                    else if (object.Equals(checkBoxCell.EditingCellFormattedValue, false))
                    {
                        //true                       
                        this.dataGridView1.Rows[rowIndex].Cells["实核销金额"].Value = this.dataGridView1.Rows[rowIndex].Cells["应核销金额"].Value.ToString().Trim();
                        CalSumMoney();
                    }
                }
                //bool check = Convert.ToBoolen(DataGridView.Rows[i].cells[j].FormatedValue);
            }
        }
        /// <summary>
        /// 是否完全核销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow dr in this.dataGridView1.Rows)
                {
                    if (dr.Cells["单据号"].Value != null && dr.Cells["单据号"].Value.ToString().Trim() != "")
                    {
                        dr.Cells["实核销金额"].Value = dr.Cells["应核销金额"].Value.ToString().Trim();
                        dr.Cells["核完"].Value = true;
                        CalSumMoney();
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow dr in this.dataGridView1.Rows)
                {
                    if (dr.Cells["单据号"].Value != null && dr.Cells["单据号"].Value.ToString().Trim() != "")
                    {
                        dr.Cells["实核销金额"].Value = "";
                        dr.Cells["核完"].Value = false;
                        CalSumMoney();
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //控制某一列只能输入数字
            if (e.Control is TextBox)
            {
                TextBox tb = e.Control as TextBox;
                if (this.dataGridView1.CurrentCell.OwningColumn.HeaderText == "实核销金额")
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                else
                    tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
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
        /// 应付自动核销
        /// 核销规则为: 相同单据日期、客户名、商品名，数量相反，金额能够正负抵消。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoVeri_Click(object sender, EventArgs e)
        {
            #region //方法1
            /*
            int i = 0;
            List<string> SqlAdd = new List<string>();
            string sql1="";
            string sql2="";
            int num1 = 0;
            int num2 = 0;
            double Amount1 = 0.0;
            double Amount2 = 0.0;
            double MoneyPayed1 = 0.0;
            double MoneyPayed2 = 0.0;
            string OccurTime1 = "";
            string OccurTime2 = "";
            string ReceiptTypeID1 = "";
            string ReceiptTypeID2 = "";
            string CustID1 = "";
            string CustID2 = "";
            string MatId1 = "";
            string MatId2 = "";
            if (txtCustId .Text!=""&& dt!=null) //不存在数据时
            {
                if (MessageBox.Show("是否核销掉当前数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string sqlpd = "select ReceiptId " +
                                  "from T_Receipt_Main_Det where Amount!=MoneyPayed and  ((ReceiptTypeID ='01' " +
                                  " and InvoiceNo!='77777777'and InvoiceNo!='11111111' and InvoiceNo!='22222222' " +
                                  " and InvoiceNo!='33333333' and InvoiceNo!='55555555' and InvoiceNo!='66666666') " +
                                  " or (ReceiptTypeID ='20')) and CustID='" + txtCustId.Text.Trim() + "'";
                    DataTable dtt=new DataTable ();
                    dtt=(new SqlDBConnect ()).Get_Dt (sqlpd );
                    int count = dtt.Rows.Count;
                    if (count == 0)
                    {
                        MessageBox.Show("不存在满足核销的记录！", "提示");
                        return;
                    }
                    string Strsql = "select ReceiptTypeID ,CustID ,MatId , " +
                                  "num ,price ,Amount ,MoneyPayed,OccurTime,ReceiptId " +
                                  "from T_Receipt_Main_Det where Amount!=MoneyPayed and ((ReceiptTypeID ='01' " +
                                  " and InvoiceNo!='77777777'and InvoiceNo!='11111111' and InvoiceNo!='22222222' " +
                                  " and InvoiceNo!='33333333' and InvoiceNo!='55555555' and InvoiceNo!='66666666') " +
                                  " or (ReceiptTypeID ='20')) and CustID='" + txtCustId.Text.Trim() + "' " +
                                  " order by MatId ,CustID,OccurTime";
                    DataTable Table = (new SqlDBConnect()).Get_Dt(Strsql);
                    for (i=0; i < Table.Rows.Count; i++)
                    {
                        ReceiptTypeID1 = Table.Rows[i][0].ToString();
                        ReceiptTypeID2 = Table.Rows[i + 1][0].ToString();
                        CustID1 = Table.Rows[i][1].ToString();
                        CustID2 = Table.Rows[i + 1][1].ToString();
                        MatId1 = Table.Rows[i][2].ToString();
                        MatId2 = Table.Rows[i + 1][2].ToString();
                        num1  = Convert.ToInt32 (Table.Rows[i][3].ToString());
                        num2  = Convert.ToInt32 (Table.Rows[i+1][3].ToString());
                        Amount1 = Convert.ToDouble(Table.Rows[i][5].ToString());
                        MoneyPayed1 = Convert.ToDouble(Table.Rows[i][6].ToString());
                        Amount2 = Convert.ToDouble(Table.Rows[i+1][5].ToString());
                        MoneyPayed2 = Convert.ToDouble(Table.Rows[i+1][6].ToString());
                        OccurTime1 = Convert.ToDateTime (Table.Rows[i][7]).ToString("yyyy-MM-dd");
                        OccurTime2 = Convert.ToDateTime(Table.Rows[i+1][7]).ToString("yyyy-MM-dd");
                        //if (Amount ==MoneyPayed ) continue ;
                        if (ReceiptTypeID1 !=ReceiptTypeID2  && CustID1 ==CustID2 
                            && MatId1 ==MatId2  && Math .Abs (num1) ==Math .Abs (num2)
                            && Math .Abs(Amount1 )==Math .Abs (Amount2 )  && OccurTime1 ==OccurTime2 )
                        {
                            
                            sql1 = "Update T_Receipt_Main_Det set MoneyPayed='" + Amount1 + "'where ReceiptId='" + Table.Rows[i][8].ToString() + "'";
                            SqlAdd.Add(sql1);
                            sql2 = "Update T_Receipt_Main_Det set MoneyPayed='" + Amount2 + "'where ReceiptId='" + Table.Rows[i + 1][8].ToString() + "'";
                            SqlAdd.Add(sql2);
                            i++;
                            
                        }
                        else
                        {
                            continue;
                        }
                        //sql1 = "";
                        //sql2 = "";
                    }
                    try
                    {
                        (new SqlDBConnect()).Exec_Tansaction(SqlAdd);
                        MessageBox.Show("操作成功！", "提示");
                        this.dataGridView1.DataSource = (new DataTable()).DefaultView;
                        InitDataGridView();
                        
                    }
                    catch
                    {
                        return;
                    }
                }
            }*/
            #endregion //方法1

            #region //方法2
            List<string> SqlAdd = new List<string>();
            string sql1 = "";
            string sql2 = "";
            int num1 = 0;
            int num2 = 0;
            double Amount1 = 0.0;
            double Amount2 = 0.0;
            string OccurTime1 = "";
            string OccurTime2 = "";
            string MatId1 = "";
            string MatId2 = "";

            if (txtCustId.Text != "" && dt != null) //不存在数据时
            {
                if (MessageBox.Show("是否核销掉当前数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string sqlpd = "select ReceiptId " +
                                  "from T_Receipt_Main_Det where Amount!=MoneyPayed and  ((ReceiptTypeID ='01' " +
                                  " and InvoiceNo!='77777777'and InvoiceNo!='11111111' and InvoiceNo!='22222222' " +
                                  " and InvoiceNo!='33333333' and InvoiceNo!='55555555' and InvoiceNo!='66666666') " +
                                  " or (ReceiptTypeID ='20')) and CustID='" + txtCustId.Text.Trim() + "'";
                    DataTable dtt = new DataTable();
                    dtt = (new SqlDBConnect()).Get_Dt(sqlpd);
                    int count = dtt.Rows.Count;
                    if (count == 0)
                    {
                        MessageBox.Show("不存在满足核销的记录！", "提示");
                        return;
                    }
                    foreach (DataGridViewRow dr in this.dataGridView1.Rows)
                    {
                        if (this.dataGridView1.Rows[dr.Index].Cells["单据类别"].Value.ToString() == "01")
                        {
                            num1 = Convert.ToInt32(dataGridView1.Rows[dr.Index].Cells["数量"].Value.ToString());
                            Amount1 = Convert.ToDouble(dataGridView1.Rows[dr.Index].Cells["单据发票合计数"].Value.ToString());
                            OccurTime1 = Convert.ToDateTime(dataGridView1.Rows[dr.Index].Cells["开单日期"].Value).ToString("yyyy-MM-dd");
                            MatId1 = dataGridView1.Rows[dr.Index].Cells["商品代码"].Value.ToString();

                            foreach (DataGridViewRow drr in this.dataGridView1.Rows)
                            {
                                if (this.dataGridView1.Rows[drr.Index].Cells["单据类别"].Value.ToString() == "20")
                                {
                                    num2 = Convert.ToInt32(dataGridView1.Rows[drr.Index].Cells["数量"].Value.ToString());
                                    Amount2 = Convert.ToDouble(dataGridView1.Rows[drr.Index].Cells["单据发票合计数"].Value.ToString());
                                    MatId2 = dataGridView1.Rows[drr.Index].Cells["商品代码"].Value.ToString();
                                    OccurTime2 = Convert.ToDateTime(dataGridView1.Rows[drr.Index].Cells["开单日期"].Value).ToString("yyyy-MM-dd");
                                    if (MatId1 == MatId2 && Math.Abs(num1) == Math.Abs(num2) && Math.Abs(Amount1) == Math.Abs(Amount2) && OccurTime1 == OccurTime2)
                                    {
                                        sql1 = "Update T_Receipts_Det set MoneyPayed='" + Amount1 + "'where ReceiptId='" + dataGridView1.Rows[dr.Index].Cells["单据号"].Value.ToString() + "'";
                                        SqlAdd.Add(sql1);
                                        sql2 = "Update T_Receipts_Det set MoneyPayed='" + Amount2 + "'where ReceiptId='" + dataGridView1.Rows[drr.Index].Cells["单据号"].Value.ToString() + "'";
                                        SqlAdd.Add(sql2);
                                        continue;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    try
                    {
                        (new SqlDBConnect()).Exec_Tansaction(SqlAdd);
                        MessageBox.Show("操作成功！", "提示");
                        this.dataGridView1.DataSource = (new DataTable()).DefaultView;
                        InitDataGridView();

                    }
                    catch
                    {
                        return;
                    }
                }
            }
            #endregion //方法2
        }

        private void FinancialForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.txtCustId.Focused)
                {
                    SendKeys.Send("{TAB}");
                }                
                SendKeys.Send("{TAB}");//也可以使用这个代替SelectNextControl
            }
        }
    }
}
