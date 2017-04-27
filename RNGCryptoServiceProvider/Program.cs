using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace RNGCryptoServiceProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
                Console.WriteLine(getPP());
            Console.ReadLine();
        }


        private void Encode()
        {

        }

        private static string getPP()
        {
            var randomNumberGenerator = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var tokenData = new byte[32];
            randomNumberGenerator.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData.Concat(Guid.NewGuid().ToByteArray()).ToArray())
                  .Replace('/', '_')
                  .Replace('+', '-');
        }
    }
}
