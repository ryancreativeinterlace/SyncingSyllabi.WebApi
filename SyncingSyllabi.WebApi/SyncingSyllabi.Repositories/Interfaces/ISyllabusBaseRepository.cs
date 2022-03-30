using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface ISyllabusBaseRepository
    {
        SyllabusDto CreateSyllabus(SyllabusDto syllabusDto);
        SyllabusDto UpdateSyllabus(SyllabusDto syllabusDto);
        SyllabusDto GetSyllabus(long syllabusId, long userId);
    }
}
