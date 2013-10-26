using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace MyMvc.Helper
{
    public class WebHelper
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

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static string GetIP()
        {
            string IP;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            return IP;
        }
    }
}
