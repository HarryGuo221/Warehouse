using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using grproLib;

namespace Warehouse
{
    public partial class WFilter : Form
    {
        public string tableName;  //数据库表名
        public string strSql = ""; //绑定DataGridView的Sql
        public List<string> s_items;  //显示内容，字段，类型        
        private List<string> s_field;
        private List<string> s_type;
        private List<string> s_caption;
        private Dictionary<string, string> s_field_caption;
                
        private int returnType = 0; // 需要返回的类型  *返回什么类型*        
        private string returnFieldName = ""; // 需要返回的字段名        
        private string return_Sql = "";
        /// <summary>
        /// 返回符合条件的sql语句
        /// </summary>
        public string Return_Sql
        {
          get { return return_Sql; }
          set { return_Sql = value; }
        }
        private List<string> return_Items = new List<string>();
        /// <summary>
        /// 返回的指定字段的值的集合
        /// </summary>
        public List<string> Return_Items
        {
          get { return return_Items; }
          set { return_Items = value; }
        }
        private Dictionary<string, Dictionary<string, string>> return_Fileds_Values = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        ///  返回选择条件（字段名，字段值）
        /// </summary>
        ///
        public Dictionary<string, Dictionary<string, string>> Return_Fileds_Values
        {
            get { return return_Fileds_Values; }
            set { return_Fileds_Values = value; }
        }
        private bool isClose = false;

        /// <summary>
        /// 是否关闭窗体
        /// </summary>
        public bool IsClose
        {
            get { return isClose; }
            set { isClose = value; }
        }
        private bool isClickOk = false;

        /// <summary>
        /// 是否点击了“确定”按钮
        /// </summary>
        public bool IsClickOk
        {
            get { return isClickOk; }
            set { isClickOk = value; }
        }
        public delegate void ClickOkChange();

        /// <summary>
        /// 用户信息改变 事件
        /// </summary>
        public event ClickOkChange clickOkChange;
        
        public WFilter(int returnType, string fieldName, bool isColse)
        {
            InitializeComponent();
            s_items = new List<string>();
            s_field = new List<string>();
            s_type = new List<string>();
            s_caption = new List<string>();
            s_field_caption = new Dictionary<string, string>();
            this.isClose = isColse;

            this.returnType = returnType;
            this.returnFieldName = fieldName;

            this.Height = 310;
        }

        private void init_show()
        {
            lb_Items.Items.Clear();          
            
            string stmp, scap, sfil, slx;
            
            for (int i = 0; i < s_items.Count ; i++)
            {
                stmp = s_items[i].Trim();
                
                scap = stmp.Substring(0,stmp.IndexOf(","));  //字段描述
                stmp = stmp.Substring(stmp.IndexOf(',') + 1, stmp.Length - stmp.IndexOf(',') - 1);
                sfil = stmp.Substring(0, stmp.IndexOf(','));  //字段名
                slx = stmp.Substring(stmp.IndexOf(',') + 1, stmp.Length - stmp.IndexOf(',') - 1);  //字段类型
                lb_Items.Items.Add(scap);
                s_field.Add(sfil);
                s_caption.Add(scap);
                s_type.Add(slx);
                s_field_caption.Add(sfil, scap);//英文字段-中文字段
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//重置条件
            lb_sql.Items.Clear();
            lb_where.Items.Clear();
            this.Height = 310;

            this.return_Fileds_Values.Clear();
        }        

        private void lb_Items_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (lb_Items.SelectedIndex!=-1)
            {
                this.tb1.Text = "";
                cb_opa.Items.Clear();
               lb_ts.Visible=true;
               lb_field.Visible=true;
               //lb_field.Text = s_field[lb_Items.SelectedIndex];
               lb_field.Text = s_caption[lb_Items.SelectedIndex];
               if (s_type[lb_Items.SelectedIndex].Trim()=="C")
               {
                   cb_opa.Items.Add("包含");
                   cb_opa.Items.Add("等于");
               }
               else if (s_type[lb_Items.SelectedIndex].Trim()=="N")
               {
                    cb_opa.Items.Add(">");
                    cb_opa.Items.Add(">=");
                    cb_opa.Items.Add("<");
                    cb_opa.Items.Add("<=");
                    cb_opa.Items.Add("=");
                    cb_opa.Items.Add("!=");
                    cb_opa.Items.Add("范围");
               }
               cb_opa.SelectedIndex = 0;

                //如果选择的是“日期”
               if (lb_Items.SelectedItem.ToString().Trim().Contains("日期"))
                   this.tb1.Text = DateTime.Now.ToString("yyy-MM-dd");
                   //this.tb1.Text = string.Format("{0:u}", DateTime.Now);
                   //this.tb1.Text = DateTime.Now.Year.ToString().Trim() + "-" + DateTime.Now.Month.ToString().Trim() + "-" + DateTime.Now.Day.ToString().Trim();
               //如果选择的是“工作年月”
               if (lb_Items.SelectedItem.ToString().Trim().Contains("工作年月"))
                   this.tb1.Text = DateTime.Now.ToString("yyyMM");
                   //this.tb1.Text = DateTime.Now.Year.ToString().Trim() + DateTime.Now.Month.ToString().Trim();
            }
        }

        private void WFilter_Load(object sender, EventArgs e)
        {
            init_show();
            if (this.lb_Items.Items.Count > 0)
                this.lb_Items.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lb_Items.SelectedIndex==-1) 
                return;

            //返回选择条件（字段名，字段值）       
            Dictionary<string, string> opa_value = new Dictionary<string, string>();
            string tb1Text = tb1.Text.Trim();
            string opa = cb_opa.Text.Trim();
            if (tb2.Visible == true && tb2.Text.Trim() != "")
                tb1Text = tb1Text + "~" + tb2.Text.Trim();
            opa_value.Add(opa, tb1Text);

            if (!return_Fileds_Values.Keys.Contains(lb_Items.SelectedItem.ToString().Trim()))
                return_Fileds_Values.Add(lb_Items.SelectedItem.ToString().Trim(), opa_value);

            string s_fil=s_field[lb_Items.SelectedIndex];
            string s_cap = s_caption[lb_Items.SelectedIndex];
            string s_opa="";
            string s_val="";
            string s_txt = "";
            string s_tj="";
            if (tb1.Text.Trim()=="") return;
            if (((cb_opa.Text) == "范围") && (tb2.Text.Trim() == "")) return;

            if ((cb_opa.Text)=="包含")
            {
                s_opa=" like ";
                s_val="'%"+tb1.Text.Trim()+"%'";
                s_txt = tb1.Text.Trim();
            }
            else if ((cb_opa.Text)=="等于")
            {
                s_opa=" like ";
                s_val= "'" + tb1.Text.Trim() + "'";
                s_txt = tb1.Text.Trim();
            }
            else if ((cb_opa.Text)=="范围")
            {
               s_opa=" between ";
               s_val="'"+tb1.Text.Trim()+"' and '"+tb2.Text.Trim()+"'";
               s_txt = tb1.Text.Trim() + " ~ " + tb2.Text.Trim();
            }
            else
            {
               s_opa=cb_opa.Text.Trim();
               s_val="'"+tb1.Text.Trim()+"'";
               s_txt = tb1.Text.Trim();
            }

            if (lb_sql.Items.Count<=0)
            {
                lb_sql.Items.Add("(" + s_fil + " " + s_opa + " " + s_val + ")");
                lb_where.Items.Add("(" + s_cap + " " + cb_opa.Text + " " + s_txt + ")");
            }
            else
            {
                lb_sql.Items.Add(" and (" + s_fil + " " + s_opa + " " + s_val + ")") ;
                lb_where.Items.Add(" 并且 (" + s_cap + " " + cb_opa.Text + " " + s_txt + ")");
            }
            tb1.Text = "";
            tb2.Text = "";

           
        }        

        private void cb_opa_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cb_opa.Text.Trim() == "范围")
            {
                tb2.Visible = true;
            }
            else
                tb2.Visible = false;
        }

        private void 删除选中行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cur_index = lb_where.SelectedIndex;
            lb_where.Items.RemoveAt(cur_index);
            lb_sql.Items.RemoveAt(cur_index);
            this.Refresh();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //以中文字段名显示
            //Return_Sql = "select * from " + tableName + " where "; //英文字段名           
            //for (int i = 0; i < lb_sql.Items.Count; i++)
            //{               
            //    Return_Sql += " " + lb_sql.Items[i].ToString().Trim();
            //}
            Return_Sql = "";
            if (lb_sql.Items.Count > 0)
            {
                if (this.strSql.Contains(" where "))
                    Return_Sql += this.strSql + " and ";
                else
                    Return_Sql += this.strSql + " where ";
                for (int i = 0; i < lb_sql.Items.Count; i++)
                {
                    Return_Sql += " " + lb_sql.Items[i].ToString().Trim();
                }
            }
            else
                Return_Sql = this.strSql;

            try
            {
                SqlDBConnect db = new SqlDBConnect();
                DataTable dt = new DataTable();
                dt = db.Get_Dt(Return_Sql);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("没有符合条件的记录存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }               
                InitFuncs initFuncs = new InitFuncs();
                initFuncs.InitDataGridView(this.dataGridView, dt);
                this.Height = 450;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n" + Return_Sql, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// 确定
        /// </summary>       
        private void button3_Click(object sender, EventArgs e)
        {
            this.isClickOk = true;//点击了“确定”按钮
            
            Return_Sql = "";
            if (lb_sql.Items.Count > 0)
            {
                if (this.strSql.Contains(" where "))
                    Return_Sql += this.strSql + " and ";
                else
                    Return_Sql += this.strSql + " where "; //加SQL搜索条件
                
                for (int i = 0; i < lb_sql.Items.Count; i++)
                {
                    Return_Sql += " " + lb_sql.Items[i].ToString().Trim();
                }
            }
            else
                Return_Sql = this.strSql;//z

            this.return_Items.Clear();
            if (this.dataGridView.SelectedRows.Count > 0)
            {
                for (int i = 0; i < this.dataGridView.SelectedRows.Count; i++)
                {
                    this.return_Items.Add(this.dataGridView.SelectedRows[i].Cells[this.s_field_caption[this.returnFieldName].Trim()].Value.ToString().Trim());
                }
                if (this.isClose == true)
                {
                    this.DialogResult = DialogResult.OK;                    
                }
                
            }
            else
            {
                //DialogResult dialogResult = MessageBox.Show("没有数据或没有选中记录，是否继续？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //if (dialogResult == DialogResult.No)
                //{
                //    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //    this.Close();
                //}
                //else
                //    return ;
                                
                if (this.isClose == true)
                    this.DialogResult = DialogResult.OK;

                this.Close();
            }
            if (this.clickOkChange !=null)
                clickOkChange(); //激活代理事件，在调用窗体中处理 
        }
      
        /// <summary>
        /// 返回选中的一条记录里的相应字段值，或是返回所有记录里的相应字段值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.returnType == 0)
            {
                if ((sender as DataGridView).SelectedRows.Count > 0)
                {
                    if (this.s_field_caption.Keys.Contains(this.returnFieldName))
                        this.return_Items.Add((sender as DataGridView).SelectedRows[0].Cells[this.s_field_caption[this.returnFieldName].Trim()].Value.ToString().Trim());
                }
            }
            else if (this.returnType == 1)
            {
                string strSql = this.return_Sql; 
                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                    return;

                foreach (DataRow dr in dt.Rows)
                {
                    this.return_Items.Add(this.s_field_caption[this.returnFieldName].Trim());
                }
            }
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void WFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button3.Enabled = true;
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
