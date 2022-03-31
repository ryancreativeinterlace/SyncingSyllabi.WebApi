﻿using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface ISyllabusService
    {
        SyllabusDto CreateSyllabus(SyllabusRequestModel syllabusRequestModel);
        SyllabusDto UpdateSyllabus(SyllabusRequestModel syllabusRequestModel);
        SyllabusDto GetSyllabus(long syllabusId, long userId);
        PaginatedResultDto<SyllabusDataOutputModel> GetSyllabusDetailsList(SyllabusRequestModel syllabusRequestModel);
    }
}
