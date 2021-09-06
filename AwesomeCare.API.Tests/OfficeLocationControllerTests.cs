using AutoMapper;
using AwesomeCare.API.Controllers;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.OfficeLocation;
using AwesomeCare.Model.Models;
using MapperConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace AwesomeCare.API.Tests
{
    public class OfficeLocationControllerTests
    {
        public OfficeLocationControllerTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<OfficeLocationProfile>();
            });
           
        }
        [Fact]
        public async Task Get_SingleItem_ById_Test()
        {
            //Arrange
            var logger = new Mock<ILogger<OfficeLocationController>>();
            var genericRepository = new Mock<IGenericRepository<OfficeLocation>>();
          
            var officeLocationFiller = new Filler<OfficeLocation>();
            var officeLocation = officeLocationFiller.Create();
            genericRepository.Setup(r => r.GetEntity(It.IsAny<int>()))
                .ReturnsAsync(officeLocation);

            var controller = new OfficeLocationController(logger.Object, genericRepository.Object);
            //Act
            var result = await controller.Get(1);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.IsType<GetOfficeLocation>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
            
        }
    }
}
