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
    public partial class StockStatusReportForm : Form
    {
        //定义Grid++Report报表主对象
        protected GridppReport Report = new GridppReport();
        protected GridppReport SubReport = new GridppReport();

        public StockStatusReportForm()
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

            this.axGRDisplayViewer1.Start();
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
            SqlDBConnect db = new SqlDBConnect();
            string storeHouseId = "J";
            int matType = 0;
            string dateTimeFrom = this.DateTimePickerFrom.Value.ToString("yyyy-MM-dd").Trim(); //"2011-07-01";//
            string dateTimeTo = this.DateTimePickerTo.Value.ToString("yyyy-MM-dd").Trim(); // "2011-07-31";//
            string firstDayOfCurMonth = (new DateTime(DateTimePickerFrom.Value.Year, DateTimePickerFrom.Value.Month, 1)).ToString().Trim();//当月第一天 //"2011-07-01";//
            string curWorkMonth = dateTimeFrom.Substring(0, 7);
            curWorkMonth = curWorkMonth.Remove(curWorkMonth.IndexOf('-'), 1); //当前工作月

            string strSql = "select FirstCount,FirstCostPrice,FirstMoney,BalanceTime from T_Stock_Status " +
                            "where StoreHouseId='{0}' and MatId='{1}' and MatType={2} and BalanceTime='{3}'";
            //strSql = string.Format(strSql, storeHouseId, matId, matType, maxBalanceTime);


            //string strSql = "select T_MatInf.MatId AS 物料编号,T_Receipt_Main.SourceStoreH 仓库编号,T_Receipts_Det.MatType 物料类型, " +
            //                       " dbo.T_MatInf.MatName AS 物料名称, dbo.T_MatInf.Specifications AS 型号规格, " +
            //                       " dbo.T_MatInf.Units AS 单位,  dbo.T_Stock_Status.FirstCount AS 期初数量 " +
            //                       " T_Receipt_Main.CurWorkMonth AS 工作年月, T_Receipt_Main.OccurTime AS 单据日期 " +
            //                       " into " + TempName + " FROM  dbo.T_MatInf " +
            //                       "  inner join T_Receipts_Det on T_Receipts_Det.MatId=T_MatInf.MatId " +
            //                       " inner join T_Receipt_Main on T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId " +
            //                       " left  JOIN dbo.T_Stock_Status ON dbo.T_Stock_Status.MatId = dbo.T_MatInf.MatId  " +
            //                       "  and T_Stock_Status.StoreHouseId=T_Receipt_Main.SourceStoreH and T_Receipts_Det.MatType=T_Stock_Status.MatType " +
            //                       " and T_Stock_Status.BalanceTime='" + maxbatime + "'";


            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("类别");    dc.Caption = "类别";
            DataColumn dc1 = new DataColumn("佳图");   dc1.Caption = "佳图";
            DataColumn dc2 = new DataColumn("佳图空"); dc2.Caption = "佳图空";
            DataColumn dc3 = new DataColumn("欣图");   dc3.Caption = "欣图";
            DataColumn dc4 = new DataColumn("欣图空"); dc4.Caption = "欣图空";
            DataColumn dc5 = new DataColumn("瑞博");   dc5.Caption = "瑞博";
            DataColumn dc6 = new DataColumn("瑞博空"); dc6.Caption = "瑞博空";
            DataColumn dc7 = new DataColumn("信息");   dc7.Caption = "信息";
            DataColumn dc8 = new DataColumn("信息空"); dc8.Caption = "信息空";
            DataColumn dc9 = new DataColumn("合计"); dc8.Caption = "合计";
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1); dt.Columns.Add(dc2);
            dt.Columns.Add(dc3); dt.Columns.Add(dc4);
            dt.Columns.Add(dc5); dt.Columns.Add(dc6);
            dt.Columns.Add(dc7); dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
                        
            int i = -1;
            //上日列帐
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "上日列帐";
            string sqlStockStatus = "select FirstCount,FirstCostPrice,FirstMoney,BalanceTime from T_Stock_Status " +
                                    "where BalanceTime='"+ curWorkMonth + "'";

            //dt.Rows[i]["佳图"] = sumJOut;
            //dt.Rows[i]["佳图空"] = sumJKOut;
            //dt.Rows[i]["欣图"] = sumXOut;
            //dt.Rows[i]["欣图空"] = sumXKOut;
            //dt.Rows[i]["瑞博"] = sumROut;
            //dt.Rows[i]["瑞博空"] = sumRKOut;
            //dt.Rows[i]["信息"] = sumHOut;
            //dt.Rows[i]["信息空"] = sumHKOut;
            //dt.Rows[i]["合计"] = sumJOut + sumJKOut + sumXOut + sumXKOut + sumROut + sumRKOut + sumHOut + sumHKOut;
            //暂估入库
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "暂估入库";

            //发出商品
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "发出商品";

            //实际结存
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "实际结存";
            
            //空行            
            dt.Rows.Add();
            i++;
            
            //01-进货单            
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "01-进货单";

            #region 01-进货单
            double JAmount01 = 0.0, JKAmount01 = 0.0, XAmount01 = 0.0, XKAmount01 = 0.0, RAmount01 = 0.0, RKAmount01 = 0.0, HAmount01 = 0.0, HKAmount01 = 0.0;
            double JNotTax01 = 0.0, JKNotTax01 = 0.0, XNotTax01 = 0.0, XKNotTax01 = 0.0, RNotTax01 = 0.0, RKNotTax01 = 0.0, HNotTax01 = 0.0, HKNotTax01 = 0.0;
            double JTax01 = 0.0, JKTax01 = 0.0, XTax01 = 0.0, XKTax01 = 0.0, RTax01 = 0.0, RKTax01 = 0.0, HTax01 = 0.0, HKTax01 = 0.0;
            double JCost01 = 0.0, JKCost01 = 0.0, XCost01 = 0.0, XKCost01 = 0.0, RCost01 = 0.0, RKCost01 = 0.0, HCost01 = 0.0, HKCost01 = 0.0;            

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount01 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["NotTax"].ToString().Trim() != "")
                            JKNotTax01 += Convert.ToDouble(dr["NotTax"].ToString().Trim());
                        if (dr["Tax"].ToString().Trim() != "")
                            JKTax01 += Convert.ToDouble(dr["Tax"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost01 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
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
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount01 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["NotTax"].ToString().Trim() != "")
                            XKNotTax01 += Convert.ToDouble(dr["NotTax"].ToString().Trim());
                        if (dr["Tax"].ToString().Trim() != "")
                            XKTax01 += Convert.ToDouble(dr["Tax"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost01 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
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
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount01 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["NotTax"].ToString().Trim() != "")
                            RKNotTax01 += Convert.ToDouble(dr["NotTax"].ToString().Trim());
                        if (dr["Tax"].ToString().Trim() != "")
                            RKTax01 += Convert.ToDouble(dr["Tax"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost01 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
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
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount01 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["NotTax"].ToString().Trim() != "")
                            HKNotTax01 += Convert.ToDouble(dr["NotTax"].ToString().Trim());
                        if (dr["Tax"].ToString().Trim() != "")
                            HKTax01 += Convert.ToDouble(dr["Tax"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost01 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //含税金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "含税金额";
            dt.Rows[i]["佳图"] = JAmount01;
            dt.Rows[i]["佳图空"] = JKAmount01;
            dt.Rows[i]["欣图"] = XAmount01;
            dt.Rows[i]["欣图空"] = XKAmount01;
            dt.Rows[i]["瑞博"] = RAmount01;
            dt.Rows[i]["瑞博空"] = RKAmount01;
            dt.Rows[i]["信息"] = HAmount01;
            dt.Rows[i]["信息空"] = HKAmount01;
            dt.Rows[i]["合计"] = JAmount01 + JKAmount01 + XAmount01 + XKAmount01 + RAmount01 + RKAmount01 + HAmount01 + HKAmount01;
            //不含税金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "不含税金额";
            dt.Rows[i]["佳图"] = JNotTax01;
            dt.Rows[i]["佳图空"] = JKNotTax01;
            dt.Rows[i]["欣图"] = XNotTax01;
            dt.Rows[i]["欣图空"] = XKNotTax01;
            dt.Rows[i]["瑞博"] = RNotTax01;
            dt.Rows[i]["瑞博空"] = RKNotTax01;
            dt.Rows[i]["信息"] = HNotTax01;
            dt.Rows[i]["信息空"] = HKNotTax01;
            dt.Rows[i]["合计"] = JNotTax01 + JKNotTax01 + XNotTax01 + XKNotTax01 + RNotTax01 + RKNotTax01 + HNotTax01 + HKNotTax01;
            //税额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "税额";
            dt.Rows[i]["佳图"] = JTax01;
            dt.Rows[i]["佳图空"] = JKTax01;
            dt.Rows[i]["欣图"] = XTax01;
            dt.Rows[i]["欣图空"] = XKTax01;
            dt.Rows[i]["瑞博"] = RTax01;
            dt.Rows[i]["瑞博空"] = RKTax01;
            dt.Rows[i]["信息"] = HTax01;
            dt.Rows[i]["信息空"] = HKTax01;
            dt.Rows[i]["合计"] = JTax01 + JKTax01 + XTax01 + XKTax01 + RTax01 + RKTax01 + HTax01 + HKTax01;
            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount01;
            dt.Rows[i]["佳图空"] = JKAmount01;
            dt.Rows[i]["欣图"] = XAmount01;
            dt.Rows[i]["欣图空"] = XKAmount01;
            dt.Rows[i]["瑞博"] = RAmount01;
            dt.Rows[i]["瑞博空"] = RKAmount01;
            dt.Rows[i]["信息"] = HAmount01;
            dt.Rows[i]["信息空"] = HKAmount01;
            dt.Rows[i]["合计"] = JAmount01 + JKAmount01 + XAmount01 + XKAmount01 + RAmount01 + RKAmount01 + HAmount01 + HKAmount01;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JAmount01;
            dt.Rows[i]["佳图空"] = JKAmount01;
            dt.Rows[i]["欣图"] = XAmount01;
            dt.Rows[i]["欣图空"] = XKAmount01;
            dt.Rows[i]["瑞博"] = RAmount01;
            dt.Rows[i]["瑞博空"] = RKAmount01;
            dt.Rows[i]["信息"] = HAmount01;
            dt.Rows[i]["信息空"] = HKAmount01;
            dt.Rows[i]["合计"] = JAmount01 + JKAmount01 + XAmount01 + XKAmount01 + RAmount01 + RKAmount01 + HAmount01 + HKAmount01;
            #endregion

            //03-假设进货单            
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "03-假设进货单";

            #region 03-假设进货单
            double JAmount03 = 0.0, JKAmount03 = 0.0, XAmount03 = 0.0, XKAmount03 = 0.0, RAmount03 = 0.0, RKAmount03 = 0.0, HAmount03 = 0.0, HKAmount03 = 0.0;           
            double JCost03 = 0.0, JKCost03 = 0.0, XCost03 = 0.0, XKCost03 = 0.0, RCost03 = 0.0, RKCost03 = 0.0, HCost03 = 0.0, HKCost03 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());                        
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());                       
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());                        
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());                       
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());                        
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());                        
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount03 += Convert.ToDouble(dr["Amount"].ToString().Trim());                       
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost03 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }
           
            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount03;
            dt.Rows[i]["佳图空"] = JKAmount03;
            dt.Rows[i]["欣图"] = XAmount03;
            dt.Rows[i]["欣图空"] = XKAmount03;
            dt.Rows[i]["瑞博"] = RAmount03;
            dt.Rows[i]["瑞博空"] = RKAmount03;
            dt.Rows[i]["信息"] = HAmount03;
            dt.Rows[i]["信息空"] = HKAmount03;
            dt.Rows[i]["合计"] = JAmount03 + JKAmount03 + XAmount03 + XKAmount03 + RAmount03 + RKAmount03 + HAmount03 + HKAmount03;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JAmount03;
            dt.Rows[i]["佳图空"] = JKAmount03;
            dt.Rows[i]["欣图"] = XAmount03;
            dt.Rows[i]["欣图空"] = XKAmount03;
            dt.Rows[i]["瑞博"] = RAmount03;
            dt.Rows[i]["瑞博空"] = RKAmount03;
            dt.Rows[i]["信息"] = HAmount03;
            dt.Rows[i]["信息空"] = HKAmount03;
            dt.Rows[i]["合计"] = JAmount03 + JKAmount03 + XAmount03 + XKAmount03 + RAmount03 + RKAmount03 + HAmount03 + HKAmount03;
            #endregion

            //04-盈亏单          
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "04-盈亏单";

            #region 04-盈亏单
            double JCost04 = 0.0, JKCost04 = 0.0, XCost04 = 0.0, XKCost04 = 0.0, RCost04 = 0.0, RKCost04 = 0.0, HCost04 = 0.0, HKCost04 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKCost04 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                }
            }            

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost04;
            dt.Rows[i]["佳图空"] = JKCost04;
            dt.Rows[i]["欣图"] = XCost04;
            dt.Rows[i]["欣图空"] = XKCost04;
            dt.Rows[i]["瑞博"] = RCost04;
            dt.Rows[i]["瑞博空"] = RKCost04;
            dt.Rows[i]["信息"] = HCost04;
            dt.Rows[i]["信息空"] = HKCost04;
            dt.Rows[i]["合计"] = JCost04 + JKCost04 + XCost04 + XKCost04 + RCost04 + RKCost04 + HCost04 + HKCost04;
            #endregion

            //20-假设结账单          
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "20-假设结账单";
            #region 20-假设结账单

            double JCost20 = 0.0, JKCost20 = 0.0, XCost20 = 0.0, XKCost20 = 0.0, RCost20 = 0.0, RKCost20 = 0.0, HCost20 = 0.0, HKCost20 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKCost20 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                    }
                }
            }

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost20;
            dt.Rows[i]["佳图空"] = JKCost20;
            dt.Rows[i]["欣图"] = XCost20;
            dt.Rows[i]["欣图空"] = XKCost20;
            dt.Rows[i]["瑞博"] = RCost20;
            dt.Rows[i]["瑞博空"] = RKCost20;
            dt.Rows[i]["信息"] = HCost20;
            dt.Rows[i]["信息空"] = HKCost20;
            dt.Rows[i]["合计"] = JCost20 + JKCost20 + XCost20 + XKCost20 + RCost20 + RKCost20 + HCost20 + HKCost20;
            #endregion

            //收入小计            
            dt.Rows.Add();
            i++;
            #region 收入小计
            double sumJIn=JAmount01 + JAmount03 + JCost04 + JCost20;
            double sumJKIn=JKAmount01 + JKAmount03 + JKCost04 + JKCost20;
            double sumXIn=XAmount01 + XAmount03 + XCost04 + XCost20;
            double sumXKIn= XKAmount01 + XKAmount03 + XKCost04 + XKCost20;
            double sumRIn=RAmount01 + RAmount03 + RCost04 + RCost20;
            double sumRKIn=RKAmount01 + RKAmount03 + RKCost04 + RKCost20;
            double sumHIn=HAmount01 + HAmount03 + HCost04 + HCost20;
            double sumHKIn=HKAmount01 + HKAmount03 + HKCost04 + HKCost20;
            dt.Rows[i]["类别"] = "收入小计";
            dt.Rows[i]["佳图"] = sumJIn;
            dt.Rows[i]["佳图空"] = sumJKIn;
            dt.Rows[i]["欣图"] = sumXIn;
            dt.Rows[i]["欣图空"] =sumXKIn;
            dt.Rows[i]["瑞博"] = sumRIn;
            dt.Rows[i]["瑞博空"] = sumRKIn;
            dt.Rows[i]["信息"] = sumHIn;
            dt.Rows[i]["信息空"] = sumHKIn;
            dt.Rows[i]["合计"] = sumJIn + sumJKIn + sumXIn + sumXKIn + sumRIn + sumRKIn + sumHIn + sumHKIn;
            #endregion

            //空行            
            dt.Rows.Add();
            i++;

            //51-销售单1            
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "51-销售单1";

            #region 51-销售单1
            double JAmount51 = 0.0, JKAmount51 = 0.0, XAmount51 = 0.0, XKAmount51 = 0.0, RAmount51 = 0.0, RKAmount51 = 0.0, HAmount51 = 0.0, HKAmount51 = 0.0;            
            double JCost51 = 0.0, JKCost51 = 0.0, XCost51 = 0.0, XKCost51 = 0.0, RCost51 = 0.0, RKCost51 = 0.0, HCost51 = 0.0, HKCost51 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());                       
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());                        
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());                       
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());                       
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());                       
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());                        
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount51 += Convert.ToDouble(dr["Amount"].ToString().Trim());                        
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost51 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }
            
            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount51;
            dt.Rows[i]["佳图空"] = JKAmount51;
            dt.Rows[i]["欣图"] = XAmount51;
            dt.Rows[i]["欣图空"] = XKAmount51;
            dt.Rows[i]["瑞博"] = RAmount51;
            dt.Rows[i]["瑞博空"] = RKAmount51;
            dt.Rows[i]["信息"] = HAmount51;
            dt.Rows[i]["信息空"] = HKAmount51;
            dt.Rows[i]["合计"] = JAmount51 + JKAmount51 + XAmount51 + XKAmount51 + RAmount51 + RKAmount51 + HAmount51 + HKAmount51;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost51;
            dt.Rows[i]["佳图空"] = JKCost51;
            dt.Rows[i]["欣图"] = XCost51;
            dt.Rows[i]["欣图空"] = XKCost51;
            dt.Rows[i]["瑞博"] = RCost51;
            dt.Rows[i]["瑞博空"] = RKCost51;
            dt.Rows[i]["信息"] = HCost51;
            dt.Rows[i]["信息空"] = HKCost51;
            dt.Rows[i]["合计"] = JCost51 + JKCost51 + XCost51 + XKCost51 + RCost51 + RKCost51 + HCost51 + HKCost51;

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = JAmount51-JCost51;
            dt.Rows[i]["佳图空"] = JKAmount51-JKCost51;
            dt.Rows[i]["欣图"] = XAmount51-XCost51;
            dt.Rows[i]["欣图空"] = XKAmount51-XKCost51;
            dt.Rows[i]["瑞博"] = RAmount51-RCost51;
            dt.Rows[i]["瑞博空"] = RKAmount51-RKCost51;
            dt.Rows[i]["信息"] = HAmount51-HCost51;
            dt.Rows[i]["信息空"] = HKAmount51-HKCost51;
            dt.Rows[i]["合计"] = JAmount51 - JCost51 + JKAmount51 - JKCost51 + XAmount51 - XCost51 + XKAmount51 - XKCost51 +
                                 RAmount51 - RCost51 + RKAmount51 - RKCost51 + HAmount51 - HCost51 + HKAmount51 - HKCost51;

            //求“当月第1天”到“开始时间” 价税合计、成本金额、毛利
            double JAmount51Add = 0.0, JKAmount51Add = 0.0, XAmount51Add = 0.0, XKAmount51Add = 0.0, RAmount51Add = 0.0, RKAmount51Add = 0.0, HAmount51Add = 0.0, HKAmount51Add = 0.0;
            double JCost51Add = 0.0, JKCost51Add = 0.0, XCost51Add = 0.0, XKCost51Add = 0.0, RCost51Add = 0.0, RKCost51Add = 0.0, HCost51Add = 0.0, HKCost51Add = 0.0;

            string sql51Add= "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount51Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost51Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //累计：价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：价税合计";
            dt.Rows[i]["佳图"] = JAmount51Add;
            dt.Rows[i]["佳图空"] = JKAmount51Add;
            dt.Rows[i]["欣图"] = XAmount51Add;
            dt.Rows[i]["欣图空"] = XKAmount51Add;
            dt.Rows[i]["瑞博"] = RAmount51Add;
            dt.Rows[i]["瑞博空"] = RKAmount51Add;
            dt.Rows[i]["信息"] = HAmount51Add;
            dt.Rows[i]["信息空"] = HKAmount51Add;
            dt.Rows[i]["合计"] = JAmount51Add + JKAmount51Add + XAmount51Add + XKAmount51Add + RAmount51Add + RKAmount51Add + HAmount51Add + HKAmount51Add;

            //累计：成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：成本金额";
            dt.Rows[i]["佳图"] = JCost51Add;
            dt.Rows[i]["佳图空"] = JKCost51Add;
            dt.Rows[i]["欣图"] = XCost51Add;
            dt.Rows[i]["欣图空"] = XKCost51Add;
            dt.Rows[i]["瑞博"] = RCost51Add;
            dt.Rows[i]["瑞博空"] = RKCost51Add;
            dt.Rows[i]["信息"] = HCost51Add;
            dt.Rows[i]["信息空"] = HKCost51Add;
            dt.Rows[i]["合计"] = JCost51Add + JKCost51Add + XCost51Add + XKCost51Add + RCost51Add + RKCost51Add + HCost51Add + HKCost51Add;

            //累计：毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：毛利";
            dt.Rows[i]["佳图"] = JAmount51Add - JCost51Add;
            dt.Rows[i]["佳图空"] = JKAmount51Add - JKCost51Add;
            dt.Rows[i]["欣图"] = XAmount51Add - XCost51Add;
            dt.Rows[i]["欣图空"] = XKAmount51Add - XKCost51Add;
            dt.Rows[i]["瑞博"] = RAmount51Add - RCost51Add;
            dt.Rows[i]["瑞博空"] = RKAmount51Add - RKCost51Add;
            dt.Rows[i]["信息"] = HAmount51Add - HCost51Add;
            dt.Rows[i]["信息空"] = HKAmount51Add - HKCost51Add;
            dt.Rows[i]["合计"] = JAmount51Add - JCost51Add + JKAmount51Add - JKCost51Add + XAmount51Add - XCost51Add + XKAmount51Add - XKCost51Add +
                                 RAmount51Add - RCost51Add + RKAmount51Add - RKCost51Add + HAmount51Add - HCost51Add + HKAmount51Add - HKCost51Add;
            #endregion

            //52-销售单2            
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "52-销售单2";

            #region 52-销售单2
            double JAmount52 = 0.0, JKAmount52 = 0.0, XAmount52 = 0.0, XKAmount52 = 0.0, RAmount52 = 0.0, RKAmount52 = 0.0, HAmount52 = 0.0, HKAmount52 = 0.0;
            double JCost52 = 0.0, JKCost52 = 0.0, XCost52 = 0.0, XKCost52 = 0.0, RCost52 = 0.0, RKCost52 = 0.0, HCost52 = 0.0, HKCost52 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount52 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost52 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount52;
            dt.Rows[i]["佳图空"] = JKAmount52;
            dt.Rows[i]["欣图"] = XAmount52;
            dt.Rows[i]["欣图空"] = XKAmount52;
            dt.Rows[i]["瑞博"] = RAmount52;
            dt.Rows[i]["瑞博空"] = RKAmount52;
            dt.Rows[i]["信息"] = HAmount52;
            dt.Rows[i]["信息空"] = HKAmount52;
            dt.Rows[i]["合计"] = JAmount52 + JKAmount52 + XAmount52 + XKAmount52 + RAmount52 + RKAmount52 + HAmount52 + HKAmount52;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost52;
            dt.Rows[i]["佳图空"] = JKCost52;
            dt.Rows[i]["欣图"] = XCost52;
            dt.Rows[i]["欣图空"] = XKCost52;
            dt.Rows[i]["瑞博"] = RCost52;
            dt.Rows[i]["瑞博空"] = RKCost52;
            dt.Rows[i]["信息"] = HCost52;
            dt.Rows[i]["信息空"] = HKCost52;
            dt.Rows[i]["合计"] = JCost52 + JKCost52 + XCost52 + XKCost52 + RCost52 + RKCost52 + HCost52 + HKCost52;

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = JAmount52 - JCost52;
            dt.Rows[i]["佳图空"] = JKAmount52 - JKCost52;
            dt.Rows[i]["欣图"] = XAmount52 - XCost52;
            dt.Rows[i]["欣图空"] = XKAmount52 - XKCost52;
            dt.Rows[i]["瑞博"] = RAmount52 - RCost52;
            dt.Rows[i]["瑞博空"] = RKAmount52 - RKCost52;
            dt.Rows[i]["信息"] = HAmount52 - HCost52;
            dt.Rows[i]["信息空"] = HKAmount52 - HKCost52;
            dt.Rows[i]["合计"] = JAmount52 - JCost52 + JKAmount52 - JKCost52 + XAmount52 - XCost52 + XKAmount52 - XKCost52 +
                                 RAmount52 - RCost52 + RKAmount52 - RKCost52 + HAmount52 - HCost52 + HKAmount52 - HKCost52;

            //求“当月第1天”到“开始时间” 价税合计、成本金额、毛利
            double JAmount52Add = 0.0, JKAmount52Add = 0.0, XAmount52Add = 0.0, XKAmount52Add = 0.0, RAmount52Add = 0.0, RKAmount52Add = 0.0, HAmount52Add = 0.0, HKAmount52Add = 0.0;
            double JCost52Add = 0.0, JKCost52Add = 0.0, XCost52Add = 0.0, XKCost52Add = 0.0, RCost52Add = 0.0, RKCost52Add = 0.0, HCost52Add = 0.0, HKCost52Add = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount52Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost52Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //累计：价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：价税合计";
            dt.Rows[i]["佳图"] = JAmount52Add;
            dt.Rows[i]["佳图空"] = JKAmount52Add;
            dt.Rows[i]["欣图"] = XAmount52Add;
            dt.Rows[i]["欣图空"] = XKAmount52Add;
            dt.Rows[i]["瑞博"] = RAmount52Add;
            dt.Rows[i]["瑞博空"] = RKAmount52Add;
            dt.Rows[i]["信息"] = HAmount52Add;
            dt.Rows[i]["信息空"] = HKAmount52Add;
            dt.Rows[i]["合计"] = JAmount52Add + JKAmount52Add + XAmount52Add + XKAmount52Add + RAmount52Add + RKAmount52Add + HAmount52Add + HKAmount52Add;

            //累计：成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：成本金额";
            dt.Rows[i]["佳图"] = JCost52Add;
            dt.Rows[i]["佳图空"] = JKCost52Add;
            dt.Rows[i]["欣图"] = XCost52Add;
            dt.Rows[i]["欣图空"] = XKCost52Add;
            dt.Rows[i]["瑞博"] = RCost52Add;
            dt.Rows[i]["瑞博空"] = RKCost52Add;
            dt.Rows[i]["信息"] = HCost52Add;
            dt.Rows[i]["信息空"] = HKCost52Add;
            dt.Rows[i]["合计"] = JCost52Add + JKCost52Add + XCost52Add + XKCost52Add + RCost52Add + RKCost52Add + HCost52Add + HKCost52Add;

            //累计：毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：毛利";
            dt.Rows[i]["佳图"] = JAmount52Add - JCost52Add;
            dt.Rows[i]["佳图空"] = JKAmount52Add - JKCost52Add;
            dt.Rows[i]["欣图"] = XAmount52Add - XCost52Add;
            dt.Rows[i]["欣图空"] = XKAmount52Add - XKCost52Add;
            dt.Rows[i]["瑞博"] = RAmount52Add - RCost52Add;
            dt.Rows[i]["瑞博空"] = RKAmount52Add - RKCost52Add;
            dt.Rows[i]["信息"] = HAmount52Add - HCost52Add;
            dt.Rows[i]["信息空"] = HKAmount52Add - HKCost52Add;
            dt.Rows[i]["合计"] = JAmount52Add - JCost52Add + JKAmount52Add - JKCost52Add + XAmount52Add - XCost52Add + XKAmount52Add - XKCost52Add +
                                 RAmount52Add - RCost52Add + RKAmount52Add - RKCost52Add + HAmount52Add - HCost52Add + HKAmount52Add - HKCost52Add;
            #endregion

            //53-调拨单           
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "53-调拨单";

            #region 53-调拨单
            double JAmount53 = 0.0, JKAmount53 = 0.0, XAmount53 = 0.0, XKAmount53 = 0.0, RAmount53 = 0.0, RKAmount53 = 0.0, HAmount53 = 0.0, HKAmount53 = 0.0;
            double JCost53 = 0.0, JKCost53 = 0.0, XCost53 = 0.0, XKCost53 = 0.0, RCost53 = 0.0, RKCost53 = 0.0, HCost53 = 0.0, HKCost53 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount53 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost53 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount53;
            dt.Rows[i]["佳图空"] = JKAmount53;
            dt.Rows[i]["欣图"] = XAmount53;
            dt.Rows[i]["欣图空"] = XKAmount53;
            dt.Rows[i]["瑞博"] = RAmount53;
            dt.Rows[i]["瑞博空"] = RKAmount53;
            dt.Rows[i]["信息"] = HAmount53;
            dt.Rows[i]["信息空"] = HKAmount53;
            dt.Rows[i]["合计"] = JAmount53 + JKAmount53 + XAmount53 + XKAmount53 + RAmount53 + RKAmount53 + HAmount53 + HKAmount53;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost53;
            dt.Rows[i]["佳图空"] = JKCost53;
            dt.Rows[i]["欣图"] = XCost53;
            dt.Rows[i]["欣图空"] = XKCost53;
            dt.Rows[i]["瑞博"] = RCost53;
            dt.Rows[i]["瑞博空"] = RKCost53;
            dt.Rows[i]["信息"] = HCost53;
            dt.Rows[i]["信息空"] = HKCost53;
            dt.Rows[i]["合计"] = JCost53 + JKCost53 + XCost53 + XKCost53 + RCost53 + RKCost53 + HCost53 + HKCost53;

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = JAmount53 - JCost53;
            dt.Rows[i]["佳图空"] = JKAmount53 - JKCost53;
            dt.Rows[i]["欣图"] = XAmount53 - XCost53;
            dt.Rows[i]["欣图空"] = XKAmount53 - XKCost53;
            dt.Rows[i]["瑞博"] = RAmount53 - RCost53;
            dt.Rows[i]["瑞博空"] = RKAmount53 - RKCost53;
            dt.Rows[i]["信息"] = HAmount53 - HCost53;
            dt.Rows[i]["信息空"] = HKAmount53 - HKCost53;
            dt.Rows[i]["合计"] = JAmount53 - JCost53 + JKAmount53- JKCost53 + XAmount53 - XCost53 + XKAmount53 - XKCost53 +
                                 RAmount53 - RCost53 + RKAmount53 - RKCost53 + HAmount53 - HCost53 + HKAmount53 - HKCost53;
            #endregion

            //54-销售单4           
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "54-销售单4";

            #region 54-销售单4
            double JAmount54 = 0.0, JKAmount54 = 0.0, XAmount54 = 0.0, XKAmount54 = 0.0, RAmount54 = 0.0, RKAmount54 = 0.0, HAmount54 = 0.0, HKAmount54 = 0.0;
            double JCost54 = 0.0, JKCost54 = 0.0, XCost54 = 0.0, XKCost54 = 0.0, RCost54 = 0.0, RKCost54 = 0.0, HCost54 = 0.0, HKCost54 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount54 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost54 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount54;
            dt.Rows[i]["佳图空"] = JKAmount54;
            dt.Rows[i]["欣图"] = XAmount54;
            dt.Rows[i]["欣图空"] = XKAmount54;
            dt.Rows[i]["瑞博"] = RAmount54;
            dt.Rows[i]["瑞博空"] = RKAmount54;
            dt.Rows[i]["信息"] = HAmount54;
            dt.Rows[i]["信息空"] = HKAmount54;
            dt.Rows[i]["合计"] = JAmount54 + JKAmount54 + XAmount54 + XKAmount54 + RAmount54 + RKAmount54 + HAmount54 + HKAmount54;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost54;
            dt.Rows[i]["佳图空"] = JKCost54;
            dt.Rows[i]["欣图"] = XCost54;
            dt.Rows[i]["欣图空"] = XKCost54;
            dt.Rows[i]["瑞博"] = RCost54;
            dt.Rows[i]["瑞博空"] = RKCost54;
            dt.Rows[i]["信息"] = HCost54;
            dt.Rows[i]["信息空"] = HKCost54;
            dt.Rows[i]["合计"] = JCost54 + JKCost54 + XCost54 + XKCost54 + RCost54 + RKCost54 + HCost54 + HKCost54;

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = JAmount54 - JCost54;
            dt.Rows[i]["佳图空"] = JKAmount54 - JKCost54;
            dt.Rows[i]["欣图"] = XAmount54 - XCost54;
            dt.Rows[i]["欣图空"] = XKAmount54 - XKCost54;
            dt.Rows[i]["瑞博"] = RAmount54 - RCost54;
            dt.Rows[i]["瑞博空"] = RKAmount54 - RKCost54;
            dt.Rows[i]["信息"] = HAmount54 - HCost54;
            dt.Rows[i]["信息空"] = HKAmount54 - HKCost54;
            dt.Rows[i]["合计"] = JAmount54 - JCost54 + JKAmount54 - JKCost54 + XAmount54 - XCost54 + XKAmount54 - XKCost54 +
                                 RAmount54 - RCost54 + RKAmount54 - RKCost54 + HAmount54 - HCost54 + HKAmount54 - HKCost54;
            #endregion

            //55-销售单3           
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "55-销售单3";

            #region 55-销售单3
            double JAmount55 = 0.0, JKAmount55 = 0.0, XAmount55 = 0.0, XKAmount55 = 0.0, RAmount55 = 0.0, RKAmount55 = 0.0, HAmount55 = 0.0, HKAmount55 = 0.0;
            double JCost55 = 0.0, JKCost55 = 0.0, XCost55 = 0.0, XKCost55 = 0.0, RCost55 = 0.0, RKCost55 = 0.0, HCost55 = 0.0, HKCost55 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount55 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost55 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount55;
            dt.Rows[i]["佳图空"] = JKAmount55;
            dt.Rows[i]["欣图"] = XAmount55;
            dt.Rows[i]["欣图空"] = XKAmount55;
            dt.Rows[i]["瑞博"] = RAmount55;
            dt.Rows[i]["瑞博空"] = RKAmount55;
            dt.Rows[i]["信息"] = HAmount55;
            dt.Rows[i]["信息空"] = HKAmount55;
            dt.Rows[i]["合计"] = JAmount55 + JKAmount55 + XAmount55 + XKAmount55 + RAmount55 + RKAmount55 + HAmount55 + HKAmount55;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost55;
            dt.Rows[i]["佳图空"] = JKCost55;
            dt.Rows[i]["欣图"] = XCost55;
            dt.Rows[i]["欣图空"] = XKCost55;
            dt.Rows[i]["瑞博"] = RCost55;
            dt.Rows[i]["瑞博空"] = RKCost55;
            dt.Rows[i]["信息"] = HCost55;
            dt.Rows[i]["信息空"] = HKCost55;
            dt.Rows[i]["合计"] = JCost55 + JKCost55 + XCost55 + XKCost55 + RCost55 + RKCost55 + HCost55 + HKCost55;

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = JAmount55 - JCost55;
            dt.Rows[i]["佳图空"] = JKAmount55 - JKCost55;
            dt.Rows[i]["欣图"] = XAmount55 - XCost55;
            dt.Rows[i]["欣图空"] = XKAmount55 - XKCost55;
            dt.Rows[i]["瑞博"] = RAmount55 - RCost55;
            dt.Rows[i]["瑞博空"] = RKAmount55 - RKCost55;
            dt.Rows[i]["信息"] = HAmount55 - HCost55;
            dt.Rows[i]["信息空"] = HKAmount55 - HKCost55;
            dt.Rows[i]["合计"] = JAmount55 - JCost55 + JKAmount55 - JKCost55 + XAmount55 - XCost55 + XKAmount55 - XKCost55 +
                                 RAmount55 - RCost55 + RKAmount55 - RKCost55 + HAmount55 - HCost55 + HKAmount55 - HKCost55;
            #endregion

            //56-销售单5            
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "56-销售单5";

            #region 56-销售单5
            double JAmount56 = 0.0, JKAmount56 = 0.0, XAmount56 = 0.0, XKAmount56 = 0.0, RAmount56 = 0.0, RKAmount56 = 0.0, HAmount56 = 0.0, HKAmount56 = 0.0;
            double JCost56= 0.0, JKCost56 = 0.0, XCost56 = 0.0, XKCost56 = 0.0, RCost56 = 0.0, RKCost56 = 0.0, HCost56 = 0.0, HKCost56 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount56 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost56 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount56;
            dt.Rows[i]["佳图空"] = JKAmount56;
            dt.Rows[i]["欣图"] = XAmount56;
            dt.Rows[i]["欣图空"] = XKAmount56;
            dt.Rows[i]["瑞博"] = RAmount56;
            dt.Rows[i]["瑞博空"] = RKAmount56;
            dt.Rows[i]["信息"] = HAmount56;
            dt.Rows[i]["信息空"] = HKAmount56;
            dt.Rows[i]["合计"] = JAmount56 + JKAmount56 + XAmount56 + XKAmount56 + RAmount56 + RKAmount56 + HAmount56 + HKAmount56;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost56;
            dt.Rows[i]["佳图空"] = JKCost56;
            dt.Rows[i]["欣图"] = XCost56;
            dt.Rows[i]["欣图空"] = XKCost56;
            dt.Rows[i]["瑞博"] = RCost56;
            dt.Rows[i]["瑞博空"] = RKCost56;
            dt.Rows[i]["信息"] = HCost56;
            dt.Rows[i]["信息空"] = HKCost56;
            dt.Rows[i]["合计"] = JCost56 + JKCost56 + XCost56 + XKCost56 + RCost56 + RKCost56 + HCost56 + HKCost56;

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = JAmount56 - JCost56;
            dt.Rows[i]["佳图空"] = JKAmount56 - JKCost56;
            dt.Rows[i]["欣图"] = XAmount56 - XCost56;
            dt.Rows[i]["欣图空"] = XKAmount56 - XKCost56;
            dt.Rows[i]["瑞博"] = RAmount56 - RCost56;
            dt.Rows[i]["瑞博空"] = RKAmount56 - RKCost56;
            dt.Rows[i]["信息"] = HAmount56 - HCost56;
            dt.Rows[i]["信息空"] = HKAmount56 - HKCost56;
            dt.Rows[i]["合计"] = JAmount56 - JCost56 + JKAmount56 - JKCost56 + XAmount56 - XCost56 + XKAmount56 - XKCost56 +
                                 RAmount56 - RCost56 + RKAmount56 - RKCost56 + HAmount56 - HCost56 + HKAmount56 - HKCost56;

            //求“当月第1天”到“开始时间” 价税合计、成本金额、毛利
            double JAmount56Add = 0.0, JKAmount56Add = 0.0, XAmount56Add = 0.0, XKAmount56Add = 0.0, RAmount56Add = 0.0, RKAmount56Add = 0.0, HAmount56Add = 0.0, HKAmount56Add = 0.0;
            double JCost56Add = 0.0, JKCost56Add = 0.0, XCost56Add = 0.0, XKCost56Add = 0.0, RCost56Add = 0.0, RKCost56Add = 0.0, HCost56Add = 0.0, HKCost56Add = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount56Add += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost56Add += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //累计：价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：价税合计";
            dt.Rows[i]["佳图"] = JAmount56Add;
            dt.Rows[i]["佳图空"] = JKAmount56Add;
            dt.Rows[i]["欣图"] = XAmount56Add;
            dt.Rows[i]["欣图空"] = XKAmount56Add;
            dt.Rows[i]["瑞博"] = RAmount56Add;
            dt.Rows[i]["瑞博空"] = RKAmount56Add;
            dt.Rows[i]["信息"] = HAmount56Add;
            dt.Rows[i]["信息空"] = HKAmount56Add;
            dt.Rows[i]["合计"] = JAmount56Add + JKAmount56Add + XAmount56Add + XKAmount56Add + RAmount56Add + RKAmount56Add + HAmount56Add + HKAmount56Add;

            //累计：成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：成本金额";
            dt.Rows[i]["佳图"] = JCost56Add;
            dt.Rows[i]["佳图空"] = JKCost56Add;
            dt.Rows[i]["欣图"] = XCost56Add;
            dt.Rows[i]["欣图空"] = XKCost56Add;
            dt.Rows[i]["瑞博"] = RCost56Add;
            dt.Rows[i]["瑞博空"] = RKCost56Add;
            dt.Rows[i]["信息"] = HCost56Add;
            dt.Rows[i]["信息空"] = HKCost56Add;
            dt.Rows[i]["合计"] = JCost56Add + JKCost56Add + XCost56Add + XKCost56Add + RCost56Add + RKCost56Add + HCost56Add + HKCost56Add;

            //累计：毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "累计：毛利";
            dt.Rows[i]["佳图"] = JAmount56Add - JCost56Add;
            dt.Rows[i]["佳图空"] = JKAmount56Add - JKCost56Add;
            dt.Rows[i]["欣图"] = XAmount56Add - XCost56Add;
            dt.Rows[i]["欣图空"] = XKAmount56Add - XKCost56Add;
            dt.Rows[i]["瑞博"] = RAmount56Add - RCost56Add;
            dt.Rows[i]["瑞博空"] = RKAmount56Add - RKCost56Add;
            dt.Rows[i]["信息"] = HAmount56Add - HCost56Add;
            dt.Rows[i]["信息空"] = HKAmount56Add - HKCost56Add;
            dt.Rows[i]["合计"] = JAmount56Add - JCost56Add + JKAmount56Add - JKCost56Add + XAmount56Add - XCost56Add + XKAmount56Add - XKCost56Add +
                                 RAmount56Add - RCost56Add + RKAmount56Add - RKCost56Add + HAmount56Add - HCost56Add + HKAmount56Add - HKCost56Add;
            #endregion

            //75-借用单           
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "75-借用单";

            #region 75-借用单
            double JAmount75 = 0.0, JKAmount75 = 0.0, XAmount75 = 0.0, XKAmount75 = 0.0, RAmount75 = 0.0, RKAmount75 = 0.0, HAmount75 = 0.0, HKAmount75 = 0.0;
            double JCost75 = 0.0, JKCost75 = 0.0, XCost75 = 0.0, XKCost75 = 0.0, RCost75 = 0.0, RKCost75 = 0.0, HCost75 = 0.0, HKCost75 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount75 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost75 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount75;
            dt.Rows[i]["佳图空"] = JKAmount75;
            dt.Rows[i]["欣图"] = XAmount75;
            dt.Rows[i]["欣图空"] = XKAmount75;
            dt.Rows[i]["瑞博"] = RAmount75;
            dt.Rows[i]["瑞博空"] = RKAmount75;
            dt.Rows[i]["信息"] = HAmount75;
            dt.Rows[i]["信息空"] = HKAmount75;
            dt.Rows[i]["合计"] = JAmount75 + JKAmount75 + XAmount75 + XKAmount75 + RAmount75 + RKAmount75 + HAmount75 + HKAmount75;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost75;
            dt.Rows[i]["佳图空"] = JKCost75;
            dt.Rows[i]["欣图"] = XCost75;
            dt.Rows[i]["欣图空"] = XKCost75;
            dt.Rows[i]["瑞博"] = RCost75;
            dt.Rows[i]["瑞博空"] = RKCost75;
            dt.Rows[i]["信息"] = HCost75;
            dt.Rows[i]["信息空"] = HKCost75;
            dt.Rows[i]["合计"] = JCost75 + JKCost75 + XCost75 + XKCost75 + RCost75 + RKCost75 + HCost75 + HKCost75;

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = JAmount75 - JCost75;
            dt.Rows[i]["佳图空"] = JKAmount75 - JKCost75;
            dt.Rows[i]["欣图"] = XAmount75 - XCost75;
            dt.Rows[i]["欣图空"] = XKAmount75 - XKCost75;
            dt.Rows[i]["瑞博"] = RAmount75 - RCost75;
            dt.Rows[i]["瑞博空"] = RKAmount75 - RKCost75;
            dt.Rows[i]["信息"] = HAmount75 - HCost75;
            dt.Rows[i]["信息空"] = HKAmount75 - HKCost75;
            dt.Rows[i]["合计"] = JAmount75 - JCost75 + JKAmount75 - JKCost75 + XAmount75 - XCost75 + XKAmount75 - XKCost75 +
                                 RAmount75 - RCost75 + RKAmount75 - RKCost75 + HAmount75 - HCost75 + HKAmount75 - HKCost75;
            #endregion

            //88-租机单          
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "88-租机单";

            #region 88-租机单
            double JAmount88 = 0.0, JKAmount88 = 0.0, XAmount88 = 0.0, XKAmount88 = 0.0, RAmount88 = 0.0, RKAmount88 = 0.0, HAmount88 = 0.0, HKAmount88 = 0.0;
            double JCost88 = 0.0, JKCost88 = 0.0, XCost88 = 0.0, XKCost88 = 0.0, RCost88 = 0.0, RKCost88 = 0.0, HCost88 = 0.0, HKCost88 = 0.0;

            string sql88 = "select OccurTime, SourceStoreH, num, price, Amount,NotTax,Tax,STaxPurchPrice,TTaxPurchPrice from T_Receipt_Main_Det " +
                           "where ReceiptTypeID = '88' and (OccurTime between '{0}' and '{1}')";
            sql88 = string.Format(sql88, dateTimeFrom, dateTimeTo);
            DataTable dt88= db.Get_Dt(sql88);
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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            JKAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            XKAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            RKAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost88+= Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["Amount"].ToString().Trim() != "")
                            HKAmount88 += Convert.ToDouble(dr["Amount"].ToString().Trim());
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost88 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }

            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            dt.Rows[i]["佳图"] = JAmount88;
            dt.Rows[i]["佳图空"] = JKAmount88;
            dt.Rows[i]["欣图"] = XAmount88;
            dt.Rows[i]["欣图空"] = XKAmount88;
            dt.Rows[i]["瑞博"] = RAmount88;
            dt.Rows[i]["瑞博空"] = RKAmount88;
            dt.Rows[i]["信息"] = HAmount88;
            dt.Rows[i]["信息空"] = HKAmount88;
            dt.Rows[i]["合计"] = JAmount88 + JKAmount88 + XAmount88 + XKAmount88 + RAmount88 + RKAmount88 + HAmount88 + HKAmount88;

            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost88;
            dt.Rows[i]["佳图空"] = JKCost88;
            dt.Rows[i]["欣图"] = XCost88;
            dt.Rows[i]["欣图空"] = XKCost88;
            dt.Rows[i]["瑞博"] = RCost88;
            dt.Rows[i]["瑞博空"] = RKCost88;
            dt.Rows[i]["信息"] = HCost88;
            dt.Rows[i]["信息空"] = HKCost88;
            dt.Rows[i]["合计"] = JCost88 + JKCost88 + XCost88 + XKCost88 + RCost88 + RKCost88 + HCost88 + HKCost88;

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";
            dt.Rows[i]["佳图"] = JAmount88 - JCost88;
            dt.Rows[i]["佳图空"] = JKAmount88 - JKCost88;
            dt.Rows[i]["欣图"] = XAmount88 - XCost88;
            dt.Rows[i]["欣图空"] = XKAmount88 - XKCost88;
            dt.Rows[i]["瑞博"] = RAmount88 - RCost88;
            dt.Rows[i]["瑞博空"] = RKAmount88 - RKCost88;
            dt.Rows[i]["信息"] = HAmount88 - HCost88;
            dt.Rows[i]["信息空"] = HKAmount88 - HKCost88;
            dt.Rows[i]["合计"] = JAmount88 - JCost88 + JKAmount88 - JKCost88 + XAmount88 - XCost88 + XKAmount88 - XKCost88 +
                                 RAmount88 - RCost88 + RKAmount88 - RKCost88 + HAmount88 - HCost88 + HKAmount88 - HKCost88;
            #endregion

            //90-暂估结账销售          
            dt.Rows.Add();
            i++;            
            dt.Rows[i]["类别"] = "90-暂估结账销售";
            
            #region 90-暂估结账销售            
            double JCost90 = 0.0, JKCost90 = 0.0, XCost90 = 0.0, XKCost90 = 0.0, RCost90 = 0.0, RKCost90 = 0.0, HCost90 = 0.0, HKCost90 = 0.0;

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
                    if (dr["SourceStoreH"].ToString().Trim() == "JK")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            JKCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "X")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "XK")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            XKCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "R")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "RK")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            RKCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "H")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                    if (dr["SourceStoreH"].ToString().Trim() == "HK")
                    {
                        if (dr["TTaxPurchPrice"].ToString().Trim() != "")
                            HKCost90 += Convert.ToDouble(dr["TTaxPurchPrice"].ToString().Trim());
                    }
                }
            }
            //成本金额
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "成本金额";
            dt.Rows[i]["佳图"] = JCost90;
            dt.Rows[i]["佳图空"] = JKCost90;
            dt.Rows[i]["欣图"] = XCost90;
            dt.Rows[i]["欣图空"] = XKCost90;
            dt.Rows[i]["瑞博"] = RCost90;
            dt.Rows[i]["瑞博空"] = RKCost90;
            dt.Rows[i]["信息"] = HCost90;
            dt.Rows[i]["信息空"] = HKCost90;
            dt.Rows[i]["合计"] = JCost90 + JKCost90 + XCost90 + XKCost90 + RCost90 + RKCost90 + HCost90 + HKCost90;
            #endregion
            
            //发出小计            
            dt.Rows.Add();
            i++;
            #region 发出小计
            double sumJOut = JCost51 + JCost52 + JCost54 + JCost55 + JCost56 + JCost75 + JCost88 + JCost90;
            double sumJKOut = JKCost51 + JKCost52 + JKCost54 + JKCost55 + JKCost56 + JKCost75 + JKCost88 + JKCost90;
            double sumXOut = XCost51 + XCost52 + XCost54 + XCost55 + XCost56 + XCost75 + XCost88 + XCost90;
            double sumXKOut = XKCost51 + XKCost52 + XKCost54 + XKCost55 + XKCost56 + XKCost75 + XKCost88 + XKCost90;
            double sumROut = RCost51 + RCost52 + RCost54 + RCost55 + RCost56 + RCost75 + RCost88 + RCost90;
            double sumRKOut = RKCost51 + RKCost52 + RKCost54 + RKCost55 + RKCost56 + RKCost75 + RKCost88 + RKCost90;
            double sumHOut = HCost51 + HCost52 + HCost54 + HCost55 + HCost56 + HCost75 + HCost88 + HCost90;
            double sumHKOut = HKCost51 + HKCost52 + HKCost54 + HKCost55 + HKCost56 + HKCost75 + HKCost88 + HKCost90;
            dt.Rows[i]["类别"] = "发出小计";
            dt.Rows[i]["佳图"] = sumJOut;
            dt.Rows[i]["佳图空"] = sumJKOut;
            dt.Rows[i]["欣图"] = sumXOut;
            dt.Rows[i]["欣图空"] = sumXKOut;
            dt.Rows[i]["瑞博"] = sumROut;
            dt.Rows[i]["瑞博空"] = sumRKOut;
            dt.Rows[i]["信息"] = sumHOut;
            dt.Rows[i]["信息空"] = sumHKOut;
            dt.Rows[i]["合计"] = sumJOut + sumJKOut + sumXOut + sumXKOut + sumROut + sumRKOut + sumHOut + sumHKOut;
            #endregion

            //空行            
            dt.Rows.Add();
            i++;

            //本日列帐
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "本日列帐";
            //dt.Rows[i]["佳图"] = sumJOut;
            //dt.Rows[i]["佳图空"] = sumJKOut;
            //dt.Rows[i]["欣图"] = sumXOut;
            //dt.Rows[i]["欣图空"] = sumXKOut;
            //dt.Rows[i]["瑞博"] = sumROut;
            //dt.Rows[i]["瑞博空"] = sumRKOut;
            //dt.Rows[i]["信息"] = sumHOut;
            //dt.Rows[i]["信息空"] = sumHKOut;
            //dt.Rows[i]["合计"] = sumJOut + sumJKOut + sumXOut + sumXKOut + sumROut + sumRKOut + sumHOut + sumHKOut;
            //暂估入库
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "暂估入库";

            //发出商品
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "发出商品";

            //实际结存
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "实际结存";

            //空行            
            dt.Rows.Add();
            i++;

            //副表项目
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "副表项目";
            //dt.Rows[i]["佳图"] = "金额";
            //dt.Rows[i]["佳图空"] = "累计金额";
                        
            //价税合计
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "价税合计";
            
            //销售成本
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "销售成本";

            //毛利
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利";

            //毛利率
            dt.Rows.Add();
            i++;
            dt.Rows[i]["类别"] = "毛利率";

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
            Report.DetailGrid.ColumnTitle.Height = 0.98;
            Report.DetailGrid.ColumnContent.Height = 0.58;
            
            //定义数据集的各个字段
            IGRRecordset RecordSet = Report.DetailGrid.Recordset;
            RecordSet.AddField("类别", GRFieldType.grftString);
            RecordSet.AddField("佳图", GRFieldType.grftString).Format = "0.##";
            RecordSet.AddField("佳图空", GRFieldType.grftString).Format = "0.##";
            RecordSet.AddField("欣图", GRFieldType.grftString).Format = "0.##";
            RecordSet.AddField("欣图空", GRFieldType.grftString).Format = "0.##";
            RecordSet.AddField("瑞博", GRFieldType.grftString).Format = "0.##";
            RecordSet.AddField("瑞博空", GRFieldType.grftString).Format = "0.##";
            RecordSet.AddField("信息", GRFieldType.grftString).Format = "0.##";
            RecordSet.AddField("信息空", GRFieldType.grftString).Format = "0.##";
            RecordSet.AddField("合计", GRFieldType.grftString).Format = "0.##";  

            //定义列
            Report.DetailGrid.AddColumn("类别", "类别", "类别", 2.78);
            Report.DetailGrid.AddColumn("佳图", "佳图", "佳图", 2.38);
            Report.DetailGrid.AddColumn("佳图空", "佳图空", "佳图空", 2.38);
            Report.DetailGrid.AddColumn("欣图", "欣图", "欣图", 2.38);
            Report.DetailGrid.AddColumn("欣图空", "欣图空", "欣图空", 2.38);
            Report.DetailGrid.AddColumn("瑞博", "瑞博", "瑞博", 2.38);
            Report.DetailGrid.AddColumn("瑞博空", "瑞博空", "瑞博空", 2.38);
            Report.DetailGrid.AddColumn("信息", "信息", "信息", 2.38);
            Report.DetailGrid.AddColumn("信息空", "信息空", "信息空", 2.38);
            Report.DetailGrid.AddColumn("合计", "合计", "合计", 2.38);

            try
            {
                //cc.TitleCell.BackColor = ColorToOleColor(Color.Gainsboro);
                //cc.TitleCell.TextAlign = GRTextAlign.grtaMiddleCenter;

                //cc.ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;


                //Report.DetailGrid.ColumnTitle.BackColor = 196;
                //Report.DetailGrid.Columns[0].TitleCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                //Report.DetailGrid.Columns[0].ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
                ////显示格式（对齐）
                //for (int i = 0; i < Report.DetailGrid.Columns.Count; i++)
                //{
                //    Report.DetailGrid.Columns[i].TitleCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                //    if (i == 0)
                //        Report.DetailGrid.Columns[i].ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                //    else
                //        Report.DetailGrid.Columns[i].ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
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
    }
}
