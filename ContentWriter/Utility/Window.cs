using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Utility
{

    internal class Window
    {
        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.dll", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr SetFocus(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);





        //Sets a window to be a child window of another window
        [DllImport("USER32.DLL")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        //Sets window attributes
        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        //Gets window attributes
        [DllImport("USER32.DLL")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport("User32")]
        static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        public const int WM_COMMAND = 0x0112;
        public const int WM_CLOSE = 0xF060;
        public int handle;
        public static Window Instance;

        private Window() { }
        static Window()
        {
            Instance = new Window();
        }

     

        //assorted constants needed
        public static int GWL_STYLE = -16;
        public static int GWL_EXSTYLE = -16;

        public static int WS_CHILD = 0x40000000; //child window
        public static int WS_BORDER = 0x00800000; //window with border
        public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
        public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar
        public static uint MF_REMOVE = 0x1000;
        public static uint MF_BYPOSITION = 0x400;


        public const int WS_MINIMIZE = 0x20000000;
        public const int WS_MAXIMIZE = 0x01000000;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_THICKFRAME = 0x00040000;

        public const int WS_EX_DLGMODALFRAME = 0x00000001;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_STATICEDGE = 0x00020000;
        public const int SW_HIDE = 0;

        public void WindowsReStyle(IntPtr handle)
        {
            int style = GetWindowLong(handle, GWL_STYLE);
            //   int count = GetMenuItemCount(handle);
            //loop & remove
            //for (int i = 0; i < count; i++)
            //    RemoveMenu(handle, 0, (MF_BYPOSITION | MF_REMOVE));

            ////force a redraw
            //DrawMenuBar(handle);
            SetWindowLong(handle, GWL_STYLE, (style & ~(WS_CAPTION | WS_THICKFRAME | WS_SYSMENU | WS_MINIMIZE | WS_MAXIMIZE)));

            int lExStyle = GetWindowLong(handle, GWL_EXSTYLE);
            lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);
            SetWindowLong(handle, GWL_EXSTYLE, lExStyle);


        }
        private const int _XPOS = 50; // X position for the top right corner of MediaCenter

        private const int _YPOS = 500; // Y position for the top right corner of MediaCenter

        private const int _XSIZE = 800; // Width of MediaCenter

        private const int _YSIZE = 800; // Heigth of MediaCenter
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SWP_NOMOVE = 0x0002;
        private const int SW_SHOW = 5;

        // User defined types for Dll Import


        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        public void ShowWindow(IntPtr handle, bool visible)
        {
            if (visible)
                ShowWindow(handle, SW_SHOW);
            else
                ShowWindow(handle, SW_HIDE);

        }

        public void SetWindowOnTop(IntPtr handle)
        {
            SetWindowPos(handle,       // handle to window
            (int)HWND_TOPMOST,  // placement-order handle
            0,     // horizontal position
            0,      // vertical position
            0,  // width
            0, // height
            SWP_SHOWWINDOW | SWP_NOSIZE | SWP_NOMOVE// window-positioning options
            );

        }

     

        public IntPtr FindWindow(int processId)
        {
            Process processes = Process.GetProcessById(processId);
            IntPtr pFoundWindow = new IntPtr();
           if(processes != null)
            {
                pFoundWindow = processes.MainWindowHandle;
            }
            return pFoundWindow;
        }

        private void MoveWindowEx(IntPtr handle, int x, int y, int width, int height)
        {
            MoveWindow(handle, x, y, width, height, true);
        }

      

        public void MoveProcessWindow(IntPtr handle, int x, int y, int width, int height)
        {
            MoveWindowEx(handle, x, y, width, height);
        }
       
        public void StartProcess(string processName, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = processName;
            startInfo.Arguments = arguments;
            Process.Start(startInfo);
        }

      

        internal void SetWinFocus(IntPtr hnd)
        {

            SetForegroundWindow((int)hnd);
            SetFocus(hnd);
        }
    }
}
