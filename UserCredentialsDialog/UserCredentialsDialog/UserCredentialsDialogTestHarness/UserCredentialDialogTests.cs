using System;
using System.Collections.Generic;
using System.Text;
using Security.Windows.Forms;
using System.Windows.Forms;
using System.Diagnostics;

namespace CredentialsDialogTestHarness
{
	class UserCredentialDialogTests
	{
		public void DefaultFlagsTest()
		{
			using (UserCredentialsDialog dialog = new UserCredentialsDialog())
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					// validate credentials against an authentication authority
					// ...
					// If credentials are valid
					// and the user checked the "remember my password" option
					if (dialog.SaveChecked)
					{
						dialog.ConfirmCredentials(true);
					}

					Console.WriteLine(dialog.User);
					Console.WriteLine(dialog.PasswordToString());
					Console.WriteLine(dialog.Domain);
				}
			}
		}

		public void ReadOnlyNotSaveCredsTest()
		{
			using (UserCredentialsDialog dialog = new UserCredentialsDialog())
			{
				dialog.Flags = UserCredentialsDialogFlags.KeepUsername | UserCredentialsDialogFlags.DoNotPersist;
				// Preload the user to some value
				dialog.User = Environment.UserName;
				dialog.Domain = Environment.UserDomainName;
				dialog.ShowDialog();
			}
		}

		public void OnlyAdminAccountTest()
		{
			using (UserCredentialsDialog dialog = new UserCredentialsDialog("OnlyAdminApp"))
			{
				// Add to the Default flags the Admin only filter
				dialog.Flags |= UserCredentialsDialogFlags.RequestAdministrator;
				dialog.ShowDialog();
			}
		}

		public void RunAsProcessTest()
		{
			using (UserCredentialsDialog dialog = new UserCredentialsDialog())
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					ProcessStartInfo info = new ProcessStartInfo("notepad.exe");
					info.UseShellExecute = false;
					info.UserName = dialog.User;
					info.Password = dialog.Password;
					info.Domain = dialog.Domain;
					using (Process install = Process.Start(info))
					{
						install.WaitForExit();
						Console.WriteLine(install.ExitCode);
					}
				}
			}
		}

		public void CustomizeDialogTest()
		{
			using (UserCredentialsDialog dialog = new UserCredentialsDialog("mytarget", "MyApplication", "Enter your creds"))
			{
				dialog.ShowDialog();
			}
		}
	}
}
