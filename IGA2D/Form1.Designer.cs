namespace IGA2D
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.InputBox = new System.Windows.Forms.TextBox();
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.优化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ScaleBox = new System.Windows.Forms.TextBox();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SigmaX = new System.Windows.Forms.RadioButton();
            this.SigmaY = new System.Windows.Forms.RadioButton();
            this.TauXY = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Disp = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputBox
            // 
            this.InputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InputBox.Location = new System.Drawing.Point(6, 15);
            this.InputBox.Multiline = true;
            this.InputBox.Name = "InputBox";
            this.InputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InputBox.Size = new System.Drawing.Size(294, 243);
            this.InputBox.TabIndex = 4;
            this.InputBox.WordWrap = false;
            // 
            // OutputBox
            // 
            this.OutputBox.BackColor = System.Drawing.SystemColors.Window;
            this.OutputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OutputBox.Location = new System.Drawing.Point(6, 15);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputBox.Size = new System.Drawing.Size(294, 229);
            this.OutputBox.TabIndex = 5;
            this.OutputBox.WordWrap = false;
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.toolStripSeparator2,
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.toolStripSeparator1,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.ShortcutKeyDisplayString = " ";
            this.新建ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(121, 6);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.ShortcutKeyDisplayString = " ";
            this.打开ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.ShortcutKeyDisplayString = " ";
            this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.ShortcutKeyDisplayString = " ";
            this.另存为ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.另存为ToolStripMenuItem.Text = "另存为";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.ShortcutKeyDisplayString = " ";
            this.退出ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 计算ToolStripMenuItem
            // 
            this.计算ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.计算ToolStripMenuItem1,
            this.优化ToolStripMenuItem,
            this.toolStripSeparator3,
            this.导出ToolStripMenuItem});
            this.计算ToolStripMenuItem.Name = "计算ToolStripMenuItem";
            this.计算ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.计算ToolStripMenuItem.Text = "分析";
            // 
            // 计算ToolStripMenuItem1
            // 
            this.计算ToolStripMenuItem1.Name = "计算ToolStripMenuItem1";
            this.计算ToolStripMenuItem1.ShortcutKeyDisplayString = " ";
            this.计算ToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.计算ToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.计算ToolStripMenuItem1.Text = "计算";
            this.计算ToolStripMenuItem1.Click += new System.EventHandler(this.计算ToolStripMenuItem1_Click);
            // 
            // 优化ToolStripMenuItem
            // 
            this.优化ToolStripMenuItem.Name = "优化ToolStripMenuItem";
            this.优化ToolStripMenuItem.ShortcutKeyDisplayString = " ";
            this.优化ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.优化ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.优化ToolStripMenuItem.Text = "优化";
            this.优化ToolStripMenuItem.Click += new System.EventHandler(this.优化ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(109, 6);
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.ShortcutKeyDisplayString = " ";
            this.导出ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.导出ToolStripMenuItem.Text = "导出";
            this.导出ToolStripMenuItem.Click += new System.EventHandler(this.导出ToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.计算ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(816, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.InputBox);
            this.groupBox1.Location = new System.Drawing.Point(508, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 264);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.OutputBox);
            this.groupBox2.Location = new System.Drawing.Point(508, 299);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(306, 250);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出";
            // 
            // ScaleBox
            // 
            this.ScaleBox.Location = new System.Drawing.Point(430, 18);
            this.ScaleBox.Name = "ScaleBox";
            this.ScaleBox.Size = new System.Drawing.Size(65, 21);
            this.ScaleBox.TabIndex = 1;
            this.ScaleBox.Text = "100";
            // 
            // Picture
            // 
            this.Picture.Location = new System.Drawing.Point(2, 18);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(500, 500);
            this.Picture.TabIndex = 0;
            this.Picture.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(402, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "比例";
            // 
            // SigmaX
            // 
            this.SigmaX.AutoSize = true;
            this.SigmaX.Location = new System.Drawing.Point(258, 19);
            this.SigmaX.Name = "SigmaX";
            this.SigmaX.Size = new System.Drawing.Size(41, 16);
            this.SigmaX.TabIndex = 3;
            this.SigmaX.Text = "σx";
            this.SigmaX.UseVisualStyleBackColor = true;
            this.SigmaX.CheckedChanged += new System.EventHandler(this.SigmaX_CheckedChanged);
            // 
            // SigmaY
            // 
            this.SigmaY.AutoSize = true;
            this.SigmaY.Location = new System.Drawing.Point(305, 19);
            this.SigmaY.Name = "SigmaY";
            this.SigmaY.Size = new System.Drawing.Size(41, 16);
            this.SigmaY.TabIndex = 4;
            this.SigmaY.Text = "σy";
            this.SigmaY.UseVisualStyleBackColor = true;
            this.SigmaY.CheckedChanged += new System.EventHandler(this.SigmaY_CheckedChanged);
            // 
            // TauXY
            // 
            this.TauXY.AutoSize = true;
            this.TauXY.Location = new System.Drawing.Point(352, 19);
            this.TauXY.Name = "TauXY";
            this.TauXY.Size = new System.Drawing.Size(47, 16);
            this.TauXY.TabIndex = 5;
            this.TauXY.Text = "τxy";
            this.TauXY.UseVisualStyleBackColor = true;
            this.TauXY.CheckedChanged += new System.EventHandler(this.TauXY_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Disp);
            this.groupBox3.Controls.Add(this.SigmaX);
            this.groupBox3.Controls.Add(this.TauXY);
            this.groupBox3.Controls.Add(this.ScaleBox);
            this.groupBox3.Controls.Add(this.SigmaY);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.Picture);
            this.groupBox3.Location = new System.Drawing.Point(3, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(504, 522);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图像";
            // 
            // Disp
            // 
            this.Disp.AutoSize = true;
            this.Disp.Checked = true;
            this.Disp.Location = new System.Drawing.Point(205, 19);
            this.Disp.Name = "Disp";
            this.Disp.Size = new System.Drawing.Size(47, 16);
            this.Disp.TabIndex = 6;
            this.Disp.TabStop = true;
            this.Disp.Text = "Disp";
            this.Disp.UseVisualStyleBackColor = true;
            this.Disp.CheckedChanged += new System.EventHandler(this.Disp_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 552);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(832, 591);
            this.MinimumSize = new System.Drawing.Size(832, 591);
            this.Name = "Form1";
            this.Text = "IGA2D";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 优化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox Picture;
        private System.Windows.Forms.TextBox ScaleBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton TauXY;
        private System.Windows.Forms.RadioButton SigmaY;
        private System.Windows.Forms.RadioButton SigmaX;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton Disp;
    }
}

