using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;
using System.Windows.Forms;

namespace Warehouse.DAO
{
    class WorkSheetDAO
    {
        #region //sql语句
        public DataTable GetDatasOfUsers(string Table)
        {
            string strSql = "select wsCode as 工单编号,AcceptDay as 受理日期,UserName as 维修技术员,wsStyle as 方式,CustName as 客户名称,CallBillSysId as 对应召唤单号,SortName as 物料名称,ManufactureNum as 制造号码,CallType as 召唤类型,"
                + "ServiceType as 服务类别,WorkContent as 工作内容,RepairContent as 报修内容,startTime as 启动时间,ArriveTime as 到达时间,departTime as 离开时间,TotalCopyNum as 总复印张数,chokedPaperNum as 卡纸张数,mcbj as MCBJ,ColorCopyNum as 彩印张数,"
                + "TotalPlateNum as 总制版张数,TonerStorage as 碳粉存储量,ErrAppearance as 故障现象,ErrPlace as 故障部位,ErrorDetail as 故障详细,ErrPart as 故障部件,ErrDescription as 故障描述,Process as 处理过程,Result as 修理结果,unrepairedReasons as 未修复理由,"
                + "ReportAttach as 报告附表,ClearItem as 清洁项目,purchasePowderDate as 预计购粉日期,purchaseOilDate as 预计购油墨日期,technicalFee as 技术工费,trafficFee as 交通费,RenewalFee as 续保费,InclusiveFee as 全包合同费,OtherFee as 其他费用,"
                + "recyclingDay as 回收预定日,InvoiceCode as 发票号,Suggest as 商家建议和要求,Status as 工单状态,ReturnDate as 返回时间 "
                + " from T_WorkSheet"
                + " left join T_Users on T_WorkSheet.RepairTech=T_Users.UserId"
                + " left join T_CustomerInf on T_WorkSheet.CustCode=T_CustomerInf.CustID"
                + " left Join T_Material_Sort on T_WorkSheet.MatCode=T_Material_Sort.SortId";
                
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            return dt;
        }
        #endregion

        #region //删除方法
        public static void DeleteByWsCode(string WsCode)
        {
            string strSql = "delete from T_WorkSheet where wsCode='{0}'";
            strSql = string.Format(strSql, WsCode);
            (new SqlDBConnect()).ExecuteNonQuery(strSql);
            MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion

        #region //初始化DataGridView
        public void InitDataGridView(DataGridView dataGridView, DataTable dt)
        {
            InitFuncs IniF = new InitFuncs();
            IniF.InitDataGridView(dataGridView, dt);
        }
        #endregion

        #region //传递ID
        public static DataTable SelectDatas(string WsCode)
        {
            string strSql = "select * from T_WorkSheet where wsCode='{0}'";
            strSql = string.Format(strSql, WsCode);

            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);

            return dt;
        }
        #endregion
    }
}
