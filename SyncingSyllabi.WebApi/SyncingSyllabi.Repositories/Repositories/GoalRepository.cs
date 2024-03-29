﻿using SyncingSyllabi.Common.Tools.Extensions;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
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
                             .Where(w => w.Id == goal.Id)
                             .Select(s => _mapper.Map<GoalEntity>(s))
                             .FirstOrDefault();

                if (getGoal == null)
                {
                    goal.FillCreated(goal.UserId);
                    goal.FillUpdated(goal.UserId);

                    ctx.Goals.Add(goal);

                    ctx.SaveChanges();

                    createGoalResult = _mapper.Map<GoalDto>(goal);
                }
            });

            return createGoalResult;
        }

        public GoalDto UpdateGoal(GoalDto goalDto)
        {
            GoalDto updatedGoalResult = null;

            var goal = _mapper.Map<GoalEntity>(goalDto);

            UseDataContext(ctx =>
            {
                var getGoal = ctx.Goals
                             .AsNoTracking()
                             .Where(w => w.Id == goal.Id &&
                                    w.UserId == goal.UserId)
                             .Select(s => _mapper.Map<GoalEntity>(s))
                             .FirstOrDefault();

                if (getGoal != null)
                {
                    getGoal.GoalTitle = !string.IsNullOrEmpty(goal.GoalTitle) ? goal.GoalTitle : getGoal.GoalTitle;
                    getGoal.GoalDescription = !string.IsNullOrEmpty(goal.GoalDescription) ? goal.GoalDescription : getGoal.GoalDescription;
                    getGoal.GoalDateStart = goal.GoalDateStart ?? getGoal.GoalDateStart;
                    getGoal.GoalDateEnd = goal.GoalDateEnd ?? getGoal.GoalDateEnd;
                    getGoal.GoalType = goal.GoalType != 0 ? goal.GoalType : getGoal.GoalType;
                    getGoal.GoalTypeName = !string.IsNullOrEmpty(goal.GoalTypeName) ? goal.GoalTypeName : getGoal.GoalTypeName;
                    getGoal.IsActive = goal.IsActive ?? getGoal.IsActive;
                    getGoal.IsCompleted = goal.IsCompleted ?? getGoal.IsCompleted;
                    getGoal.IsArchived = goal.IsArchived ?? getGoal.IsArchived;

                    getGoal.FillCreated(getGoal.UserId);
                    getGoal.FillUpdated(getGoal.UserId);

                    ctx.Goals.Update(getGoal);

                    ctx.SaveChanges();

                    updatedGoalResult = _mapper.Map<GoalDto>(getGoal);
                }
            });

            return updatedGoalResult;
        }

        public GoalDto GetGoalDetails(long goalId)
        {
            GoalDto getGoalResult = null;

            UseDataContext(ctx =>
            {
                var getGoal = ctx.Goals
                             .AsNoTracking()
                             .Where(w => w.Id == goalId &&
                                    w.IsActive.Value)
                             .Select(s => _mapper.Map<GoalEntity>(s))
                             .FirstOrDefault();

                if(getGoal != null)
                {
                    getGoalResult = _mapper.Map<GoalDto>(getGoal);
                }
            });

            return getGoalResult;
        }

        public PaginatedResultDto<GoalModel> GetGoalDetailsList(long userId, IEnumerable<SortColumnDto> sortColumn, PaginationDto pagination)
        {
            IEnumerable<GoalModel> getGoalListResult = Enumerable.Empty<GoalModel>();

            UseDataContext(ctx =>
            {
                var getGoalList = ctx.Goals
                             .AsNoTracking()
                             .Where(w => w.UserId == userId &&
                                    w.IsActive.Value)
                             .Select(s => _mapper.Map<GoalDto>(s))
                             .ToList();

                if (getGoalList.Count() > 0)
                {
                    getGoalListResult = _mapper.Map<IEnumerable<GoalModel>>(getGoalList);
                }
                
                if(sortColumn.Count() > 0)
                {
                    getGoalListResult = getGoalListResult.MultipleSort<GoalModel>(sortColumn.ToList(), SortTypeEnum.Goal).ToList();
                }
            });

            return getGoalListResult.Page(pagination);
        }

        public bool DeleteGoal(long goalId, long userId)
        {
            bool result = false;

            UseDataContext(ctx =>
            {

                var getGoal = ctx.Goals
                              .AsNoTracking()
                              .Where(w => w.Id == goalId && w.UserId == userId && w.IsActive.Value)
                              .Select(s => _mapper.Map<GoalEntity>(s))
                              .FirstOrDefault();

                if (getGoal != null)
                {
                    getGoal.IsActive = false;

                    getGoal.FillCreated(getGoal.UserId);
                    getGoal.FillUpdated(getGoal.UserId);

                    ctx.Goals.Update(getGoal);

                    ctx.SaveChanges();

                    result = true;
                }
            });

            return result;
        }

        public IEnumerable<GoalDto> GetDueGoals(DateTime dateTime)
        {
            var result = new List<GoalDto>();

            UseDataContext(ctx =>
            {

                var getGoals = ctx.Goals
                                    .AsNoTracking()
                                    .Where(w => w.GoalDateEnd.HasValue &&
                                                w.GoalDateEnd.Value.Date == dateTime.Date &&
                                                w.IsActive.Value &&
                                                !w.IsCompleted.Value &&
                                                !w.IsArchived.Value)
                                    .Select(s => _mapper.Map<GoalDto>(s))
                                    .ToList();


                if (getGoals.Count > 0)
                {
                    result.AddRange(getGoals);
                }
            });

            return result;
        }
    }
}
