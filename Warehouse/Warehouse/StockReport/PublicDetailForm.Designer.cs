namespace Warehouse.StockReport
{
    partial class PublicDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublicDetailForm));
            this.axGRDisplayViewer1 = new global::AxgrproLib.AxGRDisplayViewer();
            this.btnprintview = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Opername = new System.Windows.Forms.Label();
            this.Date = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.RecordNum = new System.Windows.Forms.Label();
            this.Worktime = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer1)).BeginInit();
            this.SuspendLayout();
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
            this.axGRDisplayViewer1.Size = new System.Drawing.Size(737, 277);
            this.axGRDisplayViewer1.TabIndex = 0;
            this.axGRDisplayViewer1.ContentCellDblClick += new global::AxgrproLib._IGRDisplayViewerEvents_ContentCellDblClickEventHandler (this.axGRDisplayViewer1_ContentCellDblClick);
            // 
            // btnprintview
            // 
            this.btnprintview.Location = new System.Drawing.Point(2, 1);
            this.btnprintview.Name = "btnprintview";
            this.btnprintview.Size = new System.Drawing.Size(65, 19);
            this.btnprintview.TabIndex = 1;
            this.btnprintview.Text = "打印预览";
            this.btnprintview.UseVisualStyleBackColor = true;
            this.btnprintview.Click += new System.EventHandler(this.btnprintview_Click);
            // 
            // btnprint
            // 
            this.btnprint.Location = new System.Drawing.Point(72, 1);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(45, 19);
            this.btnprint.TabIndex = 2;
            this.btnprint.Text = "打印";
            this.btnprint.UseVisualStyleBackColor = true;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(257, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "XX数码科技有限公司";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Opername
            // 
            this.Opername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Opername.AutoSize = true;
            this.Opername.Location = new System.Drawing.Point(600, 22);
            this.Opername.Name = "Opername";
            this.Opername.Size = new System.Drawing.Size(41, 12);
            this.Opername.TabIndex = 8;
            this.Opername.Text = "label1";
            // 
            // Date
            // 
            this.Date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Date.AutoSize = true;
            this.Date.Location = new System.Drawing.Point(600, 3);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(41, 12);
            this.Date.TabIndex = 7;
            this.Date.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Location = new System.Drawing.Point(-6, 38);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(750, 1);
            this.textBox1.TabIndex = 10;
            // 
            // RecordNum
            // 
            this.RecordNum.AutoSize = true;
            this.RecordNum.Location = new System.Drawing.Point(123, 3);
            this.RecordNum.Name = "RecordNum";
            this.RecordNum.Size = new System.Drawing.Size(41, 12);
            this.RecordNum.TabIndex = 12;
            this.RecordNum.Text = "label2";
            // 
            // Worktime
            // 
            this.Worktime.AutoSize = true;
            this.Worktime.Location = new System.Drawing.Point(123, 21);
            this.Worktime.Name = "Worktime";
            this.Worktime.Size = new System.Drawing.Size(41, 12);
            this.Worktime.TabIndex = 11;
            this.Worktime.Text = "label2";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(0, 316);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(734, 20);
            this.progressBar1.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 19);
            this.button1.TabIndex = 14;
            this.button1.Text = "刷新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PublicDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 336);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.RecordNum);
            this.Controls.Add(this.Worktime);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Opername);
            this.Controls.Add(this.Date);
            this.Controls.Add(this.btnprint);
            this.Controls.Add(this.btnprintview);
            this.Controls.Add(this.axGRDisplayViewer1);
            this.Name = "PublicDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.JHDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::AxgrproLib.AxGRDisplayViewer axGRDisplayViewer1;
        private System.Windows.Forms.Button btnprintview;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Opername;
        private System.Windows.Forms.Label Date;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label RecordNum;
        private System.Windows.Forms.Label Worktime;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
    }
}