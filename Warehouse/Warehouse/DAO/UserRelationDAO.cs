using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class UserRelationDAO
    {
        public static DataTable GetDatas()
        {           
            string strSql = "select T_Users.UserName as 人员, T_UsersView.UserName as 上级人员, T_UserType.UtypeName as 人员类型, T_UserGrade.Gname as 人员等级, T_UserRelation.CanRepair as 可维修机型 " +
                            "from T_UserRelation,T_Users,T_UsersView,T_UserType,T_UserGrade " +
                            "where T_UserRelation.Pid=T_Users.UserId and T_UserRelation.ParentPId=T_UsersView.UserId " +
                            "and T_UserRelation.Utype=T_UserType.TypeId and T_UserRelation.Gid=T_UserGrade.Gid";
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            return dt;
        }

        public static DataTable GetDatasByParentPId(string strParentPId)
        {
            string strSql = "select T_Users.UserName as 下级人员, T_UserType.UtypeName as 人员类型, T_UserGrade.Gname as 人员等级, T_UserRelation.CanRepair as 可维修机型 " +
                            "from T_UserRelation inner join T_Users " +
                            "on T_UserRelation.ParentPId='{0}' and T_UserRelation.Pid=T_Users.UserId left outer join T_UserType " +
                            "on T_UserRelation.Utype=T_UserType.TypeId left outer join T_UserGrade " +
                            "on T_UserRelation.Gid=T_UserGrade.Gid";           
            strSql = string.Format(strSql, strParentPId);

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            return dt;
        }

        public static DataTable GetDatasByParentPIdAndPid(string strParentPId, string strPid)
        {
            string strSql = "select * from T_UserRelation where ParentPId='{0}' and Pid='{1}'";
            strSql = string.Format(strSql, strParentPId, strPid);

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            return dt;
        }
    }
}
