using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using grproLib;
using System.Data;
using Warehouse.Base;
using System.Windows.Forms;
using Warehouse.DB;
using System.Data.SqlClient;

namespace Warehouse.StockReport
{
    class RecordPreview
    {
        public string JSLX;          //结算类型
        public string BarOrBd_id;    //绑定编号
        public string childid;       //子报表合同编号
        public DateTime czfrom;     //抄张起始日期
        public DateTime czto;       //抄张终止日期
        public double totmoney;      //合计金额
        public string mulid;     //记录结算单抬头的合同编号
        public DataTable table = new DataTable();//预报表连接的数据集
        int i = 0;
        int j = -1;
        //定义父、子报表
        public GridppReport Report = new GridppReport();
        public GridppReport SubReport = new GridppReport();

        public RecordPreview()
        {
            //父报表
            Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "RecordMain.grf");
            Report.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);
            //注意要使用该事件(传递Datatable到Report)
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Report_FetchRecord);

            //子报表关联
            SubReport.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "RecordFee.grf");
            SubReport.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            SubReport.Initialize += new _IGridppReportEvents_InitializeEventHandler(SubReport_Initialize);

            Report.ControlByName("SubReport1").AsSubReport.Report = SubReport;
        }

        void Report_FetchRecord()
        {
            throw new NotImplementedException();
        }

        void Report_FetchRecord(ref bool pEof)
        {
            GridReportUtility.FillRecordToReport(Report, this.table);
        }
        //主报表相关事件
        public void Report_Initialize()
        {
            DataTable dt = new DataTable();
            //查找基本开票及日期信息
            string sql = "";
            //客户名称及备注
            sql = "select T_CustomerInf .CustName ,T_Bargains .memo "
                 + "from T_Bargains "
                 + " left join T_CustomerInf on T_Bargains.CustCode =T_CustomerInf .CustID "
                 + " where T_Bargains.Sysid =" + this.BarOrBd_id;
            dt = (new SqlDBConnect()).Get_Dt(sql);
            if ((new DBUtil()).yn_exist_data(sql))
            {
                Report.ParameterByName("CustName").AsString = dt.Rows[0][0].ToString(); //客户名称
                Report.ParameterByName("Memo").AsString = dt.Rows[0][1].ToString();   // 备注(合同)
            }
            //显示四种费用或其他费用之和
            sql = "select sum(totMoney)基本费用 from T_MacSettle where BargOrBd_id='" + this.childid + "'" +
                "and czfrom='" + this.czfrom.ToString("yyyy-MM-dd") + "'and czto='" + this.czto.ToString("yyyy-MM-dd") + "'" +
                "and num is  NULL and Fee is NULL";
            double basefee = 0.0;//基本费用
            dt = (new SqlDBConnect()).Get_Dt(sql);
            if (dt.Rows[0][0] != DBNull.Value)
                basefee = Convert.ToDouble((new SqlDBConnect()).Ret_Single_val_Sql(sql));
            string fee1 = "", fee2 = "", fee3 = "", fee4 = "";
            //不等零就显示出来
            double billfee = 0.0, leftfee = 0.0, forefee = 0.0;
            billfee = totmoney - basefee;//开票金额
            sql = "select sum(PrePay)-sum(Paid) from T_MacSettlePrePay where BargOrBd_Id ='" + this.childid + "'";
            if (((new DBUtil()).yn_exist_data(sql)) && ((new SqlDBConnect()).Ret_Single_val_Sql(sql) != ""))
            {
                leftfee = Convert.ToDouble((new SqlDBConnect()).Ret_Single_val_Sql(sql));//余额
            }
            sql = "select sum(PrePay) from T_MacSettlePrePay where BargOrBd_Id ='" + this.childid + "'";
            if (((new DBUtil()).yn_exist_data(sql)) && ((new SqlDBConnect()).Ret_Single_val_Sql(sql) != ""))
            {
                forefee = Convert.ToDouble((new SqlDBConnect()).Ret_Single_val_Sql(sql));
            }
            else
            {
                //将静态框隐藏等其他框隐藏
                Report.ControlByName("StaticBox13").Visible = false;
                Report.ControlByName("StaticBox12").Visible = false;
                Report.ControlByName("MemoBox9").Visible = false;
                Report.ControlByName("MemoBox10").Visible = false;
            }
            if ((basefee - forefee) <= 0)
            {
                basefee = 0.0;
                if ((billfee + basefee - forefee) <= 0)
                {
                    billfee = 0.0;
                }
                else
                {
                    billfee = totmoney - forefee;
                }
            }
            else
            {
                basefee = basefee - forefee;//抄张费用
            }
            fee1 = billfee.ToString();
            fee2 = basefee.ToString();
            fee3 = leftfee.ToString();
            fee4 = forefee.ToString();
            Report.ParameterByName("开票金额").AsString = fee1;
            Report.ParameterByName("基本费用").AsString = fee2;
            Report.ParameterByName("余额").AsString = fee3;
            Report.ParameterByName("预付费金额").AsString = fee4;


            //获取开票日期范围
            sql = "select FeeFrom,FeeTo from T_MacSettle where BargOrBd_id='" + this.childid + "'" +
                "and czfrom='" + this.czfrom.ToString("yyyy-MM-dd") + "'and czto='" + this.czto.ToString("yyyy-MM-dd") + "'" +
                "and num is  NULL and Fee is NULL";
            dt = (new SqlDBConnect()).Get_Dt(sql);
            if ((new DBUtil()).yn_exist_data(sql) && (basefee != 0.0))
            {
                Report.ParameterByName("费用日期").AsString = (Convert.ToDateTime(dt.Rows[0][0].ToString())).ToString("yyyy-MM-dd") + '至' + (Convert.ToDateTime(dt.Rows[0][1].ToString())).ToString("yyyy-MM-dd");
            }
            //不等零就显示出来
            if (billfee != 0.0)
            {
                Report.ParameterByName("开票日期范围").AsString = this.czfrom.ToString("yyyy-MM-dd") + '至' + this.czto.ToString("yyyy-MM-dd");
            }
            //获取合同结算单抬头,多合同只取第一个的结算抬头
            if (this.JSLX == "单机")
            {
                sql = "select BillTitle from T_Bargains where Sysid=" + this.BarOrBd_id;
            }
            else
            {
                sql = "select BillTitle from T_Bargains where Sysid=" + this.mulid;
            }
            //只选择第一个合同的Id
            Report.ParameterByName("QYTitle").AsString = (new DBUtil()).Get_Single_val(sql);
            //查找客户发票抬头信息(多抬头放到一行)
            string custmesql = "select Sysid from T_CustomerMac "
                + "where CustID in"
                + "(select CustCode from T_Bargains where Sysid =" + this.BarOrBd_id + ") ";
            if ((new DBUtil()).yn_exist_data(custmesql))
            {
                string Title = "";
                string sysid = "";
                string strsql = "";
                dt = (new SqlDBConnect()).Get_Dt(custmesql);
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        sysid = dt.Rows[i][0].ToString();
                        strsql = "select Ititle from T_CustomerMaInvoice where CmSysId='" + sysid + "'";
                        if ((new DBUtil()).yn_exist_data(strsql))
                        {
                            DataTable da = (new SqlDBConnect()).Get_Dt(strsql);
                            Title += da.Rows[0][0].ToString() + '、';
                        }
                        else
                        {
                            continue;
                        }
                    }
                    catch (Exception t)
                    {
                        MessageBox.Show(t.ToString());
                    }
                }
                Report.ParameterByName("Title").AsString = Title;
            }
            //GridReportUtility.FillRecordToReport(Report, this.table);
        }
        //子报表相关事件
        public void SubReport_Initialize()
        {
            string sql = "select  hctype 幅面,moneytype 使用类型,Num 使用张数,Fee 单价," +
                         "feefrom 费用起始日期, feeto 费用终止日期, totMoney 小计 " +
                         "from T_MacSettle where BargOrBd_id='" + this.childid 
                         + "'and czfrom='" + this.czfrom.ToString("yyyy-MM-dd") + "' " +
                         "and czto='" + this.czto.ToString("yyyy-MM-dd") + "'";
            SubReport.DetailGrid.Recordset.QuerySQL = sql;
        }
        //录入抄张明细
        public void TableInsert()
        {
            DataColumn a = new DataColumn("JXJH"); a.Caption = "JXJH";
            DataColumn b = new DataColumn("LastDate"); b.Caption = "LastDate";
            DataColumn c = new DataColumn("LastNum"); c.Caption = "LastNum";
            DataColumn d = new DataColumn("Curdate"); d.Caption = "Curdate";
            DataColumn e = new DataColumn("CurNum"); e.Caption = "CurNum";
            DataColumn f = new DataColumn("hctype"); f.Caption = "hctype";
            DataColumn g = new DataColumn("Usenum"); g.Caption = "Usenum";

            table.Columns.Add(a); table.Columns.Add(b);
            table.Columns.Add(c); table.Columns.Add(d);
            table.Columns.Add(e); table.Columns.Add(f);
            table.Columns.Add(g);
            //子报表的合同号
            this.childid = this.BarOrBd_id;
            string Copysql = "";
            //单机合同
            if (this.JSLX == "单机")
            {
                //找到对应的合同的抄张明细
                Copysql = "select mtype 机型,manufactcode 机号,Curdate 本次抄张日期,CurNum 本次抄张张数, " +
                           "LastDate 上次抄张日期,LastNum 上次抄张张数,czfrom 抄张起始日期,czTo 抄张终止日期,ltrim(Rtrim(hctype)) 幅面 " +
                           "from T_RecordCopy where barsysid=" + this.BarOrBd_id + " and czfrom='" + this.czfrom.ToString("yyyy-MM-dd") + "'" +
                           "and czto='" + this.czto.ToString("yyyy-MM-dd") + "'";
                CreatCopy(Copysql, this.BarOrBd_id);

            }
            else//绑定合同
            {
                Copysql = "select Barsysid from T_BargBindMacs where Bdid='" + this.BarOrBd_id + "'";
                if ((new DBUtil()).yn_exist_data(Copysql))
                {
                    DataTable dr = new DataTable();
                    int i = 0;
                    dr = (new SqlDBConnect()).Get_Dt(Copysql);
                    for (i = 0; i < dr.Rows.Count; i++)
                    {
                        //根据绑定编号找到合同编号(sysid)此时Barorbd_id充当sysid
                        this.BarOrBd_id = dr.Rows[i][0].ToString();
                        //记录第一条合同编号(结算抬头)
                        mulid = dr.Rows[0][0].ToString();
                        //找到对应的合同的抄张明细
                        Copysql = "select mtype 机型,manufactcode 机号,Curdate 本次抄张日期,CurNum 本次抄张张数, " +
                           "LastDate 上次抄张日期,LastNum 上次抄张张数,czfrom 抄张起始日期,czTo 抄张终止日期,ltrim(Rtrim(hctype)) 幅面 " +
                           "from T_RecordCopy where barsysid=" + this.BarOrBd_id + " and czfrom='" + this.czfrom.ToString("yyyy-MM-dd") + "'" +
                           "and czto='" + this.czto.ToString("yyyy-MM-dd") + "'";
                        CreatCopy(Copysql, this.BarOrBd_id);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        public void CreatCopy(string Copysql, string ID)
        {
            DataTable dt = new DataTable();
            DataTable dtt = new DataTable();
            if ((new DBUtil()).yn_exist_data(Copysql))
            {
                dt = (new SqlDBConnect()).Get_Dt(Copysql);
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string sqll = "select LastDate from T_RecordCopy where barsysid=" + ID + "" +
                                  " and czfrom='" + this.czfrom.ToString("yyyy-MM-dd") + "'" +
                                  " and czto='" + this.czto.ToString("yyyy-MM-dd") + "'and hctype='" + dt.Rows[i][8] + "'";
                    dtt = (new SqlDBConnect()).Get_Dt(sqll);
                    string time;
                    if ((new DBUtil()).yn_exist_data(sqll))
                    {
                        time = Convert.ToDateTime(dtt.Rows[0][0].ToString()).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        time = "本次为第一次抄张";
                    }
                    //当数量为空时为其赋值为0
                    if (dt.Rows[i][5] == DBNull.Value)
                        dt.Rows[i][5] = 0;
                    if (dt.Rows[i][3] == DBNull.Value)
                        dt.Rows[i][3] = 0;
                    //机型机号
                    string JXJH = dt.Rows[i][0].ToString().Trim() + '-' + dt.Rows[i][1].ToString().Trim();
                    int usenum = Convert.ToInt32(dt.Rows[i][3].ToString()) - Convert.ToInt32(dt.Rows[i][5].ToString());
                    //每循环一次增加一行j初始值为-1
                    j++;
                    table.Rows.Add();
                    table.Rows[j]["JXJH"] = JXJH;
                    table.Rows[j]["Lastdate"] = time;
                    table.Rows[j]["LastNum"] = dt.Rows[i][5];
                    table.Rows[j]["Curdate"] = Convert.ToDateTime(dt.Rows[i][2].ToString()).ToString("yyyy-MM-dd");
                    table.Rows[j]["CurNum"] = dt.Rows[i][3];
                    table.Rows[j]["hctype"] = dt.Rows[i][8];
                    table.Rows[j]["Usenum"] = usenum;
                }
            }
        }
    }
}
