using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Warehouse.DB;
using System.Data.SqlClient;
using System.Windows.Forms;
using Warehouse.Base;

namespace Warehouse
{
    ///物料类定义
    class MaterialDAO
    {
        #region//显示物料名称及详细信息
        public static DataTable getDatatableOfMaterial()
        {
            string sql = "select t_matinf.MatID 物料编号, t_matinf.MatName as 物料名称," +
                        "Specifications 型号规格 ,Units 计量单位,Brand 品牌,ProductType 产品种类," +
                        "Models 机种,OrderCode 订货编码,ColorType 色彩,Speed 速度等级,Format 幅面," +
                        "ynStopProduct 是否已停产,ConfigType 配置类型,purchaseprice 标准进货价," +
                        "SaleCalcPrice 业务销售核算成本价,SaleLimitPrice 业务销售限价,GuidePrice 厂方销售指导价," +
                        "LimitPrice 厂方销售限价,LowestPrice 计划销售底价,RetailPrice 零售价,bargainPrice 合同价," +
                        "PlanInPrice 计划进价,MaxStockNum 最高库存,MinStockNum 最低库存,Package 包装," +
                        "ConstantPrice 不变价,OriginPlace 产地,QualityDegree 质量等级,IsMainProduct 是否主要产品," +
                        "Memo 备注,PinYinCode 拼音编码 " +
                        "from T_MatInf";


            return (new SqlDBConnect()).Get_Dt(sql);
        }
        public static DataTable getDatasetOfMaterial()
        {
            string sql1 = "select MatName from T_MatInf";
            DataTable ds = new DataTable();
            return (new SqlDBConnect()).Get_Dt(sql1);
        }
        #endregion

        #region//删除选定行物料信息
        public void delete_mat(string matsysid)
        {
            string delete_sql = "delete from T_MatInf where MatID='{0}'";
            delete_sql = string.Format(delete_sql, matsysid);
            (new SqlDBConnect()).ExecuteNonQuery(delete_sql);
        }
        #endregion
 
        #region//显示技术资料原件信息
        public DataTable getMatDoc(string matid)
        {
            DataTable dt = new DataTable();
            string select_sql = "select T_MatDocs.MatSysId as 附件编号, T_MatDocs.MatId 物料编号, T_MatInf.MatName 物料名称," +
                                "T_MatDocs.DocName 资料名称,T_MatDocs.doctype 媒体类型,T_MatDocs.memo 备注" +
                                " from T_MatDocs inner join T_MatInf on T_MatDocs.MatID=T_MatInf.MatID and T_MatDocs.matid='" + matid + "'";
            return dt = (new SqlDBConnect()).Get_Dt(select_sql);
        }
        #endregion

        #region//删除选定行技术资料原件信息
        public void delete_matdoc(string matid, string docname)
        {
            string delete_sql = "delete from t_matdocs where matid='" + matid + "' and DocName='" + docname + "'";
            (new SqlDBConnect()).ExecuteNonQuery(delete_sql);
        }
        #endregion

        #region//获取物料期限（寿命）信息
        public DataTable getMatLifeInf(string matId)
        {
            DataTable dt;
            string sql_ = "select T_MatLifeInf.matid 物料编号,T_MatLifeInf.difficulty as 难度系数,T_MatLifeInf.PrnNumMonth as 月承印量," +
                        "T_MatLifeInf.WarrantyNum as 保修到期张数,T_MatLifeInf.WarrantyMonth as 保修期,T_MatLifeInf.LifePeriod 时间使用寿命," +
                        "T_MatLifeInf.LifeNum 印刷使用寿命,T_MatLifeInf.DesignLifePeriod 时间设计寿命,T_MatLifeInf.DesignLifeNum 张数设计寿命," +
                        "T_MatLifeInf.LimitLifePeriod 时间极限寿命,T_MatLifeInf.LimitLifeNum 张数极限寿命 " +
                        "from T_MatLifeInf,T_MatInf where T_MatLifeInf.MatID=T_MatInf.MatID and T_MatLifeInf.MatId='{0}'";
            sql_ = string.Format(sql_, matId);

            return dt = (new SqlDBConnect()).Get_Dt(sql_);
        }
        #endregion

        #region//删除选定行寿命信息
        public void delete_matbi(string matid)
        {
            string dele_sql = "delete from T_MatLifeInf where matid='" + matid + "'";
            (new SqlDBConnect()).ExecuteNonQuery(dele_sql);
        }
        #endregion

        #region//获取数据库中物料耗材信息
        public DataTable getMatRelaInfo(string pmatid)
        {
            DataTable dt;
            string select_sql = "select T_MatInf.MatID as 耗材选构件编号, T_MatInf.MatName as 耗材选构件名称,T_Mat_Rela.ContainsNumber as 配置数量,T_Mat_Rela.CopyNumber as 平均复印张数," +
                                "T_Mat_Rela.Memo 备注 from T_Mat_Rela,T_MatInf where " +
                                "T_MatInf.MatID=T_Mat_Rela.ChildMatID and T_Mat_Rela.ParentMatID='" + pmatid + "'";
            //string select_sql = "select T_MatInf.MatID 子物料编号,T_MatInf.MatName 子物料,T_Mat_Rela.ContainsNumber 配置数量,T_Mat_Rela.CopyNumber 平均复印张数," +
            //                  "T_Mat_Rela.Memo 备注 from T_Mat_Rela left join T_MatInf on " +
            //                  "T_MatInf.MatID=T_Mat_Rela.ChildMatID and T_Mat_Rela.ParentMatID='" + pmatid + "'";

            return dt = (new SqlDBConnect()).Get_Dt(select_sql);
        }
        #endregion

        #region//删除选中行物料耗材信息
        public void DeleteMatRela(string ParentMatID, string ChildMatID)
        {
            string dele_sql = "delete from T_Mat_Rela where ParentMatID='" + ParentMatID + "' and ChildMatID='" + ChildMatID + "'";
            (new SqlDBConnect()).ExecuteNonQuery(dele_sql);
        }
        #endregion


        #region//获取物料配套总表信息
        public DataTable getMatAssemblyMain()
        {
            DataTable dt;
            string select_sql = "select sysid, brand as 品牌,"
                     + "AssName 套机名称,Description 套机描述,"
                     + "Memo 备注 from T_MatAssemblyMain";
            return dt = (new SqlDBConnect()).Get_Dt(select_sql);
        }
        #endregion

        #region//删除物料总表配套信息
        public void dele_MatAssemblyMain(int sysid, string matid)
        {
            string dele_sql = "";
            if (matid == "")
                dele_sql = "delete from T_MatAssemblyMain where sysid='" + sysid + "'";
            else
                dele_sql = "delete from T_MatAssemblyChild where sysid='" + sysid + "'" +
                    "and matid='" + matid + "'";
            (new SqlDBConnect()).ExecuteNonQuery(dele_sql);
        }
        #endregion

        #region//获取物料配套子表信息
        public DataTable getMatAssemblyChild(int sysid)
        {
            DataTable dt;

            string sql = "select sysid 配套编号,T_MatAssemblyChild.matid 物料编号,T_MatInf .MatName 物料名称,num 数量 " +
                             "from T_MatAssemblyChild " +
                             "left join T_MatInf on T_MatInf .MatId =T_MatAssemblyChild .matid " +
                             " where sysid='" + sysid + "'";
            return dt = (new SqlDBConnect()).Get_Dt(sql);
        }
        public DataTable getMatAssemblyChild()
        {
            DataTable dt;
            string sql_ = "select sysid 配套编号,T_MatAssemblyChild.matid 物料编号,T_MatInf .MatName 物料名称, " +
                          "num 数量 from T_MatAssemblyChild " +
                          "left join T_MatInf on T_MatInf .MatId =T_MatAssemblyChild .matid ";
            return dt = (new SqlDBConnect()).Get_Dt(sql_);
        }
        #endregion

        #region//删除物料配套子表信息
        public void dele_MatAssemblyChild(int sysid, string matid)
        {
            string dele_sql = "delete from T_MatAssemblyChild where sysid=" + sysid + " and matid='" + matid + "'";
            (new SqlDBConnect()).ExecuteNonQuery(dele_sql);
        }
        #endregion
    }

}