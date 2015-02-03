namespace Warehouse.Bargain
{
    partial class PrePayAdd
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
            this.s_iskp = new System.Windows.Forms.CheckBox();
            this.s_BargOrBd_Id = new System.Windows.Forms.TextBox();
            this.s_OperUser = new System.Windows.Forms.TextBox();
            this.s_memo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.n_PrePay = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.s_OccurDay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.s_lx = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.s_lx);
            this.panel1.Controls.Add(this.s_iskp);
            this.panel1.Controls.Add(this.s_BargOrBd_Id);
            this.panel1.Controls.Add(this.s_OperUser);
            this.panel1.Controls.Add(this.s_memo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.n_PrePay);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.s_OccurDay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 92);
            this.panel1.TabIndex = 0;
            // 
            // s_iskp
            // 
            this.s_iskp.AutoSize = true;
            this.s_iskp.Enabled = false;
            this.s_iskp.Location = new System.Drawing.Point(178, 39);
            this.s_iskp.Name = "s_iskp";
            this.s_iskp.Size = new System.Drawing.Size(72, 16);
            this.s_iskp.TabIndex = 8;
            this.s_iskp.Text = "是否开票";
            this.s_iskp.UseVisualStyleBackColor = true;
            // 
            // s_BargOrBd_Id
            // 
            this.s_BargOrBd_Id.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.s_BargOrBd_Id.Location = new System.Drawing.Point(266, 35);
            this.s_BargOrBd_Id.Name = "s_BargOrBd_Id";
            this.s_BargOrBd_Id.Size = new System.Drawing.Size(57, 21);
            this.s_BargOrBd_Id.TabIndex = 7;
            this.s_BargOrBd_Id.Visible = false;
            // 
            // s_OperUser
            // 
            this.s_OperUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.s_OperUser.Location = new System.Drawing.Point(266, 13);
            this.s_OperUser.Name = "s_OperUser";
            this.s_OperUser.Size = new System.Drawing.Size(57, 21);
            this.s_OperUser.TabIndex = 6;
            this.s_OperUser.Visible = false;
            // 
            // s_memo
            // 
            this.s_memo.Location = new System.Drawing.Point(58, 62);
            this.s_memo.Name = "s_memo";
            this.s_memo.Size = new System.Drawing.Size(253, 21);
            this.s_memo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "备注";
            // 
            // n_PrePay
            // 
            this.n_PrePay.Location = new System.Drawing.Point(59, 36);
            this.n_PrePay.Name = "n_PrePay";
            this.n_PrePay.Size = new System.Drawing.Size(100, 21);
            this.n_PrePay.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "金额";
            // 
            // s_OccurDay
            // 
            this.s_OccurDay.CustomFormat = "yyyy-MM-dd";
            this.s_OccurDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.s_OccurDay.Location = new System.Drawing.Point(58, 9);
            this.s_OccurDay.Name = "s_OccurDay";
            this.s_OccurDay.Size = new System.Drawing.Size(101, 21);
            this.s_OccurDay.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日期";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(191, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // s_lx
            // 
            this.s_lx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.s_lx.Location = new System.Drawing.Point(191, 12);
            this.s_lx.Name = "s_lx";
            this.s_lx.Size = new System.Drawing.Size(40, 21);
            this.s_lx.TabIndex = 10;
            this.s_lx.Text = "预收";
            this.s_lx.Visible = false;
            // 
            // PrePayAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 129);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "PrePayAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "客户预收登记";
            this.Load += new System.EventHandler(this.PrePayAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrePayAdd_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox s_memo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox n_PrePay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker s_OccurDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox s_OperUser;
        private System.Windows.Forms.TextBox s_BargOrBd_Id;
        private System.Windows.Forms.CheckBox s_iskp;
        private System.Windows.Forms.TextBox s_lx;
    }
}