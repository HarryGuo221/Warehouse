using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;
using System.Windows.Forms;

namespace Warehouse.DAO
{
    class QuerySatisfiDAO
    {
        #region //初始化DataGridView
        public void InitDataGridView(DataGridView dataGridView, DataTable dt)
        {
            InitFuncs IniF = new InitFuncs();
            IniF.InitDataGridView(dataGridView, dt);
        }
        #endregion

        #region //sql语句
        public DataTable GetDatasOfUsers(string Table)
        {
            string strSql = "select sysid as 系统编号,wsCode as 维修工单号, T_CustomerInf.CustName as 客户名称,visitDate as 回访日期," +
                 "VisitTime as 回访时间,VisitContent as 回访内容,isdo as 是否受理,Result as 结果,Attitude as 调度员态度," +
                "ArrivalSpeed as 到位速度,Agility as 操作敏捷度,explanation as 内容说明,Courtesy as 技术员礼节,Score as 总分 from T_QuerySatisfi "+
                "left outer join T_CustomerInf on(T_QuerySatisfi.CustCode=T_CustomerInf.CustID)";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            return dt;
        }
        #endregion
    }
}
