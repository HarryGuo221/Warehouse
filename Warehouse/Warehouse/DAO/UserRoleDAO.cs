using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;


namespace Warehouse.DAO
{
    class UserRoleDAO
    {
        public static DataTable GetDatasByUserId(string userId)
        {           
            string strSql = "select T_Users.UserName as 用户名,T_Roles.RoleName as 角色名 from T_Users,T_Roles,T_User_Role " +
                            "where T_User_Role.UserId = '{0}' and T_User_Role.UserId=T_Users.UserId and T_User_Role.RoleId=T_Roles.RoleId";
            strSql = string.Format(strSql, userId);

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            return dt;
        }

    }
}
