using AutoMapper;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;


namespace SyncingSyllabi.Data.Transformation.AutoMapperProfiles
{
    public class SyncingSyllabiMappingProfiles : Profile
    {
        public SyncingSyllabiMappingProfiles()
        {
            CreateMap<UserDto, UserModel>(MemberList.None).ReverseMap();
            CreateMap<AuthTokenDto, AuthModel>(MemberList.None).ReverseMap();
            CreateMap<GoalDto, GoalModel>(MemberList.None).ReverseMap();
            CreateMap<SortColumnDto, SortColumnModel>(MemberList.None).ReverseMap();
            CreateMap<PaginationDto, PaginationModel>(MemberList.None).ReverseMap();
            CreateMap<UserCodeDto, UserCodeModel>(MemberList.None).ReverseMap();
            CreateMap<UserEmailTrackingDto, UserEmailTrackingModel>(MemberList.None).ReverseMap();
            CreateMap<SyllabusDto, SyllabusModel>(MemberList.None).ReverseMap();
            CreateMap<AssignmentDto, AssignmentModel>(MemberList.None).ReverseMap();
            CreateMap<DateRangeDto, DateRangeModel>(MemberList.None).ReverseMap();
            CreateMap<UserNotificationDto, UserNotificationModel>(MemberList.None).ReverseMap();
        }
    }
}
