using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
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

        public static byte[] ConvertPdfToImage(IFormFile file, IEnumerable<int> pages)
        {
            var fileByte = new byte[0];
            var result = new MemoryStream();
            
            try
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    Document pdfDocument = new Document(ms);

                    var filterPdf = pdfDocument.Pages.Where(w => pages.Any(a => a == w.Number)).ToList();

                    foreach (var page in filterPdf)
                    {
                        // Define Resolution
                        Resolution resolution = new Resolution(400);

                        // Create Jpeg device with specified attributes
                        // Width, Height, Resolution
                        JpegDevice JpegDevice = new JpegDevice(600, 800, resolution);

                        // Convert a particular page and save the image to stream
                        JpegDevice.Process(pdfDocument.Pages[page.Number], result);
                        var fileData = Convert.ToBase64String(result.ToArray());
                        fileByte = Convert.FromBase64String(fileData);
                    }
                }

                return fileByte;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
