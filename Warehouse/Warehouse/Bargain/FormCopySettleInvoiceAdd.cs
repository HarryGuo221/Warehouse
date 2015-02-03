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
    public partial class FormCopySettleInvoiceAdd : Form
    {
        public delegate void FormCopySettleInvoiceChange();
        public event FormCopySettleInvoiceChange FormCopySettleInvoiceChange_;

        public string OpaMethod; //开票人操作="0",结算登记开票人操作="1"

        public string type;  //"add" or "edit"
        public string Rid;    //传入的单据编号

        public string SqlUpdateStatus;  //传入的更新抄张表ynjs的SQL

        public string custName;   //客户名
        public string sids;       //结算表中的sysid
        public string lx;  //结算 、预付

        public double totmoney = 0;   //开票总金额

        public string curUserName;
        
        DataTable dtcom;
            

        public FormCopySettleInvoiceAdd()
        {
            InitializeComponent();
            //this.dgv_tmp.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) { };
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void show_det(string rid)
        {
            string sql_ = "select groups,T_StoreHouse.shName as kpcorp,"
                +"T_Invoice.itname as Itype,"
                 +"Ititle ,"
                + "Icontent,Iprice,Inum,Imoney,"
                + "Memo,SaleUser,Techuser "
                +" from T_SettleAccountDet "
                + " left join T_Invoice on T_SettleAccountDet.Itype=T_Invoice.itcode "
                + " left join T_StoreHouse on T_SettleAccountDet.kpcorp=T_StoreHouse.shid "
                + "where rid='" + rid + "'";
            dtcom = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_tmp.DataSource = dtcom.DefaultView;
            this.dgv_tmp.Columns[0].HeaderText = "分组";
            this.dgv_tmp.Columns[0].DataPropertyName = "Groups";
            this.dgv_tmp.Columns[1].HeaderText = "所属公司";
            this.dgv_tmp.Columns[1].DataPropertyName = "kpcorp";
            this.dgv_tmp.Columns[2].HeaderText = "发票种类";
            this.dgv_tmp.Columns[2].DataPropertyName = "Itype";
            this.dgv_tmp.Columns[3].HeaderText = "发票抬头";
            this.dgv_tmp.Columns[3].DataPropertyName = "Ititle";
            this.dgv_tmp.Columns[4].HeaderText = "发票内容";
            this.dgv_tmp.Columns[4].DataPropertyName = "Icontent";
            this.dgv_tmp.Columns[5].HeaderText = "单价";
            this.dgv_tmp.Columns[5].DataPropertyName = "Iprice";
            this.dgv_tmp.Columns[6].HeaderText = "数量";
            this.dgv_tmp.Columns[6].DataPropertyName = "Inum";
            this.dgv_tmp.Columns[7].HeaderText = "发票金额";
            this.dgv_tmp.Columns[7].DataPropertyName = "Imoney";
            this.dgv_tmp.Columns[8].HeaderText = "技术员";
            this.dgv_tmp.Columns[8].DataPropertyName = "Techuser";
            this.dgv_tmp.Columns[9].HeaderText = "业务员";
            this.dgv_tmp.Columns[9].DataPropertyName = "SaleUser";
            this.dgv_tmp.Columns[10].HeaderText = "备注";
            this.dgv_tmp.Columns[10].DataPropertyName = "Memo";
            
        }

        private void FormCopySettleInvoiceAdd_Load(object sender, EventArgs e)
        {
            if (this.OpaMethod == "0")
            {
                this.btn_New.Visible = false;
                this.btn_Del.Visible = false;
                this.btn_Change.Visible = false;
                this.btn_Save.Visible = false;
                this.btn_toSale.Visible = true;
            }
            else
            {
                this.btn_New.Visible = true;
                this.btn_Del.Visible = true;
                this.btn_Change.Visible = true;
                this.btn_Save.Visible = true;
                this.btn_toSale.Visible = false;
            }

            this.dgv_tmp.AllowUserToAddRows = false;
            string sql_ = "";
            dtcom = new DataTable("tb_tmp");
            ////构造该有多少个核算记录行
            dtcom.Columns.Add("Groups", Type.GetType("System.String"));
            dtcom.Columns.Add("kpcorp", Type.GetType("System.String"));
            dtcom.Columns.Add("Itype", Type.GetType("System.String"));
            dtcom.Columns.Add("Ititle", Type.GetType("System.String"));
            dtcom.Columns.Add("Icontent", Type.GetType("System.String"));
            dtcom.Columns.Add("Iprice", Type.GetType("System.Double"));
            dtcom.Columns.Add("Inum", Type.GetType("System.Double"));
            dtcom.Columns.Add("Imoney", Type.GetType("System.Double"));
            dtcom.Columns.Add("Techuser", Type.GetType("System.String"));
            dtcom.Columns.Add("SaleUser", Type.GetType("System.String"));
            dtcom.Columns.Add("Memo", Type.GetType("System.String"));
            this.dgv_tmp.DataSource = dtcom.DefaultView;
            this.dgv_tmp.Columns[0].HeaderText = "分组";
            this.dgv_tmp.Columns[0].DataPropertyName = "Groups";
            this.dgv_tmp.Columns[1].HeaderText = "所属公司";
            this.dgv_tmp.Columns[1].DataPropertyName = "kpcorp";
            this.dgv_tmp.Columns[2].HeaderText = "发票种类";
            this.dgv_tmp.Columns[2].DataPropertyName = "Itype";
            this.dgv_tmp.Columns[3].HeaderText = "发票抬头";
            this.dgv_tmp.Columns[3].DataPropertyName = "Ititle";
            this.dgv_tmp.Columns[4].HeaderText = "发票内容";
            this.dgv_tmp.Columns[4].DataPropertyName = "Icontent";
            this.dgv_tmp.Columns[5].HeaderText = "单价";
            this.dgv_tmp.Columns[5].DataPropertyName = "Iprice";
            this.dgv_tmp.Columns[6].HeaderText = "数量";
            this.dgv_tmp.Columns[6].DataPropertyName = "Inum";
            this.dgv_tmp.Columns[7].HeaderText = "发票金额";
            this.dgv_tmp.Columns[7].DataPropertyName = "Imoney";
            this.dgv_tmp.Columns[8].HeaderText = "技术员";
            this.dgv_tmp.Columns[8].DataPropertyName = "Techuser";
            this.dgv_tmp.Columns[9].HeaderText = "业务员";
            this.dgv_tmp.Columns[9].DataPropertyName = "SaleUser";
            this.dgv_tmp.Columns[10].HeaderText = "备注";
            this.dgv_tmp.Columns[10].DataPropertyName = "Memo";
            
            this.n_total.Text = this.totmoney.ToString("F2");
            if (this.type == "add")
            {
                this.s_custname.Text = this.custName;
                this.s_ids.Text = this.sids;
                this.s_lx.Text = this.lx;
                
                this.s_operaUsr.Text = this.curUserName;
                this.s_occurTime.Text = DBUtil.getServerTime().ToString();
            }
            else
            {
                sql_ = "select * from T_SettleAccountMain where rid='" + this.Rid + "'";
                (new InitFuncs()).ShowDatas(this.panel1, sql_);
                show_det(this.Rid);
            
            }
     
        }

        
        /// <summary>
        /// 设置DataGridView列单元格的样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
        }
       
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string sql_="";
            string swhere = "";
            string ReceiptId;  //单据号
            string SqlUpdateBillRull = "";
            string Itype = "", Ititle = "", Icontent = "", Imoney = "", Imemo = "", Igroup = "",Isaleuser="";
            string Ikpcorp = "",Iprice="",Inum=""; 
            string Itech = "";
            string bargsys_ = "";
            string sqlupdate_ynjs ="";
            string idlst = this.s_ids.Text;
            int pos_=-1;
            while (idlst.IndexOf(";") != -1)
            { 
                pos_=idlst.IndexOf(";");
                bargsys_ = idlst.Substring(0, pos_);
                idlst = idlst.Substring(pos_+1, idlst.Length - pos_-1);
                if (swhere == "")
                    swhere = " where (sysid=" + bargsys_ + ") ";
                else
                    swhere = swhere + " or (sysid=" + bargsys_ + ") ";
            }
            if (s_lx.Text == "结算开票")
            {
                sqlupdate_ynjs = "update T_MacSettle set iskp=1,kpMoney=totmoney " + swhere;
            }
            else
            {
                sqlupdate_ynjs = "update T_MacSettlePrePay set iskp=1 " + swhere;
            }
            //MessageBox.Show(sqlupdate_ynjs);
            this.s_memo.Focus();

            if (this.dgv_tmp.Rows.Count <= 0)
            {
                MessageBox.Show("请输入<发票抬头>、<内容>、<金额>等明细内容后保存！");
                return;
            }

            //求合计金额
            double tot=0;
            for (int i = 0; i < this.dgv_tmp.Rows.Count; i++)
            {
               if ((this.dgv_tmp.Rows[i].Cells[7].Value!=null)
                   &&(this.dgv_tmp.Rows[i].Cells[7].Value.ToString().Trim()!=""))
                tot = tot + Convert.ToDouble(this.dgv_tmp.Rows[i].Cells[7].Value.ToString().Trim());
            }

            
            if (Util.Is_Equal(Convert.ToDouble(n_total.Text.Trim()),tot)==false)
           //if ((Convert.ToDouble(n_total.Text.Trim()).ToString("F2")
            //    != tot.ToString("F2")))
            {
                MessageBox.Show("开票总金额和明细合计金额不相等，请检查!");
                return;
            }
            //

            List<string> SqlLst = new List<string>();

            if (this.type == "add")  //******新增的情况
            {
                //产生单据号,更新单据规则
                ReceiptId = DBUtil.Produce_Bill_Id("CR", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull);
                SqlLst.Add(SqlUpdateBillRull);

                //插入主表信息
                this.s_Rid.Text = ReceiptId;
                sql_ = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_SettleAccountMain");
                SqlLst.Add(sql_);

                //插入子表

                for (int i = 0; i < this.dgv_tmp.Rows.Count; i++)
                {
                    if (this.dgv_tmp.Rows[i].Cells[1].Value != null)
                    {
                        if (this.dgv_tmp.Rows[i].Cells[0].Value == null)
                            Igroup = "";
                        else
                            Igroup = this.dgv_tmp.Rows[i].Cells[0].Value.ToString().Trim();


                        if (this.dgv_tmp.Rows[i].Cells[1].Value == null)
                            Ikpcorp = "";
                        else
                        {
                            Ikpcorp = this.dgv_tmp.Rows[i].Cells[1].Value.ToString().Trim();
                            //将名称转换成编码保存
                            Ikpcorp = (new DBUtil()).Get_Single_val("T_StoreHouse", "Shid", "shname", Ikpcorp);
                        }

                        if (this.dgv_tmp.Rows[i].Cells[2].Value == null)
                            Itype = "";
                        else
                        {
                            Itype = this.dgv_tmp.Rows[i].Cells[2].Value.ToString().Trim();
                            //将名称转换成编码保存
                            Itype = (new DBUtil()).Get_Single_val("T_Invoice", "itcode", "ITName", Itype);
                        }
                        Ititle = this.dgv_tmp.Rows[i].Cells[3].Value.ToString().Trim();

                        if (this.dgv_tmp.Rows[i].Cells[4].Value == null)
                            Icontent = "";
                        else
                            Icontent = this.dgv_tmp.Rows[i].Cells[4].Value.ToString().Trim();

                        if (this.dgv_tmp.Rows[i].Cells[5].Value == null)
                            Iprice = "null";
                        else
                            Iprice = this.dgv_tmp.Rows[i].Cells[5].Value.ToString().Trim();

                        if (this.dgv_tmp.Rows[i].Cells[6].Value == null)
                            Inum = "null";
                        else
                            Inum = this.dgv_tmp.Rows[i].Cells[6].Value.ToString().Trim();


                        if (this.dgv_tmp.Rows[i].Cells[7].Value == null)
                            Imoney = "null";
                        else
                            Imoney = this.dgv_tmp.Rows[i].Cells[7].Value.ToString().Trim();

                        if (this.dgv_tmp.Rows[i].Cells[8].Value == null)
                            Itech = "";
                        else
                            Itech = this.dgv_tmp.Rows[i].Cells[8].Value.ToString().Trim();

                        if (this.dgv_tmp.Rows[i].Cells[9].Value == null)
                            Isaleuser = "";
                        else
                            Isaleuser = this.dgv_tmp.Rows[i].Cells[9].Value.ToString().Trim();

                        if (this.dgv_tmp.Rows[i].Cells[10].Value == null)
                            Imemo = "";
                        else
                            Imemo = this.dgv_tmp.Rows[i].Cells[10].Value.ToString().Trim();

                       
                        sql_ = "insert into T_SettleAccountDet(rid,Itype,ITitle,IContent,Imoney,Memo,groups,saleuser,Techuser,kpcorp,Iprice,Inum)"
                           + " values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}',{10},{11})";
                        sql_ = string.Format(sql_, ReceiptId, Itype, Ititle, Icontent, Imoney, Imemo, Igroup, Isaleuser, Itech, Ikpcorp, Iprice, Inum);
                        SqlLst.Add(sql_);
                    }
                }
                SqlLst.Add(sqlupdate_ynjs);  //更新T_MacSettle的iskp

            }
            else  //******修改的情况
            {
                swhere = " where rid='" + this.Rid + "'";
                sql_ = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_SettleAccountMain", swhere);
                SqlLst.Add(sql_);

                //删除子表
                sql_ = "delete from T_SettleAccountDet where Rid='" + this.Rid + "'";
                SqlLst.Add(sql_);

                //插入子表
                for (int i = 0; i < this.dgv_tmp.Rows.Count; i++)
                {
                    if (this.dgv_tmp.Rows[i].Cells[1].Value != null)
                    {
                        if (this.dgv_tmp.Rows[i].Cells[0].Value == null)
                            Igroup = "";
                        else
                            Igroup = this.dgv_tmp.Rows[i].Cells[0].Value.ToString().Trim();


                        if (this.dgv_tmp.Rows[i].Cells[1].Value == null)
                            Ikpcorp = "";
                        else
                        {
                            Ikpcorp = this.dgv_tmp.Rows[i].Cells[1].Value.ToString().Trim();
                            //将名称转换成编码保存
                            Ikpcorp = (new DBUtil()).Get_Single_val("T_StoreHouse", "Shid", "shname", Ikpcorp);
                         }

                        if (this.dgv_tmp.Rows[i].Cells[2].Value == null)
                            Itype = "";
                        else
                        {
                            Itype = this.dgv_tmp.Rows[i].Cells[2].Value.ToString().Trim();
                            //将名称转换成编码保存
                            Itype = (new DBUtil()).Get_Single_val("T_Invoice", "itcode", "ITName", Itype);
                      
                        }

                        Ititle = this.dgv_tmp.Rows[i].Cells[3].Value.ToString().Trim();
                        if (this.dgv_tmp.Rows[i].Cells[4].Value == null)
                            Icontent = "";
                        else
                            Icontent = this.dgv_tmp.Rows[i].Cells[4].Value.ToString().Trim();
                        
                        if (this.dgv_tmp.Rows[i].Cells[5].Value == null)
                            Iprice = "null";
                        else
                            Iprice = this.dgv_tmp.Rows[i].Cells[5].Value.ToString().Trim();
                        
                        if (this.dgv_tmp.Rows[i].Cells[6].Value == null)
                            Inum = "null";
                        else
                            Inum = this.dgv_tmp.Rows[i].Cells[6].Value.ToString().Trim();


                        if (this.dgv_tmp.Rows[i].Cells[7].Value == null)
                            Imoney = "null";
                        else
                            Imoney = this.dgv_tmp.Rows[i].Cells[7].Value.ToString().Trim();

                        if (this.dgv_tmp.Rows[i].Cells[8].Value == null)
                            Itech = "";
                        else
                            Itech = this.dgv_tmp.Rows[i].Cells[8].Value.ToString().Trim();

                        if (this.dgv_tmp.Rows[i].Cells[9].Value == null)
                            Isaleuser = "";
                        else
                            Isaleuser = this.dgv_tmp.Rows[i].Cells[9].Value.ToString().Trim();

                        if (this.dgv_tmp.Rows[i].Cells[10].Value == null)
                            Imemo = "";
                        else
                            Imemo = this.dgv_tmp.Rows[i].Cells[10].Value.ToString().Trim();

                       
                        sql_ = "insert into T_SettleAccountDet(rid,Itype,ITitle,IContent,Imoney,Memo,groups,saleuser,Techuser,kpcorp,Iprice,Inum)"
                        + " values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}',{10},{11})";
                        sql_ = string.Format(sql_, this.Rid, Itype, Ititle, Icontent, Imoney, Imemo, Igroup, Isaleuser, Itech, Ikpcorp, Iprice, Inum);
                        SqlLst.Add(sql_);
                    }
                }
                SqlLst.Add(sqlupdate_ynjs);  //更新T_recordcopy的ynjs
            }

            //以事务的方式执行SQL
            try
            {
                (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                if (this.s_lx.Text.Trim()=="预收开票")
                  FormCopySettleInvoiceChange_();
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
            }
            //
            //
        }

       
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.dgv_tmp.SelectedRows[0].IsNewRow == true) return;
            int index_ = this.dgv_tmp.SelectedRows[0].Index;
            if ((index_==-1)||(index_==this.dgv_tmp.Rows.Count)) return;

            if (MessageBox.Show("确认删除当前行?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                == DialogResult.OK)
            {
                this.dgv_tmp.Rows.RemoveAt(index_);
                this.dtcom.AcceptChanges();
            }
        }

        private void FormCopySettleInvoiceAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void dgv_tmp_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if (e.Control is DataGridViewComboBoxEditingControl)
            //{
            //    #region ComboBox列
            //    //检测列
            //    DataGridView dgv = (DataGridView)sender;
            //    if ((dgv.CurrentCell.OwningColumn.HeaderText == "发票种类")
            //        )
            //    {
            //        //取得可以编辑被表示的控件 
            //        DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)e.Control;
            //        cb.DropDownStyle = ComboBoxStyle.DropDown;
            //    }
            //    #endregion
            //}
        }

        void kpchang_()
        {
          this.show_det(this.Rid);
        }

        //计算开票余额
        private double Get_Money_Remain()
        {
            double tot = Convert.ToDouble(this.n_total.Text.Trim());
            for (int i = 0; i < dtcom.Rows.Count; i++)
            {
                tot = tot - Convert.ToDouble(dtcom.Rows[i]["Imoney"].ToString().Trim());
            }
            return tot;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //计算开票余额

            FormCopySettleInvoiceAdddet fca = new FormCopySettleInvoiceAdddet();
            fca.JSD_sysids = this.s_ids.Text;  //多个结算单的sysid
            fca.JSLX = this.s_lx.Text.Trim();
            //fca.Imoney = Get_Money_Remain().ToString("N2");

            if (fca.ShowDialog() == DialogResult.OK)
            {
                DataRow dr = dtcom.NewRow();
                if (fca.t_kpcorp.Text.Trim() != "--请选择--")
                    dr["kpcorp"] = fca.t_kpcorp.Text.Trim();
                else
                    dr["kpcorp"] = "";

                if (fca.t_Itype.Text.Trim() != "--请选择--")
                    dr["Itype"] = fca.t_Itype.Text.Trim();
                else
                    dr["Itype"] = "";

                dr["Ititle"] = fca.s_Ititle.Text.Trim();
                dr["Icontent"] = fca.s_Icontent.Text.Trim();
                dr["Iprice"] = fca.n_Iprice.Text.Trim();
                dr["Inum"] = fca.n_Inum.Text.Trim();
                dr["Imoney"] = fca.n_Imoney.Text.Trim();
                dr["Techuser"] = fca.s_Techuser.Text.Trim();
                dr["SaleUser"] = fca.s_SaleUser.Text.Trim();
                dr["Memo"] = fca.s_Memo.Text.Trim();
                dr["groups"] = fca.s_groups.Text.Trim();
                dtcom.Rows.Add(dr);
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (this.dgv_tmp.SelectedRows.Count<=0) return;
            
            int index_ = this.dgv_tmp.SelectedRows[0].Index;
            if ((index_==-1)||(index_==this.dgv_tmp.Rows.Count)) return;

            FormCopySettleInvoiceAdddet fca = new FormCopySettleInvoiceAdddet();
            fca.JSD_sysids = this.s_ids.Text;  //多个结算单的sysid
            fca.JSLX = this.s_lx.Text.Trim();

            fca.kpcorp = dtcom.Rows[index_]["kpcorp"].ToString().Trim();  //所属公司
            fca.Itype = dtcom.Rows[index_]["Itype"].ToString().Trim();    //发票类型
            fca.Ititle= dtcom.Rows[index_]["Ititle"].ToString().Trim();
            fca.Icontent = dtcom.Rows[index_]["Icontent"].ToString().Trim();
            fca.Iprice = dtcom.Rows[index_]["Iprice"].ToString().Trim();
            fca.Inum = dtcom.Rows[index_]["Inum"].ToString().Trim();
            fca.Imoney= dtcom.Rows[index_]["Imoney"].ToString().Trim();
            fca.Itech = dtcom.Rows[index_]["Techuser"].ToString().Trim();
            fca.IsaleUser= dtcom.Rows[index_]["SaleUser"].ToString().Trim();
            fca.Imemo= dtcom.Rows[index_]["Memo"].ToString().Trim();
            fca.Igroup= dtcom.Rows[index_]["groups"].ToString().Trim();
            
            if (fca.ShowDialog() == DialogResult.OK)
            {

                if (fca.t_kpcorp.Text.Trim() != "--请选择--")
                    dtcom.Rows[index_]["kpcorp"]= fca.t_kpcorp.Text.Trim();
                else
                    dtcom.Rows[index_]["kpcorp"] = "";

                if (fca.t_Itype.Text.Trim() != "--请选择--")
                    dtcom.Rows[index_]["Itype"]= fca.t_Itype.Text.Trim();
                else
                    dtcom.Rows[index_]["Itype"]="";

                dtcom.Rows[index_]["Ititle"] = fca.s_Ititle.Text.Trim();
                dtcom.Rows[index_]["Icontent"] = fca.s_Icontent.Text.Trim();
                dtcom.Rows[index_]["Iprice"] = fca.n_Iprice.Text.Trim();
                dtcom.Rows[index_]["Inum"] = fca.n_Inum.Text.Trim();
                dtcom.Rows[index_]["Imoney"] = fca.n_Imoney.Text.Trim();
                dtcom.Rows[index_]["Techuser"] = fca.s_Techuser.Text.Trim();
                dtcom.Rows[index_]["SaleUser"] = fca.s_SaleUser.Text.Trim();
                dtcom.Rows[index_]["Memo"] = fca.s_Memo.Text.Trim();
                dtcom.Rows[index_]["Groups"] = fca.s_groups.Text.Trim();
            }
        }

       

       
    }
}
