namespace PixelMagic.GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectWoWProcessToAttachTo));
            this.cmbWoW = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbWoW
            // 
            this.cmbWoW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWoW.FormattingEnabled = true;
            this.cmbWoW.Location = new System.Drawing.Point(12, 129);
            this.cmbWoW.Name = "cmbWoW";
            this.cmbWoW.Size = new System.Drawing.Size(173, 21);
            this.cmbWoW.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PixelMagic.GUI.Properties.Resources.wow2;
            this.pictureBox1.Location = new System.Drawing.Point(-76, -12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(559, 130);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Image = global::PixelMagic.GUI.Properties.Resources.Close_16x16;
            this.cmdCancel.Location = new System.Drawing.Point(295, 128);
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
            this.cmdConnect.Location = new System.Drawing.Point(216, 128);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(75, 23);
            this.cmdConnect.TabIndex = 2;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Image = global::PixelMagic.GUI.Properties.Resources.Refresh2_16x16;
            this.cmdRefresh.Location = new System.Drawing.Point(189, 128);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(23, 23);
            this.cmdRefresh.TabIndex = 5;
            this.cmdRefresh.TabStop = false;
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 106);
            this.panel1.TabIndex = 7;
            // 
            // SelectWoWProcessToAttachTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 160);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmbWoW);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.cmdRefresh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectWoWProcessToAttachTo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please select a WoW Process to connect to:";
            this.Load += new System.EventHandler(this.SelectWoWProcessToAttachTo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.ComboBox cmbWoW;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
    }
}