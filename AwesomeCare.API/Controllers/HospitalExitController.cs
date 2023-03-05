using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.HospitalExit;
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
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HospitalExitController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<HospitalExit> _hospitalExitRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<HospitalExitOfficerToTakeAction> _officertoactRepository;

        public HospitalExitController(AwesomeCareDbContext dbContext, IGenericRepository<HospitalExit> hospitalExitRepository,IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<HospitalExitOfficerToTakeAction> officertoactRepository)
        {
            _hospitalExitRepository = hospitalExitRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffRepository = staffRepository;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetHospitalExit>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _hospitalExitRepository.Table.ToList();
            return Ok(getEntities);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostHospitalExit post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hospital = Mapper.Map<HospitalExit>(post);
            await _hospitalExitRepository.InsertEntity(hospital);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutHospitalExit models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.OfficerToTakeAction.ToList())
            {
                var entity = _dbContext.Set<HospitalExitOfficerToTakeAction>();
                var filterentity = entity.Where(c => c.HospitalExitId == model.HospitalExitId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            var hospital = Mapper.Map<HospitalExit>(models);
            await _hospitalExitRepository.UpdateEntity(hospital);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetHospitalExit), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _hospitalExitRepository.Table
                                           where c.HospitalExitId == id.Value
                                           select new GetHospitalExit
                                           {
                                               HospitalExitId= c.HospitalExitId,
                                               ClientId = c.ClientId,
                                               Reference = c.Reference,
                                               Date = c.Date,
                                               Status = c.Status,
                                               OfficerToTakeAction = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.HospitalExitId == c.HospitalExitId
                                                               select new GetHospitalExitOfficerToTakeAction
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               AreContinentProductNeedAndAvailable = c.AreContinentProductNeedAndAvailable,
                                               AreEqipmentNeededAvailable = c.AreEqipmentNeededAvailable,
                                               AreLocalSupportOrProgramNeeded = c.AreLocalSupportOrProgramNeeded,
                                               AreStaffTrainnedOnEquipmentNeeded = c.AreStaffTrainnedOnEquipmentNeeded,
                                               ConditionOnDischarge = c.ConditionOnDischarge,
                                               ContactIncaseOfReAdmission = c.ContactIncaseOfReAdmission,
                                               IsCarePlanUpdated = c.IsCarePlanUpdated,
                                               IsGrosSriesAvaible = c.IsGrosSriesAvaible,
                                               IsHomeCleaned = c.IsHomeCleaned,
                                               isLittleCashAvailableForServiceUser = c.isLittleCashAvailableForServiceUser,
                                               IsMedicationAvaialable = c.IsMedicationAvaialable,
                                               isRotaTeamInformed = c.isRotaTeamInformed,
                                               IsServiceUseronRota = c.IsServiceUseronRota,
                                               ModeOfMeansOfTrasportBackHome = c.ModeOfMeansOfTrasportBackHome,
                                               NumberOfStaffRequiredOnDischarge = c.NumberOfStaffRequiredOnDischarge,
                                               PurposeofAdmission = c.PurposeofAdmission,
                                               ReablementRequired = c.ReablementRequired,
                                               Remark = c.Remark,
                                               Time = c.Time,
                                               URLLINK = c.URLLINK,
                                               WhichSupportIsNeeded = c.WhichSupportIsNeeded,
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
    }
}
