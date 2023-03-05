
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AwesomeCare.Services.Services;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.Model.Models;
using Microsoft.Extensions.Configuration;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TestEmailController : ControllerBase
    {
        private readonly IMailChimpService emailService;
        private readonly IGoogleService googleService;
        private readonly ILogger<TestEmailController> logger;
        private readonly IGenericRepository<Client> clientRepository;
        private readonly IConfiguration configuration;

        public TestEmailController(ILogger<TestEmailController> logger,
            IMailChimpService emailService,
             IGoogleService googleService,
             IGenericRepository<Client> clientRepository,
             IConfiguration configuration)
        {
            this.emailService = emailService;
            this.googleService = googleService;
            this.logger = logger;
            this.clientRepository = clientRepository;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(int clientId, string distance, string staffGeolocation)
        {
            var result = await IsLocationValid(clientId, distance, staffGeolocation);
            // Instantiate new manager
            //var result = await emailService.SendAsync("Cuidado Test", "Email testing", true, new List<AwesomeCare.DataTransferObject.Models.MailChimp.Recipient>
            //{
            //    new AwesomeCare.DataTransferObject.Models.MailChimp.Recipient
            //    {
            //         Email ="olamidejames007@gmail.com",
            //          Name = "Olamide James",
            //          Type= DataTransferObject.Enums.EmailTypeEnum.to
            //    }
            //});
            return Ok(result);
        }

        async Task<bool> IsLocationValid(int clientId, string distance, string staffGeolocation)
        {
            var client = await clientRepository.GetEntity(clientId);
            if (client != null)
            {
                var staffGeo = staffGeolocation.Split(';');
                var origin = $"{staffGeo[0]},{staffGeo[1]}";
                var destination = $"{client.Latitude},{client.Longitude}";

                var getDistance = await googleService.DistanceMatrix(origin, destination, "imperial", "walking", configuration["Google:key"].ToString());
                             
                
                if (!getDistance.IsSuccessStatusCode) return false;

                var content = getDistance.Content;

                var jsonResult = content.ToString();
                logger.LogInformation($"Google Distance Matrix for Rota {1} ClientId {clientId} response is {jsonResult}");
                if (content.Rows.Count == 0) return false; 

                var element = content.Rows[0].Elements[0];
                if (!element.Status.Equals("ok",System.StringComparison.OrdinalIgnoreCase)) return false;

                var distanceValue = double.Parse(element.Distance.Value.ToString());

                //  var clockInDistance = double.TryParse(distance, out double clockDistance) ? clockDistance : 0;
                logger.LogInformation($"{client.Firstname} with clientId {clientId} has location distance {client.LocationDistance}");
                return (distanceValue <= client.LocationDistance);
            }
            return false;
        }

    }
}
