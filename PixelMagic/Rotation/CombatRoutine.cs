//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading;
using PixelMagic.GUI;
using PixelMagic.Helpers;

// ReSharper disable FunctionNeverReturns
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable PublicConstructorInAbstractClass
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace PixelMagic.Rotation
{
    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public abstract class CombatRoutine
    {
        public enum RotationState
        {
            Stopped = 0,
            Running = 1
        }

        public enum RotationType
        {
            SingleTarget = 0,
            AOE = 1
        }

        private volatile RotationType _rotationType = RotationType.SingleTarget;

        private Thread characterInfo;
        public CombatRoutine combatRoutine;
        private Thread mainThread;

        private bool messageShown;
        private frmMain parent;

        private readonly ManualResetEvent pause = new ManualResetEvent(false);

        private int PulseFrequency = 100;

        private readonly Random random;

        public CombatRoutine()
        {
            random = new Random(DateTime.Now.Second);
        }

        public RotationState State { get; private set; } = RotationState.Stopped;

        public RotationType Type => _rotationType;

        public abstract string Name { get; }
        public abstract string Class { get; }

        public void CharacterInfoThread()
        {
            while (true)
            {
                pause.WaitOne();

                Threads.UpdateTextBox(parent.txtHealth, WoW.HealthPercent.ToString());
                Threads.UpdateTextBox(parent.txtPower, WoW.Power.ToString());
                Threads.UpdateTextBox(parent.txtTargetHealth, WoW.TargetHealthPercent.ToString());
                Threads.UpdateTextBox(parent.txtTargetCasting, WoW.TargetIsCasting.ToString());

                Thread.Sleep(500);
            }
        }

        private void MainThreadTick()
        {
            try
            {
                while (true)
                {
                    pause.WaitOne();

                    if (WoW.HasFocus)
                    {
                        Pulse();

                        if (!messageShown)
                        {
                            Log.Write("Rotation resumed", Color.Gray);

                            messageShown = true;
                        }
                    }
                    else
                    {
                        Log.Write("Rotation paused until WoW Window has focus again.", Color.Gray);
                        messageShown = false;
                    }

                    Thread.Sleep(PulseFrequency + random.Next(50));
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, Color.Red);
            }
            Thread.Sleep(random.Next(50)); // Make the bot more human-like add some randomness in
        }

        public void Load(frmMain parent)
        {
            this.parent = parent;

            PulseFrequency = int.Parse(ConfigFile.Pulse.ToString());
            Log.Write("Using Pulse Frequency (ms) = " + PulseFrequency);

            characterInfo = new Thread(CharacterInfoThread) {IsBackground = true};
            characterInfo.Start();

            mainThread = new Thread(MainThreadTick) {IsBackground = true};
            mainThread.Start();

            combatRoutine = this;

            Initialize();
        }

        internal void Dispose()
        {
            Log.Write("Stopping Character Info Thread...");
            Log.Write("Stopping Pulse() timer...");

            Pause();

            Thread.Sleep(100); // Wait for it to close entirely so that all bitmap reading is done
        }

        internal void ForcePulseUpdate()
        {
            PulseFrequency = int.Parse(ConfigFile.Pulse.ToString());
            Log.Write("Using Pulse Frequency (ms) = " + PulseFrequency);
        }

        public void Start()
        {
            try
            {
                if (State == RotationState.Stopped)
                {
                    Log.Write("Starting bot...", Color.Green);

                    if (WoW.pWow == null)
                    {
                        Log.Write("World of warcraft is not detected / running, please login before attempting to restart the bot", Color.Red);
                        return;
                    }

                    pause.Set();

                    State = RotationState.Running;
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error Starting Combat Routine", Color.Red);
                Log.Write(ex.Message, Color.Red);
            }
        }

        public void Pause()
        {
            try
            {
                if (State == RotationState.Running)
                {
                    Log.Write("Stopping bot.", Color.Black);

                    Stop();

                    pause.Reset();

                    State = RotationState.Stopped;

                    Log.Write("Combat routine has been stopped sucessfully.", Color.IndianRed);
                }
            }
            catch (Exception ex)
            {
                Log.Write("Error Stopping Combat Routine", Color.Red);
                Log.Write(ex.Message, Color.Red);
            }
        }

        public void ChangeType(RotationType rotationType)
        {
            if (_rotationType != rotationType)
            {
                _rotationType = rotationType;

                Log.Write("Rotation type: " + rotationType);

                Overlay.updateLabels();
            }
        }

        public abstract void Initialize();
        public abstract void Stop();
        public abstract void Pulse();
    }
}