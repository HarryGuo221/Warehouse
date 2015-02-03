using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Windows.Forms; 

namespace Warehouse.DB
{    
    /// <summary>
    /// SqlServer数据库连接类
    /// </summary>
    class SqlDBConnect
{
        #region //数据库，和IP
        public static string db_, ip_, db_jitoa;  //数据库，和IP
        #endregion

        #region //私有成员
        public string cons = "";
        public SqlConnection conn = null;       
        #endregion
        
        #region //构造函数
        public SqlDBConnect()
        {
            cons = "Data Source=" + ip_ + ";Initial Catalog=" + db_ + "; Integrated Security=SSPI";

            conn = new SqlConnection(cons);
        }
        public SqlDBConnect(string DBName)
        {
            cons = "Data Source=" + ip_ + ";Initial Catalog=" + db_jitoa + "; Integrated Security=SSPI";

            conn = new SqlConnection(cons);
        }
        #endregion
         
        #region //打开连接
        public void OpenDb()
        {
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region //关闭连接
        public void CloseDb()
        {
            if (!object.Equals(conn,null) && (conn.State!=ConnectionState.Closed))
            {
                conn.Close();
            }
        }
        #endregion

        #region //释放连接
        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }
        #endregion

        #region  //执行单条SQL(插入、更新、删除)
        /// <summary>
        /// 执行单条SQL(插入、更新、删除)
        /// </summary>
        /// <param name="sql_"></param>
        public void ExecuteNonQuery(string sql_)
        {
            try
            {
                OpenDb();
                SqlCommand cm = new SqlCommand(sql_, conn);
                cm.ExecuteNonQuery();
                cm.Dispose();
                cm = null;
                CloseDb();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString() + "  " + sql_);
            }
        }
        #endregion

        #region  //执行SQL返回首行首列的值
        /// <summary>
        /// 执行SQL返回首行首列的值
        /// </summary>
        public string Ret_Single_val_Sql(string sql_)
        {
            string RetStr = "";
            try
            {
                OpenDb();
                SqlCommand cm = new SqlCommand(sql_, conn);
                RetStr=cm.ExecuteScalar().ToString();
                cm.Dispose();
                cm = null;
                CloseDb();               
            }
            catch(Exception e)
            {
                throw new Exception(e.ToString() + ", " + sql_);
            }

            return RetStr;
        }
        #endregion

        #region  //执行SQL返回DataSet数据集
        /// <summary>
        /// 执行SQL返回DataSet数据集
        /// </summary>
        /// <param name="sql_"></param>
        /// <returns></returns>
        public DataSet Get_Ds(string sql_)
        {
            DataSet ds = null;
            try
            {
                OpenDb();
                SqlDataAdapter myad = new SqlDataAdapter(sql_,conn);
                ds = new DataSet();
                myad.Fill(ds);//用数据适配器填充数据集
                myad.Dispose();
                myad = null;
                CloseDb();
               
            }
            catch(Exception e)
            {
                throw new Exception(e.ToString() + "  " + sql_);
            }
            return ds;
        }
        #endregion

        #region  //执行SQL返回DataTable数据集
        /// <summary>
        /// 执行SQL返回DataTable数据集
        /// </summary>
        /// <param name="sql_"></param>
        /// <returns></returns>
        public DataTable Get_Dt(string sql_)
        {
            DataTable dt = null;
            DataSet ds = null;
            try
            {
                OpenDb();
                SqlDataAdapter myad = new SqlDataAdapter(sql_, conn);
                ds = new DataSet();
                myad.Fill(ds);//用数据适配器填充数据集
                myad.Dispose();
                myad = null;
                CloseDb();

                if (ds.Tables.Count <= 0)
                    return null;
                dt = ds.Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString() + "  " + sql_);
            }
            return dt;
        }
        #endregion

        #region 执行返回多个值的SQL
        public void Get_Multi_Vals(string sql_, ref string v1, ref string v2, ref string v3)
        {
            v1 = "";
            v2 = "";
            v3 = "";
            DataTable dt = Get_Dt(sql_);
            if (dt.Rows.Count > 0)
            {
                v1 = dt.Rows[0][0].ToString().Trim();
                v2 = dt.Rows[0][1].ToString().Trim();
                v3 = dt.Rows[0][2].ToString().Trim();
            }
        }
        #endregion
        

        #region //执行SQL返回指定表的DataSet数据集
        /// <summary>
        /// 执行SQL返回指定表的DataSet数据集
        /// </summary>
        /// <param name="sql_"></param>
        /// <param name="tables"></param>
        /// <returns></returns>
        public DataSet Get_Ds_sl(string sql_, string tables)
        {
            DataSet ds = null;
            try
            {
                OpenDb();
                SqlDataAdapter myad = new SqlDataAdapter(sql_, conn);
                ds = new DataSet();
                myad.Fill(ds, tables);
                myad.Dispose();
                myad = null;
                CloseDb();

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString() + "  " + sql_);
            }
            return ds;
        }
        #endregion

        #region  //执行返回DataReader数据集的SQL(Select)
        /// <summary>
        /// 执行返回DataReader数据集的SQL(Select)
        /// </summary>
        /// <param name="sql_"></param>
        /// <returns></returns>
        public SqlDataReader Get_Rs(string sql_)
        {
            SqlDataReader rs = null;
            try
            {
                OpenDb();
                SqlCommand cm = new SqlCommand(sql_, conn);
                rs = cm.ExecuteReader(CommandBehavior.CloseConnection);
                cm.Dispose();
                cm = null;
                CloseDb();

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString() + "  " + sql_);
            }
            return rs;
        }
        #endregion        
        
        #region //使用事务执行多条SQL(插入、更新、删除)
        /// <summary>
        /// 使用事务执行多条SQL(插入、更新、删除)
        /// </summary>
        /// <param name="sqls"></param>
        public void Exec_Tansaction(List<string> sqls)
        {
            if (sqls.Count == 0) return;

            OpenDb();
            
            // 启动一个事务。 
            SqlTransaction myTran = conn.BeginTransaction();
            // 为事务创建一个命令
            SqlCommand myCom = new SqlCommand();
            myCom.Connection = conn;
            myCom.Transaction = myTran;
            try
            {
                foreach (string sql in sqls)
                {
                    myCom.CommandText = sql;
                    myCom.ExecuteNonQuery();
                }                
                myTran.Commit();//提交事务
            }
            catch (Exception Ex)
            {
                myTran.Rollback();
                
                //返回异常的错误信息 
                //MessageBox.Show("提交数据失败!\n" + Ex.ToString(), "异常信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw new Exception("提交数据失败!\n" + Ex.ToString());                
            }
            finally
            {
                conn.Close();
            }            
        }
        #endregion

        //使用事务的方式，执行多条SQL，并返回插入后的值
        public Int32 Exec_Tansaction_ReturnLast(List<string> sqls)
        {
            if (sqls.Count == 0) return -1;
            Int32 RetVal = -1;
            OpenDb();
            string sql_ = "";
            // 启动一个事务。 
            SqlTransaction myTran = conn.BeginTransaction();
            // 为事务创建一个命令
            SqlCommand myCom = new SqlCommand();
            myCom.Connection = conn;
            myCom.Transaction = myTran;
            try
            {
                for (int i = 0; i < sqls.Count;i++ )
                {
                    sql_ = sqls[i];
                    myCom.CommandText = sql_;
                    if (i != sqls.Count - 1)
                    {
                        myCom.ExecuteNonQuery();
                    }
                    else
                    {
                       RetVal=Convert.ToInt32(myCom.ExecuteScalar());
                    }
                }
                myTran.Commit();  //提交事务
                return RetVal;
            }
            catch (Exception Ex)
            {
                myTran.Rollback();
                return -1;
                //返回异常的错误信息 
                //MessageBox.Show("提交数据失败!\n" + Ex.ToString(), "异常信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw new Exception("提交数据失败!\n" + Ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
                
    }
}
