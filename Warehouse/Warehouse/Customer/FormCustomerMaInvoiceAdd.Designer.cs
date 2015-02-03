namespace Warehouse.Customer
{
    partial class FormCustomerMaInvoiceAdd
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
            this.panelkp = new System.Windows.Forms.Panel();
            this.s_matid = new System.Windows.Forms.ComboBox();
            this.t_kpcorp = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.s_CmSysId = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.t_IType = new System.Windows.Forms.ComboBox();
            this.s_Memo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.s_Ititle = new System.Windows.Forms.TextBox();
            this.Tel1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.s_kpcorp = new System.Windows.Forms.TextBox();
            this.s_IType = new System.Windows.Forms.TextBox();
            this.panelkp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelkp
            // 
            this.panelkp.Controls.Add(this.s_IType);
            this.panelkp.Controls.Add(this.s_kpcorp);
            this.panelkp.Controls.Add(this.s_matid);
            this.panelkp.Controls.Add(this.t_kpcorp);
            this.panelkp.Controls.Add(this.label2);
            this.panelkp.Controls.Add(this.s_CmSysId);
            this.panelkp.Controls.Add(this.label33);
            this.panelkp.Controls.Add(this.t_IType);
            this.panelkp.Controls.Add(this.s_Memo);
            this.panelkp.Controls.Add(this.label1);
            this.panelkp.Controls.Add(this.label11);
            this.panelkp.Controls.Add(this.s_Ititle);
            this.panelkp.Controls.Add(this.Tel1);
            this.panelkp.Controls.Add(this.label6);
            this.panelkp.Location = new System.Drawing.Point(-2, -1);
            this.panelkp.Name = "panelkp";
            this.panelkp.Size = new System.Drawing.Size(390, 152);
            this.panelkp.TabIndex = 3;
            // 
            // s_matid
            // 
            this.s_matid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.s_matid.FormattingEnabled = true;
            this.s_matid.Location = new System.Drawing.Point(76, 68);
            this.s_matid.Name = "s_matid";
            this.s_matid.Size = new System.Drawing.Size(302, 20);
            this.s_matid.TabIndex = 137;
            // 
            // t_kpcorp
            // 
            this.t_kpcorp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.t_kpcorp.FormattingEnabled = true;
            this.t_kpcorp.Location = new System.Drawing.Point(77, 13);
            this.t_kpcorp.Name = "t_kpcorp";
            this.t_kpcorp.Size = new System.Drawing.Size(94, 20);
            this.t_kpcorp.TabIndex = 136;
            this.t_kpcorp.SelectedIndexChanged += new System.EventHandler(this.t_kpcorp_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 135;
            this.label2.Text = "所属公司";
            // 
            // s_CmSysId
            // 
            this.s_CmSysId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.s_CmSysId.Location = new System.Drawing.Point(316, 40);
            this.s_CmSysId.Name = "s_CmSysId";
            this.s_CmSysId.Size = new System.Drawing.Size(41, 21);
            this.s_CmSysId.TabIndex = 134;
            this.s_CmSysId.Visible = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(381, 43);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(11, 12);
            this.label33.TabIndex = 133;
            this.label33.Text = "*";
            // 
            // t_IType
            // 
            this.t_IType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.t_IType.FormattingEnabled = true;
            this.t_IType.Location = new System.Drawing.Point(256, 13);
            this.t_IType.Name = "t_IType";
            this.t_IType.Size = new System.Drawing.Size(122, 20);
            this.t_IType.TabIndex = 1;
            this.t_IType.SelectedIndexChanged += new System.EventHandler(this.t_IType_SelectedIndexChanged);
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
            this.label11.Location = new System.Drawing.Point(197, 16);
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
            this.button1.Location = new System.Drawing.Point(394, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(394, 120);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // s_kpcorp
            // 
            this.s_kpcorp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.s_kpcorp.Location = new System.Drawing.Point(94, 12);
            this.s_kpcorp.Name = "s_kpcorp";
            this.s_kpcorp.Size = new System.Drawing.Size(34, 21);
            this.s_kpcorp.TabIndex = 138;
            this.s_kpcorp.Visible = false;
            // 
            // s_IType
            // 
            this.s_IType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.s_IType.Location = new System.Drawing.Point(271, 12);
            this.s_IType.Name = "s_IType";
            this.s_IType.Size = new System.Drawing.Size(46, 21);
            this.s_IType.TabIndex = 139;
            this.s_IType.Visible = false;
            // 
            // FormCustomerMaInvoiceAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 152);
            this.Controls.Add(this.panelkp);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.KeyPreview = true;
            this.Name = "FormCustomerMaInvoiceAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开票信息";
            this.Load += new System.EventHandler(this.FormCustomerMaInvoiceAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormCustomerMaInvoiceAdd_KeyPress);
            this.panelkp.ResumeLayout(false);
            this.panelkp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelkp;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox t_IType;
        private System.Windows.Forms.TextBox s_Memo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox s_Ititle;
        private System.Windows.Forms.Label Tel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox s_CmSysId;
        private System.Windows.Forms.ComboBox t_kpcorp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox s_matid;
        public System.Windows.Forms.TextBox s_kpcorp;
        public System.Windows.Forms.TextBox s_IType;
    }
}