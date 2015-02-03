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
    public partial class PrePayAdd : Form
    {
        public delegate void PrePayAddChange();
        public event PrePayAddChange PrePayAddChange_;


        public string curUser = "";  //当前操作者
        public string type = "";     // add、edit
        public string BargOrBdId = "";
        
        public string sysid_ = "";    //传入的系统编号
        public PrePayAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrePayAdd_Load(object sender, EventArgs e)
        {
            string sql_ = "";
            //限制值类型文本框的输入 
            (new InitFuncs()).Num_limited(this.panel1);
            this.s_OperUser.Text = this.curUser;
            if (type == "add")
            {
                s_OccurDay.Value = (DBUtil.getServerTime());
                this.s_BargOrBd_Id.Text = this.BargOrBdId;
            }
            else
            {
                sql_ = "select * from T_MacSettlePrePay where sysid="
                    + this.sysid_;
                (new InitFuncs()).ShowDatas(this.panel1, sql_);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_ = "",swhere="";
            if (this.type == "add")
            {
                sql_ = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_MacSettlePrePay");
            }
            else
            {
                swhere = " where sysid=" + this.sysid_;
                sql_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_MacSettlePrePay", swhere);
            }
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
                MessageBox.Show("操作成功！");
                PrePayAddChange_();
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("操作失败！");
            }
        }

        private void PrePayAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
