using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TransparentWindow
{
   public class AttachedWindow
    {
        string _processId;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;

        const short SWP_NOMOVE = 0X2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 0X4;
        const int SWP_SHOWWINDOW = 0x0040;


        public object LastProcessIdfile { get; private set; }

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public AttachedWindow(string processId)
        {
            _processId = processId;
        }

        public  void ShowAttachWindow()
        {
            try
            {
                ShowWindowAsync(Process.GetProcessById(int.Parse(_processId)).MainWindowHandle, SW_SHOWNORMAL);
            }
            catch (Exception ex)
            {

            }
        }

        public  void MinizeAttachWindow()
        {
            try
            {
                ShowWindowAsync(Process.GetProcessById(int.Parse(_processId)).MainWindowHandle, SW_SHOWMINIMIZED);
            }
            catch (Exception ex)
            {

            }
        }

        [DllImport("user32.dll")]
        public static extern long GetWindowRect(int hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }


        public  void MoveAttacheWindowOld(int top, int left)
        {
            IntPtr handle = Process.GetProcessById(int.Parse(_processId)).MainWindowHandle;
            RECT Rect = new RECT();
            GetWindowRect((int)handle, ref Rect);
            var width = Rect.Right - Rect.Left;
            var height = Rect.Bottom - Rect.Top;

            if (handle != IntPtr.Zero)
            {

                MoveWindow(handle, left, top, width, height, true);
            }
        }

        public  void MoveAttacheWindow(int moveTopBy, int moveLeftBy)
        {
            IntPtr handle = Process.GetProcessById(int.Parse(_processId)).MainWindowHandle;
            RECT Rect = new RECT();
            GetWindowRect((int)handle, ref Rect);
            var width = Rect.Right - Rect.Left;
            var height = Rect.Bottom - Rect.Top;

            if (handle != IntPtr.Zero)
            {
                MoveWindow(handle, Rect.Left - moveLeftBy, Rect.Top - moveTopBy, width, height, true);
            }
        }

        public  void MoveResizeAttacheWindow(Rectangle rec)
        {
            IntPtr handle = Process.GetProcessById(int.Parse(_processId)).MainWindowHandle;

            if (handle != IntPtr.Zero)
            {
                MoveWindow(handle, rec.Left,rec.Top, rec.Width, rec.Height, true);
            }
        }

        public void MoveResizeAttacheWindow(int left, int top, int width, int height)
        {
            IntPtr handle = Process.GetProcessById(int.Parse(_processId)).MainWindowHandle;

            if (handle != IntPtr.Zero)
            {
                MoveWindow(handle, left, top, width, height, true);
            }
        }
        public  Rectangle GetAttacheWindowSize()
        {
            IntPtr handle = Process.GetProcessById(int.Parse(_processId)).MainWindowHandle;
            RECT Rect = new RECT();
            GetWindowRect((int)handle, ref Rect);
            var width = Rect.Right - Rect.Left;
            var height = Rect.Bottom - Rect.Top;

            return new Rectangle(new Point(Rect.Left, Rect.Top), new Size(width, height));
        }

        public  IntPtr GetAttacheWindowHandle()
        {
            return Process.GetProcessById(int.Parse(_processId)).MainWindowHandle;
        }
    }
}
