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

namespace Warehouse.Modal
{
    public partial class EditReceiptModalDefineForm : Form
    {
        /// <summary>
        /// 是编辑，还是添加
        /// </summary>
        private string type;
        /// <summary>
        /// 单据模板编号
        /// </summary>
        private string receTypeId;
        public delegate void ReceiptModalDefineChange();
        /// <summary>
        /// 用户信息改变 事件
        /// </summary>
        public event ReceiptModalDefineChange receiptModalDefineChange;
        public EditReceiptModalDefineForm(string type, string receTypeId)
        {
            InitializeComponent();

            this.type = type;
            this.receTypeId = receTypeId;
        }

        private void ReceiptModalDefineForm_Load(object sender, EventArgs e)
        {
            InitFuncs initFuncs = new InitFuncs();            

            if (this.type == "edit")
            {
                string strSqlSelect = "select * from T_ReceiptModal where ReceTypeID='{0}'";
                strSqlSelect = string.Format(strSqlSelect, this.receTypeId);

                initFuncs.ShowDatas(this.panelReceiptModal, strSqlSelect);
                this.s_ReceTypeID.ReadOnly = true;
            }
        }
        /// <summary>
        /// 是否临时单据（若是，以下都为否）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void s_istemp_CheckedChanged(object sender, EventArgs e)
        {
            //若是临时单据，其他CheckBox都不可用
            if (this.s_istemp.Checked)
            {
                foreach (Control con in this.panelReceiptModal.Controls)
                {
                    if (con is CheckBox)
                    {
                        if (!(con as CheckBox).Equals(this.s_istemp))
                            (con as CheckBox).Enabled = false;
                    }
                }
            }
            else
            {
                foreach (Control con in this.panelReceiptModal.Controls)
                {
                    if (con is CheckBox)
                    {
                        if (!(con as CheckBox).Equals(this.s_istemp))
                            (con as CheckBox).Enabled = true;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region //验证
            if (this.s_ReceTypeID.Text == "")
            {
                MessageBox.Show("模板编号不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.s_ReceName.Text.Trim() == "")
            {
                MessageBox.Show("模板名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.n_DetailRows.Text.Trim() != "" && !Util.IsNumberic(this.n_DetailRows))
            {
                MessageBox.Show("子表行数输入有误，请输入数值型数据！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
                        
            InitFuncs initFuncs = new InitFuncs();
            DBUtil dbUtil = new DBUtil();
            string strSql = "";
            string strSql_ = "";

            if (this.type == "add")
            {
                //插入之前的判断
                bool isExistKey = dbUtil.Is_Exist_Data("T_ReceiptModal", "ReceTypeID", this.s_ReceTypeID.Text.Trim());
                if (isExistKey)
                {
                    MessageBox.Show("该单据模板已定义！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                strSql_ = "select * from T_ReceiptModal where Ltrim(Rtrim(ReceName))='{0}'";
                strSql_ = string.Format(strSql_, this.s_ReceName.Text.Trim());
                
                //插入
                strSql = initFuncs.Build_Insert_Sql(this.panelReceiptModal, "T_ReceiptModal");                
               
            }
            else if (this.type == "edit")
            {
                //更新
                strSql_ = "select * from T_ReceiptModal where Ltrim(Rtrim(ReceTypeID)) != '{0}' and Ltrim(Rtrim(ReceName))='{1}'";
                strSql_ = string.Format(strSql_, this.s_ReceTypeID.Text.Trim(), this.s_ReceName.Text.Trim());

                string strSqlWhere = "where ReceTypeID='" + this.s_ReceTypeID.Text.Trim() + "'";
                strSql = initFuncs.Build_Update_Sql(this.panelReceiptModal, "T_ReceiptModal", strSqlWhere);
            }
            if (dbUtil.yn_exist_data(strSql_))
            {
                MessageBox.Show("该模板名已存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            (new SqlDBConnect()).ExecuteNonQuery(strSql);

            MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            receiptModalDefineChange(); //激活代理事件，在父窗体中处理
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
