namespace Warehouse
{
    partial class ImportExcelForm
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
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTableName = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnImport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxName = new System.Windows.Forms.ComboBox();
            this.comboBoxAddress = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxPhone = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxFax = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxContactPerson = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxMemo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择Excel文件";
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(114, 9);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(356, 21);
            this.textBoxFilePath.TabIndex = 1;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(476, 7);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(65, 23);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Text = "浏览";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "工作表名称";
            // 
            // comboBoxTableName
            // 
            this.comboBoxTableName.FormattingEnabled = true;
            this.comboBoxTableName.Location = new System.Drawing.Point(114, 47);
            this.comboBoxTableName.Name = "comboBoxTableName";
            this.comboBoxTableName.Size = new System.Drawing.Size(121, 20);
            this.comboBoxTableName.TabIndex = 4;
            this.comboBoxTableName.SelectedIndexChanged += new System.EventHandler(this.comboBoxTableName_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 187);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(866, 274);
            this.dataGridView1.TabIndex = 5;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(781, 46);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(65, 23);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxFilePath);
            this.panel1.Controls.Add(this.comboBoxTableName);
            this.panel1.Controls.Add(this.btnBrowser);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 81);
            this.panel1.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(476, 45);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.comboBoxMemo);
            this.panel2.Controls.Add(this.btnImport);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.comboBoxContactPerson);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.comboBoxFax);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.comboBoxPhone);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.comboBoxAddress);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.comboBoxName);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.comboBoxCode);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(12, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(866, 82);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(15, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(46, 69);
            this.panel3.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(4, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 52);
            this.label5.TabIndex = 8;
            this.label5.Text = "选择对应的列";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "单位名称";
            // 
            // comboBoxCode
            // 
            this.comboBoxCode.FormattingEnabled = true;
            this.comboBoxCode.Location = new System.Drawing.Point(153, 14);
            this.comboBoxCode.Name = "comboBoxCode";
            this.comboBoxCode.Size = new System.Drawing.Size(81, 20);
            this.comboBoxCode.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "单位编码";
            // 
            // comboBoxName
            // 
            this.comboBoxName.FormattingEnabled = true;
            this.comboBoxName.Location = new System.Drawing.Point(313, 14);
            this.comboBoxName.Name = "comboBoxName";
            this.comboBoxName.Size = new System.Drawing.Size(81, 20);
            this.comboBoxName.TabIndex = 10;
            // 
            // comboBoxAddress
            // 
            this.comboBoxAddress.FormattingEnabled = true;
            this.comboBoxAddress.Location = new System.Drawing.Point(452, 14);
            this.comboBoxAddress.Name = "comboBoxAddress";
            this.comboBoxAddress.Size = new System.Drawing.Size(81, 20);
            this.comboBoxAddress.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(417, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "地址";
            // 
            // comboBoxPhone
            // 
            this.comboBoxPhone.FormattingEnabled = true;
            this.comboBoxPhone.Location = new System.Drawing.Point(587, 14);
            this.comboBoxPhone.Name = "comboBoxPhone";
            this.comboBoxPhone.Size = new System.Drawing.Size(81, 20);
            this.comboBoxPhone.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(552, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "电话";
            // 
            // comboBoxFax
            // 
            this.comboBoxFax.FormattingEnabled = true;
            this.comboBoxFax.Location = new System.Drawing.Point(153, 46);
            this.comboBoxFax.Name = "comboBoxFax";
            this.comboBoxFax.Size = new System.Drawing.Size(81, 20);
            this.comboBoxFax.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(112, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "传真";
            // 
            // comboBoxContactPerson
            // 
            this.comboBoxContactPerson.FormattingEnabled = true;
            this.comboBoxContactPerson.Location = new System.Drawing.Point(313, 46);
            this.comboBoxContactPerson.Name = "comboBoxContactPerson";
            this.comboBoxContactPerson.Size = new System.Drawing.Size(81, 20);
            this.comboBoxContactPerson.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(266, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "联系人";
            // 
            // comboBoxMemo
            // 
            this.comboBoxMemo.FormattingEnabled = true;
            this.comboBoxMemo.Location = new System.Drawing.Point(452, 46);
            this.comboBoxMemo.Name = "comboBoxMemo";
            this.comboBoxMemo.Size = new System.Drawing.Size(81, 20);
            this.comboBoxMemo.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(417, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "备注";
            // 
            // ImportExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ImportExcelForm";
            this.Text = "导入Excel数据";
            this.Load += new System.EventHandler(this.ImportExcelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxTableName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox comboBoxCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBoxName;
        private System.Windows.Forms.ComboBox comboBoxAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxPhone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxContactPerson;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxFax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxMemo;
        private System.Windows.Forms.Label label10;

    }
}