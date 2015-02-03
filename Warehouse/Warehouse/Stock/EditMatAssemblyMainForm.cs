using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DAO;
using Warehouse.DB;
using Warehouse.Base;

namespace Warehouse.Stock
{
    public partial class EditMatAssemblyMainForm : Form
    {
        private string Type;
        private int Sysid;
        private string Assname;

        public delegate void MatAssemblyMainFormChange();
        public event MatAssemblyMainFormChange matAssemblyMainFormChange;

        public EditMatAssemblyMainForm(string type,int sysid,string assname)
        {
            InitializeComponent();
            this.Type = type;
            this.Sysid = sysid;
            this.Assname = assname;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Util.ControlTextIsNUll(s_AssName))
            {
                MessageBox.Show("请输入配套名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                InitFuncs initFuncs = new InitFuncs();
                bool isExist;
                string sql = "";
                string sql_ = "";
                if (this.Type == "add")
                {
                    sql = initFuncs.Build_Insert_Sql(this.panel1, "T_MatAssemblyMain");
                    sql_="select AssName from T_MatAssemblyMain where Ltrim(Rtrim(assname)) like '"+ this.s_AssName.Text.Trim()+"'";
                }
                else if (this.Type == "edit")
                {
                    sql_ = "select AssName from T_MatAssemblyMain where Ltrim(Rtrim(assname)) like '{0}' and Ltrim(Rtrim(sysid)) not like {1}";
                    sql_ = string.Format(sql_, this.s_AssName.Text.Trim(), this.Sysid);
                    
                    string sqlwhere = "where sysid=" + this.Sysid;
                    sql = initFuncs.Build_Update_Sql(this.panel1, "T_MatAssemblyMain", sqlwhere);    
                }
                //验证修改的名称是否已存在
                isExist = (new DBUtil()).yn_exist_data(sql_);
                if (isExist == false)
                {
                    (new SqlDBConnect()).ExecuteNonQuery(sql);
                    matAssemblyMainFormChange();
                    MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("该配套名称已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EditMatAssemblyMainForm_Load(object sender, EventArgs e)
        {
            //值类型文本框输入限制
            (new InitFuncs()).Num_limited(this.panel1);
            if (this.Type == "edit")
            { 
                string show_sql="select * from T_MatAssemblyMain where sysid={0}";
                show_sql=string.Format(show_sql,this.Sysid);
                (new InitFuncs()).ShowDatas(this.panel1, show_sql);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditMatAssemblyMainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);         
        }
    }
}
