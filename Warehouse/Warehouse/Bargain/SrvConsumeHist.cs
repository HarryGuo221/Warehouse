using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;

namespace Warehouse.Bargain
{
    public partial class SrvConsumeHist : Form
    {
        //public string sheetid;   //工单编号
        public string machineid;   //机器编号
        public SrvConsumeHist()
        {
            InitializeComponent();
        }

        private void show_SheetHist()
        {
            string sql_ = "select "
                + "tl_SrvSaleItem.saledate as 日期,"
                + "tl_SrvSaleItem.partcode as 部品编码,"
                + "tl_SrvSaleItem.partname as 部件名称,"
                + "T_worktype.wcname as 销售类别,"
                + "tl_SrvSaleItem.saleprice as 售价,"
                 + "tl_SrvSaleItem.count as 数量,"
                + "tl_SrvSaleItem.mainte_jz as 给纸部,"
                + "tl_SrvSaleItem.mainte_bs as 搬送部,"
                + "tl_SrvSaleItem.mainte_dy as 定影部,"
                + "tl_SrvSaleItem.mainte_xg as 选购件,"
                + "tl_SrvSaleItem.mainte_cx as 成像部,"
                + "tl_SrvSaleItem.mainte_gx as 光学部,"
                //+ "mainte_zy as xx部,"
                + "tl_SrvSaleItem.salememo as 销售备注,"
                + "tl_SrvSaleItem.invoiceno as 发票号"
                + " from tl_SrvSaleItem "
                +" left join t_worksheet on t_worksheet.sysid=tl_SrvSaleItem.sheetid "
                + " left join T_worktype on tl_SrvSaleItem.saleworktype=T_worktype.wcid "
                + " where t_worksheet.machineid=" + this.machineid;
            DataTable dt = (new SqlDBConnect()).Get_Dt(sql_);
            this.dgv_consume.DataSource = dt.DefaultView;
            this.toolStripStatusLabel2.Text = "记录数：" + dt.Rows.Count.ToString().Trim();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SrvConsumeHist_Load(object sender, EventArgs e)
        {
            this.dgv_consume.AllowUserToAddRows = false;
            show_SheetHist();
        }
    }
}
