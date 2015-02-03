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
    public partial class FormErrAddSol : Form
    {
        //数据成员,传递值
        public string ErrSysid_ ="";    //错误编码对应的系统编号
        public string sysid_ = "";
        public string type = "";
        
        public FormErrAddSol()
        {
            InitializeComponent();
        }

        public delegate void ErrSolFormChange();
        public event ErrSolFormChange ErrSolFormChange_;

        private void FormErrAddSol_Load(object sender, EventArgs e)
        {
            n_Esysid.Text = this.ErrSysid_;
            this.t_ErrorCode.Text = (new DBUtil()).Get_Single_val("T_errors", "ErrorCode", "sysid", this.ErrSysid_);
            
            this.s_Gperson.KeyDown+=new KeyEventHandler(InfoFind.UserName_KeyDown);

            if (this.type=="edit") //编辑
            {
                string sql_ = "select * from t_ErrSol where sysid=" + this.sysid_+" and Esysid="+this.ErrSysid_;
                (new InitFuncs()).ShowDatas(panel1, sql_);
            }
            
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            if (this.type=="edit") //编辑
            {
                string swhere_ = " where sysid=" + this.sysid_+" and Esysid="+this.ErrSysid_;
                sql_ = (new InitFuncs()).Build_Update_Sql(panel1, "T_ErrSol", swhere_);
            }
            else
            {
                sql_ = (new InitFuncs()).Build_Insert_Sql(panel1, "T_ErrSol");
            }
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
                if (this.type=="add")
                {
                    //MessageBox.Show("新增成功！");
                    S_reason.Text = "";
                    s_solution.Text = "";
                    S_reason.Focus();
                    ErrSolFormChange_(); //激活代理事件，在...中处理
                }
                else
                {
                    ErrSolFormChange_(); //激活代理事件，在...中处理
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void FormErrAddSol_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
