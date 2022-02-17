using AutoMapper;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
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

            config.CreateMap<UserEntity, UserDto>(MemberList.None).ReverseMap();
            config.CreateMap<AuthTokenEntity, AuthTokenDto>(MemberList.None).ReverseMap();

            var mapperConfig = new MapperConfiguration(config);
            mapperConfig.AssertConfigurationIsValid();

            return new Mapper(mapperConfig);
        }
    }
}
