namespace VideoCapture
{
    partial class Record
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Video_CNTRL = new System.Windows.Forms.TrackBar();
            this.Time_Label = new System.Windows.Forms.Label();
            this.Frame_lbl = new System.Windows.Forms.Label();
            this.Codec_lbl = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.play_pause_BTN1 = new System.Windows.Forms.Panel();
            this.prgLevel = new System.Windows.Forms.ProgressBar();
            this.txtSpoken = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TeamPerformance = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Video_CNTRL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1228, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordVideoToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // recordVideoToolStripMenuItem
            // 
            this.recordVideoToolStripMenuItem.Name = "recordVideoToolStripMenuItem";
            this.recordVideoToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.recordVideoToolStripMenuItem.Text = "Record Video";
            this.recordVideoToolStripMenuItem.Click += new System.EventHandler(this.recordVideoToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // Video_CNTRL
            // 
            this.Video_CNTRL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Video_CNTRL.Location = new System.Drawing.Point(12, 518);
            this.Video_CNTRL.Name = "Video_CNTRL";
            this.Video_CNTRL.Size = new System.Drawing.Size(750, 45);
            this.Video_CNTRL.TabIndex = 2;
            this.Video_CNTRL.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Video_CNTRL.Scroll += new System.EventHandler(this.Video_CNTRL_Scroll);
            // 
            // Time_Label
            // 
            this.Time_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Time_Label.AutoSize = true;
            this.Time_Label.Location = new System.Drawing.Point(695, 546);
            this.Time_Label.Name = "Time_Label";
            this.Time_Label.Size = new System.Drawing.Size(33, 13);
            this.Time_Label.TabIndex = 4;
            this.Time_Label.Text = "Time:";
            // 
            // Frame_lbl
            // 
            this.Frame_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Frame_lbl.AutoSize = true;
            this.Frame_lbl.Location = new System.Drawing.Point(695, 566);
            this.Frame_lbl.Name = "Frame_lbl";
            this.Frame_lbl.Size = new System.Drawing.Size(39, 13);
            this.Frame_lbl.TabIndex = 5;
            this.Frame_lbl.Text = "Frame:";
            // 
            // Codec_lbl
            // 
            this.Codec_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Codec_lbl.AutoSize = true;
            this.Codec_lbl.Location = new System.Drawing.Point(695, 583);
            this.Codec_lbl.Name = "Codec_lbl";
            this.Codec_lbl.Size = new System.Drawing.Size(41, 13);
            this.Codec_lbl.TabIndex = 6;
            this.Codec_lbl.Text = "Codec:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(26, 32);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(726, 480);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // play_pause_BTN1
            // 
            this.play_pause_BTN1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.play_pause_BTN1.BackgroundImage = global::VideoCapture.Properties.Resources.Play;
            this.play_pause_BTN1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.play_pause_BTN1.Location = new System.Drawing.Point(375, 546);
            this.play_pause_BTN1.Name = "play_pause_BTN1";
            this.play_pause_BTN1.Size = new System.Drawing.Size(50, 50);
            this.play_pause_BTN1.TabIndex = 3;
            this.play_pause_BTN1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.play_pause_BTN1_MouseUp);
            // 
            // prgLevel
            // 
            this.prgLevel.Location = new System.Drawing.Point(759, 32);
            this.prgLevel.Name = "prgLevel";
            this.prgLevel.Size = new System.Drawing.Size(457, 23);
            this.prgLevel.TabIndex = 10;
            this.prgLevel.Click += new System.EventHandler(this.prgLevel_Click);
            // 
            // txtSpoken
            // 
            this.txtSpoken.Location = new System.Drawing.Point(759, 62);
            this.txtSpoken.Multiline = true;
            this.txtSpoken.Name = "txtSpoken";
            this.txtSpoken.ReadOnly = true;
            this.txtSpoken.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSpoken.Size = new System.Drawing.Size(457, 162);
            this.txtSpoken.TabIndex = 11;
            this.txtSpoken.TextChanged += new System.EventHandler(this.txtSpoken_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Location = new System.Drawing.Point(759, 231);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(457, 26);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "Team Performance";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TeamPerformance
            // 
            this.TeamPerformance.Location = new System.Drawing.Point(759, 264);
            this.TeamPerformance.Multiline = true;
            this.TeamPerformance.Name = "TeamPerformance";
            this.TeamPerformance.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TeamPerformance.Size = new System.Drawing.Size(457, 248);
            this.TeamPerformance.TabIndex = 13;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 616);
            this.Controls.Add(this.TeamPerformance);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtSpoken);
            this.Controls.Add(this.prgLevel);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.Codec_lbl);
            this.Controls.Add(this.Frame_lbl);
            this.Controls.Add(this.Time_Label);
            this.Controls.Add(this.play_pause_BTN1);
            this.Controls.Add(this.Video_CNTRL);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Record";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Record_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Video_CNTRL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TrackBar Video_CNTRL;
        private System.Windows.Forms.Panel play_pause_BTN1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label Time_Label;
        private System.Windows.Forms.Label Frame_lbl;
        private System.Windows.Forms.Label Codec_lbl;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ProgressBar prgLevel;
        private System.Windows.Forms.TextBox txtSpoken;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox TeamPerformance;
        private System.Windows.Forms.Timer timer1;
    }
}