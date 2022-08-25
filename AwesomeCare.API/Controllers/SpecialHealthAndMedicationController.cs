using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Health;
using AwesomeCare.DataTransferObject.DTOs.Health.Balance;
using AwesomeCare.DataTransferObject.DTOs.Health.HealthAndLiving;
using AwesomeCare.DataTransferObject.DTOs.Health.HistoryOfFall;
using AwesomeCare.DataTransferObject.DTOs.Health.PhysicalAbility;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthAndMedication;
using AwesomeCare.DataTransferObject.DTOs.Health.SpecialHealthCondition;
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
    public class SpecialHealthAndMedicationController : ControllerBase
    {
        private IGenericRepository<SpecialHealthAndMedication> _spmedsRepository;


        public SpecialHealthAndMedicationController(IGenericRepository<SpecialHealthAndMedication> spmedsRepository)
        {
            _spmedsRepository = spmedsRepository;

        }
        #region CarePlanHealth
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetSpecialHealthAndMedication>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _spmedsRepository.Table.ToList();
            return Ok(getEntities);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostSpecialHealthAndMedication postCarePlanHealth)
        {
            if (postCarePlanHealth == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanHealth = Mapper.Map<SpecialHealthAndMedication>(postCarePlanHealth);
            await _spmedsRepository.InsertEntity(CarePlanHealth);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostSpecialHealthAndMedication models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanHealth = Mapper.Map<SpecialHealthAndMedication>(models);
            await _spmedsRepository.UpdateEntity(CarePlanHealth);
            return Ok();

        }
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetSpecialHealthAndMedication), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHealth = await (from c in _spmedsRepository.Table
                                           where c.SHMId == id.Value
                                           select new GetSpecialHealthAndMedication
                                           {
                                               AccessMedication = c.AccessMedication,
                                               AdminLvl = c.AdminLvl,
                                               By = c.By,
                                               Consent = c.Consent,
                                               Date = c.Date,
                                               FamilyMeds = c.FamilyMeds,
                                               FamilyReturnMed = c.FamilyReturnMed,
                                               ClientId = c.ClientId,
                                               LeftoutMedicine = c.LeftoutMedicine,
                                               MedAccessDenial = c.MedAccessDenial,
                                               MedicationAllergy = c.MedicationAllergy,
                                               MedicationStorage = c.MedicationStorage,
                                               MedKeyCode = c.MedKeyCode,
                                               MedsGPOrder = c.MedsGPOrder,
                                               NameFormMedicaiton = c.NameFormMedicaiton,
                                               NoMedAccess = c.NoMedAccess,
                                               OverdoseContact = c.OverdoseContact,
                                               PharmaMARChart = c.PharmaMARChart,
                                               PNRDoses = c.PNRDoses,
                                               PNRMedList = c.PNRMedList,
                                               PNRMedReq = c.PNRMedReq,
                                               PNRMedsAdmin = c.PNRMedsAdmin,
                                               PNRMedsMissing = c.PNRMedsMissing,
                                               SHMId = c.SHMId,
                                               SpecialStorage = c.SpecialStorage,
                                               TempMARChart = c.TempMARChart,
                                               Type = c.Type,
                                               WhoAdminister = c.WhoAdminister
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHealth);
        }
        
        [HttpGet("GetbyClient/{id}")]
        [ProducesResponseType(type: typeof(GetSpecialHealthAndMedication), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHealth = await (from c in _spmedsRepository.Table
                                           where c.ClientId == id.Value
                                           select new GetSpecialHealthAndMedication
                                           {
                                               AccessMedication = c.AccessMedication,
                                               AdminLvl = c.AdminLvl,
                                               By = c.By,
                                               Consent = c.Consent,
                                               Date = c.Date,
                                               FamilyMeds = c.FamilyMeds,
                                               FamilyReturnMed = c.FamilyReturnMed,
                                               ClientId = c.ClientId,
                                               LeftoutMedicine = c.LeftoutMedicine,
                                               MedAccessDenial = c.MedAccessDenial,
                                               MedicationAllergy = c.MedicationAllergy,
                                               MedicationStorage = c.MedicationStorage,
                                               MedKeyCode = c.MedKeyCode,
                                               MedsGPOrder = c.MedsGPOrder,
                                               NameFormMedicaiton = c.NameFormMedicaiton,
                                               NoMedAccess = c.NoMedAccess,
                                               OverdoseContact = c.OverdoseContact,
                                               PharmaMARChart = c.PharmaMARChart,
                                               PNRDoses = c.PNRDoses,
                                               PNRMedList = c.PNRMedList,
                                               PNRMedReq = c.PNRMedReq,
                                               PNRMedsAdmin = c.PNRMedsAdmin,
                                               PNRMedsMissing = c.PNRMedsMissing,
                                               SHMId = c.SHMId,
                                               SpecialStorage = c.SpecialStorage,
                                               TempMARChart = c.TempMARChart,
                                               Type = c.Type,
                                               WhoAdminister = c.WhoAdminister
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHealth);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _spmedsRepository.GetEntity(id);
            await _spmedsRepository.DeleteEntity(entity);
            return Ok();
        }
        #endregion
    }
}
