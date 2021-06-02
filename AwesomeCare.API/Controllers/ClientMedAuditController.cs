﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientMedicationAudit;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientMedAuditController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientMedAudit> _clientMedAuditRepository;
        
        public ClientMedAuditController(AwesomeCareDbContext dbContext, IGenericRepository<ClientMedAudit> clientMedAuditRepository, IGenericRepository<Client> clientRepository)
        {
            _clientMedAuditRepository = clientMedAuditRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
        }
        #region ClientMedAudit
        /// <summary>
        /// Get All ClientMedAudit
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientMedAudit>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientMedAuditRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientMedAudit
        /// </summary>
        /// <param name="postClientMedAudit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientMedAudit postClientMedAudit)
        {
            if (postClientMedAudit == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientMedAudit = Mapper.Map<ClientMedAudit>(postClientMedAudit);
            var newClientMedAudit = await _clientMedAuditRepository.InsertEntity(ClientMedAudit);
            var getClientMedAudit = Mapper.Map<GetClientMedAudit>(newClientMedAudit);
            return Ok(getClientMedAudit);


        }
        /// <summary>
        /// Update ClientMedAudit
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientMedAudit), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutClientMedAudit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _clientMedAuditRepository.GetEntity(model.MedAuditId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _clientMedAuditRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetClientMedAudit>(updateEntity);
            return Ok(getEntity);

        }
        /// <summary>
        /// Get ClientMedAudit by MedAuditId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientMedAudit), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientMedAudit = await (from c in _clientMedAuditRepository.Table
                                           where c.MedAuditId == id
                                           select new GetClientMedAudit
                                           {
                                               ClientId = c.ClientId,
                                               ActionRecommended = c.ActionRecommended,
                                               ActionTaken = c.ActionTaken,
                                               Attachment = c.Attachment,
                                               Date = c.Date,
                                               NextDueDate = c.NextDueDate,
                                               Deadline = c.Deadline,
                                               EvidenceOfActionTaken = c.EvidenceOfActionTaken,
                                               GapsInAdmistration = c.GapsInAdmistration,
                                               HardCopyReview = c.HardCopyReview,
                                               LessonLearntAndShared = c.LessonLearntAndShared,
                                               LogURL = c.LogURL,
                                               MarChartReview = c.MarChartReview,
                                               MedicationConcern = c.MedicationConcern,
                                               MedicationInfoUploadEefficiency = c.MedicationInfoUploadEefficiency,
                                               MedicationSupplyEfficiency = c.MedicationSupplyEfficiency,
                                               NameOfAuditor = c.NameOfAuditor,
                                               Observations = c.Observations,
                                               OfficerToTakeAction = c.OfficerToTakeAction,
                                               Remarks = c.Remarks,
                                               RepeatOfIncident = c.RepeatOfIncident,
                                               RightsOfMedication = c.RightsOfMedication,
                                               RotCause = c.RotCause,
                                               Status = c.Status,
                                               ThinkingServiceUsers = c.ThinkingServiceUsers,
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientMedAudit);
        }
        #endregion
    }
}