using AutoMapper;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Services
{
    public class GoalService : IGoalService
    {
        private readonly IMapper _mapper;
        private readonly IGoalBaseRepository _goalBaseRepository;

        public GoalService
        (
            IMapper mapper,
            IGoalBaseRepository goalBaseRepository
        )
        {
            _mapper = mapper;
            _goalBaseRepository = goalBaseRepository;
        }

        public GoalDto CreateGoal(GoalRequestModel goalRequestModel)
        {
            GoalDto creteGoalResult = null;

            var goalModel = new GoalModel();
            goalModel.UserId = goalRequestModel.UserId;
            goalModel.GoalTitle = goalRequestModel.GoalTitle.Trim();
            goalModel.GoalDescription = goalRequestModel.GoalDescription.Trim();
            goalModel.GoalDateStart = goalRequestModel.GoalDateStart;
            goalModel.GoalDateEnd = goalRequestModel.GoalDateEnd;
            goalModel.GoalType = (GoalTypeEnum)goalRequestModel.GoalType;
            goalModel.IsActive = true;
            goalModel.IsCompleted = false;
            goalModel.IsArchived = false;

            switch (goalRequestModel.GoalType)
            {
                case (int)GoalTypeEnum.ShortTerm:
                    goalModel.GoalTypeName = "Short-Term";
                    break;

                case (int)GoalTypeEnum.MediumTerm:
                    goalModel.GoalTypeName = "Medium-Term";
                    break;

                case (int)GoalTypeEnum.LongTerm:
                    goalModel.GoalTypeName = "Long-Term";
                    break;
            }

            GoalDto goal = _mapper.Map<GoalDto>(goalModel);

            if (goal != null)
            {
                creteGoalResult = _goalBaseRepository.CreateGoal(goal);
            }

            return creteGoalResult;
        }

        public GoalDto UpdateGoal(GoalRequestModel goalRequestModel)
        {
            GoalDto updatedGoalResult = null;

            var goalModel = new GoalModel();
            goalModel.Id = goalRequestModel.GoalId;
            goalModel.UserId = goalRequestModel.UserId;
            goalModel.GoalTitle = !string.IsNullOrEmpty(goalRequestModel.GoalTitle) ? goalRequestModel.GoalTitle.Trim() : string.Empty;
            goalModel.GoalDescription = !string.IsNullOrEmpty(goalRequestModel.GoalDescription) ? goalRequestModel.GoalDescription.Trim() : string.Empty;
            goalModel.GoalDateStart = goalRequestModel.GoalDateStart ?? null;
            goalModel.GoalDateEnd = goalRequestModel.GoalDateEnd ?? null;
            goalModel.GoalType = goalRequestModel.GoalType != null ? (GoalTypeEnum)goalRequestModel.GoalType : 0;
            goalModel.IsActive = goalRequestModel.IsActive ?? null;
            goalModel.IsCompleted = goalRequestModel.IsCompleted ?? null;
            goalModel.IsArchived = goalRequestModel.IsAchieved ?? null;

            switch (goalRequestModel.GoalType)
            {
                case (int)GoalTypeEnum.ShortTerm:
                    goalModel.GoalTypeName = "Short-Term";
                    break;

                case (int)GoalTypeEnum.MediumTerm:
                    goalModel.GoalTypeName = "Medium-Term";
                    break;

                case (int)GoalTypeEnum.LongTerm:
                    goalModel.GoalTypeName = "Long-Term";
                    break;
            }

            GoalDto goal = _mapper.Map<GoalDto>(goalModel);

            if (goal != null)
            {
                updatedGoalResult = _goalBaseRepository.UpdateGoal(goal);
            }

            return updatedGoalResult;
        }

        public GoalDto GetGoalDetails(long goalId)
        {
            GoalDto getGoalResult = null;

            getGoalResult = _goalBaseRepository.GetGoalDetails(goalId);

            return getGoalResult;
        }
    }
}
