namespace Warehouse.Stock
{
    partial class ModelMatsPrice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelMatsPrice));
            this.dgv_consume = new System.Windows.Forms.DataGridView();
            this.HC = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_part = new System.Windows.Forms.DataGridView();
            this.XGJ = new System.Windows.Forms.TabPage();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ChildSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_model = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Delete = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_consume)).BeginInit();
            this.HC.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_part)).BeginInit();
            this.XGJ.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_model)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_consume
            // 
            this.dgv_consume.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_consume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_consume.Location = new System.Drawing.Point(3, 3);
            this.dgv_consume.MultiSelect = false;
            this.dgv_consume.Name = "dgv_consume";
            this.dgv_consume.ReadOnly = true;
            this.dgv_consume.RowTemplate.Height = 23;
            this.dgv_consume.Size = new System.Drawing.Size(588, 359);
            this.dgv_consume.TabIndex = 2;
            this.dgv_consume.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_consume_DataError);
            this.dgv_consume.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_consume_EditingControlShowing);
            // 
            // HC
            // 
            this.HC.Controls.Add(this.dgv_consume);
            this.HC.Location = new System.Drawing.Point(4, 21);
            this.HC.Name = "HC";
            this.HC.Padding = new System.Windows.Forms.Padding(3);
            this.HC.Size = new System.Drawing.Size(594, 365);
            this.HC.TabIndex = 0;
            this.HC.Text = "耗材";
            this.HC.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 30);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // dgv_part
            // 
            this.dgv_part.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_part.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_part.Location = new System.Drawing.Point(3, 3);
            this.dgv_part.MultiSelect = false;
            this.dgv_part.Name = "dgv_part";
            this.dgv_part.ReadOnly = true;
            this.dgv_part.RowTemplate.Height = 23;
            this.dgv_part.Size = new System.Drawing.Size(588, 350);
            this.dgv_part.TabIndex = 2;
            this.dgv_part.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_part_DataError);
            this.dgv_part.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_part_EditingControlShowing);
            // 
            // XGJ
            // 
            this.XGJ.Controls.Add(this.dgv_part);
            this.XGJ.Location = new System.Drawing.Point(4, 21);
            this.XGJ.Name = "XGJ";
            this.XGJ.Padding = new System.Windows.Forms.Padding(3);
            this.XGJ.Size = new System.Drawing.Size(594, 356);
            this.XGJ.TabIndex = 1;
            this.XGJ.Text = "选购件";
            this.XGJ.UseVisualStyleBackColor = true;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(33, 32);
            this.toolStripButton1.Text = "设置";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ChildSave
            // 
            this.ChildSave.Image = ((System.Drawing.Image)(resources.GetObject("ChildSave.Image")));
            this.ChildSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChildSave.Name = "ChildSave";
            this.ChildSave.Size = new System.Drawing.Size(33, 32);
            this.ChildSave.Text = "保存";
            this.ChildSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ChildSave.Click += new System.EventHandler(this.ChildSave_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(33, 32);
            this.toolStripButton3.Text = "关闭";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(57, 32);
            this.toolStripButton5.Text = "批量设置";
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(33, 32);
            this.toolStripButton2.Text = "移除";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.HC);
            this.tabControl1.Controls.Add(this.XGJ);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(602, 390);
            this.tabControl1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(339, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(602, 390);
            this.panel2.TabIndex = 9;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(336, 35);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 390);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(336, 425);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(605, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_model);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 412);
            this.panel1.TabIndex = 7;
            // 
            // dgv_model
            // 
            this.dgv_model.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_model.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_model.Location = new System.Drawing.Point(0, 29);
            this.dgv_model.MultiSelect = false;
            this.dgv_model.Name = "dgv_model";
            this.dgv_model.ReadOnly = true;
            this.dgv_model.RowTemplate.Height = 23;
            this.dgv_model.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_model.Size = new System.Drawing.Size(336, 383);
            this.dgv_model.TabIndex = 1;
            this.dgv_model.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_model_CellClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(336, 29);
            this.panel3.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(237, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "全部机型";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "筛选条件";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(70, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(118, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton5,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.ChildSave,
            this.Delete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(941, 35);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Delete
            // 
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(33, 32);
            this.Delete.Text = "删除";
            this.Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // ModelMatsPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 447);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ModelMatsPrice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机型、耗材及选购件报价资料";
            this.Load += new System.EventHandler(this.ModelMatsPrice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_consume)).EndInit();
            this.HC.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_part)).EndInit();
            this.XGJ.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_model)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_consume;
        private System.Windows.Forms.TabPage HC;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgv_part;
        private System.Windows.Forms.TabPage XGJ;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton ChildSave;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_model;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Delete;
    }
}