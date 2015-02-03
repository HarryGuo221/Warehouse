namespace Warehouse.Stock
{
    partial class MatSplitMainForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.s_ReceiptId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.s_ManufactCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxMatName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxMatType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnFindMat = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCurUserName = new System.Windows.Forms.TextBox();
            this.s_OpaUser = new System.Windows.Forms.TextBox();
            this.s_MatID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.n_OperateType = new System.Windows.Forms.TextBox();
            this.s_OccurTime = new System.Windows.Forms.DateTimePicker();
            this.n_MatType = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(111, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "拆分机器";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "拆件单据编号：";
            // 
            // s_ReceiptId
            // 
            this.s_ReceiptId.Location = new System.Drawing.Point(115, 44);
            this.s_ReceiptId.Name = "s_ReceiptId";
            this.s_ReceiptId.Size = new System.Drawing.Size(146, 21);
            this.s_ReceiptId.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "被拆物料：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "被拆物料类型：";
            // 
            // s_ManufactCode
            // 
            this.s_ManufactCode.Location = new System.Drawing.Point(115, 129);
            this.s_ManufactCode.Name = "s_ManufactCode";
            this.s_ManufactCode.Size = new System.Drawing.Size(146, 21);
            this.s_ManufactCode.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "制造编号：";
            // 
            // comboBoxMatName
            // 
            this.comboBoxMatName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMatName.FormattingEnabled = true;
            this.comboBoxMatName.Items.AddRange(new object[] {
            "--请选择--",
            "0新机",
            "1旧机",
            "2样机"});
            this.comboBoxMatName.Location = new System.Drawing.Point(115, 71);
            this.comboBoxMatName.Name = "comboBoxMatName";
            this.comboBoxMatName.Size = new System.Drawing.Size(146, 20);
            this.comboBoxMatName.TabIndex = 3;
            this.comboBoxMatName.SelectedIndexChanged += new System.EventHandler(this.comboBoxMatName_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "操作人员：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(100, 223);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(186, 223);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(265, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "*";
            // 
            // comboBoxMatType
            // 
            this.comboBoxMatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMatType.FormattingEnabled = true;
            this.comboBoxMatType.Location = new System.Drawing.Point(115, 100);
            this.comboBoxMatType.MaxLength = 20;
            this.comboBoxMatType.Name = "comboBoxMatType";
            this.comboBoxMatType.Size = new System.Drawing.Size(146, 20);
            this.comboBoxMatType.TabIndex = 5;
            this.comboBoxMatType.SelectedIndexChanged += new System.EventHandler(this.comboBoxMatType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(265, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "*";
            // 
            // btnFindMat
            // 
            this.btnFindMat.Location = new System.Drawing.Point(276, 70);
            this.btnFindMat.Name = "btnFindMat";
            this.btnFindMat.Size = new System.Drawing.Size(47, 23);
            this.btnFindMat.TabIndex = 4;
            this.btnFindMat.Text = "查找";
            this.btnFindMat.UseVisualStyleBackColor = true;
            this.btnFindMat.Click += new System.EventHandler(this.btnFindMat_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(265, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "*";
            // 
            // txtCurUserName
            // 
            this.txtCurUserName.Location = new System.Drawing.Point(115, 159);
            this.txtCurUserName.Name = "txtCurUserName";
            this.txtCurUserName.ReadOnly = true;
            this.txtCurUserName.Size = new System.Drawing.Size(146, 21);
            this.txtCurUserName.TabIndex = 7;
            // 
            // s_OpaUser
            // 
            this.s_OpaUser.BackColor = System.Drawing.Color.GreenYellow;
            this.s_OpaUser.Location = new System.Drawing.Point(32, 159);
            this.s_OpaUser.Name = "s_OpaUser";
            this.s_OpaUser.Size = new System.Drawing.Size(19, 21);
            this.s_OpaUser.TabIndex = 21;
            this.s_OpaUser.Visible = false;
            // 
            // s_MatID
            // 
            this.s_MatID.BackColor = System.Drawing.Color.GreenYellow;
            this.s_MatID.Location = new System.Drawing.Point(32, 68);
            this.s_MatID.Name = "s_MatID";
            this.s_MatID.Size = new System.Drawing.Size(19, 21);
            this.s_MatID.TabIndex = 22;
            this.s_MatID.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "发生时间：";
            this.label10.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.n_OperateType);
            this.panel1.Controls.Add(this.s_OccurTime);
            this.panel1.Controls.Add(this.n_MatType);
            this.panel1.Controls.Add(this.comboBoxMatName);
            this.panel1.Controls.Add(this.s_ReceiptId);
            this.panel1.Controls.Add(this.s_ManufactCode);
            this.panel1.Controls.Add(this.comboBoxMatType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.s_MatID);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.s_OpaUser);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtCurUserName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnFindMat);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 261);
            this.panel1.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(3, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 28;
            this.label11.Text = "操作类型0";
            this.label11.Visible = false;
            // 
            // n_OperateType
            // 
            this.n_OperateType.BackColor = System.Drawing.Color.Red;
            this.n_OperateType.Location = new System.Drawing.Point(63, 23);
            this.n_OperateType.Name = "n_OperateType";
            this.n_OperateType.Size = new System.Drawing.Size(33, 21);
            this.n_OperateType.TabIndex = 27;
            this.n_OperateType.Text = "0";
            this.n_OperateType.Visible = false;
            // 
            // s_OccurTime
            // 
            this.s_OccurTime.CustomFormat = "yyyy-MM-dd";
            this.s_OccurTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.s_OccurTime.Location = new System.Drawing.Point(115, 186);
            this.s_OccurTime.Name = "s_OccurTime";
            this.s_OccurTime.Size = new System.Drawing.Size(146, 21);
            this.s_OccurTime.TabIndex = 8;
            this.s_OccurTime.Visible = false;
            // 
            // n_MatType
            // 
            this.n_MatType.BackColor = System.Drawing.Color.GreenYellow;
            this.n_MatType.Location = new System.Drawing.Point(5, 100);
            this.n_MatType.Name = "n_MatType";
            this.n_MatType.Size = new System.Drawing.Size(19, 21);
            this.n_MatType.TabIndex = 25;
            this.n_MatType.Visible = false;
            // 
            // MatSplitMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 261);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "MatSplitMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拆件管理-机器";
            this.Load += new System.EventHandler(this.MatSplitMainForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MatSplitMainForm_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox s_ReceiptId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox s_ManufactCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMatName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxMatType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnFindMat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCurUserName;
        private System.Windows.Forms.TextBox s_OpaUser;
        private System.Windows.Forms.TextBox s_MatID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox n_MatType;
        private System.Windows.Forms.DateTimePicker s_OccurTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox n_OperateType;
    }
}