using Kennedy.ManagedHooks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    //https://www.codeproject.com/Articles/6362/Global-System-Hooks-in-NET
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyBoardHook = new KeyboardHook();
        private void Form1_Load(object sender, EventArgs e)
        {
            //mouseHook.MouseEvent += MouseHook_MouseEvent;
            //mouseHook.InstallHook();

            keyBoardHook.KeyboardEvent += KeyBoardHook_KeyboardEvent;
            keyBoardHook.InstallHook();
        }

        private void KeyBoardHook_KeyboardEvent(KeyboardEvents kEvent, Keys key)
        {
            textBox1.Text += key.ToString();
        }

        private void MouseHook_MouseEvent(MouseEvents mEvent, Point point)
        {
            textBox1.Text += mEvent.ToString();
        }
    }
}
