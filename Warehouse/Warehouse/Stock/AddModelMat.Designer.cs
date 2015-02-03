namespace Warehouse.Stock
{
    partial class AddModelMat
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.t_NatName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.n_CopyCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.n_refPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.s_MatId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.s_ModelName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.s_stype = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.s_stype);
            this.panel1.Controls.Add(this.t_NatName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.n_CopyCount);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.n_refPrice);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.s_MatId);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.s_ModelName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 203);
            this.panel1.TabIndex = 0;
            // 
            // t_NatName
            // 
            this.t_NatName.Location = new System.Drawing.Point(96, 63);
            this.t_NatName.Name = "t_NatName";
            this.t_NatName.ReadOnly = true;
            this.t_NatName.Size = new System.Drawing.Size(170, 21);
            this.t_NatName.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "物料名称";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(96, 171);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(170, 21);
            this.textBox6.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "备注";
            // 
            // n_CopyCount
            // 
            this.n_CopyCount.Location = new System.Drawing.Point(96, 144);
            this.n_CopyCount.Name = "n_CopyCount";
            this.n_CopyCount.Size = new System.Drawing.Size(170, 21);
            this.n_CopyCount.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "参考报价";
            // 
            // n_refPrice
            // 
            this.n_refPrice.Location = new System.Drawing.Point(96, 117);
            this.n_refPrice.Name = "n_refPrice";
            this.n_refPrice.Size = new System.Drawing.Size(170, 21);
            this.n_refPrice.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "平均复印张数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "类型";
            // 
            // s_MatId
            // 
            this.s_MatId.Location = new System.Drawing.Point(96, 36);
            this.s_MatId.Name = "s_MatId";
            this.s_MatId.Size = new System.Drawing.Size(170, 21);
            this.s_MatId.TabIndex = 3;
            this.s_MatId.Leave += new System.EventHandler(this.s_MatId_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "物料";
            // 
            // s_ModelName
            // 
            this.s_ModelName.Location = new System.Drawing.Point(96, 9);
            this.s_ModelName.Name = "s_ModelName";
            this.s_ModelName.ReadOnly = true;
            this.s_ModelName.Size = new System.Drawing.Size(170, 21);
            this.s_ModelName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "机型";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(174, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // s_stype
            // 
            this.s_stype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.s_stype.FormattingEnabled = true;
            this.s_stype.Items.AddRange(new object[] {
            "--请选择--",
            "耗材",
            "选购件"});
            this.s_stype.Location = new System.Drawing.Point(96, 90);
            this.s_stype.Name = "s_stype";
            this.s_stype.Size = new System.Drawing.Size(170, 20);
            this.s_stype.TabIndex = 14;
            // 
            // AddModelMat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 243);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "AddModelMat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "耗材及选构件";
            this.Load += new System.EventHandler(this.AddModelMat_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox n_CopyCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox n_refPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox s_MatId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox s_ModelName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox t_NatName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox s_stype;
    }
}