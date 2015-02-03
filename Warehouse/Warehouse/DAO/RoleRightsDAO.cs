using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.DB;
using System.Data;

namespace Warehouse.DAO
{
    class RoleRightsDAO
    {
        public static List<string> GetFunctionById(string roleId)
        {
            List<string> listFunctions = new List<string>();
            string strSql = "select [Function] from T_Role_Rights where RoleId='{0}'";
            strSql = string.Format(strSql, roleId);

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    listFunctions.Add(dr["Function"].ToString().Trim());
                }
            }

            return listFunctions;
        }



    }
}
