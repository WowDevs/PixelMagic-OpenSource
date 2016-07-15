namespace PixelMagic.GUI.GUI
{
    partial class SelectWoWProcessToAttachTo
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
            this.cmbWoW = new System.Windows.Forms.ComboBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbWoW
            // 
            this.cmbWoW.FormattingEnabled = true;
            this.cmbWoW.Location = new System.Drawing.Point(12, 13);
            this.cmbWoW.Name = "cmbWoW";
            this.cmbWoW.Size = new System.Drawing.Size(173, 21);
            this.cmbWoW.TabIndex = 2;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Image = global::PixelMagic.GUI.Properties.Resources.Close_16x16;
            this.cmdCancel.Location = new System.Drawing.Point(295, 12);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Image = global::PixelMagic.GUI.Properties.Resources.Register;
            this.cmdConnect.Location = new System.Drawing.Point(216, 12);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(75, 23);
            this.cmdConnect.TabIndex = 1;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Image = global::PixelMagic.GUI.Properties.Resources.Refresh2_16x16;
            this.cmdRefresh.Location = new System.Drawing.Point(189, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(23, 23);
            this.cmdRefresh.TabIndex = 0;
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // SelectWoWProcessToAttachTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 47);
            this.ControlBox = false;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmbWoW);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.cmdRefresh);
            this.Name = "SelectWoWProcessToAttachTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please select a WoW Process to connect to:";
            this.Load += new System.EventHandler(this.SelectWoWProcessToAttachTo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.ComboBox cmbWoW;
        private System.Windows.Forms.Button cmdCancel;
    }
}