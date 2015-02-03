using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;
using System.Windows.Forms;
 
namespace Warehouse.DAO
{
    class CustContactsDAO
    {
        /// <summary>
        /// 获得操作用户表的全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDatasOfCustContact(string custid)
        {
            string strSql = "select  CName as 联系人姓名,CType as 类别,T_CustContacts.Tel as 联系电话,sex as 性别,Position as 职务, Birthday as 生日,hobby as 兴趣爱好,T_CustContacts. memo as 备注" +
                            " from T_CustContacts where CustID='"+custid +"'";                    
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            return dt;
        }
        #region// 删除数据
        public void Delete(DataGridView ddv)
        {
            SqlDBConnect sqldb = new SqlDBConnect();

            int indexid = 0;
            indexid = ddv.CurrentRow.Index;
            string sql = "";
            sql = "delete from T_CustContacts where CName='" + ddv.Rows[indexid].Cells["联系姓名"].Value.ToString().Trim () + "'";
            try
            {
                sqldb.ExecuteNonQuery(sql);
                MessageBox.Show("删除成功！"); 
                ddv.Rows.RemoveAt(ddv.CurrentRow.Index);
                ddv.Refresh();
            }
            catch { MessageBox.Show("更新失败！"); }

            finally
            {
               
            }

        }

        #endregion
    }
}
