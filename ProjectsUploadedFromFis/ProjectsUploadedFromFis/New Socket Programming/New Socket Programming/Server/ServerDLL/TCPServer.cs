using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using ServerDLL.CommandHandler;


namespace ServerDLL
{
    internal class TCPServer : AbstractServer, IDisposable 
    {
        bool disposed = false;
        TcpListener tcpListener;
        public TCPServer(string port) :   base(port)
        {

        }

        public override void Start()
        {
            //TcpListener is listening on the given port... {
            tcpListener = new TcpListener(int.Parse( this.Port));
            tcpListener.Start();
            //Accepts a new connection...
            while (true)
            {

                Socket socketForClient = tcpListener.AcceptSocket();
                //StreamWriter and StreamReader Classes for reading and writing the data to and fro.
                //The server reads the meassage sent by the Client ,converts it to upper case and sends it back to the client.
                //Lastly close all the streams.

                try
                {
                    if (socketForClient.Connected)
                    {
                        while (true)
                        {
                            Console.WriteLine("Client connected");
                            NetworkStream networkStream = new NetworkStream(socketForClient);
                            StreamWriter streamWriter = new StreamWriter(networkStream);
                            StreamReader streamReader = new StreamReader(networkStream);
                            string line = streamReader.ReadLine();
                            //   Console.WriteLine("Read:" + line);
                            CommadMessage msg = new CommadMessage(line,streamWriter);
                            commandHandler.HandleCommand(msg);


                            streamWriter.Flush();
                            streamWriter.Close();
                            streamWriter.Dispose();
                            streamReader.Close();
                            streamReader.Dispose();
                            networkStream.Close();
                            networkStream.Dispose();
                        }
                    }
                    socketForClient.Close();
                    Console.WriteLine("Exiting...");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
        }

     
        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }

  

    }
}
