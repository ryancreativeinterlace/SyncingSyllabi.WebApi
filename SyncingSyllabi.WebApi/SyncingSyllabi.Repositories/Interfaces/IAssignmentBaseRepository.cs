using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface IAssignmentBaseRepository
    {
        AssignmentDto CreateAssignment(AssignmentDto assignmentDto);
        AssignmentDto UpdateAssignment(AssignmentDto assignmentDto);
        AssignmentDto GetAssignment(long assignmentId, long userId);
        AssignmentListResponseModel GetAssignmentDetailsList(long userId, IEnumerable<SortColumnDto> sortColumn, PaginationDto pagination, DateRangeDto dateRange);
        bool DeleteAssignment(long assignmentId, long userId);
    }
}
