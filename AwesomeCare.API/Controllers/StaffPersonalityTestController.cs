using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffPersonalityTest;
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
    public class StaffPersonalityTestController : ControllerBase
    {
        private IGenericRepository<StaffPersonalityTest> _staffTestRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;

        public StaffPersonalityTestController(IGenericRepository<StaffPersonalityTest> staffTestRepository, IGenericRepository<StaffPersonalInfo> staffRepository)
        {
            _staffTestRepository = staffTestRepository;
            _staffRepository = staffRepository;
        }


        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffPersonalityTest>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _staffTestRepository.Table.ToList();
            return Ok(getEntities);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] List<PostStaffPersonalityTest> post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var test = Mapper.Map<List<StaffPersonalityTest>>(post);
            await _staffTestRepository.InsertEntities(test);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] List<PostStaffPersonalityTest> models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tests = Mapper.Map<List<StaffPersonalityTest>>(models);
            foreach (var test in tests)
            {
                await _staffTestRepository.UpdateEntity(test);
            }           
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(List<GetStaffPersonalityTest>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var entity = _staffTestRepository.Table.Where(s=>s.StaffPersonalInfoId==id.Value).ToList();
            var getentity = Mapper.Map<List<GetStaffPersonalityTest>>(entity);
            return Ok(getentity);
        }

    }
}
