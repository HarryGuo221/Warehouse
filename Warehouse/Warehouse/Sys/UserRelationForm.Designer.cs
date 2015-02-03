namespace Warehouse.Sys
{
    partial class UserRelationForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewUserRela = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除该人员ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFind = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxChildUser = new System.Windows.Forms.GroupBox();
            this.txtCanRepair = new System.Windows.Forms.TextBox();
            this.txtChildUserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxUserGrade = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxUserType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtCurUserName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserRela)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBoxChildUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前人员：";
            // 
            // dataGridViewUserRela
            // 
            this.dataGridViewUserRela.AllowUserToAddRows = false;
            this.dataGridViewUserRela.AllowUserToDeleteRows = false;
            this.dataGridViewUserRela.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewUserRela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUserRela.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewUserRela.Location = new System.Drawing.Point(6, 78);
            this.dataGridViewUserRela.Name = "dataGridViewUserRela";
            this.dataGridViewUserRela.RowTemplate.Height = 23;
            this.dataGridViewUserRela.Size = new System.Drawing.Size(459, 187);
            this.dataGridViewUserRela.TabIndex = 5;
            this.dataGridViewUserRela.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUserRela_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除该人员ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // 删除该人员ToolStripMenuItem
            // 
            this.删除该人员ToolStripMenuItem.Name = "删除该人员ToolStripMenuItem";
            this.删除该人员ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除该人员ToolStripMenuItem.Text = "删除该人员";
            this.删除该人员ToolStripMenuItem.Click += new System.EventHandler(this.删除该人员ToolStripMenuItem_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(220, 61);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(89, 23);
            this.btnFind.TabIndex = 8;
            this.btnFind.Text = "选择下级人员";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14F);
            this.label3.Location = new System.Drawing.Point(172, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "人员关系设定";
            // 
            // groupBoxChildUser
            // 
            this.groupBoxChildUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxChildUser.Controls.Add(this.txtCanRepair);
            this.groupBoxChildUser.Controls.Add(this.txtChildUserName);
            this.groupBoxChildUser.Controls.Add(this.label7);
            this.groupBoxChildUser.Controls.Add(this.label6);
            this.groupBoxChildUser.Controls.Add(this.comboBoxUserGrade);
            this.groupBoxChildUser.Controls.Add(this.label5);
            this.groupBoxChildUser.Controls.Add(this.comboBoxUserType);
            this.groupBoxChildUser.Controls.Add(this.label4);
            this.groupBoxChildUser.Controls.Add(this.btnSave);
            this.groupBoxChildUser.Controls.Add(this.dataGridViewUserRela);
            this.groupBoxChildUser.Location = new System.Drawing.Point(12, 99);
            this.groupBoxChildUser.Name = "groupBoxChildUser";
            this.groupBoxChildUser.Size = new System.Drawing.Size(471, 271);
            this.groupBoxChildUser.TabIndex = 10;
            this.groupBoxChildUser.TabStop = false;
            this.groupBoxChildUser.Text = "下级人员";
            // 
            // txtCanRepair
            // 
            this.txtCanRepair.Location = new System.Drawing.Point(84, 49);
            this.txtCanRepair.Name = "txtCanRepair";
            this.txtCanRepair.Size = new System.Drawing.Size(110, 21);
            this.txtCanRepair.TabIndex = 32;
            // 
            // txtChildUserName
            // 
            this.txtChildUserName.Location = new System.Drawing.Point(84, 17);
            this.txtChildUserName.Name = "txtChildUserName";
            this.txtChildUserName.ReadOnly = true;
            this.txtChildUserName.Size = new System.Drawing.Size(110, 21);
            this.txtChildUserName.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 30;
            this.label7.Text = "可维修机型：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "下级人员：";
            // 
            // comboBoxUserGrade
            // 
            this.comboBoxUserGrade.FormattingEnabled = true;
            this.comboBoxUserGrade.Location = new System.Drawing.Point(268, 48);
            this.comboBoxUserGrade.Name = "comboBoxUserGrade";
            this.comboBoxUserGrade.Size = new System.Drawing.Size(110, 20);
            this.comboBoxUserGrade.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "人员等级：";
            // 
            // comboBoxUserType
            // 
            this.comboBoxUserType.FormattingEnabled = true;
            this.comboBoxUserType.Location = new System.Drawing.Point(268, 17);
            this.comboBoxUserType.Name = "comboBoxUserType";
            this.comboBoxUserType.Size = new System.Drawing.Size(110, 20);
            this.comboBoxUserType.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "人员类型：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(400, 45);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 23);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtCurUserName
            // 
            this.txtCurUserName.Location = new System.Drawing.Point(96, 63);
            this.txtCurUserName.Name = "txtCurUserName";
            this.txtCurUserName.ReadOnly = true;
            this.txtCurUserName.Size = new System.Drawing.Size(110, 21);
            this.txtCurUserName.TabIndex = 23;
            // 
            // UserRelationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 382);
            this.Controls.Add(this.groupBoxChildUser);
            this.Controls.Add(this.txtCurUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFind);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserRelationForm";
            this.Text = "人员关系设定";
            this.Load += new System.EventHandler(this.UserRelationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserRela)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBoxChildUser.ResumeLayout(false);
            this.groupBoxChildUser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewUserRela;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxChildUser;
        private System.Windows.Forms.TextBox txtCurUserName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除该人员ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxUserGrade;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxUserType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCanRepair;
        private System.Windows.Forms.TextBox txtChildUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}