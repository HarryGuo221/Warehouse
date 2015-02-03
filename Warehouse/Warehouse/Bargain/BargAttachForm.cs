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
using System.Data.SqlClient;
using System.IO;

namespace Warehouse.Bargain
{
    public partial class BargAttachForm : Form
    {
        public BargAttachForm()
        {
            InitializeComponent();
        }
        private string Type;
        /// <summary>
        /// 合同编号
        /// </summary>
        private string barsysid;

        public BargAttachForm(string sysid)
        {
            InitializeComponent();

            this.Type = "add";
            this.barsysid = sysid;
        }
               
        private void BargAttachForm_Load(object sender, EventArgs e)
        {
            initdataGridview();
            //显示合同对应的客户信息
            label4.Text = "";
            string sql_ = "select T_Bargains.Mtype,T_Bargains.Manufactcode,"
               + "T_CustomerMac.Mdepart"
               +" from T_Bargains,T_CustomerMac "
               + " where T_Bargains.MachineID=T_CustomerMac.sysid "
               + " and T_Bargains.sysid=" + this.barsysid;
            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            if (dt.Rows.Count > 0)
            {
                label4.Text = dt.Rows[0]["Mdepart"].ToString().Trim() + "   "
                    + "机型：" + dt.Rows[0]["Mtype"].ToString().Trim() + "   "
                    + "机号：" + dt.Rows[0]["Manufactcode"].ToString().Trim();
            }
            this.n_barsysid.Text = this.barsysid;
        }
        
        private void initdataGridview()
        {
            DataTable dt = new DataTable();
            dt=(new BargainDAO()).getBargAttach(this.barsysid) ;
            (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);

            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[1].Visible = false;

        }

        #region//确认添加附件及其相关信息
        /// <summary>
        /// 保存
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //输入数据有效性验证
            if (s_AttachName.Text.Trim() == "")
            {
                MessageBox.Show("附件名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string strSqlSel = "";
            string strSql = "";
            DBUtil dbUtil = new DBUtil();
            SqlDBConnect db = new SqlDBConnect();
            if (this.Type == "add")
            {
                //判断该资料原件记录是否已存在 
                strSqlSel = "select * from T_BargAttach where barsysid={0} and AttachName='{1}'";
                strSqlSel = string.Format(strSqlSel, this.barsysid, this.s_AttachName.Text.Trim());

                strSql = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_BargAttach");
            }
            else if (this.Type == "edit")
            {
                int SysId = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["附件编号"].Value.ToString().Trim());
                strSqlSel = "select * from T_BargAttach where BarSysId like {0} and Ltrim(Rtrim(AttachName)) like '{1}' and Ltrim(Rtrim(SysId)) not like {2}";
                strSqlSel = string.Format(strSqlSel, this.barsysid, this.s_AttachName.Text.Trim(), SysId);

                string strWhere = "where SysId=" + SysId;
                strSql = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_BargAttach", strWhere);

            }
            bool isExist = dbUtil.yn_exist_data(strSqlSel);
            if (isExist)
            {
                MessageBox.Show("当前合同已存在该附件名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            db.ExecuteNonQuery(strSql);

            //MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            initdataGridview();
        }
        #endregion

        #region //添加附件
        /// <summary>
        /// 添加附件
        /// </summary>
        private void btnAddDoc_Click(object sender, EventArgs e)
        {
            #region 验证
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;
            if (this.dataGridView1.SelectedRows[0].Cells["barsysid"].Value == null) return;

            if (this.s_AttachName.Text.Trim() == "")
            {
                MessageBox.Show("请先填写附件名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //判断是否存在记录
            string strSqlSel = "select * from T_BargAttach where barsysid={0} and AttachName='{1}'";
            strSqlSel = string.Format(strSqlSel, this.barsysid, this.s_AttachName.Text.Trim());
            bool isExist = (new DBUtil()).yn_exist_data(strSqlSel);
            if (isExist == false)
            {
                MessageBox.Show("该记录不存在，不能为其添加附件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "word文档(*.doc)|*.doc|Excel电子表格(*.xls)|*.xls|所有文件(*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = fd.FileName;
                string fileExt = fileName.Substring(fileName.LastIndexOf('.'));
                //将文件读入到字节数组
                System.IO.FileStream fs = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] buffByte = new byte[fs.Length];
                fs.Read(buffByte, 0, (int)fs.Length);
                fs.Close();
                fs = null;

                SqlDBConnect dbc = new SqlDBConnect();
                string sql_ = "update T_BargAttach set Attachment=@file,doctype=@doctype where barsysid={0} and AttachName='{1}'";
                sql_ = string.Format(sql_, this.barsysid, this.s_AttachName.Text.Trim());

                SqlCommand cm = new SqlCommand();
                cm.Connection = dbc.conn;
                dbc.OpenDb();
                cm.CommandText = sql_;
                cm.Parameters.Add("@file", System.Data.SqlDbType.Image);
                cm.Parameters.Add("@doctype", System.Data.SqlDbType.NChar);
                cm.Parameters[0].Value = buffByte;
                cm.Parameters[1].Value = fileExt;
                try
                {
                    cm.ExecuteNonQuery();
                    MessageBox.Show("绑定成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    initdataGridview();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("绑定失败" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                dbc.CloseDb();
            }
        }
        #endregion
        /// <summary>
        /// 新增
        /// </summary>
        private void btnNew_Click(object sender, EventArgs e)
        {
            this.Type = "add";
            this.btnAddDoc.Enabled = false;
            this.btnGetAttach.Enabled = false;
            this.btnDel.Enabled = false;
            s_AttachName.Text = "";
            s_memo.Text = "";
            s_AttachName.Focus();
        }
        /// <summary>
        /// 单击某行提供修改功能
        /// </summary>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null)
                return;
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim() == "")
                return;
            this.btnAddDoc.Enabled = true;
            this.btnGetAttach.Enabled = true;
            this.btnDel.Enabled = true;
            this.Type = "edit";

            InitFuncs inf = new InitFuncs();
            string barsysid=this.dataGridView1.SelectedRows[0].Cells["barsysid"].Value.ToString().Trim();
            string AttachName = this.dataGridView1.SelectedRows[0].Cells["附件名称"].Value.ToString().Trim();
            string sel_sql = "select * from T_BargAttach where barsysid=" + barsysid + " and AttachName='" + AttachName+"'";
            inf.ShowDatas(this.panel1, sel_sql);
        }
        /// <summary>
        /// 查看附件
        /// </summary>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows[0].Cells["barsysid"].Value == null) return;
            if (this.dataGridView1.SelectedRows[0].Cells["barsysid"].Value.ToString().Trim() == "") return;
                

            string strDocName = this.dataGridView1.SelectedRows[0].Cells["附件名称"].Value.ToString().Trim();
            string strSql = "select Attachment,doctype from T_BargAttach where barsysid={0} and AttachName='{1}'";
            strSql = string.Format(strSql, this.barsysid, strDocName);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            if (dt.Rows[0]["Attachment"] == System.DBNull.Value || dt.Rows[0]["doctype"] == System.DBNull.Value)
            {
                MessageBox.Show("还没有上传附件！");
                return;
            }
            Byte[] bytes = (Byte[])dt.Rows[0]["Attachment"];

            //string currentDirectory = Directory.GetCurrentDirectory() + "\\Temp\\";
            string currentDirectory = Application.StartupPath + "\\Temp\\";
            string path = currentDirectory + "temp" + dt.Rows[0]["doctype"].ToString().Trim();
            File.WriteAllBytes(path, bytes);

            System.Diagnostics.Process.Start(path);
        }
        /// <summary>
        /// 删除合同附件记录
        /// </summary>
        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("确认删除选中合同附件!", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                    {
                        string barsysid = this.dataGridView1.SelectedRows[0].Cells["barsysid"].Value.ToString().Trim();
                        string attachName = this.dataGridView1.SelectedRows[0].Cells["附件名称"].Value.ToString().Trim();
                        BargainDAO bdao = new BargainDAO();
                        bdao.dele_bargAttach(barsysid, attachName);
                    }
                    initdataGridview();
                    s_AttachName.Text = "";
                    s_memo.Text = "";
                    this.Type = "add";
                }
                else
                    return;
            }
            else
                return;
        }
        /// <summary>
        /// 查看附件
        /// </summary>
        private void 查看附件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1_CellDoubleClick(null, null);
        }

        private void BargAttachForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

    }
}
