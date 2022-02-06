using SyncingSyllabi.Contexts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public Int64 Id { get; set; }
    }
}
