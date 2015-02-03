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

namespace Warehouse.Customer
{
    public partial class FormCustomerMaInvoiceAdd : Form
    {
        public FormCustomerMaInvoiceAdd()
        {
            InitializeComponent();
        }
        public delegate void CustomerfpmatFormChange();
        public event CustomerfpmatFormChange customerfpmatFormChange;

        public string sysid;   //
        public string Cmsysid;   //系统编号
        public string type;   
        private void FormCustomerMaInvoiceAdd_Load(object sender, EventArgs e)
        {
            InitFuncs inf = new InitFuncs();
            //inf.InitComboBox(this.t_IType, "[发票]发票种类");
            inf.InitComboBox(this.t_IType, "T_Invoice", "ITName");  //发票类型
            inf.InitComboBox(this.t_kpcorp, "T_StoreHouse", "SHName");  //所属公司


            //开票内容，从物料表中取，要对应到具体的物料（以A打头的物料）
            this.s_matid.Items.Clear();
            string matid_="", matname_="";
            string sql_ = "select matid,matname from T_matinf where Matid like 'A%'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                matid_ = dt.Rows[m]["matid"].ToString().Trim();
                matname_ = dt.Rows[m]["matname"].ToString().Trim();
                this.s_matid.Items.Add("["+matid_+"]"+matname_);
            }
            // 

            s_CmSysId.Text = Cmsysid;
            if (this.type == "edit")
            {
                string strSql = "select * from T_CustomerMaInvoice where CmSysId='{0}'and sysid='{1}'";
                strSql = string.Format(strSql, this.Cmsysid,this.sysid);
                (new InitFuncs()).ShowDatas(this.panelkp, strSql);
                this.t_kpcorp.Text = (new DBUtil()).Get_Single_val("T_StoreHouse", "Shname", "shid", this.s_kpcorp.Text.Trim());
                this.t_IType.Text = (new DBUtil()).Get_Single_val("T_Invoice", "ITName", "Itcode", this.s_IType.Text.Trim());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Util.ControlTextIsNUll(this.s_Ititle))
            {
                MessageBox.Show("发票抬头不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Ititle.Focus();
                return;
            }
            //数据处理
            InitFuncs inf = new InitFuncs();
            DBUtil dbUtil = new DBUtil();
            string sqlexe = "";
            string sql_ = "";

            if (type == "add")
            {
                //bool isExist = false;
                //sql_ = "select Ititle from T_CustomerMaInvoice where CmSysId='" + this.Cmsysid + "' and Ititle='" + this.s_Ititle.Text.Trim() + "'";
                //isExist = dbUtil.yn_exist_data(sql_);
                //if (isExist)
                //{
                //    MessageBox.Show("该类发票已存在！");
                //    return;
                //}
                sqlexe = inf.Build_Insert_Sql(this.panelkp, "T_CustomerMaInvoice");
            }
            else
            {
                string swhere = " where CmSysId='" + this.Cmsysid + "'and sysid="+this .sysid;
                sqlexe = inf.Build_Update_Sql(this.panelkp, "T_CustomerMaInvoice", swhere);
            }
            
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sqlexe);
                MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                customerfpmatFormChange();
            }
            catch (Exception w)
            {
                MessageBox.Show(w.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormCustomerMaInvoiceAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void t_kpcorp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_kpcorp.Text = (new DBUtil()).Get_Single_val("T_StoreHouse", "Shid", "shname", this.t_kpcorp.Text.Trim());
      
        }

        private void t_IType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_IType.Text = (new DBUtil()).Get_Single_val("T_Invoice", "Itcode", "ITName", this.t_IType.Text.Trim());
        }
    }
}
