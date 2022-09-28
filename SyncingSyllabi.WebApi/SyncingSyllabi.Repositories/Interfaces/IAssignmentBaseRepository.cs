using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface IAssignmentBaseRepository
    {
        AssignmentResponseModel CreateAssignment(AssignmentDto assignmentDto);
        AssignmentDto UpdateAssignment(AssignmentDto assignmentDto);
        AssignmentDto GetAssignment(long assignmentId);
        AssignmentListResponseModel GetAssignmentDetailsList(long userId, bool? isCompleted, IEnumerable<SortColumnDto> sortColumn, PaginationDto pagination, DateRangeDto dateRange);
        bool DeleteAssignment(long assignmentId, long userId);
        IEnumerable<AssignmentDto> GetDueAssignments(DateTime dateTime);
        bool DeleteAssignmentAttachment(long assignmentId);
    }
}
