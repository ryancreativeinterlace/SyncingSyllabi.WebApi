using SyncingSyllabi.Contexts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Entities.Base
{
    public abstract class BaseEntity : IEntity
    {
        public Int64 Id { get; set; }
        public bool Active { get; set; }
    }
}
