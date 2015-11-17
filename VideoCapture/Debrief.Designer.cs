namespace VideoCapture
{
    partial class Debrief
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Video_CNTRL = new System.Windows.Forms.TrackBar();
            this.Time_Label = new System.Windows.Forms.Label();
            this.Frame_lbl = new System.Windows.Forms.Label();
            this.Codec_lbl = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttontext = new System.Windows.Forms.Button();
            this.play_pause_BTN = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Video_CNTRL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openVideoToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openVideoToolStripMenuItem
            // 
            this.openVideoToolStripMenuItem.Name = "openVideoToolStripMenuItem";
            this.openVideoToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.openVideoToolStripMenuItem.Text = "Open Video";
            this.openVideoToolStripMenuItem.Click += new System.EventHandler(this.openVideoToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // Video_CNTRL
            // 
            this.Video_CNTRL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Video_CNTRL.Location = new System.Drawing.Point(12, 568);
            this.Video_CNTRL.Name = "Video_CNTRL";
            this.Video_CNTRL.Size = new System.Drawing.Size(776, 45);
            this.Video_CNTRL.TabIndex = 2;
            this.Video_CNTRL.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Video_CNTRL.MouseCaptureChanged += new System.EventHandler(this.Video_CNTRL_MouseCaptureChanged);
            // 
            // Time_Label
            // 
            this.Time_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Time_Label.AutoSize = true;
            this.Time_Label.Location = new System.Drawing.Point(719, 591);
            this.Time_Label.Name = "Time_Label";
            this.Time_Label.Size = new System.Drawing.Size(33, 13);
            this.Time_Label.TabIndex = 4;
            this.Time_Label.Text = "Time:";
            // 
            // Frame_lbl
            // 
            this.Frame_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Frame_lbl.AutoSize = true;
            this.Frame_lbl.Location = new System.Drawing.Point(719, 614);
            this.Frame_lbl.Name = "Frame_lbl";
            this.Frame_lbl.Size = new System.Drawing.Size(39, 13);
            this.Frame_lbl.TabIndex = 5;
            this.Frame_lbl.Text = "Frame:";
            // 
            // Codec_lbl
            // 
            this.Codec_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Codec_lbl.AutoSize = true;
            this.Codec_lbl.Location = new System.Drawing.Point(717, 631);
            this.Codec_lbl.Name = "Codec_lbl";
            this.Codec_lbl.Size = new System.Drawing.Size(41, 13);
            this.Codec_lbl.TabIndex = 6;
            this.Codec_lbl.Text = "Codec:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(909, 33);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(351, 299);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // buttontext
            // 
            this.buttontext.Location = new System.Drawing.Point(909, 609);
            this.buttontext.Name = "buttontext";
            this.buttontext.Size = new System.Drawing.Size(94, 23);
            this.buttontext.TabIndex = 8;
            this.buttontext.Text = "Select Text File";
            this.buttontext.UseVisualStyleBackColor = true;
            this.buttontext.Click += new System.EventHandler(this.buttontext_Click_1);
            // 
            // play_pause_BTN
            // 
            this.play_pause_BTN.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.play_pause_BTN.BackgroundImage = global::VideoCapture.Properties.Resources.Play;
            this.play_pause_BTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.play_pause_BTN.Location = new System.Drawing.Point(384, 603);
            this.play_pause_BTN.Name = "play_pause_BTN";
            this.play_pause_BTN.Size = new System.Drawing.Size(50, 50);
            this.play_pause_BTN.TabIndex = 3;
            this.play_pause_BTN.Paint += new System.Windows.Forms.PaintEventHandler(this.play_pause_BTN_Paint);
            this.play_pause_BTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.play_pause_BTN_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(794, 529);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(909, 360);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(347, 219);
            this.richTextBox2.TabIndex = 9;
            this.richTextBox2.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1335, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Transcript";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1335, 396);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Word Usage";
            // 
            // Debrief
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 672);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.buttontext);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Codec_lbl);
            this.Controls.Add(this.Frame_lbl);
            this.Controls.Add(this.Time_Label);
            this.Controls.Add(this.play_pause_BTN);
            this.Controls.Add(this.Video_CNTRL);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Debrief";
            this.Text = "TeamVis-> Debriefing";
            this.Load += new System.EventHandler(this.Debrief_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Video_CNTRL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TrackBar Video_CNTRL;
        private System.Windows.Forms.Panel play_pause_BTN;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label Time_Label;
        private System.Windows.Forms.Label Frame_lbl;
        private System.Windows.Forms.Label Codec_lbl;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttontext;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}