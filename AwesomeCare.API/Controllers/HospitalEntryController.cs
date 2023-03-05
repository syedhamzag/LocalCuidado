using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.HospitalEntry;
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
    public class HospitalEntryController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<HospitalEntry> _hospitalEntryRepository;
        private IGenericRepository<HospitalEntryStaffInvolved> _hospitalEntryStaffRepository;
        private IGenericRepository<HospitalEntryPersonToTakeAction> _hospitalEntryPersonRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;

        public HospitalEntryController(AwesomeCareDbContext dbContext, IGenericRepository<HospitalEntry> hospitalEntryRepository, IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<HospitalEntryStaffInvolved> hospitalEntryStaffRepository, IGenericRepository<HospitalEntryPersonToTakeAction> hospitalEntryPersonRepository)
        {

            _dbContext = dbContext;
            _staffRepository = staffRepository;
            _hospitalEntryRepository = hospitalEntryRepository;
            _hospitalEntryPersonRepository = hospitalEntryPersonRepository;
            _hospitalEntryStaffRepository = hospitalEntryStaffRepository;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetHospitalEntry>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities =  _hospitalEntryRepository.Table.ToList();
            return Ok(getEntities);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostHospitalEntry post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var hospitalEntry = Mapper.Map<HospitalEntry>(post);
            await _hospitalEntryRepository.InsertEntity(hospitalEntry);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutHospitalEntry models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.StaffInvolved.ToList())
            {
                var entity = _dbContext.Set<HospitalEntryStaffInvolved>();
                var filterentity = entity.Where(c => c.HospitalEntryId == model.HospitalEntryId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            var hospitalEntry = Mapper.Map<HospitalEntry>(models);
            await _hospitalEntryRepository.UpdateEntity(hospitalEntry);
            return Ok();

        }
        
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetHospitalEntry), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _hospitalEntryRepository.Table
                                           where c.HospitalEntryId == id.Value
                                           select new GetHospitalEntry
                                           {
                                               HospitalEntryId = c.HospitalEntryId,
                                               ClientId = c.ClientId,
                                               Reference = c.Reference,
                                               Date = c.Date,
                                               Status = c.Status,
                                               Attachment = c.Attachment,
                                               CauseofAdmission = c.CauseofAdmission,
                                               ConditionOfAdmission = c.ConditionOfAdmission,
                                               IsFamilyInformed = c.IsFamilyInformed,
                                               IsHomeCleaned = c.IsHomeCleaned,
                                               LastDateofAdmission = c.LastDateofAdmission,
                                               MeansOfTransport = c.MeansOfTransport,
                                               NameParamedicStaff = c.NameParamedicStaff,
                                               ParamicStaffTeamNo = c.ParamicStaffTeamNo,
                                               PossibleDateReturn = c.PossibleDateReturn,
                                               PurposeofAdmission = c.PurposeofAdmission,
                                               Remark = c.Remark,
                                               Time = c.Time,
                                               URLLINK = c.URLLINK,
                                               StaffInvolved = (from com in _hospitalEntryStaffRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.HospitalEntryId == c.HospitalEntryId
                                                               select new GetHospitalEntryStaffInvolved
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               PersonToTakeAction = (from com in _hospitalEntryPersonRepository.Table
                                                                join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                                where com.HospitalEntryId == c.HospitalEntryId
                                                                select new GetHospitalEntryPersonToTakeAction
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
