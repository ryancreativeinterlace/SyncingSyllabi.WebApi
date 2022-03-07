using SyncingSyllabi.Data.Constants;
using System;
using static System.Linq.Enumerable;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Helpers
{
    public class KeyCodeHelper
    {
        public static string GenerateRandomKeyCode()
        {
            try
            {
                string generatedCode = string.Empty;

                Random genCode = new Random();
                var randomString = (from m in Repeat(DataConstants.KeyCode, 15) select m[genCode.Next(m.Length)]).ToArray();
                generatedCode = new string(randomString);;

                return generatedCode;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string GenerateRandomIntegerCode()
        {
            try
            {
                string generatedCode = string.Empty;

                Random genCode = new Random();
                var code = genCode.Next(0, 999999);
                generatedCode = code.ToString("D6");

                return generatedCode;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
