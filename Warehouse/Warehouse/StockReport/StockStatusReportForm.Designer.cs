namespace Warehouse.StockReport
{
    partial class StockStatusReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DayReportForm));
            this.axGRDisplayViewer1 = new global::AxgrproLib.AxGRDisplayViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.DateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axGRDisplayViewer1
            // 
            this.axGRDisplayViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axGRDisplayViewer1.Enabled = true;
            this.axGRDisplayViewer1.Location = new System.Drawing.Point(12, 44);
            this.axGRDisplayViewer1.Name = "axGRDisplayViewer1";
            this.axGRDisplayViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRDisplayViewer1.OcxState")));
            this.axGRDisplayViewer1.Size = new System.Drawing.Size(879, 401);
            this.axGRDisplayViewer1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(558, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "至";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(412, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 11);
            this.label1.TabIndex = 20;
            this.label1.Text = "从";
            // 
            // DateTimePickerTo
            // 
            this.DateTimePickerTo.CustomFormat = "yyyy-MM-dd";
            this.DateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePickerTo.Location = new System.Drawing.Point(580, 17);
            this.DateTimePickerTo.Name = "DateTimePickerTo";
            this.DateTimePickerTo.Size = new System.Drawing.Size(112, 21);
            this.DateTimePickerTo.TabIndex = 19;
            // 
            // DateTimePickerFrom
            // 
            this.DateTimePickerFrom.CustomFormat = "yyyy-MM-dd";
            this.DateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePickerFrom.Location = new System.Drawing.Point(436, 17);
            this.DateTimePickerFrom.Name = "DateTimePickerFrom";
            this.DateTimePickerFrom.Size = new System.Drawing.Size(112, 21);
            this.DateTimePickerFrom.TabIndex = 18;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(108, 15);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Location = new System.Drawing.Point(13, 15);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPrintPreview.TabIndex = 16;
            this.btnPrintPreview.Text = "打印预览";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(718, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // DayReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 457);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DateTimePickerTo);
            this.Controls.Add(this.DateTimePickerFrom);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPrintPreview);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.axGRDisplayViewer1);
            this.Name = "DayReportForm";
            this.Text = "日报表";
            this.Load += new System.EventHandler(this.DayReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private global::AxgrproLib.AxGRDisplayViewer axGRDisplayViewer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DateTimePickerTo;
        private System.Windows.Forms.DateTimePicker DateTimePickerFrom;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnRefresh;
    }
}