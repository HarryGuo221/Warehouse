using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Base;
using Warehouse.DB;

namespace Warehouse.Stock
{
    public partial class EditMatRela : Form
    {
        private string Type;
        private string pMatId;
        private string pMatName;
        private int curRowIndex = 0;

        public EditMatRela()
        {
            InitializeComponent();
        }      

        public EditMatRela(string type, string pmatId, string pmatname)
        {
            InitializeComponent();
            this.Type = type;
            this.pMatId = pmatId;
            this.pMatName = pmatname;
        }

        public void initdataGridview()
        {
            DataTable dt;
            dt = (new MaterialDAO()).getMatRelaInfo(this.pMatId);
            (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);

            this.dataGridView1.ClearSelection();
            this.dataGridView1.Rows[this.curRowIndex].Selected = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region//输入数据有效性验证
            if (Util.ControlTextIsNUll(CmatnametextBox) == true)
            {
                MessageBox.Show("请选择添加子物料", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            //建立事务
            List<string> strsqls = new List<string>();
            InitFuncs inifuncs = new InitFuncs();
            if (this.Type == "add")
            {
                string insert_sql = inifuncs.Build_Insert_Sql(this.panelMatRela, "T_Mat_Rela");
                strsqls.Add(insert_sql);
                //判断数据是否已存在
                bool isExsit = (new DBUtil()).Is_Exist_Data("T_Mat_Rela", "ParentMatID", this.s_ParentMatID.Text.ToString().Trim(), "ChildMatID", this.s_ChildMatID.Text.ToString().Trim());
                //插入一条
                if (isExsit == false)
                {
                    (new SqlDBConnect()).Exec_Tansaction(strsqls);
                    MessageBox.Show("添加成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("该机型耗材对照记录已存在");
                initdataGridview();
            }
            if (this.Type == "edit")
            {
                string parentMatCode = this.s_ParentMatID.Text.ToString().Trim();
                string childMatCode = this.s_ChildMatID.Text.ToString().Trim();
                string swhere="where ParentMatID='" + parentMatCode + "' and ChildMatID='" + childMatCode + "'";
                string update_sql = inifuncs.Build_Update_Sql(this.panelMatRela, "T_Mat_Rela", swhere);
                (new SqlDBConnect()).ExecuteNonQuery(update_sql);
                initdataGridview();
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);           
            }
        }

        private void EditMatRela_Load(object sender, EventArgs e)
        {
            //限制值类型文本框的输入
            (new InitFuncs()).Num_limited(this.panelMatRela);

            initdataGridview();
            InitFuncs inifuncs = new InitFuncs();        
            this.s_ParentMatID.Text = this.pMatId;
            this.Pmatnametextbox.Text = this.pMatName;
            Pmatnametextbox.ReadOnly = true;
            CmatnametextBox.ReadOnly = true;
        }

        //单击某行初始化修改界面
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows[0].Cells["耗材选构件编号"].Value == null || this.dataGridView1.SelectedRows[0].Cells["耗材选构件编号"].Value.ToString().Trim() == "")
                return;
            CmatnametextBox.ReadOnly = false;
            this.Type = "edit";//将编辑类型切换至修改            
            s_ChildMatID.Text = this.dataGridView1.SelectedRows[0].Cells["耗材选构件编号"].Value.ToString().Trim();
            CmatnametextBox.Text = this.dataGridView1.SelectedRows[0].Cells["耗材选构件名称"].Value.ToString().Trim();
            InitFuncs inf = new InitFuncs();
            string select_matrela="select * from T_Mat_Rela where ParentMatID='"+s_ParentMatID.Text.Trim()+"' and ChildMatID='"+s_ChildMatID.Text.Trim()+"'";
            inf.ShowDatas(this.panelMatRela, select_matrela);
            CmatnametextBox.ReadOnly = true;

            this.curRowIndex = e.RowIndex;
        }

        #region//查找子物料
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            CmatnametextBox.Clear();
            WFilter wf = new WFilter(1, "MatName", true);
            wf.tableName = "T_MatInf";    //表名   
            wf.strSql = "select T_MatInf.MatID 物料编号,MatName 物料名称,Specifications 型号规格 ,Units 计量单位,"+
                        "Brand 品牌,ProductType 产品种类,ColorType 色彩,Speed 速度等级,Format 幅面,ynStopProduct 是否已停产,"+
                        "ConfigType 配置类型,purchaseprice 标准进货价 "+ "from T_MatInf";

            wf.s_items.Add("物料编号,MatID,C");
            wf.s_items.Add("物料名称,MatName,C");
            wf.s_items.Add("拼音编码,PinYinCode,C");
            wf.s_items.Add("品牌,Brand,C");
           
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                //插入
                List<string> sqls = new List<string>();
                DBUtil dbUtil = new DBUtil();
                string curpmatid = this.pMatId;

                foreach (string MatName in wf.Return_Items)
                {
                    string curcmatid = dbUtil.Get_Single_val("T_MatInf", "MatID", "MatName", MatName.Trim());
                    if (curcmatid == "")
                        continue;
                    //插入前判断存在性和与与主物料的排斥性
                    string strSqlSel = "select * from T_Mat_Rela where ParentMatId='{0}' and ChildMatId='{1}'";
                    strSqlSel = string.Format(strSqlSel, curpmatid, curcmatid);
                    bool isExit = dbUtil.yn_exist_data(strSqlSel);

                    if (isExit == true || curpmatid == curcmatid.Trim())
                        continue;

                    string strSql = "insert into T_Mat_Rela(ParentMatId,ChildMatId) values('{0}','{1}')";
                    strSql = string.Format(strSql, curpmatid, curcmatid);
                    sqls.Add(strSql);
                }
                (new SqlDBConnect()).Exec_Tansaction(sqls);

                initdataGridview();
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0)
                return;
            if (this.dataGridView1.SelectedRows[0].Cells["耗材选构件编号"].Value == null || this.dataGridView1.SelectedRows[0].Cells["耗材选构件编号"].Value.ToString().Trim() == "")
                return;
            {
                DialogResult dr = MessageBox.Show("确认删除选中信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK )
                {
                    for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                    {

                        string ChildMatID = this.dataGridView1.SelectedRows[0].Cells["耗材选构件编号"].Value.ToString().Trim();
                        (new MaterialDAO()).DeleteMatRela(this.pMatId,ChildMatID);
                        initdataGridview();
                    }
                    //清除输入框中的值
                    s_ChildMatID.Text = "";
                    CmatnametextBox.Text = "";
                    n_ContainsNumber.Text = "";
                    n_CopyNumber.Text = "";
                    s_Memo.Text = "";
                }
                else
                    return;
            }
         
        }

        private void EditMatRela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendKeys.Send("{Tab}");
            }
        }
    }
}
