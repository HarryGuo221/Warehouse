using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Warehouse.DB;
using System.Data;
using Warehouse.DAO;

namespace Warehouse.Base
{
    public struct MatInfo
    {
        public string receiptId; //单据号
        public string SStoreHId; //仓库
        public int orderNo; //顺序号
        public string matId; //物料编码
        public int matType; //物料类型
        public double num; //数量
        public double notVerNum; //当前未核销数量
        public double price; //单价
    }

    public struct StockStatus
    {
        /// <summary>
        /// 当前库存
        /// </summary>
        public double stockNum; //当前库存
        /// <summary>
        /// 期初数量
        /// </summary>
        public double firstCount; //期初数量
        /// <summary>
        /// 本期收入数量
        /// </summary>
        public double stockInCount; //收入数量
        /// <summary>
        /// 本期发出数量
        /// </summary>
        public double stockOutCount; //发出数量      
    }

    class ReceiptModCfg
    {          
        /// <summary>
        /// 构造函数
        /// </summary>
        public ReceiptModCfg()
        {            
        }
        /// <summary>
        /// 获得单据总表中的对应项
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetReceiptMainItems()
        {
            Dictionary<string, string> showItems = new Dictionary<string, string>();

            #region 设置总表对应关系
            showItems.Add("单据号", "s_ReceiptId");
            showItems.Add("单据日期", "s_OccurTime");
            showItems.Add("自定义单号", "s_CustomerReceiptNo");            
            showItems.Add("当前工作年月", "s_CurWorkMonth");
            showItems.Add("客户单位", "s_CustID");            
            showItems.Add("订货类型", "s_OrderType");
            showItems.Add("预计入库日", "s_predictArrivalDate");
            showItems.Add("发票类型", "s_InvoiceType");
            showItems.Add("用户发票签收", "s_ynReceiveInvoice");
            showItems.Add("发票号", "s_InvoiceNo");
            showItems.Add("仓库", "s_SourceStoreH");
            showItems.Add("目标仓库", "s_DestStoreH");
            showItems.Add("单据发生原因", "s_OccurReasons");
            showItems.Add("生效起日", "s_StartDate");
            showItems.Add("有效止日", "s_EndDate");
            showItems.Add("不含税金额", "n_NoTaxAccount");
            showItems.Add("税额", "n_Tax");
            showItems.Add("（折）扣率", "n_Discount");
            showItems.Add("提货方式", "s_DeliveryMethod");
            showItems.Add("送货日期", "s_DeliveryDate");
            showItems.Add("送货人", "s_DeliveryPerson");
            showItems.Add("收货日期", "s_ReceiptDate");
            showItems.Add("收货人", "s_ReceiptPerson");
            showItems.Add("业务员", "s_Salesman");
            showItems.Add("技术员", "s_technician");
            showItems.Add("验收员", "s_VerifyPerson");
            showItems.Add("保管员", "s_Keeper");
            showItems.Add("制单人", "s_BillUser");
            showItems.Add("收款人", "s_accountPerson");
            showItems.Add("复核员", "s_CheckPerson");
            showItems.Add("支票号", "s_InvoiceNO");
            showItems.Add("备注", "s_Memo");
            showItems.Add("开票抬头", "s_BillingTitle");
            showItems.Add("账号", "s_AccountNo");
            showItems.Add("税号", "s_TaxNo");
            showItems.Add("出库性质", "s_OutStoreType");
            showItems.Add("区域", "s_Area");
            showItems.Add("销售信息", "s_SaleInf");
            showItems.Add("是否送货", "s_ynDelivery ");
            showItems.Add("送货要求到达时间", "s_mustArriveTime");
            showItems.Add("是否安装", "s_ynInstall");
            showItems.Add("要求安装时间", "s_MustInstallTime");
            showItems.Add("市场支援费", "s_MarketFee");
            showItems.Add("运费", "s_trafficFee");
            showItems.Add("材料费", "s_MaterialFee");
            showItems.Add("汇款日期", "s_remitDate");
            showItems.Add("用户账期", "s_OffPeriod");
            #endregion

            return showItems;
        }
        /// <summary>
        /// 获得单据子表中的对应项
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetReceiptDetailItems()
        {
            Dictionary<string, string> showItems = new Dictionary<string, string>();
            #region 设置子表对应关系
            showItems.Add("单据号", "s_ReceiptId");
            showItems.Add("物料编码", "s_MatId");
            showItems.Add("类型", "n_MatType");
            showItems.Add("单价", "n_price");
            showItems.Add("数量", "n_num");
            showItems.Add("金额", "n_Amount");
            showItems.Add("税率", "n_TaxRat");
            showItems.Add("不含税金额", "n_NotTax");
            showItems.Add("税额", "n_Tax");
            showItems.Add("箱号", "s_BoxNo");
            showItems.Add("批号", "s_lotCode");
            showItems.Add("批数量", "n_LotNum");
            showItems.Add("成本单价", "n_STaxPurchPrice");
            showItems.Add("标准进货价格（不含税）", "n_NotaxPurchPrice");
            showItems.Add("成本总价", "n_TTaxPurchPrice");
            showItems.Add("本次进货底价（含税）核算", "n_PurchLowestPrice");
            showItems.Add("本单可折扣金额", "n_CanDiscountTot");
            showItems.Add("本单已折扣金额", "n_DiscountedTot");
            showItems.Add("折扣订单号", "s_DiscountedOrderNo");
            showItems.Add("折扣属性", "s_DiscountAttr");
            showItems.Add("计价返利方式", "s_RebateCalcMethod");
            showItems.Add("90单金额是否为返利", "s_Yncalc90");
            showItems.Add("订货性质", "s_OrderType");
            showItems.Add("保卡对应客户", "s_WarrantyUser");
            showItems.Add("保卡是否返回", "s_YnReturnWC");
            #endregion

            return showItems;
        }

        /// <summary>
        /// 获得单据模板中某一个控件
        /// </summary>
        /// <param name="panelMain"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Control GetControlByName(Panel panelMain, string controlType, string controlName)
        {            
            foreach (Control con in panelMain.Controls)
            {               
                if (con is Panel)
                {
                    foreach (Control o in (con as Panel).Controls)
                    {
                        if (controlType == "TextBox")
                        {
                            if (o is TextBox && (o as TextBox).Name == controlName)
                            {
                                return o as TextBox;
                            }
                        }
                        else if (controlType == "ComboBox")
                        {
                            if (o is ComboBox && (o as ComboBox).Name == controlName)
                            {
                                return o as ComboBox;
                            }
                        }
                        else if (controlType == "DateTimePicker")
                        {
                            if (o is DateTimePicker && (o as DateTimePicker).Name == controlName)
                            {
                                return o as DateTimePicker;
                            }
                        }
                    }
                }
               
            }

            return null;
        }
        /// <summary>
        /// 清空单据中控件的值
        /// </summary>
        /// <param name="panelMain"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static void ClearControOfReceipt(Panel panelMain)
        {
            foreach (Control con in panelMain.Controls)
            {
                if (con is Panel)
                {
                    foreach (Control o in (con as Panel).Controls)
                    {
                        if (o is TextBox)
                        {
                            (o as TextBox).Text = "";
                        }
                        else if (o is ComboBox)
                        {
                            (o as ComboBox).SelectedIndex = 0;
                        }
                    }
                }
            }
        }

        #region 生成将Panel中已绑定字段控件中的数据插入到表tab_中的SQL语句，返回SQL语句
        /// <summary>
        /// 生成将Panel中已绑定字段控件中的数据插入到表tab_中的SQL语句，返回SQL语句
        /// 如：用户名的TextBox，Name属性命名为s_name
        /// </summary>
        /// <param name="Pa"></param>
        /// <param name="tab_">要插入的数据表_表名</param>
        /// <returns>返回相应SQL语句</returns>
        public static string Build_Insert_Sql_Panel(Panel Pa, string tab_)
        {
            string sql_ = "insert into " + tab_;
            string fiels = "", vals = "";
            string stmp = "", fiel_ = "", val_ = "", lx_ = "";
            foreach (Control control in Pa.Controls)
            {
                if (control is Panel)
                {
                    foreach (Control o in (control as Panel).Controls)
                    {
                        string str = "";
                        if (o is Label)
                            str = o.Text.Trim();
                        fiel_ = "";

                        if (o is TextBox)  //处理TextBox
                        {
                            if (((TextBox)o).Text.Trim().Length == 0) continue;  //未填入值，不处理
                            stmp = ((TextBox)o).Name.Trim().ToLower();
                            lx_ = stmp.Substring(0, 2);
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理


                            if (lx_ == "s_")
                            {
                                if (fiel_ == "password")//如果是密码框的话，加密存储
                                {
                                    val_ = "'" + Util.GetMD5str(((TextBox)o).Text.Trim()) + "'";
                                }
                                else
                                    val_ = "'" + ((TextBox)o).Text.Trim() + "'";
                            }
                            else
                                val_ = ((TextBox)o).Text.Trim();
                        }

                        if (o is ComboBox)  //处理ComboBox
                        {
                            stmp = ((ComboBox)o).Name.Trim().ToLower();
                            lx_ = stmp.Substring(0, 2);
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                            if (((ComboBox)o).SelectedIndex == 0)
                            {
                                val_ = "";
                                fiel_ = "";
                            }
                            else
                                val_ = ((ComboBox)o).Text.Trim();

                            if (lx_ == "s_")
                                val_ = "'" + val_ + "'";

                        }
                        if (o is CheckBox)  //只处理存储为true,false的情况
                        {
                            stmp = ((CheckBox)o).Name.Trim().ToLower();
                            lx_ = stmp.Substring(0, 2);
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                            if (((CheckBox)o).Checked)
                                val_ = "true";
                            else
                                val_ = "false";

                            if (lx_ == "s_")
                                val_ = "'" + val_ + "'";
                        }
                        if (o is DateTimePicker)
                        {
                            stmp = ((DateTimePicker)o).Name.Trim().ToLower();
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            val_ = "'" + ((DateTimePicker)o).Value + "'";
                        }

                        //构造Sql局部
                        if (fiel_ != "")
                        {
                            if (fiels.Length == 0)
                                fiels = fiels + fiel_;
                            else
                                fiels = fiels + "," + fiel_;

                            if (vals.Length == 0)
                                vals = vals + val_;
                            else
                                vals = vals + "," + val_;
                        }
                    }
                }
                if (control is TableLayoutPanel)
                {
                    foreach (Control o in (control as TableLayoutPanel).Controls)
                    {                        
                        if (o is TextBox)  //处理TextBox
                        {
                            if (((TextBox)o).Text.Trim().Length == 0) continue;  //未填入值，不处理
                            stmp = ((TextBox)o).Name.Trim().ToLower();
                            lx_ = stmp.Substring(0, 2);
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                            if (lx_ == "s_")
                                val_ = "'" + ((TextBox)o).Text.Trim() + "'";
                            else
                                val_ = ((TextBox)o).Text.Trim();
                        }
                    }
                }

            }

            sql_ = sql_ + "(" + fiels + ") values(" + vals + ")";
            return sql_;
        }
        #endregion

        #region 生成将Panel中已绑定字段控件中的数据更新到表tab_中的SQL语句，返回SQL语句
        /// <summary>
        /// 生成将Panel中已绑定字段控件中的数据更新到表tab_中的SQL语句，返回SQL语句
        /// </summary>
        /// <param name="Pa"></param>
        /// <param name="tab_">要更新的数据表_表名</param>
        /// //swhere为Where条件
        public static string Build_Update_Sql(Panel Pa, string tab_, string swhere)
        {
            string sql_ = "";
            string sqltmp_ = "", stmp = "", fiel_ = "", val_ = "", lx_ = "";

            foreach (Control con in Pa.Controls)
            {
                if (con is Panel)
                    foreach (Control o in (con as Panel).Controls)
                    {
                        fiel_ = "";

                        if (o is TextBox) //处理TextBox
                        {
                            stmp = ((TextBox)o).Name.Trim().ToLower();
                            lx_ = stmp.Substring(0, 2);
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                            if (((TextBox)o).Text.Trim().Length == 0)
                                val_ = "null";
                            else
                            {
                                if (lx_ == "s_")
                                {
                                    if (fiel_ == "password")//如果是密码框的话，加密存储
                                    {
                                        val_ = "'" + Util.GetMD5str(((TextBox)o).Text.Trim()) + "'";
                                    }
                                    else
                                        val_ = "'" + ((TextBox)o).Text.Trim() + "'";
                                }
                                else
                                {
                                    val_ = ((TextBox)o).Text.Trim();
                                }
                            }
                        }
                        if (o is ComboBox)  //处理ComboBox
                        {
                            stmp = ((ComboBox)o).Name.Trim().ToLower();
                            lx_ = stmp.Substring(0, 2);
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                            if (((ComboBox)o).SelectedIndex == 0)
                            {
                                val_ = "";
                                fiel_ = "";
                            }
                            else
                                val_ = ((ComboBox)o).Text.Trim();//

                            if (lx_ == "s_")
                                val_ = "'" + val_ + "'";

                        }
                        if (o is CheckBox)  //只处理存储为true,false的情况
                        {
                            stmp = ((CheckBox)o).Name.Trim().ToLower();
                            lx_ = stmp.Substring(0, 2);
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                            if (((CheckBox)o).Checked)
                                val_ = "true";
                            else
                                val_ = "false";

                            if (lx_ == "s_")
                                val_ = "'" + val_ + "'";
                        }
                        if (o is DateTimePicker)  //处理DateTimePicker
                        {
                            stmp = ((DateTimePicker)o).Name.Trim().ToLower();
                            lx_ = stmp.Substring(0, 2);
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                            val_ = ((DateTimePicker)o).Value.ToString();//


                            if (lx_ == "s_")
                                val_ = "'" + val_ + "'";

                        }                        
                        //构造Sql局部
                        if (fiel_ != "")
                        {
                            if (sqltmp_ != "")
                                sqltmp_ = sqltmp_ + "," + fiel_ + "=" + val_;
                            else
                                sqltmp_ = fiel_ + "=" + val_;
                        }
                    }
                if (con is TableLayoutPanel)
                {
                    foreach (Control o in (con as TableLayoutPanel).Controls)
                    {
                        if (o is TextBox)  //处理TextBox
                        {
                            if (((TextBox)o).Text.Trim().Length == 0) continue;  //未填入值，不处理
                            stmp = ((TextBox)o).Name.Trim().ToLower();
                            lx_ = stmp.Substring(0, 2);
                            fiel_ = stmp.Substring(2, stmp.Length - 2);
                            if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                            if (lx_ == "s_")
                                val_ = "'" + ((TextBox)o).Text.Trim() + "'";
                            else
                                val_ = ((TextBox)o).Text.Trim();
                        }
                    }
                }

            }
            if (sqltmp_ != "")
                sql_ = "update " + tab_ + " set " + sqltmp_ + " " + swhere;
            else
                sql_ = "";
            return sql_;
        }
        #endregion        

        #region 将selectsql_查询语句执行结果，绑定到pa中Name属性指定了值的控件上
        /// <summary>
        /// 将selectsql_查询语句执行结果，绑定到pa中Name属性指定了值的控件上
        /// </summary>
        /// <param name="Pa"></param>
        /// <param name="selectsql_"></param>
        public static void ShowDatas(Panel Pa, string selectsql_)
        {
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(selectsql_);
            string filename_ = "", stmp = "", lx_;
            string fiel_ = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows) //行
                {
                    for (int i = 0; i <= dt.Columns.Count - 1; i++) //列
                    {
                        filename_ = dt.Columns[i].ColumnName.ToLower().Trim();
                        foreach (Control con in Pa.Controls)
                        {
                            if (con is Panel)
                                foreach (Control o in (con as Panel).Controls)
                                {
                                    if (o is TextBox)
                                    {
                                        stmp = ((TextBox)o).Name.Trim().ToLower();
                                        lx_ = stmp.Substring(0, 2);
                                        fiel_ = stmp.Substring(2, stmp.Length - 2).Trim();
                                        if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                                        if (fiel_.ToLower() == filename_)
                                        {
                                            if (dr[filename_] is DBNull)
                                                ((TextBox)o).Text = "";
                                            else
                                                ((TextBox)o).Text = dr[filename_].ToString().Trim();
                                            break;
                                        }
                                    }
                                    if (o is ComboBox)
                                    {
                                        stmp = ((ComboBox)o).Name.Trim().ToLower();
                                        lx_ = stmp.Substring(0, 2);
                                        fiel_ = stmp.Substring(2, stmp.Length - 2).Trim();
                                        //if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                                        if (fiel_.ToLower() == filename_)
                                        {
                                            if (dr[filename_] is DBNull)
                                                ((ComboBox)o).Text = "";
                                            else
                                            {
                                                if (((ComboBox)o).DropDownStyle == ComboBoxStyle.DropDownList)
                                                    ((ComboBox)o).SelectedIndex = ((ComboBox)o).Items.IndexOf(dr[filename_].ToString().Trim());
                                                else
                                                    ((ComboBox)o).Text = dr[filename_].ToString().Trim();
                                            }
                                            break;
                                        }
                                    }
                                    if (o is DateTimePicker)
                                    {
                                        stmp = ((DateTimePicker)o).Name.Trim().ToLower();
                                        lx_ = stmp.Substring(0, 2);
                                        fiel_ = stmp.Substring(2, stmp.Length - 2).Trim();
                                        if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                                        if (fiel_.ToLower() == filename_)
                                        {
                                            if (dr[filename_] is DBNull)
                                                ((DateTimePicker)o).Value = Convert.ToDateTime(DBNull.Value);
                                            else
                                                ((DateTimePicker)o).Value = Convert.ToDateTime(dr[filename_].ToString().Trim());
                                            break;
                                        }
                                    }
                                    if (o is CheckBox) //只处理存储为true,false的情况
                                    {
                                        stmp = ((CheckBox)o).Name.Trim().ToLower();
                                        lx_ = stmp.Substring(0, 2);
                                        fiel_ = stmp.Substring(2, stmp.Length - 2).Trim();
                                        if ((lx_ != "s_") && (lx_ != "n_")) continue;   //其余控件不处理

                                        if (fiel_.ToLower() == filename_)
                                        {
                                            if (dr[filename_].ToString().Trim() == "True")
                                                ((CheckBox)o).Checked = true;
                                            else if (dr[filename_].ToString().Trim() == "False")
                                                ((CheckBox)o).Checked = false;
                                            break;
                                        }
                                    }
                                } //end of foreach

                        }  //end of foreach
                    } //end of for
                }//end of foreach
            }

        }
        #endregion 

        /// <summary>
        /// 组建单据子表的Insert Sql
        /// </summary>
        public static string Build_Insert_Sql_Receipts_Det(string strReceiptId,DataGridView dataGridViewReceDetail, DataGridViewRow dr)
        {
            string strSqlDetail = "insert into T_Receipts_Det";
            string filds = "(ReceiptId,OrderNo";
            string values = " values('{0}',{1}";
            values = string.Format(values, strReceiptId, (dr.Index + 1).ToString());

            for (int i = 0; i < dataGridViewReceDetail.Columns.Count; i++)
            {
                if (dr.Cells[i].OwningColumn.HeaderText.Trim() == "商品名称" ||
                    dr.Cells[i].OwningColumn.HeaderText.Trim() == "规格型号" ||
                    dr.Cells[i].OwningColumn.HeaderText.Trim() == "计量单位")
                    continue;
                if (dr.Cells[i].Value == null)
                    continue;
                if (dr.Cells[i].OwningColumn.HeaderText.Trim() == "商品编号")
                {
                    filds += ",MatId";
                    values += ",'" + dr.Cells[i].Value.ToString().Trim() + "'";
                    continue;
                }
                string fildName = dr.Cells[i].OwningColumn.Name.Trim().Substring(2);
                string fildType = dr.Cells[i].OwningColumn.Name.Trim().Substring(0, 2);
                filds += "," + fildName;
                if (fildType == "s_")
                    values += ",'" + dr.Cells[i].Value.ToString().Trim() + "'";
                else if (fildType == "n_")
                    values += "," + dr.Cells[i].Value.ToString().Trim();

            }
            filds += ")";
            values += ")";
            strSqlDetail += filds + values;

            return strSqlDetail;
        }
        /// <summary>
        /// 组建单据子表的Update Sql
        /// </summary>
        public static string Build_Update_Sql_Receipts_Det(string strReceiptId, DataGridView dataGridViewReceDetail, DataGridViewRow dr)
        {
            string strSqlDetail = "update T_Receipts_Det set ";
            string filds = "";
            string values = "";
            string sqltmp_ = "";

            for (int i = 0; i < dataGridViewReceDetail.Columns.Count; i++)
            {
                if (dr.Cells[i].OwningColumn.HeaderText.Trim() == "商品名称" ||
                    dr.Cells[i].OwningColumn.HeaderText.Trim() == "规格型号" ||
                    dr.Cells[i].OwningColumn.HeaderText.Trim() == "计量单位")
                    continue;
                if (dr.Cells[i].Value == null)
                    continue;
                if (dr.Cells[i].OwningColumn.HeaderText.Trim() == "商品编号")
                {
                    filds = "MatId";
                    values = "'" + dr.Cells[i].Value.ToString().Trim() + "'";
                }
                else
                {
                    string fildName = dr.Cells[i].OwningColumn.Name.Trim().Substring(2);
                    string fildType = dr.Cells[i].OwningColumn.Name.Trim().Substring(0, 2);
                    filds = fildName;
                    if (dr.Cells[i].Value.ToString().Trim() == "")
                        continue;
                    if (fildType == "s_")
                        values = "'" + dr.Cells[i].Value.ToString().Trim() + "'";
                    else if (fildType == "n_")
                        values = "" + dr.Cells[i].Value.ToString().Trim();
                }

                //构造Sql局部
                if (filds != "")
                {
                    if (sqltmp_ != "")
                        sqltmp_ = sqltmp_ + "," + filds + "=" + values;
                    else
                        sqltmp_ = filds + "=" + values;
                }
            }

            string strWhere_ = " where ReceiptId='{0}' and OrderNo={1}";
            strWhere_ = string.Format(strWhere_, strReceiptId, dr.Index + 1);

            strSqlDetail += sqltmp_ + strWhere_;

            return strSqlDetail;
        }
        /// <summary>
        /// 组建单据子表的Insert Sql
        /// </summary>
        public static string Build_Insert_Sql_Receipts_Det(string strReceiptId, DataGridView dataGridViewReceDetail, DataGridViewRow dr,int orderIndex, int num, double price)
        {
            string strSqlDetail = "insert into T_Receipts_Det";
            string filds = "(ReceiptId,OrderNo,STaxPurchPrice";
            string values = " values('{0}',{1},{2}";
            values = string.Format(values, strReceiptId, orderIndex.ToString(), price);

            for (int i = 0; i < dataGridViewReceDetail.Columns.Count; i++)
            {
                if (dr.Cells[i].OwningColumn.HeaderText.Trim() == "商品名称" ||
                    dr.Cells[i].OwningColumn.HeaderText.Trim() == "规格型号" ||
                    dr.Cells[i].OwningColumn.HeaderText.Trim() == "计量单位")
                    continue;
                if (dr.Cells[i].Value == null)
                    continue;
                if (dr.Cells[i].OwningColumn.HeaderText.Trim() == "商品编号")
                {
                    filds += ",MatId";
                    values += ",'" + dr.Cells[i].Value.ToString().Trim() + "'";
                    continue;
                }
                if (dr.Cells[i].OwningColumn.HeaderText.Trim() == "数量")
                {
                    filds += ",num";
                    values += "," + num;
                    continue;
                }
                if (dr.Cells[i].OwningColumn.HeaderText.Trim() == "单价")
                {
                    filds += ",price";
                    values += "," + price;
                    continue;
                }
                string fildName = dr.Cells[i].OwningColumn.Name.Trim().Substring(2);
                string fildType = dr.Cells[i].OwningColumn.Name.Trim().Substring(0, 2);
                filds += "," + fildName;
                if (fildType == "s_")
                    values += ",'" + dr.Cells[i].Value.ToString().Trim() + "'";
                else if (fildType == "n_")
                    values += "," + dr.Cells[i].Value.ToString().Trim();

            }
            filds += ")";
            values += ")";
            strSqlDetail += filds + values;

            return strSqlDetail;
        }
        /// <summary>
        /// 获得某一仓库、某一物料、某一类型商品的库存
        /// </summary>
        /// <param name="SStorehouseId"></param>
        /// <param name="matId"></param>
        /// <param name="matType"></param>
        /// <returns></returns>
        public static StockStatus GetStockNum(string SStorehouseId, string matId, int matType)
        {
            double stockNum = 0; //当前库存
            double firstCount = 0; //期初数量
            double stockInCount = 0; //收入数量
            double stockOutCount = 0; //发出数量
            SqlDBConnect db = new SqlDBConnect();

            //查找最近一次结存的数量
            string maxBalanceTime = StockStatusDAO.GetBalanceTime();
            string strSql_ = "select FirstCount,FirstCostPrice,BalanceTime from T_Stock_Status " +
                              "where StoreHouseId='{0}' and MatId='{1}' and MatType={2} and BalanceTime='{3}'";
            strSql_ = string.Format(strSql_, SStorehouseId, matId, matType, maxBalanceTime);
            DataTable dt_ = db.Get_Dt(strSql_);
            if (dt_ != null && dt_.Rows.Count > 0)
                firstCount = Convert.ToDouble(dt_.Rows[0]["FirstCount"].ToString().Trim());

            //入库
            string sql_1 = "select sum(num) num from T_Receipt_Main_Det where SourceStoreH='{0}' and MatId='{1}' and MatType='{2}' and CurWorkMonth >= '{3}' " +
                           "and ReceiptTypeID < '51' and ReceiptTypeID != 'YS' and ReceiptTypeID != 'YF'";
            sql_1 = string.Format(sql_1, SStorehouseId, matId, matType, maxBalanceTime);
            DataTable dt_1 = db.Get_Dt(sql_1);
            if (dt_1 != null && dt_1.Rows.Count > 0)
            {               
                if (dt_1.Rows[0]["num"].ToString().Trim() != "")
                    stockInCount = Convert.ToDouble(dt_1.Rows[0]["num"].ToString().Trim());              
            }

            //出库
            string sql_2 = "select sum(num) num from T_Receipt_Main_Det where SourceStoreH='{0}' and MatId='{1}' and MatType='{2}' and CurWorkMonth >= '{3}' " +
                           "and ReceiptTypeID >= '51' and ReceiptTypeID <=  '90'";
            sql_2 = string.Format(sql_2, SStorehouseId, matId, matType, maxBalanceTime);
            DataTable dt_2 = db.Get_Dt(sql_2);
            if (dt_2 != null && dt_2.Rows.Count > 0)
            {               
                if (dt_2.Rows[0]["num"].ToString().Trim() != "")
                    stockOutCount = Convert.ToDouble(dt_2.Rows[0]["num"].ToString().Trim());               
            }
            stockNum = firstCount + stockInCount - stockOutCount;

            StockStatus stockStatus = new StockStatus();
            stockStatus.firstCount = firstCount;
            stockStatus.stockInCount = stockInCount;
            stockStatus.stockOutCount = stockOutCount;
            stockStatus.stockNum = stockNum;

            return stockStatus;
        }
    }
}
