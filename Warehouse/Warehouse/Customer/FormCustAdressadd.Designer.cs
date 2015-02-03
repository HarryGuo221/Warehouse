namespace Warehouse.Customer
{
    partial class FormCustAdressadd
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
            this.s_AddType = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.s_CustID = new System.Windows.Forms.TextBox();
            this.s_Memo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.s_Addr = new System.Windows.Forms.TextBox();
            this.Tel1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // s_AddType
            // 
            this.s_AddType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.s_AddType.FormattingEnabled = true;
            this.s_AddType.Location = new System.Drawing.Point(76, 10);
            this.s_AddType.Name = "s_AddType";
            this.s_AddType.Size = new System.Drawing.Size(121, 20);
            this.s_AddType.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.s_AddType);
            this.panel1.Controls.Add(this.s_CustID);
            this.panel1.Controls.Add(this.s_Memo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.s_Addr);
            this.panel1.Controls.Add(this.Tel1);
            this.panel1.Location = new System.Drawing.Point(4, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 117);
            this.panel1.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(366, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 133;
            this.label10.Text = "*";
            // 
            // s_CustID
            // 
            this.s_CustID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.s_CustID.Location = new System.Drawing.Point(17, 86);
            this.s_CustID.Name = "s_CustID";
            this.s_CustID.Size = new System.Drawing.Size(33, 21);
            this.s_CustID.TabIndex = 131;
            this.s_CustID.Visible = false;
            // 
            // s_Memo
            // 
            this.s_Memo.Location = new System.Drawing.Point(76, 63);
            this.s_Memo.Multiline = true;
            this.s_Memo.Name = "s_Memo";
            this.s_Memo.Size = new System.Drawing.Size(284, 46);
            this.s_Memo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 129;
            this.label1.Text = "备注";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 127;
            this.label11.Text = "地址种类";
            // 
            // s_Addr
            // 
            this.s_Addr.Location = new System.Drawing.Point(76, 36);
            this.s_Addr.Name = "s_Addr";
            this.s_Addr.Size = new System.Drawing.Size(284, 21);
            this.s_Addr.TabIndex = 2;
            // 
            // Tel1
            // 
            this.Tel1.AutoSize = true;
            this.Tel1.Location = new System.Drawing.Point(15, 36);
            this.Tel1.Name = "Tel1";
            this.Tel1.Size = new System.Drawing.Size(53, 12);
            this.Tel1.TabIndex = 125;
            this.Tel1.Text = "客户地址";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(388, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(388, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormCustAdressadd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 131);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormCustAdressadd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户地址管理";
            this.Load += new System.EventHandler(this.FormCustAdressadd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormCustAdressadd_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox s_AddType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox s_CustID;
        private System.Windows.Forms.TextBox s_Memo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox s_Addr;
        private System.Windows.Forms.Label Tel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
    }
}