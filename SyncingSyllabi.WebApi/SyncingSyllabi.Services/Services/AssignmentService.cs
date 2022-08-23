using AutoMapper;
using SyncingSyllabi.Common.Tools.Helpers;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using SyncingSyllabi.Data.Settings;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Services.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentBaseRepository _assignmentBaseRepository;
        private readonly IS3FileRepository _s3FileRepository;
        private readonly S3Settings _s3Settings;

        public AssignmentService
        (
            IMapper mapper,
            IAssignmentBaseRepository assignmentBaseRepository,
            IS3FileRepository s3FileRepository,
            S3Settings s3Settings
        )
        {
            _mapper = mapper;
            _assignmentBaseRepository = assignmentBaseRepository;
            _s3FileRepository = s3FileRepository;
            _s3Settings = s3Settings;
        }

        public AssignmentDto CreateAssignment(AssignmentRequestModel assignmentRequestModel)
        {
            AssignmentDto creteAssignmentResult = null;

            var assignmentModel = new AssignmentModel();
            assignmentModel.UserId = assignmentRequestModel.UserId;
            assignmentModel.SyllabusId = assignmentRequestModel.SyllabusId ?? 0;
            assignmentModel.AssignmentTitle = !string.IsNullOrEmpty(assignmentRequestModel.AssignmentTitle) ? assignmentRequestModel.AssignmentTitle.Trim() : string.Empty;
            assignmentModel.Notes = !string.IsNullOrEmpty(assignmentRequestModel.Notes) ? assignmentRequestModel.Notes.Trim() : string.Empty;
            assignmentModel.ColorInHex = !string.IsNullOrEmpty(assignmentRequestModel.ColorInHex) ? assignmentRequestModel.ColorInHex.Trim() : string.Empty;
            assignmentModel.AssignmentDateStart = DateTime.Now;
            assignmentModel.AssignmentDateEnd = assignmentRequestModel.AssignmentDateEnd;
            assignmentModel.IsCompleted = false;
            assignmentModel.IsActive = true;

            if(assignmentRequestModel.AttachmentFile != null)
            {
                string ext = System.IO.Path.GetExtension(assignmentRequestModel.AttachmentFile.FileName);

                assignmentModel.AttachmentFileName = assignmentRequestModel.AttachmentFile.FileName;

                var fileName = $"{Guid.NewGuid().ToString()}{ext}";

                var fileBytes = FileHelper.FileMemoryStreamConverter(assignmentRequestModel.AttachmentFile);

                if (fileBytes.Length > 0)
                {
                    _s3FileRepository.UploadFile(_s3Settings.AssignmentAttachmentDirectory, fileName, fileBytes).GetAwaiter().GetResult();

                    assignmentModel.Attachment = fileName;
                }
            }

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
            assignmentModel.Id = assignmentRequestModel.AssignmentId;
            assignmentModel.UserId = assignmentRequestModel.UserId;
            assignmentModel.SyllabusId = assignmentRequestModel.SyllabusId ?? 0;
            assignmentModel.AssignmentTitle = !string.IsNullOrEmpty(assignmentRequestModel.AssignmentTitle) ? assignmentRequestModel.AssignmentTitle.Trim() : string.Empty;
            assignmentModel.Notes = !string.IsNullOrEmpty(assignmentRequestModel.Notes) ? assignmentRequestModel.Notes.Trim() : string.Empty;
            assignmentModel.ColorInHex = !string.IsNullOrEmpty(assignmentRequestModel.ColorInHex) ? assignmentRequestModel.ColorInHex.Trim() : string.Empty;
            assignmentModel.AssignmentDateStart = assignmentRequestModel.AssignmentDateStart ?? null;
            assignmentModel.AssignmentDateEnd = assignmentRequestModel.AssignmentDateEnd ?? null;
            assignmentModel.IsCompleted = assignmentRequestModel.IsCompleted ?? null;
            assignmentModel.IsActive = assignmentRequestModel.IsActive ?? null;

            if (assignmentRequestModel.AttachmentFile != null)
            {
                string ext = System.IO.Path.GetExtension(assignmentRequestModel.AttachmentFile.FileName);

                assignmentModel.AttachmentFileName = assignmentRequestModel.AttachmentFile.FileName;

                var fileName = $"{Guid.NewGuid().ToString()}{ext}";

                var fileBytes = FileHelper.FileMemoryStreamConverter(assignmentRequestModel.AttachmentFile);

                if (fileBytes.Length > 0)
                {
                    _s3FileRepository.UploadFile(_s3Settings.AssignmentAttachmentDirectory, fileName, fileBytes).GetAwaiter().GetResult();

                    assignmentModel.Attachment = fileName;
                }
            }

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

            if (!string.IsNullOrEmpty(getAssignmentResult.Attachment))
            {
                // Get Presigned URL
                getAssignmentResult.Attachment = _s3FileRepository.GetPreSignedUrl(_s3Settings.AssignmentAttachmentDirectory, getAssignmentResult.Attachment, string.Empty, string.Empty, DateTime.Now.AddDays(2));
            }

            return getAssignmentResult;
        }

        public AssignmentListResponseModel GetAssignmentDetailsList(AssignmentListRequestModel assignmentRequestModel)
        {
            var paginationDto = assignmentRequestModel.Pagination != null ? _mapper.Map<PaginationDto>(assignmentRequestModel.Pagination) : null;
            var sortColumnDto = assignmentRequestModel.Sort?.Select(f => _mapper.Map<SortColumnDto>(f));
            var dateRangeDto = assignmentRequestModel.DateRange.StartDate != null ? _mapper.Map<DateRangeDto>(assignmentRequestModel.DateRange) : null;

            var getAssignmentList =_assignmentBaseRepository.GetAssignmentDetailsList(assignmentRequestModel.UserId, assignmentRequestModel.IsCompleted, sortColumnDto, paginationDto, dateRangeDto);

            if(getAssignmentList.Data.Items.Count() > 0)
            {
                foreach (var assignment in getAssignmentList.Data.Items)
                {
                    if(assignment.Attachment != null)
                    {
                        // Get Presigned URL
                        assignment.Attachment = _s3FileRepository.GetPreSignedUrl(_s3Settings.AssignmentAttachmentDirectory, assignment.Attachment, string.Empty, string.Empty, DateTime.Now.AddDays(2));
                    }
                }
            }

            return getAssignmentList;
        }

        public bool DeleteAssignment(long assignmentId, long userId)
        {
            return _assignmentBaseRepository.DeleteAssignment(assignmentId, userId);
        }
    }
}
