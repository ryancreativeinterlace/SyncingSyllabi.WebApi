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
    }
}
