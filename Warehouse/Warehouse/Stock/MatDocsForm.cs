using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Warehouse.DB;
using Warehouse.Base;
using System.IO;

namespace Warehouse.Stock
{
    public partial class MatDocsForm : Form
    {
        public string Type;
       
        public  string sysid_ = "";  //当前资料的编码
        public MatDocsForm()
        {
            InitializeComponent();
        }

        private void MatDocsForm_Load(object sender, EventArgs e)
        {
            string sql_="";
            if (this.Type == "edit")
            {
                sql_ = "select docname,doctype,memo from T_MatDocs where sysid=" + this.sysid_;
                (new InitFuncs()).ShowDatas(this.panel1, sql_);
                this.btn_look.Enabled = true;
                this.btnAddMatDoc.Enabled = true;
            }
            else
            {
                this.btn_look.Enabled = false;
                this.btnAddMatDoc.Enabled = false;
            }
        }
       
        
        #region //添加技术资料原件附件
        /// <summary>
        /// 添加技术资料附件
        /// </summary>
        private void btnAddMatDoc_Click(object sender, EventArgs e)
        {
            if (this.s_DocName.Text.Trim() == "")
            {
                MessageBox.Show("请先填写资料名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            //判断是否存在记录
            string strSqlSel="";
            if (this.Type=="add")
            {
               strSqlSel = "select * from T_MatDocs where DocName='{0}'";
            }
            else
            {
               strSqlSel = "select * from T_MatDocs where DocName='{0}' and sysid!="+this.sysid_;
            }
            
            strSqlSel = string.Format(strSqlSel, this.s_DocName.Text.Trim());
            bool isExist = (new DBUtil()).yn_exist_data(strSqlSel);
            if (isExist)
            {
                MessageBox.Show("该资料名已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            

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
                string sql_ = "update T_MatDocs set DocBlob=@file,doctype=@doctype where sysid='{0}'";
                sql_ = string.Format(sql_, this.sysid_);

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
                    MessageBox.Show("上传成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("上传失败" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                dbc.CloseDb();
            }           
        }
        #endregion

        
        private void MatDocsForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sysid_ == "") return;

            string strSql = "select DocBlob,doctype from T_MatDocs where sysid=" + sysid_;

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            if (dt.Rows[0]["DocBlob"] == System.DBNull.Value)
            {
                MessageBox.Show("该资料没有绑定附件！");
                return;
            }
            Byte[] bytes = (Byte[])dt.Rows[0]["DocBlob"];

            string currentDirectory = Application.StartupPath + "\\Temp\\";
            string path = currentDirectory + "temp" + dt.Rows[0]["doctype"].ToString().Trim();
            File.WriteAllBytes(path, bytes);
            System.Diagnostics.Process.Start(path);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            if (this.Type == "add")
            {
                sql_ = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_MatDocs");
            }
            else
            { 
                string swhere=" where sysid="+this.sysid_;
                sql_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_MatDocs", swhere);
            }

            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql_);
            }
            catch
            { }
        }

    }
}
