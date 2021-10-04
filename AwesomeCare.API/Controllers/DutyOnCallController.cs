using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.DutyOnCall;
using AwesomeCare.Model.Models;
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
    public class DutyOnCallController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<DutyOnCall> _DutyOnCallRepository;
        private IGenericRepository<DutyOnCallPersonToAct> _DutyOnCallStaffRepository;
        private IGenericRepository<DutyOnCallPersonResponsible> _DutyOnCallPersonRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;

        public DutyOnCallController(AwesomeCareDbContext dbContext, IGenericRepository<DutyOnCall> DutyOnCallRepository, IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<DutyOnCallPersonToAct> DutyOnCallStaffRepository, IGenericRepository<DutyOnCallPersonResponsible> DutyOnCallPersonRepository)
        {

            _dbContext = dbContext;
            _staffRepository = staffRepository;
            _DutyOnCallRepository = DutyOnCallRepository;
            _DutyOnCallPersonRepository = DutyOnCallPersonRepository;
            _DutyOnCallStaffRepository = DutyOnCallStaffRepository;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetDutyOnCall>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _DutyOnCallRepository.Table.ToList();
            return Ok(getEntities);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostDutyOnCall post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var DutyOnCall = Mapper.Map<DutyOnCall>(post);
            await _DutyOnCallRepository.InsertEntity(DutyOnCall);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutDutyOnCall models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.PersonToAct.ToList())
            {
                var entity = _dbContext.Set<DutyOnCallPersonToAct>();
                var filterentity = entity.Where(c => c.DutyOnCallId == model.DutyOnCallId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            var DutyOnCall = Mapper.Map<DutyOnCall>(models);
            await _DutyOnCallRepository.UpdateEntity(DutyOnCall);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetDutyOnCall), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _DutyOnCallRepository.Table
                                   where c.DutyOnCallId == id.Value
                                   select new GetDutyOnCall
                                   {
                                       DutyOnCallId = c.DutyOnCallId,
                                       ActionTaken = c.ActionTaken,
                                       ClientId = c.ClientId,
                                       ClientInitial = c.ClientInitial,
                                       DateOfCall = c.DateOfCall,
                                       DateOfIncident = c.DateOfIncident,
                                       DetailsOfIncident = c.DetailsOfIncident,
                                       DetailsRequired = c.DetailsRequired,
                                       NotifyPerson = c.NotifyPerson,
                                       NotifyStaffInvolved = c.NotifyStaffInvolved,
                                       PositionOfReporting = c.PositionOfReporting,
                                       Priority = c.Priority,
                                       RefNo = c.RefNo,
                                       Remarks = c.Remarks,
                                       ReportedBy = c.ReportedBy,
                                       StaffBlacklisted = c.StaffBlacklisted,
                                       Subject = c.Subject,
                                       TelephoneToCall = c.TelephoneToCall,
                                       TimeOfCall = c.TimeOfCall,
                                       TypeOfDutyCall = c.TypeOfDutyCall,
                                       TypeOfIncident = c.TypeOfIncident,
                                       Status = c.Status,
                                       Attachment = c.Attachment,
                                       
                                       PersonToAct = (from com in _DutyOnCallStaffRepository.Table
                                                        join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                        where com.DutyOnCallId == c.DutyOnCallId
                                                        select new GetDutyOnCallPersonToAct
                                                        {
                                                            StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                            StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                        }).ToList(),
                                       PersonResponsible = (from com in _DutyOnCallPersonRepository.Table
                                                             join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                             where com.DutyOnCallId == c.DutyOnCallId
                                                             select new GetDutyOnCallPersonResponsible
                                                             {
                                                                 StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                 StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                             }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
    }
}
