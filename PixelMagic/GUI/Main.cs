//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

// Icon Backlink: http://icons8.com/ (http://www.iconarchive.com/show/ios7-icons-by-icons8/Animals-Ant-icon.html)

using Microsoft.CSharp;
using PixelMagic.Helpers;
using PixelMagic.Rotation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.GUI
{
    public partial class frmMain : Form
    {
        internal static CombatRoutine combatRoutine;
        internal KeyboardHook hook;
        internal Dictionary<int, string> classes;

        internal frmMain()
        {
            InitializeComponent();

            classes = new Dictionary<int, string>();
            classes.Add(1, "Warrior");
            classes.Add(2, "Paladin");
            classes.Add(3, "Hunter");
            classes.Add(4, "Rogue");
            classes.Add(5, "Priest");
            classes.Add(6, "DeathKnight");
            classes.Add(7, "Shaman");
            classes.Add(8, "Mage");
            classes.Add(9, "Warlock");
            classes.Add(10, "Monk");
            classes.Add(11, "Druid");
            classes.Add(12, "DemonHunter");
        }

        #region Get GIT Version
        private int _gitVersion = 0;

        private int GitHubVersion
        {
            get
            {
                if (_gitVersion == 0)
                {
                    try
                    {
                        string versionInfo = Web.GetString("https://raw.githubusercontent.com/winifix/Pixel-Bot-Sample-Application/master/AntiVirusBeta/Properties/AssemblyInfo.cs").
                            Split('\r').
                            FirstOrDefault(r => r.Contains("AssemblyFileVersion")).
                            Replace("\n", "").
                            Replace("[assembly: AssemblyFileVersion(\"", "").
                            Replace("\")]", "").
                            Split('.')[0];

                        _gitVersion = int.Parse(versionInfo);
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

        int LocalVersion = int.Parse(Application.ProductVersion.Split('.')[0].ToString());

        public bool PlayErrorSounds
        {
            get
            {
                return chkPlayErrorSounds.Checked;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            debuggingToolStripMenuItem.Visible = System.Diagnostics.Debugger.IsAttached;

            FormClosing += FrmMain_FormClosing;
            Shown += FrmMain_Shown;
            Log.Initialize(rtbLog, this);

            Log.WritePixelMagic("Welcome to PixelMagic Premium Edition developed by WiNiFiX (BETA)", Color.Blue);
            Log.WriteNoTime("For support please visit: http://goo.gl/0AqNxv");
            Log.HorizontalLine = "-".PadLeft(124, '-');
            Log.DrawHorizontalLine();
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
                Log.Write("Cleanup Completed.");
                e.Cancel = false;
            }
            catch(Exception ex)
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

                hook.RegisterHotKey(PixelMagic.Helpers.ModifierKeys.Ctrl, Keys.S, "Start / Stop Rotation");
                hook.RegisterHotKey(PixelMagic.Helpers.ModifierKeys.Alt, Keys.S, "Single Target");
                hook.RegisterHotKey(PixelMagic.Helpers.ModifierKeys.Alt, Keys.A, "AOE Targets");
            }
        }

        private void MouseHook_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.X > 1920)
            //    return;

            //if (WoW.HasFocus)
            //{
                txtMouseXYClick.Text = $"{e.X}, {e.Y}";
            //}

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Log.Write($"Cursor clicked at (x, y) = {e.X}, {e.Y}");
            }
        }

        public static string OperatingSystem
        {
            get
            {
                string result = string.Empty;
                
                var moc = new ManagementObjectSearcher(@"SELECT * FROM Win32_OperatingSystem ");
                foreach (ManagementObject o in moc.Get())
                {
                    var x64 = (Environment.Is64BitOperatingSystem ? "(x64)" : "(x86)");
                    result = $@"{o["Caption"]} {x64} Version {o["Version"]} SP {o["ServicePackMajorVersion"]}.{o["ServicePackMinorVersion"]}";
                    break;
                }

                return result.Replace("Microsoft", "").Trim();
            }
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                ConfigFile.Initialize();
                SpellBook.Initialize();

                Thread mousePos = new Thread(delegate ()
                {
                    while(true)
                    {
                        Threads.UpdateTextBox(txtMouseXY, Cursor.Position.X + "," + Cursor.Position.Y);
                        Thread.Sleep(10);
                    }
                });
                mousePos.IsBackground = true;
                mousePos.Start();

                Log.Write(OperatingSystem);
                Log.Write("WoW Path: " + WoW.InstallPath, Color.Black);
                Log.Write("AddOn Path: " + WoW.AddonPath, Color.Black);
                
                foreach (var item in classes)
                {
                    if (!Directory.Exists(Application.StartupPath + "\\Rotations\\" + item.Value))
                        Directory.CreateDirectory(Application.StartupPath + "\\Rotations\\" + item.Value);
                }
                
                ReloadHotkeys();

                nudPulse.Value = ConfigFile.Pulse;
                
                WoW.Initialize();

                if (ConfigFile.LastRotation == "")
                {
                    Log.Write("Please select a rotation to load from 'File' -> 'Load Rotation...'", Color.Green);
                }
                else
                {
                    Log.Write("Current Rotation: " + ConfigFile.LastRotation, Color.Green);

                    if (!LoadProfile(ConfigFile.LastRotation, txtHealth, txtPower, txtTargetCasting, txtTargetHealth))
                    {
                        Log.Write("Failed to load profile, please select a valid file.", Color.Red);
                        return;
                    }
                }

                //TestRotation rot = new TestRotation();
                //rot.Init(txtHealth, txtPower, txtTargetHealth, txtTargetCasting);
                //combatRoutine = rot.combatRoutine;
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, Color.Red);
            }
        }

        private void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            lblHotkeyInfo.Text = e.Modifier.ToString() + " + " + e.Key.ToString();

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
                        return;
                    }
                }
            }
            else
            {
                if (e.Modifier == PixelMagic.Helpers.ModifierKeys.Ctrl)
                {
                    if (e.Key == Keys.S)
                    {
                        cmdStartBot_Click(null, null);
                    }
                }

                if (e.Modifier == PixelMagic.Helpers.ModifierKeys.Alt)
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
                }
            }
            else
            {             
                combatRoutine.Pause();
                cmdStartBot.Text = "Start bot";
            }            
        }

        private void cmdDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=CPDNWHKSVWGKA");
        }

        private void loadRotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (combatRoutine?.State == CombatRoutine.RotationState.Running)
            {
                combatRoutine?.Pause();
            }

            OpenFileDialog fileBrowser = new OpenFileDialog();
            fileBrowser.Filter = "CS (*.cs)|*.cs|ENC (*.enc)|*.enc";
            fileBrowser.InitialDirectory = Application.StartupPath + "\\Rotations";
            DialogResult res = fileBrowser.ShowDialog();

            if (res == DialogResult.OK)
            {
                if (!LoadProfile(fileBrowser.FileName, txtHealth, txtPower, txtTargetCasting, txtTargetHealth))
                {
                    Log.Write("Failed to load profile, please select a valid file.", Color.Red);
                    return;
                }
                else
                {
                    // We loaded the profile successfully, save it as the current profile
                    ConfigFile.WriteValue("PixelMagic", "LastProfile", fileBrowser.FileName);
                }
            }
        }

        private bool LoadProfile(string fileName, TextBox txtHealth, TextBox txtPower, TextBox txtTargetHealth, TextBox txtTargetCasting)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string code = sr.ReadToEnd();

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

                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters();

                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");    // For Windows Forms use
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");          // For System.Drawing.Point and System.Drawing.Color use
                parameters.ReferencedAssemblies.Add("System.Data.dll");             
                parameters.ReferencedAssemblies.Add("System.Xml.dll");
                parameters.ReferencedAssemblies.Add("System.Linq.dll");
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Threading.dll");
                parameters.ReferencedAssemblies.Add(Application.ExecutablePath);
                parameters.GenerateInMemory = true;
                parameters.GenerateExecutable = false;

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (CompilerError error in results.Errors)
                    {
                        Log.Write($"Error ({error.ErrorNumber}): {error.ErrorText}", Color.Red);
                    }

                    return false;
                }

                Assembly assembly = results.CompiledAssembly;

                foreach (Type t in assembly.GetTypes())
                {
                    if (t.IsClass)
                    {
                        object obj = Activator.CreateInstance(t);
                        combatRoutine = (CombatRoutine)obj;
                        
                        combatRoutine.Load(this);

                        Log.Write("Successfully loaded combat routine: " + combatRoutine.Name, Color.Green);
                        
                        Overlay.showOverlay(new Point(20, 680));

                        return true;
                    }
                }

                return false;
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
                    string rotationSource = "";

                    using (StreamReader sr = new StreamReader(ConfigFile.LastRotation))
                    {
                        string contents = sr.ReadToEnd();

                        string line1 = contents.Split('\r')[0].Trim();

                        if (!line1.Contains("@"))
                        {
                            throw new Exception("You are not permitted to encrypt a combat routine if you have not yet specified an email address on the top line of the routine");
                        }

                        rotationSource = Encryption.Encrypt(contents);
                    }

                    using (StreamWriter sw = new StreamWriter(ConfigFile.LastRotation.Replace(".cs", ".enc")))
                    {
                        sw.Write(rotationSource);
                        sw.Flush();
                    }

                    Log.Write("File has beem encrypted successfully.", Color.Green);
                    Log.Write("Encrypted name: " + ConfigFile.LastRotation.Replace(".cs", ".enc"), Color.Green);
                }
                catch(Exception ex)
                {
                    Log.Write(ex.Message, Color.Red);
                }
            }
        }

        private void hotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupHotkeys f = new SetupHotkeys();
            f.ShowDialog();

            ReloadHotkeys();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //WoW.LeftClick(504, 982);
        }

        private void spellbookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupSpellBook f = new SetupSpellBook();
            f.ShowDialog();
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
    }
}
