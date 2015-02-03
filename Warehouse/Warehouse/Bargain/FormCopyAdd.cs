using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.Bargain.calcFee;
using Warehouse.Base;

namespace Warehouse.Bargain
{
    public partial class FormCopyAdd : Form
    {
        public string type;    //新增add, 修改edit
        
        //public string Bargid_;  //合同编号
        public string barsysid_;  //合同系统编号

        private int HsPeriod, CzPeriod;

        public string Mtype_;    //机型
        public string ManufactCode_;  //机号
        public string hctype_;   //幅面
        
        public string curUser_;  //当前操作用户

        public string CzFrom, CzTo;    //抄张周期起始日期
        
        public int MaxCzOrder_;  //一个核算周期最多有几次抄张

        DateTime BargStartDay, PlanCzDay;  

        private Int32 LastNum_;  //上次抄张数
        private string  LastDay_;  //上次抄张日
        
        public FormCopyAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void get_bargain_inf()
        {
            string sql_ = "";
            //"获取核算周期" hs_period
            sql_ = "select checkperiod from T_Bargains where sysid=" + this.barsysid_;
            int hs_period = Convert.ToInt16((new DBUtil()).Get_Single_val(sql_));
            this.HsPeriod = hs_period;

            //获取"抄张周期" cz_period
            sql_ = "select CopyNumGap from T_Bargains where sysid=" + this.barsysid_;
            int cz_period = Convert.ToInt16((new DBUtil()).Get_Single_val(sql_));
            this.CzPeriod = cz_period;

            this.MaxCzOrder_ = hs_period / cz_period;

            //获取"合同初始日期" 
            sql_ = "select startDate from T_Bargains where sysid=" + this.barsysid_;
            string sStr = (new DBUtil()).Get_Single_val(sql_);
            DateTime Day_ = Convert.ToDateTime(sStr);
            this.BargStartDay = Day_;

            //获取"计划抄张日期" 
            sql_ = "select Firstczdate from T_Bargains where sysid=" + this.barsysid_;
            sStr = (new DBUtil()).Get_Single_val(sql_);
            Day_ = Convert.ToDateTime(sStr);
            this.PlanCzDay = Day_;
      
        }

        private void FormCopyAdd_Load(object sender, EventArgs e)
        {
            //获取合同的相关信息
            get_bargain_inf();
            //限制值类型文本框的输入 
            (new InitFuncs()).Num_limited(this.panel1);

            this.s_mtype.Text = this.Mtype_;
            this.s_manufactcode.Text = this.ManufactCode_;
            this.n_barsysid.Text = this.barsysid_;

            t_bargid.Text = (new DBUtil()).Get_Single_val("T_bargains", "Bargid", "sysid", this.barsysid_);
               
            this.s_hctype.Text = this.hctype_;

            this.s_czFrom.Text = this.CzFrom;
            this.s_CzTo.Text = this.CzTo;

            //预计抄张日期
            TimeSpan dltt=this.PlanCzDay-this.BargStartDay;
            int dltDays=dltt.Days;

            this.s_CurDate.Value = (Convert.ToDateTime(CzFrom)).AddMonths(this.CzPeriod).AddDays(dltDays);

            
            string sql_ = "";
            if (this.type == "add")   //新增的情况
            {
                //获取上次抄张日期和读数
                string LastF=(Convert.ToDateTime(CzFrom).AddMonths(-1*this.CzPeriod)).ToString("yyyy-MM-dd");
                string LastT=(Convert.ToDateTime(CzFrom).AddDays(-1)).ToString("yyyy-MM-dd");
                sql_ = "select curDate,curNum from T_RecordCopy "
                               + " where barsysid=" + this.barsysid_
                               + " and mtype='" + this.Mtype_
                               + "' and manufactcode='" + this.ManufactCode_
                               + "' and hctype='" + this.hctype_ + "'"
                               + " and Czto='" + LastT + "' and CzFrom='" + LastF + "'";
                DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
                if (dt.Rows.Count > 0)
                {
                    this.LastNum_ = Convert.ToInt32(dt.Rows[0]["curNum"].ToString().Trim());
                    this.LastDay_ = Convert.ToDateTime(dt.Rows[0]["curDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                }
                else //第一次抄张
                {
                    sql_ = "select StartNum from T_BargFee where barsysid=" + this.barsysid_ 
                        + " and hctype='" + this.hctype_ + "'";
                    this.LastNum_ = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));
                    this.LastDay_ = this.BargStartDay.ToString("yyyy-MM-dd");
                }
                
                n_lastNum.Text = this.LastNum_.ToString().Trim();
                s_lastDate.Text = this.LastDay_;
                ////
                
                s_occurTime.Text = DBUtil.getServerTime().ToShortDateString();
                s_operaUser.Text = this.curUser_;
            }
            else   //修改
            {
                sql_ = "select * from T_RecordCopy where barsysid=" + this.barsysid_
                    + " and hctype='" + this.hctype_ + "'"
                    + " and mtype='" + this.Mtype_ + "'"
                    + " and manufactcode='" + this.ManufactCode_
                    + "' and hctype='" + this.hctype_
                    + "' and czFrom='" + Convert.ToDateTime(this.CzFrom).ToString("yyyy-MM-dd")
                    + "' and czto='" + Convert.ToDateTime(this.CzTo).ToString("yyyy-MM-dd")+"'";
                (new InitFuncs()).ShowDatas(this.panel1, sql_);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_="";
            string swhere = "";

            if (Util.ControlTextIsNUll(this.n_CurNum))
            {
                MessageBox.Show("请输入抄张数！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.n_CurNum.Focus();
                return;
            }
            //验证此次的输入应该比该机抄张的最大值大
            if (this.s_CurDate.Value <= Convert.ToDateTime(s_lastDate.Text.ToString().Trim()))
            {
                MessageBox.Show("错误，本次抄张日期小于上次抄张日期!");
                return;
            }

            if (Convert.ToInt32(this.n_CurNum.Text) < Convert.ToInt32(this.n_lastNum.Text))
            {
                MessageBox.Show("错误，本次张数小于上次张数");
                return;
            }
            
            
            //抄张日期大于合同的终止日期 ??需要验证吗？

            if (this.type == "add")
            {
                sql_ = "select CurNum from T_RecordCopy "
                  + "where barsysid=" + this.barsysid_
                  + " and mtype='" + this.Mtype_
                  + "' and manufactcode='" + this.ManufactCode_
                  + "' and hctype='" + this.hctype_
                  + "' and CzFrom='" + this.CzFrom
                  + "' and czTo='" + this.CzTo+"'";
                if ((new DBUtil()).yn_exist_data(sql_))
                {
                    MessageBox.Show("该抄张周期已有抄张记录!");
                    return;
                }
                sql_ = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_RecordCopy");
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(sql_);
                    this.DialogResult = DialogResult.OK;
                }
                catch
                { }
            }
            else
            {
                List<string> SqlLst = new List<string>(); 
                  //修改本次抄张
                 swhere="where barsysid=" + this.barsysid_
                  + " and mtype='" + this.Mtype_
                  + "' and manufactcode='" + this.ManufactCode_
                  + "' and hctype='" + this.hctype_
                  + "' and CzFrom='" + this.CzFrom
                  + "' and czTo='" + this.CzTo + "'";
                
                sql_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_RecordCopy",swhere);
                 SqlLst.Add(sql_);

                 //修改下次抄张的"上次抄张数"
                 DateTime NextFrom = (Convert.ToDateTime(CzFrom)).AddMonths(this.CzPeriod);
                 DateTime NextTo = (Convert.ToDateTime(CzTo)).AddMonths(this.CzPeriod);
                 sql_="select lastNum from T_RecordCopy where barsysid=" + this.barsysid_
                  + " and mtype='" + this.Mtype_
                  + "' and manufactcode='" + this.ManufactCode_
                  + "' and hctype='" + this.hctype_
                  + "' and Czfrom='" + NextFrom.ToString("yyyy-MM-dd")
                  + "' and czto='" + NextTo.ToString("yyyy-MM-dd")+"'";

                bool isexist = (new DBUtil()).yn_exist_data(sql_);
                if (isexist)
                { 
                   sql_="update T_RecordCopy set lastNum="+this.n_CurNum.Text.ToString().Trim()
                        +" where barsysid=" + this.barsysid_
                        + " and mtype='" + this.Mtype_
                        + "' and manufactcode='" + this.ManufactCode_
                        + "' and hctype='" + this.hctype_
                        + "' and Czfrom='" + NextFrom.ToString("yyyy-MM-dd")
                        + "' and czto='" + NextTo.ToString("yyyy-MM-dd") + "'";

                   SqlLst.Add(sql_);
                }
                try
                {
                    (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                    this.DialogResult = DialogResult.OK;
                }
                catch
                { }

            }
            

            //try
            //{
            //      //保存单据修改信息
            //      (new SqlDBConnect()).ExecuteNonQuery(sql_);
            //      //费用计算及保存 ===========
            //      List<string> SqlLst = new List<string>();
            //      //1)清除之前计费
            //       sql_ = "delete from T_MacSettle where cmsysid="
            //        + this.cmsysid_ + " and hctype='" + this.hctype_
            //        + "' and hsxh=" + this.hsxh_.ToString().Trim()
            //        + " and czxh=" + this.czxh_.ToString().Trim();
            //      SqlLst.Add(sql_);
            //      BargFee bf = new BargFee(this.Bargid_, this.hctype_, this.cmsysid_,
            //                              this.hsxh_, this.czxh_);
            //      //2)计算基本费用
            //      string sql1_ = "", sql2_ = "", sql3_ = "", sql4_ = "", sql5_ = "";
            //      bf.Get_Insert_SQL(ref sql1_, ref sql2_, ref sql3_, ref sql4_, ref sql5_);
            //      if (sql1_.Trim() != "") SqlLst.Add(sql1_);
            //      if (sql2_.Trim() != "") SqlLst.Add(sql2_);
            //      if (sql3_.Trim() != "") SqlLst.Add(sql3_);
            //      if (sql4_.Trim() != "") SqlLst.Add(sql4_);
            //      if (sql5_.Trim() != "") SqlLst.Add(sql5_);

            //      //3）计算预收费
            //      sql_ = bf.Get_Insert_SQL_PayBefore();
            //      if (sql_.Trim()!="") SqlLst.Add(sql_);

            //      (new SqlDBConnect()).Exec_Tansaction(SqlLst);
            //     //=======================
            //     this.DialogResult = DialogResult.OK;
            //}
            //catch
            //{
            //    MessageBox.Show("不能完成抄张费用计算，请检查合同设置！"
            //        +"\r\t"+"或手工计算后,通过[新增结算明细]的方式填入!");
            //    return;
            //}

        }

        

        private void FormCopyAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
