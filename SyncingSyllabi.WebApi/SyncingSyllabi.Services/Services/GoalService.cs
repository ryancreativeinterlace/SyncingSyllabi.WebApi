using AutoMapper;
using SyncingSyllabi.Common.Tools.Helpers;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Services.Services
{
    public class GoalService : IGoalService
    {
        private readonly IMapper _mapper;
        private readonly IGoalBaseRepository _goalBaseRepository;
        private readonly IUserBaseRepository _userBaseRepository;

        public GoalService
        (
            IMapper mapper,
            IGoalBaseRepository goalBaseRepository,
            IUserBaseRepository userBaseRepository
        )
        {
            _mapper = mapper;
            _goalBaseRepository = goalBaseRepository;
            _userBaseRepository = userBaseRepository;
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
            goalModel.IsArchived = goalRequestModel.IsArchived ?? null;

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

        public PaginatedResultDto<GoalModel> GetGoalDetailsList(GoalRequestModel goalRequestModel)
        {
            PaginatedResultDto<GoalModel> result = null;

            var paginationDto = goalRequestModel.Pagination != null ? _mapper.Map<PaginationDto>(goalRequestModel.Pagination) : null;
            var sortColumnDto = goalRequestModel.Sort?.Select(f => _mapper.Map<SortColumnDto>(f));

            // get user timeZone
            var user = _userBaseRepository.GetUserById(goalRequestModel.UserId);

            if (goalRequestModel.UserId == 0)
            {
                // Dummy Data
                result = new PaginatedResultDto<GoalModel>()
                {
                    Items = new List<GoalModel>()
                    {
                        new GoalModel()
                        {
                             Id = 0,
                            UserId = 0,
                            GoalTitle = "My Long Time Goal",
                            GoalDescription = "Long Term Goal",
                            IsActive = true,
                            IsArchived = false,
                            GoalType = GoalTypeEnum.LongTerm,
                            GoalTypeName = GoalTypeEnum.LongTerm.ToString(),
                            IsCompleted = false,
                            GoalDateStart = DateTime.Now.AddDays(-3),
                            GoalDateEnd = DateTime.Now.AddDays(1),
                            CreatedBy = 1,
                            DateCreated = DateTime.Now.AddDays(-3),
                            UpdatedBy = 1,
                            DateUpdated = DateTime.Now.AddDays(-3),
                        },
                        new GoalModel()
                        {   
                            Id = 0,
                            UserId = 0,
                            GoalTitle = "My Mid Time Goal",
                            GoalDescription = "Mid Term Goal",
                            IsActive = true,
                            IsArchived = false,
                            GoalType = GoalTypeEnum.MediumTerm,
                            GoalTypeName = GoalTypeEnum.MediumTerm.ToString(),
                            IsCompleted = false,
                            GoalDateStart = DateTime.Now.AddDays(-3),
                            GoalDateEnd = DateTime.Now,
                            CreatedBy = 1,
                            DateCreated = DateTime.Now.AddDays(-3),
                            UpdatedBy = 1,
                            DateUpdated = DateTime.Now.AddDays(-3),
                        },
                        new GoalModel()
                        {
                            Id = 0,
                            UserId = 0,
                            GoalTitle = "My Short Time Goal",
                            GoalDescription = "Short Term Goal",
                            IsActive = true,
                            IsArchived = false,
                            GoalType = GoalTypeEnum.ShortTerm,
                            GoalTypeName = GoalTypeEnum.ShortTerm.ToString(),
                            IsCompleted = false,
                            GoalDateStart = DateTime.Now.AddDays(-3),
                            GoalDateEnd = DateTime.Now.AddDays(-1),
                            CreatedBy = 1,
                            DateCreated = DateTime.Now.AddDays(-3),
                            UpdatedBy = 1,
                            DateUpdated = DateTime.Now.AddDays(-3)
                        }
                    },
                    Success = true,
                    TotalCount = 3,
                    Take = 3,
                    Skip = 0
                };

            }
            else
            {
                var goalList = _goalBaseRepository.GetGoalDetailsList(goalRequestModel.UserId, sortColumnDto, paginationDto);

                if(goalList.Items.Count() > 0)
                {
                    foreach(var goalItem in goalList.Items)
                    {
                        // convert to user time zone
                        goalItem.GoalDateStart = TimeZoneHelper.ConvertToUserTimeZone(goalItem.GoalDateStart.Value, user.TimeZone);
                        goalItem.GoalDateEnd = TimeZoneHelper.ConvertToUserTimeZone(goalItem.GoalDateEnd.Value, user.TimeZone);
                        goalItem.DateCreated = TimeZoneHelper.ConvertToUserTimeZone(goalItem.DateCreated, user.TimeZone);
                        goalItem.DateUpdated = TimeZoneHelper.ConvertToUserTimeZone(goalItem.DateUpdated, user.TimeZone);
                    }
                }

                result = _goalBaseRepository.GetGoalDetailsList(goalRequestModel.UserId, sortColumnDto, paginationDto);
            }

            return result;
        }

        public bool DeleteGoal(long goalId, long userId)
        {
            return _goalBaseRepository.DeleteGoal(goalId, userId);
        }
    }
}
