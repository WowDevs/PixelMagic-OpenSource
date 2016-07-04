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
                    if (WoW.CanCast(3) && WoW.Focus >= 40) // Kill Command
                    {
                        WoW.SendKey(WoW.Keys.S);
                        return;
                    }

                    if (WoW.CanCast(2) && WoW.Focus >= 30) // Arcane Shot
                    {
                        WoW.SendKey(WoW.Keys.Y);
                        return;
                    }

                    if (WoW.CanCast(1)) // Steady Shot
                    {
                        WoW.SendKey(WoW.Keys.T);
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