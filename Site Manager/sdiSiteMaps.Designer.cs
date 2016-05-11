namespace Site_Manager
{
    partial class sdiSiteMaps
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
            this.gridcolumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnLegacy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnLegacyPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnASR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridcolumnASRPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSiteMap = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaps)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMaps
            // 
            this.gridMaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMaps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridcolumnType,
            this.gridcolumnLegacy,
            this.gridcolumnLegacyPrefix,
            this.gridcolumnASR,
            this.gridcolumnASRPrefix});
            this.gridMaps.Location = new System.Drawing.Point(25, 25);
            this.gridMaps.Name = "gridMaps";
            this.gridMaps.ReadOnly = true;
            this.gridMaps.Size = new System.Drawing.Size(575, 400);
            this.gridMaps.TabIndex = 0;
            this.gridMaps.TabStop = false;
            // 
            // gridcolumnType
            // 
            this.gridcolumnType.HeaderText = "Type";
            this.gridcolumnType.Name = "gridcolumnType";
            this.gridcolumnType.ReadOnly = true;
            // 
            // gridcolumnLegacy
            // 
            this.gridcolumnLegacy.HeaderText = "Legacy";
            this.gridcolumnLegacy.Name = "gridcolumnLegacy";
            this.gridcolumnLegacy.ReadOnly = true;
            // 
            // gridcolumnLegacyPrefix
            // 
            this.gridcolumnLegacyPrefix.HeaderText = "Legacy Prefix";
            this.gridcolumnLegacyPrefix.Name = "gridcolumnLegacyPrefix";
            this.gridcolumnLegacyPrefix.ReadOnly = true;
            // 
            // gridcolumnASR
            // 
            this.gridcolumnASR.HeaderText = "ASR";
            this.gridcolumnASR.Name = "gridcolumnASR";
            this.gridcolumnASR.ReadOnly = true;
            // 
            // gridcolumnASRPrefix
            // 
            this.gridcolumnASRPrefix.HeaderText = "ASR Prefix";
            this.gridcolumnASRPrefix.Name = "gridcolumnASRPrefix";
            this.gridcolumnASRPrefix.ReadOnly = true;
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
            // sdiSiteMaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 456);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSiteMap);
            this.Controls.Add(this.gridMaps);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "sdiSiteMaps";
            this.ShowInTaskbar = false;
            this.Text = "Site Map";
            ((System.ComponentModel.ISupportInitialize)(this.gridMaps)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMaps;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnLegacy;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnLegacyPrefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnASR;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridcolumnASRPrefix;
        private System.Windows.Forms.Button btnSiteMap;
        private System.Windows.Forms.Button btnClose;
    }
}