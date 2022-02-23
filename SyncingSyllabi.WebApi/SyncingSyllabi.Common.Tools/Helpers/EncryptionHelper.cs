using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Helpers
{
    public class EncryptionHelper
    {
        public static string EncryptString(string password)
        {
            string passwordEncode = string.Empty;

            if (!string.IsNullOrEmpty(password))
            {
                password = password.Trim();

                byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

                passwordEncode = Convert.ToBase64String(passwordBytes);
            }

            return passwordEncode;
        }

        public static string DecryptString(string password)
        {
            string passwordDecode = string.Empty;

            if (!string.IsNullOrEmpty(password))
            {
                byte[] passwordBytse = Convert.FromBase64String(password);
                passwordDecode = Encoding.UTF8.GetString(passwordBytse);
            }

            return passwordDecode;
        }
    }
}
