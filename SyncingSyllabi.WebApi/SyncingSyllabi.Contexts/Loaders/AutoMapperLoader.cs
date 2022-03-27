using AutoMapper;
using AutoMapper.Configuration;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Loaders
{
    public class AutoMapperLoader
    {
        public static IMapper Load()
        {
            var config = new MapperConfigurationExpression();

            // Entities to Dtos
            config.CreateMap<UserEntity, UserDto>(MemberList.None).ReverseMap();
            config.CreateMap<AuthTokenEntity, AuthTokenDto>(MemberList.None).ReverseMap();
            config.CreateMap<GoalEntity, GoalDto>(MemberList.None).ReverseMap();
            config.CreateMap<UserCodeEntity, UserCodeDto>(MemberList.None).ReverseMap();
            config.CreateMap<UserEmailTrackingEntity, UserEmailTrackingDto>(MemberList.None).ReverseMap();
            config.CreateMap<SyllabusEntity, SyllabusDto>(MemberList.None).ReverseMap();

            // Dtos to Models
            config.CreateMap<UserDto, UserModel>(MemberList.None).ReverseMap();
            config.CreateMap<AuthTokenDto, AuthModel>(MemberList.None).ReverseMap();
            config.CreateMap<GoalDto, GoalModel>(MemberList.None).ReverseMap();
            config.CreateMap<SortColumnDto, SortColumnModel>(MemberList.None).ReverseMap();
            config.CreateMap<PaginationDto, PaginationModel>(MemberList.None).ReverseMap();
            config.CreateMap<UserCodeDto, UserCodeModel>(MemberList.None).ReverseMap();
            config.CreateMap<UserEmailTrackingDto, UserEmailTrackingModel>(MemberList.None).ReverseMap();
            config.CreateMap<SyllabusDto, SyllabusModel>(MemberList.None).ReverseMap();

            var mapperConfig = new MapperConfiguration(config);
            mapperConfig.AssertConfigurationIsValid();

            return new Mapper(mapperConfig);
        }
    }
}
