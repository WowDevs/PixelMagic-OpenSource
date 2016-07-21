// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class DKUnholy : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "DK Unholy Rotation";
            }
        }

        public override string Class
        {
            get
            {
                return "Deathknight";
            }
        }

        public override void Initialize()
        {
            Log.Write("Welcome to DK Unholy", Color.Green);
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    //On GCD
                    if (!WoW.IsSpellOnCooldown("Death Coil"))
                    {
                        //actions +=/ outbreak,target_if = !dot.virulent_plague.ticking
                        if (!WoW.HasDebuff("Virulent Plague") && WoW.CurrentRunes >= 1 && WoW.CanCast("Outbreak", true, false, true, false, true))
                        {
                            WoW.CastSpellByName("Outbreak");
                        }
                        if (WoW.CanCast("Dark Transformation", true, true, true, false, true))
                        {
                            WoW.CastSpellByName("Dark Transformation");
                        }

                        if (WoW.CanCast("Death Coil", true, false, false, false, true) && (WoW.RunicPower >= 80) || (WoW.HasBuff("Sudden Doom") && WoW.IsSpellOnCooldown("Dark Arbiter")))
                        {
                            WoW.CastSpellByName("Death Coil");
                        }
                        if (WoW.CanCast("Festering Strike", true, false, true, false, true) && WoW.GetDebuffStacks("Festering Wound") <= 4)
                        {
                            WoW.CastSpellByName("Festering Strike");
                        }
                        if (WoW.CanCast("Clawing Shadows", true, false, false, false, true) && WoW.CurrentRunes >= 3)
                        {
                            WoW.CastSpellByName("Clawing Shadows");
                        }
                        //actions +=/ dark_transformation
                    }
                    //Off gcd
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                
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
Spell,85948,Festering Strike,D1
Spell,77575,Outbreak,D2
Spell,207311,Clawing Shadows,D3
Spell,47541,Death Coil,D4
Spell,194918,Blighted Rune Weapon,D5
Spell,63560,Dark Transformation,D6
Spell,207349,Dark Arbiter, Q
Aura,81340,Sudden Doom
Aura,194310,Festering Wound
Aura,191587,Virulent Plague
*/

