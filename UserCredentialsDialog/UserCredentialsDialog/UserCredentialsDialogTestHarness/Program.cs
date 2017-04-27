using System;
using System.Collections.Generic;
using System.Text;

namespace CredentialsDialogTestHarness
{
	class Program
	{
		static void Main(string[] args)
		{
			UserCredentialDialogTests tests = new UserCredentialDialogTests();

			tests.DefaultFlagsTest();
			//tests.ReadOnlyNotSaveCredsTest();
			//tests.OnlyAdminAccountTest();
			//tests.RunAsProcessTest();
			//tests.CustomizeDialogTest();
		}
	}
}
