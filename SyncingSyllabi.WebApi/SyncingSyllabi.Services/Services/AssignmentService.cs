using AutoMapper;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentBaseRepository _assignmentBaseRepository;

        public AssignmentService
        (
            IMapper mapper,
            IAssignmentBaseRepository assignmentBaseRepository
        )
        {
            _mapper = mapper;
            _assignmentBaseRepository = assignmentBaseRepository;
        }

        public AssignmentDto CreateAssignment(AssignmentRequestModel assignmentRequestModel)
        {
            AssignmentDto creteAssignmentResult = null;

            var assignmentModel = new AssignmentModel();
            assignmentModel.UserId = assignmentRequestModel.UserId;
            assignmentModel.SyllabusId = assignmentRequestModel.SyllabusId;
            assignmentModel.Notes = !string.IsNullOrEmpty(assignmentRequestModel.Notes) ? assignmentRequestModel.Notes.Trim() : string.Empty;
            assignmentModel.ColorInHex = !string.IsNullOrEmpty(assignmentRequestModel.ColorInHex) ? assignmentRequestModel.ColorInHex.Trim() : string.Empty;
            assignmentModel.AssignmentDateStart = DateTime.Now;
            assignmentModel.AssignmentDateEnd = assignmentRequestModel.AssignmentDateEnd;
            assignmentModel.IsActive = true;

            AssignmentDto assignment = _mapper.Map<AssignmentDto>(assignmentModel);

            if (assignment != null)
            {
                creteAssignmentResult = _assignmentBaseRepository.CreateAssignment(assignment);
            }

            return creteAssignmentResult;
        }

        public AssignmentDto UpdateAssignment(AssignmentRequestModel assignmentRequestModel)
        {
            AssignmentDto creteAssignmentResult = null;

            var assignmentModel = new AssignmentModel();
            assignmentModel.UserId = assignmentRequestModel.UserId;
            assignmentModel.SyllabusId = assignmentRequestModel.SyllabusId;
            assignmentModel.Notes = !string.IsNullOrEmpty(assignmentRequestModel.Notes) ? assignmentRequestModel.Notes.Trim() : string.Empty;
            assignmentModel.ColorInHex = !string.IsNullOrEmpty(assignmentRequestModel.ColorInHex) ? assignmentRequestModel.ColorInHex.Trim() : string.Empty;
            assignmentModel.AssignmentDateStart = assignmentRequestModel.AssignmentDateStart ?? null;
            assignmentModel.AssignmentDateEnd = assignmentRequestModel.AssignmentDateEnd ?? null;
            assignmentModel.IsActive = assignmentRequestModel.IsActive ?? null;

            AssignmentDto assignment = _mapper.Map<AssignmentDto>(assignmentModel);

            if (assignment != null)
            {
                creteAssignmentResult = _assignmentBaseRepository.UpdateAssignment(assignment);
            }

            return creteAssignmentResult;
        }

        public AssignmentDto GetAssignment(long assignmentId, long userId)
        {
            AssignmentDto getAssignmentResult = null;

            getAssignmentResult = _assignmentBaseRepository.GetAssignment(assignmentId, userId);

            return getAssignmentResult;
        }
    }
}
