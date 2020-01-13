using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffInfoController : ControllerBase
    {
        private IGenericRepository<StaffPersonalInfo> _staffInfoRepository;
        private ILogger<BaseRecordController> _logger;
        public StaffInfoController(IGenericRepository<StaffPersonalInfo> staffInfoRepository, ILogger<BaseRecordController> logger)
        {
            _staffInfoRepository = staffInfoRepository;
            _logger = logger;
        }

        [HttpGet("{id}",Name ="GetStaffById")]
        [ProducesResponseType(type: typeof(GetStaffPersonalInfo), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int? id)
        {

            var entity =await _staffInfoRepository.GetEntity(id);
            var getEntity = Mapper.Map<GetStaffPersonalInfo>(entity);
            return Ok(getEntity);
        }
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffPersonalInfo>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {

            var entity = await _staffInfoRepository.GetEntities();
            var getEntity = Mapper.Map<List<GetStaffPersonalInfo>>(entity);
            return Ok(getEntity);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(GetStaffPersonalInfo), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody]PostStaffPersonalInfo model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEmailRegistered = _staffInfoRepository.Table.Any(s => s.Email.Equals(model.Email));
            if (isEmailRegistered)
            {
                ModelState.AddModelError("Email", $"Email {model.Email} is registered");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<StaffPersonalInfo>(model);
            var entity = await _staffInfoRepository.InsertEntity(postEntity);
            entity.RegistrationId = $"AHS/ST/{DateTime.Now.ToString("yy")}/{ entity.StaffPersonalInfoId.ToString("D6")}";
            entity = await _staffInfoRepository.UpdateEntity(entity);

            return CreatedAtAction("GetAsync", new { id = entity.StaffPersonalInfoId }, entity);
            
        }
    }
}