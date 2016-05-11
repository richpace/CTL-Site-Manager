namespace Site_Manager
{
    partial class mdiMain
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuMainFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainWindowWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuMainWindow,
            this.menuMainHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.MdiWindowListItem = this.menuMainWindow;
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1234, 24);
            this.menuMain.TabIndex = 5;
            this.menuMain.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainFileNew,
            this.menuMainFileOpen,
            this.menuSeparator,
            this.menuMainFileExit});
            this.menuFile.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuFile.MergeIndex = 0;
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuMainFileNew
            // 
            this.menuMainFileNew.Name = "menuMainFileNew";
            this.menuMainFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuMainFileNew.Size = new System.Drawing.Size(141, 22);
            this.menuMainFileNew.Text = "&New";
            this.menuMainFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // menuMainFileOpen
            // 
            this.menuMainFileOpen.Name = "menuMainFileOpen";
            this.menuMainFileOpen.Size = new System.Drawing.Size(141, 22);
            this.menuMainFileOpen.Text = "&Open";
            this.menuMainFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // menuSeparator
            // 
            this.menuSeparator.MergeIndex = 99;
            this.menuSeparator.Name = "menuSeparator";
            this.menuSeparator.Size = new System.Drawing.Size(138, 6);
            // 
            // menuMainFileExit
            // 
            this.menuMainFileExit.MergeIndex = 99;
            this.menuMainFileExit.Name = "menuMainFileExit";
            this.menuMainFileExit.Size = new System.Drawing.Size(141, 22);
            this.menuMainFileExit.Text = "E&xit";
            this.menuMainFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuMainWindow
            // 
            this.menuMainWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainWindowWindows});
            this.menuMainWindow.Name = "menuMainWindow";
            this.menuMainWindow.Size = new System.Drawing.Size(63, 20);
            this.menuMainWindow.Text = "&Window";
            // 
            // menuMainWindowWindows
            // 
            this.menuMainWindowWindows.Name = "menuMainWindowWindows";
            this.menuMainWindowWindows.Size = new System.Drawing.Size(132, 22);
            this.menuMainWindowWindows.Text = "Windows...";
            // 
            // menuMainHelp
            // 
            this.menuMainHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainHelpAbout});
            this.menuMainHelp.Name = "menuMainHelp";
            this.menuMainHelp.Size = new System.Drawing.Size(44, 20);
            this.menuMainHelp.Text = "&Help";
            // 
            // menuMainHelpAbout
            // 
            this.menuMainHelpAbout.Name = "menuMainHelpAbout";
            this.menuMainHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.menuMainHelpAbout.Text = "&About";
            // 
            // mdiMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 712);
            this.Controls.Add(this.menuMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMain;
            this.MinimumSize = new System.Drawing.Size(1250, 750);
            this.Name = "mdiMain";
            this.Text = "Site Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuMainFileNew;
        private System.Windows.Forms.ToolStripMenuItem menuMainFileOpen;
        private System.Windows.Forms.ToolStripSeparator menuSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuMainFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuMainWindow;
        private System.Windows.Forms.ToolStripMenuItem menuMainWindowWindows;
        private System.Windows.Forms.ToolStripMenuItem menuMainHelp;
        private System.Windows.Forms.ToolStripMenuItem menuMainHelpAbout;
    }
}

