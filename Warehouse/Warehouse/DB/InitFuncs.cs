using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.DAO;
using Warehouse.Base;
using System.Drawing;

namespace Warehouse.DB
{
    class InitFuncs
    {
        public InitFuncs()
        {            
            //TODO: 在此处添加构造函数逻辑
            
        }
        #region 用一个表中的某一字段 初始化ComboBox
        /// <summary>
        /// 用一个表中的某一字段 初始化ComboBox
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="tableName">数据表</param>
        /// <param name="d_zdm">数据表中的字段</param>
        public void InitComboBox(ComboBox comboBox, string tableName, string d_zdm)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("--请选择--");

            string strSql = "select distinct {0} from {1}";
            strSql = string.Format(strSql, d_zdm, tableName);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows)
            {
                comboBox.Items.Add(dr[d_zdm].ToString().Trim());
            }

            comboBox.SelectedIndex = 0; 
        }
        /// <summary>
        /// 根据传入的sql语句用某一字段初始化ComboBox
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="d_zdm"></param>
        /// <param name="strSql"></param>
        /// <param name="type"></param>
        public void InitComboBox(ComboBox comboBox, string d_zdm, string strSql, int type)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("--请选择--");            
            
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dt.Rows)
            {
                comboBox.Items.Add(dr[d_zdm].ToString().Trim());
            }

            comboBox.SelectedIndex = 0;
        }
        #endregion

        #region 初始化ComboBox(将码表T_SelItems中的数据绑定到ComboBox控件)
        /// <summary>
        /// 初始化ComboBox(将码表T_SelItems中的数据绑定到ComboBox控件)
        /// </summary>
        /// <param name="comBox"></param>
        /// <param name="lx_"></param>
        public void InitComboBox(ComboBox comBox, string itemVal)
        {
            comBox.Items.Clear();
            comBox.Items.Add("--请选择--");

            string sql_ = "select distinct ItemVal from T_SelItems where ItemType='" + itemVal + "'";
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(sql_);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                    comBox.Items.Add(dr["ItemVal"].ToString().Trim());
            }
            comBox.Items.Add("");
            comBox.SelectedIndex = 0;            
        }
        #endregion

        #region 初始化CheckBoxList（将码表中的数据绑定到CheckBoxList控件）
        /// <summary>
        /// 初始化CheckBoxList（将码表中的数据绑定到CheckBoxList控件）
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="lx_"></param>
        public void InitCheckedListBox(CheckedListBox clb, string itemVal)
        {
            clb.Items.Clear();

            string sql_ = "select ItemVal from T_SelItems where ItemType='" + itemVal + "'";
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(sql_);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                    clb.Items.Add(dr["ItemVal"].ToString().Trim());
            }
            
        }
        #endregion               
        
        /*** 程序中，依照Panel中控件的“Name属性”的一定命名规则，来识别“字段”、“数据类型”的对应规则：
        s_字段名：字符型或日期字段
        n_字段名：数值型字段
        ***/
        #region 生成将Panel中已绑定字段控件中的数据插入到表tab_中的SQL语句，返回SQL语句        
        /// <summary>
        /// 生成将Panel中已绑定字段控件中的数据插入到表tab_中的SQL语句，返回SQL语句
        /// 如：用户名的TextBox，Name属性命名为s_name
        /// </summary>
        /// <param name="Pa"></param>
        /// <param name="tab_">要插入的数据表_表名</param>
        /// <returns>返回相应SQL语句</returns>
        public string Build_Insert_Sql(Panel Pa, string tab_)
        {
            string sql_ = "insert into " + tab_;
            string fiels = "", vals = "";
            string stmp = "", fiel_ = "", val_ = "", lx_ = "";
            foreach (Control o in Pa.Controls)
            {
                string str = "";
                if (o is Label)
                    str = o.Text.Trim();
                fiel_ = "";

                if (o is TextBox)  //处理TextBox
                {
                    stmp = ((TextBox)o).Name.Trim().ToLower();
                    lx_ = stmp.Substring(0, 2);
                    fiel_ = stmp.Substring(2, stmp.Length - 2);
                    if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                    if (((TextBox)o).Text.Trim().Length == 0)
                        val_ = "null";
                    else
                    {
                        if (lx_ == "s_")
                        {
                            if (fiel_ == "password")//如果是密码框的话，加密存储
                            {
                                val_ = "'" + Util.GetMD5str(((TextBox)o).Text.Trim()) + "'";
                            }
                            else
                                val_ = "'" + ((TextBox)o).Text.Trim() + "'";
                        }
                        else
                        {
                            val_ = ((TextBox)o).Text.Trim();
                        }
                    }
                }

                if (o is ComboBox)  //处理ComboBox
                {
                    stmp = ((ComboBox)o).Name.Trim().ToLower();
                    lx_ = stmp.Substring(0, 2);
                    fiel_ = stmp.Substring(2, stmp.Length - 2);
                    if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                    if (((ComboBox)o).SelectedIndex == 0)
                    {
                        val_ = "";
                        fiel_ = "";   
                    }
                    else
                        val_ = ((ComboBox)o).Text.Trim();

                    if (lx_ == "s_")
                        val_ = "'" + val_ + "'";

                }
                if (o is CheckBox)  //只处理存储为true,false的情况
                {
                    stmp = ((CheckBox)o).Name.Trim().ToLower();
                    lx_ = stmp.Substring(0, 2);
                    fiel_ = stmp.Substring(2, stmp.Length - 2);
                    if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                    if (((CheckBox)o).Checked)
                        val_ = "true";
                    else
                        val_ = "false";

                    if (lx_ == "s_")
                        val_ = "'" + val_ + "'";
                }                 
                if (o is DateTimePicker)
                {
                    stmp = ((DateTimePicker)o).Name.Trim().ToLower();
                    lx_ = stmp.Substring(0, 2);
                    if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                    fiel_ = stmp.Substring(2, stmp.Length - 2);
                    val_ = "'" + ((DateTimePicker)o).Value + "'"; 
                }

                if (o is RadioButton)//////
                {
                    if (((RadioButton)o).Checked == true)
                    {
                        stmp = ((RadioButton)o).Name.Trim().ToLower();
                        lx_ = stmp.Substring(0, 2);
                        if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                        fiel_ = stmp.Substring(2, stmp.Length - 2);
                        val_ = "'" + ((RadioButton)o).Text.Trim() + "'";
                    }
                 }

                //构造Sql局部
                if (fiel_ != "")
                {
                    if (fiels.Length == 0)
                        fiels = fiels + fiel_;
                    else
                        fiels = fiels + "," + fiel_;

                    if (vals.Length == 0)
                        vals = vals + val_;
                    else
                        vals = vals + "," + val_;
                }
            }

            sql_ = sql_ + "(" + fiels + ") values(" + vals + ")";
            return sql_;
        }
        
        #endregion

        #region 生成将Panel中已绑定字段控件中的数据更新到表tab_中的SQL语句，返回SQL语句
        /// <summary>
        /// 生成将Panel中已绑定字段控件中的数据更新到表tab_中的SQL语句，返回SQL语句
        /// </summary>
        /// <param name="Pa"></param>
        /// <param name="tab_">要更新的数据表_表名</param>
        /// //swhere为Where条件
        public string Build_Update_Sql(Panel Pa, string tab_, string swhere)
        {
            string sql_ = "";
            string sqltmp_ = "", stmp = "", fiel_ = "", val_ = "", lx_ = "";

            foreach (Control o in Pa.Controls)
            {
                fiel_ = "";

                if (o.Enabled == false) continue;  //如果Enabled=false 则不处理

                if (o is TextBox)   //处理TextBox
                {
                    stmp = ((TextBox)o).Name.Trim().ToLower();
                    
                    lx_ = stmp.Substring(0, 2);
                    fiel_ = stmp.Substring(2, stmp.Length - 2);
                    if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理
                    
                    if (((TextBox)o).Text.Trim().Length == 0)
                        val_ = "null";
                    else
                    {
                        if (lx_ == "s_")
                        {
                            if (fiel_ == "password")//如果是密码框的话，加密存储
                            {
                                val_ = "'" + Util.GetMD5str(((TextBox)o).Text.Trim()) + "'";
                            }
                            else
                            val_ = "'" + ((TextBox)o).Text.Trim() + "'";
                        }
                        else
                        {
                            val_ = ((TextBox)o).Text.Trim();
                        }
                    }
                }
                if (o is ComboBox)  //处理ComboBox
                {
                    stmp = ((ComboBox)o).Name.Trim().ToLower();
                    lx_ = stmp.Substring(0, 2);
                    fiel_ = stmp.Substring(2, stmp.Length - 2);
                    if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                    if (((ComboBox)o).SelectedIndex == 0)
                    {
                        val_ = "";
                        //fiel_ = "";   //bug 2011-08-31改
                    }
                    else
                        val_ = ((ComboBox)o).Text.Trim();//

                    if (lx_ == "s_")
                        val_ = "'" + val_ + "'";

                }
                if (o is CheckBox)  //只处理存储为true,false的情况
                {
                    stmp = ((CheckBox)o).Name.Trim().ToLower();
                    lx_ = stmp.Substring(0, 2);
                    fiel_ = stmp.Substring(2, stmp.Length - 2);
                    if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                    if (((CheckBox)o).Checked)
                        val_ = "true";
                    else
                        val_ = "false";

                    if (lx_ == "s_")
                        val_ = "'" + val_ + "'";
                }
                if (o is DateTimePicker)  //处理DateTimePicker
                {
                    stmp = ((DateTimePicker)o).Name.Trim().ToLower();
                    lx_ = stmp.Substring(0, 2);
                    fiel_ = stmp.Substring(2, stmp.Length - 2);
                    if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                    val_ = ((DateTimePicker)o).Value.ToString();//

                    if (lx_ == "s_")
                        val_ = "'" + val_ + "'";

                }
                //构造Sql局部
                if (fiel_ != "")
                {
                    if (sqltmp_ != "")
                        sqltmp_ = sqltmp_ + "," + fiel_ + "=" + val_;
                    else
                        sqltmp_ = fiel_ + "=" + val_;
                }

            }
            if (sqltmp_ != "")
                sql_ = "update " + tab_ + " set " + sqltmp_ + " " + swhere;
            else
                sql_ = "";
            return sql_;
        }
        #endregion        

        #region 将selectsql_查询语句执行结果，绑定到pa中Name属性指定了值的控件上
        /// <summary>
        /// 将selectsql_查询语句执行结果，绑定到pa中Name属性指定了值的控件上
        /// </summary>
        /// <param name="Pa"></param>
        /// <param name="selectsql_"></param>
        public void ShowDatas(Panel Pa, string selectsql_)
        {
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(selectsql_);
            string filename_ = "", stmp = "", lx_,str="";
            string fiel_ = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows) //行
                {
                    for (int i = 0; i <= dt.Columns.Count - 1; i++) //列
                    {
                        filename_ = dt.Columns[i].ColumnName.ToLower().Trim();
                        foreach (Control o in Pa.Controls)
                        {
                            if (o is TextBox)
                            {
                                stmp = ((TextBox)o).Name.Trim().ToLower();
                                lx_ = stmp.Substring(0, 2);
                                fiel_ = stmp.Substring(2, stmp.Length - 2).Trim();
                                if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                                if (fiel_.ToLower() == filename_)
                                {
                                    if (dr[filename_] is DBNull)
                                        ((TextBox)o).Text = "";
                                    else
                                        ((TextBox)o).Text = dr[filename_].ToString().Trim();
                                    break;
                                }
                            }
                            if (o is ComboBox)
                            {
                                stmp = ((ComboBox)o).Name.Trim().ToLower();
                                lx_ = stmp.Substring(0, 2);
                                fiel_ = stmp.Substring(2, stmp.Length - 2).Trim();
                                //if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                                if (fiel_.ToLower() == filename_)
                                {
                                    if (dr[filename_] is DBNull)
                                        ((ComboBox)o).Text = "";
                                    else
                                    {
                                        if (((ComboBox)o).DropDownStyle == ComboBoxStyle.DropDownList)
                                        {
                                            //如果选项中无，则加一个
                                            if (((ComboBox)o).Items.IndexOf(dr[filename_].ToString().Trim()) == -1)
                                                ((ComboBox)o).Items.Add(dr[filename_].ToString().Trim());
                                            
                                            ((ComboBox)o).SelectedIndex = ((ComboBox)o).Items.IndexOf(dr[filename_].ToString().Trim());
                                        }
                                        else
                                            ((ComboBox)o).Text = dr[filename_].ToString().Trim();
                                    }
                                        break;
                                }
                            }
                            if (o is DateTimePicker )
                            {
                                stmp = ((DateTimePicker)o).Name.Trim().ToLower();
                                lx_ = stmp.Substring(0, 2);
                                fiel_ = stmp.Substring(2, stmp.Length - 2).Trim();
                                if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                                if (fiel_.ToLower() == filename_)
                                {
                                    if (dr[filename_] is DBNull)
                                        //((DateTimePicker)o).Value = Convert.ToDateTime(DBNull.Value);  //error
                                        ((DateTimePicker)o).Value = Convert.ToDateTime("1900-01-01 00:00:01");
                                    else
                                        ((DateTimePicker)o).Value = Convert.ToDateTime(dr[filename_].ToString().Trim());
                                    break;
                                }
                            }
                            if (o is CheckBox) //只处理存储为true,false的情况
                            {
                                stmp = ((CheckBox)o).Name.Trim().ToLower();
                                lx_ = stmp.Substring(0, 2);
                                fiel_ = stmp.Substring(2, stmp.Length - 2).Trim();
                                if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                                if (fiel_.ToLower() == filename_)
                                {                                    
                                    if (dr[filename_].ToString().Trim() == "True")
                                        ((CheckBox)o).Checked = true;
                                    else if (dr[filename_].ToString().Trim() == "False")
                                        ((CheckBox)o).Checked = false;
                                    break;
                                }
                            }

                        }  //end of foreach
                    } //end of for
                }//end of foreach
            }
            
        }
        #endregion 

      
      

        #region 清空Panel中控件中的输入值
        /// <summary>
        /// 清空Panel中控件中的输入值
        /// </summary>
        /// <param name="Pa"></param>
        public void ClearData(Panel Pa)
        {
            foreach (Control o in Pa.Controls)
            {
                if (o is TextBox)
                {
                    (o as TextBox).Text = "";
                }
                if (o is ComboBox)
                {
                    (o as ComboBox).SelectedIndex = 0;
                }
                if (o is CheckBox)
                {
                    (o as CheckBox).Checked = false;
                }
            }
        }
        #endregion

        #region 验证字符串是否是一个合法的日期
        public bool is_day(string stxt, char sfgf)
        {
            if (stxt.Trim().Length != 10)
                return false;
            if ((stxt.Trim().Substring(4, 1) != sfgf.ToString()) || (stxt.Trim().Substring(7, 1) != sfgf.ToString()))
                return false;
            string[] str = stxt.Trim().Split(sfgf);
            try
            {
                if (Convert.ToInt16(str[0]) < 1900)
                    return false;
                if ((Convert.ToInt16(str[1]) <= 0) || (Convert.ToInt16(str[1]) > 12))
                    return false;
                if ((Convert.ToInt16(str[2]) <= 0) || (Convert.ToInt16(str[1]) > 31))
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 去掉panel中控件的边框
        /// <summary>
        /// 去掉panel中控件的边框
        /// </summary>
        /// <param name="Pa"></param>
        public void FormatControlNoBorder(Panel Pa)
        {
            foreach (Control o in Pa.Controls)
            {
                if (o is TextBox)
                {
                    ((TextBox)o).BorderStyle = BorderStyle.None;
                }
                                
            }
        }
        #endregion

        #region 使panel中控件成为只读
        /// <summary>
        /// 使panel中控件成为只读
        /// </summary>
        /// <param name="Pa"></param>
        public void FormatControlReadonly(Panel Pa)
        {
            foreach (Control o in Pa.Controls)
            {
                if (o is TextBox)
                {
                    ((TextBox)o).ReadOnly = true;
                }
            }
        }
        #endregion

        #region 初始化DataGridView
        /// <summary>
        /// 初始化显示数据表
        /// </summary>
        public void InitDataGridView(DataGridView dataGridView, DataTable dt)
        {            
            dataGridView.Columns.Clear();
            dataGridView.DataSource = null;
            dataGridView.DataSource = dt.DefaultView;
            dataGridView.AutoGenerateColumns = true;            
            dataGridView.ReadOnly = true;

            //初始化表头
            InitDataGridViewHeader(dataGridView);
        }
        /// <summary>
        /// 初始化表头
        /// </summary>
        public void InitDataGridViewHeader(DataGridView dataGridView)
        {
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //列头宽度      
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; //.ColumnHeader;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; //标题居中显示
            }
            //行头宽度         
            dataGridView.RowHeadersWidth = 25;
        }
        #endregion

        #region//限制文本框只能输入数字
        public void Num_limited(Panel pa)
        {
            foreach (object o in pa.Controls)
            {
                if (o is TextBox)
                {
                    if (((TextBox)o).Name.Substring(0, 1) == "n")
                        ((TextBox)o).KeyPress += new KeyPressEventHandler(tb_KeyPress);
                }
                else
                    continue;
            }
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////e.KeyChar的值可以与十进制或十六进制的值对应,但显示出来的e.KeyChar.Tostring()为非ASCALL码值 
            ////更改过得方法可以使用键盘上的字符来操作,eg:Ctrl+C
            ////0为十进制，(char)0 表ASCALL码的NULL,等价与十六进制的00
            //if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键,0x20为十六进制的空格表示,或换成十进制的32亦可
            //if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数,同上0x2D(为'-'),转化为十进制为45
            //if (e.KeyChar > 0x20)//其他小于32的字符(符号)不处理，即不触发e.Handel=True
            //{
            //    MessageBox.Show(e.KeyChar.ToString());
            //    try
            //    {
            //        Convert.ToDouble(((TextBox)sender).Text + e.KeyChar.ToString());
            //    }
            //    catch
            //    {
            //        e.KeyChar = (char)0;   //处理非法字符
            //    }
            //}
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 46 && e.KeyChar != 8)
            //if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }
        #endregion

        #region 控制TextBox文本框输入，全部为数字认为输入的是 UserId,否则认为输入的是 UserName
        private TextBox CurTextBox;
        /// <summary>
        /// 控制TextBox文本框输入，全部为数字认为输入的是 UserId,否则认为输入的是 UserName
        /// </summary>
        /// <param name="textBox"></param>
        public void UserTextBoxInput(TextBox textBox)
        {
            string textBoxText = textBox.Text.Trim();
            if (textBoxText == "")
                return;
            DBUtil dbUtil = new DBUtil();
            if (Util.StringIsNum(textBoxText)) //字符串中全部是数字
            {
                //认为TextBox中输入的是 UserId
                string strSql = "select * from T_users where UserId='" + textBoxText + "'";
                bool isExist = dbUtil.yn_exist_data(strSql);
                if (isExist == false)
                {
                    MessageBox.Show("该用户编号不存在", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox.Text = "";
                    return;
                }
                else
                {
                    textBox.Text = (new DBUtil()).Get_Single_val("T_users", "UserName", "UserId", textBoxText);
                }
            }
            else
            {
                //认为TextBox中输入的是 UserName
                string ZJM = Util.ChineseCharacterToLetter(textBoxText);
                string strSql = "select UserName from T_users where UserNameZJM like '" + ZJM + "%'";
                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("该用户不存在", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox.Text = "";
                    return;
                }

                ContextMenuStrip cMenuStrip = new ContextMenuStrip();               
                foreach (DataRow dr in dt.Rows)
                {
                    cMenuStrip.Items.Add(dr["UserName"].ToString().Trim());
                    cMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(cMenuStrip_ItemClicked);
                }
                Point point = new Point(textBox.Left, textBox.Bottom);
                cMenuStrip.Show(textBox, point);

                this.CurTextBox = textBox;

                //验证TextBox值是否存在
                string strSql_ = "select * from T_users where UserName= '" + textBox.Text.Trim() + "'";
                bool isExist = dbUtil.yn_exist_data(strSql_);
                if (isExist == false)
                {
                    textBox.Text = "";
                }
            }
        }

        void cMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.CurTextBox.Text = e.ClickedItem.Text.Trim();
        }
        #endregion

        #region 模糊查找到需要的记录，并赋值给显示控件
        /// <summary>
        /// 模糊查找到需要的记录，并赋值给显示控件
        /// </summary>
        /// <param name="control">传入的显示信息的控件</param>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">希望得到值的字段</param>
        /// <param name="findFields">希望在哪些字段中模糊查找</param>
        /// <param name="displayFields">在显示界面中现实的字段</param>
        public static void FindInfoToControl(Control control, string tableName, string fieldName, Dictionary<string, string> findFields, Dictionary<string, string> displayFields)
        {
            string controlText = control.Text.ToString().Trim();
            if (controlText == "" || displayFields.Count <= 0)
                return;

            string strSql = "select ";            
            foreach (string field in displayFields.Keys)
            {
                strSql += field.Trim() + " as " + displayFields[field].Trim() + ",";
            }

            strSql = strSql.Remove(strSql.Length - 1); //删除最后一个逗号
            strSql += " from " + tableName + " where ";

            foreach (string field in findFields.Keys)
            {
                if (findFields[field].Trim() == "s")
                    strSql += field + " like '%" + controlText + "%' or ";
                else if (findFields[field].Trim() == "n")
                    strSql += field + " like %" + controlText + "% or ";
            }
            strSql = strSql.Remove(strSql.Length - 3);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("查找的信息不存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.Text = "";
                return;
            }
            if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
            {
                control.Text = dt.Rows[0][displayFields[fieldName].Trim()].ToString().Trim();
            }
            else
            {
                FilterInfoForm form = new FilterInfoForm(control, dt, fieldName);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
            }
        }
        /// <summary>
        /// 根据传入的SQl模糊查找到需要的记录
        /// </summary>
        /// <param name="control">传入的显示信息的控件</param>
        /// <param name="strSql">传入的SQL</param>
        /// <param name="fieldName">希望得到值的字段</param>
        public static void FindInfoToControl(Control control, string strSql, string fieldName)
        {
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("查找的信息不存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                control.Text = "";
                return;
            }
            if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
            {
                control.Text = dt.Rows[0][fieldName].ToString().Trim();
            }
            else
            {
                FilterInfoForm form = new FilterInfoForm(control, dt, fieldName);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
            }
        }
        #endregion

        //验证控件中的值是否，是某个表中，某一列的值
        public static bool isRightValue(Control control,string whichtable,string whichfield)
        {
            string sql_="select "+whichfield+" from "+whichtable
                +" where "+whichfield+"='"+control.Text.Trim()+"'";
            return (new DBUtil()).yn_exist_data(sql_);
        }

        //验证控件中的值是否，是某个表中，某一列的值
        public static bool isRightValue(string StrTxt,string whichtable,string whichfield)
        {
            string sql_="select "+whichfield+" from "+whichtable
                +" where "+whichfield+"='"+StrTxt.Trim()+"'";
            return (new DBUtil()).yn_exist_data(sql_);
        }

    }
}