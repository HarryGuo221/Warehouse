namespace Warehouse.Modal
{
    partial class EditReceiptModalDefineForm
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
            this.panelReceiptModal = new System.Windows.Forms.Panel();
            this.s_ReceTypeID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.n_DetailRows = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.s_InOrOutBound = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.s_ynAffectStock = new System.Windows.Forms.CheckBox();
            this.s_ynAffectCost = new System.Windows.Forms.CheckBox();
            this.s_ynpay = new System.Windows.Forms.CheckBox();
            this.s_ynVerification = new System.Windows.Forms.CheckBox();
            this.s_istemp = new System.Windows.Forms.CheckBox();
            this.s_ReceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelReceiptModal.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelReceiptModal
            // 
            this.panelReceiptModal.Controls.Add(this.s_ReceTypeID);
            this.panelReceiptModal.Controls.Add(this.label7);
            this.panelReceiptModal.Controls.Add(this.label6);
            this.panelReceiptModal.Controls.Add(this.btnCancel);
            this.panelReceiptModal.Controls.Add(this.btnSave);
            this.panelReceiptModal.Controls.Add(this.label5);
            this.panelReceiptModal.Controls.Add(this.n_DetailRows);
            this.panelReceiptModal.Controls.Add(this.label4);
            this.panelReceiptModal.Controls.Add(this.s_InOrOutBound);
            this.panelReceiptModal.Controls.Add(this.label3);
            this.panelReceiptModal.Controls.Add(this.s_ynAffectStock);
            this.panelReceiptModal.Controls.Add(this.s_ynAffectCost);
            this.panelReceiptModal.Controls.Add(this.s_ynpay);
            this.panelReceiptModal.Controls.Add(this.s_ynVerification);
            this.panelReceiptModal.Controls.Add(this.s_istemp);
            this.panelReceiptModal.Controls.Add(this.s_ReceName);
            this.panelReceiptModal.Controls.Add(this.label2);
            this.panelReceiptModal.Controls.Add(this.label1);
            this.panelReceiptModal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReceiptModal.Location = new System.Drawing.Point(0, 0);
            this.panelReceiptModal.Name = "panelReceiptModal";
            this.panelReceiptModal.Size = new System.Drawing.Size(413, 234);
            this.panelReceiptModal.TabIndex = 6;
            // 
            // s_ReceTypeID
            // 
            this.s_ReceTypeID.Location = new System.Drawing.Point(83, 53);
            this.s_ReceTypeID.Name = "s_ReceTypeID";
            this.s_ReceTypeID.Size = new System.Drawing.Size(108, 21);
            this.s_ReceTypeID.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(385, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 36;
            this.label7.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(195, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "*";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(304, 199);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(214, 199);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14F);
            this.label5.Location = new System.Drawing.Point(126, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 19);
            this.label5.TabIndex = 32;
            this.label5.Text = "单据模板定义";
            // 
            // n_DetailRows
            // 
            this.n_DetailRows.Location = new System.Drawing.Point(262, 110);
            this.n_DetailRows.Name = "n_DetailRows";
            this.n_DetailRows.Size = new System.Drawing.Size(117, 21);
            this.n_DetailRows.TabIndex = 5;
            this.n_DetailRows.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "子表行数：";
            this.label4.Visible = false;
            // 
            // s_InOrOutBound
            // 
            this.s_InOrOutBound.FormattingEnabled = true;
            this.s_InOrOutBound.Items.AddRange(new object[] {
            "--请选择--",
            "出库",
            "入库"});
            this.s_InOrOutBound.Location = new System.Drawing.Point(83, 111);
            this.s_InOrOutBound.Name = "s_InOrOutBound";
            this.s_InOrOutBound.Size = new System.Drawing.Size(108, 20);
            this.s_InOrOutBound.TabIndex = 4;
            this.s_InOrOutBound.Text = "出库";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "出库/入库：";
            // 
            // s_ynAffectStock
            // 
            this.s_ynAffectStock.AutoSize = true;
            this.s_ynAffectStock.Checked = true;
            this.s_ynAffectStock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.s_ynAffectStock.Location = new System.Drawing.Point(83, 146);
            this.s_ynAffectStock.Name = "s_ynAffectStock";
            this.s_ynAffectStock.Size = new System.Drawing.Size(96, 16);
            this.s_ynAffectStock.TabIndex = 6;
            this.s_ynAffectStock.Text = "是否操作库存";
            this.s_ynAffectStock.UseVisualStyleBackColor = true;
            // 
            // s_ynAffectCost
            // 
            this.s_ynAffectCost.AutoSize = true;
            this.s_ynAffectCost.Checked = true;
            this.s_ynAffectCost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.s_ynAffectCost.Location = new System.Drawing.Point(83, 168);
            this.s_ynAffectCost.Name = "s_ynAffectCost";
            this.s_ynAffectCost.Size = new System.Drawing.Size(96, 16);
            this.s_ynAffectCost.TabIndex = 8;
            this.s_ynAffectCost.Text = "是否影响成本";
            this.s_ynAffectCost.UseVisualStyleBackColor = true;
            // 
            // s_ynpay
            // 
            this.s_ynpay.AutoSize = true;
            this.s_ynpay.Checked = true;
            this.s_ynpay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.s_ynpay.Location = new System.Drawing.Point(214, 168);
            this.s_ynpay.Name = "s_ynpay";
            this.s_ynpay.Size = new System.Drawing.Size(120, 16);
            this.s_ynpay.TabIndex = 9;
            this.s_ynpay.Text = "是否进应收应付帐";
            this.s_ynpay.UseVisualStyleBackColor = true;
            // 
            // s_ynVerification
            // 
            this.s_ynVerification.AutoSize = true;
            this.s_ynVerification.Checked = true;
            this.s_ynVerification.CheckState = System.Windows.Forms.CheckState.Checked;
            this.s_ynVerification.Location = new System.Drawing.Point(214, 146);
            this.s_ynVerification.Name = "s_ynVerification";
            this.s_ynVerification.Size = new System.Drawing.Size(132, 16);
            this.s_ynVerification.TabIndex = 7;
            this.s_ynVerification.Text = "是否可进行核销操作";
            this.s_ynVerification.UseVisualStyleBackColor = true;
            // 
            // s_istemp
            // 
            this.s_istemp.AutoSize = true;
            this.s_istemp.Location = new System.Drawing.Point(83, 86);
            this.s_istemp.Name = "s_istemp";
            this.s_istemp.Size = new System.Drawing.Size(96, 16);
            this.s_istemp.TabIndex = 3;
            this.s_istemp.Text = "是否临时单据";
            this.s_istemp.UseVisualStyleBackColor = true;
            this.s_istemp.CheckedChanged += new System.EventHandler(this.s_istemp_CheckedChanged);
            // 
            // s_ReceName
            // 
            this.s_ReceName.Location = new System.Drawing.Point(262, 52);
            this.s_ReceName.Name = "s_ReceName";
            this.s_ReceName.Size = new System.Drawing.Size(117, 21);
            this.s_ReceName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "模板名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "模板编号：";
            // 
            // EditReceiptModalDefineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 234);
            this.Controls.Add(this.panelReceiptModal);
            this.MaximizeBox = false;
            this.Name = "EditReceiptModalDefineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单据模板定义";
            this.Load += new System.EventHandler(this.ReceiptModalDefineForm_Load);
            this.panelReceiptModal.ResumeLayout(false);
            this.panelReceiptModal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelReceiptModal;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox n_DetailRows;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox s_InOrOutBound;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox s_ynAffectStock;
        private System.Windows.Forms.CheckBox s_ynAffectCost;
        private System.Windows.Forms.CheckBox s_ynpay;
        private System.Windows.Forms.CheckBox s_ynVerification;
        private System.Windows.Forms.CheckBox s_istemp;
        private System.Windows.Forms.TextBox s_ReceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox s_ReceTypeID;

    }
}