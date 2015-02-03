using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.Base;
using Warehouse.Customer;
using Warehouse.Stock;

namespace Warehouse.Bargain
{
    public partial class Formorder : Form
    {
        public string CurrentUser = "";

        private string CurMachineId = "";
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
        string BaseSql_Accept = "select T_CustomerCall.sysid ,"
                +"T_CustomerCall.endflag as 状态,"
                + "T_WorkType.wcName as 工作类型,"
                + "T_CustomerCall.Technician2 as 派工技术员,"
                //+" T_CustomerInf.CustName as 客户名,"
                + "T_CustomerCall.PlanedDay as 预定日,"
                + "T_CustomerCall.PlanedTime as 时间,"
                +" TB_workResult.repairstatusname as 行动结果,"
                + " T_CustomerMac.Mdepart as 客户,"
                + " T_CustomerCall.Mtype as 机型,"
                +" T_CustomerCall.AcceptMemo as 报修内容,"
                + " T_CustomerCall.CallDate as 召唤日,"
                + " T_CustomerCall.CallTime as 时间,"
                + " T_CustomerCall.rsphours as 响应时间,"
                + " T_CustomerCall.UrgentDegree as 紧急度,"
                + " T_CustomerCall.Technician1 as 预定技术员,"
                + " T_CustomerCall.Odrarrivetime as 到达,"
                + " T_CustomerCall.Odrleavetime as 离开,"
                + "tb_maintetype.maintetypename as 服务类别,"
                + " T_CustomerCall.OdrProduceType as 来源,"
                + " T_CustomerCall.Contact as 联系人,"
                + " T_CustomerCall.Tel as 电话 "
                + " from T_CustomerCall "
                + " left join T_CustomerMac on T_CustomerCall.MachineID=T_CustomerMac.sysid"
                + " left join T_Worksheet on T_CustomerCall.sysid=T_Worksheet.CallBillSysId"
                + " left join tb_maintetype on T_CustomerCall.Odrmaintetype=tb_maintetype.maintetypecode "
                + " left join T_WorkType on T_CustomerCall.WorkType=T_WorkType.wcid "
                + " left join TB_workResult on T_CustomerCall.ActionResults=TB_workResult.repairstatuscode ";

               private string WhereTj_Accept = "";


        public Formorder()
        {
            InitializeComponent();
        }


        //显示客户的维修历史
        private void show_dgv_worksheet(string MachineId)
        {
            string sql_ = "select  T_WorkSheet.sysid ,"
                  + "T_WorkSheet.wsCode as 工单编号,"
                  + "T_WorkSheet.orderDate  as 召唤日,"
                  + "T_WorkSheet.orderTime  as 时间,"
                  + "T_WorkSheet.departTime  as 离开时间,"
                  + "TB_workResult.repairstatusname  as 处理结果,"
                  + "T_customerMac.Mdepart  as 客户名,"
                  + "T_WorkSheet.Mtype  as 机型,"
                  + "T_WorkType.wcName  as 类型,"
                  + "T_WorkSheet.TrblDetail  as 故障详细,"
                  + "T_WorkSheet.TotalCopyNum as 总复印张数,"
                  + "T_WorkSheet.ColorNum1 as 专彩张数,"
                  + "T_WorkSheet.ColorNum2 as 普彩张数,"
                  + "T_WorkSheet.chokedPaperNum as 卡纸张数,"
                  + "T_WorkSheet.TotalPlateNum as 制版数,"
                  + "T_WorkSheet.MCBJ as MCBJ,"
                  + "T_WorkSheet.RepairTech as 技术员,"
                  + "tb_maintetype.maintetypename as 服务类别,"
                  + "T_WorkSheet.is_first as 初次维修 "
                  + " from T_WorkSheet "
                  + " left join T_CustomerMac on T_WorkSheet.MachineID=T_CustomerMac.sysid"
                  + " left join TB_workResult on T_WorkSheet.WorkResult=TB_workResult.repairstatuscode "
                  + " left join tb_maintetype on T_WorkSheet.ServiceType=tb_maintetype.maintetypecode "
                  + " left join T_WorkType on T_WorkSheet.WorkContent=T_WorkType.wcid "
                  
                  + " where T_WorkSheet.MachineId=" + MachineId;

            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            (new InitFuncs()).InitDataGridView(this.dgv_worksheet, dt);
            this.dgv_worksheet.Columns[0].Visible = false; //不显示Sysid
            this.dgv_worksheet.Columns[1].Visible = false; //不显示MachineId

            this.lbtot_workSheet.Text = "记录数：" + (this.dgv_worksheet.Rows.Count ).ToString().Trim();
        
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            p4.Visible = true;
            p3.Visible = false;
            p2.Visible = false;
            p1.Visible = true;
            p1.Dock = DockStyle.Fill;
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            p4.Visible = false;
            p3.Visible = true;
            p3.Dock = DockStyle.Fill;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            p3.Visible = false;
            p4.Visible = true;
            p1.Visible = false;
            p2.Visible = true;
            p2.Dock = DockStyle.Fill;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            p3.Visible = true;
            p3.Dock = DockStyle.Bottom;
            p3.Height = this.Height / 5;
            p4.Visible = true;
            p4.Dock = DockStyle.Fill;

            p1.Visible = true;
            p1.Dock = DockStyle.Left;
            p1.Width = this.Width / 3+20;
            p2.Visible = true;
            p2.Dock = DockStyle.Fill;
        }

        //显示客户机器信息
        private void show_dgv_custmac()
        {
           
            string sql_ = this.BaseSql_CustMac + " " + this.WhereTj_CustMac;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            (new InitFuncs()).InitDataGridView(this.dgv_CustMac, dt);
            this.dgv_CustMac.Columns["sysid"].Visible = false;
            
            this.lbtot_CustMac.Text = "记录数：" + (this.dgv_CustMac.Rows.Count ).ToString().Trim();
            this.dgv_worksheet.DataSource = null;
            
            this.dgv_CustMac.Columns["等级"].Width = 30;
            this.dgv_CustMac.Columns["代码"].Width = 50;
            this.dgv_CustMac.Columns["客户名"].Width = 200;
            this.dgv_CustMac.Columns["地址"].Width = 260;
            
        }


        private void customerCallFormChange_()
        {
            show_dgv_accept();
        }

        //显示满足条件的召唤单
        private void show_dgv_accept()
        {
            string sql_ = "";
            sql_ = this.BaseSql_Accept + " " + this.WhereTj_Accept;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            (new InitFuncs()).InitDataGridView(this.dgv_accept, dt);
            this.dgv_accept.Columns[0].Visible = false; //不显示系统编号
            this.lbtot_paigong.Text = "记录数：" + (this.dgv_accept.Rows.Count).ToString().Trim();
            this.dgv_accept.Columns["状态"].Width = 40;
            this.dgv_accept.Columns["客户"].Width = 250;
            this.dgv_accept.Columns["报修内容"].Width = 260;
            this.dgv_accept.Columns["预定日"].Width = 70;
            this.dgv_accept.Columns["工作类型"].Width = 60;
            this.dgv_accept.Columns["时间"].Width = 70;
            this.dgv_accept.Columns["派工技术员"].Width = 70;
            this.dgv_accept.Columns["行动结果"].Width = 70;
        }



        private void Formorder_Load(object sender, EventArgs e)
        {
            this.dgv_accept.AllowUserToAddRows = false;
            this.dgv_CustMac.AllowUserToAddRows = false;
            this.dgv_worksheet.AllowUserToAddRows = false;
            
            tb_tech.KeyDown+=new KeyEventHandler(InfoFind.UserName_KeyDown);
            show_dgv_custmac();
            comboBox1.SelectedIndex = 0;
            //show_dgv_accept();

        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dgv_CustMac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            addWorkFormChange_();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //根据Custid(客户ID),CustName(客户名),PinYinCode(拼音码),
            //mtype，Manufactcode(机号)来模糊查询
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                
                //跳字搜索
                textBoxName = textBoxName.Replace(" ","%");
                //
                if (textBoxName == "") return;
                string swhere = " where T_CustomerInf.CustID like '%{0}%'"
                     + " or CustName like '%{1}%' or PinYinCode like '%{2}%'"
                     + " or (T_CustomerMac.sysid in "
                     + "(select sysid from T_CustomerMac where "
                     + " T_CustomerMac.Manufactcode like '%{3}%' "
                     + " or T_CustomerMac.Managecode like '%{4}%' "
                     + " or T_CustomerMac.Mdepart like '%{5}%' "
                     + " or T_CustomerMac.mtype like '%{6}%'))";

                swhere = string.Format(swhere, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);
                this.WhereTj_CustMac = swhere;
                //显示列表
                this.show_dgv_custmac();
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dgv_accept.SelectedRows.Count > 0)
            {
                if (this.dgv_accept.SelectedRows[0].Cells["sysid"].Value == null) return;
                string sysid = this.dgv_accept.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
                
                if (sysid == "") return;

                CustomerCallAdd cca = new CustomerCallAdd();
                cca.CurrentUser = this.CurrentUser;
                cca.type = "edit";
                cca.CallId = sysid;
                cca.customerCallFormChange_ += new CustomerCallAdd.CustomerCallFormChange(customerCallFormChange_);
                cca.ShowDialog();
            }
        }

        private void tb_tech_TextChanged(object sender, EventArgs e)
        {
            if (this.tb_tech.Text.Trim() != "")
            {
                this.WhereTj_Accept = " where Technician2 like '%" + this.tb_tech.Text.Trim() + "%'";
                show_dgv_accept();
            }
            else
            {
                this.WhereTj_Accept = "";
                show_dgv_accept();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dgv_accept.SelectedRows.Count > 0)
            {
                if (this.dgv_accept.SelectedRows[0].Cells["sysid"].Value == null) return;

                string sysid = this.dgv_accept.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
                if (sysid == "") return;

                string endflag_ =(new DBUtil()).Get_Single_val("T_customerCall","endflag","sysid",sysid) ;
                if (endflag_=="0")
                {
                    FormBeginCustCall fbc = new FormBeginCustCall();
                    fbc.CallSysid_ = sysid;   //受理单系统编号
                    fbc.ShowDialog();
                }
            }
        }

        private void addWorkFormChange_()
        {
            if (dgv_CustMac.SelectedRows.Count > 0)
            {
                if (this.dgv_CustMac.SelectedRows[0].Cells["sysid"].Value == null) return;

                string machsysid = this.dgv_CustMac.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
                if (machsysid == "") return;

                this.CurMachineId = machsysid;
                show_dgv_worksheet(this.CurMachineId);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.dgv_worksheet.SelectedRows.Count <= 0) return;
            if (this.dgv_worksheet.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_worksheet.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();

            AddWorkForm awf = new AddWorkForm();
            awf.addWorkFormChange_+=new AddWorkForm.AddWorkFormChange(addWorkFormChange_);
            awf.type = "edit";  
            awf.sysid_ = sysid_;   //派工单编号
            awf.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dgv_CustMac.SelectedRows.Count <= 0) return;
            if (dgv_CustMac.SelectedRows[0].Cells["sysid"].Value == null) return;
            if (dgv_CustMac.SelectedRows[0].Cells["sysid"].Value.ToString().Trim() == "") return;
            
            //string custid_ = dgv_CustMac.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            string machineid_ = dgv_CustMac.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            
            AddWorkForm awf = new AddWorkForm();
            awf.type = "add";  //新增时传入"客户编码"
            awf.machineid = machineid_;
            awf.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.dgv_worksheet.SelectedRows.Count <= 0) return;
            if (this.dgv_worksheet.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_worksheet.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();

            FormQuerySatisfi fqs = new FormQuerySatisfi();
            fqs.wssysid = sysid_;
            fqs.ShowDialog();
        }

        private void dgv_accept_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow dgr = dgv_accept.Rows[e.RowIndex];
            if (dgr.Cells["状态"].Value.ToString().Trim() == "0")
            {
                dgr.Cells["状态"].Value = "未派";
            }
            else if (dgr.Cells["状态"].Value.ToString().Trim() == "1")
                {
                    dgr.Cells["状态"].Value = "已派";
                }
            else if (dgr.Cells["状态"].Value.ToString().Trim() == "2")
                {
                    dgr.Cells["状态"].Value = "完成";
                }

        }

       

       

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime Dnow = DBUtil.getServerTime().AddDays(-1);
            if (comboBox1.SelectedIndex == 0)
            {
                this.WhereTj_Accept = " where T_CustomerCall.endflag='0'";
            }
            else if (comboBox1.SelectedIndex == 1)
                {
                    this.WhereTj_Accept = " where T_CustomerCall.endflag='1'";
                }
           else if (comboBox1.SelectedIndex == 2)
                {
                    this.WhereTj_Accept = " where T_CustomerCall.endflag='2'"
                        +" and T_worksheet.returndate >='"+Dnow.ToString("yyyy-MM-dd")+"'" ;
                }
          this.show_dgv_accept();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.dgv_worksheet.SelectedRows.Count <= 0) return;
            if (this.dgv_worksheet.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_worksheet.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            FormSetCustCall4Time fs4 = new FormSetCustCall4Time();
            fs4.wssysid_ = sysid_;
            fs4.ShowDialog();
        }

        

        private void button8_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.WhereTj_CustMac = "";
            this.show_dgv_custmac();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.dgv_CustMac.SelectedRows[0].Cells["sysid"].Value == null) return;

            string machsysid = this.dgv_CustMac.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (machsysid == "") return;

            SrvConsumeHist sch = new SrvConsumeHist();
            sch.machineid = machsysid;
            sch.ShowDialog();
        }

       

       
    }
}
