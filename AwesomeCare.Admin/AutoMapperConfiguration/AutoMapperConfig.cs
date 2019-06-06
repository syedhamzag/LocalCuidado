using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.AutoMapperConfiguration
{
    public class AutoMapperConfig
    {
       

        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<MapperProfile>();
            });
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
