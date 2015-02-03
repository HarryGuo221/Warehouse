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

namespace Warehouse
{
    public partial class ImportExcelForm : Form
    {
        /// <summary>
        /// 当前数据表
        /// </summary>
        private DataTable currentDataTable;

        public ImportExcelForm()
        {
            InitializeComponent();
        }

        private void ImportExcelForm_Load(object sender, EventArgs e)
        {
            //添加窗体加载操作
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            //打开一个文件选择框
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Excel文件";
            ofd.FileName = "";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//为了获取特定的系统文件夹，可以使用System.Environment类的静态方法GetFolderPath()。该方法接受一个Environment.SpecialFolder枚举，其中可以定义要返回路径的哪个系统目录
            ofd.Filter = "Excel文件(*.xls)|*.xls";
            ofd.ValidateNames = true;     //文件有效性验证ValidateNames，验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true;  //验证路径有效性
            ofd.CheckPathExists = true; //验证文件有效性

            string strName = string.Empty;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strName = ofd.FileName;
            }

            if (strName == "")
            {
                MessageBox.Show("没有选择Excel文件！无法进行数据导入！");
                return;
            }

            //显示打开文件的路径
            this.textBoxFilePath.Text = strName;
            
            //绑定 工作表名 组合框
            List<string> excelTableNames = DBUtil.getExcelTableNames(strName);
            this.comboBoxTableName.DataSource = excelTableNames;

            //string excelTableName = this.comboBoxTableName.Text.Trim();
            ////调用导入数据方法
            //DBUtil.EcxelToDataGridView(strName, excelTableName, this.dataGridView1);

            ////绑定各字段组合框
            //bandFields();
        }

        private void comboBoxTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strName = this.textBoxFilePath.Text.Trim();
            string excelTableName = this.comboBoxTableName.Text.Trim();
            //调用导入数据方法
            DBUtil.EcxelToDataGridView(strName, excelTableName, this.dataGridView1);

            //绑定各字段组合框
            bandFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 绑定各字段组合框
        /// </summary>
        private void bandFields()
        {
            string strName = this.textBoxFilePath.Text.Trim();
            string excelTableName = this.comboBoxTableName.Text.Trim();
            DataTable dt = DBUtil.getEcxelTable(strName, excelTableName);

            this.currentDataTable = dt;//把当前的数据表赋值给一个全局变量

            List<string> fields = new List<string>();
            for (int i = 1; i <= dt.Columns.Count; i++)
            {
                fields.Add("F" + i);
            }

            this.comboBoxAddress.DataSource = fields.ToArray();
            this.comboBoxCode.DataSource = fields.ToArray();
            this.comboBoxContactPerson.DataSource = fields.ToArray();
            this.comboBoxFax.DataSource = fields.ToArray();
            this.comboBoxMemo.DataSource = fields.ToArray();
            this.comboBoxName.DataSource = fields.ToArray();
            this.comboBoxPhone.DataSource = fields.ToArray();

            this.comboBoxAddress.Text = "";
            this.comboBoxCode.Text = "";
            this.comboBoxContactPerson.Text = "";
            this.comboBoxFax.Text = "";
            this.comboBoxMemo.Text = "";
            this.comboBoxName.Text = "";
            this.comboBoxPhone.Text = "";
        }
        /// <summary>
        /// 把选择的数据导入数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            //Providers providers = new Providers();
            //providers.ProvCode = this.comboBoxCode.Text.Trim();
            //providers.ProvName = this.comboBoxName.Text.Trim();
            //providers.ProvAddress = this.comboBoxAddress.Text.Trim();
            //providers.ProvPhone = this.comboBoxPhone.Text.Trim();
            //providers.ProvFax = this.comboBoxFax.Text.Trim();
            //providers.ProvContact = this.comboBoxContactPerson.Text.Trim();
            //providers.Memo = this.comboBoxMemo.Text.Trim();

            //if (this.comboBoxCode.Text.Trim() == "")
            //{
            //    MessageBox.Show("必须指定哪一列为单位编码！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            ////插入数据库
            //ProvidersDAO.insertIntoProviders(this.currentDataTable, providers);
        }

    }
}
