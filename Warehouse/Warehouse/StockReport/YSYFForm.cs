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
    public partial class YSYFForm : Form
    {
        public string Strsql = "";      //条件语句
        public string TypeName = "";    //类型名
        //定义Grid++Report报表主对象
        private GridppReport Report = new GridppReport();
        public DataTable table = new DataTable();//预报表连接的数据集
        int t = 0;
        int r = 0;
        int s = -1;
        string OccurTime = ""; //核销日期
        string Record = "";//核销记录
        string PinZhengHao = "";//凭证号
        string Memo = ""; //备注
        public YSYFForm()
        {
            InitializeComponent();
        }

        public void ShowReport()
        {
            if (TypeName == "YFSearch")
            {
                Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "YFSearch.grf");
                this.Text = "应付款查询";
            }
            else
            {
                Report.LoadFromFile(GridReportUtility.GetReportTemplatePath() + "YSSearch.grf");
                this.Text = "应收款查询";
            }
            //ReportReprove(TypeName);
            //父报表
            Report.DetailGrid.Recordset.ConnectionString = GridReportUtility.GetDatabaseConnectionString();
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Report_FetchRecord);
            //设定查询显示器关联的报表
            this.axGRDisplayViewer1.Report = Report;

            this.axGRDisplayViewer1.Start();
        }

        void Report_FetchRecord()
        {
            throw new NotImplementedException();
        }

        private void YSYFForm_Load(object sender, EventArgs e)
        {
        }
        void Report_Initialize()
        {
            string sql = "select SiteName from T_SysConfig";
            Report.ParameterByName("Title").AsString = (new DBUtil()).Get_Single_val(sql);
        }
        void Report_FetchRecord(ref bool pEof)
        {
            DataTable dt = GetDataTable();
            GridReportUtility.FillRecordToReport(Report, dt);
        }
        private DataTable GetDataTable()
        {
            //每次刷新要先把Table中的行列清空，否则会重复加载...出错
            table.Rows.Clear();
            table.Columns.Clear();
            
            if (TypeName == "YFSearch")
            {
                DataColumn aa = new DataColumn("单据号"); aa.Caption = "单据号";
                DataColumn ab = new DataColumn("单据类别"); ab.Caption = "单据类别";
                DataColumn a = new DataColumn("发票编号"); a.Caption = "发票编号";
                DataColumn b = new DataColumn("单据日期"); b.Caption = "单据日期";
                DataColumn c = new DataColumn("客户名称"); c.Caption = "客户名称";
                DataColumn d = new DataColumn("商品编号"); d.Caption = "商品编号";
                DataColumn e = new DataColumn("商品名称"); e.Caption = "商品名称";
                DataColumn f = new DataColumn("仓库"); f.Caption = "仓库";
                DataColumn g = new DataColumn("数量"); g.Caption = "数量";
                DataColumn h = new DataColumn("含税金额"); h.Caption = "含税金额";
                DataColumn i = new DataColumn("核销金额"); i.Caption = "核销金额";
                DataColumn j = new DataColumn("余额"); j.Caption = "余额";
                DataColumn k = new DataColumn("核销日期"); k.Caption = "核销日期";
                DataColumn l = new DataColumn("核销记录"); l.Caption = "核销记录";
                DataColumn m = new DataColumn("凭证号"); m.Caption = "凭证号";
                DataColumn n = new DataColumn("备注"); n.Caption = "备注";

                table.Columns.Add(aa); table.Columns.Add(ab);
                table.Columns.Add(a); table.Columns.Add(b);
                table.Columns.Add(c); table.Columns.Add(d);
                table.Columns.Add(e); table.Columns.Add(f);
                table.Columns.Add(g); table.Columns.Add(h);
                table.Columns.Add(i); table.Columns.Add(j);
                table.Columns.Add(k); table.Columns.Add(l);
                table.Columns.Add(m); table.Columns.Add(n);

                #region //处理报表的数据

                string ReceiptId = ""; //单据编号
                string Sql = "";
                //获取查询窗口中传回的条件语句
                DataTable dt = (new SqlDBConnect()).Get_Dt(Strsql);
                this.RecordNum.Text = "查询结果：共有" + dt.Rows.Count.ToString() + "条记录";
                if (dt == null || dt.Rows.Count <= 0)
                    return null;

                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = dt.Rows.Count;
                for (t = 0; t < dt.Rows.Count; t++)
                {
                    s++;
                    table.Rows.Add();
                    table.Rows[s]["发票编号"] = dt.Rows[t][0].ToString();
                    table.Rows[s]["单据日期"] = Convert.ToDateTime(dt.Rows[t][1]).ToString("yyyy-MM-dd");
                    table.Rows[s]["客户名称"] = dt.Rows[t][2];
                    table.Rows[s]["商品编号"] = dt.Rows[t][3];
                    table.Rows[s]["商品名称"] = dt.Rows[t][4];
                    table.Rows[s]["仓库"] = dt.Rows[t][5];
                    table.Rows[s]["数量"] = dt.Rows[t][6];
                    table.Rows[s]["含税金额"] = dt.Rows[t][7];
                    table.Rows[s]["核销金额"] = dt.Rows[t][8];
                    table.Rows[s]["余额"] = dt.Rows[t][9];
                    table.Rows[s]["单据号"] = dt.Rows[t][10];
                    table.Rows[s]["单据类别"] = dt.Rows[t][11];
                    ReceiptId = dt.Rows[t][10].ToString();
                    Sql = "select OccurTime 核销日期,Record 核销记录,PinZhengHao 凭证号,Memo 备注 " +
                            "from T_Financial where FinancialId in (select FinancialId from T_Financial_Det " +
                            "where ReceiptId='" + ReceiptId + "')";

                    DataTable dtt = (new SqlDBConnect()).Get_Dt(Sql);
                    if (dtt != null && dtt.Rows.Count > 0)
                    {
                        for (r = 0; r < dtt.Rows.Count; r++)
                        {
                            if (dtt.Rows[r][0].ToString().Trim() != "")
                                OccurTime += Convert.ToDateTime(dtt.Rows[r][0]).ToString("yyyy-MM-dd") + ',';
                            if (dtt.Rows[r][0].ToString().Trim() != "" && dt.Rows[t][8].ToString().Trim() != "")
                                Record += Convert.ToDateTime(dtt.Rows[r][0]).ToString("yyyy-MM-dd") + ','
                                       + Convert.ToDouble(dt.Rows[t][8].ToString().Trim()).ToString("0.00") + ',';
                            if (dtt.Rows[r][2].ToString().Trim() != "")
                                PinZhengHao += dtt.Rows[r][2].ToString() + ',';
                            if (dtt.Rows[r][3].ToString().Trim() != "")
                                Memo += dtt.Rows[r][3].ToString() + ',';
                        }
                        if (OccurTime != "")
                            table.Rows[s]["核销日期"] = OccurTime.Substring(0, OccurTime.Length - 1);
                        if (Record != "")
                            table.Rows[s]["核销记录"] = Record.Substring(0, Record.Length - 1);
                        if (PinZhengHao != "")
                            table.Rows[s]["凭证号"] = PinZhengHao.Substring(0, PinZhengHao.Length - 1);
                        if (Memo != "")
                            table.Rows[s]["备注"] = Memo.Substring(0, Memo.Length - 1);
                        OccurTime = "";
                        Record = "";
                        PinZhengHao = "";
                        Memo = "";
                    }
                    this.progressBar1.Value++;
                }

                //归0
                this.progressBar1.Value = 0;
                s = -1;
                #endregion
            }
            else
            {
                DataColumn aa = new DataColumn("单据号"); aa.Caption = "单据号";
                DataColumn ab = new DataColumn("单据类别"); ab.Caption = "单据类别";
                DataColumn a = new DataColumn("发票编号"); a.Caption = "发票编号";
                DataColumn b = new DataColumn("单据日期"); b.Caption = "单据日期";
                DataColumn bc = new DataColumn("客户编码"); bc.Caption = "客户编码";
                DataColumn c = new DataColumn("客户名称"); c.Caption = "客户名称";
                DataColumn d = new DataColumn("商品编号"); d.Caption = "商品编号";
                DataColumn e = new DataColumn("商品名称"); e.Caption = "商品名称";
                DataColumn f = new DataColumn("仓库"); f.Caption = "仓库";
                DataColumn g = new DataColumn("数量"); g.Caption = "数量";
                DataColumn h = new DataColumn("含税金额"); h.Caption = "含税金额";
                DataColumn i = new DataColumn("核销金额"); i.Caption = "核销金额";
                DataColumn j = new DataColumn("余额"); j.Caption = "余额";
                DataColumn jk = new DataColumn("成本金额"); jk.Caption = "成本金额";
                DataColumn jl = new DataColumn("毛利"); jl.Caption = "毛利";
                DataColumn kn = new DataColumn("账期"); kn.Caption = "账期";
                DataColumn km = new DataColumn("管理区号"); km.Caption = "管理区号";
                DataColumn kj = new DataColumn("复核员"); kj.Caption = "复核员";
                DataColumn kl = new DataColumn("业务员"); kl.Caption = "业务员";
                DataColumn kk = new DataColumn("操作员"); kk.Caption = "操作员";
                DataColumn k = new DataColumn("核销日期"); k.Caption = "核销日期";
                DataColumn l = new DataColumn("核销记录"); l.Caption = "核销记录";
                DataColumn m = new DataColumn("凭证号"); m.Caption = "凭证号";
                DataColumn n = new DataColumn("备注"); n.Caption = "备注";

                table.Columns.Add(aa); table.Columns.Add(ab);
                table.Columns.Add(a); table.Columns.Add(b);
                table.Columns.Add(c); table.Columns.Add(d);
                table.Columns.Add(e); table.Columns.Add(f);
                table.Columns.Add(g); table.Columns.Add(h);
                table.Columns.Add(i); table.Columns.Add(j);
                table.Columns.Add(k); table.Columns.Add(l);
                table.Columns.Add(m); table.Columns.Add(n);
                table.Columns.Add(bc); table.Columns.Add(jk);
                table.Columns.Add(jl); table.Columns.Add(km);
                table.Columns.Add(kn); table.Columns.Add(kj);
                table.Columns.Add(kl); table.Columns.Add(kk);

                #region //处理报表的数据

                string ReceiptId = ""; //单据编号
                string Sql = "";
                //获取查询窗口中传回的条件语句
                DataTable dt = (new SqlDBConnect()).Get_Dt(Strsql);
                this.RecordNum.Text = "查询结果：共有" + dt.Rows.Count.ToString() + "条记录";
                if (dt == null || dt.Rows.Count <= 0)
                    return null;

                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = dt.Rows.Count;
                for (t = 0; t < dt.Rows.Count; t++)
                {
                    s++;
                    table.Rows.Add();
                    table.Rows[s]["单据号"] = dt.Rows[t][0];
                    table.Rows[s]["单据类别"] = dt.Rows[t][1];
                    table.Rows[s]["发票编号"] = dt.Rows[t][2].ToString();
                    table.Rows[s]["单据日期"] = Convert.ToDateTime(dt.Rows[t][3]).ToString("yyyy-MM-dd");
                    table.Rows[s]["客户编码"] = dt.Rows[t][4];
                    table.Rows[s]["客户名称"] = dt.Rows[t][5];
                    table.Rows[s]["商品编号"] = dt.Rows[t][6];
                    table.Rows[s]["商品名称"] = dt.Rows[t][7];
                    table.Rows[s]["仓库"] = dt.Rows[t][8];
                    table.Rows[s]["数量"] = dt.Rows[t][9];
                    table.Rows[s]["含税金额"] = dt.Rows[t][10];
                    table.Rows[s]["核销金额"] = dt.Rows[t][11];
                    table.Rows[s]["余额"] = dt.Rows[t][12];
                    table.Rows[s]["成本金额"] = dt.Rows[t][13];
                    table.Rows[s]["毛利"] = dt.Rows[t][14];
                    table.Rows[s]["账期"] = dt.Rows[t][15];
                    table.Rows[s]["管理区号"] = dt.Rows[t][16];
                    table.Rows[s]["复核员"] = dt.Rows[t][17];
                    table.Rows[s]["业务员"] = dt.Rows[t][18];
                    table.Rows[s]["操作员"] = dt.Rows[t][19];
                    ReceiptId = dt.Rows[t][0].ToString();
                    Sql = "select OccurTime 核销日期,Record 核销记录,PinZhengHao 凭证号,Memo 备注 " +
                            "from T_Financial where FinancialId in (select FinancialId from T_Financial_Det " +
                            "where ReceiptId='" + ReceiptId + "')";

                    DataTable dtt = (new SqlDBConnect()).Get_Dt(Sql);
                    if (dtt != null && dtt.Rows.Count > 0)
                    {
                        for (r = 0; r < dtt.Rows.Count; r++)
                        {
                            if (dtt.Rows[r][0].ToString().Trim() != "")
                                OccurTime += Convert.ToDateTime(dtt.Rows[r][0]).ToString("yyyy-MM-dd") + ',';
                            if (dtt.Rows[r][0].ToString().Trim() != "" && dt.Rows[t][11].ToString().Trim() != "")
                                Record += Convert.ToDateTime(dtt.Rows[r][0]).ToString("yyyy-MM-dd") + ','
                                       + Convert.ToDouble(dt.Rows[t][11].ToString().Trim()).ToString("0.00") + ',';
                            if (dtt.Rows[r][2].ToString().Trim() != "")
                                PinZhengHao += dtt.Rows[r][2].ToString() + ',';
                            if (dtt.Rows[r][3].ToString().Trim() != "")
                                Memo += dtt.Rows[r][3].ToString() + ',';
                        }
                        if (OccurTime != "")
                            table.Rows[s]["核销日期"] = OccurTime.Substring(0, OccurTime.Length - 1);
                        if (Record != "")
                            table.Rows[s]["核销记录"] = Record.Substring(0, Record.Length - 1);
                        if (PinZhengHao != "")
                            table.Rows[s]["凭证号"] = PinZhengHao.Substring(0, PinZhengHao.Length - 1);
                        if (Memo != "")
                            table.Rows[s]["备注"] = Memo.Substring(0, Memo.Length - 1);
                        OccurTime = "";
                        Record = "";
                        PinZhengHao = "";
                        Memo = "";
                    }
                    this.progressBar1.Value++;
                }
                //归0
                this.progressBar1.Value = 0;
                s = -1;
                #endregion
            }
            return table;
        }

        private void btnprintview_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            Report.Print(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axGRDisplayViewer1.Stop();
            this.axGRDisplayViewer1.Start();
        }
    }
}
