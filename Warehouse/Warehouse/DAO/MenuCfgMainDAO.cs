using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class MenuCfgMainDAO
    {
        /// <summary>
        /// 获得T_MenuCfgMain表的全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDatas()
        {
            string strSql = "select Sysid as 编号, MainMenu as 主菜单项, ShortCut as 快捷键, OrderIndex as 主顺序号 " +
                            "from T_MenuCfgMain order by 主菜单项, 主顺序号";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }
        /// <summary>
        /// 根据 编号 删除一条记录
        /// </summary>
        /// <param name="receTypeID"></param>
        public static void DeleteBySysid(string sysId)
        {
            string strSql = "delete from T_MenuCfgMain where Sysid={0}";
            strSql = string.Format(strSql, sysId);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }

        public static DataTable GetDatasToMainMenu()
        {
            string strSql = "select * from T_MenuCfgMain";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }

    }
}
