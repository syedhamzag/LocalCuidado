using System;
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
using AwesomeCare.DataTransferObject.DTOs.ClientComplain;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComplainController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientComplainRegister> _complainRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<ComplainOfficerToAct> _officertoactRepository;
        private IGenericRepository<ComplainStaffName> _staffnameRepository;

        public ComplainController(AwesomeCareDbContext dbContext, IGenericRepository<ClientComplainRegister> complainRepository, IGenericRepository<Client> clientRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository,
        IGenericRepository<ComplainOfficerToAct> officertoactRepository,
        IGenericRepository<ComplainStaffName> staffnameRepository)
        {
            _complainRepository = complainRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _staffRepository = staffRepository;
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
        /// Create Complain
        /// </summary>
        /// <param name="postComplain"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostComplainRegister postComplain)
        {

            if (postComplain == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var complain = Mapper.Map<ClientComplainRegister>(postComplain);
            await _complainRepository.InsertEntity(complain);
            return Ok();
        }
        /// <summary>
        /// Update Medication
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutComplainRegister model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var complain = Mapper.Map<ClientComplainRegister>(model);
            await _complainRepository.UpdateEntity(complain);
            return Ok();


        }
        /// <summary>
        /// Get Complain by ClientId and ComplainId
        /// </summary>
        /// <param name="complainId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("/Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientComplainRegister), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getComplain = await (from complain in _complainRepository.Table
                                                              where complain.ComplainId == id.Value
                                                              select new GetClientComplainRegister
                                                              {
                                                                  ComplainId = complain.ComplainId,
                                                                  Reference = complain.Reference,
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
                                                                  REMARK = complain.REMARK,
                                                                  ROOTCAUSE = complain.ROOTCAUSE,
                                                                  SOURCEOFCOMPLAINTS = complain.SOURCEOFCOMPLAINTS,
                                                                  StatusId = complain.StatusId,
                                                                  OfficerToAct = (from com in _officertoactRepository.Table
                                                                                  join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                                                  where com.ComplainId == complain.ComplainId
                                                                                  select new GetComplainOfficerToAct
                                                                                  {
                                                                                      StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                                      StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                                                  }).ToList(),
                                                                  StaffName = (from com in _staffnameRepository.Table
                                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                                               where com.ComplainId == complain.ComplainId
                                                                               select new GetComplainStaffName
                                                                               {
                                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                                               }).ToList()

                                                              }
                      ).FirstOrDefaultAsync();
            return Ok(getComplain);
        }
        #endregion

    }
}