using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using grproLib;
using System.Data;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Base
{
    /// <summary>
    /// GridppReport 的摘要说明。
    /// </summary>
    class GridReportUtility
    {
        //public const string GetDatabaseConnectionString()  = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\..\Data\Northwind.mdb";

        //此函数用来注册Grid++Report，使Grid++Report不会运行在试用版状态下，你必须在你的应用程序启动时调用此函数
        //用你自己的序列号代替"BS530DTAS2"，"BS530DTAS2"是一个无效的序列号
        public static void RegisterGridppReport()
        {
            GridppReport TempGridppReport = new GridppReport();
            bool Succeeded = TempGridppReport.Register("BS530DTAS2");
            if (!Succeeded)
                System.Windows.Forms.MessageBox.Show("Register Grid++Report Failed, Grid++Report will run in trial mode.", "Register", 
                                                      System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
        }

        private struct MatchFieldPairType
        {
            public IGRField grField;
            public int MatchColumnIndex;
        }
        List<Dictionary<string, string>> files = new List<Dictionary<string, string>>();
        /// <summary>
        /// 将 DataReader 的数据转储到 Grid++Report 的数据集中
        /// </summary>
        /// <param name="Report"></param>
        /// <param name="dr"></param>
        public static void FillRecordToReport(IGridppReport Report, IDataReader dr)
        {
            MatchFieldPairType[] MatchFieldPairs = new MatchFieldPairType[Math.Min(Report.DetailGrid.Recordset.Fields.Count, dr.FieldCount)];

            //根据字段名称与列名称进行匹配，建立DataReader字段与Grid++Report记录集的字段之间的对应关系
            int MatchFieldCount = 0;
            for (int i = 0; i < dr.FieldCount; ++i)
            {
                foreach (IGRField fld in Report.DetailGrid.Recordset.Fields)
                {
                    if (String.Compare(fld.RunningDBField, dr.GetName(i), true) == 0)
                    {
                        MatchFieldPairs[MatchFieldCount].grField = fld;
                        MatchFieldPairs[MatchFieldCount].MatchColumnIndex = i;
                        ++MatchFieldCount;
                        break;
                    }
                }
            }

            // Loop through the contents of the OleDbDataReader object.
            // 将 DataReader 中的每一条记录转储到Grid++Report 的数据集中去
            while (dr.Read())
            {
                Report.DetailGrid.Recordset.Append();

                for (int i = 0; i < MatchFieldCount; ++i)
                {
                    if (!dr.IsDBNull(MatchFieldPairs[i].MatchColumnIndex))
                        MatchFieldPairs[i].grField.Value = dr.GetValue(MatchFieldPairs[i].MatchColumnIndex);
                }

                Report.DetailGrid.Recordset.Post();
            }
        }
        /// <summary>
        /// 将 DataTable 的数据转储到 Grid++Report 的数据集中
        /// </summary>
        /// <param name="Report"></param>
        /// <param name="dt"></param>
        public static void FillRecordToReport(IGridppReport Report, DataTable dt)
        {
            MatchFieldPairType[] MatchFieldPairs = new MatchFieldPairType[Math.Min(Report.DetailGrid.Recordset.Fields.Count, dt.Columns.Count)];

            //根据字段名称与列名称进行匹配，建立DataReader字段与Grid++Report记录集的字段之间的对应关系
            int MatchFieldCount = 0;
            for (int i = 0; i < dt.Columns.Count; ++i)
            {
                foreach (IGRField fld in Report.DetailGrid.Recordset.Fields)
                {
                    if (String.Compare(fld.Name, dt.Columns[i].ColumnName, true) == 0)
                    {
                        MatchFieldPairs[MatchFieldCount].grField = fld;
                        MatchFieldPairs[MatchFieldCount].MatchColumnIndex = i;
                        ++MatchFieldCount;
                        break;
                    }
                }
            }

            // 将 DataTable 中的每一条记录转储到 Grid++Report 的数据集中去
            foreach (DataRow dr in dt.Rows)
            {
                Report.DetailGrid.Recordset.Append();

                for (int i = 0; i < MatchFieldCount; ++i)
                {
                    if (!dr.IsNull(MatchFieldPairs[i].MatchColumnIndex))
                        MatchFieldPairs[i].grField.Value = dr[MatchFieldPairs[i].MatchColumnIndex];
                }

                Report.DetailGrid.Recordset.Post();
            }
        }

        public static uint RGBToOleColor(byte r, byte g, byte b)
        {
            return ((uint)b) * 256 * 256 + ((uint)g) * 256 + r;
        }

        public static uint ColorToOleColor(System.Drawing.Color val)
        {
            return RGBToOleColor(val.R, val.G, val.B);
        }
        
        public static string GetReportTemplatePath()
        {
            return Application.StartupPath + "\\Reports\\";
        }

        public static string GetDatabaseConnectionString()
        {                       
            //string cons = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID={0};Password={1};"+
            //              "Initial Catalog={2};Data Source={3}";
            //cons = string.Format(cons, "sa", "LLzy62563076", SqlDBConnect.db_, SqlDBConnect.ip_);

            return "Provider=SQLOLEDB.1;Persist Security Info=False;" + (new SqlDBConnect()).cons;
        }
    }
}
