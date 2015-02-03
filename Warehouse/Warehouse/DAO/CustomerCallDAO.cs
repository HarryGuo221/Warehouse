using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using System.Data;

namespace Warehouse.DAO
{
    class CustomerCallDAO
    {

        public static DataTable GetDatasofCustomerCall()
        {
            string strSql = "select sysid as 系统编号, CallTime as 召唤时间,CustName as 客户名称,T_CustomerCall. BargId as 合同编号 ," +
                " T_CustomerCall.Contact as 联系人 ,T_CustomerCall.Tel as 联系电话, ReceiptID as 对应业务单据号,SortName as 物料编号, RepairContent as 报修内容," +
                " wcName as 工作类型,UrgentDegree as 紧急程度,Technician1 as 预定技术员, PlanedDay as 预定日 ,ActionResults as 行动结果 from T_CustomerCall left join T_CustomerInf on " +
                " T_CustomerCall.CustCode=T_CustomerInf.CustID  left join T_WorkType on T_CustomerCall.WorkType=T_WorkType.wcid  left join  T_Bargains on T_CustomerCall.BargId=T_Bargains.BargId" +
                " left join T_Material_Sort on T_CustomerCall.ManufactCode=T_Material_Sort.SortId ";
                           
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }
        #region// 删除客户数据
        public void Delete(DataGridView ddv)
        {
            SqlDBConnect sqldb = new SqlDBConnect();
            int indexid = 0;
            indexid = ddv.CurrentRow.Index;
            string sql = "";
            sql = "delete from T_CustomerCall where BargId='" + ddv.Rows[indexid].Cells["合同编号"].Value.ToString() + "'";
            try
            {
                sqldb.ExecuteNonQuery(sql);
                //MessageBox.Show("数据更新成功！");
                ddv.Rows.RemoveAt(ddv.CurrentRow.Index);
                ddv.Refresh();
            }
            catch { MessageBox.Show("数据更新失败或存在外码约束！"); }

            finally
            {
            }
        }
        #endregion
    }
}

        