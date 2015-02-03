namespace Warehouse.Financial
{
    partial class FinancialForm
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
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnFindCustId = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRecord = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxPayMethod = new System.Windows.Forms.ComboBox();
            this.txtAbstract = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInMoney = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPinZhengHao = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBankAccount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labSumY = new System.Windows.Forms.Label();
            this.labSumS = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.labSumInvoice = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAutoVeri = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(64, 31);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(121, 21);
            this.txtInvoiceNo.TabIndex = 3;
            this.txtInvoiceNo.TextChanged += new System.EventHandler(this.txtInvoiceNo_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "发票号：";
            // 
            // btnFindCustId
            // 
            this.btnFindCustId.Location = new System.Drawing.Point(190, 3);
            this.btnFindCustId.Name = "btnFindCustId";
            this.btnFindCustId.Size = new System.Drawing.Size(37, 23);
            this.btnFindCustId.TabIndex = 23;
            this.btnFindCustId.Text = "…";
            this.btnFindCustId.UseVisualStyleBackColor = true;
            this.btnFindCustId.Click += new System.EventHandler(this.btnFindCustId_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(786, 82);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(786, 109);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "重置";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(64, 111);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(713, 21);
            this.txtMemo.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "备注：";
            // 
            // txtRecord
            // 
            this.txtRecord.Location = new System.Drawing.Point(64, 84);
            this.txtRecord.Name = "txtRecord";
            this.txtRecord.Size = new System.Drawing.Size(713, 21);
            this.txtRecord.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "核销记录：";
            // 
            // comboBoxPayMethod
            // 
            this.comboBoxPayMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPayMethod.FormattingEnabled = true;
            this.comboBoxPayMethod.Location = new System.Drawing.Point(294, 31);
            this.comboBoxPayMethod.Name = "comboBoxPayMethod";
            this.comboBoxPayMethod.Size = new System.Drawing.Size(121, 20);
            this.comboBoxPayMethod.TabIndex = 4;
            this.comboBoxPayMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxPayMethod_SelectedIndexChanged);
            // 
            // txtAbstract
            // 
            this.txtAbstract.Location = new System.Drawing.Point(478, 57);
            this.txtAbstract.Name = "txtAbstract";
            this.txtAbstract.Size = new System.Drawing.Size(299, 21);
            this.txtAbstract.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(441, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "摘要：";
            // 
            // txtInMoney
            // 
            this.txtInMoney.Location = new System.Drawing.Point(666, 30);
            this.txtInMoney.Name = "txtInMoney";
            this.txtInMoney.ReadOnly = true;
            this.txtInMoney.Size = new System.Drawing.Size(111, 21);
            this.txtInMoney.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(603, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "核销金额：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(233, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "支付方式：";
            // 
            // txtCustName
            // 
            this.txtCustName.Location = new System.Drawing.Point(294, 4);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(483, 21);
            this.txtCustName.TabIndex = 2;
            this.txtCustName.TextChanged += new System.EventHandler(this.s_CustName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(233, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "客户名称：";
            // 
            // txtPinZhengHao
            // 
            this.txtPinZhengHao.Location = new System.Drawing.Point(478, 30);
            this.txtPinZhengHao.Name = "txtPinZhengHao";
            this.txtPinZhengHao.Size = new System.Drawing.Size(113, 21);
            this.txtPinZhengHao.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "凭证号：";
            // 
            // txtBankAccount
            // 
            this.txtBankAccount.Location = new System.Drawing.Point(64, 57);
            this.txtBankAccount.Name = "txtBankAccount";
            this.txtBankAccount.Size = new System.Drawing.Size(351, 21);
            this.txtBankAccount.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "银行账号：";
            // 
            // txtCustId
            // 
            this.txtCustId.Location = new System.Drawing.Point(64, 4);
            this.txtCustId.Name = "txtCustId";
            this.txtCustId.Size = new System.Drawing.Size(121, 21);
            this.txtCustId.TabIndex = 1;
            this.txtCustId.TextChanged += new System.EventHandler(this.s_CustId_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户代码：";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(883, 116);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "是否全部核销";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 191);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1003, 244);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.53333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.46667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 229F));
            this.tableLayoutPanel1.Controls.Add(this.labSumY, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labSumS, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label13, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labSumInvoice, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 441);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1003, 34);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // labSumY
            // 
            this.labSumY.AutoSize = true;
            this.labSumY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labSumY.Location = new System.Drawing.Point(594, 17);
            this.labSumY.Name = "labSumY";
            this.labSumY.Size = new System.Drawing.Size(174, 16);
            this.labSumY.TabIndex = 5;
            this.labSumY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSumS
            // 
            this.labSumS.AutoSize = true;
            this.labSumS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labSumS.Location = new System.Drawing.Point(775, 17);
            this.labSumS.Name = "labSumS";
            this.labSumS.Size = new System.Drawing.Size(224, 16);
            this.labSumS.TabIndex = 29;
            this.labSumS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(479, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "发票合计数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(594, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(174, 15);
            this.label12.TabIndex = 1;
            this.label12.Text = "应核销金额";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(775, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(224, 15);
            this.label13.TabIndex = 2;
            this.label13.Text = "实核销金额";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Location = new System.Drawing.Point(443, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 16);
            this.label14.TabIndex = 3;
            this.label14.Text = "合计";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSumInvoice
            // 
            this.labSumInvoice.AutoSize = true;
            this.labSumInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labSumInvoice.Location = new System.Drawing.Point(479, 17);
            this.labSumInvoice.Name = "labSumInvoice";
            this.labSumInvoice.Size = new System.Drawing.Size(108, 16);
            this.labSumInvoice.TabIndex = 4;
            this.labSumInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("宋体", 15F);
            this.labelInfo.Location = new System.Drawing.Point(336, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(89, 20);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = "应收账务";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(13, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(999, 153);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAutoVeri);
            this.panel1.Controls.Add(this.txtInvoiceNo);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.btnFindCustId);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtRecord);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.comboBoxPayMethod);
            this.panel1.Controls.Add(this.txtAbstract);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtInMoney);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtCustName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtPinZhengHao);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtBankAccount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCustId);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(11, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 137);
            this.panel1.TabIndex = 1;
            // 
            // btnAutoVeri
            // 
            this.btnAutoVeri.Location = new System.Drawing.Point(899, 82);
            this.btnAutoVeri.Name = "btnAutoVeri";
            this.btnAutoVeri.Size = new System.Drawing.Size(75, 23);
            this.btnAutoVeri.TabIndex = 29;
            this.btnAutoVeri.Text = "自动核销";
            this.btnAutoVeri.UseVisualStyleBackColor = true;
            this.btnAutoVeri.Click += new System.EventHandler(this.btnAutoVeri_Click);
            // 
            // FinancialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 483);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataGridView1);
            this.KeyPreview = true;
            this.Name = "FinancialForm";
            this.Text = "应收应付（核销）";
            this.Load += new System.EventHandler(this.FinancialForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FinancialForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFindCustId;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRecord;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxPayMethod;
        private System.Windows.Forms.TextBox txtAbstract;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtInMoney;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPinZhengHao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBankAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCustId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label labSumY;
        private System.Windows.Forms.Label labSumS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labSumInvoice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAutoVeri;
    }
}