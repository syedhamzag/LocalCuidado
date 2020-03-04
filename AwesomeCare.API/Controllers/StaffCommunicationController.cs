using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffCommunication;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffCommunicationController : ControllerBase
    {
        private IGenericRepository<StaffCommunication> _staffCommunicationRepository;
        private ILogger<StaffCommunicationController> _logger;
        private IDbContext _dbContext;

        public StaffCommunicationController(IGenericRepository<StaffCommunication> staffCommunicationRepository, ILogger<StaffCommunicationController> logger, IDbContext dbContext)
        {
            _staffCommunicationRepository = staffCommunicationRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}", Name = "GetStaffCommunicationById")]
        [ProducesResponseType(type: typeof(GetStaffCommunication), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAsync(int? id)
        {
            var staffEntity = _dbContext.Set<StaffPersonalInfo>();
            var baseRecordItemEntity = _dbContext.Set<BaseRecordItemModel>();
            var getEntities = (from staffcom in _staffCommunicationRepository.Table
                               join personinvolved in staffEntity on staffcom.PersonInvolved equals personinvolved.StaffPersonalInfoId
                               join personresponsible in staffEntity on staffcom.PersonResponsibleForAction equals personresponsible.StaffPersonalInfoId
                               join @class in baseRecordItemEntity on staffcom.CommunicationClassId equals @class.BaseRecordItemId
                               join status in baseRecordItemEntity on staffcom.Status equals status.BaseRecordItemId
                               where staffcom.StaffCommunicationId == id
                               select new GetStaffCommunication
                               {
                                   Status = status.ValueName,
                                   ActionTaken = staffcom.ActionTaken,
                                   Attachment = staffcom.Attachment,
                                   CommunicationClass = @class.ValueName,
                                   Concern = staffcom.Concern,
                                   ExpectedAction = staffcom.ExpectedAction,
                                   PersonInvolved = string.Concat(personinvolved.FirstName, " ", personinvolved.MiddleName, " ", personinvolved.LastName),
                                   PersonResponsibleForAction = string.Concat(personresponsible.FirstName, " ", personresponsible.MiddleName, " ", personresponsible.LastName),
                                   StaffCommunicationId = staffcom.StaffCommunicationId,
                                   Telephone = staffcom.Telephone,
                                   ValueDate = staffcom.ValueDate
                               }).FirstOrDefault();


            return Ok(getEntities);
        }


        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffCommunication>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            var staffEntity = _dbContext.Set<StaffPersonalInfo>();
            var baseRecordItemEntity = _dbContext.Set<BaseRecordItemModel>();
            var getEntities = (from staffcom in _staffCommunicationRepository.Table
                          join personinvolved in staffEntity on staffcom.PersonInvolved equals personinvolved.StaffPersonalInfoId
                          join personresponsible in staffEntity on staffcom.PersonResponsibleForAction equals personresponsible.StaffPersonalInfoId
                          join @class in baseRecordItemEntity on staffcom.CommunicationClassId equals @class.BaseRecordItemId
                          join status in baseRecordItemEntity on staffcom.Status equals status.BaseRecordItemId
                          select new GetStaffCommunication
                          {
                              Status = status.ValueName,
                              ActionTaken = staffcom.ActionTaken,
                              Attachment = staffcom.Attachment,
                              CommunicationClass = @class.ValueName,
                              Concern = staffcom.Concern,
                              ExpectedAction = staffcom.ExpectedAction,
                              PersonInvolved = string.Concat(personinvolved.FirstName, " ", personinvolved.MiddleName, " ", personinvolved.LastName),
                              PersonResponsibleForAction = string.Concat(personresponsible.FirstName, " ", personresponsible.MiddleName, " ", personresponsible.LastName),
                              StaffCommunicationId = staffcom.StaffCommunicationId,
                              Telephone = staffcom.Telephone,
                              ValueDate = staffcom.ValueDate
                          }).ToList();

           
            return Ok(getEntities);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(GetStaffCommunication), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody]PostStaffCommunication model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<StaffCommunication>(model);
            var entity = await _staffCommunicationRepository.InsertEntity(postEntity);

            return Ok();
            // return CreatedAtAction("GetAsync", new { id = entity.StaffCommunicationId }, entity);


        }


    }
}