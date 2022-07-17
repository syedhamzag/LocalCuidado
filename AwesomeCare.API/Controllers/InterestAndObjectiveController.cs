using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.Interest;
using AwesomeCare.DataTransferObject.DTOs.InterestAndObjective.PersonalityTest;
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
    public class InterestAndObjectiveController : ControllerBase
    {
        private IGenericRepository<InterestAndObjective> _interestRepository;
        private IGenericRepository<Interest> _inteRepository;
        private IGenericRepository<PersonalityTest> _ptestRepository;

        public InterestAndObjectiveController(IGenericRepository<InterestAndObjective> interestRepository, IGenericRepository<Interest> inteRepository, IGenericRepository<PersonalityTest> ptestRepository)
        {
            _interestRepository = interestRepository;
            _inteRepository = inteRepository;
            _ptestRepository = ptestRepository;
        }

        #region CarePlanInterests
        /// <summary>
        /// Get All CarePlanInterests
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetInterestAndObjective>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _interestRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create CarePlanInterests
        /// </summary>
        /// <param name="postCarePlanInterests"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostInterestAndObjective postCarePlanInterests)
        {
            if (postCarePlanInterests == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CarePlanInterests = Mapper.Map<InterestAndObjective>(postCarePlanInterests);
            await _interestRepository.InsertEntity(CarePlanInterests);
            return Ok();
        }
        /// <summary>
        /// Update CarePlanInterests
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostInterestAndObjective models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CarePlanInterests = Mapper.Map<InterestAndObjective>(models);
            await _interestRepository.UpdateEntity(CarePlanInterests);
            return Ok();

        }
        /// <summary>
        /// Get CarePlanInterests by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetInterestAndObjective), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanInterests = await (from c in _interestRepository.Table
                                            where c.ClientId == id.Value
                                            select new GetInterestAndObjective
                                            {
                                                GoalId = c.GoalId,
                                                CareGoal = c.CareGoal,
                                                ClientId = c.ClientId,
                                                Brief = c.Brief,
                                                Interest = (from p in _inteRepository.Table
                                                               where p.GoalId == c.GoalId
                                                               select new GetInterest
                                                               {
                                                                   CommunityActivity = p.CommunityActivity,
                                                                   EventAwarness = p.EventAwarness,
                                                                   GoalAndObjective = p.GoalAndObjective,
                                                                   InformalActivity = p.InformalActivity,
                                                                   InterestId = p.InterestId,
                                                                   LeisureActivity = p.LeisureActivity,
                                                                   MaintainContact = p.MaintainContact,
                                                                   GoalId = p.GoalId,
                                                               }).ToList(),
                                                PersonalityTest = (from p in _ptestRepository.Table
                                                            where p.GoalId == c.GoalId
                                                            select new GetPersonalityTest
                                                            {
                                                                Question = p.Question,
                                                                Answer = p.Answer,
                                                                TestId = p.TestId,
                                                                GoalId = p.GoalId,
                                                            }).ToList(),
                                            }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanInterests);
        }
        /// <summary>
        /// Get CarePlanInterests by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetbyClient/{id}")]
        [ProducesResponseType(type: typeof(GetInterestAndObjective), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getCarePlanInterests = await (from c in _interestRepository.Table
                                              where c.ClientId == id.Value
                                              select new GetInterestAndObjective
                                              {
                                                  GoalId = c.GoalId,
                                                  CareGoal = c.CareGoal,
                                                  ClientId = c.ClientId,
                                                  Brief = c.Brief,
                                                  Interest = (from p in _inteRepository.Table
                                                              where p.GoalId == c.GoalId
                                                              select new GetInterest
                                                              {
                                                                  CommunityActivity = p.CommunityActivity,
                                                                  EventAwarness = p.EventAwarness,
                                                                  GoalAndObjective = p.GoalAndObjective,
                                                                  InformalActivity = p.InformalActivity,
                                                                  InterestId = p.InterestId,
                                                                  LeisureActivity = p.LeisureActivity,
                                                                  MaintainContact = p.MaintainContact,
                                                                  GoalId = p.GoalId,
                                                              }).ToList(),
                                                  PersonalityTest = (from p in _ptestRepository.Table
                                                                     where p.GoalId == c.GoalId
                                                                     select new GetPersonalityTest
                                                                     {
                                                                         Question = p.Question,
                                                                         Answer = p.Answer,
                                                                         TestId = p.TestId,
                                                                         GoalId = p.GoalId,
                                                                     }).ToList(),
                                              }
                      ).FirstOrDefaultAsync();
            return Ok(getCarePlanInterests);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = await _interestRepository.GetEntity(id);
            await _interestRepository.DeleteEntity(entity);
            return Ok();
        }
        #endregion
    }
}
