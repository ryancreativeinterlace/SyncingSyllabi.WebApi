using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class SyllabusListResponseModel : BaseResponseModel<PaginatedResultDto<SyllabusDataOutputModel>>
    {
    }
}
