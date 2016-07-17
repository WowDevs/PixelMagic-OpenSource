//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

// Icon Backlink: http://icons8.com/ (http://www.iconarchive.com/show/ios7-icons-by-icons8/Animals-Ant-icon.html)

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using Microsoft.CSharp;
using PixelMagic.Helpers;
using PixelMagic.Rotation;

// ReSharper disable once CheckNamespace
namespace PixelMagic.GUI
{
    public partial class frmMain : Form
    {
        internal static CombatRoutine combatRoutine;
        private readonly Dictionary<int, string> classes;
        private KeyboardHook hook;

        public static string Exe_Version
        {
            get
            {
                return System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetEntryAssembly().Location).ToString("yyyy.MM.dd");
            }
        }

        private readonly int LocalVersion = int.Parse(Application.ProductVersion.Split('.')[0]);

        internal frmMain()
        {
            InitializeComponent();
            
            classes = new Dictionary<int, string>
            {
                { 1, "Warrior"},
                { 2, "Paladin"},
                { 3, "Hunter"},
                { 4, "Rogue"},
                { 5, "Priest"},
                { 6, "DeathKnight"},
                { 7, "Shaman"},
                { 8, "Mage"},
                { 9, "Warlock"},
                { 10, "Monk"},
                { 11, "Druid"},
                { 12, "DemonHunter"}
            };
        }

        private static string OperatingSystem
        {
            get
            {
                var result = string.Empty;

                var moc = new ManagementObjectSearcher(@"SELECT * FROM Win32_OperatingSystem ");
                foreach (var managementBaseObject in moc.Get())
                {
                    var o = (ManagementObject) managementBaseObject;
                    var x64 = Environment.Is64BitOperatingSystem ? "(x64)" : "(x86)";
                    result = $@"{o["Caption"]} {x64} Version {o["Version"]} SP {o["ServicePackMajorVersion"]}.{o["ServicePackMinorVersion"]}";
                    break;
                }

                return result.Replace("Microsoft", "").Trim();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = string.Format(toolStripStatusLabel1.Text, Exe_Version);

            // Its annoying as hell when people use incorrect culture info, this will force it to use the correct number and date formats.
            var ci = new CultureInfo("en-ZA") { DateTimeFormat = {ShortDatePattern = "yyyy/MM/dd"}, NumberFormat = { NumberDecimalSeparator = ".", CurrencyDecimalSeparator = "." } };
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            FormClosing += FrmMain_FormClosing;
            Shown += FrmMain_Shown;
            Log.Initialize(rtbLog, this);

            Log.WritePixelMagic("Welcome to PixelMagic Premium Edition developed by WiNiFiX (BETA)", Color.Blue);
            Log.WriteNoTime("For support please visit: http://goo.gl/0AqNxv");
            Log.WriteNoTime("To view a sample rotation see the file: " + Application.StartupPath + "\\Rotations\\Hunter\\Hunter.cs", Color.Gray);
            Log.HorizontalLine = "-".PadLeft(136, '-');
            Log.DrawHorizontalLine();
        }

        private bool LoadProfile(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                var code = sr.ReadToEnd();

                if (code.Trim() == "")
                {
                    MessageBox.Show("Please select a non blank file", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (fileName.EndsWith(".enc"))
                {
                    Log.Write("Decrypting profile...", Color.Black);

                    try
                    {
                        code = Encryption.Decrypt(code);

                        Log.Write("Profile has been decrypted successfully", Color.Green);
                    }
                    catch (Exception ex)
                    {
                        Log.Write(ex.Message, Color.Red);
                    }
                }

                Log.Write($"Compiling profile [{fileName}]...", Color.Black);

                var provider = new CSharpCodeProvider();
                var parameters = new CompilerParameters();

                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll"); // For Windows Forms use
                parameters.ReferencedAssemblies.Add("System.Drawing.dll"); // For System.Drawing.Point and System.Drawing.Color use
                parameters.ReferencedAssemblies.Add("System.Data.dll");
                parameters.ReferencedAssemblies.Add("System.Xml.dll");
                parameters.ReferencedAssemblies.Add("System.Linq.dll");
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Threading.dll");
                parameters.ReferencedAssemblies.Add(Application.ExecutablePath);
                parameters.GenerateInMemory = true;
                parameters.GenerateExecutable = false;

                var results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError error in results.Errors)
                    {
                        Log.Write($"Error ({error.ErrorNumber}): {error.ErrorText}", Color.Red);
                    }

                    return false;
                }

                var assembly = results.CompiledAssembly;

                foreach (var t in assembly.GetTypes())
                {
                    if (t.IsClass)
                    {
                        var obj = Activator.CreateInstance(t);
                        combatRoutine = (CombatRoutine)obj;

                        combatRoutine.Load(this);
                        combatRoutine.FileName = fileName;

                        Log.Write("Successfully loaded combat routine: " + combatRoutine.Name, Color.Green);

                        Overlay.showOverlay(new Point(20, 680));
                 
                        if (SpellBook.Initialize(fileName))
                        {
                            spellbookToolStripMenuItem.Enabled = true;
                            submitTicketToolStripMenuItem.Enabled = true;

                            cmdStartBot.Enabled = true;
                            cmdStartBot.BackColor = Color.LightGreen;
                            return true;
                        }
                        else
                        {
                            spellbookToolStripMenuItem.Enabled = false;
                            submitTicketToolStripMenuItem.Enabled = false;

                            cmdStartBot.Enabled = false;
                            cmdStartBot.BackColor = Color.WhiteSmoke;
                            return false;
                        }                        
                    }
                }

                return false;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Log.Write("Performing Cleanup, application closing...");
                Log.Write(" - Keyboard Hotkey Hooks...");
                hook?.Dispose();
                Log.Write(" - Done.");
                Log.Write(" - Combat Routine...");
                combatRoutine?.Dispose();
                Log.Write(" - Done");
                Log.Write(" - WoW Pixel Reading System...");
                WoW.Dispose();
                Log.Write(" - Done");
                Log.Write(" - Mouse Hook");
                MouseHook.ForceUnsunscribeFromGlobalMouseEvents();
                Log.Write(" - Done");
                Log.Write("Cleanup Completed.");
                e.Cancel = false;
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, Color.Red);
            }
        }

        private void ReloadHotkeys()
        {
            hook?.Dispose();

            hook = new KeyboardHook();
            hook.KeyPressed += Hook_KeyPressed;

            MouseHook.MouseClick += MouseHook_MouseClick;

            chkPlayErrorSounds.Checked = ConfigFile.PlayErrorSounds;

            if (ConfigFile.ReadValue("Hotkeys", "cmbStartRotationKey") != "")
            {
                hook.RegisterHotKey(Keyboard.StartRotationModifierKey, Keyboard.StartRotationKey, "Start Rotation");

                if (Keyboard.StartRotationModifierKey != Keyboard.StopRotationModifierKey || Keyboard.StartRotationKey != Keyboard.StopRotationKey)
                    hook.RegisterHotKey(Keyboard.StopRotationModifierKey, Keyboard.StopRotationKey, "Stop Rotation");

                hook.RegisterHotKey(Keyboard.SingleTargetModifierKey, Keyboard.SingleTargetKey, "Single Target");

                if (Keyboard.SingleTargetModifierKey != Keyboard.AOEModifierKey || Keyboard.SingleTargetKey != Keyboard.AOEKey)
                    hook.RegisterHotKey(Keyboard.AOEModifierKey, Keyboard.AOEKey, "AOE Targets");
            }
            else
            {
                // Defaults - Hotkeys not setup

                hook.RegisterHotKey(Helpers.ModifierKeys.Ctrl, Keys.S, "Start / Stop Rotation");
                hook.RegisterHotKey(Helpers.ModifierKeys.Alt, Keys.S, "Single Target");
                hook.RegisterHotKey(Helpers.ModifierKeys.Alt, Keys.A, "AOE Targets");
            }
        }

        private void MouseHook_MouseClick(object sender, MouseEventArgs e)
        {
            txtMouseXYClick.Text = $"{e.X}, {e.Y}";

            //if (Debugger.IsAttached)
            //{
            //    Log.Write($"Cursor clicked at (x, y) = {e.X}, {e.Y}");
            //}
        }

        public Process process;

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                ConfigFile.Initialize();
                
                Log.Write(OperatingSystem);

                var i = 0;
                foreach (var screen in Screen.AllScreens)
                {
                    i++;
                    Log.Write($"Screen [{i}] - depth: {screen.BitsPerPixel}bit - resolution: {screen.Bounds.Width}x{screen.Bounds.Height}");
                }
                
                foreach (var item in classes)
                {
                    if (!Directory.Exists(Application.StartupPath + "\\Rotations\\" + item.Value))
                        Directory.CreateDirectory(Application.StartupPath + "\\Rotations\\" + item.Value);
                }

                ReloadHotkeys();

                nudPulse.Value = ConfigFile.Pulse;

                GUI.SelectWoWProcessToAttachTo f = new GUI.SelectWoWProcessToAttachTo(this);
                f.ShowDialog();
                
                if (process == null)
                {
                    Close();
                }

                WoW.Initialize(process);

                Log.Write("WoW Path: " + WoW.InstallPath, Color.Gray);
                Log.Write("AddOn Path: " + WoW.AddonPath, Color.Gray);

                var mousePos = new Thread(delegate ()
                {
                    while (true)
                    {
                        Threads.UpdateTextBox(txtMouseXY, Cursor.Position.X + "," + Cursor.Position.Y);
                        Thread.Sleep(10);
                    }
                    // ReSharper disable once FunctionNeverReturns
                })
                { IsBackground = true };
                mousePos.Start();
                                
                Log.Write("Please select a rotation to load from 'File' -> 'Load Rotation...'", Color.Green);
                Log.Write("Please note that you can only start bot or setup spellbook once you have loaded a rotation", Color.Black);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, Color.Red);
            }
        }

        private void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            lblHotkeyInfo.Text = e.Modifier + " + " + e.Key;

            if (ConfigFile.ReadValue("Hotkeys", "cmbStartRotationKey") != "")
            {
                if (e.Modifier == Keyboard.StartRotationModifierKey && e.Key == Keyboard.StartRotationKey)
                {
                    cmdStartBot_Click(null, null);
                    return;
                }

                if (e.Modifier == Keyboard.StopRotationModifierKey && e.Key == Keyboard.StopRotationKey)
                {
                    cmdStartBot_Click(null, null);
                    return;
                }

                if (combatRoutine == null)
                    return;

                if (e.Modifier == Keyboard.SingleTargetModifierKey && e.Key == Keyboard.SingleTargetKey)
                {
                    if (combatRoutine.Type != CombatRoutine.RotationType.SingleTarget)
                    {
                        combatRoutine.ChangeType(CombatRoutine.RotationType.SingleTarget);
                        return;
                    }
                }

                if (e.Modifier == Keyboard.AOEModifierKey && e.Key == Keyboard.AOEKey)
                {
                    if (combatRoutine.Type != CombatRoutine.RotationType.AOE)
                    {
                        combatRoutine.ChangeType(CombatRoutine.RotationType.AOE);
                    }
                }
            }
            else
            {
                if (e.Modifier == Helpers.ModifierKeys.Ctrl)
                {
                    if (e.Key == Keys.S)
                    {
                        cmdStartBot_Click(null, null);
                    }
                }

                if (e.Modifier == Helpers.ModifierKeys.Alt)
                {
                    if (e.Key == Keys.S)
                    {
                        combatRoutine.ChangeType(CombatRoutine.RotationType.SingleTarget);
                    }

                    if (e.Key == Keys.A)
                    {
                        combatRoutine.ChangeType(CombatRoutine.RotationType.AOE);
                    }
                }
            }
        }

        private void cmdStartBot_Click(object sender, EventArgs e)
        {
            if (combatRoutine == null)
            {
                MessageBox.Show("Please select a rotation to load before starting the bot.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (combatRoutine.State == CombatRoutine.RotationState.Stopped)
            {
                combatRoutine.Start();

                if (combatRoutine.State == CombatRoutine.RotationState.Running)
                {
                    cmdStartBot.Text = "Stop bot";
                    cmdStartBot.BackColor = Color.Salmon;
                }
            }
            else
            {
                combatRoutine.Pause();
                cmdStartBot.Text = "Start bot";
                cmdStartBot.BackColor = Color.LightGreen;
            }
        }

        private void cmdDonate_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=CPDNWHKSVWGKA");
        }

        private void loadRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (combatRoutine?.State == CombatRoutine.RotationState.Running)
            {
                combatRoutine?.Pause();
            }

            var fileBrowser = new OpenFileDialog
            {
                Filter = "CS (*.cs)|*.cs|ENC (*.enc)|*.enc",
                InitialDirectory = Application.StartupPath + "\\Rotations"
            };
            var res = fileBrowser.ShowDialog();

            if (res == DialogResult.OK)
            {
                if (!LoadProfile(fileBrowser.FileName))
                {
                    Log.Write("Failed to load profile, please select a valid file.", Color.Red);
                }
                else
                {
                    // We loaded the profile successfully, save it as the current profile
                    ConfigFile.WriteValue("PixelMagic", "LastProfile", fileBrowser.FileName);
                }
            }
        }        

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (combatRoutine.State == CombatRoutine.RotationState.Running)
            {
                combatRoutine.Pause();
            }

            Application.Exit();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check versions                               
            Log.Write("Latest GitHub version: " + GitHubVersion);
            Log.Write("Current version: " + LocalVersion);

            if (GitHubVersion > LocalVersion)
            {
                Log.Write("Please note you are not running the latest version of the bot, please update it.", Color.Red);
            }
        }

        private void encryptCombatRoutineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfigFile.LastRotation.EndsWith(".enc"))
            {
                Log.Write("The currently selected routine is already encrypted", Color.Red);
                return;
            }

            if (ConfigFile.LastRotation != "")
            {
                try
                {
                    string rotationSource;

                    using (var sr = new StreamReader(ConfigFile.LastRotation))
                    {
                        var contents = sr.ReadToEnd();

                        var line1 = contents.Split('\r')[0].Trim();

                        if (!line1.Contains("@"))
                        {
                            throw new Exception("You are not permitted to encrypt a combat routine if you have not yet specified an email address on the top line of the routine");
                        }

                        rotationSource = Encryption.Encrypt(contents);
                    }

                    using (var sw = new StreamWriter(ConfigFile.LastRotation.Replace(".cs", ".enc")))
                    {
                        sw.Write(rotationSource);
                        sw.Flush();
                    }

                    Log.Write("File has beem encrypted successfully.", Color.Green);
                    Log.Write("Encrypted name: " + ConfigFile.LastRotation.Replace(".cs", ".enc"), Color.Green);
                }
                catch (Exception ex)
                {
                    Log.Write(ex.Message, Color.Red);
                }
            }
        }

        private void hotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new SetupHotkeys();
            f.ShowDialog();

            ReloadHotkeys();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //WoW.LeftClick(504, 982);
        }

        private void spellbookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new SetupSpellBook();
            f.ShowDialog();

            WoW.SendMacro("/console scriptErrors 1");   // Show wow Lua errors
            WoW.SendMacro("/reload");
        }

        private void chkPlayErrorSounds_CheckedChanged(object sender, EventArgs e)
        {
            ConfigFile.PlayErrorSounds = chkPlayErrorSounds.Checked;
        }

        private void chkDisableOverlay_CheckedChanged(object sender, EventArgs e)
        {
            ConfigFile.DisableOverlay = chkDisableOverlay.Checked;
        }

        private void nudPulse_ValueChanged(object sender, EventArgs e)
        {
            ConfigFile.Pulse = nudPulse.Value;

            combatRoutine?.ForcePulseUpdate();
        }

        #region Get GIT Version

        private int _gitVersion;

        private int GitHubVersion
        {
            get
            {
                if (_gitVersion == 0)
                {
                    try
                    {
                        var versionInfo = Web.GetString("https://raw.githubusercontent.com/winifix/Pixel-Bot-Sample-Application/master/AntiVirusBeta/Properties/AssemblyInfo.cs").
                            Split('\r').
                            FirstOrDefault(r => r.Contains("AssemblyFileVersion"))?.
                            Replace("\n", "").
                            Replace("[assembly: AssemblyFileVersion(\"", "").
                            Replace("\")]", "").
                            Split('.')[0];

                        if (versionInfo != null) _gitVersion = int.Parse(versionInfo);
                    }
                    catch
                    {
                        _gitVersion = 0;
                    }
                }
                return _gitVersion;
            }
        }

        #endregion

        private void reloadAddonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WoW.SendMacro("/console scriptErrors 1");   // Show wow Lua errors
            WoW.SendMacro("/reload");
        }

        private void rtbLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void submitTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI.frmSubmitTicket f = new GUI.frmSubmitTicket();
            f.ShowDialog();
        }
    }
}