namespace Warehouse.Stock
{
    partial class EditMatRela
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
            this.panelMatRela = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.CmatnametextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.s_ChildMatID = new System.Windows.Forms.ComboBox();
            this.s_ParentMatID = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Pmatnametextbox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.n_CopyNumber = new System.Windows.Forms.TextBox();
            this.s_Memo = new System.Windows.Forms.TextBox();
            this.n_ContainsNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelMatRela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMatRela
            // 
            this.panelMatRela.Controls.Add(this.button3);
            this.panelMatRela.Controls.Add(this.CmatnametextBox);
            this.panelMatRela.Controls.Add(this.label8);
            this.panelMatRela.Controls.Add(this.buttonSearch);
            this.panelMatRela.Controls.Add(this.label7);
            this.panelMatRela.Controls.Add(this.label6);
            this.panelMatRela.Controls.Add(this.s_ChildMatID);
            this.panelMatRela.Controls.Add(this.s_ParentMatID);
            this.panelMatRela.Controls.Add(this.label2);
            this.panelMatRela.Controls.Add(this.label1);
            this.panelMatRela.Controls.Add(this.Pmatnametextbox);
            this.panelMatRela.Controls.Add(this.dataGridView1);
            this.panelMatRela.Controls.Add(this.n_CopyNumber);
            this.panelMatRela.Controls.Add(this.s_Memo);
            this.panelMatRela.Controls.Add(this.n_ContainsNumber);
            this.panelMatRela.Controls.Add(this.label5);
            this.panelMatRela.Controls.Add(this.label4);
            this.panelMatRela.Controls.Add(this.label3);
            this.panelMatRela.Controls.Add(this.button2);
            this.panelMatRela.Controls.Add(this.button1);
            this.panelMatRela.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMatRela.Location = new System.Drawing.Point(0, 0);
            this.panelMatRela.Name = "panelMatRela";
            this.panelMatRela.Size = new System.Drawing.Size(583, 353);
            this.panelMatRela.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(496, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // CmatnametextBox
            // 
            this.CmatnametextBox.Location = new System.Drawing.Point(93, 39);
            this.CmatnametextBox.Name = "CmatnametextBox";
            this.CmatnametextBox.Size = new System.Drawing.Size(173, 21);
            this.CmatnametextBox.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(272, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 44;
            this.label8.Text = "*";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(289, 39);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(42, 23);
            this.buttonSearch.TabIndex = 43;
            this.buttonSearch.Text = "查找";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 41;
            this.label7.Text = "耗材(选购件)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "机器";
            // 
            // s_ChildMatID
            // 
            this.s_ChildMatID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.s_ChildMatID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.s_ChildMatID.FormattingEnabled = true;
            this.s_ChildMatID.Location = new System.Drawing.Point(206, 93);
            this.s_ChildMatID.Name = "s_ChildMatID";
            this.s_ChildMatID.Size = new System.Drawing.Size(41, 20);
            this.s_ChildMatID.TabIndex = 36;
            this.s_ChildMatID.Visible = false;
            // 
            // s_ParentMatID
            // 
            this.s_ParentMatID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.s_ParentMatID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.s_ParentMatID.ForeColor = System.Drawing.SystemColors.InfoText;
            this.s_ParentMatID.FormattingEnabled = true;
            this.s_ParentMatID.Location = new System.Drawing.Point(86, 93);
            this.s_ParentMatID.Name = "s_ParentMatID";
            this.s_ParentMatID.Size = new System.Drawing.Size(36, 20);
            this.s_ParentMatID.TabIndex = 35;
            this.s_ParentMatID.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(143, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "子卡片编号";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(20, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "主卡片编号";
            this.label1.Visible = false;
            // 
            // Pmatnametextbox
            // 
            this.Pmatnametextbox.Location = new System.Drawing.Point(93, 12);
            this.Pmatnametextbox.Name = "Pmatnametextbox";
            this.Pmatnametextbox.Size = new System.Drawing.Size(173, 21);
            this.Pmatnametextbox.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 96);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(559, 245);
            this.dataGridView1.TabIndex = 38;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(115, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.删除ToolStripMenuItem.Text = "删除(&D)";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // n_CopyNumber
            // 
            this.n_CopyNumber.Location = new System.Drawing.Point(422, 39);
            this.n_CopyNumber.Name = "n_CopyNumber";
            this.n_CopyNumber.Size = new System.Drawing.Size(68, 21);
            this.n_CopyNumber.TabIndex = 4;
            // 
            // s_Memo
            // 
            this.s_Memo.Location = new System.Drawing.Point(93, 66);
            this.s_Memo.MaxLength = 50;
            this.s_Memo.Name = "s_Memo";
            this.s_Memo.Size = new System.Drawing.Size(397, 21);
            this.s_Memo.TabIndex = 5;
            // 
            // n_ContainsNumber
            // 
            this.n_ContainsNumber.Location = new System.Drawing.Point(422, 12);
            this.n_ContainsNumber.Name = "n_ContainsNumber";
            this.n_ContainsNumber.Size = new System.Drawing.Size(68, 21);
            this.n_ContainsNumber.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "备注";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(344, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 31;
            this.label4.Text = "平均复印张数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(368, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "配置数量";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(496, 66);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(496, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EditMatRela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 353);
            this.Controls.Add(this.panelMatRela);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "EditMatRela";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机器、耗材(选购件)对照表";
            this.Load += new System.EventHandler(this.EditMatRela_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditMatRela_KeyPress);
            this.panelMatRela.ResumeLayout(false);
            this.panelMatRela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMatRela;
        private System.Windows.Forms.TextBox n_CopyNumber;
        private System.Windows.Forms.ComboBox s_ChildMatID;
        private System.Windows.Forms.ComboBox s_ParentMatID;
        private System.Windows.Forms.TextBox s_Memo;
        private System.Windows.Forms.TextBox n_ContainsNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Pmatnametextbox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox CmatnametextBox;
        private System.Windows.Forms.Button button3;
    }
}