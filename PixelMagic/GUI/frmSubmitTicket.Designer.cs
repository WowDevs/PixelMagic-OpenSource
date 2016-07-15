namespace PixelMagic.GUI.GUI
{
    partial class frmSubmitTicket
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
            this.cmdSubmitTicket = new System.Windows.Forms.Button();
            this.cmbClass = new System.Windows.Forms.ComboBox();
            this.cmbSpec = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRotationPath = new System.Windows.Forms.TextBox();
            this.chkConfirm = new System.Windows.Forms.CheckBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdSubmitTicket
            // 
            this.cmdSubmitTicket.Location = new System.Drawing.Point(517, 251);
            this.cmdSubmitTicket.Name = "cmdSubmitTicket";
            this.cmdSubmitTicket.Size = new System.Drawing.Size(96, 23);
            this.cmdSubmitTicket.TabIndex = 1;
            this.cmdSubmitTicket.Text = "Submit Ticket";
            this.cmdSubmitTicket.UseVisualStyleBackColor = true;
            this.cmdSubmitTicket.Click += new System.EventHandler(this.cmdSubmitTicket_Click);
            // 
            // cmbClass
            // 
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.Items.AddRange(new object[] {
            "Warrior",
            "Paladin",
            "Hunter",
            "Rogue",
            "Priest",
            "Death Knight",
            "Shaman",
            "Mage",
            "Warlock",
            "Monk",
            "Druid",
            "Demon Hunter"});
            this.cmbClass.Location = new System.Drawing.Point(70, 12);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(121, 21);
            this.cmbClass.TabIndex = 2;
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmdClass_SelectedIndexChanged);
            // 
            // cmbSpec
            // 
            this.cmbSpec.FormattingEnabled = true;
            this.cmbSpec.Location = new System.Drawing.Point(70, 41);
            this.cmbSpec.Name = "cmbSpec";
            this.cmbSpec.Size = new System.Drawing.Size(121, 21);
            this.cmbSpec.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Class";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Spec";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Rotation";
            // 
            // txtRotationPath
            // 
            this.txtRotationPath.Location = new System.Drawing.Point(70, 70);
            this.txtRotationPath.Name = "txtRotationPath";
            this.txtRotationPath.Size = new System.Drawing.Size(545, 20);
            this.txtRotationPath.TabIndex = 7;
            // 
            // chkConfirm
            // 
            this.chkConfirm.AutoSize = true;
            this.chkConfirm.Location = new System.Drawing.Point(70, 98);
            this.chkConfirm.Name = "chkConfirm";
            this.chkConfirm.Size = new System.Drawing.Size(329, 17);
            this.chkConfirm.TabIndex = 8;
            this.chkConfirm.Text = "I confirm that my keybindings match those setup in the spellbook";
            this.chkConfirm.UseVisualStyleBackColor = true;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(70, 121);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(545, 116);
            this.txtComments.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Comments";
            // 
            // frmSubmitTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 287);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkConfirm);
            this.Controls.Add(this.txtRotationPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSpec);
            this.Controls.Add(this.cmbClass);
            this.Controls.Add(this.cmdSubmitTicket);
            this.Name = "frmSubmitTicket";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Submit Ticket";
            this.Load += new System.EventHandler(this.frmSubmitTicket_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSubmitTicket;
        private System.Windows.Forms.ComboBox cmbClass;
        private System.Windows.Forms.ComboBox cmbSpec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRotationPath;
        private System.Windows.Forms.CheckBox chkConfirm;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label label4;
    }
}