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

namespace Warehouse.Customer
{
    public partial class FormSetCustCall4Time : Form
    {
        public string wssysid_ = ""; //传入的工单编号
        
        public FormSetCustCall4Time()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSetCustCall4Time_Load(object sender, EventArgs e)
        {
            string sql_ = "select OrderTime,startTime,ArriveTime,departTime from T_workSheet where SysId=" + this.wssysid_;
            (new InitFuncs()).ShowDatas(this.panel1, sql_);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string swhere = " where sysId=" + this.wssysid_;
            string sql_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_workSheet", swhere);
            (new SqlDBConnect()).ExecuteNonQuery(sql_);
            this.DialogResult = DialogResult.OK;
        }

        private void FormSetCustCall4Time_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
