using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class PaginationDto
    {
        public bool IncludeTotal { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
