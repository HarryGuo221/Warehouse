namespace Warehouse.Modal
{
    partial class ReceiptQueryForm
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
            this.txtCustomerReceiptId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxReceName = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReceTypeID = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvReceipt_Main = new System.Windows.Forms.DataGridView();
            this.dgvReceipt_Det = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt_Det)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "单据类别";
            // 
            // txtCustomerReceiptId
            // 
            this.txtCustomerReceiptId.Location = new System.Drawing.Point(81, 9);
            this.txtCustomerReceiptId.Name = "txtCustomerReceiptId";
            this.txtCustomerReceiptId.Size = new System.Drawing.Size(121, 21);
            this.txtCustomerReceiptId.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "自定义单据号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "单据日期";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(81, 36);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(121, 21);
            this.dtpFrom.TabIndex = 5;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(273, 36);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(121, 21);
            this.dtpTo.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "到";
            // 
            // comboBoxReceName
            // 
            this.comboBoxReceName.FormattingEnabled = true;
            this.comboBoxReceName.Location = new System.Drawing.Point(273, 9);
            this.comboBoxReceName.Name = "comboBoxReceName";
            this.comboBoxReceName.Size = new System.Drawing.Size(121, 20);
            this.comboBoxReceName.TabIndex = 4;
            this.comboBoxReceName.SelectedIndexChanged += new System.EventHandler(this.comboBoxReceName_SelectedIndexChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(411, 35);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 7;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtReceTypeID);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBoxReceName);
            this.panel1.Controls.Add(this.txtCustomerReceiptId);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Location = new System.Drawing.Point(6, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 69);
            this.panel1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(400, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "*";
            // 
            // txtReceTypeID
            // 
            this.txtReceTypeID.Location = new System.Drawing.Point(411, 8);
            this.txtReceTypeID.Name = "txtReceTypeID";
            this.txtReceTypeID.ReadOnly = true;
            this.txtReceTypeID.Size = new System.Drawing.Size(75, 21);
            this.txtReceTypeID.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 107);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvReceipt_Main);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvReceipt_Det);
            this.splitContainer1.Size = new System.Drawing.Size(1004, 408);
            this.splitContainer1.SplitterDistance = 284;
            this.splitContainer1.TabIndex = 11;
            // 
            // dgvReceipt_Main
            // 
            this.dgvReceipt_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceipt_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReceipt_Main.Location = new System.Drawing.Point(0, 0);
            this.dgvReceipt_Main.Name = "dgvReceipt_Main";
            this.dgvReceipt_Main.RowTemplate.Height = 23;
            this.dgvReceipt_Main.Size = new System.Drawing.Size(1004, 284);
            this.dgvReceipt_Main.TabIndex = 0;
            this.dgvReceipt_Main.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipt_Main_CellClick);
            this.dgvReceipt_Main.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceipt_Main_CellDoubleClick);
            // 
            // dgvReceipt_Det
            // 
            this.dgvReceipt_Det.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceipt_Det.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReceipt_Det.Location = new System.Drawing.Point(0, 0);
            this.dgvReceipt_Det.Name = "dgvReceipt_Det";
            this.dgvReceipt_Det.RowTemplate.Height = 23;
            this.dgvReceipt_Det.Size = new System.Drawing.Size(1004, 120);
            this.dgvReceipt_Det.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 90);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // ReceiptQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 518);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReceiptQueryForm";
            this.Text = "单据查询";
            this.Load += new System.EventHandler(this.ReceiptQueryForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt_Det)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCustomerReceiptId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxReceName;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvReceipt_Main;
        private System.Windows.Forms.DataGridView dgvReceipt_Det;
        private System.Windows.Forms.TextBox txtReceTypeID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
    }
}