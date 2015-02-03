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
    /// SqlServer���ݿ�������
    /// </summary>
    class SqlDBConnect
{
        #region //���ݿ⣬��IP
        public static string db_, ip_, db_jitoa;  //���ݿ⣬��IP
        #endregion

        #region //˽�г�Ա
        public string cons = "";
        public SqlConnection conn = null;       
        #endregion
        
        #region //���캯��
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
         
        #region //������
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

        #region //�ر�����
        public void CloseDb()
        {
            if (!object.Equals(conn,null) && (conn.State!=ConnectionState.Closed))
            {
                conn.Close();
            }
        }
        #endregion

        #region //�ͷ�����
        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }
        #endregion

        #region  //ִ�е���SQL(���롢���¡�ɾ��)
        /// <summary>
        /// ִ�е���SQL(���롢���¡�ɾ��)
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

        #region  //ִ��SQL�����������е�ֵ
        /// <summary>
        /// ִ��SQL�����������е�ֵ
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

        #region  //ִ��SQL����DataSet���ݼ�
        /// <summary>
        /// ִ��SQL����DataSet���ݼ�
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
                myad.Fill(ds);//������������������ݼ�
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

        #region  //ִ��SQL����DataTable���ݼ�
        /// <summary>
        /// ִ��SQL����DataTable���ݼ�
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
                myad.Fill(ds);//������������������ݼ�
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

        #region ִ�з��ض��ֵ��SQL
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
        

        #region //ִ��SQL����ָ�����DataSet���ݼ�
        /// <summary>
        /// ִ��SQL����ָ�����DataSet���ݼ�
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

        #region  //ִ�з���DataReader���ݼ���SQL(Select)
        /// <summary>
        /// ִ�з���DataReader���ݼ���SQL(Select)
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
        
        #region //ʹ������ִ�ж���SQL(���롢���¡�ɾ��)
        /// <summary>
        /// ʹ������ִ�ж���SQL(���롢���¡�ɾ��)
        /// </summary>
        /// <param name="sqls"></param>
        public void Exec_Tansaction(List<string> sqls)
        {
            if (sqls.Count == 0) return;

            OpenDb();
            
            // ����һ������ 
            SqlTransaction myTran = conn.BeginTransaction();
            // Ϊ���񴴽�һ������
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
                myTran.Commit();//�ύ����
            }
            catch (Exception Ex)
            {
                myTran.Rollback();
                
                //�����쳣�Ĵ�����Ϣ 
                //MessageBox.Show("�ύ����ʧ��!\n" + Ex.ToString(), "�쳣��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw new Exception("�ύ����ʧ��!\n" + Ex.ToString());                
            }
            finally
            {
                conn.Close();
            }            
        }
        #endregion

        //ʹ������ķ�ʽ��ִ�ж���SQL�������ز�����ֵ
        public Int32 Exec_Tansaction_ReturnLast(List<string> sqls)
        {
            if (sqls.Count == 0) return -1;
            Int32 RetVal = -1;
            OpenDb();
            string sql_ = "";
            // ����һ������ 
            SqlTransaction myTran = conn.BeginTransaction();
            // Ϊ���񴴽�һ������
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
                myTran.Commit();  //�ύ����
                return RetVal;
            }
            catch (Exception Ex)
            {
                myTran.Rollback();
                return -1;
                //�����쳣�Ĵ�����Ϣ 
                //MessageBox.Show("�ύ����ʧ��!\n" + Ex.ToString(), "�쳣��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw new Exception("�ύ����ʧ��!\n" + Ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
                
    }
}
