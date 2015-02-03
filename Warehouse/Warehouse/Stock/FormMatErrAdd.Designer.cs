namespace Warehouse.Stock
{
    partial class FormMatErrAdd
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.s_memo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.S_errorCode = new System.Windows.Forms.TextBox();
            this.S_errorApperance = new System.Windows.Forms.TextBox();
            this.s_ErrorPlace = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.s_ErrorName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(241, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.s_ErrorName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.s_memo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.S_errorCode);
            this.panel1.Controls.Add(this.S_errorApperance);
            this.panel1.Controls.Add(this.s_ErrorPlace);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(457, 250);
            this.panel1.TabIndex = 10;
            // 
            // s_memo
            // 
            this.s_memo.Location = new System.Drawing.Point(105, 152);
            this.s_memo.Multiline = true;
            this.s_memo.Name = "s_memo";
            this.s_memo.Size = new System.Drawing.Size(307, 79);
            this.s_memo.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "故障现象";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "故障代码";
            // 
            // S_errorCode
            // 
            this.S_errorCode.Location = new System.Drawing.Point(104, 45);
            this.S_errorCode.Name = "S_errorCode";
            this.S_errorCode.Size = new System.Drawing.Size(308, 21);
            this.S_errorCode.TabIndex = 1;
            // 
            // S_errorApperance
            // 
            this.S_errorApperance.Location = new System.Drawing.Point(105, 100);
            this.S_errorApperance.Multiline = true;
            this.S_errorApperance.Name = "S_errorApperance";
            this.S_errorApperance.Size = new System.Drawing.Size(307, 45);
            this.S_errorApperance.TabIndex = 3;
            // 
            // s_ErrorPlace
            // 
            this.s_ErrorPlace.FormattingEnabled = true;
            this.s_ErrorPlace.Location = new System.Drawing.Point(105, 73);
            this.s_ErrorPlace.Name = "s_ErrorPlace";
            this.s_ErrorPlace.Size = new System.Drawing.Size(308, 20);
            this.s_ErrorPlace.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "故障现象";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "故障部位";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "故障定义";
            // 
            // s_ErrorName
            // 
            this.s_ErrorName.Location = new System.Drawing.Point(104, 17);
            this.s_ErrorName.Name = "s_ErrorName";
            this.s_ErrorName.Size = new System.Drawing.Size(308, 21);
            this.s_ErrorName.TabIndex = 24;
            // 
            // FormMatErrAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 291);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMatErrAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "故障信息";
            this.Load += new System.EventHandler(this.FormMatErrAdd_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormMatErrAdd_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox S_errorCode;
        private System.Windows.Forms.TextBox S_errorApperance;
        private System.Windows.Forms.ComboBox s_ErrorPlace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox s_memo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox s_ErrorName;
        private System.Windows.Forms.Label label2;
    }
}