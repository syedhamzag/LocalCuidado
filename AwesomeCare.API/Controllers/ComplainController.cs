﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.DataTransferObject.DTOs.ClientComplainRegister;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;

namespace AwesomeCare.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComplainController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientComplainRegister> _complainRepository;
        
        public ComplainController(AwesomeCareDbContext dbContext, IGenericRepository<ClientComplainRegister> complainRepository, IGenericRepository<Client> clientRepository)
        {
            _complainRepository = complainRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
        }
        #region ComplainRegister
        /// <summary>
        /// Get All Complain
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientComplainRegister>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _complainRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Get All Complain
        /// </summary>
        /// <returns></returns>
        [HttpGet("{complainId}")]
        [ProducesResponseType(type: typeof(GetClientComplainRegister), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int complainId)
        {
            var getEntities = _complainRepository.Table.ProjectTo<GetClientComplainRegister>().FirstOrDefault(s=>s.ComplainId==complainId);
            return Ok(getEntities);
        }
        /// <summary>
        /// Create Complain
        /// </summary>
        /// <param name="postComplain"></param>
        /// <returns></returns>
        [HttpPost("Complain")]
        [ProducesResponseType(type: typeof(GetClientComplainRegister), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostComplainRegister([FromBody] PostComplainRegister postComplain)
        {

            if (postComplain == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var complain = Mapper.Map<ClientComplainRegister>(postComplain);
            var newComplain = new ClientComplainRegister();
            if (complain.ComplainId > 0)
            {
                newComplain = await _complainRepository.UpdateEntity(complain);
            }
            else
            {
                newComplain = await _complainRepository.InsertEntity(complain);
            }

            var getComplain = Mapper.Map<GetClientComplainRegister>(newComplain);
            return CreatedAtAction("Get", new { complainId = getComplain.ComplainId }, getComplain);


        }
        /// <summary>
        /// Update Medication
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientComplainRegister), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutComplainRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _complainRepository.GetEntity(model.ComplainId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _complainRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetClientComplainRegister>(updateEntity);

            return Ok(getEntity);


        }
        /// <summary>
        /// Get Complain by ClientId and ComplainId
        /// </summary>
        /// <param name="complainId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("/GetComplain/{clientId}/{complainId}", Name = "GetClientComplainRegister")]
        [ProducesResponseType(type: typeof(GetClientComplainRegister), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComplain(int? clientId, int? complainId)
        {
            if (!complainId.HasValue)
                return BadRequest("id Parameter is required");

            var getComplain = await (from client in _clientRepository.Table
                                     where client.ClientId == clientId.Value
                                     select new GetClient
                                     {
                                         Firstname = client.Firstname,
                                         Middlename = client.Middlename,
                                         GetClientComplain = (from complain in _complainRepository.Table
                                                              where complain.ComplainId == complainId.Value
                                                              && complain.ClientId == clientId.Value
                                                              select new GetClientComplainRegister
                                                              {
                                                                  ClientId = complain.ClientId,
                                                                  ACTIONTAKEN = complain.ACTIONTAKEN,
                                                                  COMPLAINANTCONTACT = complain.COMPLAINANTCONTACT,
                                                                  CONCERNSRAISED = complain.CONCERNSRAISED,
                                                                  DATEOFACKNOWLEDGEMENT = complain.DATEOFACKNOWLEDGEMENT,
                                                                  DATERECIEVED = complain.DATERECIEVED,
                                                                  DUEDATE = complain.DUEDATE,
                                                                  EvidenceFilePath = complain.EvidenceFilePath,
                                                                  FINALRESPONSETOFAMILY = complain.FINALRESPONSETOFAMILY,
                                                                  INCIDENTDATE = complain.INCIDENTDATE,
                                                                  INVESTIGATIONOUTCOME = complain.INVESTIGATIONOUTCOME,
                                                                  IRFNUMBER = complain.IRFNUMBER,
                                                                  LETTERTOSTAFF = complain.LETTERTOSTAFF,
                                                                  LINK = complain.LINK,
                                                                  OFFICERTOACTId = complain.OFFICERTOACTId,
                                                                  REMARK = complain.REMARK,
                                                                  ROOTCAUSE = complain.ROOTCAUSE,
                                                                  SOURCEOFCOMPLAINTS = complain.SOURCEOFCOMPLAINTS,
                                                                  STAFFId = complain.STAFFId,
                                                                  StatusId = complain.StatusId,
                                                              }).ToList()
                                     }
                      ).FirstOrDefaultAsync();
            return Ok(getComplain);
        }
        #endregion

    }
}