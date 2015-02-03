namespace Warehouse.StockReport
{
    partial class FormStockPandian
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStockPandian));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxZD = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBoxTJ = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBoxFrom = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxTo = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.axGRDisplayViewer1 = new  global::AxgrproLib.AxGRDisplayViewer ();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 478);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1004, 15);
            this.progressBar1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripComboBoxZD,
            this.toolStripComboBoxTJ,
            this.toolStripTextBoxFrom,
            this.toolStripLabel1,
            this.toolStripTextBoxTo,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1004, 31);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::Warehouse.Properties.Resources.print_1;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(65, 28);
            this.toolStripButton1.Text = "打印";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::Warehouse.Properties.Resources.preview;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(86, 28);
            this.toolStripButton2.Text = "打印预览";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripComboBoxZD
            // 
            this.toolStripComboBoxZD.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripComboBoxZD.ForeColor = System.Drawing.SystemColors.InfoText;
            this.toolStripComboBoxZD.Items.AddRange(new object[] {
            "期初数量",
            "收入数量",
            "发出数量",
            "结存数量",
            "结存成本"});
            this.toolStripComboBoxZD.Name = "toolStripComboBoxZD";
            this.toolStripComboBoxZD.Size = new System.Drawing.Size(121, 31);
            this.toolStripComboBoxZD.Text = "--请选择条件--";
            // 
            // toolStripComboBoxTJ
            // 
            this.toolStripComboBoxTJ.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripComboBoxTJ.ForeColor = System.Drawing.SystemColors.InfoText;
            this.toolStripComboBoxTJ.Items.AddRange(new object[] {
            ">",
            "<",
            ">=",
            "<=",
            "!=",
            "=",
            "范围"});
            this.toolStripComboBoxTJ.Name = "toolStripComboBoxTJ";
            this.toolStripComboBoxTJ.Size = new System.Drawing.Size(75, 31);
            this.toolStripComboBoxTJ.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxTJ_SelectedIndexChanged);
            // 
            // toolStripTextBoxFrom
            // 
            this.toolStripTextBoxFrom.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripTextBoxFrom.ForeColor = System.Drawing.SystemColors.InfoText;
            this.toolStripTextBoxFrom.Name = "toolStripTextBoxFrom";
            this.toolStripTextBoxFrom.Size = new System.Drawing.Size(100, 31);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(20, 28);
            this.toolStripLabel1.Text = "至";
            this.toolStripLabel1.Visible = false;
            // 
            // toolStripTextBoxTo
            // 
            this.toolStripTextBoxTo.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripTextBoxTo.ForeColor = System.Drawing.SystemColors.InfoText;
            this.toolStripTextBoxTo.Name = "toolStripTextBoxTo";
            this.toolStripTextBoxTo.Size = new System.Drawing.Size(100, 31);
            this.toolStripTextBoxTo.Visible = false;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 28);
            this.toolStripButton3.Text = "刷新";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // axGRDisplayViewer1
            // 
            this.axGRDisplayViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axGRDisplayViewer1.Enabled = true;
            this.axGRDisplayViewer1.Location = new System.Drawing.Point(8, 36);
            this.axGRDisplayViewer1.Name = "axGRDisplayViewer1";
            this.axGRDisplayViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRDisplayViewer1.OcxState")));
            this.axGRDisplayViewer1.Size = new System.Drawing.Size(984, 436);
            this.axGRDisplayViewer1.TabIndex = 5;
            // 
            // FormStockPandian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 493);
            this.Controls.Add(this.axGRDisplayViewer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.progressBar1);
            this.Name = "FormStockPandian";
            this.Text = "FormStockPandian";
            this.Load += new System.EventHandler(this.FormStockPandian_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxZD;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxTJ;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxFrom;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxTo;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private global::AxgrproLib.AxGRDisplayViewer axGRDisplayViewer1;
    }
}