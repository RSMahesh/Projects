using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public class KeyPressSimulater
    {

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public const int VK_RCONTROL = 0xA3; //Right Control key code
        public void PressKeyH()
        {
            PressKey(Keys.H);
        }

        public void PressKey(Keys key)
        {
            keybd_event((byte)key, 0, 0, 0);
            Thread.Sleep(100);
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
        }
        public void PressShiftDown()
        {
            keybd_event((int)Keys.LShiftKey, 0, 0, 0);
        }
        public void PressShiftUp()
        {
            keybd_event((int)Keys.LShiftKey, 0, KEYEVENTF_KEYUP, 0);
        }
        public void ToggleCapLocks()
        {
            keybd_event((int)Keys.CapsLock, 0, 0, 0);
            Thread.Sleep(100);
            keybd_event((int)Keys.CapsLock, 0, KEYEVENTF_KEYUP, 0);
        }
    }
}
