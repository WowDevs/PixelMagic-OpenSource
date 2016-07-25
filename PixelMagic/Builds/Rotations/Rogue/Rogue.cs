// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

#pragma warning disable 414

namespace PixelMagic.Rotation
{
    public class Rogue : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Rogue Sample";
            }
        }

        public override string Class
        {
            get
            {
                return "Rogue";
            }
        } 

        public override void Initialize()
        {
            Log.Write("Welcome to Rogue Sample", Color.Green);
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
                    //Log.Write("Has Aura [Deadly Poison]: " + WoW.HasAura("Deadly Poison").ToString());
                    Log.Write("Energy: " + WoW.Energy);

                    //var ac = WoW.GetAuraCount("Deadly Poison");

                    //if (ac != lastAuraCount)
                    //{
                    //    Log.Write("Aura Count: " + ac);
                    //    lastAuraCount = ac;
                    //}
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
AddonAuthor=Rogue
AddonName=PixelMagic
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,703,Garrote,D7
Aura,2818,Deadly Poison
*/

