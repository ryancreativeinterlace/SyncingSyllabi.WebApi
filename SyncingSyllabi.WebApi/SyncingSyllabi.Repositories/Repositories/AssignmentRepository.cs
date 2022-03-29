using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
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
    }
}
