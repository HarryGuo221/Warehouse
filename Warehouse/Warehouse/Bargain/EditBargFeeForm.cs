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

namespace Warehouse.Bargain
{
    public partial class EditBargFeeForm : Form
    {
        
        public string Type;    //新增、修改
        public int htyears;    //合同期月数（计算5年核算张数用)
        
        //以下两项确定一个收费
        public string hctype;   //幅面
        public string BarSysId;  //
        public string BargId = "";

        public string Jftype = "";   //基础张数、免印、无免印

        public delegate void FeeChange();
        public event FeeChange FeeChange_;

        public EditBargFeeForm()
        {
            InitializeComponent();
        }
      
        private void EditBargFeeForm_Load(object sender, EventArgs e)
        {
            //有免印张数,无免印张数,有基本张数
            if (this.Jftype == "有基本张数")
            {
                this.n_MyNum.Text = "";
                this.n_MyNum.ReadOnly = true;
                this.n_BaseNum.ReadOnly = false;
            }
            if (this.Jftype == "有免印张数")
            {
                this.n_BaseNum.Text = "";
                this.n_MyNum.ReadOnly = false;
                this.n_BaseNum.ReadOnly = true;
                
            }
            if (this.Jftype == "无免印张数")
            {
                this.n_BaseNum.Text = "";
                this.n_MyNum.Text = "";
                this.n_MyNum.ReadOnly = true;
                this.n_BaseNum.ReadOnly = true;
               
            }

            //限制值类型文本框的输入
            (new InitFuncs()).Num_limited(this.panel1);

            InitFuncs inf = new InitFuncs();
            inf.InitComboBox(this.s_HcType, "[合同]幅面");
            this.t_BargId.Text = this.BargId;
           
            s_HcType.SelectedIndex = 0;
            if (this.Type == "add")
            {
                //s_BargId.Text = this.BargId;
                n_barsysid.Text = this.BarSysId;
                this.s_HcType.Enabled = true;
            }
            else
            {
                string sql_ = "select * from T_bargFee where barsysid=" + this.BarSysId
                    +" and hctype='"+this.hctype+"'";
                (new InitFuncs()).ShowDatas(this.panel1, sql_);
                this.s_HcType.Enabled = false;
            }
            
            //有免印张数,无免印张数,有基本张数
            if (this.Jftype == "有基本张数")
            {
                this.n_MyNum.Text = "";
                this.n_MyNum.ReadOnly = true;
                this.n_BaseNum.ReadOnly = false;
            }
            if (this.Jftype == "有免印张数")
            {
                this.n_BaseNum.Text = "";
                this.n_MyNum.ReadOnly = false;
                this.n_BaseNum.ReadOnly = true;

            }
            if (this.Jftype == "无免印张数")
            {
                this.n_BaseNum.Text = "";
                this.n_MyNum.Text = "";
                this.n_MyNum.ReadOnly = true;
                this.n_BaseNum.ReadOnly = true;

            }

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //输入数据有效性验证
            if (Util.ControlTextIsNUll(s_HcType) == true)
            {
                MessageBox.Show("幅面必须选择！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                s_HcType.Focus();
                return;
            }
            
            string sql_="";
            
            string sqlexe_="";
            if (this.Type == "add")  //新增
            {
                sql_ = "select hctype from T_BargFee where barsysid=" + this.BarSysId 
                  + " and hctype='" + this.s_HcType.Text.Trim() + "'";
                bool isexist = (new DBUtil()).yn_exist_data(sql_);
                if (isexist)
                {
                    MessageBox.Show("该幅面的相关数据已经设置！");
                    return;
                }
                sqlexe_=(new InitFuncs()).Build_Insert_Sql(this.panel1,"T_bargFee");
            }
            else
            {
               string swhere = "where barsysid=" + this.BarSysId + " and hctype='" + this.hctype+"'";
                    sqlexe_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_BargFee", swhere);
            }
            
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sqlexe_);
                MessageBox.Show("保存成功!");
                FeeChange_();
                if (this.Type == "add")  //继续新增
                {
                    (new InitFuncs()).ClearData(this.panel1);
                    n_barsysid.Text = this.BarSysId;
                    //s_BargId.Text = this.BargId;
                }
                else  //修改则关闭
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch
            {
            }
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           this.Close();
        }

        private void EditBargFeeForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void n_NumPerMonth_TextChanged(object sender, EventArgs e)
        {
            if (this.n_NumPerMonth.Text.Trim() != "")
            {
                Int32 n = (this.htyears * Convert.ToInt16(this.n_NumPerMonth.Text) / 60);
                tb_5y.Text = n.ToString().Trim();
            }
            else
            {
                tb_5y.Text = "";
            }
        }

              
       
    }
}

