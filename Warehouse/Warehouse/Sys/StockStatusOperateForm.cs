using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DAO;
using Warehouse.DB;
using Warehouse.Base;
using System.Threading;

namespace Warehouse.Sys
{
    public partial class StockStatusOperateForm : Form
    {
        public string curWorkMonth = "";
        public StockStatusOperateForm()
        {
            InitializeComponent();
        }

        private void StockStatusOperateForm_Load(object sender, EventArgs e)
        {
            string maxBalanceTime = StockStatusDAO.GetBalanceTime();
            this.textBoxStockMonth.Text = this.curWorkMonth.Trim();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ThreadStart threadStart = new ThreadStart(StockStatusOperate);
            Thread thread = new Thread(threadStart);
            thread.Start();

            GC.Collect();                          
        }

        private void StockStatusOperate()
        {
            try
            {
                DateTime from_, to_;
                from_ = System.DateTime.Now;

                string balanceTime = this.textBoxStockMonth.Text.Trim();
                if (balanceTime.Length != 6)
                {
                    MessageBox.Show("请输入正确的结存年月（如：201108）！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string maxBalanceTime = StockStatusDAO.GetBalanceTime();
                if (string.Compare(balanceTime, maxBalanceTime) > 0)
                {
                    MessageBox.Show("前面还有未结存的月份！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SqlDBConnect db = new SqlDBConnect();
                string strSql = "select distinct SourceStoreH, MatId, MatType from T_Receipts_Det,T_Receipt_Main " +
                                "where CurWorkMonth = '{0}' and T_Receipts_Det.receiptId=T_Receipt_Main.receiptId and ReceiptTypeID != 'YS' and ReceiptTypeID != 'YF'";
                strSql = string.Format(strSql, balanceTime);
                DataTable dt = db.Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                
                this.Invoke((EventHandler)delegate { this.progressBar1.Value = 0; });
                this.Invoke((EventHandler)delegate { this.progressBar1.Minimum = 0; });
                this.Invoke((EventHandler)delegate { this.progressBar1.Maximum = dt.Rows.Count; });
                              
                
                List<string> sqls = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    double firstCount = 0; //期初数量                    
                    double firstMoney = 0.0; //期初成本金额                           
                    double stockInCount = 0; //收入数量                    
                    double stockInMoney = 0.0;
                    double stockOutCount = 0; //发出数量                   
                    double stockOutMoney = 0.0;
                    double lastCount = 0; //期末数量
                    double lastMoney = 0.0; //期末金额
                    double firstRoadCount = 0; //期初在途数量（未冲销03单）
                    double firstRoadMoney = 0.0;
                    double firstOutCount = 0; //期初发出数量（75、88数量）
                    double firstOutMoney = 0.0;
                    double lastRoadCount = 0; //期末在途数量（未冲销03单）
                    double lastRoadMoney = 0.0;
                    double lastOutCount = 0; //期末发出数量（75、88数量）
                    double lastOutMoney = 0.0;

                    double roadCount = 0; //本期在途数量
                    double roadMoney = 0.0;
                    double outCount = 0; //本期发出数量
                    double outMoney = 0.0;

                    string storeHouseId = dr["SourceStoreH"].ToString().Trim();
                    string matId = dr["MatId"].ToString().Trim();
                    int matType = Convert.ToInt32(dr["MatType"].ToString().Trim());                    

                    //入库
                    string sql_1 = "select sum(num) as num, sum(TTaxPurchPrice) as TTaxPurchPrice from T_Receipt_Main_Det " +
                                   "where  CurWorkMonth = '{0}' and SourceStoreH='{1}' and MatId='{2}' and MatType={3} " +
                                   "and ReceiptTypeID < '51' and ReceiptTypeID != 'YS' and ReceiptTypeID != 'YF'";
                    sql_1 = string.Format(sql_1, balanceTime, storeHouseId, matId, matType);
                    DataTable dt_1 = db.Get_Dt(sql_1);
                    if (dt_1 != null && dt_1.Rows.Count > 0)
                    {
                        if (dt_1.Rows[0]["num"].ToString().Trim() != "")
                            stockInCount = Convert.ToDouble(dt_1.Rows[0]["num"].ToString().Trim());
                        if (dt_1.Rows[0]["TTaxPurchPrice"].ToString().Trim() != "")
                            stockInMoney = Convert.ToDouble(dt_1.Rows[0]["TTaxPurchPrice"].ToString().Trim());                        
                    }

                    //出库
                    string sql_2 = "select sum(num) as num, sum(TTaxPurchPrice) as TTaxPurchPrice from T_Receipt_Main_Det " +
                                   "where CurWorkMonth = '{0}' and SourceStoreH='{1}' and MatId='{2}' and MatType={3}  " +
                                   "and ReceiptTypeID >= '51' and ReceiptTypeID != 'YS' and ReceiptTypeID != 'YF'";// and ReceiptTypeID != '90'";
                    sql_2 = string.Format(sql_2, balanceTime, storeHouseId, matId, matType);
                    DataTable dt_2 = db.Get_Dt(sql_2);
                    if (dt_2 != null && dt_2.Rows.Count > 0)
                    {                       
                        if (dt_2.Rows[0]["num"].ToString().Trim() != "")
                            stockOutCount = Convert.ToDouble(dt_2.Rows[0]["num"].ToString().Trim());
                        if (dt_2.Rows[0]["TTaxPurchPrice"].ToString().Trim() != "")
                            stockOutMoney = Convert.ToDouble(dt_2.Rows[0]["TTaxPurchPrice"].ToString().Trim());//                       
                    }
                    //本期在途（未冲销03单）
                    string sql_3 = "select sum(num) as num,sum(TTaxPurchPrice) as TTaxPurchPrice from T_Receipt_Main_Det " +
                                   "where  CurWorkMonth = '{0}' and SourceStoreH='{1}' and MatId='{2}' and MatType={3}  " +
                                   "and (ReceiptTypeID = '03' or ReceiptTypeID = '20')";
                    sql_3 = string.Format(sql_3, balanceTime, storeHouseId, matId, matType);
                    DataTable dt_3 = db.Get_Dt(sql_3);
                    if (dt_3 != null && dt_3.Rows.Count > 0)
                    {                       
                        if (dt_3.Rows[0]["num"].ToString().Trim() != "")
                            roadCount = Convert.ToDouble(dt_3.Rows[0]["num"].ToString().Trim());
                        if (dt_3.Rows[0]["TTaxPurchPrice"].ToString().Trim() != "")
                            roadMoney = Convert.ToDouble(dt_3.Rows[0]["TTaxPurchPrice"].ToString().Trim());//                        
                    }

                    //本期发出（75、88数量）
                    string sql_4 = "select sum(num) as num,sum(TTaxPurchPrice) as TTaxPurchPrice from T_Receipt_Main_Det " +
                                   "where CurWorkMonth = '{0}' and SourceStoreH='{1}' and MatId='{2}' and MatType={3} " +
                                   "and (ReceiptTypeID = '75' or ReceiptTypeID = '88')";
                    sql_4 = string.Format(sql_4, balanceTime, storeHouseId, matId, matType);
                    DataTable dt_4 = db.Get_Dt(sql_4);
                    if (dt_4 != null && dt_4.Rows.Count > 0)
                    {
                        if (dt_4.Rows[0]["num"].ToString().Trim() != "")
                            outCount = Convert.ToDouble(dt_4.Rows[0]["num"].ToString().Trim());
                        if (dt_4.Rows[0]["TTaxPurchPrice"].ToString().Trim() != "")
                            outMoney = Convert.ToDouble(dt_4.Rows[0]["TTaxPurchPrice"].ToString().Trim());//
                    }

                    //期初
                    string strSql_ = "select FirstCount,FirstCostPrice,FirstMoney,FirstRoadCount,FirstRoadMoney,FirstOutCount,FirstOutMoney,BalanceTime from T_Stock_Status " +
                                     "where BalanceTime='{0}' and StoreHouseId='{1}' and MatId='{2}' and MatType={3} ";
                    strSql_ = string.Format(strSql_, balanceTime, storeHouseId, matId, matType);
                    DataTable dt_ = db.Get_Dt(strSql_);
                    if (dt_ != null && dt_.Rows.Count > 0)
                    {
                        //取该物料本期期初的数量、成本单价、成本金额
                        firstCount = Convert.ToDouble(dt_.Rows[0]["FirstCount"].ToString().Trim());
                        firstMoney = Convert.ToDouble(dt_.Rows[0]["FirstMoney"].ToString().Trim());
                        firstRoadCount = Convert.ToDouble(dt_.Rows[0]["FirstRoadCount"].ToString().Trim());
                        firstRoadMoney = Convert.ToDouble(dt_.Rows[0]["FirstRoadMoney"].ToString().Trim());
                        firstOutCount = Convert.ToDouble(dt_.Rows[0]["FirstOutCount"].ToString().Trim());
                        firstOutMoney = Convert.ToDouble(dt_.Rows[0]["FirstOutMoney"].ToString().Trim());

                        lastCount = firstCount + stockInCount - stockOutCount;
                        lastMoney = firstMoney + stockInMoney - stockOutMoney;
                        lastRoadCount = firstRoadCount + roadCount;
                        lastRoadMoney = firstRoadMoney + roadMoney;
                        lastOutCount = firstOutCount + outCount;
                        lastOutMoney = firstOutMoney + outMoney;

                        string strSqlUpdate = "update T_Stock_Status set StockInCount={0},StockInMoney={1},StockOutCount={2},StockOutMoney={3}," +
                                              "LastCount={4},LastMoney={5},LastRoadCount={6},LastRoadMoney={7},LastOutCount={8},LastOutMoney={9} " +
                                              " where BalanceTime='{10}' and StoreHouseId='{11}' and MatId='{12}' and MatType={13} ";
                        strSqlUpdate = string.Format(strSqlUpdate, stockInCount, stockInMoney, stockOutCount, stockOutMoney, lastCount, lastMoney,
                                                     lastRoadCount, lastRoadMoney, lastOutCount, lastOutMoney,
                                                     balanceTime, storeHouseId, matId, matType);
                        sqls.Add(strSqlUpdate);
                    }
                    else //不存在，为本工作月新增商品
                    {
                        lastCount = stockInCount - stockOutCount;
                        lastMoney = stockInMoney - stockOutMoney;
                        lastRoadCount = roadCount;
                        lastRoadMoney = roadMoney;
                        lastOutCount = outCount;
                        lastOutMoney = outMoney;
                        string strSqlInsert = "insert into T_Stock_Status(StoreHouseId,MatId,MatType,BalanceTime,FirstCount,FirstMoney,FirstRoadCount,FirstRoadMoney,FirstOutCount,FirstOutMoney," +
                                                                         "StockInCount,StockInMoney,StockOutCount,StockOutMoney,LastCount,LastMoney,LastRoadCount,LastRoadMoney,LastOutCount,LastOutMoney) " +
                                              "values('{0}','{1}',{2},'{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}";
                        strSqlInsert = string.Format(strSqlInsert, storeHouseId, matId, matType, balanceTime, 0, 0, 0, 0, 0, 0, stockInCount, stockInMoney, stockOutCount, stockOutMoney,
                                                     lastCount, lastMoney, lastRoadCount, lastRoadMoney, lastOutCount, lastOutMoney);
                        sqls.Add(strSqlInsert);
                    }

                    this.Invoke((EventHandler)delegate { this.progressBar1.Value += 1; });               
                }

                //处理结存库中存在，但当月并未发生的物料
                string sqlSS = "select T_Stock_Status.* from T_Stock_Status " +
                             "where T_Stock_Status.StoreHouseId + T_Stock_Status.MatId + CONVERT(Nvarchar, T_Stock_Status.matType) not in " +
                                   "(select T_Receipt_Main_Det.SourceStoreH + T_Receipt_Main_Det.MatId + CONVERT(Nvarchar, T_Receipt_Main_Det.matType) "+
                                     "from T_Receipt_Main_Det where CurWorkMonth='{0}' and ReceiptTypeID != 'YS' and ReceiptTypeID != 'YF') " +
                             "and BalanceTime = '{1}'";
                sqlSS = string.Format(sqlSS, balanceTime, balanceTime);

                DataTable dtSS = db.Get_Dt(sqlSS);

                this.Invoke((EventHandler)delegate { this.progressBar1.Value = 0; });
                this.Invoke((EventHandler)delegate { this.progressBar1.Minimum = 0; });
                this.Invoke((EventHandler)delegate { this.progressBar1.Maximum = dtSS.Rows.Count; });

                if (dtSS != null && dtSS.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSS.Rows)
                    {
                        string storeHouseId = dr["StoreHouseId"].ToString().Trim();
                        string matId = dr["MatId"].ToString().Trim();
                        int matType = Convert.ToInt32(dr["MatType"].ToString().Trim());
                        double firstCount = Convert.ToDouble(dr["FirstCount"].ToString().Trim());
                        double firstMoney = Convert.ToDouble(dr["FirstMoney"].ToString().Trim());
                        double firstRoadCount = Convert.ToDouble(dr["FirstRoadCount"].ToString().Trim());
                        double firstRoadMoney = Convert.ToDouble(dr["FirstRoadMoney"].ToString().Trim());
                        double firstOutCount = Convert.ToDouble(dr["FirstOutCount"].ToString().Trim());
                        double firstOutMoney = Convert.ToDouble(dr["FirstOutMoney"].ToString().Trim());

                        string strSqlUpdate = "update T_Stock_Status set StockInCount={0},StockInMoney={1},StockOutCount={2},StockOutMoney={3}," +
                                              "LastCount={4},LastMoney={5},LastRoadCount={6},LastRoadMoney={7},LastOutCount={8},LastOutMoney={9} " +
                                              " where  BalanceTime='{10}' and StoreHouseId='{11}' and MatId='{12}' and MatType={13}";
                        strSqlUpdate = string.Format(strSqlUpdate, 0, 0, 0, 0, firstCount, firstMoney, firstRoadCount, firstRoadMoney, firstOutCount, firstOutMoney,
                                                     balanceTime, storeHouseId, matId, matType);
                        sqls.Add(strSqlUpdate);

                        this.Invoke((EventHandler)delegate { this.progressBar1.Value += 1; });
                    }
                }

                db.Exec_Tansaction(sqls); //执行

                //插入下一个结存月的期初信息
                List<string> sqlsIns = new List<string>();
                string nextBalanceTime = Util.GetNextMonth(balanceTime); ;//计算下一个月                

                string strSqlIns = "select * from T_Stock_Status where BalanceTime='{0}'";
                strSqlIns = string.Format(strSqlIns, balanceTime);
                DataTable dtIns = db.Get_Dt(strSqlIns);

                this.Invoke((EventHandler)delegate { this.progressBar1.Value = 0; });
                this.Invoke((EventHandler)delegate { this.progressBar1.Minimum = 0; });
                this.Invoke((EventHandler)delegate { this.progressBar1.Maximum = dtIns.Rows.Count; });
            
                for (int i = 0; i < dtIns.Rows.Count; i++)
                {
                    string storeHouseId = dtIns.Rows[i]["StoreHouseId"].ToString().Trim();
                    string matId = dtIns.Rows[i]["MatId"].ToString().Trim();
                    int matType = Convert.ToInt32(dtIns.Rows[i]["MatType"].ToString().Trim());

                    double lastCount = Convert.ToDouble(dtIns.Rows[i]["LastCount"].ToString().Trim()); //期末数量          
                    double lastMoney = Convert.ToDouble(dtIns.Rows[i]["LastMoney"].ToString().Trim()); //期末金额
                    double lastRoadCount = Convert.ToDouble(dtIns.Rows[i]["LastRoadCount"].ToString().Trim()); //期末在途数量（未冲销03单）
                    double lastRoadMoney = Convert.ToDouble(dtIns.Rows[i]["LastRoadMoney"].ToString().Trim());
                    double lastOutCount = Convert.ToDouble(dtIns.Rows[i]["LastOutCount"].ToString().Trim()); //期末发出数量（75、88数量）
                    double lastOutMoney = Convert.ToDouble(dtIns.Rows[i]["LastOutMoney"].ToString().Trim());

                    string sql = "insert into T_Stock_Status(StoreHouseId,MatId,MatType,BalanceTime,FirstCount,FirstMoney,FirstRoadCount,FirstRoadMoney,FirstOutCount,FirstOutMoney) " +
                                 "values('{0}','{1}',{2},'{3}',{4},{5},{6},{7},{8},{9})";
                    sql = string.Format(sql, storeHouseId, matId, matType, nextBalanceTime, lastCount, lastMoney, lastRoadCount, lastRoadMoney, lastOutCount, lastOutMoney);
                    sqlsIns.Add(sql);

                    this.Invoke((EventHandler)delegate { this.progressBar1.Value += 1; });
                }
                db.Exec_Tansaction(sqlsIns);

                to_ = System.DateTime.Now;
                string strInf = "结存成功！\n开始时间：" + from_.ToShortTimeString() + "\n结束时间：" + to_.ToShortTimeString();

                MessageBox.Show(strInf, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Invoke((EventHandler)delegate
                {
                    this.progressBar1.Value = 0;
                });
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }


    }
}
