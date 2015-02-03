using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.Base;

namespace Warehouse.Bargain
{
    public partial class BargainsBindAdd : Form
    {
        public string type = "";
        public string bdid = "";   //传入的单据号

        private DataTable dtmac;
        private DataTable dtpara;

        public delegate void BargainsBindAddChg();
        public event BargainsBindAddChg BargainsBindAddChg_;


        public BargainsBindAdd()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            string swhere = "";
            if (Util.ControlTextIsNUll(this.n_BaseFee))
            {
                MessageBox.Show("基本金额项必填！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.n_BaseFee.Focus();
                return;
            }
            if (Util.ControlTextIsNUll(this.s_bdstatus))
            {
                MessageBox.Show("请确定捆绑类型！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_bdstatus.Focus();
                return;
            }

           

            //遍历所有的机号，填入s_allBarIds
            string sbars = "";
            for (int i = 0; i < this.dgv_macs.Rows.Count; i++)
            {
                if (this.dgv_macs.Rows[i].Cells["机号"].Value != null)
                {
                    if (sbars == "")
                        sbars = this.dgv_macs.Rows[i].Cells["机号"].Value.ToString().Trim();
                    else
                        sbars = sbars + "," + this.dgv_macs.Rows[i].Cells["机号"].Value.ToString().Trim();
                }
            }
            this.s_All_manufacts.Text = sbars;
            ///***********************
            ///
            if (!verify_ok(this.dtmac))
            {
                return;
            }

            List<string> SqlLst = new List<string>();
            
            string sql_ = "";
            string ReceiptId="";
            string SqlUpdateBillRull="";
            string barid_, mtype_, mc_;
            string hctype_, myzs_, jbzs_;
            if (this.type == "add")
            {
                 //产生单据号,更新单据规则
                ReceiptId = DBUtil.Produce_Bill_Id("KB", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull);
                SqlLst.Add(SqlUpdateBillRull); 

                //插入主表信息
                this.s_bdid.Text = ReceiptId;
                sql_ = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_BargBind");
                SqlLst.Add(sql_);  

                //插入机器子表
                for (int i = 0; i < this.dgv_macs.Rows.Count; i++)
                {
                    if (this.dgv_macs.Rows[i].Cells["合同系统编号"].Value != null)
                    {
                        barid_ = this.dgv_macs.Rows[i].Cells["合同系统编号"].Value.ToString().Trim();
                        mtype_ = this.dgv_macs.Rows[i].Cells["机型"].Value.ToString().Trim();
                        mc_ = this.dgv_macs.Rows[i].Cells["机号"].Value.ToString().Trim();

                        sql_ = "insert into T_BargBindMacs(bdid,barsysid,Mtype,Manufactcode)"
                           + " values('{0}',{1},'{2}','{3}')";
                        sql_ = string.Format(sql_, ReceiptId, barid_, mtype_, mc_);
                        SqlLst.Add(sql_);
                    }
                }


                //插入参数子表
                sql_ = "delete from T_BargBindSet where bdid='" + this.bdid + "'";
                SqlLst.Add(sql_);
                for (int i = 0; i < this.dgv_para.Rows.Count; i++)
                {
                    if (this.dgv_para.Rows[i].Cells["幅面"].Value != null)
                    {
                        hctype_ = this.dgv_para.Rows[i].Cells["幅面"].Value.ToString().Trim();
                        myzs_ = this.dgv_para.Rows[i].Cells["免印张数"].Value.ToString().Trim();
                        jbzs_ = this.dgv_para.Rows[i].Cells["基本印量"].Value.ToString().Trim();

                        sql_ = "insert into T_BargBindSet(bdid,Hctype,MyNum,BaseNum)"
                           + " values('{0}','{1}',{2},{3})";
                        sql_ = string.Format(sql_, ReceiptId, hctype_, myzs_, jbzs_);
                        SqlLst.Add(sql_);
                    }
                }
                //
            }
            else
            {
                ReceiptId = this.s_bdid.Text.Trim();
                swhere = " where Bdid='" + this.bdid + "'";
                sql_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_BargBind", swhere);
                SqlLst.Add(sql_);

                ////插入参数子表
                sql_ = "delete from T_BargBindMacs where bdid='" + this.bdid + "'";
                SqlLst.Add(sql_);
                
                for (int i = 0; i < this.dgv_macs.Rows.Count; i++)
                {
                    if (this.dgv_macs.Rows[i].Cells["合同系统编号"].Value != null)
                    {
                        barid_ = this.dgv_macs.Rows[i].Cells["合同系统编号"].Value.ToString().Trim();
                        mtype_ = this.dgv_macs.Rows[i].Cells["机型"].Value.ToString().Trim();
                        mc_ = this.dgv_macs.Rows[i].Cells["机号"].Value.ToString().Trim();

                        sql_ = "insert into T_BargBindMacs(bdid,barsysid,Mtype,Manufactcode)"
                           + " values('{0}',{1},'{2}','{3}')";
                        sql_ = string.Format(sql_, ReceiptId, barid_, mtype_, mc_);
                        SqlLst.Add(sql_);
                    }
                }

                ////插入参数子表
                sql_ = "delete from T_BargBindSet where bdid='" + this.bdid + "'";
                SqlLst.Add(sql_);

                for (int i = 0; i < this.dgv_para.Rows.Count; i++)
                {
                    if (this.dgv_para.Rows[i].Cells["幅面"].Value != null)
                    {
                        hctype_ = this.dgv_para.Rows[i].Cells["幅面"].Value.ToString().Trim();
                        myzs_ = this.dgv_para.Rows[i].Cells["免印张数"].Value.ToString().Trim();
                        jbzs_ = this.dgv_para.Rows[i].Cells["基本印量"].Value.ToString().Trim();

                        sql_ = "insert into T_BargBindSet(bdid,Hctype,MyNum,BaseNum)"
                           + " values('{0}','{1}',{2},{3})";
                        sql_ = string.Format(sql_, ReceiptId, hctype_, myzs_, jbzs_);
                        SqlLst.Add(sql_);
                    }
                }

            }
            //*********
            try
            {
                (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                BargainsBindAddChg_();
                this.DialogResult = DialogResult.OK;
            }
            catch
            { }
            //
        }

        private void BargainsBindAdd_Load(object sender, EventArgs e)
        {
            this.dgv_macs.AllowUserToAddRows = false;
            this.dgv_para.AllowUserToAddRows = false;
            (new InitFuncs()).Num_limited(this.panel1);
            this.s_bdid.Text = this.bdid;
            if (this.type == "edit")
            {
                string sql_ = "select * from T_BargBind where bdid='" + this.bdid + "'";
                (new InitFuncs()).ShowDatas(this.panel1, sql_);
            }
            this.s_occurtime.Text=DBUtil.getServerTime().ToString("yyyy-MM-dd");
            show_dgv_macs();
            show_dgv_paras();
        }

        private void show_dgv_macs()
        {
            string sql_ = "select T_BargBindMacs.barsysid as 合同系统编号,"
                +"T_bargains.bargid as 合同号,"
                + "T_BargBindMacs.mtype as 机型,T_BargBindMacs.Manufactcode as 机号 "
                + " from T_BargBindMacs, T_bargains "
                + " where T_BargBindMacs.barsysid=T_bargains.sysid "
                +" and T_BargBindMacs.Bdid='" + this.bdid + "'";
            dtmac = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_macs.DataSource = dtmac.DefaultView;
         }
        
        private void show_dgv_paras()
        {
            string sql_ = "select hctype as 幅面,MyNum as 免印张数,baseNum as 基本印量 "
                + " from T_BargBindSet "
                + " where Bdid='" + this.bdid + "'";
            
            dtpara = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_para.DataSource = dtpara.DefaultView;
        }


      

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string strSql = "select T_bargains.sysid as 合同系统编号,"
                                +"T_bargains.bargid as 合同编号,"
                                +"T_bargains.custcode as 客户编码,"
                                + "T_customerInf.CustName as 客户名称,"
                                + "T_bargains.mtype as 机型,"
                                + "T_bargains.Manufactcode as 机号"
                                + " from T_bargains "
                                + "left join T_customerInf on T_bargains.custcode=T_customerInf.custid"
                                + " where T_bargains.custcode like '%{0}%' "
                                + "or T_customerInf.CustName like '%{1}%' "
                                + "or T_customerInf.PinYinCode like '%{2}%' "
                                + "or T_bargains.mtype like '%{3}%' "
                                + "or T_bargains.Manufactcode like '%{4}%' "
                                +"or T_bargains.bargid like '%{5}%' ";
                                strSql = string.Format(strSql, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);
                
                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                fr.unVisible_Column_index_ = 0;
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    DataRow ndr = dtmac.NewRow();
                    ndr[0] = fr.dr_.Cells["合同系统编号"].Value.ToString().Trim();
                    ndr[1] = fr.dr_.Cells["合同编号"].Value.ToString().Trim();
                    ndr[2] = fr.dr_.Cells["机型"].Value.ToString();
                    ndr[3] = fr.dr_.Cells["机号"].Value.ToString();
                    
                    //
                    dtmac.Rows.Add(ndr);
                    textBox.Text = "";
                }

                

            }
        }

      
        //验证绑定合同的一致性（以下内容必须一致）:
        //***(开始日期（StartDate），结束日期（EndDate），合同期时间（Periodgap）
        //核算周期（CheckPeriod），抄张周期（CopyNumGap），
        //预收费周期（ForeadNum），收费类型（FeeType）
        //预收费方式（PreChargeMethod）
        //租赁计费方式（Jftype），首次抄张日期（firstCzDate）
        private bool verify_ok(DataTable dt)
        {
            DataTable dtmp;
            string sql_="";
            string firstbarid_ = "",barid_;
            string BCheckPeriod = "";
            string BCopyNumGap="",BForeadNum="",BFeeType="",BPreChargeMethod="";
            string BJftype = "", BfirstCzDate = "";
            string LCheckPeriod, LCopyNumGap, LForeadNum, LFeeType, LPreChargeMethod,LJftype,LfirstCzDate;
            DateTime FirstCzDay1=Convert.ToDateTime("1900-01-01"), FirstCzDay2;
            int CzPeriod=0;
            
            //for (int i = 0; i < dt.Rows.Count; i++)
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (i == 0)
                    {
                        firstbarid_ = dr["合同系统编号"].ToString().Trim();
                        sql_ = "select CheckPeriod,"
                            + "CopyNumGap,ForeadNum,FeeType,PreChargeMethod,Jftype,firstCzDate"
                            + " from T_Bargains where sysid=" + firstbarid_;
                        dtmp = (new SqlDBConnect()).Get_Dt(sql_);
                        BCheckPeriod = dtmp.Rows[0]["CheckPeriod"].ToString().Trim();
                        BCopyNumGap = dtmp.Rows[0]["CopyNumGap"].ToString().Trim();
                        BForeadNum = dtmp.Rows[0]["ForeadNum"].ToString().Trim();
                        BFeeType = dtmp.Rows[0]["FeeType"].ToString().Trim();
                        BPreChargeMethod = dtmp.Rows[0]["PreChargeMethod"].ToString().Trim();
                        BJftype = dtmp.Rows[0]["Jftype"].ToString().Trim();
                        BfirstCzDate = dtmp.Rows[0]["firstCzDate"].ToString().Trim();
                        FirstCzDay1 = Convert.ToDateTime(BfirstCzDate);  //首次抄张日
                        CzPeriod=Convert.ToInt16(BCopyNumGap);
                    }
                    else
                    {
                        barid_ = dr["合同系统编号"].ToString().Trim();
                        //验证两个Barid的上述属性是否相同
                        sql_ = "select StartDate,EndDate,Periodgap,CheckPeriod,"
                            + "CopyNumGap,ForeadNum,FeeType,PreChargeMethod,Jftype,firstCzDate "
                            + " from T_Bargains where sysid=" + barid_;
                        dtmp = (new SqlDBConnect()).Get_Dt(sql_);
                        LCheckPeriod = dtmp.Rows[0]["CheckPeriod"].ToString().Trim();
                        LCopyNumGap = dtmp.Rows[0]["CopyNumGap"].ToString().Trim();
                        LForeadNum = dtmp.Rows[0]["ForeadNum"].ToString().Trim();
                        LFeeType = dtmp.Rows[0]["FeeType"].ToString().Trim();
                        LPreChargeMethod = dtmp.Rows[0]["PreChargeMethod"].ToString().Trim();
                        LJftype = dtmp.Rows[0]["Jftype"].ToString().Trim();
                        LfirstCzDate = dtmp.Rows[0]["firstCzDate"].ToString().Trim();
                        FirstCzDay2 = Convert.ToDateTime(LfirstCzDate); //首次抄张日期
                        if (
                             (LCheckPeriod != BCheckPeriod) || (LCopyNumGap != BCopyNumGap)
                            || (LForeadNum != BForeadNum) || (LFeeType != BFeeType)
                            || (LPreChargeMethod != BPreChargeMethod)
                            || (LJftype != BJftype))
                        //|| (LfirstCzDate!=BfirstCzDate))
                        {
                            MessageBox.Show("列表中合同不满足捆绑要求的条件！");
                            return false;
                        }
                        if (FirstCzDay2.Day != FirstCzDay1.Day)
                        {
                            MessageBox.Show("列表中合同首次抄张日不满足捆绑要求的条件！");
                            return false;
                        }
                        else
                        {
                            if (((FirstCzDay2.Year - FirstCzDay1.Year) * 12 + (FirstCzDay2.Month - FirstCzDay1.Month)
                                ) % CzPeriod != 0)
                            {
                                MessageBox.Show("列表中合同首次抄张日不满足捆绑要求的条件！");
                                return false;
                            }
                        }


                    }
                    i += 1;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dgv_macs.SelectedRows.Count <= 0) return;

            if (this.dgv_macs.CurrentRow.Index == -1) return;
            if (MessageBox.Show("确认移除当前行?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.OK)
            {
                int index_ = this.dgv_macs.CurrentRow.Index;
                this.dgv_macs.Rows.RemoveAt(index_);
                this.dtmac.AcceptChanges();
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BargainBindAddhctype bh = new BargainBindAddhctype();
            if (bh.ShowDialog() == DialogResult.OK)
            {
                if (isExist_dgv_para(bh.hctype_))
                {
                    MessageBox.Show("已存在该幅面的设置！");
                    return;
                }
                DataRow dr = dtpara.NewRow();
                dr[0] = bh.hctype_;
                dr[1] = bh.myzs_;
                dr[2] = bh.jbzs_;
                dtpara.Rows.Add(dr);
            }
        }

        private bool isExist_dgv_para(string hctype_)
        {
            for (int i = 0; i < this.dgv_para.Rows.Count - 1; i++)
            {
                if (this.dgv_para.Rows[i].Cells[0].Value!=null)
                {
                if (this.dgv_para.Rows[i].Cells["幅面"].Value.ToString().Trim() == hctype_)
                    return true;
                    break;
                }
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dgv_para.SelectedRows.Count <= 0) return;

            if (this.dgv_para.CurrentRow.Index == -1) return;
            if (MessageBox.Show("确认移除当前行?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.OK)
            {
                int index_ = this.dgv_para.CurrentRow.Index;
                this.dgv_para.Rows.RemoveAt(index_);
                //同步更新dtpara
                this.dtpara.AcceptChanges();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string swhere = "";
            for (int k = 0; k < dtmac.Rows.Count; k++)
            { 
               if (k==0)
                   swhere = "where sysid=" + dtmac.Rows[k]["合同系统编号"].ToString().Trim();
               else
                   swhere = swhere + " or sysid=" + dtmac.Rows[k]["合同系统编号"].ToString().Trim();
            }
            if (swhere != "")
            {
                BargainBindVerify bbv = new BargainBindVerify();
                bbv.swheres = swhere;
                bbv.ShowDialog();
            }
        }
    }
}
