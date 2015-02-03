using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Warehouse.DB;
using System.Windows.Forms;
using Warehouse.Base;
using grproLib;

namespace Warehouse.StockReport
{
    public partial class FormStockPandian : Form
    {
        //定义报表
        protected GridppReport Report = new GridppReport();

        public string Tjwhere = "";
        private string TjMaxBatime;//当前所选条件下对应的结存年月
        private string TjPretime;//月初到所选日期前一天(为范围时取左端点前一天
        private string TjWhen;//所选时间限制条件
        private string TJnum = "";

        public DataTable Dtb=null;
        DataRow dr = null;
        private DataTable newDT = new DataTable();

        //对取出元素值为空的赋零值
        public static double YNdbnull(string DTelement)
        {
            object o = DTelement;
            if (o != DBNull.Value && DTelement != "")
            {
                return Convert.ToDouble(DTelement);
            }
            else
                return 0;
        }

        public FormStockPandian(string Tjwhere, string MaxBatime, string sqlPretime, string sqlWhen,string TJnum)
        {
            InitializeComponent();
            this.Tjwhere = Tjwhere;
            this.TjMaxBatime = MaxBatime;
            this.TjPretime = sqlPretime;
            this.TjWhen = sqlWhen;
            this.TJnum = TJnum;
            if (this.Tjwhere.IndexOf("T_Receipt_Main_Det.MatName") != -1)
            {
                this.Tjwhere = Tjwhere.Replace("T_Receipt_Main_Det.MatName"," t_matinf.matname ");
            }

            //build_dt();
            //Produce_dt();
            //get_tot();
            //this.newDT = Dtb.Copy();       
            //设置与数据源的连接串，因为在设计时指定的数据库路径是绝对路径。
            //Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(Report_Initialize);
            //Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Report_FetchRecord);
            //this.axGRDisplayViewer1.Report = Report;
        }

        void Report_Initialize()
        {
            DefineReport();
        }

        void Report_FetchRecord(ref bool pEof)
        {
            GridReportUtility.FillRecordToReport(Report, this.newDT);
        }


        private void FormStockPandian_Load(object sender, EventArgs e)
        {
            //this.axGRDisplayViewer1.Start();      
        }

        public void build_dt()
        {
            Dtb = new DataTable("tb_tmp");
          //  Dtb.Columns.Add("序号", Type.GetType("System.String"));
            Dtb.Columns.Add("卡片编号", Type.GetType("System.String"));
            Dtb.Columns.Add("卡片名称", Type.GetType("System.String"));
            Dtb.Columns.Add("仓库", Type.GetType("System.String"));
            Dtb.Columns.Add("类型", Type.GetType("System.Double"));
            Dtb.Columns.Add("期初数量", Type.GetType("System.Double"));
            Dtb.Columns.Add("期初成本", Type.GetType("System.Double"));

            Dtb.Columns.Add("收入数量", Type.GetType("System.Double"));
            Dtb.Columns.Add("收入成本", Type.GetType("System.Double"));

            Dtb.Columns.Add("发出数量", Type.GetType("System.Double"));
            Dtb.Columns.Add("发出成本", Type.GetType("System.Double"));

            Dtb.Columns.Add("结存数量", Type.GetType("System.Double"));
            Dtb.Columns.Add("结存成本", Type.GetType("System.Double"));          
        }

        //期间收入
        //private void Update_Dts_In(DateTime FromD,DateTime ToD, string lx)
        private void Update_Dts_In(string sqlwhen, string lx)
        {
            DataTable dt=new DataTable();
            string ck_, matid_, matname_, lx_;
            string sql_;
            if (sqlwhen=="")
            {
                sqlwhen = " and T_Receipt_Main_Det.CurWorkMonth= '" + this.TjMaxBatime + "'";
            }
            sql_ = "select T_Receipt_Main_Det.SourceStoreH,"
                    + " T_Receipt_Main_Det.matid,"
                    + " T_Receipt_Main_Det.mattype,"
                    + " T_matinf.MatName,"
                    + "sum(T_Receipt_Main_Det.num) as num,"
                    + "sum(T_Receipt_Main_Det.TTaxPurchPrice) as PurchPrice "
                    + " from T_Receipt_Main_Det,T_matinf "
                    + " where T_Receipt_Main_Det.matid=T_matinf.matid "
                    + " and T_Receipt_Main_Det.receipttypeid<'51'"
                    +  this.Tjwhere + sqlwhen
                    + " group by T_Receipt_Main_Det.SourceStoreH,"
                    + " T_Receipt_Main_Det.matid,T_Receipt_Main_Det.mattype,"
                    + " T_matinf.MatName ";

            dt = (new SqlDBConnect()).Get_Dt(sql_);

            Double Nin=0.0;//数量
            Double Moneyin=0.0;//金额

            for (int m = 0; m < dt.Rows.Count; m++)
            {
                bool isfound = false;
                ck_ = dt.Rows[m]["SourceStoreH"].ToString().Trim();
                matid_ = dt.Rows[m]["matid"].ToString().Trim();
                matname_ = dt.Rows[m]["matname"].ToString().Trim();
                lx_ = dt.Rows[m]["mattype"].ToString().Trim();
                if (dt.Rows[m]["num"].ToString().Trim()!="")
                    Nin = Convert.ToDouble(dt.Rows[m]["num"].ToString().Trim());
                if (dt.Rows[m]["PurchPrice"].ToString().Trim()!="")
                    Moneyin = Convert.ToDouble(dt.Rows[m]["PurchPrice"].ToString().Trim());

                for (int k = 0; k < Dtb.Rows.Count; k++)
                {
                    if ((Dtb.Rows[k]["卡片编号"].ToString().Trim() == matid_)
                        && (Dtb.Rows[k]["仓库"].ToString().Trim() == ck_)
                            && (Dtb.Rows[k]["类型"].ToString().Trim() == lx_))
                    {
                        dr = Dtb.Rows[k];
                        if (lx == "add")
                        {
                            if (dr["期初数量"].ToString().Trim() == "")
                                dr["期初数量"] = Nin;
                            else
                                dr["期初数量"] = Convert.ToDouble(dr["期初数量"].ToString().Trim()) + Nin;

                            if (dr["期初成本"].ToString().Trim() == "")
                                dr["期初成本"] = Moneyin;
                            else
                                dr["期初成本"] = Math.Round(Convert.ToDouble(dr["期初成本"].ToString().Trim()),2) + Moneyin;                                                 
                        }
                        else
                        {
                            dr["收入数量"] = Nin;
                            dr["收入成本"] = Moneyin;
                        }
                        isfound = true;
                    }                 
                }
                if (isfound == false)
                {
                    DataRow drr = Dtb.NewRow();
                    drr["卡片编号"] = matid_;
                    drr["卡片名称"] = matname_;
                    drr["仓库"] = ck_;
                    drr["类型"] = lx_;

                    if (lx == "add")
                    {
                        if (drr["期初数量"].ToString().Trim() != "")
                            drr["期初数量"] = Convert.ToDouble(drr["期初数量"].ToString().Trim()) + Nin;
                        else
                            drr["期初数量"] = Nin;

                        if (drr["期初成本"].ToString().Trim() != "")
                            drr["期初成本"] = Math.Round(Convert.ToDouble(drr["期初成本"].ToString().Trim()), 2) + Moneyin;
                        else
                            drr["期初成本"] = Moneyin;                                             
                    }
                    else
                    {                      
                        drr["收入数量"] = Nin;
                        drr["收入成本"] = Moneyin;                        
                    }                                       
                    //drr["发出数量"] = 0.0;
                    //drr["发出成本"] = 0.0;
                  
                    //drr["结存数量"] = 0.0;
                    //drr["结存成本"] = 0.0;
                    Dtb.Rows.Add(drr);
                
                }           
            }                 
         }


        //期间发出
        private void Update_Dts_Out(string sqlwhen, string lx)
        {
            DataTable dt =null;
            string ck_, matid_, matname_, lx_;
            string sql_;
            if (sqlwhen == "")
            {
                sqlwhen = " and T_Receipt_Main_Det.CurWorkMonth= '" + this.TjMaxBatime + "'";
            }
            sql_ = "select T_Receipt_Main_Det.SourceStoreH,"
                    + " T_Receipt_Main_Det.matid,"
                    + " T_Receipt_Main_Det.mattype,"
                    + " T_matinf.MatName,"
                    + "sum(T_Receipt_Main_Det.num) as num,"
                    + "sum(T_Receipt_Main_Det.TTaxPurchPrice) as PurchPrice "
                    + " from T_Receipt_Main_Det,T_matinf "
                    + " where T_Receipt_Main_Det.matid=T_matinf.matid "
                    + " and T_Receipt_Main_Det.receipttypeid>='51'"
                    + this.Tjwhere + sqlwhen
                    + " group by T_Receipt_Main_Det.SourceStoreH,"
                    + " T_Receipt_Main_Det.matid,T_Receipt_Main_Det.mattype,"
                    + " T_matinf.MatName ";
            dt = (new SqlDBConnect()).Get_Dt(sql_);

            Double Nout=0.0;
            Double Moneyout=0.0;
            
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                ck_ = dt.Rows[m]["SourceStoreH"].ToString().Trim();
                matid_ = dt.Rows[m]["matid"].ToString().Trim();
                matname_ = dt.Rows[m]["matname"].ToString().Trim();
                lx_ = dt.Rows[m]["mattype"].ToString().Trim();
                if (dt.Rows[m]["num"].ToString().Trim()!="")
                    Nout = Convert.ToDouble(dt.Rows[m]["num"].ToString().Trim());
                if (dt.Rows[m]["PurchPrice"].ToString().Trim()!="")
                    Moneyout = Convert.ToDouble(dt.Rows[m]["PurchPrice"].ToString().Trim());
                bool isfound = false;
                for (int k = 0; k < Dtb.Rows.Count; k++)
                {
                    if ((Dtb.Rows[k]["卡片编号"].ToString().Trim() == matid_)
                        && (Dtb.Rows[k]["仓库"].ToString().Trim() == ck_)
                            && (Dtb.Rows[k]["类型"].ToString().Trim() == lx_))
                    {
                        dr = Dtb.Rows[k];
                        if (lx == "edit")
                        {
                            dr["发出数量"] =  Nout;
                            dr["发出成本"] = Moneyout;
                        }
                        else
                        {
                            if (dr["期初数量"].ToString().Trim() == "")
                                dr["期初数量"] = 0.0 - Nout;
                            else
                                dr["期初数量"] = Convert.ToDouble(dr["期初数量"].ToString().Trim()) - Nout;
                            
                            if (dr["期初成本"].ToString().Trim() == "")
                                dr["期初成本"] = 0.0 - Moneyout;
                            else
                                dr["期初成本"] = Math.Round(Convert.ToDouble(dr["期初成本"].ToString().Trim()),2) - Moneyout;
                          
                        }
                        isfound = true;
                    }            
                }
                if (isfound == false) 
                {
                    DataRow drr = Dtb.NewRow();
                    drr["卡片编号"] = matid_;
                    drr["卡片名称"] = matname_;
                    drr["仓库"] = ck_;
                    drr["类型"] = lx_;

                    if (lx == "edit")
                    {
                        drr["发出数量"] = Nout;
                        drr["发出成本"] = Moneyout;                     
                    }
                    else
                    {
                        if (drr["期初成本"].ToString().Trim() == "")
                            drr["期初成本"] = 0.0 - Moneyout;
                        else 
                            drr["期初成本"] = Math.Round(Convert.ToDouble(drr["期初成本"].ToString().Trim()),2) - Moneyout;

                        if (dr["期初数量"].ToString().Trim() == "")
                            drr["期初数量"] = Convert.ToDouble(dr["期初数量"].ToString().Trim()) - Nout;
                        else
                            drr["期初数量"] = 0 - Nout;                                              
                    }
                  
                    //drr["收入数量"] = 0.0;
                    //drr["收入成本"] = 0.0;
                    //drr["结存数量"] = 0.0;
                    //drr["结存成本"] = 0.0;
                    Dtb.Rows.Add(drr);

                }
            }           
        }


        public void Produce_dt()
        {
            DataTable dt=new DataTable();
            this.progressBar1.Maximum = 7;
            this.progressBar1.Value = 0;

            string TJwhere_ = "";
            if (this.Tjwhere != "" && Tjwhere.IndexOf("T_Receipt_Main_Det") != -1)
            {
                TJwhere_ = Tjwhere.Replace(" T_Receipt_Main_Det", "T_Stock_status");
                if (TJwhere_.IndexOf("T_Stock_status.MatName") != -1)
                    TJwhere_ = TJwhere_.Replace("T_Stock_status.MatName", " t_matinf.matname");
                if (TJwhere_.IndexOf("T_Stock_status.SourceStoreH") != -1)
                    TJwhere_ = TJwhere_.Replace("T_Stock_status.SourceStoreH", "T_Stock_status.storeHouseid");
            }
            string sql_ = "select T_Stock_status.storeHouseid,"
                + "T_Stock_status.matid,"
                + "T_matinf.matname,"
                + "T_Stock_status.mattype,"
                + "T_Stock_status.firstcount,"
                + "T_Stock_status.firstmoney "
                + " from T_Stock_status,T_matinf "
                + " where T_Stock_status.matid=T_matinf.matid "
                // + " and T_Stock_status.balanceTime='201108'";  //
                + TJwhere_
                + " and T_Stock_status.balanceTime='" + this.TjMaxBatime + "'";
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            string ck_, matid_, matname_, lx_;
            Double Nqc=0.0;//期初数量
            Double Moneyqc=0.0;//期初金额
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                ck_ = dt.Rows[m]["storeHouseid"].ToString().Trim();
                matid_ = dt.Rows[m]["matid"].ToString().Trim();
                matname_ = dt.Rows[m]["matname"].ToString().Trim();
                lx_ = dt.Rows[m]["mattype"].ToString().Trim();
                if (dt.Rows[m]["firstcount"].ToString().Trim() == "")
                    Nqc = 0;
                else
                    Nqc = Convert.ToDouble(dt.Rows[m]["firstcount"].ToString().Trim());
                if (dt.Rows[m]["firstmoney"].ToString().Trim() == "")
                    Moneyqc = 0.0;
                else
                    Moneyqc = Convert.ToDouble(dt.Rows[m]["firstmoney"].ToString().Trim());
                dr = Dtb.NewRow();
                dr["卡片编号"] = matid_;
                dr["卡片名称"] = matname_;
                dr["仓库"] = ck_;
                dr["类型"] = lx_;
                dr["期初数量"] = Nqc;
                dr["期初成本"] = Moneyqc;
                dr["收入数量"] = 0.0;
                dr["收入成本"] = 0.0;
                dr["发出数量"] = 0.0;
                dr["发出成本"] = 0.0;
                dr["结存数量"] = 0.0;
                dr["结存成本"] = 0.0;
                Dtb.Rows.Add(dr);
            }
            this.progressBar1.Value = 1;
           
            Update_Dts_In(this.TjPretime,"add");//所选时间区间前一天到月初的数据
            this.progressBar1.Value = 2;
           
            Update_Dts_In(this.TjWhen, "edit");//所选时间区间内发生的数据
            this.progressBar1.Value = 3;

            Update_Dts_Out(TjPretime, "add");
            this.progressBar1.Value = 4;
        
            Update_Dts_Out(this.TjWhen,  "edit");
            this.progressBar1.Value = 5;
            double f1 = 0.0, f2 = 0.0, f3 = 0.0;
            for (int m = 0; m < Dtb.Rows.Count; m++)
            {
                dr = Dtb.Rows[m];
                if (dr["期初数量"].ToString().Trim() == "")
                    f1 = 0;
                else
                    f1 = Convert.ToDouble(dr["期初数量"].ToString().Trim());
                if (dr["收入数量"].ToString().Trim() == "")
                    f2 = 0;
                else
                    f2 = Convert.ToDouble(dr["收入数量"].ToString().Trim());
                if (dr["发出数量"].ToString().Trim() == "")
                    f3 = 0;
                else
                    f3 = Convert.ToDouble(dr["发出数量"].ToString().Trim());


                dr["结存数量"] = f1 + f2 - f3;

                if (dr["期初成本"].ToString().Trim() == "")
                    f1 = 0;
                else
                    f1 = Convert.ToDouble(dr["期初成本"].ToString().Trim());
                if (dr["收入成本"].ToString().Trim() == "")
                    f2 = 0;
                else
                    f2 = Convert.ToDouble(dr["收入成本"].ToString().Trim());
                if (dr["发出成本"].ToString().Trim() == "")
                    f3 = 0;
                else
                    f3 = Convert.ToDouble(dr["发出成本"].ToString().Trim());

                dr["结存成本"] = f1 + f2 - f3;


            }
            this.progressBar1.Value = 6;

            for (int i = 0; i < this.Dtb.Rows.Count; i++)
            {
                //double firstNum = Convert.ToDouble(Dtb.Rows[i]["期初数量"].ToString().Trim());
                //double inNum = Convert.ToDouble(Dtb.Rows[i]["收入数量"].ToString().Trim());
                //double outNum = Convert.ToDouble(Dtb.Rows[i]["发出数量"].ToString().Trim());
                //double sumNum = Convert.ToDouble(Dtb.Rows[i]["结存数量"].ToString().Trim());
                //double sumMoney = Convert.ToDouble(Dtb.Rows[i]["结存成本"].ToString().Trim());

                //string condition = this.TJnum;

                //if (Convert.ToBoolean(Microsoft.JScript.GlobalObject.eval(this.TJnum)) == true)
                //{

                //}


            }
          
          
        
            //设置与数据源的连接串，因为在设计时指定的数据库路径是绝对路径。
          
            this.newDT = Dtb.Copy();

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

        public void get_tot()//合计
        {
            DataRow dr;
            double Nqc = 0.0, Mqc = 0, Nin = 0.0, Nout = 0.0, Njc = 0.0, Mjc = 0.0;
            for (int i = 0; i < Dtb.Rows.Count; i++)
            {

                dr = Dtb.Rows[i];
                //if (Math.Abs(Convert.ToDouble(dr["结存成本"].ToString().Trim())) < 0.00001)
                //    continue;
                if (dr["期初数量"].ToString().Trim() != "")
                    Nqc = Convert.ToDouble(dr["期初数量"].ToString().Trim()) + Nqc;
                if (dr["期初成本"].ToString().Trim() != "")
                    Mqc = Convert.ToDouble(dr["期初成本"].ToString().Trim()) + Mqc;
                if (dr["收入数量"].ToString().Trim() != "")
                    Nin = Convert.ToDouble(dr["收入数量"].ToString().Trim()) + Nin;

                if (dr["发出数量"].ToString().Trim() != "")
                    Nout = Convert.ToDouble(dr["发出数量"].ToString().Trim()) + Nout;

                if (dr["结存数量"].ToString().Trim() != "")
                    Njc = Convert.ToDouble(dr["结存数量"].ToString().Trim()) + Njc;

                if (dr["结存成本"].ToString().Trim() != "")
                    Mjc = Convert.ToDouble(dr["结存成本"].ToString().Trim()) + Mjc;
            }

            dr = Dtb.NewRow();
            dr["卡片编号"] = "合计";
            dr["卡片名称"] = Dtb.Rows.Count.ToString();
            dr["仓库"] = "";
            dr["类型"] = DBNull.Value;
            dr["期初数量"] = Nqc;
            dr["期初成本"] = Mqc;
            dr["收入数量"] = Nin;
            dr["收入成本"] = DBNull.Value;
            dr["发出数量"] = Nout;
            dr["发出成本"] = DBNull.Value;
            dr["结存数量"] = Njc;
            dr["结存成本"] = Mjc;
            Dtb.Rows.Add(dr);
        }

        
        private void GetFieldName(string str)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxZD.SelectedIndex == -1 || this.toolStripComboBoxTJ.SelectedIndex == -1 || toolStripTextBoxFrom.Text.Trim() == "")
            {
                return;         
            }
            if (toolStripTextBoxTo.Visible == true && toolStripTextBoxTo.Text.Trim() == "")
                return;
            
            //this.newDT = Dtb.Clone();
            newDT.Rows.Clear();
            if (this.toolStripComboBoxZD.SelectedIndex == 0)
            {
                for (int i = 0; i < this.Dtb.Rows.Count; i++)
                {
                    if (this.toolStripComboBoxTJ.Text.IndexOf(">") != -1 && this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1&&
                        Convert.ToDouble(YNdbnull(Dtb.Rows[i]["期初数量"].ToString())) > Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                    {                                             
                        newDT.ImportRow(Dtb.Rows[i]);
                    }
                    else if (this.toolStripComboBoxTJ.Text.IndexOf("<") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                        Convert.ToDouble(YNdbnull(Dtb.Rows[i]["期初数量"].ToString())) < Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                    {
                        newDT.ImportRow(Dtb.Rows[i]);
                    }
                    else if (this.toolStripComboBoxTJ.Text.IndexOf(">=") != -1 &&
                        Convert.ToDouble(Dtb.Rows[i]["期初数量"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                    {
                        newDT.ImportRow(Dtb.Rows[i]);
                    }
                    else if (this.toolStripComboBoxTJ.Text.IndexOf("<=") != -1 && 
                        Convert.ToDouble(Dtb.Rows[i]["期初数量"]) <= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                    {
                        newDT.ImportRow(Dtb.Rows[i]);
                    }
                    else if (this.toolStripComboBoxTJ.Text.IndexOf("=") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                        this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1 && this.toolStripComboBoxTJ.Text.IndexOf("!=") == -1 &&
                        Convert.ToDouble(Dtb.Rows[i]["期初数量"]) - Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim())<0.000001)
                    {
                        newDT.ImportRow(Dtb.Rows[i]);
                    }
                    else if (this.toolStripComboBoxTJ.Text.IndexOf("!=") != -1 &&
                        Convert.ToDouble(Dtb.Rows[i]["期初数量"]) != Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                    {
                        newDT.ImportRow(Dtb.Rows[i]);
                    }
                    else if (this.toolStripComboBoxTJ.Text.IndexOf("范围") != -1 &&
                       Convert.ToDouble(Dtb.Rows[i]["期初数量"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim())
                        && Convert.ToDouble(Dtb.Rows[i]["期初数量"]) <= Convert.ToDouble(this.toolStripTextBoxTo.Text.Trim()))
                    {
                        newDT.ImportRow(Dtb.Rows[i]);
                    }
                }
            }
                ////
                if (this.toolStripComboBoxZD.SelectedIndex == 1)
                {
                    for (int i = 0; i < this.Dtb.Rows.Count; i++)
                    {
                        if (this.toolStripComboBoxTJ.Text.IndexOf(">") != -1 && this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["收入数量"]) > Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("<") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["收入数量"]) < Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf(">=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["收入数量"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("<=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["收入数量"]) <= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("=") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                            this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1 && this.toolStripComboBoxTJ.Text.IndexOf("!=") == -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["收入数量"]) - Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()) < 0.000001)
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("!=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["收入数量"]) != Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("范围") != -1 &&
                           Convert.ToDouble(Dtb.Rows[i]["收入数量"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim())
                            && Convert.ToDouble(Dtb.Rows[i]["收入数量"]) <= Convert.ToDouble(this.toolStripTextBoxTo.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                    }
                }
                    //////
                if (this.toolStripComboBoxZD.SelectedIndex == 2)
                {
                    for (int i = 0; i < this.Dtb.Rows.Count; i++)
                    {
                        if (this.toolStripComboBoxTJ.Text.IndexOf(">") != -1 && this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1 &&
                           Convert.ToDouble(Dtb.Rows[i]["发出数量"]) > Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("<") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["发出数量"]) < Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf(">=") != -1 &&
                            Convert.ToDouble( Dtb.Rows[i]["发出数量"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("<=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["发出数量"]) <= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("=") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                            this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1 && this.toolStripComboBoxTJ.Text.IndexOf("!=") == -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["发出数量"]) - Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()) < 0.000001)
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("!=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["发出数量"]) != Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("范围") != -1 &&
                           Convert.ToDouble(Dtb.Rows[i]["发出数量"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim())
                            && Convert.ToDouble(Dtb.Rows[i]["发出数量"]) <= Convert.ToDouble(this.toolStripTextBoxTo.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                    }
                }

                    //////
                if (this.toolStripComboBoxZD.SelectedIndex == 3)
                {
                    for (int i = 0; i < this.Dtb.Rows.Count; i++)
                    {
                        if (this.toolStripComboBoxTJ.Text.IndexOf(">") != -1 && this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1 &&
                           Convert.ToDouble(Dtb.Rows[i]["结存数量"]) > Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("<") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存数量"]) < Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf(">=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存数量"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("<=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存数量"]) <= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("=") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                            this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1 && this.toolStripComboBoxTJ.Text.IndexOf("!=") == -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存数量"]) - Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()) < 0.000001)
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("!=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存数量"]) != Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("范围") != -1 &&
                           Convert.ToDouble(Dtb.Rows[i]["结存数量"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim())
                            && Convert.ToDouble(Dtb.Rows[i]["结存数量"]) <= Convert.ToDouble(this.toolStripTextBoxTo.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                    }
                }
                    ///////
                if (this.toolStripComboBoxZD.SelectedIndex == 4)
                {
                    for (int i = 0; i < this.Dtb.Rows.Count; i++)
                    {
                        if (this.toolStripComboBoxTJ.Text.IndexOf(">") != -1 && this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1 &&
                           Convert.ToDouble(Dtb.Rows[i]["结存成本"]) > Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("<") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存成本"]) < Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf(">=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存成本"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("<=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存成本"]) <= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("=") != -1 && this.toolStripComboBoxTJ.Text.IndexOf("<=") == -1 &&
                            this.toolStripComboBoxTJ.Text.IndexOf(">=") == -1 && this.toolStripComboBoxTJ.Text.IndexOf("!=") == -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存成本"]) - Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()) < 0.000001)
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("!=") != -1 &&
                            Convert.ToDouble(Dtb.Rows[i]["结存成本"]) != Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                        else if (this.toolStripComboBoxTJ.Text.IndexOf("范围") != -1 &&
                           Convert.ToDouble(Dtb.Rows[i]["结存成本"]) >= Convert.ToDouble(this.toolStripTextBoxFrom.Text.Trim())
                            && Convert.ToDouble(Dtb.Rows[i]["结存成本"]) <= Convert.ToDouble(this.toolStripTextBoxTo.Text.Trim()))
                        {
                            newDT.ImportRow(Dtb.Rows[i]);
                        }
                    }
                }
                axGRDisplayViewer1.Stop();
                this.axGRDisplayViewer1.Start();
        }

        private void toolStripComboBoxTJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.toolStripLabel1.Visible = false;
            this.toolStripTextBoxTo.Visible = false;
            if (toolStripComboBoxTJ.SelectedIndex == 6)
            {
                this.toolStripLabel1.Visible = true;
                this.toolStripTextBoxTo.Visible = true;
            }
        }



        ////
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
            Report.DesignBottomMargin =0.7;
            Report.DesignTopMargin = 0.8;
            Report.DesignLeftMargin = 0.4;
            Report.DesignRightMargin = 0.4;

           // 插入一个部件框
           // IGRStaticBox StaticBox = Report.PageHeader.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            IGRStaticBox StaticBox = Report.PageHeader.Controls.Add(GRControlType.grctStaticBox).AsStaticBox;
            StaticBox.Text = "XX数码科技有限公司";   
            StaticBox.ForeColor = 255 * 256 * 256 + 0 * 256 + 0;
            StaticBox.TextAlign = GRTextAlign.grtaMiddleCenter;
            StaticBox.Font.Point = 9;
            StaticBox.Left = 1.0;
            StaticBox.Top = 0.5;
            StaticBox.Width = 8.20;
            StaticBox.Height = 1;           
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
            StaticBox.Text = "库存盘点表";
            StaticBox.Center = GRCenterStyle.grcsHorizontal; //使部件框在节中水平方向上居中对齐
            StaticBox.Font.Point = 20;
            StaticBox.Font.Bold = true;
            StaticBox.Top = 0.40;
            StaticBox.Width = 4.64;
            StaticBox.Height = 0.8;
        }
        private void DefineDetailGrid()
        {
            Report.InsertDetailGrid();
            Report.DetailGrid.ColumnTitle.Height = 0.68;
            Report.DetailGrid.ColumnContent.Height = 0.58;

            Report.DetailGrid.ColumnContent.AlternatingBackColor = 151 * 256 * 256 + 255 * 256 + 255;//内容行交替色
            Report.DetailGrid.ColumnTitle.BackColor = 217 * 256 * 256 + 217 * 256 + 217; //标题行颜色

            //定义数据集的各个字段
            IGRRecordset RecordSet = Report.DetailGrid.Recordset;
            //RecordSet.AddField("序号", GRFieldType.grftString);
            RecordSet.AddField("卡片编号", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("卡片名称", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("仓库", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("类型", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("期初数量", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("收入数量", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("发出数量", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("结存数量", GRFieldType.grftString).Format = "#,##0.00";
            RecordSet.AddField("结存成本", GRFieldType.grftString).Format = "#,##0.00";



            Report.DetailGrid.AddColumn("序号", "序号","序号", 1.0).ContentCell.TextAlign = GRTextAlign.grtaMiddleCenter;

            Report.DetailGrid.AddColumn("卡片编号", "卡片编号", "卡片编号", 2.3).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("卡片名称", "卡片名称", "卡片名称", 5.4).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("仓库", "仓库", "仓库", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("类型", "类型", "类型", 0.9).ContentCell.TextAlign = GRTextAlign.grtaMiddleLeft;
            Report.DetailGrid.AddColumn("期初数量", "期初数量", "期初数量", 2.2).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
            Report.DetailGrid.AddColumn("收入数量", "收入数量", "收入数量", 2.0).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
            Report.DetailGrid.AddColumn("发出数量", "发出数量", "发出数量", 1.8).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
            Report.DetailGrid.AddColumn("结存数量", "结存数量", "结存数量", 2.38).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;
            Report.DetailGrid.AddColumn("结存成本", "结存成本", "结存成本", 2.38).ContentCell.TextAlign = GRTextAlign.grtaMiddleRight;

            //定义列(定义明细网格) 后部分为明细显示的位置方法
                      
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
            Group.Footer.Font.Bold = true;

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
            SummaryBox.DataField = "期初数量";
            SummaryBox.AlignColumn = "期初数量"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;

            SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0";
            SummaryBox.DataField = "收入数量";
            SummaryBox.AlignColumn = "收入数量"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;

            SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0";
            SummaryBox.DataField = "发出数量";
            SummaryBox.AlignColumn = "发出数量"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;

            SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0";
            SummaryBox.DataField = "结存数量";
            SummaryBox.AlignColumn = "结存数量"; //通过对齐到列确定部件框的左边位置与宽度
            SummaryBox.TextAlign = GRTextAlign.grtaMiddleRight;
            SummaryBox.Top = 0.19;
            SummaryBox.Height = 0.40;

            SummaryBox = Group.Footer.Controls.Add(GRControlType.grctSummaryBox).AsSummaryBox;
            SummaryBox.SummaryFun = GRSummaryFun.grsfSum;
            SummaryBox.Format = "#,##0.00";
            SummaryBox.DataField = "结存成本";
            SummaryBox.AlignColumn = "结存成本"; //通过对齐到列确定部件框的左边位置与宽度
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
    }
}
