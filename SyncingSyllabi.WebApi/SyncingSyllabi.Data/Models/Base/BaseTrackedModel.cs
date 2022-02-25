using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Base
{
    public class BaseTrackedModel : BaseModel
    {
        public DateTime DateCreated { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public Int64 UpdatedBy { get; set; }
    }
}
