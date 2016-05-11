namespace Site_Manager
{
    partial class sdiSiteSchedule
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
            this.btnCutsheet = new System.Windows.Forms.Button();
            this.treeSchedule = new System.Windows.Forms.TreeView();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblNothing = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCutsheet
            // 
            this.btnCutsheet.Location = new System.Drawing.Point(650, 50);
            this.btnCutsheet.Name = "btnCutsheet";
            this.btnCutsheet.Size = new System.Drawing.Size(150, 23);
            this.btnCutsheet.TabIndex = 0;
            this.btnCutsheet.Text = "Create Cutsheet(s)";
            this.btnCutsheet.UseVisualStyleBackColor = true;
            this.btnCutsheet.Click += new System.EventHandler(this.btnCutsheet_Click);
            // 
            // treeSchedule
            // 
            this.treeSchedule.Location = new System.Drawing.Point(25, 25);
            this.treeSchedule.Name = "treeSchedule";
            this.treeSchedule.Size = new System.Drawing.Size(600, 425);
            this.treeSchedule.TabIndex = 1;
            this.treeSchedule.TabStop = false;
            this.treeSchedule.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeSchedule_AfterCheck);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(650, 397);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblNothing
            // 
            this.lblNothing.Location = new System.Drawing.Point(275, 50);
            this.lblNothing.Name = "lblNothing";
            this.lblNothing.Size = new System.Drawing.Size(100, 50);
            this.lblNothing.TabIndex = 3;
            this.lblNothing.Text = "Nothing Scheduled";
            this.lblNothing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sdiSiteSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 462);
            this.Controls.Add(this.lblNothing);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.treeSchedule);
            this.Controls.Add(this.btnCutsheet);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "sdiSiteSchedule";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Site Schedule";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCutsheet;
        private System.Windows.Forms.TreeView treeSchedule;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNothing;
    }
}