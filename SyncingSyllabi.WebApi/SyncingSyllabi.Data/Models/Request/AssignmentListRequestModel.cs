using SyncingSyllabi.Data.Models.Request.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class AssignmentListRequestModel : BaseListRequestModel
    {
        public Int64 UserId { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
