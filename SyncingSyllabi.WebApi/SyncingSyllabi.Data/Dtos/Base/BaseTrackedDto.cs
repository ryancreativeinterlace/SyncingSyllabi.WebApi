using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Base
{
    public class BaseTrackedDto : BaseDto
    {
        public DateTime DateCreated { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public Int64 UpdatedBy { get; set; }
    }
}
