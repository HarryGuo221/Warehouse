using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Warehouse.DB;
using Warehouse.Base;

namespace Warehouse.Bargain
{
    public partial class BargainBindAddhctype : Form
    {
        public string hctype_;
        public string jbzs_;
        public string myzs_;

        public BargainBindAddhctype()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BargainBindAddhctype_Load(object sender, EventArgs e)
        {
            (new InitFuncs()).Num_limited(this.panel1);
            //初始化无码数据
            InitFuncs inf = new InitFuncs();
            inf.InitComboBox(this.s_HcType, "[合同]幅面");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.s_HcType.SelectedIndex == 0)
            {
                MessageBox.Show("请选择幅面");
                return;
            }
            if (Util.ControlTextIsNUll(this.n_BaseNum))
            {
                MessageBox.Show("请输入基本印量！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.n_BaseNum.Focus();
                return;
            }
            if (Util.ControlTextIsNUll(this.n_myNum))
            {
                MessageBox.Show("请输入免印张数！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.n_myNum.Focus();
                return;
            }
            this.hctype_ = this.s_HcType.Text.Trim();
            this.myzs_ = this.n_myNum.Text.Trim();
            this.jbzs_ = this.n_BaseNum.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }
    }
}
