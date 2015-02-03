namespace Warehouse.Stock
{
    partial class MatSplitChildForm
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
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxMatType = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.s_ChildManuCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReceiptId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.n_ChildMatType = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtMainMatName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(267, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 37;
            this.label9.Text = "*";
            // 
            // comboBoxMatType
            // 
            this.comboBoxMatType.FormattingEnabled = true;
            this.comboBoxMatType.Location = new System.Drawing.Point(121, 12);
            this.comboBoxMatType.MaxLength = 20;
            this.comboBoxMatType.Name = "comboBoxMatType";
            this.comboBoxMatType.Size = new System.Drawing.Size(138, 20);
            this.comboBoxMatType.TabIndex = 34;
            this.comboBoxMatType.Text = "--请选择--";
            this.comboBoxMatType.SelectedIndexChanged += new System.EventHandler(this.comboBoxMatType_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(349, 46);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(46, 23);
            this.btnClose.TabIndex = 31;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(289, 46);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(46, 23);
            this.btnOK.TabIndex = 30;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // s_ChildManuCode
            // 
            this.s_ChildManuCode.Location = new System.Drawing.Point(121, 48);
            this.s_ChildManuCode.Name = "s_ChildManuCode";
            this.s_ChildManuCode.Size = new System.Drawing.Size(138, 21);
            this.s_ChildManuCode.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "子物料制造编号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "子物料类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "被拆机器：";
            // 
            // txtReceiptId
            // 
            this.txtReceiptId.Location = new System.Drawing.Point(102, 50);
            this.txtReceiptId.Name = "txtReceiptId";
            this.txtReceiptId.ReadOnly = true;
            this.txtReceiptId.Size = new System.Drawing.Size(121, 21);
            this.txtReceiptId.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "拆件单据编号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(170, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "零件管理";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(14, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 309);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "零件管理";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.s_ChildManuCode);
            this.panel1.Controls.Add(this.comboBoxMatType);
            this.panel1.Controls.Add(this.n_ChildMatType);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 289);
            this.panel1.TabIndex = 40;
            // 
            // n_ChildMatType
            // 
            this.n_ChildMatType.BackColor = System.Drawing.Color.GreenYellow;
            this.n_ChildMatType.Location = new System.Drawing.Point(26, 12);
            this.n_ChildMatType.Name = "n_ChildMatType";
            this.n_ChildMatType.Size = new System.Drawing.Size(16, 21);
            this.n_ChildMatType.TabIndex = 39;
            this.n_ChildMatType.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(381, 195);
            this.dataGridView1.TabIndex = 38;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // txtMainMatName
            // 
            this.txtMainMatName.Location = new System.Drawing.Point(306, 50);
            this.txtMainMatName.Name = "txtMainMatName";
            this.txtMainMatName.ReadOnly = true;
            this.txtMainMatName.Size = new System.Drawing.Size(121, 21);
            this.txtMainMatName.TabIndex = 39;
            // 
            // MatSplitChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 410);
            this.Controls.Add(this.txtMainMatName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtReceiptId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MatSplitChildForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "零件管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MatSplitChildForm_FormClosing);
            this.Load += new System.EventHandler(this.MatSplitChildForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxMatType;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox s_ChildManuCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReceiptId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtMainMatName;
        private System.Windows.Forms.TextBox n_ChildMatType;
        private System.Windows.Forms.Panel panel1;
    }
}