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
using Warehouse.StockReport;
using Warehouse.Base;

namespace Warehouse.Bargain
{
    public partial class RecordCopySettle : Form
    {
        public string curUser = "";  //制单人
        public string Bdid="";     //绑定编号
        public string BarSysId="";    //合同系统编号
        public string Mtype="";      //机型
        public string Manufactcode="";   //机号

        private string JSLX="";  //结算类型：单机、捆绑
        private DateTime JsFrom;  //
        private DateTime JsTo;

        private string Where_AllBargIds="";   //所有相关的合同号

        private double totmoney = 0;  //结算后的总金额
        
        public RecordCopySettle()
        {
            InitializeComponent();
        }

        private void ppa_PrePayAddChange_()
        {
            show_prepay();
        }

        //抄张结算是否已经开票
        private bool Is_KaiPiao_JieSuan(string BargOrBd_,string czfrom, string czto)
        {
            string sql_ = "select Sysid from T_MacSettle"
                + " where BargOrBd_id='" + BargOrBd_ + "'"
                //+ " and Hctype='" + hctype + "'"
                + " and CzFrom='" + czfrom + "'"
                + " and CzTo='" + czto + "'"
                + " and iskp=1";
            return (new DBUtil()).yn_exist_data(sql_);
        }
        
        //预收是否已经开票
        private bool Is_KaiPiao_PrePay(string BargOrBd_,string sysid_)
        {
            string sql_ = "select Sysid from T_MacSettlePrePay"
                + " where BargOrBd_id='" + BargOrBd_ + "'"
                + " and lx='预收'"
                + " and sysid="+sysid_
                +" and iskp=1";
                
            return (new DBUtil()).yn_exist_data(sql_);
        }


        private void show_prepay()
        { 
           string sid="";
            if (this.JSLX=="单机")
                sid=this.BarSysId;
            else 
                sid=this.Bdid;
            string sql_ = "select T_MacSettlePrePay.sysid,"
                + "T_MacSettlePrePay.settlesysid,"
                + "T_MacSettlePrePay.OccurDay as 日期,"
                + "T_MacSettlePrePay.PrePay as 预收金额,"
                + "T_MacSettle.Feefrom as 费用期间起日,"
                + "T_MacSettle.Feeto as 费用期间止日,"
                + "T_MacSettlePrePay.Paid as 抵扣金额,"
                + "T_MacSettlePrePay.iskp as 是否开票,"
                + "T_MacSettlePrePay.memo as 备注 "
                + " from T_MacSettlePrePay "
                + " left join T_MacSettle on T_MacSettlePrePay.SettleSysid=T_MacSettle.sysid "
                + " where T_MacSettlePrePay.BargOrBd_Id='" + sid + "'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_prepay.DataSource = dt.DefaultView;
            this.dgv_prepay.Columns["sysid"].Visible = false;
            this.dgv_prepay.Columns["settlesysid"].Visible = false;

            this.dgv_prepay.Columns["日期"].Width= 70;
            this.dgv_prepay.Columns["预收金额"].Width = 90;

            //显示预收费余额
            double allprepay_ = 0, allpay_ = 0, moneyye_=0;
            sql_ = "select Sum(PrePay) from T_MacSettlePrePay "
            + " where T_MacSettlePrePay.BargOrBd_Id='" + sid + "'";
            string ye = (new DBUtil()).Get_Single_val(sql_);
            if (ye != "")
                allprepay_ = Convert.ToDouble(ye);
            else
                allprepay_ = 0;

            sql_ = "select Sum(Paid) from T_MacSettlePrePay "
            + " where T_MacSettlePrePay.BargOrBd_Id='" + sid + "'";
            ye = (new DBUtil()).Get_Single_val(sql_);
            if (ye != "")
                allpay_ = Convert.ToDouble(ye);
            else
                allpay_ = 0;

            moneyye_ = allprepay_ - allpay_;  //计算的余额

            this.lb_prePay.Text = "预收费余额：" + moneyye_.ToString().Trim();
          
        }
        private void RecordCopySettle_Load(object sender, EventArgs e)
        {
            this.JSLX = "";
            this.dgv_czLst.AllowUserToAddRows = false;
            this.dgv_fee.AllowUserToAddRows = false;
            this.dgv_barg.AllowUserToAddRows = false;
            this.dgv_prepay.AllowUserToAddRows = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string strSql = "select T_bargains.sysid as 合同系统编号,"
                                +"T_bargains.bargid as 合同号,"
                                + "T_bargains.custcode as 客户编码,"
                                + "T_customerInf.CustName as 客户名称,"
                                + "T_bargains.mtype as 机型,"
                                + "T_bargains.Manufactcode as 机号,"
                                + "T_bargains.StartDate as 起始日期,"
                                + "T_bargains.EndDate as 终止日期 "
                                + " from T_bargains "
                                + "left join T_customerInf on T_bargains.custcode=T_customerInf.custid"
                                + " where (T_bargains.Bargstatus='有效') and("
                                +" T_bargains.custcode like '%{0}%' "
                                + "or T_customerInf.CustName like '%{1}%' "
                                + "or T_customerInf.PinYinCode like '%{2}%' "
                                + "or T_bargains.bargid like '%{3}%' "
                                + "or T_bargains.mtype like '%{4}%' "
                                + "or T_bargains.Manufactcode like '%{5}%')";

                strSql = string.Format(strSql, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);

                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                fr.unVisible_Column_index_ = 0;
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    string barsysid_ = fr.dr_.Cells["合同系统编号"].Value.ToString().Trim();
                    string mt_ = fr.dr_.Cells["机型"].Value.ToString().Trim();
                    string mc_=fr.dr_.Cells["机号"].Value.ToString().Trim();

                    //重置变量
                    this.dgv_fee.DataSource = null;
                    this.lb_tot.Text = "";
                    this.BarSysId = "";
                    this.Bdid = "";

                    //判断是否是捆绑中的合同编号
                    this.Bdid = get_bdid(barsysid_);
                    this.BarSysId = barsysid_;
                    if (this.Bdid != "")
                    {
                        this.JSLX = "捆绑";
                        string sql1_ = "select Manufactcode from T_BargBindMacs where Bdid='" + this.Bdid + "'";
                        string ms = "";
                        DataTable dt1 = (new SqlDBConnect()).Get_Dt(sql1_);
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (ms == "")
                                ms = dt1.Rows[i]["Manufactcode"].ToString().Trim();
                            else
                                ms = ms + "," + dt1.Rows[i]["Manufactcode"].ToString().Trim();
                        }
                        this.tb_id.Text = this.Bdid;
                        this.tb_ms.Text = ms;
                        this.lb_lx.Text = "（多机捆绑结算）";
                        this.label2.Text = "结算绑定号";
                        produce_cz_lst(this.JSLX, this.Bdid);
                    }
                    else
                    {
                        this.JSLX = "单机";
                        this.lb_lx.Text = "（单机结算）";
                        this.tb_id.Text = this.BarSysId;
                        this.tb_ms.Text = mc_;
                        this.label2.Text = "结算合同号";
                        produce_cz_lst(this.JSLX, this.BarSysId);
                    }
                    //判断抄张是否完成
                    //显示预收费
                    show_prepay();
                }
            }
        }

        private void produce_cz_lst(string jslx,string sid)
        {
            string sql_="";
            if (jslx == "单机")
            {
                sql_ = "select Distinct czfrom as 抄张周期起日,czto as 抄张周期止日 from t_recordcopy"
                  + " where barsysid=" + sid ;
            }
            else
            {
                sql_ = "select Distinct czfrom as 抄张周期起日,czto as 抄张周期止日 from t_recordcopy"
                    + " where barsysid in "
                    + "(select barsysid from T_BargBindMacs where Bdid='" + sid + "')";
            }
            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_czLst.DataSource = dt.DefaultView;
            
        }


        
        private string get_bdid(string barid_)
        {
            string sql_ = "select T_BargBind.Bdid from T_BargBindMacs,T_BargBind"
                + " where T_BargBindMacs.bdid=T_BargBind.bdid"
                + " and T_BargBindMacs.barsysid=" + barid_
                + " and T_BargBind.bdstatus='有效'";
            string bdid = (new DBUtil()).Get_Single_val(sql_);
            return bdid;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //单台机器结算：合同编号，第hsxh个核算周期中的第czxh次抄张
        private void RecordCopyJieSuan(string lx,string sid,DateTime JsF,DateTime JsT)
        {
            string sql_ = "";
            //如果已经开票,则不允许再结算
            sql_ = "select rid from T_SettleAccountMain where BarOrBd_id like '" 
                 + sid + "' and czfrom='"+this.JsFrom.ToString("yyyy-MM-dd").Trim()
                 +"' and czto='"+this.JsTo.ToString("yyyy-MM-dd").Trim()+"'";
            if ((new DBUtil()).yn_exist_data(sql_))
            {
                MessageBox.Show("已经结算!");
                return;
            }
            //===========================
            List<string> SqlLst = new List<string>();
            //1)清除之前计费
            sql_ = "delete from T_MacSettle "
                + "where BargOrBd_Id='" + sid
                + "' and czfrom='" + JsF.ToString("yyyy-MM-dd").Trim()+"'"
                + " and czto='" + JsT.ToString("yyyy-MM-dd").Trim()+"'";
            SqlLst.Add(sql_);

            
            //2)获取该合同抄张对应的幅面
            if (lx == "单机")
                sql_ = "select HcType from T_BargFee where barsysid=" + sid ;
            else
                sql_ = " select Hctype from T_BargBindSet where Bdid='" + sid + "'";
            
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            string shctype = "";
            BargFee bf ;
            string sql1_ = "", sql2_ = "", sql3_ = "", sql4_ = "", sql5_ = "";
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                shctype = dt.Rows[k]["Hctype"].ToString().Trim();
                bf = new BargFee(lx, sid, shctype,JsF,JsT);
                
                //计算预收费
                if (k == 0)  //预收费不分幅面
                {
                    sql_ = bf.Get_Insert_SQL_PayBefore();
                    if (sql_.Trim() != "") SqlLst.Add(sql_);
                    
                }

                //计算基本费用
                bf.Get_Insert_SQL(ref sql1_, ref sql2_, ref sql3_, ref sql4_, ref sql5_);
                if (sql1_.Trim() != "") SqlLst.Add(sql1_);
                if (sql2_.Trim() != "") SqlLst.Add(sql2_);
                if (sql3_.Trim() != "") SqlLst.Add(sql3_);
                if (sql4_.Trim() != "") SqlLst.Add(sql4_);
                if (sql5_.Trim() != "") SqlLst.Add(sql5_);
                
            }

            try
            {
                (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("不能完成抄张费用计算，请检查合同设置！");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        //是否可以结算
        private bool Can_JieSuan(string jslx, string sid,DateTime JsF,DateTime JsT)
        {
            DataTable dt;
            DataTable dt1;
            string sql_ = "",sql2_="";
            string shctype = "";
            string bargid_;
            if (jslx == "单机")
            {
                //该机的所有幅面
                sql_ = "select HcType from T_BargFee where barsysid=" + sid.Trim() ;
                dt = (new SqlDBConnect()).Get_Dt(sql_);
                for (int k = 0; k < dt.Rows.Count; k++)
                {   
                    shctype = dt.Rows[k]["hctype"].ToString().Trim();
                    if (Is_Record_Copy(sid, shctype, JsF, JsT) == false)
                        return false;
                }
            }
            else
            {
                //该捆绑设定的所有幅面
                sql_ = "select HcType from T_BargBindSet where bdid='" + sid.Trim() + "'";
                dt = (new SqlDBConnect()).Get_Dt(sql_);
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    sql2_ = "select barsysid from T_BargBindMacs where Bdid='" + sid.Trim() + "'";
                    dt1 = (new SqlDBConnect()).Get_Dt(sql2_);
                    for (int m = 0; m < dt1.Rows.Count; m++)
                    {
                        bargid_ = dt1.Rows[m]["barsysid"].ToString().Trim();
                        shctype = dt.Rows[k]["hctype"].ToString().Trim();
                        if (Is_Record_Copy(bargid_, shctype, JsF, JsT) == false)
                            return false;
                    }
                }
            }
            return true;
        }

        private bool Is_Record_Copy(string barid, string hctype, DateTime JsF, DateTime JsT)
        {
            string sql_ = "";
            sql_ = "select curNum from T_RecordCopy where barsysid=" + barid
                + " and hctype='" + hctype + "'"
                + " and czFrom='" + JsF.ToString("yyyy-MM-dd").Trim()+"'"
                + " and czTo='" + JsT.ToString("yyyy-MM-dd").Trim()+"'";
            return (new DBUtil()).yn_exist_data(sql_);
        }

        private void JieSuan()
        {
            if ((this.BarSysId.Trim() == "") && (this.Bdid.Trim() == "")) return;
            string sdjh = "";
            if (this.JSLX == "单机")
            {
                sdjh = this.BarSysId;
                if (Can_JieSuan(this.JSLX, this.BarSysId, this.JsFrom, this.JsTo))
                    RecordCopyJieSuan(this.JSLX, this.BarSysId, this.JsFrom, this.JsTo);
                else
                {
                    MessageBox.Show("没有完整的抄张记录！无法结算");
                    return;
                }
            }
            else
            {
                sdjh = this.Bdid;
                if (Can_JieSuan(this.JSLX, this.Bdid, this.JsFrom, this.JsTo))
                    RecordCopyJieSuan(this.JSLX, this.Bdid, this.JsFrom, this.JsTo);
                else
                {
                    MessageBox.Show("没有完整的抄张记录！无法结算");
                    return;
                }
            }

            DataTable dt;
            string sql_ = "select " //sysid as 结算编号,"
                +"BargOrBd_id as 合同或捆绑号,"
                +"Hctype as 幅面,FeeFrom as 开始日期,"
                + "Feeto as 结束日期,MoneyType as 费用类别,Fee as 单价,"
                + "Num as 数量,TotMoney as 金额 from T_MacSettle "
                + " where BargOrBd_id='" + sdjh + "'"
                + " and czFrom='" + this.JsFrom.ToString("yyyy-MM-dd").Trim() + "'"
                + " and czTo='" + this.JsTo.ToString("yyyy-MM-dd").Trim() + "'";
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_fee.DataSource = dt.DefaultView;

            //计算显示合计数
            sql_ = "select sum(TotMoney) as tot from T_MacSettle"
                + " where BargOrBd_id='" + sdjh + "'"
                + " and czFrom='" + this.JsFrom.ToString("yyyy-MM-dd").Trim() + "'"
                + " and czTo='" + this.JsTo.ToString("yyyy-MM-dd").Trim() + "'";
            if ((new DBUtil()).Get_Single_val(sql_).ToString().Trim() != "")
            {
                this.totmoney = Convert.ToDouble((new DBUtil()).Get_Single_val(sql_).ToString().Trim());
                this.lb_tot.Text = "合计金额：" + totmoney.ToString().Trim();
            }

        }


        //根据Bargsysi或BDid得到单位名称
        private string Get_CustName(string BarOrBid)
        {
            string sql_ = "";
            string barsysid_ = "";
            if (BarOrBid.Substring(0, 2) == "KB")
            {
                sql_ = "select BarSysid from T_BargBindMacs where Bdid like '" + BarOrBid + "'";
                barsysid_ = (new DBUtil()).Get_Single_val(sql_);
            }
            else
                barsysid_ = BarOrBid;
            sql_ = "select T_customerInf.CustName from "
                + "T_customerInf,T_Bargains "
                + " where T_customerInf.Custid=T_bargains.CustCode "
                + " and T_bargains.sysid=" + barsysid_;
            return (new DBUtil()).Get_Single_val(sql_);
        }

        
        private void dgv_czLst_DoubleClick(object sender, EventArgs e)
        {
            string sid = "";
            if (this.JSLX == "单机")
            {
                sid = this.BarSysId;
            }
            else
            {
                sid = this.Bdid;
            }
            if (this.dgv_czLst.SelectedRows.Count <= 0) return;
            if (this.dgv_czLst.SelectedRows[0].Cells["抄张周期起日"].Value == null) return;
            if (this.dgv_czLst.SelectedRows[0].Cells["抄张周期止日"].Value == null) return;

            DateTime df = Convert.ToDateTime
               (this.dgv_czLst.SelectedRows[0].Cells["抄张周期起日"].Value.ToString().Trim());
            DateTime dtt = Convert.ToDateTime
                (this.dgv_czLst.SelectedRows[0].Cells["抄张周期止日"].Value.ToString().Trim());
            this.JsFrom = df;
            this.JsTo = dtt;

            this.lb_tot.Text = "";
            this.totmoney = 0;

            DataTable dt;
            string sql_ = "select "  //sysid as 结算编号,"
                +"BargOrBd_id as 合同或捆绑号,"
                +"Hctype as 幅面,FeeFrom as 费用起始日期,"
                + "Feeto as 费用终止日期,MoneyType as 费用类别,Fee as 单价,"
                + "Num as 数量,TotMoney as 金额 from T_MacSettle "
                + " where BargOrBd_id='" + sid + "'"
                + " and czFrom='" + this.JsFrom.ToString("yyyy-MM-dd").Trim() + "'"
                + " and czTo='" + this.JsTo.ToString("yyyy-MM-dd").Trim() + "'";
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_fee.DataSource = dt.DefaultView;

            //计算显示合计数
            sql_ = "select sum(TotMoney) as tot from T_MacSettle"
                + " where BargOrBd_id='" + sid + "'"
                + " and czFrom='" + this.JsFrom.ToString("yyyy-MM-dd").Trim() + "'"
                + " and czTo='" + this.JsTo.ToString("yyyy-MM-dd").Trim() + "'";
            if ((new DBUtil()).Get_Single_val(sql_).ToString().Trim() != "")
            {
                this.totmoney = Convert.ToDouble((new DBUtil()).Get_Single_val(sql_).ToString().Trim());
                this.lb_tot.Text = "合计金额：" + totmoney.ToString().Trim();
            }


                
        }

        private void show_all_unKaiPiao()
        {
            //单机
            string sql_ = "select BargOrBd_id as 合同或捆绑号,"
                + "czfrom as 抄张周期起日,"
                + "czto as 抄张周期止日,"
                + "sum(totmoney)-sum(KpMoney) as 未开票金额 "
                + " from T_MacSettle where ((iskp=0) or (iskp is null))"
                +" and abs(totmoney-KpMoney)>0 "
                + " group by BargOrBd_id,czfrom,czto "
                + " order by BargOrBd_id";

            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_barg.DataSource = dt.DefaultView;
            //金额设置成为2位小数
            this.dgv_barg.Columns[3].DefaultCellStyle.Format = "F2";
            //带货币符号
            toolStripStatusLabel2.Text = "记录数：" + dt.Rows.Count.ToString().Trim();
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            show_all_unKaiPiao();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.dgv_czLst.SelectedRows.Count <= 0) return;
            if (this.dgv_czLst.SelectedRows[0].Cells["抄张周期起日"].Value == null) return;
            if (this.dgv_czLst.SelectedRows[0].Cells["抄张周期止日"].Value == null) return;


            DateTime Jf = Convert.ToDateTime
                (this.dgv_czLst.SelectedRows[0].Cells["抄张周期起日"].Value.ToString().Trim());
            DateTime Jt = Convert.ToDateTime
                (this.dgv_czLst.SelectedRows[0].Cells["抄张周期止日"].Value.ToString().Trim());
            string BargId = "";
            if (this.JSLX == "单机")
            {
                BargId = this.BarSysId;
                string Strsql = "select * from T_Bargains where sysid=" + BargId;
                if (!(new DBUtil()).yn_exist_data(Strsql))
                {
                    MessageBox.Show("该合同记录不存在！", "提示");
                    return;
                }
            }
            else
            {
                BargId = this.Bdid;
                string Strsql = "select * from T_BargBindMacs where bdid='" + BargId + "'";
                if (!(new DBUtil()).yn_exist_data(Strsql))
                {
                    MessageBox.Show("该绑定合同记录不存在！", "提示");
                    return;
                }
            }
            //计算显示合计数
            string sql = "select sum(TotMoney) as tot from T_MacSettle"
                + " where BargOrBd_id='" + BargId + "'"
                + " and czFrom='" + Jf.ToString("yyyy-MM-dd").Trim() + "'"
                + " and czTo='" + Jt.ToString("yyyy-MM-dd").Trim() + "'";
            double totm = 0.0;
            DataTable dt = new DataTable();
            dt = (new SqlDBConnect()).Get_Dt(sql);
            if (dt.Rows[0][0] != DBNull.Value)
                totm = Convert.ToDouble((new DBUtil()).Get_Single_val(sql).ToString().Trim());
            RecordPreview Rview = new RecordPreview();
            Rview.JSLX = this.JSLX;
            Rview.BarOrBd_id = BargId;
            Rview.czfrom = Jf ;
            Rview.czto = Jt ;
            Rview.totmoney = totm;
            Rview.TableInsert ();
            //报表查看器
            PreviewForm theForm = new PreviewForm();
            theForm.AttachReport(Rview.Report);
            theForm.ShowDialog();
        }

        private void dgv_barg_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgv_barg.SelectedRows.Count <= 0) return;
            if (this.dgv_barg.SelectedRows[0].Cells[0].Value == null) return;

            string barid = this.dgv_barg.SelectedRows[0].Cells[0].Value.ToString().Trim();
            string sfrom = this.dgv_barg.SelectedRows[0].Cells[1].Value.ToString().Trim();
            string sto = this.dgv_barg.SelectedRows[0].Cells[2].Value.ToString().Trim();

            if (barid == "") return;
            DateTime df, dt;
            if (barid.Substring(0, 2) == "KB")
            {
                this.Bdid = barid;
                this.BarSysId = "";
                string sql_ = "select Manufactcode from T_BargBindMacs where Bdid='" + this.Bdid + "'";
                string ms = "";
                DataTable dt1 = (new SqlDBConnect()).Get_Dt(sql_);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (ms == "")
                        ms = dt1.Rows[i]["Manufactcode"].ToString().Trim();
                    else
                        ms = ms + "," + dt1.Rows[i]["Manufactcode"].ToString().Trim();
                }
                this.tabControl1.SelectedIndex = 1;
                this.JSLX = "捆绑";
                this.tb_id.Text = this.Bdid;
                this.tb_ms.Text = ms;
                this.lb_lx.Text = "（多机捆绑结算）";
                this.label2.Text = "结算绑定号";
                produce_cz_lst(this.JSLX, this.Bdid);
                
            }
            else
            {

                this.BarSysId = barid;
                this.Bdid = "";
                this.tabControl1.SelectedIndex = 1;
                this.JSLX = "单机";
                this.lb_lx.Text = "（单机结算）";
                this.tb_id.Text = barid;
                //this.tb_ms.Text = mc;
                this.label2.Text = "结算合同号";
                produce_cz_lst(this.JSLX, this.BarSysId);
                
                
            }
            for (int m = 0; m < this.dgv_czLst.Rows.Count; m++)
            {
                df = Convert.ToDateTime(this.dgv_czLst.Rows[m].Cells["抄张周期起日"].Value.ToString().Trim());
                dt = Convert.ToDateTime(this.dgv_czLst.Rows[m].Cells["抄张周期止日"].Value.ToString().Trim());

                if ((Convert.ToDateTime(sfrom) == df)
                    && (Convert.ToDateTime(sto) == dt))
                {
                    this.dgv_czLst.Rows[m].Selected = true;
                    this.dgv_czLst_DoubleClick(null, null);
                    break;
                }
            }
            //显示预收费
            show_prepay();
               
        }

        //private void dgv_barg_DoubleClick(object sender, EventArgs e)
        //{
        //    if (this.dgv_barg.SelectedRows.Count <= 0) return;
        //    if (this.dgv_barg.SelectedRows[0].Cells[0].Value == null) return;

        //    string barid = this.dgv_barg.SelectedRows[0].Cells[0].Value.ToString().Trim();
        //    string mc = this.dgv_barg.SelectedRows[0].Cells[2].Value.ToString().Trim();
        //    this.BarSysId = barid;
        //    this.Bdid = "";
        //    this.tabControl1.SelectedIndex = 1;
        //    this.JSLX = "单机";
        //    this.lb_lx.Text = "（单机结算）";
        //    this.tb_id.Text = barid;
        //    this.tb_ms.Text = mc;
        //    this.label2.Text = "结算合同号";
        //    produce_cz_lst(this.JSLX, this.BarSysId);
        //}

       
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (this.dgv_czLst.SelectedRows.Count <= 0) return;
            if (this.dgv_czLst.SelectedRows[0].Cells["抄张周期起日"].Value == null) return;
            if (this.dgv_czLst.SelectedRows[0].Cells["抄张周期止日"].Value == null) return;

            if (MessageBox.Show("确认重新结算吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.dgv_fee.DataSource = null;
                this.lb_tot.Text = "";
                this.totmoney = 0;

                DateTime df = Convert.ToDateTime
                    (this.dgv_czLst.SelectedRows[0].Cells["抄张周期起日"].Value.ToString().Trim());
                DateTime dt = Convert.ToDateTime
                    (this.dgv_czLst.SelectedRows[0].Cells["抄张周期止日"].Value.ToString().Trim());
                this.JsFrom = df;
                this.JsTo = dt;

                //已经开票的，不允许结算
                string sid = "";
                if (this.JSLX == "") return;
                if (this.JSLX == "单机")
                {
                    sid = this.BarSysId;
                }
                else
                {
                    sid = this.Bdid;
                }
                if (Is_KaiPiao_JieSuan(sid,JsFrom.ToString("yyyy-MM-dd"),JsTo.ToString("yyyy-MM-dd")))
                {
                    MessageBox.Show("已开票，不允许再结算");
                    return;
                }
                //
                //

                JieSuan();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.JSLX == "") return;
            if (MessageBox.Show("确认进行首笔预收费结算吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                string sql_ = "";
                string sid = "", fee_ = "";
                DateTime czfrom, ysto;
                string sczfrom, systo;
                int ysmonth = 0;  //几月预收一次
                DataTable dt;
                if (this.JSLX == "单机")
                {
                    sid = this.BarSysId;
                    sql_ = "select forfee,FirstCzDate,ForeadNum from T_Bargains"
                     + " where sysid=" + this.BarSysId;
                    dt = (new SqlDBConnect()).Get_Dt(sql_);
                    if (dt.Rows.Count <= 0) return;
                    fee_ = dt.Rows[0]["forfee"].ToString().Trim();
                    //首次抄张日的计算
                    czfrom = Convert.ToDateTime(dt.Rows[0]["FirstCzDate"].ToString().Trim());
                    if (dt.Rows[0]["ForeadNum"].ToString().Trim()!="")
                       ysmonth = Convert.ToInt16(dt.Rows[0]["ForeadNum"].ToString().Trim());
                    else
                    {
                        MessageBox.Show("该合同无首次预付费!");
                        return;
                    }
                    ysto = czfrom.AddMonths(ysmonth).AddDays(-1);
                    //
                    sczfrom = czfrom.ToString("yyyy-MM-dd");
                    //
                    systo = ysto.ToString("yyyy-MM-dd");
                }
                else
                {
                    sid = this.Bdid;
                    sql_ = "select BaseFee from T_BargBind where Bdid='" + this.Bdid + "'";
                    fee_ = (new DBUtil()).Get_Single_val(sql_);
                    
                    sql_ = "select barsysid from T_BargBindMacs where Bdid='" + this.Bdid + "'";
                    string barid_ = (new DBUtil()).Get_Single_val(sql_);
                    //
                    sql_ = "select FirstCzDate,ForeadNum from T_Bargains"
                     + " where sysid=" + barid_;
                    dt = (new SqlDBConnect()).Get_Dt(sql_);
                    if (dt.Rows.Count <= 0) return;
                    czfrom = Convert.ToDateTime(dt.Rows[0]["FirstCzDate"].ToString().Trim());
                    
                    if (dt.Rows[0]["ForeadNum"].ToString().Trim() != "")
                        ysmonth = Convert.ToInt16(dt.Rows[0]["ForeadNum"].ToString().Trim());
                    else
                    {
                        MessageBox.Show("该捆绑无首次预付费!");
                        return;
                    }
                    
                    ysto = czfrom.AddMonths(ysmonth).AddDays(-1);

                    sczfrom = czfrom.ToString("yyyy-MM-dd");
                    systo = ysto.ToString("yyyy-MM-dd");
                }

                this.JsFrom = czfrom;  //首次结算(Czfrom=czto);
                this.JsTo = czfrom;
                //首次结算(Czfrom=czto);
               
                //已经开票的，不允许结算
                if (Is_KaiPiao_JieSuan(sid,this.JsFrom.ToString("yyyy-MM-dd"),this.JsTo.ToString("yyyy-MM-dd")))
                {
                    MessageBox.Show("已开票，不允许删除");
                    return;
                }
                //
                {
                    //===========================

                    List<string> SqlLst = new List<string>();
                    //删除已结算的结果
                    sql_ = "delete from T_MacSettle where lx='{0}' and BargOrBd_id='{1}'"
                        + " and czfrom='{2}' and czto='{3}'";
                    sql_ = string.Format(sql_, this.JSLX, sid, sczfrom, sczfrom);
                    SqlLst.Add(sql_);
                    //新的结算结果
                    sql_ = "insert into T_MacSettle(lx,BargOrBd_id,Feefrom,feeto,"
                        + "MoneyType,TotMoney,memo,czfrom,czto) values('{0}','{1}','{2}','{3}',"
                        + "'{4}',{5},'{6}','{7}','{8}')";
                    sql_ = string.Format(sql_, this.JSLX, sid, sczfrom, systo,
                        "预收金额", fee_, "周期基本金额", sczfrom, sczfrom);
                    SqlLst.Add(sql_);

                    try
                    {
                        (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                    }
                    catch
                    {
                        MessageBox.Show("结算首次费用失败!");
                    }
                }
                //显示收费
                DataTable dt1;
                string sql1_ = "select BargOrBd_id as 单据号,Hctype as 幅面,FeeFrom as 费用起始日期,"
                    + "Feeto as 费用终止日期,MoneyType as 费用类别,Fee as 单价,"
                    + "Num as 数量,TotMoney as 金额 from T_MacSettle "
                    + " where BargOrBd_id='" + sid + "'"
                    + " and czFrom='" + sczfrom + "'"
                    + " and czTo='" + sczfrom + "'";
                dt1 = (new SqlDBConnect()).Get_Dt(sql1_);
                this.dgv_fee.DataSource = dt1.DefaultView;

                //计算显示合计数
                sql1_ = "select sum(TotMoney) as tot from T_MacSettle"
                    + " where BargOrBd_id='" + sid + "'"
                    + " and czFrom='" + sczfrom + "'"
                    + " and czTo='" + sczfrom + "'";
                this.totmoney = Convert.ToDouble((new DBUtil()).Get_Single_val(sql1_).ToString().Trim());
                this.lb_tot.Text = "合计金额：" + totmoney.ToString().Trim();
            
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.JSLX == "") return;
            this.lb_tot.Text = "";
            this.totmoney = 0;
            string sql_ = "";
            string sid = "", fee_ = "";
            DateTime czfrom, ysto;
            string sczfrom, systo;
            int ysmonth = 0;  //几月预收一次
            DataTable dt;
            if (this.JSLX == "单机")
            {
                sid = this.BarSysId;
                sql_ = "select forfee,FirstCzDate,ForeadNum from T_Bargains"
                 + " where sysid=" + this.BarSysId;
                dt = (new SqlDBConnect()).Get_Dt(sql_);
                if (dt.Rows.Count <= 0) return;
                fee_ = dt.Rows[0]["forfee"].ToString().Trim();
                //首次抄张日的计算
                czfrom = Convert.ToDateTime(dt.Rows[0]["FirstCzDate"].ToString().Trim());
                if (dt.Rows[0]["ForeadNum"].ToString().Trim() != "")
                    ysmonth = Convert.ToInt16(dt.Rows[0]["ForeadNum"].ToString().Trim());
                else
                  {
                      MessageBox.Show("该合同无预付费！");
                      return;
                  }
                ysto = czfrom.AddMonths(ysmonth).AddDays(-1);
                //
                sczfrom = czfrom.ToString("yyyy-MM-dd");
                //
                systo = ysto.ToString("yyyy-MM-dd");
            }
            else
            {
                sid = this.Bdid;
                sql_ = "select BaseFee from T_BargBind where Bdid='" + this.Bdid + "'";
                fee_ = (new DBUtil()).Get_Single_val(sql_);

                sql_ = "select barsysid from T_BargBindMacs where Bdid='" + this.Bdid + "'";
                string barid_ = (new DBUtil()).Get_Single_val(sql_);  //捆绑对应的第1个合同
                //
                sql_ = "select FirstCzDate,ForeadNum from T_Bargains"
                 + " where sysid=" + barid_;
                dt = (new SqlDBConnect()).Get_Dt(sql_);
                if (dt.Rows.Count <= 0) return;
                
                if (dt.Rows[0]["ForeadNum"].ToString().Trim() != "")
                    ysmonth = Convert.ToInt16(dt.Rows[0]["ForeadNum"].ToString().Trim());
                else
                {
                    MessageBox.Show("该捆绑无预付费！");
                    return;
                }

                czfrom = Convert.ToDateTime(dt.Rows[0]["FirstCzDate"].ToString().Trim());
                ysto = czfrom.AddMonths(ysmonth).AddDays(-1);

                sczfrom = czfrom.ToString("yyyy-MM-dd");
                systo = ysto.ToString("yyyy-MM-dd");
            }

            this.JsFrom = czfrom;  //首次结算(Czfrom=czto);
            this.JsTo = czfrom;
            //显示收费
            DataTable dt1;
            string sql1_ = "select BargOrBd_id as 单据号,Hctype as 幅面,FeeFrom as 费用起始日期,"
                + "Feeto as 费用终止日期,MoneyType as 费用类别,Fee as 单价,"
                + "Num as 数量,TotMoney as 金额 from T_MacSettle "
                + " where BargOrBd_id='" + sid + "'"
                + " and czFrom='" + sczfrom + "'"
                + " and czTo='" + sczfrom + "'";
            dt1 = (new SqlDBConnect()).Get_Dt(sql1_);
            this.dgv_fee.DataSource = dt1.DefaultView;

            //计算显示合计数
           
                sql1_ = "select sum(TotMoney) as tot from T_MacSettle"
                    + " where BargOrBd_id='" + sid + "'"
                    + " and czFrom='" + sczfrom + "'"
                    + " and czTo='" + sczfrom + "'";
                if ((new DBUtil()).Get_Single_val(sql1_).ToString().Trim() != "")
                {
                    this.totmoney = Convert.ToDouble((new DBUtil()).Get_Single_val(sql1_).ToString().Trim());
                    this.lb_tot.Text = "合计金额：" + totmoney.ToString().Trim();
                }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sid="";
            if (this.JSLX == "") return;
            if (this.JSLX == "单机")
            {
                sid = this.BarSysId;
            }
            else
            {
                sid = this.Bdid;
            }
            PrePayAdd ppa = new PrePayAdd();
            ppa.PrePayAddChange_+=new PrePayAdd.PrePayAddChange(ppa_PrePayAddChange_);
            ppa.BargOrBdId = sid;
            ppa.curUser = this.curUser;
            ppa.type = "add";  //新增
            ppa.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dgv_prepay.SelectedRows.Count <= 0) return;
            if (this.dgv_prepay.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_prepay.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            string settlesysid_ = this.dgv_prepay.SelectedRows[0].Cells["settlesysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            if (settlesysid_ != "") return;   //该记录是抵扣产生的


            string sid = "";
            if (this.JSLX == "") return;
            if (this.JSLX == "单机")
            {
                sid = this.BarSysId;
            }
            else
            {
                sid = this.Bdid;
            }

            if (Is_KaiPiao_PrePay(sid, sysid_))
            {
                MessageBox.Show("已开票，不允许修改");
                return;
            }
            
            PrePayAdd ppa = new PrePayAdd();
            ppa.PrePayAddChange_ += new PrePayAdd.PrePayAddChange(ppa_PrePayAddChange_);
            ppa.BargOrBdId = sid;
            ppa.sysid_ = sysid_;
            ppa.curUser = this.curUser;
            ppa.type = "edit";  //新增
            ppa.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql_="";
            if (this.dgv_prepay.SelectedRows.Count <= 0) return;
            if (this.dgv_prepay.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_prepay.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            string settlesysid_ = this.dgv_prepay.SelectedRows[0].Cells["settlesysid"].Value.ToString().Trim();
            
            if (sysid_ == "") return;
            if (settlesysid_ != "") return;   //该记录是抵扣产生的

            //已经开票的预收，不允许删除
            string sid = "";
            if (this.JSLX == "") return;
            if (this.JSLX == "单机")
            {
                sid = this.BarSysId;
            }
            else
            {
                sid = this.Bdid;
            }
            if (Is_KaiPiao_PrePay(sid, sysid_))
            {
                MessageBox.Show("已开票，不允许删除");
                return;
            }
            //

            DialogResult dr = MessageBox.Show("确认删除该笔预收费记录吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                sql_ = "delete from T_MacSettlePrePay where sysid=" + sysid_;
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(sql_);
                    ppa_PrePayAddChange_();
                    MessageBox.Show("删除成功！");
                }
                catch
                {
                    MessageBox.Show("删除失败！");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.JSLX == "") return;
            string sid = "";
            string sql_ = "";
            double moneyye_ = 0, dkmoney_ = 0;  //余额，抵扣金额
            double allprepay_ = 0, allpay_ = 0; 
            if (Math.Abs(this.totmoney)<0.00001) return;

            if (this.JSLX == "单机")
            {
                sid = this.BarSysId;
            }
            else
            {
                sid = this.Bdid;
            }

            //已经开票的，不允许再抵扣
            if (Is_KaiPiao_JieSuan(sid, this.JsFrom.ToString("yyyy-MM-dd"),this.JsTo.ToString("yyyy-MM-dd")))
            {
                MessageBox.Show("已经开过票,不能再抵扣!");
                return;
            }
            //
            if (MessageBox.Show("确认进行预收费抵扣吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.Cancel) return;
                
            //计算预收费余额
            sql_="select Sum(PrePay) from T_MacSettlePrePay "
            +" where T_MacSettlePrePay.BargOrBd_Id='" + sid + "'";
            string ye = (new DBUtil()).Get_Single_val(sql_);
            if (ye != "")
                allprepay_ = Convert.ToDouble(ye);
            else
                allprepay_ = 0;

            sql_ = "select Sum(Paid) from T_MacSettlePrePay "
            + " where T_MacSettlePrePay.BargOrBd_Id='" + sid + "'";
            ye = (new DBUtil()).Get_Single_val(sql_);
            if (ye != "")
                allpay_ = Convert.ToDouble(ye);
            else
                allpay_ = 0;

            moneyye_ = allprepay_ - allpay_;  //计算的余额

            
            //没有余额，则无法抵扣
            if (Math.Abs(moneyye_) < 0.00001) return;   

            //计算可以抵扣的金额
            if (moneyye_ > this.totmoney)
                dkmoney_ = this.totmoney;   //全部抵扣
            else
                dkmoney_ = moneyye_;        //部分抵扣

            bool isexist = false;
            //找到结算明细对应的sysid（可能有多条记录）
            List<string> SqlLst = new List<string>();
            DataTable dt;
            string sysid_="";
            double thismoney_=0,thisDkMoney_=0;
            sql_ = "select sysid,TotMoney from T_MacSettle "
                + " where BargOrBd_id='" + sid + "'"
                + " and czFrom='" + this.JsFrom.ToString("yyyy-MM-dd").Trim() + "'"
                + " and czTo='" + this.JsTo.ToString("yyyy-MM-dd").Trim() + "'";
            dt=(new SqlDBConnect()).Get_Dt(sql_);
            for (int k=0;k<dt.Rows.Count;k++)
            {
               sysid_ =dt.Rows[k]["sysid"].ToString().Trim();
               if (dt.Rows[k]["TotMoney"].ToString().Trim()!="")
                   thismoney_=Convert.ToDouble((dt.Rows[k]["TotMoney"].ToString().Trim()));
               else
                   thismoney_=0;
               
               //计算本次可抵扣金额
                if (dkmoney_>thismoney_)
                {
                   thisDkMoney_=thismoney_;
                }
                else
                {
                   thisDkMoney_=dkmoney_;
                }
                
                //
               if (sysid_ != "")
               {
                //先删除之前抵扣
                sql_ = "delete from T_MacSettlePrePay "
                    + " where BargOrBd_Id='" + sid + "' "
                    + " and SettleSysid=" + sysid_;
                SqlLst.Add(sql_);

                //插入新的抵扣金额
                sql_ = "insert into T_MacSettlePrePay(BargOrBd_Id,SettleSysid,Paid)"
                        + " values('{0}',{1},{2})";
                sql_ = string.Format(sql_, sid, sysid_, thisDkMoney_.ToString().Trim());
                SqlLst.Add(sql_);
                
                //更新已开票金额
                sql_ = "update T_macsettle set kpmoney={0} where sysid={1}";
                sql_=string.Format(sql_,thisDkMoney_.ToString().Trim(),sysid_);
                SqlLst.Add(sql_);

                dkmoney_=dkmoney_-thisDkMoney_;
                if (Math.Abs(dkmoney_)<0.00001)
                    break;
              }
           }
                //已事务的方式执行
                try
                {
                    (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                    MessageBox.Show("抵扣成功");
                    ppa_PrePayAddChange_();
                }
                catch
                { }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            if (this.dgv_prepay.SelectedRows.Count <= 0) return;
            if (this.dgv_prepay.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_prepay.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            string settlesysid_ = this.dgv_prepay.SelectedRows[0].Cells["settlesysid"].Value.ToString().Trim();

            if (sysid_ == "") return;
            if (settlesysid_ == "") return;   //该记录是登记的预收费

            //如果开过票，则不允许移出
            string sid = "";
            if (this.JSLX == "") return;
            if (this.JSLX == "单机")
            {
                sid = this.BarSysId;
            }
            else
            {
                sid = this.Bdid;
            }
            if (Is_KaiPiao_JieSuan(sid, JsFrom.ToString("yyyy-MM-dd"), JsTo.ToString("yyyy-MM-dd")))
            {
                MessageBox.Show("已开票，不允许再删除抵扣!");
                return;
            }
            //

            DialogResult dr = MessageBox.Show("确认删除该笔抵扣记录吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                List<string> SqlLst = new List<string>();
                sql_ = "delete from T_MacSettlePrePay where sysid=" + sysid_;
                SqlLst.Add(sql_);
                sql_ = "update t_macsettle set kpmoney=0 where sysid=" + settlesysid_;
                SqlLst.Add(sql_);
                
                try
                {
                    (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                    ppa_PrePayAddChange_();
                    MessageBox.Show("删除成功！");
                }
                catch
                {
                    MessageBox.Show("删除失败！");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            string sids = "";   //结算表中的sysid，有多个
            string custname = "";
            double tot = 0;
            //选多次结算开票
                if (this.dgv_barg.SelectedRows.Count <= 0) return;

                string BarOrBid = "", Fday = "", Tday = "";
                for (int i = 0; i < this.dgv_barg.SelectedRows.Count; i++)
                {
                    //获取客户名
                    BarOrBid = this.dgv_barg.SelectedRows[i].Cells[0].Value.ToString().Trim();
                    custname = Get_CustName(BarOrBid);
                    //获取序列号
                    Fday = this.dgv_barg.SelectedRows[i].Cells[1].Value.ToString().Trim();
                    Tday = this.dgv_barg.SelectedRows[i].Cells[2].Value.ToString().Trim();
                    Fday = Convert.ToDateTime(Fday).ToString("yyyy-MM-dd");
                    Tday = Convert.ToDateTime(Tday).ToString("yyyy-MM-dd");
                    if (this.dgv_barg.SelectedRows[i].Cells[3].Value.ToString().Trim() != "")
                        tot = tot + Convert.ToDouble(this.dgv_barg.SelectedRows[i].Cells[3].Value.ToString().Trim());

                    sql_ = "select sysid from T_MacSettle where BargOrBd_id='" + BarOrBid + "'"
                        + " and Czfrom='" + Fday + "' and czto='" + Tday + "'";
                    DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
                    for (int m = 0; m < dt.Rows.Count; m++)
                    {
                        sids = dt.Rows[m]["sysid"].ToString().Trim() + ";" + sids;
                    }
                }
                FormCopySettleInvoiceAdd fcs = new FormCopySettleInvoiceAdd();
                fcs.sids = sids;
                fcs.type = "add";
                fcs.lx = "结算开票";
                fcs.totmoney = tot;
                fcs.custName = custname;

                if (fcs.ShowDialog() == DialogResult.OK)
                {
                    show_all_unKaiPiao();
                }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            string sids = "";   //结算表中的sysid，有多个
            string custname = "";
            double tot = 0;
            string swhere = "";

            string Fday = "", Tday = "",sjsid="";
            if (this.JSLX == "") return;
            if (this.JSLX == "单机")
            {
                sjsid = this.BarSysId;
            }
            else
            {
               sjsid = this.Bdid;
            }
                custname = Get_CustName(sjsid);
                Fday = this.JsFrom.ToString("yyyy-MM-dd");
                Tday = this.JsTo.ToString("yyyy-MM-dd");

            ////
                //已经开票的，不允许再开票
                if (Is_KaiPiao_JieSuan(sjsid, Fday,Tday))
                {
                    MessageBox.Show("已经开过票!");
                    return;
                }
                //
                //
            ////
                tot = this.totmoney;
                sql_ = "select sysid from T_MacSettle where BargOrBd_id='" + sjsid + "'"
                    + " and Czfrom='" + Fday + "' and czto='" + Tday + "'";
                DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
                string stmp="";
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    stmp=dt.Rows[m]["sysid"].ToString().Trim();
                    if (swhere == "")
                    {
                        swhere = " (SettleSysid=" + stmp+")";
                    }
                    else
                        swhere = swhere + " or (SettleSysid=" + stmp + ")";
                    sids = stmp + ";" + sids;
                }
            
            //需要扣除已经抵扣的金额
            double thistot = 0;
            sql_ = "select sum(Paid) from T_MacSettlePrePay "
                    + " where BargOrBd_Id='" + sjsid + "'"
                    + " and (" + swhere + ")";
            stmp = (new DBUtil()).Get_Single_val(sql_).Trim();
            if (stmp != "")
                thistot = Convert.ToDouble(stmp);
            //
            FormCopySettleInvoiceAdd fcs = new FormCopySettleInvoiceAdd();
            fcs.sids = sids;
            fcs.type = "add";
            fcs.lx = "结算开票";
            fcs.totmoney = tot - thistot;
            fcs.custName = custname;
            fcs.ShowDialog();
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.dgv_prepay.SelectedRows.Count <= 0) return;
            if (this.dgv_prepay.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dgv_prepay.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            string settlesysid_ = this.dgv_prepay.SelectedRows[0].Cells["settlesysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            if (settlesysid_ != "") return;   //该记录是抵扣产生的

            string sids = "";   //结算表中的sysid，有多个
            string custname = "";
            double tot = 0;
            if (this.dgv_prepay.SelectedRows[0].Cells["预收金额"].Value.ToString().Trim() != "")
                tot = Convert.ToDouble(this.dgv_prepay.SelectedRows[0].Cells["预收金额"].Value.ToString().Trim());
            
            string sjsid = "";

            if (this.JSLX == "") return;
            if (this.JSLX == "单机")
            {
                sjsid = this.BarSysId;
            }
            else
            {
                sjsid = this.Bdid;
            }
            custname = Get_CustName(sjsid);

            ////
            if (Is_KaiPiao_PrePay(sjsid,sysid_))
            {
                MessageBox.Show("已经开过票!");
                return;
            }
            ////

            sids = sysid_ + ";";

            FormCopySettleInvoiceAdd fcs = new FormCopySettleInvoiceAdd();
            fcs.FormCopySettleInvoiceChange_ += new FormCopySettleInvoiceAdd.FormCopySettleInvoiceChange(ppa_PrePayAddChange_);
            
            fcs.sids = sids;
            fcs.type = "add";
            fcs.lx = "预收开票";
            fcs.totmoney = tot;
            fcs.custName = custname;
            fcs.ShowDialog();
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            show_all_unKaiPiao();
        }

      
        
    }
}
