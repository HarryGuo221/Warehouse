namespace Warehouse.Sys
{
    partial class FormChgSpecialData
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
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtpwd = new System.Windows.Forms.TextBox();
            this.t_Superopa = new System.Windows.Forms.TextBox();
            this.s_Curopa = new System.Windows.Forms.TextBox();
            this.s_OccurTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.s_ChgItems = new System.Windows.Forms.TextBox();
            this.s_Superopa = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(90, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(173, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(41, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(209, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "该项数据修改需要授权";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.s_Superopa);
            this.panel1.Controls.Add(this.s_ChgItems);
            this.panel1.Controls.Add(this.txtpwd);
            this.panel1.Controls.Add(this.t_Superopa);
            this.panel1.Controls.Add(this.s_Curopa);
            this.panel1.Controls.Add(this.s_OccurTime);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(20, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 115);
            this.panel1.TabIndex = 11;
            // 
            // txtpwd
            // 
            this.txtpwd.Location = new System.Drawing.Point(115, 85);
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.PasswordChar = '*';
            this.txtpwd.Size = new System.Drawing.Size(115, 21);
            this.txtpwd.TabIndex = 15;
            // 
            // t_Superopa
            // 
            this.t_Superopa.Location = new System.Drawing.Point(115, 58);
            this.t_Superopa.Name = "t_Superopa";
            this.t_Superopa.Size = new System.Drawing.Size(115, 21);
            this.t_Superopa.TabIndex = 14;
            // 
            // s_Curopa
            // 
            this.s_Curopa.Location = new System.Drawing.Point(115, 31);
            this.s_Curopa.Name = "s_Curopa";
            this.s_Curopa.ReadOnly = true;
            this.s_Curopa.Size = new System.Drawing.Size(115, 21);
            this.s_Curopa.TabIndex = 13;
            // 
            // s_OccurTime
            // 
            this.s_OccurTime.Location = new System.Drawing.Point(115, 3);
            this.s_OccurTime.Name = "s_OccurTime";
            this.s_OccurTime.ReadOnly = true;
            this.s_OccurTime.Size = new System.Drawing.Size(115, 21);
            this.s_OccurTime.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "授权人密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "授权人";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "当前操作者";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "发生时间";
            // 
            // s_ChgItems
            // 
            this.s_ChgItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.s_ChgItems.Location = new System.Drawing.Point(9, 30);
            this.s_ChgItems.Name = "s_ChgItems";
            this.s_ChgItems.Size = new System.Drawing.Size(29, 21);
            this.s_ChgItems.TabIndex = 16;
            this.s_ChgItems.Visible = false;
            // 
            // s_Superopa
            // 
            this.s_Superopa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.s_Superopa.Location = new System.Drawing.Point(11, 61);
            this.s_Superopa.Name = "s_Superopa";
            this.s_Superopa.Size = new System.Drawing.Size(27, 21);
            this.s_Superopa.TabIndex = 17;
            this.s_Superopa.Visible = false;
            // 
            // FormChgSpecialData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 191);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "FormChgSpecialData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "提示";
            this.Load += new System.EventHandler(this.FormChgSpecialData_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.TextBox t_Superopa;
        private System.Windows.Forms.TextBox s_Curopa;
        private System.Windows.Forms.TextBox s_OccurTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox s_ChgItems;
        private System.Windows.Forms.TextBox s_Superopa;
    }
}