using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using grproLib;
using grdesLib;
using Warehouse.DB;
using Warehouse.DAO;
using Warehouse.Base;


namespace Warehouse.StockReport
{
    public partial class PurchaseSSForm : Form
    {
        protected GridppReport Report = new GridppReport();

        private string SqlWhere = "";
       
        private DataTable Dt = new DataTable();
        private DataRow Dr ;

        public PurchaseSSForm(string Sqlwhere)
        {        
            InitializeComponent();
            this.SqlWhere = Sqlwhere;
            //Report.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            //Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);      
     
            //this.axGRDisplayViewer1.Report = Report;
        }

        void Report_Initialize()
        {
            DefineReport();
        }
        void Report_FetchRecord(ref bool pEof)
        {
            GridReportUtility.FillRecordToReport(Report, this.Dt);
        }

        private void PurchaseSSForm_Load(object sender, EventArgs e)
        {
            //this.axGRDisplayViewer1.Start();
        }


        public void build_dt()
        {
            Dt = new DataTable("tb_tmp");
            //  Dtb.Columns.Add("序号", Type.GetType("System.String"));
            Dt.Columns.Add("单据日期", Type.GetType("System.String"));
            Dt.Columns.Add("仓库编号", Type.GetType("System.String"));
            Dt.Columns.Add("物料类型", Type.GetType("System.String"));
            Dt.Columns.Add("单据名称", Type.GetType("System.String"));
            Dt.Columns.Add("单据编号", Type.GetType("System.String"));
            Dt.Columns.Add("发票号", Type.GetType("System.String"));
            Dt.Columns.Add("客户名称", Type.GetType("System.String"));
            Dt.Columns.Add("数量in", Type.GetType("System.Double"));
            Dt.Columns.Add("成本in", Type.GetType("System.Double"));
            Dt.Columns.Add("数量out", Type.GetType("System.Double"));
            Dt.Columns.Add("成本out", Type.GetType("System.Double"));
            Dt.Columns.Add("数量sum", Type.GetType("System.Double"));
            Dt.Columns.Add("成本sum", Type.GetType("System.Double"));
        }

        //收入
        private void Update_Dts_In_Out()
        {
            DataTable dt = new DataTable();
            //string ck_, matid_, matname_, lx_;
            //string sql_;
            //string sql = "select  CONVERT(char(10), dbo.T_Receipt_Main_Det.OccurTime, 121) AS OCTIME,"+
            //            " T_Receipt_Main_Det.SourceStoreH AS SOHO ,"+
            //            " dbo.T_Receipt_Main_Det.MatType AS MATY,"+
            //            " dbo.T_ReceiptModal.ReceName AS RENA,"+
            //            " dbo.T_Receipt_Main_Det.ReceiptId AS REID, "+
            //            " dbo.T_Receipt_Main_Det.InvoiceNO AS INNO,"+
            //            " dbo.T_CustomerInf.CustName AS CUNA,"+
            //            " T_Receipt_Main_Det.num as Num ,"+
            //            " T_Receipt_Main_Det.TTaxPurchPrice COSTIN " +
            //            "  FROM dbo.T_Receipt_Main_Det "+
            //            " LEFT OUTER JOIN dbo.T_CustomerInf ON T_Receipt_Main_Det.ReceiptTypeID>0 and T_Receipt_Main_Det.ReceiptTypeID<51 " +
            //            " AND dbo.T_CustomerInf.CustID = dbo.T_Receipt_Main_Det.CustID " +
            //            " INNER JOIN dbo.T_ReceiptModal ON dbo.T_Receipt_Main_Det.ReceiptTypeID = dbo.T_ReceiptModal.ReceTypeID  " 
            //            + this.SqlWhere +
            //            " order by T_Receipt_Main_Det.OccurTime";

            string sql = "select   T_Receipt_Main_Det.Matid as MATID,"+
                        "CONVERT(char(10), dbo.T_Receipt_Main_Det.OccurTime, 121) AS OCTIME," +
                        " T_Receipt_Main_Det.SourceStoreH AS SOHO ," +
                        " dbo.T_Receipt_Main_Det.MatType AS MATY," +
                        " dbo.T_ReceiptModal.ReceName AS RENA," +
                        " dbo.T_Receipt_Main_Det.ReceiptId AS REID, " +
                        " dbo.T_Receipt_Main_Det.InvoiceNO AS INNO," +
                        " dbo.T_CustomerInf.CustName AS CUNA," +
                        " T_Receipt_Main_Det.num as Num ," +
                        " T_Receipt_Main_Det.TTaxPurchPrice COST " +
                        " FROM dbo.T_Receipt_Main_Det " +
                        " LEFT OUTER JOIN dbo.T_CustomerInf ON " +
                        "  dbo.T_CustomerInf.CustID = dbo.T_Receipt_Main_Det.CustID " +
                        " INNER JOIN dbo.T_ReceiptModal ON dbo.T_Receipt_Main_Det.ReceiptTypeID = dbo.T_ReceiptModal.ReceTypeID  "
                        + this.SqlWhere +
                        " order by T_Receipt_Main_Det.OccurTime";

            dt = (new SqlDBConnect()).Get_Dt(sql);

            for (int m = 0; m < dt.Rows.Count; m++)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["MATID"].ToString().Trim().IndexOf("K-") == -1)
                    {
                        if (dt.Rows[m]["RENA"].ToString().Trim().IndexOf("进货单") != -1 || dt.Rows[m]["RENA"].ToString().Trim().IndexOf("假设进货单") != -1 ||
                            dt.Rows[m]["RENA"].ToString().Trim().IndexOf("盈亏单") != -1 || dt.Rows[m]["RENA"].ToString().Trim().IndexOf("盘点单") != -1 ||
                            dt.Rows[m]["RENA"].ToString().Trim().IndexOf("假设结账单") != -1)
                        {
                            DataRow Dr = Dt.NewRow();

                            Dr["单据日期"] = dt.Rows[m]["OCTIME"];
                            Dr["仓库编号"] = dt.Rows[m]["SOHO"];
                            Dr["物料类型"] = dt.Rows[m]["MATY"];
                            Dr["单据名称"] = dt.Rows[m]["RENA"];
                            Dr["单据编号"] = dt.Rows[m]["REID"];
                            Dr["发票号"] = dt.Rows[m]["INNO"];
                            Dr["客户名称"] = dt.Rows[m]["CUNA"];
                            Dr["数量in"] = dt.Rows[m]["Num"];
                            Dr["成本in"] = dt.Rows[m]["COST"];

                            Dt.Rows.Add(Dr);
                        }
                        if (dt.Rows[m]["RENA"].ToString().IndexOf("暂估结账销售单") != -1)//90单不要数量
                        {
                            DataRow Dr = Dt.NewRow();
                            Dr["单据日期"] = dt.Rows[m]["OCTIME"];
                            Dr["仓库编号"] = dt.Rows[m]["SOHO"];
                            Dr["物料类型"] = dt.Rows[m]["MATY"];
                            Dr["单据名称"] = dt.Rows[m]["RENA"];
                            Dr["单据编号"] = dt.Rows[m]["REID"];
                            Dr["发票号"] = dt.Rows[m]["INNO"];
                            Dr["客户名称"] = dt.Rows[m]["CUNA"];
                            Dr["成本out"] = dt.Rows[m]["COST"];

                            Dt.Rows.Add(Dr);
                            this.progressBar1.Value = 3;
                        }

                        if (dt.Rows[m]["RENA"].ToString().IndexOf("销售单1") != -1 || dt.Rows[m]["RENA"].ToString().Trim().IndexOf("销售单2") != -1 ||
                            dt.Rows[m]["RENA"].ToString().Trim().IndexOf("调拨单") != -1 || dt.Rows[m]["RENA"].ToString().Trim().IndexOf("自用单") != -1 ||
                            dt.Rows[m]["RENA"].ToString().Trim().IndexOf("赠送单") != -1 || dt.Rows[m]["RENA"].ToString().Trim().IndexOf("销售单5") != -1 ||
                            dt.Rows[m]["RENA"].ToString().Trim().IndexOf("借用单") != -1 || dt.Rows[m]["RENA"].ToString().Trim().IndexOf("租机单") != -1)
                        {
                            DataRow Dr = Dt.NewRow();
                            Dr["单据日期"] = dt.Rows[m]["OCTIME"];
                            Dr["仓库编号"] = dt.Rows[m]["SOHO"];
                            Dr["物料类型"] = dt.Rows[m]["MATY"];
                            Dr["单据名称"] = dt.Rows[m]["RENA"];
                            Dr["单据编号"] = dt.Rows[m]["REID"];
                            Dr["发票号"] = dt.Rows[m]["INNO"];
                            Dr["客户名称"] = dt.Rows[m]["CUNA"];
                            Dr["数量out"] = dt.Rows[m]["Num"];
                            Dr["成本out"] = dt.Rows[m]["COST"];

                            Dt.Rows.Add(Dr);
                            this.progressBar1.Value = 4;
                        }
                    }
                }
            }
        }

        //发出
        //private void Update_Dts_Out()
        //{
        //    DataTable dt = new DataTable();
          
        //    string sql = "select  CONVERT(char(10), dbo.T_Receipt_Main_Det.OccurTime, 121) AS OCTIME," +
        //                " T_Receipt_Main_Det.SourceStoreH AS SOHO ," +
        //                " dbo.T_Receipt_Main_Det.MatType AS MATY," +
        //                " dbo.T_ReceiptModal.ReceName AS RENA," +
        //                " dbo.T_Receipt_Main_Det.ReceiptId AS REID, " +
        //                " dbo.T_Receipt_Main_Det.InvoiceNO AS INNO," +
        //                " dbo.T_CustomerInf.CustName AS CUNA," +
        //                " T_Receipt_Main_Det.num as Num ," +
        //                " T_Receipt_Main_Det.TTaxPurchPrice COSTOUT, " +                    
        //                " T_Receipt_Main_Det.ReceiptTypeID as RETYID " +
        //                " FROM dbo.T_Receipt_Main_Det " +
        //                " LEFT OUTER JOIN dbo.T_CustomerInf ON T_Receipt_Main_Det.ReceiptTypeID>51 " +
        //                " and T_Receipt_Main_Det.ReceiptTypeID < 90  " +
        //                " AND dbo.T_CustomerInf.CustID = dbo.T_Receipt_Main_Det.CustID " +
        //                " INNER JOIN dbo.T_ReceiptModal ON dbo.T_Receipt_Main_Det.ReceiptTypeID = dbo.T_ReceiptModal.ReceTypeID  "
        //                + this.SqlWhere +
        //                " order by T_Receipt_Main_Det.OccurTime";

        //    dt = (new SqlDBConnect()).Get_Dt(sql);
        //    for (int m = 0; m < dt.Rows.Count; m++)
        //    {
        //        Dr = Dt.NewRow();
        //        if (dt.Rows[m]["RETYID"].ToString().IndexOf("90") != -1)//90单不要数量
        //        {
        //            Dr["单据日期"] = dt.Rows[m]["OCTIME"];
        //            Dr["仓库编号"] = dt.Rows[m]["SOHO"];
        //            Dr["物料类型"] = dt.Rows[m]["MATY"];
        //            Dr["单据名称"] = dt.Rows[m]["RENA"];
        //            Dr["单据编号"] = dt.Rows[m]["REID"];
        //            Dr["发票号"] = dt.Rows[m]["INNO"];
        //            Dr["客户名称"] = dt.Rows[m]["CUNA"];
        //            Dr["成本out"] = dt.Rows[m]["COSTOUT"];
                   
        //            this.progressBar1.Value = 3;
        //        }
                    
        //        else
        //        {
        //            Dr["单据日期"] = dt.Rows[m]["OCTIME"];
        //            Dr["仓库编号"] = dt.Rows[m]["SOHO"];
        //            Dr["物料类型"] = dt.Rows[m]["MATY"];
        //            Dr["单据名称"] = dt.Rows[m]["RENA"];
        //            Dr["单据编号"] = dt.Rows[m]["REID"];
        //            Dr["发票号"] = dt.Rows[m]["INNO"];
        //            Dr["客户名称"] = dt.Rows[m]["CUNA"];
        //            Dr["数量out"] = dt.Rows[m]["Num"];
        //            Dr["成本out"] = dt.Rows[m]["COSTOUT"];                   
                    
        //            this.progressBar1.Value = 4;
        //        }
        //        Dt.Rows.Add(Dr);
        //    }
        //}

        //库存
        private void Update_Dts_Stock()
        {
            if (Dt.Rows.Count > 0)
            {
                Dt.Rows[0]["数量sum"] = Convert.ToDouble(Util.YNdbnull(Dt.Rows[0]["数量in"].ToString())) -
                                        Convert.ToDouble(Util.YNdbnull(Dt.Rows[0]["数量out"].ToString()));
                Dt.Rows[0]["成本sum"] = Convert.ToDouble(Util.YNdbnull(Dt.Rows[0]["成本in"].ToString())) -
                                        Convert.ToDouble(Util.YNdbnull(Dt.Rows[0]["成本out"].ToString()));
                for (int m = 1; m < Dt.Rows.Count; m++)
                {
                    Dt.Rows[m]["数量sum"] = Convert.ToDouble(Util.YNdbnull(Dt.Rows[m - 1]["数量sum"].ToString())) +
                                            Convert.ToDouble(Util.YNdbnull(Dt.Rows[m]["数量in"].ToString())) -
                                            Convert.ToDouble(Util.YNdbnull(Dt.Rows[m]["数量out"].ToString()));
                    Dt.Rows[m]["成本sum"] = Convert.ToDouble(Util.YNdbnull(Dt.Rows[m - 1]["成本sum"].ToString())) +
                                            Convert.ToDouble(Util.YNdbnull(Dt.Rows[m]["成本in"].ToString())) -
                                            Convert.ToDouble(Util.YNdbnull(Dt.Rows[m]["成本out"].ToString()));
                }
            }
        }
        //生成Dt
        public void Produce_dt()
        {
           
            this.progressBar1.Value = 1;

            Update_Dts_In_Out();
            this.progressBar1.Value = 2;
       
            //Update_Dts_Out();

            Update_Dts_Stock();
            this.progressBar1.Value = 5;  
            this.progressBar1.Value = 6;

            //设置与数据源的连接串，因为在设计时指定的数据库路径是绝对路径

            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Report_FetchRecord);
            this.axGRDisplayViewer1.Report = Report;
            this.axGRDisplayViewer1.Start();

            this.progressBar1.Value = 7;
        }

        void Report_FetchRecord()
        {
            throw new NotImplementedException();
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
            Report.PageHeader.Height = 0.48;
            Report.DesignBottomMargin = 0.7;
            Report.DesignTopMargin = 0.8;
            Report.DesignLeftMargin = 0.4;
            Report.DesignRightMargin = 0.4;

            Report.DesignPaperWidth = 29.700;
            Report.DesignPaperLength = 21.000;

            // 插入一个部件框
            // IGRStaticBox StaticBox = Report.PageHeader.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            IGRStaticBox StaticBox = Report.PageHeader.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            StaticBox.Text = "XX数码科技有限公司";
            StaticBox.ForeColor = 255 * 256 * 256 + 0 * 256 + 0;
            StaticBox.TextAlign = GRTextAlign.grtaMiddleCenter;
            StaticBox.Font.Point = 9;
            StaticBox.Top = 0.40;
            StaticBox.Width = 5.64;
            StaticBox.Height = 0.58;
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
            Reportheader.Height = 1.25;

            //插入一个静态文本框,显示报表标题文字
            IGRStaticBox StaticBox = Reportheader.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            StaticBox.Text = "进销存明细表";
            StaticBox.Center = GRCenterStyle.grcsHorizontal; //使部件框在节中水平方向上居中对齐
            StaticBox.Font.Point = 18;
            StaticBox.Font.Bold = true;
            StaticBox.Top = 0.40;
            StaticBox.Width = 4.64;
            StaticBox.Height = 0.8;
        }
        private void DefineDetailGrid()
        {
            Report.InsertDetailGrid();
            Report.DetailGrid.ColumnTitle.Height = 0.98;//标题行高度
           // Report.DetailGrid.ColumnTitle.TitleCells[3].WordWrap = true;
         
            Report.DetailGrid.ColumnContent.Height = 0.58;//内容行高度



            Report.DetailGrid.ColumnContent.AlternatingBackColor = 230 * 256 * 256 + 217 * 256 + 217;//内容行交替背景色
            Report.DetailGrid.ColumnTitle.BackColor = 217 * 256 * 256 + 217 * 256 + 217;//标题行背景色

            //定义数据集的各个字段
            IGRRecordset RecordSet = Report.DetailGrid.Recordset;
            RecordSet.AddField("单据日期", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("仓库编号", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("物料类型", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("单据名称", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("单据编号", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("发票号", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("客户名称", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("数量in", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("成本in", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("数量out", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("成本out", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("数量sum", GRFieldType.grftString).Format = "#,##0";
            RecordSet.AddField("成本sum", GRFieldType.grftString).Format = "#,##0.00";


            Report.DetailGrid.AddColumn("序号", "序号", "序号", 1.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;

            Report.DetailGrid.AddColumn("单据日期", "单据日期", "单据日期", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("仓库编号", "仓库编号", "仓库编号", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("物料类型", "物料类型", "物料类型", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("单据名称", "单据名称", "单据名称", 2.7).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("单据编号", "单据编号", "单据编号", 3.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("发票号", "发票号", "发票号", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("客户名称", "客户名称", "客户名称", 7.0).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("数量in", "数量", "数量in", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
            Report.DetailGrid.AddColumn("成本in", "成本", "成本in", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
            Report.DetailGrid.AddColumn("数量out", "数量", "数量out", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
            Report.DetailGrid.AddColumn("成本out", "成本", "成本out", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
            Report.DetailGrid.AddColumn("数量sum", "数量", "数量sum", 2.28).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
            Report.DetailGrid.AddColumn("成本sum", "成本", "成本sum", 2.58).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
            
            //标题行绕行
            Report.DetailGrid.ColumnTitle.TitleCells[3].WordWrap = true;
            Report.DetailGrid.ColumnTitle.TitleCells[4].WordWrap = true;
            Report.DetailGrid.ColumnContent.ContentCells[6].WordWrap = true;

            //定义行号系统变量
            IGRColumn Column = Report.DetailGrid.Columns[1];
            Column.ContentCell.FreeCell = true;
            Column.ContentCell.Controls.RemoveAll();
            IGRSystemVarBox SystemVarBox = Column.ContentCell.Controls.Add(GRControlType.grctSystemVarBox).AsSystemVarBox;
            SystemVarBox.Dock = GRDockStyle.grdsFill;
            SystemVarBox.SystemVar = GRSystemVarType.grsvRowNo;
           
            //定义双层表头
            //定义标题组
            IGRColumnTitleCell ColumnTitleCell1 = Report.DetailGrid.AddGroupTitle("收入", "收入");
            ColumnTitleCell1.TextAlign = GRTextAlign.grtaMiddleCenter;
            ColumnTitleCell1.EncloseColumn("数量in");
            ColumnTitleCell1.EncloseColumn("成本in");

            IGRColumnTitleCell ColumnTitleCell2 = Report.DetailGrid.AddGroupTitle("发出", "发出");
            ColumnTitleCell2.TextAlign = GRTextAlign.grtaMiddleCenter;
            ColumnTitleCell2.EncloseColumn("数量out");
            ColumnTitleCell2.EncloseColumn("成本out");

            IGRColumnTitleCell ColumnTitleCell3 = Report.DetailGrid.AddGroupTitle("库存", "库存");
            ColumnTitleCell3.TextAlign = GRTextAlign.grtaMiddleCenter;
            ColumnTitleCell3.EncloseColumn("数量sum");
            ColumnTitleCell3.EncloseColumn("成本sum");

            //定义分组
            IGRGroup Group = Report.DetailGrid.Groups.Add();
            // Group.ByFields = "OrderID";

            //<<定义分组头
            Group.Header.Height = 0.0;

            //<<定义分组尾
            Group.Footer.Height = 0.6;

            //定义分组尾的缺省字体为粗体，其拥有的部件框如没有显示定义字体，则将应用缺省字体
            //Group.Footer.Font.Bold = true;

            IGRStaticBox StaticBox = Group.Header.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            StaticBox = Group.Footer.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            StaticBox.Text = "合计";
            StaticBox.Left = 0.1;
            StaticBox.Top = 0.1;
            StaticBox.Width = 2.59;
            StaticBox.Height = 0.5;

            IGRSummaryBox SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0";
            SummaryBox.DataField = "数量in";
            SummaryBox.AlignColumn = "数量in"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;

            SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0.00";
            SummaryBox.DataField = "成本in";
            SummaryBox.AlignColumn = "成本in"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;

            SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0";
            SummaryBox.DataField = "数量out";
            SummaryBox.AlignColumn = "数量out"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;

            SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0.00";
            SummaryBox.DataField = "成本out";
            SummaryBox.AlignColumn = "成本out"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;

            SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0";
            SummaryBox.DataField = "数量sum";
            SummaryBox.AlignColumn = "数量sum"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;

            SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0.00";
            SummaryBox.DataField = "成本sum";
            SummaryBox.AlignColumn = "成本sum"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Report.Print(true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Report_Initialize();
        }
    }
}
