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

namespace Warehouse.Bargain
{
    public partial class FormCopySettleInvoiceAdddet : Form
    {
        public string JSD_sysids="";    //多个结算单的sysid
        public string JSLX = "";        //“抄张结算开票”或是“预收费”开票
        

        public string kpcorp = "";
        public string Itype="";
        public string Ititle="";
        public string Icontent="";
        public string Iprice = "";
        public string Inum = "";
        public string Imoney="";
        public string Imemo="";
        public string Igroup = "";
        public string Itech = "";
        public string IsaleUser = "";
            
       
        public delegate void kpchang();
        public event kpchang kpchang_;

        public FormCopySettleInvoiceAdddet()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormCopySettleInvoiceAdddet_Load(object sender, EventArgs e)
        {
            //限制值类型文本框的输入
            (new InitFuncs()).Num_limited(this.panel1);

            InitFuncs inf = new InitFuncs();
           
            inf.InitComboBox(this.t_Itype, "T_Invoice", "ITName");  //发票类型
            inf.InitComboBox(this.t_kpcorp, "T_StoreHouse", "SHName");  //所属公司


            //开票内容，从物料表中取，要对应到具体的物料（以A打头的物料）
            this.s_Icontent.Items.Clear();
            string matid_ = "", matname_ = "";
            string sql_ = "select matid,matname from T_matinf where Matid like 'A%'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                matid_ = dt.Rows[m]["matid"].ToString().Trim();
                matname_ = dt.Rows[m]["matname"].ToString().Trim();
                this.s_Icontent.Items.Add("[" + matid_ + "]" + matname_);
            }
            // 
            this.s_SaleUser.KeyDown+=new KeyEventHandler(InfoFind.UserName_KeyDown);
            this.s_Techuser.KeyDown += new KeyEventHandler(InfoFind.UserName_KeyDown);
             
            //t_Itype.SelectedIndex = this.t_Itype.Items.IndexOf(Itype);

            s_Ititle.Text = this.Ititle;
            s_Icontent.Text = this.Icontent;
            n_Inum.Text = this.Inum;
            n_Iprice.Text = this.Iprice;
            n_Imoney.Text = this.Imoney;
            s_Techuser.Text = this.Itech;
            s_SaleUser.Text = this.IsaleUser;
           
            this.s_kpcorp.Text = (new DBUtil()).Get_Single_val("T_StoreHouse", "Shid", "shname", this.kpcorp);
            this.s_Itype.Text = (new DBUtil()).Get_Single_val("T_Invoice", "ITcode", "Itname", this.Itype); 

            s_Memo.Text = this.Imemo;
            s_groups.SelectedIndex = this.s_groups.Items.IndexOf(this.Igroup);
            this.t_kpcorp.Text = (new DBUtil()).Get_Single_val("T_StoreHouse", "Shname", "shid", this.s_kpcorp.Text.Trim());
            this.t_Itype.Text = (new DBUtil()).Get_Single_val("T_Invoice", "ITName", "Itcode", this.s_Itype.Text.Trim());
      
        }

        private void FormCopySettleInvoiceAdddet_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        //根据结算单编号,查询到多个合同sysid，并返回Where条件
        private string ReturnWhere_Get_machines()
        {
            DataTable dt;
            string sjsdids = this.JSD_sysids;  //多个结算单的sysid
            string bargsys_ = "",bargsys1_="";
            string sql1_ = "";
            int pos_ = -1;
            string swhere = "";
            while (sjsdids.IndexOf(";") != -1)
            {
                pos_ = sjsdids.IndexOf(";");
                bargsys_ = sjsdids.Substring(0, pos_);
                if (bargsys_ == "") continue;
                sjsdids = sjsdids.Substring(pos_ + 1, sjsdids.Length - pos_ - 1);
                //根据结算单号得到BarOrBD_id
                if (this.JSLX == "结算开票")
                {
                    sql1_ = "select BargOrBd_id from T_MacSettle where sysid=" + bargsys_;
                    bargsys_ = (new DBUtil()).Get_Single_val(sql1_);
                }
                else
                {
                    sql1_ = "select BargOrBd_id from T_MacSettlePrePay where sysid=" + bargsys_;
                    bargsys_ = (new DBUtil()).Get_Single_val(sql1_);
                }
                //
                
                if (bargsys_.Substring(0, 2) == "KB")
                {
                    sql1_ = "select BarSysid from T_BargBindMacs "
                            + "where Bdid='" + bargsys_ + "'";
                    dt = (new SqlDBConnect()).Get_Dt(sql1_);
                    for (int m = 0; m < dt.Rows.Count; m++)
                    {
                        bargsys1_ = dt.Rows[m]["BarSysid"].ToString().Trim();
                        if (swhere == "")
                            swhere = " where (sysid=" + bargsys1_ + ")";
                        else
                            swhere = swhere+" or (sysid=" + bargsys1_ + ")";
                    }
                }
                else
                {
                    if (swhere == "")
                        swhere = " where (sysid=" + bargsys_ + ")";
                    else
                        swhere = swhere+" or (sysid=" + bargsys_ + ")";
                }
            }
            return swhere;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string swhere = ReturnWhere_Get_machines();
            string strSql = "select distinct T_StoreHouse.shname as 所属公司,"
                + "T_Invoice.ITname as 发票类型,"
                + "Ititle as 发票抬头,"
                + "matid as 发票内容,"
                + "Memo as 备注"
                + " from T_CustomerMaInvoice "
                +" left join T_StoreHouse on T_CustomerMaInvoice.kpcorp=T_StoreHouse.shid "
                + " left join T_Invoice on T_CustomerMaInvoice.Itype=T_Invoice.ITCode "
                + " where T_CustomerMaInvoice.cmsysid in "
                + "(select MachineId from t_bargains "
                + swhere + ")";
                
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
            fr.unVisible_Column_index_ = -1;
            if (fr.ShowDialog() == DialogResult.OK)
            {
                this.t_kpcorp.Text = fr.dr_.Cells["所属公司"].Value.ToString();
                this.t_Itype.Text = fr.dr_.Cells["发票类型"].Value.ToString();
                this.t_Itype_SelectedIndexChanged(null,null);
                this.t_kpcorp_SelectedIndexChanged(null, null);
                this.s_Ititle.Text = fr.dr_.Cells["发票抬头"].Value.ToString();
                this.s_Icontent.Text = fr.dr_.Cells["发票内容"].Value.ToString();
                this.s_Memo.Text = fr.dr_.Cells["备注"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DBUtil dbUtil = new DBUtil();
            if (Util.ControlTextIsNUll(this.n_Imoney))
            {
                MessageBox.Show("请输入发票金额！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.n_Imoney.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
           
        }

       
        private void s_SaleUser_Leave(object sender, EventArgs e)
        {
            if (this.s_SaleUser.Text.Trim() == "") return;
            if (!InitFuncs.isRightValue(s_SaleUser, "T_Users", "userName"))
            {
                MessageBox.Show("该业务员不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_SaleUser.Focus();
                this.s_SaleUser.Clear();
                return;
            }
        }

        private void s_Techuser_Leave(object sender, EventArgs e)
        {
            if (this.s_Techuser.Text.Trim() == "") return;
            if (!InitFuncs.isRightValue(s_Techuser, "T_Users", "userName"))
            {
                MessageBox.Show("该技术员不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Techuser.Focus();
                this.s_Techuser.Clear();
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string swhere = this.ReturnWhere_Get_machines();

            string strSql = "select distinct Ptech as 技术员,Psale as 业务员 "
                + " from T_CustomerMac "
                 + " where T_CustomerMac.sysid in "
                + "(select MachineId from t_bargains "
                + swhere + ")";
            
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
            fr.unVisible_Column_index_ = -1;
            if (fr.ShowDialog() == DialogResult.OK)
            {
                this.s_Techuser.Text = fr.dr_.Cells["技术员"].Value.ToString();
                this.s_SaleUser.Text = fr.dr_.Cells["业务员"].Value.ToString();
            }
        }

        private void t_kpcorp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_kpcorp.Text = (new DBUtil()).Get_Single_val("T_StoreHouse", "Shid", "shname", this.t_kpcorp.Text.Trim());
      
        }

        private void t_Itype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_Itype.Text = (new DBUtil()).Get_Single_val("T_Invoice", "Itcode", "ITName", this.t_Itype.Text.Trim());
      
        }

        private void Iprice_TextChanged(object sender, EventArgs e)
        {
            Calc_Imoney();
        }

        private void Calc_Imoney()
        {
            double price_, num_;
            if (this.n_Iprice.Text.Trim() == "")
                price_ = 0;
            else
                price_ = Convert.ToDouble(this.n_Iprice.Text.Trim());

            if (this.n_Inum.Text.Trim() == "")
                num_ = 0;
            else
                num_ = Convert.ToDouble(this.n_Inum.Text.Trim());
            this.n_Imoney.Text = (price_ * num_).ToString("F2");
        }
       
    }
}
