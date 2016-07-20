//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

// ReSharper disable once CheckNamespace

namespace PixelMagic.Helpers
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class WoW
    {
        [Flags]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public enum Keys
        {
            A = 0x41,
            Add = 0x6b,
            Alt = 0x40000,
            Apps = 0x5d,
            Attn = 0xf6,
            B = 0x42,
            Back = 8,
            BrowserBack = 0xa6,
            BrowserFavorites = 0xab,
            BrowserForward = 0xa7,
            BrowserHome = 0xac,
            BrowserRefresh = 0xa8,
            BrowserSearch = 170,
            BrowserStop = 0xa9,
            C = 0x43,
            Cancel = 3,
            Capital = 20,
            CapsLock = 20,
            Clear = 12,
            Control = 0x20000,
            ControlKey = 0x11,
            Crsel = 0xf7,
            D = 0x44,
            D0 = 0x30,
            D1 = 0x31,
            D2 = 50,
            D3 = 0x33,
            D4 = 0x34,
            D5 = 0x35,
            D6 = 0x36,
            D7 = 0x37,
            D8 = 0x38,
            D9 = 0x39,
            Decimal = 110,
            Delete = 0x2e,
            Divide = 0x6f,
            Down = 40,
            E = 0x45,
            End = 0x23,
            Enter = 13,
            EraseEof = 0xf9,
            Escape = 0x1b,
            Execute = 0x2b,
            Exsel = 0xf8,
            F = 70,
            F1 = 0x70,
            F10 = 0x79,
            F11 = 0x7a,
            F12 = 0x7b,
            F13 = 0x7c,
            F14 = 0x7d,
            F15 = 0x7e,
            F16 = 0x7f,
            F17 = 0x80,
            F18 = 0x81,
            F19 = 130,
            F2 = 0x71,
            F20 = 0x83,
            F21 = 0x84,
            F22 = 0x85,
            F23 = 0x86,
            F24 = 0x87,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 120,
            FinalMode = 0x18,
            G = 0x47,
            H = 0x48,
            HanguelMode = 0x15,
            HangulMode = 0x15,
            HanjaMode = 0x19,
            Help = 0x2f,
            Home = 0x24,
            I = 0x49,
            ImeAccept = 30,
            ImeAceept = 30,
            ImeConvert = 0x1c,
            ImeModeChange = 0x1f,
            ImeNonconvert = 0x1d,
            Insert = 0x2d,
            J = 0x4a,
            JunjaMode = 0x17,
            K = 0x4b,
            KanaMode = 0x15,
            KanjiMode = 0x19,
            KeyCode = 0xffff,
            L = 0x4c,
            LaunchApplication1 = 0xb6,
            LaunchApplication2 = 0xb7,
            LaunchMail = 180,
            LButton = 1,
            LControlKey = 0xa2,
            Left = 0x25,
            LineFeed = 10,
            LMenu = 0xa4,
            LShiftKey = 160,
            LWin = 0x5b,
            M = 0x4d,
            MButton = 4,
            MediaNextTrack = 0xb0,
            MediaPlayPause = 0xb3,
            MediaPreviousTrack = 0xb1,
            MediaStop = 0xb2,
            Menu = 0x12,
            Modifiers = -65536,
            Multiply = 0x6a,
            N = 0x4e,
            Next = 0x22,
            NoName = 0xfc,
            None = 0,
            NumLock = 0x90,
            NumPad0 = 0x60,
            NumPad1 = 0x61,
            NumPad2 = 0x62,
            NumPad3 = 0x63,
            NumPad4 = 100,
            NumPad5 = 0x65,
            NumPad6 = 0x66,
            NumPad7 = 0x67,
            NumPad8 = 0x68,
            NumPad9 = 0x69,
            O = 0x4f,
            Oem1 = 0xba,
            Oem102 = 0xe2,
            Oem2 = 0xbf,
            Oem3 = 0xc0,
            Oem4 = 0xdb,
            Oem5 = 220,
            Oem6 = 0xdd,
            Oem7 = 0xde,
            Oem8 = 0xdf,
            OemBackslash = 0xe2,
            OemClear = 0xfe,
            OemCloseBrackets = 0xdd,
            Oemcomma = 0xbc,
            OemMinus = 0xbd,
            OemOpenBrackets = 0xdb,
            OemPeriod = 190,
            OemPipe = 220,
            Oemplus = 0xbb,
            OemQuestion = 0xbf,
            OemQuotes = 0xde,
            OemSemicolon = 0xba,
            Oemtilde = 0xc0,
            P = 80,
            Pa1 = 0xfd,
            Packet = 0xe7,
            PageDown = 0x22,
            PageUp = 0x21,
            Pause = 0x13,
            Play = 250,
            Print = 0x2a,
            PrintScreen = 0x2c,
            Prior = 0x21,
            ProcessKey = 0xe5,
            Q = 0x51,
            R = 0x52,
            RButton = 2,
            RControlKey = 0xa3,
            Return = 13,
            Right = 0x27,
            RMenu = 0xa5,
            RShiftKey = 0xa1,
            RWin = 0x5c,
            S = 0x53,
            Scroll = 0x91,
            Select = 0x29,
            SelectMedia = 0xb5,
            Separator = 0x6c,
            Shift = 0x10000,
            ShiftKey = 0x10,
            Sleep = 0x5f,
            Snapshot = 0x2c,
            Space = 0x20,
            Subtract = 0x6d,
            T = 0x54,
            Tab = 9,
            U = 0x55,
            Up = 0x26,
            V = 0x56,
            VolumeDown = 0xae,
            VolumeMute = 0xad,
            VolumeUp = 0xaf,
            W = 0x57,
            X = 0x58,
            XButton1 = 5,
            XButton2 = 6,
            Y = 0x59,
            Z = 90,
            Zoom = 0xfb
        }

        internal static Process pWow;
        private static Random random;
        private static readonly object thisLock = new object();
        private static readonly Bitmap screenPixel = new Bitmap(1, 1);
        private static DataTable dtColorHelper;
        
        public static void Initialize(Process wowProcess)
        {
            random = new Random();

            pWow = wowProcess;

            Log.Write("Successfully connected to WoW with process ID: " + pWow.Id, Color.Green);

            var is64 = pWow.ProcessName.Contains("64");

            Log.Write($"WoW Version: {Version} (x{(is64 ? "64" : "86")})", Color.Gray);

            var wowRectangle = new Rectangle();
            GetWindowRect(pWow.MainWindowHandle, ref wowRectangle);
            Log.Write($"WoW Screen Resolution: {wowRectangle.Width}x{wowRectangle.Height}", Color.Gray);

            if (ConfigFile.ReadValue("PixelMagic", "AddonName") == "")
            {
                Log.Write("This is the first time you have run the program, please specify a name you would like the PixelMagic addon to use");
                Log.Write("this can be anything you like (letters only no numbers)");

                while (ConfigFile.ReadValue("PixelMagic", "AddonName") == "")
                {
                    GUI.frmSelectAddonName f = new GUI.frmSelectAddonName();
                    f.ShowDialog();
                }
            }

            Log.Write($"Addon Name set to: [{ConfigFile.ReadValue("PixelMagic", "AddonName")}]", Color.Blue);

            dtColorHelper = new DataTable();
            dtColorHelper.Columns.Add("Percent");
            dtColorHelper.Columns.Add("Unrounded");
            dtColorHelper.Columns.Add("Rounded");
            dtColorHelper.Columns.Add("Value");

            for (int i = 0; i <= 99; i++)
            {
                DataRow drNew = dtColorHelper.NewRow();
                drNew["Percent"] = (i < 10) ? "0.0" + i : "0." + i;
                drNew["Unrounded"] = double.Parse(drNew["Percent"].ToString()) * 255;
                drNew["Rounded"] = Math.Round(double.Parse(drNew["Percent"].ToString()) * 255, 0);
                drNew["Value"] = i;
                dtColorHelper.Rows.Add(drNew);
            }
            {
                DataRow drNew = dtColorHelper.NewRow();
                drNew["Percent"] = "255";
                drNew["Unrounded"] = "255";
                drNew["Rounded"] = "255";
                drNew["Value"] = 0;
                dtColorHelper.Rows.Add(drNew);
            }
        }

        private static string Version => pWow.MainModule.FileVersionInfo.FileVersion;

        public static string InstallPath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(pWow?.MainModule.FileName);
            }
        }

        public static string AddonPath => InstallPath + "\\Interface\\AddOns";

        private static bool LimitedUserExists
        {
            get
            {
                using (var pc = new PrincipalContext(ContextType.Machine))
                {
                    var up = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, "Limited");
                    return up != null;
                }
            }
        }

        public static bool HasTarget
        {
            get
            {
                var c = GetBlockColor(2, 3);
                return (c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B);
            }
        }

        //public static bool HasBossTarget
        //{
        //    get
        //    {
        //        Color c = GetBlockColor(2, 3);
        //        return ((c.R == Color.Blue.R) && (c.G == Color.Blue.G) && (c.B == Color.Blue.B));
        //    }
        //}

        public static bool PlayerIsCasting
        {
            get
            {
                var c = GetBlockColor(3, 3);
                return (c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B);
            }
        }

        public static bool TargetIsCasting
        {
            get
            {
                var c = GetBlockColor(4, 3);
                return (c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B);
            }
        }

        public static bool TargetIsVisible
        {
            get
            {
                var c = GetBlockColor(5, 3);
                return (c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B);
            }
        }

        public static bool TargetIsFriend
        {
            get
            {
                var c = GetBlockColor(1, 3);
                return (c.R == 0) && (c.G == 255) && (c.B == 0);
            }
        }

        public static int CurrentRunes
        {
            get
            {
                var runes = 0;
                for (var x = 1; x <= 7; x++)
                {
                    var c = GetBlockColor(x, 7);
                    if ((c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B))
                    {
                        runes++;
                    }
                }
                return runes;
            }
        }

        public static int CurrentComboPoints
        {
            get
            {
                var comboPoints = 0;
                for (var x = 1; x <= 8; x++)
                {
                    var c = GetBlockColor(x, 7);
                    if ((c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B))
                    {
                        comboPoints++;
                    }
                }
                return comboPoints;
            }
        }

        public static int CurrentSoulShards
        {
            get
            {
                var ss = 0;
                for (var x = 1; x <= 5; x++)
                {
                    var c = GetBlockColor(x, 7);
                    if ((c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B))
                    {
                        ss++;
                    }
                }
                return ss;
            }
        }

        public static int CurrentHolyPower
        {
            get
            {
                var hp = 0;
                for (var x = 1; x <= 5; x++)
                {
                    var c = GetBlockColor(x, 7);
                    if ((c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B))
                    {
                        hp++;
                    }
                }
                return hp;
            }
        }

        public static bool TargetIsEnemy => !TargetIsFriend;

        public static int HealthPercent
        {
            get
            {
                // First we build up the binary string that makes up health
                // This is read from the 1st row on the screen of pixel information
                // It is displayed as binary, so 100% health = 1100100
                var binaryHealth = "";

                for (var x = 1; x <= 7; x++)
                {
                    var c = GetBlockColor(x, 1);
                    binaryHealth += (c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B) ? "1" : "0";
                }

                return Convert.ToInt32(binaryHealth, 2);
            }
        }

        public static int TargetHealthPercent
        {
            get
            {
                // First we build up the binary string that makes up health
                // This is read from the 2nd row on the screen of pixel information
                // It is displayed as binary, so 100% health = 1100100
                var binaryHealth = "";

                for (var x = 15; x <= 21; x++)
                {
                    var c = GetBlockColor(x, 1);
                    binaryHealth += (c.R == 0) && (c.G == 0) && (c.B == 255) ? "1" : "0";
                }

                return Convert.ToInt32(binaryHealth, 2);
            }
        }

        public static int Power
        {
            get
            {
                // First we build up the binary string that makes up power
                // This is read from the 4th row on the screen of pixel information
                // It is displayed as binary, so 100 power = 1100100
                var binaryPower = "";

                for (var x = 8; x <= 14; x++)
                {
                    var c = GetBlockColor(x, 1);
                    binaryPower += (c.R == 0) && (c.G == 255) && (c.B == 0) ? "1" : "0";
                }

                return Convert.ToInt32(binaryPower, 2);
            }
        }
        public static int Focus => Power;
        public static int Mana => Power;
        public static int Energy => Power;
        public static int Rage => Power;
        public static int Fury => Power;

        public static bool HasFocus
        {
            get
            {
                var activatedHandle = GetForegroundWindow();
                if (activatedHandle == IntPtr.Zero)
                {
                    return false; // No window is currently activated
                }

                int activeProcId;
                GetWindowThreadProcessId(activatedHandle, out activeProcId);

                if (pWow == null)
                {
                    throw new Exception("World of warcraft is not detected / running, please login before attempting to restart the bot");
                }

                return activeProcId == pWow.Id;
            }
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle rect);

        public static void Dispose()
        {
            Log.Write("Disposing of WoW Process...");
            pWow.Close();
            pWow = null;
            Log.Write("Disposing of WoW Process Completed.");
        }

        public static bool IsSpellOnCooldown(int spellNoInArrayOfSpells) // This will take the spell no from the array of spells, 1, 2, 3 ..... n
        {
            var c = GetBlockColor(spellNoInArrayOfSpells, 2);
            return (c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B);
        }

        public static bool IsSpellOnCooldown(string spellBookSpellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);

            if (spell == null)
            {
                Log.Write($"[IsSpellOnCooldown] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
                return false;
            }

            return IsSpellOnCooldown(spell.InternalSpellNo);
        }

        public static bool IsSpellInRange(int spellNoInArrayOfSpells) // This will take the spell no from the array of spells, 1, 2, 3 ..... n
        {
            var c = GetBlockColor(spellNoInArrayOfSpells, 6);
            return (c.R == Color.Red.R) && (c.G == Color.Red.G) && (c.B == Color.Red.B);
        }

        public static bool IsSpellInRange(string spellBookSpellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);

            if (spell == null)
            {
                Log.Write($"[IsSpellInRange] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
                return false;
            }

            return IsSpellInRange(spell.InternalSpellNo);
        }

        public static bool CanCast(int spellNoInArrayOfSpells, 
                                   bool checkIfPlayerIsCasting = true, 
                                   bool checkIfSpellIsOnCooldown = true, 
                                   bool checkIfSpellIsInRange = true, 
                                   bool checkSpellCharges = true, 
                                   bool checkIfTargetIsVisible = true)
        {
            if (checkIfPlayerIsCasting)
                if (PlayerIsCasting)
                    return false;

            if (checkIfSpellIsOnCooldown)
                if (IsSpellOnCooldown(spellNoInArrayOfSpells))
                    return false;

            if (checkIfSpellIsInRange)
                if (IsSpellInRange(spellNoInArrayOfSpells) == false)
                    return false;

            if (checkSpellCharges)
                if (GetSpellCharges(spellNoInArrayOfSpells) <= 0)
                    return false;

            if (checkIfTargetIsVisible)
                if (TargetIsVisible == false)
                    return false;

            return true;
        }

        public static bool CanCast(string spellBookSpellName,
                                   bool checkIfPlayerIsCasting = true,
                                   bool checkIfSpellIsOnCooldown = true,
                                   bool checkIfSpellIsInRange = true,
                                   bool checkSpellCharges = true,
                                   bool checkIfTargetIsVisible = true)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);

            if (spell == null)
            {
                Log.Write($"[CanCast] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
                return false;
            }

            return CanCast(spell.InternalSpellNo, checkIfPlayerIsCasting, checkIfSpellIsOnCooldown, checkIfSpellIsInRange, checkSpellCharges, checkIfTargetIsVisible);
        }

        private static void SendKey(Keys key, int milliseconds = 50, string spellName = null)
        {
            if (spellName == null)
            {
                Log.Write("Sending keypress: " + key, Color.Gray);
            }
            else
            {
                Log.Write("Casting spell: " + spellName, Color.Gray);
            }

            if (milliseconds < 50)
                milliseconds = 50;

            milliseconds = milliseconds + random.Next(50);

            KeyDown(key);
            Thread.Sleep(milliseconds);
            KeyUp(key);
        }

        public static void SendKeyAtLocation(Keys key, int x, int y)
        {
            Log.Write($"Sending keypress {key} at location: x = {x}, y = {y}", Color.Gray);

            KeyDown(key);
            Thread.Sleep(50);
            KeyUp(key);

            Mouse.LeftClick(x, y);
        }

        public static void SendMacro(string macro)
        {
            Log.Write("Sending macro: " + macro, Color.Gray);

            KeyPressRelease(Keys.Enter);
            Thread.Sleep(100);
            Write(macro);
            Thread.Sleep(100);
            KeyPressRelease(Keys.Enter);
        }
        
        public static int GetBuffStacks(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(5 + auraNoInArrayOfAuras, 3);

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                string stacks = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()["Value"].ToString();

                return int.Parse(stacks);
            }
            catch(Exception ex)
            {
                Log.Write("Failed to find buff stacks for color G = " + c.G, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }
             
            return 0;   
        }
        
        public static int GetBuffStacks(string auraName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == auraName);

            if (aura == null)
            {
                Log.Write($"[GetBuffStacks] Unable to find buff with name '{auraName}' in Spell Book");
                return -1;
            }

            return GetBuffStacks(aura.InternalAuraNo);
        }

        public static int GetDebuffTimeRemaining(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 8);

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                var stacks = dtColorHelper.Select($"[Rounded] = '{c.B}'").FirstOrDefault()["Value"].ToString();

                return int.Parse(stacks);
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find debuff stacks for color G = " + c.B, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        public static int GetDebuffTimeRemaining(string debuffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == debuffName);

            if (aura == null)
            {
                Log.Write($"[GetDebuffTimeRemaining] Unable to find debuff with name '{debuffName}' in Spell Book");
                return -1;
            }

            return GetDebuffTimeRemaining(aura.InternalAuraNo);
        }

        public static int GetDebuffStacks(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 8);

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                var stacks = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()["Value"].ToString();

                return int.Parse(stacks);
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find debuff stacks for color G = " + c.G, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        public static int GetDebuffStacks(string debuffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == debuffName);

            if (aura == null)
            {
                Log.Write($"[GetDebuffTimeRemaining] Unable to find debuff with name '{debuffName}' in Spell Book");
                return -1;
            }

            return GetDebuffStacks(aura.InternalAuraNo);
        }

        public static int GetSpellCharges(int spellNoInArrayOfSpells)
        {
            var c = GetBlockColor(spellNoInArrayOfSpells, 9);

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                string stacks = dtColorHelper.Select($"[Rounded] = '{c.G}'").FirstOrDefault()["Value"].ToString();

                return int.Parse(stacks);
            }
            catch (Exception ex)
            {
                Log.Write("Failed to find spell charge stacks for color G = " + c.G, Color.Red);
                Log.Write("Error: " + ex.Message, Color.Red);
            }

            return 0;
        }

        public static int GetSpellCharges(string spellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellName);

            if (spell == null)
            {
                Log.Write($"[GetSpellCharges] Unable to find spell with name '{spellName}' in Spell Book");
                return -1;
            }

            return GetSpellCharges(spell.InternalSpellNo);
        }

        public static bool HasBuff(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(5 + auraNoInArrayOfAuras, 3);
            return ((c.R != 255) && (c.G != 255) && (c.B != 255));
        }

        public static bool HasBuff(string buffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == buffName);

            if (aura == null)
            {
                Log.Write($"[HasAura] Unable to find aura with name '{buffName}' in Spell Book");
                return false;
            }

            return HasBuff(aura.InternalAuraNo);
        }

        public static bool HasDebuff(string debuffName)
        {
            var aura = SpellBook.Auras.FirstOrDefault(s => s.AuraName == debuffName);

            if (aura == null)
            {
                Log.Write($"[HasDebuff] Unable to find debuff with name '{debuffName}' in Spell Book");
                return false;
            }

            return HasDebuff(aura.InternalAuraNo);
        }

        public static bool HasDebuff(int auraNoInArrayOfAuras)
        {
            var c = GetBlockColor(auraNoInArrayOfAuras, 8);
            return ((c.R != 255) && (c.G != 255) && (c.B != 255));
        }

        public static void CastSpellByName(string spellBookSpellName)
        {
            var spell = SpellBook.Spells.FirstOrDefault(s => s.SpellName == spellBookSpellName);

            if (spell == null)
            {
                Log.Write($"[CastSpellByName] Unable to find spell with name '{spellBookSpellName}' in Spell Book");
                return;
            }

            SendKey(spell.Key, 50, spellBookSpellName);
        }

        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr srchDC, int srcX, int srcY, int srcW, int srcH, IntPtr desthDC, int destX, int destY, int op);

        // This is apparently one of the fastest ways to read single pixel color
        // http://stackoverflow.com/questions/17130138/fastest-way-to-get-screen-pixel-color-in-c-sharp

        public static Color GetBlockColor(int column, int row)
        {
            if (pWow == null)
                return Color.Black;

            if ((column <= 0) || (row <= 0))
                throw new Exception("x and or y must be >= 1");

            column = (column - 1)*7; // For some unknown reason pixel size of 5x5 in WoW = 7x7 in C#
            row = (row - 1)*7;

            lock (thisLock) // We lock the bitmap "screenPixel" here to avoid it from being accessed by multiple threads at the same time and crashing
            {
                try
                {
                    using (var gdest = Graphics.FromImage(screenPixel))
                    {
                        using (var gsrc = Graphics.FromHwnd(pWow.MainWindowHandle))
                        {
                            var hSrcDC = gsrc.GetHdc();
                            var hDC = gdest.GetHdc();
                            BitBlt(hDC, 0, 0, 1, 1, hSrcDC, column, row, (int) CopyPixelOperation.SourceCopy);
                            gdest.ReleaseHdc();
                            gsrc.ReleaseHdc();
                        }
                    }
                    var temp = screenPixel.GetPixel(0, 0);

                    return temp;
                }
                catch (Exception ex)
                {
                    Log.Write("Failed to find pixel color from screen, this is usually due to wow closing while", Color.Red);
                    Log.Write("attempting to find the pixel color", Color.Red);
                    Log.Write("Error Details: " + ex.Message, Color.Red);

                    throw;
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        //public Color GetPixelColorAt(int x, int y)
        //{
        //    if (pWow == null)
        //        return Color.Black;

        //    using (Graphics gdest = Graphics.FromImage(screenPixel))
        //    {
        //        using (Graphics gsrc = Graphics.FromHwnd(pWow.MainWindowHandle))
        //        {
        //            IntPtr hSrcDC = gsrc.GetHdc();
        //            IntPtr hDC = gdest.GetHdc();
        //            int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, x, y, (int)CopyPixelOperation.SourceCopy);
        //            gdest.ReleaseHdc();
        //            gsrc.ReleaseHdc();
        //        }
        //    }
        //    Color temp = screenPixel.GetPixel(0, 0);

        //    if ((temp.R == Color.White.R) && (temp.G == Color.White.G) && (temp.B == Color.White.B))
        //    {
        //        Log.Write($"Color @ (x,y) = ({x},{y}) = {temp.ToString()}", Color.Black);
        //    }
        //    else
        //    {
        //        Log.Write($"Color @ (x,y) = ({x},{y}) = {temp.ToString()}", temp);
        //    }

        //    return temp;
        //}

        //public class Status
        //{
        //    public enum GameState
        //    {
        //        InGame,
        //        NotInGame
        //    }
        //}

        //public Status.GameState GameState
        //{
        //    get
        //    {
        //        if (wow.Read<byte>(Offsets.GameState) == 1)
        //        {
        //            return Status.GameState.InGame;
        //        }
        //        else
        //        {
        //            return Status.GameState.NotInGame;
        //        }
        //    }
        //}

        #region Keyboard Input

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, UIntPtr wParam, UIntPtr lParam);

        private static void KeyDown(Keys Key)
        {
            SendMessage(pWow.MainWindowHandle, 0x100, (int) Key, 0);
        }

        private static void KeyUp(Keys Key)
        {
            SendMessage(pWow.MainWindowHandle, 0x101, (int) Key, 0);
        }

        private static void KeyPressRelease(Keys key)
        {
            KeyDown(key);
            Thread.Sleep(50);
            KeyUp(key);
        }

        private static void Write(string text, params object[] args)
        {
            foreach (var character in string.Format(text, args))
            {
                PostMessage(pWow.MainWindowHandle, 0x0102, new UIntPtr(character), UIntPtr.Zero);
            }
        }

        #endregion
    }
}