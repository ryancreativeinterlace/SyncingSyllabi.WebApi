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
        }
    }
}
