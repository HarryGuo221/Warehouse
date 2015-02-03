namespace Warehouse.Stock
{
    partial class FormErrAddSol
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
            this.label2 = new System.Windows.Forms.Label();
            this.t_ErrorCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.S_reason = new System.Windows.Forms.TextBox();
            this.s_solution = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.s_Gperson = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.n_Esysid = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(168, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(285, 224);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "故障编码";
            // 
            // t_ErrorCode
            // 
            this.t_ErrorCode.Location = new System.Drawing.Point(80, 17);
            this.t_ErrorCode.Name = "t_ErrorCode";
            this.t_ErrorCode.ReadOnly = true;
            this.t_ErrorCode.Size = new System.Drawing.Size(139, 21);
            this.t_ErrorCode.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "可能原因";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "解决方法";
            // 
            // S_reason
            // 
            this.S_reason.Location = new System.Drawing.Point(80, 44);
            this.S_reason.MaxLength = 100;
            this.S_reason.Multiline = true;
            this.S_reason.Name = "S_reason";
            this.S_reason.Size = new System.Drawing.Size(442, 71);
            this.S_reason.TabIndex = 4;
            // 
            // s_solution
            // 
            this.s_solution.Location = new System.Drawing.Point(80, 121);
            this.s_solution.MaxLength = 100;
            this.s_solution.Multiline = true;
            this.s_solution.Name = "s_solution";
            this.s_solution.Size = new System.Drawing.Size(442, 91);
            this.s_solution.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.s_Gperson);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.n_Esysid);
            this.panel1.Controls.Add(this.s_solution);
            this.panel1.Controls.Add(this.S_reason);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.t_ErrorCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 218);
            this.panel1.TabIndex = 10;
            // 
            // s_Gperson
            // 
            this.s_Gperson.Location = new System.Drawing.Point(380, 17);
            this.s_Gperson.Name = "s_Gperson";
            this.s_Gperson.Size = new System.Drawing.Size(106, 21);
            this.s_Gperson.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(335, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "提供者";
            // 
            // n_Esysid
            // 
            this.n_Esysid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.n_Esysid.Location = new System.Drawing.Point(168, 17);
            this.n_Esysid.Name = "n_Esysid";
            this.n_Esysid.Size = new System.Drawing.Size(29, 21);
            this.n_Esysid.TabIndex = 19;
            this.n_Esysid.Visible = false;
            // 
            // FormErrAddSol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 254);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormErrAddSol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "故障原因及解决方案";
            this.Load += new System.EventHandler(this.FormErrAddSol_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormErrAddSol_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox t_ErrorCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox S_reason;
        private System.Windows.Forms.TextBox s_solution;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox n_Esysid;
        private System.Windows.Forms.TextBox s_Gperson;
        private System.Windows.Forms.Label label1;
    }
}