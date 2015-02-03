using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Sys
{
    public partial class LoginOpaHist : Form
    {
        public LoginOpaHist()
        {
            InitializeComponent();
        }

        private void show_dgv_log()
        {
            string sql_ = "";
            sql_ = "select username as 操作者,LoginTime as 登陆时间,LogoutTime as 退出时间 "
                + " from T_LogHist order by LoginTime Desc";
            DataTable dt1;
            dt1 = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_login.DataSource = dt1.DefaultView;
        }

        private void show_dgv_chg()
        {
            string sql_ = "";
            sql_ = "select OccurTime as 时间,Curopa as 操作者,Superopa as 授权人,ChgItems as 操作内容 "
              + " from T_SpecOpaHist order by OccurTime Desc";
            DataTable dt2;
            dt2 = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_chg.DataSource = dt2.DefaultView;
            this.dgv_chg.Columns["操作内容"].Width = 260;
        }

        private void LoginOpaHist_Load(object sender, EventArgs e)
        {
            show_dgv_log();
            show_dgv_chg();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            string usr_,logint_,logoutt_;
            if (this.tabControl1.SelectedIndex==0)
            {
                if (this.dgv_login.SelectedRows.Count <= 0) return;
                if (MessageBox.Show("确认删除选中登陆日志记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    for (int i = 0; i < this.dgv_login.SelectedRows.Count; i++)
                    {
                        usr_=this.dgv_login.SelectedRows[i].Cells["操作者"].Value.ToString().Trim();
                        logint_=this.dgv_login.SelectedRows[i].Cells["登陆时间"].Value.ToString().Trim();
                        logoutt_=this.dgv_login.SelectedRows[i].Cells["退出时间"].Value.ToString().Trim();

                        sql_ = "delete from T_LogHist where UserName='" + usr_ + "' and LoginTime='" + logint_ + "'";
                        try
                        {
                            (new SqlDBConnect()).ExecuteNonQuery(sql_);
                        }
                        catch
                        { }
                    }
                    show_dgv_log();

                }
            }
            else if (this.tabControl1.SelectedIndex == 1)
            {
                if (this.dgv_chg.SelectedRows.Count <= 0) return;
                if (MessageBox.Show("确认删除选中数据操作日志记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    for (int i = 0; i < this.dgv_chg.SelectedRows.Count; i++)
                    {
                         usr_ = this.dgv_chg.SelectedRows[i].Cells["操作者"].Value.ToString().Trim();
                         logint_= this.dgv_chg.SelectedRows[i].Cells["时间"].Value.ToString().Trim();

                         sql_ = "delete from T_SpecOpaHist where Curopa='" + usr_ + "' and OccurTime='" + logint_ + "'";
                        try
                        {
                            (new SqlDBConnect()).ExecuteNonQuery(sql_);
                        }
                        catch
                        { }
                    }
                    show_dgv_chg();
                }
            }
                
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
