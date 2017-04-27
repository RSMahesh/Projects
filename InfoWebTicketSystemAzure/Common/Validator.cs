using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ResellerClub.Common
{
   public static class Validator
    {
       public static  bool ValidateEmailId(string inputEmail)
       {

           string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                 @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
           Regex re = new Regex(strRegex);
           if (re.IsMatch(inputEmail))
               return (true);
           else
               return (false);
       }

       public static bool ValidatePhoneNumber(string phoneNumber)
       {
           return Regex.IsMatch(phoneNumber, @"^(?:[0-9]+(?:(-|\s)[0-9])?)*$");
       }
    }
}
