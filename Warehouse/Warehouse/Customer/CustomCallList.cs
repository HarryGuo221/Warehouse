using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.Stock;

namespace Warehouse.Customer
{
    public partial class CustomCallList : Form
    {   
        public string CurrentUser="";  //当前用户(已成功登陆的)

        private string curSelCustID = "";   //当前选择的CustId;
        //****查询客户机器用的SQL
        private string BaseSql_CustMac = "select T_CustomerMac.sysid,"
                     + "T_CustomerMac.Degreed as 等级,"
                     + "T_CustomerMac.customno as 代码,"
                     + "T_CustomerInf.CustName as 客户名,"
                     + "T_CustomerMac.Mdepart as 地址,"
                     + "T_AreaInf.Area as 地区,"
                     + "T_CustomerMac.Pcode as 邮编,"
                     + "T_CustomerMac.Mtype as 机型,"
                     + "T_CustomerMac.Manufactcode as 机号,"
                     + "T_CustomerMac.Managecode as 管理编号,"
                     + "tb_maintetype.maintetypename as 类别,"
                     + "T_CustomerMac.ptech as 技术员,"
                     + " T_CustomerMac.saleDate as 安装日期,"
                     + " T_CustomerMac.MainteEndDate as 签约终止日,"
                     + " TB_machusedtype.usedname as 使用类别,"
                     + " T_CustomerMac.copy_rate as 日复印量,"
                     + " T_CustomerMac.zb_rate as 日制版量,"
                     + " T_CustomerMac.psale as 业务员,"
                     + " T_CustomerMac.pred_bydate as 保养日期,"
                     + "T_CustomerMac.pred_tonerdate as 换墨日期,"
                     + "T_CustomerMac.pred_zbdate as 版纸日期,"
                     + "T_CustomerMac.pred_maintedate as 保修到期,"
                     + "T_CustomerMac.pred_buydate as 换买日期,"
                     //+ "T_CustomerMac.Mbrand as 机器品牌," 
                     //+ "T_CustomerMac.Mmodel as 机器种类,"
                     + "T_CustomerMac.copy_rate_color as 彩色日印量,"
                     + " T_CustomerMac.pred_colorDate as 彩粉日期,"
                     + "T_CustomerMac.pred_ggdate as 黑鼓日期,"
                     + "T_CustomerMac.pred_colordate as 彩鼓日期 "
                     + " from T_CustomerMac "
                     + " left join T_CustomerInf on T_CustomerMac.custid=T_CustomerInf.custid"
                     + " left join T_AreaInf on T_AreaInf.Areaid=T_CustomerMac.areacode "
                     + " left join tb_maintetype on tb_maintetype.maintetypecode=T_CustomerMac.MainteType "
                     + " left join TB_machusedtype on TB_machusedtype.used_type=T_CustomerMac.used_type ";

         private string WhereTj_CustMac = "";

        //***查询受理单的SQL
        string BaseSql_Accept = "select T_CustomerCall.sysid ,T_CustomerCall.status as 状态,"
                +"T_CustomerInf.CustName as 客户名,"
                +"T_CustomerCall.mtype as 机型,"
                +"T_CustomerCall.ManufactCode as 机号,"
                +"T_CustomerCall.RepairContent as 保修内容,"
                +"T_CustomerCall.CallDate as 召唤日,"
                +"T_CustomerCall.CallTime as 召唤时间,"
                +"T_CustomerCall.UrgentDegree as 紧急度,"
                +"T_CustomerCall.Technician1 as 预定技术员,"
                +"T_CustomerCall.Technician2 as 派工技术员,"
                +"T_CustomerCall.PlanedDay as 预定日,"
                +"T_CustomerCall.PlanedTime 预定时间,"
                +"T_CustomerCall.QuotedStartTime as 报价开始时间,"
                +"T_CustomerCall.QuotedEndTime as 报价结束时间,"
                +"T_CustomerCall.ActionResults as 行动结果"
                + " from T_CustomerCall "
                + " Left join T_customerinf on T_CustomerCall.CustCode=T_customerInf.CustId";
        private string WhereTj_Accept = "";

        public CustomCallList()
        {
            InitializeComponent();
        }

        private void CustomCallList_Load(object sender, EventArgs e)
        {
            //隐藏行头
            //dgv_accept.RowHeadersVisible = false; 
            //显示客户机器
            this.show_dgv_custmac();
            
            ////显示受理列表
            //this.WhereTj_Accept = " where T_CustomerCall.status='未派'";
           this.show_dgv_accept();

        }

    
        //显示客户机器信息
        private void show_dgv_custmac()
        {
            string sql_ = this.BaseSql_CustMac + " " + this.WhereTj_CustMac;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            (new InitFuncs()).InitDataGridView(this.dgv_CustMac, dt);
            this.dgv_CustMac.Columns["sysid"].Visible = false;
            this.lbtot_CustMac.Text = "记录数：" + (this.dgv_CustMac.Rows.Count - 1).ToString().Trim();
            this.dgv_worksheet.DataSource = null;
        }

         //显示满足条件的召唤单
        private void show_dgv_accept()
        {
            string sql_ = "";
            sql_ = this.BaseSql_Accept + " " + this.WhereTj_Accept;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            (new InitFuncs()).InitDataGridView(this.dgv_accept, dt);
            this.dgv_accept.Columns[0].Visible = false; //不显示系统编号
            this.lbtot_paigong.Text = "记录数："+(this.dgv_accept.Rows.Count - 1).ToString().Trim();
        }
        

        //显示客户的维修历史
        private void show_dgv_worksheet(string custid_)
        {
            string sql_ = "select  sysid as 系统编号,wsCode as 工单编号,T_customerInf.CustName as 客户名称,AcceptDay as 受理日期,ManufactureNum as 机号,"
                  + "CallType as 召唤类型,ServiceType as 服务类别,WorkContent as 工作内容,RepairContent as 报修内容,"
                  + "startTime as 启动时间,ArriveTime as 到达时间,departTime as 离开时间,"
                  + "TotalCopyNum as 总复印张数,chokedPaperNum as 卡纸张数,ColorNum1 as 专彩张数,ColorNum2 as 普彩张数,"
                  + "BlackNum as 黑白张数,TotalPlateNum as 总制版张数, ErrDescription as 故障描述,Process as 处理过程,"
                  + "Result as 修理结果 "
                  + " from T_WorkSheet "
                  + " Left join T_customerInf on T_customerInf.Custid=T_WorkSheet.Custid"
                  + " where T_WorkSheet.CustId='" + custid_ + "'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            (new InitFuncs()).InitDataGridView(this.dgv_worksheet, dt);
            this.dgv_CustMac.Columns[0].Visible = false; //不显示系统编号
            this.lbtot_workSheet.Text = "记录数：" + (this.dgv_worksheet.Rows.Count - 1).ToString().Trim();
        }


         private void customerCallFormChange_()
        {
            show_dgv_accept();
        }

         private void addWorkFormChange_()
         {
             if (this.curSelCustID == "") return;
             show_dgv_worksheet(this.curSelCustID);
         }

      
        private void button4_Click(object sender, EventArgs e)
        {
            this.WhereTj_CustMac = "";
            show_dgv_custmac();
            show_dgv_accept();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dgv_CustMac.SelectedRows.Count > 0)
            {
                if (this.dgv_CustMac.SelectedRows[0].Cells["sysid"].Value == null) return;
                string cmsysid_ = this.dgv_CustMac.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
                if (cmsysid_ == "") return;
               
                CustomerCallAdd cca = new CustomerCallAdd();
                cca.customerCallFormChange_ += new CustomerCallAdd.CustomerCallFormChange(customerCallFormChange_);
                cca.CurrentUser = this.CurrentUser;
                cca.cmsysid = cmsysid_;
                cca.type = "add";
                cca.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dgv_accept.SelectedRows.Count > 0)
            {
                if (this.dgv_accept.SelectedRows[0].Cells["sysid"].Value == null) return;
                
                //if (this.dgv_accept.SelectedRows[0].Cells["客户代号"].Value == null) return;
                //if (this.dgv_accept.SelectedRows[0].Cells["机型"].Value == null) return;
                //if (this.dgv_accept.SelectedRows[0].Cells["机号"].Value == null) return;

                string sysid = this.dgv_accept.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
                //string mtype_ = this.dgv_accept.SelectedRows[0].Cells["机型"].Value.ToString().Trim();
                //string mc_ = this.dgv_accept.SelectedRows[0].Cells["机号"].Value.ToString().Trim();
               
                if (sysid== "") return;
                
                CustomerCallAdd cca = new CustomerCallAdd();
                cca.CurrentUser = this.CurrentUser;
                cca.type = "edit";
                cca.CallId = sysid;
                cca.customerCallFormChange_ += new CustomerCallAdd.CustomerCallFormChange(customerCallFormChange_);
                cca.ShowDialog();
            }
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dgv_accept.SelectedRows.Count > 0)
            {
                if (this.dgv_accept.SelectedRows[0].Cells["sysid"].Value == null) return;
                
                string sysid = this.dgv_accept.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
                if (sysid == "") return;

                string status_ = this.dgv_accept.SelectedRows[0].Cells["状态"].Value.ToString().Trim();
                if ((sysid != "")&&(status_=="未派"))
                {
                    //FormBeginCustCall fbc = new FormBeginCustCall();
                    //fbc.sysid_ = sysid;   //受理单系统编号
                    //fbc.ShowDialog();
                }
            }
        }

    
        private void dgv_accept_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.ColumnIndex == 1 && e.RowIndex == 1)

            //if ((e.Value != null) && (e.Value.ToString().Trim() == "已派"))
            //{
            //    e.Handled = true;
            //    using (SolidBrush brush = new SolidBrush(Color.Chocolate))
            //    {
            //        e.Graphics.FillRectangle(brush, e.CellBounds.Left + 2, e.CellBounds.Top + 2, e.CellBounds.Width - 4, e.CellBounds.Height -4);
            //    }
            //    e.PaintContent(e.CellBounds);
            //} 

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dgv_CustMac.SelectedRows.Count <= 0) return;
            if (dgv_CustMac.SelectedRows[0].Cells["sysid"].Value == null) return;
            if (dgv_CustMac.SelectedRows[0].Cells["sysid"].Value.ToString().Trim() == "") return;
            string machineid_ = dgv_CustMac.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            AddWorkForm awf = new AddWorkForm();
            awf.type = "add";  //新增时传入"客户编码"
            awf.machineid = machineid_;
            awf.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.dgv_worksheet.SelectedRows.Count <= 0) return;
            if (this.dgv_worksheet.SelectedRows[0].Cells["系统编号"].Value == null) return;
            string sysid_ = this.dgv_worksheet.SelectedRows[0].Cells["系统编号"].Value.ToString().Trim();
            
            AddWorkForm awf = new AddWorkForm();
            awf.type = "edit";  //新增时传入"客户编码"
            awf.sysid_ = sysid_;
            awf.ShowDialog();
        }

        private void dgv_CustMac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_CustMac.SelectedRows.Count > 0)
            {
                if (this.dgv_CustMac.SelectedRows[0].Cells["sysid"].Value == null) return;

                string custsysid = this.dgv_CustMac.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
                if (custsysid == "") return;
                this.curSelCustID = custsysid;
                //show_dgv_worksheet(this.curSelCustID);
            }
        }

       
        private void tb_selv_KeyDown(object sender, KeyEventArgs e)
        {
            //根据Custid(客户ID),CustName(客户名),PinYinCode(拼音码),
            //mtype，Manufactcode(机号)来模糊查询
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string swhere = " where T_CustomerInf.CustID like '%{0}%'"
                     + " or CustName like '%{1}%' or PinYinCode like '%{2}%'"
                     + " or (T_CustomerMac.sysid in "
                     + "(select sysid from T_CustomerMac where "
                     + " T_CustomerMac.Manufactcode like '%{3}%' "
                     + " or T_CustomerMac.Managecode like '%{4}%' "
                     + " or T_CustomerMac.mtype like '%{5}%'))";

                swhere = string.Format(swhere, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);
                this.WhereTj_CustMac = swhere;
                //显示列表
                this.show_dgv_custmac();
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.WhereTj_Accept = " where T_CustomerCall.status='已派'";
                this.show_dgv_accept();
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked)
            {
                this.WhereTj_Accept = " where T_CustomerCall.status='未派'";
                this.show_dgv_accept();
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (this.radioButton3.Checked)
            {
                this.WhereTj_Accept = " ";
                this.show_dgv_accept();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.splitContainer1.Visible = !this.splitContainer1.Visible;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
