using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Bargain
{
    public partial class RecordCopyNum : Form
    {
        private string mtype_ = "";   //当前选择的机型
        private string manufactCode_ = "";//当前选择的机号
        private string hctype_ = "";  //当前选择的幅面

        public string barsysid_ = "";   //传入的合同系统号
        public string bargid_ = "";   //传入的合同号


        //private int hsorder_ = 1;      //在第几个核算
        //private int czorder_ = 1;      //在核算周期的第几个抄张周期


        private int hsperiod;          //核算月
        private int czperiod;           //抄张月
       // private int MaxCzOrder;        //一个核算周期最多有几次抄张
        
        DateTime BargStartDay;         //合同开始时间
        //DateTime CzFrom, CzTo;         //所属抄张周期的起、止日期
        public RecordCopyNum()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //根据Custid(客户ID),CustName(客户名),PinYinCode(拼音码),
            //mtype，Manufactcode(机号)来模糊查询
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string sql_ = "select T_bargains.sysid as 合同系统编号,"
                    +"T_bargains.BargId as 合同编号,"
                    +"T_CustomerInf.custName as 客户名称,"
                    + "T_bargains.mtype as 机型,"
                    +"T_bargains.Manufactcode as 机号,"
                    + "T_bargains.StartDate as 起始日,"
                    + "T_bargains.EndDate as 终止日,"
                    + "T_CustomerInf.CustID as 客户编号 "
                    + " from T_bargains "
                    + " left join T_CustomerInf on T_bargains.CustCode=T_CustomerInf.CustID "
                    + " where (T_bargains.Bargstatus like '有效') "
                    + " and (T_CustomerInf.CustID like '%{0}%'"
                    + " or CustName like '%{1}%' or PinYinCode like '%{2}%'"
                    + " or (T_bargains.BargId in "
                    + "(select bargid from T_bargains "
                    + " where Manufactcode like '%{3}%' "
                    +" or bargid like '%{4}%' "
                    + " or mtype like '%{5}%')))";
                sql_ = string.Format(sql_, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);

                DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
                FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                fr.unVisible_Column_index_ = -1;
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    this.dgv_hs.DataSource = false;
                    this.dgv_cz.DataSource = false;

                    string barsysid_ = fr.dr_.Cells["合同系统编号"].Value.ToString();
                   
                    string barid_=fr.dr_.Cells["合同编号"].Value.ToString();

                    tb_jx.Text= fr.dr_.Cells["机型"].Value.ToString();
                    tb_jh.Text= fr.dr_.Cells["机号"].Value.ToString();
                    tb_bargid.Text = barid_;

                    //合同号
                    this.barsysid_ = barsysid_;
                    this.bargid_ = barid_;
                    //机号
                    this.manufactCode_ = tb_jh.Text.Trim();
                    //机型
                    this.mtype_ = tb_jx.Text.Trim();

                    sql_="select hctype from T_BargFee where barsysid="+barsysid_;
                    dt = (new SqlDBConnect()).Get_Dt(sql_);
                    this.cb_hctype.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.cb_hctype.Items.Add(dt.Rows[i]["hctype"].ToString().Trim());
                    }
                }

            }
        }


        //生成核算周期列表
        private void produce_hs_lst()
        {
            if (this.hctype_ == "") return;
            if (this.manufactCode_ == "") return;
            if (this.mtype_ == "") return;

            string sql_ = "";
            //获取合同对应的数据
            //"获取核算周期" hs_period
            sql_ = "select checkperiod from T_Bargains where sysid=" + this.barsysid_;
            if ((new DBUtil()).Get_Single_val(sql_) == "")
            {
                MessageBox.Show("该合同核算周期未输入!");
                return;
            }
            int hs_period = Convert.ToInt16((new DBUtil()).Get_Single_val(sql_));
            this.hsperiod = hs_period;

            //获取"抄张周期" cz_period
            sql_ = "select CopyNumGap from T_Bargains where sysid=" + this.barsysid_;
            if ((new DBUtil()).Get_Single_val(sql_) == "")
            {
                MessageBox.Show("该合同抄张周期未输入!");
                return;
            }
            int cz_period = Convert.ToInt16((new DBUtil()).Get_Single_val(sql_));
            this.czperiod = cz_period;

           
            //获取"合同期时间" ht_period
            sql_ = "select Periodgap from T_Bargains where sysid=" + this.barsysid_;
            int ht_period = Convert.ToInt16((new DBUtil()).Get_Single_val(sql_));

            //获取"合同初始日期" Dstart
            sql_ = "select firstczDate from T_Bargains where sysid=" + this.barsysid_;
            string firstczDate = (new DBUtil()).Get_Single_val(sql_);
            
            //按首次抄张日推算抄张周期
            DateTime Dstart = Convert.ToDateTime(firstczDate);  
            this.BargStartDay = Dstart;

           
            //获取"合同终止日期" Dend
            sql_ = "select EndDate from T_Bargains where sysid=" + this.barsysid_;
            string sEndDate = (new DBUtil()).Get_Single_val(sql_);
            DateTime Dend = Convert.ToDateTime(sEndDate);

            //获取"对应机器初始读数" startNum
            sql_ = "select Startnum from T_BargFee "
                 +" where barsysid="+this.barsysid_
                 //+ " and Mtype='"+this.mtype_+"'"
                 //+ " and Manufactcode='"+this.manufactCode_+"'"
                 + "  and HcType='" + this.hctype_ + "'";
            int startNum = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));

            //构造该有多少个核算记录行
            DataTable Dtb = new DataTable("tb_hs");
            Dtb.Columns.Add("初始张数", Type.GetType("System.Int32"));
            Dtb.Columns.Add("初始日期", Type.GetType("System.String"));
            Dtb.Columns.Add("终止日期", Type.GetType("System.String"));
            Dtb.Columns.Add("抄张次数", Type.GetType("System.Int32"));

            //获取当前日期
            DateTime Dcurrent = DBUtil.getServerTime();
            DateTime DFrom, DTo;

            int Ncopy = startNum;
            int NCopyhs = 0;
            for (int k = 0; k < (ht_period / hs_period); k++)  //多个核算周期
            {
                //从日期 DFrom
                //到日期
                DFrom = Dstart.AddMonths(hs_period * k);
                DTo = Dstart.AddMonths(hs_period * (k + 1)).AddDays(-1);

                //取数计算======
                get_CopyTot(DFrom, DTo, ref NCopyhs);
                //如果抄到数，则记录，否则取上次
                if (NCopyhs > 0)
                    Ncopy = NCopyhs;
                else
                    Ncopy = 0;
                //==============

                DataRow newRow;
                newRow = Dtb.NewRow();
                if (Ncopy>0)
                    newRow["初始张数"] = Ncopy;
                else
                    newRow["初始张数"] = System.DBNull.Value;
                newRow["初始日期"] = DFrom.ToString("yyyy-MM-dd");
                newRow["终止日期"] = DTo.ToString("yyyy-MM-dd");
                newRow["抄张次数"] = hs_period / cz_period;
                Dtb.Rows.Add(newRow);
                

            }

            this.dgv_hs.DataSource = Dtb.DefaultView;
            this.dgv_hs.Columns[0].Width = 80;
            this.dgv_hs.Columns[1].Width = 80;
            this.dgv_hs.Columns[2].Width = 80;
            this.dgv_hs.Columns[3].Width = 80;
        }


        //生成某一个“核算期间”的对应“抄张周期”列表
        // iniNum：本核算周期的期初读数.
        private void produce_cz_lst(DateTime DFrom, DateTime DTo, int startNum)
        {
            //获取"抄张周期" cz_period（每过多少月抄张一次）
            string sql_ = "select CopyNumGap from T_Bargains where sysid=" + this.barsysid_;
            int cz_period = Convert.ToInt16((new DBUtil()).Get_Single_val(sql_));

            //获取"计划抄张日" firstCzDate（）
            sql_ = "select firstCzDate from T_Bargains where sysid=" + this.barsysid_;
            DateTime CzDatePlan = Convert.ToDateTime((new DBUtil()).Get_Single_val(sql_));

            TimeSpan dltD=CzDatePlan - this.BargStartDay; //抄张日和起始日之差
            int dltDays = dltD.Days;

            //"获取核算周期" hs_period（每过多少月核算一次）
            sql_ = "select checkperiod from T_Bargains where sysid=" + this.barsysid_;
            int hs_period = Convert.ToInt16((new DBUtil()).Get_Single_val(sql_));

            //构造抄张列表
            //构造该有多少个抄张记录行
            DataTable Dtb_cz = new DataTable("tb_cz");
            Dtb_cz.Columns.Add("sysid", Type.GetType("System.Int32"));
            Dtb_cz.Columns.Add("initNum", Type.GetType("System.Int32"));
            Dtb_cz.Columns.Add("recordNum", Type.GetType("System.Int32"));
            Dtb_cz.Columns.Add("PlanDay", Type.GetType("System.String"));
            Dtb_cz.Columns.Add("OccurDay", Type.GetType("System.String"));

            Dtb_cz.Columns.Add("CzFrom", Type.GetType("System.String"));
            Dtb_cz.Columns.Add("CzTo", Type.GetType("System.String"));


            DateTime DFrom_cz, DTo_cz,DPlan_cz;
            int NFrom = startNum, Ndel = 0;  //期初和本次实抄数量
            string czDate = "";   //记录实际抄张日期

            for (int m = 0; m < (hs_period / cz_period); m++)
            {
                //从日期 DFrom_cz
                //到日期 DTo_cz
                DFrom_cz = DFrom.AddMonths(cz_period * m);
                DTo_cz = DFrom.AddMonths(cz_period * (m + 1)).AddDays(-1);
                //DPlan_cz = DFrom_cz.AddDays(dltDays);
                //首次抄张日默认取合同的起始日期
                DPlan_cz = DFrom_cz.AddDays(dltDays).AddMonths(cz_period);
                
                //取数计算(*******）
                czDate = "";
                get_CopyTot(DFrom_cz,DTo_cz, ref Ndel, ref czDate);
                //*****************
                DataRow newRow;
                newRow = Dtb_cz.NewRow();
                newRow["sysid"] = m + 1;
                if (NFrom > 0)
                    newRow["initNum"] = NFrom;    //上次
                else
                    newRow["initNum"] = System.DBNull.Value;    //上次
                
                if (Ndel != 0)
                    newRow["recordNum"] = Ndel;   //本次
                else
                    newRow["recordNum"] = System.DBNull.Value;

                newRow["PlanDay"] = DPlan_cz.ToString("yyyy-MM-dd");
                newRow["OccurDay"] = czDate;
                newRow["CzFrom"] = DFrom_cz.ToString("yyyy-MM-dd");
                newRow["CzTo"] = DTo_cz.ToString("yyyy-MM-dd");


                Dtb_cz.Rows.Add(newRow);

                //if (Ndel > NFrom) 
                    NFrom = Ndel;
            }

            this.dgv_cz.DataSource = Dtb_cz.DefaultView;
            this.dgv_cz.Columns[0].HeaderText = "序号";
            this.dgv_cz.Columns[1].HeaderText = "初次读数";
            this.dgv_cz.Columns[2].HeaderText = "实际读数";
            this.dgv_cz.Columns[3].HeaderText = "计划抄张日";
            this.dgv_cz.Columns[4].HeaderText = "实际抄张日";
            this.dgv_cz.Columns[5].HeaderText = "抄张周期起日";
            this.dgv_cz.Columns[6].HeaderText = "抄张周期止日";

            this.dgv_cz.Columns[0].Width = 60;
            this.dgv_cz.Columns[1].Width = 80;
            this.dgv_cz.Columns[2].Width = 80;
            this.dgv_cz.Columns[3].Width = 90;
            this.dgv_cz.Columns[4].Width = 90;

        }


        //获取某一个期间的实际抄张量
        //返回: Ntot：实际抄张量, occurDate:实际抄张日
        private void get_CopyTot(DateTime Dfrom,DateTime Dto, ref int Ntot, ref string OccurDate)
        {
            DataTable dt;
            string sql_ = "select curDate,CurNum from T_RecordCopy "
                    + " where Czfrom='" + Dfrom.ToString("yyyy-MM-dd")+"'"
                    +" and Czto='" + Dto.ToString("yyyy-MM-dd")+"'"
                    + " and mtype='" + this.mtype_ + "'"
                    + " and manufactcode='" + this.manufactCode_ + "'"
                    + " and hctype='" + this.hctype_ + "'";
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            Ntot = 0;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["CurNum"].ToString().Trim() == "")
                {
                    Ntot = 0;
                    OccurDate = "";
                }
                else
                {
                    Ntot = Convert.ToInt32(dt.Rows[0]["CurNum"].ToString().Trim());
                    OccurDate = Convert.ToDateTime(dt.Rows[0]["curDate"].ToString().Trim()).ToString("yyyy-MM-dd");

                }
            }
            else
                OccurDate = "";
        }


        //获取某一个核算周期的实际抄张量（重载）
        //返回: Ntot：实际抄张量
        private void get_CopyTot(DateTime Dfrom,DateTime Dto, ref int Ntot)
        {
            string sql_ = "";
            Ntot = 0;
            if (Dfrom != BargStartDay)  //不是第一次
            {
                sql_ = "select CurNum from T_RecordCopy "
                        + " where czto='" + Dfrom.AddDays(-1).ToString("yyyy-MM-dd") + "'"
                        + " and mtype='" + this.mtype_ + "'"
                        + " and manufactcode='" + this.manufactCode_ + "'"
                        + " and hctype='" + this.hctype_ + "'"
                        + " order by czto desc ";
                DataTable dt;
                dt = (new SqlDBConnect()).Get_Dt(sql_);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["CurNum"].ToString().Trim() != "")
                    {
                        Ntot = Convert.ToInt32(dt.Rows[0]["CurNum"].ToString().Trim());
                    }
                    else
                        Ntot = 0;
                }
            }
            else
            {
                //获取"对应机器初始读数" startNum
                sql_ = "select StartnuM from T_BargFee "
                     + " where barsysid=" + this.barsysid_
                     + "  and HcType='" + this.hctype_ + "'";
                if ((new DBUtil()).Get_Single_val(sql_).Trim() != "")
                    Ntot = Convert.ToInt32((new DBUtil()).Get_Single_val(sql_));
            }


        }

        private void cb_hctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hctype_ = this.cb_hctype.Text.Trim();
            this.dgv_cz.DataSource = null;
            //生成并显示多次结算
            produce_hs_lst();
        }

        

        private void dgv_hs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgv_cz.DataSource = null;
            if (this.dgv_hs.SelectedRows.Count <= 0) return;
            if (this.dgv_hs.SelectedRows[0].Cells[0].Value == null) return;
            if (this.dgv_hs.SelectedRows[0].Cells["初始日期"].Value.ToString().Trim() == "") return;

            DateTime DFrom, DTo;
            DFrom = Convert.ToDateTime(this.dgv_hs.SelectedRows[0].Cells["初始日期"].Value.ToString());
            DTo = Convert.ToDateTime(this.dgv_hs.SelectedRows[0].Cells["终止日期"].Value.ToString());
            int startNum=0;
            if (this.dgv_hs.SelectedRows[0].Cells["初始张数"].Value != null)
            {
                if (this.dgv_hs.SelectedRows[0].Cells["初始张数"].Value.ToString().Trim()!="")
                startNum = Convert.ToInt32(this.dgv_hs.SelectedRows[0].Cells["初始张数"].Value.ToString());
            }
                produce_cz_lst(DFrom, DTo, startNum);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dgv_cz.SelectedRows.Count <= 0) return;
            if (this.dgv_cz.SelectedRows[0].Cells[0].Value == null) return;
            if (this.dgv_cz.SelectedRows[0].Cells[0].Value.ToString().Trim() == null) return;

            string czfrom=this.dgv_cz.SelectedRows[0].Cells["CzFrom"].Value.ToString();
            string czto = this.dgv_cz.SelectedRows[0].Cells["CzTo"].Value.ToString();
           
            FormCopyAdd fca = new FormCopyAdd();
            //传入参数
            fca.barsysid_ = this.barsysid_;
            fca.ManufactCode_ = this.manufactCode_;
            fca.Mtype_ = this.mtype_;
            fca.hctype_ = this.hctype_;
            fca.CzFrom = czfrom;
            fca.CzTo = czto;
            //
            fca.type = "add";
            if (fca.ShowDialog() == DialogResult.OK)
            {
                //this.czorder_ = 1;
                this.dgv_hs_CellClick(null, null);
            }
        }

        private void dgv_cz_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgv_cz.SelectedRows.Count <= 0) return;
            if (this.dgv_cz.SelectedRows[0].Cells[0].Value == null) return;
            if (this.dgv_cz.SelectedRows[0].Cells[0].Value.ToString().Trim() == null) return;

            int index_ = this.dgv_cz.SelectedRows[0].Index;
            //this.czorder_ = index_ + 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.dgv_cz.SelectedRows.Count <= 0) return;
            if (this.dgv_cz.SelectedRows[0].Cells[0].Value == null) return;
            if (this.dgv_cz.SelectedRows[0].Cells[0].Value.ToString().Trim() == null) return;

            string czfrom = this.dgv_cz.SelectedRows[0].Cells["CzFrom"].Value.ToString();
            string czto = this.dgv_cz.SelectedRows[0].Cells["CzTo"].Value.ToString();
           
            string sql_ = "select lastNum from T_RecordCopy where barsysid=" + this.barsysid_
                  + " and mtype='" + this.mtype_
                  + "' and manufactcode='" + this.manufactCode_
                  + "' and hctype='" + this.hctype_
                  +"' and Czfrom='" + czfrom+"'"
                  + " and czto='" + czto+"'";
            bool isexist = (new DBUtil()).yn_exist_data(sql_);
            if (!isexist)
            {
                MessageBox.Show("该次还未抄张,不能执行修改操作!");
                return;
            }

            FormCopyAdd fca = new FormCopyAdd();
            //传入参数
            fca.barsysid_ = this.barsysid_;
            fca.ManufactCode_ = this.manufactCode_;
            fca.Mtype_ = this.mtype_;
            fca.hctype_ = this.hctype_;
            fca.CzFrom = czfrom;
            fca.CzTo = czto;
            fca.type = "edit";
            if (fca.ShowDialog() == DialogResult.OK)
            {
                this.dgv_hs_CellClick(null, null);
            }
        }

        private void RecordCopyNum_Load(object sender, EventArgs e)
        {
            this.dgv_hs.AllowUserToAddRows = false;
            this.dgv_cz.AllowUserToAddRows = false;
            this.dgv_should.AllowUserToAddRows = false;

            DateTime now_=DBUtil.getServerTime();
            int year_ = now_.Year;
            int month_ = now_.Month;
            this.dateTimePicker1.Value = Convert.ToDateTime(year_.ToString().Trim() + "-"
                + month_.ToString().Trim() + "-1");
            this.dateTimePicker2.Value = now_;
        }

        //查询期间未抄张的机器和幅面
        private void get_all_uncopy(DateTime dfrom,DateTime dto)
        {
            DataTable DtLast = new DataTable("TbNotRecordCopy");
            DtLast.Columns.Add("合同系统编号", Type.GetType("System.String"));
            DtLast.Columns.Add("合同编号", Type.GetType("System.String"));
            DtLast.Columns.Add("客户编号", Type.GetType("System.String"));
            DtLast.Columns.Add("客户名称", Type.GetType("System.String"));
            DtLast.Columns.Add("机型", Type.GetType("System.String"));
            DtLast.Columns.Add("机号", Type.GetType("System.String"));
            DtLast.Columns.Add("幅面", Type.GetType("System.String"));
            DtLast.Columns.Add("合同开始日期", Type.GetType("System.String"));
            DtLast.Columns.Add("抄张周期", Type.GetType("System.String"));
            DtLast.Columns.Add("预计抄张日", Type.GetType("System.String"));
            DtLast.Columns.Add("联系信息", Type.GetType("System.String"));
           
            //遍历所有有效的合同
            string sql_="",sql1="",sql2="";
            string barid_, custid_, mt_, mc_;
            string barsysid_ = "";
            int checkp,htperiod;
            DateTime startD, firstcz,CurD;

            string hctype_,shctypes;
            string custName_="", lxr="";
            sql_ = "select sysid,BargId,CustCode,Mtype,Manufactcode,"
                + "CopyNumGap,startDate,firstczdate,Periodgap"
                +" from T_Bargains where Bargstatus!='过期' and Bargstatus!='作废'";
            DataTable dt,dt1,dt2;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                barsysid_ = dt.Rows[i]["sysid"].ToString().Trim();
                barid_ = dt.Rows[i]["BargId"].ToString().Trim();
                custid_ = dt.Rows[i]["CustCode"].ToString().Trim();
                
                mt_ = dt.Rows[i]["Mtype"].ToString().Trim();
                mc_ = dt.Rows[i]["Manufactcode"].ToString().Trim();
                if (dt.Rows[i]["CopyNumGap"].ToString().Trim() != "")
                    checkp = Convert.ToInt16(dt.Rows[i]["CopyNumGap"].ToString().Trim());
                else
                {
                    MessageBox.Show("有合同抄张周期未正确录入!");
                    return;
                }

                if (dt.Rows[i]["Periodgap"].ToString().Trim() != "")
                    htperiod = Convert.ToInt16(dt.Rows[i]["Periodgap"].ToString().Trim());
                else
                {
                    MessageBox.Show("有合同的合同月数未正确录入!");
                    return;
                }
                startD = Convert.ToDateTime(dt.Rows[i]["startDate"].ToString().Trim());
                firstcz = Convert.ToDateTime(dt.Rows[i]["firstczdate"].ToString().Trim());

                shctypes = "";
                for (int k = 1; k <= htperiod / checkp; k++)
                {
                    CurD = startD.AddMonths(k * checkp).AddDays(-1); //每期的最后抄张日
                    if ((CurD <= dto) && (CurD >= dfrom))
                    {
                        sql1 = "select hctype from T_BargFee where barsysid=" + barsysid_;
                        dt1 = (new SqlDBConnect()).Get_Dt(sql1);
                        for (int m = 0; m < dt1.Rows.Count; m++)
                        {
                            hctype_ = dt1.Rows[m]["hctype"].ToString().Trim();
                            if (ynFinishCz(CurD, barsysid_, hctype_) == false)
                            {
                                if (shctypes == "")
                                    shctypes = hctype_;
                                else
                                    shctypes = shctypes + "," + hctype_;
                            }
                        }
                    }
                }

                if (shctypes != "")  //有未抄张的幅面
                {
                    //获取客户名称
                    custName_ = (new DBUtil()).Get_Single_val("t_customerInf", "CustName", "custid", custid_);
                    //获取联系人信息
                    sql2 = "select CName,Tel,Tel1,Tel2 from T_CustMaContacts "
                      + "left join T_CustomerMac on T_CustMaContacts.CmSysId=T_CustomerMac.sysid "
                      + " where T_CustomerMac.Mtype='" + mt_ + "'"
                      + " and T_CustomerMac.Manufactcode='" + mc_ + "'"
                      + " and T_CustMaContacts.ctype='抄张联系人'";
                    dt2 = (new SqlDBConnect()).Get_Dt(sql2);
                    if (dt2.Rows.Count > 0)
                    {
                        if (dt2.Rows[0]["CName"].ToString().Trim()!="")
                        lxr=lxr+" "+dt2.Rows[0]["CName"].ToString().Trim();
                        if (dt2.Rows[0]["Tel"].ToString().Trim()!="")
                        lxr=lxr+" "+dt2.Rows[0]["Tel"].ToString().Trim();
                        if (dt2.Rows[0]["Tel1"].ToString().Trim() != "")
                            lxr = lxr + " " + dt2.Rows[0]["Tel1"].ToString().Trim();
                        if (dt2.Rows[0]["Tel2"].ToString().Trim() != "")
                            lxr = lxr + " " + dt2.Rows[0]["Tel2"].ToString().Trim();
                    }
                    //
                    DataRow dr = DtLast.NewRow();
                    dr[0] = barid_;  //合同编号
                    dr[1] = custid_;
                    dr[2] = custName_;   //客户名称
                    dr[3] = mt_;
                    dr[4] = mc_;  //机号
                    dr[5] = shctypes;   //幅面
                    dr[6] = startD.ToString("yyyy-MM-dd");   //合同开始日期 
                    dr[7] = checkp.ToString();   //抄张周期
                    dr[8] = firstcz.ToString("yyyy-MM-dd");   //抄张日期
                    dr[9] = lxr;   //联系人 
                    DtLast.Rows.Add(dr);
                
                }

            }
            this.dgv_should.DataSource=DtLast.DefaultView;
            this.lb_cnt.Text="未抄张机器个数："+DtLast.Rows.Count.ToString().Trim();
        }

        private bool ynFinishCz(DateTime curd, string barsysid, string hctype)
        {
            string sql_ = "select curNum from T_RecordCopy"
                 + " where czfrom <='" + curd.ToString("yyyy-MM-dd").Trim() + "'"
                 + " and czto >='" + curd.ToString("yyyy-MM-dd").Trim() + "'"
                 + " and barsysid=" + barsysid 
                 +" and hctype='"+hctype+"'";
            return ((new DBUtil()).yn_exist_data(sql_));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            get_all_uncopy(dateTimePicker1.Value, dateTimePicker2.Value);
            if (this.dgv_should.Columns.Count > 0) 
            {
                this.dgv_should.Columns[0].Width = 80;
                this.dgv_should.Columns[1].Width = 80;
                this.dgv_should.Columns[2].Width = 120;
                this.dgv_should.Columns[7].Width = 80;
                this.dgv_should.Columns[9].Width = 200;
            }
        }

        private void dgv_should_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgv_should.SelectedRows.Count<=0) return;
            if (this.dgv_should.SelectedRows[0].Cells["合同编号"].Value == null) return;
            if (this.dgv_should.SelectedRows[0].Cells["机型"].Value == null) return;
            if (this.dgv_should.SelectedRows[0].Cells["机号"].Value==null) return;
            
            string barid_ = this.dgv_should.SelectedRows[0].Cells["合同编号"].Value.ToString().Trim();
            string mt_=this.dgv_should.SelectedRows[0].Cells["机型"].Value.ToString().Trim();
            string mc_=this.dgv_should.SelectedRows[0].Cells["机号"].Value.ToString().Trim();
            if (mt_ == "") return;
            if (mc_ == "") return;
            if (barid_ == "") return;
            this.tabControl1.SelectedIndex = 1;


            tb_jx.Text = mt_;
            tb_jh.Text = mc_;
            //合同号
            this.bargid_ = barid_;
            //机号
            this.manufactCode_ = tb_jh.Text.Trim();
            //机型
            this.mtype_ = tb_jx.Text.Trim();

            string sql_ = "";
            sql_ = "select hctype from T_BargFee where BargId='" + barid_ + "'";
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.cb_hctype.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.cb_hctype.Items.Add(dt.Rows[i]["hctype"].ToString().Trim());
            }


        }

       
       

    }
}
