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
    }
}
