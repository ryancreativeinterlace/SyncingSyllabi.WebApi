using AutoMapper;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Transformation.AutoMapperProfiles
{
    public class SyncingSyllabiMappingProfiles : Profile
    {
        public SyncingSyllabiMappingProfiles()
        {
            CreateMap<UserDto, UserModel>(MemberList.None).ReverseMap();
        }
    }
}
