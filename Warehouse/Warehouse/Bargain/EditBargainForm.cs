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
using Warehouse.DAO;

namespace Warehouse.Bargain
{
    public partial class EditBargainForm : Form
    {
        public EditBargainForm()
        {
            InitializeComponent();
        }
        
        public delegate void EditBargainFormChange();
        public event EditBargainFormChange editBargainFormChange;

        private string Type;  //新增,修改
        private string BargSysId;    //合同系统编号

        private string custid_;  //客户编号

        public EditBargainForm(string type, string bargId)
        {
            InitializeComponent();
            this.Type = type;
            this.BargSysId = bargId;

        }


        void EditBargFeeForm_FeeChange()
        {
            show_fee_lst(this.BargSysId);
        }

        private void disEnableTextBox(bool sta_)
        {
            foreach (Control ob in this.panel1.Controls)
                {
                    if (ob is TextBox)
                    { 
                       if (((TextBox)ob).Top>220)
                           ((TextBox)ob).Enabled = sta_;
                    }
                    if (ob is ComboBox)
                    {
                        if (((ComboBox)ob).Top > 220)
                            ((ComboBox)ob).Enabled = sta_;
                    }
                    if (ob is DateTimePicker)
                    {
                        if (((DateTimePicker)ob).Top > 220)
                            ((DateTimePicker)ob).Enabled = sta_;
                    }
                }
            
        }

        private void BargainForm_Load(object sender, EventArgs e)
        {
            //限制值类型文本框的输入 
            (new InitFuncs()).Num_limited(this.panel1);

            s_SaleUser.KeyDown+=new KeyEventHandler(InfoFind.UserName_KeyDown);
            
            // 合同:无码数据初始化
            InitFuncs inf = new InitFuncs();
            //inf.InitComboBox(this.s_ResponseHour, "[合同]响应速度");
            inf.InitComboBox(this.t_ContMaintetype, "[合同]签约类型");

            //区域:有码数据初始化
            inf.InitComboBox(this.t_UseType, "TB_machusedtype", "usedname");  //区域
            inf.InitComboBox(this.t_ContMaintetype, "tb_maintetype", "maintetypename");  //保修类别
           
            //合同:有码数据初始化
            s_Jftype.SelectedIndex = 0;
            s_ContractType.SelectedIndex = 0;
            t_ContMaintetype.SelectedIndex = 0;
            s_FeeType.SelectedIndex = 0;
            s_PreChargeMethod.SelectedIndex = 0;
            s_Payouttype.SelectedIndex = 0;

           
            if (this.Type == "add")
            {
                this.s_Bargstatus.SelectedIndex = 1;
                s_BargId.ReadOnly = false;
                this.panel3.Enabled = false; //不允许设置相关物料
                //this.panel4.Enabled = false;
            }
            else if (this.Type == "edit")
            {
                this.panel3.Enabled = true;
                //this.panel4.Enabled = true;

                string sel_sql = "select * from T_Bargains where sysid=" + this.BargSysId ;
                inf.ShowDatas(this.panel1, sel_sql);
                //以下两行必须在ShowDatas后
                t_CustCode.Text = (new DBUtil()).Get_Single_val("T_CustomerInf", "custname", "custid", s_CustCode.Text.Trim());
                this.custid_ = s_CustCode.Text.Trim();
                this.t_UseType.Text = (new DBUtil()).Get_Single_val("TB_machusedtype", "usedname", "used_type", this.s_UseType.Text.Trim());
                this.t_ContMaintetype.Text = (new DBUtil()).Get_Single_val("tb_maintetype", "maintetypename", "maintetypecode", this.s_ContMaintetype.Text.Trim());
                
                this.t_CustCode.Text = (new DBUtil()).Get_Single_val("T_CustomerMac", "Mdepart", "sysid", this.n_MachineId.Text.Trim());
                
                //显示计费用信息
                show_fee_lst(this.BargSysId);
            }
        }

        private void show_fee_lst(string barid_)
       {
            DataTable dt = new DataTable();
            string select_sql = "select T_BargFee.HcType 幅面,"
                +"T_BargFee.StartNum as 初始读数," 
                + "T_BargFee.BaseNum 基本印量,T_BargFee.Fee1 保修期内基本印量内单张收费,"
                + "T_BargFee.fee2 保修期外基本印量内单张收费 ,T_BargFee.fee3 保修期内基本印量外单张收费,"
                +"T_BargFee.fee4 保修期外基本印量外单张收费,T_BargFee.PageNumAdd 张数递增量,"
                + " T_BargFee.PriceAdd 单价递增量,T_BargFee.MyNum as 免印张数,"
                +" T_BargFee.NumPerMonth 月印量,T_BargFee.cbper as 考核成本,"
                +" T_BargFee.Memo as 备注 "
                + " from T_BargFee where barsysid='" + barid_ + "'";
            dt = (new SqlDBConnect()).Get_Dt(select_sql);
            (new InitFuncs()).InitDataGridView(this.dgv_bargFee, dt);
            
            //this.dgv_bargFee.ClearSelection();
            //this.dgv_bargFee.Rows[this.curRowIndex].Selected = true;
        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            DBUtil dbUtil = new DBUtil();
            if (this.s_ContractType.Enabled == true)
            {
                if (Util.ControlTextIsNUll(this.s_ContractType))
                {
                    MessageBox.Show("请选择合同类型！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.s_ContractType.Focus();
                    return;
                }
            }
            if (Util.ControlTextIsNUll(this.s_Addtype))
            {
                MessageBox.Show("请选择签定类型！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_Addtype.Focus();
                return;
            }

            if (Util.ControlTextIsNUll(this.s_BargId))
            {
                MessageBox.Show("合同编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.s_BargId.Focus();
                return;
            }

            if (Util.ControlTextIsNUll(this.s_CustCode))
            {
                MessageBox.Show("客户信息必选！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            t_CustCode_Leave(null, null);

            if (this.s_ContractType.Text.Trim() == "全包")
            {
                if (Util.ControlTextIsNUll(this.s_FeeType))
                {
                    MessageBox.Show("收费类型必选！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.s_FeeType.Focus();
                    return;
                }

                if (Util.ControlTextIsNUll(this.n_Periodgap))
                {
                    MessageBox.Show("合同期时间(月）必须输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.n_Periodgap.Focus();
                    return;
                }


                if (Util.ControlTextIsNUll(this.n_CheckPeriod))
                {
                    MessageBox.Show("核算周期必须输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.n_CheckPeriod.Focus();
                    return;
                }
                if (Util.ControlTextIsNUll(this.n_CopyNumGap))
                {
                    MessageBox.Show("抄张周期必须输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.n_CopyNumGap.Focus();
                    return;
                }

                //如果有预收费
                if (this.s_PreChargeMethod.SelectedIndex==1)
                {
                    if (Util.ControlTextIsNUll(this.n_ForeadNum))
                    {
                      MessageBox.Show("预收费周期必须输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                      this.n_ForeadNum.Focus();
                      return;
                    }
                    if (Util.ControlTextIsNUll(this.n_ForFee))
                    {
                        MessageBox.Show("周期性收费金额必须输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.n_ForFee.Focus();
                        return;
                    }

                }

            }
            try
            {
                if (this.Type == "add")
                {
                    List<string> SqlLst = new List<string>();
                    
                    string strSqlInsert = (new InitFuncs()).Build_Insert_Sql(this.panel1, "T_Bargains");
                    SqlLst.Add(strSqlInsert);
                    string sql_ = "SELECT CAST(scope_identity() AS int)";
                    SqlLst.Add(sql_);

                    try
                    {
                        Int32 retv=(new SqlDBConnect()).Exec_Tansaction_ReturnLast(SqlLst);
                        if (retv != -1)
                        {
                            MessageBox.Show("添加成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.custid_ = s_CustCode.Text.Trim();
                            editBargainFormChange();
                            this.BargSysId = retv.ToString().Trim();
                            //MessageBox.Show(this.BargSysId);
                            this.panel3.Enabled = true;
                            this.Type = "edit";
                        }
                        //this.DialogResult = DialogResult.OK;
                        ////允许设置物料和计费
                        
                        
                    }
                    catch
                    { }

                }
                else if (this.Type == "edit")
                {
                    string strSqlUpdate = (new InitFuncs()).Build_Update_Sql(this.panel1, "T_Bargains", " where  sysid=" + this.BargSysId );
                    
                    //执行
                    (new SqlDBConnect()).ExecuteNonQuery(strSqlUpdate);
                    MessageBox.Show("更新成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //(new BargainsForm()).initDataGridview();
                    this.custid_ = s_CustCode.Text.Trim(); 
                    editBargainFormChange();
                    //this.DialogResult = DialogResult.OK;
                }


            }
            catch
            {
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        
        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        
        

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.n_Periodgap.Text.Trim() == "")
            {
                MessageBox.Show("请输入合同期时间（月）!");
                return;
            }
            if (this.dgv_bargFee.SelectedRows.Count > 0)
            {
                if (this.dgv_bargFee.SelectedRows[0].Cells["幅面"].Value == null) return;

                string hctyp_ = this.dgv_bargFee.SelectedRows[0].Cells["幅面"].Value.ToString().Trim();
                if (hctyp_.Trim() == "") return;
                EditBargFeeForm ebf = new EditBargFeeForm();
                ebf.htyears = Convert.ToInt16(this.n_Periodgap.Text.Trim());
                ebf.Type = "edit";
                ebf.BargId = this.s_BargId.Text.Trim();
                ebf.Jftype = this.s_Jftype.Text.Trim();
                ebf.hctype = hctyp_;
                ebf.BarSysId = this.BargSysId;
                ebf.FeeChange_ += new EditBargFeeForm.FeeChange(EditBargFeeForm_FeeChange);
                ebf.ShowDialog();
            }
            
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (this.dgv_bargFee.SelectedRows.Count > 0)
            {
                if (this.dgv_bargFee.SelectedRows[0].Cells["幅面"].Value == null) return;
                string hcty_ = this.dgv_bargFee.SelectedRows[0].Cells["幅面"].Value.ToString().Trim();
                if (hcty_ == "") return;
                
                DialogResult dr = MessageBox.Show("确认删除该项计费信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    string sql_ = "delete from T_bargFee where barsysid=" + this.BargSysId + " and hctype='" + hcty_+"'";
                    (new SqlDBConnect()).ExecuteNonQuery(sql_);
                    EditBargFeeForm_FeeChange();
                }

            }
        }

        private void s_ContractType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (s_ContractType.SelectedIndex==0)  //"年包"
            {
                s_FeeType.Enabled = false;
                n_Periodgap.Enabled = false;
                s_Endwarry.Enabled = false;
                t_UseType.Enabled = false;
                n_TerminalNum.Enabled = false;
                n_WarrantyCopyNum.Enabled = false;
                
                n_CheckPeriod.Enabled = false;
                n_CopyNumGap.Enabled = false;
                
                s_PreChargeMethod.Enabled = false;
                n_ForeadNum.Enabled = false;
                
               
                
                s_BillTitle.Enabled = false;
                n_Checkfee.Enabled = false;
                s_TonerType.Enabled = false;
                n_Tonerprice.Enabled = false;
                s_Jftype.Enabled = false;
                n_ForFee.Enabled = false;
            }
            else
            {
                #region //显示
                s_FeeType.Enabled = true;
                n_Periodgap.Enabled = true;
                s_Endwarry.Enabled = true;
                t_UseType.Enabled = true;
                n_TerminalNum.Enabled = true;
                n_WarrantyCopyNum.Enabled = true;
               
                n_CheckPeriod.Enabled = true;
                n_CopyNumGap.Enabled = true;
                n_ForeadNum.Enabled = true;
                
                s_PreChargeMethod.Enabled = true;
                n_ForeadNum.Enabled = true;
                
                s_PreChargeMethod.Enabled = true;
               
                s_BillTitle.Enabled = true;
                s_Jftype.Enabled = true;
                n_ForFee.Enabled = true;
                #endregion
            }

        }

        private void s_PreChargeMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        
        private void s_FeeType_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //if (s_FeeType.SelectedIndex==1)   // "按基本金额收费"
            //{
            //    n_baseFeeMonth.Enabled = true;
            //}
            //else
            //{
            //    n_baseFeeMonth.Enabled = false;
               
            //}

            if (s_FeeType.SelectedIndex == 2)   // "按鼓收费"
            {
                n_Checkfee.Enabled = true;
                s_TonerType.Enabled = true;
                n_Tonerprice.Enabled = true;
            }
            else
            {
                n_Checkfee.Enabled = false;
                s_TonerType.Enabled = false;
                n_Tonerprice.Enabled = false;
            }

       }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.n_Periodgap.Text.Trim() == "")
            {
                MessageBox.Show("请输入合同期时间（月）!");
                return;
            }
            EditBargFeeForm ebf = new EditBargFeeForm(); 
            ebf.Type = "add";
            ebf.BargId = this.s_BargId.Text.Trim();
            ebf.htyears = Convert.ToInt16(this.n_Periodgap.Text.Trim());
            ebf.BarSysId = this.BargSysId;
            ebf.Jftype = this.s_Jftype.Text.Trim();
            ebf.FeeChange_ +=new EditBargFeeForm.FeeChange(EditBargFeeForm_FeeChange);
            ebf.ShowDialog();
        }
       
        
        private void t_CustCode_Leave(object sender, EventArgs e)
        {
            //if (this.t_CustCode.Text.Trim() == "") return;
            //if (!InitFuncs.isRightValue(t_CustCode, "T_customerMac", "Mdepart"))
            //{
            //    MessageBox.Show("该客户机器不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    this.t_CustCode.Focus();
            //    this.s_CustCode.Clear();
            //    return;
            //}
        }

        private void EditBargainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           

            
        }

        private void t_UseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_UseType.Text = (new DBUtil()).Get_Single_val("TB_machusedtype", "used_type", "usedname", this.t_UseType.Text.Trim());
        }

        private void s_CustCode_TextChanged(object sender, EventArgs e)
        {
            //this.s_mtype.Text = "";
            //this.s_Manufactcode.Text = "";
            //this.n_MachineId.Text = "";
        }

        private void t_CustCode_KeyDown(object sender, KeyEventArgs e)
        {
            //根据Custid(客户ID),CustName(客户名),PinYinCode(拼音码),
            //mtype，Manufactcode(机号)来模糊查询
            if (e.KeyCode == Keys.Enter)
            {
                //如果有抄张记录了,则不允许改
                if (this.Type == "edit")
                {
                    string sql_ = "select * from T_RecordCopy where barsysid=" + this.BargSysId ;
                    if ((new DBUtil()).yn_exist_data(sql_))
                    {
                        MessageBox.Show("该合同已有对应的抄张记录,不允许修改客户信息!");
                        return;
                    }
                }
                //

                string BaseSql = "select T_CustomerMac.Sysid as sysid,"
                + "T_CustomerInf.CustID as 客户编码,"
                +"T_CustomerInf.CustName as 客户名称,"
                + "T_CustomerMac.Mdepart as 机器地址,"
                + "T_CustomerMac.Mtype as 机型,"
                + "T_CustomerMac.Manufactcode as 机号"
                + " from T_CustomerMac "
                + " left Join T_CustomerInf on T_CustomerInf.CustID=T_CustomerMac.CustID";
          
                TextBox textBox = sender as TextBox;
                string textBoxName = textBox.Text.Trim();
                if (textBoxName == "") return;
                string swhere = " where T_CustomerInf.CustID like '%{0}%'"
                    + " or CustName like '%{1}%' or PinYinCode like '%{2}%'"
                    + " or (T_CustomerMac.sysid in "
                    + "(select sysid from T_CustomerMac where "
                    + " T_CustomerMac.Manufactcode like '%{3}%' "
                    + " or T_CustomerMac.mtype like '%{4}%'))";
         
                swhere = string.Format(swhere, textBoxName, textBoxName, textBoxName, textBoxName, textBoxName);
               
                //显示列表
                DataTable dt = (new SqlDBConnect()).Get_Dt(BaseSql + swhere);
                FilterInfo_ReturnGrid fr = new FilterInfo_ReturnGrid(dt);
                fr.unVisible_Column_index_ = 0;
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    if (fr.dr_.Cells["sysid"].Value == null) return;
                    if (fr.dr_.Cells["sysid"].Value.ToString().Trim() == "") return;
                    this.s_CustCode.Text = fr.dr_.Cells["客户编码"].Value.ToString();
                    this.t_CustCode.Text = fr.dr_.Cells["机器地址"].Value.ToString();
                    this.n_MachineId.Text = fr.dr_.Cells["sysid"].Value.ToString();
                    this.s_mtype.Text = fr.dr_.Cells["机型"].Value.ToString();
                    this.s_Manufactcode.Text = fr.dr_.Cells["机号"].Value.ToString();
                }
               
            }
        }

        private void t_ContMaintetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_ContMaintetype.Text = (new DBUtil()).Get_Single_val("tb_maintetype", "maintetypecode", "maintetypename", this.t_ContMaintetype.Text.Trim());
        }

        private void s_FeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.s_FeeType.SelectedIndex == 1)
            {
                //s_PreChargeMethod.SelectedIndex = 2;
                n_ForeadNum.Enabled = false;
                n_ForFee.Enabled = false;
            }
            else
            {
                n_ForeadNum.Enabled = true;
                n_ForFee.Enabled = true;
            }
        }

        private void s_ContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (s_ContractType.SelectedIndex == 2)
                disEnableTextBox(true);
            else
                disEnableTextBox(false);
        }
   
    }
}
