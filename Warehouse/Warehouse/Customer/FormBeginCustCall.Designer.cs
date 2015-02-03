namespace Warehouse.Customer
{
    partial class FormBeginCustCall
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
            this.n_endflag = new System.Windows.Forms.TextBox();
            this.s_AcceptMemo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.s_PlanedTime = new System.Windows.Forms.DateTimePicker();
            this.s_PlanedDay = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.u = new System.Windows.Forms.Label();
            this.s_WorkType = new System.Windows.Forms.TextBox();
            this.t_WorkType = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.s_UrgentDegree = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.s_CustCode = new System.Windows.Forms.TextBox();
            this.TbargainType = new System.Windows.Forms.TextBox();
            this.s_Technician1 = new System.Windows.Forms.TextBox();
            this.s_ManufactCode = new System.Windows.Forms.TextBox();
            this.s_mtype = new System.Windows.Forms.TextBox();
            this.s_Tel = new System.Windows.Forms.TextBox();
            this.s_Contact = new System.Windows.Forms.TextBox();
            this.t_Mdepart = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "确定派工";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(230, 221);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.n_endflag);
            this.panel1.Controls.Add(this.s_AcceptMemo);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.s_PlanedTime);
            this.panel1.Controls.Add(this.s_PlanedDay);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.u);
            this.panel1.Controls.Add(this.s_WorkType);
            this.panel1.Controls.Add(this.t_WorkType);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.s_UrgentDegree);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.s_CustCode);
            this.panel1.Controls.Add(this.TbargainType);
            this.panel1.Controls.Add(this.s_Technician1);
            this.panel1.Controls.Add(this.s_ManufactCode);
            this.panel1.Controls.Add(this.s_mtype);
            this.panel1.Controls.Add(this.s_Tel);
            this.panel1.Controls.Add(this.s_Contact);
            this.panel1.Controls.Add(this.t_Mdepart);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 215);
            this.panel1.TabIndex = 18;
            // 
            // n_endflag
            // 
            this.n_endflag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.n_endflag.Location = new System.Drawing.Point(12, 193);
            this.n_endflag.Name = "n_endflag";
            this.n_endflag.Size = new System.Drawing.Size(31, 21);
            this.n_endflag.TabIndex = 104;
            this.n_endflag.Visible = false;
            // 
            // s_AcceptMemo
            // 
            this.s_AcceptMemo.Location = new System.Drawing.Point(89, 175);
            this.s_AcceptMemo.Multiline = true;
            this.s_AcceptMemo.Name = "s_AcceptMemo";
            this.s_AcceptMemo.Size = new System.Drawing.Size(311, 33);
            this.s_AcceptMemo.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 101;
            this.label7.Text = "受理备注";
            // 
            // s_PlanedTime
            // 
            this.s_PlanedTime.CustomFormat = "HH:mm";
            this.s_PlanedTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.s_PlanedTime.Location = new System.Drawing.Point(300, 148);
            this.s_PlanedTime.Name = "s_PlanedTime";
            this.s_PlanedTime.ShowUpDown = true;
            this.s_PlanedTime.Size = new System.Drawing.Size(100, 21);
            this.s_PlanedTime.TabIndex = 11;
            // 
            // s_PlanedDay
            // 
            this.s_PlanedDay.CustomFormat = "yyyy-MM-dd";
            this.s_PlanedDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.s_PlanedDay.Location = new System.Drawing.Point(89, 148);
            this.s_PlanedDay.Name = "s_PlanedDay";
            this.s_PlanedDay.ShowUpDown = true;
            this.s_PlanedDay.Size = new System.Drawing.Size(100, 21);
            this.s_PlanedDay.TabIndex = 10;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(247, 152);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 98;
            this.label19.Text = "预定时间";
            // 
            // u
            // 
            this.u.AutoSize = true;
            this.u.Location = new System.Drawing.Point(44, 152);
            this.u.Name = "u";
            this.u.Size = new System.Drawing.Size(41, 12);
            this.u.TabIndex = 97;
            this.u.Text = "预定日";
            // 
            // s_WorkType
            // 
            this.s_WorkType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.s_WorkType.Location = new System.Drawing.Point(325, 93);
            this.s_WorkType.Name = "s_WorkType";
            this.s_WorkType.Size = new System.Drawing.Size(31, 21);
            this.s_WorkType.TabIndex = 96;
            this.s_WorkType.Visible = false;
            // 
            // t_WorkType
            // 
            this.t_WorkType.Location = new System.Drawing.Point(300, 93);
            this.t_WorkType.Name = "t_WorkType";
            this.t_WorkType.ReadOnly = true;
            this.t_WorkType.Size = new System.Drawing.Size(100, 21);
            this.t_WorkType.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(244, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 35;
            this.label10.Text = "工作类型";
            // 
            // s_UrgentDegree
            // 
            this.s_UrgentDegree.Location = new System.Drawing.Point(89, 93);
            this.s_UrgentDegree.Name = "s_UrgentDegree";
            this.s_UrgentDegree.ReadOnly = true;
            this.s_UrgentDegree.Size = new System.Drawing.Size(100, 21);
            this.s_UrgentDegree.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 33;
            this.label9.Text = "紧急程度";
            // 
            // s_CustCode
            // 
            this.s_CustCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.s_CustCode.Location = new System.Drawing.Point(300, 6);
            this.s_CustCode.Name = "s_CustCode";
            this.s_CustCode.Size = new System.Drawing.Size(30, 21);
            this.s_CustCode.TabIndex = 19;
            this.s_CustCode.Visible = false;
            // 
            // TbargainType
            // 
            this.TbargainType.Location = new System.Drawing.Point(300, 121);
            this.TbargainType.Name = "TbargainType";
            this.TbargainType.ReadOnly = true;
            this.TbargainType.Size = new System.Drawing.Size(100, 21);
            this.TbargainType.TabIndex = 9;
            // 
            // s_Technician1
            // 
            this.s_Technician1.Location = new System.Drawing.Point(89, 120);
            this.s_Technician1.Name = "s_Technician1";
            this.s_Technician1.ReadOnly = true;
            this.s_Technician1.Size = new System.Drawing.Size(100, 21);
            this.s_Technician1.TabIndex = 30;
            // 
            // s_ManufactCode
            // 
            this.s_ManufactCode.Location = new System.Drawing.Point(300, 39);
            this.s_ManufactCode.Name = "s_ManufactCode";
            this.s_ManufactCode.ReadOnly = true;
            this.s_ManufactCode.Size = new System.Drawing.Size(100, 21);
            this.s_ManufactCode.TabIndex = 3;
            // 
            // s_mtype
            // 
            this.s_mtype.Location = new System.Drawing.Point(89, 39);
            this.s_mtype.Name = "s_mtype";
            this.s_mtype.ReadOnly = true;
            this.s_mtype.Size = new System.Drawing.Size(100, 21);
            this.s_mtype.TabIndex = 2;
            // 
            // s_Tel
            // 
            this.s_Tel.Location = new System.Drawing.Point(300, 66);
            this.s_Tel.Name = "s_Tel";
            this.s_Tel.ReadOnly = true;
            this.s_Tel.Size = new System.Drawing.Size(100, 21);
            this.s_Tel.TabIndex = 5;
            // 
            // s_Contact
            // 
            this.s_Contact.Location = new System.Drawing.Point(89, 66);
            this.s_Contact.Name = "s_Contact";
            this.s_Contact.ReadOnly = true;
            this.s_Contact.Size = new System.Drawing.Size(100, 21);
            this.s_Contact.TabIndex = 4;
            // 
            // t_Mdepart
            // 
            this.t_Mdepart.Location = new System.Drawing.Point(89, 12);
            this.t_Mdepart.Name = "t_Mdepart";
            this.t_Mdepart.ReadOnly = true;
            this.t_Mdepart.Size = new System.Drawing.Size(311, 21);
            this.t_Mdepart.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "派工技术员";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(242, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "联系电话";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "联系人";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "合同类别";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(242, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "制造编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "机型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "客户名";
            // 
            // FormBeginCustCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 252);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormBeginCustCall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "派工确认";
            this.Load += new System.EventHandler(this.FormBeginCustCall_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormBeginCustCall_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TbargainType;
        private System.Windows.Forms.TextBox s_Technician1;
        private System.Windows.Forms.TextBox s_ManufactCode;
        private System.Windows.Forms.TextBox s_mtype;
        private System.Windows.Forms.TextBox s_Tel;
        private System.Windows.Forms.TextBox s_Contact;
        private System.Windows.Forms.TextBox t_Mdepart;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox s_CustCode;
        private System.Windows.Forms.TextBox s_UrgentDegree;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox t_WorkType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox s_WorkType;
        private System.Windows.Forms.TextBox s_AcceptMemo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker s_PlanedTime;
        private System.Windows.Forms.DateTimePicker s_PlanedDay;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label u;
        private System.Windows.Forms.TextBox n_endflag;

    }
}