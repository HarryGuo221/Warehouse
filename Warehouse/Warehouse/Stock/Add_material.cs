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
using System.IO;

namespace Warehouse.Stock
{
    public partial class Add_materialForm : Form
    {
        private string type;
        public string matId;

        public delegate void MaterialInfoFormChange();
        /// <summary>
        /// 物料卡片信息改变事件
        /// </summary>
        public event MaterialInfoFormChange materialInfoFormChange;

        public Add_materialForm()
        {
            InitializeComponent();
        }
        public Add_materialForm(string type, string matSysId)
        {
            InitializeComponent();
            this.type = type;
            this.matId = matSysId;            
        }      

        #region//初始化combobox
        private void Add_materialForm_Load(object sender, EventArgs e)
        {
           //限制值类型文本框的输入

           (new InitFuncs()).Num_limited(this.panel1);

            //初始化界面
            InitFuncs inf = new InitFuncs();
            //
            inf.InitComboBox(s_Units, "[物料]计量单位");//初始化“列”单位Units
            inf.InitComboBox(s_QualityDegree, "[物料]质量等级");//初始化“列”质量等级QualityDegree
            inf.InitComboBox(this.s_ColorType,"[物料]色彩");
            inf.InitComboBox(this.s_Format, "[物料]幅面");
            inf.InitComboBox(this.s_Speed, "[物料]速度等级");
            inf.InitComboBox(this.s_ProductType, "[物料]产品种类");
            inf.InitComboBox(this.s_types, "[物料]机种");
            inf.InitComboBox(this.s_ConfigType, "[物料]配置类型");
            inf.InitComboBox(this.s_Brand, "[机型]品牌");

            if (this.type == "edit")
            {
                s_MatID.ReadOnly = true;

                string strsql = "select * from T_MatInf where MatID='{0}'";
                strsql = string.Format(strsql, this.matId);
                inf.ShowDatas(this.panel1, strsql);
            }
            else
            {
                s_MatID.ReadOnly = false;
                s_InputTime.Text = DBUtil.getServerTime().ToString();

                this.s_MatID.Text = this.matId;//单据模块用
            }
        }
        #endregion

        #region//添加或更改物料卡片
        private void buttonOK_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            string sqlExe_ = "";
            bool ynExistName = false;
            DBUtil dbUtil = new DBUtil();
            #region//必填项验证
            if (Util.ControlTextIsNUll(this.s_MatID))
            {
                MessageBox.Show("请输入物料编号！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (Util.ControlTextIsNUll(this.s_MatName) == true)
            {
                MessageBox.Show("请输入物料名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion   
            try            
            {
                if (this.type == "add")
                {
                    #region// 判断物料ID和//名称是否存在
                    string sql = "select * from t_matinf where matid='" + this.s_MatID.Text.Trim() + "'";
                    bool ynExistID = dbUtil.yn_exist_data(sql);
                    if (ynExistID == true)
                    {
                        MessageBox.Show("该物料编号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //sql_ = "select Matid from T_MatInf where MatName='" + s_MatName.Text.Trim() + "'";
                    //ynExistName = dbUtil.yn_exist_data(sql_);

                    //if (ynExistName == true)
                    //{
                    //    MessageBox.Show("该物料名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    s_MatName.Focus();
                    //    return;
                    //}
                    #endregion

                    sqlExe_ = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_MatInf");
                    
                    try
                    {
                        (new SqlDBConnect()).ExecuteNonQuery(sqlExe_);
                        MessageBox.Show("添加成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (this.matId == "") //单据模块新增代码
                            materialInfoFormChange();
                        this.DialogResult = DialogResult.OK;

                        this.matId = this.s_MatID.Text.Trim();//单据模块新增代码
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (this.type == "edit")
                {
                    //判断新物料名称存在否
                    sql_ = "select Matid from T_MatInf where MatName='" + s_MatName.Text.Trim() + "' and matid<>'"+this.matId+"'";
                    ynExistName = dbUtil.yn_exist_data(sql_);

                    string swhere = " where MatId='" + this.matId + "'";
                    sqlExe_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_MatInf", swhere);
                    if (!ynExistName)
                    {
                        sqlExe_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_MatInf", " where  MatId='" + this.s_MatID.Text.Trim() + "'");
                        //执行
                        (new SqlDBConnect()).ExecuteNonQuery(sqlExe_);
                        materialInfoFormChange();
                        MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("该物料名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch
            {
            }       
        }
         #endregion

        #region//退出编辑
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void s_MatName_TextChanged(object sender, EventArgs e)
        {
            s_PinYinCode.Text = Util.GetPYM(this.s_MatName.Text.Trim());//生成拼音码
        }

        private void Add_materialForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void s_Models_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string BasicSql = "select SysID, ModelName as 机型名,"
                + "ProdCtgrID as 分类,"
                + "ModelType as 机器类别,"
                + "ModelName1 as 机型别名1,ModelName2 as 机型别名2,"
                + "ModelGrade  as 机器等级,"
                + "Modelbrand as 品牌,"
                + "Ndxs as 难度系数 "
                + "from T_Model";

                string swhere = " where ModelName like '%{0}%'" +
                                 "or ModelName1 like '%{1}%'" +
                                 "or ModelName2 like '%{2}%'" +
                                 "or ProdCtgrID like '%{3}%'" +
                                 "or ModelType like '%{4}%'" +
                                 "or Modelbrand like '%{5}%'" +
                                 "or ModelGrade like '%{6}%'";
                swhere = string.Format(swhere, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);

                //显示列表
                DataTable dt = (new SqlDBConnect()).Get_Dt(BasicSql + swhere);
                FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                fr.unVisible_Column_index_ = 0;
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    if (fr.dr_.Cells["sysid"].Value == null) return;
                    if (fr.dr_.Cells["sysid"].Value.ToString().Trim() == "") return;
                    this.s_Models.Text = fr.dr_.Cells["机型名"].Value.ToString().Trim();
                }
            }
        }
    
    }
}
