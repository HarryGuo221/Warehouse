using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DAO;
using Warehouse.DB;
using Warehouse.Base;
using Warehouse.Customer;

namespace Warehouse.Stock
{
    public partial class AddWorkForm : Form
    {
        public string type;  //新增add ,修改edit
        public string sysid_="";     //传入的维修工单系统编号(修改时)
        public string machineid;   //传入的机器编号

        private string custid_ = "";  //客户编码

        

        private string callid_ = "";  //召唤单编号
        public AddWorkForm()
        {
            InitializeComponent();
        }
        
        public delegate void AddWorkFormChange();
        public event AddWorkFormChange addWorkFormChange_;
      
        #region //通过回车转Tab的方法(先把Form的Keypreview事件设为True)
        private void AddWorkForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
        #endregion

        private void Show_CustCall(string callid)
        {
            string sql_ = "";
            sql_ = "select * from T_CustomerCall where sysid=" + callid_;
            (new InitFuncs()).ShowDatas(this.panelWorkSheet, sql_);
            ////根据召唤单--> bargid-->合同类型(有问题)
            //string bargid_ = (new DBUtil()).Get_Single_val("T_CustomerCall", "BargId", "sysid", this.callid_);
            //if (bargid_.Trim() != "")
            //{
            //    this.lb_bargainType.Text = "合同类别：" + (new DBUtil()).Get_Single_val("T_Bargains", "ContractType", "bargid", bargid_);
            //    this.lb_xysj.Text = "响应时间：" + (new DBUtil()).Get_Single_val("T_Bargains", "ResponseHour", "bargid", bargid_);
            //}
            //else
            //{
            //    this.lb_bargainType.Text = "";
            //    this.lb_xysj.Text = "";
            //}
        }
      
        private void AddWorkForm_Load(object sender, EventArgs e)
        {
            //限制值类型文本框的输入 
            (new InitFuncs()).Num_limited(this.panel1);

            this.lb_bargainType.Text = "";
            this.lb_xysj.Text = "";
            string sql_ = "";

            //有码数据初始化
            InitFuncs inf = new InitFuncs();
            inf.InitComboBox(this.t_workResult, "TB_workResult", "repairstatusname");  //行动结果
            inf.InitComboBox(this.t_ServiceType, "tb_maintetype", "maintetypename");  //服务类别
            inf.InitComboBox(this.t_WorkType, "T_WorkType", "wcName");  //工作类别
            inf.InitComboBox(this.t_WorkContent, "T_WorkType", "wcName");  //工作内容

            // 无码数据初始化
            inf.InitComboBox(this.s_wsStyle, "[派工]方式");
            inf.InitComboBox(this.s_ActionResults, "[派工]行动结果");
            inf.InitComboBox(this.s_UrgentDegree, "[召唤]紧急程度");
            inf.InitComboBox(this.s_CallType, "[召唤]召唤类型");
            inf.InitComboBox(this.s_wsStyle, "[召唤]方式");

            inf.InitComboBox(this.s_ReportAttach, "[派工]报告附表");
            inf.InitComboBox(this.s_ClearItem, "[派工]清洁项目");
            inf.InitComboBox(this.s_ClearResult, "[派工]清洁结果");

            inf.InitComboBox(this.s_Becauseof1, "[派工]取消原因");
            inf.InitComboBox(this.s_Becauseof2, "[派工]原因备注");
            //查询受理单编号
            this.callid_ = (new DBUtil()).Get_Single_val("T_worksheet", "CallBillSysId", "sysid", this.sysid_);
            
            //显示受理单的相关内容（有受理单的情况）
            if (this.callid_.Trim() != "")
            {
                // 合同:无码数据初始化
                this.panelWorkSheet.Visible = true;
                this.lb_zhd.Visible = true;
                this.n_CallBillSysId.Visible = true;
                
                //显示召唤单
                Show_CustCall(this.callid_);

                t_CustCode.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "custname", "custid", s_CustCode.Text.Trim());
                this.s_CustId.Text = this.s_CustCode.Text.Trim();
                //
                t_WorkType.Text = (new DBUtil()).Get_Single_val("T_worktype", "wcname", "wcid", s_WorkType.Text.Trim());
            }
            else
            {
                // 合同:无码数据初始化
                this.panelWorkSheet.Visible = false;
                this.lb_zhd.Visible = false;
                this.n_CallBillSysId.Visible = false;
                this.Height = 325;
            }

            return;

            if (type == "add")
            {
                n_machineid.Text = this.machineid;
                //获取客户编号
                this.custid_ = (new DBUtil()).Get_Single_val("t_CustomerMac","custid","sysid",this.machineid);
                this.s_mtype.Text = (new DBUtil()).Get_Single_val("t_CustomerMac", "mtype", "sysid", this.machineid);
                s_CustCode.Text = this.custid_;
                s_CustId.Text = this.custid_;
                t_CustCode.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "custname", "custid", this.custid_);
                s_wsCode.Focus();
            }
            else
            {
                sql_ = "select * from T_workSheet where sysid=" + this.sysid_;
                inf.ShowDatas(this.panel1, sql_);
                this.machineid = this.n_machineid.Text.Trim();
                //查找或获取关联值
                t_CustCode.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "custname", "custid", s_CustCode.Text.Trim());
                this.t_workResult.Text = (new DBUtil()).Get_Single_val("TB_workResult", "repairstatusname", "repairstatuscode", this.s_workResult.Text.Trim());
                this.t_WorkContent.Text = (new DBUtil()).Get_Single_val("T_WorkType", "wcName", "wcid", this.s_WorkContent.Text.Trim());
                this.t_ServiceType.Text = (new DBUtil()).Get_Single_val("tb_maintetype", "maintetypename", "maintetypecode", this.s_ServiceType.Text.Trim());
       
                //
                if (s_AffectiveFlg.Text.ToLower().Trim() == "false")
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
            }
       }
 

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void butsave_Click_1(object sender, EventArgs e)
        //{
        //    string strSql = "";
        //    InitFuncs inf = new InitFuncs();
        //    if (Util.ControlTextIsNUll(this.s_wsCode))
        //    {
        //        MessageBox.Show("工单编号不能为空!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }
            
        //    //得到SQL语句
        //    if (type == "add")
        //    {
        //        //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
        //        string SqlUpdateBillRull = "";
        //        s_wsCode.Text = DBUtil.Produce_Bill_Id("WX", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull);
                
        //        //
        //        strSql = inf.Build_Insert_Sql(this.panelWorkSheet, "T_WorkSheet");
                
        //        //使用事务执行多条SQL语句
        //        try
        //        {

        //            List<string> SqlLst = new List<string>();
        //            SqlLst.Add(strSql);
        //            SqlLst.Add(SqlUpdateBillRull);

        //            (new SqlDBConnect()).Exec_Tansaction(SqlLst);
        //            addWorkFormChange(); //激活代理事件
        //            this.DialogResult = DialogResult.OK;
        //        }
        //        catch (Exception w)
        //        {
        //            s_wsCode.Text = "";
        //            MessageBox.Show(w.ToString());
        //        }
        //    }
        //    else  //修改
        //    {
        //        strSql = inf.Build_Update_Sql(this.panelWorkSheet, "T_WorkSheet", " where wsCode='" + this.s_wsCode.Text.Trim() + "'");
        //        try
        //        {
        //            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        //            addWorkFormChange(); //激活代理事件
        //            this.DialogResult = DialogResult.OK;
        //       }
        //        catch (Exception w)
        //        {
        //            MessageBox.Show(w.ToString());
        //        }
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                s_AffectiveFlg.Text = "0";
            else
                s_AffectiveFlg.Text = "1";
        }

        private void butsave_Click(object sender, EventArgs e)
        {
            if (Util.ControlTextIsNUll(this.s_wsCode))
            {
                MessageBox.Show("请输入工单编号！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DBUtil dbUtil = new DBUtil();

            List<string> SqlLst = new List<string>();
                   
            //数据处理
            string strSql = "";
            InitFuncs initFuncs = new InitFuncs();
            try
            {
                if (this.type == "add")
                {
                    string sql = "select * from T_worksheet where wsCode='" + this.s_wsCode.Text.Trim() + "'";
                    bool ynExistID = dbUtil.yn_exist_data(sql);
                    if (ynExistID == true)
                    {
                        MessageBox.Show("该工单号已存在！");
                        s_wsCode.Focus();
                        return;
                    }
                    strSql = initFuncs.Build_Insert_Sql(this.panel1, "T_worksheet");
                }
                else if (this.type == "edit")
                {
                    strSql = initFuncs.Build_Update_Sql(this.panel1, "T_worksheet", " where Sysid=" + this.sysid_);
                }

                //执行SQL
                try
                 {
                     //保存工单
                     SqlLst.Add(strSql);
                     //有对应召唤单的情况下，修改召唤单的状态为返回
                     if (this.n_CallBillSysId.Text.Trim() != "")
                     {
                         strSql = "update T_CustomerCall set endflag='2' where sysid="
                             + this.n_CallBillSysId.Text;
                         SqlLst.Add(strSql);
                     }
                     //
                        (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                         MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         addWorkFormChange_();
                         this.DialogResult = DialogResult.OK;
                     
                }
                catch (Exception ew)
                {
                  //  MessageBox.Show(ew.ToString());
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
       


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_ = "",sql2_="";
            if (MessageBox.Show("确认重新受理吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                string CallDate_ = "";
                string callDay_ = "", CallTime_ = "";
                callDay_ = (new DBUtil()).Get_Single_val("T_CustomerCall", "CallDate", "sysid", this.callid_);
                callDay_ = Convert.ToDateTime(callDay_).Date.ToString("yyyy-MM-dd");

                CallTime_ = (new DBUtil()).Get_Single_val("T_CustomerCall", "CallTime", "sysid", this.callid_);
                CallTime_ = Convert.ToDateTime(CallTime_).ToShortTimeString();

                //获取报修内容，故障详细等
                string Teltrbtype_ = "", probcode_ = "", AcceptMemo_ = "";
                sql_ = "select Teltrbtype,probcode,AcceptMemo from T_customerCall "
                    + " where sysid=" + this.callid_;
                (new SqlDBConnect()).Get_Multi_Vals(sql_, ref Teltrbtype_, ref probcode_, ref AcceptMemo_);

                //写入： 召唤单号，CustId， machineId等信息
                sql2_ = " insert into t_worksheet(CallBillSysId,"
                + "custid,machineId,orderDate,orderTime,"
                + "RepairContent,TrblDetail,"
                + "mtype,RepairTech,startTime) values({0},"
                + "'{1}',{2},'{3}','{4}',"
                + "'{5}','{6}',"
                + "'{7}','{8}','{9}')";

                sql2_ = string.Format(sql2_, this.callid_,
                    this.s_CustCode.Text.Trim(), this.machineid, callDay_, CallTime_,
                    Teltrbtype_, probcode_,
                    this.s_mtype.Text.Trim(), this.s_Technician1.Text.Trim(),
                    DBUtil.getServerTime().ToString().Trim());

                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(sql2_);
                    MessageBox.Show("重新派工成功!");
                    return;
                }
                catch(Exception w)
                {
                }

            }
        }

        private void t_ServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_ServiceType.Text = (new DBUtil()).Get_Single_val("tb_maintetype", "maintetypecode", "maintetypename", this.t_ServiceType.Text.Trim());
        }

        private void t_workResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_workResult.Text = (new DBUtil()).Get_Single_val("TB_workResult", "repairstatuscode", "repairstatusname", this.t_workResult.Text.Trim());
       
        }

        private void t_WorkContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_WorkContent.Text = (new DBUtil()).Get_Single_val("T_WorkType", "wcid", "wcName", this.t_WorkContent.Text.Trim());
        }

       
        
    }
}
  