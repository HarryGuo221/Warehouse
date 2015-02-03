using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using System.Data;

namespace Warehouse.Base
{
    class InfoFind
    {
        /// <summary>
        /// 模糊查找需要的记录，并把“客户编码”赋值给相应控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CustId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control control = sender as Control;
                string controlText = control.Text.Trim();
                if (controlText == "") return;

                string strSql = "select CustID as 客户编码,CustName as 客户名称,PinYinCode as 助记符,communicateAddr as 通信地址, Degree as 客户等级 " +
                                "from T_CustomerInf where CustID like '%{0}%' or CustName like '%{0}%' or PinYinCode like '%{0}%'";
                strSql = string.Format(strSql, controlText);
                
                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("查找的信息不存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    control.Text = "";
                    return;
                }
                if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
                {
                    control.Text = dt.Rows[0]["客户编码"].ToString().Trim();
                }
                else
                {
                    FilterInfoForm form = new FilterInfoForm(control, dt, "客户编码");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
        }
        /// <summary>
        /// 模糊查找需要的记录，并把“客户名称”赋值给相应控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CustName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control control = sender as Control;
                string controlText = control.Text.Trim();
                if (controlText == "") return;

                string strSql = "select CustID as 客户编码,CustName as 客户名称,PinYinCode as 助记符,communicateAddr as 通信地址, Degree as 客户等级 " +
                                "from T_CustomerInf where CustID like '%{0}%' or CustName like '%{0}%' or PinYinCode like '%{0}%'";
                strSql = string.Format(strSql, controlText);

                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("查找的信息不存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    control.Text = "";
                    return;
                }
                if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
                {
                    control.Text = dt.Rows[0]["客户名称"].ToString().Trim();
                }
                else
                {
                    FilterInfoForm form = new FilterInfoForm(control, dt, "客户名称");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }

            }
        }
        /// <summary>
        /// 模糊查找需要的记录，并把“用户编码”赋值给相应控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void UserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control control = sender as Control;
                string controlText = control.Text.Trim();
                if (controlText == "") return;

                string strSql = "select UserId as 用户编码,UserName as 用户名,UserNameZJM as 用户名助记码,OfficeTel as 办公电话, MobileTel as 移动电话 " +
                                "from T_Users where UserId like '%{0}%' or UserName like '%{0}%' or UserNameZJM like '%{0}%'";
                strSql = string.Format(strSql, controlText);

                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("查找的信息不存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    control.Text = "";
                    return;
                }
                if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
                {
                    control.Text = dt.Rows[0]["用户编码"].ToString().Trim();
                }
                else
                {
                    FilterInfoForm form = new FilterInfoForm(control, dt, "用户编码");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
        }
        /// <summary>
        /// 模糊查找需要的记录，并把“用户名”赋值给相应控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void UserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control control = sender as Control;
                string controlText = control.Text.Trim();
                if (controlText == "") return;

                string strSql = "select UserId as 用户编码,UserName as 用户名,UserNameZJM as 用户名助记码,OfficeTel as 办公电话, MobileTel as 移动电话 " +
                                "from T_Users where UserId like '%{0}%' or UserName like '%{0}%' or UserNameZJM like '%{0}%'";
                strSql = string.Format(strSql, controlText);

                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("查找的信息不存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    control.Text = "";
                    return;
                }
                if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
                {
                    control.Text = dt.Rows[0]["用户名"].ToString().Trim();
                }
                else
                {
                    FilterInfoForm form = new FilterInfoForm(control, dt, "用户名");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
        }
        /// <summary>
        /// 模糊查找需要的记录，并把“商品编号”赋值给相应控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MatId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control control = sender as Control;
                string controlText = control.Text.Trim();
                if (controlText == "") return;

                string strSql = "select MatID 商品编号,MatName 商品名称,Specifications 型号规格,Units 计量单位 " +
                                 "from T_MatInf where MatID like '%{0}%' or MatName like '%{0}%'";
                strSql = string.Format(strSql, controlText);

                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("查找的信息不存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    control.Text = "";
                    return;
                }
                if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
                {
                    control.Text = dt.Rows[0]["商品编号"].ToString().Trim();
                }
                else
                {
                    FilterInfoForm form = new FilterInfoForm(control, dt, "商品编号");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
        }
        /// <summary>
        /// 模糊查找需要的记录，并把“商品名称”赋值给相应控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MatName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control control = sender as Control;
                string controlText = control.Text.Trim();
                if (controlText == "") return;

                string strSql = "select MatID 商品编号,MatName 商品名称,Specifications 型号规格,Units 计量单位 " +
                                 "from T_MatInf where MatID like '%{0}%' or MatName like '%{0}%'";
                strSql = string.Format(strSql, controlText);

                DataTable dt = (new SqlDBConnect()).Get_Dt(strSql);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("查找的信息不存在，请重新输入！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    control.Text = "";
                    return;
                }
                if (dt.Rows.Count == 1) //只查找到1条匹配记录，直接赋值
                {
                    control.Text = dt.Rows[0]["商品名称"].ToString().Trim();
                }
                else
                {
                    FilterInfoForm form = new FilterInfoForm(control, dt, "商品名称");
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                }
            }
        }
        /// <summary>
        ///  检查控件中输入的“客户编号”是否存在数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CustId_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            string controlText = control.Text.Trim();
            if (controlText == "") return;

            if (!InitFuncs.isRightValue(controlText, "T_customerinf", "custId"))
            {
                MessageBox.Show("该客户编号不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                control.Text = "";
                return;
            }
        }
        /// <summary>
        ///  检查控件中输入的“客户名称”是否存在数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CustName_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            string controlText = control.Text.Trim();
            if (controlText == "") return;

            if (!InitFuncs.isRightValue(controlText, "T_customerinf", "custName"))
            {
                MessageBox.Show("该客户名称不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                control.Text = "";
                return;
            }
        }
        /// <summary>
        ///  检查控件中输入的“用户编号”是否存在数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void UserId_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            string controlText = control.Text.Trim();
            if (controlText == "") return;

            if (!InitFuncs.isRightValue(controlText, "T_Users", "UserId"))
            {
                MessageBox.Show("该用户编号不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                control.Text = "";
                return;
            }
        }
        /// <summary>
        ///  检查控件中输入的“用户名称”是否存在数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void UserName_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            string controlText = control.Text.Trim();
            if (controlText == "") return;

            if (!InitFuncs.isRightValue(controlText, "T_Users", "UserName"))
            {
                MessageBox.Show("该用户名称不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                control.Text = "";
                return;
            }
        }
        /// <summary>
        ///  检查控件中输入的“物料编号”是否存在数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MatId_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            string controlText = control.Text.Trim();
            if (controlText == "") return;

            if (!InitFuncs.isRightValue(controlText, "T_MatInf", "MatId"))
            {
                MessageBox.Show("该物料编号不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                control.Text = "";
                return;
            }
        }
        /// <summary>
        ///  检查控件中输入的“物料名称”是否存在数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MatName_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            string controlText = control.Text.Trim();
            if (controlText == "") return;

            if (!InitFuncs.isRightValue(controlText, "T_MatInf", "MatName"))
            {
                MessageBox.Show("该物料名称不存在！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                control.Text = "";
                return;
            }
        }
        /// <summary>
        /// 高级查询，返回“物料编码”
        /// </summary>
        /// <returns></returns>
        public static string Find_MatId()
        {
            string strMatID = "";
            WFilter wf = new WFilter(0, "MatID", true);
            wf.tableName = "T_MatInf";    //表名  
            wf.strSql = "select T_MatInf.MatID 物料编号,MatName 物料名称,Specifications 型号规格,Units 计量单位,Brand 品牌,ProductType 产品种类," +
                        "ColorType 色彩,Speed 速度等级,Format 幅面,Models 机种,ynStopProduct 是否已停产,ConfigType 配置类型 " +
                        " from T_MatInf";

            wf.s_items.Add("物料编号,MatID,C");
            wf.s_items.Add("物料名称,MatName,C");
            wf.s_items.Add("型号规格,Specifications,C");
            wf.s_items.Add("品牌,Brand,C");
            wf.s_items.Add("产品种类,ProductType,C");
            wf.s_items.Add("机种,Models,C");
            wf.s_items.Add("配置类型,ConfigType,C");
            wf.ShowDialog();           

            if (wf.DialogResult == DialogResult.OK)
            {
                if (wf.Return_Items.Count <= 0)
                    return strMatID;
                strMatID = wf.Return_Items[0].Trim();//获得商品编号
            }
            return strMatID;
        }
        /// <summary>
        /// 高级查询，返回“物料名称”
        /// </summary>
        /// <returns></returns>
        public static string Find_MatName()
        {
            string strMatName = "";
            WFilter wf = new WFilter(0, "MatName", true);
            wf.tableName = "T_MatInf";    //表名  
            wf.strSql = "select T_MatInf.MatID 物料编号,MatName 物料名称,Specifications 型号规格,Units 计量单位,Brand 品牌,ProductType 产品种类," +
                        "ColorType 色彩,Speed 速度等级,Format 幅面,Models 机种,ynStopProduct 是否已停产,ConfigType 配置类型 " +
                        " from T_MatInf";

            wf.s_items.Add("物料编号,MatID,C");
            wf.s_items.Add("物料名称,MatName,C");
            wf.s_items.Add("品牌,Brand,C");
            wf.s_items.Add("产品种类,ProductType,C");
            wf.s_items.Add("机种,Models,C");
            wf.s_items.Add("配置类型,ConfigType,C");
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                if (wf.Return_Items.Count <= 0)
                    return strMatName;
                strMatName = wf.Return_Items[0].Trim();//获得物料名称
            }
            return strMatName;
        }
        /// <summary>
        /// 高级查询，返回“客户编号”
        /// </summary>
        /// <returns></returns>
        public static string Find_CustId()
        {
            string strCustId = "";
            WFilter wf = new WFilter(0, "CustID", true);
            wf.tableName = "T_CustomerInf";    //表名    
            wf.strSql = "select CustID as 客户编号, CustName as 客户名称, CustType as 类别,PinYinCode as 拼音助记码,whichTrade as 所在行业, City as 城市地区,Province as 省," +
                        "communicateAddr as 通信地址,BankAccount as 银行帐号, CredDegree as 信用等级, InvoiceTitle as 发票抬头 " +
                        "from T_CustomerInf";

            wf.s_items.Add("客户编号,CustID,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("拼音助记码,PinYinCode,C");
            wf.s_items.Add("类别,CustType.CName,C");
            wf.s_items.Add("所在行业,whichTrade,C");
            wf.s_items.Add("通信地址,communicateAddr,C");
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                if (wf.Return_Items.Count <= 0)
                    return strCustId;
                strCustId = wf.Return_Items[0].Trim();//客户编号
            }
            return strCustId;
        }
        /// <summary>
        /// 高级查询，返回“客户名称”
        /// </summary>
        /// <returns></returns>
        public static string Find_CustName()
        {
            string strCustName = "";
            WFilter wf = new WFilter(0, "CustName", true);
            wf.tableName = "T_CustomerInf";    //表名    
            wf.strSql = "select CustID as 客户编号, CustName as 客户名称, CustType as 类别,PinYinCode as 拼音助记码,whichTrade as 所在行业, City as 城市地区,Province as 省," +
                        "communicateAddr as 通信地址,BankAccount as 银行帐号, CredDegree as 信用等级, InvoiceTitle as 发票抬头 " +
                        "from T_CustomerInf";

            wf.s_items.Add("客户编号,CustID,C");
            wf.s_items.Add("客户名称,CustName,C");
            wf.s_items.Add("拼音助记码,PinYinCode,C");
            wf.s_items.Add("类别,CustType.CName,C");
            wf.s_items.Add("所在行业,whichTrade,C");
            wf.s_items.Add("通信地址,communicateAddr,C");
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                if (wf.Return_Items.Count <= 0)
                    return strCustName;
                strCustName = wf.Return_Items[0].Trim();//客户名称
            }
            return strCustName;
        }
        /// <summary>
        /// 高级查询，返回“用户编号”
        /// </summary>
        /// <returns></returns>
        public static string Find_UserId()
        {
            string strUserId = "";
            WFilter wf = new WFilter(0, "UserId", true);
            wf.tableName = "T_users";    //表名             
            wf.strSql = "select T_Users.UserId as 用户编码, T_Users.UserName as 用户名, T_Users.ynAdmin as 是否系统管理员,T_Branch.BName as 所属部门," +
                        "T_Users.JobPosition as 职位,T_UserType.UTypeName as 类别,T_Users.SmsTel as 接收短信电话号码 " +
                        "from [T_Users] left join T_Branch " +
                        "on T_Users.BranchId=T_Branch.BId left join T_UserType on T_Users.DefaultUserType=T_UserType.TypeId";

            wf.s_items.Add("用户编码,UserId,C");
            wf.s_items.Add("用户名,UserName,C");
            wf.s_items.Add("所属部门,BName,C");
            wf.s_items.Add("职位,JobPosition,C");
            wf.s_items.Add("类别,UTypeName,C");          
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                if (wf.Return_Items.Count <= 0)
                    return strUserId;
                strUserId = wf.Return_Items[0].Trim();//用户编码
            }
            return strUserId;
        }
        /// <summary>
        /// 高级查询，返回“用户名”
        /// </summary>
        /// <returns></returns>
        public static string Find_UserName()
        {
            string strUserName = "";
            WFilter wf = new WFilter(0, "UserName", true);
            wf.tableName = "T_users";    //表名             
            wf.strSql = "select T_Users.UserId as 用户编码, T_Users.UserName as 用户名, T_Users.ynAdmin as 是否系统管理员,T_Branch.BName as 所属部门," +
                        "T_Users.JobPosition as 职位,T_UserType.UTypeName as 类别,T_Users.SmsTel as 接收短信电话号码 " +
                        "from [T_Users] left join T_Branch " +
                        "on T_Users.BranchId=T_Branch.BId left join T_UserType on T_Users.DefaultUserType=T_UserType.TypeId";

            wf.s_items.Add("用户编码,UserId,C");
            wf.s_items.Add("用户名,UserName,C");
            wf.s_items.Add("所属部门,BName,C");
            wf.s_items.Add("职位,JobPosition,C");
            wf.s_items.Add("类别,UTypeName,C");
            wf.ShowDialog();

            if (wf.DialogResult == DialogResult.OK)
            {
                if (wf.Return_Items.Count <= 0)
                    return strUserName;
                strUserName = wf.Return_Items[0].Trim();//用户名
            }
            return strUserName;
        }

    }
}
