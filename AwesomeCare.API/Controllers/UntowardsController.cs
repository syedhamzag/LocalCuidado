using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Migrations;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Untowards;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UntowardsController : ControllerBase
    {
        private IGenericRepository<Model.Models.Untowards> _unTowardsRepository;
        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;
        private IGenericRepository<BaseRecordModel> _baseRecordRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<Client> _clientRepository;
        private IGenericRepository<ClientInvolvingPartyItem> _clientInvolvingPartyRepository;
        private AwesomeCareDbContext _dbContext;
        public UntowardsController(AwesomeCareDbContext dbContext, IGenericRepository<ClientInvolvingPartyItem> clientInvolvingPartyRepository, IGenericRepository<Client> clientRepository, IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<Model.Models.Untowards> unTowardsRepository, IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, IGenericRepository<BaseRecordModel> baseRecordRepository)
        {
            _unTowardsRepository = unTowardsRepository;
            _baseRecordItemRepository = baseRecordItemRepository;
            _baseRecordRepository = baseRecordRepository;
            _dbContext = dbContext;
            _staffRepository = staffRepository;
            _clientRepository = clientRepository;
            _clientInvolvingPartyRepository = clientInvolvingPartyRepository;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(GetUntowards), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUntoWards([FromBody]PostUntowards model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var untowards = Mapper.Map<Model.Models.Untowards>(model);

            untowards.TicketNumber = DateTime.Now.ToString("yyyyMMddhhmmss");
            var entity = await _unTowardsRepository.InsertEntity(untowards);


            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(type: typeof(List<GetUntowards>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var entities = _unTowardsRepository.Table.ProjectTo<GetUntowards>().ToList();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(type: typeof(GetUntowardsDetails), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            var entity = (from un in _unTowardsRepository.Table
                          join involvingParty in _clientInvolvingPartyRepository.Table on un.TypeofRequiredNotification equals involvingParty.ClientInvolvingPartyItemId
                          join client in _clientRepository.Table on un.HomeCareClientId equals client.ClientId
                          where un.UntowardsId == id
                          select new GetUntowardsDetails
                          {
                              UntowardsId = un.UntowardsId,
                              ActionStatus = un.ActionStatus,
                              Attachment =un.Attachment,
                              Date = un.Date,
                              Details = un.Details,
                              PersonReporting = un.PersonReporting,
                              PersonReportingEmail = un.PersonReportingEmail,
                              PersonReportingTelephone = un.PersonReportingTelephone,
                              Priority = un.Priority,
                              Subject = un.Subject,
                              TicketNumber = un.TicketNumber,
                              TimeOfCall = un.TimeOfCall,
                              ActionRequired = un.ActionRequired,
                              ActionTaken = un.ActionTaken,
                              ExpectedDateAndTimeOfFeedback = un.ExpectedDateAndTimeOfFeedback,
                              FinalActionToCloseCase = un.FinalActionToCloseCase,
                              HomeCareClient =string.Concat(client.Firstname," ",client.Middlename," ",client.Surname),
                              HospitalEntryReason = un.HospitalEntryReason,
                              HospitalExitDetails = un.HospitalExitDetails,
                              IsBlackListRequired = un.IsBlackListRequired?"Yes":"No",
                              IsHospitalEntry = un.IsHospitalEntry ? "Yes" : "No",
                              IsHospitalExit = un.IsHospitalExit ? "Yes" : "No",
                              Others =un.Others,
                              ShouldNotifyInvolvingStaff = un.ShouldNotifyInvolvingStaff ? "Yes" : "No",
                              TypeofRequiredNotification= involvingParty.ItemName,
                              OfficerToAct = (from officer in un.OfficerToAct
                                              join staff in _staffRepository.Table on officer.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                              select new GetUntowardsOfficerToAct
                                              {
                                                  StaffPersonalInfoId = officer.StaffPersonalInfoId,
                                                  Staff = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName),
                                                  UntowardsId = officer.UntowardsId,
                                                  UntowardsOfficerToActId = officer.UntowardsOfficerToActId
                                              }).ToList(),
                              StaffInvolved = (from staffinv in un.StaffInvolved
                                               join staff in _staffRepository.Table on staffinv.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                               select new GetUntowardsStaffInvolved
                                               {
                                                   StaffPersonalInfoId = staffinv.StaffPersonalInfoId,
                                                   Staff = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName),
                                                   UntowardsId = staffinv.UntowardsId,
                                                   UntowardsStaffInvolvedId = staffinv.UntowardsStaffInvolvedId
                                               }).ToList()
                          }).FirstOrDefault();
            return Ok(entity);
        }
    }
}