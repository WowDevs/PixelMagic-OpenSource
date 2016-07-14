// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;

namespace PixelMagic.Rotation
{
    public class Hunter : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Hunter Sample";
            }
        }

        public override string Class
        {
            get
            {
                return "Hunter";
            }
        } 

        public override void Initialize()
        {
            Log.Write("Welcome to Hunter Sample", Color.Green);
        }

        public override void Stop()
        {
            // Move pet to me
            WoW.SendKeyAtLocation(WoW.Keys.Z, 900, 500);   // Pet Passive and Move To = /petpassive /petmoveto
        }

        public override void Pulse()
        {   
			if (combatRoutine.Type == RotationType.SingleTarget)  // Do Single Target Stuff here
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy)
                {
                    if (WoW.CanCast("Kill Command") && WoW.Focus >= 40) // Kill Command
                    {
                        WoW.CastSpellByName("Kill Command");
                        return;
                    }

                    if (WoW.CanCast("Arcane Shot") && WoW.Focus >= 30) // Arcane Shot
                    {
                        WoW.CastSpellByName("Arcane Shot");
                        return;
                    }

                    if (WoW.CanCast("Steady Shot")) // Steady Shot
                    {
                        WoW.CastSpellByName("Steady Shot");
                        Thread.Sleep(1000);
                        return;
                    }
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
SpellBook.db
Spell,56641,Steady Shot, T
Spell,3044,Arcane Shot, Y
Spell,34026,Kill Command, S
Aura,1,Testing
Aura,2,Testing 2
*/