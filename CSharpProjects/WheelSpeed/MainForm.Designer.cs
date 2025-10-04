namespace WheelSpeed
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.operateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRunStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboBoxDeltaRpm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFreq = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownSpokes = new WheelSpeed.NumericUpDownEx();
            this.checkBoxMarked = new System.Windows.Forms.CheckBox();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownRpm = new WheelSpeed.NumericUpDownEx();
            this.pictureBoxRGB = new System.Windows.Forms.PictureBox();
            this.timerRun = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpokes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRpm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRGB)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operateToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(643, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // operateToolStripMenuItem
            // 
            this.operateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runStopToolStripMenuItem});
            this.operateToolStripMenuItem.Name = "operateToolStripMenuItem";
            this.operateToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.operateToolStripMenuItem.Text = "操作";
            // 
            // runStopToolStripMenuItem
            // 
            this.runStopToolStripMenuItem.Name = "runStopToolStripMenuItem";
            this.runStopToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.runStopToolStripMenuItem.Text = "转/停车轮";
            this.runStopToolStripMenuItem.ToolTipText = "点击转动车轮";
            this.runStopToolStripMenuItem.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.helpToolStripMenuItem.Text = "帮助";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.aboutToolStripMenuItem.Text = "关于";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRunStop,
            this.toolStripButtonHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(643, 38);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRunStop
            // 
            this.toolStripButtonRunStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRunStop.Image = global::WheelSpeed.Properties.Resources.RunWheel;
            this.toolStripButtonRunStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRunStop.Name = "toolStripButtonRunStop";
            this.toolStripButtonRunStop.Size = new System.Drawing.Size(36, 35);
            this.toolStripButtonRunStop.Text = "转/停车轮";
            this.toolStripButtonRunStop.ToolTipText = "点击转动车轮";
            this.toolStripButtonRunStop.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // toolStripButtonHelp
            // 
            this.toolStripButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHelp.Image = global::WheelSpeed.Properties.Resources.HelpFilled;
            this.toolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.Size = new System.Drawing.Size(36, 35);
            this.toolStripButtonHelp.Text = "帮助";
            this.toolStripButtonHelp.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 437);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(643, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusMessage
            // 
            this.toolStripStatusMessage.Name = "toolStripStatusMessage";
            this.toolStripStatusMessage.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusMessage.Text = "车轮停转中";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 63);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxDeltaRpm);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxFreq);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownSpokes);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxMarked);
            this.splitContainer1.Panel1.Controls.Add(this.buttonDraw);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownRpm);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxRGB);
            this.splitContainer1.Size = new System.Drawing.Size(643, 374);
            this.splitContainer1.SplitterDistance = 164;
            this.splitContainer1.TabIndex = 3;
            // 
            // comboBoxDeltaRpm
            // 
            this.comboBoxDeltaRpm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDeltaRpm.FormattingEnabled = true;
            this.comboBoxDeltaRpm.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "12",
            "15",
            "20",
            "25",
            "30",
            "40",
            "50",
            "60",
            "80",
            "100"});
            this.comboBoxDeltaRpm.Location = new System.Drawing.Point(36, 40);
            this.comboBoxDeltaRpm.Name = "comboBoxDeltaRpm";
            this.comboBoxDeltaRpm.Size = new System.Drawing.Size(98, 20);
            this.comboBoxDeltaRpm.TabIndex = 11;
            this.comboBoxDeltaRpm.SelectedIndexChanged += new System.EventHandler(this.comboBoxDeltaRpm_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "转速增量(Rpm)";
            // 
            // comboBoxFreq
            // 
            this.comboBoxFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFreq.FormattingEnabled = true;
            this.comboBoxFreq.Items.AddRange(new object[] {
            "1",
            "2",
            "2.5",
            "4",
            "5",
            "8",
            "10",
            "12.5",
            "20",
            "25",
            "40",
            "50",
            "100"});
            this.comboBoxFreq.Location = new System.Drawing.Point(31, 228);
            this.comboBoxFreq.Name = "comboBoxFreq";
            this.comboBoxFreq.Size = new System.Drawing.Size(98, 20);
            this.comboBoxFreq.TabIndex = 9;
            this.comboBoxFreq.SelectedIndexChanged += new System.EventHandler(this.comboBoxFreq_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "采样频率(Hz)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "辐条数（3-12）：";
            // 
            // numericUpDownSpokes
            // 
            this.numericUpDownSpokes.Location = new System.Drawing.Point(34, 167);
            this.numericUpDownSpokes.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDownSpokes.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownSpokes.Name = "numericUpDownSpokes";
            this.numericUpDownSpokes.Size = new System.Drawing.Size(98, 21);
            this.numericUpDownSpokes.TabIndex = 6;
            this.numericUpDownSpokes.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownSpokes.WheelIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSpokes.ValueChanged += new System.EventHandler(this.numericUpDownSpokes_ValueChanged);
            // 
            // checkBoxMarked
            // 
            this.checkBoxMarked.AutoSize = true;
            this.checkBoxMarked.Location = new System.Drawing.Point(31, 273);
            this.checkBoxMarked.Name = "checkBoxMarked";
            this.checkBoxMarked.Size = new System.Drawing.Size(72, 16);
            this.checkBoxMarked.TabIndex = 5;
            this.checkBoxMarked.Text = "颜色标记";
            this.checkBoxMarked.UseVisualStyleBackColor = true;
            this.checkBoxMarked.CheckedChanged += new System.EventHandler(this.checkBoxMarked_CheckedChanged);
            // 
            // buttonDraw
            // 
            this.buttonDraw.Location = new System.Drawing.Point(28, 307);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(75, 23);
            this.buttonDraw.TabIndex = 4;
            this.buttonDraw.Text = "转/停车轮";
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "转速（RPM）：";
            // 
            // numericUpDownRpm
            // 
            this.numericUpDownRpm.Location = new System.Drawing.Point(36, 104);
            this.numericUpDownRpm.Maximum = new decimal(new int[] {
            4500,
            0,
            0,
            0});
            this.numericUpDownRpm.Minimum = new decimal(new int[] {
            4500,
            0,
            0,
            -2147483648});
            this.numericUpDownRpm.Name = "numericUpDownRpm";
            this.numericUpDownRpm.Size = new System.Drawing.Size(98, 21);
            this.numericUpDownRpm.TabIndex = 2;
            this.numericUpDownRpm.WheelIncrement = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownRpm.ValueChanged += new System.EventHandler(this.numericUpDownRpm_ValueChanged);
            // 
            // pictureBoxRGB
            // 
            this.pictureBoxRGB.BackColor = System.Drawing.Color.White;
            this.pictureBoxRGB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxRGB.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxRGB.Name = "pictureBoxRGB";
            this.pictureBoxRGB.Size = new System.Drawing.Size(475, 374);
            this.pictureBoxRGB.TabIndex = 0;
            this.pictureBoxRGB.TabStop = false;
            this.pictureBoxRGB.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxRGB_Paint);
            // 
            // timerRun
            // 
            this.timerRun.Interval = 40;
            this.timerRun.Tick += new System.EventHandler(this.timerRun_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 459);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "车轮旋转视觉效果";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpokes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRpm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRGB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRunStop;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMessage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBoxRGB;
        private System.Windows.Forms.Label label2;
        private NumericUpDownEx numericUpDownRpm;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Timer timerRun;
        private System.Windows.Forms.CheckBox checkBoxMarked;
        private System.Windows.Forms.Label label3;
        private NumericUpDownEx numericUpDownSpokes;
        private System.Windows.Forms.ComboBox comboBoxFreq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.ToolStripMenuItem operateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxDeltaRpm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem runStopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

