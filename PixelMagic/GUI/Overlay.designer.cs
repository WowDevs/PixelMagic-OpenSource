namespace PixelMagic.GUI
{
    partial class Overlay
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
            this.lblMain = new System.Windows.Forms.Label();
            this.AoELabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.BackColor = System.Drawing.Color.Transparent;
            this.lblMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(150)))));
            this.lblMain.Location = new System.Drawing.Point(48, 9);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(114, 24);
            this.lblMain.TabIndex = 6;
            this.lblMain.Text = "Status Mode";
            this.lblMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblMain_MouseDown);
            this.lblMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblMain_MouseMove);
            this.lblMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblMain_MouseUp);
            // 
            // AoELabel
            // 
            this.AoELabel.AutoSize = true;
            this.AoELabel.BackColor = System.Drawing.Color.Transparent;
            this.AoELabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AoELabel.ForeColor = System.Drawing.Color.LightGreen;
            this.AoELabel.Location = new System.Drawing.Point(30, 57);
            this.AoELabel.Name = "AoELabel";
            this.AoELabel.Size = new System.Drawing.Size(162, 24);
            this.AoELabel.TabIndex = 7;
            this.AoELabel.Text = "Single-Target ON!";
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(214, 119);
            this.Controls.Add(this.AoELabel);
            this.Controls.Add(this.lblMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Overlay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Dude, Where\'s My Car?";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.Overlay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblMain;
        public System.Windows.Forms.Label AoELabel;
    }
}