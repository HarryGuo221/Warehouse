namespace Warehouse.Receipt
{
    partial class VerificateConfigForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVerificateIn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(13, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(903, 356);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F);
            this.label1.Location = new System.Drawing.Point(250, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "核销操作设置";
            // 
            // btnVerificateIn
            // 
            this.btnVerificateIn.Location = new System.Drawing.Point(13, 27);
            this.btnVerificateIn.Name = "btnVerificateIn";
            this.btnVerificateIn.Size = new System.Drawing.Size(75, 23);
            this.btnVerificateIn.TabIndex = 14;
            this.btnVerificateIn.Text = "核销入库";
            this.btnVerificateIn.UseVisualStyleBackColor = true;
            this.btnVerificateIn.Click += new System.EventHandler(this.btnVerificateIn_Click);
            // 
            // VerificateConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 424);
            this.Controls.Add(this.btnVerificateIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "VerificateConfigForm";
            this.Text = "核销操作设置";
            this.Load += new System.EventHandler(this.VerificateConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVerificateIn;
    }
}