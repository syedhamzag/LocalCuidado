﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Client;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;
        private IGenericRepository<BaseRecordModel> _baseRecordRepository;
        private IDbContext _dbContext;
        public ClientController(IDbContext dbContext, IGenericRepository<Client> clientRepository, IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, IGenericRepository<BaseRecordModel> baseRecordRepository)
        {
            _clientRepository = clientRepository;
            _baseRecordItemRepository = baseRecordItemRepository;
            _baseRecordRepository = baseRecordRepository;
            _dbContext = dbContext;
        }
        /// <summary>
        /// Create Client
        /// </summary>
        /// <param name="postClient"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostClient([FromBody]PostClient postClient)
        {
            if (postClient == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check if client is not already registered

            var isClientRegistered = _clientRepository.Table.Any(c => c.Email.Trim().Equals(postClient.Email.Trim(), StringComparison.InvariantCultureIgnoreCase));
            if (isClientRegistered)
            {
                ModelState.AddModelError("", $"Client with email address {postClient.Email} is already registered");
                return BadRequest(ModelState);
            }

            var client = Mapper.Map<Client>(postClient);            
            var newClient = await _clientRepository.InsertEntity(client);

            newClient.UniqueId = $"AHS/CT/{DateTime.Now.ToString("yy")}/{ newClient.ClientId.ToString("D6")}";
            newClient = await _clientRepository.UpdateEntity(newClient);

            var getClient = Mapper.Map<GetClient>(newClient);
            return CreatedAtAction("GetClient", new { id = getClient.ClientId }, getClient);

        }

        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(GetClient), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            //var client = await _clientRepository.GetEntity(id);
            // var getClient = Mapper.Map<GetClient>(client);
            var getClient = await (from client in _clientRepository.Table
                                   join baseRecItem in _baseRecordItemRepository.Table on client.StatusId equals baseRecItem.BaseRecordItemId
                                   join baseRec in _baseRecordRepository.Table on baseRecItem.BaseRecordId equals baseRec.BaseRecordId
                                   where baseRec.KeyName == "Client_Status" && client.ClientId == id.Value
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       Firstname = client.Firstname,
                                       Middlename = client.Middlename,
                                       Surname = client.Surname,
                                       Email = client.Email,
                                       About = client.About,
                                       Hobbies = client.Hobbies,
                                       StartDate = client.StartDate,
                                       EndDate = client.EndDate,
                                       Keyworker = client.Keyworker,
                                       IdNumber = client.IdNumber,
                                       GenderId = client.GenderId,
                                       NumberOfCalls = client.NumberOfCalls,
                                       AreaCodeId = client.AreaCodeId,
                                       TeritoryId = client.TeritoryId,
                                       ServiceId = client.ServiceId,
                                       ProvisionVenue = client.ProvisionVenue,
                                       PostCode = client.PostCode,
                                       Rate = client.Rate,
                                       TeamLeader = client.TeamLeader,
                                       DateOfBirth = client.DateOfBirth,
                                       Telephone = client.Telephone,
                                       LanguageId = client.LanguageId,
                                       KeySafe = client.KeySafe,
                                       ChoiceOfStaffId = client.ChoiceOfStaffId,
                                       StatusId = client.StatusId,
                                       Status = baseRecItem.ValueName,
                                       CapacityId = client.CapacityId,
                                       ProviderReference = client.ProviderReference,
                                       NumberOfStaff = client.NumberOfStaff,
                                       UniqueId = client.UniqueId,
                                       PassportFilePath = client.PassportFilePath
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getClient);
        }

        [HttpGet]
        [ProducesResponseType(type: typeof(List<GetClient>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClients()
        {

            var getClient = await (from client in _clientRepository.Table
                                   join baseRecItem in _baseRecordItemRepository.Table on client.StatusId equals baseRecItem.BaseRecordItemId
                                   join baseRec in _baseRecordRepository.Table on baseRecItem.BaseRecordId equals baseRec.BaseRecordId
                                   where baseRec.KeyName == "Client_Status"
                                   select new GetClient
                                   {
                                       ClientId = client.ClientId,
                                       Firstname = client.Firstname,
                                       Middlename = client.Middlename,
                                       Surname = client.Surname,
                                       Email = client.Email,
                                       About = client.About,
                                       Hobbies = client.Hobbies,
                                       StartDate = client.StartDate,
                                       EndDate = client.EndDate,
                                       Keyworker = client.Keyworker,
                                       IdNumber = client.IdNumber,
                                       GenderId = client.GenderId,
                                       NumberOfCalls = client.NumberOfCalls,
                                       AreaCodeId = client.AreaCodeId,
                                       TeritoryId = client.TeritoryId,
                                       ServiceId = client.ServiceId,
                                       ProvisionVenue = client.ProvisionVenue,
                                       PostCode = client.PostCode,
                                       Rate = client.Rate,
                                       TeamLeader = client.TeamLeader,
                                       DateOfBirth = client.DateOfBirth,
                                       Telephone = client.Telephone,
                                       LanguageId = client.LanguageId,
                                       KeySafe = client.KeySafe,
                                       ChoiceOfStaffId = client.ChoiceOfStaffId,
                                       StatusId = client.StatusId,
                                       Status = baseRecItem.ValueName,
                                       CapacityId = client.CapacityId,
                                       ProviderReference = client.ProviderReference,
                                       NumberOfStaff = client.NumberOfStaff,
                                       UniqueId = client.UniqueId,
                                       PassportFilePath = client.PassportFilePath
                                   }
                      ).ToListAsync();

            return Ok(getClient);
        }

        [HttpGet("{clientId}")]
        [ProducesResponseType(type: typeof(List<GetClient>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditClient(int? clientId)
        {
            if (!clientId.HasValue)
                return BadRequest("id Parameter is required");
            var getClient = await _clientRepository.Table.Where(c => c.ClientId == clientId).ProjectTo<GetClientForEdit>().FirstOrDefaultAsync();//.Include(c => c.RegulatoryContact).Include(i => i.InvolvingParties).FirstOrDefaultAsync();

            return Ok(getClient);
        }

        [HttpPut("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutClient([FromBody]PutClient model, int? clientId)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = Mapper.Map<Client>(model);
            client.ClientId = clientId.Value;
            _dbContext.Attach(client);
            var properties = model.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                _dbContext.Entry(client).Property(prop.Name).IsModified = true;
            }
            var id = await _dbContext.SaveChangesAsync();
            return Ok(id);
        }
    }
}