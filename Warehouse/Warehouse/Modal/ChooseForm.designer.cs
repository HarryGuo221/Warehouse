namespace Warehouse.Modal
{
    partial class ChooseForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.b2 = new System.Windows.Forms.CheckBox();
            this.b1 = new System.Windows.Forms.CheckBox();
            this.groupBoxB = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBoxB.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择需要隐藏的数据：";
            // 
            // b2
            // 
            this.b2.AutoSize = true;
            this.b2.Location = new System.Drawing.Point(79, 20);
            this.b2.Name = "b2";
            this.b2.Size = new System.Drawing.Size(48, 16);
            this.b2.TabIndex = 10;
            this.b2.Text = "金额";
            this.b2.UseVisualStyleBackColor = true;
            // 
            // b1
            // 
            this.b1.AutoSize = true;
            this.b1.Location = new System.Drawing.Point(25, 20);
            this.b1.Name = "b1";
            this.b1.Size = new System.Drawing.Size(48, 16);
            this.b1.TabIndex = 9;
            this.b1.Text = "单价";
            this.b1.UseVisualStyleBackColor = true;
            // 
            // groupBoxB
            // 
            this.groupBoxB.Controls.Add(this.b1);
            this.groupBoxB.Controls.Add(this.b2);
            this.groupBoxB.Location = new System.Drawing.Point(15, 38);
            this.groupBoxB.Name = "groupBoxB";
            this.groupBoxB.Size = new System.Drawing.Size(148, 51);
            this.groupBoxB.TabIndex = 13;
            this.groupBoxB.TabStop = false;
            this.groupBoxB.Text = "显示商品明细";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(203, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(203, 78);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ChooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 105);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxB);
            this.Controls.Add(this.label1);
            this.Name = "ChooseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条件筛选";
            this.groupBoxB.ResumeLayout(false);
            this.groupBoxB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox b2;
        private System.Windows.Forms.CheckBox b1;
        private System.Windows.Forms.GroupBox groupBoxB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}