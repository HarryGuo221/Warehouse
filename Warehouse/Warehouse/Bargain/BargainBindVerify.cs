using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse
{
    public partial class BargainBindVerify : Form
    {
        public string swheres = "";

        public BargainBindVerify()
        {
            InitializeComponent();
        }

        private void 关闭_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BargainBindVerify_Load(object sender, EventArgs e)
        {
            //核算周期（CheckPeriod），抄张周期（CopyNumGap），
            //预收费周期（ForeadNum），收费类型（FeeType）
            //预收费方式（PreChargeMethod)、首次抄张日(firstCzDate)，租赁计费方式(Jftype)
            string sql_ = ""; 
            sql_ = "select bargid as 合同编号,"
                  + "firstCzDate as 首次抄张日,"
                  +"CheckPeriod as 核算周期,CopyNumGap as 抄张周期,ForeadNum as 预收费周期,"
                  + "FeeType as 收费类型,PreChargeMethod 预收费方式,Jftype as 租赁计费方式 "
                  + " from T_Bargains "+swheres;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_bargs.DataSource = dt.DefaultView;
 
        }
    }
}
