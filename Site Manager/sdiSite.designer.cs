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
            this.btnAutoAssign = new System.Windows.Forms.Button();
            this.tabsMapping = new System.Windows.Forms.TabControl();
            this.tabAssigned = new System.Windows.Forms.TabPage();
            this.gridMaps = new System.Windows.Forms.DataGridView();
            this.gridcolumnMType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnMLegacy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnMLegacyPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnMASR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnMASRPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabUnassigned = new System.Windows.Forms.TabPage();
            this.gridUnMaps = new System.Windows.Forms.DataGridView();
            this.gridcolumnUType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnULegacy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnULegacyPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuSite.SuspendLayout();
            this.contextLegacyNode.SuspendLayout();
            this.tabsMapping.SuspendLayout();
            this.tabAssigned.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaps)).BeginInit();
            this.tabUnassigned.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUnMaps)).BeginInit();
            this.SuspendLayout();
            // 
            // treeIOX
            // 
            this.treeIOX.CheckBoxes = true;
            this.treeIOX.Location = new System.Drawing.Point(394, 25);
            this.treeIOX.MinimumSize = new System.Drawing.Size(200, 200);
            this.treeIOX.Name = "treeIOX";
            this.treeIOX.Size = new System.Drawing.Size(200, 200);
            this.treeIOX.TabIndex = 0;
            this.treeIOX.TabStop = false;
            this.treeIOX.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeIOX_NodeMouseClick);
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
            this.lblASR.Location = new System.Drawing.Point(391, 12);
            this.lblASR.Name = "lblASR";
            this.lblASR.Size = new System.Drawing.Size(71, 13);
            this.lblASR.TabIndex = 2;
            this.lblASR.Text = "ASR Devices";
            // 
            // lblLegacy
            // 
            this.lblLegacy.AutoSize = true;
            this.lblLegacy.Location = new System.Drawing.Point(7, 12);
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
            this.menuSite.Size = new System.Drawing.Size(606, 24);
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
            this.menuSiteFileSeparator.Size = new System.Drawing.Size(135, 6);
            // 
            // menuFileSave
            // 
            this.menuFileSave.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuFileSave.MergeIndex = 3;
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuFileSave.Size = new System.Drawing.Size(138, 22);
            this.menuFileSave.Text = "&Save";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuFileSaveAs.MergeIndex = 4;
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            this.menuFileSaveAs.Size = new System.Drawing.Size(138, 22);
            this.menuFileSaveAs.Text = "Save &As";
            this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
            // 
            // menuFileClose
            // 
            this.menuFileClose.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuFileClose.MergeIndex = 5;
            this.menuFileClose.Name = "menuFileClose";
            this.menuFileClose.Size = new System.Drawing.Size(138, 22);
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
            this.menuSiteDevices.Size = new System.Drawing.Size(152, 22);
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
            this.menuSiteMaps.Size = new System.Drawing.Size(152, 22);
            this.menuSiteMaps.Text = "Maps";
            this.menuSiteMaps.Click += new System.EventHandler(this.menuSiteMaps_Click);
            // 
            // menuSiteCircuits
            // 
            this.menuSiteCircuits.Name = "menuSiteCircuits";
            this.menuSiteCircuits.Size = new System.Drawing.Size(152, 22);
            this.menuSiteCircuits.Text = "Circuits";
            this.menuSiteCircuits.Click += new System.EventHandler(this.menuSiteCircuits_Click);
            // 
            // menuSiteSchedule
            // 
            this.menuSiteSchedule.Name = "menuSiteSchedule";
            this.menuSiteSchedule.Size = new System.Drawing.Size(152, 22);
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
            // btnAutoAssign
            // 
            this.btnAutoAssign.Location = new System.Drawing.Point(216, 50);
            this.btnAutoAssign.Name = "btnAutoAssign";
            this.btnAutoAssign.Size = new System.Drawing.Size(172, 23);
            this.btnAutoAssign.TabIndex = 5;
            this.btnAutoAssign.Text = "<-- Auto Assign -->";
            this.btnAutoAssign.UseVisualStyleBackColor = true;
            this.btnAutoAssign.Click += new System.EventHandler(this.btnAutoAssign_Click);
            // 
            // tabsMapping
            // 
            this.tabsMapping.Controls.Add(this.tabAssigned);
            this.tabsMapping.Controls.Add(this.tabUnassigned);
            this.tabsMapping.Location = new System.Drawing.Point(10, 231);
            this.tabsMapping.Name = "tabsMapping";
            this.tabsMapping.SelectedIndex = 0;
            this.tabsMapping.Size = new System.Drawing.Size(588, 239);
            this.tabsMapping.TabIndex = 6;
            // 
            // tabAssigned
            // 
            this.tabAssigned.Controls.Add(this.gridMaps);
            this.tabAssigned.Location = new System.Drawing.Point(4, 22);
            this.tabAssigned.Name = "tabAssigned";
            this.tabAssigned.Padding = new System.Windows.Forms.Padding(3);
            this.tabAssigned.Size = new System.Drawing.Size(580, 213);
            this.tabAssigned.TabIndex = 0;
            this.tabAssigned.Text = "Assigned";
            this.tabAssigned.UseVisualStyleBackColor = true;
            // 
            // gridMaps
            // 
            this.gridMaps.AllowUserToAddRows = false;
            this.gridMaps.AllowUserToDeleteRows = false;
            this.gridMaps.AllowUserToResizeColumns = false;
            this.gridMaps.AllowUserToResizeRows = false;
            this.gridMaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMaps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridcolumnMType,
            this.gridcolumnMLegacy,
            this.gridcolumnMLegacyPrefix,
            this.gridcolumnMASR,
            this.gridcolumnMASRPrefix});
            this.gridMaps.Location = new System.Drawing.Point(6, 6);
            this.gridMaps.Name = "gridMaps";
            this.gridMaps.ReadOnly = true;
            this.gridMaps.Size = new System.Drawing.Size(568, 200);
            this.gridMaps.TabIndex = 0;
            this.gridMaps.TabStop = false;
            // 
            // gridcolumnMType
            // 
            this.gridcolumnMType.HeaderText = "Type";
            this.gridcolumnMType.Name = "gridcolumnMType";
            this.gridcolumnMType.ReadOnly = true;
            // 
            // gridcolumnMLegacy
            // 
            this.gridcolumnMLegacy.HeaderText = "Legacy";
            this.gridcolumnMLegacy.Name = "gridcolumnMLegacy";
            this.gridcolumnMLegacy.ReadOnly = true;
            // 
            // gridcolumnMLegacyPrefix
            // 
            this.gridcolumnMLegacyPrefix.HeaderText = "Legacy Prefix";
            this.gridcolumnMLegacyPrefix.Name = "gridcolumnMLegacyPrefix";
            this.gridcolumnMLegacyPrefix.ReadOnly = true;
            // 
            // gridcolumnMASR
            // 
            this.gridcolumnMASR.HeaderText = "ASR";
            this.gridcolumnMASR.Name = "gridcolumnMASR";
            this.gridcolumnMASR.ReadOnly = true;
            // 
            // gridcolumnMASRPrefix
            // 
            this.gridcolumnMASRPrefix.HeaderText = "ASR Prefix";
            this.gridcolumnMASRPrefix.Name = "gridcolumnMASRPrefix";
            this.gridcolumnMASRPrefix.ReadOnly = true;
            // 
            // tabUnassigned
            // 
            this.tabUnassigned.Controls.Add(this.gridUnMaps);
            this.tabUnassigned.Location = new System.Drawing.Point(4, 22);
            this.tabUnassigned.Name = "tabUnassigned";
            this.tabUnassigned.Padding = new System.Windows.Forms.Padding(3);
            this.tabUnassigned.Size = new System.Drawing.Size(580, 213);
            this.tabUnassigned.TabIndex = 1;
            this.tabUnassigned.Text = "Unassigned";
            this.tabUnassigned.UseVisualStyleBackColor = true;
            // 
            // gridUnMaps
            // 
            this.gridUnMaps.AllowUserToAddRows = false;
            this.gridUnMaps.AllowUserToDeleteRows = false;
            this.gridUnMaps.AllowUserToResizeColumns = false;
            this.gridUnMaps.AllowUserToResizeRows = false;
            this.gridUnMaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUnMaps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridcolumnUType,
            this.gridcolumnULegacy,
            this.gridcolumnULegacyPrefix});
            this.gridUnMaps.Location = new System.Drawing.Point(6, 6);
            this.gridUnMaps.Name = "gridUnMaps";
            this.gridUnMaps.Size = new System.Drawing.Size(568, 200);
            this.gridUnMaps.TabIndex = 0;
            // 
            // gridcolumnUType
            // 
            this.gridcolumnUType.HeaderText = "Type";
            this.gridcolumnUType.Name = "gridcolumnUType";
            // 
            // gridcolumnULegacy
            // 
            this.gridcolumnULegacy.HeaderText = "Legacy";
            this.gridcolumnULegacy.Name = "gridcolumnULegacy";
            // 
            // gridcolumnULegacyPrefix
            // 
            this.gridcolumnULegacyPrefix.HeaderText = "Legacy Prefix";
            this.gridcolumnULegacyPrefix.Name = "gridcolumnULegacyPrefix";
            // 
            // sdiSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(606, 479);
            this.Controls.Add(this.tabsMapping);
            this.Controls.Add(this.btnAutoAssign);
            this.Controls.Add(this.lblLegacy);
            this.Controls.Add(this.lblASR);
            this.Controls.Add(this.treeIOS);
            this.Controls.Add(this.treeIOX);
            this.Controls.Add(this.menuSite);
            this.MinimumSize = new System.Drawing.Size(415, 200);
            this.Name = "sdiSite";
            this.ShowInTaskbar = false;
            this.Text = "Site";
            this.Resize += new System.EventHandler(this.sdiSite_Resize);
            this.menuSite.ResumeLayout(false);
            this.menuSite.PerformLayout();
            this.contextLegacyNode.ResumeLayout(false);
            this.tabsMapping.ResumeLayout(false);
            this.tabAssigned.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMaps)).EndInit();
            this.tabUnassigned.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridUnMaps)).EndInit();
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
        private System.Windows.Forms.Button btnAutoAssign;
        private System.Windows.Forms.TabControl tabsMapping;
        private System.Windows.Forms.TabPage tabAssigned;
        private System.Windows.Forms.DataGridView gridMaps;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMType;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMLegacy;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMLegacyPrefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMASR;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMASRPrefix;
        private System.Windows.Forms.TabPage tabUnassigned;
        private System.Windows.Forms.DataGridView gridUnMaps;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnUType;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnULegacy;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnULegacyPrefix;
    }
}