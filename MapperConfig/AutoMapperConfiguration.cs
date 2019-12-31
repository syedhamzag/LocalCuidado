using System;
using AutoMapper;
namespace MapperConfig
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
