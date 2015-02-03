namespace Warehouse.Stock
{
    partial class Material_info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Material_info));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.物料期限ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.机型耗材对照ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.添加物料 = new System.Windows.Forms.ToolStripButton();
            this.修改 = new System.Windows.Forms.ToolStripButton();
            this.删除 = new System.Windows.Forms.ToolStripButton();
            this.技术资料原件 = new System.Windows.Forms.ToolStripButton();
            this.物料寿命 = new System.Windows.Forms.ToolStripButton();
            this.机型耗材 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.物料期限ToolStripMenuItem,
            this.机型耗材对照ToolStripMenuItem,
            this.删除DToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.toolStripMenuItem1.Text = "技术资料原件";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 物料期限ToolStripMenuItem
            // 
            this.物料期限ToolStripMenuItem.Name = "物料期限ToolStripMenuItem";
            this.物料期限ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.物料期限ToolStripMenuItem.Text = "物料期限";
            this.物料期限ToolStripMenuItem.Click += new System.EventHandler(this.物料期限ToolStripMenuItem_Click);
            // 
            // 机型耗材对照ToolStripMenuItem
            // 
            this.机型耗材对照ToolStripMenuItem.Name = "机型耗材对照ToolStripMenuItem";
            this.机型耗材对照ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.机型耗材对照ToolStripMenuItem.Text = "机型耗材对照";
            this.机型耗材对照ToolStripMenuItem.Click += new System.EventHandler(this.机型耗材对照ToolStripMenuItem_Click);
            // 
            // 删除DToolStripMenuItem
            // 
            this.删除DToolStripMenuItem.Name = "删除DToolStripMenuItem";
            this.删除DToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.删除DToolStripMenuItem.Text = "删除(&D)";
            this.删除DToolStripMenuItem.Click += new System.EventHandler(this.删除DToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加物料,
            this.修改,
            this.删除,
            this.技术资料原件,
            this.物料寿命,
            this.机型耗材});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(948, 51);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // 添加物料
            // 
            this.添加物料.Image = global::Warehouse.Properties.Resources.add1_conew1;
            this.添加物料.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.添加物料.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.添加物料.Name = "添加物料";
            this.添加物料.Size = new System.Drawing.Size(60, 48);
            this.添加物料.Text = "添加物料";
            this.添加物料.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.添加物料.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // 修改
            // 
            this.修改.Image = global::Warehouse.Properties.Resources.alter_conew;
            this.修改.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.修改.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.修改.Name = "修改";
            this.修改.Size = new System.Drawing.Size(60, 48);
            this.修改.Text = "修改物料";
            this.修改.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.修改.Click += new System.EventHandler(this.修改_Click);
            // 
            // 删除
            // 
            this.删除.Image = ((System.Drawing.Image)(resources.GetObject("删除.Image")));
            this.删除.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.删除.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.删除.Name = "删除";
            this.删除.Size = new System.Drawing.Size(60, 48);
            this.删除.Text = "删除物料";
            this.删除.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.删除.Click += new System.EventHandler(this.删除_Click);
            // 
            // 技术资料原件
            // 
            this.技术资料原件.Image = global::Warehouse.Properties.Resources.mediafile_conew11;
            this.技术资料原件.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.技术资料原件.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.技术资料原件.Name = "技术资料原件";
            this.技术资料原件.Size = new System.Drawing.Size(60, 43);
            this.技术资料原件.Text = "技术资料";
            this.技术资料原件.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.技术资料原件.Visible = false;
            this.技术资料原件.Click += new System.EventHandler(this.技术资料原件_Click);
            // 
            // 物料寿命
            // 
            this.物料寿命.Image = global::Warehouse.Properties.Resources.life_conew11;
            this.物料寿命.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.物料寿命.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.物料寿命.Name = "物料寿命";
            this.物料寿命.Size = new System.Drawing.Size(92, 48);
            this.物料寿命.Text = "物料期限(寿命)";
            this.物料寿命.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.物料寿命.Visible = false;
            this.物料寿命.Click += new System.EventHandler(this.物料寿命_Click);
            // 
            // 机型耗材
            // 
            this.机型耗材.Image = global::Warehouse.Properties.Resources.zujian_conew1;
            this.机型耗材.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.机型耗材.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.机型耗材.Name = "机型耗材";
            this.机型耗材.Size = new System.Drawing.Size(84, 48);
            this.机型耗材.Text = "机型耗材对照";
            this.机型耗材.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.机型耗材.Visible = false;
            this.机型耗材.Click += new System.EventHandler(this.机型耗材_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(948, 32);
            this.panel1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(317, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "全部物料";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Image = global::Warehouse.Properties.Resources.egs;
            this.button1.Location = new System.Drawing.Point(279, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(226, 21);
            this.textBox1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox1, "按\"物料编码\",\"物料名称\"和\"拼音码\"进行筛选");
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "筛选";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 538);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(948, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 83);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(948, 455);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Material_info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 560);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Material_info";
            this.Text = "物料信息";
            this.Load += new System.EventHandler(this.Material_info_Load_1);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 物料期限ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 机型耗材对照ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除DToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton 添加物料;
        private System.Windows.Forms.ToolStripButton 修改;
        private System.Windows.Forms.ToolStripButton 删除;
        private System.Windows.Forms.ToolStripButton 技术资料原件;
        private System.Windows.Forms.ToolStripButton 物料寿命;
        private System.Windows.Forms.ToolStripButton 机型耗材;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}