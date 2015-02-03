using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.DAO;
using Warehouse.Base;

namespace Warehouse.Stock
{
    public partial class MatSplitCForm : Form
    {
        private int curRowIndex;

        public MatSplitCForm()
        {
            InitializeComponent();
        }
        public delegate void MatSplitChildFormChange();
        /// <summary>
        /// 零件信息改变 事件
        /// </summary>
        public event MatSplitChildFormChange matSplitChildFormChange;
                
        private string strReceiptId;//拆件单据编号
        private string strMainMatID;//被拆物料编号
        private string ManufacCode;//被拆物料制造编号
        private string Type;

        public MatSplitCForm(string type, string strReceiptId, string strMainMatID, string manufacCode)
        {
            InitializeComponent();            
            this.strReceiptId = strReceiptId;
            this.strMainMatID = strMainMatID;
            this.ManufacCode = manufacCode;
            this.Type = type;
        }

       
        private void InitDataGridView()
        {
            DataTable dt = MatSplitChildDAO.GetDatasByReceiptId(this.strReceiptId);
            (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);

            this.dataGridView1.ClearSelection();
            this.dataGridView1.Rows[curRowIndex].Selected = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region 验证
            if (this.comboBoxMatType.SelectedIndex == 0 || this.comboBoxMatType.Text.Trim() == "")
            {
                MessageBox.Show("请选择零件类型！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.textBoxChildMatName.Text.Trim() == "")
            {
                MessageBox.Show("请输入零件名称！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.s_ChildManuCode.Text.Trim() == "")
            {
                MessageBox.Show("请输入零件制造编号！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion

            DBUtil dbUtil = new DBUtil();
            string sql = "";
            string sql_ = "";
            bool isExist=false;        
            if (this.Type == "add")
            {
                //构造添加sql
                this.s_ChildMatID.Text = dbUtil.Get_Single_val("T_MatInf", "MatID", "MatName", this.textBoxChildMatName.Text.Trim());
                this.n_ChildMatType.Text = (Util.GetMatType(this.comboBoxMatType.Text.Trim())).ToString();
                sql = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_MatSplit_Child");
                //构造判断sql
                sql_ = "select * from T_MatSplit_Child where ReceiptId='{0}' and ChildMatID='{1}'";
                sql_ = string.Format(sql_, this.strReceiptId, this.s_ChildMatID.Text.ToString().Trim());             
            }
            else if (this.Type == "edit")
            {
                //构造更新sql
                string strMatChildName = ""; 
                if (this.dataGridView1.SelectedRows[0].Cells["零件"].Value != null)
                    strMatChildName = this.dataGridView1.SelectedRows[0].Cells["零件"].Value.ToString().Trim();
                string ChildMatID = dbUtil.Get_Single_val("T_MatInf", "MatID", "MatName", strMatChildName);
                string strWhere = "where ReceiptId='{0}' and ChildMatID='{1}'";
                strWhere = string.Format(strWhere, this.s_ReceiptId.Text.Trim(), ChildMatID);
                sql = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_MatSplit_Child", strWhere);
                //构造判断sql
                string select_sysid = "select sysid from T_MatSplit_Child where ReceiptId='{0}' and ChildMatID='{1}'";
                select_sysid = string.Format(select_sysid, this.strReceiptId, this.s_ChildMatID.Text.ToString().Trim());
                string sysid = dbUtil.Get_Single_val(select_sysid);
                sql_ = "select ChildMatID from T_MatSplit_Child where LTRIM(RTRIM(sysid)) not like {0} and LTRIM(RTRIM(ChildMatID)) like '{1}' and LTRIM(RTRIM(ReceiptId)) like '{2}'";
                sql_ = string.Format(sql_, sysid, this.s_ChildMatID.Text.Trim(),this.strReceiptId);               
            }
            isExist=dbUtil.yn_exist_data(sql_);
            if (isExist == false)
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql);
                InitDataGridView();
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("该零件信息已存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                    
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void MatSplitChildForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            matSplitChildFormChange(); //激活代理事件，在MatSplitForm中处理
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {           
            //判断是否选择空项
            if (this.dataGridView1.SelectedRows[0].Cells["拆件单据编号"].Value == null)
                return;
            this.Type = "edit";//切换编辑模式
            this.curRowIndex = e.RowIndex;//获取当前所在行数

            //生成给控件赋值的sql
            string show_sql = "select * from T_MatSplit_Child where ReceiptId='{0}' and ChildMatID='{1}'";
            string receiptid = this.dataGridView1.SelectedRows[0].Cells["拆件单据编号"].Value.ToString().Trim();
            string childmat=this.dataGridView1.SelectedRows[0].Cells["零件"].Value.ToString().Trim();
            string ChildMatID = (new DBUtil()).Get_Single_val("t_matinf", "MatID", "matname", childmat);
            show_sql = string.Format(show_sql, receiptid, ChildMatID);
           
            (new InitFuncs()).ShowDatas(this.panel1, show_sql);//给控件赋值
            //特殊处理的控件
            if (this.n_ChildMatType.Text.Trim() == "")
                return;
            this.comboBoxMatType.Text = Util.GetMatTypeName(Convert.ToInt32(this.n_ChildMatType.Text.Trim()));
            this.textBoxChildMatName.Text = childmat;
        }

        private void comboBoxMatType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.n_ChildMatType.Text = Util.GetMatType(this.comboBoxMatType.Text.Trim()).ToString();
        }

        private void MatSplitCForm_Load(object sender, EventArgs e)
        {
            (new InitFuncs()).InitComboBox(this.comboBoxMatType, "[物料]被拆物料类型");
 
            this.s_ReceiptId.Text = this.strReceiptId;
            this.txtMainMatName.Text = this.strMainMatID;
            this.s_ParaentManuCode.Text = (new DBUtil()).Get_Single_val("T_MatSplit_Main", "ManufactCode", "ReceiptId", this.s_ReceiptId.Text.Trim());
            this.s_ParaentManuCode.Text = this.ManufacCode;
            InitDataGridView();
            //限制值类型文本框的输入
            (new InitFuncs()).Num_limited(this.panel1);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0 && this.dataGridView1.SelectedRows[0].Cells["拆件单据编号"].Value!=null)
            {
                DialogResult dr = MessageBox.Show("确认删除选中零件？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    MatSplitChildDAO.DeleteByReceiptIdAndChildMatID(this.s_ReceiptId.Text.Trim(), this.s_ChildMatID.Text.Trim());
                }
                InitDataGridView();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.Type = "add";
            foreach (object o in this.panel1.Controls)
            {
                if (o is TextBox && ((TextBox)o).ReadOnly == false)
                    ((TextBox)o).Text = "";
                if (o is ComboBox)
                    ((ComboBox)o).SelectedIndex = 0;
                else
                    continue;
            }           
        }

        private void comboBoxMatType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            this.n_ChildMatType.Text = (Util.GetMatType(this.comboBoxMatType.Text.Trim())).ToString();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.textBoxChildMatName.Clear();
            WFilter wf = new WFilter(0, "MatName", true);
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
                if (wf.Return_Items.Count > 0)
                {
                    this.textBoxChildMatName.Text = wf.Return_Items[0];
                }
            }
        }

        private void MatSplitCForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
