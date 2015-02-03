using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;
using Warehouse.Base;

namespace Warehouse.DAO
{
    class MenuCfgChildDAO
    {
        /// <summary>
        /// 获得T_MenuCfgChild表的全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDatas()
        {
            string strSql = "select T_MenuCfgChild.Sysid as 编号,T_MenuCfgMain.MainMenu as 主菜单项,T_ReceiptModal.ReceName as 子菜单项," +
                            "T_MenuCfgChild.ShortCut as 快捷键, T_MenuCfgChild.OrderIndex as 顺序号 " +
                            "from T_MenuCfgChild left join  T_MenuCfgMain " +
                            "on T_MenuCfgChild.MainMenuId=T_MenuCfgMain.Sysid left join T_ReceiptModal on T_MenuCfgChild.SubMenu=T_ReceiptModal.ReceTypeID "+
                            "order by T_MenuCfgChild.MainMenuId,T_MenuCfgChild.OrderIndex";
            
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }
        /// <summary>
        /// 获得系统编号
        /// </summary>
        /// <param name="intMainMenuId"></param>
        /// <param name="strSubMenu"></param>
        /// <returns></returns>
        public static int GetSysIdByMainMenuIdAndSubMenu(int intMainMenuId, string strSubMenu)
        {
            string strSql = "select SysId from T_MenuCfgChild " +
                            "where MainMenuId={0} and SubMenu='{1}'";
            strSql = string.Format(strSql, intMainMenuId, strSubMenu);

            string strSysId = (new SqlDBConnect()).Ret_Single_val_Sql(strSql);

            return Convert.ToInt32(strSysId);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        public static void Insert(MenuCfgChild menuCfgChild)
        {
            string strSqlInsert = "insert into T_MenuCfgChild(MainMenuId,SubMenu,ShortCut,OrderIndex) values({0},'{1}','{2}',{3})";
            strSqlInsert = string.Format(strSqlInsert, menuCfgChild.MainMenuId, menuCfgChild.SubMenu, menuCfgChild.ShortCut, menuCfgChild.OrderIndex);

            (new SqlDBConnect()).ExecuteNonQuery(strSqlInsert);
        }
        /// <summary>
        /// 根据SysId更新数据
        /// </summary>
        public static void UpdateBySysId(MenuCfgChild menuCfgChild, int intSysId)
        {
            string strSql = "update T_MenuCfgChild set MainMenuId={0},SubMenu='{1}',ShortCut='{2}',OrderIndex={3} " +
                            "where SysId={4}";
            strSql = string.Format(strSql, menuCfgChild.MainMenuId, menuCfgChild.SubMenu, menuCfgChild.ShortCut, menuCfgChild.OrderIndex, intSysId);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }
        /// <summary>
        /// 根据SysId删除数据
        /// </summary>
        /// <param name="intSysId"></param>
        public static void DeleteBySysId(int intSysId)
        {
            string strSql = "delete from T_MenuCfgChild where SysId={0}";
            strSql = string.Format(strSql, intSysId);

            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }

        public static DataTable GetDatasToChildMenu()
        {
            string strSql = "select * from T_MenuCfgChild order by MainMenuId,OrderIndex";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }

    }
}
