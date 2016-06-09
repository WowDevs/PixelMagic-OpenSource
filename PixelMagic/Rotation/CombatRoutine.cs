//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using PixelMagic.GUI;
using PixelMagic.Helpers;
using System;
using System.Threading;

namespace PixelMagic.Rotation
{
    public abstract class CombatRoutine
    {
        private frmMain parent;

        ManualResetEvent pause = new ManualResetEvent(false);
        private Thread mainThread;
        public CombatRoutine combatRoutine;

        private Random random;
        private Thread characterInfo;

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

        private RotationState _state = RotationState.Stopped;

        public RotationState State
        {
            get
            {
                return _state;
            }
        }

        private volatile RotationType _rotationType = RotationType.SingleTarget;

        public RotationType Type
        {
            get
            {
                return _rotationType;
            }
        }

        public CombatRoutine()
        {
            random = new Random(DateTime.Now.Second);
        }

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

        private bool messageShown = false;

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
                            Log.Write("Rotation resumed", System.Drawing.Color.Gray);

                            messageShown = true;
                        }
                    }
                    else
                    {
                        Log.Write("Rotation paused until WoW Window has focus again.", System.Drawing.Color.Gray);
                        messageShown = false;
                    }

                    Thread.Sleep(PulseFrequency + random.Next(50));
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, System.Drawing.Color.Red);
            }
            Thread.Sleep(random.Next(50));  // Make the bot more human-like add some randomness in
        }

        private int PulseFrequency = 100;

        public void Load(frmMain parent)
        {
            this.parent = parent;

            PulseFrequency = int.Parse(ConfigFile.Pulse.ToString());
            Log.Write("Using Pulse Frequency (ms) = " + PulseFrequency);

            characterInfo = new Thread(CharacterInfoThread);
            characterInfo.IsBackground = true;
            characterInfo.Start();

            mainThread = new Thread(MainThreadTick);
            mainThread.IsBackground = true;
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
                if (_state == RotationState.Stopped)
                {
                    Log.Write("Starting bot...", System.Drawing.Color.Green);

                    if (WoW.pWow == null)
                    {
                        Log.Write("World of warcraft is not detected / running, please login before attempting to restart the bot", System.Drawing.Color.Red);
                        return;
                    }

                    pause.Set();

                    _state = RotationState.Running;
                }
            }
            catch(Exception ex)
            {
                Log.Write("Error Starting Combat Routine", System.Drawing.Color.Red);
                Log.Write(ex.Message, System.Drawing.Color.Red);
            }
        }
                
        public void Pause()
        {
            try
            {
                if (_state == RotationState.Running)
                {
                    Log.Write("Stopping bot.", System.Drawing.Color.Black);

                    Stop();

                    pause.Reset();

                    _state = RotationState.Stopped;                    

                    Log.Write("Combat routine has been stopped sucessfully.", System.Drawing.Color.IndianRed);
                }
            }
            catch(Exception ex)
            {
                Log.Write("Error Stopping Combat Routine", System.Drawing.Color.Red);
                Log.Write(ex.Message, System.Drawing.Color.Red);
            }
        }        

        public void ChangeType(RotationType rotationType)
        {
            if (_rotationType != rotationType)
            {
                _rotationType = rotationType;

                Log.Write("Rotation type: " + rotationType.ToString());

                Overlay.updateLabels();
            }
        }

        public abstract string Name { get; }
        public abstract string Class { get; }
        public abstract void Initialize();
        public abstract void Stop();
        public abstract void Pulse();        
    }
}
