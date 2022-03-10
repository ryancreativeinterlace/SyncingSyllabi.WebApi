using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Entities.Base
{
    public abstract class BaseTrackedEntity : BaseEntity
    {
        public BaseTrackedEntity()
        {

        }

        public DateTime DateCreated { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public Int64 UpdatedBy { get; set; }


        public void FillCreated(Int64 userId)
        {
            CreatedBy = userId;
            DateCreated = DateTime.Now;
            UpdatedBy = userId;
            DateUpdated = DateTime.Now;
        }

        public void FillUpdated(Int64 userId)
        {
            UpdatedBy = userId;
            DateUpdated = DateTime.Now;
        }
    }
}
