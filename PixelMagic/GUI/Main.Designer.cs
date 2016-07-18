namespace PixelMagic.GUI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cmdStartBot = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lblHotkeyInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTargetHealth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTargetCasting = new System.Windows.Forms.TextBox();
            this.cmdDonate = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spellbookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadAddonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptCombatRoutineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpPlayer = new System.Windows.Forms.GroupBox();
            this.grpTarget = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grpMousePosition = new System.Windows.Forms.GroupBox();
            this.txtMouseXY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkPlayErrorSounds = new System.Windows.Forms.CheckBox();
            this.chkDisableOverlay = new System.Windows.Forms.CheckBox();
            this.grpMouseClickPosition = new System.Windows.Forms.GroupBox();
            this.txtMouseXYClick = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudPulse = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgPower = new ColorProgressBar.ColorProgressBar();
            this.prgPlayerHealth = new ColorProgressBar.ColorProgressBar();
            this.menuStrip1.SuspendLayout();
            this.grpPlayer.SuspendLayout();
            this.grpTarget.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpMousePosition.SuspendLayout();
            this.grpMouseClickPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPulse)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdStartBot
            // 
            this.cmdStartBot.Enabled = false;
            this.cmdStartBot.ForeColor = System.Drawing.Color.Black;
            this.cmdStartBot.Location = new System.Drawing.Point(13, 21);
            this.cmdStartBot.Name = "cmdStartBot";
            this.cmdStartBot.Size = new System.Drawing.Size(128, 31);
            this.cmdStartBot.TabIndex = 1;
            this.cmdStartBot.Text = "Start bot";
            this.cmdStartBot.UseVisualStyleBackColor = false;
            this.cmdStartBot.Click += new System.EventHandler(this.cmdStartBot_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLog.BackColor = System.Drawing.Color.White;
            this.rtbLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rtbLog.Location = new System.Drawing.Point(0, 27);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbLog.Size = new System.Drawing.Size(816, 513);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            this.rtbLog.TextChanged += new System.EventHandler(this.rtbLog_TextChanged);
            // 
            // lblHotkeyInfo
            // 
            this.lblHotkeyInfo.AutoSize = true;
            this.lblHotkeyInfo.Location = new System.Drawing.Point(476, 312);
            this.lblHotkeyInfo.Name = "lblHotkeyInfo";
            this.lblHotkeyInfo.Size = new System.Drawing.Size(68, 13);
            this.lblHotkeyInfo.TabIndex = 3;
            this.lblHotkeyInfo.Text = "[Hotkey Info]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Health";
            // 
            // txtTargetHealth
            // 
            this.txtTargetHealth.ForeColor = System.Drawing.Color.Black;
            this.txtTargetHealth.Location = new System.Drawing.Point(55, 19);
            this.txtTargetHealth.Name = "txtTargetHealth";
            this.txtTargetHealth.ReadOnly = true;
            this.txtTargetHealth.Size = new System.Drawing.Size(88, 20);
            this.txtTargetHealth.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(11, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Casting";
            // 
            // txtTargetCasting
            // 
            this.txtTargetCasting.ForeColor = System.Drawing.Color.Black;
            this.txtTargetCasting.Location = new System.Drawing.Point(55, 41);
            this.txtTargetCasting.Name = "txtTargetCasting";
            this.txtTargetCasting.ReadOnly = true;
            this.txtTargetCasting.Size = new System.Drawing.Size(88, 20);
            this.txtTargetCasting.TabIndex = 11;
            // 
            // cmdDonate
            // 
            this.cmdDonate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDonate.BackColor = System.Drawing.Color.White;
            this.cmdDonate.ForeColor = System.Drawing.Color.DimGray;
            this.cmdDonate.Location = new System.Drawing.Point(838, 493);
            this.cmdDonate.Name = "cmdDonate";
            this.cmdDonate.Size = new System.Drawing.Size(128, 31);
            this.cmdDonate.TabIndex = 12;
            this.cmdDonate.Text = "Donate";
            this.cmdDonate.UseVisualStyleBackColor = false;
            this.cmdDonate.Click += new System.EventHandler(this.cmdDonate_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.setupToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRotationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Image = global::PixelMagic.GUI.Properties.Resources.Project_16x16;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadRotationToolStripMenuItem
            // 
            this.loadRotationToolStripMenuItem.Name = "loadRotationToolStripMenuItem";
            this.loadRotationToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.loadRotationToolStripMenuItem.Text = "Load Rotation...";
            this.loadRotationToolStripMenuItem.Click += new System.EventHandler(this.loadRotationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hotkeysToolStripMenuItem,
            this.spellbookToolStripMenuItem,
            this.reloadAddonToolStripMenuItem});
            this.setupToolStripMenuItem.Image = global::PixelMagic.GUI.Properties.Resources.PageSetup_16x16;
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.setupToolStripMenuItem.Text = "Setup";
            // 
            // hotkeysToolStripMenuItem
            // 
            this.hotkeysToolStripMenuItem.Name = "hotkeysToolStripMenuItem";
            this.hotkeysToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.hotkeysToolStripMenuItem.Text = "Hotkeys";
            this.hotkeysToolStripMenuItem.Click += new System.EventHandler(this.hotkeysToolStripMenuItem_Click);
            // 
            // spellbookToolStripMenuItem
            // 
            this.spellbookToolStripMenuItem.Enabled = false;
            this.spellbookToolStripMenuItem.Name = "spellbookToolStripMenuItem";
            this.spellbookToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.spellbookToolStripMenuItem.Text = "Spellbook";
            this.spellbookToolStripMenuItem.Click += new System.EventHandler(this.spellbookToolStripMenuItem_Click);
            // 
            // reloadAddonToolStripMenuItem
            // 
            this.reloadAddonToolStripMenuItem.Name = "reloadAddonToolStripMenuItem";
            this.reloadAddonToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.reloadAddonToolStripMenuItem.Text = "Reload Addon";
            this.reloadAddonToolStripMenuItem.Click += new System.EventHandler(this.reloadAddonToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encryptCombatRoutineToolStripMenuItem});
            this.toolsToolStripMenuItem.Image = global::PixelMagic.GUI.Properties.Resources.Version_16x16;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // encryptCombatRoutineToolStripMenuItem
            // 
            this.encryptCombatRoutineToolStripMenuItem.Name = "encryptCombatRoutineToolStripMenuItem";
            this.encryptCombatRoutineToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.encryptCombatRoutineToolStripMenuItem.Text = "Encrypt Combat Routine";
            this.encryptCombatRoutineToolStripMenuItem.Click += new System.EventHandler(this.encryptCombatRoutineToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.submitTicketToolStripMenuItem});
            this.helpToolStripMenuItem.Image = global::PixelMagic.GUI.Properties.Resources.help;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates...";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // submitTicketToolStripMenuItem
            // 
            this.submitTicketToolStripMenuItem.Enabled = false;
            this.submitTicketToolStripMenuItem.Name = "submitTicketToolStripMenuItem";
            this.submitTicketToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.submitTicketToolStripMenuItem.Text = "Submit Ticket";
            this.submitTicketToolStripMenuItem.Click += new System.EventHandler(this.submitTicketToolStripMenuItem_Click);
            // 
            // grpPlayer
            // 
            this.grpPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPlayer.Controls.Add(this.prgPower);
            this.grpPlayer.Controls.Add(this.prgPlayerHealth);
            this.grpPlayer.ForeColor = System.Drawing.Color.Black;
            this.grpPlayer.Location = new System.Drawing.Point(825, 96);
            this.grpPlayer.Name = "grpPlayer";
            this.grpPlayer.Size = new System.Drawing.Size(151, 74);
            this.grpPlayer.TabIndex = 14;
            this.grpPlayer.TabStop = false;
            this.grpPlayer.Text = "Player";
            // 
            // grpTarget
            // 
            this.grpTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTarget.Controls.Add(this.txtTargetHealth);
            this.grpTarget.Controls.Add(this.label1);
            this.grpTarget.Controls.Add(this.label4);
            this.grpTarget.Controls.Add(this.txtTargetCasting);
            this.grpTarget.ForeColor = System.Drawing.Color.Black;
            this.grpTarget.Location = new System.Drawing.Point(825, 175);
            this.grpTarget.Name = "grpTarget";
            this.grpTarget.Size = new System.Drawing.Size(151, 74);
            this.grpTarget.TabIndex = 15;
            this.grpTarget.TabStop = false;
            this.grpTarget.Text = "Target";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cmdStartBot);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(825, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(151, 66);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pixel Magic";
            // 
            // grpMousePosition
            // 
            this.grpMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMousePosition.Controls.Add(this.txtMouseXY);
            this.grpMousePosition.Controls.Add(this.label5);
            this.grpMousePosition.Controls.Add(this.label6);
            this.grpMousePosition.ForeColor = System.Drawing.Color.Black;
            this.grpMousePosition.Location = new System.Drawing.Point(825, 255);
            this.grpMousePosition.Name = "grpMousePosition";
            this.grpMousePosition.Size = new System.Drawing.Size(151, 51);
            this.grpMousePosition.TabIndex = 17;
            this.grpMousePosition.TabStop = false;
            this.grpMousePosition.Text = "Mouse Position";
            // 
            // txtMouseXY
            // 
            this.txtMouseXY.ForeColor = System.Drawing.Color.Black;
            this.txtMouseXY.Location = new System.Drawing.Point(55, 19);
            this.txtMouseXY.Name = "txtMouseXY";
            this.txtMouseXY.ReadOnly = true;
            this.txtMouseXY.Size = new System.Drawing.Size(88, 20);
            this.txtMouseXY.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(11, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "X, Y = ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 10;
            // 
            // chkPlayErrorSounds
            // 
            this.chkPlayErrorSounds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPlayErrorSounds.AutoSize = true;
            this.chkPlayErrorSounds.Checked = true;
            this.chkPlayErrorSounds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlayErrorSounds.ForeColor = System.Drawing.Color.Black;
            this.chkPlayErrorSounds.Location = new System.Drawing.Point(840, 382);
            this.chkPlayErrorSounds.Name = "chkPlayErrorSounds";
            this.chkPlayErrorSounds.Size = new System.Drawing.Size(110, 17);
            this.chkPlayErrorSounds.TabIndex = 18;
            this.chkPlayErrorSounds.Text = "Play Error Sounds";
            this.chkPlayErrorSounds.UseVisualStyleBackColor = true;
            this.chkPlayErrorSounds.CheckedChanged += new System.EventHandler(this.chkPlayErrorSounds_CheckedChanged);
            // 
            // chkDisableOverlay
            // 
            this.chkDisableOverlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDisableOverlay.AutoSize = true;
            this.chkDisableOverlay.Checked = true;
            this.chkDisableOverlay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisableOverlay.ForeColor = System.Drawing.Color.Black;
            this.chkDisableOverlay.Location = new System.Drawing.Point(840, 405);
            this.chkDisableOverlay.Name = "chkDisableOverlay";
            this.chkDisableOverlay.Size = new System.Drawing.Size(100, 17);
            this.chkDisableOverlay.TabIndex = 19;
            this.chkDisableOverlay.Text = "Disable Overlay";
            this.chkDisableOverlay.UseVisualStyleBackColor = true;
            this.chkDisableOverlay.CheckedChanged += new System.EventHandler(this.chkDisableOverlay_CheckedChanged);
            // 
            // grpMouseClickPosition
            // 
            this.grpMouseClickPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMouseClickPosition.Controls.Add(this.txtMouseXYClick);
            this.grpMouseClickPosition.Controls.Add(this.label7);
            this.grpMouseClickPosition.ForeColor = System.Drawing.Color.Black;
            this.grpMouseClickPosition.Location = new System.Drawing.Point(825, 309);
            this.grpMouseClickPosition.Name = "grpMouseClickPosition";
            this.grpMouseClickPosition.Size = new System.Drawing.Size(151, 51);
            this.grpMouseClickPosition.TabIndex = 20;
            this.grpMouseClickPosition.TabStop = false;
            this.grpMouseClickPosition.Text = "Last Mouse Click Position";
            // 
            // txtMouseXYClick
            // 
            this.txtMouseXYClick.ForeColor = System.Drawing.Color.Black;
            this.txtMouseXYClick.Location = new System.Drawing.Point(55, 19);
            this.txtMouseXYClick.Name = "txtMouseXYClick";
            this.txtMouseXYClick.ReadOnly = true;
            this.txtMouseXYClick.Size = new System.Drawing.Size(88, 20);
            this.txtMouseXYClick.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(11, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "X, Y = ";
            // 
            // nudPulse
            // 
            this.nudPulse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPulse.ForeColor = System.Drawing.Color.Black;
            this.nudPulse.Location = new System.Drawing.Point(896, 437);
            this.nudPulse.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPulse.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPulse.Name = "nudPulse";
            this.nudPulse.Size = new System.Drawing.Size(66, 20);
            this.nudPulse.TabIndex = 21;
            this.nudPulse.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPulse.ValueChanged += new System.EventHandler(this.nudPulse_ValueChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(835, 439);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Pulse (ms)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 537);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 24);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(195, 19);
            this.toolStripStatusLabel2.Text = "PixelMagic (developed by WiNiFiX)";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(85, 19);
            this.toolStripStatusLabel1.Text = "Build Date: {0}";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(69, 19);
            this.toolStripStatusLabel3.Text = "Version: {0}";
            // 
            // prgPower
            // 
            this.prgPower.BackColor = System.Drawing.Color.White;
            this.prgPower.BarColor = System.Drawing.Color.PaleTurquoise;
            this.prgPower.BorderColor = System.Drawing.Color.Green;
            this.prgPower.FillStyle = ColorProgressBar.ColorProgressBar.FillStyles.Solid;
            this.prgPower.ForeColor = System.Drawing.Color.White;
            this.prgPower.Location = new System.Drawing.Point(13, 45);
            this.prgPower.Maximum = 100;
            this.prgPower.Minimum = 0;
            this.prgPower.Name = "prgPower";
            this.prgPower.Size = new System.Drawing.Size(128, 20);
            this.prgPower.Step = 10;
            this.prgPower.TabIndex = 25;
            this.prgPower.Value = 70;
            // 
            // prgPlayerHealth
            // 
            this.prgPlayerHealth.BackColor = System.Drawing.Color.White;
            this.prgPlayerHealth.BarColor = System.Drawing.Color.Salmon;
            this.prgPlayerHealth.BorderColor = System.Drawing.Color.Green;
            this.prgPlayerHealth.FillStyle = ColorProgressBar.ColorProgressBar.FillStyles.Solid;
            this.prgPlayerHealth.ForeColor = System.Drawing.Color.White;
            this.prgPlayerHealth.Location = new System.Drawing.Point(13, 19);
            this.prgPlayerHealth.Maximum = 100;
            this.prgPlayerHealth.Minimum = 0;
            this.prgPlayerHealth.Name = "prgPlayerHealth";
            this.prgPlayerHealth.Size = new System.Drawing.Size(128, 20);
            this.prgPlayerHealth.Step = 10;
            this.prgPlayerHealth.TabIndex = 24;
            this.prgPlayerHealth.Value = 70;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nudPulse);
            this.Controls.Add(this.grpMouseClickPosition);
            this.Controls.Add(this.chkDisableOverlay);
            this.Controls.Add(this.chkPlayErrorSounds);
            this.Controls.Add(this.grpMousePosition);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpTarget);
            this.Controls.Add(this.grpPlayer);
            this.Controls.Add(this.cmdDonate);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.lblHotkeyInfo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpPlayer.ResumeLayout(false);
            this.grpTarget.ResumeLayout(false);
            this.grpTarget.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.grpMousePosition.ResumeLayout(false);
            this.grpMousePosition.PerformLayout();
            this.grpMouseClickPosition.ResumeLayout(false);
            this.grpMouseClickPosition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPulse)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdStartBot;
        private System.Windows.Forms.Label lblHotkeyInfo;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTargetHealth;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtTargetCasting;
        private System.Windows.Forms.Button cmdDonate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpPlayer;
        private System.Windows.Forms.GroupBox grpTarget;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encryptCombatRoutineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hotkeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spellbookToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpMousePosition;
        private System.Windows.Forms.TextBox txtMouseXY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.CheckBox chkPlayErrorSounds;
        public System.Windows.Forms.CheckBox chkDisableOverlay;
        private System.Windows.Forms.GroupBox grpMouseClickPosition;
        private System.Windows.Forms.TextBox txtMouseXYClick;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudPulse;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem reloadAddonToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem submitTicketToolStripMenuItem;
        internal System.Windows.Forms.RichTextBox rtbLog;
        public ColorProgressBar.ColorProgressBar prgPlayerHealth;
        public ColorProgressBar.ColorProgressBar prgPower;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    }
}

