namespace Site_Manager
{
    partial class sdiSiteCircuits
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
            this.treeDCS = new System.Windows.Forms.TreeView();
            this.tabCircuits = new System.Windows.Forms.TabControl();
            this.tabDCS = new System.Windows.Forms.TabPage();
            this.tabMON = new System.Windows.Forms.TabPage();
            this.treeMON = new System.Windows.Forms.TreeView();
            this.tabPHY = new System.Windows.Forms.TabPage();
            this.treePHY = new System.Windows.Forms.TreeView();
            this.calendarCircuits = new System.Windows.Forms.MonthCalendar();
            this.cbDisplayScheduled = new System.Windows.Forms.CheckBox();
            this.btnAssign = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbDIV = new System.Windows.Forms.CheckBox();
            this.cbDense = new System.Windows.Forms.CheckBox();
            this.cbOther = new System.Windows.Forms.CheckBox();
            this.cbMultilink = new System.Windows.Forms.CheckBox();
            this.gbDisplay = new System.Windows.Forms.GroupBox();
            this.cbScheduledOnly = new System.Windows.Forms.CheckBox();
            this.textboxSearch = new System.Windows.Forms.TextBox();
            this.tabCircuits.SuspendLayout();
            this.tabDCS.SuspendLayout();
            this.tabMON.SuspendLayout();
            this.tabPHY.SuspendLayout();
            this.gbDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeDCS
            // 
            this.treeDCS.CheckBoxes = true;
            this.treeDCS.Location = new System.Drawing.Point(25, 25);
            this.treeDCS.Name = "treeDCS";
            this.treeDCS.Size = new System.Drawing.Size(625, 350);
            this.treeDCS.TabIndex = 0;
            this.treeDCS.TabStop = false;
            this.treeDCS.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeDCS_AfterCheck);
            // 
            // tabCircuits
            // 
            this.tabCircuits.Controls.Add(this.tabDCS);
            this.tabCircuits.Controls.Add(this.tabMON);
            this.tabCircuits.Controls.Add(this.tabPHY);
            this.tabCircuits.Location = new System.Drawing.Point(25, 25);
            this.tabCircuits.Name = "tabCircuits";
            this.tabCircuits.SelectedIndex = 0;
            this.tabCircuits.Size = new System.Drawing.Size(678, 425);
            this.tabCircuits.TabIndex = 1;
            this.tabCircuits.TabStop = false;
            // 
            // tabDCS
            // 
            this.tabDCS.Controls.Add(this.treeDCS);
            this.tabDCS.Location = new System.Drawing.Point(4, 22);
            this.tabDCS.Name = "tabDCS";
            this.tabDCS.Padding = new System.Windows.Forms.Padding(3);
            this.tabDCS.Size = new System.Drawing.Size(670, 399);
            this.tabDCS.TabIndex = 0;
            this.tabDCS.Text = "DCS";
            this.tabDCS.UseVisualStyleBackColor = true;
            // 
            // tabMON
            // 
            this.tabMON.Controls.Add(this.treeMON);
            this.tabMON.Location = new System.Drawing.Point(4, 22);
            this.tabMON.Name = "tabMON";
            this.tabMON.Padding = new System.Windows.Forms.Padding(3);
            this.tabMON.Size = new System.Drawing.Size(670, 399);
            this.tabMON.TabIndex = 1;
            this.tabMON.Text = "MON";
            this.tabMON.UseVisualStyleBackColor = true;
            // 
            // treeMON
            // 
            this.treeMON.CheckBoxes = true;
            this.treeMON.Location = new System.Drawing.Point(25, 25);
            this.treeMON.Name = "treeMON";
            this.treeMON.Size = new System.Drawing.Size(625, 350);
            this.treeMON.TabIndex = 0;
            this.treeMON.TabStop = false;
            this.treeMON.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeMON_AfterCheck);
            // 
            // tabPHY
            // 
            this.tabPHY.Controls.Add(this.treePHY);
            this.tabPHY.Location = new System.Drawing.Point(4, 22);
            this.tabPHY.Name = "tabPHY";
            this.tabPHY.Padding = new System.Windows.Forms.Padding(3);
            this.tabPHY.Size = new System.Drawing.Size(670, 399);
            this.tabPHY.TabIndex = 2;
            this.tabPHY.Text = "PHY";
            this.tabPHY.UseVisualStyleBackColor = true;
            // 
            // treePHY
            // 
            this.treePHY.CheckBoxes = true;
            this.treePHY.Location = new System.Drawing.Point(25, 25);
            this.treePHY.Name = "treePHY";
            this.treePHY.Size = new System.Drawing.Size(625, 350);
            this.treePHY.TabIndex = 0;
            this.treePHY.TabStop = false;
            this.treePHY.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treePHY_AfterCheck);
            // 
            // calendarCircuits
            // 
            this.calendarCircuits.Location = new System.Drawing.Point(730, 175);
            this.calendarCircuits.MaxSelectionCount = 1;
            this.calendarCircuits.Name = "calendarCircuits";
            this.calendarCircuits.TabIndex = 2;
            this.calendarCircuits.TabStop = false;
            // 
            // cbDisplayScheduled
            // 
            this.cbDisplayScheduled.AutoSize = true;
            this.cbDisplayScheduled.Location = new System.Drawing.Point(133, 25);
            this.cbDisplayScheduled.Name = "cbDisplayScheduled";
            this.cbDisplayScheduled.Size = new System.Drawing.Size(77, 17);
            this.cbDisplayScheduled.TabIndex = 3;
            this.cbDisplayScheduled.TabStop = false;
            this.cbDisplayScheduled.Text = "Scheduled";
            this.cbDisplayScheduled.UseVisualStyleBackColor = true;
            this.cbDisplayScheduled.CheckedChanged += new System.EventHandler(this.cbDisplayScheduled_CheckedChanged);
            // 
            // btnAssign
            // 
            this.btnAssign.Location = new System.Drawing.Point(730, 350);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(227, 23);
            this.btnAssign.TabIndex = 5;
            this.btnAssign.TabStop = false;
            this.btnAssign.Text = "Schedule";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(805, 423);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbDIV
            // 
            this.cbDIV.AutoSize = true;
            this.cbDIV.Location = new System.Drawing.Point(10, 25);
            this.cbDIV.Name = "cbDIV";
            this.cbDIV.Size = new System.Drawing.Size(44, 17);
            this.cbDIV.TabIndex = 8;
            this.cbDIV.Text = "DIV";
            this.cbDIV.UseVisualStyleBackColor = true;
            this.cbDIV.CheckedChanged += new System.EventHandler(this.cbDIV_CheckedChanged);
            // 
            // cbDense
            // 
            this.cbDense.AutoSize = true;
            this.cbDense.Location = new System.Drawing.Point(10, 50);
            this.cbDense.Name = "cbDense";
            this.cbDense.Size = new System.Drawing.Size(84, 17);
            this.cbDense.TabIndex = 9;
            this.cbDense.Text = "Channelized";
            this.cbDense.UseVisualStyleBackColor = true;
            this.cbDense.CheckedChanged += new System.EventHandler(this.cbDense_CheckedChanged);
            // 
            // cbOther
            // 
            this.cbOther.AutoSize = true;
            this.cbOther.Location = new System.Drawing.Point(10, 100);
            this.cbOther.Name = "cbOther";
            this.cbOther.Size = new System.Drawing.Size(52, 17);
            this.cbOther.TabIndex = 10;
            this.cbOther.Text = "Other";
            this.cbOther.UseVisualStyleBackColor = true;
            this.cbOther.CheckedChanged += new System.EventHandler(this.cbOther_CheckedChanged);
            // 
            // cbMultilink
            // 
            this.cbMultilink.AutoSize = true;
            this.cbMultilink.Location = new System.Drawing.Point(10, 75);
            this.cbMultilink.Name = "cbMultilink";
            this.cbMultilink.Size = new System.Drawing.Size(64, 17);
            this.cbMultilink.TabIndex = 11;
            this.cbMultilink.Text = "Multilink";
            this.cbMultilink.UseVisualStyleBackColor = true;
            this.cbMultilink.CheckedChanged += new System.EventHandler(this.cbMultilink_CheckedChanged);
            // 
            // gbDisplay
            // 
            this.gbDisplay.Controls.Add(this.cbScheduledOnly);
            this.gbDisplay.Controls.Add(this.cbMultilink);
            this.gbDisplay.Controls.Add(this.cbOther);
            this.gbDisplay.Controls.Add(this.cbDense);
            this.gbDisplay.Controls.Add(this.cbDIV);
            this.gbDisplay.Controls.Add(this.cbDisplayScheduled);
            this.gbDisplay.Location = new System.Drawing.Point(730, 40);
            this.gbDisplay.Name = "gbDisplay";
            this.gbDisplay.Size = new System.Drawing.Size(227, 125);
            this.gbDisplay.TabIndex = 12;
            this.gbDisplay.TabStop = false;
            this.gbDisplay.Text = "Display";
            // 
            // cbScheduledOnly
            // 
            this.cbScheduledOnly.AutoSize = true;
            this.cbScheduledOnly.Location = new System.Drawing.Point(133, 50);
            this.cbScheduledOnly.Name = "cbScheduledOnly";
            this.cbScheduledOnly.Size = new System.Drawing.Size(47, 17);
            this.cbScheduledOnly.TabIndex = 12;
            this.cbScheduledOnly.Text = "Only";
            this.cbScheduledOnly.UseVisualStyleBackColor = true;
            this.cbScheduledOnly.CheckedChanged += new System.EventHandler(this.cbScheduledOnly_CheckedChanged);
            // 
            // textboxSearch
            // 
            this.textboxSearch.Location = new System.Drawing.Point(408, 18);
            this.textboxSearch.Name = "textboxSearch";
            this.textboxSearch.Size = new System.Drawing.Size(291, 20);
            this.textboxSearch.TabIndex = 13;
            this.textboxSearch.TextChanged += new System.EventHandler(this.textboxSearch_TextChanged);
            // 
            // sdiSiteCircuits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 462);
            this.Controls.Add(this.textboxSearch);
            this.Controls.Add(this.gbDisplay);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAssign);
            this.Controls.Add(this.calendarCircuits);
            this.Controls.Add(this.tabCircuits);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "sdiSiteCircuits";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Site Circuits";
            this.tabCircuits.ResumeLayout(false);
            this.tabDCS.ResumeLayout(false);
            this.tabMON.ResumeLayout(false);
            this.tabPHY.ResumeLayout(false);
            this.gbDisplay.ResumeLayout(false);
            this.gbDisplay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDCS;
        private System.Windows.Forms.TabControl tabCircuits;
        private System.Windows.Forms.TabPage tabDCS;
        private System.Windows.Forms.TabPage tabMON;
        private System.Windows.Forms.TabPage tabPHY;
        private System.Windows.Forms.TreeView treeMON;
        private System.Windows.Forms.TreeView treePHY;
        private System.Windows.Forms.MonthCalendar calendarCircuits;
        private System.Windows.Forms.CheckBox cbDisplayScheduled;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox cbDIV;
        private System.Windows.Forms.CheckBox cbDense;
        private System.Windows.Forms.CheckBox cbOther;
        private System.Windows.Forms.CheckBox cbMultilink;
        private System.Windows.Forms.GroupBox gbDisplay;
        private System.Windows.Forms.CheckBox cbScheduledOnly;
        private System.Windows.Forms.TextBox textboxSearch;
    }
}