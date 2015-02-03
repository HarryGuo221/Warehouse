using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Warehouse.DAO;
using Warehouse.DB;

namespace Warehouse.Stock
{
    public partial class Material_info : Form
    {
        private int curRowIndex = 0;
        
        string Basesql= "select t_matinf.MatID 物料编号, "
                           +"t_matinf.MatName as 物料名称," 
                           +"Specifications 型号规格 ,Units 计量单位,"
                           +"Brand 品牌,ProductType 产品种类," 
                           +"types as 机种,models as 机型,"
                           +"OrderCode 订货编码,ColorType 色彩,"
                           +"Speed 速度等级,Format 幅面," 
                           +"ynStopProduct 是否已停产,ConfigType 配置类型,"
                           +"SaleCalcPrice 业务销售核算成本价,"
                           +"SaleLimitPrice 业务销售限价,"
                           +"GuidePrice 厂方销售指导价," 
                           +"LimitPrice 厂方销售限价,LowestPrice 计划销售底价,"
                           +"PlanInPrice 标准进货价,MaxStockNum 库存上限,"
                           +"MinStockNum 最低库存," 
                           +"OriginPlace 产地,QualityDegree 质量等级," 
                           +"PinYinCode 拼音编码 "
                           +"from T_MatInf";
        
        string whereTJ = "";
        
        public Material_info()
        {
            InitializeComponent();
        }

        #region//初始化datagridview1，显示物料卡片信息
        public void initDataGridView()
        {
            DataTable dt = (new SqlDBConnect()).Get_Dt(this.Basesql +" "+this.whereTJ);
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.InitDataGridView(this.dataGridView1, dt);

            this.dataGridView1.ClearSelection();
            
            if(this.curRowIndex>0)
            this.dataGridView1.Rows[this.curRowIndex].Selected = true;

            this.dataGridView1.Columns["物料编号"].Width = 105;
            this.dataGridView1.Columns["物料名称"].Width = 250;
            this.dataGridView1.Columns["型号规格"].Width = 80;
            this.dataGridView1.Columns["计量单位"].Width = 80;
            this.toolStripStatusLabel1.Text = "  记录数："
                  + (this.dataGridView1.Rows.Count).ToString().Trim();
           
        }
        private void Material_info_Load_1(object sender, EventArgs e)
        {
            this.dataGridView1.AllowUserToAddRows = false;
            //initDataGridView();            
        }
        #endregion

       
        #region//双击修改物料卡片
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.curRowIndex = e.RowIndex;
            修改_Click(sender, e);            
        }
        #endregion
           
        #region//添加物料卡片     
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Add_materialForm add_mat = new Add_materialForm("add", "");
            add_mat.materialInfoFormChange += new Add_materialForm.MaterialInfoFormChange(add_mat_materialInfoFormChange);
            add_mat.ShowDialog();
        }
        void add_mat_materialInfoFormChange()
        {
            initDataGridView();
        }
        #endregion

        #region//修改物料卡片信息
        private void 修改_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                if (this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value == null)
                    return;              
                string matSysId = this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value.ToString().Trim();
                if (matSysId == "")
                    return;
                Add_materialForm edit_mat = new Add_materialForm("edit", matSysId);
                edit_mat.materialInfoFormChange += new Add_materialForm.MaterialInfoFormChange(edit_mat_materialInfoFormChange);
                edit_mat.ShowDialog();
            }
            else
                MessageBox.Show("请选中您要修改的行！");
        }
        void edit_mat_materialInfoFormChange()
        {
            initDataGridView();
        }
        #endregion

        #region//删除卡片信息
        private void 删除_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0)
                return;
            if (this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value == null)
                return;
            if (this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value.ToString().Trim() == "")
                return;

            DialogResult dr= MessageBox.Show("确认删除选中记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                try
                {
                    MaterialDAO mat_dao = new MaterialDAO();
                    mat_dao.delete_mat(this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value.ToString().Trim());
                    initDataGridView();
                }
                catch
                {
                    MessageBox.Show("请先删除其它表中涉及该物料的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        #endregion 

        private void 技术资料原件_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count == 1)
            //{
            //    string MatName = this.dataGridView1.SelectedRows[0].Cells["物料名称"].Value.ToString().Trim();
            //    MatDocsForm form = new MatDocsForm(MatName);
            //    form.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("请选中一个要添加原件的物料对象！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
        }

        private void 物料寿命_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value == null) return;
            if (this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value.ToString().Trim() == "") return;

            string matid = this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value.ToString().Trim();
            string matname=this.dataGridView1.SelectedRows[0].Cells["物料名称"].Value.ToString().Trim();
            EditMatBasicInfo form = new EditMatBasicInfo("add",matid,matname);
            form.ShowDialog();   
        }
        private void 机型耗材_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value == null) return;
            if (this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value.ToString().Trim() == "") return;

            string Pmatid = this.dataGridView1.SelectedRows[0].Cells["物料编号"].Value.ToString().Trim();
            string Pmatname=this.dataGridView1.SelectedRows[0].Cells["物料名称"].Value.ToString().Trim();
            EditMatRela MatRF = new EditMatRela("add", Pmatid, Pmatname);
            MatRF.ShowDialog();
        }

        //技术资料原件
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            技术资料原件_Click(sender, e);
        }
        //物料期限寿命
        private void 物料期限ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            物料寿命_Click(sender, e);
        }
        //机型耗材对照
        private void 机型耗材对照ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            机型耗材_Click(sender, e);
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            删除_Click(sender, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //根据物料编码MatID,物料名称(MatName)和拼音助记码(PYcode)来查询
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;////
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                whereTJ= " where Matid like '%{0}%'"
                    + " or MatName like '%{1}%' or PinYinCode like '%{2}%'";
                whereTJ = string.Format(whereTJ, textBoxName, textBoxName, textBoxName);
                initDataGridView();
              
             
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.whereTJ = "";
            initDataGridView();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WFilter wf = new WFilter(0, "MatID", true);
            wf.tableName = "T_MatInf";    //表名   
            wf.strSql = "select T_MatInf.MatID 物料编号,MatName 物料名称,Specifications 型号规格 ,Units 计量单位," +
                        "Brand 品牌,ProductType 产品种类,ColorType 色彩,Speed 速度等级,Format 幅面, ynStopProduct 是否已停产," +
                        "ConfigType 配置类型 " + "from T_MatInf";

            wf.s_items.Add("物料编号,MatID,C");
            wf.s_items.Add("物料名称,MatName,C");
            wf.s_items.Add("拼音编码,PinYinCode,C");
            wf.s_items.Add("品牌,Brand,C");

            wf.ShowDialog();
            
            if (wf.DialogResult == DialogResult.OK)
            {
                if (wf.Return_Items.Count <= 0)
                {
                    MessageBox.Show("无对应结果!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.whereTJ = " where MatID='" + wf.Return_Items[0].Trim()+"'";
                initDataGridView();
            }
        }
    }
}
