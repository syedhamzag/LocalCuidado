using AutoMapper;

namespace AwesomeCare.API.AutoMapperConfig
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
