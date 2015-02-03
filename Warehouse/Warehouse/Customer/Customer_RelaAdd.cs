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
using Warehouse.DAO;

namespace Warehouse.Customer
{
    public partial class Customer_RelaAdd : Form
    {
       
        private string custid;
        public Customer_RelaAdd(string type, string custid)
        {
            
            this.custid = custid;
            InitializeComponent();
        }

        private void Customer_RelaAdd_Load(object sender, EventArgs e)
        {
            string strCustName = (new DBUtil()).Get_Single_val("T_CustomerInf", "CustName", "CustID", this.custid );
            this.c_CustID.Text = strCustName;
            InitDataGridView();
            
        }
        private void InitDataGridView()
        {
            DataTable dt =CustomerDAO.GetDataOfCustomerRela (this .custid );
            
            (new InitFuncs()).InitDataGridView(this.dataGridViewcustrela , dt);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            WFilter wf = new WFilter(1, "CustName", true);
            wf.tableName = "T_CustomerInf";    //表名 
            wf.strSql = "select CustID as 客户编号, CustName as 客户名称, CustType as 类别,City as 城市地区,communicateAddr as 通信地址, CredDegree as 信用等级,T_CustomerImp.importance as 客户重要度 " +
                            " from T_CustomerInf left join T_CustomerImp " +
                            " on T_CustomerInf.ImportanceDegreeId= T_CustomerImp.Iid";

            wf.s_items.Add("客户编号,CustID,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("类别,CustType,C");
            wf.s_items.Add("城市地区,City,C");
            wf.s_items.Add("重要度,importance,C");
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                //插入
                List<string> sqls = new List<string>();
                DBUtil dbUtil = new DBUtil();
                string custId = this.custid ;

                foreach (string CustName in wf.Return_Items)
                {
                    string parentcustId = dbUtil.Get_Single_val("T_CustomerInf", "CustID", "CustName", CustName.Trim());
                    if (parentcustId == "")
                        return;

                    //插入前判断
                    string strSqlSel = "select * from T_Customer_Rela where CustID='{0}' and ParentID='{1}'";
                    strSqlSel = string.Format(strSqlSel, custId, parentcustId);
                    bool isExit = dbUtil.yn_exist_data(strSqlSel);

                    if (isExit == true)
                        return;

                    string strSql = "insert into T_Customer_Rela(CustID,ParentID) values('{0}','{1}')";
                    strSql = string.Format(strSql, custId, parentcustId);
                    sqls.Add(strSql);

                }
                (new SqlDBConnect()).Exec_Tansaction(sqls);
                
                InitDataGridView();
                
            }
        }

        
        private void buttonok_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewcustrela.SelectedRows.Count <= 0)
                return;

            InitFuncs initFuncs = new InitFuncs();
            string strcustID=(new SqlDBConnect ()).Ret_Single_val_Sql ("select CustID from T_CustomerInf where CustName='"+ this .c_CustID .Text .Trim ()+"'");
 
            string strParentName = this.dataGridViewcustrela.SelectedRows [0].Cells["下级客户名称"].Value.ToString().Trim();

            DBUtil dbUtil = new DBUtil();
            string strParentId = dbUtil.Get_Single_val("T_CustomerInf", "CustID", "CustName", strParentName);

            string strSqlUpdate = "update T_Customer_Rela set InvoiceTitle='" + this.s_InvoiceTitle.Text.Trim() + "' , Memo='" + this.s_Memo.Text.Trim() + "' where CustID='" + strcustID + "' and ParentID='" + strParentId + "'";
            (new SqlDBConnect()).ExecuteNonQuery (strSqlUpdate );
            //MessageBox.Show("更新成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            InitDataGridView();
        }

        private void buttoncancell_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 删除该关系ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    int selectedCellRowIndex = this.dataGridViewcustrela.SelectedCells[0].RowIndex;
                    string strParentName = this.dataGridViewcustrela.Rows[selectedCellRowIndex].Cells["下级客户名称"].Value.ToString().Trim();

                    DBUtil dbUtil = new DBUtil();
                    string strParentId = dbUtil.Get_Single_val("T_CustomerInf", "CustID", "CustName", strParentName);

                    string strSqlDel = "delete from T_Customer_Rela where CustID='{0}' and ParentID='{1}'";
                    strSqlDel = string.Format(strSqlDel, this.custid, strParentId);

                    (new SqlDBConnect()).ExecuteNonQuery(strSqlDel);

                    //初始化DataGridView
                    InitDataGridView();

                    Util.ClearControlText(this.groupBox1);
                }
                catch
                {
                }
            }
        }

        private void dataGridViewcustrela_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewcustrela .SelectedRows.Count <= 0)
                return;
            string strChildName = this.dataGridViewcustrela.SelectedRows[0].Cells["下级客户名称"].Value.ToString().Trim();

            this.c_ParentID.Text = strChildName;
            this.s_InvoiceTitle.Text = this.dataGridViewcustrela.SelectedRows[0].Cells["发票抬头"].Value.ToString().Trim();
            this.s_Memo.Text = this.dataGridViewcustrela.SelectedRows[0].Cells["备注"].Value.ToString().Trim();
  
        }

        private void Customer_RelaAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
