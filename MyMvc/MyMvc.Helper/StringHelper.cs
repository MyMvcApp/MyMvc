using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace MyMvc.Helper
{
    public class StringHelper
    {
        /// <summary>
        /// md5加密方法
        /// </summary>
        /// <param name="input">要加密的字符</param>
        /// <returns></returns>
        public static string GetMD5Hash(String input)
        {
            try
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5").ToUpper();
            }
            catch
            {
                return "";
            }
        } 
    }
}
