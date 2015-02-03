using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class StockStatusDAO
    {
        public DataTable getStockStatusDt(DataTable dt)
        {
            string select_sql = "select T_Stock_Status.MatId 物料编号,T_Stock_Status.MatType 类型,T_Stock_Status.StoreHouseId 仓库编号 ," +
                "T_Stock_Status.Balanceyear 结存年,T_Stock_Status.BalanceMonth 结存月,T_Stock_Status.BalanceTime 结存时间," +
                "T_Stock_Status.LastCount 上期结存数量,T_Stock_Status.LastCost 上期结存成本,T_Stock_Status.LastMoney 上期结存金额," +
                "T_Stock_Status.CountChanged 本期发生数量,T_Stock_Status.MoneyChanged 本期发生金额,T_Stock_Status.CurCost 本期结存成本" +
                "from T_Stock_Status";
            dt = (new SqlDBConnect()).Get_Dt(select_sql);
            return dt;
        }
        public void delete_ss(string matid,string mattype)
        {
            string dele_sql = "delete from T_Stock_Status where MatId='" + matid + "' and MatType='"+mattype+"'";
            (new SqlDBConnect()).ExecuteNonQuery(dele_sql);
        }
        /// <summary>
        /// 获取最近的一次结存的 结存时间
        /// </summary>
        /// <returns></returns>
        public static string GetBalanceTime()
        {
            string maxBalanceTime = "";
            string strSql_ = "select max(BalanceTime) BalanceTime from T_Stock_Status";
            DataTable dt_ = (new SqlDBConnect()).Get_Dt(strSql_);
            if (dt_ != null && dt_.Rows.Count > 0)
                maxBalanceTime = dt_.Rows[0][0].ToString().Trim(); //取最近的一次结存的 结存时间

            return maxBalanceTime;
        }
    }
}
