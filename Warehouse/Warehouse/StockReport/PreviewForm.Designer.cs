namespace Warehouse.StockReport
{
    partial class PreviewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewForm));
            this.axGRPrintViewer1 = new  global::AxgrproLib.AxGRPrintViewer ();
            ((System.ComponentModel.ISupportInitialize)(this.axGRPrintViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // axGRPrintViewer1
            // 
            this.axGRPrintViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axGRPrintViewer1.Enabled = true;
            this.axGRPrintViewer1.Location = new System.Drawing.Point(0, 0);
            this.axGRPrintViewer1.Name = "axGRPrintViewer1";
            this.axGRPrintViewer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRPrintViewer1.OcxState")));
            this.axGRPrintViewer1.Size = new System.Drawing.Size(568, 218);
            this.axGRPrintViewer1.TabIndex = 2;
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 218);
            this.Controls.Add(this.axGRPrintViewer1);
            this.Name = "PreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印抄张信息";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreviewForm_FormClosed);
            this.Load += new System.EventHandler(this.PreviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axGRPrintViewer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private global::AxgrproLib.AxGRPrintViewer axGRPrintViewer1;
    }
}