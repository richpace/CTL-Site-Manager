namespace Site_Manager
{
    partial class sdiSite
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
            this.treeIOX = new System.Windows.Forms.TreeView();
            this.treeIOS = new System.Windows.Forms.TreeView();
            this.lblASR = new System.Windows.Forms.Label();
            this.lblLegacy = new System.Windows.Forms.Label();
            this.menuSite = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteDevices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteDevicesAddASR = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteDevicesAddLegacy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteMaps = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteCircuits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.addASRDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLegacyDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextLegacyNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextLegacyNodeAssignASR = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMapSites = new System.Windows.Forms.Button();
            this.menuSite.SuspendLayout();
            this.contextLegacyNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeIOX
            // 
            this.treeIOX.Location = new System.Drawing.Point(330, 25);
            this.treeIOX.MinimumSize = new System.Drawing.Size(200, 200);
            this.treeIOX.Name = "treeIOX";
            this.treeIOX.Size = new System.Drawing.Size(200, 200);
            this.treeIOX.TabIndex = 0;
            this.treeIOX.TabStop = false;
            // 
            // treeIOS
            // 
            this.treeIOS.AllowDrop = true;
            this.treeIOS.Location = new System.Drawing.Point(10, 25);
            this.treeIOS.MinimumSize = new System.Drawing.Size(200, 200);
            this.treeIOS.Name = "treeIOS";
            this.treeIOS.Size = new System.Drawing.Size(200, 200);
            this.treeIOS.TabIndex = 1;
            this.treeIOS.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeIOS_NodeMouseClick);
            // 
            // lblASR
            // 
            this.lblASR.AutoSize = true;
            this.lblASR.Location = new System.Drawing.Point(330, 12);
            this.lblASR.Name = "lblASR";
            this.lblASR.Size = new System.Drawing.Size(71, 13);
            this.lblASR.TabIndex = 2;
            this.lblASR.Text = "ASR Devices";
            // 
            // lblLegacy
            // 
            this.lblLegacy.AutoSize = true;
            this.lblLegacy.Location = new System.Drawing.Point(10, 12);
            this.lblLegacy.Name = "lblLegacy";
            this.lblLegacy.Size = new System.Drawing.Size(84, 13);
            this.lblLegacy.TabIndex = 3;
            this.lblLegacy.Text = "Legacy Devices";
            // 
            // menuSite
            // 
            this.menuSite.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuSiteDatabase});
            this.menuSite.Location = new System.Drawing.Point(0, 0);
            this.menuSite.Name = "menuSite";
            this.menuSite.Size = new System.Drawing.Size(554, 24);
            this.menuSite.TabIndex = 4;
            this.menuSite.Text = "menuSite";
            this.menuSite.Visible = false;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSiteFileSeparator,
            this.menuFileSave,
            this.menuFileSaveAs,
            this.menuFileClose});
            this.menuFile.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.menuFile.MergeIndex = 0;
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuSiteFileSeparator
            // 
            this.menuSiteFileSeparator.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuSiteFileSeparator.MergeIndex = 2;
            this.menuSiteFileSeparator.Name = "menuSiteFileSeparator";
            this.menuSiteFileSeparator.Size = new System.Drawing.Size(149, 6);
            // 
            // menuFileSave
            // 
            this.menuFileSave.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuFileSave.MergeIndex = 3;
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuFileSave.Size = new System.Drawing.Size(152, 22);
            this.menuFileSave.Text = "&Save";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuFileSaveAs.MergeIndex = 4;
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            this.menuFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.menuFileSaveAs.Text = "Save &As";
            this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
            // 
            // menuFileClose
            // 
            this.menuFileClose.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuFileClose.MergeIndex = 5;
            this.menuFileClose.Name = "menuFileClose";
            this.menuFileClose.Size = new System.Drawing.Size(152, 22);
            this.menuFileClose.Text = "Close";
            this.menuFileClose.Click += new System.EventHandler(this.menuFileClose_Click);
            // 
            // menuSiteDatabase
            // 
            this.menuSiteDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSiteDevices,
            this.menuSiteMaps,
            this.menuSiteCircuits,
            this.menuSiteSchedule});
            this.menuSiteDatabase.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuSiteDatabase.MergeIndex = 1;
            this.menuSiteDatabase.Name = "menuSiteDatabase";
            this.menuSiteDatabase.Size = new System.Drawing.Size(38, 20);
            this.menuSiteDatabase.Text = "&Site";
            // 
            // menuSiteDevices
            // 
            this.menuSiteDevices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSiteDevicesAddASR,
            this.menuSiteDevicesAddLegacy});
            this.menuSiteDevices.Name = "menuSiteDevices";
            this.menuSiteDevices.Size = new System.Drawing.Size(123, 22);
            this.menuSiteDevices.Text = "&Devices...";
            // 
            // menuSiteDevicesAddASR
            // 
            this.menuSiteDevicesAddASR.Name = "menuSiteDevicesAddASR";
            this.menuSiteDevicesAddASR.Size = new System.Drawing.Size(174, 22);
            this.menuSiteDevicesAddASR.Text = "Add &ASR Device";
            this.menuSiteDevicesAddASR.Click += new System.EventHandler(this.menuSiteDevicesAddASR_Click);
            // 
            // menuSiteDevicesAddLegacy
            // 
            this.menuSiteDevicesAddLegacy.Name = "menuSiteDevicesAddLegacy";
            this.menuSiteDevicesAddLegacy.Size = new System.Drawing.Size(174, 22);
            this.menuSiteDevicesAddLegacy.Text = "Add &Legacy Device";
            this.menuSiteDevicesAddLegacy.Click += new System.EventHandler(this.menuSiteDevicesAddLegacy_Click);
            // 
            // menuSiteMaps
            // 
            this.menuSiteMaps.Enabled = false;
            this.menuSiteMaps.Name = "menuSiteMaps";
            this.menuSiteMaps.Size = new System.Drawing.Size(123, 22);
            this.menuSiteMaps.Text = "Maps";
            this.menuSiteMaps.Click += new System.EventHandler(this.menuSiteMaps_Click);
            // 
            // menuSiteCircuits
            // 
            this.menuSiteCircuits.Name = "menuSiteCircuits";
            this.menuSiteCircuits.Size = new System.Drawing.Size(123, 22);
            this.menuSiteCircuits.Text = "Circuits";
            this.menuSiteCircuits.Click += new System.EventHandler(this.menuSiteCircuits_Click);
            // 
            // menuSiteSchedule
            // 
            this.menuSiteSchedule.Name = "menuSiteSchedule";
            this.menuSiteSchedule.Size = new System.Drawing.Size(123, 22);
            this.menuSiteSchedule.Text = "&Schedule";
            this.menuSiteSchedule.Click += new System.EventHandler(this.menuSiteSchedule_Click);
            // 
            // addASRDeviceToolStripMenuItem
            // 
            this.addASRDeviceToolStripMenuItem.Name = "addASRDeviceToolStripMenuItem";
            this.addASRDeviceToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // addLegacyDeviceToolStripMenuItem
            // 
            this.addLegacyDeviceToolStripMenuItem.Name = "addLegacyDeviceToolStripMenuItem";
            this.addLegacyDeviceToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // contextLegacyNode
            // 
            this.contextLegacyNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextLegacyNodeAssignASR});
            this.contextLegacyNode.Name = "contextLegacy";
            this.contextLegacyNode.Size = new System.Drawing.Size(185, 26);
            // 
            // contextLegacyNodeAssignASR
            // 
            this.contextLegacyNodeAssignASR.Name = "contextLegacyNodeAssignASR";
            this.contextLegacyNodeAssignASR.Size = new System.Drawing.Size(184, 22);
            this.contextLegacyNodeAssignASR.Text = "Assign Preferred ASR";
            this.contextLegacyNodeAssignASR.Click += new System.EventHandler(this.contextLegacyNodeAssignASR_Click);
            // 
            // btnMapSites
            // 
            this.btnMapSites.Location = new System.Drawing.Point(220, 50);
            this.btnMapSites.Name = "btnMapSites";
            this.btnMapSites.Size = new System.Drawing.Size(100, 23);
            this.btnMapSites.TabIndex = 5;
            this.btnMapSites.Text = "<--Create Maps-->";
            this.btnMapSites.UseVisualStyleBackColor = true;
            this.btnMapSites.Click += new System.EventHandler(this.btnMapSites_Click);
            // 
            // sdiSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(554, 239);
            this.Controls.Add(this.btnMapSites);
            this.Controls.Add(this.lblLegacy);
            this.Controls.Add(this.lblASR);
            this.Controls.Add(this.treeIOS);
            this.Controls.Add(this.treeIOX);
            this.Controls.Add(this.menuSite);
            this.MaximumSize = new System.Drawing.Size(600, 300);
            this.MinimumSize = new System.Drawing.Size(415, 200);
            this.Name = "sdiSite";
            this.ShowInTaskbar = false;
            this.Text = "Site";
            this.Resize += new System.EventHandler(this.sdiSite_Resize);
            this.menuSite.ResumeLayout(false);
            this.menuSite.PerformLayout();
            this.contextLegacyNode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeIOX;
        private System.Windows.Forms.TreeView treeIOS;
        private System.Windows.Forms.Label lblASR;
        private System.Windows.Forms.Label lblLegacy;
        private System.Windows.Forms.MenuStrip menuSite;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileClose;
        private System.Windows.Forms.ToolStripMenuItem menuSiteDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuFileSave;
        private System.Windows.Forms.ToolStripMenuItem menuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator menuSiteFileSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuSiteDevices;
        private System.Windows.Forms.ToolStripMenuItem addASRDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addLegacyDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuSiteDevicesAddASR;
        private System.Windows.Forms.ToolStripMenuItem menuSiteDevicesAddLegacy;
        private System.Windows.Forms.ToolStripMenuItem menuSiteMaps;
        private System.Windows.Forms.ContextMenuStrip contextLegacyNode;
        private System.Windows.Forms.ToolStripMenuItem contextLegacyNodeAssignASR;
        private System.Windows.Forms.ToolStripMenuItem menuSiteCircuits;
        private System.Windows.Forms.ToolStripMenuItem menuSiteSchedule;
        private System.Windows.Forms.Button btnMapSites;
    }
}