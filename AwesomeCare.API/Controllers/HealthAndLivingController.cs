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
    public class HealthAndLivingController : ControllerBase
    {

        private IGenericRepository<HealthAndLiving> _healthlivingRepository;

        public HealthAndLivingController(IGenericRepository<HealthAndLiving> healthlivingRepository)
        {
            _healthlivingRepository = healthlivingRepository;
        }
        #region CarePlanHealth
        /// <summary>
        /// Get All CarePlanHealth
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetHealthAndLiving>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _healthlivingRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create CarePlanHealth
        /// </summary>
        /// <param name="postCarePlanHealth"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostHealthAndLiving postCarePlanHealth)
        {
            if (postCarePlanHealth == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanHealth = Mapper.Map<HealthAndLiving>(postCarePlanHealth);
            await _healthlivingRepository.InsertEntity(CarePlanHealth);
            return Ok();
        }
        /// <summary>
        /// Update CarePlanHealth
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostCarePlanHealth models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanHealth = Mapper.Map<HealthAndLiving>(models);
            await _healthlivingRepository.UpdateEntity(CarePlanHealth);
            return Ok();

        }
        /// <summary>
        /// Get CarePlanHealth by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetHealthAndLiving), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHealth = await (from c in _healthlivingRepository.Table
                                           where c.HLId == id.Value
                                           select new GetHealthAndLiving
                                           {
                                                               AbilityToRead = c.AbilityToRead,
                                                               AlcoholicDrink = c.AlcoholicDrink,
                                                               AllowChats = c.AllowChats,
                                                               BriefHealth = c.BriefHealth,
                                                               CareSupport = c.CareSupport,
                                                               ConstraintAttachment = c.ConstraintAttachment,
                                                               ConstraintDetails = c.ConstraintDetails,
                                                               ConstraintRequired = c.ConstraintRequired,
                                                               ContinenceIssue = c.ContinenceIssue,
                                                               ContinenceNeeds = c.ContinenceNeeds,
                                                               ContinenceSource = c.ContinenceSource,
                                                               DehydrationRisk = c.DehydrationRisk,
                                                               EatingWithStaff = c.EatingWithStaff,
                                                               Email = c.Email,
                                                               FamilyUpdate = c.FamilyUpdate,
                                                               FinanceManagement = c.FinanceManagement,
                                                               ClientId = c.ClientId,
                                                               HLId = c.HLId,
                                                               LaundaryRequired = c.LaundaryRequired,
                                                               LetterOpening = c.LetterOpening,
                                                               LifeStyle = c.LifeStyle,
                                                               MeansOfComm = c.MeansOfComm,
                                                               MovingAndHandling = c.MovingAndHandling,
                                                               NeighbourInvolment = c.NeighbourInvolment,
                                                               ObserveHealth = c.ObserveHealth,
                                                               PostalService = c.PostalService,
                                                               PressureSore = c.PressureSore,
                                                               ShoppingRequired = c.ShoppingRequired,
                                                               Smoking = c.Smoking,
                                                               SpecialCaution = c.SpecialCaution,
                                                               SpecialCleaning = c.SpecialCleaning,
                                                               SupportToBed = c.SupportToBed,
                                                               TeaChocolateCoffee = c.TeaChocolateCoffee,
                                                               TextFontSize = c.TextFontSize,
                                                               TVandMusic = c.TVandMusic,
                                                               VideoCallRequired = c.VideoCallRequired,
                                                               WakeUp =c.WakeUp
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHealth);
        }
        /// <summary>
        /// Get CarePlanHealth by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetbyClient/{id}")]
        [ProducesResponseType(type: typeof(GetHealthAndLiving), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanHealth = await (from c in _healthlivingRepository.Table
                                           where c.ClientId == id.Value
                                           select new GetHealthAndLiving
                                           {
                                               AbilityToRead = c.AbilityToRead,
                                               AlcoholicDrink = c.AlcoholicDrink,
                                               AllowChats = c.AllowChats,
                                               BriefHealth = c.BriefHealth,
                                               CareSupport = c.CareSupport,
                                               ConstraintAttachment = c.ConstraintAttachment,
                                               ConstraintDetails = c.ConstraintDetails,
                                               ConstraintRequired = c.ConstraintRequired,
                                               ContinenceIssue = c.ContinenceIssue,
                                               ContinenceNeeds = c.ContinenceNeeds,
                                               ContinenceSource = c.ContinenceSource,
                                               DehydrationRisk = c.DehydrationRisk,
                                               EatingWithStaff = c.EatingWithStaff,
                                               Email = c.Email,
                                               FamilyUpdate = c.FamilyUpdate,
                                               FinanceManagement = c.FinanceManagement,
                                               ClientId = c.ClientId,
                                               HLId = c.HLId,
                                               LaundaryRequired = c.LaundaryRequired,
                                               LetterOpening = c.LetterOpening,
                                               LifeStyle = c.LifeStyle,
                                               MeansOfComm = c.MeansOfComm,
                                               MovingAndHandling = c.MovingAndHandling,
                                               NeighbourInvolment = c.NeighbourInvolment,
                                               ObserveHealth = c.ObserveHealth,
                                               PostalService = c.PostalService,
                                               PressureSore = c.PressureSore,
                                               ShoppingRequired = c.ShoppingRequired,
                                               Smoking = c.Smoking,
                                               SpecialCaution = c.SpecialCaution,
                                               SpecialCleaning = c.SpecialCleaning,
                                               SupportToBed = c.SupportToBed,
                                               TeaChocolateCoffee = c.TeaChocolateCoffee,
                                               TextFontSize = c.TextFontSize,
                                               TVandMusic = c.TVandMusic,
                                               VideoCallRequired = c.VideoCallRequired,
                                               WakeUp = c.WakeUp
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanHealth);
        }
        #endregion
    }
}
