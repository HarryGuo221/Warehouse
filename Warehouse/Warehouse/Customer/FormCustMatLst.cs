using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Customer
{
    
    public partial class FormCustMatLst : Form
    {
        private int curRowIndex = 0;     //选中的行序号
        private int verScrollBar = 0;    //滚动条的位置

        //基础SQL语句（不含Where条件)
        private string BaseSql = "select T_CustomerMac.Sysid as sysid,"
                +"T_CustomerInf.CustName as 客户名称,"
                +"T_CustomerMac.Mdepart as 机器地址,"
                + "T_CustomerMac.CAdd as 备注地址,"
                +"T_AreaInf.Area as 地区,"
                +"T_CustomerMac.Pcode as 邮编,"
                +"T_CustomerMac.Stitle as 送货抬头,"
                + "T_CustomerMac.Sadd as 送货地址,"
                +"T_CustomerMac.Mtype as 机型,"
                + "T_CustomerMac.Manufactcode as 机号,"
                + "T_CustomerMac.ptech as 预订技术员,"
                + "T_CustomerMac.psale as 预订业务员 "
                + " from T_CustomerMac "
                + " left Join T_AreaInf on T_CustomerMac.areaid=T_AreaInf.Areaid "
                + " left Join T_CustomerInf on T_CustomerInf.CustID=T_CustomerMac.CustID";
               
        //where条件
        private string WhereTj = "";
   
        public FormCustMatLst()
        {
            InitializeComponent();
        }

        private void Show_CustMat_Lst(string sql_)
        {
            //string sql_ = this.BaseSql + " " + this.WhereTj;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            (new InitFuncs()).InitDataGridView(this.dgv_custMat, dt);
            try
            {
                this.dgv_custMat.ClearSelection();
                this.dgv_custMat.Rows[this.curRowIndex].Selected = true;
                this.dgv_custMat.FirstDisplayedScrollingRowIndex = this.verScrollBar;
            }
            catch { }

              this.dgv_custMat.Columns["sysid"].Visible = false;
              this.dgv_custMat.Columns["客户名称"].Width = 270;
              this.dgv_custMat.Columns["机器地址"].Width = 270;
            this.toolStripStatusLabel2.Text =this.dgv_custMat.Rows.Count.ToString().Trim();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormCustMatAdd fca = new FormCustMatAdd();
            fca.type = "add";
            fca.custmatFormChange += new FormCustMatAdd.CustMatFormChange(ref_CustMat_lst);
            fca.ShowDialog();
        }

        private void ref_CustMat_lst()
        {
            Show_CustMat_Lst(this.BaseSql + this.WhereTj);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.dgv_custMat.SelectedRows.Count <= 0) return;
            if (this.dgv_custMat.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_custMat.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;

           
            FormCustMatAdd fca = new FormCustMatAdd();
            fca.Cmsysid = sysid_;
            fca.type = "edit";
            fca.custmatFormChange += new FormCustMatAdd.CustMatFormChange(ref_CustMat_lst);
            fca.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.dgv_custMat.SelectedRows.Count <= 0) return;
            if (this.dgv_custMat.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_custMat.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            if (MessageBox.Show("确定删除客户的该条机器信息吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql_ = "";
                try
                {
                    List<string> SqlLst = new List<string>();
                    //删除发票Title
                    sql_ = "delete from T_CustomerMaInvoice where CmSysId='" + sysid_ + "'";
                    SqlLst.Add(sql_);
                    //删除联系人
                    sql_ = "delete from T_CustMaContacts where CmSysId='" + sysid_ + "'";
                    SqlLst.Add(sql_);
                    //删除客户信息
                    sql_ = "delete from T_CustomerMac where sysid=" + sysid_;
                    SqlLst.Add(sql_);

                    (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                    
                     Show_CustMat_Lst(this.BaseSql+this.WhereTj);
                }
                catch
                {
                    MessageBox.Show("该条数据与其他数据存在联系" + '\n' + "不能删除!", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
                
            }
        }

        private void FormCustMatLst_Load(object sender, EventArgs e)
        {
            this.dgv_custMat.AllowUserToAddRows = false;
            // Show_CustMat_Lst();
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click( sender,e);
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //根据Custid(客户ID),CustName(客户名),PinYinCode(拼音码),
            //mtype，Manufactcode(机号)来模糊查询
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                //跳字查询
                textBoxName = textBoxName.Replace(" ", "%");
                //======
                if (textBoxName == "") return;
                string swhere = " where T_CustomerInf.CustID like '%{0}%'"
                    + " or CustName like '%{1}%' or PinYinCode like '%{2}%'"
                    + " or (T_CustomerMac.sysid in "
                    + "(select sysid from T_CustomerMac where "
                    + " T_CustomerMac.Manufactcode like '%{3}%' "
                    + " or T_CustomerMac.mtype like '%{4}%'))";

                swhere = string.Format(swhere, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);
                this.WhereTj = swhere;
                //显示列表
                Show_CustMat_Lst(this.BaseSql + this.WhereTj);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WFilter wf = new WFilter(0, "", true);
            wf.strSql = this.BaseSql;
            wf.btnOK.Visible = false;
            wf.s_items.Add("客户编码,T_CustomerInf.Custid,C");
            wf.s_items.Add("客户名称,T_CustomerInf.CustName,C");
            wf.s_items.Add("机器地址,T_CustomerMac.Mdepart,C");
            wf.s_items.Add("合同编号,T_bargains.BargId,C");
            wf.s_items.Add("地区,T_AreaInf.Area,C");
            wf.s_items.Add("机型,T_CustomerMac.Mtype,C");
            wf.s_items.Add("机号,T_CustomerMac.Manufactcode,C");
            wf.s_items.Add("预订技术员,T_CustomerMac.ptech,C");
            wf.s_items.Add("预订业务员,T_CustomerMac.psale,C");
            
            wf.ShowDialog();
            if (wf.DialogResult == DialogResult.OK)
            {
                Show_CustMat_Lst(wf.Return_Sql);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WhereTj = "";
            //显示列表
            Show_CustMat_Lst(this.BaseSql + this.WhereTj);
        }

        private void dgv_custMat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.curRowIndex = this.dgv_custMat.SelectedRows[0].Index;
            this.verScrollBar = this.dgv_custMat.FirstDisplayedScrollingRowIndex;
       
        }
    }
}
