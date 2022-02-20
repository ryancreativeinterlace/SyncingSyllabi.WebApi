using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Interfaces
{
    public interface IEntity
    {
        Int64 Id { get; set; }
        bool? IsActive { get; set; }
    }
}
