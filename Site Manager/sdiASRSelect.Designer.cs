namespace Site_Manager
{
    partial class sdiASRSelect
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
            this.listboxIOX = new System.Windows.Forms.ListBox();
            this.buttonASRSave = new System.Windows.Forms.Button();
            this.buttonASRCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listboxIOX
            // 
            this.listboxIOX.FormattingEnabled = true;
            this.listboxIOX.Location = new System.Drawing.Point(0, 0);
            this.listboxIOX.Name = "listboxIOX";
            this.listboxIOX.Size = new System.Drawing.Size(100, 160);
            this.listboxIOX.Sorted = true;
            this.listboxIOX.TabIndex = 0;
            this.listboxIOX.TabStop = false;
            this.listboxIOX.UseTabStops = false;
            // 
            // buttonASRSave
            // 
            this.buttonASRSave.Location = new System.Drawing.Point(106, 50);
            this.buttonASRSave.Name = "buttonASRSave";
            this.buttonASRSave.Size = new System.Drawing.Size(75, 23);
            this.buttonASRSave.TabIndex = 1;
            this.buttonASRSave.Text = "Save";
            this.buttonASRSave.UseVisualStyleBackColor = true;
            this.buttonASRSave.Click += new System.EventHandler(this.buttonASRSave_Click);
            // 
            // buttonASRCancel
            // 
            this.buttonASRCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonASRCancel.Location = new System.Drawing.Point(106, 79);
            this.buttonASRCancel.Name = "buttonASRCancel";
            this.buttonASRCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonASRCancel.TabIndex = 2;
            this.buttonASRCancel.Text = "Cancel";
            this.buttonASRCancel.UseVisualStyleBackColor = true;
            this.buttonASRCancel.Click += new System.EventHandler(this.buttonASRCancel_Click);
            // 
            // sdiASRSelect
            // 
            this.AcceptButton = this.buttonASRSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonASRCancel;
            this.ClientSize = new System.Drawing.Size(193, 162);
            this.ControlBox = false;
            this.Controls.Add(this.buttonASRCancel);
            this.Controls.Add(this.buttonASRSave);
            this.Controls.Add(this.listboxIOX);
            this.Name = "sdiASRSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Select ASR...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listboxIOX;
        private System.Windows.Forms.Button buttonASRSave;
        private System.Windows.Forms.Button buttonASRCancel;
    }
}