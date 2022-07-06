using Microsoft.AspNetCore.Http;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface IS3FileRepository
    {
        Task UploadFile(string directory, string externalKey, byte[] buffer);
        Task<byte[]> DownloadFile(string directory, string externalKey);
        string GetPreSignedUrl(string directory, string externalKey, string contentType, string fileName, DateTime expiry);
        //Task<OcrSyllabusResponseModel> SyllabusDetectAsync(string directory, string externalKey, byte[] buffer);
        Task<OcrScanReponseDataModel> SyllabusDetectAsync(IFormFile imageFile, IEnumerable<string> pdfFile, IEnumerable<int> pages, OcrTypeEnum ocrTypeEnum);
    }
}
