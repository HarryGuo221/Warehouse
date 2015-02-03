namespace Warehouse.Stock
{
    partial class EditMatAssemblyMainForm
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
            this.label8 = new System.Windows.Forms.Label();
            this.s_AssName = new System.Windows.Forms.TextBox();
            this.s_Memo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.s_Description = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.s_AssName);
            this.panel1.Controls.Add(this.s_Memo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.s_Description);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 148);
            this.panel1.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(376, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 50;
            this.label8.Text = "*";
            // 
            // s_AssName
            // 
            this.s_AssName.Location = new System.Drawing.Point(72, 9);
            this.s_AssName.MaxLength = 30;
            this.s_AssName.Name = "s_AssName";
            this.s_AssName.Size = new System.Drawing.Size(298, 21);
            this.s_AssName.TabIndex = 1;
            // 
            // s_Memo
            // 
            this.s_Memo.Location = new System.Drawing.Point(72, 89);
            this.s_Memo.MaxLength = 50;
            this.s_Memo.Multiline = true;
            this.s_Memo.Name = "s_Memo";
            this.s_Memo.Size = new System.Drawing.Size(298, 45);
            this.s_Memo.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "套机名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "套机描述";
            // 
            // s_Description
            // 
            this.s_Description.Location = new System.Drawing.Point(72, 38);
            this.s_Description.MaxLength = 100;
            this.s_Description.Multiline = true;
            this.s_Description.Name = "s_Description";
            this.s_Description.Size = new System.Drawing.Size(298, 45);
            this.s_Description.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "备注";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(115, 157);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 21;
            this.buttonOK.Text = "保存 ";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(206, 157);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // EditMatAssemblyMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 189);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "EditMatAssemblyMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑套机信息";
            this.Load += new System.EventHandler(this.EditMatAssemblyMainForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditMatAssemblyMainForm_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox s_AssName;
        private System.Windows.Forms.TextBox s_Memo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox s_Description;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button button2;

    }
}