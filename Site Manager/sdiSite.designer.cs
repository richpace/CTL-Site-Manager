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
            this.treeASR = new System.Windows.Forms.TreeView();
            this.lblASR = new System.Windows.Forms.Label();
            this.lblLegacy = new System.Windows.Forms.Label();
            this.menuSite = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteDataASR = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteDataWB = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteCircuits = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSiteSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.addASRDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addLegacyDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextLegacyNode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextLegacyNodeAssignASR = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAutoAssign = new System.Windows.Forms.Button();
            this.tabsMapping = new System.Windows.Forms.TabControl();
            this.tabAssignedCX = new System.Windows.Forms.TabPage();
            this.gridAssigned = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabUnassignedCX = new System.Windows.Forms.TabPage();
            this.gridUnassigned = new System.Windows.Forms.DataGridView();
            this.treeWB = new System.Windows.Forms.TreeView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuSite.SuspendLayout();
            this.contextLegacyNode.SuspendLayout();
            this.tabsMapping.SuspendLayout();
            this.tabAssignedCX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAssigned)).BeginInit();
            this.tabUnassignedCX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUnassigned)).BeginInit();
            this.SuspendLayout();
            // 
            // treeASR
            // 
            this.treeASR.CheckBoxes = true;
            this.treeASR.Location = new System.Drawing.Point(493, 25);
            this.treeASR.MinimumSize = new System.Drawing.Size(300, 200);
            this.treeASR.Name = "treeASR";
            this.treeASR.Size = new System.Drawing.Size(300, 200);
            this.treeASR.TabIndex = 0;
            this.treeASR.TabStop = false;
            this.treeASR.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeASR_AfterCheck);
            // 
            // lblASR
            // 
            this.lblASR.AutoSize = true;
            this.lblASR.Location = new System.Drawing.Point(493, 12);
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
            this.menuSite.Size = new System.Drawing.Size(803, 24);
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
            this.menuSiteData,
            this.menuSiteCircuits,
            this.menuSiteSchedule});
            this.menuSiteDatabase.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.menuSiteDatabase.MergeIndex = 1;
            this.menuSiteDatabase.Name = "menuSiteDatabase";
            this.menuSiteDatabase.Size = new System.Drawing.Size(38, 20);
            this.menuSiteDatabase.Text = "&Site";
            // 
            // menuSiteData
            // 
            this.menuSiteData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSiteDataASR,
            this.menuSiteDataWB});
            this.menuSiteData.Name = "menuSiteData";
            this.menuSiteData.Size = new System.Drawing.Size(122, 22);
            this.menuSiteData.Text = "Data";
            // 
            // menuSiteDataASR
            // 
            this.menuSiteDataASR.Name = "menuSiteDataASR";
            this.menuSiteDataASR.Size = new System.Drawing.Size(172, 22);
            this.menuSiteDataASR.Text = "Add ASR Device...";
            this.menuSiteDataASR.Click += new System.EventHandler(this.menuSiteDataASR_Click);
            // 
            // menuSiteDataWB
            // 
            this.menuSiteDataWB.Name = "menuSiteDataWB";
            this.menuSiteDataWB.Size = new System.Drawing.Size(172, 22);
            this.menuSiteDataWB.Text = "Add Legacy Data...";
            this.menuSiteDataWB.Click += new System.EventHandler(this.menuSiteDataWB_Click);
            // 
            // menuSiteCircuits
            // 
            this.menuSiteCircuits.Name = "menuSiteCircuits";
            this.menuSiteCircuits.Size = new System.Drawing.Size(122, 22);
            this.menuSiteCircuits.Text = "Circuits";
            this.menuSiteCircuits.Click += new System.EventHandler(this.menuSiteCircuits_Click);
            // 
            // menuSiteSchedule
            // 
            this.menuSiteSchedule.Name = "menuSiteSchedule";
            this.menuSiteSchedule.Size = new System.Drawing.Size(122, 22);
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
            // 
            // btnAutoAssign
            // 
            this.btnAutoAssign.Location = new System.Drawing.Point(316, 50);
            this.btnAutoAssign.Name = "btnAutoAssign";
            this.btnAutoAssign.Size = new System.Drawing.Size(172, 23);
            this.btnAutoAssign.TabIndex = 5;
            this.btnAutoAssign.Text = "<-- Auto Assign -->";
            this.btnAutoAssign.UseVisualStyleBackColor = true;
            this.btnAutoAssign.Click += new System.EventHandler(this.btnAutoAssign_Click);
            // 
            // tabsMapping
            // 
            this.tabsMapping.Controls.Add(this.tabAssignedCX);
            this.tabsMapping.Controls.Add(this.tabUnassignedCX);
            this.tabsMapping.Location = new System.Drawing.Point(10, 231);
            this.tabsMapping.Name = "tabsMapping";
            this.tabsMapping.SelectedIndex = 0;
            this.tabsMapping.Size = new System.Drawing.Size(783, 239);
            this.tabsMapping.TabIndex = 6;
            this.tabsMapping.TabStop = false;
            // 
            // tabAssignedCX
            // 
            this.tabAssignedCX.Controls.Add(this.gridAssigned);
            this.tabAssignedCX.Location = new System.Drawing.Point(4, 22);
            this.tabAssignedCX.Name = "tabAssignedCX";
            this.tabAssignedCX.Padding = new System.Windows.Forms.Padding(3);
            this.tabAssignedCX.Size = new System.Drawing.Size(775, 213);
            this.tabAssignedCX.TabIndex = 2;
            this.tabAssignedCX.Text = "Assigned";
            this.tabAssignedCX.UseVisualStyleBackColor = true;
            // 
            // gridAssigned
            // 
            this.gridAssigned.AllowUserToAddRows = false;
            this.gridAssigned.AllowUserToDeleteRows = false;
            this.gridAssigned.AllowUserToResizeRows = false;
            this.gridAssigned.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAssigned.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.gridAssigned.Location = new System.Drawing.Point(3, 6);
            this.gridAssigned.Name = "gridAssigned";
            this.gridAssigned.ReadOnly = true;
            this.gridAssigned.Size = new System.Drawing.Size(766, 201);
            this.gridAssigned.TabIndex = 0;
            this.gridAssigned.TabStop = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Type";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Legacy Device";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Legacy Prefix";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ASR Device";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ASR Prefix";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // tabUnassignedCX
            // 
            this.tabUnassignedCX.Controls.Add(this.gridUnassigned);
            this.tabUnassignedCX.Location = new System.Drawing.Point(4, 22);
            this.tabUnassignedCX.Name = "tabUnassignedCX";
            this.tabUnassignedCX.Padding = new System.Windows.Forms.Padding(3);
            this.tabUnassignedCX.Size = new System.Drawing.Size(775, 213);
            this.tabUnassignedCX.TabIndex = 3;
            this.tabUnassignedCX.Text = "Unassigned";
            this.tabUnassignedCX.UseVisualStyleBackColor = true;
            // 
            // gridUnassigned
            // 
            this.gridUnassigned.AllowUserToAddRows = false;
            this.gridUnassigned.AllowUserToDeleteRows = false;
            this.gridUnassigned.AllowUserToResizeRows = false;
            this.gridUnassigned.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUnassigned.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column7,
            this.Column8});
            this.gridUnassigned.Location = new System.Drawing.Point(3, 6);
            this.gridUnassigned.Name = "gridUnassigned";
            this.gridUnassigned.ReadOnly = true;
            this.gridUnassigned.Size = new System.Drawing.Size(766, 201);
            this.gridUnassigned.TabIndex = 0;
            this.gridUnassigned.TabStop = false;
            // 
            // treeWB
            // 
            this.treeWB.AllowDrop = true;
            this.treeWB.Location = new System.Drawing.Point(10, 25);
            this.treeWB.MinimumSize = new System.Drawing.Size(300, 200);
            this.treeWB.Name = "treeWB";
            this.treeWB.Size = new System.Drawing.Size(300, 200);
            this.treeWB.TabIndex = 7;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Type";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Legacy Device";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Legacy Prefix";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // sdiSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(803, 479);
            this.Controls.Add(this.treeWB);
            this.Controls.Add(this.tabsMapping);
            this.Controls.Add(this.btnAutoAssign);
            this.Controls.Add(this.lblLegacy);
            this.Controls.Add(this.lblASR);
            this.Controls.Add(this.treeASR);
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
            this.tabAssignedCX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAssigned)).EndInit();
            this.tabUnassignedCX.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridUnassigned)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeASR;
        private System.Windows.Forms.Label lblASR;
        private System.Windows.Forms.Label lblLegacy;
        private System.Windows.Forms.MenuStrip menuSite;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileClose;
        private System.Windows.Forms.ToolStripMenuItem menuSiteDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuFileSave;
        private System.Windows.Forms.ToolStripMenuItem menuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator menuSiteFileSeparator;
        private System.Windows.Forms.ToolStripMenuItem addASRDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addLegacyDeviceToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextLegacyNode;
        private System.Windows.Forms.ToolStripMenuItem contextLegacyNodeAssignASR;
        private System.Windows.Forms.ToolStripMenuItem menuSiteCircuits;
        private System.Windows.Forms.ToolStripMenuItem menuSiteSchedule;
        private System.Windows.Forms.Button btnAutoAssign;
        private System.Windows.Forms.TabControl tabsMapping;
        private System.Windows.Forms.TabPage tabAssignedCX;
        private System.Windows.Forms.TabPage tabUnassignedCX;
        private System.Windows.Forms.TreeView treeWB;
        private System.Windows.Forms.ToolStripMenuItem menuSiteData;
        private System.Windows.Forms.ToolStripMenuItem menuSiteDataASR;
        private System.Windows.Forms.ToolStripMenuItem menuSiteDataWB;
        private System.Windows.Forms.DataGridView gridAssigned;
        private System.Windows.Forms.DataGridView gridUnassigned;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}