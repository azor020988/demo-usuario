using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System;

namespace DemoUsuarios
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static string HASH256(string clearString)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] bytClearString = uEncode.GetBytes(clearString);
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(bytClearString);
            return Convert.ToBase64String(hash);
        }
    }
}
