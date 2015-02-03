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

namespace Warehouse.Customer
{
    public partial class FormBeginCustCall : Form
    {
        public string CallSysid_ = "";  //召唤单编号(传入)
        
        private string cmsysid = "";  //机器编号（系统自编）
        private string machineid = "";
        
        public FormBeginCustCall()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormBeginCustCall_Load(object sender, EventArgs e)
        {
            DBUtil dbu = new DBUtil();
            string sql_ = "select Cmsysid from T_CustomerCall where sysid=" + CallSysid_;
            this.cmsysid = dbu.Get_Single_val(sql_);   //机器系统编号
            if (this.cmsysid == "")  this.Close();

            this.t_Mdepart.Text = dbu.Get_Single_val("T_CustomerMac", "Mdepart", "sysid", this.cmsysid);
            this.machineid = this.cmsysid;

            //根据Cmsysid得到<合同信息>//得到合同类别
            sql_ = "select ContractType from T_Bargains where MachineId=" + this.machineid;
            this.TbargainType.Text = dbu.Get_Single_val(sql_);

            //根据合同编号查找其他信息
            sql_="select * from T_customerCall where sysid="+this.CallSysid_;
            (new InitFuncs()).ShowDatas(this.panel1, sql_);
            
            t_WorkType.Text = (new DBUtil()).Get_Single_val("T_worktype", "wcname", "wcid", s_WorkType.Text.Trim());
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定派工吗？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //修改召唤单的状态
                 this.n_endflag.Text="1";   //已派
                 string sql1_ =  (new InitFuncs()).Build_Update_Sql(this.panel1, "T_CustomerCall", " where sysid=" + this.CallSysid_);
                
                // //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
                //string SqlUpdateBillRull = "";
                //string wscode_ = DBUtil.Produce_Bill_Id("WX", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull);

                 //产生新工单(插入s_Technician1为空错)
                // string sql2_ = " insert into t_worksheet(RepairTech,CallBillSysId,custid,startTime) values('{0}',{1},'{2}','{3}')";
                // sql2_=string.Format(sql2_, s_Technician1.Text.Trim(), this.sysid_,this.s_CustCode.Text.Trim(), DBUtil.getServerTime().ToString().Trim());
                 
                //向派工单中插入一条数据（还需插入其他字段）
                 string  CallDate_="" ;
                 string callDay_="", CallTime_="";
                 callDay_ = (new DBUtil()).Get_Single_val("T_CustomerCall", "CallDate", "sysid", this.CallSysid_);
                 callDay_ = Convert.ToDateTime(callDay_).Date.ToString("yyyy-MM-dd");

                 CallTime_ = (new DBUtil()).Get_Single_val("T_CustomerCall", "CallTime", "sysid", this.CallSysid_);
                 CallTime_=Convert.ToDateTime(CallTime_).ToShortTimeString();

                 //获取报修内容，故障详细等
                 string Teltrbtype_ = "", probcode_ = "", AcceptMemo_="";
                 string sql_ = "select Teltrbtype,probcode,AcceptMemo from T_customerCall "
                     + " where sysid=" + this.CallSysid_;
                 (new SqlDBConnect()).Get_Multi_Vals(sql_, ref Teltrbtype_, ref probcode_, ref AcceptMemo_);

                 //AcceptMemo_写入到故障详细
                 //写入： 召唤单号，CustId， machineId等信息
                 string sql2_ = " insert into t_worksheet(CallBillSysId,"
                 + "custid,machineId,orderDate,orderTime,"
                 +"RepairContent,TrblDetail,"
                 + "mtype,RepairTech,startTime) values({0},"
                 + "'{1}',{2},'{3}','{4}',"
                 + "'{5}','{6}',"
                 + "'{7}','{8}','{9}')";

                 sql2_ = string.Format(sql2_, this.CallSysid_, 
                     this.s_CustCode.Text.Trim(),this.machineid,callDay_,CallTime_,
                     Teltrbtype_,AcceptMemo_,
                     this.s_mtype.Text.Trim(), this.s_Technician1.Text.Trim(),
                     DBUtil.getServerTime().ToString().Trim());
               
                //使用事务去执行SQL语句
                 List<string> SqlLst = new List<string>();
                 SqlLst.Add(sql1_);
                 SqlLst.Add(sql2_);
                 
                try
                {
                    (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                    this.DialogResult = DialogResult.OK;
                }
                catch
                { 
                }
            }
        }

        private void FormBeginCustCall_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
