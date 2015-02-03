using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.Base;
using Warehouse.DB;
using Warehouse.DAO;

namespace Warehouse
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            string strSql = "select T_CustomerInf.CustID,T_CustomerInf.CustName,T_CustomerInf.communicateAddr,T_CustContacts.Tel " +
                                       "from T_CustomerInf inner join T_CustContacts " +
                                       "on  T_CustomerInf.CustID='{0}' and T_CustomerInf.CustID=T_CustContacts.CustID";
            strSql = string.Format(strSql, "1");

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            (new InitFuncs()).InitDataGridView(this.dataGridView1, dt);
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;

            this.progressBar1.Minimum = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Text = Util.GetPYM(this.textBox1.Text.Trim());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.Text = Util.ConvertMoney(Convert.ToDecimal(this.textBox1.Text.Trim()));
        }

        private void button3_Click(object sender, EventArgs e)
        {           
            DBUtil dbUtil = new DBUtil();           
            string strSql = "select T_Receipt_Main.*,T_Receipts_Det.* from T_Receipts_Det,T_Receipt_Main "+
                            "where T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return;
            this.progressBar1.Maximum = dt.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                List<string> sqls = new List<string>();
                int numTem = 0; //记录事务处理中，未实际插入数据表的num
                double lastCostTem = 0.0; //记录未实际存入数据库的最后的成本单价

                string curWorkMonth = dt.Rows[i]["CurWorkMonth"].ToString().Trim();
                string occurTime = dt.Rows[i]["OccurTime"].ToString().Trim();
                string strReceiptId = dt.Rows[i]["ReceiptId"].ToString().Trim();//单据类型
                string strReceTypeId = dt.Rows[i]["ReceiptTypeID"].ToString().Trim();//单据类型
                int matType = Convert.ToInt32(dt.Rows[i]["MatType"].ToString().Trim());//物料类型
                string matId = dt.Rows[i]["MatId"].ToString().Trim();                
                string SStorehouseId = dt.Rows[i]["SourceStoreH"].ToString().Trim();//仓库
                int num = Convert.ToInt32(dt.Rows[i]["num"].ToString().Trim());
                double price = Convert.ToDouble(dt.Rows[i]["price"].ToString().Trim());
                int orderNo = Convert.ToInt32(dt.Rows[i]["OrderNo"].ToString().Trim());
                
                string InOrOutBound = dbUtil.Get_Single_val("T_ReceiptModal", "InOrOutBound", "ReceTypeID", strReceTypeId);
                if (strReceTypeId == "03")
                {
                    #region 03单
                    if (matType == 0) //新机
                    {
                        //(0新机)计算成本单价
                        double costPrice = CalculateCostPrice("03", matId, matType, SStorehouseId,num, price, ref numTem, ref lastCostTem);

                        string strSqlUpdateCostPrice = "update T_Receipts_Det set STaxPurchPrice={0} where ReceiptId='{1}' and OrderNo={2}";
                        strSqlUpdateCostPrice = string.Format(strSqlUpdateCostPrice, costPrice, strReceiptId, orderNo);

                        sqls.Add(strSqlUpdateCostPrice);
                        numTem += num;

                        //更新当前仓库、当前物料ID、当前类型的商品的CurAveragePrice(移动平均单价)
                        string strSqlUpdateCurAverPrice = "update T_Receipts_Det set CurAveragePrice={0} from T_Receipt_Main,T_Receipts_Det " +
                                                          "where T_Receipt_Main.SourceStoreH='{1}' and T_Receipt_Main.CurWorkMonth > '{2}' " +
                                                          "and T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and T_Receipts_Det.MatId='{3}' and T_Receipts_Det.MatType={4}";
                        strSqlUpdateCurAverPrice = string.Format(strSqlUpdateCurAverPrice, costPrice, SStorehouseId, StockStatusDAO.GetBalanceTime(), matId, matType);
                        sqls.Add(strSqlUpdateCurAverPrice);
                    }
                    else
                    {
                        //（1旧机、2样机）无核销 计算成本单价
                        string strSqlUpdateCostPrice = "update T_Receipts_Det set STaxPurchPrice={0} where ReceiptId='{1}' and OrderNo={2}";
                        strSqlUpdateCostPrice = string.Format(strSqlUpdateCostPrice, price, strReceiptId, orderNo);

                        sqls.Add(strSqlUpdateCostPrice);
                    }
                    //如果是03单，要同时更新 YnCompleteVerificate_03 字段                   
                    string strSqlUpdate_03 = "update T_Receipts_Det set YnCompleteVerificate_03='false' where ReceiptId='{0}' and OrderNo={1}";
                    strSqlUpdate_03 = string.Format(strSqlUpdate_03, strReceiptId, orderNo);

                    sqls.Add(strSqlUpdate_03);
                    #endregion
                }
                if (strReceTypeId == "01")
                {
                    bool isFound = false;
                    string strSqlSel = "select * from S90核销明细 where 进单类别='03' and 销单编号 in " +
                                       "(select 销单编号 from s90核销明细 where 销单类别='20' and 进单编号='{0}' and 进单分录号={1} and 进单类别='{2}' and 进单工作年月='{3}')";
                    strSqlSel = string.Format(strSqlSel, strReceiptId, orderNo, strReceTypeId, curWorkMonth);
                    DataTable dataTable = (new SqlDBConnect()).Get_Dt(strSqlSel);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                        isFound = true;
                    if (isFound == false) //普通01单
                    {
                        #region 普通01单
                        if (matType == 0) //新机
                        {
                            //(0新机)计算成本单价
                            double costPrice = CalculateCostPrice("01", matId, matType, SStorehouseId, num, price, ref numTem, ref lastCostTem);

                            string strSqlUpdateCostPrice = "update T_Receipts_Det set STaxPurchPrice={0} where ReceiptId='{1}' and OrderNo={2}";
                            strSqlUpdateCostPrice = string.Format(strSqlUpdateCostPrice, costPrice, strReceiptId, orderNo);

                            sqls.Add(strSqlUpdateCostPrice);
                            numTem += num;

                            //更新当前仓库、当前物料ID、当前类型的商品的CurAveragePrice(移动平均单价)
                            string strSqlUpdateCurAverPrice = "update T_Receipts_Det set CurAveragePrice={0} from T_Receipt_Main,T_Receipts_Det " +
                                                              "where T_Receipt_Main.SourceStoreH='{1}' and T_Receipt_Main.CurWorkMonth > '{2}' " +
                                                              "and T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and T_Receipts_Det.MatId='{3}' and T_Receipts_Det.MatType={4}";
                            strSqlUpdateCurAverPrice = string.Format(strSqlUpdateCurAverPrice, costPrice, SStorehouseId, StockStatusDAO.GetBalanceTime(), matId, matType);
                            sqls.Add(strSqlUpdateCurAverPrice);
                        }
                        else
                        {
                            //（1旧机、2样机）无核销 计算成本单价
                            string strSqlUpdateCostPrice = "update T_Receipts_Det set STaxPurchPrice={0} where ReceiptId='{1}' and OrderNo={2}";
                            strSqlUpdateCostPrice = string.Format(strSqlUpdateCostPrice, price, strReceiptId, orderNo);

                            sqls.Add(strSqlUpdateCostPrice);
                        }
                        #endregion
                    }
                    else //为核销03单的01单
                    {
                        #region //核销03单的01单
                        int num1 = 0;
                        string receiptId20 = "";
                        string receiptId90 = "";

                        string receiptId03 = dataTable.Rows[i]["简单编号"].ToString().Trim();
                        int orderNo03 = Convert.ToInt32(dataTable.Rows[i]["进单分录号"].ToString().Trim());
                        string receiptTypeId03 = dataTable.Rows[i]["进单类别"].ToString().Trim();
                        string curWorkMonth03 = dataTable.Rows[i]["进单工作年月"].ToString().Trim();
                        int num03 = Convert.ToInt32(dataTable.Rows[i]["进货数量"].ToString().Trim());
                        double price03 = Convert.ToDouble(dataTable.Rows[i]["进货单价"].ToString().Trim());

                        //自动产生一个20单，一个90单
                        for (int j = 0; j < dataTable.Rows.Count - 1; j++)
                        {
                            num1++;
                            //生成20单                               
                            if (num1 == 1)
                            {
                                //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
                                string SqlUpdateBillRull20 = "";
                                receiptId20 = DBUtil.Produce_Bill_Id("20", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull20);

                                string strSqlMain_20 = "insert into T_Receipt_Main(ReceiptId,CurWorkMonth,OccurTime,SourceStoreH,ReceiptTypeID) values('{0}','{1}','{2}','{3}','{4}')";
                                strSqlMain_20 = string.Format(strSqlMain_20, receiptId20, curWorkMonth, occurTime,SStorehouseId, "20");
                                sqls.Add(strSqlMain_20);
                                sqls.Add(SqlUpdateBillRull20);//更新单据号
                            }
                            string strSqlDet_20 = "insert into T_Receipts_Det(ReceiptId,OrderNo,MatId,MatType,price,num,ReceiptId20_03) values('{0}',{1},'{2}',{3},{4},{5},'{6}')";
                            strSqlDet_20 = string.Format(strSqlDet_20, receiptId20,orderNo03, matId, matType, price03, -num, receiptId03);
                            sqls.Add(strSqlDet_20);

                            //计算20单 物料成本                            
                            double costPrice = CalculateCostPrice("20", matId, matType, SStorehouseId, -num, price03, ref numTem, ref lastCostTem);

                            string strSqlUpdateCostPrice = "update T_Receipts_Det set STaxPurchPrice={0} where ReceiptId='{1}' and OrderNo={2}";
                            strSqlUpdateCostPrice = string.Format(strSqlUpdateCostPrice, costPrice, receiptId20, orderNo03);

                            sqls.Add(strSqlUpdateCostPrice);
                            numTem += -num;/////
                            
                            //计算该01单 物料成本
                            double costPrice01 = CalculateCostPrice("01", matId, matType, SStorehouseId, num, price, ref numTem, ref lastCostTem);

                            string strSqlUpdateCostPrice_01 = "update T_Receipts_Det set STaxPurchPrice={0} where ReceiptId='{1}' and OrderNo={2}";
                            strSqlUpdateCostPrice_01 = string.Format(strSqlUpdateCostPrice_01, costPrice01, strReceiptId, orderNo);

                            sqls.Add(strSqlUpdateCostPrice_01);
                            numTem += num;/////

                            //生成90单                                
                            if (num1 == 1) //子表有多行，主表只插入一条记录
                            {
                                //产生单据号,并返回更新＂T_ReceiptRule＂的SQL语句
                                string SqlUpdateBillRull90 = "";
                                receiptId90 = DBUtil.Produce_Bill_Id("90", DBUtil.getServerTime().ToString().Trim(), ref SqlUpdateBillRull90);

                                string strSqlMain_90 = "insert into T_Receipt_Main(ReceiptId,CurWorkMonth,OccurTime,SourceStoreH,ReceiptTypeID) values('{0}','{1}','{2}','{3}','{4}')";
                                strSqlMain_90 = string.Format(strSqlMain_90, receiptId90, curWorkMonth, occurTime, SStorehouseId, "90");
                                sqls.Add(strSqlMain_90);
                                sqls.Add(SqlUpdateBillRull90);//更新单据号

                            }
                            string strSqlDet_90 = "insert into T_Receipts_Det(ReceiptId,OrderNo,MatId,MatType,price,num,ReceiptId90_03) values('{0}',{1},'{2}',{3},{4},{5},'{6}')";
                            strSqlDet_90 = string.Format(strSqlDet_90, receiptId90, orderNo03, matId, matType, price - price03, num, receiptId03);
                            sqls.Add(strSqlDet_90);

                            //计算90单 物料成本 
                            double costPrice90 = CalculateCostPrice("90", matId, matType, SStorehouseId, num, price - price03, ref numTem, ref lastCostTem);

                            string strSqlUpdateCostPrice_90 = "update T_Receipts_Det set STaxPurchPrice={0} where ReceiptId='{1}' and OrderNo={2}";
                            strSqlUpdateCostPrice_90 = string.Format(strSqlUpdateCostPrice_90, costPrice90, receiptId90, orderNo03);

                            sqls.Add(strSqlUpdateCostPrice_90);

                            //更新当前仓库、当前物料ID、当前类型的商品的CurAveragePrice(移动平均单价)
                            string strSqlUpdateCurAverPrice = "update T_Receipts_Det set CurAveragePrice={0} from T_Receipt_Main,T_Receipts_Det " +
                                                              "where T_Receipt_Main.SourceStoreH='{1}' and T_Receipt_Main.CurWorkMonth > '{2}' " +
                                                              "and T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and T_Receipts_Det.MatId='{3}' and T_Receipts_Det.MatType={4}";
                            strSqlUpdateCurAverPrice = string.Format(strSqlUpdateCurAverPrice, costPrice90, SStorehouseId, StockStatusDAO.GetBalanceTime(), matId, matType);
                            sqls.Add(strSqlUpdateCurAverPrice);

                            //更新03单的已核销数量 AlreadyVerificateNum_03
                            string strSqlUpdate_AlreadyVerificateNum_03 = "update T_Receipts_Det set AlreadyVerificateNum_03=AlreadyVerificateNum_03 + {0} where ReceiptId='{1}' and OrderNo={2}";
                            strSqlUpdate_AlreadyVerificateNum_03 = string.Format(strSqlUpdate_AlreadyVerificateNum_03, num, receiptId03, orderNo03);
                            sqls.Add(strSqlUpdate_AlreadyVerificateNum_03);

                            //如果该条物料核销完，更新相应03单的YnCompleteVerificate_03='true'
                            if (num03 - num <= 0)
                            {
                                string strSqlUpdate_03 = "update T_Receipts_Det set YnCompleteVerificate_03='true' where ReceiptId='{0}' and OrderNo={1}";
                                strSqlUpdate_03 = string.Format(strSqlUpdate_03, receiptId03, orderNo03);
                                sqls.Add(strSqlUpdate_03);
                            }

                            //更新01单子表该条记录的 ReceiptId01_03
                            string strSqlUpate_01 = "update T_Receipts_Det set ReceiptId01_03='{0}' where ReceiptId='{1}' and OrderNo={2}";
                            strSqlUpate_01 = string.Format(strSqlUpate_01, receiptId03, strReceiptId, orderNo);

                            sqls.Add(strSqlUpate_01);                           
                        }
                        #endregion
                    }

                }
                if (InOrOutBound == "出库")
                {
                    double costPrice = CalculateCostPriceOut(matId, matType, SStorehouseId);

                    //更新成本单价(为入库时的移动平均成本)
                    string strSqlUpdateCostPrice = "update T_Receipts_Det set STaxPurchPrice={0} where ReceiptId='{1}' and OrderNo={2}";
                    strSqlUpdateCostPrice = string.Format(strSqlUpdateCostPrice, costPrice, strReceiptId, orderNo);

                    sqls.Add(strSqlUpdateCostPrice);
                }

                //执行事务处理所有的Sql
                SqlDBConnect db = new SqlDBConnect();
                db.Exec_Tansaction(sqls);
                this.progressBar1.Value += 1; 
            }

        }
        /// <summary>
        /// 计算物料 成本单价 (移动平均法，入库)
        /// <param name="num"></param>
        /// <param name="price"></param>
        /// <param name="numTem">记录事务处理中，未实际插入数据表的num</param> 
        /// </summary>   
        private double CalculateCostPrice(string receiptType, string matId, int matType, string storeHouseId, int num, double price, ref int numTem, ref double lastCostTem)
        {
            SqlDBConnect db = new SqlDBConnect();
            double costPrice = 0.0;//最终返回的 物料的 成本单价

            string strSql = "select LastCount,LastCost,BalanceTime from T_Stock_Status where StoreHouseId='{0}' and MatId='{1}' and MatType={2} " +
                            "order by BalanceTime";
            strSql = string.Format(strSql, storeHouseId, matId, matType);
            DataTable dt = db.Get_Dt(strSql);

            //移动加权平均方式           
            int allNum = 0;
            int lastCount = 0; //取最近的一次结存的 数量
            double lastCost = 0.0; //取最近的一次结存的 成本单价
            string BalanceTime = "190001"; //取最近的一次结存的 结存时间
            if (dt != null && dt.Rows.Count > 0)
            {
                //取最近的一次结存的数量、成本单价
                lastCount = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["LastCount"].ToString().Trim());
                lastCost = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1]["LastCost"].ToString().Trim());
                BalanceTime = dt.Rows[dt.Rows.Count - 1]["BalanceTime"].ToString().Trim();
            }
            allNum += lastCount;

            string strSqlSel = "select T_Receipts_Det.ReceiptId,num,STaxPurchPrice,CurAveragePrice from T_Receipts_Det,T_Receipt_Main,T_ReceiptModal " +
                                "where T_ReceiptModal.ReceTypeID=T_Receipt_Main.ReceiptTypeID and T_ReceiptModal.InOrOutBound='入库' and " +
                                        "T_Receipt_Main.SourceStoreH='{0}' and T_Receipt_Main.CurWorkMonth > '{1}' and " +
                                        "T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and " +
                                        "T_Receipts_Det.MatId='{2}' and T_Receipts_Det.MatType={3}";
            strSqlSel = string.Format(strSqlSel, storeHouseId, BalanceTime, matId, matType);
            DataTable dtDet = db.Get_Dt(strSqlSel);

            double costLast = 0.0;//当前仓库中该物料最后一条记录的“成本单价”
            if (dtDet != null && dtDet.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDet.Rows)
                {
                    if (dr["ReceiptId"].ToString().Trim().Substring(0, 2) == "90") //90单（不影响库存）不计算数量
                        continue;
                    int numDet = Convert.ToInt32(dr["num"].ToString().Trim());
                    allNum += numDet;
                }
                costLast = Convert.ToDouble(dtDet.Rows[dtDet.Rows.Count - 1]["CurAveragePrice"].ToString().Trim());//该仓库该物料该类型最后一次移动平均价
            }
            else //从最后一次结存到目前，单据子表中没有该物料信息
            {
                costLast = lastCost;
            }
            if (lastCostTem == 0.0) //第一次未实际存入的成本单价
                lastCostTem = costLast;

            costLast = lastCostTem;//

            allNum += numTem;
            if (receiptType == "90")
                costPrice = (costLast * allNum - price * num) / allNum;
            else
                costPrice = (costLast * allNum + price * num) / (allNum + num);

            lastCostTem = costPrice;//把未实际存入的成本单价赋给 临时变量
           

            return costPrice;
        }
        /// <summary>
        /// 获得物料 成本单价 (移动平均法，出库)       
        /// </summary>   
        private double CalculateCostPriceOut(string matId, int matType, string storeHouseId)
        {
            SqlDBConnect db = new SqlDBConnect();
            double costPrice = 0.0;//最终返回的 物料的 成本单价
                       
            string maxBalanceTime = StockStatusDAO.GetBalanceTime();

            string strSql = "select LastCount,LastCost,BalanceTime from T_Stock_Status " +
                            "where StoreHouseId='{0}' and MatId='{1}' and MatType={2} and BalanceTime='{3}'";
            strSql = string.Format(strSql, storeHouseId, matId, matType, maxBalanceTime);
            DataTable dt = db.Get_Dt(strSql);

            double lastCost = 0.0; //取最近的一次结存的 成本单价
            string BalanceTime = "190001"; //取最近的一次结存的 结存时间
            if (dt != null && dt.Rows.Count > 0)
            {
                //取最近的一次结存的 成本单价                    
                lastCost = Convert.ToDouble(dt.Rows[0]["LastCost"].ToString().Trim());
                BalanceTime = dt.Rows[0]["BalanceTime"].ToString().Trim();
            }

            string strSqlSel = "select T_Receipts_Det.ReceiptId,STaxPurchPrice,CurAveragePrice from T_Receipts_Det,T_Receipt_Main,T_ReceiptModal " +
                                "where T_ReceiptModal.ReceTypeID=T_Receipt_Main.ReceiptTypeID and T_ReceiptModal.InOrOutBound='入库' and " +
                                        "T_Receipt_Main.SourceStoreH='{0}' and T_Receipt_Main.CurWorkMonth > '{1}' and " +
                                        "T_Receipt_Main.ReceiptId=T_Receipts_Det.ReceiptId and " +
                                        "T_Receipts_Det.MatId='{2}' and T_Receipts_Det.MatType={3}";
            strSqlSel = string.Format(strSqlSel, storeHouseId, BalanceTime, matId, matType);
            DataTable dtDet = db.Get_Dt(strSqlSel);

            if (dtDet != null && dtDet.Rows.Count > 0)
            {
                costPrice = Convert.ToDouble(dtDet.Rows[dtDet.Rows.Count - 1]["CurAveragePrice"].ToString().Trim());//该仓库该物料该类型最后一次移动平均价
            }
            else
                costPrice = lastCost;
           
            return costPrice;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Compare("YS", "90") > 0 ? "YS>90":"YS<=90");
            MessageBox.Show(string.Compare("YF", "90") > 0 ? "YF>90" : "YF<=90");
            MessageBox.Show(string.Compare("YS", "YF") > 0 ? "YS>YF" : "YS<=YF");
        }



    }
}
