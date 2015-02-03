using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class RolesDAO
    {
        /// <summary>
        /// 获得RoleId表总的全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDatas()
        {
            string strSql = "select RoleId as 角色编号, RoleName as 角色名,Rmemo as 备注 from T_Roles";

            SqlDBConnect db = new SqlDBConnect();

            return db.Get_Dt(strSql);
        }
        
    }
}
