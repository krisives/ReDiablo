namespace ReDiablo
{
    partial class GfxViewer
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
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomX1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomX2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomX4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canvas = new System.Windows.Forms.Panel();
            this.currentWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.currentHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.currentFrame = new System.Windows.Forms.NumericUpDown();
            this.maxFrame = new System.Windows.Forms.Label();
            this.nextFrameButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(430, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(124, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.saveToolStripMenuItem.Text = "Save Table";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomX1ToolStripMenuItem,
            this.zoomX2ToolStripMenuItem,
            this.zoomX4ToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // zoomX1ToolStripMenuItem
            // 
            this.zoomX1ToolStripMenuItem.Name = "zoomX1ToolStripMenuItem";
            this.zoomX1ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.zoomX1ToolStripMenuItem.Text = "Zoom X1";
            this.zoomX1ToolStripMenuItem.Click += new System.EventHandler(this.zoomX1ToolStripMenuItem_Click);
            // 
            // zoomX2ToolStripMenuItem
            // 
            this.zoomX2ToolStripMenuItem.Checked = true;
            this.zoomX2ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.zoomX2ToolStripMenuItem.Name = "zoomX2ToolStripMenuItem";
            this.zoomX2ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.zoomX2ToolStripMenuItem.Text = "Zoom X2";
            this.zoomX2ToolStripMenuItem.Click += new System.EventHandler(this.zoomX2ToolStripMenuItem_Click);
            // 
            // zoomX4ToolStripMenuItem
            // 
            this.zoomX4ToolStripMenuItem.Name = "zoomX4ToolStripMenuItem";
            this.zoomX4ToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.zoomX4ToolStripMenuItem.Text = "Zoom X4";
            this.zoomX4ToolStripMenuItem.Click += new System.EventHandler(this.zoomX4ToolStripMenuItem_Click);
            // 
            // canvas
            // 
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvas.Location = new System.Drawing.Point(0, 27);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(430, 270);
            this.canvas.TabIndex = 1;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // currentWidth
            // 
            this.currentWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.currentWidth.Location = new System.Drawing.Point(44, 303);
            this.currentWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.currentWidth.Name = "currentWidth";
            this.currentWidth.Size = new System.Drawing.Size(64, 20);
            this.currentWidth.TabIndex = 2;
            this.currentWidth.ValueChanged += new System.EventHandler(this.currentWidth_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 305);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Width:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Height:";
            // 
            // currentHeight
            // 
            this.currentHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.currentHeight.Location = new System.Drawing.Point(165, 303);
            this.currentHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.currentHeight.Name = "currentHeight";
            this.currentHeight.Size = new System.Drawing.Size(64, 20);
            this.currentHeight.TabIndex = 5;
            this.currentHeight.ValueChanged += new System.EventHandler(this.currentHeight_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Frame:";
            // 
            // currentFrame
            // 
            this.currentFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.currentFrame.Location = new System.Drawing.Point(315, 303);
            this.currentFrame.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.currentFrame.Name = "currentFrame";
            this.currentFrame.Size = new System.Drawing.Size(64, 20);
            this.currentFrame.TabIndex = 7;
            this.currentFrame.ValueChanged += new System.EventHandler(this.currentFrame_ValueChanged);
            // 
            // maxFrame
            // 
            this.maxFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxFrame.Location = new System.Drawing.Point(385, 305);
            this.maxFrame.Name = "maxFrame";
            this.maxFrame.Size = new System.Drawing.Size(42, 13);
            this.maxFrame.TabIndex = 9;
            this.maxFrame.Text = "/ 0";
            // 
            // nextFrameButton
            // 
            this.nextFrameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nextFrameButton.Location = new System.Drawing.Point(235, 300);
            this.nextFrameButton.Name = "nextFrameButton";
            this.nextFrameButton.Size = new System.Drawing.Size(32, 23);
            this.nextFrameButton.TabIndex = 10;
            this.nextFrameButton.Text = ">";
            this.nextFrameButton.UseVisualStyleBackColor = true;
            this.nextFrameButton.Click += new System.EventHandler(this.nextFrameButton_Click);
            // 
            // GfxViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 325);
            this.Controls.Add(this.nextFrameButton);
            this.Controls.Add(this.maxFrame);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.currentFrame);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.currentHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentWidth);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GfxViewer";
            this.Text = "Graphic Viewer";
            this.Resize += new System.EventHandler(this.GfxViewer_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.NumericUpDown currentWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown currentHeight;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown currentFrame;
        private System.Windows.Forms.Label maxFrame;
        private System.Windows.Forms.Button nextFrameButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomX1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomX2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomX4ToolStripMenuItem;
    }
}

