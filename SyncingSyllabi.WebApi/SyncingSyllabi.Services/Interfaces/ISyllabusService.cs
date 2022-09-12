using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface ISyllabusService
    {
        SyllabusResponseModel CreateSyllabus(SyllabusRequestModel syllabusRequestModel);
        SyllabusDto UpdateSyllabus(SyllabusRequestModel syllabusRequestModel);
        SyllabusDto GetSyllabus(long syllabusId, long userId);
        PaginatedResultDto<SyllabusDataOutputModel> GetSyllabusDetailsList(SyllabusRequestModel syllabusRequestModel);
        bool DeleteSyllabus(long syllabusId, long userId);
        OcrScanReponseDataModel OcrScan(OcrScanRequestModel syllabusRequestModel);
    }
}
