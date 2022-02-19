using AutoMapper;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
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

        public GoalDto CreateGoal (GoalModel goalModel)
        {
            GoalDto goalResult = null;

            goalModel.GoalTitle.Trim();
            goalModel.GoalDescription.Trim();
            goalModel.Active = true;
            goalModel.IsCompleted = false;
            goalModel.IsArchived = false;

            switch (goalModel.GoalType)
            {
                case GoalTypeEnum.ShortTerm:
                    goalModel.GoalTypeName = "Short-Term";
                    break;

                case GoalTypeEnum.MediumTerm:
                    goalModel.GoalTypeName = "Medium-Term";
                    break;

                case GoalTypeEnum.LongTerm:
                    goalModel.GoalTypeName = "Long-Term";
                    break;

                default:
                    goalModel.GoalTypeName = "None";
                    break;
            }

            GoalDto goal = _mapper.Map<GoalDto>(goalModel);




            return goalResult;
        }
    }
}
