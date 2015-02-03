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
    public partial class MacModelFormAdd : Form
    {
        public string ID = ""; // 系统编号
        public string type = "";  //新增、编辑

        public MacModelFormAdd()
        {
            InitializeComponent();
        }
        //定义委托及事件
        public delegate void MacModelChange();
        public event MacModelChange macmodelchange;

        private void panelmodel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MacModelFormAdd_Load(object sender, EventArgs e)
        {
            //限制值类型文本框的输入 
            (new InitFuncs()).Num_limited(this.panelmodel);

            InitFuncs inf = new InitFuncs();
            //无码初始化
            inf.InitComboBox(this.s_ProdCtgrID, "[机型]分类");
            inf.InitComboBox(this.s_ModelType, "[机型]机型类别");
            inf.InitComboBox(this.s_ModelGrade, "[机型]机器等级");
            inf.InitComboBox(this.s_Modelbrand, "[机型]品牌");

            if (this.type == "edit")
            {
                string strsql = "select * from T_Model where SysID='{0}'";
                strsql = string.Format(strsql, this.ID);
                (new InitFuncs()).ShowDatas(this.panelmodel, strsql);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Util.ControlTextIsNUll(this.s_ModelName))
            {
                MessageBox.Show("机型名称不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_ModelName.Focus();
                return;
            }
            //数据处理
            InitFuncs inf = new InitFuncs();
            DBUtil dbUtil = new DBUtil();
            string sqlexe = "";
            string sql_ = "";
            if (this.type == "add")
            {
                sql_ = "select ModelName from T_Model where ModelName='" + this.s_ModelName .Text .Trim ()+ "'";
                if (dbUtil.yn_exist_data(sql_))
                {
                    MessageBox.Show("该机器名称已存在！");
                    this.s_ModelName.Focus();
                    return;
                }
                sqlexe = inf.Build_Insert_Sql(this.panelmodel, "T_Model");
            }
            else
            {
                sql_ = "select ModelName from T_Model where ModelName='" + this.s_ModelName.Text.Trim() + "'" +
                      " and SysID!=" + this.ID;
                if (dbUtil.yn_exist_data(sql_))
                {
                    MessageBox.Show("该机器名称已存在！");
                    this.s_ModelName.Focus();
                    return;
                }
                string swhere = " where SysID=" + this.ID ;
                sqlexe = inf.Build_Update_Sql(this.panelmodel, "T_Model", swhere);
            }
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sqlexe);
                MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                macmodelchange();
            }
            catch (Exception w)
            {
                MessageBox.Show(w.ToString());
            }
        }

        private void MacModelFormAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
