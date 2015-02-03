namespace Warehouse.Sys
{
    partial class UsersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersForm));
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加人员ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看人员信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除人员信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.角色管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下级人员管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAssignRole = new System.Windows.Forms.Button();
            this.btnAssignChildUser = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRoleManage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonChildUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.AllowUserToAddRows = false;
            this.dataGridViewUsers.AllowUserToDeleteRows = false;
            this.dataGridViewUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewUsers.Location = new System.Drawing.Point(0, 35);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.ReadOnly = true;
            this.dataGridViewUsers.RowTemplate.Height = 23;
            this.dataGridViewUsers.Size = new System.Drawing.Size(800, 398);
            this.dataGridViewUsers.TabIndex = 1;
            this.dataGridViewUsers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUsers_CellDoubleClick);
            this.dataGridViewUsers.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewUsers_RowPostPaint);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加人员ToolStripMenuItem,
            this.查看人员信息ToolStripMenuItem,
            this.删除人员信息ToolStripMenuItem,
            this.toolStripSeparator1,
            this.角色管理ToolStripMenuItem,
            this.下级人员管理ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 120);
            // 
            // 添加人员ToolStripMenuItem
            // 
            this.添加人员ToolStripMenuItem.Name = "添加人员ToolStripMenuItem";
            this.添加人员ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.添加人员ToolStripMenuItem.Text = "添加人员信息";
            this.添加人员ToolStripMenuItem.Click += new System.EventHandler(this.添加人员ToolStripMenuItem_Click);
            // 
            // 查看人员信息ToolStripMenuItem
            // 
            this.查看人员信息ToolStripMenuItem.Name = "查看人员信息ToolStripMenuItem";
            this.查看人员信息ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.查看人员信息ToolStripMenuItem.Text = "查看人员信息";
            this.查看人员信息ToolStripMenuItem.Click += new System.EventHandler(this.查看人员信息ToolStripMenuItem_Click);
            // 
            // 删除人员信息ToolStripMenuItem
            // 
            this.删除人员信息ToolStripMenuItem.Name = "删除人员信息ToolStripMenuItem";
            this.删除人员信息ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.删除人员信息ToolStripMenuItem.Text = "删除人员信息";
            this.删除人员信息ToolStripMenuItem.Click += new System.EventHandler(this.删除人员信息ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // 角色管理ToolStripMenuItem
            // 
            this.角色管理ToolStripMenuItem.Name = "角色管理ToolStripMenuItem";
            this.角色管理ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.角色管理ToolStripMenuItem.Text = "角色管理";
            this.角色管理ToolStripMenuItem.Click += new System.EventHandler(this.角色管理ToolStripMenuItem_Click);
            // 
            // 下级人员管理ToolStripMenuItem
            // 
            this.下级人员管理ToolStripMenuItem.Name = "下级人员管理ToolStripMenuItem";
            this.下级人员管理ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.下级人员管理ToolStripMenuItem.Text = "下级人员管理";
            this.下级人员管理ToolStripMenuItem.Click += new System.EventHandler(this.下级人员管理ToolStripMenuItem_Click);
            // 
            // btnAssignRole
            // 
            this.btnAssignRole.Location = new System.Drawing.Point(246, 0);
            this.btnAssignRole.Name = "btnAssignRole";
            this.btnAssignRole.Size = new System.Drawing.Size(75, 23);
            this.btnAssignRole.TabIndex = 2;
            this.btnAssignRole.Text = "角色管理";
            this.btnAssignRole.UseVisualStyleBackColor = true;
            // 
            // btnAssignChildUser
            // 
            this.btnAssignChildUser.Location = new System.Drawing.Point(327, 0);
            this.btnAssignChildUser.Name = "btnAssignChildUser";
            this.btnAssignChildUser.Size = new System.Drawing.Size(100, 23);
            this.btnAssignChildUser.TabIndex = 4;
            this.btnAssignChildUser.Text = "下级人员管理";
            this.btnAssignChildUser.UseVisualStyleBackColor = true;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(3, 0);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 7;
            this.btnAddUser.Text = "添加人员";
            this.btnAddUser.UseVisualStyleBackColor = true;
            // 
            // btnEditUser
            // 
            this.btnEditUser.Location = new System.Drawing.Point(84, 0);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(75, 23);
            this.btnEditUser.TabIndex = 8;
            this.btnEditUser.Text = "修改人员";
            this.btnEditUser.UseVisualStyleBackColor = true;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(165, 0);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteUser.TabIndex = 9;
            this.btnDeleteUser.Text = "删除人员";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(433, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonEdit,
            this.toolStripButtonDelete,
            this.toolStripButtonRoleManage,
            this.toolStripButtonChildUsers,
            this.toolStripButtonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 40);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(60, 37);
            this.toolStripButtonAdd.Text = "增加人员";
            this.toolStripButtonAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEdit.Image")));
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(84, 37);
            this.toolStripButtonEdit.Text = "修改人员信息";
            this.toolStripButtonEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonEdit.Click += new System.EventHandler(this.toolStripButtonEdit_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(60, 37);
            this.toolStripButtonDelete.Text = "删除人员";
            this.toolStripButtonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripButtonRoleManage
            // 
            this.toolStripButtonRoleManage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRoleManage.Image")));
            this.toolStripButtonRoleManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRoleManage.Name = "toolStripButtonRoleManage";
            this.toolStripButtonRoleManage.Size = new System.Drawing.Size(60, 37);
            this.toolStripButtonRoleManage.Text = "角色管理";
            this.toolStripButtonRoleManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonRoleManage.Click += new System.EventHandler(this.toolStripButtonRoleManage_Click);
            // 
            // toolStripButtonChildUsers
            // 
            this.toolStripButtonChildUsers.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonChildUsers.Image")));
            this.toolStripButtonChildUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChildUsers.Name = "toolStripButtonChildUsers";
            this.toolStripButtonChildUsers.Size = new System.Drawing.Size(84, 37);
            this.toolStripButtonChildUsers.Text = "下级人员管理";
            this.toolStripButtonChildUsers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonChildUsers.Visible = false;
            this.toolStripButtonChildUsers.Click += new System.EventHandler(this.toolStripButtonChildUsers_Click);
            // 
            // toolStripButtonClose
            // 
            this.toolStripButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClose.Image")));
            this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClose.Name = "toolStripButtonClose";
            this.toolStripButtonClose.Size = new System.Drawing.Size(36, 37);
            this.toolStripButtonClose.Text = "退出";
            this.toolStripButtonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 433);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnAssignChildUser);
            this.Controls.Add(this.btnAssignRole);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dataGridViewUsers);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.btnEditUser);
            this.Controls.Add(this.btnAddUser);
            this.Name = "UsersForm";
            this.Text = "操作员列表";
            this.Load += new System.EventHandler(this.UsersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Button btnAssignRole;
        private System.Windows.Forms.Button btnAssignChildUser;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看人员信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除人员信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 添加人员ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 角色管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下级人员管理ToolStripMenuItem;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonRoleManage;
        private System.Windows.Forms.ToolStripButton toolStripButtonChildUsers;
        private System.Windows.Forms.ToolStripButton toolStripButtonClose;
    }
}