using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientFoodIntake;
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
    public class ClientFoodIntakeController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientFoodIntake> _ClientFoodIntakeRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<FoodIntakeOfficerToAct> _officertoactRepository;
        private IGenericRepository<FoodIntakeStaffName> _staffnameRepository;
        private IGenericRepository<FoodIntakePhysician> _physicianRepository;

        public ClientFoodIntakeController(AwesomeCareDbContext dbContext, IGenericRepository<ClientFoodIntake> ClientFoodIntakeRepository,
                    IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<FoodIntakeOfficerToAct> officertoactRepository,
        IGenericRepository<FoodIntakeStaffName> staffnameRepository, IGenericRepository<FoodIntakePhysician> physicianRepository)
        {
            _ClientFoodIntakeRepository = ClientFoodIntakeRepository;
            _dbContext = dbContext;
            _officertoactRepository = officertoactRepository;
            _staffnameRepository = staffnameRepository;
            _physicianRepository = physicianRepository;
            _staffRepository = staffRepository;
        }
        #region ClientFoodIntake
        /// <summary>
        /// Get All ClientFoodIntake
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientFoodIntake>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _ClientFoodIntakeRepository.Table.ToList();
            return Ok(getEntities);
        }
        /// <summary>
        /// Create ClientFoodIntake
        /// </summary>
        /// <param name="postClientFoodIntake"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientFoodIntake postClientFoodIntake)
        {
            if (postClientFoodIntake == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ClientFoodIntake = Mapper.Map<ClientFoodIntake>(postClientFoodIntake);
            await _ClientFoodIntakeRepository.InsertEntity(ClientFoodIntake);
            return Ok();
        }
        /// <summary>
        /// Update ClientFoodIntake
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientFoodIntake models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.OfficerToAct.ToList())
            {
                var entity = _dbContext.Set<FoodIntakeOfficerToAct>();
                var filterentity = entity.Where(c => c.FoodIntakeId == model.FoodIntakeId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.Physician.ToList())
            {
                var entity = _dbContext.Set<FoodIntakePhysician>();
                var filterentity = entity.Where(c => c.FoodIntakeId == model.FoodIntakeId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.StaffName.ToList())
            {
                var entity = _dbContext.Set<FoodIntakeStaffName>();
                var filterentity = entity.Where(c => c.FoodIntakeId == model.FoodIntakeId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            var result = _dbContext.SaveChanges();

            var ClientFoodIntake = Mapper.Map<ClientFoodIntake>(models);
            await _ClientFoodIntakeRepository.UpdateEntity(ClientFoodIntake);
            return Ok();

        }
        /// <summary>
        /// Get ClientFoodIntake by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientFoodIntake), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientFoodIntake = await (from c in _ClientFoodIntakeRepository.Table
                                           where c.FoodIntakeId == id
                                           select new GetClientFoodIntake
                                           {
                                               FoodIntakeId = c.FoodIntakeId,
                                               Reference = c.Reference,
                                               ClientId = c.ClientId,
                                               Time = c.Time,
                                               Goal = c.Goal,
                                               Date = c.Date,
                                               StatusImage = c.StatusImage,
                                               StatusAttach = c.StatusAttach,
                                               Comment = c.Comment,
                                               PhysicianResponse = c.PhysicianResponse,
                                               CurrentIntake = c.CurrentIntake,
                                               Deadline = c.Deadline,
                                               Remarks = c.Remarks,
                                               Status = c.Status,
                                               OfficerToAct = (from com in _officertoactRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.FoodIntakeId == c.FoodIntakeId
                                                               select new GetFoodIntakeOfficerToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList(),
                                               StaffName = (from com in _staffnameRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.FoodIntakeId == c.FoodIntakeId
                                                            select new GetFoodIntakeStaffName
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList(),
                                               Physician = (from com in _physicianRepository.Table
                                                            join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                            where com.FoodIntakeId == c.FoodIntakeId
                                                            select new GetFoodIntakePhysician
                                                            {
                                                                StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)
                                                            }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientFoodIntake);
        }
        #endregion
    }
}