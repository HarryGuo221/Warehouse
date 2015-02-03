namespace Warehouse.Modal
{
    partial class MenuConfigChildForm
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
            this.dataGridViewMenuChid = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMenuChild = new System.Windows.Forms.Panel();
            this.s_SubMenu = new System.Windows.Forms.TextBox();
            this.n_MainMenuId = new System.Windows.Forms.TextBox();
            this.txtSysid = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.s_ShortCut = new System.Windows.Forms.TextBox();
            this.comboBoxReceName = new System.Windows.Forms.ComboBox();
            this.comboBoxMainMenu = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.n_OrderIndex = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenuChid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panelMenuChild.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewMenuChid
            // 
            this.dataGridViewMenuChid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMenuChid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMenuChid.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewMenuChid.Location = new System.Drawing.Point(9, 195);
            this.dataGridViewMenuChid.Name = "dataGridViewMenuChid";
            this.dataGridViewMenuChid.RowTemplate.Height = 23;
            this.dataGridViewMenuChid.Size = new System.Drawing.Size(410, 210);
            this.dataGridViewMenuChid.TabIndex = 9;
            this.dataGridViewMenuChid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMenuChid_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // panelMenuChild
            // 
            this.panelMenuChild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMenuChild.Controls.Add(this.s_SubMenu);
            this.panelMenuChild.Controls.Add(this.n_MainMenuId);
            this.panelMenuChild.Controls.Add(this.txtSysid);
            this.panelMenuChild.Controls.Add(this.label8);
            this.panelMenuChild.Controls.Add(this.label7);
            this.panelMenuChild.Controls.Add(this.label6);
            this.panelMenuChild.Controls.Add(this.s_ShortCut);
            this.panelMenuChild.Controls.Add(this.comboBoxReceName);
            this.panelMenuChild.Controls.Add(this.comboBoxMainMenu);
            this.panelMenuChild.Controls.Add(this.btnClear);
            this.panelMenuChild.Controls.Add(this.btnSave);
            this.panelMenuChild.Controls.Add(this.label5);
            this.panelMenuChild.Controls.Add(this.label4);
            this.panelMenuChild.Controls.Add(this.label1);
            this.panelMenuChild.Controls.Add(this.n_OrderIndex);
            this.panelMenuChild.Controls.Add(this.label3);
            this.panelMenuChild.Controls.Add(this.label2);
            this.panelMenuChild.Location = new System.Drawing.Point(9, 13);
            this.panelMenuChild.Name = "panelMenuChild";
            this.panelMenuChild.Size = new System.Drawing.Size(410, 176);
            this.panelMenuChild.TabIndex = 8;
            // 
            // s_SubMenu
            // 
            this.s_SubMenu.BackColor = System.Drawing.Color.GreenYellow;
            this.s_SubMenu.Location = new System.Drawing.Point(215, 73);
            this.s_SubMenu.Name = "s_SubMenu";
            this.s_SubMenu.Size = new System.Drawing.Size(24, 21);
            this.s_SubMenu.TabIndex = 20;
            this.s_SubMenu.Visible = false;
            // 
            // n_MainMenuId
            // 
            this.n_MainMenuId.BackColor = System.Drawing.Color.GreenYellow;
            this.n_MainMenuId.Location = new System.Drawing.Point(215, 36);
            this.n_MainMenuId.Name = "n_MainMenuId";
            this.n_MainMenuId.Size = new System.Drawing.Size(24, 21);
            this.n_MainMenuId.TabIndex = 19;
            this.n_MainMenuId.Visible = false;
            // 
            // txtSysid
            // 
            this.txtSysid.Location = new System.Drawing.Point(342, 37);
            this.txtSysid.Name = "txtSysid";
            this.txtSysid.ReadOnly = true;
            this.txtSysid.Size = new System.Drawing.Size(53, 21);
            this.txtSysid.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(295, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "编号：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(280, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "子菜单项：";
            // 
            // s_ShortCut
            // 
            this.s_ShortCut.Location = new System.Drawing.Point(104, 108);
            this.s_ShortCut.Name = "s_ShortCut";
            this.s_ShortCut.Size = new System.Drawing.Size(187, 21);
            this.s_ShortCut.TabIndex = 14;
            // 
            // comboBoxReceName
            // 
            this.comboBoxReceName.FormattingEnabled = true;
            this.comboBoxReceName.Location = new System.Drawing.Point(104, 73);
            this.comboBoxReceName.Name = "comboBoxReceName";
            this.comboBoxReceName.Size = new System.Drawing.Size(170, 20);
            this.comboBoxReceName.TabIndex = 13;
            this.comboBoxReceName.SelectedIndexChanged += new System.EventHandler(this.comboBoxReceName_SelectedIndexChanged);
            // 
            // comboBoxMainMenu
            // 
            this.comboBoxMainMenu.FormattingEnabled = true;
            this.comboBoxMainMenu.Location = new System.Drawing.Point(104, 37);
            this.comboBoxMainMenu.Name = "comboBoxMainMenu";
            this.comboBoxMainMenu.Size = new System.Drawing.Size(170, 20);
            this.comboBoxMainMenu.TabIndex = 12;
            this.comboBoxMainMenu.SelectedIndexChanged += new System.EventHandler(this.comboBoxMainMenu_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(320, 142);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "新增";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(320, 107);
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
            this.label5.Location = new System.Drawing.Point(280, 40);
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
            this.label4.Text = "子菜单单据模板设置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "主菜单项：";
            // 
            // n_OrderIndex
            // 
            this.n_OrderIndex.Location = new System.Drawing.Point(104, 144);
            this.n_OrderIndex.Name = "n_OrderIndex";
            this.n_OrderIndex.Size = new System.Drawing.Size(187, 21);
            this.n_OrderIndex.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "顺序号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "快捷键定义：";
            // 
            // MenuConfigChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 417);
            this.Controls.Add(this.dataGridViewMenuChid);
            this.Controls.Add(this.panelMenuChild);
            this.Name = "MenuConfigChildForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "子菜单单据模板设置";
            this.Load += new System.EventHandler(this.MenuConfigChildForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMenuChid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelMenuChild.ResumeLayout(false);
            this.panelMenuChild.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewMenuChid;
        private System.Windows.Forms.Panel panelMenuChild;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox n_OrderIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox s_ShortCut;
        private System.Windows.Forms.ComboBox comboBoxReceName;
        private System.Windows.Forms.ComboBox comboBoxMainMenu;
        private System.Windows.Forms.TextBox txtSysid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.TextBox s_SubMenu;
        private System.Windows.Forms.TextBox n_MainMenuId;
    }
}