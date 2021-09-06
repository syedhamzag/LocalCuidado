using AutoMapper;
using System;
using Xunit;
using MapperConfig;

namespace MapperConfig.Tests
{
    public class MapperTest
    {
        [Fact]
        public void OfficeLocationProfileTest()
        {
            var config = new MapperConfiguration(c=>
            c.AddProfile<OfficeLocationProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}
