using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class ReceiptModalDAO
    {
        /// <summary>
        /// 获得T_ReceiptModal表的全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDatas()
        {
            string strSql = "select ReceTypeID as 单据模板编号, ReceName as 单据模板名, istemp as 是否临时单据, " +
                            "InOrOutBound as 入库出库, DetailRows as 子表行数, ynAffectStock as 是否操作库存, ynAffectCost as 是否影响成本, " +
                            "ynVerification as 是否可进行核销操作, ynpay as 是否进应收应付账 " +
                            "from T_ReceiptModal ";                       
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt; 
        }
        /// <summary>
        /// 根据 单据模板编号 删除一条记录
        /// </summary>
        /// <param name="receTypeID"></param>
        public static void DeleteByReceTypeID(string receTypeID)
        {
            string strSql = "delete from T_ReceiptModal where ReceTypeID='{0}'";
            strSql = string.Format(strSql, receTypeID);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }


    }
}
