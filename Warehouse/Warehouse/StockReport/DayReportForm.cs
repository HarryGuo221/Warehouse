using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using grproLib;
using Warehouse.Base;
using Warehouse.DB;

namespace Warehouse.StockReport
{
    public partial class DayReportForm : Form
    {
        //定义Grid++Report报表主对象
        protected GridppReport Report = new GridppReport();
        protected GridppReport SubReport = new GridppReport();
        string dateTimeFrom = "";
        string dateTimeTo = "";
        public DayReportForm()
        {
            InitializeComponent();

            //Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "日报总表.grf");

            //设置与数据源的连接串，因为在设计时指定的数据库路径是绝对路径。
            //Report.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Report_FetchRecord);

            //子报表关联            
            //SubReport.Initialize += new _IGridppReportEvents_InitializeEventHandler(SubReport_Initialize);
            //SubReport.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(SubReport_FetchRecord);

            //Report.ControlByName("SubReport01").AsSubReport.Report = SubReport;

            //设定查询显示器关联的报表
            this.axGRDisplayViewer1.Report = Report;
        }

        void Report_FetchRecord()
        {
            throw new NotImplementedException();
        }

        private void DayReportForm_Load(object sender, EventArgs e)
        {
            this.DateTimePickerFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//默认当月第一天

            //this.axGRDisplayViewer1.Start();
        }

        void Report_Initialize()
        {
            DefineReport();
        }

        void Report_FetchRecord(ref bool pEof)
        {
            DataTable dt = GetDataTable();
            GridReportUtility.FillRecordToReport(Report, dt);
        }

        private DataTable GetDataTable()
        {
            //设置滚动条初始值
            this.progressBar1.Maximum = 80;
            this.progressBar1.Value = 0;
            SqlDBConnect db = new SqlDBConnect();
            dateTimeFrom = this.DateTimePickerFrom.Value.ToString("yyyy-MM-dd").Trim(); //"2011-07-01";//
            dateTimeTo = this.DateTimePickerTo.Value.ToString("yyyy-MM-dd").Trim(); // "2011-07-31";//
            string firstDayOfCurMonth = (new DateTime(DateTimePickerFrom.Value.Year, DateTimePickerFrom.Value.Month, 1)).ToString("yyyy-MM-dd").Trim();//当月第一天 //"2011-07-01";//
            string curWorkMonth = dateTimeFrom.Substring(0, 7);
            curWorkMonth = curWorkMonth.Remove(curWorkMonth.IndexOf('-'), 1); //当前工作月
            //获取起始时间的前一天日期
            string Tsql = "select convert(varchar(10),dateadd(day,-1,'" + dateTimeFrom + "'),120)";
            string TimeTo = (new SqlDBConnect()).Ret_Single_val_Sql(Tsql).ToString();

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("类别"); dc.Caption = "类别";
            DataColumn dc1 = new DataColumn("佳图"); dc1.Caption = "佳图";
            DataColumn dc2 = new DataColumn("欣图"); dc2.Caption = "欣图";
            DataColumn dc3 = new DataColumn("瑞博"); dc3.Caption = "瑞博";
            DataColumn dc4 = new DataColumn("信息"); dc4.Caption = "信息";
            DataColumn dc5 = new DataColumn("合计"); dc5.Caption = "合计";
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);
            dt.Columns.Add(dc3); dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);

            int i = -1;
            //上日列帐
            dt.Rows.Add();
            i++;
            int recordnum = i;
            this.progressBar1.Value = i;

            //暂估入库
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            #region //暂估入库
            double JAmountzg = 0.0, XAmountzg = 0.0, RAmountzg = 0.0, HAmountzg = 0.0;
            string Foreadsql = "select StoreHouseId ,SUM (FirstRoadMoney)Amount from T_Stock_Status where BalanceTime ='" + curWorkMonth + "'group by StoreHouseId ";
            DataTable dtzg = db.Get_Dt(Foreadsql);
            if (dtzg != null && dtzg.Rows.Count > 0)
            {
                foreach (DataRow dr in dtzg.Rows)
                {
                    if (dr["StoreHouseId"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmountzg += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["StoreHouseId"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmountzg += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["StoreHouseId"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmountzg += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["StoreHouseId"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmountzg += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                }
            }
            string strsql0320 = "select SourceStoreH, SUM(Amount) Amount from T_Receipt_Main_Det where (OccurTime between '{0}' and '{1}') and (ReceiptTypeID ='03'or ReceiptTypeID='20') " +
                                "Group by SourceStoreH";
            strsql0320 = string.Format(strsql0320, firstDayOfCurMonth, TimeTo);
            DataTable dt0320 = db.Get_Dt(strsql0320);
            if (dt0320 != null && dt0320.Rows.Count > 0)
            {
                foreach (DataRow dr in dt0320.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmountzg += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmountzg += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmountzg += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmountzg += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                }
            }
            dt.Rows[i]["类别"] = "暂估入库";
            dt.Rows[i]["佳图"] = JAmountzg.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmountzg.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmountzg.ToString("0.00");
            dt.Rows[i]["信息"] = HAmountzg.ToString("0.00");
            double Foread = JAmountzg + XAmountzg + RAmountzg + HAmountzg;
            dt.Rows[i]["合计"] = Foread.ToString("0.00");
            #endregion

            //发出商品
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            #region //发出商品
            double JCostsent = 0.0, XCostsent = 0.0, RCostsent = 0.0, HCostsent = 0.0;
            string Sentsql = "select StoreHouseId ,SUM (FirstOutMoney)Cost from T_Stock_Status where BalanceTime ='" + curWorkMonth + "'group by StoreHouseId ";
            DataTable dtsent = db.Get_Dt(Sentsql);
            if (dtsent != null && dtsent.Rows.Count > 0)
            {
                foreach (DataRow dr in dtsent.Rows)
                {
                    if (dr["StoreHouseId"].ToString().Trim() == "J")
                    {
                        if (dr["Cost"].ToString().Trim() != "")
                            JCostsent += Convert.ToDouble(dr["Cost"].ToString().Trim());
                    }
                    if (dr["StoreHouseId"].ToString().Trim() == "X")
                    {
                        if (dr["Cost"].ToString().Trim() != "")
                            XCostsent += Convert.ToDouble(dr["Cost"].ToString().Trim());
                    }
                    if (dr["StoreHouseId"].ToString().Trim() == "R")
                    {
                        if (dr["Cost"].ToString().Trim() != "")
                            RCostsent += Convert.ToDouble(dr["Cost"].ToString().Trim());
                    }
                    if (dr["StoreHouseId"].ToString().Trim() == "H")
                    {
                        if (dr["Cost"].ToString().Trim() != "")
                            HCostsent += Convert.ToDouble(dr["Cost"].ToString().Trim());
                    }
                }
            }
            string strsql7588 = "select SourceStoreH, SUM(TTaxPurchPrice) Cost from T_Receipt_Main_Det where (OccurTime between '{0}' and '{1}') and (ReceiptTypeID ='75'or ReceiptTypeID='88') " +
                                "Group by SourceStoreH";
            strsql7588 = string.Format(strsql7588, firstDayOfCurMonth, TimeTo);
            DataTable dt7588 = db.Get_Dt(strsql7588);
            if (dt7588 != null && dt7588.Rows.Count > 0)
            {
                foreach (DataRow dr in dt7588.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Cost"].ToString().Trim() != "")
                            JCostsent += Convert.ToDouble(dr["Cost"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Cost"].ToString().Trim() != "")
                            XCostsent += Convert.ToDouble(dr["Cost"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Cost"].ToString().Trim() != "")
                            RCostsent += Convert.ToDouble(dr["Cost"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Cost"].ToString().Trim() != "")
                            HCostsent += Convert.ToDouble(dr["Cost"].ToString().Trim());
                    }
                }
            }
            dt.Rows[i]["类别"] = "发出商品";
            dt.Rows[i]["佳图"] = JCostsent.ToString("0.00");
            dt.Rows[i]["欣图"] = XCostsent.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCostsent.ToString("0.00");
            dt.Rows[i]["信息"] = HCostsent.ToString("0.00");
            double Sentout = JCostsent + XCostsent + RCostsent + HCostsent;
            dt.Rows[i]["合计"] = Sentout.ToString("0.00");
            #endregion


            //实际结存
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            #region //实际结存
            double JAmountjc = 0.0, XAmountjc = 0.0, RAmountjc = 0.0, HAmountjc = 0.0;
            string strsqlsr = "select SourceStoreH, SUM(TTaxPurchPrice) Amount from T_Receipt_Main_Det where (OccurTime between '{0}' and '{1}') " +
                              "and (ReceiptTypeID ='01'or ReceiptTypeID='03'or ReceiptTypeID='04'or ReceiptTypeID='20') " +
                              "Group by SourceStoreH";
            strsqlsr = string.Format(strsqlsr, firstDayOfCurMonth, TimeTo);
            DataTable dtsr = db.Get_Dt(strsqlsr);
            if (dtsr != null && dtsr.Rows.Count > 0)
            {
                foreach (DataRow dr in dtsr.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                }
            }
            string strsqlfc = "select SourceStoreH, SUM(Amount) Amount from T_Receipt_Main_Det where (OccurTime between '{0}' and '{1}') " +
                             "and (ReceiptTypeID ='51'or ReceiptTypeID='52'or ReceiptTypeID='54'or ReceiptTypeID='55'or ReceiptTypeID='56'" +
                             "or ReceiptTypeID='75'or ReceiptTypeID='88'or ReceiptTypeID='90') " +
                             "Group by SourceStoreH";
            strsqlfc = string.Format(strsqlfc, firstDayOfCurMonth, TimeTo);
            DataTable dtfc = db.Get_Dt(strsqlfc);
            if (dtfc != null && dtfc.Rows.Count > 0)
            {
                foreach (DataRow dr in dtfc.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim()) * -1;
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim()) * -1;
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim()) * -1;
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim()) * -1;
                    }
                }
            }
            string Stocksql = "select StoreHouseId ,SUM (FirstMoney)Amount from T_Stock_Status where BalanceTime ='" + curWorkMonth + "'group by StoreHouseId ";
            DataTable dtjc = db.Get_Dt(Stocksql);
            if (dtjc != null && dtjc.Rows.Count > 0)
            {
                foreach (DataRow dr in dtjc.Rows)
                {
                    if (dr["StoreHouseId"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["StoreHouseId"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["StoreHouseId"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["StoreHouseId"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmountjc += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                }
            }
            dt.Rows[i]["类别"] = "实际结存";
            dt.Rows[i]["佳图"] = JAmountjc.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmountjc.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmountjc.ToString("0.00");
            dt.Rows[i]["信息"] = HAmountjc.ToString("0.00");
            double Realstock = JAmountjc + XAmountjc + RAmountjc + HAmountjc;
            dt.Rows[i]["合计"] = Realstock.ToString("0.00");
            #endregion

            #region//上日列账
            dt.Rows[recordnum]["类别"] = "上日列帐";
            dt.Rows[recordnum]["佳图"] = (JAmountjc + JCostsent - JAmountzg).ToString("0.00");
            dt.Rows[recordnum]["欣图"] = (XAmountjc + XCostsent - XAmountzg).ToString("0.00");
            dt.Rows[recordnum]["瑞博"] = (RAmountjc + RCostsent - RAmountzg).ToString("0.00");
            dt.Rows[recordnum]["信息"] = (HAmountjc + HCostsent - HAmountzg).ToString("0.00");
            dt.Rows[recordnum]["合计"] = (Realstock + Sentout - Foread).ToString("0.00");
            #endregion


            //空行            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            //01-进货单            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "01-进货单";

            #region 01-进货单
            double JAmount01 = 0.0, XAmount01 = 0.0, RAmount01 = 0.0, HAmount01 = 0.0;
            double JNotTax01 = 0.0, XNotTax01 = 0.0, RNotTax01 = 0.0, HNotTax01 = 0.0;
            double JTax01 = 0.0, XTax01 = 0.0, RTax01 = 0.0, HTax01 = 0.0;
            double JCost01 = 0.0, XCost01 = 0.0, RCost01 = 0.0, HCost01 = 0.0;

            string sql01 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                          "where ReceiptTypeID = '01' and (OccurTime between '{0}' and '{1}')";
            sql01 = string.Format(sql01, dateTimeFrom, dateTimeTo);
            DataTable dt01 = db.Get_Dt(sql01);
            if (dt01 != null && dt01.Rows.Count > 0)
            {
                foreach (DataRow dr in dt01.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount01 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["NotTax"].ToString().Trim() != "")
                            JNotTax01 += Convert.ToDouble(dr["NotTax"].ToString().Trim());
                        if (dr["Tax"].ToString().Trim() != "")
                            JTax01 += Convert.ToDouble(dr["Tax"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost01 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount01 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["NotTax"].ToString().Trim() != "")
                            XNotTax01 += Convert.ToDouble(dr["NotTax"].ToString().Trim());
                        if (dr["Tax"].ToString().Trim() != "")
                            XTax01 += Convert.ToDouble(dr["Tax"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost01 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount01 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["NotTax"].ToString().Trim() != "")
                            RNotTax01 += Convert.ToDouble(dr["NotTax"].ToString().Trim());
                        if (dr["Tax"].ToString().Trim() != "")
                            RTax01 += Convert.ToDouble(dr["Tax"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost01 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount01 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["NotTax"].ToString().Trim() != "")
                            HNotTax01 += Convert.ToDouble(dr["NotTax"].ToString().Trim());
                        if (dr["Tax"].ToString().Trim() != "")
                            HTax01 += Convert.ToDouble(dr["Tax"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost01 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //含税金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "含税金额";
            dt.Rows[i]["佳图"] = JAmount01.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount01.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount01.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount01.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount01 + XAmount01 + RAmount01 + HAmount01).ToString("0.00");
            //不含税金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "不含税金额";
            dt.Rows[i]["佳图"] = JNotTax01.ToString("0.00");
            dt.Rows[i]["欣图"] = XNotTax01.ToString("0.00");
            dt.Rows[i]["瑞博"] = RNotTax01.ToString("0.00");
            dt.Rows[i]["信息"] = HNotTax01.ToString("0.00");
            dt.Rows[i]["合计"] = (JNotTax01 + XNotTax01 + RNotTax01 + HNotTax01).ToString("0.00");
            //税额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "税额";
            dt.Rows[i]["佳图"] = JTax01.ToString("0.00");
            dt.Rows[i]["欣图"] = XTax01.ToString("0.00");
            dt.Rows[i]["瑞博"] = RTax01.ToString("0.00");
            dt.Rows[i]["信息"] = HTax01.ToString("0.00");
            dt.Rows[i]["合计"] = (JTax01 + XTax01 + RTax01 + HTax01).ToString("0.00");
            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount01.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount01.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount01.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount01.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount01 + XAmount01 + RAmount01 + HAmount01).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JAmount01.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount01.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount01.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount01.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount01 + XAmount01 + RAmount01 + HAmount01).ToString("0.00");
            #endregion

            //03-假设进货单            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "03-假设进货单";

            #region 03-假设进货单
            double JAmount03 = 0.0, XAmount03 = 0.0, RAmount03 = 0.0, HAmount03 = 0.0;
            double JCost03 = 0.0, XCost03 = 0.0, RCost03 = 0.0, HCost03 = 0.0;

            string sql03 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,TTaxPurchPrice from T_Receipt_Main_Det " +
                            "where ReceiptTypeID = '03' and (OccurTime between '{0}' and '{1}')";
            sql03 = string.Format(sql03, dateTimeFrom, dateTimeTo);
            DataTable dt03 = db.Get_Dt(sql03);
            if (dt03 != null && dt03.Rows.Count > 0)
            {
                foreach (DataRow dr in dt03.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount03.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount03.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount03.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount03.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount03 + XAmount03 + RAmount03 + HAmount03).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JAmount03.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount03.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount03.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount03.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount03 + XAmount03 + RAmount03 + HAmount03).ToString("0.00");
            #endregion

            //04-盈亏单          
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "04-盈亏单";

            #region 04-盈亏单
            double JCost04 = 0.0, XCost04 = 0.0, RCost04 = 0.0, HCost04 = 0.0;

            string sql04 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '04' and (OccurTime between '{0}' and '{1}')";
            sql04 = string.Format(sql04, dateTimeFrom, dateTimeTo);
            DataTable dt04 = db.Get_Dt(sql04);
            if (dt04 != null && dt04.Rows.Count > 0)
            {
                foreach (DataRow dr in dt04.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                }
            }

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost04.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost04.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost04.ToString("0.00");
            dt.Rows[i]["信息"] = HCost04.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost04 + XCost04 + RCost04 + HCost04).ToString("0.00");
            #endregion

            //20-假设结账单          
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "20-假设结账单";
            #region 20-假设结账单

            double JCost20 = 0.0, XCost20 = 0.0, RCost20 = 0.0, HCost20 = 0.0;

            string sql20 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,TTaxPurchPrice from T_Receipt_Main_Det " +
                            "where ReceiptTypeID = '20' and (OccurTime between '{0}' and '{1}')";
            sql20 = string.Format(sql20, dateTimeFrom, dateTimeTo);
            DataTable dt20 = db.Get_Dt(sql20);
            if (dt20 != null && dt20.Rows.Count > 0)
            {
                foreach (DataRow dr in dt20.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                }
            }

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost20.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost20.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost20.ToString("0.00");
            dt.Rows[i]["信息"] = HCost20.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost20 + XCost20 + RCost20 + HCost20).ToString("0.00");
            #endregion

            //收入小计            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            #region 收入小计
            double sumJIn = JAmount01 + JAmount03 + JCost04 + JCost20;
            double sumXIn = XAmount01 + XAmount03 + XCost04 + XCost20;
            double sumRIn = RAmount01 + RAmount03 + RCost04 + RCost20;
            double sumHIn = HAmount01 + HAmount03 + HCost04 + HCost20;
            dt.Rows[i]["类别"] = "收入小计";
            dt.Rows[i]["佳图"] = sumJIn.ToString("0.00");
            dt.Rows[i]["欣图"] = sumXIn.ToString("0.00");
            dt.Rows[i]["瑞博"] = sumRIn.ToString("0.00");
            dt.Rows[i]["信息"] = sumHIn.ToString("0.00");
            dt.Rows[i]["合计"] = (sumJIn + sumXIn + sumRIn + sumHIn).ToString("0.00");
            #endregion

            //空行            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;

            //51-销售单1            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "51-销售单1";

            #region 51-销售单1
            double JAmount51 = 0.0, XAmount51 = 0.0, RAmount51 = 0.0, HAmount51 = 0.0;
            double JCost51 = 0.0, XCost51 = 0.0, RCost51 = 0.0, HCost51 = 0.0;

            string sql51 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '51' and (OccurTime between '{0}' and '{1}')";
            sql51 = string.Format(sql51, dateTimeFrom, dateTimeTo);
            DataTable dt51 = db.Get_Dt(sql51);
            if (dt51 != null && dt51.Rows.Count > 0)
            {
                foreach (DataRow dr in dt51.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount51.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount51.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount51.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount51.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount51 + XAmount51 + RAmount51 + HAmount51).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost51.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost51.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost51.ToString("0.00");
            dt.Rows[i]["信息"] = HCost51.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost51 + XCost51 + RCost51 + HCost51).ToString("0.00");

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = (JAmount51 - JCost51).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount51 - XCost51).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount51 - RCost51).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount51 - HCost51).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount51 - JCost51 + XAmount51 - XCost51 +
                                 RAmount51 - RCost51 + HAmount51 - HCost51).ToString("0.00");

            //求“当月第1天”到“开始时间” 价税合计、成本金额、毛利
            double JAmount51Add = 0.0, XAmount51Add = 0.0, RAmount51Add = 0.0, HAmount51Add = 0.0;
            double JCost51Add = 0.0, XCost51Add = 0.0, RCost51Add = 0.0, HCost51Add = 0.0;

            string sql51Add = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '51' and (OccurTime between '{0}' and '{1}')";
            sql51Add = string.Format(sql51Add, firstDayOfCurMonth, dateTimeTo);
            DataTable dt51Add = db.Get_Dt(sql51Add);
            if (dt51Add != null && dt51Add.Rows.Count > 0)
            {
                foreach (DataRow dr in dt51Add.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //累计：价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：价税合计";
            dt.Rows[i]["佳图"] = JAmount51Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount51Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount51Add.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount51Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount51Add + XAmount51Add + RAmount51Add + HAmount51Add).ToString("0.00");

            //累计：成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：成本金额";
            dt.Rows[i]["佳图"] = JCost51Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost51Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost51Add.ToString("0.00");
            dt.Rows[i]["信息"] = HCost51Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost51Add + XCost51Add + RCost51Add + HCost51Add).ToString("0.00");

            //累计：毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：毛利";
            dt.Rows[i]["佳图"] = (JAmount51Add - JCost51Add).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount51Add - XCost51Add).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount51Add - RCost51Add).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount51Add - HCost51Add).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount51Add - JCost51Add + XAmount51Add - XCost51Add +
                                 RAmount51Add - RCost51Add + HAmount51Add - HCost51Add).ToString("0.00");
            #endregion

            //52-销售单2            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "52-销售单2";

            #region 52-销售单2
            double JAmount52 = 0.0, XAmount52 = 0.0, RAmount52 = 0.0, HAmount52 = 0.0;
            double JCost52 = 0.0, XCost52 = 0.0, RCost52 = 0.0, HCost52 = 0.0;

            string sql52 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '52' and (OccurTime between '{0}' and '{1}')";
            sql52 = string.Format(sql52, dateTimeFrom, dateTimeTo);
            DataTable dt52 = db.Get_Dt(sql52);
            if (dt52 != null && dt52.Rows.Count > 0)
            {
                foreach (DataRow dr in dt52.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount52.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount52.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount52.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount52.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount52 + XAmount52 + RAmount52 + HAmount52).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost52.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost52.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost52.ToString("0.00");
            dt.Rows[i]["信息"] = HCost52.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost52 + XCost52 + RCost52 + HCost52).ToString("0.00");

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = (JAmount52 - JCost52).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount52 - XCost52).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount52 - RCost52).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount52 - HCost52).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount52 - JCost52 + XAmount52 - XCost52 +
                                 RAmount52 - RCost52 + HAmount52 - HCost52).ToString("0.00");

            //求“当月第1天”到“开始时间” 价税合计、成本金额、毛利
            double JAmount52Add = 0.0, XAmount52Add = 0.0, RAmount52Add = 0.0, HAmount52Add = 0.0;
            double JCost52Add = 0.0, XCost52Add = 0.0, RCost52Add = 0.0, HCost52Add = 0.0;

            string sql52Add = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '52' and (OccurTime between '{0}' and '{1}')";
            sql52Add = string.Format(sql52Add, firstDayOfCurMonth, dateTimeTo);
            DataTable dt52Add = db.Get_Dt(sql52Add);
            if (dt52Add != null && dt52Add.Rows.Count > 0)
            {
                foreach (DataRow dr in dt52Add.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //累计：价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：价税合计";
            dt.Rows[i]["佳图"] = JAmount52Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount52Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount52Add.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount52Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount52Add + XAmount52Add + RAmount52Add + HAmount52Add).ToString("0.00");

            //累计：成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：成本金额";
            dt.Rows[i]["佳图"] = JCost52Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost52Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost52Add.ToString("0.00");
            dt.Rows[i]["信息"] = HCost52Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost52Add + XCost52Add + RCost52Add + HCost52Add).ToString("0.00");

            //累计：毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：毛利";
            dt.Rows[i]["佳图"] = (JAmount52Add - JCost52Add).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount52Add - XCost52Add).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount52Add - RCost52Add).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount52Add - HCost52Add).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount52Add - JCost52Add + XAmount52Add - XCost52Add +
                                 RAmount52Add - RCost52Add + HAmount52Add - HCost52Add).ToString("0.00");
            #endregion

            //53-调拨单           
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "53-调拨单";

            #region 53-调拨单
            double JAmount53 = 0.0, XAmount53 = 0.0, RAmount53 = 0.0, HAmount53 = 0.0;
            double JCost53 = 0.0, XCost53 = 0.0, RCost53 = 0.0, HCost53 = 0.0;

            string sql53 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '53' and (OccurTime between '{0}' and '{1}')";
            sql53 = string.Format(sql53, dateTimeFrom, dateTimeTo);
            DataTable dt53 = db.Get_Dt(sql53);
            if (dt53 != null && dt53.Rows.Count > 0)
            {
                foreach (DataRow dr in dt53.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount53.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount53.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount53.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount53.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount53 + XAmount53 + RAmount53 + HAmount53).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost53.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost53.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost53.ToString("0.00");
            dt.Rows[i]["信息"] = HCost53.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost53 + XCost53 + RCost53 + HCost53).ToString("0.00");

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = (JAmount53 - JCost53).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount53 - XCost53).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount53 - RCost53).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount53 - HCost53).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount53 - JCost53 + XAmount53 - XCost53 +
                                 RAmount53 - RCost53 + HAmount53 - HCost53).ToString("0.00");
            #endregion

            //54-销售单4           
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "54-销售单4";

            #region 54-销售单4
            double JAmount54 = 0.0, XAmount54 = 0.0, RAmount54 = 0.0, HAmount54 = 0.0;
            double JCost54 = 0.0, XCost54 = 0.0, RCost54 = 0.0, HCost54 = 0.0;

            string sql54 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '54' and (OccurTime between '{0}' and '{1}')";
            sql54 = string.Format(sql54, dateTimeFrom, dateTimeTo);
            DataTable dt54 = db.Get_Dt(sql54);
            if (dt54 != null && dt54.Rows.Count > 0)
            {
                foreach (DataRow dr in dt54.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount54.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount54.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount54.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount54.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount54 + XAmount54 + RAmount54 + HAmount54).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost54.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost54.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost54.ToString("0.00");
            dt.Rows[i]["信息"] = HCost54.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost54 + XCost54 + RCost54 + HCost54).ToString("0.00");

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = (JAmount54 - JCost54).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount54 - XCost54).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount54 - RCost54).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount54 - HCost54).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount54 - JCost54 + XAmount54 - XCost54 +
                                 RAmount54 - RCost54 + HAmount54 - HCost54).ToString("0.00");
            //求“当月第1天”到“开始时间” 价税合计、成本金额、毛利
            double JAmount54Add = 0.0, XAmount54Add = 0.0, RAmount54Add = 0.0, HAmount54Add = 0.0;
            double JCost54Add = 0.0, XCost54Add = 0.0, RCost54Add = 0.0, HCost54Add = 0.0;

            string sql54Add = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '54' and (OccurTime between '{0}' and '{1}')";
            sql54Add = string.Format(sql54Add, firstDayOfCurMonth, dateTimeTo);
            DataTable dt54Add = db.Get_Dt(sql54Add);
            if (dt54Add != null && dt54Add.Rows.Count > 0)
            {
                foreach (DataRow dr in dt54Add.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount54Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost54Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount54Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost54Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount54Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost54Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount54Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost54Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //累计：价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：价税合计";
            dt.Rows[i]["佳图"] = JAmount54Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount54Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount54Add.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount54Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount54Add + XAmount54Add + RAmount54Add + HAmount54Add).ToString("0.00");

            //累计：成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：成本金额";
            dt.Rows[i]["佳图"] = JCost54Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost54Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost54Add.ToString("0.00");
            dt.Rows[i]["信息"] = HCost54Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost54Add + XCost54Add + RCost54Add + HCost54Add).ToString("0.00");

            //累计：毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：毛利";
            dt.Rows[i]["佳图"] = (JAmount54Add - JCost54Add).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount54Add - XCost54Add).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount54Add - RCost54Add).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount54Add - HCost54Add).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount54Add - JCost54Add + XAmount54Add - XCost54Add +
                                 RAmount54Add - RCost54Add + HAmount54Add - HCost54Add).ToString("0.00");
            #endregion

            //55-销售单3           
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "55-销售单3";

            #region 55-销售单3
            double JAmount55 = 0.0, XAmount55 = 0.0, RAmount55 = 0.0, HAmount55 = 0.0;
            double JCost55 = 0.0, XCost55 = 0.0, RCost55 = 0.0, HCost55 = 0.0;

            string sql55 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '55' and (OccurTime between '{0}' and '{1}')";
            sql55 = string.Format(sql55, dateTimeFrom, dateTimeTo);
            DataTable dt55 = db.Get_Dt(sql55);
            if (dt55 != null && dt55.Rows.Count > 0)
            {
                foreach (DataRow dr in dt55.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount55.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount55.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount55.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount55.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount55 + XAmount55 + RAmount55 + HAmount55).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost55.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost55.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost55.ToString("0.00");
            dt.Rows[i]["信息"] = HCost55.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost55 + XCost55 + RCost55 + HCost55).ToString("0.00");

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = (JAmount55 - JCost55).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount55 - XCost55).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount55 - RCost55).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount55 - HCost55).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount55 - JCost55 + XAmount55 - XCost55 +
                                 RAmount55 - RCost55 + HAmount55 - HCost55).ToString("0.00");
            //求“当月第1天”到“开始时间” 价税合计、成本金额、毛利
            double JAmount55Add = 0.0, XAmount55Add = 0.0, RAmount55Add = 0.0, HAmount55Add = 0.0;
            double JCost55Add = 0.0, XCost55Add = 0.0, RCost55Add = 0.0, HCost55Add = 0.0;

            string sql55Add = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '55' and (OccurTime between '{0}' and '{1}')";
            sql55Add = string.Format(sql55Add, firstDayOfCurMonth, dateTimeTo);
            DataTable dt55Add = db.Get_Dt(sql55Add);
            if (dt55Add != null && dt55Add.Rows.Count > 0)
            {
                foreach (DataRow dr in dt55Add.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount55Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost55Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount55Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost55Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount55Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost55Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount55Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost55Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //累计：价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：价税合计";
            dt.Rows[i]["佳图"] = JAmount55Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount55Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount55Add.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount55Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount55Add + XAmount55Add + RAmount55Add + HAmount55Add).ToString("0.00");

            //累计：成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：成本金额";
            dt.Rows[i]["佳图"] = JCost55Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost55Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost55Add.ToString("0.00");
            dt.Rows[i]["信息"] = HCost55Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost55Add + XCost55Add + RCost55Add + HCost55Add).ToString("0.00");

            //累计：毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：毛利";
            dt.Rows[i]["佳图"] = (JAmount55Add - JCost55Add).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount55Add - XCost55Add).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount55Add - RCost55Add).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount55Add - HCost55Add).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount55Add - JCost55Add + XAmount55Add - XCost55Add +
                                 RAmount55Add - RCost55Add + HAmount55Add - HCost55Add).ToString("0.00");
            #endregion

            //56-销售单5            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "56-销售单5";

            #region 56-销售单5
            double JAmount56 = 0.0, XAmount56 = 0.0, RAmount56 = 0.0, HAmount56 = 0.0;
            double JCost56 = 0.0, XCost56 = 0.0, RCost56 = 0.0, HCost56 = 0.0;

            string sql56 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                            "where ReceiptTypeID = '56' and (OccurTime between '{0}' and '{1}')";
            sql56 = string.Format(sql56, dateTimeFrom, dateTimeTo);
            DataTable dt56 = db.Get_Dt(sql56);
            if (dt56 != null && dt56.Rows.Count > 0)
            {
                foreach (DataRow dr in dt56.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount56.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount56.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount56.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount56.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount56 + XAmount56 + RAmount56 + HAmount56).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost56.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost56.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost56.ToString("0.00");
            dt.Rows[i]["信息"] = HCost56.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost56 + XCost56 + RCost56 + HCost56).ToString("0.00");

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = (JAmount56 - JCost56).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount56 - XCost56).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount56 - RCost56).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount56 - HCost56).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount56 - JCost56 + XAmount56 - XCost56 +
                                 RAmount56 - RCost56 + HAmount56 - HCost56).ToString("0.00");

            //求“当月第1天”到“开始时间” 价税合计、成本金额、毛利
            double JAmount56Add = 0.0, XAmount56Add = 0.0, RAmount56Add = 0.0, HAmount56Add = 0.0;
            double JCost56Add = 0.0, XCost56Add = 0.0, RCost56Add = 0.0, HCost56Add = 0.0;

            string sql56Add = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '56' and (OccurTime between '{0}' and '{1}')";
            sql56Add = string.Format(sql56Add, firstDayOfCurMonth, dateTimeTo);
            DataTable dt56Add = db.Get_Dt(sql56Add);
            if (dt56Add != null && dt56Add.Rows.Count > 0)
            {
                foreach (DataRow dr in dt56Add.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //累计：价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：价税合计";
            dt.Rows[i]["佳图"] = JAmount56Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount56Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount56Add.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount56Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount56Add + XAmount56Add + RAmount56Add + HAmount56Add).ToString("0.00");

            //累计：成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：成本金额";
            dt.Rows[i]["佳图"] = JCost56Add.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost56Add.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost56Add.ToString("0.00");
            dt.Rows[i]["信息"] = HCost56Add.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost56Add + XCost56Add + RCost56Add + HCost56Add).ToString("0.00");

            //累计：毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：毛利";
            dt.Rows[i]["佳图"] = (JAmount56Add - JCost56Add).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount56Add - XCost56Add).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount56Add - RCost56Add).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount56Add - HCost56Add).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount56Add - JCost56Add + XAmount56Add - XCost56Add +
                                 RAmount56Add - RCost56Add + HAmount56Add - HCost56Add).ToString("0.00");
            #endregion

            //75-借用单           
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "75-借用单";

            #region 75-借用单
            double JAmount75 = 0.0, XAmount75 = 0.0, RAmount75 = 0.0, HAmount75 = 0.0;
            double JCost75 = 0.0, XCost75 = 0.0, RCost75 = 0.0, HCost75 = 0.0;

            string sql75 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '75' and (OccurTime between '{0}' and '{1}')";
            sql75 = string.Format(sql75, dateTimeFrom, dateTimeTo);
            DataTable dt75 = db.Get_Dt(sql75);
            if (dt75 != null && dt75.Rows.Count > 0)
            {
                foreach (DataRow dr in dt75.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount75.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount75.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount75.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount75.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount75 + XAmount75 + RAmount75 + HAmount75).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost75.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost75.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost75.ToString("0.00");
            dt.Rows[i]["信息"] = HCost75.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost75 + XCost75 + RCost75 + HCost75).ToString("0.00");

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = (JAmount75 - JCost75).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount75 - XCost75).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount75 - RCost75).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount75 - HCost75).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount75 - JCost75 + XAmount75 - XCost75 +
                                 RAmount75 - RCost75 + HAmount75 - HCost75).ToString("0.00");
            #endregion

            //88-租机单          
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "88-租机单";

            #region 88-租机单
            double JAmount88 = 0.0, XAmount88 = 0.0, RAmount88 = 0.0, HAmount88 = 0.0;
            double JCost88 = 0.0, XCost88 = 0.0, RCost88 = 0.0, HCost88 = 0.0;

            string sql88 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '88' and (OccurTime between '{0}' and '{1}')";
            sql88 = string.Format(sql88, dateTimeFrom, dateTimeTo);
            DataTable dt88 = db.Get_Dt(sql88);
            if (dt88 != null && dt88.Rows.Count > 0)
            {
                foreach (DataRow dr in dt88.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount88.ToString("0.00");
            dt.Rows[i]["欣图"] = XAmount88.ToString("0.00");
            dt.Rows[i]["瑞博"] = RAmount88.ToString("0.00");
            dt.Rows[i]["信息"] = HAmount88.ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount88 + XAmount88 + RAmount88 + HAmount88).ToString("0.00");

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost88.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost88.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost88.ToString("0.00");
            dt.Rows[i]["信息"] = HCost88.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost88 + XCost88 + RCost88 + HCost88).ToString("0.00");

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = (JAmount88 - JCost88).ToString("0.00");
            dt.Rows[i]["欣图"] = (XAmount88 - XCost88).ToString("0.00");
            dt.Rows[i]["瑞博"] = (RAmount88 - RCost88).ToString("0.00");
            dt.Rows[i]["信息"] = (HAmount88 - HCost88).ToString("0.00");
            dt.Rows[i]["合计"] = (JAmount88 - JCost88 + XAmount88 - XCost88 +
                                 RAmount88 - RCost88 + HAmount88 - HCost88).ToString("0.00");
            #endregion

            //90-暂估结账销售          
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "90-暂估结账销售";

            #region 90-暂估结账销售
            double JCost90 = 0.0, XCost90 = 0.0, RCost90 = 0.0, HCost90 = 0.0;

            string sql90 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '90' and (OccurTime between '{0}' and '{1}')";
            sql90 = string.Format(sql90, dateTimeFrom, dateTimeTo);
            DataTable dt90 = db.Get_Dt(sql90);
            if (dt90 != null && dt90.Rows.Count > 0)
            {
                foreach (DataRow dr in dt90.Rows)
                {
                    if (dr["SourceStoreH"].ToString().Trim() == "J")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }
            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost90.ToString("0.00");
            dt.Rows[i]["欣图"] = XCost90.ToString("0.00");
            dt.Rows[i]["瑞博"] = RCost90.ToString("0.00");
            dt.Rows[i]["信息"] = HCost90.ToString("0.00");
            dt.Rows[i]["合计"] = (JCost90 + XCost90 + RCost90 + HCost90).ToString("0.00");
            #endregion

            //发出小计            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            #region 发出小计
            double sumJOut = JCost51 + JCost52 + JCost54 + JCost55 + JCost56 + JCost75 + JCost88 + JCost90;
            double sumXOut = XCost51 + XCost52 + XCost54 + XCost55 + XCost56 + XCost75 + XCost88 + XCost90;
            double sumROut = RCost51 + RCost52 + RCost54 + RCost55 + RCost56 + RCost75 + RCost88 + RCost90;
            double sumHOut = HCost51 + HCost52 + HCost54 + HCost55 + HCost56 + HCost75 + HCost88 + HCost90;
            dt.Rows[i]["类别"] = "发出小计";
            dt.Rows[i]["佳图"] = sumJOut.ToString("0.00");
            dt.Rows[i]["欣图"] = sumXOut.ToString("0.00");
            dt.Rows[i]["瑞博"] = sumROut.ToString("0.00");
            dt.Rows[i]["信息"] = sumHOut.ToString("0.00");
            dt.Rows[i]["合计"] = (sumJOut + sumXOut + sumROut + sumHOut).ToString("0.00");
            #endregion

            //空行            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            #region//本日列帐
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "本日列帐";
            double Na = JAmountjc + JCostsent - JAmountzg + JAmount01 + JCost04 - sumJOut + JCost75 + JCost88;
            double Nc = XAmountjc + XCostsent - XAmountzg + XAmount01 + XCost04 - sumXOut + XCost75 + XCost88;
            double Ne = RAmountjc + RCostsent - RAmountzg + RAmount01 + RCost04 - sumROut + RCost75 + RCost88;
            double Ng = HAmountjc + HCostsent - HAmountzg + HAmount01 + HCost04 - sumHOut + HCost75 + HCost88;
            dt.Rows[i]["佳图"] = Na.ToString("0.00");
            dt.Rows[i]["欣图"] = Nc.ToString("0.00");
            dt.Rows[i]["瑞博"] = Ne.ToString("0.00");
            dt.Rows[i]["信息"] = Ng.ToString("0.00");
            dt.Rows[i]["合计"] = (Na + Nc + Ne + Ng).ToString("0.00");
            #endregion

            //暂估入库
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            #region//暂估入库
            double Za = JAmountzg + JAmount03 + JCost20;
            double Zc = XAmountzg + XAmount03 + XCost20;
            double Ze = RAmountzg + RAmount03 + RCost20;
            double Zg = HAmountzg + HAmount03 + HCost20;
            dt.Rows[i]["类别"] = "暂估入库";
            dt.Rows[i]["佳图"] = Za.ToString("0.00");
            dt.Rows[i]["欣图"] = Zc.ToString("0.00");
            dt.Rows[i]["瑞博"] = Ze.ToString("0.00");
            dt.Rows[i]["信息"] = Zg.ToString("0.00");
            dt.Rows[i]["合计"] = (Za + Zc + Ze + Zg).ToString("0.00");
            #endregion

            //发出商品
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            #region//发出商品
            double Senta = JCostsent + JCost75 + JCost88;
            double Sentc = XCostsent + XCost75 + XCost88;
            double Sente = RCostsent + RCost75 + RCost88;
            double Sentg = HCostsent + HCost75 + HCost88;
            dt.Rows[i]["类别"] = "发出商品";
            dt.Rows[i]["佳图"] = Senta.ToString("0.00");
            dt.Rows[i]["欣图"] = Sentc.ToString("0.00");
            dt.Rows[i]["瑞博"] = Sente.ToString("0.00");
            dt.Rows[i]["信息"] = Sentg.ToString("0.00");
            dt.Rows[i]["合计"] = (Senta + Sentc + Sente + Sentg).ToString("0.00");
            #endregion

            //实际结存
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            #region//实际结存
            double Rsa = JAmountjc + sumJIn - sumJOut;
            double Rsc = XAmountjc + sumXIn - sumXOut;
            double Rse = RAmountjc + sumRIn - sumROut;
            double Rsg = HAmountjc + sumHIn - sumHOut;
            dt.Rows[i]["类别"] = "实际结存";
            dt.Rows[i]["佳图"] = Rsa.ToString("0.00");
            dt.Rows[i]["欣图"] = Rsc.ToString("0.00");
            dt.Rows[i]["瑞博"] = Rse.ToString("0.00");
            dt.Rows[i]["信息"] = Rsg.ToString("0.00");
            dt.Rows[i]["合计"] = (Rsa + Rsc + Rse + Rsg).ToString("0.00");
            #endregion

            //空行            
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;

            //副表项目
            #region 副表项目
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            dt.Rows[i]["类别"] = "副表项目";
            dt.Rows[i]["佳图"] = "金额   ";
            dt.Rows[i]["欣图"] = "含税金额  ";
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            //价税合计
            #region//价税合计(金额)
            double Jsa = JAmount51 + JAmount52 + JAmount56;
            double Jsc = XAmount51 + XAmount52 + XAmount56;
            double Jse = RAmount51 + RAmount52 + RAmount56;
            double Jsg = HAmount51 + HAmount52 + HAmount56;
            dt.Rows[i]["类别"] = "价税合计";
            double JSamount = Jsa + Jsc + Jse + Jsg;
            dt.Rows[i]["佳图"] = JSamount.ToString("0.00");
            #endregion
            #region//价税合计( 累计金额)
            double Jsaa = JAmount51Add + JAmount52Add + JAmount56Add;
            double Jscc = XAmount51Add + XAmount52Add + XAmount56Add;
            double Jsee = RAmount51Add + RAmount52Add + RAmount56Add;
            double Jsgg = HAmount51Add + HAmount52Add + HAmount56Add;
            double LJSamount = Jsaa + Jscc + Jsee + Jsgg;
            dt.Rows[i]["欣图"] = (LJSamount).ToString("0.00");
            #endregion
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            //销售成本
            #region//销售成本(金额)
            double Ssa = JCost51 + JCost52 + JCost54 + JCost55 + JCost56 + JCost90;
            double Ssc = XCost51 + XCost52 + XCost54 + XCost55 + XCost56 + XCost90;
            double Sse = RCost51 + RCost52 + RCost54 + RCost55 + RCost56 + RCost90;
            double Ssg = HCost51 + HCost52 + HCost54 + HCost55 + HCost56 + HCost90;
            dt.Rows[i]["类别"] = "销售成本";
            dt.Rows[i]["佳图"] = (Ssa + Ssc + Sse + Ssg).ToString("0.00");
            #endregion
            #region//销售成本(含税金额)
            double Ssaa = JCost51Add + JCost52Add + JCost54Add + JCost55Add + JCost56Add + JCost90;
            double Sscc = XCost51Add + XCost52Add + XCost54Add + XCost55Add + XCost56Add + XCost90;
            double Ssee = RCost51Add + RCost52Add + RCost54Add + RCost55Add + RCost56Add + RCost90;
            double Ssgg = HCost51Add + HCost52Add + HCost54Add + HCost55Add + HCost56Add + HCost90;
            dt.Rows[i]["欣图"] = (Ssaa + Sscc + Ssee + Ssgg).ToString("0.00");
            #endregion
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            //毛利
            #region//毛利(金额)
            double Mla = Jsa - Ssa;
            double Mlc = Jsc - Ssc;
            double Mle = Jse - Sse;
            double Mlg = Jsg - Ssg;
            dt.Rows[i]["类别"] = "毛利";
            double LVamount = Mla + Mlc + Mle + Mlg;
            dt.Rows[i]["佳图"] = LVamount.ToString("0.00");
            #endregion
            #region//毛利(含税金额)
            double Mlaa = Jsa - Ssa;
            double Mlcc = Jsc - Ssc;
            double Mlee = Jse - Sse;
            double Mlgg = Jsg - Ssg;
            double LLVamount = Mlaa + Mlcc + Mlee + Mlgg;
            dt.Rows[i]["欣图"] = LLVamount.ToString("0.00");
            #endregion
            dt.Rows.Add();
            i++;
            this.progressBar1.Value = i;
            //毛利率
            #region//毛利率(金额)
            dt.Rows[i]["类别"] = "毛利率";
            dt.Rows[i]["佳图"] = (LVamount / JSamount * 100).ToString("#0.#0") + "%";
            #endregion
            #region//毛利率
            dt.Rows[i]["欣图"] = (LLVamount / LJSamount * 100).ToString("#0.#0") + "%";
            #endregion

            #endregion

            this.progressBar1.Value = 0;
            return dt;
        }
        private void DefineReport()
        {
            Report.Clear();

            //定义报表主对象的属性
            Report.Font.Point = 9;

            //定义页眉
            DefinePageHeader();

            //定义页脚
            DefinePageFooter();

            //定义报表头
            DefineReportHeader();

            //定义明细网格
            DefineDetailGrid();
        }
        private void DefinePageHeader()
        {
            Report.InsertPageHeader();
            Report.PageHeader.Height = 0.58;

            //插入一个部件框
            //IGRStaticBox StaticBox = Report.PageHeader.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            //StaticBox.Text = "XX数码科技有限公司";            
            //StaticBox.ForeColor = 255 * 256 * 256 + 0 * 256 + 0;
            //StaticBox.TextAlign = GRTextAlign.grtaMiddleCenter;
            //StaticBox.Width = 8.20;
            //StaticBox.Height = 0.58;           
        }

        private void DefinePageFooter()
        {
            Report.InsertPageFooter();

            //插入一个系统变量框,显示页号
            IGRSystemVarBox PageNoBox = Report.PageFooter.Controls.Add(GRControlType.grctSystemVarBox).AsSystemVarBox;
            PageNoBox.SystemVar = GRSystemVarType.grsvPageNumber;
            PageNoBox.TextAlign = GRTextAlign.grtaMiddleRight;
            PageNoBox.Left = 12.78;
            PageNoBox.Top = 0;
            PageNoBox.Width = 1.40;
            PageNoBox.Height = 0.40;

            //插入一个静态文本框,显示页号与总页数中间的分隔斜线字符'/'
            IGRStaticBox StaticBox = Report.PageFooter.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            StaticBox.Text = "/";
            StaticBox.TextAlign = GRTextAlign.grtaMiddleCenter;
            StaticBox.Left = 14.18;
            StaticBox.Top = 0;
            StaticBox.Width = 0.40;
            StaticBox.Height = 0.40;

            //插入另一个系统变量框,显示页数
            IGRSystemVarBox PageCountBox = Report.PageFooter.Controls.Add(GRControlType.grctSystemVarBox).AsSystemVarBox;
            PageCountBox.SystemVar = GRSystemVarType.grsvPageCount;
            PageCountBox.Left = 14.58;
            PageCountBox.Top = 0;
            PageCountBox.Width = 1.40;
            PageCountBox.Height = 0.40;
        }

        private void DefineReportHeader()
        {
            IGRReportHeader Reportheader = Report.InsertReportHeader();
            Reportheader.Height = 1.38;

            //插入一个静态文本框,显示报表标题文字
            IGRStaticBox StaticBox = Reportheader.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            StaticBox.Text = "进销存日报表";
            StaticBox.Center = GRCenterStyle.grcsHorizontal; //使部件框在节中水平方向上居中对齐
            StaticBox.Font.Point = 15;
            StaticBox.Font.Bold = true;
            StaticBox.Top = 0.40;
            StaticBox.Width = 5.64;
            StaticBox.Height = 0.58;
        }
        private void DefineDetailGrid()
        {
            Report.InsertDetailGrid();
            Report.DetailGrid.ColumnTitle.Height = 0.68;
            Report.DetailGrid.ColumnContent.Height = 0.58;

            //Report.DetailGrid.ColumnContent.AlternatingBackColor = 151 * 256 * 256 + 255 * 256 + 255;//内容行交替色
            Report.DetailGrid.ColumnTitle.BackColor = 217 * 256 * 256 + 217 * 256 + 217; //标题行颜色

            //定义数据集的各个字段
            IGRRecordset RecordSet = Report.DetailGrid.Recordset;
            RecordSet.AddField("类别", GRFieldType.grftString);
            RecordSet.AddField("佳图", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("欣图", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("瑞博", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("信息", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("合计", GRFieldType.grftString).Format = "#,##0.00";

            //定义列(定义明细网格) 后部分为明细显示的位置方法
            Report.DetailGrid.AddColumn("类别", "类别", "类别", 3.78).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("佳图", "佳图", "佳图", 3.38).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
            Report.DetailGrid.AddColumn("欣图", "欣图", "欣图", 3.38).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
            Report.DetailGrid.AddColumn("瑞博", "瑞博", "瑞博", 3.38).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
            Report.DetailGrid.AddColumn("信息", "信息", "信息", 3.38).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
            Report.DetailGrid.AddColumn("合计", "合计", "合计", 3.38).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Report.Print(true);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer1.Stop();
            this.axGRDisplayViewer1.Start();
        }

        #region 双击弹出对应单据的明细
        /// <summary>
        /// 转换仓库名称
        /// </summary>
        /// <param name="name">仓库名称</param>
        /// <returns></returns>
        public string GetNameType(string name)
        {
            string type = "";
            switch (name)
            {
                case "佳图":
                    type = "J"; break;
                case "佳图空":
                    type = "X"; break;
                case "欣图空":
                    type = "R"; break;
                case "瑞博空":
                    type = "H"; break;
                default: break;
            }
            return type;
        }
        /// <summary>
        /// 显示对应单据报表
        /// </summary>
        /// <param name="rowHeaderName">行头名</param>
        /// <param name="columnName">列头名</param>
        public void SqlToReport(string rowHeaderName, string columnName)
        {
            string TypeName = "";
            string sql = "SELECT ReceiptId  单据号,InvoiceNo 发票编号, ReceiptTypeID  单据类别," +
                        "CustomerReceiptNo 自定义编号,CurWorkMonth 工作年月,OccurTime 单据日期," +
                        "CustName 客户名称,MatId 商品编号 ,MatName 商品名称,SourceStoreH 仓库," +
                        "MatType 类型,num 数量,price 含税单价,Amount 含税金额,TTaxPurchPrice 成本金额," +
                        "ML 毛利, lotCode 批号,VerifyPerson 验收员,BillUser 操作员,Memo 备注 " +
                        "FROM T_JXDetail where SourceStoreH='" + columnName + "' and ";
            string strsql = "";
            switch (rowHeaderName)
            {
                case "01": TypeName = "JHDetail";
                    strsql = sql + " ReceiptTypeID='01'"; break;
                case "03": TypeName = "JHDetail";
                    strsql = sql + " ReceiptTypeID='03'"; break;
                case "20": TypeName = "JHDetail";
                    strsql = sql + " ReceiptTypeID='20'"; break;
                case "51": TypeName = "XSDetail";
                    strsql = sql + " ReceiptTypeID='51'"; break;
                case "52": TypeName = "XSDetail";
                    strsql = sql + " ReceiptTypeID='52'"; break;
                case "53": TypeName = "XSDetail";
                    strsql = sql + " ReceiptTypeID='53'"; break;
                case "54": TypeName = "XSDetail";
                    strsql = sql + " ReceiptTypeID='54'"; break;
                case "55": TypeName = "XSDetail";
                    strsql = sql + " ReceiptTypeID='55'"; break;
                case "56": TypeName = "XSDetail";
                    strsql = sql + " ReceiptTypeID='56'"; break;
                case "75": TypeName = "XSDetail";
                    strsql = sql + " ReceiptTypeID='75'"; break;
                case "88": TypeName = "XSDetail";
                    strsql = sql + " ReceiptTypeID='88'"; break;
                case "90": TypeName = "XSDetail";
                    strsql = sql + " ReceiptTypeID='90'"; break;
                default: break;
            }
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = new DataTable();
            strsql += " and (OccurTime between '" + dateTimeFrom + "' and '" + dateTimeTo + "')";
            dt = db.Get_Dt(strsql);
            string count = dt.Rows.Count.ToString();
            //通过sql条件显示报表窗体
            PublicDetailForm form = new PublicDetailForm();
            form.MdiParent = this.MdiParent as MainForm;
            form.Show();
            form.strSql = strsql;
            form.count = count;
            form.ReiceName = TypeName;
            form.Username = (this.MdiParent as MainForm).userName;
            form.ShowReport();
        }
        private void axGRDisplayViewer1_ContentCellDblClick(object sender, global::AxgrproLib._IGRDisplayViewerEvents_ContentCellDblClickEvent e)
        {
            //int rowIndex = this.axGRDisplayViewer1.SelRowNo;
            //int columnIndex = this.axGRDisplayViewer1.SelColumnNo;
            //获取对应的列名
            string columnName = e.pSender.Column.Name;
            //获取对应列的行头值
            string rowHeaderName = Report.FieldByName("类别").AsString;
            columnName = GetNameType(columnName);
            rowHeaderName = rowHeaderName.Substring(0, 2);
            //只取有单据列别的行头
            if (!Util.StringIsNum(rowHeaderName)) return;
            SqlToReport(rowHeaderName, columnName);
        }
        #endregion
    }
}
