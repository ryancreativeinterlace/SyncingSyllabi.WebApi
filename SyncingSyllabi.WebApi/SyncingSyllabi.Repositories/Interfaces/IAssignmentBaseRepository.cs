﻿using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface IAssignmentBaseRepository
    {
        AssignmentDto CreateAssignment(AssignmentDto assignmentDto);
    }
}