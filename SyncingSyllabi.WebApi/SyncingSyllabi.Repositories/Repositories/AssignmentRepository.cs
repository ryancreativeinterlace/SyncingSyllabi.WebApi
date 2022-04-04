using SyncingSyllabi.Common.Tools.Extensions;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Repositories.Repositories
{
    public partial class AssignmentBaseRepository
    {
        public AssignmentDto CreateAssignment(AssignmentDto assignmentDto)
        {
            AssignmentDto result = null;

            var assignment = _mapper.Map<AssignmentEntity>(assignmentDto);

            UseDataContext(ctx =>
            {
                var getAssignment = ctx.Assignments
                                     .AsNoTracking()
                                     .Where(w =>
                                            w.UserId == assignment.UserId &&
                                            w.SyllabusId == assignment.SyllabusId &&
                                            w.IsActive.Value)
                                     .Select(s => _mapper.Map<AssignmentEntity>(s))
                                     .FirstOrDefault();

                if (getAssignment == null)
                {
                    assignment.FillCreated(assignment.UserId);
                    assignment.FillUpdated(assignment.UserId);

                    ctx.Assignments.Add(assignment);

                    ctx.SaveChanges();

                    result = _mapper.Map<AssignmentDto>(assignment);
                }
            });

            return result;
        }

        public AssignmentDto UpdateAssignment(AssignmentDto assignmentDto)
        {
            AssignmentDto result = null;

            var assignment = _mapper.Map<AssignmentEntity>(assignmentDto);

            UseDataContext(ctx =>
            {
                var getAssignment = ctx.Assignments
                                 .AsNoTracking()
                                 .Where(w => w.Id == assignment.Id && w.UserId == assignment.UserId && w.IsActive.Value)
                                 .Select(s => _mapper.Map<AssignmentEntity>(s))
                                 .FirstOrDefault();

                if (getAssignment != null)
                {
                    getAssignment.Notes = !string.IsNullOrEmpty(assignment.Notes) ? assignment.Notes : getAssignment.Notes;
                    getAssignment.ColorInHex = !string.IsNullOrEmpty(assignment.ColorInHex) ? assignment.ColorInHex : getAssignment.ColorInHex;
                    getAssignment.AssignmentDateStart = assignment.AssignmentDateStart ?? getAssignment.AssignmentDateStart;
                    getAssignment.AssignmentDateEnd = assignment.AssignmentDateEnd ?? getAssignment.AssignmentDateEnd;
                    getAssignment.IsActive = assignment.IsActive ?? getAssignment.IsActive;

                    getAssignment.FillCreated(getAssignment.UserId);
                    getAssignment.FillUpdated(getAssignment.UserId);

                    ctx.Assignments.Update(getAssignment);

                    ctx.SaveChanges();

                    result = _mapper.Map<AssignmentDto>(getAssignment);
                }
            });

            return result;
        }

        public AssignmentDto GetAssignment(long assignmentId, long userId)
        {
            AssignmentDto result = null;

            UseDataContext(ctx =>
            {

                var getAssignment = ctx.Assignments
                                     .AsNoTracking()
                                     .Where(w => w.Id == assignmentId && w.UserId == userId && w.IsActive.Value)
                                     .Select(s => _mapper.Map<AssignmentEntity>(s))
                                     .FirstOrDefault();

                result = _mapper.Map<AssignmentDto>(getAssignment);

            });

            return result;
        }

        public AssignmentListResponseModel GetAssignmentDetailsList(long userId, IEnumerable<SortColumnDto> sortColumn, PaginationDto pagination)
        {
            var result = new AssignmentListResponseModel();

            var errorList = new List<string>();

            IEnumerable<AssignmentModel> getAssignmentListResult = Enumerable.Empty<AssignmentModel>();

            UseDataContext(ctx =>
            {
                var getAssignmentList = ctx.Assignments
                                         .AsNoTracking()
                                         .Where(w => w.UserId == userId &&
                                                w.IsActive.Value)
                                         .Select(s => _mapper.Map<AssignmentDto>(s))
                                         .ToList();

                if (getAssignmentList.Count() > 0)
                {
                    getAssignmentListResult = _mapper.Map<IEnumerable<AssignmentModel>>(getAssignmentList);

                    if (sortColumn.Count() > 0)
                    {
                        getAssignmentListResult = getAssignmentListResult.MultipleSort<AssignmentModel>(sortColumn.ToList(), SortTypeEnum.Assignment).ToList();
                    }

                    if (getAssignmentListResult.Count() > 0)
                    {
                        result.Data = getAssignmentListResult.Page(pagination);
                    }
                }
                else
                {
                    errorList.Add("No result");
                }

                if(errorList.Count > 0)
                {
                    result.Errors = errorList;
                    result.Data.Success = false;
                }
                
            });

            return result;
        }
    }
}
