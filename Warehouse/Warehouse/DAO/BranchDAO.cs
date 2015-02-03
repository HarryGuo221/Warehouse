using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.DB;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Warehouse.Sys;

namespace Warehouse.DAO
{
    class BranchDAO
    {
        SqlDBConnect SDB = new SqlDBConnect();

        #region 初始化DataGridView
        /// <summary>
        /// 初始化DataGridView
        /// </summary>
        /// <param name="dataGridView">dgv</param>
        /// <param name="dt">数据集</param>
        public void InitDataGridView(DataGridView dataGridView, DataTable dt)
        {
            InitFuncs IniF = new InitFuncs();
            IniF.InitDataGridView(dataGridView, dt);
        }
        #endregion

        #region 定义一个通用显示在Tab的方法
        /// <summary>
        /// 定义一个通用显示在Tab的方法
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="tablename">表名</param>
        /// <param name="Id">主码</param>
        /// <param name="Panelname">Panelname</param>
        /// <param name="datagrid">dgv</param>
        public void Data_ToTab(string name, string tablename, string Id, Panel Panelname,DataGridView datagrid)
        {
            string ID = datagrid.SelectedRows[0].Cells[name].Value.ToString().Trim();
            string strSql = "select * from {0} where " + Id + "='{1}'";
            strSql = string.Format(strSql, tablename, ID);
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.ShowDatas(Panelname, strSql);
        }
        #endregion

        #region  获得操作表的全部数据(谨记：如果有外码约束且其值可以为空时请用A left outer join B的方法)
        /// <summary>
        /// 获得操作表的全部数据
        /// </summary>
        /// <param name="Table">表名</param>
        /// <returns></returns>
        public DataTable GetDatasOfUsers(string Table)
        {
            string strSql = "";
            switch (Table)
            {
                 case "T_Branch":
                    {
                        strSql = "select T_Branch.BId as 部门编号,T_Branch.BName as 部门名称,T_Users.UserName as 部门主管,T_Branch.memo as 备注 " +
                                "from T_Branch left outer join T_Users on (T_Branch.HeadId=T_Users.UserId)";
                        break;
                    }
                 case "T_Roles":
                    {
                        strSql = "select RoleId as 角色编号,RoleName as 角色名,Rmemo as 备注 from T_Roles";
                        break;
                    }
                 case "T_SysConfig":
                    {
                        strSql = "select SiteCode as 站点代号,SiteName as 站点使用单位名称,UploadTime as 上传时间 ,SHName as 默认仓库,CurWorkMonth as 当前工作月,Addr as 单位地址, Stel as 单位电话, " +
                                "TaxRegNum as 税务登记号,Bank as 开户银行,Contact as 联系人,Sfax as 传真,Semail as Email "+
                                 "from T_SysConfig left outer join T_StoreHouse on (T_SysConfig.DefaStoreHouseId=T_StoreHouse.SHId)";
                        break;
                    }
                case "T_AreaInf":
                    {
                        strSql = "select Areaid as 系统编号,Area as 地区名,ratio as 区域积分 from T_AreaInf";
                        break;
                    }
                case "T_UserType":
                    {
                        strSql ="select TypeId as 操作员类别编号,UtypeName as 类别名 from T_UserType"; 
                        break;
                    }
                case "T_User_Area":
                    {
                        //注意在地区和 '"'之间要留空格，不然就会出现意想不到的错误
                        strSql = "select T_User_Area.UAid as 序列号,T_Users .UserName as 用户名,T_AreaInf .Area as 地区 " +
                                 "from T_Users ,T_AreaInf ,T_User_Area " +
                                  "where (T_Users .UserId =T_User_Area .Userid and T_AreaInf .Areaid =T_User_Area .UAreaid )";
                        break;
                    }
                case "T_UserGrade":
                    {
                        strSql = "select T_UserGrade .GId as 系统等级编码, T_UserType .UTypeName as 类型,T_UserGrade .Gname as 等级名称,T_UserGrade .Para1 as 参数1 " +
                                "from T_UserGrade left outer join T_UserType on (T_UserGrade.Utype=T_UserType .TypeId) ";                        
                        break;
                    }
                case "T_StoreHouse":
                    {
                        strSql = "select T_StoreHouse.SHId as 仓库编号,T_StoreHouse.SHName as 仓库名,SortName as 组织类别, T_Users.UserName as  库管员,T_StoreHouse.SHAddr as 仓库地址, " +
                            "T_StoreHouse.Tel as 电话,T_StoreHouse.Fax as 传真,T_StoreHouse.Email as Email,T_StoreHouse.NetAddr as 网络地址,T_StoreHouse.Storememo as 备注 " +
                            " from T_StoreHouse left outer join T_Users on (T_StoreHouse.SHKeeper=T_Users.UserId)left outer join T_Material_Sort on(T_StoreHouse.Sortcode=T_Material_Sort.SortId)";
                        break;
                    }
                case "T_Invoice":
                    {
                        strSql = "select ITCode as 发票类型编号,ITName as 发票类型名,TaxRate as 税率,InvMemo as 备注 from T_Invoice ";
                        break;
                    }
                case "T_Material_Sort":
                    {
                        strSql = "select SortId as 物料分类编号,SortName as 物料分类名称 from T_Material_Sort";
                        break;
                    }
                case "T_PayMethod":
                    {
                        strSql = "select PMid as 支付编号,PMName as 支付方式,Pmemo as 备注 from T_PayMethod ";
                        break;
                    }
                case "T_CustomerImp":
                    {
                        strSql = "select Iid as 重要度编码,importance as 重要度,CustMemo as 备注 from T_CustomerImp";
                        break;
                    }
                case "T_SelItems":
                    {
                        strSql = "select ItemType as 基础数据类型,ItemVal as 选项数据 from T_SelItems";
                        break;
                    }
                case "T_ServiceType":
                    {
                        strSql = "select scid as 维修类别编码,scName as 维修类别名 from T_ServiceType";
                        break;
                    }
                case "T_WorkType":
                    {
                        strSql = "select wcid as 工作类别编码,wcName as 工作类别名,score as 工作积分 from T_WorkType";
                        break;
                    }
                case "T_Holidays":
                    {
                        strSql = "select Holiday as 节假日,Hmemo as 备注 from T_Holidays";
                        break; 
                    }
                case "T_MachLocate":
                    {
                        strSql = "select machlctcode 部位编码,machlctname 部位名称,memo 备注  from T_MachLocate";
                        break;
                    }
                case "T_machlocatepart":
                    {
                        strSql = "select machlctpartcode 部件编码, T_machLocate.machlctname 所属部位名称,machlctpartname 部件名称 " +
                                "from T_machlocatepart left join T_MachLocate on T_machlocatepart.machlctcode=T_MachLocate.machlctcode";
                        break;
                    }
                case "T_TrbType":
                    {
                        strSql = "select trbcode 故障类别代码,trbmemo 故障类别名称,trbno 其它 from T_TrbType";
                        break;
                    }
                case "T_TrbTypeDtl":
                    {
                        strSql = "select trbcodedtl 故障类别明细代码,T_TrbType.trbmemo 故障类别名称,trbcodename 故障类别名称 from T_TrbTypeDtl " +
                                "left join T_TrbType on T_TrbTypeDtl.trbcode=T_TrbType.trbcode";
                        break;
                    }
                case "T_TelTrbType":
                    {
                        strSql = "select telcode 故障代码,telmemo 故障现象,telno 其它 from T_TelTrbType";
                        break;
                    }
                case "T_machproblem":
                    {
                        strSql = "select probcodeid 系统编号,T_TelTrbType.telmemo 故障现象,probname 故障现象明细 from T_machproblem " +
                                "left join  T_TelTrbType on T_machproblem.telcode=T_TelTrbType.telcode";
                        break;
                    }
                default :
                    break;
            }
            DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
            return dt;
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Mid"></param>
        /// <param name="ID"></param>
        public static void DeleteMethod(string Table,string Mid, string ID)
        {
            string strSql = "delete from {0} where {1}='{2}'";
            strSql = string.Format(strSql, Table,Mid,ID);
            (new SqlDBConnect()).ExecuteNonQuery(strSql);
        }
        #endregion

        #region //通用重置
        public void Resetdata(TextBox controls,Panel panelname)
        {
            controls.ReadOnly = false;
            controls.Focus();
            InitFuncs initFuncs = new InitFuncs();
            initFuncs.ClearData(panelname);
        }
        #endregion

        #region 右键的删除方法
        /// <summary>
        /// 右键的删除方法
        /// </summary>
        /// <param name="datagrid">dgv</param>
        /// <param name="name">列名</param>
        /// <param name="tablename">表名</param>
        /// <param name="Id">主码</param>
        /// <param name="panlename">容器名</param>
        public void Delete(DataGridView datagrid,string name,string tablename,string Id,Panel panlename)
        {
            try
            {
                if (datagrid.SelectedRows.Count <= 0)
                    return;
                string ID = datagrid.SelectedRows[0].Cells[name].Value.ToString().Trim();
                if (ID == "") return;
                DeleteMethod(tablename, Id, ID);
                (new InitFuncs()).ClearData(panlename);
            }
            catch (Exception w)
            {
                MessageBox.Show(w.ToString());
                //MessageBox.Show("该数据与用户表存在外码联系不能删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion 
    }
}



