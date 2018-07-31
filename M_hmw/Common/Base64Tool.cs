using System;
using System.Text;

namespace ServiceInterface.Common
{
    public static class Base64Tool
    {
        ///<summary>
        ///Base64加密
        ///</summary>
        ///<param name="message"></param>
        ///<returns></returns>
        public static string Base64Code(string message)
        {
            var bytes = Encoding.Default.GetBytes(message);
            return Convert.ToBase64String(bytes);
        }

        ///<summary>
        ///Base64解密
        ///</summary>
        ///<param name="message"></param>
        ///<returns></returns>
        public static string DecodeBase64ToString(string message)
        {
            var bytes = Convert.FromBase64String(message);
            return Encoding.Default.GetString(bytes);
        }

        ///<summary>
        ///Base64解密
        ///</summary>
        ///<param name="message"></param>
        ///<returns></returns>
        public static byte[] DecodeBase64ToByteArray(string message)
        {
            return Convert.FromBase64String(message);
        }
    }
}