namespace Warehouse.Customer
{
    partial class FormCustInvoiceAdd
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
            this.label33 = new System.Windows.Forms.Label();
            this.s_IType = new System.Windows.Forms.ComboBox();
            this.s_CustID = new System.Windows.Forms.TextBox();
            this.s_Memo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.s_Ititle = new System.Windows.Forms.TextBox();
            this.Tel1 = new System.Windows.Forms.Label();
            this.s_Idet = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label33);
            this.panel1.Controls.Add(this.s_IType);
            this.panel1.Controls.Add(this.s_CustID);
            this.panel1.Controls.Add(this.s_Memo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.s_Ititle);
            this.panel1.Controls.Add(this.Tel1);
            this.panel1.Controls.Add(this.s_Idet);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(2, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 152);
            this.panel1.TabIndex = 0;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(384, 43);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(11, 12);
            this.label33.TabIndex = 133;
            this.label33.Text = "*";
            // 
            // s_IType
            // 
            this.s_IType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.s_IType.FormattingEnabled = true;
            this.s_IType.Location = new System.Drawing.Point(76, 13);
            this.s_IType.Name = "s_IType";
            this.s_IType.Size = new System.Drawing.Size(134, 20);
            this.s_IType.TabIndex = 1;
            // 
            // s_CustID
            // 
            this.s_CustID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.s_CustID.Location = new System.Drawing.Point(3, 112);
            this.s_CustID.Name = "s_CustID";
            this.s_CustID.Size = new System.Drawing.Size(33, 21);
            this.s_CustID.TabIndex = 131;
            this.s_CustID.Visible = false;
            // 
            // s_Memo
            // 
            this.s_Memo.Location = new System.Drawing.Point(76, 94);
            this.s_Memo.Multiline = true;
            this.s_Memo.Name = "s_Memo";
            this.s_Memo.Size = new System.Drawing.Size(302, 50);
            this.s_Memo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 129;
            this.label1.Text = "备注";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 127;
            this.label11.Text = "发票种类";
            // 
            // s_Ititle
            // 
            this.s_Ititle.Location = new System.Drawing.Point(76, 40);
            this.s_Ititle.Name = "s_Ititle";
            this.s_Ititle.Size = new System.Drawing.Size(302, 21);
            this.s_Ititle.TabIndex = 2;
            // 
            // Tel1
            // 
            this.Tel1.AutoSize = true;
            this.Tel1.Location = new System.Drawing.Point(17, 43);
            this.Tel1.Name = "Tel1";
            this.Tel1.Size = new System.Drawing.Size(53, 12);
            this.Tel1.TabIndex = 125;
            this.Tel1.Text = "发票抬头";
            // 
            // s_Idet
            // 
            this.s_Idet.Location = new System.Drawing.Point(76, 67);
            this.s_Idet.Name = "s_Idet";
            this.s_Idet.Size = new System.Drawing.Size(302, 21);
            this.s_Idet.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 123;
            this.label6.Text = "发票内容";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(408, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(408, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormCustInvoiceAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 163);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormCustInvoiceAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开票信息";
            this.Load += new System.EventHandler(this.FormCustInvoiceAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormCustInvoiceAdd_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox s_Memo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox s_Ititle;
        private System.Windows.Forms.Label Tel1;
        private System.Windows.Forms.TextBox s_Idet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox s_CustID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox s_IType;
        private System.Windows.Forms.Label label33;
    }
}