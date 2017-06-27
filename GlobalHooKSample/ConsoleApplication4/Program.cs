using Kennedy.ManagedHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            var mouseHook = new MouseHook();
            mouseHook.MouseEvent += MouseHook_MouseEvent;
            mouseHook.InstallHook();
        }

        private static void MouseHook_MouseEvent(MouseEvents mEvent, System.Drawing.Point point)
        {
            Console.WriteLine("asaas");
        }
    }
}
