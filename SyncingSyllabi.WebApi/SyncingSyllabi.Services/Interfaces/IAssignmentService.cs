using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface IAssignmentService
    {
        AssignmentResponseModel CreateAssignment(AssignmentRequestModel assignmentRequestModel);
        AssignmentDto UpdateAssignment(AssignmentRequestModel assignmentRequestModel);
        AssignmentDto GetAssignment(long assignmentId, long userId);
        AssignmentListResponseModel GetAssignmentDetailsList(AssignmentListRequestModel assignmentRequestModel);
        bool DeleteAssignment(long assignmentId, long userId);
        bool DeleteAssignmentAttachment(long assignmentId);
        bool DeleteAllAssignmentByUserId(long userId);
        AssignmentAttachmentResponseModel AssignmentAttachment(long assignmentId);
    }
}
