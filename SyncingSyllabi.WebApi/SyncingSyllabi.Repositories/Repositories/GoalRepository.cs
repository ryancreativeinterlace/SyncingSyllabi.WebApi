using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Repositories.Repositories
{
    public partial class GoalBaseRepository
    {
        public GoalDto CreateGoal(GoalDto goalDto)
        {
            GoalDto goalResult = null;

            var goal = _mapper.Map<GoalEntity>(goalDto);

            UseDataContext(ctx =>
            {
                var getGoal = ctx.Goals
                             .AsNoTracking()
                             .Where(w => w.UserId == goal.UserId &&
                                    w.GoalTitle.ToLower() == goal.GoalTitle.ToLower())
                             .Select(s => _mapper.Map<GoalDto>(s))
                             .FirstOrDefault();

                if (getGoal == null)
                {
                    ctx.Goals.Add(goal);

                    goal.FillCreated(goal.UserId);
                    goal.FillUpdated(goal.UserId);

                    ctx.SaveChanges();

                    goalResult = _mapper.Map<GoalDto>(goal);
                }
            });

            return goalResult;
        }
    }
}
