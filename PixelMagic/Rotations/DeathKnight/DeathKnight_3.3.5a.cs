// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

// BETA TESTING FOR 3.3.5a - TESTING ONLY !!! DONT TELL ME IT DOESNT WORK ITS NOT MEANT TO YET !!!

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;

namespace PixelMagic.Rotation
{
    public class DeathKnight : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "DeathKnight Sample";
            }
        }

        public override string Class
        {
            get
            {
                return "DeathKnight";
            }
        } 

        public override void Initialize()
        {
            Log.Write("Welcome to Death Knight Sample", Color.Green);
        }

        public override void Stop()
        {
        }

        private int lastAuraCount = 0;

        public override void Pulse()
        {   
			if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {

                }
            }
            if (combatRoutine.Type == RotationType.AOE)
            {
                // Do AOE Stuff here                
            }            
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=DeathKnight
AddonName=PixelMagic
WoWVersion=WOTLK - 30300
[SpellBook.db]
*/

