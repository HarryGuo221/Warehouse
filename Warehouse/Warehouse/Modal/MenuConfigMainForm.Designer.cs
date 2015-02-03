namespace Warehouse.Modal
{
    partial class MenuConfigMainForm
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
            this.s_MainMenu = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.s_ShortCut = new System.Windows.Forms.TextBox();
            this.n_OrderIndex = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelMenuMain = new System.Windows.Forms.Panel();
            this.n_Sysid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewMenuMain = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMenuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenuMain)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "主菜单项：";
            // 
            // s_MainMenu
            // 
            this.s_MainMenu.Location = new System.Drawing.Point(105, 53);
            this.s_MainMenu.Name = "s_MainMenu";
            this.s_MainMenu.Size = new System.Drawing.Size(154, 21);
            this.s_MainMenu.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "快捷键定义：";
            // 
            // s_ShortCut
            // 
            this.s_ShortCut.Location = new System.Drawing.Point(105, 89);
            this.s_ShortCut.Name = "s_ShortCut";
            this.s_ShortCut.Size = new System.Drawing.Size(154, 21);
            this.s_ShortCut.TabIndex = 3;
            // 
            // n_OrderIndex
            // 
            this.n_OrderIndex.Location = new System.Drawing.Point(105, 124);
            this.n_OrderIndex.Name = "n_OrderIndex";
            this.n_OrderIndex.Size = new System.Drawing.Size(154, 21);
            this.n_OrderIndex.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "主顺序号：";
            // 
            // panelMenuMain
            // 
            this.panelMenuMain.Controls.Add(this.n_Sysid);
            this.panelMenuMain.Controls.Add(this.label6);
            this.panelMenuMain.Controls.Add(this.btnClear);
            this.panelMenuMain.Controls.Add(this.btnSave);
            this.panelMenuMain.Controls.Add(this.label5);
            this.panelMenuMain.Controls.Add(this.label4);
            this.panelMenuMain.Controls.Add(this.label1);
            this.panelMenuMain.Controls.Add(this.n_OrderIndex);
            this.panelMenuMain.Controls.Add(this.s_MainMenu);
            this.panelMenuMain.Controls.Add(this.label3);
            this.panelMenuMain.Controls.Add(this.label2);
            this.panelMenuMain.Controls.Add(this.s_ShortCut);
            this.panelMenuMain.Location = new System.Drawing.Point(12, 12);
            this.panelMenuMain.Name = "panelMenuMain";
            this.panelMenuMain.Size = new System.Drawing.Size(381, 154);
            this.panelMenuMain.TabIndex = 6;
            // 
            // n_Sysid
            // 
            this.n_Sysid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.n_Sysid.Location = new System.Drawing.Point(105, 28);
            this.n_Sysid.Name = "n_Sysid";
            this.n_Sysid.ReadOnly = true;
            this.n_Sysid.Size = new System.Drawing.Size(53, 14);
            this.n_Sysid.TabIndex = 11;
            this.n_Sysid.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "编号：";
            this.label6.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(292, 122);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "新增";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(292, 87);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(265, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14F);
            this.label4.Location = new System.Drawing.Point(79, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "主菜单单据模板设置";
            // 
            // dataGridViewMenuMain
            // 
            this.dataGridViewMenuMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMenuMain.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewMenuMain.Location = new System.Drawing.Point(12, 172);
            this.dataGridViewMenuMain.Name = "dataGridViewMenuMain";
            this.dataGridViewMenuMain.RowTemplate.Height = 23;
            this.dataGridViewMenuMain.Size = new System.Drawing.Size(381, 130);
            this.dataGridViewMenuMain.TabIndex = 7;
            this.dataGridViewMenuMain.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMenuMain_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // MenuConfigMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 314);
            this.Controls.Add(this.dataGridViewMenuMain);
            this.Controls.Add(this.panelMenuMain);
            this.Name = "MenuConfigMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "主菜单单据模板设置";
            this.Load += new System.EventHandler(this.MenuConfigMainForm_Load);
            this.panelMenuMain.ResumeLayout(false);
            this.panelMenuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenuMain)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox s_MainMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox s_ShortCut;
        private System.Windows.Forms.TextBox n_OrderIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelMenuMain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewMenuMain;
        private System.Windows.Forms.TextBox n_Sysid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
    }
}