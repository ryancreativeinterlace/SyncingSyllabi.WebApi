using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface IGoalBaseRepository
    {
        GoalDto CreateGoal(GoalDto goalDto);
        GoalDto UpdateGoal(GoalDto goalDto);
        GoalDto GetGoalDetails(long goalId);
        PaginatedResultDto<GoalModel> GetGoalDetailsList(long userId, IEnumerable<SortColumnDto> sortColumn, PaginationDto pagination);
        bool DeleteGoal(long goalId, long userId);
    }
}
