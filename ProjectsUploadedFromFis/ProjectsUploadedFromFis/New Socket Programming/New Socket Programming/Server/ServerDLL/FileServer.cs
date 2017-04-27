using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Drawing.Imaging;
using System.IO;
using ServerDLL.CommandHandler;

namespace ServerDLL
{
    internal class FileServer : AbstractServer, IDisposable
    {
        Timer timer;
        double interval = 400;
        StreamReader serverStream;
        string serverStreamName = "server.txt";
        string clientStreamName = "client.txt";
        string LastMsgGuid = string.Empty;
        private const char splitChr = (char)3;
        bool disposed = false;

        StreamWriter clientStream;

        public FileServer(double interval, string commmunicationFolder)
            : base(commmunicationFolder)
        {
            timer = new Timer(interval);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            FileStream fsServer = File.Open(commmunicationFolder + @"\" + serverStreamName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            FileStream fsClient = File.Open(commmunicationFolder + @"\" + clientStreamName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

            serverStream = new StreamReader(fsServer);
            clientStream = new StreamWriter(fsClient);

        }

        public override void Start()
        {
            timer.Start();
        }

        private void timer_Elapsed(object sender, EventArgs e)
        {
            ReadCommand();
        }

        public double Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        protected bool ReadCommand()
        {
            serverStream.BaseStream.Position = 0;
            string command = serverStream.ReadToEnd();
            if (command != null && command.Trim() != "")
            {
                command = GetNewRequest(command);
                if (command.Trim() == "")
                    return true;
                if (string.Compare(command.Trim(),"disconnect", StringComparison.InvariantCulture) == 0)
                    return false;

                CommadMessage msg = new CommadMessage(command, clientStream, this.Port, LastMsgGuid);
                this.commandHandler.HandleCommand(msg);
               
            }
            return true;
        }

        private string GetNewRequest(string message)
        {
            string[] arr = message.Split(splitChr);
            if (arr.Length < 2)
                return "";
            if (LastMsgGuid.Trim() != arr[1].Trim())
            {
                LastMsgGuid = arr[1].Trim();
                return arr[0];
            }
            else
                return "";
        }

        ~FileServer()
        {
            Dispose(false);
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        public virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    clientStream.Dispose();
                    serverStream.Dispose();
                    timer.Dispose();
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed.
                /********CloseHandle(handle);
                handle = IntPtr.Zero;*/
                // Note that this is not thread safe.
                // Another thread could start disposing the object
                // after the managed resources are disposed,
                // but before the disposed flag is set to true.
                // If thread safety is necessary, it must be
                // implemented by the client.
            }
            disposed = true;
        }
    }
}