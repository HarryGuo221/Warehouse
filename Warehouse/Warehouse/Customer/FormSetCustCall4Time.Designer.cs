namespace Warehouse.Customer
{
    partial class FormSetCustCall4Time
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.s_departTime = new System.Windows.Forms.DateTimePicker();
            this.s_ArriveTime = new System.Windows.Forms.DateTimePicker();
            this.s_startTime = new System.Windows.Forms.DateTimePicker();
            this.s_orderTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(72, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(165, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.s_departTime);
            this.panel1.Controls.Add(this.s_ArriveTime);
            this.panel1.Controls.Add(this.s_startTime);
            this.panel1.Controls.Add(this.s_orderTime);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 127);
            this.panel1.TabIndex = 10;
            // 
            // s_departTime
            // 
            this.s_departTime.CustomFormat = "yyyy-MM-dd hh:mm";
            this.s_departTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.s_departTime.Location = new System.Drawing.Point(112, 99);
            this.s_departTime.Name = "s_departTime";
            this.s_departTime.ShowUpDown = true;
            this.s_departTime.Size = new System.Drawing.Size(128, 21);
            this.s_departTime.TabIndex = 4;
            // 
            // s_ArriveTime
            // 
            this.s_ArriveTime.CustomFormat = "yyyy-MM-dd hh:mm";
            this.s_ArriveTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.s_ArriveTime.Location = new System.Drawing.Point(112, 73);
            this.s_ArriveTime.Name = "s_ArriveTime";
            this.s_ArriveTime.ShowUpDown = true;
            this.s_ArriveTime.Size = new System.Drawing.Size(128, 21);
            this.s_ArriveTime.TabIndex = 3;
            // 
            // s_startTime
            // 
            this.s_startTime.CustomFormat = "yyyy-MM-dd hh:mm";
            this.s_startTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.s_startTime.Location = new System.Drawing.Point(112, 46);
            this.s_startTime.Name = "s_startTime";
            this.s_startTime.ShowUpDown = true;
            this.s_startTime.Size = new System.Drawing.Size(128, 21);
            this.s_startTime.TabIndex = 2;
            // 
            // s_orderTime
            // 
            this.s_orderTime.CustomFormat = "yyyy-MM-dd hh:mm";
            this.s_orderTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.s_orderTime.Location = new System.Drawing.Point(112, 18);
            this.s_orderTime.Name = "s_orderTime";
            this.s_orderTime.ShowUpDown = true;
            this.s_orderTime.Size = new System.Drawing.Size(128, 21);
            this.s_orderTime.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "召唤时间";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "离开时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "到达时间";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "启动时间";
            // 
            // FormSetCustCall4Time
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 173);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormSetCustCall4Time";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "时间跟踪";
            this.Load += new System.EventHandler(this.FormSetCustCall4Time_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormSetCustCall4Time_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker s_departTime;
        private System.Windows.Forms.DateTimePicker s_ArriveTime;
        private System.Windows.Forms.DateTimePicker s_startTime;
        private System.Windows.Forms.DateTimePicker s_orderTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}