using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Warehouse.DB;
using System.Windows.Forms;

namespace Warehouse.Bargain.calcFee
{
    class BargFee
    {
        //私有成员
        private string DjOrKb;  //单机、捆绑
        private string BdId;    //捆绑编号

        private string BargId;  //合同编号
        private string Hctype;  //幅面(彩色A4,黑白A4...)

        //合同总表参数
        //计费模式：4种(按“基本张数收费”、“按基本金额收费”
        //、“租赁”、“按硒鼓收费”）
        private string FeeType;
        //租赁计算方式：3种（“有免印张数”、“无免印张数”,“有基本张数”）
        private string Jftype;
        //先用，先付
        private string PreChargeMethod;     
        
        private DateTime BargStartDay;   //合同开始日期 
        private DateTime BargEndDay;     //合同终止日期 
        private DateTime FistCzDay;      //首次抄张日期  (判断是否是第一次抄张)

        private int BargPeriod;          //合同月数
        private int HsPeriod;       //核算周期 （每隔多少月核算一次)
        private int CzPeriod;       //抄张周期  （每隔多少月抄张一次)
        private int PrePayPeriod;    //预收费周期 (每隔多少月预收费一次）

        private double BaseMoney;    //基本金额(合同总表中)

        //合同计费方式的参数
        private double BFee1;     //保修期内基本印量内单张收费费率
        private double BFee2;     //保修期外基本印量内单张收费费率
        private double BFee3;     //保修期内基本印量外单张收费费率
        private double BFee4;     //保修期外基本印量外单张收费费率
        private int NumAdd;       //张数递增量
        private double PriceAdd;  //价格递增量
        private int BaseNum;      //基础印量
        private int MianYin;      //免印张数

        //本抄张周期参数
        private DateTime CzFromDay;   //抄张周期的起日期
        private DateTime czToDay;     //抄张周期的止日期
        private DateTime FeeFromDay;  //费用对应的起日
        private DateTime FeeToDay;    //费用对应的止日

        private bool IsInWarranty;    //是否在保修期内
        private bool IsLastCzInHs;   //是否是核算周期的最后一次抄张
        
        private DateTime JsFromDay;  //结算开始日
        private DateTime JsToDay;    //结算终止日
        private DateTime ThisHsFromDay;   //对应核算周期的起始日(9.1改)

        private int InitNum = 0;    //初次读数
        private int CurNum;       //本次抄张数
        private int LastNum;      //上次抄张数

        //************************************************************
        private int OldNum;          //换机时旧机未核算的张数(***换机,抄张周期)
        //************************************************************

        private int FirstNum;        //本核算周期的第一次抄张数
        
        //****************************************************************************
        private int FirstNumOld = 0;   //因换机导致的对核算周期首次抄张数的影响(***换机)
        //****************************************************************************

        // 临时变量，保存递增后的费率
        private double Fee1 = 0;    //保修期内基本印量内单张收费费率
        private double Fee2 = 0;    //保修期外基本印量内单张收费费率
        private double Fee3 = 0;    //保修期内基本印量外单张收费费率
        private double Fee4 = 0;    //保修期外基本印量外单张收费费率

        //暂存计算结果
        private int Num1;       //保修期内基本印量内单张收费"张数"和"金额"
        private double Price1;
        private int Num2;       //保修期外基本印量内单张收费"张数"和"金额"
        private double Price2;
        private int Num3;       //保修期内基本印量外单张收费"张数"和"金额"
        private double Price3;
        private int Num4;       //保修期外基本印量外单张收费"张数"和"金额"
        private double Price4;
        private double ResultMoney;   //求得的基本金额

        private void Clear_Para()
        {
            Num1 = 0;
            Price1 = 0;
            Num2 = 0;
            Price2 = 0;
            Num3 = 0;
            Price3 = 0;
            Num4 = 0;
            Price4 = 0;
            ResultMoney = 0;
        }

        //类型（单机、捆绑）,合同编号，幅面, 核算序号,抄张序号
        public BargFee(string lx, string id_, string Hctype_, DateTime JsFrom, DateTime JsTo)  
        {
            bool cancalcu = false;
            this.Hctype = Hctype_;
            this.JsFromDay = JsFrom;
            this.JsToDay = JsTo;
            if (lx == "单机")
            {
                this.DjOrKb = "单机";
                this.BargId = id_;
                this.BdId = "";
                cancalcu=get_private_para_dj();
            }
            else //绑定机器
            {
                this.DjOrKb = "捆绑";
                this.BargId = "";
                this.BdId = id_;
                cancalcu=get_private_para_bd();
            }
            
            //获取旧机的抄张信息*****
            Get_OldMac_CzInf();
            
             if (this.IsLastCzInHs)
                 Get_OldMac_FirstHsInf();

             this.FirstNum = this.FirstNum - this.FirstNumOld;
             this.CurNum = this.CurNum + this.OldNum;
            //*********************
            if (cancalcu)
                Get_Money();
            else
            {
                MessageBox.Show("合同设置不正确，无法计算费用");
                return;
            }
            
        }


        //*************************************
        //获取旧机需核算的信息
        private void Get_OldMac_FirstHsInf()
        {
            this.FirstNumOld = 0;
            Int32 n1 = 0, n3 = 0;
            DataTable dt, dt1;
            string sql_ = "", sql1_ = "";
            //如是单机
            if (this.DjOrKb == "单机")
            {
                n1 = 0;
                n3 = 0;
                sql_ = "select LastMacCzNum1,LastMacCzNum3 from T_bargFee "
                    + " where barsysid=" + this.BargId
                    + " and changeAtDate>='" + this.ThisHsFromDay.ToString("yyyy-MM-dd") + "'"
                    + " and changeAtDate<='" + this.JsToDay.ToString("yyyy-MM-dd") + "'";
                dt = (new SqlDBConnect()).Get_Dt(sql_);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["LastMacCzNum1"].ToString().Trim() != "")
                        n1 = Convert.ToInt32(dt.Rows[0]["LastMacCzNum1"].ToString().Trim());
                    else
                        n1 = 0;
                   
                    if (dt.Rows[0]["LastMacCzNum3"].ToString().Trim() != "")
                        n3 = Convert.ToInt32(dt.Rows[0]["LastMacCzNum3"].ToString().Trim());
                    else
                        n3 = 0;
                }
                this.FirstNumOld = n3 - n1;
            }
            else   //捆绑的情况
            {
                string sbarid = "";
                sql1_ = "select BarSysid from T_BargBindMacs where Bdid='" + this.BdId + "'";
                dt1 = (new SqlDBConnect()).Get_Dt(sql1_);
                for (int m = 0; m < dt1.Rows.Count; m++)
                {
                    n1 = 0;
                    n3 = 0;
                    sbarid = dt1.Rows[m]["BarSysid"].ToString().Trim();  //系统合同编号
                    sql_ = "select LastMacCzNum1,LastMacCzNum3 from T_bargFee "
                    + " where barsysid=" + sbarid
                    + " and changeAtDate>='" + this.ThisHsFromDay.ToString("yyyy-MM-dd") + "'"
                    + " and changeAtDate<='" + this.JsToDay.ToString("yyyy-MM-dd") + "'";
                    dt = (new SqlDBConnect()).Get_Dt(sql_);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["LastMacCzNum1"].ToString().Trim() != "")
                            n1 = Convert.ToInt32(dt.Rows[0]["LastMacCzNum1"].ToString().Trim());
                        else
                            n1 = 0;
                        
                        if (dt.Rows[0]["LastMacCzNum3"].ToString().Trim() != "")
                            n3 = Convert.ToInt32(dt.Rows[0]["LastMacCzNum3"].ToString().Trim());
                        else
                            n3 = 0;
                    }
                    this.FirstNumOld = this.FirstNumOld + n3 - n1;
                   
                }
            }
        }
        //************************************************
        
        //*************************************
        //获取旧机的相关信息
        private void Get_OldMac_CzInf()
        {
            this.OldNum = 0;
            Int32  n2=0, n3 = 0;
            DataTable dt,dt1;
            string sql_ = "",sql1_="";
            //如是单机
            if (this.DjOrKb == "单机")
            {
                n2 = 0;
                n3 = 0;
                sql_ = "select LastMacCzNum2,LastMacCzNum3 from T_bargFee "
                    + " where barsysid=" + this.BargId
                    + " and changeAtDate>='" + this.JsFromDay.ToString("yyyy-MM-dd") + "'"
                    + " and changeAtDate<='" + this.JsToDay.ToString("yyyy-MM-dd") + "'";
                dt = (new SqlDBConnect()).Get_Dt(sql_);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["LastMacCzNum2"].ToString().Trim() != "")
                        n2 = Convert.ToInt32(dt.Rows[0]["LastMacCzNum2"].ToString().Trim());
                    else
                        n2 = 0;
                    if (dt.Rows[0]["LastMacCzNum3"].ToString().Trim() != "")
                        n3 = Convert.ToInt32(dt.Rows[0]["LastMacCzNum3"].ToString().Trim());
                    else
                        n3 = 0;
                }
               this.OldNum = n3 - n2;
            }
            else   //捆绑的情况
            {
                string sbarid = "";
                sql1_ = "select BarSysid from T_BargBindMacs where Bdid='" + this.BdId + "'";
                dt1 = (new SqlDBConnect()).Get_Dt(sql1_);
                for (int m = 0; m < dt1.Rows.Count; m++)
                {
                    n2 = 0;
                    n3 = 0;
                    sbarid = dt1.Rows[m]["BarSysid"].ToString().Trim();  //系统合同编号
                    sql_ = "select LastMacCzNum2,LastMacCzNum3 from T_bargFee "
                    + " where barsysid=" + sbarid
                    + " and changeAtDate>='" + this.JsFromDay.ToString("yyyy-MM-dd") + "'"
                    + " and changeAtDate<='" + this.JsToDay.ToString("yyyy-MM-dd") + "'";
                    dt = (new SqlDBConnect()).Get_Dt(sql_);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["LastMacCzNum2"].ToString().Trim() != "")
                            n2 = Convert.ToInt32(dt.Rows[0]["LastMacCzNum2"].ToString().Trim());
                        else
                            n2 = 0;
                        if (dt.Rows[0]["LastMacCzNum3"].ToString().Trim() != "")
                            n3 = Convert.ToInt32(dt.Rows[0]["LastMacCzNum3"].ToString().Trim());
                        else
                            n3 = 0;
                    }
                    this.OldNum = this.OldNum+n3 - n2;
                }
            }
        }
        //************************************************

        public void Get_Money()
        {
            Clear_Para();  //清空以前的值

            if (this.FeeType == "按基本张数收费")
            {
                if (HsPeriod == CzPeriod)
                    CalcMoney_HsEquCz();
                else
                    CalcMoney_HsGtCz();
            }
            else if ((this.FeeType == "按基本金额收费")|| (this.FeeType == "租赁"))
            {
                   //if (this.PreChargeMethod == "先用")
                   //先付、先用在计算上无区别，
                   //如果是先付，则还需要收预收费,针对下一个收费周期
                    if (this.Jftype == "有免印张数")
                    {
                      CalcMoney_XianYong_MianYin();
                    }
                    else if (this.Jftype == "有基本张数")
                    {
                        //按《按基本张数收费》
                        if (HsPeriod == CzPeriod)
                            CalcMoney_HsEquCz();
                        else
                            CalcMoney_HsGtCz();
                    }
                    else
                    {
                        CalcMoney_XianYong_AllNo();
                    }
            }
        }

        //*************(单机合同)************
        //获取单机合同的相关系统参数
        //***********************************
        private bool get_private_para_dj()
        {
           string sql_="";
           //获取合同总信息
           sql_ = "select feetype,jftype,startDate,EndDate,Periodgap,checkperiod,CopyNumGap,"
               + "forfee,PreChargeMethod,ForeadNum,firstczdate from T_Bargains"
               + " where sysid=" + this.BargId ;
           DataTable dt;
           dt = (new SqlDBConnect()).Get_Dt(sql_);
           if (dt.Rows.Count <= 0) return false;

           //计费模式(“基本张数收费”、“按基本金额收费”
           //、“租赁”、“按硒鼓收费”）

           try
           {
               this.FeeType = dt.Rows[0]["feetype"].ToString().Trim();

               //租赁计算方式（有免印张数,无免印张数,有基本张数）
               this.Jftype = dt.Rows[0]["Jftype"].ToString().Trim();
               
               //先用、先付
               this.PreChargeMethod = dt.Rows[0]["PreChargeMethod"].ToString().Trim();


               //合同开始日期
               this.BargStartDay = Convert.ToDateTime(dt.Rows[0]["startDate"].ToString().Trim());
               //合同终止日期
               this.BargEndDay = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString().Trim());
               //首次抄张日期
               this.FistCzDay = Convert.ToDateTime(dt.Rows[0]["firstczdate"].ToString().Trim());
               

               //合同的月份数
               this.BargPeriod = Convert.ToInt16(dt.Rows[0]["Periodgap"].ToString().Trim());
               //核算周期
               this.HsPeriod = Convert.ToInt16(dt.Rows[0]["checkperiod"].ToString().Trim());
               //抄张周期
               this.CzPeriod = Convert.ToInt16(dt.Rows[0]["CopyNumGap"].ToString().Trim());

               //预收费周期 
               if (dt.Rows[0]["ForeadNum"].ToString().Trim() != "")
                   this.PrePayPeriod = Convert.ToInt16(dt.Rows[0]["ForeadNum"].ToString().Trim());
               else
                   this.PrePayPeriod = 0;

               //基本金额(合同总表中)
               if (dt.Rows[0]["ForFee"].ToString().Trim() != "")
                   this.BaseMoney = Convert.ToDouble(dt.Rows[0]["ForFee"].ToString().Trim());
               else
                   this.BaseMoney = 0; 
           }
           catch
           {
               MessageBox.Show("合同计费设置有误,请核查!");
               return false;
           }

           //获取合同计费方法
           sql_ = " select Fee1,Fee2,Fee3,Fee4,PageNumAdd,PriceAdd,MyNum,BaseNum "
               + " from T_bargFee where barsysid=" + this.BargId
               + " and hctype='" + this.Hctype + "'";
           dt = (new SqlDBConnect()).Get_Dt(sql_);
           if (dt.Rows.Count <= 0) return false;
           //保修期内基本印量内单张收费费率
           try
           {
               this.BFee1 = Convert.ToDouble(dt.Rows[0]["Fee1"].ToString().Trim());
           }
           catch
           {
               MessageBox.Show("合同未设置[保修期内基本印量内单张收费费率]");
               return false;
           }
           //保修期外基本印量内单张收费费率
           try
           {
               this.BFee2 = Convert.ToDouble(dt.Rows[0]["Fee2"].ToString().Trim());
           }
           catch
           {
               MessageBox.Show("合同未设置[保修期外基本印量内单张收费费率]");
               return false;
           }
           //保修期内基本印量外单张收费费率  
           try
           {
               this.BFee3 = Convert.ToDouble(dt.Rows[0]["Fee3"].ToString().Trim());
           }
           catch
           {
               MessageBox.Show("合同未设置[保修期内基本印量外单张收费费率]");
               return false;
           }
           //保修期外基本印量外单张收费费率   
           try
           {
               this.BFee4 = Convert.ToDouble(dt.Rows[0]["Fee4"].ToString().Trim());
           }
           catch
           {
               MessageBox.Show("合同未设置[保修期外基本印量外单张收费费率]");
               return false;
           }

           //-==================
           double dlt = 0.00000001;
           if ((Math.Abs(this.BFee1) < dlt) && (Math.Abs(this.BFee2) < dlt)
               && (Math.Abs(this.BFee3) < dlt) && (Math.Abs(this.BFee4) < dlt))
           {
               MessageBox.Show("抄张单价未录入!");
               return false;
           }

           //张数递增量  
           if (dt.Rows[0]["PageNumAdd"].ToString().Trim() == "")
               this.NumAdd = 0;
           else
               this.NumAdd = Convert.ToInt32(dt.Rows[0]["PageNumAdd"].ToString().Trim());
           
           if (dt.Rows[0]["PriceAdd"].ToString().Trim() == "")
               this.PriceAdd = 0;
           else
               this.PriceAdd = Convert.ToDouble(dt.Rows[0]["PriceAdd"].ToString().Trim());
           
           //基础印量 
           if (dt.Rows[0]["BaseNum"].ToString().Trim() == "")
               this.BaseNum = 0;
           else
               this.BaseNum = Convert.ToInt32(dt.Rows[0]["BaseNum"].ToString().Trim());
           
           if (dt.Rows[0]["MyNum"].ToString().Trim()=="")
              this.MianYin=0;
           else
              this.MianYin = Convert.ToInt32(dt.Rows[0]["MyNum"].ToString().Trim());
           
           //获取本次抄张数
           sql_ = "select curNum from T_RecordCopy "
               + "where barsysid=" + this.BargId 
               + " and Czfrom='" + this.JsFromDay.ToString("yyyy-MM-dd").Trim()+"'"
               +"  and Czto='" + this.JsToDay.ToString("yyyy-MM-dd").Trim()+"'"
               + " and hctype='" + this.Hctype + "'";
           dt = (new SqlDBConnect()).Get_Dt(sql_);
           if (dt.Rows.Count <= 0) return false;
           this.CurNum = Convert.ToInt32(dt.Rows[0]["curNum"].ToString().Trim());

            //初始读数
           sql_ = "select StartNum from T_BargFee "
                  + " where Barsysid=" + this.BargId
                  + " and HcType='" + this.Hctype + "'";
           if ((new DBUtil()).Get_Single_val(sql_) != "")
               this.InitNum = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));
           else
               this.InitNum = 0;

           

            //获取上期抄张数
           if ( this.JsFromDay.ToString("yyyy-MM-dd").Trim()
                ==this.FistCzDay.ToString("yyyy-MM-dd").Trim()
               )     //如第一次，则是机器的初始读数
           {
               this.LastNum = this.InitNum;
           }
           else
           {
               sql_ = "select LastNum from T_RecordCopy "
                + "where barsysid=" + this.BargId 
                 + " and Czfrom='" + this.JsFromDay.ToString("yyyy-MM-dd").Trim() + "'"
                 + "  and Czto='" + this.JsToDay.ToString("yyyy-MM-dd").Trim() + "'"
                 + " and hctype='" + this.Hctype + "'";
              this.LastNum = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));
           }

           //计算抄张结算周期的起止月
           this.CzFromDay = this.JsFromDay;
           this.czToDay = this.JsToDay;
           
           //计算是否在保修期
           if (this.czToDay < this.BargEndDay)
               this.IsInWarranty = true;
           else
               this.IsInWarranty = false;
            
           //================================================
           //判断是否是核算周期内的最后一次抄张
           //判断“月数差”% "核算月数"的关系
           int dltm = this.CzFromDay.Month - this.FistCzDay.Month;
           int dltyear = this.CzFromDay.Year - this.FistCzDay.Year;
           //月份差+抄张周期 
           int DltMonth = dltyear * 12 + dltm +this.CzPeriod;
           
           if (DltMonth % this.HsPeriod==0)  
              this.IsLastCzInHs=true;
            else
              this.IsLastCzInHs=false;
           //=================================================  

          //取本核算周期内的第一次抄张数 （后序计算使用）
          //*****************************
           Get_First_Num_DJ(this.FistCzDay, this.JsFromDay, this.JsToDay);
          //****************************  
            return true;
        }

        //取该抄张周期对应的核算周期的初次读数（单机）
        private void Get_First_Num_DJ(DateTime FirstCzDay,DateTime Dfrom,DateTime Dto)
        {
            int m = 0;   //记录是第几个核算周期
            DateTime TDay = FirstCzDay;
            if (TDay == Dfrom)
                this.FirstNum = this.InitNum;
            else
            {
                while (TDay <= Dfrom)
                {
                    m = m + 1;
                    TDay = TDay.AddMonths(this.HsPeriod);
                }
                DateTime DFirstCzInHs = FirstCzDay.AddMonths((m-1)* this.HsPeriod).AddDays(-1);

                //本核算周期的起日
                this.ThisHsFromDay = FirstCzDay.AddMonths((m - 1) * this.HsPeriod);

                if (DFirstCzInHs.AddMonths(-1 * this.CzPeriod).AddDays(1) < this.FistCzDay)
                    this.FirstNum = this.InitNum;
                else
                {
                    string sql_ = "select CurNum from T_RecordCopy "
                     + "where barsysid=" + this.BargId
                      + " and Czfrom='" + DFirstCzInHs.AddMonths(-1 * this.CzPeriod).AddDays(1).ToString("yyyy-MM-dd").Trim() + "'"
                      + "  and Czto='" + DFirstCzInHs.ToString("yyyy-MM-dd").Trim() + "'"
                      + " and hctype='" + this.Hctype + "'";
                    string sn = (new DBUtil()).Get_Single_val(sql_);
                    if (sn != "")
                        this.FirstNum = Convert.ToInt32(sn);
                    else
                        this.FirstNum = 0;
                }
            }
       }

        //取该抄张周期对应的核算周期的初次读数（捆绑）
        //换机累计在此考虑
        private void Get_First_Num_KB(DateTime FirstCzDay, DateTime Dfrom, DateTime Dto)
        {
            int m = 0;   //记录是第几个核算周期
            DateTime TDay = FirstCzDay;
            if (TDay == Dfrom)
                this.FirstNum = this.InitNum;
            else
            {
                while (TDay <= Dfrom)
                {
                    m = m + 1;
                    TDay = TDay.AddMonths(this.HsPeriod);
                }
                DateTime DFirstCzInHs = FirstCzDay.AddMonths((m - 1) * this.HsPeriod).AddDays(-1);
                
                //本核算周期的起日
                this.ThisHsFromDay = FirstCzDay.AddMonths((m - 1) * this.HsPeriod);

                if (DFirstCzInHs.AddMonths(-1 * this.CzPeriod).AddDays(1) < this.FistCzDay)
                    this.FirstNum = this.InitNum;
                else
                {
                    string sql_ = "select CurNum from T_RecordCopy "
                      + "where barsysid in "
                      + " (select Barsysid from T_BargBindMacs where Bdid='" + this.BdId + "')"
                      + " and Czfrom='" + DFirstCzInHs.AddMonths(-1 * this.CzPeriod).AddDays(1).ToString("yyyy-MM-dd").Trim() + "'"
                      + "  and Czto='" + DFirstCzInHs.ToString("yyyy-MM-dd").Trim() + "'"
                      + " and hctype='" + this.Hctype + "'";
                    string sn = (new DBUtil()).Get_Single_val(sql_);
                    if (sn != "")
                        this.FirstNum = Convert.ToInt32(sn);
                    else
                        this.FirstNum = 0;
                }
            }
        }


        //********************************
        //获取捆绑合同的相关信息
        //“免印张数”、“基本印张”和“基本金额”
        //“本次印量”、“上期印量” 取数上的差别
        //********************************
        private bool get_private_para_bd()
        {
            //合同信息以捆绑中的第1个合同为准:
            string sql_ = "select barsysid from T_BargBindMacs where Bdid='" + this.BdId + "'";
            string FirstBargId = (new DBUtil()).Get_Single_val(sql_);

            //获取合同总信息
            sql_ = "select feetype,jftype,startDate,EndDate,Periodgap,checkperiod,CopyNumGap,"
                + "forfee,PreChargeMethod,ForeadNum,firstczDate from T_Bargains"
                + " where sysid=" + FirstBargId ;
            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            if (dt.Rows.Count <= 0) return false;
            //计费模式(四种）
            this.FeeType = dt.Rows[0]["feetype"].ToString().Trim();
            //租赁计算方式（有免印张数,无免印张数,有基本张数）
            this.Jftype = dt.Rows[0]["Jftype"].ToString().Trim();
            //先用、先付
            this.PreChargeMethod = dt.Rows[0]["PreChargeMethod"].ToString().Trim();

            try
            {
                //合同开始日期
                this.BargStartDay = Convert.ToDateTime(dt.Rows[0]["startDate"].ToString().Trim());
                //合同终止日期
                this.BargEndDay = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString().Trim());
                //首次抄张日期
                this.FistCzDay = Convert.ToDateTime(dt.Rows[0]["firstczdate"].ToString().Trim());
                
                //合同的月份数
                this.BargPeriod = Convert.ToInt16(dt.Rows[0]["Periodgap"].ToString().Trim());
                //核算周期
                this.HsPeriod = Convert.ToInt16(dt.Rows[0]["checkperiod"].ToString().Trim());
                //抄张周期
                this.CzPeriod = Convert.ToInt16(dt.Rows[0]["CopyNumGap"].ToString().Trim());

                if (dt.Rows[0]["ForeadNum"].ToString().Trim() != "")
                    this.PrePayPeriod = Convert.ToInt16(dt.Rows[0]["ForeadNum"].ToString().Trim());
                else
                    this.PrePayPeriod = 0;
            }
            catch
            {
                MessageBox.Show("合同参数设置有误,请核查!");
                return false;
            }
            
            //获取合同计费方法
            sql_ = " select Fee1,Fee2,Fee3,Fee4,PageNumAdd,PriceAdd,MyNum,BaseNum "
                + " from T_bargFee where Barsysid=" + FirstBargId 
                + " and hctype='" + this.Hctype + "'";
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            if (dt.Rows.Count <= 0) return false;
            //保修期内基本印量内单张收费费率
            try
            {
                this.BFee1 = Convert.ToDouble(dt.Rows[0]["Fee1"].ToString().Trim());
            }
            catch
            {
                MessageBox.Show("合同未设置[保修期内基本印量内单张收费费率]");
                return false;
            }
                //保修期外基本印量内单张收费费率
            try
            {
                this.BFee2 = Convert.ToDouble(dt.Rows[0]["Fee2"].ToString().Trim());
            }
            catch
            {
                MessageBox.Show("合同未设置[保修期外基本印量内单张收费费率]");
                return false;
            }
            //保修期内基本印量外单张收费费率    
            try
            {
                this.BFee3 = Convert.ToDouble(dt.Rows[0]["Fee3"].ToString().Trim());
            }
            catch
            {
                MessageBox.Show("合同未设置[保修期内基本印量外单张收费费率]");
                return false;
            }
            //保修期外基本印量外单张收费费率  
            try
            {
                this.BFee4 = Convert.ToDouble(dt.Rows[0]["Fee4"].ToString().Trim());
            }
            catch
            {
                MessageBox.Show("合同未设置[保修期外基本印量外单张收费费率]");
                return false;
            }
            //-==================
            double dlt=0.00000001;
            if ((Math.Abs(this.BFee1) < dlt) && (Math.Abs(this.BFee2) < dlt)
                && (Math.Abs(this.BFee3) < dlt) && (Math.Abs(this.BFee4) < dlt))
            {
                MessageBox.Show("抄张单价未录入!");
                return false;
            }
            //张数递增量      
            if (dt.Rows[0]["PageNumAdd"].ToString().Trim() == "")
               this.NumAdd = 0;
            else
               this.NumAdd = Convert.ToInt32(dt.Rows[0]["PageNumAdd"].ToString().Trim());
            
            //价格递增量
            if (dt.Rows[0]["PriceAdd"].ToString().Trim() == "")
                this.PriceAdd = 0;
            else
                this.PriceAdd = Convert.ToDouble(dt.Rows[0]["PriceAdd"].ToString().Trim());
            
            //以下从捆绑中去取数量
            //************************************
            //基本金额(从捆绑中取数量)
            sql_ = "select BaseFee from T_BargBind where Bdid='" + this.BdId + "'";
            if ((new DBUtil()).Get_Single_val(sql_) != "")
                this.BaseMoney = Convert.ToDouble((new DBUtil()).Get_Single_val(sql_));
            else
                this.BaseMoney = 0;

            //基础印量(从捆绑中取数)
            sql_ = "select BaseNum from T_BargBindSet where Bdid='" + this.BdId + "'"
                + " and hctype='" + this.Hctype + "'";
            if ((new DBUtil()).Get_Single_val(sql_) != "")
                this.BaseNum = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));
            else
                this.BaseNum = 0;
            
            //免印张数(从捆绑中取数)
            sql_ = "select MyNum from T_BargBindSet where Bdid='" + this.BdId + "'"
                + " and hctype='" + this.Hctype + "'";
            if ((new DBUtil()).Get_Single_val(sql_) != "")
                this.MianYin = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));
            else
                this.MianYin = 0;
            //********************************

            //初始读数
            sql_ = "select StartNum from T_BargFee "
                   +" where barsysid in "
                    + " (select Barsysid from T_BargBindMacs where Bdid='" + this.BdId + "')"
                   + " and HcType='" + this.Hctype + "'";
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.InitNum=0;
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                if (dt.Rows[m]["StartNum"].ToString().Trim() != "")
                    this.InitNum = this.InitNum + Convert.ToInt32(dt.Rows[m]["StartNum"].ToString().Trim());
            }
            

            //获取捆绑中多个机器本次抄张数之和
            sql_ = "select sum(curNum) as TotCurNum from T_RecordCopy "
                + "where barsysid in "
                + " (select Barsysid from T_BargBindMacs where Bdid='" + this.BdId + "')"
                + " and Czfrom='" + this.JsFromDay.ToString("yyyy-MM-dd").Trim() + "'"
                + "  and Czto='" + this.JsToDay.ToString("yyyy-MM-dd").Trim() + "'"
                + " and hctype='" + this.Hctype + "'";
            this.CurNum = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));
            
            //获取上期抄张数（如第一次，则是机器的初始读数)
            if (this.JsFromDay.ToString("yyyy-MM-dd").Trim()
                == this.FistCzDay.ToString("yyyy-MM-dd").Trim()
               )   //如第一次，则是机器的初始读数
           {
                sql_ = "select sum(StartNum) as TotStartNum from T_BargFee "
                    + "where barsysid in "
                    + " (select barsysid from T_BargBindMacs where Bdid='" + this.BdId + "')"
                    + " and HcType='" + this.Hctype + "'";
                this.LastNum = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));
            }
            else
            {
                sql_ = "select sum(LastNum) as TotLastNum from T_RecordCopy "
                + "where barsysid in "
                + " (select Barsysid from T_BargBindMacs where Bdid='" + this.BdId + "')"
                + " and Czfrom='" + this.JsFromDay.ToString("yyyy-MM-dd").Trim() + "'"
                + "  and Czto='" + this.JsToDay.ToString("yyyy-MM-dd").Trim() + "'"
                + " and hctype='" + this.Hctype + "'";
                this.LastNum = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));
            }

            //计算抄张结算周期的起止月
            this.CzFromDay = this.JsFromDay;
            this.czToDay = this.JsToDay;

            //计算是否在保修期
            if (this.czToDay < this.BargEndDay)
                this.IsInWarranty = true;
            else
                this.IsInWarranty = false;


            //================================================
            //判断是否是核算周期内的最后一次抄张
            //判断“月数差”% "核算月数"的关系
            int dltm = this.CzFromDay.Month - this.FistCzDay.Month;
            int dltyear = this.CzFromDay.Year - this.FistCzDay.Year;
            //月份差+抄张周期 
            int DltMonth = dltyear * 12 + dltm + this.CzPeriod;

            if (DltMonth % this.HsPeriod == 0)
                this.IsLastCzInHs = true;
            else
                this.IsLastCzInHs = false;

           
             //取本核算周期内的第一次抄张数 （后序计算使用）
            //*****************************
            Get_First_Num_KB(this.FistCzDay, this.JsFromDay, this.JsToDay);
            //****************************  
            return true;
        }

        private void Update_Fee()
        {
            double DltFee = 0;  //费率的变化量
            //计算张数递增，导致费率的变化情况
            //注意：只有两次张数都在一个递增范围内，费率才增加
            if (NumAdd != 0)
            {
                DltFee = (double)Math.Floor((double)(LastNum / NumAdd)) * PriceAdd;
                Fee1 = BFee1 + DltFee;
                Fee2 = BFee2 + DltFee;
                Fee3 = BFee3 + DltFee;
                Fee4 = BFee4 + DltFee;
            }
            else
            {
                Fee1 = BFee1; 
                Fee2 = BFee2;
                Fee3 = BFee3;
                Fee4 = BFee4;
            }
        }

        //=================================
        //按《基本张数》（核算周期=抄张周期）的情况
        //=================================
        public void CalcMoney_HsEquCz()
        {
            this.FeeFromDay = this.CzFromDay;
            this.FeeToDay = this.czToDay;

            Update_Fee();  //计算"印张递增"导致的"费率变化"
            Int32 NDlt = CurNum - LastNum;   //实际张数
            if (NDlt < BaseNum)     //基础印量内
            {
                if (IsInWarranty)
                {
                    Num1 = BaseNum;
                    Price1 = Fee1;
                }
                else
                {
                    Num2 = BaseNum;
                    Price2 = Fee2;
                }
            }
            else  //基础印量外
            {
                if (IsInWarranty)
                {
                    Num1 = BaseNum;
                    Price1 = Fee1;

                    Num3 = NDlt - BaseNum;
                    Price3 = Fee3;
                }
                else
                {
                    Num2 = BaseNum;
                    Price2 = Fee2;

                    Num4 = NDlt - BaseNum;
                    Price4 = Fee4;
                }
            }
        }
        

        //=======================================
        //按《基本张数》（核算周期>抄张周期)
        //=======================================
        public void CalcMoney_HsGtCz()
        {
            this.FeeFromDay = this.CzFromDay;
            this.FeeToDay = this.czToDay;

            Int32 DltN=0;   //本次抄张数
            Update_Fee();  //计算"印张递增"导致的"费率变化"
            DltN=CurNum-LastNum;

            if ((CurNum-this.FirstNum)<this.BaseNum)  //未到基本印量
            {
                if (IsInWarranty)
                {
                    //保修内基本印量内
                    Num1 = DltN;
                    Price1 = Fee1;
                }
                else
                {
                     //保修外基本印量内
                    Num2 = DltN;
                    Price2 = Fee2;
                }
            
            }
            else
            {
                //如果上期已到了基本印量
                if ((LastNum - this.FirstNum) < this.BaseNum)
                {
                    if (IsInWarranty)
                    {
                        //保修内基本印量内
                        Num1 = this.BaseNum - (LastNum - this.FirstNum);
                        Price1 = Fee1;

                        //保修内基本印量外
                        Num3 = DltN - Num1;
                        Price3 = Fee3;
                    }
                    else
                    {
                        //保修内基本印量内
                        Num2 = this.BaseNum - (LastNum - this.FirstNum);
                        Price2 = Fee2;

                        //保修内基本印量外
                        Num4 = DltN - Num2;
                        Price4 = Fee4;
                    }
                }
                else
                {
                    if (IsInWarranty)
                    {
                        //保修内基本印量外
                        Num3 = DltN;
                        Price3 = Fee3;
                    }
                    else
                    {
                       //保修内基本印量外
                        Num4 = DltN ;
                        Price4 = Fee4;
                    }
                
                }
            }

            
            //如果是“核算周期内的最后一次抄张”，计算是否有“补差”
            if (IsLastCzInHs==true)  
            {
                if ((CurNum - this.FirstNum) < this.BaseNum)
                {
                    if (IsInWarranty)
                    {
                        //保修内基本印量内
                        Num1 = Num1 + (this.BaseNum - (CurNum - this.FirstNum));
                        Price1 = Fee1;
                    }
                    else
                    {
                        Num3 = Num3 + (this.BaseNum - (CurNum - this.FirstNum));
                        Price3 = Fee3;
                    }
                }
            }
        }
        //========================================

        //**************************************
        //《按基本金额》->先用->有免印张数的情况
        //***************************************
        public void CalcMoney_XianYong_MianYin()
           {
               this.FeeFromDay = this.CzFromDay;
               this.FeeToDay = this.czToDay;

            Update_Fee();  //计算"印张递增"导致的"费率变化"
            int Dlt = CurNum - LastNum;  //实际印量
            //------------------------//核算周期=抄张周期
            if (HsPeriod == CzPeriod)
            {
                ResultMoney = BaseMoney;      //基本金额都要收取
                if (Dlt > this.MianYin)   //大于免印张数,还同时要收超印费
                {
                    if (IsInWarranty)
                    {
                        Num3 = Dlt - this.MianYin;
                        Price3 = Fee3;
                    }
                    else
                    {
                        Num4 = Dlt - this.MianYin;
                        Price4= Fee4;
                    }
                }

            }  //----------------核算周期>抄张周期
            else
            {
                ResultMoney = BaseMoney;   //基本金额每次都要收取
                if (IsLastCzInHs)  //如果是核算周期内的最后一次抄张
                {
                    //印张数Dlt计算：本核算周期内最末一次抄张数 - 核算周期第一次的抄张数
                    if ((CurNum - FirstNum) > MianYin)
                    {
                        this.FeeFromDay = this.ThisHsFromDay;
                        this.FeeToDay = this.czToDay;
                        if (IsInWarranty)
                        {
                            Num3 = CurNum - FirstNum - MianYin;
                            Price3 = Fee3;
                        }
                        else
                        {
                            Num4 = CurNum - FirstNum - MianYin;
                            Price4 = Fee4;
                        }
                    }
                }
            }
        }

       //****************************************************
        //《按基本金额》-> 先用 ->无基本印量、无免印张数的情况
        //****************************************************
        public void CalcMoney_XianYong_AllNo()
        {
            this.FeeFromDay = this.CzFromDay;
            this.FeeToDay = this.czToDay;

            //计算费率的变化
            Update_Fee();
            int Dlt = CurNum - LastNum;  //实际印量

            if (IsInWarranty)
            {
                Num3= CurNum - LastNum;
                Price3 = Fee3;
            }
            else
            {
                Num4 = CurNum - LastNum;
                Price4= Fee4;
            }
        }

        //收下一周期的预付费
        public string Get_Insert_SQL_PayBefore()
        {
            DateTime YsFrom, YsTo;
            string sql_ = "";
            double YsMoney = 0;
            if (this.PreChargeMethod==null)  return "";
            if (this.PrePayPeriod == 0) return "";

            YsMoney = this.BaseMoney; //预收金
           
            //不在预付周期
            int dltm = this.czToDay.Month - this.FistCzDay.Month;
            int dlty = this.czToDay.Year - this.FistCzDay.Year;
            int dltMonth = dlty * 12 + dltm;
            if (dltMonth % this.PrePayPeriod != 0) return "";

            //先付和先用区别: ”费用周期“不一致
            if (this.PreChargeMethod.Trim() == "先付")
            {
                YsTo = this.czToDay.AddMonths(this.PrePayPeriod);
                YsFrom = YsTo.AddMonths(-1 * this.PrePayPeriod).AddDays(1);
            }
            else
            {
                YsTo = this.czToDay;
                YsFrom = YsTo.AddMonths(-1 * this.PrePayPeriod).AddDays(1);
            }

            //抄过合同终止日的预收费，不再收
            if (YsFrom >= this.BargEndDay) return "";   //超过合同终止日，则不再预收费  

            sql_ = " insert into T_MacSettle(lx,BargOrBd_Id,"
                    + "FeeFrom,Feeto,Czfrom,Czto,moneytype,totMoney)"
                    + " values('{0}','{1}',"
                    + "'{2}','{3}','{4}','{5}','周期基本金额',{6})";
                if (this.DjOrKb == "单机")
                {
                    sql_ = string.Format(sql_, this.DjOrKb, this.BargId,
                        YsFrom, YsTo,this.CzFromDay,this.czToDay,YsMoney);
                }
                else
                {
                    sql_ = string.Format(sql_, this.DjOrKb, this.BdId,
                        YsFrom, YsTo, this.CzFromDay, this.czToDay, YsMoney);
                }
            return sql_;
        }


        //得到插入的SQL语句
        public void Get_Insert_SQL(ref string sql1,
                                   ref string sql2,
                                   ref string sql3,
                                   ref string sql4,
                                   ref string sql5
                                    )
        {
            sql1 = "";
            sql2 = "";
            sql3 = "";
            sql4 = "";
            sql5 = "";
            
            string sql_ = "";
            sql_ = " insert into T_MacSettle(lx,BargOrBd_Id,Hctype,"
                    + "FeeFrom,FeeTo,Czfrom,Czto,moneytype,Num,Fee,totMoney)"
                    + " values('{0}','{1}','{2}',"
                    + "'{3}','{4}','{5}','{6}','{7}',{8},{9},{10})";

            string sid = "";
            if (this.DjOrKb == "单机")
                sid = this.BargId;
            else
                sid = this.BdId;

            string moneytype_ = "";
            double jcPrice=0,jctot=0;
            int jcNum=0;
            if ((this.Num1!=0) && (this.Price1!=0))
            {
                moneytype_="保内基本印量内";
                jcNum=this.Num1;
                jcPrice=this.Price1;
                jctot = jcNum * jcPrice;
                sql1 = string.Format(sql_, this.DjOrKb,sid, this.Hctype,
                    this.FeeFromDay, this.FeeToDay, this.CzFromDay, this.czToDay, moneytype_,
                     jcNum, jcPrice, jctot);
            }
 
            if ((this.Num2!=0) && (this.Price2!=0))
            {
                moneytype_ = "保外基本印量内";
                jcNum = this.Num2;
                jcPrice = this.Price2;
                jctot = jcNum * jcPrice;
                sql2 = string.Format(sql_, this.DjOrKb, sid, this.Hctype,
                    this.FeeFromDay, this.FeeToDay, this.CzFromDay, this.czToDay, moneytype_,
                     jcNum, jcPrice, jctot);

            }

            if ((this.Num3!=0) && (this.Price3!=0))
            {
                moneytype_ = "保内基本印量外";
                jcNum = this.Num3;
                jcPrice = this.Price3;
                jctot = jcNum * jcPrice;
                sql3 = string.Format(sql_, this.DjOrKb, sid, this.Hctype,
                    this.FeeFromDay, this.FeeToDay, this.CzFromDay, this.czToDay, moneytype_,
                     jcNum, jcPrice, jctot);
            }
            
            if ((this.Num4!=0) && (this.Price4!=0))
            {
                moneytype_ = "保外基本印量外";
                jcNum = this.Num4;
                jcPrice = this.Price4;
                jctot = jcNum * jcPrice;
                sql4 = string.Format(sql_, this.DjOrKb, sid, this.Hctype,
                    this.FeeFromDay, this.FeeToDay, this.CzFromDay, this.czToDay, moneytype_,
                     jcNum, jcPrice, jctot);
            }
            //if (this.ResultMoney!=0)
            //{
            //    moneytype_="周期基本金额";
            //    jcNum=0;
            //    jcPrice=0;
            //    jctot=this.ResultMoney;
            //    sql5 = string.Format(sql_, this.DjOrKb, sid, this.Hctype,
            //        this.CzFromDay, this.czToDay, this.CzFromDay, this.czToDay, moneytype_,
            //         jcNum, jcPrice, jctot);
            //}
            
        }

     
    }
}
