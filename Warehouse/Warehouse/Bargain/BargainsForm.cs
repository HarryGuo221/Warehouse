using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.DAO;
using Warehouse.Sys;
using Warehouse.Base;

namespace Warehouse.Bargain
{
    public partial class BargainsForm : Form
    {
        public string SelectBarSysId = "";   //选中的合同号

        private int curRowIndex = 0;     //选中的行序号
        private int verScrollBar = 0;

        private string StrTjKeyDown = "";  //模糊筛选条件
        private string StrTjGroup = "";    //分组
        private string StrTjToDate = "";   //到期


        List<string> GridColumnOrder= new List<string>();

        //基本SQL语句
        private string BaseSql = "select T_Bargains.sysid,"
                    + "Bargstatus as 状态,"
                    + "ContractType as 合同类型,"
                    + "tb_maintetype.maintetypename as 保修类别,"
                    + "BargId as 合同编号,"
                    + "T_CustomerInf.CustName as 单位名称,T_bargains.Mtype as 机型,"
                    + "T_bargains.Manufactcode as 机号,T_CustomerMac.Mdepart as 机器地址,"
                    + "Ynthree as 第三方合同,Payouttype as 合同分类,"
                    + "ThirdName as 第三方单位,Jftype as 收费或付费,"
                    + "FeeType as 收费类型,"    //"ContractType as 合同类型,"
                    + "StartDate 合同起始日,"
                    + "EndDate as 合同终止日,TerminalNum as 合同终止张数,"
                    + "Endwarry as 保修到期年月,WarrantyCopyNum as 保修复印量,"
                    + "MaintainGap as 上门保养周期,ResponseHour as 响应速度,"
                    + "RenewalFee as 续保费,UseType as 使用类型,"
            //+ "Endwarry as 保修到期年月,CheckPeriod as 核算周期,"
            //+ "BillTitle as 结算单抬头,"
                    + "PreChargeMethod as 预收费方式,"
            //+ "CopyNumGap as 抄张周期,"
                    + " Addtype as 签定类型,"
                    + " PreBargID as 前合同号,"
                    + " SaleUser as 业务员,"
                    + " T_bargains.memo as 备注 "
                    + "from T_Bargains "
                    + " left join T_CustomerMac on T_CustomerMac.sysid= T_Bargains.machineid "
                    + " left join T_CustomerInf on T_CustomerInf.Custid= T_CustomerMac.custid "
                    + " left join tb_maintetype on T_Bargains.contmaintetype= tb_maintetype.maintetypecode ";


        //筛选条件
        private string WhereTj = "";


        private void Refresh_GridColumnOrder()
        {
            this.GridColumnOrder.Clear();
            for (int m=0; m<this.dataGridView1.Columns.Count; m++)
            {
               this.GridColumnOrder.Add(this.dataGridView1.Columns[m].DisplayIndex.ToString());
            }
        }

        private void ReShow_By_OldColumnOrder()
        {
           if (this.GridColumnOrder.Count <= 0) return;
           int k = -1;
           for (int m=0; m<this.dataGridView1.Columns.Count;m++)
           {
               k = Convert.ToInt16(this.GridColumnOrder[m]);
               this.dataGridView1.Columns[m].DisplayIndex=k;
           }
        }

        public BargainsForm()
        {
            InitializeComponent();
        }

        private string Get_WhereTJ()
        {
            string stj = "";
            if (this.StrTjGroup != "")
            {
                if (stj == "")
                    stj = " where (" + this.StrTjGroup + ")";
                else
                    stj = stj + " and (" + this.StrTjGroup + ")";
            }

            if (this.StrTjKeyDown != "")
            {
                if (stj == "")
                    stj = " where (" + this.StrTjKeyDown + ")";
                else
                    stj = stj + " and (" + this.StrTjKeyDown + ")";
            }
            if (this.StrTjToDate != "")
            {
                if (stj == "")
                    stj = " where (" + this.StrTjToDate + ")";
                else
                    stj = stj + " and (" + this.StrTjToDate + ")";
            }
            return stj;
        }

        public void initDataGridview(string sql_)
        {
            //保存显示列的顺序
            Refresh_GridColumnOrder();

            DataTable dt;
            dt = (new SqlDBConnect()).Get_Dt(sql_);
            (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);

            try
            {
                this.dataGridView1.ClearSelection();
                this.dataGridView1.Rows[this.curRowIndex].Selected = true;
                this.toolStripStatusLabel2.Text = (this.dataGridView1.Rows.Count).ToString().Trim();
                this.dataGridView1.FirstDisplayedScrollingRowIndex = this.verScrollBar;
            }
            catch { }
            this.dataGridView1.Columns["sysid"].Visible = false;  //隐藏sysid

            this.dataGridView1.Columns["状态"].Width = 60;
            this.dataGridView1.Columns["单位名称"].Width = 260;
            this.dataGridView1.Columns["机器地址"].Width = 260;
            //按显示列顺序显示
            ReShow_By_OldColumnOrder();
        }

        private void BargainsForm_Load(object sender, EventArgs e)
        {
            //以下语句在数据量大的时候，严重影响速度!
            //this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; 
            this.dataGridView1.AllowUserToAddRows = false;
            this.cb_lx.SelectedIndex = 0;
            this.dateTimePicker1.Value = DBUtil.getServerTime();
        }


        private void 查询附件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 删除合同ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (this.dataGridView1.SelectedRows[0].Cells["合同编号"].Value == null) return;
                string bargid = this.dataGridView1.SelectedRows[0].Cells["合同编号"].Value.ToString().Trim();
                if (bargid == "") return;

                DialogResult dr = MessageBox.Show("确认删除选中合同记录！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    (new BargainDAO()).delete_bargain(bargid);
                    form_editBargainFormChange();
                }
                else
                    return;
            }
        }

        //双击修改合同
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.curRowIndex = this.dataGridView1.SelectedRows[0].Index;
            this.verScrollBar = this.dataGridView1.FirstDisplayedScrollingRowIndex;

        }

        //添加合同
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            EditBargainForm form = new EditBargainForm("add", "");
            form.editBargainFormChange += new EditBargainForm.EditBargainFormChange(form_editBargainFormChange);
            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }
        void form_editBargainFormChange()
        {
            initDataGridview(this.BaseSql + this.WhereTj);
        }

        //修改合同
        private void toolStripButtonModify_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            //this.curRowIndex = this.dataGridView1.SelectedRows[0].Index;
            //this.verScrollBar = this.dataGridView1.FirstDisplayedScrollingRowIndex;
            EditBargainForm form = new EditBargainForm("edit", sysid_);
            form.editBargainFormChange += new EditBargainForm.EditBargainFormChange(form_editBargainFormChange);
            form.ShowDialog();

        }


        //删除合同
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            string sql_ = "";
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;
                string sysid_ = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
                if (sysid_ == "") return;
                DialogResult dr = MessageBox.Show("确认删除选中合同记录！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    List<string> SqlLst = new List<string>();
                    //删除合同计费信息
                    sql_ = "delete from T_BargFee where barsysid=" + sysid_;
                    SqlLst.Add(sql_);
                    //删除合同附件信息
                    sql_ = "delete from T_BargAttach where barsysid=" + sysid_;
                    SqlLst.Add(sql_);
                    //删除合同信息
                    sql_ = "delete from T_Bargains where sysid=" + sysid_;
                    SqlLst.Add(sql_);

                    (new SqlDBConnect()).Exec_Tansaction(SqlLst);

                    form_editBargainFormChange();
                }
                this.dataGridView1.ClearSelection();
            }
        }

        //合同附件
        private void toolStripButtonAttach_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;

            string sysid = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid == "") return;
            BargAttachForm form = new BargAttachForm(sysid);
            form.ShowDialog();
        }



        private void 修改合同ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonModify_Click(sender, e);
        }

        private void 合同附件ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //根据Custid(客户ID),PinYinCode(拼音码),
            //mtype，Manufactcode(机号)来模糊查询
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textBox = sender as TextBox;
                filter_datas(textBox);
                this.WhereTj = Get_WhereTJ();
                this.initDataGridview(this.BaseSql + this.WhereTj);
            }
        }

        private void filter_datas(TextBox textBox)
        {
            this.StrTjKeyDown = "";
            string textBoxName = textBox.Text.Trim();
            //跳字筛选
            textBoxName = textBoxName.Replace(" ", "%");
            //
            if (textBoxName == "") return;
            string swhere = " T_CustomerMac.CustID like '%{0}%'"
                + " or T_CustomerMac.pinyin like '%{1}%' "
                + " or T_bargains.Manufactcode like '%{2}%'"
                + " or T_bargains.mtype like '%{3}%'"
                + " or T_CustomerMac.Mdepart like '%{4}%'"
                + " or T_bargains.bargid like '%{5}%'"
                + " or T_CustomerInf.CustName like '%{6}%'";

            swhere = string.Format(swhere, textBoxName, textBoxName, textBoxName,
                textBoxName, textBoxName, textBoxName, textBoxName);
            this.StrTjKeyDown = swhere;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            filter_datas(this.textBox1);
            this.WhereTj = Get_WhereTJ();
            this.initDataGridview(this.BaseSql + this.WhereTj);
        }

        private void 复制计算模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["合同编号"].Value == null) return;
            string BargId = this.dataGridView1.SelectedRows[0].Cells["合同编号"].Value.ToString().Trim();
            string barsysid = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            this.SelectBarSysId = barsysid;
            MessageBox.Show("已将合同[" + BargId + "(" + this.SelectBarSysId + ")]的计费模式复制！");
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            if (this.SelectBarSysId == "")
            {
                粘贴计算模式ToolStripMenuItem.Enabled = false;
            }
            else
                粘贴计算模式ToolStripMenuItem.Enabled = true;
        }

        private void 粘贴计算模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql_, selsql_ = "";
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["合同编号"].Value == null) return;
            string BargId = this.dataGridView1.SelectedRows[0].Cells["合同编号"].Value.ToString().Trim();
            string barsysid = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (SelectBarSysId == barsysid) return;

            string show_ = "注意：该操作将清除合同[" + BargId + "(" + barsysid + ")]的计算模式"
                + "\n\r" + "确定将合同[(" + this.SelectBarSysId + ")]的计算模式复制到合同[" + BargId + "(" + barsysid + ")]吗?";
            if (MessageBox.Show(show_, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                List<string> SqlLst = new List<string>();
                sql_ = "delete from T_BargFee where barsysid=" + barsysid;
                SqlLst.Add(sql_);

                string hctype_ = "";
                string fee1_, fee2_, fee3_, fee4_, PriceAdd_;
                string PageNumAdd_, MyNum_, BaseNum_, StartNum_;
                DataTable dt;
                selsql_ = "select * from T_BargFee where barsysid=" + this.SelectBarSysId;
                dt = (new SqlDBConnect()).Get_Dt(selsql_);
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    hctype_ = dt.Rows[k]["HcType"].ToString().Trim();
                    if (dt.Rows[k]["fee1"].ToString().Trim() != "")
                        fee1_ = dt.Rows[k]["fee1"].ToString().Trim();
                    else
                        fee1_ = "null";
                    if (dt.Rows[k]["fee2"].ToString().Trim() != "")
                        fee2_ = dt.Rows[k]["fee2"].ToString().Trim();
                    else
                        fee2_ = "null";
                    if (dt.Rows[k]["fee3"].ToString().Trim() != "")
                        fee3_ = dt.Rows[k]["fee3"].ToString().Trim();
                    else
                        fee3_ = "null";
                    if (dt.Rows[k]["fee4"].ToString().Trim() != "")
                        fee4_ = dt.Rows[k]["fee4"].ToString().Trim();
                    else
                        fee4_ = "null";

                    if (dt.Rows[k]["PageNumAdd"].ToString().Trim() != "")
                        PageNumAdd_ = dt.Rows[k]["PageNumAdd"].ToString().Trim();
                    else
                        PageNumAdd_ = "null";

                    if (dt.Rows[k]["PriceAdd"].ToString().Trim() != "")
                        PriceAdd_ = dt.Rows[k]["PriceAdd"].ToString().Trim();
                    else
                        PriceAdd_ = "null";

                    if (dt.Rows[k]["MyNum"].ToString().Trim() != "")
                        MyNum_ = dt.Rows[k]["MyNum"].ToString().Trim();
                    else
                        MyNum_ = "null";

                    if (dt.Rows[k]["BaseNum"].ToString().Trim() != "")
                        BaseNum_ = dt.Rows[k]["BaseNum"].ToString().Trim();
                    else
                        BaseNum_ = "null";

                    if (dt.Rows[k]["StartNum"].ToString().Trim() != "")
                        StartNum_ = dt.Rows[k]["StartNum"].ToString().Trim();
                    else
                        StartNum_ = "null";

                    sql_ = "insert into T_BargFee(barsysid,HcType,Fee1,Fee2,Fee3,Fee4,"
                        + "PageNumAdd,PriceAdd,MyNum,BaseNum,StartNum) values({0},"
                        + "'{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10})";
                    sql_ = string.Format(sql_, barsysid, hctype_, fee1_, fee2_, fee3_, fee4_,
                        PageNumAdd_, PriceAdd_, MyNum_, BaseNum_, StartNum_);
                    SqlLst.Add(sql_);
                }

                try
                {
                    (new SqlDBConnect()).Exec_Tansaction(SqlLst);
                    MessageBox.Show("计算模式粘贴成功！");
                }
                catch
                {
                    MessageBox.Show("计算模式粘贴操作失败！");
                }

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            string sql_ = "";
            if (this.dataGridView1.SelectedRows[0].Cells["合同编号"].Value == null) return;
            string bargid = this.dataGridView1.SelectedRows[0].Cells["合同编号"].Value.ToString().Trim();
            if (bargid == "") return;
            DialogResult dr = MessageBox.Show("确认作废选中合同吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                sql_ = "update T_Bargains set Bargstatus='作废' where BargId='" + bargid + "'";
                try
                {
                    (new SqlDBConnect()).ExecuteNonQuery(sql_);
                    initDataGridview(this.BaseSql + this.WhereTj);
                    MessageBox.Show("作废操作成功!");
                }
                catch
                { }
            }
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_lx_SelectedIndexChanged(object sender, EventArgs e)
        {
            //合同类型
            this.StrTjGroup = "";
            if (cb_lx.SelectedIndex == 0)
            {
                this.StrTjGroup = " T_Bargains.bargstatus like '有效'";
            }
            else if (cb_lx.SelectedIndex == 1)
            {
                this.StrTjGroup = " T_Bargains.bargstatus like '过期'";
            }
            else if (cb_lx.SelectedIndex == 2)
            {
                this.StrTjGroup = " T_Bargains.bargstatus like '作废'";
            }
            else
                this.StrTjGroup = "";

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.StrTjToDate = " T_Bargains.enddate <='" + dateTimePicker1.Value.ToShortDateString() + "'";
            }
            else
                this.StrTjToDate = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WFilter wf = new WFilter(0, "", true);
            wf.strSql = this.BaseSql;
            wf.btnOK.Visible = false;
            wf.s_items.Add("状态,T_bargains.Bargstatus,C");
            wf.s_items.Add("合同类型,T_bargains.ContractType,C");
            wf.s_items.Add("保修类别,tb_maintetype.maintetypename,C");
            wf.s_items.Add("合同编号,T_bargains.BargId,C");
            wf.s_items.Add("单位名称,T_CustomerInf.CustName,C");
            wf.s_items.Add("机型,T_bargains.Mtype,C");
            wf.s_items.Add("机号,T_bargains.Manufactcode,C");
            wf.s_items.Add("机器地址,T_CustomerMac.Mdepart,C");
            wf.s_items.Add("收费类型,T_bargains.FeeType,C");
            wf.s_items.Add("合同起始日,T_bargains.StartDate,C");
            wf.s_items.Add("合同终止日,T_bargains.EndDate,C");
            wf.s_items.Add("合同终止张数,T_bargains.TerminalNum,N");
            wf.s_items.Add("上门保养周期,T_bargains.MaintainGap,N");
            wf.s_items.Add("响应速度,T_bargains.ResponseHour,N");
            wf.s_items.Add("续保费,T_bargains.RenewalFee,N");
            wf.s_items.Add("抄张周期,T_bargains.CopyNumGap,N");
            wf.s_items.Add("签定类型,T_bargains.Addtype,C");
            wf.s_items.Add("业务员,T_bargains.SaleUser,C");
            wf.s_items.Add("备注,T_bargains.memo,C");

            wf.ShowDialog();
            if (wf.DialogResult == DialogResult.OK)
            {
                initDataGridview(wf.Return_Sql);
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0) return;
            if (this.dataGridView1.SelectedRows[0].Cells["sysid"].Value == null) return;
            string sysid_ = this.dataGridView1.SelectedRows[0].Cells["sysid"].Value.ToString().Trim();
            if (sysid_ == "") return;
            string sql_="";
            DialogResult dr = MessageBox.Show("确认续签该合同吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                List<string> SqlLst = new List<string>();
                 sql_ = "select MachineId,BargId,StartDate,EndDate,MaintainGap,"
                    + "ResponseHour,SignedType,ContMaintetype,RenewalFee,CheckPeriod,"
                    + "CopyNumGap,Firstczdate,ForFee,ForeadNum,Periodgap,"
                    + "TerminalNum,Install_date,PreChargeMethod,BillTitle,CustCode,"
                    + "FeeType,Ynthree,ThirdName,Payouttype,Jftype,"
                    + "ContractType,EndWarry,WarrantyCopyNum,UseType,Checkfee,"
                    + "TonerType,Tonerprice,Bargstatus,Mtype,Manufactcode,"
                    + "memo,addtype,preBargId,SaleUser,ServiceResponse,"
                    + "Mainte_endcnt_a,cancelReason"
                    + " from T_Bargains "
                    + " where sysid=" + sysid_;
                DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);

                string MachineId, BargId, StartDate, EndDate, MaintainGap;
                string ResponseHour, SignedType, ContMaintetype, RenewalFee, CheckPeriod;
                string CopyNumGap, Firstczdate, ForFee, ForeadNum, Periodgap;
                string TerminalNum, Install_date, PreChargeMethod, BillTitle, CustCode;
                string FeeType, Jftype;
                string ContractType, EndWarry, WarrantyCopyNum, UseType, Checkfee;
                string TonerType, Tonerprice, Bargstatus, Mtype, Manufactcode;
                string memo, addtype, preBargId, SaleUser, ServiceResponse;
                string Mainte_endcnt_a, cancelReason;

                if (dt.Rows.Count>0)
                {
                    MachineId = Util.Get_Fields_Format(dt.Rows[0]["MachineId"].ToString(), "N");
                    //BargId = Util.Get_Fields_Format(dt.Rows[0]["BargId"].ToString(), "S");
                    BargId = "''";  //续签合同编号为""
                    StartDate = Util.Get_Fields_Format(dt.Rows[0]["StartDate"].ToString(), "S");
                    EndDate = Util.Get_Fields_Format(dt.Rows[0]["EndDate"].ToString(), "S");
                    MaintainGap = Util.Get_Fields_Format(dt.Rows[0]["MaintainGap"].ToString(), "N");
                    ResponseHour = Util.Get_Fields_Format(dt.Rows[0]["ResponseHour"].ToString(), "N");
                    SignedType = Util.Get_Fields_Format(dt.Rows[0]["SignedType"].ToString(), "S");
                    ContMaintetype = Util.Get_Fields_Format(dt.Rows[0]["ContMaintetype"].ToString(), "S");
                    RenewalFee = Util.Get_Fields_Format(dt.Rows[0]["RenewalFee"].ToString(), "N");
                    CheckPeriod = Util.Get_Fields_Format(dt.Rows[0]["CheckPeriod"].ToString(), "N");
                    CopyNumGap = Util.Get_Fields_Format(dt.Rows[0]["CopyNumGap"].ToString(), "N");
                    Firstczdate = Util.Get_Fields_Format(dt.Rows[0]["Firstczdate"].ToString(), "S");
                    ForFee = Util.Get_Fields_Format(dt.Rows[0]["ForFee"].ToString(), "N");
                    ForeadNum = Util.Get_Fields_Format(dt.Rows[0]["ForeadNum"].ToString(), "N");
                    Periodgap = Util.Get_Fields_Format(dt.Rows[0]["Periodgap"].ToString(), "N");
                    TerminalNum = Util.Get_Fields_Format(dt.Rows[0]["TerminalNum"].ToString(), "N");
                    Install_date = Util.Get_Fields_Format(dt.Rows[0]["Install_date"].ToString(), "S");

                    PreChargeMethod = Util.Get_Fields_Format(dt.Rows[0]["PreChargeMethod"].ToString(), "S");
                    BillTitle = Util.Get_Fields_Format(dt.Rows[0]["BillTitle"].ToString(), "S");
                    CustCode = Util.Get_Fields_Format(dt.Rows[0]["CustCode"].ToString(), "S");
                    FeeType = Util.Get_Fields_Format(dt.Rows[0]["FeeType"].ToString(), "S");
                    Jftype = Util.Get_Fields_Format(dt.Rows[0]["Jftype"].ToString(), "S");
                    ContractType = Util.Get_Fields_Format(dt.Rows[0]["ContractType"].ToString(), "S");
                    EndWarry = Util.Get_Fields_Format(dt.Rows[0]["EndWarry"].ToString(), "S");
                    WarrantyCopyNum = Util.Get_Fields_Format(dt.Rows[0]["WarrantyCopyNum"].ToString(), "N");
                    UseType = Util.Get_Fields_Format(dt.Rows[0]["UseType"].ToString(), "S");
                    Checkfee = Util.Get_Fields_Format(dt.Rows[0]["Checkfee"].ToString(), "N");
                    TonerType = Util.Get_Fields_Format(dt.Rows[0]["TonerType"].ToString(), "S");
                    Tonerprice = Util.Get_Fields_Format(dt.Rows[0]["Tonerprice"].ToString(), "N");
                    Bargstatus = Util.Get_Fields_Format(dt.Rows[0]["Bargstatus"].ToString(), "S");
                    Mtype = Util.Get_Fields_Format(dt.Rows[0]["Mtype"].ToString(), "S");
                    Manufactcode = Util.Get_Fields_Format(dt.Rows[0]["Manufactcode"].ToString(), "S");
                    memo = Util.Get_Fields_Format(dt.Rows[0]["memo"].ToString(), "S");
                    //addtype = Util.Get_Fields_Format(dt.Rows[0]["addtype"].ToString(), "S");
                    addtype = "'续签'";
                    preBargId = Util.Get_Fields_Format(dt.Rows[0]["preBargId"].ToString(), "S");
                    SaleUser = Util.Get_Fields_Format(dt.Rows[0]["SaleUser"].ToString(), "S");
                    ServiceResponse = Util.Get_Fields_Format(dt.Rows[0]["ServiceResponse"].ToString(), "S");
                    Mainte_endcnt_a = Util.Get_Fields_Format(dt.Rows[0]["Mainte_endcnt_a"].ToString(), "N");
                    cancelReason = Util.Get_Fields_Format(dt.Rows[0]["cancelReason"].ToString(), "S");
                    sql_ = "insert into T_Bargains("
                        + "MachineId,BargId,StartDate,EndDate,MaintainGap,"
                        + "ResponseHour,SignedType,ContMaintetype,RenewalFee,CheckPeriod,"
                        + "CopyNumGap,Firstczdate,ForFee,ForeadNum,Periodgap,"
                        + "TerminalNum,Install_date,PreChargeMethod,BillTitle,CustCode,"
                        + "FeeType,Jftype,"
                        + "ContractType,EndWarry,WarrantyCopyNum,UseType,Checkfee,"
                        + "TonerType,Tonerprice,Bargstatus,Mtype,Manufactcode,"
                        + "memo,addtype,preBargId,SaleUser,ServiceResponse,"
                        + "Mainte_endcnt_a,cancelReason)"
                        + " values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},"
                        + "{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},"
                        + "{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},"
                        + "{30},{31},{32},{33},{34},{35},{36},{37},{38})";
                    sql_ = string.Format(sql_, MachineId, BargId, StartDate, EndDate, MaintainGap,
                        ResponseHour, SignedType, ContMaintetype, RenewalFee, CheckPeriod,
                        CopyNumGap, Firstczdate, ForFee, ForeadNum, Periodgap,
                        TerminalNum, Install_date, PreChargeMethod, BillTitle, CustCode,
                        FeeType, Jftype,
                        ContractType, EndWarry, WarrantyCopyNum, UseType, Checkfee,
                        TonerType, Tonerprice, Bargstatus, Mtype, Manufactcode,
                        memo, addtype, preBargId, SaleUser, ServiceResponse,
                        Mainte_endcnt_a, cancelReason);
                    SqlLst.Add(sql_);
                    SqlLst.Add("SELECT CAST(scope_identity() AS int)");
                }


               string SqlInsFee="";
                //获取子表信息
                sql_="select HcType,Fee1,Fee2,Fee3,Fee4,"
                    +"PageNumAdd,PriceAdd,MyNum,BaseNum,cbper,"
                    +"NumPerMonth,Memo"
                    +" from T_bargfee where barsysid="+sysid_;
                dt=(new SqlDBConnect()).Get_Dt(sql_);
                string HcType="", Fee1="", Fee2="", Fee3="", Fee4="";
                string PageNumAdd="", PriceAdd="", MyNum="", BaseNum="", cbper="";
                string NumPerMonth="", Memo="";
                   
                if (dt.Rows.Count>0)
                {
                    HcType = Util.Get_Fields_Format(dt.Rows[0]["HcType"].ToString(), "S");
                    Fee1 = Util.Get_Fields_Format(dt.Rows[0]["Fee1"].ToString(), "N");
                    Fee2 = Util.Get_Fields_Format(dt.Rows[0]["Fee2"].ToString(), "N");
                    Fee3 = Util.Get_Fields_Format(dt.Rows[0]["Fee3"].ToString(), "N");
                    Fee4 = Util.Get_Fields_Format(dt.Rows[0]["Fee4"].ToString(), "N");
                    PageNumAdd = Util.Get_Fields_Format(dt.Rows[0]["PageNumAdd"].ToString(), "N");
                    PriceAdd = Util.Get_Fields_Format(dt.Rows[0]["PriceAdd"].ToString(), "N");
                    MyNum = Util.Get_Fields_Format(dt.Rows[0]["MyNum"].ToString(), "N");
                    BaseNum = Util.Get_Fields_Format(dt.Rows[0]["BaseNum"].ToString(), "N");
                     cbper = Util.Get_Fields_Format(dt.Rows[0]["cbper"].ToString(), "N");
                    NumPerMonth = Util.Get_Fields_Format(dt.Rows[0]["NumPerMonth"].ToString(), "N");
                    Memo = Util.Get_Fields_Format(dt.Rows[0]["Memo"].ToString(), "S");
                    SqlInsFee="insert into T_bargfee(barsysid,HcType,Fee1,Fee2,Fee3,Fee4,"
                        +"PageNumAdd,PriceAdd,MyNum,BaseNum,cbper,NumPerMonth,Memo)"
                        +" values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},"
                        +"{10},{11},{12})";
                }
                    //插入主表
                try
                {
                    int RetVal=(new SqlDBConnect()).Exec_Tansaction_ReturnLast(SqlLst);
                    if (RetVal != -1)
                    {
                        if (SqlInsFee != "")
                        {
                            SqlInsFee = string.Format(SqlInsFee, RetVal.ToString().Trim(),
                                HcType, Fee1, Fee2, Fee3, Fee4, PageNumAdd, PriceAdd, MyNum, BaseNum, cbper,
                                NumPerMonth, Memo);
                            try
                            {
                                (new SqlDBConnect()).ExecuteNonQuery(SqlInsFee);
                                this.WhereTj = " where T_bargains.sysid=" + sysid_
                                    + " or T_bargains.sysid=" + RetVal.ToString().Trim();
                                initDataGridview(this.BaseSql + this.WhereTj);
                                MessageBox.Show("续签成功!");
                            }
                            catch
                            {
                                MessageBox.Show("续签失败!");
                            }
                        }
                    }
                    
                }
                catch
                {
                    
                }


            }

        }

    
    }
       

    
}
