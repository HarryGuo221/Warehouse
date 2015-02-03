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
using Warehouse.Base;

namespace Warehouse.Stock
{
    public partial class MatSplitMainForm : Form
    {
        public delegate void MatSplitMainFormChange();
        /// <summary>
        /// 拆分机器信息改变 事件
        /// </summary>
        public event MatSplitMainFormChange matSplitMainFormChange;

        private string type;
        private string strReceiptId;//拆件单据编号
        private string curUserName;//登录用户名

        public MatSplitMainForm(string type, string strReceiptId, string userName)
        {
            InitializeComponent();
            this.type = type;
            this.strReceiptId = strReceiptId;
            this.curUserName = userName;
        }

        private void MatSplitMainForm_Load(object sender, EventArgs e)
        {
            InitFuncs initFuncs = new InitFuncs();
            string strWhere = "select distinct MatName from T_Mat_Rela,T_MatInf where T_Mat_Rela.ParentMatID=T_MatInf.MatID";
            initFuncs.InitComboBox(this.comboBoxMatName, "MatName", strWhere, 1);
            this.txtCurUserName.Text = this.curUserName;//获取当前登陆用户名
            initFuncs.InitComboBox(this.comboBoxMatType, "[物料]被拆物料类型");
            this.s_OpaUser.Text = (new DBUtil()).Get_Single_val("T_Users", "UserId", "UserName", this.curUserName.Trim());

            if (this.type == "edit")
            {
                //this.comboBoxOperateType.Text = Util.GetOperateTypeName(this.OperateType);
                this.comboBoxMatName.Items.Clear();//编辑时，不允许修改被拆物料
                this.s_ReceiptId.ReadOnly = true;                
                ShowDatas();
            }            
        }

        private void ShowDatas()
        {
            string strSql = "select * from T_MatSplit_Main where ReceiptId='{0}'";
            strSql = string.Format(strSql, strReceiptId);

            (new InitFuncs()).ShowDatas(this.panel1, strSql);
            DBUtil dbUtil = new DBUtil();

            string matName = dbUtil.Get_Single_val("T_MatInf", "MatName", "MatID", this.s_MatID.Text.Trim());
            this.comboBoxMatName.Items.Add("--请选择--");
            this.comboBoxMatName.Items.Add(matName);
            this.comboBoxMatName.Text = matName;
            this.comboBoxMatType.Text = Util.GetMatTypeName(Convert.ToInt32(this.n_MatType.Text.ToString().Trim()));
            this.txtCurUserName.Text = dbUtil.Get_Single_val("T_Users", "UserName", "UserID", this.s_OpaUser.Text.Trim());
                       
        }

        private void btnFindMat_Click(object sender, EventArgs e)
        {
            WFilter wf = new WFilter(0, "MatName", true);
            wf.tableName = "T_MatInf";    //表名  
            wf.strSql = "select distinct T_MatInf.MatID 物料编号,MatName 物料名称," +
                        "Specifications 型号规格 ,Units 计量单位,Brand 品牌,ProductType 产品种类," +
                        "ColorType 色彩,Speed 速度等级,Format 幅面," +
                        "ynStopProduct 是否已停产,ConfigType 配置类型,purchaseprice 标准进货价 " +
                        "from T_MatInf,T_Mat_Rela "+
                        " where T_Mat_Rela.ParentMatID=T_MatInf.MatID ";

            wf.s_items.Add("物料编号,MatID,C");       
            wf.s_items.Add("物料名称,MatName,C");
            wf.s_items.Add("品牌,Brand,C");
            wf.s_items.Add("产品种类,ProductType,C");
            wf.s_items.Add("标准进货价,purchaseprice,N");
            wf.s_items.Add("配置类型,ConfigType,C");
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                if (wf.Return_Items.Count <= 0)
                    return;
                this.comboBoxMatName.Text = wf.Return_Items[0].Trim();
            }
        }      

        private void btnOK_Click(object sender, EventArgs e)
        {
            #region 验证
            if (this.s_ReceiptId.Text.Trim() == "")
            {
                MessageBox.Show("请输入拆件单据编号！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
          
            if (this.comboBoxMatName.SelectedIndex == 0 || this.comboBoxMatName.Text.Trim() == "")
            {
                MessageBox.Show("请选择被拆物料！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.comboBoxMatType.SelectedIndex == 0 || this.comboBoxMatType.Text.Trim() == "")
            {
                MessageBox.Show("请选择被拆物料类型！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion

            SqlDBConnect db = new SqlDBConnect();
            InitFuncs initFuncs = new InitFuncs();
            DBUtil dbUtil = new DBUtil();
            this.s_OccurTime.Format = DateTimePickerFormat.Short;
            this.s_OccurTime.Value = DBUtil.getServerTime();            

            if (this.type == "add")
            {
                //插入之前判断
                string strSqlSel = "select * from T_MatSplit_Main where ReceiptId='{0}'";
                strSqlSel = string.Format(strSqlSel, this.s_ReceiptId.Text.Trim());
                bool isExist = dbUtil.yn_exist_data(strSqlSel);

                if (isExist)
                {
                    MessageBox.Show("已存在该拆件单据！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    List<string> sqls = new List<string>();
                    //插入拆件管理主表
                    string strSqlInsert = initFuncs.Build_Insert_Sql(this.panel1, "T_MatSplit_Main");
                    sqls.Add(strSqlInsert);

                    db.Exec_Tansaction(sqls);
                    MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    matSplitMainFormChange(); //激活代理事件，在MatSplitForm中处理
                    this.Close();
                }
            }
            else if (this.type == "edit")
            {
                //更新       
                string strWhere = "where ReceiptId='{0}'";
                strWhere = string.Format(strWhere, this.s_ReceiptId.Text.Trim());
                string strSqlUpdate = initFuncs.Build_Update_Sql(this.panel1, "T_MatSplit_Main", strWhere);
                db.ExecuteNonQuery(strSqlUpdate);

                MessageBox.Show("修改成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                matSplitMainFormChange(); //激活代理事件，在MatSplitForm中处理
                this.Close();
            }            
           
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxMatName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.s_MatID.Text = (new DBUtil()).Get_Single_val("T_MatInf", "MatID", "MatName", this.comboBoxMatName.Text.Trim());
        }

        private void comboBoxMatType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.n_MatType.Text = Util.GetMatType(this.comboBoxMatType.Text.Trim()).ToString();
        }

        private void MatSplitMainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.Control_keypress(e);
        }
    }
}
