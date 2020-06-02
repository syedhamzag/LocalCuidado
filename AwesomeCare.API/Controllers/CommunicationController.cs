using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Communication;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        private IGenericRepository<Communication> _communicationRepository;
        private ILogger<CommunicationController> _logger;

        public CommunicationController(IGenericRepository<Communication> communicationRepository, ILogger<CommunicationController> logger)
        {
            _communicationRepository = communicationRepository;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]PostCommunication model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            // var identities = this.User.Claims.ToList() ;
            var communication = Mapper.Map<Communication>(model);
            var userId = this.User.SubClaim();
            communication.From = userId;
            var entity = await _communicationRepository.InsertEntity(communication);

            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var userId = this.User.SubClaim();
            var entities = _communicationRepository.Table.Where(c => c.From == userId).ToList();
            return Ok(entities);
        }
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{

        //}
    }
}