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

        public GoalDto CreateGoal (GoalRequestModel goalRequestModel)
        {
            GoalDto goalResult = null;

            var goalModel = new GoalModel();
            goalModel.UserId = goalRequestModel.UserId;
            goalModel.GoalTitle = goalRequestModel.GoalTitle.Trim();
            goalModel.GoalDescription = goalRequestModel.GoalDescription.Trim();
            goalModel.GoalDateStart = goalRequestModel.GoalDateStart;
            goalModel.GoalDateEnd = goalRequestModel.GoalDateEnd;
            goalModel.GoalType = (GoalTypeEnum)goalRequestModel.GoalType;
            goalModel.Active = true;
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

                default:
                    goalModel.GoalTypeName = "None";
                    break;
            }

            GoalDto goal = _mapper.Map<GoalDto>(goalModel);

            if(goal != null)
            {
                goalResult = _goalBaseRepository.CreateGoal(goal);
            }

            return goalResult;
        }
    }
}
