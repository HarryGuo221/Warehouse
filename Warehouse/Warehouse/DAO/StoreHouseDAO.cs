using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Base;
using Warehouse.DB;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Warehouse.DAO
{
    class StoreHouseDAO
    { 
        SqlDBConnect sqldb = new DB.SqlDBConnect();
        public  DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        
        #region//得到DataTable数据集
        public static DataTable getDataTableOfStoreHouse()
        {
            string strSql = "select T_StoreHouse.SHId as 仓库编号,T_StoreHouse.SHName as 仓库名," +
                            "T_StoreHouse.SHKeeper as 库管员,T_StoreHouse.SHAddr as 仓库地址," +
                            "T_StoreHouse.Tel as 电话,T_StoreHouse.Fax as 传真,T_StoreHouse.NetAddr 网络地址," +
                            "T_StoreHouse.Storememo as 备注 from T_StoreHouse";
            return (new SqlDBConnect()).Get_Dt(strSql);
        }
        #endregion

        #region//删除仓库数据信息
        public static void DeleteBySHSysId(string SHSysId)
        {
            string strSql = "delete from T_StoreHouse where SHId={0}";
            strSql = string.Format(strSql, SHSysId);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }
        #endregion
    }
}
