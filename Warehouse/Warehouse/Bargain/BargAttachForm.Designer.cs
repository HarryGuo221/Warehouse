namespace Warehouse.Bargain
{
    partial class BargAttachForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnGetAttach = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.s_memo = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.s_AttachName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddDoc = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看附件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.n_barsysid = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "附件名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "备注";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.n_barsysid);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.btnGetAttach);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.s_memo);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.s_AttachName);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnAddDoc);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(4, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 130);
            this.panel1.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(14, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 103;
            this.label4.Text = "label4";
            // 
            // btnDel
            // 
            this.btnDel.Enabled = false;
            this.btnDel.Location = new System.Drawing.Point(395, 69);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 8;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.删除DToolStripMenuItem_Click);
            // 
            // btnGetAttach
            // 
            this.btnGetAttach.Enabled = false;
            this.btnGetAttach.Location = new System.Drawing.Point(395, 40);
            this.btnGetAttach.Name = "btnGetAttach";
            this.btnGetAttach.Size = new System.Drawing.Size(75, 23);
            this.btnGetAttach.TabIndex = 5;
            this.btnGetAttach.Text = "浏览媒体";
            this.btnGetAttach.UseVisualStyleBackColor = true;
            this.btnGetAttach.Click += new System.EventHandler(this.查看附件ToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(288, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 101;
            this.label5.Text = "*";
            // 
            // s_memo
            // 
            this.s_memo.Location = new System.Drawing.Point(68, 69);
            this.s_memo.Multiline = true;
            this.s_memo.Name = "s_memo";
            this.s_memo.Size = new System.Drawing.Size(214, 52);
            this.s_memo.TabIndex = 3;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(314, 69);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // s_AttachName
            // 
            this.s_AttachName.Location = new System.Drawing.Point(68, 42);
            this.s_AttachName.Name = "s_AttachName";
            this.s_AttachName.Size = new System.Drawing.Size(214, 21);
            this.s_AttachName.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(314, 98);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddDoc
            // 
            this.btnAddDoc.Enabled = false;
            this.btnAddDoc.Location = new System.Drawing.Point(314, 40);
            this.btnAddDoc.Name = "btnAddDoc";
            this.btnAddDoc.Size = new System.Drawing.Size(75, 23);
            this.btnAddDoc.TabIndex = 4;
            this.btnAddDoc.Text = "绑定媒体";
            this.btnAddDoc.UseVisualStyleBackColor = true;
            this.btnAddDoc.Click += new System.EventHandler(this.btnAddDoc_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 138);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(473, 205);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看附件ToolStripMenuItem,
            this.删除DToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
            // 
            // 查看附件ToolStripMenuItem
            // 
            this.查看附件ToolStripMenuItem.Name = "查看附件ToolStripMenuItem";
            this.查看附件ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.查看附件ToolStripMenuItem.Text = "浏览媒体";
            this.查看附件ToolStripMenuItem.Click += new System.EventHandler(this.查看附件ToolStripMenuItem_Click);
            // 
            // 删除DToolStripMenuItem
            // 
            this.删除DToolStripMenuItem.Name = "删除DToolStripMenuItem";
            this.删除DToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.删除DToolStripMenuItem.Text = "删除(&D)";
            this.删除DToolStripMenuItem.Click += new System.EventHandler(this.删除DToolStripMenuItem_Click);
            // 
            // n_barsysid
            // 
            this.n_barsysid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.n_barsysid.Location = new System.Drawing.Point(379, 10);
            this.n_barsysid.Name = "n_barsysid";
            this.n_barsysid.Size = new System.Drawing.Size(43, 21);
            this.n_barsysid.TabIndex = 105;
            this.n_barsysid.Visible = false;
            // 
            // BargAttachForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 355);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "BargAttachForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合同附件";
            this.Load += new System.EventHandler(this.BargAttachForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BargAttachForm_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox s_memo;
        private System.Windows.Forms.TextBox s_AttachName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAddDoc;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看附件ToolStripMenuItem;
        private System.Windows.Forms.Button btnGetAttach;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox n_barsysid;
    }
}