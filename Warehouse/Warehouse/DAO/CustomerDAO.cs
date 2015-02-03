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
    class CustomerDAO
    {
        public static DataTable GetDatasOfCustomer()
        {
            string strSql = "select CustID as 客户编号, CustName as 客户名称, PinYinCode as 拼音助记码,"+
              " communicateAddr as 通信地址,CustType as 类别,whichTrade as 所在行业,City as 城市地区,BillAddr as 发票寄送地址," +
             " DeliveryTitle as 送货抬头,TaxRegistNumber as 税务登记号,AtBank as 开户银行,BankAccount as 银行账号, CredDegree as 信用等级 ," +
             "CredDegreeMoney as 信用金额,Area as 管理区号,AccountPeriod as '默认账期(天)'," +
            " T_CustomerImp.importance as 重要度,Fax as 传真,Email,InputTime as 输入日期 ,Memo as 备注 " +
            " from T_CustomerInf left join T_CustomerImp on T_CustomerInf.ImportanceDegreeId=T_CustomerImp.Iid " +
            " left join T_AreaInf on T_CustomerInf.AreaCode =T_AreaInf.Areaid";
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }
        
        #region// 删除客户数据
        public void Delete(DataGridView ddv)
        {
            int indexid = 0;
            indexid = ddv.CurrentRow.Index;
            string sql = "";
            sql = "delete from T_CustomerInf where CustID='" + ddv.Rows[indexid].Cells[0].Value.ToString() + "'";
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(sql);
                //ddv.Rows.RemoveAt(ddv.CurrentRow.Index);
                ddv.Refresh();
            }
            catch { MessageBox.Show("数据更新失败或存在外码约束！"); }

            finally
            {
            }
        }

        #endregion

        public static DataTable GetDataOfCustomerRela(string strID)
        {
            string strSql = "select T_CustomerInf.CustName as 下级客户名称,T_Customer_Rela.InvoiceTitle as 发票抬头,T_Customer_Rela.Memo as 备注 " +
                            "from T_Customer_Rela inner join T_CustomerInf" +
                            " on T_Customer_Rela.CustID='{0}' and T_Customer_Rela.ParentID=T_CustomerInf.CustID  ";
            strSql = string.Format(strSql, strID);         
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            return dt;
            //DataTable dts = new DataTable("customerrela");
            //DataColumn primadts =dts. Columns.Add("客户姓名", typeof(String));
            //primadts.AllowDBNull =false ;
            //dts.Columns.Add("父客户姓名", typeof(String));
            //dts.Columns.Add("发票抬头", typeof(String));
            //dts.Columns.Add("备注", typeof(String));
            
            //for (int i = 0; i <= dt.Rows.Count-1; i++)
            //    {
            //        string sql1 = "select CustName " +
            //                "from T_CustomerInf where CustID='" + dt.Rows[i][0].ToString() + "'"; 
            //        string custname=( new SqlDBConnect ()).Ret_Single_val_Sql(sql1 );
            //        string sql2 = "select CustName " +
            //                "from T_CustomerInf where CustID='" + dt.Rows[i][1].ToString() + "'";
            //        string parentcustname = (new SqlDBConnect()).Ret_Single_val_Sql(sql2); 

            //        DataRow newrow = dts.NewRow();
            //        newrow["客户姓名"] = custname;
            //        newrow["父客户姓名"] = parentcustname;
            //        newrow["发票抬头"] = dt.Rows[i][2].ToString ();
            //        newrow["备注"] = dt.Rows[i][3].ToString();
            //        dts.Rows.Add(newrow);
            //    }
            //return dts;
        }
        #region// 删除客户关系数据
        public void DeleteRela(DataGridView ddv)
        {
            SqlDBConnect sqldb = new SqlDBConnect();
            int indexid = 0;
            indexid = ddv.CurrentRow.Index;
            string sqlcustid = "select CustID " +
                           "from T_CustomerInf where CustName='" + ddv.Rows[indexid].Cells[0].Value.ToString().Trim() + "'";
            string custid= (new SqlDBConnect()).Ret_Single_val_Sql(sqlcustid);
            string sqlparentcustid = "select CustID " +
                           "from T_CustomerInf where CustName='" + ddv.Rows[indexid].Cells[1].Value.ToString().Trim() + "'";
            string parentcustid = (new SqlDBConnect()).Ret_Single_val_Sql(sqlparentcustid);

            string sql = "delete from T_Customer_Rela where CustID='" + custid + "' and ParentID='" + parentcustid + "'";
            try
            {
                sqldb.ExecuteNonQuery(sql);
                MessageBox.Show("数据更新成功！");
                ddv.Rows.RemoveAt(ddv.CurrentRow.Index);
                ddv.Refresh();
            }
            catch
            { MessageBox.Show("数据更新失败！"); }

            finally
            {
            }
        }
        #endregion
    }
}

  