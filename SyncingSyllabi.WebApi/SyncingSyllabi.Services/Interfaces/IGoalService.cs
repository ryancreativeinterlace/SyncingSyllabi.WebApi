using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface IGoalService
    {
        GoalDto CreateGoal(GoalRequestModel goalRequestModel);
        GoalDto UpdateGoal(GoalRequestModel goalRequestModel);
        GoalDto GetGoalDetails(long goalId);
        PaginatedResultDto<GoalModel> GetGoalDetailsList(GoalRequestModel goalRequestModel);
        bool DeleteGoal(long goalId, long userId);
    }
}
