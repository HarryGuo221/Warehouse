namespace Warehouse
{
    partial class BargainBindVerify
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
            this.关闭 = new System.Windows.Forms.Button();
            this.dgv_bargs = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bargs)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.关闭);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 37);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(575, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用以捆绑的合同，下表中罗列属性(除首次抄张日外)必须完全相同，首次抄张日应按抄张周期吻合,请校对！";
            // 
            // 关闭
            // 
            this.关闭.Location = new System.Drawing.Point(681, 7);
            this.关闭.Name = "关闭";
            this.关闭.Size = new System.Drawing.Size(62, 23);
            this.关闭.TabIndex = 0;
            this.关闭.Text = "关闭";
            this.关闭.UseVisualStyleBackColor = true;
            this.关闭.Click += new System.EventHandler(this.关闭_Click);
            // 
            // dgv_bargs
            // 
            this.dgv_bargs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_bargs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_bargs.Location = new System.Drawing.Point(0, 37);
            this.dgv_bargs.Name = "dgv_bargs";
            this.dgv_bargs.ReadOnly = true;
            this.dgv_bargs.RowTemplate.Height = 23;
            this.dgv_bargs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_bargs.Size = new System.Drawing.Size(755, 309);
            this.dgv_bargs.TabIndex = 1;
            // 
            // BargainBindVerify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 346);
            this.Controls.Add(this.dgv_bargs);
            this.Controls.Add(this.panel1);
            this.Name = "BargainBindVerify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "合同捆绑校验";
            this.Load += new System.EventHandler(this.BargainBindVerify_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bargs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_bargs;
        private System.Windows.Forms.Button 关闭;
        private System.Windows.Forms.Label label1;
    }
}