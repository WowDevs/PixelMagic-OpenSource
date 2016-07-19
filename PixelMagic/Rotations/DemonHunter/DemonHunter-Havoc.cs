// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class DemonHunterHavoc : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Demon Hunter Havoc Sample";
            }
        }

        public override string Class
        {
            get
            {
                return "Demon Hunter";
            }
        } 

        public override void Initialize()
        {
            Log.Write("Welcome to Demon Hunter Havoc Leveling", Color.Green);
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
                    if (WoW.CanCast("Chaos Strike") && WoW.Fury >= 40) // Fury Spender
                    {
                        WoW.CastSpellByName("Chaos Strike");
                        return;
                    }

                    if (WoW.CanCast("Demons Bite") && WoW.Fury <= 60)  // Fury Generator
                    {
                        WoW.CastSpellByName("Demons Bite");
                        return;
                    }                    
                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                // Do AOE Stuff here
            }            
        }

        public override Form SettingsForm { get; set; }
    }
}

/*
[AddonDetails.db]
AddonAuthor=DemonHunter
AddonName=PixelMagic
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,162243,Demons Bite, T
Spell,162794,Chaos Strike, Y
*/

