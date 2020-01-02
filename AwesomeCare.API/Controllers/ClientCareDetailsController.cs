using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientCareDetails;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientCareDetailsController : ControllerBase
    {
        private IGenericRepository<ClientCareDetails> _clientCareDetailsRepository;
        private ILogger<ClientCareDetailsController> _logger;

        public ClientCareDetailsController(IGenericRepository<ClientCareDetails> clientCareDetailsRepository, ILogger<ClientCareDetailsController> logger)
        {
            _clientCareDetailsRepository = clientCareDetailsRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get ClientCareDetails by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientCareDetailsById")]
        [ProducesResponseType(type: typeof(GetClientCareDetails), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _clientCareDetailsRepository.Table.ProjectTo<GetClientCareDetails>().FirstOrDefault(d => d.ClientCareDetailsId == id);

            return Ok(getEntity);
        }

        /// <summary>
        /// Create ClientRota
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(type: typeof(GetClientCareDetails), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]PostClientCareDetails model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var postEntity = Mapper.Map<ClientCareDetails>(model);
            var newEntity = await _clientCareDetailsRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetClientCareDetails>(newEntity);

            return CreatedAtRoute("GetClientCareDetailsById", new { id = getEntity.ClientCareDetailsId }, getEntity);
        }
    }
}