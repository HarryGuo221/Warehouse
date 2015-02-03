using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Base;
using Warehouse.DB;

namespace Warehouse.Receipt
{
    public partial class YsYfForm : Form
    {
        string Type = "";           //预收或预付
        string MatidType = "";      //物料
        string Username = "";       //登录名
        string ReceiptTypeID = "";  //单据类别
        string Elseperson = "";
        string curWorkMonth = "";

        public YsYfForm(string MType, string username)
        {
            InitializeComponent();
            this.Type = MType;
            Username = username;
            Show_Data();
        }
        private void YsYfForm_Load(object sender, EventArgs e)
        {
            //模糊查询
            this.CustID.KeyDown += new KeyEventHandler(InfoFind.CustId_KeyDown);
            this.CustName.KeyDown += new KeyEventHandler(InfoFind.CustName_KeyDown);
            this.curWorkMonth = (this.MdiParent as MainForm).curWorkMonth;
        }
        public void Show_Data()
        {
            //窗体名称
            this.Text = Type + "凭单输入";
            //单据日期
            labAtuoRecordTime.Text = DBUtil.getServerTime().ToString("yyyy-MM-dd");
            //制单人
            BillUser.Text = this.Username;
            if (this.Type == "预付款")
            {
                this.MatidType = "YF_YFK";
                MatId.Text = this.MatidType;
                Matname.Text = "预付款";
                ReceiptTypeID = "YF";
                this.InvoiceNo.Text = "预付款99-1";
            }
            else
            {
                this.MatidType = "YS_YSK";
                MatId.Text = this.MatidType;
                Matname.Text = "预收款";
                ReceiptTypeID = "YS";
                this.InvoiceNo.Text = "预收款99-1";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.CustID.Text.Trim() == "")
            {
                MessageBox.Show("客户编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<string> ListSql = new List<string>();
            string ReceiptId = "";
            //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
            string SqlUpdateBillRullYS = "";
            ReceiptId = DBUtil.Produce_Bill_Id(ReceiptTypeID, DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRullYS);

            string InserSql1 = "insert into T_Receipt_Main(ReceiptId,CurWorkMonth,OccurTime," +
                            " AutoRecordTime,ReceiptTypeID,CustID,CustName,SourceStoreH,InvoiceNo," +
                            " VerifyPerson,BillUser,Memo) values ('{0}','{1}','{2}','{3}'," +
                            " '{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
            InserSql1 = string.Format(InserSql1, ReceiptId, this.curWorkMonth, DDPOccurTime.Text.Trim(),
                        labAtuoRecordTime.Text.Trim(), ReceiptTypeID, CustID.Text.Trim(),
                        CustName.Text.Trim(), SourceStoreH.Text.Trim(), InvoiceNo.Text.Trim(),
                        VerifyPerson.Text.Trim(), BillUser.Text.Trim(), Memo.Text.Trim());

            ListSql.Add(InserSql1);
            ListSql.Add(SqlUpdateBillRullYS);//更新单据号

            double amount = 0.0;
            if (this.Amount.Text.Trim() != "")
                amount = Convert.ToDouble(this.Amount.Text.Trim()) * -1;
            string InsertSql2 = "insert into T_Receipts_Det(ReceiptId,MatId,OrderNo,Amount) values ('{0}','{1}','{2}',{3})";
            InsertSql2 = string.Format(InsertSql2, ReceiptId, this.MatidType, "1", amount);
            ListSql.Add(InsertSql2);
            try
            {
                (new SqlDBConnect()).Exec_Tansaction(ListSql);
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.CustID.Text = "";
            }
            catch (Exception w)
            {
                MessageBox.Show(w.ToString());
            }

        }

        private void CustID_TextChanged(object sender, EventArgs e)
        {
            this.CustName.TextChanged -= CustName_TextChanged;
            this.CustName.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "CustName", "CustID", this.CustID.Text.Trim());

            this.CustName.TextChanged += CustName_TextChanged;
        }

        private void comboBoxSourceStoreH_SelectedIndexChanged(object sender, EventArgs e)
        {
            SourceStoreH.Text = (new DBUtil()).Get_Single_val("T_StoreHouse", "SHId", "SHName",
                                this.comboBoxSourceStoreH.Text.Trim());
        }

        #region 高级查询

        private void Search_Click(object sender, EventArgs e)
        {
            this.CustID.Text = InfoFind.Find_CustId();
        }
        #endregion
        #region 用户模糊查询
        /// <summary>
        /// 用户模糊查询
        /// </summary>
        private void ElsePerson(TextBox textBox)
        {
            string textBoxName = textBox.Text.Trim();
            if (textBoxName == "") return;
            string Strsql = "select UserId 用户编码,UserName 用户名称, " +
                          " UserNameZJM 助记码,MobileTel 移动电话 " +
                          " from T_Users where UserId like '%{0}%' " +
                          " or UserName like '%{1}%'or UserNameZJM like '%{2}%' " +
                          " or MobileTel like '%{3}%'";
            Strsql = string.Format(Strsql, textBoxName, textBoxName, textBoxName, textBoxName);
            DataTable dt = (new SqlDBConnect()).Get_Dt(Strsql);
            FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
            fr.unVisible_Column_index_ = -1;//不隐藏
            if (fr.ShowDialog() == DialogResult.OK)
            {
                this.Elseperson = fr.dr_.Cells["用户名称"].Value.ToString().Trim();
            }
        }

        private void VerifyPerson_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                ElsePerson(textBox);
                VerifyPerson.Text = Elseperson;
            }
        }

        private void BillUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                ElsePerson(textBox);
                BillUser.Text = Elseperson;
            }
        }
        #endregion

        private void YsYfForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
        //限制只能输入数值型
        private void Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }

            //if ("1234567890".IndexOf(e.KeyChar) == -1) e.Handled = true;

            //if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 46 && e.KeyChar != 8)
            //{
            //    e.Handled = true;
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustName_TextChanged(object sender, EventArgs e)
        {
            this.CustID .Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "CustID", "CustName", this.CustName .Text.Trim());
        }


    }
}
