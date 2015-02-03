using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;

namespace Warehouse.DAO
{
    class MatRelaDAO
    {
        //获取数据库中物料耗材信息
        public DataTable getMatRelaInfo(DataTable dt)
        {
            string select_sql = "select ParentMatID 主卡片编号,ChildMatID 子卡片编号,ContainsNumber 配置数量,CopyNumber 平均复印张数," +
                "Memo 备注 from T_Mat_Rela";
            return dt = (new SqlDBConnect()).Get_Dt(select_sql);
        }

        //删除选中行
        public void DeleteMatRela(string ParentMatCode, string ChildMatCode)
        {
            string dele_sql = "delete from T_Mat_Rela where ParentMatID='" + ParentMatCode + "'and ChildMatID='" + ChildMatCode + "'";
            (new SqlDBConnect()).ExecuteNonQuery(dele_sql);
        }
    }
}
