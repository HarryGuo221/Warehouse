using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class MatSplitMainDAO
    {
        /// <summary>
        /// 获得拆件管理主表的全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDatas()
        {
            string strSql = "select distinct T_MatSplit_Main.ReceiptId as 拆件单据编号,T_MatSplit_Main.OccurTime as 发生时间," +
                            "T_MatInf.MatName as 被拆物料,T_MatSplit_Main.MatType as 机器类型, " +
                            "T_MatSplit_Main.ManufactCode as 制造编号,T_Users.UserName as 操作员," +
                            "T_MatSplit_Main.OperateType as 操作类型 from T_MatSplit_Main,T_Mat_Rela,T_MatInf,T_Users " +
                            "where T_MatSplit_Main.MatID=T_Mat_Rela.ParentMatID and T_Mat_Rela.ParentMatID=T_MatInf.MatID " +
                            "and T_MatSplit_Main.OpaUser=T_Users.UserId";
            
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }
        
        public static void DeleteByReceiptId(string strReceiptId)
        {
            string strSql = "delete from T_MatSplit_Main where ReceiptId='{0}'";
            strSql = string.Format(strSql, strReceiptId);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);

        }

        public static DataTable SelectDatas(string strReceiptId)
        {
            string strSql = "select * from T_MatSplit_Main where ReceiptId='{0}'";
            strSql = string.Format(strSql, strReceiptId);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }

    }
}
