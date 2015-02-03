namespace Warehouse.Stock
{
    partial class MatDocsForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.s_DocName = new System.Windows.Forms.TextBox();
            this.s_memo = new System.Windows.Forms.TextBox();
            this.btnAddMatDoc = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看附件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除该行记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_look = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "资料名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "备注";
            // 
            // s_DocName
            // 
            this.s_DocName.Location = new System.Drawing.Point(60, 7);
            this.s_DocName.MaxLength = 20;
            this.s_DocName.Name = "s_DocName";
            this.s_DocName.Size = new System.Drawing.Size(254, 21);
            this.s_DocName.TabIndex = 1;
            // 
            // s_memo
            // 
            this.s_memo.Location = new System.Drawing.Point(60, 34);
            this.s_memo.MaxLength = 30;
            this.s_memo.Multiline = true;
            this.s_memo.Name = "s_memo";
            this.s_memo.Size = new System.Drawing.Size(254, 48);
            this.s_memo.TabIndex = 2;
            // 
            // btnAddMatDoc
            // 
            this.btnAddMatDoc.Enabled = false;
            this.btnAddMatDoc.Location = new System.Drawing.Point(358, 11);
            this.btnAddMatDoc.Name = "btnAddMatDoc";
            this.btnAddMatDoc.Size = new System.Drawing.Size(88, 23);
            this.btnAddMatDoc.TabIndex = 3;
            this.btnAddMatDoc.Text = "绑定附件";
            this.btnAddMatDoc.UseVisualStyleBackColor = true;
            this.btnAddMatDoc.Click += new System.EventHandler(this.btnAddMatDoc_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看附件ToolStripMenuItem,
            this.删除该行记录ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
            // 
            // 查看附件ToolStripMenuItem
            // 
            this.查看附件ToolStripMenuItem.Name = "查看附件ToolStripMenuItem";
            this.查看附件ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.查看附件ToolStripMenuItem.Text = "查看附件";
            // 
            // 删除该行记录ToolStripMenuItem
            // 
            this.删除该行记录ToolStripMenuItem.Name = "删除该行记录ToolStripMenuItem";
            this.删除该行记录ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.删除该行记录ToolStripMenuItem.Text = "删除(&D)";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btn_look);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnAddMatDoc);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.s_DocName);
            this.panel1.Controls.Add(this.s_memo);
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 103);
            this.panel1.TabIndex = 11;
            // 
            // btn_look
            // 
            this.btn_look.Enabled = false;
            this.btn_look.Location = new System.Drawing.Point(359, 50);
            this.btn_look.Name = "btn_look";
            this.btn_look.Size = new System.Drawing.Size(87, 23);
            this.btn_look.TabIndex = 4;
            this.btn_look.Text = "浏览附件";
            this.btn_look.UseVisualStyleBackColor = true;
            this.btn_look.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(320, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "*";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(138, 113);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(259, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MatDocsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 145);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.button3);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MatDocsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "媒体类技术资料";
            this.Load += new System.EventHandler(this.MatDocsForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MatDocsForm_KeyPress);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox s_DocName;
        private System.Windows.Forms.TextBox s_memo;
        private System.Windows.Forms.Button btnAddMatDoc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除该行记录ToolStripMenuItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem 查看附件ToolStripMenuItem;
        private System.Windows.Forms.Button btn_look;
        private System.Windows.Forms.Button button3;
    }
}