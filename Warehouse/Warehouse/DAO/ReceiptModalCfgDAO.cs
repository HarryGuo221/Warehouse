using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class ReceiptModalCfgDAO
    {
        /// <summary>
        /// 插入
        /// </summary>
        public static void DeleteByRTypeID(string strRTypeID)
        {
            string strSql = "delete from T_ReceiptModCfg where RTypeID='{0}'";
            strSql = string.Format(strSql, strRTypeID);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }

        public static DataTable GetDatasByRTypeID(string strRTypeID)
        {
            string strSql = "select ShowItem from T_ReceiptModCfg where RTypeID='{0}'";
            strSql = string.Format(strSql, strRTypeID);

            return (new SqlDBConnect()).Get_Dt(strSql);
        }
        public static DataTable GetDatasByRTypeIDAndSummaryOrDetail(string strRTypeID, int SummaryOrDetail)
        {
            string strSql = "select ShowItem from T_ReceiptModCfg where RTypeID='{0}' and SummaryOrDetail={1}";
            strSql = string.Format(strSql, strRTypeID, SummaryOrDetail);

            return (new SqlDBConnect()).Get_Dt(strSql);
        }
        /// <summary>
        /// 获得显示项的已排序的列表
        /// </summary>
        /// <param name="strRTypeID"></param>
        /// <param name="intSummaryOrDetail"></param>
        /// <param name="intUporDown"></param>
        /// <returns></returns>
        public static SortedList<int, string> GetShowItems(string strRTypeID, int intSummaryOrDetail, int intUporDown)
        {
            SortedList<int, string> showItems = new SortedList<int, string>();

            string strSql = "select * from T_ReceiptModCfg where RTypeID='{0}' and SummaryOrDetail={1} and UporDown={2}";
            strSql = string.Format(strSql, strRTypeID, intSummaryOrDetail, intUporDown);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return null;

            showItems.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                showItems.Add(Convert.ToInt32(dr["OrderNum"].ToString().Trim()), dr["ShowItem"].ToString().Trim());
            }

            return showItems;
        }

        public static DataTable GetAllDatasByRTypeID(string strRTypeID)
        {
            string strSql = "select * from T_ReceiptModCfg where RTypeID='{0}'";
            strSql = string.Format(strSql, strRTypeID);

            return (new SqlDBConnect()).Get_Dt(strSql);
        }
        public static bool IsExistData(string tableName, string sqlWhere)
        {
            string strSql = "select * from {0} {1}";
            strSql = string.Format(strSql, tableName, sqlWhere);

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            if (dt.Rows.Count <= 0)
                return false;
            return true;
        }

    }
}
