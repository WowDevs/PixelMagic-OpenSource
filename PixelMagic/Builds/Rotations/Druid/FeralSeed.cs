// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class FeralSeed : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Feral Seed Rotation";
            }
        }

        public override string Class
        {
            get
            {
                return "Druid";
            }
        }

        public override void Initialize()
        {
            Log.Write("Welcome to Feral Seed", Color.Green);
        }

        public override void Stop()
        {
            // Move pet to me
            // WoW.SendKeyAtLocation(WoW.Keys.Z, 900, 500);   // Pet Passive and Move To = /petpassive /petmoveto
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    if (!WoW.IsSpellOnCooldown("Swipe"))
                    {
                        if (WoW.CanCast("Tigers Fury") && WoW.Energy <= 50) // Wtf
                        {
                            WoW.CastSpellByName("Tigers Fury");
                            return;
                        }

                        if (WoW.CanCast("Healing Touch") && WoW.CurrentComboPoints >= 4 && WoW.HasBuff("Clearcasting") && !WoW.HasBuff("Bloodtalons")) //
                        {
                            WoW.CastSpellByName("Healing Touch");
                            return;
                        }

                        if (WoW.CanCast("Ferocious Bite") && WoW.CurrentComboPoints >= 5 && WoW.Energy >= 50 && (WoW.TargetHealthPercent <= 25 || WoW.GetDebuffTimeRemaining("Rip") >= 10)) // 
                        {
                            WoW.CastSpellByName("Ferocious Bite");
                            return;
                        }

                        if (WoW.CanCast("Rake") && WoW.Energy >= 35 && (!WoW.HasDebuff("Rake") || WoW.GetDebuffTimeRemaining("Rake") <= 3)) // Rake
                        {
                            WoW.CastSpellByName("Rake");
                            return;
                        }

                        if (WoW.CanCast("Rip") && WoW.Energy >= 30 && (!WoW.HasDebuff("Rip") || (WoW.GetDebuffTimeRemaining("Rip") <= 4.7 && WoW.CurrentComboPoints >= 5)))  // 
                        {
                            WoW.CastSpellByName("Rip");
                            return;
                        }

                        if (WoW.CanCast("Shred") && WoW.CurrentComboPoints <= 5)  // 
                        {
                            WoW.CastSpellByName("Shred");
                            return;
                        }
                    }
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                // Do AOE Stuff here

                // Log.Write("Has Aura: " + WoW.HasAura("Furious Howl"));
                // Log.Write("Aura Count: " + WoW.GetAuraCount("Furious Howl"));
                // Log.Write("Aura Count: " + WoW.GetAuraCount("Taste for Blood"));
            }
        }

        public override Form SettingsForm { get; set; }
    }
}

/*
[AddonDetails.db]
AddonAuthor=ThomasTrainWoop
AddonName=Engine
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,5217,Tigers Fury,D9
Spell,5185,Healing Touch,D1
Spell,22568,Ferocious Bite,D2
Aura,145152,Bloodtalons
*/

