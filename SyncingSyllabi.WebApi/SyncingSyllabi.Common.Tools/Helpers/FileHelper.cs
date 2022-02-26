using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Helpers
{
    public class FileHelper
    {
        public static byte[] FileMemoryStreamConverter(IFormFile file)
        {
            var fileByte = new byte[0];

            try
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileData = Convert.ToBase64String(ms.ToArray());
                    fileByte = Convert.FromBase64String(fileData);
                }

                return fileByte;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
