namespace Warehouse
{
    partial class WFilter
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lb_Items = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_field = new System.Windows.Forms.Label();
            this.lb_ts = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.tb2 = new System.Windows.Forms.TextBox();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.cb_opa = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除选中行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lb_where = new System.Windows.Forms.ListBox();
            this.lb_sql = new System.Windows.Forms.ListBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lb_Items);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 251);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件可选列";
            // 
            // lb_Items
            // 
            this.lb_Items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Items.FormattingEnabled = true;
            this.lb_Items.ItemHeight = 12;
            this.lb_Items.Location = new System.Drawing.Point(3, 17);
            this.lb_Items.Name = "lb_Items";
            this.lb_Items.Size = new System.Drawing.Size(167, 231);
            this.lb_Items.TabIndex = 0;
            this.lb_Items.SelectedIndexChanged += new System.EventHandler(this.lb_Items_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lb_field);
            this.groupBox2.Controls.Add(this.lb_ts);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.tb2);
            this.groupBox2.Controls.Add(this.tb1);
            this.groupBox2.Controls.Add(this.cb_opa);
            this.groupBox2.Location = new System.Drawing.Point(191, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 91);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "条件设定";
            // 
            // lb_field
            // 
            this.lb_field.AutoSize = true;
            this.lb_field.Location = new System.Drawing.Point(74, 20);
            this.lb_field.Name = "lb_field";
            this.lb_field.Size = new System.Drawing.Size(41, 12);
            this.lb_field.TabIndex = 5;
            this.lb_field.Text = "label2";
            this.lb_field.Visible = false;
            // 
            // lb_ts
            // 
            this.lb_ts.AutoSize = true;
            this.lb_ts.Location = new System.Drawing.Point(15, 20);
            this.lb_ts.Name = "lb_ts";
            this.lb_ts.Size = new System.Drawing.Size(53, 12);
            this.lb_ts.TabIndex = 4;
            this.lb_ts.Text = "字段名：";
            this.lb_ts.Visible = false;
            // 
            // button4
            // 
            this.button4.Image = global::Warehouse.Properties.Resources.ARROW3D1;
            this.button4.Location = new System.Drawing.Point(192, 37);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(33, 23);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tb2
            // 
            this.tb2.Location = new System.Drawing.Point(86, 63);
            this.tb2.Name = "tb2";
            this.tb2.Size = new System.Drawing.Size(100, 21);
            this.tb2.TabIndex = 2;
            this.tb2.Visible = false;
            // 
            // tb1
            // 
            this.tb1.Location = new System.Drawing.Point(86, 39);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(100, 21);
            this.tb1.TabIndex = 1;
            // 
            // cb_opa
            // 
            this.cb_opa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_opa.FormattingEnabled = true;
            this.cb_opa.ItemHeight = 12;
            this.cb_opa.Location = new System.Drawing.Point(11, 40);
            this.cb_opa.Name = "cb_opa";
            this.cb_opa.Size = new System.Drawing.Size(69, 20);
            this.cb_opa.TabIndex = 0;
            this.cb_opa.SelectedValueChanged += new System.EventHandler(this.cb_opa_SelectedValueChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(217, 245);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(54, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "查询";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(355, 245);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "重置条件";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(288, 245);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(46, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "确定";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除选中行ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(131, 26);
            // 
            // 删除选中行ToolStripMenuItem
            // 
            this.删除选中行ToolStripMenuItem.Name = "删除选中行ToolStripMenuItem";
            this.删除选中行ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.删除选中行ToolStripMenuItem.Text = "删除选中行";
            this.删除选中行ToolStripMenuItem.Click += new System.EventHandler(this.删除选中行ToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lb_where);
            this.groupBox3.Controls.Add(this.lb_sql);
            this.groupBox3.Location = new System.Drawing.Point(191, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(247, 127);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "已设定的条件";
            // 
            // lb_where
            // 
            this.lb_where.ContextMenuStrip = this.contextMenuStrip1;
            this.lb_where.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_where.FormattingEnabled = true;
            this.lb_where.ItemHeight = 12;
            this.lb_where.Location = new System.Drawing.Point(3, 17);
            this.lb_where.Name = "lb_where";
            this.lb_where.Size = new System.Drawing.Size(241, 55);
            this.lb_where.TabIndex = 4;
            // 
            // lb_sql
            // 
            this.lb_sql.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb_sql.FormattingEnabled = true;
            this.lb_sql.ItemHeight = 12;
            this.lb_sql.Location = new System.Drawing.Point(3, 72);
            this.lb_sql.Name = "lb_sql";
            this.lb_sql.Size = new System.Drawing.Size(241, 52);
            this.lb_sql.TabIndex = 0;
            this.lb_sql.Visible = false;
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 279);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(417, 156);
            this.dataGridView.TabIndex = 8;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            // 
            // WFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 447);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.button2);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "WFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "高级查询";
            this.Load += new System.EventHandler(this.WFilter_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WFilter_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox lb_Items;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox tb2;
        private System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.ComboBox cb_opa;
        private System.Windows.Forms.Label lb_field;
        private System.Windows.Forms.Label lb_ts;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除选中行ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lb_sql;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.ListBox lb_where;
        public System.Windows.Forms.DataGridView dataGridView;
    }
}