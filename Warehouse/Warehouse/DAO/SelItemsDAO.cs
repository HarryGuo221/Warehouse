using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;
using System.Data.SqlClient;
using System.Windows.Forms;
using Warehouse.Base;

namespace Warehouse.DAO
{
    class SelItemsDAO
    {
        #region//显示基础选项及详细信息
        public static DataTable getDatatableOfSelItems()
        {
            string sql = "select * from T_SelItems";
            return (new SqlDBConnect()).Get_Dt(sql);
        }
        public static DataTable getDatasetOfSelItems()
        {
            string sql1 = "select ItemType from T_SelItems";
            DataTable ds = new DataTable();
            return (new SqlDBConnect()).Get_Dt(sql1);
        }
        #endregion

        #region//将基础选项修改内容写回数据库
        public void update_set(DataSet ds)
        {
            SqlDataAdapter sda = new SqlDataAdapter();
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            sda.Update(ds);
            MessageBox.Show("修改成功");
        }
        #endregion

        #region//删除信息写回数据库
        public void update_delete()
        {
            SqlDBConnect sqldbc = new SqlDBConnect();
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter();
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            sda.Update(ds);
        }
        #endregion

        #region//删除选定行基础选项信息
        public void delete_sel(DataGridView data_gv)
        {
            DialogResult dr = MessageBox.Show("确定删除吗", "提醒", MessageBoxButtons.OKCancel);

            if (data_gv.SelectedRows.Count > 0)
            {
                for (int i = 0; i < data_gv.SelectedRows.Count; i++)
                {
                    data_gv.Rows.RemoveAt(data_gv.SelectedRows[i].Index);
                    SelItemsDAO ma = new SelItemsDAO();
                    ma.update_delete();
                }
            }

        }
        #endregion

        /// <summary>
        /// 选择某一 无码“基础数据类型” 对应的值
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public static List<string> GetItemVals(string itemType)
        {
            List<string> itemVals = new List<string>();
            itemVals.Add("--请选择--");

            string strSql = "select ItemVal from T_SelItems where ItemType='{0}'";
            strSql = string.Format(strSql, itemType);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return itemVals;

            foreach (DataRow dr in dt.Rows)
                itemVals.Add(dr["ItemVal"].ToString().Trim());

            return itemVals;
        }
    }
}
