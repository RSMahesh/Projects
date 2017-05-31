using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TransparentWindow
{
    public class Transpainter
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        enum WindowLongFlags : int
        {
            GWL_EXSTYLE = -20,
            GWLP_HINSTANCE = -6,
            GWLP_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4,
            DWLP_USER = 0x8,
            DWLP_MSGRESULT = 0x0,
            DWLP_DLGPROC = 0x4
        }

        private Form _form;
        AttachedWindow _attachedWindow;
        public Transpainter(Form form, AttachedWindow attachedWindow)
        {
            _form = form;
            _attachedWindow = attachedWindow;
        }
        private enum WS_EX : int
        {
            Transparent = 0X20,
            Layered = 0x80000
        }

        private enum LWA : int
        {
            ColorKey = 0X1,
            Alpha = 0X2
        }
        private int m_InitialStyle;

        public int WindowTransparentAlpha = 80;
        public void MakeTranseparent()
        {
            _form.FormBorderStyle = FormBorderStyle.None;
            m_InitialStyle = GetWindowLong(_form.Handle, (int)WindowLongFlags.GWL_EXSTYLE);
            SetWindowLong(
                _form.Handle,
                (int)WindowLongFlags.GWL_EXSTYLE,
                (uint)(m_InitialStyle | (int)WS_EX.Layered | (int)WS_EX.Transparent));
            byte abc = (byte)(255 * ((double)WindowTransparentAlpha / (double)100));
            SetLayeredWindowAttributes(_form.Handle, 0, abc, (uint)LWA.Alpha);
        }

        public void DecreaseTransparency(int percent)
        {
            if (WindowTransparentAlpha < 100)
            {
                WindowTransparentAlpha += percent;

                if (WindowTransparentAlpha > 100)
                {
                    WindowTransparentAlpha = 100;
                }

                MakeTranseparent();
            }
        }

        public void IncreaseTransparency(int percent)
        {
            if (WindowTransparentAlpha > 0)
            {
                WindowTransparentAlpha -= percent;

                if (WindowTransparentAlpha < 0)
                {
                    WindowTransparentAlpha = 0;
                }

                MakeTranseparent();
            }
        }

        private bool transParent = false;
        public void ToggleTransparent()
        {
            if (!transParent)
            {
                this.MakeTranseparent();
            }
            else
            {
                _form.FormBorderStyle = FormBorderStyle.Sizable;
                SetWindowLong(_form.Handle, (int)WindowLongFlags.GWL_EXSTYLE, (uint)(m_InitialStyle | (int)WS_EX.Layered));
                int tempWindowTransparentAlpha = 70;
                byte abc = (byte)(255 * ((double)tempWindowTransparentAlpha / (double)100));
                SetLayeredWindowAttributes(_form.Handle, 0, abc, (uint)LWA.Alpha);
            }

            transParent = !transParent;
        }

        public void MakeTranseparent12()
        {
            var handle = _attachedWindow.GetAttacheWindowHandle();

            _form.FormBorderStyle = FormBorderStyle.None;
            m_InitialStyle = GetWindowLong(handle, (int)WindowLongFlags.GWL_EXSTYLE);
            SetWindowLong(
                handle,
                (int)WindowLongFlags.GWL_EXSTYLE,
                (uint)(m_InitialStyle | (int)WS_EX.Layered | (int)WS_EX.Transparent));
            byte abc = (byte)(255 * ((double)WindowTransparentAlpha / (double)100));

            SetLayeredWindowAttributes(handle, 0, abc, (uint)LWA.Alpha);

        }
    }
}
