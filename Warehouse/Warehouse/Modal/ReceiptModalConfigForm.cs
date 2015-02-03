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

namespace Warehouse.Modal
{
    public partial class ReceiptModalConfigForm : Form
    {
        public ReceiptModalConfigForm()
        {
            InitializeComponent();
        }

        private void ReceiptModalConfigForm_Load(object sender, EventArgs e)
        {
            InitFuncs initFuncs = new InitFuncs();            
            initFuncs.InitComboBox(this.comboBoxReceName, "T_ReceiptModal", "ReceName");
            initFuncs.InitComboBox(this.comboBoxReceiptTypeID, "T_ReceiptModal", "ReceTypeID");            
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 验证
            if (this.comboBoxReceiptTypeID.SelectedIndex == 0)
            {
                MessageBox.Show("请选择单据模板！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.checkBoxReceiptId.Checked == false)
            {
                MessageBox.Show("单据号必须选择！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            string strRTypeID = this.comboBoxReceiptTypeID.Text.Trim();
            
            //插入前先全部删除
            string strSqlDelete = "delete from T_ReceiptModCfg where RTypeID='{0}'";
            strSqlDelete = string.Format(strSqlDelete, strRTypeID);

            List<string> sqls = new List<string>();
            sqls.Add(strSqlDelete);

            Insert(this.listBoxMainTop, ref sqls, strRTypeID);
            Insert(this.listBoxMainBottom, ref sqls, strRTypeID);
            Insert(this.listBoxDetail, ref sqls, strRTypeID);
            
            SqlDBConnect db = new SqlDBConnect();
            db.Exec_Tansaction(sqls);

            MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 插入
        /// </summary>  
        private void Insert(ListBox listBox, ref List<string> sqls, string strRTypeID)
        {
            if (listBox.Items.Count <= 0)
                return;
            
            if (listBox == this.listBoxMainTop)
            {
                foreach (string item in listBox.Items)
                {
                    string strSql = "insert into T_ReceiptModCfg(RTypeID,ShowItem,SummaryOrDetail,UporDown,OrderNum) values('{0}','{1}',{2},{3},{4})";
                    strSql = string.Format(strSql, strRTypeID, item.ToString().Trim(), 0, 0, listBox.Items.IndexOf(item));
                    sqls.Add(strSql); 
                }
            }
            else if (listBox == this.listBoxMainBottom)
            {
                foreach (string item in listBox.Items)
                {
                    string strSql = "insert into T_ReceiptModCfg(RTypeID,ShowItem,SummaryOrDetail,UporDown,OrderNum) values('{0}','{1}',{2},{3},{4})";
                    strSql = string.Format(strSql, strRTypeID, item.ToString().Trim(), 0, 1, listBox.Items.IndexOf(item));
                    sqls.Add(strSql);
                }
            }
            else if (listBox == this.listBoxDetail)
            {
                foreach (string item in listBox.Items)
                {
                    string strSql = "insert into T_ReceiptModCfg(RTypeID,ShowItem,SummaryOrDetail,UporDown,OrderNum) values('{0}','{1}',{2},{3},{4})";
                    strSql = string.Format(strSql, strRTypeID, item.ToString().Trim(), 1, -1, listBox.Items.IndexOf(item));
                    sqls.Add(strSql);
                }
            }
        }
        /// <summary>
        /// 插入 (没有使用)
        /// </summary>        
        private void Insert(Panel panel, ref List<string> sqls, string strRTypeID)
        {            
            DBUtil dbUtil = new DBUtil();
            foreach (Control o in panel.Controls)
            {
                if (o is CheckBox && (o as CheckBox).Checked == true)
                {                   
                    string strSql = "insert into T_ReceiptModCfg(RTypeID,ShowItem) values('{0}','{1}')";
                    strSql = string.Format(strSql, strRTypeID, (o as CheckBox).Text.Trim());
                    sqls.Add(strSql);                    
                }
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.ClearData(this.panelRecMain);
            initFuncs.ClearData(this.panelRecDet);

            this.listBoxMain.Items.Clear();
            this.listBoxMainTop.Items.Clear();
            this.listBoxMainBottom.Items.Clear();
            this.listBoxDetail.Items.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxReceTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string receTypeID = this.comboBoxReceiptTypeID.Text.Trim();
            string receName = (new DBUtil()).Get_Single_val("T_ReceiptModal", "ReceName", "ReceTypeID", receTypeID);

            this.comboBoxReceName.Text = receName;

            //提取已存在的选项
            showItems(receTypeID);
        }

        private void comboBoxReceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string receName = this.comboBoxReceName.Text.Trim();
            string receTypeID = (new DBUtil()).Get_Single_val("T_ReceiptModal", "ReceTypeID", "ReceName", receName);

            this.comboBoxReceiptTypeID.Text = receTypeID;            
        }
        /// <summary>
        /// 根据单据模板编号 绑定显示CheckBox
        /// </summary>
        /// <param name="receTypeID"></param>
        private void showItems(string receTypeID)
        {
            btnClear_Click(null, null);

            DataTable dtMain = ReceiptModalCfgDAO.GetDatasByRTypeIDAndSummaryOrDetail(receTypeID, 0);
            if (dtMain == null || dtMain.Rows.Count <= 0)
                return;

            foreach (DataRow dr in dtMain.Rows)
            {               
                foreach (Control o in this.panelRecMain.Controls) //在主表中查找
                {
                    if (o is CheckBox)
                    {
                        if ((o as CheckBox).Text.Trim() == dr["ShowItem"].ToString().Trim())
                        {
                            (o as CheckBox).Checked = true;                            
                            break;
                        }
                    }
                }                       
            }

            //在子表中查找
            DataTable dtDet = ReceiptModalCfgDAO.GetDatasByRTypeIDAndSummaryOrDetail(receTypeID, 1);
            if (dtDet == null || dtDet.Rows.Count <= 0)
                return;
            foreach (DataRow dr in dtDet.Rows)
            {
                foreach (Control o in this.panelRecDet.Controls) //在子表中查找
                {
                    if (o is CheckBox)
                    {
                        if ((o as CheckBox).Text.Trim() == dr["ShowItem"].ToString().Trim())
                        {
                            (o as CheckBox).Checked = true;
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 响应 单据总表可操作项 CheckBox选择事件
        /// </summary>        
        private void checkBoxMain_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked == true && !this.listBoxMain.Items.Contains(cb.Text.Trim()))
            {
                this.listBoxMain.Items.Add(cb.Text.Trim());
                this.listBoxMain.SelectedItem = cb.Text.Trim();
            }
            else if (cb.Checked == false && this.listBoxMain.Items.Contains(cb.Text.Trim()))
            {
                this.listBoxMain.Items.Remove(cb.Text.Trim());
                //默认选中第一项
                if (this.listBoxMain.Items.Count > 0)
                    this.listBoxMain.SelectedIndex = 0;
            }            
        }
        /// <summary>
        /// listBoxMain添加项时
        /// </summary>
        private void listBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxMain.Items.Count <= 0)
                return;
            string strItem = this.listBoxMain.Items[this.listBoxMain.Items.Count - 1].ToString().Trim();

            string receTypeID = this.comboBoxReceiptTypeID.Text.Trim();
            if (receTypeID == "")
                return;

            DataTable dt = ReceiptModalCfgDAO.GetDatasByRTypeID(receTypeID);
            if (dt == null || dt.Rows.Count <= 0)
                return;

            string sqlWhereMainTop = "where RTypeID='{0}' and ShowItem='{1}' and SummaryOrDetail=0 and UporDown=0";
            sqlWhereMainTop = string.Format(sqlWhereMainTop, receTypeID, strItem);
            bool isExist = ReceiptModalCfgDAO.IsExistData("T_ReceiptModCfg", sqlWhereMainTop);
            if (isExist)
            {
                this.listBoxMainTop.Items.Add(strItem);
                this.listBoxMain.Items.Remove(strItem);
            }
            string sqlWhereMainButtom = "where RTypeID='{0}' and ShowItem='{1}' and SummaryOrDetail=0 and UporDown=1";
            sqlWhereMainButtom = string.Format(sqlWhereMainButtom, receTypeID, strItem);
            bool isExistButtom = ReceiptModalCfgDAO.IsExistData("T_ReceiptModCfg", sqlWhereMainButtom);
            if (isExistButtom)
            {
                this.listBoxMainBottom.Items.Add(strItem);
                this.listBoxMain.Items.Remove(strItem);
            }
        }
        /// <summary>
        /// 响应 单据子表可操作项 CheckBox选择事件
        /// </summary>   
        private void checkBoxDetail_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked == true && !this.listBoxDetail.Items.Contains(cb.Text.Trim()))
                this.listBoxDetail.Items.Add(cb.Text.Trim());
            else if (cb.Checked == false && this.listBoxDetail.Items.Contains(cb.Text.Trim()))
                this.listBoxDetail.Items.Remove(cb.Text.Trim());

            //默认选中第一项
            if (this.listBoxDetail.Items.Count > 0)
                this.listBoxDetail.SelectedIndex = 0;
        }
        /// <summary>
        /// 单据总表项 → 单据上部项 //← ↑↓
        /// </summary>
        private void btnMainToMainTop_Click(object sender, EventArgs e)
        {
            if (this.listBoxMain.Items.Count <= 0 || this.listBoxMain.SelectedItem == null)
                return;
            this.listBoxMainTop.Items.Add(this.listBoxMain.SelectedItem);
            this.listBoxMain.Items.Remove(this.listBoxMain.SelectedItem);

            //默认选中第一项
            if (this.listBoxMain.Items.Count > 0)
                this.listBoxMain.SelectedIndex = 0;
            if (this.listBoxMainTop.Items.Count > 0)
                this.listBoxMainTop.SelectedIndex = 0;
        }
        /// <summary>
        /// 单据上部项 → 单据总表项
        /// </summary>
        private void btnMainTopToMain_Click(object sender, EventArgs e)
        {
            this.listBoxMain.SelectedIndexChanged -= listBoxMain_SelectedIndexChanged;//不触发listBoxMain的SelectedIndexChanged事件

            if (this.listBoxMainTop.Items.Count <= 0 || this.listBoxMainTop.SelectedItem == null)
                return;
            this.listBoxMain.Items.Add(this.listBoxMainTop.SelectedItem);
            this.listBoxMainTop.Items.Remove(this.listBoxMainTop.SelectedItem);

            //默认选中第一项
            if (this.listBoxMain.Items.Count > 0)
                this.listBoxMain.SelectedIndex = 0;
            if (this.listBoxMainTop.Items.Count > 0)
                this.listBoxMainTop.SelectedIndex = 0;

            this.listBoxMain.SelectedIndexChanged += new EventHandler(listBoxMain_SelectedIndexChanged);//恢复listBoxMain的SelectedIndexChanged事件
        }
        /// <summary>
        /// 单据总表项 → 单据下部项 
        /// </summary>
        private void btnMainToMainBottom_Click(object sender, EventArgs e)
        {
            if (this.listBoxMain.Items.Count <= 0 || this.listBoxMain.SelectedItem == null)
                return;
            this.listBoxMainBottom.Items.Add(this.listBoxMain.SelectedItem);
            this.listBoxMain.Items.Remove(this.listBoxMain.SelectedItem);

            //默认选中第一项
            if (this.listBoxMain.Items.Count > 0)
                this.listBoxMain.SelectedIndex = 0;
            if (this.listBoxMainBottom.Items.Count > 0)
                this.listBoxMainBottom.SelectedIndex = 0;
        }
        /// <summary>
        /// 单据下部项 → 单据总表项
        /// </summary>
        private void btnMainBottomToMain_Click(object sender, EventArgs e)
        {
            if (this.listBoxMainBottom.Items.Count <= 0 || this.listBoxMainBottom.SelectedItem == null)
                return;
            this.listBoxMain.Items.Add(this.listBoxMainBottom.SelectedItem);
            this.listBoxMainBottom.Items.Remove(this.listBoxMainBottom.SelectedItem);

            //默认选中第一项
            if (this.listBoxMain.Items.Count > 0)
                this.listBoxMain.SelectedIndex = 0;
            if (this.listBoxMainBottom.Items.Count > 0)
                this.listBoxMainBottom.SelectedIndex = 0;
        }
        /// <summary>
        /// 向上移动按钮
        /// </summary>
        /// <param name="listBox"></param>
        private void btnUp(ListBox listBox)
        {
            if (listBox.Items.Count <= 1 || listBox.SelectedItem == null || listBox.SelectedIndex == 0)
                return;
            string selectItem = listBox.SelectedItem.ToString();
            int selectItemIndex = listBox.SelectedIndex;

            listBox.Items.Remove(selectItem);
            listBox.Items.Insert(selectItemIndex - 1, selectItem);//

            //仍旧选中当前项
            listBox.SelectedIndex = selectItemIndex - 1;
        }
        /// <summary>
        /// 向下移动按钮
        /// </summary>
        /// <param name="listBox"></param>
        private void btnDown(ListBox listBox)
        {
            if (listBox.Items.Count <= 1 || listBox.SelectedItem == null || listBox.SelectedIndex == listBox.Items.Count - 1)
                return;
            string selectItem = listBox.SelectedItem.ToString();
            int selectItemIndex = listBox.SelectedIndex;

            listBox.Items.Remove(selectItem);
            listBox.Items.Insert(selectItemIndex + 1, selectItem);//

            //仍旧选中当前项
            listBox.SelectedIndex = selectItemIndex + 1;
        }
        private void btnMainTopUp_Click(object sender, EventArgs e)
        {
            btnUp(this.listBoxMainTop);
        }

        private void btnMainTopDown_Click(object sender, EventArgs e)
        {
            btnDown(this.listBoxMainTop);
        }

        private void btnMainBottomUp_Click(object sender, EventArgs e)
        {
            btnUp(this.listBoxMainBottom);
        }

        private void btnMainBottomDown_Click(object sender, EventArgs e)
        {
            btnDown(this.listBoxMainBottom);
        }

        private void btnDetailUp_Click(object sender, EventArgs e)
        {
            btnUp(this.listBoxDetail);
        }

        private void btnDetailDown_Click(object sender, EventArgs e)
        {
            btnDown(this.listBoxDetail);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
              

    }
}
