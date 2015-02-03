using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class SysConfigDAO
    {
        /// <summary>
        /// 获得当前成本计算方式
        /// </summary>
        /// <returns></returns>
        public static string GetCalcMethod()
        {
            string strSql = "select calcmethod from T_SysConfig";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return "";
            else
                return dt.Rows[0]["calcmethod"].ToString().Trim();

        }
    }
}
