﻿using SyncingSyllabi.Contexts.Entities;
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
            GoalDto createGoalResult = null;

            var goal = _mapper.Map<GoalEntity>(goalDto);

            UseDataContext(ctx =>
            {
                var getGoal = ctx.Goals
                             .AsNoTracking()
                             .Where(w => w.UserId == goal.UserId &&
                                    w.GoalTitle.ToLower() == goal.GoalTitle.ToLower())
                             .Select(s => _mapper.Map<GoalEntity>(s))
                             .FirstOrDefault();

                if (getGoal == null)
                {
                    ctx.Goals.Add(goal);

                    goal.FillCreated(goal.UserId);
                    goal.FillUpdated(goal.UserId);

                    ctx.SaveChanges();

                    createGoalResult = _mapper.Map<GoalDto>(goal);
                }
            });

            return createGoalResult;
        }

        public GoalDto UpdateGoal(GoalDto goalDto)
        {
            GoalDto updateGoalResult = null;

            var goal = _mapper.Map<GoalEntity>(goalDto);

            UseDataContext(ctx =>
            {
                var getGoal = ctx.Goals
                             .AsNoTracking()
                             .Where(w => w.Id == goalDto.Id &&
                                    w.UserId == goal.UserId)
                             .Select(s => _mapper.Map<GoalEntity>(s))
                             .FirstOrDefault();

                if (getGoal != null)
                {
                    getGoal.GoalTitle = !string.IsNullOrEmpty(goal.GoalTitle) ? goal.GoalTitle : getGoal.GoalTitle;
                    getGoal.GoalTitle = !string.IsNullOrEmpty(goal.GoalDescription) ? goal.GoalDescription : getGoal.GoalDescription ;
                    getGoal.GoalDateStart = goal.GoalDateStart ?? getGoal.GoalDateStart;
                    getGoal.GoalDateEnd = goal.GoalDateEnd ?? getGoal.GoalDateEnd;
                    getGoal.GoalType = goal.GoalType != 0 ? goal.GoalType : getGoal.GoalType;
                    getGoal.GoalTypeName = !string.IsNullOrEmpty(goal.GoalTypeName) ? goal.GoalTypeName : getGoal.GoalTypeName;
                    getGoal.IsActive = goal.IsActive ?? getGoal.IsActive;
                    getGoal.IsCompleted = goal.IsCompleted ?? getGoal.IsCompleted;
                    getGoal.IsArchived = goal.IsArchived ?? getGoal.IsArchived;

                    getGoal.FillUpdated(getGoal.UserId);

                    ctx.Goals.Update(getGoal);

                    ctx.SaveChanges();

                    updateGoalResult = _mapper.Map<GoalDto>(goal);
                }
            });

            return updateGoalResult;
        }
    }
}
