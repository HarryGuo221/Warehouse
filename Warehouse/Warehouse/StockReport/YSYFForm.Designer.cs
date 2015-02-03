namespace Warehouse.StockReport
{
	partial class YSYFForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YSYFForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.axGRDisplayViewer1 = new global::AxgrproLib.AxGRDisplayViewer ();
            this.label1 = new System.Windows.Forms.Label();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnprintview = new System.Windows.Forms.Button();
            this.RecordNum = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(-3, 373);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(915, 23);
            this.progressBar1.TabIndex = 26;
            // 
            // axGRDisplayViewer1
            // 
            this.axGRDisplayViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axGRDisplayViewer1.Enabled = true;
            this.axGRDisplayViewer1.Location = new System.Drawing.Point(-3, 40);
            this.axGRDisplayViewer1.Name = "axGRDisplayViewer1";
            this.axGRDisplayViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRDisplayViewer1.OcxState")));
            this.axGRDisplayViewer1.Size = new System.Drawing.Size(915, 331);
            this.axGRDisplayViewer1.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(357, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 16);
            this.label1.TabIndex = 29;
            this.label1.Text = "XX数码科技有限公司";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnprint
            // 
            this.btnprint.Location = new System.Drawing.Point(74, 2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(45, 19);
            this.btnprint.TabIndex = 28;
            this.btnprint.Text = "打印";
            this.btnprint.UseVisualStyleBackColor = true;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnprintview
            // 
            this.btnprintview.Location = new System.Drawing.Point(5, 2);
            this.btnprintview.Name = "btnprintview";
            this.btnprintview.Size = new System.Drawing.Size(65, 19);
            this.btnprintview.TabIndex = 27;
            this.btnprintview.Text = "打印预览";
            this.btnprintview.UseVisualStyleBackColor = true;
            this.btnprintview.Click += new System.EventHandler(this.btnprintview_Click);
            // 
            // RecordNum
            // 
            this.RecordNum.AutoSize = true;
            this.RecordNum.Location = new System.Drawing.Point(186, 6);
            this.RecordNum.Name = "RecordNum";
            this.RecordNum.Size = new System.Drawing.Size(41, 12);
            this.RecordNum.TabIndex = 30;
            this.RecordNum.Text = "label2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 19);
            this.button1.TabIndex = 31;
            this.button1.Text = "刷新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // YSYFForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 395);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.RecordNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnprint);
            this.Controls.Add(this.btnprintview);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.axGRDisplayViewer1);
            this.Name = "YSYFForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YSYFForm";
            this.Load += new System.EventHandler(this.YSYFForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private global::AxgrproLib.AxGRDisplayViewer axGRDisplayViewer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnprintview;
        private System.Windows.Forms.Label RecordNum;
        private System.Windows.Forms.Button button1;
	}
}