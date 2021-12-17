using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffTrainingMatrix;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffTrainingMatrixController : ControllerBase
    {
        private IGenericRepository<StaffTrainingMatrix> _StaffTrainingMatrixRepository;
        private ILogger<StaffTrainingMatrixController> _logger;
        public StaffTrainingMatrixController(IGenericRepository<StaffTrainingMatrix> StaffTrainingMatrixRepository, ILogger<StaffTrainingMatrixController> logger)
        {
            _StaffTrainingMatrixRepository = StaffTrainingMatrixRepository;
            _logger = logger;
        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetStaffTrainingMatrix), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = await (from h in _StaffTrainingMatrixRepository.Table
                                   where h.MatrixId == id 
                                   select new GetStaffTrainingMatrix
                                   {
                                       MatrixId = h.MatrixId,
                                       StaffPersonalInfoId = h.StaffPersonalInfoId,
                                       GetTrainingMatrixList = (from l in h.StaffTrainingMatrixList
                                                                select new GetTrainingMatrixList
                                                                {
                                                                    Date = l.Date,
                                                                    TrainingId = l.TrainingId
                                                                }).ToList()
                                       
                                   }).FirstOrDefaultAsync();

            return Ok(getEntity);
        }
        

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffTrainingMatrix>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _StaffTrainingMatrixRepository.Table.ProjectTo<GetStaffTrainingMatrix>().ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostStaffTrainingMatrix model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postEntity = Mapper.Map<StaffTrainingMatrix>(model);
            await _StaffTrainingMatrixRepository.InsertEntity(postEntity);
            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostStaffTrainingMatrix model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                var postEntity = Mapper.Map<StaffTrainingMatrix>(model);
                await _StaffTrainingMatrixRepository.UpdateEntity(postEntity);
                return Ok();
            
        }
    }
}
