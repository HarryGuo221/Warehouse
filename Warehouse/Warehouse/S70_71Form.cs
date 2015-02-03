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

namespace Warehouse
{
    public partial class S70_71Form : Form
    {
        private DataTable Dt = new DataTable();
        private DataRow Dr ;
        private string sqlwhere="";//查询条件
        private string TitleName = "";//表名
        private int selectedIndex = 0;//combobox选中索引

        private GridppReport Report =new GridppReport();

        public S70_71Form()
        {
            InitializeComponent();
        }

        void Report_Initialize()
        {
            DefineReport();
        }
        void Report_FetchRecord(ref bool pEof)
        {
            GridReportUtility.FillRecordToReport(Report, this.Dt);
        }     

        private string DataSql = "";

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            WFilter wf = new WFilter(0, "", false);
            //应收
            if (this.toolStripComboBox1.SelectedIndex == 0)
            {
                DataSql = "select * from S70取应收款单据 "; 
                wf.strSql = DataSql;

                wf.s_items.Add("单据类别,S70取应收款单据.djh,C");
                wf.s_items.Add("单据类别,S70取应收款单据.单据类别,C");
                wf.s_items.Add("发票编号,S70取应收款单据.发票编号,C");
                wf.s_items.Add("单据编号,S70取应收款单据.单据编号,C");
                wf.s_items.Add("单据日期,S70取应收款单据.单据日期,C");
                wf.s_items.Add("客户名称,S70取应收款单据.客户名称,C");
                wf.s_items.Add("商品编号,S70取应收款单据.商品编号,C");
                wf.s_items.Add("商品名称,S70取应收款单据.商品名称,C");
                wf.s_items.Add("仓库,S70取应收款单据.LX,C");
                wf.s_items.Add("数量,S70取应收款单据.数量,N");
                wf.s_items.Add("含税金额,S70取应收款单据.含税金额,N");
                wf.s_items.Add("核销金额,S70取应收款单据.核销金额,N");
                wf.s_items.Add("余额,S70取应收款单据.客户名称-S70取应收款单据.核销金额,N");
                //wf.s_items.Add("毛利,S70取应收款单据.单据日期,N");
            }
            //应付
            if (this.toolStripComboBox1.SelectedIndex == 1)
            {
                DataSql = "select * from S71取应付款单据 ";
                wf.strSql = DataSql;

                wf.s_items.Add("单据类别,S71取应付款单据.单据类别,C");
                wf.s_items.Add("发票编号,S71取应付款单据.发票编号,C");
                wf.s_items.Add("单据日期,S71取应付款单据.单据日期,C");
                wf.s_items.Add("客户名称,S71取应付款单据.客户名称,C");
                wf.s_items.Add("商品编号,S71取应付款单据.商品编号,C");
                wf.s_items.Add("商品名称,S71取应付款单据.商品名称,C");
                wf.s_items.Add("数量,S71取应付款单据.数量,N");
                wf.s_items.Add("含税金额,S71取应付款单据.含税金额,N");
                wf.s_items.Add("核销金额,S71取应付款单据.核销金额,N");
                wf.s_items.Add("余额,S71取应付款单据.客户名称-S71取应收款单据.核销金额,N");
            }
            //应收核销
            if (this.toolStripComboBox1.SelectedIndex == 2)
            {
                DataSql = "select * from S70应收款核销 ";
                wf.strSql = DataSql;

                wf.s_items.Add("工作年月,S70应收款核销.工作年月,C");
                wf.s_items.Add("核销记录,S70应收款核销.核销记录,C");
                wf.s_items.Add("客户代码,S70应收款核销.客户代码,C");
                wf.s_items.Add("客户名称,S70应收款核销.客户名称,C");
                wf.s_items.Add("核销金额,S70应收款核销.核销金额,N");
                wf.s_items.Add("支付方式,S70应收款核销.现金支票,C");
                wf.s_items.Add("凭证号,S70应收款核销.凭证号,C");
                wf.s_items.Add("备注,S70应收款核销.备注,C");
            }
            //应付核销
            if (this.toolStripComboBox1.SelectedIndex == 3)
            {
                DataSql = "select * from S71应付款核销 ";
                wf.strSql = DataSql;

                wf.s_items.Add("工作年月,S70应收款核销.工作年月,C");
                wf.s_items.Add("核销记录,S70应收款核销.核销记录,C");
                wf.s_items.Add("客户代码,S70应收款核销.客户代码,C");
                wf.s_items.Add("客户名称,S70应收款核销.客户名称,C");
                wf.s_items.Add("核销金额,S70应收款核销.核销金额,N");
                wf.s_items.Add("支付方式,S70应收款核销.现金支票,C");
                wf.s_items.Add("凭证号,S70应收款核销.凭证号,C");
                wf.s_items.Add("备注,S70应收款核销.备注,C");
            }
            //预收
            if (this.toolStripComboBox1.SelectedIndex == 4)
            {
                DataSql = "select * from S70取应收款单据 where S70取应收款单据.单据类别='50' ";
                wf.strSql = DataSql;

                wf.s_items.Add("单据类别,S70取应收款单据.djh,C");
                wf.s_items.Add("单据类别,S70取应收款单据.单据类别,C");
                wf.s_items.Add("发票编号,S70取应收款单据.发票编号,C");
                wf.s_items.Add("单据编号,S70取应收款单据.单据编号,C");
                wf.s_items.Add("单据日期,S70取应收款单据.单据日期,C");
                wf.s_items.Add("客户名称,S70取应收款单据.客户名称,C");
                wf.s_items.Add("商品编号,S70取应收款单据.商品编号,C");
                wf.s_items.Add("商品名称,S70取应收款单据.商品名称,C");
                wf.s_items.Add("仓库,S70取应收款单据.LX,C");
                wf.s_items.Add("数量,S70取应收款单据.数量,N");
                wf.s_items.Add("含税金额,S70取应收款单据.含税金额,N");
                wf.s_items.Add("核销金额,S70取应收款单据.核销金额,N");
                wf.s_items.Add("余额,S70取应收款单据.客户名称-S70取应收款单据.核销金额,N");
                //wf.s_items.Add("毛利,S70取应收款单据.单据日期,N");
            }
            //预付
            if (this.toolStripComboBox1.SelectedIndex == 5)
            {
                DataSql = "select * from T_NotOffset where S71取应付款单据.单据类别='50' ";
                wf.strSql = DataSql;

                wf.s_items.Add("单据类别,S71取应付款单据.单据类别,C");
                wf.s_items.Add("发票编号,S71取应付款单据.发票编号,C");
                wf.s_items.Add("单据日期,S71取应付款单据.单据日期,C");
                wf.s_items.Add("客户名称,S71取应付款单据.客户名称,C");
                wf.s_items.Add("商品编号,S71取应付款单据.商品编号,C");
                wf.s_items.Add("商品名称,S71取应付款单据.商品名称,C");
                wf.s_items.Add("数量,S71取应付款单据.数量,N");
                wf.s_items.Add("含税金额,S71取应付款单据.含税金额,N");
                wf.s_items.Add("核销金额,S71取应付款单据.核销金额,N");
                wf.s_items.Add("余额,S71取应付款单据.客户名称-S71取应收款单据.核销金额,N");
            }
            wf.btnOK.Enabled = false;
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                this.sqlwhere = wf.Return_Sql.Substring(wf.Return_Sql.IndexOf(" where "));

                //应收
                if (this.toolStripComboBox1.SelectedIndex == 0)
                {
                    Dt = new DataTable("tb_tmp");

                    Dt.Columns.Add("单据类别", Type.GetType("System.String"));
                    Dt.Columns.Add("单据日期", Type.GetType("System.String"));
                    Dt.Columns.Add("仓库", Type.GetType("System.String"));
                    Dt.Columns.Add("发票编号", Type.GetType("System.String"));
                    Dt.Columns.Add("客户名称", Type.GetType("System.String"));
                    Dt.Columns.Add("商品编号", Type.GetType("System.String"));
                    Dt.Columns.Add("商品名称", Type.GetType("System.String"));
                    Dt.Columns.Add("数量", Type.GetType("System.Double"));
                    Dt.Columns.Add("含税金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("核销金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("核销记录", Type.GetType("System.String"));
                    Dt.Columns.Add("操作员", Type.GetType("System.String"));
                    Dt.Columns.Add("成本金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("凭证号", Type.GetType("System.String"));
                    Dt.Columns.Add("备注", Type.GetType("System.String"));
                    Dt.Columns.Add("备注1", Type.GetType("System.String"));

                    Update_Dts_YINGSHOU();
                    Produce_dt();
                }
                //应付
                if (this.toolStripComboBox1.SelectedIndex == 1)
                {
                    Dt = new DataTable("tb_tmp");
                    Dt.Columns.Add("发票编号", Type.GetType("System.String"));
                    Dt.Columns.Add("单据日期", Type.GetType("System.String"));
                    Dt.Columns.Add("客户名称", Type.GetType("System.String"));
                    Dt.Columns.Add("商品编号", Type.GetType("System.String"));
                    Dt.Columns.Add("商品名称", Type.GetType("System.String"));
                    Dt.Columns.Add("数量", Type.GetType("System.Double"));
                    Dt.Columns.Add("含税金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("核销金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("余额", Type.GetType("System.Double"));
                    Dt.Columns.Add("核销日期", Type.GetType("System.String"));
                    Dt.Columns.Add("核销记录", Type.GetType("System.String"));
                    Dt.Columns.Add("凭证号", Type.GetType("System.String"));
                    Dt.Columns.Add("备注", Type.GetType("System.String"));
                    Dt.Columns.Add("备注1", Type.GetType("System.String"));

                    Update_Dts_YINGFU();
                    Produce_dt();
                }
                //应收核销
                if (this.toolStripComboBox1.SelectedIndex == 2)
                {
                    Dt = new DataTable("tb_tmp");
                    Dt.Columns.Add("工作年月", Type.GetType("System.String"));
                    Dt.Columns.Add("客户代码", Type.GetType("System.String"));
                    Dt.Columns.Add("客户名称", Type.GetType("System.String"));
                    Dt.Columns.Add("商品名称", Type.GetType("System.String"));
                    Dt.Columns.Add("核销金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("现金支票", Type.GetType("System.String"));
                    Dt.Columns.Add("核销记录", Type.GetType("System.String"));
                    Dt.Columns.Add("凭证号", Type.GetType("System.String"));
                    Dt.Columns.Add("备注", Type.GetType("System.String"));
                    Dt.Columns.Add("备注1", Type.GetType("System.String"));

                    Update_Dts_YINGSHOUHX();
                    Produce_dt();
                }
                //应付核销
                if (this.toolStripComboBox1.SelectedIndex == 3)
                {
                    Dt = new DataTable("tb_tmp");
                    Dt.Columns.Add("工作年月", Type.GetType("System.String"));
                    Dt.Columns.Add("核销记录", Type.GetType("System.String"));
                    Dt.Columns.Add("客户名称", Type.GetType("System.String"));
                    Dt.Columns.Add("核销金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("凭证号", Type.GetType("System.String"));
                    Dt.Columns.Add("备注", Type.GetType("System.String"));
                    Dt.Columns.Add("备注1", Type.GetType("System.String"));

                    Update_Dts_YINGFUHX();
                    Produce_dt();
                }
                //预收
                if (this.toolStripComboBox1.SelectedIndex == 4)
                {
                    Dt.Columns.Add("单据类别", Type.GetType("System.String"));
                    Dt.Columns.Add("单据日期", Type.GetType("System.String"));
                    Dt.Columns.Add("仓库", Type.GetType("System.String"));
                    Dt.Columns.Add("发票编号", Type.GetType("System.String"));
                    Dt.Columns.Add("客户名称", Type.GetType("System.String"));
                    Dt.Columns.Add("商品编号", Type.GetType("System.String"));
                    Dt.Columns.Add("商品名称", Type.GetType("System.String"));
                    Dt.Columns.Add("数量", Type.GetType("System.Double"));
                    Dt.Columns.Add("含税金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("核销金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("核销记录", Type.GetType("System.String"));
                    Dt.Columns.Add("操作员", Type.GetType("System.String"));
                    Dt.Columns.Add("成本金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("凭证号", Type.GetType("System.String"));
                    Dt.Columns.Add("备注", Type.GetType("System.String"));
                    Dt.Columns.Add("备注1", Type.GetType("System.String"));

                    Update_Dts_YUSHOU();
                    Produce_dt();
                }
                //预付
                if (this.toolStripComboBox1.SelectedIndex == 5)
                {
                    Dt = new DataTable("tb_tmp");
                    Dt.Columns.Add("发票编号", Type.GetType("System.String"));
                    Dt.Columns.Add("单据日期", Type.GetType("System.String"));
                    Dt.Columns.Add("客户名称", Type.GetType("System.String"));
                    Dt.Columns.Add("商品编号", Type.GetType("System.String"));
                    Dt.Columns.Add("商品名称", Type.GetType("System.String"));
                    Dt.Columns.Add("数量", Type.GetType("System.Double"));
                    Dt.Columns.Add("含税金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("核销金额", Type.GetType("System.Double"));
                    Dt.Columns.Add("余额", Type.GetType("System.Double"));
                    Dt.Columns.Add("核销日期", Type.GetType("System.String"));
                    Dt.Columns.Add("核销记录", Type.GetType("System.String"));
                    Dt.Columns.Add("凭证号", Type.GetType("System.String"));
                    Dt.Columns.Add("备注", Type.GetType("System.String"));
                    Dt.Columns.Add("备注1", Type.GetType("System.String"));

                    Update_Dts_YUFU();
                    Produce_dt();
                }
            }
        }
        //获取应收数据
        private void Update_Dts_YINGSHOU()
        {
            DataTable dt = new DataTable();
            string select_sql = "select [Lx] ,[单据类别],[单据日期],[发票编号],[客户编号],[客户名称],[商品编号],[商品名称],[数量]," +
                "[含税金额] ,[成本金额],[验收员],[核销金额],[核销记录],[核销人],[核销号],[操作员],[凭证号],[备注] FROM [S70取应收款单据] "+this.sqlwhere;
            dt = (new SqlDBConnect()).Get_Dt(select_sql);
            this.Dt.Clear();//清楚原有数据框架
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                if (dt.Rows.Count > 0)
                {          
                    DataRow Dr = Dt.NewRow();
                    Dr["单据类别"] = dt.Rows[m]["单据类别"];
                    Dr["单据日期"] = dt.Rows[m]["单据日期"];
                    Dr["仓库"] = dt.Rows[m]["Lx"];
                    Dr["发票编号"] = dt.Rows[m]["发票编号"];
                    Dr["客户名称"] = dt.Rows[m]["客户名称"];
                    Dr["商品编号"] = dt.Rows[m]["商品编号"];
                    Dr["商品名称"] = dt.Rows[m]["商品名称"];
                    Dr["数量"] = dt.Rows[m]["数量"];
                    Dr["含税金额"] = dt.Rows[m]["含税金额"];
                    Dr["核销金额"] = dt.Rows[m]["核销金额"];
                    Dr["核销记录"] = dt.Rows[m]["核销金额"];
                    Dr["操作员"] = dt.Rows[m]["操作员"];
                    Dr["成本金额"] = dt.Rows[m]["成本金额"];
                    Dr["凭证号"] = dt.Rows[m]["凭证号"];
                    Dr["备注"] = dt.Rows[m]["备注"];
                    Dr["备注1"] = dt.Rows[m]["备注1"];
                    Dt.Rows.Add(Dr);
                    this.progressBar1.Value = 3;
                 }
            }
        }
        //获取应付数据
        private void Update_Dts_YINGFU()
        {
            DataTable dt = new DataTable();
            string select_sql = "select [单据日期],[发票编号],[客户名称],[商品编号],[商品名称],[数量]," +
                "[含税金额],[核销金额],[核销记录],[凭证号],[备注] FROM [S71取应付款单据] " + this.sqlwhere;
            dt = (new SqlDBConnect()).Get_Dt(select_sql);
            this.Dt.Clear();//清楚原有数据框架
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow Dr = Dt.NewRow();
                    Dr["单据日期"] = dt.Rows[m]["单据日期"];
                    Dr["发票编号"] = dt.Rows[m]["发票编号"];
                    Dr["客户名称"] = dt.Rows[m]["客户名称"];
                    Dr["商品编号"] = dt.Rows[m]["商品编号"];
                    Dr["商品名称"] = dt.Rows[m]["商品名称"];
                    Dr["数量"] = dt.Rows[m]["数量"];
                    Dr["含税金额"] = dt.Rows[m]["含税金额"];
                    Dr["核销金额"] = dt.Rows[m]["核销金额"];
                    Dr["核销记录"] = dt.Rows[m]["核销记录"];
                    Dr["凭证号"] = dt.Rows[m]["凭证号"];
                    Dr["备注"] = dt.Rows[m]["备注"];
                    Dr["备注1"] = dt.Rows[m]["备注1"];
                    Dt.Rows.Add(Dr);
                    this.progressBar1.Value = 3;
                }
            }
        }

        //获取应收核销数据
        private void Update_Dts_YINGSHOUHX()
        {
            DataTable dt = new DataTable();
            string select_sql = "select 工作年月 ,客户代码,客户名称,核销金额,现金支票,凭证号,备注,备注1 FROM S71应付款核销 "+this.sqlwhere;
            dt = (new SqlDBConnect()).Get_Dt(select_sql);
            this.Dt.Clear();//清楚原有数据框架
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow Dr = Dt.NewRow();
                    Dr["工作年月"] = dt.Rows[m]["工作年月"];
                    Dr["客户代码"] = dt.Rows[m]["客户代码"];
                    Dr["客户名称"] = dt.Rows[m]["客户名称"];
                    Dr["核销金额"] = dt.Rows[m]["核销金额"];
                    Dr["现金支票"] = dt.Rows[m]["现金支票"];
                    Dr["凭证号"] = dt.Rows[m]["凭证号"];
                    Dr["备注"] = dt.Rows[m]["备注"];
                    Dr["备注1"] = dt.Rows[m]["备注1"];
                    Dt.Rows.Add(Dr);
                    this.progressBar1.Value = 3;
                }
            }
        }
        //获取应付核销数据
        private void Update_Dts_YINGFUHX()
        {
            DataTable dt = new DataTable();
            string select_sql = "select 工作年月,核销记录,客户名称,核销金额,凭证号  ,备注  ,备注1 from S71应付款核销 " +this.sqlwhere;
            dt = (new SqlDBConnect()).Get_Dt(select_sql);
            this.Dt.Clear();//清楚原有数据框架
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow Dr = Dt.NewRow();
                    Dr["工作年月"] = dt.Rows[m]["工作年月"];
                    Dr["核销记录"] = dt.Rows[m]["核销记录"];
                    Dr["客户名称"] = dt.Rows[m]["客户名称"];
                    Dr["核销金额"] = dt.Rows[m]["核销金额"];
                    Dr["凭证号"] = dt.Rows[m]["凭证号"];
                    Dr["备注"] = dt.Rows[m]["备注"];
                    Dr["备注1"] = dt.Rows[m]["备注1"];
                    Dt.Rows.Add(Dr);
                    this.progressBar1.Value = 3;
                }
            }
        }
        //获取预收数据
        private void Update_Dts_YUSHOU()
        {
            DataTable dt = new DataTable();
            this.Dt.Clear();//清楚原有数据框架

            if (this.sqlwhere != "")
                this.sqlwhere += " and 单据类别='50' ";
            else
                this.sqlwhere += " where 单据类别='50'";

            string select_sql = "select [单据日期],[发票编号],[客户名称],[商品编号],[商品名称],[数量]," +
                "[含税金额],[核销金额],[核销记录],[凭证号],[备注] FROM [S71取应付款单据] "+this.sqlwhere;
            dt = (new SqlDBConnect()).Get_Dt(select_sql);

            for (int m = 0; m < dt.Rows.Count; m++)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow Dr = Dt.NewRow();
                    Dr["单据日期"] = dt.Rows[m]["单据日期"];
                    Dr["发票编号"] = dt.Rows[m]["发票编号"];
                    Dr["客户名称"] = dt.Rows[m]["客户名称"];
                    Dr["商品编号"] = dt.Rows[m]["商品编号"];
                    Dr["商品名称"] = dt.Rows[m]["商品名称"];
                    Dr["数量"] = dt.Rows[m]["数量"];
                    Dr["含税金额"] = dt.Rows[m]["含税金额"];
                    Dr["核销金额"] = dt.Rows[m]["核销金额"];
                    Dr["核销记录"] = dt.Rows[m]["核销记录"];
                    Dr["凭证号"] = dt.Rows[m]["凭证号"];
                    Dr["备注"] = dt.Rows[m]["备注"];
                    Dr["备注1"] = dt.Rows[m]["备注1"];
                    Dt.Rows.Add(Dr);
                    this.progressBar1.Value = 3;
                }
            }
        }
        //获取预付数据
        private void Update_Dts_YUFU()
        {
            DataTable dt = new DataTable();
            this.Dt.Clear();//清楚原有数据框架

            if (this.sqlwhere != "")
                this.sqlwhere += " and 单据类别='50' ";
            else
                this.sqlwhere += " where 单据类别='50'";

            string select_sql = "select [单据日期],[发票编号],[客户名称],[商品编号],[商品名称],[数量]," +
                "[含税金额],[核销金额],[核销记录],[凭证号],[备注] FROM [S71取应付款单据] "+this.sqlwhere;
            dt = (new SqlDBConnect()).Get_Dt(select_sql);

            for (int m = 0; m < dt.Rows.Count; m++)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow Dr = Dt.NewRow();
                    Dr["单据日期"] = dt.Rows[m]["单据日期"];
                    Dr["发票编号"] = dt.Rows[m]["发票编号"];
                    Dr["客户名称"] = dt.Rows[m]["客户名称"];
                    Dr["商品编号"] = dt.Rows[m]["商品编号"];
                    Dr["商品名称"] = dt.Rows[m]["商品名称"];
                    Dr["数量"] = dt.Rows[m]["数量"];
                    Dr["含税金额"] = dt.Rows[m]["含税金额"];
                    Dr["核销金额"] = dt.Rows[m]["核销金额"];
                    Dr["核销记录"] = dt.Rows[m]["核销记录"];
                    Dr["凭证号"] = dt.Rows[m]["凭证号"];
                    Dr["备注"] = dt.Rows[m]["备注"];
                    Dr["备注1"] = dt.Rows[m]["备注1"];
                    Dt.Rows.Add(Dr);
                    this.progressBar1.Value = 3;
                }
            }
        }
        //生成Dt
        public void Produce_dt()
        {
            this.progressBar1.Maximum = 7;

            this.progressBar1.Value = 5;
            this.progressBar1.Value = 6;

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
            StaticBox.Text = this.TitleName;//表名
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
           
            switch (this.selectedIndex)
            {
                case 0:
                    RecordSet.AddField("单据类别", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("单据日期", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("仓库", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("发票编号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("客户名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("商品编号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("商品名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("数量", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("含税金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销记录", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("操作员", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("成本金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("凭证号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("备注", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("备注1", GRFieldType.grftString).Format = "#,##0.00";

                    Report.DetailGrid.AddColumn("序号", "序号", "序号", 1.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                    Report.DetailGrid.AddColumn("单据类别", "单据类别", "单据类别", 1.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                    Report.DetailGrid.AddColumn("单据日期", "单据日期", "单据日期", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("仓库", "仓库", "仓库", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("发票编号", "发票编号", "发票编号", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("客户名称", "客户名称", "客户名称", 2.7).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("商品编号", "商品编号", "商品编号", 3.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("商品名称", "商品名称", "商品名称", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("数量", "数量", "数量", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("含税金额", "含税金额", "含税金额", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销金额", "核销金额", "核销金额", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销记录", "核销记录", "核销记录", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("操作员", "操作员", "操作员", 2.28).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("成本金额", "成本金额", "成本金额", 2.58).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("凭证号", "凭证号", "凭证号", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注", "备注", "备注", 2.28).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注1", "备注1", "备注1", 2.58).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                   
                    break;
                     
                case 1:
                    RecordSet.AddField("发票编号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("单据日期", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("客户名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("商品编号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("商品名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("数量", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("含税金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("余额", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("核销日期", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销记录", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("凭证号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("备注", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("备注1", GRFieldType.grftString).Format = "#,##0.00";

                    Report.DetailGrid.AddColumn("序号", "序号", "序号", 1.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                    Report.DetailGrid.AddColumn("单据日期", "单据日期", "单据日期", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("发票编号", "发票编号", "发票编号", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("客户名称", "客户名称", "客户名称", 2.7).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("商品编号", "商品编号", "商品编号", 3.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("商品名称", "商品名称", "商品名称", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("数量", "数量", "数量", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("含税金额", "含税金额", "含税金额", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销金额", "核销金额", "核销金额", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("余额", "余额", "余额", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销日期", "核销日期", "核销日期", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销记录", "核销记录", "核销记录", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("凭证号", "凭证号", "凭证号", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注", "备注", "备注", 2.28).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注1", "备注1", "备注1", 2.58).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                   
                    break;
              
               case 2:
                    RecordSet.AddField("工作年月", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("客户代码", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("客户名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("商品名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("现金支票", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("核销记录", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("凭证号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("备注", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("备注1", GRFieldType.grftString).Format = "#,##0.00";

                    Report.DetailGrid.AddColumn("序号", "序号", "序号", 1.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                    Report.DetailGrid.AddColumn("工作年月", "工作年月", "工作年月", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("客户代码", "客户代码", "客户代码", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("客户名称", "客户名称", "客户名称", 2.7).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("商品名称", "商品名称", "商品名称", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("核销金额", "核销金额", "核销金额", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("现金支票", "现金支票", "现金支票", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销记录", "核销记录", "核销记录", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("凭证号", "凭证号", "凭证号", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注", "备注", "备注", 2.28).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注1", "备注1", "备注1", 2.58).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                   
                    break;

               case 3:
                    RecordSet.AddField("工作年月", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销记录", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("客户名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("凭证号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("备注", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("备注1", GRFieldType.grftString).Format = "#,##0.00";

                    Report.DetailGrid.AddColumn("序号", "序号", "序号", 1.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                    Report.DetailGrid.AddColumn("工作年月", "工作年月", "工作年月", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("核销记录", "核销记录", "核销记录", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("客户名称", "客户名称", "客户名称", 2.7).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("核销金额", "核销金额", "核销金额", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("凭证号", "凭证号", "凭证号", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注", "备注", "备注", 2.28).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注1", "备注1", "备注1", 2.58).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                   
                    break;
                   
               case 4:
                    RecordSet.AddField("单据类别", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("单据日期", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("仓库", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("发票编号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("客户名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("商品编号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("商品名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("数量", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("含税金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销记录", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("操作员", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("成本金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("凭证号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("备注", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("备注1", GRFieldType.grftString).Format = "#,##0.00";

                    Report.DetailGrid.AddColumn("序号", "序号", "序号", 1.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                    Report.DetailGrid.AddColumn("单据类别", "单据类别", "单据类别", 1.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                    Report.DetailGrid.AddColumn("单据日期", "单据日期", "单据日期", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("仓库", "仓库", "仓库", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("发票编号", "发票编号", "发票编号", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("客户名称", "客户名称", "客户名称", 2.7).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("商品编号", "商品编号", "商品编号", 3.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("商品名称", "商品名称", "商品名称", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("数量", "数量", "数量", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("含税金额", "含税金额", "含税金额", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销金额", "核销金额", "核销金额", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销记录", "核销记录", "核销记录", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("操作员", "操作员", "操作员", 2.28).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("成本金额", "成本金额", "成本金额", 2.58).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("凭证号", "凭证号", "凭证号", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注", "备注", "备注", 2.28).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注1", "备注1", "备注1", 2.58).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                   
                    break;
               case 5:
                    RecordSet.AddField("发票编号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("单据日期", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("客户名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("商品编号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("商品名称", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("数量", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("含税金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销金额", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("余额", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("核销日期", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("核销记录", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("凭证号", GRFieldType.grftString).Format = "#,##0.00";
                    RecordSet.AddField("备注", GRFieldType.grftString).Format = "#,##0";
                    RecordSet.AddField("备注1", GRFieldType.grftString).Format = "#,##0.00";

                    Report.DetailGrid.AddColumn("序号", "序号", "序号", 1.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;
                    Report.DetailGrid.AddColumn("单据日期", "单据日期", "单据日期", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("发票编号", "发票编号", "发票编号", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("客户名称", "客户名称", "客户名称", 2.7).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("商品编号", "商品编号", "商品编号", 3.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("商品名称", "商品名称", "商品名称", 2.1).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
                    Report.DetailGrid.AddColumn("数量", "数量", "数量", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("含税金额", "含税金额", "含税金额", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销金额", "核销金额", "核销金额", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("余额", "余额", "余额", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销日期", "核销日期", "核销日期", 1.9).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("核销记录", "核销记录", "核销记录", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("凭证号", "凭证号", "凭证号", 2.3).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注", "备注", "备注", 2.28).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                    Report.DetailGrid.AddColumn("备注1", "备注1", "备注1", 2.58).ContentCell.TextAlign = GRTextAlign.grtaBottomRight;
                   
                    break;
                default: return;
            }

           

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
      
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            Report.Print(true);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Report.PrintPreview(true);
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedIndex = this.toolStripComboBox1.SelectedIndex;//获取combobox所选索引,用于控制所要显示报表列

            if (this.toolStripComboBox1.SelectedIndex == 0)
                this.TitleName = "应收款查询";
            if (this.toolStripComboBox1.SelectedIndex == 1)
                this.TitleName = "应付款查询";
            if (this.toolStripComboBox1.SelectedIndex == 2)
                this.TitleName = "应收核销查询";
            if (this.toolStripComboBox1.SelectedIndex == 3)
                this.TitleName = "应付核销查询";
            if (this.toolStripComboBox1.SelectedIndex == 4)
                this.TitleName = "预收款查询";
            if (this.toolStripComboBox1.SelectedIndex == 5)
                this.TitleName = "预付款查询";
        }

        private void S70_71Form_Load(object sender, EventArgs e)
        {

        }
    }
}
