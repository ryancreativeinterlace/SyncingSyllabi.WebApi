using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Base
{
    public class BaseTrackedModel : BaseModel
    {
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
    }
}
