using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;
using System.Windows.Forms;

namespace Warehouse.DAO
{
    class MatBasicInfoDAO
    {
        #region//获得物料基本信息数据集
        public DataTable getDatatableOfMatBasic(DataTable dt)
        {
            string sql = "select T_MatInf.MatName 物料名称,T_MatLifeInf.difficulty as 难度系数,T_MatLifeInf.PrnNumMonth as 月承印量,"+
                "T_MatLifeInf.WarrantyNum as 保修到期张数,T_MatLifeInf.WarrantyMonth as 保修期,T_MatLifeInf.LifePeriod 时间使用寿命,"+
                "T_MatLifeInf.LifeNum 印刷使用寿命,T_MatLifeInf.DesignLifePeriod 时间设计寿命,T_MatLifeInf.DesignLifeNum 张数设计寿命,"+
                "T_MatLifeInf.LimitLifePeriod 时间极限寿命,T_MatLifeInf.LimitLifeNum 张数极限寿命,T_MatLifeInf.MatID as 物料编号 from"+
                "T_MatLifeInf,T_MatInf where T_MatLifeInf.MatID=T_MatInf.MatID";
            return (new SqlDBConnect()).Get_Dt(sql);
        }
        #endregion
        public void DeletebyMatSysId(string MatId)
        {
            string deletesql = "delete from T_MatLifeInf where MatID='" + MatId + "'";
            (new SqlDBConnect()).ExecuteNonQuery(deletesql);
        }
    }
}
