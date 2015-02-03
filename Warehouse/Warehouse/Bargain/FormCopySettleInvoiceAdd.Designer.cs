namespace Warehouse.Bargain
{
    partial class FormCopySettleInvoiceAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCopySettleInvoiceAdd));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_New = new System.Windows.Forms.ToolStripButton();
            this.btn_Change = new System.Windows.Forms.ToolStripButton();
            this.btn_Del = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_toSale = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.s_ids = new System.Windows.Forms.TextBox();
            this.s_Rid = new System.Windows.Forms.TextBox();
            this.n_total = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.s_memo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.s_operaUsr = new System.Windows.Forms.TextBox();
            this.s_occurTime = new System.Windows.Forms.TextBox();
            this.s_lx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.s_custname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_tmp = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tmp)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Save,
            this.toolStripSeparator1,
            this.btn_New,
            this.btn_Change,
            this.btn_Del,
            this.toolStripButton4,
            this.toolStripSeparator2,
            this.btn_toSale,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(698, 35);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_Save
            // 
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(33, 32);
            this.btn_Save.Text = "保存";
            this.btn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_Save.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // btn_New
            // 
            this.btn_New.Image = ((System.Drawing.Image)(resources.GetObject("btn_New.Image")));
            this.btn_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_New.Name = "btn_New";
            this.btn_New.Size = new System.Drawing.Size(81, 32);
            this.btn_New.Text = "新增开票明细";
            this.btn_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_New.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // btn_Change
            // 
            this.btn_Change.Image = ((System.Drawing.Image)(resources.GetObject("btn_Change.Image")));
            this.btn_Change.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Change.Name = "btn_Change";
            this.btn_Change.Size = new System.Drawing.Size(81, 32);
            this.btn_Change.Text = "修改发票明细";
            this.btn_Change.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_Change.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // btn_Del
            // 
            this.btn_Del.Image = ((System.Drawing.Image)(resources.GetObject("btn_Del.Image")));
            this.btn_Del.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Size = new System.Drawing.Size(81, 32);
            this.btn_Del.Text = "删除开票明细";
            this.btn_Del.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_Del.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(33, 32);
            this.toolStripButton4.Text = "打印";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // btn_toSale
            // 
            this.btn_toSale.Image = ((System.Drawing.Image)(resources.GetObject("btn_toSale.Image")));
            this.btn_toSale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_toSale.Name = "btn_toSale";
            this.btn_toSale.Size = new System.Drawing.Size(57, 32);
            this.btn_toSale.Text = "转销售单";
            this.btn_toSale.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(33, 32);
            this.toolStripButton2.Text = "关闭";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.s_ids);
            this.panel1.Controls.Add(this.s_Rid);
            this.panel1.Controls.Add(this.n_total);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.s_memo);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.s_operaUsr);
            this.panel1.Controls.Add(this.s_occurTime);
            this.panel1.Controls.Add(this.s_lx);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.s_custname);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 91);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 23;
            // 
            // s_ids
            // 
            this.s_ids.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.s_ids.Location = new System.Drawing.Point(70, 25);
            this.s_ids.Name = "s_ids";
            this.s_ids.Size = new System.Drawing.Size(424, 21);
            this.s_ids.TabIndex = 22;
            this.s_ids.Visible = false;
            // 
            // s_Rid
            // 
            this.s_Rid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.s_Rid.Location = new System.Drawing.Point(564, 62);
            this.s_Rid.Name = "s_Rid";
            this.s_Rid.Size = new System.Drawing.Size(67, 21);
            this.s_Rid.TabIndex = 17;
            this.s_Rid.Visible = false;
            // 
            // n_total
            // 
            this.n_total.Location = new System.Drawing.Point(330, 61);
            this.n_total.Name = "n_total";
            this.n_total.ReadOnly = true;
            this.n_total.Size = new System.Drawing.Size(105, 21);
            this.n_total.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(442, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "(元)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(253, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "开票金额合计";
            // 
            // s_memo
            // 
            this.s_memo.Location = new System.Drawing.Point(70, 34);
            this.s_memo.Multiline = true;
            this.s_memo.Name = "s_memo";
            this.s_memo.Size = new System.Drawing.Size(424, 21);
            this.s_memo.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "备注";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(530, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "制单人";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "制单时间";
            // 
            // s_operaUsr
            // 
            this.s_operaUsr.BackColor = System.Drawing.SystemColors.Control;
            this.s_operaUsr.Location = new System.Drawing.Point(574, 34);
            this.s_operaUsr.Name = "s_operaUsr";
            this.s_operaUsr.ReadOnly = true;
            this.s_operaUsr.Size = new System.Drawing.Size(92, 21);
            this.s_operaUsr.TabIndex = 8;
            // 
            // s_occurTime
            // 
            this.s_occurTime.BackColor = System.Drawing.SystemColors.Control;
            this.s_occurTime.Location = new System.Drawing.Point(70, 61);
            this.s_occurTime.Name = "s_occurTime";
            this.s_occurTime.ReadOnly = true;
            this.s_occurTime.Size = new System.Drawing.Size(114, 21);
            this.s_occurTime.TabIndex = 4;
            // 
            // s_lx
            // 
            this.s_lx.Location = new System.Drawing.Point(574, 7);
            this.s_lx.Name = "s_lx";
            this.s_lx.ReadOnly = true;
            this.s_lx.Size = new System.Drawing.Size(92, 21);
            this.s_lx.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(539, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "类型";
            // 
            // s_custname
            // 
            this.s_custname.Location = new System.Drawing.Point(70, 7);
            this.s_custname.Name = "s_custname";
            this.s_custname.ReadOnly = true;
            this.s_custname.Size = new System.Drawing.Size(424, 21);
            this.s_custname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "单位名称";
            // 
            // dgv_tmp
            // 
            this.dgv_tmp.AllowUserToAddRows = false;
            this.dgv_tmp.AllowUserToDeleteRows = false;
            this.dgv_tmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_tmp.Location = new System.Drawing.Point(0, 126);
            this.dgv_tmp.MultiSelect = false;
            this.dgv_tmp.Name = "dgv_tmp";
            this.dgv_tmp.ReadOnly = true;
            this.dgv_tmp.RowTemplate.Height = 23;
            this.dgv_tmp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tmp.Size = new System.Drawing.Size(698, 164);
            this.dgv_tmp.TabIndex = 4;
            this.dgv_tmp.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_tmp_EditingControlShowing);
            // 
            // FormCopySettleInvoiceAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 290);
            this.Controls.Add(this.dgv_tmp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormCopySettleInvoiceAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "抄张结算开票";
            this.Load += new System.EventHandler(this.FormCopySettleInvoiceAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormCopySettleInvoiceAdd_KeyPress);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tmp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_tmp;
        private System.Windows.Forms.TextBox s_lx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox s_custname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btn_Save;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TextBox s_operaUsr;
        private System.Windows.Forms.TextBox s_occurTime;
        private System.Windows.Forms.TextBox s_memo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox s_Rid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_Del;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox n_total;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton btn_New;
        private System.Windows.Forms.ToolStripButton btn_Change;
        private System.Windows.Forms.TextBox s_ids;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton btn_toSale;

    }
}