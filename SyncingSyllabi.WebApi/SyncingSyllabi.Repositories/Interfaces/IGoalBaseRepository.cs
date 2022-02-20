using SyncingSyllabi.Data.Dtos.Core;
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
    }
}
