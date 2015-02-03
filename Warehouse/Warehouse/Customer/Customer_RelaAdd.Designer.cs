namespace Warehouse.Customer
{
    partial class Customer_RelaAdd
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
            this.buttonok = new System.Windows.Forms.Button();
            this.buttoncancell = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.s_InvoiceTitle = new System.Windows.Forms.TextBox();
            this.panelcusrela = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.c_ParentID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewcustrela = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除该关系ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s_Memo = new System.Windows.Forms.TextBox();
            this.c_CustID = new System.Windows.Forms.TextBox();
            this.panelcusrela.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewcustrela)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonok
            // 
            this.buttonok.Location = new System.Drawing.Point(391, 84);
            this.buttonok.Name = "buttonok";
            this.buttonok.Size = new System.Drawing.Size(90, 23);
            this.buttonok.TabIndex = 8;
            this.buttonok.Text = " 保存";
            this.buttonok.UseVisualStyleBackColor = true;
            this.buttonok.Click += new System.EventHandler(this.buttonok_Click);
            // 
            // buttoncancell
            // 
            this.buttoncancell.Location = new System.Drawing.Point(403, 332);
            this.buttoncancell.Name = "buttoncancell";
            this.buttoncancell.Size = new System.Drawing.Size(90, 23);
            this.buttoncancell.TabIndex = 9;
            this.buttoncancell.Text = "关闭";
            this.buttoncancell.UseVisualStyleBackColor = true;
            this.buttoncancell.Click += new System.EventHandler(this.buttoncancell_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前客户";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "发票抬头";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "备注";
            // 
            // s_InvoiceTitle
            // 
            this.s_InvoiceTitle.Location = new System.Drawing.Point(79, 47);
            this.s_InvoiceTitle.Name = "s_InvoiceTitle";
            this.s_InvoiceTitle.Size = new System.Drawing.Size(291, 21);
            this.s_InvoiceTitle.TabIndex = 4;
            // 
            // panelcusrela
            // 
            this.panelcusrela.Controls.Add(this.groupBox1);
            this.panelcusrela.Controls.Add(this.c_CustID);
            this.panelcusrela.Controls.Add(this.buttoncancell);
            this.panelcusrela.Controls.Add(this.label1);
            this.panelcusrela.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelcusrela.Location = new System.Drawing.Point(0, 0);
            this.panelcusrela.Name = "panelcusrela";
            this.panelcusrela.Size = new System.Drawing.Size(521, 360);
            this.panelcusrela.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.c_ParentID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dataGridViewcustrela);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonok);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.s_InvoiceTitle);
            this.groupBox1.Controls.Add(this.s_Memo);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 288);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下级客户列表";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(391, 21);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(90, 23);
            this.btnFind.TabIndex = 6;
            this.btnFind.Text = "选择下级客户";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(391, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.删除该关系ToolStripMenuItem_Click);
            // 
            // c_ParentID
            // 
            this.c_ParentID.Location = new System.Drawing.Point(79, 21);
            this.c_ParentID.Name = "c_ParentID";
            this.c_ParentID.ReadOnly = true;
            this.c_ParentID.Size = new System.Drawing.Size(291, 21);
            this.c_ParentID.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "客户名";
            // 
            // dataGridViewcustrela
            // 
            this.dataGridViewcustrela.AllowUserToAddRows = false;
            this.dataGridViewcustrela.AllowUserToDeleteRows = false;
            this.dataGridViewcustrela.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewcustrela.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewcustrela.Location = new System.Drawing.Point(7, 120);
            this.dataGridViewcustrela.Name = "dataGridViewcustrela";
            this.dataGridViewcustrela.RowTemplate.Height = 23;
            this.dataGridViewcustrela.Size = new System.Drawing.Size(484, 162);
            this.dataGridViewcustrela.TabIndex = 13;
            this.dataGridViewcustrela.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewcustrela_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除该关系ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(131, 26);
            // 
            // 删除该关系ToolStripMenuItem
            // 
            this.删除该关系ToolStripMenuItem.Name = "删除该关系ToolStripMenuItem";
            this.删除该关系ToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.删除该关系ToolStripMenuItem.Text = "删除该关系";
            this.删除该关系ToolStripMenuItem.Click += new System.EventHandler(this.删除该关系ToolStripMenuItem_Click);
            // 
            // s_Memo
            // 
            this.s_Memo.Location = new System.Drawing.Point(79, 73);
            this.s_Memo.Multiline = true;
            this.s_Memo.Name = "s_Memo";
            this.s_Memo.Size = new System.Drawing.Size(291, 38);
            this.s_Memo.TabIndex = 5;
            // 
            // c_CustID
            // 
            this.c_CustID.Location = new System.Drawing.Point(76, 11);
            this.c_CustID.Name = "c_CustID";
            this.c_CustID.ReadOnly = true;
            this.c_CustID.Size = new System.Drawing.Size(306, 21);
            this.c_CustID.TabIndex = 1;
            // 
            // Customer_RelaAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 360);
            this.Controls.Add(this.panelcusrela);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Customer_RelaAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "客户关系编辑";
            this.Load += new System.EventHandler(this.Customer_RelaAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Customer_RelaAdd_KeyPress);
            this.panelcusrela.ResumeLayout(false);
            this.panelcusrela.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewcustrela)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonok;
        private System.Windows.Forms.Button buttoncancell;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox s_InvoiceTitle;
        private System.Windows.Forms.Panel panelcusrela;
        private System.Windows.Forms.TextBox s_Memo;
        private System.Windows.Forms.DataGridView dataGridViewcustrela;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox c_CustID;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除该关系ToolStripMenuItem;
        private System.Windows.Forms.TextBox c_ParentID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnFind;
    }
}