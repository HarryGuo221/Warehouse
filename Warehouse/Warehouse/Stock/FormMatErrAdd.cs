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

namespace Warehouse.Stock
{
    public partial class FormMatErrAdd : Form
    {
        public string sysid_ = "";     //Err对应的系统编号
        public string type = "";     // “增加add”还是“修改edit”
        public FormMatErrAdd()
        {
            InitializeComponent();
        }

        public delegate void ErrInfoFormChange();
        public event ErrInfoFormChange ErrInfoFormChange_;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            InitFuncs inf = new InitFuncs();
            if (this.type=="add")
            {
                sql_ = "select * from t_errors where errorcode='" + S_errorCode.Text.Trim() +"'";
                bool isExist = (new DBUtil()).yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该故障编码已经存在！");
                    S_errorCode.Focus();
                    return;
                }
                sql_ = inf.Build_Insert_Sql(panel1, "T_errors");
            }
            else
            {
                sql_ = "select * from t_errors where errorcode='" + S_errorCode.Text.Trim() + "'  and sysid!="+this.sysid_;
                bool isExist = (new DBUtil()).yn_exist_data(sql_);
                if (isExist)
                {
                    MessageBox.Show("该故障编码已经存在！");
                    S_errorCode.Focus();
                    return;
                }
                string swhere_ = " where sysid=" + this.sysid_;
                sql_ = inf.Build_Update_Sql(panel1, "t_errors", swhere_);
            }
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
                if (this.type=="add")
                {
                    S_errorCode.Text = "";
                    S_errorApperance.Text = "";
                    s_ErrorPlace.SelectedIndex = -1;
                    ErrInfoFormChange_(); //激活代理事件，在UserForm中处理
                }
                else
                {
                    ErrInfoFormChange_(); //激活代理事件，在UserForm中处理
                    this.DialogResult = DialogResult.OK; 
                }
            }
            catch(Exception ex)
            {
            }
        }

        private void FormMatErrAdd_Load(object sender, EventArgs e)
        {
            if (this.type=="edit") //编辑
            {
                string sql_ = "select * from t_errors where sysid="+this.sysid_;
                (new InitFuncs()).ShowDatas(panel1, sql_);
            }
            else
            {
                S_errorCode.Focus();
                //S_errorCode.ReadOnly = false;
            }
        }

        private void FormMatErrAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
