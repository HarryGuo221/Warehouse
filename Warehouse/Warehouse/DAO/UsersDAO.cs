using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.DB;
using Warehouse.Base;
using System.Data;
using System.Windows.Forms;
 
namespace Warehouse.DAO
{
    /// <summary>
    /// 用户 数据操作类
    /// </summary>
    class UsersDAO
    {
        /// <summary>
        /// 根据用户名和密码查找匹配记录
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="strPassWord"></param>
        /// <returns></returns>
        public static string GetReturn(string strUserId, string strPassWord)
        {
            string strSql = "select count(*) from [T_Users] where [UserId] = '{0}' and [PassWord] = '{1}'";
            strSql = string.Format(strSql, strUserId, strPassWord);

            return (new SqlDBConnect()).Ret_Single_val_Sql(strSql);
        }
        /// <summary>
        /// 获得操作用户表的全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDatasOfUsers()
        {
            string strSql = "select distinct T_Users.UserId as 用户编码, T_Users.UserName as 用户名,T_Users.UserNameZJM as 助记码, T_Users.ynAdmin as 是否系统管理员,hasSpecial as 是否有特殊权限," +
                            "T_Branch.BName as 所属部门,T_Users.JobPosition as 职位,T_UserType.UTypeName as 类别, perstype as 用户类型," +
                            "groups as 所属组,"+
                            "OfficeTel as 办公电话, MobileTel as 移动电话, Email as Email,T_Users.SmsTel as 接收短信电话号码, " +
                            "IdNumber as 身份证号码,Addr as 地址, T_Users.memo as 备注 " +
                            "from [T_Users] left join T_Branch "+
                            "on T_Users.BranchId=T_Branch.BId left join T_UserType "+
                            "on T_Users.DefaultUserType=T_UserType.TypeId";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt; 
        } 
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="strSql">insert</param>        
        public static void Insert(string strSql)
        {
            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }
        /// <summary>
        /// 根据用户名更新
        /// </summary>
        /// <param name="userNameValue"></param>
        public static void UpdateByUserId(string strSql)
        {
            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }
        /// <summary>
        /// 根据用户名删除
        /// </summary>
        /// <param name="userNameValue"></param>
        public static void DeleteByUserId(string userId)
        {
            try
            {
                string strSql = "delete from T_Users where UserId='{0}'";
                strSql = string.Format(strSql, userId);

                (new SqlDBConnect()).ExecuteNonQuery(strSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        /// <summary>
        /// 根据 用户编码 获得 部门名称
        /// </summary>
        /// <param name="strUserId"></param>
        /// <returns></returns>
        public static string GetBranchName(string strUserId)
        {
            string strSql = "select T_Branch.BName from T_Branch, T_Users where T_Users.BranchId=T_Branch.BId and T_Users.UserId='{0}'";
            strSql = string.Format(strSql, strUserId);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            if (dt == null || dt.Rows.Count <= 0)
                return "";

            return dt.Rows[0]["BName"].ToString().Trim();
        }
        /// <summary>
        /// 根据 用户编码 获得 操作员类别名
        /// </summary>
        /// <param name="strUserId"></param>
        /// <returns></returns>
        public static string GetUTypeName(string strUserId)
        {
            string strSql = "select T_UserType.UTypeName from T_UserType, T_Users where T_Users.DefaultUserType=T_UserType.TypeId and T_Users.UserId='{0}'";
            strSql = string.Format(strSql, strUserId);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            if (dt == null || dt.Rows.Count <= 0)
                return "";

            return dt.Rows[0]["UTypeName"].ToString().Trim();
        }
        /// <summary>
        /// 根据 用户编码 获得是否为 系统管理员
        /// </summary>
        /// <param name="strUserId"></param>
        /// <returns></returns>
        public static string GetYNAdmin(string strUserId)
        {
            string strSql = "select ynAdmin from T_Users where T_Users.UserId='{0}'";
            strSql = string.Format(strSql, strUserId);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            if (dt == null || dt.Rows.Count <= 0)
                return "";

            return dt.Rows[0]["ynAdmin"].ToString().Trim();
        }
        

    }
}
