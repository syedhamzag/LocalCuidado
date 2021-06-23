using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientVoice;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientVoiceController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientVoice> _clientVoiceRepository;

        public ClientVoiceController(AwesomeCareDbContext dbContext, IGenericRepository<ClientVoice> clientVoiceRepository, IGenericRepository<Client> clientRepository)
        {
            _clientVoiceRepository = clientVoiceRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
        }
        #region ClientVoice
        /// <summary>
        /// Get All ClientVoice
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientVoice>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientVoiceRepository.Table.ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create ClientVoice
        /// </summary>
        /// <param name="postClientVoice"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientVoice postClientVoice)
        {
            if (postClientVoice == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ClientVoice = Mapper.Map<ClientVoice>(postClientVoice);
            await _clientVoiceRepository.InsertEntity(ClientVoice);
            return Ok();
        }
        /// <summary>
        /// Update ClientVoice
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientVoice model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientVoice = Mapper.Map<ClientVoice>(model);
            await _clientVoiceRepository.UpdateEntity(ClientVoice);
            return Ok();

        }

        /// <summary>
        /// Get ClientVoice by VoiceId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientVoice), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientVoice = await (from c in _clientVoiceRepository.Table
                                           where c.VoiceId == id.Value
                                           select new GetClientVoice
                                           {
                                               ClientId = c.ClientId,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               Deadline = c.Deadline,
                                               EvidenceOfActionTaken = c.EvidenceOfActionTaken,
                                               LessonLearntAndShared = c.LessonLearntAndShared,
                                               URL = c.URL,
                                               Remarks = c.Remarks,
                                               RotCause = c.RotCause,
                                               Status = c.Status,
                                               ActionRequired = c.ActionRequired,
                                               ActionsTakenByMPCC = c.ActionsTakenByMPCC,
                                               AreasOfImprovements = c.AreasOfImprovements,
                                               HealthGoalLongTerm = c.HealthGoalLongTerm,
                                               HealthGoalShortTerm = c.HealthGoalShortTerm,
                                               InterestedInPrograms = c.InterestedInPrograms,
                                               NextCheckDate = c.NextCheckDate,
                                               RateServiceRecieving = c.RateServiceRecieving,
                                               RateStaffAttending = c.RateStaffAttending,
                                               SomethingSpecial = c.SomethingSpecial,

                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientVoice);
        }
        #endregion
    }
}
