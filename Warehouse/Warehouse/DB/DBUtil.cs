using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Warehouse.DB
{
    class DBUtil
    {
        #region 检查一个表中的某一个字段是否存在一个对应的值
        /// <summary>
        ///  检查一个表中的某一个字段是否存在一个对应的值
        /// </summary>
        /// <param name="tableName">数据表</param>
        /// <param name="filedName">数据表中的某一字段（字符型）</param>
        /// <param name="filedValue">要查找的值</param>
        /// <returns></returns>
        public bool Is_Exist_Data(string tableName, string filedName, string filedValue)
        {
            string strSql = "select * from {0} where {1}='{2}'";
            strSql = string.Format(strSql, tableName, filedName, filedValue);

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            if (dt.Rows.Count <= 0)
                return false;
            return true;
        }
        /// <summary>
        ///  检查一个表中的某一个字段是否存在一个对应的值
        /// </summary>
        /// <param name="tableName">数据表</param>
        /// <param name="filedName">数据表中的某一字段(整型)</param>
        /// <param name="filedValue">要查找的值</param>
        /// <returns></returns>
        public bool Is_Exist_Data(string tableName, string filedName, int filedValue)
        {
            string strSql = "select * from {0} where {1}={2}";
            strSql = string.Format(strSql, tableName, filedName, filedValue);

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            if (dt == null || dt.Rows.Count <= 0)
                return false;

            return true;
        }
        public bool Is_Exist_Data(string tableName, string filedName1, string filedValue1, string filedName2,string filedValue2)
        {
            string strsql = "select * from {0} where {1}='{2}' and {3}='{4}'";
            strsql = string.Format(strsql, tableName, filedName1, filedValue1, filedName2, filedValue2);
            DataTable dt=new DataTable();
            dt = (new SqlDBConnect()).Get_Dt(strsql);
            if (dt == null || dt.Rows.Count <= 0)
                return false;
            else
                return true;
        }

        #endregion

        #region //检查是否存在数据
        /// <summary> 
        /// 检查是否存在数据
        /// </summary>
        /// <param name="sql_">select语句</param>
        /// <returns></returns>
        public bool yn_exist_data(string sql_)
        {
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(sql_);
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region //取得某一数据表中和某一字段对应的一相应字段的值
        /// <summary>
        /// 取得某一数据表中和某一字段对应的一相应字段的值
        /// </summary>
        /// <param name="tableName">要操作的表名</param>
        /// <param name="d_zdm">目的字段</param>
        /// <param name="s_zdm">原字段名称（字符型字段）</param>
        /// <param name="s_zdz">原字段值</param>
        /// <returns></returns>
        public string Get_Single_val(string tableName, string d_zdm, string s_zdm, string s_zdz)
        {
            string retStr = "";
            string strSql = "select {0} from {1} where {2}='{3}'";
            strSql = string.Format(strSql, d_zdm, tableName, s_zdm, s_zdz);

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            if (dt != null && dt.Rows.Count > 0)
                retStr = dt.Rows[0][d_zdm].ToString().Trim();

            return retStr;
        }
        /// <summary>
        /// 取得某一数据表中和某一字段对应的一相应字段的值
        /// </summary>
        /// <param name="tableName">要操作的表名</param>
        /// <param name="d_zdm">目的字段</param>
        /// <param name="s_zdm">原字段名称(整型字段)</param>
        /// <param name="s_zdz">原字段值</param>
        /// <returns></returns>
        public string Get_Single_val(string tableName, string d_zdm, string s_zdm, int s_zdz)
        {
            string retStr = "";
            string strSql = "select {0} from {1} where {2}={3}";
            strSql = string.Format(strSql, d_zdm, tableName, s_zdm, s_zdz);

            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            if (dt != null && dt.Rows.Count > 0)
                retStr = dt.Rows[0][d_zdm].ToString().Trim();

            return retStr;
        }
        #endregion
        
        #region
        /// <summary>
        /// 取得某一数据表中和某一字段对应的一相应字段的值
        /// </summary>
        /// 传入SQL语句
        /// <returns></returns>
        public string Get_Single_val(string strSql)
        {
            string retStr = "";
            
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(strSql);

            if (dt != null && dt.Rows.Count > 0)
                retStr = dt.Rows[0][0].ToString().Trim();

            return retStr;
        }
        #endregion

        
        
        #region 获取某一表中某一字段值
        /// <summary>
        /// 获取某一表中某一字段值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="filedName"></param>
        /// <returns></returns>
        public List<string> GetOneFiledData(string tableName, string filedName)
        {
            List<string> datas = new List<string>();
            string strSql = "select distinct {0} from {1}";
            strSql = string.Format(strSql, filedName, tableName);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            if (dt == null || dt.Rows.Count <= 0)
                return null;
            foreach (DataRow dr in dt.Rows)
                datas.Add(dr[filedName].ToString().Trim());

            return datas;
        }
        #endregion

        #region //Excel文件操作
        /// <summary>
        /// 获得Excel文件连接
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns>返回连接字符串</returns>
        public static string getConn(string filePath)
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + filePath + ";Extended Properties ='Excel 8.0;HDR=NO;IMEX=1'";
        }
        /// <summary>
        /// 获取Excel文件中所有表的表名
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns>返回</returns>
        public static List<string> getExcelTableNames(string filePath)
        {
            List<string> excelTableNames = new List<string>();            

            //根据路径打开一个Excel文件并将数据填充到DataSet中
            string strConn = getConn(filePath);
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable dataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);//以表格的形式返回Excel文件的相关信息

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string tableName = dataTable.Rows[i]["Table_Name"].ToString();//"Table_Name"是固定的列名
                tableName = tableName.Remove(tableName.Length - 1);//删除表名后的字符“$”

                excelTableNames.Add(tableName);
            }                      

            return excelTableNames;
        }
        /// <summary>
        /// Excel数据导入方法
        /// 作者:lyh
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="dgv">DataGridView</param>
        public static void EcxelToDataGridView(string filePath, string excelTableName, DataGridView dgv)
        {
            //根据路径打开一个Excel文件并将数据填充到DataSet中
            string strConn = getConn(filePath);
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();           

            string strExcel = "select * from [" + excelTableName + "$]";
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, "table1");

            DataTable dt = ds.Tables[0];
            dgv.DataSource = dt.DefaultView;

            //设置数据行选中的模式
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;                       
        }
        /// <summary>
        /// 获得Excel中某一数据表数据
        /// 作者:lyh
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="excelTableName">Excel文件中表的名称</param>
        public static DataTable getEcxelTable(string filePath, string excelTableName)
        {
            //根据路径打开一个Excel文件并将数据填充到DataSet中
            string strConn = getConn(filePath);
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();

            string strExcel = "select * from [" + excelTableName + "$]";
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);
            DataSet dataSet = new DataSet();
            myCommand.Fill(dataSet, "table1");

            DataTable dataTable = dataSet.Tables[0];

            return dataTable;
        }
        #endregion
        
        #region //获取服务器时间
        public static DateTime getServerTime()
        {
            //DateTime now_ = System.DateTime.Now;
            DateTime now_=Convert.ToDateTime("1900-01-01 23:59:59");
            
            string sql_ = "select getdate()";
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(sql_);
            if (dt != null && dt.Rows.Count > 0)
            {
               now_=Convert.ToDateTime(dt.Rows[0][0].ToString());
            }
            return now_;
        }        

        #endregion

        #region //产生单据编号
        //type_:单据类型（２个字符）
        //month_:月份（年月201107）
        public static string Produce_Bill_Id(string type_, string month_,ref string UpdateSql)
        {
            //转换服务器时间为特定格式（如2011-07-01转化为201107）            
            DateTime dateTime = Convert.ToDateTime(month_);
            month_ = dateTime.Year.ToString().Trim() + dateTime.Month.ToString().Trim().PadLeft(2, '0');
           
            int maxno_ = 0;
            string BillId_ = "",tmp="";
            string SqlTmp = "";
            string sql_ = "select maxno from t_ReceiptRule where ReceiptType='"
                 + type_ + "' and whichmonth='" + month_ + "'";
            
            SqlDBConnect db = new SqlDBConnect();
            DataTable dt = db.Get_Dt(sql_);
            if (dt == null || dt.Rows.Count <= 0) //本月第１张
            {
                maxno_ = 1;
                SqlTmp = "insert into t_ReceiptRule(ReceiptType,whichMonth,MaxNo) values('{0}','{1}',{2})";
                SqlTmp = string.Format(SqlTmp, type_.Trim(), month_.Trim(), maxno_.ToString());    
            }
            else
            {
                maxno_ = Convert.ToInt16(dt.Rows[0]["maxno"])+1;
                SqlTmp = "update t_ReceiptRule set MaxNo={0} where ReceiptType='{1}' and whichMonth='{2}'" ;
                SqlTmp = string.Format(SqlTmp, maxno_.ToString(),type_.Trim(), month_.Trim());    
            }
            
            //处理成固定个数的字符
            tmp = maxno_.ToString();
            while (tmp.Length < 5)
            {
                tmp = '0'+tmp ;
            }
            //
            BillId_ = type_.Trim() + month_ + tmp;

            UpdateSql = SqlTmp;
            return BillId_;
        }
        #endregion

    }
}
