using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB; 
using System.Windows.Forms;

namespace Warehouse.DAO
{    
    class BargainDAO
    {
        #region//获取业务合同信息
        public DataTable getBargains()
        {
            DataTable dt=new DataTable();
            //string select_sql = "select T_Bargains.BargId 合同编号,T_Bargains.ManufactCode 制造编号,T_Bargains.SignTime 签定时间," +
            //    "T_CustomerInf.CustName 客户名称,T_Bargains.CustCode 业务单位编码,T_Bargains.InstallDate 安装日期,T_Bargains.InstallResult  安装结果," +
            //    "T_Bargains.StartDate 合同起始日期,T_Bargains.EndDate 合同终止日期,T_Bargains.WarrantyEndDate 保修终止日期," +
            //    "T_Bargains.WarrantyCopyNum 保修复印量,T_Bargains.RenewalEndDate 续保终止日期,T_Bargains.FirstMaintainDate 首次保养日期," +
            //    "T_Bargains.MaintainGap 上门保修周期,T_Bargains.ResponseHour 响应速度,T_Bargains.ChargeMethod 合同收费方式," +
            //    "T_Bargains.RenewalFee 续保费,T_Bargains.WarrantyType 保修类型 ,T_Bargains.SignedType 签约类型," +
            //    "T_Bargains.AllPaperNum 全包张数,T_Bargains.AllToner 全包墨粉,T_Bargains.AllBasicFee 全包基本费用," +
            //    "T_Bargains.UseType 使用类型,T_Bargains.expiredNum 保修到期张数,T_Bargains.ExpectedNum 预计印量," +
            //    "T_Bargains.InitialNum 初始计数器读数,T_Bargains.CheckPeriod 核算周期,T_Bargains.TerminalNum 合同终止张数," +
            //    "T_Bargains.Fee1 保修期内基本印量内单张收费,T_Bargains.Fee2 保修期外基本印量内单张收费, " +
            //    "T_Bargains.Fee3 保修期内基本印量外单张收费,T_Bargains.Fee4 保修期外基本印量外单张收费," +
            //    "T_Bargains.InvoiceType 发票类型,T_Bargains.BillCorp 开票公司,T_Bargains.BillContent 开票内容," +
            //    "T_Bargains.BillTitle 结算单抬头,T_Bargains.PreChargeMethod 预收费方式,T_Bargains.CopyNumGap 抄张周期," +
            //    "T_Bargains.BaseIncPriceNum 起始提价张数,T_Bargains.MySignedPerson 我方签订人, " +
            //    "T_Bargains.HisSignedPerson 对方签订人,T_Bargains.memo 备注 from T_Bargains,T_CustomerInf where" +
            //    " T_CustomerInf.CustID= T_Bargains.CustCode";
            string select_sql = "select T_Bargains.BargId 合同编号,T_Bargains.SignedType 签约类型,T_CustomerInf.CustName 客户名称,T_Bargains.CustCode 业务单位编码,"+
                "T_Bargains.Ynthree as 是否第三方合同,T_Bargains.Payouttype as 合同分类,T_Bargains.ThirdName as 第三方单位名称,T_Bargains.Jftype as 计算方式,T_Bargains.FeeType as 收费类型,T_Bargains.ContractType as 合同类型," +
                "T_Bargains.Periodgap as 合同期时间,T_Bargains.StartDate 合同起始日期,T_Bargains.EndDate 合同终止日期,T_Bargains.TerminalNum 合同终止张数,"+
                "T_Bargains.Endwarry as 保修到期年月,T_Bargains.WarrantyCopyNum 保修复印量,T_Bargains.MaintainGap 上门保修周期,T_Bargains.ResponseHour 响应速度," +
                "T_Bargains.RenewalFee 续保费,T_Bargains.UseType 使用类型,T_Bargains.Endwarry 保修到期年月,T_Bargains.CheckPeriod 核算周期,T_Bargains.TerminalNum 合同终止张数," +
                "T_Bargains.InvoiceType 发票类型,T_Bargains.BillCorp 开票公司,T_Bargains.BillContent 开票内容,T_Bargains.BillTitle 结算单抬头,T_Bargains.PreChargeMethod 预收费方式,"+
                "T_Bargains.CopyNumGap 抄张周期,T_Bargains.memo 备注 from T_Bargains left join T_CustomerInf on T_CustomerInf.CustID= T_Bargains.CustCode";

            
            return dt=(new SqlDBConnect()).Get_Dt(select_sql);
        }
        #endregion

        #region//删除选中合同记录
        public void delete_bargain(string BargId)
        {
            string delete_sql = "delete from T_Bargains where BargId='" + BargId + "'";
            try
            {
                (new SqlDBConnect()).ExecuteNonQuery(delete_sql);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        #endregion


        #region//获取合同计费信息 
        public DataTable getBargFee(string bargid)
        {
            DataTable dt = new DataTable();
            //string select_sql = "select T_BargFee.BargId 合同编号,T_BargFee.FeeName 费用名称,T_BargFee.FeeType 类型," +
            //    "T_BargFee.Period 收费周期 ,T_BargFee.FixMoney 固定费,T_BargFee.NumFrom 数量范围1,T_BargFee.NumTo 数量范围2," +
            //    "T_BargFee.FeeRatio 费率,T_BargFee.Memo 备注 from T_BargFee where BargId='" + bargid + "'";
            string select_sql = "select T_BargFee.Systemid as 系统编号,T_BargFee.HcType 耗材类型,T_BargFee.BaseNum 基本印量,T_BargFee.Fee1 保修期内基本印量内单张收费,"
                + "T_BargFee.fee2 保修期外基本印量内单张收费 ,T_BargFee.fee3 保修期内基本印量外单张收费,T_BargFee.fee4 保修期外基本印量外单张收费,T_BargFee.PageNumAdd 张数递增量,"
                + " T_BargFee.PriceAdd 单价递增量,T_BargFee.MyNum as 免印张数,T_BargFee.Memo as 备注 "
                + " from T_BargFee where BargId='" + bargid + "'";

            return dt = (new SqlDBConnect()).Get_Dt(select_sql);
        }
        #endregion

        #region//删除选中合同计费记录
        public void delete_BargFee(string Systemid)
        {
            string dele_sql = "delete from T_BargFee where Systemid='" + Systemid + "'";
            (new SqlDBConnect()).ExecuteNonQuery(dele_sql);
        }
        #endregion

        #region//获取合同附件信息
        public DataTable getBargAttach(string barsysid)
        {
            DataTable dt;
            string select_sql = "select sysid , barsysid ,AttachName 附件名称,doctype 绑定媒体类型," +
                                "memo 备注 from T_BargAttach where barsysid=" + barsysid ; 
            return dt = (new SqlDBConnect()).Get_Dt(select_sql);
        }
        #endregion

        #region//删除合同附件记录
        public void dele_bargAttach(string barsysid, string attachname)
        {
            string delete_sql = "delete from T_BargAttach where barsysid=" + barsysid + " and AttachName='" + attachname + "'";
            (new SqlDBConnect()).ExecuteNonQuery(delete_sql);
        }
        #endregion


        //
        private void getFile(byte[] content, string filePath)
        {
            string fileName = filePath;
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            //FileStream sw = new FileStream(@fileName, FileMode.Create);
            //StreamWriter fs = new StreamWriter(sw, Encoding.UTF8);
            //fs.Write(entity.Content);
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            fs.Write(content, 0, content.Length);
            fs.Flush();
            fs.Close();
            fileName = System.IO.Path.Combine(Application.StartupPath, fileName);
        }



        //改变文本框读写权限,控制计费类型对应关系
        public void control_readonly(ComboBox s_FeeType, TextBox n_Period, TextBox n_FixMoney,TextBox n_FeeRatio,TextBox n_NumFrom,TextBox n_NumTo)
        {
            if (s_FeeType.Text.Trim() == "按张计费")
            {
                n_Period.ReadOnly = true;
                n_FixMoney.ReadOnly = true;

                n_Period.Clear();
                n_FixMoney.Clear();

                n_FeeRatio.ReadOnly = false;
                n_NumFrom.ReadOnly = false;
                n_NumTo.ReadOnly = false;
            }
            if (s_FeeType.Text.Trim() == "周期性固定计费")
            {
                n_FeeRatio.ReadOnly = true;
                n_NumFrom.ReadOnly = true;
                n_NumTo.ReadOnly = true;
              

                n_FeeRatio.Clear();
                n_NumFrom.Clear();
                n_NumTo.Clear();

                n_Period.ReadOnly = false;
                n_FixMoney.ReadOnly = false;
            }
        }

        public void control_readonly(TextBox n_Period, TextBox n_FixMoney, TextBox n_FeeRatio, TextBox n_NumFrom, TextBox n_NumTo)
        {
            n_Period.ReadOnly = false;
            n_FixMoney.ReadOnly = false;
            n_FeeRatio.ReadOnly = false;
            n_NumFrom.ReadOnly = false;
            n_NumTo.ReadOnly = false;
        }
    }
}
