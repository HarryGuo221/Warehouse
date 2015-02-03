using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class MatSplitChildDAO
    {
        /// <summary>
        /// 获得拆件管理子表的全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDatas()
        {
            //string strSql = "select T_MatSplit_Child.ReceiptId as 拆件单据编号,T_MatInf.MatName as 子物料," +
            //                "T_MatSplit_Child.ChildMatType as 子物料类型, T_MatSplit_Child.ChildManuCode as 子物料制造编号 " +
            //                "from T_MatSplit_Child,T_Mat_Rela,T_MatInf " +
            //                "where T_MatSplit_Child.ChildMatID=T_Mat_Rela.ChildMatID and T_Mat_Rela.ChildMatID=T_MatInf.MatID";
            string strSql = "select T_MatSplit_Child.ReceiptId as 拆件单据编号,T_MatInf.MatName as 子物料," +
                            "T_MatSplit_Child.ChildMatType as 子物料类型, T_MatSplit_Child.ChildManuCode as 子物料制造编号 " +
                            "from T_MatSplit_Child,T_MatInf " +
                            "where T_MatSplit_Child.ChildMatID=T_MatInf.MatID";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }


        public static void DeleteByReceiptIdAndChildMatID(string strReceiptId, string strChildMatID)
        {
            string strSql = "delete from T_MatSplit_child where ReceiptId='{0}' and ChildMatID='{1}'";
            strSql = string.Format(strSql, strReceiptId, strChildMatID);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }

        public static DataTable SelectDatas(string strReceiptId, string strChildMatID)
        {
            string strSql = "select * from T_MatSplit_child where ReceiptId='{0}' and ChildMatID='{1}'";
            strSql = string.Format(strSql, strReceiptId, strChildMatID);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }
        public static void Insert(string ReceiptId, string MatID, int MatType, string ManufactCode)
        {
            string strSql = "insert into T_MatSplit_child(ReceiptId,ChildMatID,ChildMatType,ChildManuCode) values('{0}','{1}',{2},'{3}')";
            strSql = string.Format(strSql, ReceiptId, MatID, MatType, ManufactCode);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }
        public static void UpdateByReceiptIdAndChildMatID(string strReceiptId, string strChildMatID, int MatType, string ManufactCode)
        {
            string strSql = "update T_MatSplit_child set ChildMatType={0},ChildManuCode='{1}' " +
                            "where ReceiptId='{2}' and  ChildMatID='{3}'";
            strSql = string.Format(strSql, MatType, ManufactCode, strReceiptId, strChildMatID);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }

        //获取拆分零件信息
        public static DataTable GetDatasByReceiptId(string strReceiptId)
        {
            string strSql = "select T_MatSplit_Child.ReceiptId as 拆件单据编号,T_MatInf.MatName as 零件," +
                            "T_MatSplit_Child.ChildMatType as 类型, T_MatSplit_Child.ChildManuCode as 制造编号, " +
                            " T_MatSplit_Child.ChildMatPrice 价格,T_MatSplit_Child.ChildMatNum 数量" +
                            " from T_MatSplit_Child inner join t_matinf on T_MatSplit_Child.ReceiptId='{0}' and T_MatSplit_Child.childmatid=t_matinf.matid";
            strSql = string.Format(strSql, strReceiptId);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }

        //获取还原零件信息
        public static DataTable GetRestoreData(string strReceiptId)
        {
            string strSql = "select T_MatSplit_Child.ReceiptId as 拆件单据编号,T_MatInf.MatName as 零件," +
                            "T_MatSplit_Child.ChildMatType as 类型, T_MatSplit_Child.ChildManuCode as 制造编号, " +
                            " T_MatSplit_Child.ChildMatPrice 价格,T_MatSplit_Child.ChildMatNum 数量" +
                            " from T_MatSplit_Child inner join t_matinf on T_MatSplit_Child.ReceiptId='{0}' and T_MatSplit_Child.childmatid=t_matinf.matid";
            strSql = string.Format(strSql, strReceiptId);
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }
    }
}
