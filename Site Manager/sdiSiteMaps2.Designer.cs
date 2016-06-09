namespace Site_Manager
{
    partial class sdiSiteMaps2
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
            this.gridMaps = new System.Windows.Forms.DataGridView();
            this.btnSiteMap = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabsMapping = new System.Windows.Forms.TabControl();
            this.tabAssigned = new System.Windows.Forms.TabPage();
            this.tabUnassigned = new System.Windows.Forms.TabPage();
            this.gridUnMaps = new System.Windows.Forms.DataGridView();
            this.gridcolumnMType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnMLegacy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnMLegacyPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnMASR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnMASRPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnUType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnULegacy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnULegacyPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaps)).BeginInit();
            this.tabsMapping.SuspendLayout();
            this.tabAssigned.SuspendLayout();
            this.tabUnassigned.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUnMaps)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMaps
            // 
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
            this.gridMaps.Size = new System.Drawing.Size(568, 351);
            this.gridMaps.TabIndex = 0;
            this.gridMaps.TabStop = false;
            this.gridMaps.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMaps_CellClick);
            // 
            // btnSiteMap
            // 
            this.btnSiteMap.Location = new System.Drawing.Point(625, 50);
            this.btnSiteMap.Name = "btnSiteMap";
            this.btnSiteMap.Size = new System.Drawing.Size(100, 23);
            this.btnSiteMap.TabIndex = 1;
            this.btnSiteMap.Text = "Export to Excel";
            this.btnSiteMap.UseVisualStyleBackColor = true;
            this.btnSiteMap.Click += new System.EventHandler(this.btnSiteMap_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(625, 375);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 26);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabsMapping
            // 
            this.tabsMapping.Controls.Add(this.tabAssigned);
            this.tabsMapping.Controls.Add(this.tabUnassigned);
            this.tabsMapping.Location = new System.Drawing.Point(12, 12);
            this.tabsMapping.Name = "tabsMapping";
            this.tabsMapping.SelectedIndex = 0;
            this.tabsMapping.Size = new System.Drawing.Size(588, 389);
            this.tabsMapping.TabIndex = 3;
            // 
            // tabAssigned
            // 
            this.tabAssigned.Controls.Add(this.gridMaps);
            this.tabAssigned.Location = new System.Drawing.Point(4, 22);
            this.tabAssigned.Name = "tabAssigned";
            this.tabAssigned.Padding = new System.Windows.Forms.Padding(3);
            this.tabAssigned.Size = new System.Drawing.Size(580, 363);
            this.tabAssigned.TabIndex = 0;
            this.tabAssigned.Text = "Assigned";
            this.tabAssigned.UseVisualStyleBackColor = true;
            // 
            // tabUnassigned
            // 
            this.tabUnassigned.Controls.Add(this.gridUnMaps);
            this.tabUnassigned.Location = new System.Drawing.Point(4, 22);
            this.tabUnassigned.Name = "tabUnassigned";
            this.tabUnassigned.Padding = new System.Windows.Forms.Padding(3);
            this.tabUnassigned.Size = new System.Drawing.Size(580, 363);
            this.tabUnassigned.TabIndex = 1;
            this.tabUnassigned.Text = "Unassigned";
            this.tabUnassigned.UseVisualStyleBackColor = true;
            // 
            // gridUnMaps
            // 
            this.gridUnMaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUnMaps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridcolumnUType,
            this.gridcolumnULegacy,
            this.gridcolumnULegacyPrefix});
            this.gridUnMaps.Location = new System.Drawing.Point(6, 6);
            this.gridUnMaps.Name = "gridUnMaps";
            this.gridUnMaps.Size = new System.Drawing.Size(568, 351);
            this.gridUnMaps.TabIndex = 0;
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
            // sdiSiteMaps2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 470);
            this.Controls.Add(this.tabsMapping);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSiteMap);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "sdiSiteMaps2";
            this.ShowInTaskbar = false;
            this.Text = "Site Map";
            ((System.ComponentModel.ISupportInitialize)(this.gridMaps)).EndInit();
            this.tabsMapping.ResumeLayout(false);
            this.tabAssigned.ResumeLayout(false);
            this.tabUnassigned.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridUnMaps)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMaps;
        private System.Windows.Forms.Button btnSiteMap;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMType;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMLegacy;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMLegacyPrefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMASR;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnMASRPrefix;
        private System.Windows.Forms.TabControl tabsMapping;
        private System.Windows.Forms.TabPage tabAssigned;
        private System.Windows.Forms.TabPage tabUnassigned;
        private System.Windows.Forms.DataGridView gridUnMaps;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnUType;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnULegacy;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnULegacyPrefix;
    }
}