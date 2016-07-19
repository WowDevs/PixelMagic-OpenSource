// winifix@gmail.com
// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertPropertyToExpressionBody

using PixelMagic.Helpers;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PixelMagic.Rotation
{
    public class Warrior : CombatRoutine
    {
        public override string Name
        {
            get
            {
                return "Warrior Sample";
            }
        }

        public override string Class
        {
            get
            {
                return "Warrior";
            }
        } 

        public override void Initialize()
        {
            Log.Write("Welcome to Warrior Sample", Color.Green);

            SettingsForm = new Form()
            {
                Text = "Settings",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 1000,
                Height = 600,
            };

            var lblLastStandHealth = new Label()
            {
                Text = "Last Stand Health",
                Left = 20,
                Top = 20
            };
            SettingsForm.Controls.Add(lblLastStandHealth);

            var nudLastStandHealthPercent = new NumericUpDown()
            {
                Value = 0,
                Maximum = 100,
                Left = 150,
                Top = 20
            };
            SettingsForm.Controls.Add(nudLastStandHealthPercent);

            var cmdSave = new Button()
            {
                Text = "Save",
                Width = 65,
                Height = 25,
                Left = SettingsForm.Width - 200,
                Top = SettingsForm.Height - 200
            };
            cmdSave.Click += CmdSave_Click;

            SettingsForm.Controls.Add(cmdSave);
        }

        private void CmdSave_Click(object sender, System.EventArgs e)
        {
            // Save stuff here
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
                    //var ac = WoW.GetAuraCount("Taste for Blood");

                    //if (ac != lastAuraCount)
                    //{
                    //    Log.Write("Aura Count: " + ac);
                    //    lastAuraCount = ac;
                    //}

                    var ac = WoW.GetSpellCharges("Charge");

                    if (ac != lastAuraCount)
                    {
                        Log.Write("Spell Charge Count: " + ac);
                        lastAuraCount = ac;
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
AddonAuthor=Warrior
AddonName=PixelMagic
WoWVersion=Legion - 70000
[SpellBook.db]
Spell,100,Charge, D1
Aura,206333,Taste for Blood
*/

