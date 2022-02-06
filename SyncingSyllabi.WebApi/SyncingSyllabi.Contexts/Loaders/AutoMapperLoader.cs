using AutoMapper;
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

            // Sample
            // config.CreateMap<CampaignDelivery, CampaignDeliveryDto>(MemberList.None).ReverseMap();

            var mapperConfig = new MapperConfiguration(config);
            mapperConfig.AssertConfigurationIsValid();

            return new Mapper(mapperConfig);
        }
    }
}
