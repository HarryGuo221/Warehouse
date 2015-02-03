namespace Warehouse.Stock
{
    partial class MatRestoreForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.n_ChildMatType = new System.Windows.Forms.TextBox();
            this.s_ParaentManuCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.s_ChildMatID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.n_ChildMatNum = new System.Windows.Forms.TextBox();
            this.txtMainMatName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.n_ChildMatPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxChildMatName = new System.Windows.Forms.TextBox();
            this.s_ReceiptId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.s_ChildManuCode = new System.Windows.Forms.TextBox();
            this.comboBoxMatType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 442);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "还原零件管理";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonSearch);
            this.panel1.Controls.Add(this.n_ChildMatType);
            this.panel1.Controls.Add(this.s_ParaentManuCode);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Controls.Add(this.s_ChildMatID);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.n_ChildMatNum);
            this.panel1.Controls.Add(this.txtMainMatName);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.n_ChildMatPrice);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBoxChildMatName);
            this.panel1.Controls.Add(this.s_ReceiptId);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.s_ChildManuCode);
            this.panel1.Controls.Add(this.comboBoxMatType);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Location = new System.Drawing.Point(3, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 437);
            this.panel1.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(502, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "*";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(513, 35);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(34, 23);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "...";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // n_ChildMatType
            // 
            this.n_ChildMatType.BackColor = System.Drawing.Color.GreenYellow;
            this.n_ChildMatType.Location = new System.Drawing.Point(166, 62);
            this.n_ChildMatType.Name = "n_ChildMatType";
            this.n_ChildMatType.Size = new System.Drawing.Size(33, 21);
            this.n_ChildMatType.TabIndex = 39;
            this.n_ChildMatType.Visible = false;
            // 
            // s_ParaentManuCode
            // 
            this.s_ParaentManuCode.Location = new System.Drawing.Point(89, 35);
            this.s_ParaentManuCode.Name = "s_ParaentManuCode";
            this.s_ParaentManuCode.ReadOnly = true;
            this.s_ParaentManuCode.Size = new System.Drawing.Size(134, 21);
            this.s_ParaentManuCode.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(502, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 48;
            this.label10.Text = "*";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(326, 116);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 11;
            this.buttonAdd.Text = "新增";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // s_ChildMatID
            // 
            this.s_ChildMatID.BackColor = System.Drawing.Color.Red;
            this.s_ChildMatID.Location = new System.Drawing.Point(453, 36);
            this.s_ChildMatID.Name = "s_ChildMatID";
            this.s_ChildMatID.Size = new System.Drawing.Size(26, 21);
            this.s_ChildMatID.TabIndex = 46;
            this.s_ChildMatID.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 46;
            this.label11.Text = "机器制造编号";
            // 
            // n_ChildMatNum
            // 
            this.n_ChildMatNum.Location = new System.Drawing.Point(89, 89);
            this.n_ChildMatNum.Name = "n_ChildMatNum";
            this.n_ChildMatNum.Size = new System.Drawing.Size(134, 21);
            this.n_ChildMatNum.TabIndex = 8;
            // 
            // txtMainMatName
            // 
            this.txtMainMatName.Location = new System.Drawing.Point(362, 8);
            this.txtMainMatName.Name = "txtMainMatName";
            this.txtMainMatName.ReadOnly = true;
            this.txtMainMatName.Size = new System.Drawing.Size(138, 21);
            this.txtMainMatName.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 44;
            this.label8.Text = "零件数量";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "拆件单据编号";
            // 
            // n_ChildMatPrice
            // 
            this.n_ChildMatPrice.Location = new System.Drawing.Point(362, 89);
            this.n_ChildMatPrice.Name = "n_ChildMatPrice";
            this.n_ChildMatPrice.Size = new System.Drawing.Size(138, 21);
            this.n_ChildMatPrice.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "被拆机器";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(291, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 42;
            this.label7.Text = "零件价格";
            // 
            // textBoxChildMatName
            // 
            this.textBoxChildMatName.Location = new System.Drawing.Point(362, 36);
            this.textBoxChildMatName.Name = "textBoxChildMatName";
            this.textBoxChildMatName.Size = new System.Drawing.Size(138, 21);
            this.textBoxChildMatName.TabIndex = 4;
            // 
            // s_ReceiptId
            // 
            this.s_ReceiptId.Location = new System.Drawing.Point(89, 8);
            this.s_ReceiptId.Name = "s_ReceiptId";
            this.s_ReceiptId.ReadOnly = true;
            this.s_ReceiptId.Size = new System.Drawing.Size(134, 21);
            this.s_ReceiptId.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(303, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "零件名称";
            // 
            // s_ChildManuCode
            // 
            this.s_ChildManuCode.Location = new System.Drawing.Point(362, 62);
            this.s_ChildManuCode.Name = "s_ChildManuCode";
            this.s_ChildManuCode.Size = new System.Drawing.Size(138, 21);
            this.s_ChildManuCode.TabIndex = 7;
            // 
            // comboBoxMatType
            // 
            this.comboBoxMatType.FormattingEnabled = true;
            this.comboBoxMatType.Location = new System.Drawing.Point(89, 62);
            this.comboBoxMatType.MaxLength = 100;
            this.comboBoxMatType.Name = "comboBoxMatType";
            this.comboBoxMatType.Size = new System.Drawing.Size(134, 20);
            this.comboBoxMatType.TabIndex = 6;
            this.comboBoxMatType.Text = "--请选择--";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(279, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "零件制造编号";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 149);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(540, 279);
            this.dataGridView1.TabIndex = 38;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "零件类型";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(227, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 37;
            this.label9.Text = "*";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(425, 116);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(227, 116);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "保存";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // MatRestoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 466);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "MatRestoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MatRestoreForm";
            this.Load += new System.EventHandler(this.MatRestoreForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MatRestoreForm_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox n_ChildMatType;
        private System.Windows.Forms.TextBox s_ParaentManuCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox s_ChildMatID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox n_ChildMatNum;
        private System.Windows.Forms.TextBox txtMainMatName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox n_ChildMatPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxChildMatName;
        private System.Windows.Forms.TextBox s_ReceiptId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox s_ChildManuCode;
        private System.Windows.Forms.ComboBox comboBoxMatType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;

    }
}